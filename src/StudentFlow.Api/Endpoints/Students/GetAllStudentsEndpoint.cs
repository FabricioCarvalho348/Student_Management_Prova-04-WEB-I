using Microsoft.AspNetCore.Mvc;
using StudentFlow.Api.Common.Api;
using StudentFlow.Core;
using StudentFlow.Core.Handlers;
using StudentFlow.Core.Models;
using StudentFlow.Core.Requests.Students;
using StudentFlow.Core.Responses;

namespace StudentFlow.Api.Endpoints.Students;

public class GetAllStudentsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Students: Get All")
            .WithSummary("Recupera todos os estudantes.")
            .WithDescription("Recupera todos os estudantes.")
            .WithOrder(4)
            .Produces<PagedResponse<List<Student>?>>();

    private static async Task<IResult> HandleAsync(
        IStudentHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize
    )
    {
        var request = new GetAllStudentsRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await handler.GetAllAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}