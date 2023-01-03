namespace Coiny.Services.Catalog.Infrastructure.Mongo;

public interface IMongoSettings
{
    string CategoryCollectionName { get; set; }
    string ProductCollectionName { get; set; }
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }

}