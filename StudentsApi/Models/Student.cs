using System.ComponentModel.DataAnnotations;

namespace StudentsApi.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string StudentName { get; set; }

        [Required]
        public string Course { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
    }
}
