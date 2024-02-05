using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab12
{
    public class VDIFileInfo
    {
        public void GetFileDetails(string filePath)
        {
            FileInfo fileInfo = new(filePath);
            Console.WriteLine($"Full Path: {Path.GetFullPath(filePath)};\nSize: {fileInfo.Length} bytes;\nExtension: {fileInfo.Extension};\nName: {fileInfo.Name};\nCreated: {fileInfo.CreationTime};\nLast Modified: {fileInfo.LastWriteTime}\n\n");
        }
    }
}
