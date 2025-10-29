using ApiInescafe.Data;
using ApiInescafe.Services.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using ApiInescafe.Services.Interfaces;
using ApiInescafe.Services;
using ApiInescafe.Services.Blog;
using ApiInescafe.Services.Course;
using ApiInescafe.Services.SignaturePlan;
using ApiInescafe.Services.Pagamento;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var abacatePaySettings = builder.Configuration.GetSection("AbacatePaySettings");
string apiKey = abacatePaySettings["ApiKey"];
bool isSandbox = abacatePaySettings.GetValue<bool>("IsSandbox");
builder.Services.AddSingleton<dynamic>(sp => 
{
    return new Abacatepay.AbacatePay(apiKey, isSandbox);
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT (Ex: Bearer seu_token_aqui)"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ISignaturePlanService, SignaturePlanService>();
builder.Services.AddScoped<IPagamentoService>(sp => 
{
    var abacatePayClient = sp.GetRequiredService<dynamic>();
    return new PagamentoService(abacatePayClient, sp.GetRequiredService<ILogger<PagamentoService>>(), sp.GetRequiredService<AppDbContext>());
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = config["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = config["Jwt:Audience"],
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();