﻿using DisneyApi.Repositories.Base;

namespace DisneyApi.Services.DbService.Base
{
    public interface IGenericDbService<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();
        TEntity Get(int id);
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(int id);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(int id);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
    }
}