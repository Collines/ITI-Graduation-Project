namespace GraduationProject_DAL.Interfaces
{
    public interface ITranslations<T>
    {
        public Task<T?> FindAsync(int parentId);
        public Task InsertAsync(T item);
        public Task UpdateAsync(int id, int parentId, T item);
        public Task DeleteAsync(int id, int parentId);
    }
}
