using _0_Framework.Infrastructure;


namespace PartManagement.Configuration.Permissions
{
    public class PartPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                     "Part", new List<PermissionDto>
                     {
                         new PermissionDto(PartPermissions.SearchPart, "SearchParts"),
                         new PermissionDto(PartPermissions.ListPart, "ListParts"),
                         new PermissionDto(PartPermissions.CreatePart, "CreateParts"),
                         new PermissionDto(PartPermissions.EditPart, "EditParts")
                     }
                },
                {
                    "StockType", new List<PermissionDto>
                     {
                         new PermissionDto(PartPermissions.SearchStockType, "SearchStockTypes"),
                         new PermissionDto(PartPermissions.ListStockType, "ListStockTypes"),
                         new PermissionDto(PartPermissions.CreateStockType, "CreateStockTypes"),
                         new PermissionDto(PartPermissions.EditStockType, "EditStockTypes")
                     }
                },
                {
                    "EquipmentType", new List<PermissionDto>
                     {
                         new PermissionDto(PartPermissions.SearchEquipmentType, "SearchEquipmentTypes"),
                         new PermissionDto(PartPermissions.ListEquipmentType, "ListEquipmentTypes"),
                         new PermissionDto(PartPermissions.CreateEquipmentType, "CreateEquipmentTypes"),
                         new PermissionDto(PartPermissions.EditEquipmentType, "EditEquipmentTypes")
                     }
                }

            };
        }
    }
}
