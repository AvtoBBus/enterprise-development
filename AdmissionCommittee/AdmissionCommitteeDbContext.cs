using AdmissionCommittee.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AdmissionCommittee.Domain;

public class AdmissionCommitteeDbContext : DbContext
{
    public AdmissionCommitteeDbContext(DbContextOptions<AdmissionCommitteeDbContext> options) : base(options)
    {
    }

    public DbSet<Applicant> Applicant { get; set; }
    public DbSet<Direction> Direction { get; set; }
    public DbSet<ExamResult> ExamResult { get; set; }
    public DbSet<Speciality> Speciality { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}