
using SoftwareManagement.Api.Contracts.Common.Enums;

namespace SoftwareManagement.Api.Exceptions;
public class ApiException : Exception
{
    public Status Status { get; }

    public ApiException(Status status)
    {
        this.Status = status;
    }
    public ApiException(Status status, string message) : base(message) 
    {
        this.Status = status;
    }
}
