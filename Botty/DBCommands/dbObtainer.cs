using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Botty.DB;
using Discord;

namespace Botty.DBCommands
{
    public class dbObtainer : ModuleBase<SocketCommandContext>
    {

        [Command("help")]
        [Summary("Service to obtain data")]
        public async Task commands() {

            var embed = new EmbedBuilder();

            var DBContext = new Entities();
            
            var CommandList = DBContext.Commands.Where(C => C.id != 0).OrderBy(C => C.type).ToList();
            if (CommandList == null || CommandList[0] == null)
            {
                embed.Title = "Error";
                embed.Description = "Connection to database lost or table was empty";
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
