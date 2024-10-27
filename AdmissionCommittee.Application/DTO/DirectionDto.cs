namespace AdmissionCommittee.Application.DTO;

public class DirectionDto
{
    public required int Id { get; set; }
    public required int SpecialityId { get; set; }
    public required int ApplicantId { get; set; }
    public required int Priority { get; set; }
}