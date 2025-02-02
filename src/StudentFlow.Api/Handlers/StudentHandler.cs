using Microsoft.EntityFrameworkCore;
using StudentFlow.Api.Data;
using StudentFlow.Core.Handlers;
using StudentFlow.Core.Models;
using StudentFlow.Core.Requests.Students;
using StudentFlow.Core.Responses;

namespace StudentFlow.Api.Handlers;

public class StudentHandler(StudentDbContext dbContext) : IStudentHandler
{
    public async Task<BaseResponse<Student?>> CreateAsync(CreateStudentRequest request)
    {
        try
        {
            var student = new Student
            {
                Name = request.Name,
                Registration = request.Registration,
                Email = request.Email,
                Course = new Course
                {
                    CourseCode = new Guid(),
                    CourseName = request.Course.CourseName,
                    Duration = request.Course.Duration,
                },
                DateOfBirth = request.DateOfBirth
            };

            dbContext.Students.Add(student);
            await dbContext.SaveChangesAsync();

            return new BaseResponse<Student?>(student, 201, "Estudante criado com sucesso");
        }
        catch
        {
            return new BaseResponse<Student?>(null, 500, "Erro ao criar Estudante");
        }
    }


    public async Task<BaseResponse<Student?>> UpdateAsync(UpdateStudentRequest request)
    {
        try
        {
            var student = await dbContext.Students.Include(student => student.Course).FirstOrDefaultAsync(x => x.Id == request.Id);
        
            if (student is null)
                return new BaseResponse<Student?>(null, 404, "Estudante não encontrado.");

            student.Name = request.Name;
            student.Registration = request.Registration;
            student.Email = request.Email;
            student.Course.CourseName = request.Course.CourseName;
            student.Course.Duration = request.Course.Duration;
            student.DateOfBirth = request.DateOfBirth.ToUniversalTime();

            dbContext.Students.Update(student);
            await dbContext.SaveChangesAsync();

            return new BaseResponse<Student?>(student);
        }
        catch
        {
            return new BaseResponse<Student?>(null, 500, "Erro ao atualizar Estudante");
        }
    }


    public async Task<BaseResponse<Student?>> DeleteAsync(DeleteStudentRequest request)
    {
        try
        {
            var student = await dbContext
                .Students
                .FirstOrDefaultAsync(x => x.Id == request.Id);
        
            if (student is null)
                return new BaseResponse<Student?>(null, 404, "Estudante não encontrado.");
        
            dbContext.Students.Remove(student);
            await dbContext.SaveChangesAsync();

            return new BaseResponse<Student?>(student, message: "Estudante deletado com sucesso.");
        }
        catch
        {
            return new BaseResponse<Student?>(null, 500, "Não foi possível deletar o Estudante.");
        }
    }


    public async Task<BaseResponse<Student?>> GetByIdAsync(GetStudentByIdRequest request)
    {
        try
        {
            var student = await dbContext.Students.Include(student => student.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return student is null 
                ? new BaseResponse<Student?>(null, 404, "Estudante não encontrado.") 
                : new BaseResponse<Student?>(student);
        }
        catch
        {
            return new BaseResponse<Student?>(null, 500, "Não foi possível consultar o Estudante.");
        }
    }


    public async Task<PagedResponse<List<Student>>> GetAllAsync(GetAllStudentsRequest request)
    {
        try
        {
            var query = dbContext.Students
                .AsNoTracking()
                .Include(x => x.Course)
                .OrderBy(x => x.Name);

            var students = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(p => new Student
                {
                    Id = p.Id,
                    Name = p.Name,
                    Registration = p.Registration,
                    Email = p.Email,
                    Course = new Course
                    {
                        CourseName = p.Course.CourseName,
                        Duration = p.Course.Duration
                    },
                    DateOfBirth = p.DateOfBirth
                })
                .ToListAsync();
            var count = await query
                .CountAsync();

            
            return new PagedResponse<List<Student>>(students, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<Student>>(null, 500, "Não foi possível consultar os estudantes.");
        }
    }
}