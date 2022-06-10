using SoftwareManagement.ApiClient.Entities.Interfaces;

namespace SoftwareManagement.ApiClient.Entities.Applications.Requests;
public class DeleteRequest : BaseApplicationsCRUDRequest, IRequestDelete
{
    public Guid Id { get; set; }
}
