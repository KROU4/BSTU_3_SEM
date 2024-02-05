using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab12
{
    public class VDIDirInfo
    {
        public void GetDirInfo(string directoryPath)
        {
            int fileCount = Directory.GetFiles(directoryPath).Length;
            int subdirectoryCount = Directory.GetDirectories(directoryPath).Length;
            Console.WriteLine($"File Count: {fileCount};\nCreation Time: {Directory.GetCreationTime(directoryPath)};\nSubdirectories Count: {subdirectoryCount}");
            string[]? parentDirectories = Directory.GetParent(directoryPath)?.GetDirectories().Select(d => d.FullName).ToArray();
            if (parentDirectories != null)
            {
                Console.WriteLine($"Parent Directories: {string.Join(", ", parentDirectories)}");
            }
            Console.WriteLine("\n");
        }
    }
}
