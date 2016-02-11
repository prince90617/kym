using DatabaseService.Models;

namespace DatabaseService.SDK.Models.Response.Authentication {
  public class AuthenticateResponse : BaseResponse {
    public DatabaseService.Models.User AuthenticatedUser { get; set; }
  }
}
