using SoftwareManagement.ApiClient.Entities;
using SoftwareManagement.ApiClient.Entities.Applications.Requests;
using SoftwareManagement.ApiClient.Entities.Applications.Responses;

namespace SoftwareManagement.ApiClient;
public class Applications
{
    public static HttpEngine<GetRequest, ApplicationsResponse> Get => HttpEngine<GetRequest, ApplicationsResponse>.instance;
    public static HttpEngine<GetByIdRequest, ApplicationResponse> GetById => HttpEngine<GetByIdRequest, ApplicationResponse>.instance;
    public static HttpEngine<GetByNameRequest, ApplicationResponse> GetByName => HttpEngine<GetByNameRequest, ApplicationResponse>.instance;


    public static HttpEngine<CreateRequest, ApplicationResponse> Create => HttpEngine<CreateRequest, ApplicationResponse>.instance;
    public static HttpEngine<UpdateRequest, ApplicationResponse> Update => HttpEngine<UpdateRequest, ApplicationResponse>.instance;
    public static HttpEngine<DeleteRequest, ResultResponse> Delete => HttpEngine<DeleteRequest, ResultResponse>.instance;

}
