
namespace SoftwareManagement.Api.Contracts.Common.Enums;

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
}
