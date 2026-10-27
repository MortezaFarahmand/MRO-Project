using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Security.Principal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccountRepository _accountRepository;
        private readonly IAuthHelper _authHelper;

        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher,
            IFileUploader fileUploader, IAuthHelper authHelper)
        {
            _authHelper = authHelper;
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
        }



        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (command.Password == command.RePassword)
                return operation.Failed(ApplicationMessages.PasswordsNotMatch);

            var password = _passwordHasher.Hash(command.Password);
            account.ChangePassword(password);
            _accountRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Register(RegisterAccount command)
        {
            var operation = new OperationResult();
            // اگر آیدی سازمان اکانت وارد شده صفر باشد یعنی ادمین کل سیستم وارد شده باشد، همان آیدی که در
            // سلکت لیست سازمان در پنجره ایجاد اکانت انتخاب شده باشد ثبت خواهد شد، در غیر این صورت آیدی سازمان
            // اکانتی که ایجاد میشود همان آیدی سازمان اکانتی است که قصد ایجاد اکانت را دارد ثبت خواهد شد.
            //...
            long? accountOrgId = _authHelper.SetOrganizationId(command.OrganizationId);
            bool? accountIsSysAdmin = _authHelper.CurrentAccountIsSystemAdminOrNull();
            bool accountIsOrgAdmin = _authHelper.CurrentAccountIsOrganizationAdmin();

            if (accountIsSysAdmin is null)
                return operation.Failed(ApplicationMessages.NotAllowToCreate);

            if (accountIsSysAdmin == false || !(accountIsOrgAdmin && accountOrgId == command.OrganizationId))
                return operation.Failed(ApplicationMessages.NotAllowToCreate);

            if (_accountRepository.Exists(x => x.Username == command.Username || x.Mobile == command.Mobile))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var password = _passwordHasher.Hash(command.Password);
            var path = $"profilePhotos";
            var picturePath = _fileUploader.Upload(command.ProfilePhoto, path);

            var account = new Account(command.Fullname, command.Username, password,
                              command.Mobile, picturePath, accountOrgId);
            // نقش‌ها را بعد از ساخت Account اضافه کن (نه قبل از Save)
            foreach (var roleId in command.RoleIds.Distinct())
            {
                account.AddRoles(new AccountRole(account.Id, roleId)); // Id هنوز صفر است اما...
            }

            _accountRepository.Create(account);
            _accountRepository.SaveChanges();   // حالا Id واقعی تولید می‌شود
            return operation.Succeeded();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);

            long? accountOrgId = _authHelper.SetOrganizationId(command.OrganizationId);
            bool? accountIsSysAdmin = _authHelper.CurrentAccountIsSystemAdminOrNull();
            bool accountIsOrgAdmin = _authHelper.CurrentAccountIsOrganizationAdmin();

            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (accountIsSysAdmin is null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_accountRepository.Exists(x => (x.Username == command.Username || x.Mobile == command.Mobile) && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            //...
            if (accountIsSysAdmin == true || (accountIsOrgAdmin 
                    && accountOrgId == command.OrganizationId))
                accountIsOrgAdmin = command.IsOrganizationAdmin;
            else
                accountIsOrgAdmin = account.IsOrganizationAdmin;

            if (accountIsSysAdmin == true)
                accountIsSysAdmin = command.IsSystemAdmin;
            else
                accountIsSysAdmin = account.IsSystemAdmin;
            //

            var path = $"profilePhotos";
            var picturePath = _fileUploader.Upload(command.ProfilePhoto, path);

            // مهم: سینک نقش‌ها
            var accountRoles = command.RoleIds
                .Select(roleId => new AccountRole(account.Id, roleId))
                .ToList();
            account.SetRoles(accountRoles);

            account.Edit(command.Fullname, command.Username, command.Mobile,
                picturePath, accountOrgId, accountIsSysAdmin, accountIsOrgAdmin);

            _accountRepository.SaveChanges();

            return operation.Succeeded();
        }

        public EditAccount GetDetails(long id)
        {
            return _accountRepository.GetDetails(id);
        }

        public OperationResult Login(Login command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.Username);
            if (account == null)
                return operation.Failed(ApplicationMessages.WrongUserPass);

            var result = _passwordHasher.Check(account.Password, command.Password);
            if (!result.Verified)
                return operation.Failed(ApplicationMessages.WrongUserPass);

            // SelectMany پهن کردن دسترسی همه نقش‌ها توی یه لیست
            // DistinctBy حذف تکراری‌ها
            // Select تبدیل به DTO برای نمایش
            var permissions = account.AccountRoles
                .SelectMany(ar => ar.Role.Permissions)
                .DistinctBy(p => p.Code)
                .Select(p => p.Code)
                .ToList();

            var roleIds = account.AccountRoles.Select(ar => ar.RoleId).ToList();

            // محاسبه Scopeهای نقش‌های کاربر
            var roleScopes = account.AccountRoles
                .Select(ar => ar.Role.Scope)
                .Distinct()
                .ToList();

            var authViewModel = new AuthViewModel(
                account.Id,
                roleIds,
                account.Fullname,
                account.Username,
                permissions,                 // List<long>
                account.OrganizationId,
                account.IsSystemAdmin,
                account.IsOrganizationAdmin)
            { RoleScopes = roleScopes };     // ← اضافه شد

            _authHelper.Signin(authViewModel);
            return operation.Succeeded();
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            //// برای کاربر عادی و ادمین سازمان، سازمان خودش را در دراپ‌داون انتخاب کن
            //long? currentOrgId = _authHelper.CurrentAccountOrganizationId();
            //bool isSystemAdmin = _authHelper.CurrentAccountIsSystemAdmin();

            //searchModel.IsSystemAdmin = isSystemAdmin;
            //if (!isSystemAdmin)
            //{ currentOrgId = searchModel.OrganizationId; }

            //return _accountRepository.Search(searchModel, currentOrgId);
            bool? isSystemAdmin = _authHelper.CurrentAccountIsSystemAdminOrNull();
            long? currentOrgId = _authHelper.CurrentAccountOrganizationId();

            if (isSystemAdmin is null)
                return new List<AccountViewModel>();

            searchModel.IsSystemAdmin = isSystemAdmin.Value;

            if (isSystemAdmin == false)
                 searchModel.OrganizationId = currentOrgId;

            return _accountRepository.Search(searchModel, currentOrgId);
        }

        
    }
}
