using System.Collections.Generic;

namespace prova_surpresa_mvc.Models
{
    public interface IRepositorio<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
