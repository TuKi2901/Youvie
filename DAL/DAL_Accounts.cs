using DTO;
using MongoDB.Driver;

namespace DAL
{
    public class DAL_Accounts : ConnectMongoDb
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

        public async Task<bool> UpdateAccount(DTO_Accounts dTO_Accounts)
        {
            try
            {
                var collection = db.GetCollection<DTO_Accounts>(collectionName);

                var filter = Builders<DTO_Accounts>.Filter.Eq(x => x.Email, dTO_Accounts.Email);
                var update = Builders<DTO_Accounts>.Update.Set(x => x.Password, dTO_Accounts.Password);

                await collection.UpdateOneAsync(filter, update);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAccount(List<string> accountId)
        {
            try
            {
                var collection = db.GetCollection<DTO_Accounts>(collectionName);
                
                var filter = Builders<DTO_Accounts>.Filter.In(x => x.Id, accountId);

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

        //public async Task<string> Login(string email)
        //{
        //    try
        //    {
        //        var collection = db.GetCollection<DTO_Users>(collectionName);

        //        DTO_Users user = await collection.Find(x => x.Account.Email == email).FirstOrDefaultAsync();

        //        if (user == null)
        //        {
        //            throw new Exception($"Don't found user with {email}");
        //        }

        //        return user;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Error in GetUserByEmail: {ex.Message}");
        //    }
        //}
    }
}
