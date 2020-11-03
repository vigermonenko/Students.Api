using System;
using System.ComponentModel.DataAnnotations;


namespace Students.Core.Models
{
    public class AbsenteeismModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required, MaxLength(200)]
        public string StudentFullName { get; set; }

        [Required]
        public int DisciplineId { get; set; }

        [Required, MaxLength(100)]
        public string StudentLastName { get; set; }
    }
}