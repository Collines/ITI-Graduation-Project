using GraduationProject_DAL.Data.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject_BL.DTO.BannerDTOs
{
    public class BannerInsertDTO
    {

        public int Id { get; set; }

        public required string Title_EN { get; set; }

        public required string Title_AR { get; set; }

        public required string Description_EN { get; set; }

        public required string Description_AR { get; set; }

        public  string Image { get; set; }
    }
}
