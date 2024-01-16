using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MicroserviceIdentityAPI.Shared.Utils;
using MicroserviceIdentityAPI.Shared.WriteLogs.Interface;

namespace MicroserviceIdentityAPI.Shared.WriteLogs
{
    public class WriteLogCollections : IWriteLogCollections
    {
        private readonly MongoClient _dbClient;

        public WriteLogCollections()
        {
            _dbClient = new MongoClient(Constants.GetConnectionStringMongoDb());
        }

        public void WriteError(string json)
        {
            try
            {
                var dbList = _dbClient.GetDatabase(Constants.GetNameDataBaseMongoDb());
                var collection = dbList.GetCollection<BsonDocument>("LogErrorAPI");

                BsonDocument document = BsonSerializer.Deserialize<BsonDocument>(json);
                collection.InsertOne(document);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        public void WriteRequest(string json)
        {
            try
            {
                var dbList = _dbClient.GetDatabase(Constants.GetNameDataBaseMongoDb());
                var collection = dbList.GetCollection<BsonDocument>("Request");

                BsonDocument document = BsonSerializer.Deserialize<BsonDocument>(json);
                collection.InsertOne(document);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        public void WriteResponse(string json)
        {
            try
            {
                var dbList = _dbClient.GetDatabase(Constants.GetNameDataBaseMongoDb());
                var collection = dbList.GetCollection<BsonDocument>("Response");

                BsonDocument document = BsonSerializer.Deserialize<BsonDocument>(json);
                collection.InsertOne(document);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

    }
}