using System.ComponentModel.DataAnnotations;


namespace Students.Core.Models
{
    public class DisciplineModel
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }
    }
}