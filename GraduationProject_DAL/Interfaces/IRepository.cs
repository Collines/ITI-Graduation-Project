namespace GraduationProject_DAL.Interfaces
{
    public interface IRepository<T>
    {
        public Task<List<T>> GetAllAsync();
        public Task InsertAsync(T item);
        public Task UpdateAsync(int id, T item);
        public Task DeleteAsync(int id);
    }
}
