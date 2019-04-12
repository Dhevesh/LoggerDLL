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
                RollOverFile(setFileSize);
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
            var mb = 1048576.0; //1MB in bytes
            var fileSizeInMegaBytes = Math.Round((fileSizeInBytes / mb), 2);
            return fileSizeInMegaBytes;
        }

        /// <summary>
        /// This method creates a new log file if the file limit set is reached
        /// </summary>
        /// <param name="size">integer value. the maximum file size is passed into the method.</param>
        internal void RollOverFile(int size) //TODO increment the file name with a variable.
        {
            string[] getFiles = Directory.GetFiles(filePath);
            int numFiles = -1;
            foreach (var file in getFiles) 
            {
                if (file.Contains(date)) //checks if a file for the current date exists.
                {
                    numFiles++;
                }
            }
            if (numFiles > 0) //if a numbered file exists
            {
                file = file.Replace($".txt", $"_{numFiles}.txt"); //get that numbered file.
            }
            if (CheckFileSize(file) >= size) //checks the size of the file before rolling over to a new file.
            {
                // should be a better way of doing this. NEEDS REFACTORING
                if (file.Contains($"_{numFiles}"))
                {
                    file = file.Replace($"_{numFiles}.txt", $"_{numFiles+1}.txt");
                }
                else
                {
                    file = file.Replace(".txt", $"_{numFiles+1}.txt");
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
