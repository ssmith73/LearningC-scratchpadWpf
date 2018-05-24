using ScratchpadWpf.Directory.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchpadWpf.Directory
{
    public static class DirectoryStructure
    {
   
        /// <summary>
        /// Gets all logical drives on the computer
        /// </summary>
        public static List<DirectoryItem> GetLogicalDrives()
        {
            return System.IO.Directory.GetLogicalDrives().
                 Select(drive => new DirectoryItem { FullPath = drive, Type = DirectoryItemType.Drive }).ToList();
        }

  

        

        //Find file or folder name from a full path
        public static string GetFileFolderName(string directoryPath)
        {
            //c:\Something\a folder
            //c:\Something\a file.png
            // a file file.png (no \    test for this)
            if (string.IsNullOrEmpty(directoryPath))
                return string.Empty;

            //make all slashes back slashes
            var normalisedPath = directoryPath.Replace('/', '\\');

            // (find last \ in pathname)
            var lastIndex = normalisedPath.LastIndexOf('\\');

            // if we don't find a \ return just the path
            if (lastIndex <= 0)
                return directoryPath;

            //return the name after the last backslash
            return directoryPath.Substring(lastIndex + 1);
        }       
    }
}
