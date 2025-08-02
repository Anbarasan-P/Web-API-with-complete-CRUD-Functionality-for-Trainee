using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text; // Ensure you have the necessary using directives for ASP.NET Core and JWT

var builder = WebApplication.CreateBuilder(args);

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false, // Set to true if you want to validate the issuer
            ValidateAudience = false, // Set to true if you want to validate the audience
            ValidateLifetime = true, // Validate the token expiration
            ValidateIssuerSigningKey = true, // Validate the signing key
            IssuerSigningKey = new SymmetricSecurityKey(key) // Use the symmetric key from configuration
        };
    });

builder.Services.AddAuthorization(); // Add authorization services
builder.Services.AddControllers(); // Add controllers to the service collection
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => //  Configure Swagger
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Trainee API", Version = "v1" }); // Define the Swagger document

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme // Define the security scheme for JWT Bearer
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer {your JWT token}'"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement // Define the security requirement for the API
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
            Array.Empty<string>() // No specific scopes required for this API
        }
    });
});

var app = builder.Build();

app.UseAuthentication(); // Use authentication middleware without this line, the JWT authentication won't be applied
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
