using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Botty;
using Botty.DB;

namespace Botty.Hierarchy
{
    public class HierachyMain : ModuleBase<SocketCommandContext>
    {

        [Command("permission")]
        public async Task Permissions([Remainder] String argsAsString)
        {
            string[] args = argsAsString.Split(' ');
            if(args[0] == "check")
            {
                var embed = new EmbedBuilder();
                if (args[0] == null)
                {
                    embed.Color = Color.Red;
                    embed.Title = "Argument error";
                    embed.Description = "Argument #2 was empty";

                    await Context.Channel.SendMessageAsync(embed: embed.Build());
                    return;
                }

                String discordID = string.Empty;
                if(args.Length < 2)
                {
                    discordID = Context.User.Id.ToString();
                }else
                {
                    discordID = args[1];
                }

                var DBContext = new Entities();

                var perm = DBContext.Permissions.Where( U => U.discordID == discordID).FirstOrDefault();

                if (perm == null)
                {
                    embed.Color = Color.Blue;
                    embed.Title = "Permission";
                    embed.Description = "No permissions is given to the user specifyed";

                    await Context.Channel.SendMessageAsync(embed: embed.Build());
                    return;
                }

                embed.Color = Color.Blue;
                embed.Title = "Permissions";
                embed.Description = "Your type of permission is " + perm.permissionType;
                await Context.Channel.SendMessageAsync(embed: embed.Build());
            }
        }

    }
}
