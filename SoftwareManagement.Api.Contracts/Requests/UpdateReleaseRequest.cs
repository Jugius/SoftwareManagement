using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.Api.Contracts.Requests;
public class UpdateReleaseRequest : BaseRequest
{
    public Guid Id { get; set; }
    public string Version { get; set; }
    public DateTime ReleaseDate { get; set; }
    public ReleaseKind Kind { get; set; }
    public Guid ApplicationId { get; set; }
}
