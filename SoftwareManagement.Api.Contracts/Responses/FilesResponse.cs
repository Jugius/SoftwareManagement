using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.Api.Contracts.Responses;

public class FilesResponse : BaseResponse
{
    public ReleaseFile[] Files { get; set; }
}
