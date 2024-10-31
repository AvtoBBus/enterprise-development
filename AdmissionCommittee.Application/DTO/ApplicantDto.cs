namespace AdmissionCommittee.Application.DTO;

public class ApplicantDto
{
    public required string FullName { get; set; }
    public required DateTime BirthdayDate { get; set; }
    public required string Country { get; set; }
    public required string City { get; set; }
}
