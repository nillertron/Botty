using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using Discord;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Botty
{
    public class NGame : ModuleBase<SocketCommandContext>
    {
        public RandomN rand1 = RandomN.GetRandom;

       

        [Command("Guess")]
        [Summary("Ngame")]

        public async Task gameAsync(int num)
        {

            if (!RandomN.On)
            {
                await Context.Channel.SendMessageAsync("No game currently running, start number game with !startNgame");
            }
            else if (RandomN.On)
            {
                if (num == RandomN.rand)
                {
                    RandomN.gCounter++;

                    await Context.Channel.SendMessageAsync("Number was correct! " + num + "Took you " + RandomN.gCounter + " Tries");
                    RandomN.On = false;

                }
                else if (num > RandomN.rand)
                {
                    RandomN.gCounter++;

                    await Context.Channel.SendMessageAsync(num + " Was too high ! guess again with !Guess (number) \n" + RandomN.gCounter + " Tries");

                }
                else if (num < RandomN.rand)
                {
                    RandomN.gCounter++;

                    await Context.Channel.SendMessageAsync(num + " Was too low, try again with !Guess (number)\n" + RandomN.gCounter + " Tries");
                }
            }



        }
        [Command("StartNGame")]
        [Summary("Starter spillet")]
        public async Task guessAsync()
        {

            await Task.Run( () =>
            {
                rand1 = RandomN.GetRandom;
                Random rnd = new Random();
                RandomN.rand = rnd.Next(1, 100);
                RandomN.gCounter = 0;
                RandomN.On = true;

            });
            await Context.Channel.SendMessageAsync("Game is now in session! Type !Guess number to beat the game!");
        }


        [Command("StopNGame")]
        [Summary("Stopper spillet")]
        public async Task stopNGameAsync()
        {
            await Task.Run(async () =>

            {
                RandomN.On = false;
                await Context.Channel.SendMessageAsync("Game is off!");
            }
            );


        }
    }





    public class RandomN
    {
        public static int rand { get; set; }
        public static int gCounter { get; set; }
        public static bool On { get; set; }



        public static RandomN GetRandom { get; } = new RandomN();
        private RandomN()
        {

        }




    }
}

