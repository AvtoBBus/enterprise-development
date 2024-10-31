namespace AdmissionCommittee.Application.DTO;

public class ExamResultDto
{
    public required int ApplicantId { get; set; }
    public required string ExamName { get; set; }
    public required int Result { get; set; }
}
