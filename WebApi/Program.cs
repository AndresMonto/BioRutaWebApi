using BusinessLogic.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Autenticacion
builder.Services.AddAuthentication(options =>
{
    //AuthenticationScheme = "Bearer"
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwtBearerOptions =>
{
    jwtBearerOptions.Events = new JwtBearerEvents()
    {
        OnTokenValidated = context =>
        {
            List<Claim> cl = new(((ClaimsIdentity)context.Principal.Identity).Claims);
            if (cl.Count > 0)
            {
                string strUsuario = cl.Where(c => c.Type == ClaimTypes.UserData)?.FirstOrDefault()?.Value ?? "*";

                if (string.IsNullOrWhiteSpace(strUsuario))
                {
                    context.Fail("Unauthorized");
                }
            }

            return Task.CompletedTask;
        }
    };

    //https://tools.ietf.org/html/rfc7519#page-9
    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
    {
        SaveSigninToken = false,
        ValidateActor = true,
        ValidateIssuer = true, //Issuer: Emisor
        ValidateAudience = true, //Audience: Son los destinatarios del token
        ValidateLifetime = true, //Lifetime: Tiempo de vida del token
        ValidateIssuerSigningKey = true,

        ClockSkew = TimeSpan.Zero,
        RequireExpirationTime = true,
        ValidIssuer = builder.Configuration["ApiAuth:Issuer"],
        ValidAudience = builder.Configuration["ApiAuth:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["ApiAuth:SecretKey"]))
    };
});

builder.Services.AddDbContext<DbContextBioRuta>(options => options.UseMySQL(builder.Configuration["ConnectionStrings:DefaultConnection"]));

//CORS
builder.Services.AddCors(options =>
{
    string[] origings;
    
    #if DEBUG
        origings = new[] {
            "https://bioruta.azurewebsites.net",
            "http://localhost:44434"
        };
#else
        origings = new[] {
            "https://bioruta.azurewebsites.net",
        };
#endif
    options.AddPolicy("CorsPolicy",
        builder => builder.WithOrigins(origings)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x=>x.SwaggerEndpoint("/swagger/v1/swagger.json", "Test"));
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


app.Run();
