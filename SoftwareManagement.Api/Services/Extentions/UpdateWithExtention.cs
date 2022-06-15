using SoftwareManagement.Api.Contracts.Requests;
using SoftwareManagement.Api.Database.DTO;

namespace SoftwareManagement.Api.Services.Extentions;
public static class UpdateWithExtention
{
    public static void UpdateWith(this ApplicationInfoDto dto, UpdateApplicationRequest request)
    {
        dto.Name = request.Name;
        dto.Description = request.Description;
        dto.IsPublic = request.IsPublic;
    }

    public static void UpdateWith(this ApplicationReleaseDto dto, UpdateReleaseRequest request)
    {
        dto.ApplicationId = request.ApplicationId;
        dto.Version = request.Version.ToString();
        dto.Kind = (int)request.Kind;
        dto.ReleaseDate = request.ReleaseDate;
    }

    public static void UpdateWith(this ReleaseDetailDto dto, UpdateReleaseDetailRequest request)
    {
        dto.ReleaseId = request.ReleaseId;
        dto.Kind = (int)request.Kind;
        dto.Description = request.Description;
    }

}
