using WitAi;
using WitAi.Models;

namespace WitAI_DiscordBot.Lib
{
    public class WitClient
    {
        public string ApiToken { get; protected set; }
        public Wit Client { get; protected set; }

        public WitClient(string token)
        {
            ApiToken = token;
            Client = new Wit(ApiToken);
        }

        public MessageResponse Send(string message)
        {
            return Client.Message(message);
        }
    }
}