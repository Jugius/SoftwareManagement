using SoftwareManagement.ApiClient.Entities;
using SoftwareManagement.ApiClient.Entities.Applications.Requests;
using SoftwareManagement.ApiClient.Entities.Applications.Responses;

namespace SoftwareManagement.ApiClient;
public class Applications
{
    public static HttpEngine<GetApplicationsRequest, ApplicationsResponse> Get => HttpEngine<GetApplicationsRequest, ApplicationsResponse>.instance;
    public static HttpEngine<GetApplicationByIdRequest, ApplicationResponse> GetById => HttpEngine<GetApplicationByIdRequest, ApplicationResponse>.instance;
    public static HttpEngine<GetApplicationByNameRequest, ApplicationResponse> GetByName => HttpEngine<GetApplicationByNameRequest, ApplicationResponse>.instance;


    public static HttpEngine<CreateApplicationRequest, ApplicationResponse> Create => HttpEngine<CreateApplicationRequest, ApplicationResponse>.instance;
    public static HttpEngine<UpdateApplicationRequest, ApplicationResponse> Update => HttpEngine<UpdateApplicationRequest, ApplicationResponse>.instance;
    public static HttpEngine<DeleteApplicationRequest, ResultResponse> Delete => HttpEngine<DeleteApplicationRequest, ResultResponse>.instance;

}
