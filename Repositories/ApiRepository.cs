using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GitVisionMCP.Models;
using GitVisionMCP.Configurations;
public class ApiRepository
{
    private readonly Dictionary<string, ApiConnection> _config;

    public ApiRepository(Dictionary<string, ApiConnection> config)
    {
        _config = config;
    }

    public async Task<string> CallApiAsync(string apiName, string method, string endpoint = "", object payload = null, string soapAction = null, string rpcMethod = null, int rpcId = 1)
    {
        var api = _config[apiName];
        var client = new HttpClient { BaseAddress = new Uri(api.BaseUrl) };

        if (api.Headers != null)
        {
            foreach (var header in api.Headers)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        HttpResponseMessage response;

        if (method == "SOAP")
        {
            var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
            {
                Content = new StringContent(payload.ToString(), Encoding.UTF8, "text/xml")
            };
            request.Headers.Add("SOAPAction", soapAction);
            response = await client.SendAsync(request);
        }
        else if (method == "JSON-RPC")
        {
            var rpcPayload = new
            {
                jsonrpc = "2.0",
                method = rpcMethod,
                @params = payload,
                id = rpcId
            };
            var content = new StringContent(JsonConvert.SerializeObject(rpcPayload), Encoding.UTF8, "application/json");
            response = await client.PostAsync(endpoint, content);
        }
        else // REST
        {
            if (method == "GET")
                response = await client.GetAsync(endpoint);
            else if (method == "POST")
                response = await client.PostAsync(endpoint, new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"));
            else
                throw new NotSupportedException($"Unsupported REST method: {method}");
        }

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
