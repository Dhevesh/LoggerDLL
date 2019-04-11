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
        private int setFileSize = 10; //default value is 10MB.

        /// <summary>
        /// Used to configure the file path and name supplied by the user set in their application.
        /// </summary>
        /// <param name="pathName">string value. sets the path and name of the Directory.</param>
        /// <param name="_fileName">string value. sets the name of the log file.</param>
        /// <param name="_setFileSize">integer value. sets the max size of the file.</param>
        public void SetupLogFiles(string _pathName, string _fileName, int _setFileSize)
        {
            filePath = _pathName;
            file = $"{filePath}{date} {_fileName}.txt";
            setFileSize = _setFileSize;
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
                FormatFileName(setFileSize);
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

        /// <summary>
        /// This method calculates the size of the log file. used to determine the size of a file before rolling over to a new file.
        /// </summary>
        /// <param name="file">string value. takes in as input the name of the file (fully qualified with the file location).</param>
        /// <returns>size of the log file in MegaBytes.</returns>
        internal double CheckFileSize(string file)
        {
            var fileSizeInBytes = new FileInfo(file).Length;
            var mb = 1048576.0;
            var fileSizeInMegaBytes = Math.Round((fileSizeInBytes / mb), 2);
            return fileSizeInMegaBytes;
        }


        internal void FormatFileName(int size) //TODO increment the file name with a variable.
        {
            if (CheckFileSize(file) >= size)
            {
                file = file.Replace(".txt", "_1.txt");
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
