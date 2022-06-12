
using SoftwareManagement.ApiClient.Entities.Interfaces;

namespace ApiClientTests;
public abstract class TestClassBase
{
    protected enum TestingMode { Production, Development }
    protected readonly ITestOutputHelper output;
    protected TestingMode TestMode { get; set; } = TestingMode.Development;
    protected string DevelopmentDomainName = "localhost:7164";
    protected string ProductionDomainName = "_";
    protected string DevelopmentUserKey = "testKey";
    protected string ProductionUserKey = "_";

    public TestClassBase(ITestOutputHelper output)
    {
        this.output = output;
    }
    public void ApplySettingsToRequest(IRequest request)
    { 
        request.DomainName = this.TestMode == TestingMode.Development ? DevelopmentDomainName : ProductionDomainName;
        if (request is IKeyRequired keyRequired)
        {
            keyRequired.Key = this.TestMode == TestingMode.Development ? DevelopmentUserKey : ProductionUserKey;
        }
    }

}
