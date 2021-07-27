using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WitAI_DiscordBot.Lib
{
    public class BotData
    {
        public string FilePath { get; set; }
        public string DiscordApiToken { get; set; }
        public string MessageDBPath { get; set; }
        public string WitToken { get; set; }
        public bool FirstTimeUser { get; protected set; } = true;


        public BotData() {  }

        internal BotData(string filePath)
        {
            FilePath = filePath;
        }

        internal string Serialize()
        {
            var options = new JsonSerializerOptions() { WriteIndented = true };
            return JsonSerializer.Serialize(this, typeof(BotData), options);
        }

        public void SerializeToFile()
        {
            File.WriteAllText(FilePath, Serialize());
        }

        public static BotData FromFile(string filePath)
        {
            if (!File.Exists(filePath))
                return new BotData(filePath);

            var json = File.ReadAllText(filePath);
            return FromJson(json);
        }

        public static BotData FromJson(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<BotData>(json);
            }
            catch (Exception)
            {
                return new BotData();
            }
        }
    }
}