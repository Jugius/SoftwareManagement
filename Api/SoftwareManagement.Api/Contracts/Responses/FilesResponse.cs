using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.Api.Contracts.Responses;

public class FilesResponse : Response
{
    public ReleaseFile[] Files { get; set; }
}
