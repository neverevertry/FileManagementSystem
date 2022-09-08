using Application.Interfaces;
using Application.Services;
using DataAccess.Context;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Models.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string ConString = @"Data Source = SPEED; Integrated Security = True; Initial Catalog = FilesAd; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(ConString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IFileRepository, EFFileModelRepository>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<IUrlService, UrlService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
