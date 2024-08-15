using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace ChatAppSignalR.Infra
{
    public class Message
    {
        public int Id { get; set; }
        [MinLength(1)]
        public required string User { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
    }
}
