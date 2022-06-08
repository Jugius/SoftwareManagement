using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.Api.Contracts.Responses;

public class ApplicationResponse : BaseResponse
{
    public ApplicationInfo Application { get; set; }
}
