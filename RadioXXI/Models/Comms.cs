using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RadioXXI.Models
{
    public class Comms
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }
        public ICollection<Photos> Photos { get; set; }
    }
}
