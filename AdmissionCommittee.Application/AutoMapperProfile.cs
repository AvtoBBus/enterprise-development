using AdmissionCommittee.Application.DTO;
using AdmissionCommittee.Domain.Models;
using AutoMapper;

namespace AdmissionCommittee.Application;

public class AutoMapperProfile : Profile
{
    /// <summary>
    /// Convert from DTO into objects
    /// </summary>
    public AutoMapperProfile()
    {
        CreateMap<Applicant, ApplicantDto>().ReverseMap();
        CreateMap<Direction, DirectionDto>().ReverseMap();
        CreateMap<ExamResult, ExamResultDto>().ReverseMap();
        CreateMap<Speciality, SpecialityDto>().ReverseMap();
    }

}
