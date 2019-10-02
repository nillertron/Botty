using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using Discord;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Botty
{
    public class RandomSvar : ModuleBase <SocketCommandContext>
    {
        
        // Leg ikke andet ^^
        [Command("Hello")]
        [Summary("Svare")]

        public async Task HelloYouAsync()
        {
            var user = Context.User.Username;
            await Context.Channel.SendMessageAsync("hello " +user);
            await Context.User.SendMessageAsync("hi");

        }

    }

}
