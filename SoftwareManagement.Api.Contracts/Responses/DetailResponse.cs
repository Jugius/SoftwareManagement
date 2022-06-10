using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.Api.Contracts.Responses;

public class DetailResponse : Response
{
    public ReleaseDetail Detail { get; set; }
}
