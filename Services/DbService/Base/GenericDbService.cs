using DisneyApi.Repositories.Base;

namespace DisneyApi.Services.DbService.Base
{
    public class GenericDbService<TEntity, TRepository> : IGenericDbService<TEntity> where TEntity : class where TRepository : IRepository<TEntity>
    {
        TRepository _repository;

        public GenericDbService(TRepository repository)
        {
            _repository = repository;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public TEntity Get(int id)
        {
            return _repository.Get(id);
        }

        public List<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public TEntity Insert(TEntity entity)
        {
            return _repository.Insert(entity);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            return await _repository.InsertAsync(entity);
        }

        public TEntity Update(TEntity entity)
        {
            return _repository.Update(entity);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await _repository.UpdateAsync(entity);
        }
    }
}
