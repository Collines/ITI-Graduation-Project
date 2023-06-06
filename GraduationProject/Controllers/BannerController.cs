using GraduationProject_BL.DTO.BannerDTOs;
using GraduationProject_BL.DTO.DoctorDTOs;
using GraduationProject_BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BannerController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IBannerManger manager;

        public BannerController(IHttpContextAccessor _httpContextAccessor, IBannerManger _manager)
        {
            httpContextAccessor = _httpContextAccessor;
            manager = _manager;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<BannerDTO>>> GetAll()
        {
            var Banners = await manager.GetAllAsync(Utils.GetLang(httpContextAccessor));

            return Ok(Banners);
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BannerDTO>> GetById(int id)
        {
            var banner = await manager.GetByIdAsync(id, Utils.GetLang(httpContextAccessor));

            if (banner == null)
                return NotFound();

            return Ok(banner);
        }
        [HttpGet("InsertDTO/{id:int}")]
        public async Task<ActionResult<BannerInsertDTO>> GetBannerInsertDTO(int id)
        {
            var Banner = await manager.GetInsertDTOByIdAsync(id);

            if (Banner == null)
                return NotFound();

            return Ok(Banner);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBanner([FromForm] BannerFormData formData)
        {
            if (ModelState.IsValid)
            {
                await manager.InsertAsync(formData);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>> DeleteBanner(int id)
        {
            await manager.DeleteAsync(id);
            return Ok(id);
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<BannerInsertDTO>> UpdateBanner(int id, [FromForm] BannerFormData formData)
        {
            await manager.UpdateAsync(id, formData);
            return Ok();
        }
    }
}

