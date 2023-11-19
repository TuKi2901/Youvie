using DTO;
using MongoDB.Driver;

namespace DAL
{
    public class DAL_Users: ConnectMongoDb
    {
        string collectionName = "users";

        public async Task<bool> CreateUser(DTO_Users dTO_Users)
        {
            try
            {
                var collection = db.GetCollection<DTO_Users>(collectionName);

                await collection.InsertOneAsync(dTO_Users);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}