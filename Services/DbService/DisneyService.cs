using DisneyApi.Repositories;
using DisneyApi.Services.DbService.Base;

namespace DisneyApi.Services.DbService
{
    public class DisneyService<TEntity> : GenericDbService<TEntity, DisneyRepository<TEntity>> where TEntity : class
    {
        public DisneyService(DisneyRepository<TEntity> repository) : base(repository)
        {
        }
    }
}
