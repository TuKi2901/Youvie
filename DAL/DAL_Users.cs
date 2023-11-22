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

        public async Task<List<DTO_Users>> GetAllUser()
        {
            try
            {
                var collection = db.GetCollection<DTO_Users>(collectionName);

                var users = await collection.Find(_ => true).ToListAsync();

                if (users == null)
                {
                    throw new Exception();
                }

                //DataTable dataTable = new DataTable();
                //dataTable.Columns.Add("UserName", typeof(string));
                //dataTable.Columns.Add("Gender", typeof(string));
                //dataTable.Columns.Add("PhoneNumber", typeof(string));
                //dataTable.Columns.Add("Birthday", typeof(string));
                //dataTable.Columns.Add("Country", typeof(string));
                //dataTable.Columns.Add("Account", typeof(DTO_Accounts));
                //dataTable.Columns.Add("Plan", typeof(DTO_Plans));

                //List<DTO_Users> l = new List<DTO_Users>();

                //foreach (var user in users)
                //{
                //    l.Add(user);
                //}

                return users;
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}