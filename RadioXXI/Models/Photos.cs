using System.ComponentModel.DataAnnotations;

namespace RadioXXI.Models
{
    public class Photos
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Url { get; set; }

        public string Description { get; set; }

        public int? NewsId { get; set; }

        public int? CommsId { get; set; }
    }
}
