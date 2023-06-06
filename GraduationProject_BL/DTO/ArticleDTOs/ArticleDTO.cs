namespace GraduationProject_BL.DTO.ArticleDTOs
{
    public class ArticleDTO
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Image { get; set; }
        public required DateTime PostedAt { get; set; }
    }
}
