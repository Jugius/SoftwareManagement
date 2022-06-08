using SoftwareManagement.Api.Contracts.Responses;
using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.Api.Mapping;
public static class DomainToResponse
{
    public static ApplicationsResponse ToResponse(this IEnumerable<ApplicationInfo> apps) =>
        new ApplicationsResponse
        {
            Status = Contracts.Common.Enums.Status.Ok,
            Applications = apps.ToArray()
        };

    public static ApplicationResponse ToResponse(this ApplicationInfo app) =>
        new ApplicationResponse
        {
            Status = Contracts.Common.Enums.Status.Ok,
            Application = app
        };
}
