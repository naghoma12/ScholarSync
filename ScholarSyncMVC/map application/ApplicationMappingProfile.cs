using AutoMapper;
using ScholarSyncMVC.Models;
using ScholarSyncMVC.ViewModels;

namespace ScholarSyncMVC.map_application
{
	public class ApplicationMappingProfile : Profile
	{
		public ApplicationMappingProfile()
		{
			CreateMap<applicationnVM, Applicationn>()
				.ForMember(dest => dest.AcademicTranscripts_FilePath, opt => opt.Ignore()) // Ignore file paths
				.ForMember(dest => dest.LanguageProficiencyLevel_FilePath, opt => opt.Ignore())
				.ForMember(dest => dest.CV_FilePath, opt => opt.Ignore())
				.ForMember(dest => dest.MotivationLetter_FilePath, opt => opt.Ignore())
				.ForMember(dest => dest.Recommendationletters_FilePath, opt => opt.Ignore())
				.ForMember(dest => dest.Passport_FilePath, opt => opt.Ignore())
				.ForMember(dest => dest.ProofOfFinancialAbility_FilePath, opt => opt.Ignore())
				.ForMember(dest => dest.FundingSources_FilePath, opt => opt.Ignore())
				.ForMember(dest => dest.ProofOfHealthInsurance_FilePath, opt => opt.Ignore());

			CreateMap<Applicationn, applicationnVM>();
		}
	}
}
