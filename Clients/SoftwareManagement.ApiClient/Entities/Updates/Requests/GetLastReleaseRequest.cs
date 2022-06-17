
using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.ApiClient.Entities.Updates.Requests;
public class GetLastReleaseRequest : BaseUpdatesRequest
{
    protected internal override string ControllerCommandPath => base.ControllerCommandPath + "/LastRelease";
    public string Name { get; set; }
    public FileRuntimeVersion? RuntimeVersion { get; set; }
}
