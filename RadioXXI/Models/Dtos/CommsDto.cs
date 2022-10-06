using System.Collections.Generic;

namespace RadioXXI.Models.Dtos
{
    public class CommsDto
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int NewsId { get; set; }
        public int CommsId { get; set; }
        public List<Photos> Photos { get; set; }
    }
}
