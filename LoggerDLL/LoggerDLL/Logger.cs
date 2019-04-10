using System;
using System.IO;

namespace LoggerDLL
{
    public class Logger
    {
        private readonly string filePath = @"c:\TestLogs\";
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
            string date = $"{DateTime.Now:yyyyMMdd}";
            string time = $"{DateTime.Now.ToShortTimeString()} [ERROR]";
            string file = $"{filePath}{date} Log.txt";
            if (File.Exists(file))
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
