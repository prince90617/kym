namespace DatabaseService.SDK.Models.Request.Authentication {
  public class GetRefreshTokenRequest : BaseRequest {
    public string HashedToken { get; set; }
  }
}
