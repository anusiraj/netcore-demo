using System.ComponentModel.DataAnnotations;
namespace NETCoreDemo.DTOs;

using NETCoreDemo.Models;
using NETCoreDemo.Common;
using System.Collections.Generic;

public class CourseDTO : IValidatableObject //to vlaidate every objects by comparing
{
    [MinLength(5, ErrorMessage = "Name is too short, min: 5 characters")]
    public string? Name { get; set; }

    [CourseStartDate(ErrorMessage = "Start date has to be in the current year")]
    public DateTime StartDate { get; set; }

    public Course.CourseStatus Status { get; set; }

    public int Size { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) //it allows validation for all objects
    {
        if (StartDate < DateTime.Now && Status == Course.CourseStatus.NotStarted)
        {
            yield return new ValidationResult("The course has already started, wrong status", new string[]{nameof(Status), nameof(StartDate)});
        }
        if (!Name!.StartsWith("FIN"))
        {
            yield return new ValidationResult("The name has to be in the format: FIN FS<number>", new string[]{nameof(Name)});
        }
    }
}