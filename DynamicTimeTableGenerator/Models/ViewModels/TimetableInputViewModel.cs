using System.ComponentModel.DataAnnotations;

namespace DynamicTimeTableGenerator.ViewModels
{
    public class TimetableInputViewModel
    {
        [Required]
        [Range(1, 7, ErrorMessage = "Number of working days must be between 1 and 7.")]
        public int WorkingDays { get; set; }

        [Required]
        [Range(1, 8, ErrorMessage = "Number of subjects per day must be between 1 and 8.")]
        public int SubjectsPerDay { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Total subjects must be a positive number.")]
        public int TotalSubjects { get; set; }
    }
}
