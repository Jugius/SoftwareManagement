using SoftwareManagement.ApiClient.Entities.Interfaces;

namespace SoftwareManagement.ApiClient.Entities.Releases;
public class BaseReleasesCRUDRequest : BaseReleasesRequest, IKeyRequired
{
    public string Key { get; set; }
}
