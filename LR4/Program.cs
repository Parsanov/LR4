using LR4.Application;
using LR4.Application.Creators.Readers;
using LR4.Application.Creators.Writers;
using LR4.Application.Services;
using LR4.Core.Abstracts;
using LR4.Core.Interfaces;
using LR4.Persistence;
using LR4.Persistence.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<DbDataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IDataDBService, DataDBService>();
builder.Services.AddScoped<IDentistryPatientService, DentistryPatientService>();
builder.Services.AddScoped<ExcelExportService>();

builder.Services.AddScoped<ReportWriterCreator, JsonFormatW>();
builder.Services.AddScoped<ReportWriterCreator, TxtFormatW>();
builder.Services.AddScoped<ReportWriterCreator, XmlFormatW>();

builder.Services.AddScoped<ReportReaderCreator, JsonFormatR>();
builder.Services.AddScoped<ReportReaderCreator, TxtFormatR>();
builder.Services.AddScoped<ReportReaderCreator, XmlFormatR>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

// Seed тестових даних
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DbDataContext>();
    await db.Database.MigrateAsync();
    await LR4.Persistence.DentistryPatientSeeder.SeedAsync(db);
}

app.Run();
