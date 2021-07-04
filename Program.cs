using DND_Scheduler.Commands;
using DND_Scheduler.Common;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using System;
using System.Threading.Tasks;


namespace DND_Scheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            Date d = new Date(7,5, 2021);
            d.BlockMinutes(444, 890);
            ImaGen.DateToCalImg(d, "TestMonday");
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                //Manually add token here
                Token = "",
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged
            });
            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "!" }
            });

            commands.RegisterCommands<BasicCommandModule>();



            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
