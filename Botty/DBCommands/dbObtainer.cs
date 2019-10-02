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

        [Command("obtain")]
        [Summary("Service to obtain data")]
        public async Task commands() {
            var DBContext = new Entities();
            try
            {
                var Command = DBContext.Commands.Where(C => C.id == 1).FirstOrDefault();
                var embed = new EmbedBuilder();
                embed.Title = Command.CommandName;
                embed.Description = Command.CommandDesc;
                embed.Color = Color.Green;
            }
            catch
            {
                var embed = new EmbedBuilder();
                embed.Title = "Error";
                embed.Description = "Db connection failed";
                embed.Color = Color.Red;
            }
        }
    }
}
