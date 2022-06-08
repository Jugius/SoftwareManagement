using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.Api.Contracts.Responses;

public class DetailsResponse : BaseResponse
{
    public ReleaseDetail[] Details { get; set; }
}
