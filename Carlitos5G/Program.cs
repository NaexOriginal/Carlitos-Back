using Carlitos5G.Commons;
using Carlitos5G.Data;
using Carlitos5G.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar la conexión a la base de datos (modifica esto según tu configuración)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("Connection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Connection"))
    )
);

// Configuración para JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])) // Tu clave secreta
        };
    });


builder.Services.Configure<FormOptions>(options =>
{
    // Puedes ajustar el tamaño máximo de archivo según tus necesidades
    options.MultipartBodyLengthLimit = long.MaxValue;  // Permitir archivos grandes
    options.ValueLengthLimit = int.MaxValue;
});




// Inyectar servicios personalizados
builder.Services.AddScoped<AuthService>(); // Asegúrate de que el servicio de autenticación esté registrado
builder.Services.AddScoped<IAdminServices, AdminServices>();
builder.Services.AddScoped<ITutorService, TutorService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IFlipbookService, FlipbookService>();
builder.Services.AddScoped<ILikeService, LikeService>();
builder.Services.AddScoped<IPlaylistService, PlaylistService>();
builder.Services.AddScoped<IContentService, ContentService>();
builder.Services.AddScoped<IAvanceService, AvanceService>();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<IBookmarkService, BookmarkService>();


// Servicios Commons para subida de archivos
builder.Services.AddSingleton<ImageUploadService>();
builder.Services.AddSingleton<VideoUploadService>();
builder.Services.AddSingleton<FileUploadService>();

var app = builder.Build();

// Configuración de CORS
app.UseCors("AllowAll");

// Configurar el pipeline de la aplicación
app.UseStaticFiles(); // Para servir archivos estáticos desde wwwroot

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
