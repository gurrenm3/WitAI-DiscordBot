using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitAI_DiscordBot.Dal
{
    public class DataAccess : IDisposable
    {
        public string DatabasePath { get; protected set; }
        SQLiteConnection dbConnection;

        public DataAccess(string dbPath)
        {
            dbConnection = new SQLiteConnection(dbPath);
            dbConnection.CreateTable<DiscordMsg>();
        }

        public void Insert(DiscordMsg msg)
        {
            dbConnection.Insert(msg, typeof(DiscordMsg));
        }

        public void Dispose()
        {
            dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }

        ~DataAccess() { Dispose(); }
    }    
}
