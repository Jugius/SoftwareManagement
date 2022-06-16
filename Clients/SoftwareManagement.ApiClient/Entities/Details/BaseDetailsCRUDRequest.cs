using SoftwareManagement.ApiClient.Entities.Interfaces;

namespace SoftwareManagement.ApiClient.Entities.Details;
public class BaseDetailsCRUDRequest : BaseDetailsRequest, IKeyRequired
{
    public string Key { get; set; }
}
