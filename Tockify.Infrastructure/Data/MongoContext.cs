using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Tockify.Domain.Models;

namespace Tockify.Infrastructure.Data
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoConnection");
            var dbName = configuration.GetConnectionString("MongoDatabase");
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(dbName);
        }

        public IMongoCollection<ClientUserModel> ClientUsers =>
            _database.GetCollection<ClientUserModel>("ClientUsers");

        public IMongoCollection<CardModel> TaskLists =>
            _database.GetCollection<CardModel>("ToDoLists");

        public IMongoCollection<TaskItemModel> TaskItems =>
            _database.GetCollection<TaskItemModel>("TaskItems");
    }
}
