using System.ComponentModel.DataAnnotations;

namespace LineMate.Models
{
    public class Player
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Position { get; set; }
        public int JerseyNumber { get; set; }
        public int TeamId { get; set; }
        public int Line { get; set; }
        public Team Team { get; set; }
    }
}