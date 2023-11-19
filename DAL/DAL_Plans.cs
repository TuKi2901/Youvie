using DTO;
using MongoDB.Driver;

namespace DAL
{
    public class DAL_Plans: ConnectMongoDb
    {
        string collectionName = "plans";

        public async Task<DTO_Plans> GetPlan(int role)
        {
            var collection = db.GetCollection<DTO_Plans>(collectionName);

            DTO_Plans plan = await collection.Find(x => x.Role == role).FirstOrDefaultAsync();

            return plan;
        }
    }
}
