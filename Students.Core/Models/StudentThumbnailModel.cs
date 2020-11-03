using System.ComponentModel.DataAnnotations;
using Students.Infrastructure.Entities;


namespace Students.Core.Models
{
    public class StudentThumbnailModel
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required, MaxLength(50)]
        public string Group { get; set; }
    }
}