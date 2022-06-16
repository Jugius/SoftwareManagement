using System.Text;
using System.Text.Json.Serialization;
using SoftwareManagement.ApiClient.Entities.Interfaces;

namespace SoftwareManagement.ApiClient.Entities;
public abstract class BaseRequest : IRequest
{
    [JsonIgnore]
    public string DomainName { get; set; }

    [JsonIgnore]
    protected internal abstract string ControllerCommandPath { get; }
    public virtual Uri GetUri()
    {
        const string SCHEME = "https://";
        const string ROUTEPATH = "software/api";

        var queryStringParameters = this.GetQueryStringParameters()
                .Select(x =>
                    x.Value == null
                        ? Uri.EscapeDataString(x.Key)
                        : Uri.EscapeDataString(x.Key) + "=" + Uri.EscapeDataString(x.Value));
        var queryString = string.Join("&", queryStringParameters);

        StringBuilder builder = new StringBuilder(SCHEME)
                    .Append(DomainName).Append('/')
                    .Append(ROUTEPATH).Append('/')
                    .Append(ControllerCommandPath);

        if(!string.IsNullOrEmpty(queryString))
            builder.Append('?').Append(queryString);

        return new Uri(builder.ToString());

    }
    public virtual IList<KeyValuePair<string, string>> GetQueryStringParameters()
    {
        var parameters = new List<KeyValuePair<string, string>>();

        //if (!string.IsNullOrWhiteSpace(this.Key))
        //{
        //    parameters.Add("key", this.Key);
        //}

        return parameters;
    }
}
