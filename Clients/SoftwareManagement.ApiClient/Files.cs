using SoftwareManagement.ApiClient.Entities;
using SoftwareManagement.ApiClient.Entities.Files.Requests;
using SoftwareManagement.ApiClient.Entities.Files.Responses;

namespace SoftwareManagement.ApiClient;
public class Files
{
    public static HttpEngine<CreateFileRequest, FileResponse> Create => HttpEngine<CreateFileRequest, FileResponse>.instance;
    public static HttpEngine<DeleteFileRequest, ResultResponse> Delete => HttpEngine<DeleteFileRequest, ResultResponse>.instance;
}
