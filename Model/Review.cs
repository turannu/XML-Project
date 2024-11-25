using System.ComponentModel.DataAnnotations;

namespace XML_Project.Model
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
        
        [EmailAddress]
        public string? Email { get; set; }
        
        public string? Location { get; set; }
        [Required]
        [StringLength(500)]
        public string? Experience { get; set; }

        [Range(1, 5)]
        public int? Rating { get; set; } // Rating scale from 1 to 5

        public DateTime? Date { get; set; } = DateTime.Now;
        public List<string>? Tags { get; set; }
       
    }
}
