using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using Discord;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Botty.Games
{
    public class NGame : ModuleBase<SocketCommandContext>
    {
        public Random rand1 = Random.GetRandom;

       

        [Command("Guess")]
        [Summary("Ngame")]

        public async Task gameAsync(int num)
        {

            if (!Random.On)
            {
                await Context.Channel.SendMessageAsync("No game currently running, start number game with !startNgame");
            }
            else if (Random.On)
            {
                if (num == Random.rand)
                {
                    Random.gCounter++;

                    await Context.Channel.SendMessageAsync("Number was correct! " + num + "Took you " + Random.gCounter + " Tries");
                    Random.On = false;

                }
                else if (num > Random.rand)
                {
                    Random.gCounter++;

                    await Context.Channel.SendMessageAsync(num + " Was too high ! guess again with !Guess (number) \n" + Random.gCounter + " Tries");

                }
                else if (num < Random.rand)
                {
                    Random.gCounter++;

                    await Context.Channel.SendMessageAsync(num + " Was too low, try again with !Guess (number)\n" + Random.gCounter + " Tries");
                }
            }



        }
        [Command("StartNGame")]
        [Summary("Starter spillet")]
        public async Task guessAsync()
        {

            await Task.Run( () =>
            {
                rand1 = Random.GetRandom;
                System.Random rnd = new System.Random();
                Random.rand = rnd.Next(1, 100);
                Random.gCounter = 0;
                Random.On = true;

            });
            await Context.Channel.SendMessageAsync("Game is now in session! Type !Guess number to beat the game!");
        }


        [Command("StopNGame")]
        [Summary("Stopper spillet")]
        public async Task stopNGameAsync()
        {
            await Task.Run(async () =>

            {
                Random.On = false;
                await Context.Channel.SendMessageAsync("Game is off!");
            }
            );


        }
    }





    public class Random
    {
        public static int rand { get; set; }
        public static int gCounter { get; set; }
        public static bool On { get; set; }



        public static Random GetRandom { get; } = new Random();
        private Random()
        {

        }




    }
}

