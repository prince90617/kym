using DatabaseService.Models;
using System;

namespace DatabaseService.SDK.Models.Request.Authentication {
  public class SaveRefreshTokenRequest : BaseRequest {
    public string HashedToken { get; set; }
    public string Username { get; set; }
    public int ClientId { get; set; }
    public DateTime IssuedUtc { get; set; }
    public DateTime ExpiresUtc { get; set; }
    public string ProtectedTicket { get; set; }
  }
}
