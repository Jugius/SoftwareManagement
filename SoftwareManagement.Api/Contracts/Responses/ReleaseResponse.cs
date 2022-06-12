using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.Api.Contracts.Responses;

public class ReleaseResponse : Response
{
    public ApplicationRelease Release { get; set; }
}
