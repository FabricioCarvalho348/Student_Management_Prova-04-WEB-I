using StudentFlow.Api.Common.Api;
using StudentFlow.Core.Handlers;
using StudentFlow.Core.Models;
using StudentFlow.Core.Requests.Students;
using StudentFlow.Core.Responses;

namespace StudentFlow.Api.Endpoints.Students;

public class UpdateStudentEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Students: Update")
            .WithSummary("Atualiza um estudante.")
            .WithDescription("Atualiza um estudante.")
            .WithOrder(2)
            .Produces<BaseResponse<Student?>>();
    
    private static async Task<IResult> HandleAsync(
        IStudentHandler handler,
        UpdateStudentRequest request,
        long id)
    {
        request.Id = id;
        
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}