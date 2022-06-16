using SoftwareManagement.ApiClient.Entities.Interfaces;

namespace SoftwareManagement.ApiClient.Entities.Releases.Requests;
public class DeleteReleaseRequest : BaseReleasesCRUDRequest, IRequestDelete
{
    public Guid Id { get; set; }
}
