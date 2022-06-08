using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.Api.Contracts.Requests;
public class CreateReleaseDetailRequest : BaseRequest
{
    public DetailKind Kind { get; set; }
    public string Description { get; set; }
    public Guid ReleaseId { get; set; }
}
