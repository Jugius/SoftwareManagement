using SoftwareManagement.ApiClient.Entities.Common.Enums;
using SoftwareManagement.ApiClient.Entities.Interfaces;

namespace SoftwareManagement.ApiClient.Entities;
public abstract class BaseResponse : IResponse
{
    public Status? Status { get; set; }
    public string ErrorMessage { get; set; }
}
