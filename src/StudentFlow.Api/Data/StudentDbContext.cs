using System.Reflection;
using Microsoft.EntityFrameworkCore;
using StudentFlow.Core.Models;

namespace StudentFlow.Api.Data;

public class StudentDbContext(DbContextOptions<StudentDbContext> options) : DbContext(options)
{
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}