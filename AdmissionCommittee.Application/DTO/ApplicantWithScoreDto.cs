using AdmissionCommittee.Domain.Models;

namespace AdmissionCommittee.Application.DTO;
public class ApplicantWithScoreDto
{
    public required Applicant Applicant { get; set; }
    public required int Score { get; set; }
}
