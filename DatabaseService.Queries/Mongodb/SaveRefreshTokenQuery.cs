using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace DatabaseService.Queries.Mongodb
{
    public static class SaveRefreshTokenQuery
    {
        public static void SaveRefreshToken(dynamic inputs)
        {
            try
            {
                string token = inputs.HashedToken;
                string Username = inputs.Username;
                int ClientId = inputs.ClientId;
                DateTime expires_on = inputs.ExpiresUtc;
                DateTime issued_on = inputs.IssuedUtc;
                string protected_ticket = inputs.ProtectedTicket;
                var document = new BsonDocument
                            {
                               { "token", token },
                               { "username", Username },
                               { "client_id", ClientId },
                               { "expires_on", expires_on },
                               { "issued_on", issued_on },
                               { "protected_ticket", protected_ticket }
                            };
                var _database = MongodbClient.GetDatabaseClient();
                var collection = _database.GetCollection<BsonDocument>("client");
                var filter = Builders<BsonDocument>.Filter.Eq("Id", ClientId);
                var update = Builders<BsonDocument>.Update.Push("RefreshToken", document);
                var result = collection.UpdateOne(filter, update);
            }
            catch (Exception ex)
            {
                BaseClass.logger.Error("Database Query SaveRefreshToken: ", ex);
               
            }
        }
    }
}
