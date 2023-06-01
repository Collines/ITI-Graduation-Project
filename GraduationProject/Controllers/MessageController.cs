using GraduationProject_BL.DTO.BannerDTOs;
using GraduationProject_BL.Interfaces;
using GraduationProject_DAL.Data.Enums;
using GraduationProject_DAL.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class MessageController : ControllerBase
    {
        public MessageController(IMessageManager manager)
        {
            Manager = manager;
        }

        public IMessageManager Manager { get; }

        [HttpGet]
        public async Task<ActionResult<List<Message>>> GetAll()
        {
            var messages = await Manager.GetAllAsync();
            if(messages!=null && messages.Count > 0)
                return Ok(messages);
            return NotFound();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<Message>>> GetById(int id)
        {
            var message = await Manager.GetByIdAsync(id);
            if (message != null)
                return Ok(message);
            return NotFound();
        }

        [HttpPatch("ChangeStatus/{id:int}")]
        public async Task<ActionResult> ChangeStatus(int id,Message msg)
        {
            var message = await Manager.GetByIdAsync(id);
            if (message != null && msg.Id == id)
            {
                message.Status = msg.Status;
                await Manager.UpdateAsync(id,message);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var message = await Manager.GetByIdAsync(id);
            if (message != null)
            {
                await Manager.DeleteAsync(id);
                return Ok();
            }
            return BadRequest();
        }

    }
}
