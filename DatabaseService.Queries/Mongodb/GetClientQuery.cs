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
    public static class GetClientQuery
    {
        public static DatabaseService.Models.Client GetClient(int ClientId)
        {
            Mapper.CreateMap<DatabaseService.Models.Client, Client>().ForMember(src => src._id, option => option.Ignore()).ReverseMap();
                var _database = MongodbClient.GetDatabaseClient();
                var collection = _database.GetCollection<BsonDocument>("client");
                var filter = Builders<BsonDocument>.Filter.Eq("Id", ClientId);
                var doc = collection.Find(filter).ToList();
                var result = BsonSerializer.Deserialize<Client>(doc[0]);
                return Mapper.Map<DatabaseService.Models.Client>(result);

        }
    }
}
