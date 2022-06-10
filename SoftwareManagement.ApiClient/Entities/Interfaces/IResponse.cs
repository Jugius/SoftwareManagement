using SoftwareManagement.ApiClient.Entities.Common.Enums;

namespace SoftwareManagement.ApiClient.Entities.Interfaces;
public interface IResponse
{
    Status? Status { get; set; }
    string ErrorMessage { get; set; }
}
