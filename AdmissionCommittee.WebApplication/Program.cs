using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using AdmissionCommittee.Domain.Interfaces;
using AdmissionCommittee.Domain.Models;
using AdmissionCommittee.Domain.Repositories;
using AdmissionCommittee.WebApplication.Components;
using AdmissionCommittee.Domain;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration["ConnectionStrings:postrges"];

builder.Services.AddDbContext<AdmissionCommitteeDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddTransient<IRepository<Applicant, int>, ApplicantRepository>();
builder.Services.AddTransient<IRepository<AdmissionCommittee.Domain.Models.Direction, int>, DirectionRepository>();
builder.Services.AddTransient<IRepository<ExamResult, int>, ExamResultRepository>();
builder.Services.AddTransient<IRepository<Speciality, int>, SpecialityRepository>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddMudServices();

builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
