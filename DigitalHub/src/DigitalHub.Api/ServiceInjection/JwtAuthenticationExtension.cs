using DigitalHub.Domain.Constants;
using DigitalHub.Domain.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DigitalHub.Api.ServiceInjection;

public static class JwtAuthenticationExtension
{
    public static IServiceCollection AddJwtAuthenticationExtension(this IServiceCollection services)
    {
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = ConfigurationHelper.GetJWT(JWTKeys.Issuer),
                    ValidAudience = ConfigurationHelper.GetJWT(JWTKeys.Audience),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationHelper.GetJWT(JWTKeys.Secret)))
                };
            });

        services.AddAuthorization();

        return services;
    }
}
