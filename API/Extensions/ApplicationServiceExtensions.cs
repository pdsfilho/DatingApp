using System;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
    IConfiguration config)
    {
        services.AddControllers();

        //Sqlite
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });

        //CORS Policy
        services.AddCors();

        //Scoped is chosen because it is possible to use it once per client request.
        services.AddScoped<ITokenService, TokenServices>();

        //UserRepository
        services.AddScoped<IUserRepository, UserRepository>();

        //Automapper
        //Identifying where the class AutoMapperProfiles is located
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
