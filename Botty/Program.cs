using System;
using Discord.Commands;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.ComponentModel.Design;


namespace Botty
{
    class Program
    {
        private DiscordSocketClient _client;


        public static void Main(string[] args)
                    => new Program().MainAsync().GetAwaiter().GetResult();

        const string _token = "NjI2MzYyODk1MjE4OTAwOTk0.XYs_-Q.ejGAtGTEUxgLxNB2wKFT0ze49Qw";

        private CommandHandler _commandHandler;

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            _client.MessageReceived += MessageReceivedAsync;


            var token = "NjI2MzYyODk1MjE4OTAwOTk0.XYs_-Q.ejGAtGTEUxgLxNB2wKFT0ze49Qw";

            _commandHandler = new CommandHandler(_client, new CommandService());


            await _client.LoginAsync(TokenType.Bot, token);
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
