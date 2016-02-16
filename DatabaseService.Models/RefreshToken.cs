using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace DatabaseService.Models {

  public class RefreshToken {

    public ObjectId _id { get; set; }

    public string Token { get; set; }

    public string Username  { get; set; }

    public int ClientId { get; set; }

    public DateTime IssuedUtc { get; set; }

    public DateTime ExpiresUtc { get; set; }

    public string ProtectedTicket { get; set; }
  }
}
