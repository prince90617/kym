namespace DatabaseService.SDK.Models.Response.Authentication {
  public class GetRefreshTokenResponse : BaseResponse {
    public DatabaseService.Models.RefreshToken RefreshToken { get; set; }
  }
}
