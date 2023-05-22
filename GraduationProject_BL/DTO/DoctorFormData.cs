using GraduationProject_DAL.Data.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject_BL.DTO
{
    public class DoctorFormData
    {
        public int Id { get; set; }

        public string FirstName_EN { get; set; }

        public string FirstName_AR { get; set; }

        public string LastName_EN { get; set; }

        public string LastName_AR { get; set; }

        public Gender Gender { get; set; }

        public string Title_EN { get; set; }

        public string Title_AR { get; set; }

        public string Bio_EN { get; set; }

        public string Bio_AR { get; set; }

        public int DepartmentId { get; set; }

        public IFormFile? Image { get; set; }
    }
}
