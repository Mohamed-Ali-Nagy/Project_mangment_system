﻿using System.Linq.Expressions;

namespace Project_management_system.Repositories
{
    public interface IBaseRepository<T>
    {
        
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<TResult> GetAsyncWithProjectTo<TResult>(Expression<Func<TResult, bool>> predicate);
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        void Update(T entity);
        public void Add(T entity);
        void SaveChanges();
        public void Delete(T entity);
        public  Task SaveChangesAsync();
       
        Task<T> GetByID(int id);
        Task Delete(int id);
        //T First(Expression<Func<T, bool>> predicate);
    }
}
