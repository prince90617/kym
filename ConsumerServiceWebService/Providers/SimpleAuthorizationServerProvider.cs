using DatabaseService.Models.Enums;
using DatabaseService.SDK.Client;
using DatabaseService.SDK.Models.Request.Authentication;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using smlib;

namespace ConsumerServiceWebService.Providers
{
  public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider {
    public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context) {
      string clientIdValue = string.Empty;
      string clientSecret = string.Empty;
      int clientId = 0;

      if (!context.TryGetBasicCredentials(out clientIdValue, out clientSecret)) {
        context.TryGetFormCredentials(out clientIdValue, out clientSecret);
      }

      if (string.IsNullOrWhiteSpace(clientIdValue)) {
        context.SetError("invalid_clientId", "Client id should be sent");
        return;
      }

      if (!int.TryParse(clientIdValue, out clientId)) {
        context.SetError("invalid_clientId", "Client id is invalid");
        return;
      }

      var authenticationClient = new AuthenticationClient();
      var client = await authenticationClient.GetClient(new GetClientRequest { ClientId = clientId });

      if (client == null || !client.IsSuccess || client.Client == null) {
        context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system", clientId));
        return;
      }

      if (client.Client.ApplicationType == ApplicationType.NativeConfidential) {
        if (string.IsNullOrWhiteSpace(clientSecret)) {
          context.SetError("invalid_clientId", "Client secret should be sent");
          return;
        } else if (!PasswordHelper.VerifyPassword(clientSecret, client.Client.Secret)) {
          context.SetError("invalid_clientId", "Client secret is invalid");
          return;
        }
        if (!client.Client.Active) {
          context.SetError("invalid_cliendId", "Client is inactive");
          return;
        }
      }

      context.OwinContext.Set<string>("as:clientAllowedOrigin", client.Client.AllowedOrigin);
      context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.Client.RefreshTokenLifeTime.ToString());

      context.Validated();
    }


    public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context) {
      var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
      if (string.IsNullOrWhiteSpace(allowedOrigin)) {
        allowedOrigin = "*";
      }

      context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

      var authenticationClient = new AuthenticationClient();
      var response = await authenticationClient.Authenticate(new AuthenticateRequest { Username = context.UserName });
      var authenticated = response.IsSuccess && response.AuthenticatedUser != null && PasswordHelper.VerifyPassword(context.Password, response.AuthenticatedUser.HashedPassword);

      if (!authenticated) {
        context.SetError("invalid_grant", "The email or password is incorrect");
        return;
      }

      var claims = new[] {
        new Claim("sub", context.UserName),
        new Claim("role", response.AuthenticatedUser.RoleName),
        new Claim("user_id", response.AuthenticatedUser.Id.ToString())
      };

      var identity = new ClaimsIdentity(claims, context.Options.AuthenticationType);

      if (response.AuthenticatedUser.RoleName == "user") {
        context.SetError("invalid_grant", "The user does not have permissions for this service");
        return;
      }

      var props = new AuthenticationProperties(new Dictionary<string, string> {
        { "as:client_id", context.ClientId == null ? string.Empty : context.ClientId },
        { "userName", context.UserName },
        { "role", response.AuthenticatedUser.RoleName }
      });

      var ticket = new AuthenticationTicket(identity, props);
      context.Validated(ticket);
    }

    public override Task TokenEndpoint(OAuthTokenEndpointContext context) {
      foreach (KeyValuePair<string, string> property in context.Properties.Dictionary) {
        context.AdditionalResponseParameters.Add(property.Key, property.Value);
      }
      return Task.FromResult<object>(null);
    }

    public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context) {
      var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
      var currentClient = context.ClientId;

      if (originalClient != currentClient) {
        context.SetError("invalid_clientId", "Refresh token is issued to a different clientId");
      }

      var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
      var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
      context.Validated(newTicket);

      return Task.FromResult<object>(null);
    }
  }
}