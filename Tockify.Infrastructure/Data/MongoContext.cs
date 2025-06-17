using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Tockify.Domain.Models;

namespace Tockify.Infrastructure.Data
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(IConfiguration configuration)
        {
            // --- ClientUserModel (Id é Guid) ---
            if (!BsonClassMap.IsClassMapRegistered(typeof(ClientUserModel)))
            {
                BsonClassMap.RegisterClassMap<ClientUserModel>(cm =>
                {
                    cm.AutoMap();
                    cm.MapMember(x => x.Id)
                      .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
                });
            }

            // --- CardModel (Id é string, então NÃO tentamos usar GuidSerializer) ---
            if (!BsonClassMap.IsClassMapRegistered(typeof(CardModel)))
            {
                BsonClassMap.RegisterClassMap<CardModel>(cm =>
                {
                    cm.AutoMap();
                    // sem SetSerializer para Id, pois Id é string
                });
            }

            // --- TaskItemModel (Id é Guid) ---
            if (!BsonClassMap.IsClassMapRegistered(typeof(TaskItemModel)))
            {
                BsonClassMap.RegisterClassMap<TaskItemModel>(cm =>
                {
                    cm.AutoMap();
                    cm.MapMember(x => x.Id)
                      .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));

                    cm.MapMember(x => x.ToDoListId)
                      .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
                });
            }

            // Configure o cliente Mongo com GuidRepresentation.Standard de forma global
            var connectionString = configuration.GetConnectionString("MongoConnection");
            var mongoUrl = new MongoUrl(connectionString);
            var settings = MongoClientSettings.FromUrl(mongoUrl);
            settings.GuidRepresentation = GuidRepresentation.Standard;
            var client = new MongoClient(settings);

            var dbName = configuration["MongoDatabase"];
            _database = client.GetDatabase(dbName);
        }

        public IMongoCollection<ClientUserModel> ClientUsers =>
            _database.GetCollection<ClientUserModel>("ClientUsers");

        public IMongoCollection<CardModel> TaskLists =>
            _database.GetCollection<CardModel>("TaskLists");

        public IMongoCollection<TaskItemModel> TaskItems =>
            _database.GetCollection<TaskItemModel>("TaskItems");
    }
}
