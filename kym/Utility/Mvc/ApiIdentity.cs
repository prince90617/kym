using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using smlib;

namespace kym.Utility.Mvc {
  public class ApiIdentity : IIdentity {

    public ApiIdentity(string name, string accessToken, string refreshToken) {
      Name = name;
      AccessToken = accessToken;
      RefreshToken = refreshToken;
    }

    public string AuthenticationType {
      get { return "AdminServiceWebservice"; }
    }

    public bool IsAuthenticated {
      get {
        using (var client = new HttpClient()) {
            var serviceUrl = ConfigSettings.GetEnvironmentSetting("AdminServiceDNSVariableName", "http://localhost:9553").FixUrlBeginning().FixUrlEnding();
          client.BaseAddress = new Uri(serviceUrl);
          client.DefaultRequestHeaders.Accept.Clear();
          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
          client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
          HttpResponseMessage response = client.GetAsync("api/Authentication/TestAuthentication").Result;
          return response.StatusCode == HttpStatusCode.OK;
        }
      }
    }

    public string Name {
      get;
      private set;
    }

    public string AccessToken {
      get;
      private set;
    }

    public string RefreshToken {
      get;
      private set;
    }
  }
}