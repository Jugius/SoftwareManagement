using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.Api.Contracts.Responses;

public class FileResponse : Response
{
    public ReleaseFile File { get; set; }
}
