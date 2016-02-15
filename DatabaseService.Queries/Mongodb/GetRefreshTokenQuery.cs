using System;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace DatabaseService.Queries.Mongodb
{
    public static class GetRefreshTokenQuery
    {
        public static DatabaseService.Models.RefreshToken GetRefreshToken(string HashedToken)
        {
            try
            {
                Mapper.CreateMap<DatabaseService.Models.RefreshToken, RefreshToken>().ForMember(src => src._id, option => option.Ignore()).ReverseMap();
                var _database = MongodbClient.GetDatabaseClient();
                var collection = _database.GetCollection<BsonDocument>("client.RefreshToken");
                var filter = Builders<BsonDocument>.Filter.Eq("token", HashedToken);
                var doc = collection.Find(filter).ToList();
                var result = BsonSerializer.Deserialize<RefreshToken>(doc[0]);
                return Mapper.Map<DatabaseService.Models.RefreshToken>(result);
            }
            catch (Exception ex)
            {
                BaseClass.logger.Error("Database Query GetRefreshToken: ",ex);
                return null;
            }
        }
    }
}
