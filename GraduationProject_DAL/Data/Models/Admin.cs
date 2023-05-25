using System.ComponentModel.DataAnnotations;

namespace GraduationProject_DAL.Data.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(14)]
        public required string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
