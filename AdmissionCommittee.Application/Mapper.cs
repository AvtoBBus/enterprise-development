using AutoMapper;
using AdmissionCommittee.Application.DTO;
using AdmissionCommittee.Domain.Models;
using AdmissionCommittee.Domain.Repositories;

namespace AdmissionCommittee.Application;

public class Mapper(IMapper mapper)
{
    /// <summary>
    /// Convert <see cref="ApplicantDto"/> object into <see cref="Applicant"/>
    /// </summary>
    /// <param name="item">Item for convert</param>
    /// <returns><see cref="Applicant"/> object</returns>
    public Applicant GetApplicant(ApplicantDto item)
    {
        var applicant = mapper.Map<Applicant>(item);

        return applicant;
    }

    /// <summary>
    /// Convert <see cref="DirectionDto"/> object into <see cref="Direction"/>
    /// </summary>
    /// <param name="item">Item for convert</param>
    /// <returns><see cref="Direction"/> object</returns>
    public Direction GetDirection(DirectionDto item)
    {
        var direction = mapper.Map<Direction>(item);

        ApplicantRepository applicantRepository = new();
        var applicant = applicantRepository.GetById(item.Id);

        direction.ApplicantId = applicant == null ? -1 : applicant.Id;

        SpecialityRepository specialityRepository = new();
        var speciality = specialityRepository.GetById(item.Id);

        direction.SpecialityId = speciality == null ? -1 : speciality.Id;

        return direction;
    }

    /// <summary>
    /// Convert <see cref="ExamResultDto"/> object into <see cref="ExamResult"/>
    /// </summary>
    /// <param name="item">Item for convert</param>
    /// <returns><see cref="ExamResult"/> object</returns>
    public ExamResult GetExamResult(ExamResultDto item)
    {
        var examResult = mapper.Map<ExamResult>(item);

        return examResult;
    }

    /// <summary>
    /// Convert <see cref="SpecialityDto"/> object into <see cref="Speciality"/>
    /// </summary>
    /// <param name="item">Item for convert</param>
    /// <returns><see cref="Speciality"/> object</returns>
    public Speciality GetExamResult(SpecialityDto item)
    {
        var speciality = mapper.Map<Speciality>(item);

        return speciality;
    }
}
