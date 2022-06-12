using SoftwareManagement.ApiClient.Entities.Interfaces;

namespace SoftwareManagement.ApiClient.Entities.Details.Requests;
public class DeleteDetailRequest : BaseDetailsCRUDRequest, IRequestDelete
{
    public Guid Id { get; set; }
}
