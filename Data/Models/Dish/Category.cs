using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace webNET_Hits_backend_aspnet_project_1.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Category
{
    [Display(Name = "WOK")]
    WOK,
    [Display(Name = "Pizza")]
    Pizza,
    [Display(Name = "Soup")]
    Soup,
    [Display(Name = "Dessert")]
    Dessert,
    [Display(Name = "Drink")]
    Drink
}