using DatabaseService.Models;
using DatabaseService.SDK.Models.Request.Authentication;
using DatabaseService.SDK.Models.Response.Authentication;
using System.Threading.Tasks;

namespace DatabaseService.SDK.Client {
  public class AuthenticationClient : BaseClient {
    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request) {
      var path = "api/Authentication/Authenticate";
      return await RunClient<AuthenticateRequest, User, AuthenticateResponse>(request, path, (result, response) => {
        if (result == null) {
          response.IsSuccess = false;
          response.ReasonPhrase = "Email or password was incorrect";
        } else {
          response.AuthenticatedUser = result;
        }
      });
    }

    public async Task<GetClientResponse> GetClient(GetClientRequest request) {
      var path = "api/Authentication/GetClient";
      return await RunClient<GetClientRequest, DatabaseService.Models.Client, GetClientResponse>(request, path, (result, response) => {
        response.Client = result;
      });
    }

    public async Task<GetRefreshTokenResponse> GetRefreshToken(GetRefreshTokenRequest request) {
      var path = "api/Authentication/GetRefreshToken";
      return await RunClient<GetRefreshTokenRequest, RefreshToken, GetRefreshTokenResponse>(request, path, (result, response) => {
        if (result == null) {
          response.IsSuccess = false;
          response.ReasonPhrase = "Refresh token not found";
        } else {
          response.RefreshToken = result;
        }
      });
    }

    public async Task<SaveRefreshTokenResponse> SaveRefreshToken(SaveRefreshTokenRequest request) {
      var path = "api/Authentication/SaveRefreshToken";
      return await RunClient<SaveRefreshTokenRequest, SaveRefreshTokenResponse>(request, path);
    }

    public async Task<DeleteRefreshTokenResponse> DeleteRefreshToken(DeleteRefreshTokenRequest request) {
      var path = "api/Authentication/DeleteRefreshToken";
      return await RunClient<DeleteRefreshTokenRequest, DeleteRefreshTokenResponse>(request, path);
    }
  }
}
