using SoftwareManagement.ApiClient.Entities.Interfaces;

namespace SoftwareManagement.ApiClient.Entities.Applications.Requests;
public class CreateApplicationRequest : BaseApplicationsCRUDRequest, IRequestCreate
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsPublic { get; set; }
}
