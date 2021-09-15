using System;


namespace TabloidCLI.Models
{
    public class Journal
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TextContent { get; set; }
        public DateTime CreationDate { get; set; }
    }
}