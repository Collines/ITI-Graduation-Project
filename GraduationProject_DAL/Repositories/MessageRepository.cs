using GraduationProject_DAL.Data.Context;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace GraduationProject_DAL.Repositories
{
    public class MessageRepository : IRepository<Message>
    {
        public MessageRepository(HospitalDBContext _context)
        {
            context = _context;
        }

        public HospitalDBContext context { get; }

        public async Task DeleteAsync(int id)
        {
            var message = await context.Messages.FindAsync(id);
            if(message !=null)
            {
                context.Remove(message);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Message>> GetAllAsync()
        {
            return await context.Messages.ToListAsync();
        }

        public async Task InsertAsync(Message item)
        {
            if(item != null)
            {
                await context.AddAsync(item);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(int id, Message item)
        {
            var currMessage = await context.Messages.FindAsync(id);
            if(currMessage != null && currMessage.Id == item.Id) { 
                currMessage.Subject = item.Subject;
                currMessage.SenderName = item.SenderName;
                currMessage.Status = item.Status;
                currMessage.Body = item.Body;
            }
        }
    }
}
