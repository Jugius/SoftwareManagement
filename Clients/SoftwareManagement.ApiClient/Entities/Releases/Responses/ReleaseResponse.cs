using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.ApiClient.Entities.Releases.Responses;
public class ReleaseResponse : BaseResponse
{
    public ApplicationRelease Release { get; set; }
}
