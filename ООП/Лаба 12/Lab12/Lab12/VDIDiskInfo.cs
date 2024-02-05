using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab12
{
    public class VDIDiskInfo
    {
        public static void GetDiskInformation()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine($"Drive: {drive.Name}:\nFree Space: {drive.TotalFreeSpace} bytes;\nTotal Size: {drive.TotalSize} bytes;\nFile System: {drive.DriveFormat};\nVolume Label: {drive.VolumeLabel};\n\n");
            }
        }
    }
}
