using SoftwareManagement.ApiClient.Entities.Interfaces;

namespace SoftwareManagement.ApiClient.Entities.Applications.Requests;
public class GetApplicationsRequest : BaseApplicationsRequest, IRequestGet
{
    public bool Detailed { get; set; }
    public override IList<KeyValuePair<string, string>> GetQueryStringParameters()
    {
        var p = base.GetQueryStringParameters();
        if (this.Detailed)
            p.Add(new KeyValuePair<string, string>("detailed", "true"));
        return p;
    }
}
