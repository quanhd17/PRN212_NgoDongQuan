using DataAccess;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class BaseService<T> where T : class
    {
        protected readonly IGenericRepository<T> repository;

        public BaseService(IGenericRepository<T> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<T> GetAll() => repository.GetAll();
        public T GetById(object id) => repository.GetById(id);
        public void Insert(T entity)
        {
            repository.Insert(entity);
            repository.Save();
        }
        public void Update(T entity)
        {
            repository.Update(entity);
            repository.Save();
        }
        public void Delete(object id)
        {
            repository.Delete(id);
            repository.Save();
        }
    }
} 