using StudentFlow.Api.Common.Api;
using StudentFlow.Core.Handlers;
using StudentFlow.Core.Models;
using StudentFlow.Core.Requests.Students;
using StudentFlow.Core.Responses;

namespace StudentFlow.Api.Endpoints.Students;

public class DeleteStudentEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Students: Delete")
            .WithSummary("Deleta um estudante")
            .WithDescription("Deleta um estudante")
            .WithOrder(3)
            .Produces<BaseResponse<Student?>>();
    
    private static async Task<IResult> HandleAsync(
        IStudentHandler handler,
        long id)
    {
        var request = new DeleteStudentRequest
        { 
            Id = id
        };
        
        var result = await handler.DeleteAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}