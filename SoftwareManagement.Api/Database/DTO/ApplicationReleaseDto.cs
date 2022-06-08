using System.ComponentModel.DataAnnotations;

namespace SoftwareManagement.Api.Database.DTO;

public class ApplicationReleaseDto
{
    public Guid Id { get; set; }

    [MaxLength(20)]
    public string Version { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int Kind { get; set; }
    public Guid ApplicationId { get; set; }


    public ApplicationInfoDto Application { get; set; }
    public List<ReleaseDetailDto> Details { get; set; }
    public List<ReleaseFileDto> Files { get; set; }
}
