
namespace SoftwareManagement.ApiClient.Entities.Common.Enums;

public enum Status
{
    Ok,

    RequestDenied,
    InvalidKey,

    InvalidRequest,

    NotFound,
    DatabaseError,
    FileSystemError,

    UnknownError,
    
    HttpError,    
    ReadResponseError
}
