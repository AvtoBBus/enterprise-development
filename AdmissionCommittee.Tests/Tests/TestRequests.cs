using AdmissionCommittee.Tests.Fixtures;

namespace AdmissionCommittee.Tests.Tests;

/// <summary>
/// contains unit tests for testing various operations related to applicants, applications, and specialities.
/// </summary>
/// <param name="fixture">Fixture to provide data for testing</param>
public class TestRequests(AdmissionComitteeFixture fixture) : IClassFixture<AdmissionComitteeFixture>
{
    private AdmissionComitteeFixture _fixture = fixture;

    /// <summary>
    /// Tests the selection of applicants by city.
    /// </summary>
    [Fact]
    public void TestApplicantsByCity()
    {
        var query = _fixture.Applicants
            .Where(a => a.City == "�����������")
            .Select(a => a.Id)
            .ToList();

        Assert.Equivalent(3, query.Count);
        Assert.Equal([2, 6, 9], query);
    }

    /// <summary>
    /// Tests the selection of applicants who are 20+ years old.
    /// </summary>
    [Fact]
    public void TestOlderApplicants()
    {
        var query = _fixture.Applicants
            .Where(a => a.BirthdayDate.AddYears(20) < DateTime.Now)
            .OrderBy(a => a.Name)
            .Select(a => a.Name)
            .ToList();

        Assert.Equivalent(6, query.Count);
        Assert.Equal(["������", "��������", "����", "�����", "����", "������"], query);
    }

    /// <summary>
    /// Tests the selection of applicants by a specific speciality.
    /// </summary>
    [Fact]
    public void TestSelectBySpeciality()
    {
        var specialityId = _fixture.Specialities
            .Where(sp => sp.Name == "Psychology")
            .Select(sp => sp.Id)
            .FirstOrDefault();
        var ApplicantIds = _fixture.Applications
            .Where(ap => specialityId == ap.SpecialityId)
            .Select(ap => ap.ApplicantId)
            .ToList();
        var query = _fixture.Applicants
            .Where(ab => ApplicantIds
            .Contains(ab.Id))
            .Select
            (
                ab => new
                {
                    Applicant = ab,
                    TotalScore = _fixture.ExamResults
                    .Where(er => er.ApplicantId == ab.Id)
                    .Sum(er => er.Result)
                }
            ).OrderByDescending(x => x.TotalScore)
            .Select(x => x.Applicant.Name)
            .ToList();

        Assert.Equivalent(1, query.Count);
        Assert.Equal(["������"], query);
    }

    /// <summary>
    /// Tests the number of Applicants who applied to specific specialities with first priority.
    /// </summary>
    [Fact]
    public void TestFirstPrioritySpecialitiesByApplicantsAmount()
    { 
        var query1 = _fixture.Applications
            .Where(a => a is { SpecialityId: 0, Priority: 1 })
            .ToList()
            .Count;
        var query2 = _fixture.Applications
            .Where(a => a is { SpecialityId: 1, Priority: 1 })
            .ToList()
            .Count;
        var query3 = _fixture.Applications
            .Where(a => a is { SpecialityId: 2, Priority: 1 })
            .ToList()
            .Count;
        var query4 = _fixture.Applications
            .Where(a => a is { SpecialityId: 3, Priority: 1 })
            .ToList()
            .Count;
        var query5 = _fixture.Applications
            .Where(a => a is { SpecialityId: 4, Priority: 1 })
            .ToList()
            .Count;
        var query6 = _fixture.Applications
            .Where(a => a is { SpecialityId: 5, Priority: 1 })
            .ToList()
            .Count;
        var query7 = _fixture.Applications
            .Where(a => a is { SpecialityId: 6, Priority: 1 })
            .ToList()
            .Count;
        var query8 = _fixture.Applications
            .Where(a => a is { SpecialityId: 7, Priority: 1 })
            .ToList()
            .Count;
        var query9 = _fixture.Applications
            .Where(a => a is { SpecialityId: 8, Priority: 1 })
            .ToList()
            .Count;
        var query10 = _fixture.Applications
            .Where(a => a is { SpecialityId: 9, Priority: 1 })
            .ToList()
            .Count;

        Assert.Equivalent(3, query1);
        Assert.Equivalent(0, query2);
        Assert.Equivalent(0, query3);
        Assert.Equivalent(1, query4);
        Assert.Equivalent(0, query5);
        Assert.Equivalent(2, query6);
        Assert.Equivalent(0, query7);
        Assert.Equivalent(0, query8);
        Assert.Equivalent(1, query9);
        Assert.Equivalent(0, query10);
    }

    /// <summary>
    /// Tests the selection of top 5 Applicants based on their exam scores.
    /// </summary>
    [Fact]
    public void TestTopRatedApplicants()
    {
        var query = _fixture.Applicants
            .Select
            (ab => new
            {
                Applicant = ab,
                Score = _fixture.ExamResults
                .Where(er => er.ApplicantId == ab.Id)
                .Sum(er => er.Result)
            }
            ).OrderByDescending(a => a.Score)
            .Take(5)
            .Select(a => a.Applicant.Name)
            .ToList();

        Assert.Equivalent(5, query.Count);
        Assert.Equal(["����", "��������", "�����", "����", "������"], query);
    }

    /// <summary>
    /// Tests the favourite specialities chosen by the top-rated applicants based on maximum exam scores.
    /// </summary>
    [Fact]
    public void TestFavoriteSpecialitiesByopRatedApplicants()
    {
        var maxScoreByExam = _fixture.ExamResults
            .GroupBy(er => er.ExamName)
            .Select(g => new
            {
                ExamName = g.Key,
                MaxScore = g.Max(er => er.Result)
            });

        var maxScoreByExamWithApplicantId = maxScoreByExam
            .Join(_fixture.ExamResults, maxScore => maxScore.MaxScore, er => er.Result,
            (maxScore, er) => new { maxScore, er })
            .Where(joined => joined.maxScore.ExamName == joined.er.ExamName);

        var maxScoreWithExamAndApplicantInfo = maxScoreByExamWithApplicantId
            .Join(_fixture.Applicants, ms => ms.er.ApplicantId, ab => ab.Id,
            (ms, ab) => new { ms, ab });

        var topRatedApplicantsSpecialities = maxScoreWithExamAndApplicantInfo
            .Join(_fixture.Applications, ms => ms.ab.Id, ap => ap.ApplicantId,
            (ms, ap) => new
            {
                Applicant = ms.ab,
                SpecialityId = ap.SpecialityId,
                ExamName = ms.ms.er.ExamName,
                MaxScore = ms.ms.er.Result,
                Priority = ap.Priority
            });

        var query = topRatedApplicantsSpecialities
            .Where(sp => sp.Priority == 1)
            .Select(sp => sp)
            .ToList();

        Assert.Equivalent(4, query.Count);
        Assert.Equal(["����", "����", "����", "��������"],
                     query.Select(q => q.Applicant.Name).ToList());

        Assert.Equal([0, 0, 0, 8], query.Select(q => q.SpecialityId).ToList());
    }
}
