﻿using System;
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
        [Command("say")]
        [Summary("Echoes a message")]
        public Task SayAsync([Remainder][Summary("the text to echo")] string echo)
           => ReplyAsync(echo);
        [Group("sample")]
public class SampleModule : ModuleBase<SocketCommandContext>
        {
            [Command("square")]
            [Summary("Squares a number")]
            public async Task SquareAsync (
                [Summary("The number to Square")] int num)
            {
                await Context.Channel.SendMessageAsync($"{num}^2 = {Math.Pow(num, 2)}");
            }
                }
 
    }
    public class NGame : ModuleBase<SocketCommandContext>
    {
        public int state = 0;
      public int rand = 0;


        Random rnd = new Random();


        [Command("Guess")]
        [Summary("Ngame")]

        public async Task gameAsync(int num)
        {
         
            if (state == 0)
            {
                await Context.Channel.SendMessageAsync("No game currently running");
            }
            if (state == 1)
            {
                if (num == rand)
                {
                    await Context.Channel.SendMessageAsync("Number was correct! " + num);
                    state = 0;
                    rand = 0;
                }
                else if (num > rand)
                {
                    await Context.Channel.SendMessageAsync(num + " Was too high ! guess again with !Guess (number) " );
                }
                else if (num < rand)
                {
                    await Context.Channel.SendMessageAsync(num + " Was too low, try again with !Guess (number)");
                }
            }



        }
        [Command("StartGame")]
        [Summary("Ngame")]
        public async Task guessAsync()
        {
            state = 1;
            rand = rnd.Next(1, 100);

            await Context.Channel.SendMessageAsync("Game in session! Type !Guess number to beat thef game!");
        }






    }

}
