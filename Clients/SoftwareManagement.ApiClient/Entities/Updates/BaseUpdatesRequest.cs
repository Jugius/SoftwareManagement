
namespace SoftwareManagement.ApiClient.Entities.Updates;
public class BaseUpdatesRequest : BaseRequest, Interfaces.IRequestPost
{
    protected internal override string ControllerCommandPath => "updates";
}
