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
            Date d = new Date();
            ImaGen.DateToCalImage(d);
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = "ODU0MDYwMzI2MDM4MTQyOTg2.YMebqQ.HEDBHSuCQEqEp_jDDP0_ymtWc98",
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
