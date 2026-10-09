using Assignment3.Data;
using Assignment3.Repo.Interface;

namespace Assignment3.Repo.IMP
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly AppDbContext context;
        public GenericRepo(AppDbContext context)
        {
            this.context = context;
        }
        public void Create(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public ICollection<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T GetById(int Id)
        {
           return context.Set<T>().Find(Id);
        }

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }
    }
}
