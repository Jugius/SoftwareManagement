using SoftwareManagement.Api.Domain.Models;
using SoftwareManagement.ApiClient.Entities.Interfaces;

namespace SoftwareManagement.ApiClient.Entities.Details.Requests;
public class UpdateDetailRequest : BaseDetailsCRUDRequest, IRequestUpdate
{
    public Guid Id { get; set; }
    public DetailKind Kind { get; set; }
    public string Description { get; set; }
    public Guid ReleaseId { get; set; }
}
