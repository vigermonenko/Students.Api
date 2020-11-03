using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Students.Infrastructure.Entities
{
    public class Absenteeism
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required, ForeignKey(nameof(Student))]
        public int StudentId { get; set; }

        [Required, ForeignKey(nameof(Discipline))]
        public int DisciplineId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; }

        [ForeignKey(nameof(DisciplineId))]
        public Discipline Discipline { get; set; }
    }
}