using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IEnumerable<IPermissionExposer> _exposers;

        public RoleApplication(IRoleRepository roleRepository, IEnumerable<IPermissionExposer> exposers)
        {
            _roleRepository = roleRepository;
            _exposers = exposers;
        }

        public OperationResult Create(CreateRole command)
        {
            var operation = new OperationResult();
            if (_roleRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var permissions = MapCodesToPermissions(command.Permissions);
            var role = new Role(command.Name, permissions);
            _roleRepository.Create(role);
            _roleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditRole command)
        {
            var operation = new OperationResult();
            var role = _roleRepository.Get(command.Id);
            if (role == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_roleRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var permissions = MapCodesToPermissions(command.Permissions);
            role.Edit(command.Name, permissions);
            _roleRepository.SaveChanges();
            return operation.Succeeded();
        }

        private List<Permission> MapCodesToPermissions(List<int> codes)
        {
            var result = new List<Permission>();
            if (codes == null) return result;

            // جمع کردن همه دسترسی‌های همه ماژول‌ها از همه Exposer ها
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

        public EditRole GetDetails(long id) => _roleRepository.GetDetails(id);
        public List<RoleViewModel> List() => _roleRepository.List();
    }
}