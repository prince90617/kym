using kym.Models;
using Newtonsoft.Json;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace kym.Utility.Mvc {
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
  public class ApiAuthorizeAttribute : AuthorizeAttribute {
    protected override bool AuthorizeCore(HttpContextBase httpContext) {
      var isAuthenticated = base.AuthorizeCore(httpContext);
      if (isAuthenticated) {
        string cookieName = FormsAuthentication.FormsCookieName;
        if (httpContext.Request.Cookies == null ||
            httpContext.Request.Cookies[cookieName] == null) {
          return false;
        }

        var authCookie = httpContext.Request.Cookies[cookieName];
        var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

        // This is where you can read the userData part of the authentication
        // cookie and fetch the token
        var tokenInformation = JsonConvert.DeserializeObject<LoginResult>(authTicket.UserData);

        if (authTicket.Expired) {
          try {
            tokenInformation = AdminService.Instance.AuthenticateAsync<LoginResult>(tokenInformation.RefreshToken).Result;
            authTicket = new FormsAuthenticationTicket(
                2,
                authTicket.Name,
                tokenInformation.Issued.UtcDateTime,
                tokenInformation.Expires.UtcDateTime,
                authTicket.IsPersistent,
                JsonConvert.SerializeObject(tokenInformation));
            httpContext.Response.Cookies[FormsAuthentication.FormsCookieName].Value = FormsAuthentication.Encrypt(authTicket);
          } catch (ApiException) {
            return false;
          }
        }

        IPrincipal userPrincipal = new ApiPrincipal(
          new ApiIdentity(
            authTicket.Name, 
            tokenInformation.AccessToken, 
            tokenInformation.RefreshToken), 
          tokenInformation.Role);

        // Inject the custom principal in the HttpContext
        httpContext.User = userPrincipal;
      }
      return isAuthenticated;
    }
  }
}