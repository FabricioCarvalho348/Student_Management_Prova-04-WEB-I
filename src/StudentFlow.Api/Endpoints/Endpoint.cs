using StudentFlow.Api.Common.Api;
using StudentFlow.Api.Endpoints.Students;

namespace StudentFlow.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");
                
        endpoints.MapGroup("/")
            .WithTags("Health Check")
            .MapGet("/", () => new { message = "OK" });

        endpoints.MapGroup("v1/students")
            .WithTags("Students")
            .MapEndpoint<GetStudentByIdEndpoint>()
            .MapEndpoint<GetAllStudentsEndpoint>()
            .MapEndpoint<CreateStudentEndpoint>()
            .MapEndpoint<UpdateStudentEndpoint>()
            .MapEndpoint<DeleteStudentEndpoint>();
    }
    
    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}