﻿using ScholarSyncMVC.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScholarSyncMVC.Models
{
    public class Applicationn:BaseEntity
    {
        public Scholarship? Scholarship { get; set; }
        public int? ScholarshipId { get; set; }
        public AppUser User { get; set; }

        //the default in identity for id datatype "string"
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public string Status { get; set; }
        public DateTime Date { get; set; }


        // Personal Details
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string Email { get; set; }

        // Address
        public string StreetAddress { get; set; }
        public string? StreetAddressLine2 { get; set; }

        // Contact
        public AreaCode AreaCode { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }

        // Academic Qualifications

        // unversity mmkn downlist w major
        public string UniversityName { get; set; }
        public string Major { get; set; }
        public decimal GPA { get; set; }
        // m4 3arfa
        public string CurrentDegreeLevel { get; set; }
       // public IFormFile AcademicTranscripts { get; set; }

        public string AcademicTranscripts_FilePath { get; set; }
        public string AcademicTranscripts_FileName { get; set; }


        //public IFormFile LanguageProficiencyLevel { get; set; }
        public string LanguageProficiencyLevel_FilePath { get; set; }
        public string LanguageProficiencyLevel_FileName { get; set; }




        // Supporting Documents
        //public IFormFile CV { get; set; }
        public string CV_FilePath { get; set; }
        public string CV_FileName { get; set; }


        //public IFormFile MotivationLetter { get; set; }
        public string MotivationLetter_FilePath { get; set; }
        public string MotivationLetter_FileName { get; set; }


        //public IFormFile Recommendationletters { get; set; }
        public string Recommendationletters_FilePath { get; set; }
        public string Recommendationletters_FileName { get; set; }



        //public IFormFile Passport { get; set; }
        public string Passport_FilePath { get; set; }
        public string Passport_FileName { get; set; }




        // Cultural Experience
        public string? PreviousTravelExperience { get; set; }
        public string? CulturalActivities { get; set; }

        // Goals
        public string? AcademicGoals { get; set; }
        public string? PersonalGoals { get; set; }

        // Funding
        //public IFormFile ProofOfFinancialAbility { get; set; }
        public string ProofOfFinancialAbility_FilePath { get; set; }
        public string ProofOfFinancialAbility_FileName { get; set; }


        //public IFormFile FundingSources { get; set; }
        public string FundingSources_FilePath { get; set; }
        public string FundingSources_FileName { get; set; }



        // Health Insurance
        //public IFormFile? ProofOfHealthInsurance { get; set; }
        public string ProofOfHealthInsurance_FilePath { get; set; }
        public string ProofOfHealthInsurance_FileName { get; set; }



        public bool AgreeTerms { get; set; }


        public University University { get; set; }
        [ForeignKey(nameof(University))]
        public int UniversityId { get; set; }

        public Country Country { get; set; }
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        public Department Department { get; set; }
        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }

    }
}