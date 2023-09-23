using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Net;
using Discord.WebSocket;
using Microsoft.VisualBasic;

namespace MyApp
{
    internal class Program
    {
        DiscordSocketClient client;
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            client = new DiscordSocketClient();
            client.Log += Log;
            client.SlashCommandExecuted += SlashCommandHandler;
            client.Ready += Client_Ready;

            var token = "MTE1NTIwMTgyMTIyMTA2MDYzOA.GFyM-B.EFaa8yqmX4mbCbGCRkxu6NNqbiqYE-SfPiwQus";

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            Console.ReadLine();
        }

        private async Task Client_Ready()
        {
            ulong guildId = 1076420368534884362;

            var guildCommand = new SlashCommandBuilder()
                .WithName("getkey")
                .WithDescription("full naebalovo");
            try
            {
                await client.Rest.CreateGuildCommand(guildCommand.Build(), guildId);
            }
            catch (ApplicationCommandException exception)
            {
            }
        }
        private async Task SlashCommandHandler(SocketSlashCommand command)
        {
            string randompart = "";
            var r = new Random();
            while (randompart.Length < 7)
            {
                Char c = (char)r.Next(33, 125);
                if (Char.IsLetterOrDigit(c))
                    randompart += c;
            }

            await command.RespondAsync($"User : `{command.User.Id}`\nkey : `micah-{command.User.Id}-{randompart}`\nExpiration date : `неделя ебать мне похуй`");
            await command.RespondAsync($"roles : {command.User.PublicFlags}");
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}