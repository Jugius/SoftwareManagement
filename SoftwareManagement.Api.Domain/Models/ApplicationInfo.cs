
namespace SoftwareManagement.Api.Domain.Models;

public class ApplicationInfo
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsPublic { get; set; }
    public List<ApplicationRelease> Releases { get; set; }
}
