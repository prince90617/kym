using System;

namespace DatabaseService.Models {
  public class RefreshToken {
    public string Token { get; set; }
    public string Username  { get; set; }
    public int ClientId { get; set; }
    public DateTime IssuedUtc { get; set; }
    public DateTime ExpiresUtc { get; set; }
    public string ProtectedTicket { get; set; }
  }
}
