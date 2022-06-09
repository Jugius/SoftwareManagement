using SoftwareManagement.Api.Contracts.Common.Enums;

namespace SoftwareManagement.Api.Contracts;
public class BaseResponse
{
    public Status Status { get; set; }
    public static BaseResponse Ok => _okResponse;
    private static readonly BaseResponse _okResponse = new BaseResponse { Status = Status.Ok };

}
