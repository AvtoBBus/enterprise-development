using AdmissionCommittee.Application;
using AdmissionCommittee.Domain.Interfaces;
using AdmissionCommittee.Domain.Models;
using AdmissionCommittee.Domain.Repositories;
using AdmissionCommittee.Server;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddSingleton<IRepository<Applicant, int>>(_ => new ApplicantRepository(ReaderCSV.GetApplicants(builder.Configuration["DataPaths:ApplicantsDataPath"]!)));
builder.Services.AddSingleton<IRepository<Direction, int>>(_ => new DirectionRepository(ReaderCSV.GetDirections(builder.Configuration["DataPaths:DirectionsDataPath"]!)));
builder.Services.AddSingleton<IRepository<ExamResult, int>>(_ => new ExamResultRepository(ReaderCSV.GetExamResults(builder.Configuration["DataPaths:ExamResultsDataPath"]!)));
builder.Services.AddSingleton<IRepository<Speciality, int>>(_ => new SpecialityRepository(ReaderCSV.GetSpecialities(builder.Configuration["DataPaths:SpecialitiesDataPath"]!)));

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
