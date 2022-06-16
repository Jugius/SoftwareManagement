using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.ApiClient.Entities.Files.Responses;
public class FileResponse : BaseResponse
{
    public ReleaseFile File { get; set; }
}
