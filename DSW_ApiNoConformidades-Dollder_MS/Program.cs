using DSW_ApiNoConformidades_Dollder_MS.Application.Queries.Usuarios;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

    // Escuchar en todas las interfaces de red
    builder.WebHost.UseUrls("http://0.0.0.0:5000");

    //Habilita los cors
    builder.Services.AddCors(options => {
        options.AddPolicy("PermitirTodo", policy => {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    });

    // crear variable para la cadena de conexion para la BD
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    //registrar servicio para la conexion
    builder.Services.AddDbContext<ApiDbContext>(
        options => options.UseNpgsql(connectionString)
        );

    // Registrar MediatR
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(BuscarUsuariosQuery).Assembly));

    // Configurar Swagger/OpenAPI ? Nuevas líneas
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c => {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "API de Prueba",
            Version = "v1",
            Description = "Documentación de la API"
        });
    });

    //Habilita transacciones que tengan mas de 50mb para mandar los fotos
    builder.Services.Configure<FormOptions>(options =>
    {
        options.MultipartBodyLengthLimit = 52428800; // 50 MB
    });

    builder.Services.AddControllers();

var app = builder.Build();

// Middleware de Swagger ? Nuevas líneas
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
});

app.UseCors("PermitirTodo");
app.MapControllers();

app.Run();
