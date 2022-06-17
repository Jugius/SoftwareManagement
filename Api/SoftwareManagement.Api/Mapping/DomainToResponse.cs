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

    public static ReleaseResponse ToResponse(this ApplicationRelease release) =>
        new ReleaseResponse
        {
            Status = Contracts.Common.Enums.Status.Ok,
            Release = release
        };

    public static ReleasesResponse ToResponse(this IEnumerable<ApplicationRelease> releases) =>
        new ReleasesResponse
        {
            Status = Contracts.Common.Enums.Status.Ok,
            Releases = releases.ToArray()
        };

    public static DetailResponse ToResponse(this ReleaseDetail detail) =>
        new DetailResponse
        {
            Status = Contracts.Common.Enums.Status.Ok,
            Detail = detail
        };

    public static FileResponse ToResponse(this ReleaseFile file) =>
        new FileResponse
        {
            Status = Contracts.Common.Enums.Status.Ok,
            File = file
        };
}
