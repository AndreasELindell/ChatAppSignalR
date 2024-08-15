using ChatAppSignalR.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSignalR.Infra.DatabaseContext
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options){}

        public DbSet<Message> Messages { get; set; }
        public DbSet<Chatlog> Chatlogs { get; set; }
    }
}
