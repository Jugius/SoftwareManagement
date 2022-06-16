using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.Api.Contracts.Responses;

public class DetailsResponse : Response
{
    public ReleaseDetail[] Details { get; set; }
}
