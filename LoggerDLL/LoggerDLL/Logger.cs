using System;
using System.IO;

namespace LoggerDLL
{
    public class Logger
    {
        private static string filePath = @"c:\LoggerDLL\"; //This is used as a default if the user does not set their own path in their application.
        private readonly static string date = $"{DateTime.Now:yyyyMMdd}";
        private readonly string time = $"{DateTime.Now.ToShortTimeString()} [ERROR]";
        private static string file = $"{filePath}{date} Log.txt"; //This is used as a default if the user does not set their own name in their application.

        /// <summary>
        /// Used to configure the file path and name supplied by the user set in their application.
        /// </summary>
        /// <param name="pathName">string value. Sets the path and name of the Directory</param>
        /// <param name="fileName">string value. Sets the name of the log file</param>
        public void SetupLogFiles(string pathName, string fileName)
        {
            filePath = pathName;
            file = $"{filePath}{date} {fileName}.txt";
        }

        internal void CheckDirectory()
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
        }

        internal void WriteToFile(string e)
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

        public void Error(string e)
        {
            //TODO 
        }

        public void Info(string e)
        {
            //TODO
        }

        public void Warning(string e)
        {
            //TODO
        }
    }
}
