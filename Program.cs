using MantenimientoTrabajadores.Dominio.Entidades;
using MantenimientoTrabajadores.Dominio.Interfaces;
using MantenimientoTrabajadores.Infraestructura.Repositorio;
using MantenimientoTrabajadores.Model;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MantenimientoTrabajadores.Model.Response;
using MantenimientoTrabajadores.Model.Request;
using MantenimientoDepartamentoes.Infraestructura.Repositorio;
using MantenimientoProvinciaes.Infraestructura.Repositorio;
using MantenimientoDistritoes.Infraestructura.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Configurar la conexión a la base de datos y añadir Entity Framework al servicio.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MyDatabase")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc().AddViewLocalization();

builder.Services.AddScoped<ITrabajadorRepository, TrabajadorRepository>();
builder.Services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();
builder.Services.AddScoped<IProvinciaRepository, ProvinciaRepository>();
builder.Services.AddScoped<IDistritoRepository, DistritoRepository>();

// Configuración de AutoMapper
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Trabajadores, TrabajadorResponse>();
    cfg.CreateMap<TrabajadorRequest, Trabajadores>();
    cfg.CreateMap<Provincia, ProvinciaResponse>();
    cfg.CreateMap<ProvinciaRequest, Provincia>();
    cfg.CreateMap<Departamento, DepartamentoResponse>();
    cfg.CreateMap<DepartamentoRequest, Departamento>();
    cfg.CreateMap<Distrito, DistritoResponse>();
    cfg.CreateMap<DistritoRequest, Distrito>();
});

var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

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
