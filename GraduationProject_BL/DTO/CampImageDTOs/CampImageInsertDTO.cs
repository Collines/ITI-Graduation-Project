using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject_BL.DTO.CampImageDTOs
{
    public class CampImageInsertDTO
    {
        public required IFormFile Image { get; set; }
    }
}
