using SoftwareManagement.ApiClient.Entities.Applications.Requests;

namespace ApiClientTests.Applications;
public class GetByIdTest : TestClassBase
{
    public GetByIdTest(ITestOutputHelper output) : base(output) { }


    [Fact]
    public async Task GetByIdWithIncorrectIdReturnsStatusNotFound()
    {
        var request = new GetByIdRequest();
        ApplySettingsToRequest(request);

        request.Id = Guid.NewGuid();
        var response = await SoftwareManagement.ApiClient.Applications.GetById.QueryAsync(request);
        Assert.True(response.Status == SoftwareManagement.ApiClient.Entities.Common.Enums.Status.NotFound);
        
    }

    [Fact]
    public async Task GetByIdWithEmptyGuidThrows()
    {
        var request = new GetByIdRequest();
        ApplySettingsToRequest(request);

        request.Id = Guid.Empty;
        var responseTask = SoftwareManagement.ApiClient.Applications.GetById.QueryAsync(request);
        await Assert.ThrowsAsync<ArgumentException>(async() => { _ = await responseTask; });
    }

    [Fact]
    public async Task GetByIdWithCorrectIdReturnsApp()
    {
        var request = new GetByIdRequest();
        ApplySettingsToRequest(request);

        request.Id = new Guid("9ff10b44-7709-4fef-93b4-3de2cf03eb7c");
        var response = await SoftwareManagement.ApiClient.Applications.GetById.QueryAsync(request);
        Assert.True(response.Status == SoftwareManagement.ApiClient.Entities.Common.Enums.Status.Ok);
        Assert.True(response.Application.Name == "Updater.CoreWPFTest");

    }



}
