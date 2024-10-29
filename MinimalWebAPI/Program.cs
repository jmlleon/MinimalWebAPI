using Data_Access_Layer.Dapper;
using Data_Access_Layer.EntityFrameWork;
using Domain_Layer.Interfaces;
using Domain_Layer.Models;
using Microsoft.EntityFrameworkCore;
using MinimalWebAPI.Endpoint;


var builder = WebApplication.CreateBuilder(args);

#region EntityFrameWork

/*builder.Services.AddDbContext<StudentContext>(opt => opt.UseInMemoryDatabase("StudentDB"));
builder.Services.AddTransient<IRepository<Student>, StudentEFRepository>();*/

#endregion

#region DAPPER
builder.Services.AddScoped<IRepository<Student>, StudentDapperRepository>();
#endregion

//builder.Services.AddSingleton<IRepository<Student>,StudentMemoryRepository>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {


    //app.useSwagger();

}

await SqlLiteDBCreation.CreateSqlLiteDBAsync(app);

StudentEndPoint.AddEndPoint(app);


app.Run();
