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

        public async Task<bool> DeleteUserDAL(List<string> userId)
        {
            try
            {
                var collection = db.GetCollection<DTO_Users>(collectionName);

                var filter = Builders<DTO_Users>.Filter.In(x => x.Id, userId);
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
            var collection = db.GetCollection<DTO_Users>(collectionName);

            DTO_Users user = await collection.Find(x => x.Account.Email == email).FirstOrDefaultAsync();

            return user;
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

        public async Task<bool> UpdateUser(DTO_Users _user)
        {
            try
            {
                var collection = db.GetCollection<DTO_Users>(collectionName);

                var filter = Builders<DTO_Users>.Filter.Eq(x => x.Id, _user.Id);
                var update = Builders<DTO_Users>.Update.Set(x => x.UserName, _user.UserName)
                                                        .Set(x => x.Gender, _user.Gender)
                                                        .Set(x => x.PhoneNumber, _user.PhoneNumber)
                                                        .Set(x => x.Birthday, _user.Birthday)
                                                        .Set(x => x.Country, _user.Country);

                await collection.UpdateOneAsync(filter, update);

                return true;
            } 
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdatePasswordUser(string email, string password)
        {
            try
            {
                var collection = db.GetCollection<DTO_Users>(collectionName);

                var filter = Builders<DTO_Users>.Filter.Eq(x => x.Account.Email, email);
                var update = Builders<DTO_Users>.Update.Set(x => x.Account.Password, password);

                await collection.UpdateOneAsync(filter, update);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<DTO_Users>> FindUserWith(string infoUser)
        {
            try
            {
                var collection = db.GetCollection<DTO_Users>(collectionName);

                var filter = Builders<DTO_Users>.Filter.Or(
                                                            Builders<DTO_Users>.Filter.Eq(x => x.UserName, infoUser),
                                                            Builders<DTO_Users>.Filter.Eq(x => x.Gender, infoUser),
                                                            Builders<DTO_Users>.Filter.Eq(x => x.PhoneNumber, infoUser),
                                                            Builders<DTO_Users>.Filter.Eq(x => x.Country, infoUser),
                                                            Builders<DTO_Users>.Filter.Eq(x => x.Account.Email, infoUser)
                                                            //Builders<DTO_Users>.Filter.Eq(x => x.Id, infoUser)
                                                            );
                var users = await collection.Find(filter).ToListAsync();

                return users;
            }
            catch
            {
                throw new Exception("Error in DAL_FindUserWith");
            }
        }
    }
}