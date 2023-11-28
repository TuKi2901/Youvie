using DTO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Medias: ConnectMongoDb
    {
        private string collectionName = "medias";

        public async Task<List<DTO_Medias>> GetAllMedia()
        {
            try
            {
                var collection = db.GetCollection<DTO_Medias>(collectionName);

                var medias = await collection.Find(_ => true).ToListAsync();

                return medias;
            }
            catch
            {
                throw new Exception("Error in DAL_GetAllMedia");
            }
        }
    }
}
