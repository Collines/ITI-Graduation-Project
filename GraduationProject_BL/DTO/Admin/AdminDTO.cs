namespace GraduationProject_BL.DTO.Admin
{
    public class AdminDTO
    {
        public required int Id { get; set; }
        public required string UserName { get; set; }
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public required long Expiration { get; set; }
    }
}
