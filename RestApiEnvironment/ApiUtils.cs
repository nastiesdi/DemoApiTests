using System;
using Newtonsoft.Json;
using RestSharp;
using static System.String;

namespace RestApiEnvironment
{
    public class ApiUtils
    {

        private readonly RestClient _restClient = new(Constants.ApiKey);
        public RestResponse ExecuteRequest(string query, Method method, string modifier = null, Object user = null )
        {
            var modifiedQuery = !IsNullOrEmpty(modifier) ? $"{query}/{modifier}" :query;
            var restRequest = new RestRequest(modifiedQuery, method);
            if (user!=null)
            {
                restRequest.AddJsonBody(user);
            }
            
            return _restClient.Execute(restRequest);
        }

        public static T GetJsonContent<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}