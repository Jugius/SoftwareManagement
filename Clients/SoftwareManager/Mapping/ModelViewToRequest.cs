using SoftwareManagement.ApiClient.Entities.Applications.Requests;
using SoftwareManagement.ApiClient.Entities.Details.Requests;
using SoftwareManagement.ApiClient.Entities.Files.Requests;
using SoftwareManagement.ApiClient.Entities.Releases.Requests;
using SoftwareManager.ViewModels.Entities;

namespace SoftwareManager.Mapping;
public static class ModelViewToRequest
{
    public static CreateApplicationRequest ToRequestCreate(this ApplicationInfoVM appInfo) =>
        new CreateApplicationRequest
        {
            Name = appInfo.Name,
            Description = appInfo.Description,
            IsPublic = appInfo.IsPublic
        };
    public static UpdateApplicationRequest ToRequestUpdate(this ApplicationInfoVM appInfo) =>
        new UpdateApplicationRequest
        {
            Id = appInfo.Id,
            Name = appInfo.Name,
            Description = appInfo.Description,
            IsPublic = appInfo.IsPublic
        };

    public static DeleteApplicationRequest ToRequestDelete(this ApplicationInfoVM appInfo) =>
        new DeleteApplicationRequest
        {
            Id = appInfo.Id
        };

    public static CreateReleaseRequest ToRequestCreate(this ApplicationReleaseVM release) =>
        new CreateReleaseRequest
        {
            Kind = release.Kind,
            ReleaseDate = release.ReleaseDate,
            Version = release.Version,
            ApplicationId = release.ApplicationId,
        };

    public static UpdateReleaseRequest ToRequestUpdate(this ApplicationReleaseVM release) =>
        new UpdateReleaseRequest
        { 
            Id = release.Id,
            Kind = release.Kind,
            ReleaseDate = release.ReleaseDate,
            Version = release.Version,
            ApplicationId = release.ApplicationId,
        };

    public static DeleteReleaseRequest ToRequestDelete(this ApplicationReleaseVM release) =>
        new DeleteReleaseRequest
        {
            Id = release.Id
        };

    public static CreateDetailRequest ToRequestCreate(this ReleaseDetailVM detail) =>
        new CreateDetailRequest
        {
            Kind = detail.Kind,
            Description = detail.Description,
            ReleaseId = detail.ReleaseId,
        };

    public static UpdateDetailRequest ToRequestUpdate(this ReleaseDetailVM detail) =>
        new UpdateDetailRequest
        {
            Id = detail.Id,
            Kind = detail.Kind,
            Description = detail.Description,
            ReleaseId = detail.ReleaseId,
        };

    public static DeleteDetailRequest ToRequestDelete(this ReleaseDetailVM detail) =>
        new DeleteDetailRequest
        {
            Id = detail.Id
        };

    public static CreateFileRequest ToRequestCreate(this ReleaseFileVM file, byte[] fileBytes) =>
        new CreateFileRequest
        {
            Name = file.Name,
            Description = file.Description,
            Kind = file.Kind,
            RuntimeVersion = file.RuntimeVersion,
            FileBytes = fileBytes,
            ReleaseId = file.ReleaseId
        };

    public static DeleteFileRequest ToRequestDelete(this ReleaseFileVM file) =>
        new DeleteFileRequest
        {
            Id = file.Id,
        };

}
