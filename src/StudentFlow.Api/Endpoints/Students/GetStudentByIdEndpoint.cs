using StudentFlow.Api.Common.Api;
using StudentFlow.Core.Handlers;
using StudentFlow.Core.Models;
using StudentFlow.Core.Requests.Students;
using StudentFlow.Core.Responses;

namespace StudentFlow.Api.Endpoints.Students;

public class GetStudentByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Students: Get By Id")
            .WithSummary("Recupera um estudante.")
            .WithDescription("Recupera um estudante.")
            .WithOrder(5)
            .Produces<BaseResponse<Student?>>();
    
    private static async Task<IResult> HandleAsync(
        IStudentHandler handler,
        long id)
    {
        var request = new GetStudentByIdRequest
        {
            Id = id
        };
        
        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}