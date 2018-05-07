using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ScratchpadWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
    /// <summary>
    /// When the application first opens
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Get every logic drive on the machine
            foreach (var drive in Directory.GetLogicalDrives())
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
        }

        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            #region Initial Checks
            var item = (TreeViewItem)sender;
            // if the item only contains the dummy data
            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            //clear the dummy data
            item.Items.Clear();

            // Get full path name
            var fullPath = (string)item.Tag;
            #endregion

            #region Get Directories
            // Create a blank list  for directories
            var directories = new List<string>();

            // try and get directories from the folder - ignoring any issues doing so
            try
            {
                var dirs = Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                    directories.AddRange(dirs);
                //Get every folder that's 
            }
            catch
            { }

            directories.ForEach(directoryPath =>
            {
                var subItem = new TreeViewItem()
                {
                    //set header as folder name
                    Header = GetFileFolderName(directoryPath),
                    // and tag is full path
                    Tag = directoryPath 
                };
                // add dummy item to expand folder
                subItem.Items.Add(null);

                // handle expanding
                subItem.Expanded += Folder_Expanded;

                // Add this item to the parent 
                item.Items.Add(subItem);
            });
            #endregion
            
            #region Get Files
            // Create a blank list  for files
            var files = new List<string>();

            // try and get files from the folder - 
            // ignoring any issues doing so
            try
            {
                var fs = Directory.GetFiles(fullPath);
                if (fs.Length > 0)
                    files.AddRange(fs);
                //Get every folder that's 
            }
            catch
            { }

            // For each file...
            files.ForEach(filePath =>
            {
                // Create file item
                var subItem = new TreeViewItem()
                {
                    //set header as file name
                    Header = GetFileFolderName(filePath),
                    // and tag is full path
                    Tag = filePath 
                };


                // Add this item to the parent 
                item.Items.Add(subItem);
            });

            #endregion
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
