using AdmissionCommittee.Domain.Models;

namespace AdmissionCommittee.Application.DTO;

public class ApplicantTotalScoreDto
{
    public required Applicant Applicant { get; set; }
    public required int TotalScore { get; set; }
}
