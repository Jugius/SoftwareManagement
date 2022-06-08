namespace SoftwareManagement.Api.Contracts.Requests;
public class CreateApplicationRequest : BaseRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsPublic { get; set; }
}
