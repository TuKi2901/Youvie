using DTO;
using MongoDB.Driver;

namespace DAL
{
    public class DAL_Accounts: ConnectMongoDb
    {
        string collectionName = "accounts";

        private async Task<bool> CheckEmailUnique(string email)
        {
            var collection = db.GetCollection<DTO_Accounts>(collectionName);

            var users = await collection.Find(_ => true).ToListAsync();

            foreach (var u in users)
            {
                if (u.Email == email)
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<DTO_Accounts> CreateAccount(DTO_Accounts dTO_Accounts)
        {
            var collection = db.GetCollection<DTO_Accounts>(collectionName);

            if (dTO_Accounts.Email == null)
            {
                throw new Exception("Email doesn't null.");
            }

            bool checkEmail = await CheckEmailUnique(dTO_Accounts.Email);

            if (!checkEmail)
            {
                throw new Exception("Email is not unique.");
            }

            await collection.InsertOneAsync(dTO_Accounts);

            return dTO_Accounts;
        }

        public async Task<bool> DeleteAccount(List<string> email)
        {
            try
            {
                var collection = db.GetCollection<DTO_Accounts>(collectionName);

                var filter = Builders<DTO_Accounts>.Filter.In(x => x.Email, email);

                var result = await collection.DeleteManyAsync(filter);

                if (result.DeletedCount == 0)
                {
                    throw new Exception();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
