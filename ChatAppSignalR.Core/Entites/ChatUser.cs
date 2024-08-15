namespace ChatAppSignalR.Core.Entites;

public class ChatUser
{
    public required string Chatroom { get; set; } = string.Empty;
    public required string UserName { get; set; } = string.Empty;
}
