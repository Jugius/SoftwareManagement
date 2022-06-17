
namespace SoftwareManagement.ApiClient.Entities.Updates.Requests;
public class GetNewestReleasesRequest : BaseUpdatesRequest
{
    protected internal override string ControllerCommandPath => base.ControllerCommandPath + "/NewReleases";
    public string Name { get; set; }
    public Version CurrentVersion { get; set; }
}
