using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSignalR.Core.Entites;

public static class UserRoomHandler
{
    //store ConnectionID and User/chatroom info in memory for realtime updates.
    public static Dictionary<string, ChatUser> Users = new Dictionary<string, ChatUser>();
}
