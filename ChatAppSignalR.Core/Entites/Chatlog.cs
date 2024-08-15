
using ChatAppSignalR.Infra;

namespace ChatAppSignalR.Core.Entites;

public class Chatlog
{
    public int Id { get; set; }
    public string ChatRoomName { get; set; } = string.Empty;
    public List<Message> Messages { get; set; } = new List<Message>();

}
