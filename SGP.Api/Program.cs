using SGP.Persistence.PersistenceServiceRegistration;
using SGP.Services.ServiceRegistration;

var builder = WebApplication.CreateBuilder(args);

// 1. Registro de Infraestructura (Base de Datos)
builder.Services.AddPersistenceInfrastructure(builder.Configuration);

// 2. Registro de Aplicación (Servicios)
builder.Services.AddApplicationServices();

// 3. Controladores
builder.Services.AddControllers();

//Se esta usando Swagger en vez de Open Api
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// -----------------------------

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