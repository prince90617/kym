using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using smlib;

namespace kym.Utility.Mvc {
  public class AdminService {
    private AdminService(string baseUri) {
      BaseUri = baseUri;
    }

    private static AdminService _instance;

    public static AdminService Instance {
      get {
          var serviceUrl = ConfigSettings.GetEnvironmentSetting("AdminServiceDNSVariableName", "http://localhost:9553").FixUrlEnding().FixUrlBeginning();
        return _instance ?? (_instance = new AdminService(serviceUrl));
      }
    }

    public string BaseUri { get; private set; }

    public async Task<T> AuthenticateAsync<T>(string userName, string password) {
      using (var client = new HttpClient()) {
        var result = await client.PostAsync(BuildActionUri("/api/token"), new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("userName", userName), 
                    new KeyValuePair<string, string>("password", password),
                    new KeyValuePair<string, string>("client_id", "1")
                }));

        string json = await result.Content.ReadAsStringAsync();
        if (result.IsSuccessStatusCode) {
          return JsonConvert.DeserializeObject<T>(json);
        }

        throw new ApiException(result.StatusCode, json, result.ReasonPhrase);
      }
    }

    public async Task<T> AuthenticateAsync<T>(string refreshToken) {
      using (var client = new HttpClient()) {
        var result = await client.PostAsync(BuildActionUri("/api/token"), new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "refresh_token"),
                    new KeyValuePair<string, string>("refresh_token", refreshToken),
                    new KeyValuePair<string, string>("client_id", "4")
                }));

        string json = await result.Content.ReadAsStringAsync();
        if (result.IsSuccessStatusCode) {
          return JsonConvert.DeserializeObject<T>(json);
        }

        throw new ApiException(result.StatusCode, json, result.ReasonPhrase);
      }
    }

    public async Task<T> GetAsync<T>(string action, string authToken = null) {
      using (var client = new HttpClient()) {
        if (!authToken.IsNullOrWhiteSpace()) {
          //Add the authorization header
          client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + authToken);
        }

        var result = await client.GetAsync(BuildActionUri(action));

        string json = await result.Content.ReadAsStringAsync();
        if (result.IsSuccessStatusCode) {
          return JsonConvert.DeserializeObject<T>(json);
        }

        throw new ApiException(result.StatusCode, json, result.ReasonPhrase);
      }
    }

    public async Task<PagedData<T>> GetAsyncReturnPagedData<T>(string action, string authToken = null) {
      using (var client = new HttpClient()) {
        if (!authToken.IsNullOrWhiteSpace()) {
          //Add the authorization header
          client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + authToken);
        }

        var result = await client.GetAsync(BuildActionUri(action));

        string json = await result.Content.ReadAsStringAsync();
        if (result.IsSuccessStatusCode) {
          return new PagedData<T> {
            Data = JsonConvert.DeserializeObject<T>(json),
            ItemCount = int.Parse(result.Headers.GetValues("X-RecordCount").FirstOrDefault())
          };
        }

        throw new ApiException(result.StatusCode, json, result.ReasonPhrase);
      }
    }

    public async Task PutAsync<T>(string action, T data, string authToken = null) {
      using (var client = new HttpClient()) {
        if (!authToken.IsNullOrWhiteSpace()) {
          //Add the authorization header
          client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + authToken);
        }

        var result = await client.PutAsJsonAsync(BuildActionUri(action), data);
        if (result.IsSuccessStatusCode) {
          return;
        }

        string json = await result.Content.ReadAsStringAsync();
        throw new ApiException(result.StatusCode, json, result.ReasonPhrase);
      }
    }

    public async Task<TResult> PostAsync<TData, TResult>(string action, TData data, string authToken = null) {
      using (var client = new HttpClient()) {
        if (!authToken.IsNullOrWhiteSpace()) {
          //Add the authorization header
          client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + authToken);
        }

        var result = await client.PostAsJsonAsync(BuildActionUri(action), data);
        string json;
        if (result.IsSuccessStatusCode) {
          json = await result.Content.ReadAsStringAsync();
          return JsonConvert.DeserializeObject<TResult>(json);
        }

        json = await result.Content.ReadAsStringAsync();
        throw new ApiException(result.StatusCode, json, result.ReasonPhrase);
      }
    }

    public async Task<TResult> PostAsync<TResult>(string action, dynamic data, string authToken = null) {
      return await PostAsync<object, TResult>(action, data, authToken);
    }

    public async Task<PagedData<TResult>> PostAsyncReturnPagedData<TData, TResult>(string action, TData data, string authToken = null) {
      using (var client = new HttpClient()) {
        if (!authToken.IsNullOrWhiteSpace()) {
          //Add the authorization header
          client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + authToken);
        }

        var result = await client.PostAsJsonAsync(BuildActionUri(action), data);
        string json = await result.Content.ReadAsStringAsync();
        if (result.IsSuccessStatusCode) {
          return new PagedData<TResult> {
            Data = JsonConvert.DeserializeObject<TResult>(json),
            ItemCount = int.Parse(result.Headers.GetValues("X-RecordCount").FirstOrDefault())
          };
        }

        throw new ApiException(result.StatusCode, json, result.ReasonPhrase);
      }
    }

    public async Task<PagedData<TResult>> PostAsyncReturnPagedData<TResult>(string action, dynamic data, string authToken = null) {
      return await PostAsyncReturnPagedData<object, TResult>(action, data, authToken);
    }

    private string BuildActionUri(string action) {
      return BaseUri + action;
    }
  }
}