using System;
using Discord.Commands;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using Botty.Games;
using Botty.DBCommands;
using Botty.Hierarchy;

namespace Botty
{
    class Program
    {
        const string _token = "";
        private DiscordSocketClient _client;


        public static void Main(string[] args)
                    => new Program().MainAsync().GetAwaiter().GetResult();

        private CommandHandler _commandHandler;

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            _client.MessageReceived += MessageReceivedAsync;

            _commandHandler = new CommandHandler(_client, new CommandService());

            await _client.LoginAsync(TokenType.Bot, _token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        private async Task MessageReceivedAsync(SocketMessage arg)
        {
            Console.WriteLine(arg.ToString());
        }
    }
}
