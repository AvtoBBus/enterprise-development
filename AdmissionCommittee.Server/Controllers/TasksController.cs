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
    [HttpGet("{taskId}")]
    public ActionResult Get(int taskId)
    {
        List<Applicant> _applicants = applicantRepository.GetAll();
        List<Direction> _directions = directionRepository.GetAll();
        List<ExamResult> _examResults = examResultRepository.GetAll();
        List<Speciality> _specialities = specialityRepository.GetAll();

        var testCity = "Vladivostok";
        var testYear = 20;
        var testDateTime = new DateTime(2024, 10, 10);
        var testSpecialitieName = "Cyber Security";
        var testPriorityValue = 1;

        object query = null;

        switch (taskId)
        {
            case 1:
                query = _applicants.Where(a => a.City == testCity)
                    .Select(a => a.Id)
                    .ToList();
                break;
            case 2:
                query = _applicants.Where(a => a.BirthdayDate.AddYears(testYear) < testDateTime)
                    .OrderBy(a => a.FullName)
                    .Select(a => a.Id)
                    .ToList();
                break;
            case 3:
                query = (from specialities in _specialities
                         where specialities.Name == testSpecialitieName
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
                            .Select(x => x.Applicant.FullName)
                            .Distinct()
                            .ToList();
                break;
            case 4:
                query = _directions
                    .Where(direction => direction.Priority == testPriorityValue)
                    .GroupBy(direction => direction.SpecialityId)
                    .Select(group => new
                    {
                        group.Key,
                        Count = group.Count()
                    })
                    .Select(result => result.Count)
                    .ToList();
                break;
            case 5:
                query = _applicants
                    .Select(applicant => new
                    {
                        Applicant = applicant,
                        Score = _examResults
                        .Where(examRes => examRes.ApplicantId == applicant.Id)
                        .Sum(examRes => examRes.Result)
                    }).OrderByDescending(a => a.Score)
                    .Take(5)
                    .Select(a => a.Applicant.Id)
                    .ToList();
                break;
            case 6:
                var maxScoreByExam = _examResults
                   .GroupBy(examRes => examRes.ExamName)
                   .Select(Group => new
                   {
                       ExamName = Group.Key,
                       MaxScore = Group.Max(examRes => examRes.Result)
                   });

                query = maxScoreByExam
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
                break;
            default:
                return BadRequest();
        }

        return query != null ? Ok(query) : BadRequest();
    }
}
