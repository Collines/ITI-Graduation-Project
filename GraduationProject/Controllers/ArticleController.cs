using GraduationProject_BL.DTO.ArticleDTOs;
using GraduationProject_BL.DTO.DoctorDTOs;
using GraduationProject_BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        public IHttpContextAccessor HttpContextAccessor { get; }
        public IArticleManager manager { get; }

        public ArticleController(IHttpContextAccessor _httpContextAccessor, IArticleManager _manager)
        {
            HttpContextAccessor = _httpContextAccessor;
            manager = _manager;
        }

        [HttpGet]
        public async Task<ActionResult<List<ArticleDTO>>> GetAll()
        {
            var Articles = await manager.GetAllAsync(Utils.GetLang(HttpContextAccessor));
            if(Articles!=null && Articles.Count>0) {
                return Ok(Articles);
            }
            return NotFound();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<ArticleDTO>>> GetById(int id)
        {
            var Article = await manager.GetByIdAsync(id,Utils.GetLang(HttpContextAccessor));
            if (Article != null)
            {
                return Ok(Article);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] ArticleFormData formData)
        {
            if (ModelState.IsValid)
            {
                await manager.InsertAsync(formData);
                return Ok();
            }
            return BadRequest();
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            await manager.DeleteAsync(id);
            return Ok(id);
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<ArticleInsertDTO>> Update(int id, [FromForm] ArticleFormData formData)
        {
            await manager.UpdateAsync(id, formData);
            return Ok();
        }

    }
}
