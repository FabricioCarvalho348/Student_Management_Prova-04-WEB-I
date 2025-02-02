using StudentFlow.Api.Common.Api;
using StudentFlow.Core.Handlers;
using StudentFlow.Core.Models;
using StudentFlow.Core.Requests.Students;
using StudentFlow.Core.Responses;

namespace StudentFlow.Api.Endpoints.Students;

public class CreateStudentEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Students: Create")
            .WithSummary("Cria um novo estudante")
            .WithDescription("Cria um novo estudante")
            .WithOrder(1)
            .Produces<BaseResponse<Student?>>();
    
    private static async Task<IResult> HandleAsync(
        IStudentHandler handler,
        CreateStudentRequest request)
    {
        var result = await handler.CreateAsync(request);
        
        return result.IsSuccess 
            ? TypedResults.Created($"/{result.Data?.Id}", result) 
            : TypedResults.BadRequest(result);
    }
}