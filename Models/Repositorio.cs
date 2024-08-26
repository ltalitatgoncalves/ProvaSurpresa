using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace prova_surpresa_mvc.Models
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly ContextoEscola _contexto;

        public Repositorio(ContextoEscola contexto)
        {
            _contexto = contexto;
        }

        public IEnumerable<T> GetAll()
        {
            return _contexto.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _contexto.Set<T>().Find(id);
        }

        public void Add(T entity)
        {
            _contexto.Set<T>().Add(entity);
            _contexto.SaveChanges();
        }

        public void Update(T entity)
        {
            _contexto.Entry(entity).State = EntityState.Modified;
            _contexto.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            _contexto.Set<T>().Remove(entity);
            _contexto.SaveChanges();
        }
    }
}
