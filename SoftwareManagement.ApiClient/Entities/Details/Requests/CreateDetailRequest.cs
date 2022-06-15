using SoftwareManagement.Api.Domain.Models;
using SoftwareManagement.ApiClient.Entities.Interfaces;

namespace SoftwareManagement.ApiClient.Entities.Details.Requests;
public class CreateDetailRequest : BaseDetailsCRUDRequest, IRequestCreate
{
    public DetailKind Kind { get; set; }
    public string Description { get; set; }
    public Guid ReleaseId { get; set; }
}
