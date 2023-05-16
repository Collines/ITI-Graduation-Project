using System.ComponentModel.DataAnnotations;


namespace GraduationProject_DAL.Data.Models
{
	public class PatientRefreshTokens
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string RefreshToken { get; set; }
		public bool IsActive { get; set; } = true;
	}
}
