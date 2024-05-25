using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Olya.model;

public class User : IdentityUser<Guid>
{
    [Required, StringLength(100)]
    public string Name { get; set; }
    
    [Required, StringLength(100)]
    public string Surname { get; set; }

    public ICollection<Course> Courses { get; set; } = new List<Course>();
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public ICollection<Assignment> AssignmentsCreated { get; set; } = new List<Assignment>();
    public ICollection<AssignmentSubmission> AssignmentSubmissions { get; set; } = new List<AssignmentSubmission>();
}