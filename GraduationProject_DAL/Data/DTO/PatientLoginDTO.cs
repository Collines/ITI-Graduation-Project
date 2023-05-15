using System.ComponentModel.DataAnnotations;

namespace GraduationProject_DAL.Data.DTO
{
	public class PatientLoginDTO
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public required string Email { get; set; }
		[Required]
		public required string Password { get; set; }
	}
}
