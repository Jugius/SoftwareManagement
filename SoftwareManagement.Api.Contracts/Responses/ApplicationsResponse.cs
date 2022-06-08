using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.Api.Contracts.Responses;

public class ApplicationsResponse : BaseResponse
{       
    public ApplicationInfo[] Applications { get; set; }
}
