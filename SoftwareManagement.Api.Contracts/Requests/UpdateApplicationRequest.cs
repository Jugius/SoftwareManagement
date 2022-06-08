
namespace SoftwareManagement.Api.Contracts.Requests;
public class UpdateApplicationRequest : BaseRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsPublic { get; set; }
}
