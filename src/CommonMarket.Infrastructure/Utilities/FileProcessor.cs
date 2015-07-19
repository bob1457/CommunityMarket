using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMarket.Infrastructure.Utilities
{
    public class FileProcessor
    {
        public static string GetFileExtension(string fileName)
        {
            FileInfo fInfo = new FileInfo(fileName);

            return fInfo.Extension.ToLower().ToString();
        }

        public static string GetFileSize(string fileName)
        {
            FileInfo fInfo = new FileInfo(fileName);

            return fInfo.Length.ToString();
        }

        public static string ConvertStringArrayToString(string[] array)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string value in array)
            {
                stringBuilder.Append(value);
                stringBuilder.Append(';');

            }
            return stringBuilder.ToString();
        }

        public static void DeleteFile(string file)
        {
            File.Delete(file);
        }

        public static void RenameFile(string oldName, string newName)
        {
            File.Move(oldName, newName);
        }
    }
}
