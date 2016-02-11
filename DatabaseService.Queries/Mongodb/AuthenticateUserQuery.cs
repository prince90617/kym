using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;


namespace DatabaseService.Queries.Mongodb
{
    public static class AuthenticateUserQuery
    {
        public static async Task<DatabaseService.Models.User> AuthenticateUser(string username)
        {
            var _database = MongodbClient.GetDatabaseClient();
            var collection = _database.GetCollection<BsonDocument>("user");
            var filter = Builders<BsonDocument>.Filter.Eq("borough", "Manhattan");
            var doc = await collection.Find(filter).ToListAsync();
            var result = BsonSerializer.Deserialize<DatabaseService.Models.User>(doc[0]);
            return result;
        }
    }

    
}
