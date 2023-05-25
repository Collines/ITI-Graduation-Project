namespace GraduationProject_BL.DTO.DepartmentDTOs
{
    public class DepartmentInsertDTO
    {
        public int Id { get; set; }
        public required string Title_EN { get; set; }
        public required string Title_AR { get; set; }
        public required string Description_EN { get; set; }
        public required string Description_AR { get; set; }
    }
}
