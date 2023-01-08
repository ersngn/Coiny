using System.Linq.Expressions;
using AutoMapper;
using Coiny.Services.Catalog.Infrastructure.Mongo;
using Coiny.Services.Catalog.Repositories.Base;
using Coiny.Services.Catalog.Repositories.Category;
using MongoDB.Driver;

namespace Coiny.Services.Catalog.Repositories.Product;

public class ProductRepository : RepositoryBase<Models.Product.Product>, IProductRepository
{
    private readonly IMongoCollection<Models.Product.Product> _collection;

    public ProductRepository(IMapper mapper, IMongoSettings mongoSettings) : base(mapper, mongoSettings)
    {
        var dbContext = new MongoDbContext(mongoSettings);
        _collection = dbContext.GetCollection<Models.Product.Product>();
    }
    
}