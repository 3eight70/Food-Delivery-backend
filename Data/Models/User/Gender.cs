using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace webNET_Hits_backend_aspnet_project_1.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Gender
{
    [Display(Name = "Male")]
    Male,
    [Display(Name = "Female")]
    Female
}