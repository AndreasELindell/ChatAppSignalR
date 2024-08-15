using ChatAppSignalR.Core.Entites;
using ChatAppSignalR.Infra;
using ChatAppSignalR.Infra.Repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Win32;

namespace ChatAppSignalR.Hubs;

public sealed class ChatHub : Hub
{
    private readonly ChatRepository _repository;
    public ChatHub( ChatRepository repository)
    {
        _repository = repository;
    }
    

    //When disconnecting
    public override async Task OnDisconnectedAsync(Exception? ex) 
    {
        await Clients.Group(UserRoomHandler.Users[Context.ConnectionId].Chatroom).SendAsync("ReceiveMessage", $"{UserRoomHandler.Users[Context.ConnectionId].UserName} has Left");
        
        var chatroom = UserRoomHandler.Users.Values
            .Where(r => r.Chatroom == UserRoomHandler.Users[Context.ConnectionId].Chatroom).First().Chatroom;
        UserRoomHandler.Users.Remove(Context.ConnectionId);
        var userList = UserRoomHandler.Users.Values
            .Where(r => r.Chatroom == chatroom)
            .Select(u => u.UserName)
            .ToList();
        await Clients.All.SendAsync("UpdateUsersList", userList);
    }

    public async Task SendGroupMessage(string user, string message) 
    {
        await Clients.All.SendAsync("ReceiveMessage", $"{user ?? "user"}", message);

        var messageToAdd = new Message()
        {
            User = user ?? "Anonymous",
            Text = message,
            DateTime = DateTime.Now,
        };
        //await _repository.AddMessageToChatLog(chatroom, messageToAdd);
    }
    public async Task JoinGroupChat(string user, string chatroom) 
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatroom);
        UserRoomHandler.Users.Add(
            Context.ConnectionId,
            new ChatUser() { 
                Chatroom = chatroom, 
                UserName = user 
            });

        var userList = UserRoomHandler.Users.Values
            .Where(r => r.Chatroom == chatroom)
            .Select(u => u.UserName)
            .ToList();

        await Clients.Group(chatroom).SendAsync("ReceiveMessage", $"{user ?? "user"}", $"has joined chatroom {chatroom}");
        await Clients.Group(chatroom).SendAsync("UpdateUsersList", userList);

    }
}
