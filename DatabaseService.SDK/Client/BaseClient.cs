using DatabaseService.SDK.Models.Request;
using DatabaseService.SDK.Models.Response;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using smlib;

namespace DatabaseService.SDK.Client {
  public abstract class BaseClient {
    protected string _dbServiceUrl;

    public BaseClient() {
        _dbServiceUrl = ConfigSettings.GetEnvironmentSetting("DatabaseServiceDNSVariableName", "http://localhost:12870").FixUrlEnding().FixUrlBeginning();
    }

    public BaseClient(string dbServiceUrl) {
      _dbServiceUrl = dbServiceUrl;
    }

    protected HttpClient BuildClient() {
      var client = new HttpClient();
      client.BaseAddress = new Uri(_dbServiceUrl);
      client.DefaultRequestHeaders.Clear();
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      return client;
    }

    protected async Task<T> BuildResponse<T>(HttpClient client, string path, BaseRequest request) where T : BaseResponse {
      var response = Activator.CreateInstance<T>();

      var clientResponse = await client.PostAsJsonAsync(path, request);
      response.IsSuccess = clientResponse.IsSuccessStatusCode;
      response.ReasonPhrase = clientResponse.ReasonPhrase;
      response.Content = await clientResponse.Content.ReadAsStringAsync();

      return response;
    }

    protected async Task<T3> RunClient<T1, T2, T3>(T1 request, string path, Action<T2, T3> mapping)
      where T1 : BaseRequest
      where T2 : class
      where T3 : BaseResponse {
      using (var client = BuildClient()) {
        var response = await BuildResponse<T3>(client, path, request);
        var result = response.ParseContent<T2>();
        if (mapping != null) {
          mapping.Invoke(result, response);
        }
        return response;
      }
    }

    protected async Task<T2> RunClient<T1, T2>(T1 request, string path)
      where T1 : BaseRequest
      where T2 : BaseResponse {
      using (var client = BuildClient()) {
        return await BuildResponse<T2>(client, path, request);
      }
    }
  }
}
