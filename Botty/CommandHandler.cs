using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Reflection;

namespace Botty
{
    class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;

        //Modtager client og commandservice instanser via ctor
        public CommandHandler(DiscordSocketClient client, CommandService commands)
        {
            _commands = commands;
            _client = client;
            _client.MessageReceived += HandleCommandAsync;
            _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null).GetAwaiter().GetResult();
        }


        

        private async Task HandleCommandAsync(SocketMessage messeageParam)
        {
            var message = messeageParam as SocketUserMessage;
            if (message == null) return;

            //Positions nummer iforhold til prefix
            int argPos = 0;

            //Find ud af om beskeden er en kommando ud fra prefix + reagere ikke på bot beskeder
            if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos)) || message.Author.IsBot) return;

            var context = new SocketCommandContext(_client, message);

            await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null);
            //tjek at det ikke er en system besked
  

        }






    }
    public class MyCommandService : CommandService
    {

    }
}
