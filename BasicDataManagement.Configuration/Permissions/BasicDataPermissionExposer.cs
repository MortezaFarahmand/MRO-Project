using _0_Framework.Infrastructure;
using System.Collections.Generic;

namespace BasicDataManagement.Configuration.Permissions
{
    public class BasicDataPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                     "Country", new List<PermissionDto>
                     {
                         new PermissionDto(BasicDataPermissions.SearchCountrys, "SearchCountrys"),
                         new PermissionDto(BasicDataPermissions.ListCountrys, "ListCountrys"),
                         new PermissionDto(BasicDataPermissions.CreateCountrys, "CreateCountrys"),
                         new PermissionDto(BasicDataPermissions.EditCountrys, "EditCountrys"),
                     }
                },
                {
                    "City", new List<PermissionDto>
                     {
                         new PermissionDto(BasicDataPermissions.SearchCitys, "SearchCitys"),
                         new PermissionDto(BasicDataPermissions.ListCitys, "ListCitys"),
                         new PermissionDto(BasicDataPermissions.CreateCitys, "CreateCitys"),
                         new PermissionDto(BasicDataPermissions.EditCitys, "EditCitys"),
                     }
                }

            };
        }
    }
}
