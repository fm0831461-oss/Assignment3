namespace Assignment3.Repo.Interface
{
    public interface IGenericRepo<T> where T:class
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetById(int Id);
        ICollection<T> GetAll();
    }
}
