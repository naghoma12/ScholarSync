using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScholarSyncMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Education_EducationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Education_EduLevel_LevelId",
                table: "Education");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EducationId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EduLevel",
                table: "EduLevel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Education",
                table: "Education");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "EduLevel",
                newName: "EduLevels");

            migrationBuilder.RenameTable(
                name: "Education",
                newName: "Educations");

            migrationBuilder.RenameColumn(
                name: "StartYear",
                table: "Educations",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "SchoolName",
                table: "Educations",
                newName: "Institution");

            migrationBuilder.RenameColumn(
                name: "LevelId",
                table: "Educations",
                newName: "EduLevelId");

            migrationBuilder.RenameColumn(
                name: "EndYear",
                table: "Educations",
                newName: "EndDate");

            migrationBuilder.RenameIndex(
                name: "IX_Education_LevelId",
                table: "Educations",
                newName: "IX_Educations_EduLevelId");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Educations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EduLevels",
                table: "EduLevels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Educations",
                table: "Educations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_AppUserId",
                table: "Educations",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_AspNetUsers_AppUserId",
                table: "Educations",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_EduLevels_EduLevelId",
                table: "Educations",
                column: "EduLevelId",
                principalTable: "EduLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_AspNetUsers_AppUserId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_Educations_EduLevels_EduLevelId",
                table: "Educations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EduLevels",
                table: "EduLevels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Educations",
                table: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_Educations_AppUserId",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Educations");

            migrationBuilder.RenameTable(
                name: "EduLevels",
                newName: "EduLevel");

            migrationBuilder.RenameTable(
                name: "Educations",
                newName: "Education");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Education",
                newName: "StartYear");

            migrationBuilder.RenameColumn(
                name: "Institution",
                table: "Education",
                newName: "SchoolName");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Education",
                newName: "EndYear");

            migrationBuilder.RenameColumn(
                name: "EduLevelId",
                table: "Education",
                newName: "LevelId");

            migrationBuilder.RenameIndex(
                name: "IX_Educations_EduLevelId",
                table: "Education",
                newName: "IX_Education_LevelId");

            migrationBuilder.AddColumn<int>(
                name: "EducationId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EduLevel",
                table: "EduLevel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Education",
                table: "Education",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EducationId",
                table: "AspNetUsers",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Education_EducationId",
                table: "AspNetUsers",
                column: "EducationId",
                principalTable: "Education",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Education_EduLevel_LevelId",
                table: "Education",
                column: "LevelId",
                principalTable: "EduLevel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
