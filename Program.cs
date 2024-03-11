using Airepuro.Configurations;
using Airepuro.Api.Services;
using Airepuro.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MongoDatabase"));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSingleton<SensorAireServices>();
//mas seguro este
builder.Services.AddScoped<SensorAireServices>(); 
builder.Services.AddScoped<SensorTemperaturaService>(); 
builder.Services.AddScoped<UsuarioServices>(); //deben agregarse mas si hay mas colecciones en la api tambien un modelo, servicio y controlador
builder.Services.AddScoped<VentiladorServices>(); 
builder.Services.AddScoped<VhistorialServices>(); 
builder.Services.AddScoped<Vlogin>(); 

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
