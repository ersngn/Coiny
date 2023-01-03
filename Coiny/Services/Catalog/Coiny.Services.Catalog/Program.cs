using Coiny.Services.Catalog.Infrastructure.Mongo;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
#endregion
builder.Services.AddControllers();

#region MongoConfiguration
builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));
builder.Services.AddSingleton<IMongoSettings>(sp => sp.GetRequiredService<IOptions<MongoSettings>>().Value);
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();