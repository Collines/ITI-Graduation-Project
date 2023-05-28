using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject_DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedNoOfBedsToDepartmentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoOfBeds",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoOfBeds",
                table: "Departments");
        }
    }
}
