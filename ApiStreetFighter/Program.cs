using ApiStreetFighter.Profiles;
using ApiStreetFighter.Services;
using ApiStreetFighter.Validators;
using Application.Commands;
using Application.Interfaces;
using Application.Proxies;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Repository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//JWT
//variable que va a buscar la KEY al json
var key = builder.Configuration["JwtKey"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(key)
        )
    };
});

builder.Services.AddAuthorization();

//SWAGGER
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Pega: tu bearer {tu token}"
    });
});

//External Services
var SWApiUrl = builder.Configuration["ExternalApis:SWapi:BaseUrl"];

builder.Services.AddHttpClient("StarWarsApi", client =>
{
    client.BaseAddress = new Uri(SWApiUrl);
});

//Entity Framework
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

//IOC
builder.Services.AddScoped<IFightersRepository, FightersRepository>();
builder.Services.AddScoped<IGetFightersCommand, GetFightersCommand>();
builder.Services.AddScoped<IGetFighterByIdCommand, GetFighterByIdCommand>();
builder.Services.AddScoped<IDeleteFighterCommand, DeleteFighterCommand>();
builder.Services.AddScoped<IUpdateFighterCommand, UpdateFighterCommand>();
builder.Services.AddScoped<IInsertFighterCommand, InsertFighterCommand>();
builder.Services.AddScoped<IHttpSWProxy, HttpSWProxy>();

//Services
builder.Services.AddScoped<TokenService>();

//Controllers
builder.Services.AddControllers();

//Automapper
builder.Services.AddAutoMapper(cfg =>
{
    //agrega los profiles
    cfg.AddProfile<EntityProfile>();
});

//Validators
builder.Services.AddValidatorsFromAssemblyContaining<CreateUpdateEntityDTOValidator>();

var app = builder.Build();

//Pipeline
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

