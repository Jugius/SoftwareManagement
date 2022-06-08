using SoftwareManagement.Api.Contracts.Common.Enums;

namespace SoftwareManagement.Api.Contracts.Responses;
public class ErrorResponse : BaseResponse
{
    public ErrorResponse() { }
    public ErrorResponse(Status status)
    {
        this.Status = status;
    }
    public ErrorResponse(Status status, string error)
    {
        this.Status = status;
        this.ErrorMessage = error;
    }
    public string ErrorMessage { get; set; }
}
