using AdmissionCommittee.Application.DTO;
using AdmissionCommittee.Domain.Interfaces;
using AdmissionCommittee.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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

    private readonly List<Applicant> _applicants = applicantRepository.GetAll();
    private readonly List<Direction> _directions = directionRepository.GetAll();
    private readonly List<ExamResult> _examResults = examResultRepository.GetAll();
    private readonly List<Speciality> _specialities = specialityRepository.GetAll();

    [HttpGet("1")]
    public ActionResult<IEnumerable<Applicant>> ApplicantsByCity(string testCity)
    {
        if (string.IsNullOrEmpty(testCity)) return BadRequest();

        var query = _applicants.Where(a => a.City == testCity)
                    .ToList();

        return query != null ? Ok(query) : BadRequest();
    }

    [HttpGet("2")]
    public ActionResult<IEnumerable<Applicant>> OlderApplicants(int testYear, DateTime testDateTime)
    {
        if (testYear < 0) return BadRequest();

        var query = _applicants.Where(a => a.BirthdayDate.AddYears(testYear) < testDateTime)
                    .OrderBy(a => a.FullName)
                    .ToList();

        return query != null ? Ok(query) : BadRequest();
    }

    [HttpGet("3")]
    public ActionResult<IEnumerable<ApplicantTotalScoreDto>> SelectBySpeciality(string testSpecialitiesName)
    {
        if (string.IsNullOrEmpty(testSpecialitiesName)) return BadRequest();

        var query = (from specialities in _specialities
                     where specialities.Name == testSpecialitiesName
                     join directions in _directions on specialities.Id equals directions.SpecialityId
                     join applicants in _applicants on directions.ApplicantId equals applicants.Id
                     select new
                     {
                         Applicant = applicants,
                         TotalScore = _examResults
                                 .Where(examResult => examResult.ApplicantId == applicants.Id)
                                 .Sum(examResult => examResult.Result)
                     })
                            .OrderByDescending(x => x.TotalScore)
                            .Distinct()
                            .ToList();

        return query != null ? Ok(query) : BadRequest();
    }

    [HttpGet("4")]
    public ActionResult<IEnumerable<DirectionsGroupWithCountDto>> FirstPrioritySpecialitiesByApplicantsAmount(int testPriorityValue)
    {
        if (testPriorityValue < 0) return BadRequest();
        
        var query = _directions
                    .Where(direction => direction.Priority == testPriorityValue)
                    .GroupBy(direction => direction.SpecialityId)
                    .Select(directions => new
                    {
                        directions,
                        Count = directions.Count()
                    })
                    .ToList();

        return query != null ? Ok(query) : BadRequest();
    }

    [HttpGet("5")]
    public ActionResult<IEnumerable<ApplicantWithScoreDto>> TopRatedApplicants()
    {
        var query = _applicants
                    .Select(applicant => new
                    {
                        Applicant = applicant,
                        Score = _examResults
                        .Where(examRes => examRes.ApplicantId == applicant.Id)
                        .Sum(examRes => examRes.Result)
                    }).OrderByDescending(a => a.Score)
                    .Take(5)
                    .ToList();

        return query != null ? Ok(query) : BadRequest();
    }

    [HttpGet("6")]
    public ActionResult<IEnumerable<ApplicantWithSpecialityDto>> FavoriteSpecialitiesByopRatedApplicants()
    {
        var maxScoreByExam = _examResults
                   .GroupBy(examRes => examRes.ExamName)
                   .Select(Group => new
                   {
                       ExamName = Group.Key,
                       MaxScore = Group.Max(examRes => examRes.Result)
                   });

        var query = maxScoreByExam
                .Join(
                    _examResults,
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
                    _applicants,
                    maxScore => maxScore.ExamRes.ApplicantId,
                    applicant => applicant.Id,
                    (maxScore, applicant) => new
                    {
                        MaxScore = maxScore,
                        Applicant = applicant
                    }
                )
                .Join(
                    _directions,
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
