using SoftwareManagement.Api.Domain.Models;
using SoftwareManagement.ApiClient.Entities.Interfaces;

namespace SoftwareManagement.ApiClient.Entities.Releases.Requests;
public class CreateReleaseRequest : BaseReleasesCRUDRequest, IRequestCreate
{
    public Version Version { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public ReleaseKind Kind { get; set; }
    public Guid ApplicationId { get; set; }
}
