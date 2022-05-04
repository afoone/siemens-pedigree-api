using Microsoft.EntityFrameworkCore;
using siemens_pedigree_api.Models;
using System.Text;
using Microsoft.Data.SqlClient;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<JSONEntryContext>();
builder.Services.AddDbContext<JSONStructContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(
    options =>
    {
        options.AddDefaultPolicy(
            policy => policy.WithOrigins("http://localhost",
                "https://localhost", "http://localhost:9000/")
            .SetIsOriginAllowedToAllowWildcardSubdomains()
            );
    }
 );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

