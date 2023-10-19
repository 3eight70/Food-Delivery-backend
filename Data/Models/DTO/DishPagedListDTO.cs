namespace webNET_Hits_backend_aspnet_project_1.Models.DTO;

public class DishPagedListDTO
{
    public IEnumerable<Dish> Dishes { get; set; }
    public PageInfo PageInfo { get; set; }
}