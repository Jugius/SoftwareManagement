using SoftwareManagement.ApiClient.Entities.Interfaces;

namespace SoftwareManagement.ApiClient.Entities.Applications.Requests;
public class GetApplicationByNameRequest : BaseApplicationsRequest, IRequestGet
{
    protected internal override string ControllerCommandPath => base.ControllerCommandPath + "/byname";
    public string Name { get; set; }
    public override IList<KeyValuePair<string, string>> GetQueryStringParameters()
    {
        if(string.IsNullOrEmpty(Name))
            throw new ArgumentNullException("name");    

        var pars = base.GetQueryStringParameters();
        pars.Add(new KeyValuePair<string, string>("name", Name));
        return pars;
    }
}
