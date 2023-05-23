using GraduationProject_DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraduationProject_BL.DTO.PatientDTOs;

namespace GraduationProject_BL.DTO
{
    public class ReservationDTO
    {
        public  int Id { get; set; }

        public DateTime DateTime { get; set; }

        public int? Queue { get; set; }

        public int PatientId { get; set; }

        public PatientDTO? Patient { get; set; }

        public int DoctorId { get; set; }

        public DoctorDTO? Doctor { get; set; }
    }
}
