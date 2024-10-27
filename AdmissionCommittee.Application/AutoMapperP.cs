using AdmissionCommittee.Application.DTO;
using AdmissionCommittee.Domain.Models;
using AutoMapper;

public class AutoMapperP : Profile
{
    /// <summary>
    /// Convert from DTO into objects
    /// </summary>
    public AutoMapperP()
    {
        CreateMap<Applicant, ApplicantDto>().ReverseMap();
        CreateMap<Direction, DirectionDto>().ReverseMap();
        CreateMap<ExamResult, ExamResultDto>().ReverseMap();
        CreateMap<Speciality, SpecialityDto>().ReverseMap();
    }

}
