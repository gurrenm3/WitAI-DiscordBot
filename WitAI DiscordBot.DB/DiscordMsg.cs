using SQLite;

namespace WitAI_DiscordBot.Dal
{
    public class DiscordMsg
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Sender { get; set; }

        [MaxLength(2000)]
        public string Message { get; set; }


        public DiscordMsg(string sender, string msg)
        {
            Sender = sender;
            Message = msg;
        }
    }
}