using SoftwareManagement.Api.Domain.Models;
using SoftwareManagement.ApiClient.Entities.Interfaces;

namespace SoftwareManagement.ApiClient.Entities.Files.Requests;
public class CreateFileRequest : BaseFilesCRUDRequest, IRequestDelete
{    
    public string Name { get; set; }
    public FileKind Kind { get; set; }
    public FileRuntimeVersion RuntimeVersion { get; set; }
    public string Description { get; set; }
    public byte[] FileBytes { get; set; }

    public Guid ReleaseId { get; set; }
}
