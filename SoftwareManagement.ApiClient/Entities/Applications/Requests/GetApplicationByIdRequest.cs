using SoftwareManagement.ApiClient.Entities.Interfaces;

namespace SoftwareManagement.ApiClient.Entities.Applications.Requests;
public class GetApplicationByIdRequest : BaseApplicationsRequest, IRequestGet
{
    protected internal override string ControllerCommandPath => base.ControllerCommandPath + "/byid";
    public Guid Id { get; set; }
    public override IList<KeyValuePair<string, string>> GetQueryStringParameters()
    {
        var pars = base.GetQueryStringParameters();
        if (Id == Guid.Empty)
            throw new ArgumentException("id");

        pars.Add(new KeyValuePair<string, string> ("id", Id.ToString()));
        return pars;
    }
}
