using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.Api.Contracts.Responses;

public class ReleaseResponse : BaseResponse
{
    public ApplicationRelease Release { get; set; }
}
