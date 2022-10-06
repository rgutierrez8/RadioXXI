using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RadioXXI.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(6)]
        public string Title { get; set; }

        [Required]
        [MinLength(20)]
        public string Body { get; set; }

        public string Banner { get; set; }

        public List<Photos> Photos { get; set; }
    }
}
