using MongoDB.Bson;
using MongoDB.Driver;

namespace DatabaseService.Queries.Mongodb
{
   public class MongodbClient
    {
       public static IMongoDatabase GetDatabaseClient()
       {
        //   var credential = MongoCredential.CreateMongoCRCredential("kym", "kymuser", "secure@123");

        //var settings = new MongoClientSettings
        //{
        //    Credentials = new[] { credential },
        //    Server = new MongoServerAddress("ds061385.mongolab.com", 61385)
        //};
           const string connectionString = @"mongodb://kymuser:secure%40123@ds061385.mongolab.com:61385/kym";

        var mongoClient = new MongoClient(connectionString);

            //var mongoClient =new MongoClient(settings);
            return mongoClient.GetDatabase("kym");
       }
    }
}
