using System.Text.Json.Serialization;

namespace StudentFlow.Core.Models;

public class Course
{
    [JsonIgnore]
    public long Id { get; set; }
    public string CourseName { get; set; } = default!;
    
    [JsonIgnore]
    public Guid CourseCode { get; set; } = Guid.NewGuid();
    public long Duration { get; set; }
}