using ScratchpadWpf.Directory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchpadWpf.Directory
{
    public static class DirectoryStructure
    {
        public static List<DirectoryItem> GetLogicalDrives();
        #region Window Loaded
            //Get every logic drive on the machine
        foreach (var drive in system.io.Directory.GetLogicalDrives())
        {
            //Create a new item for it
            var item = new TreeViewItem
            {
                //Set the header and path
                Header = drive,
                Tag = drive
            };
            //add a dummy item (shows expand as a null item - no text on icon)
            item.Items.Add(null);

            //listen out for item being expanded
            item.Expanded += Folder_Expanded;
            // Add drive to the main tree view
            FolderView.Items.Add(item);
        }
        #endregion

        #region Helpers

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
        #endregion
    }
}
