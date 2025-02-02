using StudentFlow.Core.Models;
using StudentFlow.Core.Requests.Students;
using StudentFlow.Core.Responses;

namespace StudentFlow.Core.Handlers;

public interface IStudentHandler
{
    Task<BaseResponse<Student?>> CreateAsync(CreateStudentRequest request);
    Task<BaseResponse<Student?>> UpdateAsync(UpdateStudentRequest request);
    Task<BaseResponse<Student?>> DeleteAsync(DeleteStudentRequest request);
    Task<BaseResponse<Student?>> GetByIdAsync(GetStudentByIdRequest request);
    Task<PagedResponse<List<Student>>> GetAllAsync(GetAllStudentsRequest request);
}