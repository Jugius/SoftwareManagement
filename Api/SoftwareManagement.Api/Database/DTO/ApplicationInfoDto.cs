using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareManagement.Api.Database.DTO;

[Index(nameof(Name), IsUnique = true)]
public class ApplicationInfoDto
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Name { get; set; }

    public bool IsPublic { get; set; }

    [Required]
    [MaxLength(256)]
    public string Description { get; set; }


    public List<ApplicationReleaseDto> Releases { get; set; }
}
