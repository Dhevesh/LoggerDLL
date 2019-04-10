using System;
using System.IO;
using System.Linq;

namespace LoggerDLL
{
    internal class Logger
    {
        private readonly string filePath = @"c:\TestLogs\";
        internal void CheckDirectory()
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
        }

        internal void ErrorLog(string e) //Takes in the Exception and logs to file
        {
            CheckDirectory();
            string date = $"{DateTime.Now:(yyyyMMdd)}";
            string time = $"{DateTime.Now.ToShortTimeString()} [ERROR]";
            string file = $"Log {date}.txt";
            if (!Directory.EnumerateFileSystemEntries(filePath).Any())
            {
                using (var fileWriter = new StreamWriter(file, true))
                {
                    fileWriter.WriteLine($"{time} {e}");
                }
            }
            else
            {
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
}
