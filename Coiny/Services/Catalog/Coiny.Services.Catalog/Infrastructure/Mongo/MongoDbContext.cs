using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Coiny.Services.Catalog.Infrastructure.Mongo;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    public MongoDbContext(IMongoSettings mongoSettings)
    {
        var client = new MongoClient(mongoSettings.ConnectionString);
        _database = client.GetDatabase(mongoSettings.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>()
    {
        return _database.GetCollection<T>(typeof(T).Name.Trim());
    }

    public IMongoDatabase GetDatabase()
    {
        return _database;
    }
}