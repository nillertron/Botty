using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using Discord;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Botty
{
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        RandomSvar rndsvar = new RandomSvar();
        [Command("say")]
        [Summary("Echoes a message")]
        public Task SayAsync([Remainder][Summary("the text to echo")] string echo)
           => ReplyAsync(echo);
        [Group("sample")]
        public class SampleModule : ModuleBase<SocketCommandContext>
        {
            [Command("square")]
            [Summary("Squares a number")]
            public async Task SquareAsync(
                [Summary("The number to Square")] int num)
            {
                await Context.Channel.SendMessageAsync($"{num}^2 = {Math.Pow(num, 2)}");
            }
        }

    }

}

