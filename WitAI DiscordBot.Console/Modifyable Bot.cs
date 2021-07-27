namespace WitAI_DiscordBot.Console
{
    using System;
    using WitAi.Models;
    using Discord.WebSocket;
    using WitAI_DiscordBot.Lib;
    using Microsoft.VisualBasic;

    /// <summary>
    /// An ease of use class for other developers. Put your code here to prevent issues
    /// </summary>
    public class ModifyableBot
    {
        /// <summary>
        /// Called when a discord message is recieved
        /// </summary>
        /// <param name="message"></param>
        public void OnMessageRecieved(SocketMessage message)
        {
            Console.WriteLine($"Recieved Message from {message.Author}: \"{message.Content}\"");
        }

        /// <summary>
        /// Called when a response is returned from WitAI
        /// </summary>
        /// <param name="response"></param>
        public void OnWitResponseRecieved(MessageResponse response)
        {

        }
    }
}
