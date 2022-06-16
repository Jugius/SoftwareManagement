using SoftwareManagement.Api.Contracts.Common.Enums;

namespace SoftwareManagement.Api.Contracts.Responses;
public class ErrorResponse : Response
{
    public ErrorResponse() { }
    public ErrorResponse(Status status)
    {
        this.Status = status;
    }
    public ErrorResponse(Status status, string error)
    {
        this.Status = status;
        this.ErrorMessage = string.IsNullOrEmpty(error) ? null : error;
    }
    public string ErrorMessage { get; set; }
}
