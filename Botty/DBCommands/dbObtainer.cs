using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Botty.DB;
using Discord;
using Botty.Exceptions;
using System.Collections.Generic;

namespace Botty.DBCommands
{
    public class dbObtainer : ModuleBase<SocketCommandContext>
    {

        [Command("help")]
        [Summary("Service to obtain data")]
        public async Task commands() {
            var DBContext = new Entities();
            var embed = new EmbedBuilder();
            
            var CommandList = DBContext.Commands.Where(C => C.id != 0).ToList();
            if (CommandList == null)
            {
                embed.Title = "Error";
                embed.Description = "Connection to database lost";
                embed.Color = Color.Red;
                await Context.Channel.SendMessageAsync(embed: embed.Build());
                return;
            }
            embed.Title = "Command list";

            int i = 0;
            foreach(var item in CommandList)
            {
                embed.AddField(CommandList[i].CommandName, CommandList[i].CommandDesc);
                i++;
            }
            embed.Color = Color.Green; 

            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }
    }
}
