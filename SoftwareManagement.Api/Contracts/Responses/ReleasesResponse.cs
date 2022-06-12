using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.Api.Contracts.Responses;

public class ReleasesResponse : Response
{
    public ApplicationRelease[] Releases { get; set; }
}
