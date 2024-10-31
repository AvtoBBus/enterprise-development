namespace AdmissionCommittee.Application.DTO;
/// <summary>
/// Represents an direction submitted by an applicant for a specific speciality.
/// </summary>
public class DirectionDto
{
    /// <summary>
    /// Identifier of the speciality that the applicant is applying for.
    /// </summary>
    /// <example>1</example>
    public required int SpecialityId { get; set; }

    /// <summary>
    /// Identifier of the applicant who submitted the application.
    /// </summary>
    /// <example>1</example>
    public required int ApplicantId { get; set; }

    /// <summary>
    /// Priority of the application. Lower values indicate higher priority.
    /// </summary>
    /// <example>1</example>
    public required int Priority { get; set; }
}