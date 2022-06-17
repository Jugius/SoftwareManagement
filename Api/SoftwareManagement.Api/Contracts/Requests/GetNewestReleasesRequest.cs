
namespace SoftwareManagement.Api.Contracts.Requests;
public class GetNewestReleasesRequest
{
    public string Name { get; set; }
    public Version CurrentVersion { get; set; }
}
