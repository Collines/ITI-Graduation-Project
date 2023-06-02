using GraduationProject_BL.Interfaces;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;

namespace GraduationProject_BL.Managers
{
    public class MessageManager : IMessageManager
    {
        public MessageManager(IRepository<Message> repository)
        {
            Repository = repository;
        }

        public IRepository<Message> Repository { get; }

        public async Task DeleteAsync(int id)
        {
            await Repository.DeleteAsync(id);
        }

        public async Task<List<Message>> GetAllAsync()
        {
            return await Repository.GetAllAsync();
        }

        public async Task<Message?> GetByIdAsync(int id)
        {
            var messages = await Repository.GetAllAsync();
            return messages.Find(x => x.Id == id);
        }

        public async Task UpdateAsync(int id, Message item)
        {
            await Repository.UpdateAsync(id, item);
        }

        public async Task InsertAsync(Message item)
        {
            if(item !=null)
                await Repository.InsertAsync(item);
        }
    }
}
