using SoftwareManagement.ApiClient.Entities.Interfaces;

namespace SoftwareManagement.ApiClient.Entities.Files.Requests;
public class DeleteFileRequest : BaseFilesCRUDRequest, IRequestDelete
{
    public Guid Id { get; set; }
}
