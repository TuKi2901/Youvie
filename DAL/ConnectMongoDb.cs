using MongoDB.Driver;

namespace DAL
{
    public class ConnectMongoDb
    {
        //protected IMongoDatabase db = new MongoClient("mongodb+srv://tuan:1234@clustertest.vbq8y58.mongodb.net/").GetDatabase("Dirty");
        protected IMongoDatabase db = new MongoClient("mongodb://localhost:27017").GetDatabase("Youvie");
    }
}
