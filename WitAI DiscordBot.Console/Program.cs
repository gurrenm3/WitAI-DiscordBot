namespace WitAI_DiscordBot.Console
{
    using Discord.WebSocket;
    using System;
    using System.IO;
    using WitAI_DiscordBot.Dal;
    using WitAI_DiscordBot.Lib;

    class Program
    {
        private static ModifyableBot userDiscordBot;
        private static WitClient client;
        private static DiscordBot bot;
        private static BotData botData;
        private static DataAccess dataAccess;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the WitAPI Discord bot!");
            new Program();

            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
        }

        public Program()
        {
            Console.WriteLine("Initializing...");
            botData = GetBotData();
            if (string.IsNullOrEmpty(botData.DiscordApiToken))
                if (!TryGetDiscordToken())
                    return;

            if (string.IsNullOrEmpty(botData.WitToken))
                TryGetWitToken();

            if (!string.IsNullOrEmpty(botData.WitToken))
                client = new WitClient(botData.WitToken);

            dataAccess = InitDataAccess();

            userDiscordBot = new ModifyableBot();

            bot = new DiscordBot(botData.DiscordApiToken);
            InitMsgRecieved();
            bot.OnBotDisposed.Add(() => dataAccess.Dispose());

            Console.WriteLine("Finished Initializing!");
            bot.StartBot();
        }

        private DataAccess InitDataAccess()
        {
            string dbName = "DiscordMsgs.sqlite";
            string folderPath = Environment.CurrentDirectory;
            string dbPath = Path.Combine(folderPath, dbName);
            return new DataAccess(dbPath);
        }

        private BotData GetBotData()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "WitBot Data.json");
            return BotData.FromFile(path);
        }

        private void InitMsgRecieved()
        {
            bot.OnMessageRecieved.Add((msg) =>
            {
                userDiscordBot.OnMessageRecieved(msg);

                DiscordMsg discordMsg = new DiscordMsg(msg.Author.ToString(), msg.Content);
                dataAccess.Insert(discordMsg);

                if (client != null)
                    userDiscordBot.OnWitResponseRecieved(client.Send(msg.Content));
            });
        }

        private bool TryGetDiscordToken()
        {
            Console.WriteLine("DiscordAPI Token not found. Please enter DiscordAPI Token: ");
            string token = Console.ReadLine();
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("You did not enter a token." +
                    " Bot is unable to function without this token.");
                return false;
            }

            Console.WriteLine("Token entered. Saving to file...");
            botData.DiscordApiToken = token;
            botData.SerializeToFile();
            Console.WriteLine("Token saved!");
            return true;
        }

        private bool TryGetWitToken()
        {
            Console.WriteLine("WitAI Token not found. Please enter WitAI Token: ");
            string witAiToken = Console.ReadLine();
            if (string.IsNullOrEmpty(witAiToken))
            {
                Console.WriteLine("You did not enter a token." +
                    " WitAI features will be unavailable until you provide WitAI token");
                return false;
            }

            Console.WriteLine("Token entered. Saving to file...");
            botData.WitToken = witAiToken;
            botData.SerializeToFile();
            Console.WriteLine("Token saved!");
            return true;
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            botData.SerializeToFile();
        }
    }
}