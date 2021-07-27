using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WitAI_DiscordBot.Lib.Extensions;

namespace WitAI_DiscordBot.Lib
{
    public class DiscordBot : IDisposable
    {
        /// <summary>
        /// Actions to fire when a discord message is recieved by the bot
        /// </summary>
        public List<Action<SocketMessage>> OnMessageRecieved { get; set; } = new List<Action<SocketMessage>>();

        /// <summary>
        /// Actions to fire when the bot is destroyed
        /// </summary>
        public List<Action> OnBotDisposed { get; set; } = new List<Action>();

        /// <summary>
        /// Discord API token for the bot
        /// </summary>
        public string DiscordToken { get; protected set; }

        private readonly DiscordSocketClient _client;

        public DiscordBot(string discordToken)
        {
            DiscordToken = discordToken;

            _client = new DiscordSocketClient();
            _client.MessageReceived += _client_MessageReceived;
        }

        /// <summary>
        /// Start the discord bot
        /// </summary>
        public void StartBot()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Main function to initialize the bot
        /// </summary>
        /// <returns></returns>
        private async Task MainAsync()
        {
            Console.WriteLine("Starting bot...");
            // Tokens should be considered secret data, and never hard-coded.
            await _client.LoginAsync(TokenType.Bot, DiscordToken);
            await _client.StartAsync();

            Console.WriteLine("Bot started!");
            // Block the program until it is closed.
            await Task.Delay(Timeout.Infinite);
        }

        /// <summary>
        /// Called when the discord bot recieves a message. Ex: when a message is sent in the server
        /// </summary>
        /// <param name="arg">Message that was recieved</param>
        /// <returns></returns>
        private async Task _client_MessageReceived(SocketMessage arg)
        {
            OnMessageRecieved.InvokeAll(arg);
        }

        /// <summary>
        /// Called when the object is destroyed
        /// </summary>
        public void Dispose()
        {
            _client.Dispose();
            OnBotDisposed.InvokeAll();
            GC.SuppressFinalize(this);
        }

        ~DiscordBot() { Dispose(); }
    }
}
