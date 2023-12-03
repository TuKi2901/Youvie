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

        public async Task<DTO_Accounts> IsExistAccount(string email, string password)
        {
            try
            {
                var collection = db.GetCollection<DTO_Accounts>(collectionName);
                DTO_Accounts account = await collection.Find(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();

                if (account == null)
                {
                    throw new Exception($"Don't found user with {email}");
                }

                return account;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in IsExistAccount: {ex.Message}");
            }
        }

        public async Task<dynamic> Login(string email, string password)
        {
            try
            {
                //kiểm tra email và password trong account. Nếu đúng trả về đối tượng "account"
                var account = await IsExistAccount(email, password);

                var collectionAdmin = db.GetCollection<DTO_Admins>(collectionName);
                var admin = await collectionAdmin.Find(x => x.Account.Id == account.Id).FirstOrDefaultAsync();
                if (admin != null)
                    return admin;

                var collectionUser = db.GetCollection<DTO_Users>(collectionName);
                var user = await collectionUser.Find(x => x.Account.Id == account.Id).FirstOrDefaultAsync();
                if (user != null)
                    return user;
                else
                    throw new Exception();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Login_Account: {ex.Message}");
            }
        }
    }
}
