using System.ComponentModel.DataAnnotations;

namespace TestExercise.Models
{
    public class Provider
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
