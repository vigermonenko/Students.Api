using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Students.Infrastructure.Entities
{
    [Table("Disciplines")]
    public class Discipline
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Absenteeism> Absenteeism { get; set; }
    }
}