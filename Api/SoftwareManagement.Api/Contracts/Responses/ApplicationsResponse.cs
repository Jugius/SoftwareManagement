using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.Api.Contracts.Responses;

public class ApplicationsResponse : Response
{       
    public ApplicationInfo[] Applications { get; set; }
}
