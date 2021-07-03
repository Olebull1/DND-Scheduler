using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DND_Scheduler.Common;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace DND_Scheduler.Commands
{
    public class BasicCommandModule : BaseCommandModule
    {
        [Command("greet")]
        public async Task GreetCommand(CommandContext ctx, string name)
        {
            await ctx.RespondAsync($"Hello, thanks for calling this function {name}.");
        }

        // !block Monday -> getDateForNextMonday -> block the whole day
        [Command("block-day"), Aliases("bd")]
        public async Task BlockDayCommand(CommandContext ctx, string day)
        {
            if(Calendar.Instance.BlockDay(day) >= 0)
            {
                await ctx.RespondAsync($"Next {day} was successfully blocked off.");
            }
            else
            {
                await ctx.RespondAsync($"Sorry, I was unable to block off {day}.");
            }
            
        }
        

        [Command("block-after"), Aliases("ba")]
        public async Task BlockAfterCommand(CommandContext ctx, string date, string time)
        {
            Console.WriteLine("Block after");
            if (Calendar.Instance.BlockBeforeAfter(date, time, Calendar.befafter.AFTER) >= 0)
            {
                await ctx.RespondAsync($"Successfully blocked off all time after {time} for {date}");
            } 
            else
            {
                await ctx.RespondAsync($"Unable to block off time.");
            }
        }
        [Command("block-before"), Aliases("bb")]
        public async Task BlockBeforeCommand(CommandContext ctx, string date, string time)
        {
            Console.WriteLine("Block before");
            if (Calendar.Instance.BlockBeforeAfter(date, time, Calendar.befafter.BEFORE) >= 0)
            {
                await ctx.RespondAsync($"Successfully blocked off all time before {time} for {date}");
            }
            else
            {
                await ctx.RespondAsync($"Unable to block off time.");
            }
        }

        [Command("block-inclusive"), Aliases("bi")]
        public async Task BlockInclusiveCommand(CommandContext ctx, string date, string startTime, string endTime)
        {
            await ctx.RespondAsync($"Ima knock ur block off... punk");
        }
        [Command("block-exclusive"), Aliases("be")]
        public async Task BlockExclusiveCommand(CommandContext ctx, string date, string startTime, string endTime)
        {
            await ctx.RespondAsync($"Ima knock ur block off... punk");
        }
        [Command("block"), Aliases("b")]
        public async Task BlockCommand(CommandContext ctx)
        {
            await ctx.RespondAsync($"Ima knock ur block off... punk");
        }
    }
}
