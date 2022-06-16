using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.Api.Contracts.Requests;
public class CreateReleaseFileRequest : Request
{
    public Guid ReleaseId { get; set; }
    public string Name { get; set; }
    public FileKind Kind { get; set; }
    public FileRuntimeVersion RuntimeVersion { get; set; }
    public string Description { get; set; }
    public byte[] FileBytes { get; set; }    
}
