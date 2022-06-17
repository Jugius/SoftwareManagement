using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.ApiClient.Entities.Releases.Responses;
public class ReleasesResponse : BaseResponse
{
    public ApplicationRelease[] Releases { get; set; }
}
