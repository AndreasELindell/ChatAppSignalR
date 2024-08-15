using ChatAppSignalR.Core.Entites;
using ChatAppSignalR.Infra.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSignalR.Infra.Repositories;

public class ChatRepository
{
    private readonly ChatDbContext _context;
    public ChatRepository(ChatDbContext context)
    {
        _context = context;
    }

    public async Task<Chatlog> GetLogForRoom(string roomName) 
    { 

        return await _context.Chatlogs.Include(m => m.Messages).FirstOrDefaultAsync(cl => cl.ChatRoomName == roomName) ?? new Chatlog();
    }
    public async Task AddMessageToChatLog(string roomName, Message message) 
    {
        var chatLogToAdd = await _context.Chatlogs.FirstOrDefaultAsync(cl => cl.ChatRoomName == roomName);
        if (chatLogToAdd is null) 
        {
            chatLogToAdd = new Chatlog()
            {
                ChatRoomName = roomName,
                Messages = new List<Message>()
            };
        
            chatLogToAdd.Messages.Add(message);
            await _context.AddAsync(chatLogToAdd);
        }
        else 
        {
            chatLogToAdd.Messages.Add(message);
        }

        await _context.SaveChangesAsync();
    }
    public async Task<bool> DoRoomExist(string roomName) 
    {
        return await _context.Chatlogs.AsNoTracking().AnyAsync(cl => cl.ChatRoomName == roomName);
    }
}
