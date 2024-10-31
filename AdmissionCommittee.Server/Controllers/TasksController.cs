using AdmissionCommittee.Application.DTO;
using AdmissionCommittee.Domain.Interfaces;
using AdmissionCommittee.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdmissionCommittee.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController(
    IRepository<Applicant, int> applicantRepository,
    IRepository<Direction, int> directionRepository,
    IRepository<ExamResult, int> examResultRepository,
    IRepository<Speciality, int> specialityRepository
    ) : ControllerBase
{


    [HttpGet("ApplicantsByCity")]
    public ActionResult<IEnumerable<Applicant>> ApplicantsByCity(string testCity)
    {
        if (string.IsNullOrEmpty(testCity)) return BadRequest();

        var query = applicantRepository.GetAll().Where(a => a.City == testCity)
                    .ToList();

        return Ok(query);
    }

    [HttpGet("OlderApplicants")]
    public ActionResult<IEnumerable<Applicant>> OlderApplicants(int testYear, DateTime testDateTime)
    {
        if (testYear < 0) return BadRequest();

        var query = applicantRepository.GetAll().Where(a => a.BirthdayDate.AddYears(testYear) < testDateTime)
                    .OrderBy(a => a.FullName)
                    .ToList();

        return Ok(query);
    }

    [HttpGet("SelectBySpeciality")]
    public ActionResult<IEnumerable<ApplicantTotalScoreDto>> SelectBySpeciality(string testSpecialitiesName)
    {
        if (string.IsNullOrEmpty(testSpecialitiesName)) return BadRequest();

        var query = (from specialities in specialityRepository.GetAll()
                     where specialities.Name == testSpecialitiesName
                     join directions in directionRepository.GetAll() on specialities.Id equals directions.SpecialityId
                     join applicants in applicantRepository.GetAll() on directions.ApplicantId equals applicants.Id
                     select new
                     {
                         Applicant = applicants,
                         TotalScore = examResultRepository.GetAll()
                                 .Where(examResult => examResult.ApplicantId == applicants.Id)
                                 .Sum(examResult => examResult.Result)
                     })
                            .OrderByDescending(x => x.TotalScore)
                            .Distinct()
                            .ToList();

        return query != null ? Ok(query) : BadRequest();
    }

    [HttpGet("FirstPrioritySpecialitiesByApplicantsAmount")]
    public ActionResult<IEnumerable<DirectionsGroupWithCountDto>> FirstPrioritySpecialitiesByApplicantsAmount(int testPriorityValue)
    {
        if (testPriorityValue < 0) return BadRequest();

        var query = directionRepository.GetAll()
                    .Where(direction => direction.Priority == testPriorityValue)
                    .GroupBy(direction => direction.SpecialityId)
                    .Select(directions => new
                    {
                        directions,
                        Count = directions.Count()
                    })
                    .ToList();

        return Ok(query);
    }

    [HttpGet("TopRatedApplicants")]
    public ActionResult<IEnumerable<ApplicantWithScoreDto>> TopRatedApplicants()
    {
        var query = applicantRepository.GetAll()
                    .Select(applicant => new
                    {
                        Applicant = applicant,
                        Score = examResultRepository.GetAll()
                        .Where(examRes => examRes.ApplicantId == applicant.Id)
                        .Sum(examRes => examRes.Result)
                    }).OrderByDescending(a => a.Score)
                    .Take(5)
                    .ToList();

        return Ok(query);
    }

    [HttpGet("FavoriteSpecialitiesByopRatedApplicants")]
    public ActionResult<IEnumerable<ApplicantWithSpecialityDto>> FavoriteSpecialitiesByopRatedApplicants()
    {
        var maxScoreByExam = examResultRepository.GetAll()
                   .GroupBy(examRes => examRes.ExamName)
                   .Select(Group => new
                   {
                       ExamName = Group.Key,
                       MaxScore = Group.Max(examRes => examRes.Result)
                   });

        var query = maxScoreByExam
                .Join(
                    examResultRepository.GetAll(),
                    maxScore => maxScore.MaxScore,
                    examRes => examRes.Result,
                    (maxScore, examRes) => new
                    {
                        MaxScore = maxScore,
                        ExamRes = examRes
                    }
                )
                .Where(joined => joined.MaxScore.ExamName == joined.ExamRes.ExamName)
                .Join(
                    applicantRepository.GetAll(),
                    maxScore => maxScore.ExamRes.ApplicantId,
                    applicant => applicant.Id,
                    (maxScore, applicant) => new
                    {
                        MaxScore = maxScore,
                        Applicant = applicant
                    }
                )
                .Join(
                    directionRepository.GetAll(),
                    maxScore => maxScore.Applicant.Id,
                    direction => direction.ApplicantId,
                    (maxScore, direction) => new
                    {
                        maxScore.Applicant,
                        direction.SpecialityId,
                        maxScore.MaxScore.ExamRes.ExamName,
                        MaxScore = maxScore.MaxScore.ExamRes.Result,
                        direction.Priority
                    }
                )
                .Where(speciality => speciality.Priority == 1)
                .Select(speciality => speciality)
                .ToList()
                .Select(q => new
                {
                    applicantId = q.Applicant.Id,
                    specialityId = q.SpecialityId
                });

        return query != null ? Ok(query) : BadRequest();
    }
}
