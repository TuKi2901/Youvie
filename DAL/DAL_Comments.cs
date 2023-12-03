using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Comments: ConnectMongoDb
    {
        public async Task<DTO_Comments> AddComment(DTO_Comments comment)
        {
            try
            {
                var collection = db.GetCollection<DTO_Comments>("comments");

                await collection.InsertOneAsync(comment);

                return comment;
            }
            catch
            {
                throw new Exception("Add comment failed!!");
            }
        }
    }
}
