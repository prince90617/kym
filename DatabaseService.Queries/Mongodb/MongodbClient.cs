using MongoDB.Bson;
using MongoDB.Driver;

namespace DatabaseService.Queries.Mongodb
{
   public class MongodbClient
    {
       public static IMongoDatabase GetDatabaseClient()
       {
        var credential = MongoCredential.CreateMongoCRCredential("admin", "superuser", "secure@123");

        var settings = new MongoClientSettings
        {
            Credentials = new[] { credential }
        };

            var mongoClient =new MongoClient(settings);
            return mongoClient.GetDatabase("wc");
       }
    }
}
