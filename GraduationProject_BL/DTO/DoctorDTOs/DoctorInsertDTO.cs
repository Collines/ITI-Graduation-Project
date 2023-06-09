﻿using GraduationProject_DAL.Data.Enums;
using GraduationProject_DAL.Data.Models;
namespace GraduationProject_BL.DTO.DoctorDTOs
{
    public class DoctorInsertDTO
    {
        public int Id { get; set; }

        public required string FirstName_EN { get; set; }

        public required string FirstName_AR { get; set; }

        public required string LastName_EN { get; set; }

        public required string LastName_AR { get; set; }

        public required Gender Gender { get; set; }

        public required string Title_EN { get; set; }

        public required string Title_AR { get; set; }

        public required string Bio_EN { get; set; }

        public required string Bio_AR { get; set; }

        public required int DepartmentId { get; set; }

        public Image? Image { get; set; }
    }
}
