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
                ObjectLevelText = AsAddrObj.Level
            }).ToArray();

        for (int i = 0; i < addresses.Length; i++)          //Костыль для того, чтобы записывать значения, т.к если это делать в select new вылетает error 500
        {
            AddressObjectLevel objLevel = AddressObjectLevels(addresses[i].ObjectLevelText);
            addresses[i].ObjectLevel = objLevel.ObjectLevel;
            addresses[i].ObjectLevelText = objLevel.ObjectLevelText;
        }

        return addresses;
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