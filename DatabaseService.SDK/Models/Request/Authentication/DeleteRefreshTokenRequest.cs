namespace DatabaseService.SDK.Models.Request.Authentication {
  public class DeleteRefreshTokenRequest : BaseRequest {
    public string HashedToken { get; set; }
  }
}
