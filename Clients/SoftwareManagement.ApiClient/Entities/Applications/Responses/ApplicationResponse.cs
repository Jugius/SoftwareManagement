using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.ApiClient.Entities.Applications.Responses;

public class ApplicationResponse : BaseResponse
{
    public ApplicationInfo Application { get; set; }
}
