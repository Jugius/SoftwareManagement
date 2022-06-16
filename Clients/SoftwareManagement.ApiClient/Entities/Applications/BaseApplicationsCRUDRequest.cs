using SoftwareManagement.ApiClient.Entities.Interfaces;

namespace SoftwareManagement.ApiClient.Entities.Applications;
public class BaseApplicationsCRUDRequest : BaseApplicationsRequest, IKeyRequired
{
    public string Key { get; set; }
}
