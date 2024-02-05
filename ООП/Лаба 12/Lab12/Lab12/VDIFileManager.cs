using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab12
{
    public class VDIFileManager
    {
        private string diskPath;

        public VDIFileManager(string diskPath)
        {
            this.diskPath = diskPath;
        }

        public void ReadDirectoryInfo()
        {
            string[] filesAndFolders = Directory.GetFileSystemEntries(diskPath);
            string inspectDir = Path.Combine(diskPath, "VDIInspect");
            Directory.CreateDirectory(inspectDir);

            string dirInfoFilePath = Path.Combine(inspectDir, "VDIdirinfo.txt");
            File.WriteAllLines(dirInfoFilePath, filesAndFolders);
        }

        public void CopyandDelete()
        {
            string inspectDir = Path.Combine(diskPath, "VDIInspect");
            string dirInfoFilePath = Path.Combine(inspectDir, "VDIdirinfo.txt");

            string copyPath = Path.Combine(inspectDir, "VDIdirinfo_copy.txt");
            File.Copy(dirInfoFilePath, copyPath);
            File.Delete(dirInfoFilePath);
        }

        public void CopyFilesByExtension(string sourceDir, string destinationDir, string extension)
        {
            Directory.CreateDirectory(destinationDir);

            var filesToCopy = Directory.GetFiles(sourceDir)
                                        .Where(file => Path.GetExtension(file) == extension);

            foreach (var file in filesToCopy)
            {
                string destinationFile = Path.Combine(destinationDir, Path.GetFileName(file));
                File.Copy(file, destinationFile);
            }

            // Move XXXFiles to XXXInspect
            string xxxFilesDir = Path.Combine(diskPath, "VDIFiles");
            Directory.Move(destinationDir, xxxFilesDir);
            string inspectDir = Path.Combine(diskPath, "VDIInspect");
            Directory.Move(xxxFilesDir, Path.Combine(inspectDir, "VDIFiles"));
        }

        public void CreateAndExtractArchive()
        {
            string xxxFilesDir = Path.Combine(diskPath, "VDIInspect", "VDIFiles");
            string archivePath = Path.Combine(diskPath, "VDIInspect", "VDIFilesArchive.zip");

            ZipFile.CreateFromDirectory(xxxFilesDir, archivePath);

            string extractDir = Path.Combine(diskPath, "VDIInspect", "ExtractedFiles");
            ZipFile.ExtractToDirectory(archivePath, extractDir);
        }

    }
}
