
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoExample1.Models;
using System.Text.Json;

//

namespace MongoExample1.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<UserTest> _usertestCollection;

        public MongoDBService(IOptions<MongoDBSettings> MongoDBSettings)
        {

            MongoClient client = new MongoClient(MongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(MongoDBSettings.Value.DatabaseName);
            _usertestCollection = database.GetCollection<UserTest>(MongoDBSettings.Value.CollectionName);

        }

        public async Task CreateAsync(UserTest usertest)
        {
            usertest.CreatedDate= DateTime.Now;


            List<UserTest> datas= new List<UserTest>();
           


            var ob1= new UserTest() { fname="vishnu",CreatedDate= DateTime.Now};
            var ob2 = new UserTest() { fname = "Anushree", CreatedDate = DateTime.Now };

            datas.Add(usertest);

            datas.Add(ob1);
            datas.Add(ob2);


            await _usertestCollection.InsertManyAsync(datas);
            return;
        }

        public async Task<List<UserTest>> GetAsync()
        {
            FilterDefinitionBuilder<UserTest> builder = Builders<UserTest>.Filter;
            FilterDefinition<UserTest> filter = builder.Empty;

            //filter = filter & builder.Gte(x => x.CreatedDate, request.StartDate);

            //List<UserTest> transfers = await _dbContext.UserTransfers
            //.Find(filter)
            //.Sort(Builders<UserTest>.Sort.Descending(x => x.CreatedDate))
            //.ToListAsync();

            return await _usertestCollection.Find(filter).ToListAsync();
        }

        public async Task AddToUserTestAsync(string id, string uid)
        {

            FilterDefinition<UserTest> filter = Builders<UserTest>.Filter.Eq("Id", id);
            UpdateDefinition<UserTest> update = Builders<UserTest>.Update.AddToSet<string>("uid", uid);
            await _usertestCollection.UpdateOneAsync(filter, update);
            return;
        }


        public async Task DeleteAsync(string id)
        {

            FilterDefinition<UserTest> filter = Builders<UserTest>.Filter.Eq("Id", id);
            await _usertestCollection.DeleteOneAsync(filter);
            return;

        }
    }
}
