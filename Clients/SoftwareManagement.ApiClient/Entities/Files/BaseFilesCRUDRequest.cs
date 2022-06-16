using SoftwareManagement.ApiClient.Entities.Interfaces;

namespace SoftwareManagement.ApiClient.Entities.Files;
public class BaseFilesCRUDRequest : BaseFilesRequest, IKeyRequired
{
    public string Key { get; set; }
}
