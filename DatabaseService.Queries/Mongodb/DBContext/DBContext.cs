using DatabaseService.Models.Enums;
using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using AutoMapper;

namespace DatabaseService.Queries.Mongodb
{
   


    [BsonIgnoreExtraElements]
    public class Client
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("Id")]
        public int Id { get; set; }
        [BsonElement("Secret")]
        public string Secret { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Active")]
        public bool Active { get; set; }

        public int RefreshTokenLifeTime { get; set; }
        [BsonElement("AllowedOrigin")]
        public string AllowedOrigin { get; set; }
  }
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Photo { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Name { get; set; }
        public int Active { get; set; }
        public string RefreshToken { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class RefreshToken
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("token")]
        public string Token { get; set; }
        [BsonElement("username")]
        public string Username { get; set; }
        [BsonElement("client_id")]
        public int ClientId { get; set; }
        [BsonElement("issued_on")]
        public DateTime IssuedUtc { get; set; }
        [BsonElement("expires_on")]
        public DateTime ExpiresUtc { get; set; }
        [BsonElement("protected_ticket")]
        public string ProtectedTicket { get; set; }
    }
   
}
