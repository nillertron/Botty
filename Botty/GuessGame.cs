using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using Discord;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Botty
{
    class GuessGame : ModuleBase<SocketCommandContext>
    {

        public async Task letsPlay(int num, int state, int rand)
        {



            int playerGuess = num;
            state = 2;
            magic(playerGuess, rand, state); 
            

        }
        public int magic(int playerGuess, int rand, int state)
        {
            


                if (playerGuess == rand)
                {
                    Context.Channel.SendMessageAsync("Gz");
                state = 0;
                return state;
                    

                }
                else if(playerGuess > rand)
                {
                     Context.Channel.SendMessageAsync("Too high");
                state = 2;
                return state;
                }
                else
                {
                    Context.Channel.SendMessageAsync("Too low");
                state = 2;
                return state;
                }


        }
    }
}
