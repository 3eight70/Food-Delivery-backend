using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_1.Services;

public interface IValidatableObject
{
    IEnumerable<ValidationResult> Validate (ValidationContext validationContext);
}