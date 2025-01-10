using Application_Layer.DTO;
using Application_Layer.Interfaces;
using Application_Layer.Services;
using Data_Access_Layer.Dapper;
using Data_Access_Layer.EntityFrameWork;
using Data_Access_Layer.InMemoryDB;
using Domain_Layer.Exceptions;
using Domain_Layer.Interfaces;
using Domain_Layer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MinimalWebAPI.Endpoint;
using System.Diagnostics;


var builder = WebApplication.CreateBuilder(args);

//builder.Services.Configure<Logging>(Configuration.GetSection("Logging"));
var configValue = builder.Configuration.GetValue<string>("Repository:value");

Debug.WriteLine("The Value is "+configValue);

switch (configValue) {

    case "dapper":

        builder.Services.AddScoped<IService<StudentDTO, Student>, StudentService>();

        #region DAPPER
        builder.Services.AddScoped<IRepository<Student>, StudentDapperRepository>();
        #endregion

        break;

    case "efcore":

        #region EntityFrameWork

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddScoped<IService<StudentDTO, Student>, StudentEFService>();

        builder.Services.AddDbContext<StudentContext>(opt => opt.UseInMemoryDatabase("StudentDB"));
       
        builder.Services.AddTransient<IRepository<Student>, StudentEFRepository>();

        #endregion

        break;

    default:
        //Memory Database

        builder.Services.AddScoped<IService<StudentDTO, Student>, StudentService>();

        builder.Services.AddTransient<IRepository<Student>, StudentMemoryRepository>();

        break;


}



//builder.Services.AddSingleton<IRepository<Student>,StudentMemoryRepository>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();


var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment()) {
    //app.useSwagger();
}

#region DAPPER
if (configValue!.Contains("dapper") || configValue!.Contains("efcore")) {

    await SqlLiteDBCreation.CreateSqlLiteDBAsync(app);

}

#endregion


StudentEndPoint.AddEndPoint(app);


app.Run();
