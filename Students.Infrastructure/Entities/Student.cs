using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Students.Infrastructure.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required, MaxLength(50)]
        public string Group { get; set; }

        public ICollection<Absenteeism> Absenteeism { get; set; }
    }
}