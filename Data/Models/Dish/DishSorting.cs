using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace webNET_Hits_backend_aspnet_project_1.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DishSorting
{
    [Display(Name = "NameAsc")]
    NameAsc, 
    [Display(Name = "NameDesc")]
    NameDesc, 
    [Display(Name = "PriceAsc")]
    PriceAsc, 
    [Display(Name = "PriceDesk")]
    PriceDesc, 
    [Display(Name = "RatingAsc")]
    RatingAsc, 
    [Display(Name = "RatingDesc")]
    RatingDesc
}