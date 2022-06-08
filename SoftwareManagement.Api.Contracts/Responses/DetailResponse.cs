using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.Api.Contracts.Responses;

public class DetailResponse : BaseResponse
{
    public ReleaseDetail Detail { get; set; }
}
