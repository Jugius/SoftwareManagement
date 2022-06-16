using SoftwareManagement.ApiClient.Entities;
using SoftwareManagement.ApiClient.Entities.Details.Requests;
using SoftwareManagement.ApiClient.Entities.Details.Responses;

namespace SoftwareManagement.ApiClient;
public class Details
{
    public static HttpEngine<CreateDetailRequest, DetailResponse> Create => HttpEngine<CreateDetailRequest, DetailResponse>.instance;
    public static HttpEngine<UpdateDetailRequest, DetailResponse> Update => HttpEngine<UpdateDetailRequest, DetailResponse>.instance;
    public static HttpEngine<DeleteDetailRequest, ResultResponse> Delete => HttpEngine<DeleteDetailRequest, ResultResponse>.instance;
}
