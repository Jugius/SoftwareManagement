using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.Api.Contracts.Requests;
public class UpdateReleaseRequest : Request
{
    public Guid Id { get; set; }
    public Version Version { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public ReleaseKind Kind { get; set; }
    public Guid ApplicationId { get; set; }
}
