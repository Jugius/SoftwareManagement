using ApiClientTests.Extentions;
using SoftwareManagement.ApiClient.Entities.Applications.Requests;

namespace ApiClientTests.Applications;
public class CreateTest : TestClassBase
{
    public CreateTest(ITestOutputHelper output) : base(output) { }

    [Fact]
    public async Task CreateApplicationReturnsStatusOk()
    {
        var request = new CreateApplicationRequest
        {
            Name = "NewTesApplication",
            Description = "Тестовое приложение",
            IsPublic = true
        };

        ApplySettingsToRequest(request);

        
        var response = await SoftwareManagement.ApiClient.Applications.Create.QueryAsync(request);
        Assert.True(response.Status == SoftwareManagement.ApiClient.Entities.Common.Enums.Status.Ok);
        output.WriteLine($"AppName: {response.Application.Name}, ID: {response.Application.Id}");

    }
    [Fact]
    public async Task UpdateApplicationReturnsStatusOk()
    {
        var findRequest = new GetApplicationByNameRequest
        {
            Name = "NewUpdatedTestApplication"
        };
        ApplySettingsToRequest(findRequest);

        var findResponse = await SoftwareManagement.ApiClient.Applications.GetByName.QueryAsync(findRequest);
        output.WriteJson(findResponse);

        Assert.True(findResponse.Status == SoftwareManagement.ApiClient.Entities.Common.Enums.Status.Ok);



        var request = new UpdateApplicationRequest
        {
            Id = findResponse.Application.Id,
            Name = "UpdatedAgainTestApplication",
            Description = "Измененное тестовое приложение",
            IsPublic = false
        };

        ApplySettingsToRequest(request);


        var response = await SoftwareManagement.ApiClient.Applications.Update.QueryAsync(request);
        Assert.True(response.Status == SoftwareManagement.ApiClient.Entities.Common.Enums.Status.Ok);
        output.WriteJson(response.Application);

    }
    [Fact]
    public async Task DeleteApplicationReturnsStatusOk()
    {
        var findRequest = new GetApplicationByNameRequest
        {
            Name = "UpdatedAgainTestApplication"
        };
        ApplySettingsToRequest(findRequest);

        var findResponse = await SoftwareManagement.ApiClient.Applications.GetByName.QueryAsync(findRequest);
        output.WriteJson(findResponse);

        Assert.True(findResponse.Status == SoftwareManagement.ApiClient.Entities.Common.Enums.Status.Ok);



        var request = new DeleteApplicationRequest
        {
            Id = findResponse.Application.Id,            
        };

        ApplySettingsToRequest(request);


        var response = await SoftwareManagement.ApiClient.Applications.Delete.QueryAsync(request);
        Assert.True(response.Status == SoftwareManagement.ApiClient.Entities.Common.Enums.Status.Ok);        

    }

}
