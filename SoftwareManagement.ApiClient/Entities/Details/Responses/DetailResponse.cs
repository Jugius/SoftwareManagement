using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.ApiClient.Entities.Details.Responses;
public class DetailResponse : BaseResponse
{
    public ReleaseDetail Detail { get; set; }
}
