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

        public async Task<DTO_Users> GetUserByEmail(string email)
        {
            var collection = db.GetCollection<DTO_Users>(collectionName);

            DTO_Users user = await collection.Find(x => x.Account.Email == email).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception();
            }

            return user;
        }
    }
}