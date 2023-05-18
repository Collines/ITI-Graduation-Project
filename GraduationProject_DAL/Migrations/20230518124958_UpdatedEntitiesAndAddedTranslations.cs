using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject_DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEntitiesAndAddedTranslations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Departments_DeptId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Doctors_DocId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Patients_PId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "FName",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "FNameAR",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "BioAR",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "FName",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "FNameAR",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "LName",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DescriptionAR",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "TitleAR",
                table: "Departments");

            migrationBuilder.RenameColumn(
                name: "PId",
                table: "Reservations",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "DocId",
                table: "Reservations",
                newName: "DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_PId",
                table: "Reservations",
                newName: "IX_Reservations_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_DocId",
                table: "Reservations",
                newName: "IX_Reservations_DoctorId");

            migrationBuilder.RenameColumn(
                name: "LNameAR",
                table: "Patients",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "LName",
                table: "Patients",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "TitleAR",
                table: "Doctors",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "LNameAR",
                table: "Doctors",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "DeptId",
                table: "Doctors",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Doctors_DeptId",
                table: "Doctors",
                newName: "IX_Doctors_DepartmentId");

            migrationBuilder.CreateTable(
                name: "DepartmentTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title_EN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title_AR = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description_EN = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description_AR = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentTranslations_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName_EN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName_AR = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName_EN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName_AR = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title_EN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title_AR = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Bio_EN = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Bio_AR = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorTranslations_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName_EN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName_AR = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName_EN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName_AR = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientTranslations_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentTranslations_DepartmentId",
                table: "DepartmentTranslations",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorTranslations_DoctorId",
                table: "DoctorTranslations",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_DoctorId",
                table: "Images",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientTranslations_PatientId",
                table: "PatientTranslations",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Departments_DepartmentId",
                table: "Doctors",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Doctors_DoctorId",
                table: "Reservations",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Patients_PatientId",
                table: "Reservations",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Departments_DepartmentId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Doctors_DoctorId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Patients_PatientId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "DepartmentTranslations");

            migrationBuilder.DropTable(
                name: "DoctorTranslations");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "PatientTranslations");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Reservations",
                newName: "PId");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Reservations",
                newName: "DocId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_PatientId",
                table: "Reservations",
                newName: "IX_Reservations_PId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_DoctorId",
                table: "Reservations",
                newName: "IX_Reservations_DocId");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Patients",
                newName: "LNameAR");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Patients",
                newName: "LName");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Doctors",
                newName: "TitleAR");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Doctors",
                newName: "LNameAR");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Doctors",
                newName: "DeptId");

            migrationBuilder.RenameIndex(
                name: "IX_Doctors_DepartmentId",
                table: "Doctors",
                newName: "IX_Doctors_DeptId");

            migrationBuilder.AddColumn<string>(
                name: "FName",
                table: "Patients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FNameAR",
                table: "Patients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BioAR",
                table: "Doctors",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FName",
                table: "Doctors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FNameAR",
                table: "Doctors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Doctors",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LName",
                table: "Doctors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAR",
                table: "Departments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleAR",
                table: "Departments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Departments_DeptId",
                table: "Doctors",
                column: "DeptId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Doctors_DocId",
                table: "Reservations",
                column: "DocId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Patients_PId",
                table: "Reservations",
                column: "PId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
