using System.Linq.Expressions;
using AutoMapper;
using Coiny.Common.Models.Base;
using Coiny.Services.Catalog.Infrastructure.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Coiny.Services.Catalog.Repositories.Base;

public class RepositoryBase<T> : IRepositoryBase<T> where T : Entity
{
    private readonly IMongoCollection<T> _collection;
    private readonly IMapper _mapper;

    public RepositoryBase(IMapper mapper,IMongoSettings mongoSettings)
    {
        var dbContext = new MongoDbContext(mongoSettings);
        _collection = dbContext.GetCollection<T>();
        _mapper = mapper;
    }

    public async Task<T> AddAsync(T entity)
        {
            var options = new InsertOneOptions { BypassDocumentValidation = false };
            await _collection.InsertOneAsync(entity, options);
            return entity;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<T> entities)
        {
            var options = new BulkWriteOptions { IsOrdered = false, BypassDocumentValidation = false };
            return (await _collection.BulkWriteAsync((IEnumerable<WriteModel<T>>)entities, options)).IsAcknowledged;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            return await _collection.FindOneAndDeleteAsync(x => x.Id == entity.Id);
        }

        public async Task<T> DeleteAsync(string id)
        {
            return await _collection.FindOneAndDeleteAsync(x => x.Id == id);
        }

        public Task<T> DeleteAsync(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>>? filter = null)
        {
            return filter == null ? _collection.AsQueryable() : _collection.AsQueryable().Where(filter);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(string Id)
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(Id));
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>>? filter = null)
        {
            return filter == null ? await _collection.AsQueryable().ToListAsync() : await _collection.Find(filter).ToListAsync();
        }

        public async Task<T> UpdateAsync(string id, T entity)
        {
            return await _collection.FindOneAndReplaceAsync(e => e.Id == id, entity);
        }

        public async Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> predicate)
        {
            return await _collection.FindOneAndReplaceAsync(predicate, entity);
        }
}