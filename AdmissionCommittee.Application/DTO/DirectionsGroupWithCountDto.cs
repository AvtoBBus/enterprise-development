using AdmissionCommittee.Domain.Models;

namespace AdmissionCommittee.Application.DTO;

public class DirectionsGroupWithCountDto
{
    public required List<Direction> Directions { get; set; }
    public required int Count { get; set; }
}