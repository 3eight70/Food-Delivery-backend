using webNET_Hits_backend_aspnet_project_1.Data.Models;
using webNET_Hits_backend_aspnet_project_1.Models;

namespace webNET_Hits_backend_aspnet_project_1.Services;

public class AddressService: IAddressService
{
    private readonly GarContext _context;

    public AddressService(GarContext context)
    {
        _context = context;
    }

    public SearchAddressModel[] Search(Int64 parentObjectId, string? query)
    {
        var addresses = (from AsAdmHierarchy in _context.AsAdmHierarchies
            join AsAddrObj in _context.AsAddrObjs on AsAdmHierarchy.Objectid equals AsAddrObj.Objectid
            where AsAdmHierarchy.Parentobjid == parentObjectId && (AsAddrObj.Name.Contains(query) || string.IsNullOrEmpty(query))
            select new SearchAddressModel
            {
                ObjectId = AsAddrObj.Objectid,
                ObjectGuid = AsAddrObj.Objectguid,
                Text = AsAddrObj.Typename + " " + AsAddrObj.Name,
                ObjectLevel = GarAddressLevel.Building,
                ObjectLevelText = ""
            }).ToArray();

        return addresses;
    }
}