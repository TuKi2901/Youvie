using DTO;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Admins: ConnectMongoDb
    {
        private string collectionName = "admins";

        
        public async Task<List<DTO_Admins>> GetAllAdmin()
        {
            try
            {
                var collection = db.GetCollection<DTO_Admins>(collectionName);

                var admin = await collection.Find(_ => true).ToListAsync();

                return admin;
            }
            catch
            {
                throw new Exception("Error in DAL_GetAllAdmin");
            }
        }

        public async Task<bool> CreateAdmin(DTO_Admins dTO_Admins)
        {
            try
            {
                var collection = db.GetCollection<DTO_Admins>(collectionName);
                await collection.InsertOneAsync(dTO_Admins);
                return true;
            }
            catch
            {
                return false;
                
            }
        }

        public async Task<bool> DeleteAdmin(List<string> email)
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

        public async Task<bool> UpdateAdmin(DTO_Admins admin)
        {
            try
            {
                var collection = db.GetCollection<DTO_Admins>(collectionName);

                var filter = Builders<DTO_Admins>.Filter.Eq(x => x.Account.Email, admin.Account.Email);
                var update = Builders<DTO_Admins>.Update.Set(x => x.AdminName, admin.AdminName)
                                                        .Set(x => x.Gender, admin.Gender);

                await collection.UpdateOneAsync(filter, update);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<DTO_Admins>> FindAdminWith(string infoAdmin)
        {
            try
            {
                var collection = db.GetCollection<DTO_Admins>(collectionName);

                var filter = Builders<DTO_Admins>.Filter.Or(
                                                            Builders<DTO_Admins>.Filter.Eq(x => x.AdminName, infoAdmin),
                                                            Builders<DTO_Admins>.Filter.Eq(x => x.Gender, infoAdmin),
                                                            Builders<DTO_Admins>.Filter.Eq(x => x.Account.Email, infoAdmin)
                                                            );
                var admins = await collection.Find(filter).ToListAsync();

                return admins;
            }
            catch
            {
                throw new Exception("Error in DAL_FindAdminWith");
            }
        }
    }
}
