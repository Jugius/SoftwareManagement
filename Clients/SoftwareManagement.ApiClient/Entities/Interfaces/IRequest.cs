
namespace SoftwareManagement.ApiClient.Entities.Interfaces;
public interface IRequest
{
    string DomainName { get; set; }
    Uri GetUri();    
}
