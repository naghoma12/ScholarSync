using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScholarSyncMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class editProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Nationality_NationalityId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Nationality");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Countries_NationalityId",
                table: "AspNetUsers",
                column: "NationalityId",
                principalTable: "Countries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Countries_NationalityId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Nationality",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationality", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Nationality_NationalityId",
                table: "AspNetUsers",
                column: "NationalityId",
                principalTable: "Nationality",
                principalColumn: "Id");
        }
    }
}
