using SoftwareManagement.Api.Contracts.Requests;
using SoftwareManagement.Api.Database.DTO;

namespace SoftwareManagement.Api.Mapping;
public static class ContractToDto
{
    public static ApplicationInfoDto ToDto(this CreateApplicationRequest request) =>
        new ApplicationInfoDto
        {           
            Name = request.Name,
            IsPublic = request.IsPublic,
            Description = request.Description
        };
}
