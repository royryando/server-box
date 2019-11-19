using System;
using LiteDB;

namespace ServerBox.Lib
{
    public class DatabaseHelper
    {
        LiteDatabase Connection;
        public static string ENV_DIRECTORY = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ServerBox";

        public void Initialize()
        {
            string dbname = "serverbox";
            System.IO.Directory.CreateDirectory(ENV_DIRECTORY);
            string path = String.Format(@"{0}\{1}.db", ENV_DIRECTORY, dbname);

            Connection = new LiteDatabase(path);
        }

        public LiteDatabase GetConnection()
        {
            return Connection;
        }

    }
}
