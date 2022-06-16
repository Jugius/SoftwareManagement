using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.ApiClient.Entities.Applications.Responses;

public class ApplicationsResponse : BaseResponse
{       
    public ApplicationInfo[] Applications { get; set; }
}
