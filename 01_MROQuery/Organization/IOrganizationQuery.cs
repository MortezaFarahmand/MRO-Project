namespace _01_MROQuery.Contracts.Organization
{
    public interface IOrganizationQuery
    {
        string GetNameEn(long organizationId);
        string GetNameFa(long organizationId);
        Dictionary<long, string> GetNames(List<long> organizationIds);
    }
}