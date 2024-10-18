using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScholarSyncMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AcademicGoals",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AcademicTranscripts_FileName",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AcademicTranscripts_FilePath",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "AgreeTerms",
                table: "Applications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "AreaCode",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CV_FileName",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CV_FilePath",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CulturalActivities",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentDegreeLevel",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Applications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FundingSources_FileName",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FundingSources_FilePath",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "GPA",
                table: "Applications",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LanguageProficiencyLevel_FileName",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LanguageProficiencyLevel_FilePath",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Major",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MotivationLetter_FileName",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MotivationLetter_FilePath",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Passport_FileName",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Passport_FilePath",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PersonalGoals",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PreviousTravelExperience",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProofOfFinancialAbility_FileName",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProofOfFinancialAbility_FilePath",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProofOfHealthInsurance_FileName",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProofOfHealthInsurance_FilePath",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Recommendationletters_FileName",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Recommendationletters_FilePath",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StreetAddressLine2",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniversityId",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UniversityName",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_CountryId",
                table: "Applications",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_DepartmentId",
                table: "Applications",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_UniversityId",
                table: "Applications",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Countries_CountryId",
                table: "Applications",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Departments_DepartmentId",
                table: "Applications",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Universities_UniversityId",
                table: "Applications",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Countries_CountryId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Departments_DepartmentId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Universities_UniversityId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_CountryId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_DepartmentId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_UniversityId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "AcademicGoals",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "AcademicTranscripts_FileName",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "AcademicTranscripts_FilePath",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "AgreeTerms",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "AreaCode",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "CV_FileName",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "CV_FilePath",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "CulturalActivities",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "CurrentDegreeLevel",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "FundingSources_FileName",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "FundingSources_FilePath",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "GPA",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "LanguageProficiencyLevel_FileName",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "LanguageProficiencyLevel_FilePath",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Major",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "MotivationLetter_FileName",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "MotivationLetter_FilePath",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Passport_FileName",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Passport_FilePath",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "PersonalGoals",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "PreviousTravelExperience",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ProofOfFinancialAbility_FileName",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ProofOfFinancialAbility_FilePath",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ProofOfHealthInsurance_FileName",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ProofOfHealthInsurance_FilePath",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Recommendationletters_FileName",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Recommendationletters_FilePath",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "StreetAddressLine2",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "UniversityId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "UniversityName",
                table: "Applications");
        }
    }
}
