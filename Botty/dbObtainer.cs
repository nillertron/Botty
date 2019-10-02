using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Botty
{
    class dbObtainer : ModuleBase<SocketCommandContext>
    {

        [Command("obtain")]
        public async Task commands()
        {
            //var Context = new Entities();

            //var Command = Context.Commands.Where( C => C.id == 1).FirstOrDefault;
        }
    }
}
