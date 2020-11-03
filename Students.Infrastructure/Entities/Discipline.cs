using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Students.Infrastructure.Entities
{
    public class Discipline
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }

        public ICollection<Absenteeism> Absenteeism { get; set; }
    }
}