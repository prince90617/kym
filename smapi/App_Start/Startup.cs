using smapi.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;


[assembly: OwinStartup(typeof(smapi.App_Start.Startup))]
namespace smapi.App_Start
{
  public class Startup {
    public void Configuration(IAppBuilder app) {
      ConfigureOAuth(app);
      var config = new HttpConfiguration();
      WebApiConfig.Register(config);
      app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
      app.UseWebApi(config);
      log4net.Config.XmlConfigurator.Configure();
    }

    public void ConfigureOAuth(IAppBuilder app) {
      var OAuthServerOptions = new OAuthAuthorizationServerOptions() {
        AllowInsecureHttp = true,
        TokenEndpointPath = new PathString("/api/token"),
        AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
        Provider = new SimpleAuthorizationServerProvider(),
        RefreshTokenProvider = new SimpleRefreshTokenProvider()
      };

      //Token Generation
      app.UseOAuthAuthorizationServer(OAuthServerOptions);
      app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
    }
  }
}