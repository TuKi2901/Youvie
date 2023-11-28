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

                var users = await collection.Find(_ => true).ToListAsync();

                return users;
            }
            catch
            {
                throw new Exception("Error in DAL_GetAllAdmin");
            }
        }
    }
}
