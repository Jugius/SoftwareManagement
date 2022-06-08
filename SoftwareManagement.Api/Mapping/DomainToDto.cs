using SoftwareManagement.Api.Database.DTO;
using SoftwareManagement.Api.Domain.Models;

namespace SoftwareManagement.Api.Mapping;
public static class DomainToDto
{
    public static ApplicationInfoDto ToDto(this ApplicationInfo domainInfo) =>
        new ApplicationInfoDto
        {
            Id = domainInfo.Id,
            Name = domainInfo.Name,
            IsPublic = domainInfo.IsPublic,
            Description = domainInfo.Description
        };

    public static ApplicationReleaseDto ToDto(this ApplicationRelease domainRelease) =>
        new ApplicationReleaseDto
        {
            ApplicationId = domainRelease.ApplicationId,
            Id = domainRelease.Id,
            Version = domainRelease.Version,
            Kind = (int)domainRelease.Kind,
            ReleaseDate = domainRelease.ReleaseDate
        };

    public static ReleaseDetailDto ToDto(this ReleaseDetail domainDetail) =>
         new ReleaseDetailDto
         {
             Id = domainDetail.Id,
             Description = domainDetail.Description,
             ReleaseId = domainDetail.ReleaseId,
             Kind = (int)domainDetail.Kind
         };

    public static ReleaseFileDto ToDto(this ReleaseFile domainFile) =>
       new ReleaseFileDto
       {
           Id = domainFile.Id,
           Name = domainFile.Name,
           Kind = (int)domainFile.Kind,
           RuntimeVersion = (int)domainFile.RuntimeVersion,
           CheckSum = domainFile.CheckSum,
           Description = domainFile.Description,
           ReleaseId = domainFile.ReleaseId,
           Size = domainFile.Size,
           Uploaded = domainFile.Uploaded
       };
}
