using AdmissionCommittee.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AdmissionCommittee.Domain;

public class AdmissionCommitteeDbContext(DbContextOptions<AdmissionCommitteeDbContext> options) : DbContext(options)
{

    public DbSet<Applicant> Applicants { get; set; }
    public DbSet<Direction> Directions { get; set; }
    public DbSet<ExamResult> ExamResults { get; set; }
    public DbSet<Speciality> Specialities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}