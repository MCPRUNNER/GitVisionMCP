using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GitVisionMCP.Models;
using GitVisionMCP.Configuration;
using Microsoft.Extensions.Logging;

namespace GitVisionMCP.Repositories;

/// <summary>
/// Repository for making API calls (SOAP, REST, JSON-RPC).
/// </summary>
public class ApiRepository
{
    private readonly ILogger<ApiRepository> _logger;
    private readonly IConfigLoader _configLoader;
    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiRepository"/> class.
    /// </summary>
    public ApiRepository(ILogger<ApiRepository> logger, IConfigLoader configLoader, IHttpClientFactory httpClientFactory)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _configLoader = configLoader ?? throw new ArgumentNullException(nameof(configLoader));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    /// <summary>
    /// Serializes an object to XML string.
    /// </summary>
    private static string SerializeToXml(object obj)
    {
        if (obj == null)
        {
            return string.Empty;
        }
        var xmlSerializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
        using (var stringWriter = new System.IO.StringWriter())
        {
            xmlSerializer.Serialize(stringWriter, obj);
            return stringWriter.ToString();
        }
    }

    /// <summary>
    /// Calls an API endpoint using the specified method.
    /// Supports SOAP, JSON-RPC, and REST (GET, POST, PUT, DELETE) calls.
    /// <para>
    /// - SOAP: Uses XML payload and optional SOAPAction header.
    /// - JSON-RPC: Uses JSON-RPC 2.0 format with method, params, and id.
    /// - REST: Supports GET, POST, PUT, DELETE with JSON payload for POST/PUT.
    /// </para>
    /// </summary>
    public async Task<string> CallApiAsync(
        string apiName,
        string method,
        string endpoint = "",
        object? payload = null,
        string? soapAction = null,
        string? rpcMethod = null,
        int rpcId = 1)
    {
        try
        {
            if (string.IsNullOrEmpty(apiName))
            {
                _logger.LogError("API name is null or empty.");
                throw new ArgumentException("API name cannot be null or empty.", nameof(apiName));
            }

            if (string.IsNullOrEmpty(method))
            {
                _logger.LogError("HTTP method is null or empty.");
                throw new ArgumentException("HTTP method cannot be null or empty.", nameof(method));
            }

            if (string.IsNullOrEmpty(endpoint) && method != "SOAP" && method != "JSON-RPC")
            {
                _logger.LogError("Endpoint is required for REST methods.");
                throw new ArgumentException("Endpoint cannot be null or empty for REST methods.", nameof(endpoint));
            }

            var api = _configLoader.GetApiConnectionSettings(apiName);

            if (string.IsNullOrEmpty(api.BaseUrl))
            {
                _logger.LogError("API BaseUrl is null or empty.");
                throw new ArgumentException("API BaseUrl cannot be null or empty.", nameof(api.BaseUrl));
            }

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(api.BaseUrl);

            if (api.Headers != null)
            {
                foreach (var header in api.Headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            HttpResponseMessage response;

            // Dispatch API call based on method type
            switch (method)
            {
                case "SOAP":
                    // SOAP: Send XML payload with optional SOAPAction header
                    var xmlContent = payload != null ? SerializeToXml(payload) : string.Empty;
                    var soapContent = new StringContent(xmlContent, Encoding.UTF8, "text/xml");
                    var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
                    {
                        Content = soapContent
                    };
                    if (!string.IsNullOrEmpty(soapAction))
                    {
                        request.Headers.Add("SOAPAction", soapAction);
                    }
                    response = await client.SendAsync(request);
                    break;
                case "JSON-RPC":
                    // JSON-RPC: Send JSON-RPC 2.0 formatted payload
                    var rpcPayload = new
                    {
                        jsonrpc = "2.0",
                        method = rpcMethod,
                        @params = payload,
                        id = rpcId
                    };
                    var rpcContent = new StringContent(JsonConvert.SerializeObject(rpcPayload), Encoding.UTF8, "application/json");
                    response = await client.PostAsync(endpoint, rpcContent);
                    break;
                case "GET":
                    // REST GET: No payload
                    response = await client.GetAsync(endpoint);
                    break;
                case "POST":
                    // REST POST: Send JSON payload
                    var postContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                    response = await client.PostAsync(endpoint, postContent);
                    break;
                case "PUT":
                    // REST PUT: Send JSON payload
                    var putContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                    response = await client.PutAsync(endpoint, putContent);
                    break;
                case "DELETE":
                    // REST DELETE: No payload
                    response = await client.DeleteAsync(endpoint);
                    break;
                default:
                    throw new NotSupportedException($"Unsupported method: {method}");
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during API call execution or parameter validation.");
            throw;
        }
    }
}
