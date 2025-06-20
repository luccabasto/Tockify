using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Tockify.Infrastructure.Context
{
    public class MongoContext
    {
        public IMongoDatabase Database { get; }

        public MongoContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
            Database = client.GetDatabase(configuration["MongoDatabase"]);
        }
    }
}