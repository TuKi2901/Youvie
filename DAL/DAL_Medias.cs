using DTO;
using MongoDB.Driver;

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

        public async Task<bool> AddMedia(DTO_Medias dTO_Medias)
        {
            try
            {
                var collection = db.GetCollection<DTO_Medias>(collectionName);

                await collection.InsertOneAsync(dTO_Medias);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteMedia(List<string> idMedia)
        {
            try
            {
                var collection = db.GetCollection<DTO_Medias>(collectionName);

                var filter = Builders<DTO_Medias>.Filter.In(x => x.Id , idMedia);
                var result = await collection.DeleteManyAsync(filter);

                if (result.DeletedCount == 0)
                {
                    throw new Exception("DAL_medias Delete have an error!");
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateMedia(DTO_Medias media)
        {
            try
            {
                var collection = db.GetCollection<DTO_Medias>(collectionName);

                var filter = Builders<DTO_Medias>.Filter.Eq(x => x.Id, media.Id);
                var update = Builders<DTO_Medias>.Update.Set(x => x.MediaName, media.MediaName)
                                                        .Set(x => x.Country, media.Country)
                                                        .Set(x => x.ReleaseDate, media.ReleaseDate)
                                                        .Set(x => x.ListCategory, media.ListCategory)
                                                        .Set(x => x.ListEpisode, media.ListEpisode)
                                                        .Set(x => x.RoleMedia, media.RoleMedia)
                                                        .Set(x => x.Decription, media.Decription)
                                                        .Set(x => x.Image, media.Image);

                await collection.UpdateOneAsync(filter, update);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<DTO_Medias>> FindMedia(string infoMedia)
        {
            try
            {
                var collection = db.GetCollection<DTO_Medias>(collectionName);

                var filter = Builders<DTO_Medias>.Filter.Or(
                                                            Builders<DTO_Medias>.Filter.Eq(x => x.MediaName, infoMedia),
                                                            Builders<DTO_Medias>.Filter.Eq(x => x.Country, infoMedia),
                                                            Builders<DTO_Medias>.Filter.AnyEq(x => x.ListCategory, infoMedia)
                                                            //Builders<DTO_Medias>.Filter.Eq(x => x.Id, infoMedia)
                                                            );
                var users = await collection.Find(filter).ToListAsync();

                return users;
            }
            catch
            {
                throw new Exception("Error in DAL_FindUserWith");
            }
        }

        public async Task<DTO_Medias> GetMediaById(string idMedia)
        {
            var collection = db.GetCollection<DTO_Medias>(collectionName);

            DTO_Medias media = await collection.Find(x => x.Id == idMedia).FirstOrDefaultAsync();

            return media;
        }

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
