using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.Api.Contracts.Responses;

public class FileResponse : BaseResponse
{
    public ReleaseFile File { get; set; }
}
