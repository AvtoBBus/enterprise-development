using AdmissionCommittee.Application;
using AdmissionCommittee.Domain.Interfaces;
using AdmissionCommittee.Domain.Models;
using AdmissionCommittee.Domain.Repositories;
using AdmissionCommittee.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

var connectionString = builder.Configuration["ConnectionStrings:postrges"];

builder.Services.AddDbContext<AdmissionCommitteeDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddTransient<IRepository<Applicant, int>, ApplicantRepository>();
builder.Services.AddTransient<IRepository<Direction, int>, DirectionRepository>();
builder.Services.AddTransient<IRepository<ExamResult, int>, ExamResultRepository>();
builder.Services.AddTransient<IRepository<Speciality, int>, SpecialityRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
