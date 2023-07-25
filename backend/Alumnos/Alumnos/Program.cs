using System.Text.Json.Serialization;
using Alumnos.BusinessService;
using Alumnos.BusinessService.Interface;
using Alumnos.configuration;
using Alumnos.DataService;
using Alumnos.models;
using Alumnos.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string username = Environment.GetEnvironmentVariable("DB_USERNAME");
string password = Environment.GetEnvironmentVariable("DB_PASSWORD");
// Obtener la cadena de conexión del archivo de configuración
string connectionString = builder.Configuration.GetConnectionString("Connection_SQlServer");

Console.WriteLine($"Conexion1: {connectionString}..user: {username}..password:{password}");
// Reemplazar las variables de entorno en la cadena de conexión
connectionString = connectionString.Replace("${DB_USERNAME}", username)
                                   .Replace("${DB_PASSWORD}", password);
Console.WriteLine($"Conexion2: {connectionString}");
// Configurar la cadena de conexión en el servicio de base de datos
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(connectionString));
// congiration of  arquitecture in capas

builder.Services.AddBaseServices();

//En resumen, esta configuración es útil cuando se trabaja con objetos JSON 
//complejos que pueden tener referencias circulares y se desea serializarlos 
//a través de una API de ASP.NET Core.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    //options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});




var app = builder.Build();

// use Cors
app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//DatabaseManagementService.MigrationInitialization(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
