using MongoDB.Bson;
using MongoDB.Driver;

namespace DatabaseService.Queries.Mongodb
{

     
   public class MongodbClient
    {
       
       public static IMongoDatabase GetDatabaseClient()
       {
           const string connectionString = @"mongodb://kymuser:secure%40123@ds061385.mongolab.com:61385/kym";

        var mongoClient = new MongoClient(connectionString);

           return mongoClient.GetDatabase("kym");
       }
    }
}
