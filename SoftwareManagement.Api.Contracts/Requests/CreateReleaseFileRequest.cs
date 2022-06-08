using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.Api.Contracts.Requests;
public class CreateReleaseFileRequest : BaseRequest
{
    public string Name { get; set; }
    public FileKind Kind { get; set; }
    public FileRuntimeVersion RuntimeVersion { get; set; }
    public string CheckSum { get; set; }
    public ulong Size { get; set; }
    public string Description { get; set; }
    public Guid ReleaseId { get; set; }
}
