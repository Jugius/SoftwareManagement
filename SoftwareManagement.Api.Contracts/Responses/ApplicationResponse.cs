using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.Api.Contracts.Responses;

public class ApplicationResponse : Response
{
    public ApplicationInfo Application { get; set; }
}
