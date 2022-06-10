using SoftwareManagement.ApiClient.Entities.Applications.Requests;

namespace ApiClientTests.Applications;
public class GetTest : TestClassBase
{
    public GetTest(ITestOutputHelper output) : base(output)    {    }
    
    
    [Fact]
    public async Task GetAllReturnsStatusOk()
    {
        var request = new GetRequest();
        ApplySettingsToRequest(request);
        
        var response = await SoftwareManagement.ApiClient.Applications.Get.QueryAsync(request);

        Assert.True(response.Status == SoftwareManagement.ApiClient.Entities.Common.Enums.Status.Ok);
        output.WriteLine(response.Applications.Length.ToString());
    }



}
