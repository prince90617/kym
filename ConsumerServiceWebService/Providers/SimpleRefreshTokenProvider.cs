using DatabaseService.SDK.Client;
using DatabaseService.SDK.Models.Request.Authentication;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Threading.Tasks;
using smlib;

namespace ConsumerServiceWebService.Providers
{
  public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider {
    public async Task CreateAsync(AuthenticationTokenCreateContext context) {
      var clientIdValue = context.Ticket.Properties.Dictionary["as:client_id"];
      if (string.IsNullOrWhiteSpace(clientIdValue)) {
        return;
      }

      int clientId = 0;
      if (!int.TryParse(clientIdValue, out clientId)) {
        return;
      }

      var token = Guid.NewGuid().ToString("n");

      var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");
      var issuedUtc = DateTime.UtcNow;
      var expiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime));

      context.Ticket.Properties.IssuedUtc = issuedUtc;
      context.Ticket.Properties.ExpiresUtc = expiresUtc;

      var client = new AuthenticationClient();
      var result = await client.SaveRefreshToken(new SaveRefreshTokenRequest {
        HashedToken = PasswordHelper.HashToken(token),
        ClientId = clientId,
        Username = context.Ticket.Properties.Dictionary["userName"],
        IssuedUtc = DateTime.UtcNow,
        ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime)),
        ProtectedTicket = context.SerializeTicket()
      });

      if (result.IsSuccess) {
        context.SetToken(token);
      }
    }

    public async Task ReceiveAsync(AuthenticationTokenReceiveContext context) {
      var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
      context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
      string hashedToken = PasswordHelper.HashToken(context.Token);

      var authenticationClient = new AuthenticationClient();
      var response = await authenticationClient.GetRefreshToken(new GetRefreshTokenRequest { HashedToken = hashedToken });
      if (response.RefreshToken != null) {
        context.DeserializeTicket(response.RefreshToken.ProtectedTicket);
        await authenticationClient.DeleteRefreshToken(new DeleteRefreshTokenRequest { HashedToken = hashedToken });
      }
    }

    public void Create(AuthenticationTokenCreateContext context) {
      throw new NotImplementedException();
    }

    public void Receive(AuthenticationTokenReceiveContext context) {
      throw new NotImplementedException();
    }
  }
}