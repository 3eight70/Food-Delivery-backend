using Microsoft.EntityFrameworkCore;
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
        List<SearchAddressModel> addresses = new List<SearchAddressModel>();
        if (query == null){
        addresses = (from AsAdmHierarchy in _context.AsAdmHierarchies
            where AsAdmHierarchy.Parentobjid == parentObjectId
            from AsHouse in _context.AsHouses.Where(house => AsAdmHierarchy.Objectid == house.Objectid).DefaultIfEmpty()
            from AsAddrObj in _context.AsAddrObjs.Where(obj => AsAdmHierarchy.Objectid == obj.Objectid).DefaultIfEmpty()
            where (AsAddrObj != null && (AsAddrObj.Name.Contains(query)) ||
                   (AsHouse != null && AsHouse.Housenum.Contains(query)) || string.IsNullOrEmpty(query))

            select new SearchAddressModel
            {
                ObjectId = AsAddrObj != null ? AsAddrObj.Objectid : AsHouse.Objectid,
                ObjectGuid = AsAddrObj != null ? AsAddrObj.Objectguid : AsHouse.Objectguid,
                Text = AsAddrObj != null ? AsAddrObj.Typename + " " + AsAddrObj.Name : AsHouse.Housenum,
                ObjectLevelText = AsAddrObj != null ? AsAddrObj.Level : null
            }).Distinct().Take(10).ToList();
        }
        else
        {
            addresses = (from AsAdmHierarchy in _context.AsAdmHierarchies
                where AsAdmHierarchy.Parentobjid == parentObjectId
                from AsHouse in _context.AsHouses.Where(house => AsAdmHierarchy.Objectid == house.Objectid).DefaultIfEmpty()
                from AsAddrObj in _context.AsAddrObjs.Where(obj => AsAdmHierarchy.Objectid == obj.Objectid).DefaultIfEmpty()
                where (AsAddrObj != null && (AsAddrObj.Name.Contains(query)) ||
                       (AsHouse != null && AsHouse.Housenum.Contains(query)) || string.IsNullOrEmpty(query))

                select new SearchAddressModel
                {
                    ObjectId = AsAddrObj != null ? AsAddrObj.Objectid : AsHouse.Objectid,
                    ObjectGuid = AsAddrObj != null ? AsAddrObj.Objectguid : AsHouse.Objectguid,
                    Text = AsAddrObj != null ? AsAddrObj.Typename + " " + AsAddrObj.Name : AsHouse.Housenum,
                    ObjectLevelText = AsAddrObj != null ? AsAddrObj.Level : null
                }).Distinct().ToList();
        }

        foreach (SearchAddressModel el in addresses)
        {
            AddressObjectLevel objLevel = AddressObjectLevels(el.ObjectLevelText);
            el.ObjectLevel = objLevel.ObjectLevel;
            el.ObjectLevelText = objLevel.ObjectLevelText;
        }

        return addresses.ToArray();
    }


    public SearchAddressModel[] SearchAddressChain(Guid objectGuid)
    {
        var address = _context.AsAddrObjs.FirstOrDefault(ad => ad.Objectguid == objectGuid);
        if (address == null)
        {
            throw new InvalidOperationException("Invalid objectGuid");
        }
        var addressFromHierarchy = _context.AsAdmHierarchies.FirstOrDefault(ad => ad.Objectid == address.Objectid);
        List<AsAdmHierarchy> addressList = new List<AsAdmHierarchy>();
        addressList = GetPath(addressList, addressFromHierarchy.Objectid);

        List<SearchAddressModel> addresses = new List<SearchAddressModel>();

        foreach (AsAdmHierarchy el in addressList)
        {
            var curObject = _context.AsAddrObjs.FirstOrDefault(obj => obj.Objectid == el.Objectid);
            addresses.Add(new SearchAddressModel
            {
                ObjectId = curObject.Objectid,
                ObjectGuid = curObject.Objectguid,
                Text = curObject.Typename + " " + curObject.Name,
                ObjectLevel = AddressObjectLevels(curObject.Level).ObjectLevel,
                ObjectLevelText = AddressObjectLevels(curObject.Level).ObjectLevelText
            });
        }

        return addresses.ToArray();
    }

    private List<AsAdmHierarchy> GetPath(List<AsAdmHierarchy> addressList, Int64? objectId)
    {
        var currentObject = _context.AsAdmHierarchies.FirstOrDefault(obj => obj.Objectid == objectId);
        if (currentObject == null)
        {
            return addressList;
        }
        addressList.Add(currentObject);
        
        var parentObject = _context.AsAdmHierarchies.FirstOrDefault(obj => obj.Objectid == currentObject.Parentobjid);
        if (parentObject == null)
        {
            return addressList;
        }
        
        GetPath(addressList, parentObject.Objectid);

        return addressList;
    }


    private AddressObjectLevel AddressObjectLevels(string level)
    {
        switch (level)
        {
            case "1":
                return new AddressObjectLevel
                {
                    ObjectLevel = GarAddressLevel.Region,
                    ObjectLevelText = "Субъект РФ"
                };
            case "2":
                return new AddressObjectLevel
                {
                    ObjectLevel = GarAddressLevel.AdministrativeArea,
                    ObjectLevelText = "Административный район"
                };
            case "3":
                return new AddressObjectLevel
                {
                    ObjectLevel = GarAddressLevel.MunicipalArea,
                    ObjectLevelText = "Муниципальный район"
                };
            case "4":
                return new AddressObjectLevel
                {
                    ObjectLevel = GarAddressLevel.RuralUrbanSettlement,
                    ObjectLevelText = "Сельское/городское поселение"
                };
            case "5":
                return new AddressObjectLevel
                {
                    ObjectLevel = GarAddressLevel.City,
                    ObjectLevelText = "Город"
                };
            case "6":
                return new AddressObjectLevel
                {
                    ObjectLevel = GarAddressLevel.Locality,
                    ObjectLevelText = "Населенный пункт"
                };
            case "7":
                return new AddressObjectLevel
                {
                    ObjectLevel = GarAddressLevel.ElementOfPlanningStructure,
                    ObjectLevelText = "Элемент планировочной структуры"
                };
            case "8":
                return new AddressObjectLevel
                {
                    ObjectLevel = GarAddressLevel.ElementOfRoadNetwork,
                    ObjectLevelText = "Элемент улично-дорожной сети"
                };
            case "9":
                return new AddressObjectLevel
                {
                    ObjectLevel = GarAddressLevel.Land,
                    ObjectLevelText = "Земельный участок"
                };
            case "10":
                return new AddressObjectLevel
                {
                    ObjectLevel = GarAddressLevel.Building,
                    ObjectLevelText = "Здание (сооружение)"
                };
            case "11":
                return new AddressObjectLevel
                {
                    ObjectLevel = GarAddressLevel.Room,
                    ObjectLevelText = "Помещение"
                };
            case "12":
                return new AddressObjectLevel
                {
                    ObjectLevel = GarAddressLevel.RoomInRooms,
                    ObjectLevelText = "Помещения в пределах помещения"
                };
            case "13":
                return new AddressObjectLevel
                {
                    ObjectLevel = GarAddressLevel.AutonomousRegionLevel,
                    ObjectLevelText = "Уровень автономного округа"
                };
            case "14":
                return new AddressObjectLevel
                {
                    ObjectLevel = GarAddressLevel.IntracityLevel,
                    ObjectLevelText = "Уровень внутригородской территории"
                };
            case "15":
                return new AddressObjectLevel
                {
                    ObjectLevel = GarAddressLevel.AdditionalTerritoriesLevel,
                    ObjectLevelText = "Уровень дополнительных территорий  РФ"
                };
            case "16":
                return new AddressObjectLevel
                {
                    ObjectLevel = GarAddressLevel.LevelOfObjectsInAdditionalTerritories,
                    ObjectLevelText = "Уровень объектов на дополнительных территориях"
                };
            case "17":
                return new AddressObjectLevel
                {
                    ObjectLevel = GarAddressLevel.CarPlace,
                    ObjectLevelText = "Машиноместо"
                };
            default:
                return new AddressObjectLevel
                {
                    ObjectLevel = GarAddressLevel.Building,
                    ObjectLevelText = "Здание (сооружение)"
                };
        }
    }
}