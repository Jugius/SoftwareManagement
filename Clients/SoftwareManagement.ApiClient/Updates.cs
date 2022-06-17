using SoftwareManagement.ApiClient.Entities.Releases.Responses;
using SoftwareManagement.ApiClient.Entities.Updates.Requests;

namespace SoftwareManagement.ApiClient;
public class Updates
{
    public static HttpEngine<GetNewestReleasesRequest, ReleasesResponse> GetNewestReleases => HttpEngine<GetNewestReleasesRequest, ReleasesResponse>.instance;
    public static HttpEngine<GetLastReleaseRequest, ReleaseResponse> GetLastRelease => HttpEngine<GetLastReleaseRequest, ReleaseResponse>.instance;
}
