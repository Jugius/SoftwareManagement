using SoftwareManagement.Api.Contracts.Requests;
using SoftwareManagement.Api.Database.DTO;

namespace SoftwareManagement.Api.Mapping;
public static class ContractToDto
{
    public static ApplicationInfoDto ToDto(this CreateApplicationRequest request) =>
        new ApplicationInfoDto
        {   
            Id=Guid.Empty,
            Name = request.Name,
            IsPublic = request.IsPublic,
            Description = request.Description
        };

    public static ApplicationReleaseDto ToDto(this CreateReleaseRequest request) =>
        new ApplicationReleaseDto
        {
            Id = Guid.Empty,
            ApplicationId = request.ApplicationId,
            Version = request.Version.ToString(),
            Kind = (int)request.Kind,
            ReleaseDate = request.ReleaseDate,
        };

    public static ReleaseDetailDto ToDto(this CreateReleaseDetailRequest request) =>
        new ReleaseDetailDto
        {
            Id = Guid.Empty,
            ReleaseId = request.ReleaseId,
            Kind = (int)request.Kind,
            Description = request.Description
        };

    public static ReleaseFileDto ToDto(this CreateReleaseFileRequest request) =>
        new ReleaseFileDto
        {
            Id = Guid.Empty,
            ReleaseId = request.ReleaseId,
            Name = request.Name,
            Kind = (int)request.Kind,
            Description = request.Description,
            RuntimeVersion = (int)request.RuntimeVersion,
            Uploaded = DateTime.Now,
        };
}
