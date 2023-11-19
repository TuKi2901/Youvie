using DTO;

namespace DAL
{
    public class DAL_Accounts: ConnectMongoDb
    {
        string collectionName = "accounts";

        public async Task<DTO_Accounts> CreateAccount(DTO_Accounts dTO_Accounts)
        {
            var collection = db.GetCollection<DTO_Accounts>(collectionName);

            await collection.InsertOneAsync(dTO_Accounts);

            return dTO_Accounts;
        }
    }
}
