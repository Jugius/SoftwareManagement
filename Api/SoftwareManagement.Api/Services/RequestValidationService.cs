using SoftwareManagement.Api.Exceptions;
using SoftwareManagement.Api.Services.Helpers;

namespace SoftwareManagement.Api.Services;
public class RequestValidationService
{
    public OperationResult<bool> Validate(Contracts.Request request)
    {
        if (string.IsNullOrEmpty(request.Key))
            return new OperationResult<bool>(new ApiException(Contracts.Common.Enums.Status.RequestDenied, "Api key is empty"));

        if(request.Key != AppConfig.AdminKey)
            return new OperationResult<bool>(new ApiException(Contracts.Common.Enums.Status.RequestDenied, "Api key is invalid"));

        return new OperationResult<bool>(true);
    }
}
