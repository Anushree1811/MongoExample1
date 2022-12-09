
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
            //var filter = Builders<BsonDocument>.Filter.Eq("uid", uid);
            //var update = Builders<BsonDocument>.Update.Set("class_id", 483);
            //await _usertestCollection.UpdateOneAsync(filter, update);

            //return;

            FilterDefinition<UserTest> filter = Builders<UserTest>.Filter.Eq("Id", id);
            UpdateDefinition<UserTest> update = Builders<UserTest>.Update.AddToSet<string>("uid", uid);
            await _usertestCollection.UpdateOneAsync(filter, update);
            return;
        }

        public async Task AddToUserTestAsync2(string id, UserTest tstObj)
        {
            //Console.WriteLine(tstObj);

            //FilterDefinition<UserTest> filter = Builders<UserTest>.Filter.Eq("Id", id);
            //UpdateDefinition<UserTest> update = Builders<UserTest>.Update.Set("fname",tstObj.fname).Set("mname",tstObj.mname);
            //await _usertestCollection.UpdateOneAsync(filter, update);
            //return;

            //var filter = Builders<Customer>.Filter
            // .Eq(s => s.uid, tstObj.uid);

            FilterDefinition<UserTest> filter = Builders<UserTest>.Filter.Eq("Id", id);
            UpdateDefinition<UserTest> update = Builders<UserTest>.Update.Set(p => p.uid, tstObj.uid);
            if (!string.IsNullOrWhiteSpace(tstObj.fname))
                update = update.Set(p => p.fname, tstObj.fname);
            if (!string.IsNullOrWhiteSpace(tstObj.mname))
                update = update.Set(p => p.mname, tstObj.mname);
            if (!string.IsNullOrWhiteSpace(tstObj.lname))
                update = update.Set(p => p.lname, tstObj.lname);

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
