using DisneyApi.Contexts;
using DisneyApi.Repositories.Base;

namespace DisneyApi.Repositories
{
    public class DisneyRepository<TEntity> : GenericRepository<TEntity, DisneyDbContext> where TEntity : class
    {
        public DisneyRepository(DisneyDbContext disneyContext) : base(disneyContext)
        {
        }
    }
}
