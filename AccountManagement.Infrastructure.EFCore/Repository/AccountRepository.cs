using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
    {
        private readonly AccountContext _context;
        public AccountRepository(AccountContext context) : base(context) 
        {
            _context = context;
        }

        public Account GetBy(string username)
        {
            return _context.Accounts.FirstOrDefault(x => x.Username == username);
        }

        public EditAccount GetDetails(long id)
        {
            return _context.Accounts.Select(x => new EditAccount
            {
                Id = x.Id,  
                Fullname = x.Fullname,
                Mobile = x.Mobile,
                RoleId = x.RoleId,
                Username = x.Username,
                OrganizationId = x.OrganizationId
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel, long organizationId)
        {
            var query = _context.Accounts.Include(x => x.Role).Select(x => new AccountViewModel
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Mobile = x.Mobile,
                ProfilePhoto = x.ProfilePhoto,
                Role = x.Role.Name,
                RoleId = x.Role.Id,
                Username = x.Username,
                CreationDate = x.CreationDate.ToShortDateString(),
                OrganizationId = x.OrganizationId
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Fullname))
                query = query.Where(x => x.Fullname.Contains(searchModel.Fullname));

            if (!string.IsNullOrWhiteSpace(searchModel.Username))
                query = query.Where(x => x.Username.Contains(searchModel.Username));

            if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
                query = query.Where(x => x.Mobile.Contains(searchModel.Mobile));

            if (searchModel.RoleId != 0)
                query = query.Where(x => x.RoleId == searchModel.RoleId);

            // در این شرط در صورتی که آی دی سازمان اکانت وارد شده صفر باشد دسترسی به تمام اکانت هایی که آیدی آنها غیر صفر است دارد و
            // در غیر این صورت یعنی زمانی که آیدی سازمان اکانت وارد شده صفر نباشد فقط اکانت هایی را میتواند مشاهده کند که 
            // آیدی سازمان آن با آیدی سازمان لیست اکانت ها یکی باشد.
            if (organizationId != 0)
                query = query.Where(x => x.OrganizationId == organizationId);

            if (organizationId == 0 && searchModel.OrganizationId != 0)
                    query = query.Where(x => x.OrganizationId == searchModel.OrganizationId);

                

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
