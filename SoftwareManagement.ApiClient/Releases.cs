using SoftwareManagement.ApiClient.Entities;
using SoftwareManagement.ApiClient.Entities.Releases.Requests;
using SoftwareManagement.ApiClient.Entities.Releases.Responses;

namespace SoftwareManagement.ApiClient;
public class Releases
{
    public static HttpEngine<CreateReleaseRequest, ReleaseResponse> Create => HttpEngine<CreateReleaseRequest, ReleaseResponse>.instance;
    public static HttpEngine<UpdateReleaseRequest, ReleaseResponse> Update => HttpEngine<UpdateReleaseRequest, ReleaseResponse>.instance;
    public static HttpEngine<DeleteReleaseRequest, ResultResponse> Delete => HttpEngine<DeleteReleaseRequest, ResultResponse>.instance;
}
