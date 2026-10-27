using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Role
{
    public interface IRoleApplication
    {
        OperationResult Create(CreateRole command);
        OperationResult Edit(EditRole command);
        // ✅ نسخه‌ی جدید: هم جزئیات را میدهد هم AllowedScopes را پر میکند
        EditRole GetDetails(long id);

        // ✅ لیست بر اساس سقف دسترسی کاربر
        List<RoleViewModel> GetRoles();
    }
}
