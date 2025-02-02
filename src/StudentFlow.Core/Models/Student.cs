namespace StudentFlow.Core.Models;

public class Student
{
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Registration { get; set; } = string.Empty;
        public Course Course { get; set; } = new();
        public string Email { get; set; } = string.Empty; 
        public DateTime DateOfBirth { get; set; }
}