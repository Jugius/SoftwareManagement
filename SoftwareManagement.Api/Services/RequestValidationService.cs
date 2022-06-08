using SoftwareManagement.Api.Helpers;
using SoftwareManagement.Api.Exceptions;

namespace SoftwareManagement.Api.Services;
public class RequestValidationService
{
    public OperationResult<bool> Validate(Contracts.BaseRequest request)
    {
        if (string.IsNullOrEmpty(request.Key))
            return new OperationResult<bool>(new ApiException(Contracts.Common.Enums.Status.RequestDenied, "Api key is empty"));

        if(request.Key != AppConfig.AdminKey)
            return new OperationResult<bool>(new ApiException(Contracts.Common.Enums.Status.RequestDenied, "Api key is invalid"));

        return new OperationResult<bool>(true);
    }
}
