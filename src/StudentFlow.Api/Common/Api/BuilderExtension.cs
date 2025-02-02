using Microsoft.EntityFrameworkCore;
using StudentFlow.Api.Data;
using StudentFlow.Api.Handlers;
using StudentFlow.Core;
using StudentFlow.Core.Handlers;

namespace StudentFlow.Api.Common.Api;

public static class BuilderExtension
{
    public static void AddConfiguration(
        this WebApplicationBuilder builder)
    {
        Configuration.ConnectionString =
            builder
                .Configuration
                .GetConnectionString("DefaultConnection")
            ?? string.Empty;
    }

    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options => { options.CustomSchemaIds(n => n.FullName); });
    }
    
    public static void AddCrossOrigin(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(
            options => options.AddPolicy(
                "AllowAll",
                policy => policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            ));
    }
    
    public static void AddDataContexts(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddDbContext<StudentDbContext>(
                x => { x.UseNpgsql(Configuration.ConnectionString); });
    }
    
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddTransient<IStudentHandler, StudentHandler>();
    }
}