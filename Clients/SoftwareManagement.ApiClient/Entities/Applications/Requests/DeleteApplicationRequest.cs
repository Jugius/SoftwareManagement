using SoftwareManagement.ApiClient.Entities.Interfaces;

namespace SoftwareManagement.ApiClient.Entities.Applications.Requests;
public class DeleteApplicationRequest : BaseApplicationsCRUDRequest, IRequestDelete
{
    public Guid Id { get; set; }
}
