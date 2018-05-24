using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchpadWpf.Directory.Data
{
    public class DirectoryItem
    {
        /// <summary>
        ///  The type of this item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// The absolute path to this item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The name of the directory item
        /// </summary>
        public string Name { get { return this.Type == DirectoryItemType.Drive ? FullPath : 
                    DirectoryStructure.GetFileFolderName(this.FullPath); } }

        public static List<DirectoryItem> GetDirectoryItems(string fullPath)
        {
            
            //Create empty list
            var items = new List<DirectoryItem>();

            #region Get Folders
            // try and get directories from the folder - 
            //ignoring any issues doing so
            try
            {
                var dirs = System.IO.Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                    items.AddRange(dirs.Select(dir => new DirectoryItem { FullPath = dir, Type = DirectoryItemType.Folder }));
            }
            catch
            { }
            #endregion

            #region Get Files
            // try and get directories from the folder - 
            //ignoring any issues doing so
            try
            {
                var files = System.IO.Directory.GetFiles(fullPath);
                if (files.Length > 0)
                    items.AddRange(files.Select(file => new DirectoryItem { FullPath = file, Type = DirectoryItemType.File }));
            }
            catch
            { }
            #endregion


        }
    }
}
