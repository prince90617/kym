using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace DatabaseService.Queries.Mongodb
{
    public static class DeleteRefreshTokenQuery
    {
        public static void DeleteRefreshToken(string HashedToken)
         {
             //var _database = MongodbClient.GetDatabaseClient();
             //var collection = _database.GetCollection<BsonDocument>("client");
             //var filter = Builders<BsonDocument>.Filter.Eq("client.RefreshToken.token", HashedToken);
             //var update = Builders<BsonDocument>.Update.Pull("RefreshToken", document);
             //var result = await collection.UpdateOneAsync(filter, update);
             //RefreshToken tokenToDelete;

             //using (var ctx = new SMContext())
             //{
             //    tokenToDelete = ctx.RefreshTokens.Where(s => s.token == HashedToken).FirstOrDefault<RefreshToken>();
             //}

             ////Create new context for disconnected scenario
             //using (var newContext = new SMContext())
             //{
             //    newContext.Entry(tokenToDelete).State = System.Data.Entity.EntityState.Deleted;

             //    newContext.SaveChanges();
             //}  
            
         }
    }
}
