using System;
using System.IO;

namespace LoggerDLL
{
    public class Logger
    {
        private static string filePath = @"c:\LoggerDLL\"; //This is used as a default if the user does not set their own path in their application.
        private readonly static string date = $"{DateTime.Now:yyyyMMdd}";
        private readonly string time = $"{DateTime.Now.ToShortTimeString()}";
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

        /// <summary>
        /// Method to write the information passed to it to a text file.
        /// </summary>
        /// <param name="outPutText">string value. the text to be written to the log file</param>
        /// <param name="type">string value. type of text (error, info, warning)</param>
        internal void WriteToFile(string outPutText, string type)
        {
            CheckDirectory();

            if (File.Exists(file)) // TODO set the file size. Roll over to a new file if the limit is reached.
            {
                using (var fileWriter = new StreamWriter(file, true))
                {
                    fileWriter.WriteLine($"{time} {type} {outPutText}");
                }
            }
            else
            {
                using (var fileWriter = new StreamWriter(file, true))
                {
                    fileWriter.WriteLine($"{time} {type} {outPutText}");
                }
            }

        }


        public void Error(string error)
        {
            WriteToFile(error, "[ERROR]");
        }

        public void Info(string info)
        {
            WriteToFile(info, "[INFO]");
        }

        public void Warning(string warning)
        {
            WriteToFile(warning, "[WARNING]");
        }
    }
}
