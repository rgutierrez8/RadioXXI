using System.ComponentModel.DataAnnotations;

namespace RadioXXI.Models
{
    public class StreamServer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Ip { get; set; }

        [Required]
        public int Port { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
