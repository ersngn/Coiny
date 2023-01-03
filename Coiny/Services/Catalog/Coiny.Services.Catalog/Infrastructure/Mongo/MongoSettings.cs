namespace Coiny.Services.Catalog.Infrastructure.Mongo;

public class MongoSettings : IMongoSettings
{
    public string CategoryCollectionName { get; set; }
    public string ProductCollectionName { get; set; }
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}