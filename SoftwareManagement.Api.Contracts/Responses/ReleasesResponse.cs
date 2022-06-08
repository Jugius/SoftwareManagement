using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.Api.Contracts.Responses;

public class ReleasesResponse : BaseResponse
{
    public ApplicationRelease[] Releases { get; set; }
}
