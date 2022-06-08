using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.Api.Contracts.Requests;
public class UpdateReleaseDetailRequest : BaseRequest
{
    public Guid Id { get; set; }
    public DetailKind Kind { get; set; }
    public string Description { get; set; }
    public Guid ReleaseId { get; set; }
}
