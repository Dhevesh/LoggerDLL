using System;
using System.IO;

namespace LoggerDLL
{
    public class Logger
    {
        private readonly static string filePath = @"c:\TestLogs\"; // TODO this will take in the value from the configuration file
        private readonly static string date = $"{DateTime.Now:yyyyMMdd}";
        private readonly string time = $"{DateTime.Now.ToShortTimeString()} [ERROR]";
        private readonly string file = $"{filePath}{date} Log.txt"; 
        
        internal void CheckDirectory()
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
        }

        public void ErrorLog(string e) //Takes in the Exception and logs to file
        {
            CheckDirectory();
            
            if (File.Exists(file)) // TODO set the file size. Roll over to a new file if the limit is reached.
            {
                using (var fileWriter = new StreamWriter(file, true))
                {
                    fileWriter.WriteLine($"{time} {e}");
                }
            }
            else
            {
                using (var fileWriter = new StreamWriter(file, true))
                {
                    fileWriter.WriteLine($"{time} {e}");
                }
            }
        }
    }
}
