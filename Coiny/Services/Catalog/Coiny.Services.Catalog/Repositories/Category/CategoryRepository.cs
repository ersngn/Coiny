using AutoMapper;
using Coiny.Services.Catalog.Infrastructure.Mongo;
using Coiny.Services.Catalog.Repositories.Base;
using MongoDB.Driver;

namespace Coiny.Services.Catalog.Repositories.Category;

public class CategoryRepository : RepositoryBase<Models.Category.Category>, ICategoryRepository
{
    private readonly IMongoCollection<Models.Category.Category> _collection;

    public CategoryRepository(IMapper mapper, IMongoSettings mongoSettings) : base(mapper, mongoSettings)
    {
        var dbContext = new MongoDbContext(mongoSettings);
        _collection = dbContext.GetCollection<Models.Category.Category>();
    }
}