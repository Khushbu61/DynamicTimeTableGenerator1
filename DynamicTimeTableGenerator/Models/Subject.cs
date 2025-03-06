using System.ComponentModel.DataAnnotations;

namespace DynamicTimeTableGenerator.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Hours { get; set; }
    }
}
