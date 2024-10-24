using AutoMapper;
using AdmissionCommittee.Application.DTO;
using AdmissionCommittee.Domain.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdmissionCommittee.Application;

public class AutoMapperP : Profile
{
    /// <summary>
    /// Convert from DTO into objects
    /// </summary>
    public AutoMapperP()
    {
        CreateMap<Direction, DirectionDto>().ReverseMap();
        CreateMap<ExamResult, ExamResultDto>().ReverseMap();
    }

}
