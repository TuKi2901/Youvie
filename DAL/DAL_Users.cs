using DTO;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Data;

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

        public async Task<bool> DeleteUserDAL(List<string> email)
        {
            try
            {
                var collection = db.GetCollection<DTO_Users>(collectionName);

                var filter = Builders<DTO_Users>.Filter.In(x => x.Account.Email, email);
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

        public async Task<DTO_Users> GetUserByEmail(string email)
        {
            try
            {
                var collection = db.GetCollection<DTO_Users>(collectionName);

                DTO_Users user = await collection.Find(x => x.Account.Email == email).FirstOrDefaultAsync();

                if (user == null)
                {
                    throw new Exception($"Don't found user with {email}");
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in GetUserByEmail: {ex.Message}");
            }
        }

        public async Task<List<DTO_Users>> GetAllUser()
        {
            try
            {
                var collection = db.GetCollection<DTO_Users>(collectionName);

                var users = await collection.Find(_ => true).ToListAsync();

                return users;
            }
            catch
            {
                throw new Exception("Error in DAL_GetAllUser");
            }
        }

        public async Task<bool> UpdateUser(List<DTO_Users> _user)
        {
            try
            {
                var collection = db.GetCollection<DTO_Users>(collectionName);

                foreach (DTO_Users u in _user)
                {
                    var filter = Builders<DTO_Users>.Filter.Eq(x => x.Account.Email, u.Account.Email);
                    var update = Builders<DTO_Users>.Update.Set(x => x.UserName, u.UserName)
                                                           .Set(x => x.Gender, u.Gender)
                                                           .Set(x => x.PhoneNumber, u.PhoneNumber)
                                                           .Set(x => x.Birthday, u.Birthday)
                                                           .Set(x => x.Country, u.Country);
                    await collection.UpdateOneAsync(filter, update);
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