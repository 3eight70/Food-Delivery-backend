using System.Text.Json.Serialization;

namespace webNET_Hits_backend_aspnet_project_1.Data.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GarAddressLevel
{
    Region, 
    AdministrativeArea, 
    MunicipalArea, 
    RuralUrbanSettlement, 
    City, 
    Locality, 
    ElementOfPlanningStructure, 
    ElementOfRoadNetwork, 
    Land, 
    Building, 
    Room, 
    RoomInRooms, 
    AutonomousRegionLevel, 
    IntracityLevel, 
    AdditionalTerritoriesLevel, 
    LevelOfObjectsInAdditionalTerritories, 
    CarPlace
}