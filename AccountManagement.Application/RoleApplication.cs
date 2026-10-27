using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Application
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IAuthHelper _authHelper;
        private readonly IEnumerable<IPermissionExposer> _exposers;

        public RoleApplication(
            IRoleRepository roleRepository,
            IAuthHelper authHelper,
            IEnumerable<IPermissionExposer> exposers)
        {
            _roleRepository = roleRepository;
            _authHelper = authHelper;
            _exposers = exposers;
        }

        public OperationResult Create(CreateRole command)
        {
            var operation = new OperationResult();

            bool? isSystemAdmin = _authHelper.CurrentAccountIsSystemAdminOrNull();
            if (isSystemAdmin is null)
                return operation.Failed(ApplicationMessages.NotAllowToCreate);

            if (_roleRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var permissions = MapCodesToPermissions(command.PermissionCodes);
            var role = new Role(command.Name, permissions, command.Scope);

            _roleRepository.Create(role);
            _roleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditRole command)
        {
            var operation = new OperationResult();

            var query = new RoleQueryDto { MaxScope = _authHelper.MaxRoleScope };
            var role = _roleRepository.GetForEdit(command.Id, query);

            bool? isSystemAdmin = _authHelper.CurrentAccountIsSystemAdminOrNull();
            if (isSystemAdmin is null)
                return operation.Failed(ApplicationMessages.UnKnownRole);

            if (role is null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (role.IsSystemAdminOnly && !isSystemAdmin.Value)
                return operation.Failed(ApplicationMessages.NotAllowToEdit);

            if (_roleRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var permissions = MapCodesToPermissions(command.PermissionCodes);
            role.Edit(command.Name, permissions, command.Scope);   // ← سه پارامتر کامل

            _roleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditRole GetDetails(long id)
        {
            // سقف واقعی را از کاربر جاری بگیر (امنتر از اعتماد به مقدار آمده از بیرون)
            var query = new RoleQueryDto();
            query.MaxScope = _authHelper.MaxRoleScope;
            query.AllowedScopes = Roles.GetAllowedScopes(query.MaxScope).ToList();

            var role = _roleRepository.GetDetails(id, query);
            if (role is null) return null;

            // ✅ سطحهای مجاز برای دراپ‌داون در ViewModel
            role.AllowedScopes = query.AllowedScopes;
            return role;
        }

        public List<RoleViewModel> GetRoles()
        {
            var query = new RoleQueryDto();
            query.MaxScope = _authHelper.MaxRoleScope;
            query.AllowedScopes = Roles.GetAllowedScopes(query.MaxScope).ToList();

            if (query is null) return null;

            return _roleRepository.GetRoles(query);
        }

        //public Task<List<RoleViewModel>> GetRoles()
        //{
        //    var query = new RoleQueryDto { MaxScope = _authHelper.MaxRoleScope };
        //    return Task.FromResult(_roleRepository.GetRoles(query));
        //}

        // ----------------- Helper Methods -----------------

        private List<Permission> MapCodesToPermissions(List<long> codes)
        {
            var result = new List<Permission>();
            if (codes == null || !codes.Any())
                return result;

            var all = _exposers
                .SelectMany(x => x.Expose().SelectMany(kv => kv.Value))
                .ToList();

            foreach (var code in codes.Distinct())
            {
                var dto = all.FirstOrDefault(x => x.Code == code);
                var name = dto?.Name ?? code.ToString();
                result.Add(new Permission(code, name));
            }
            return result;
        }
    }
}