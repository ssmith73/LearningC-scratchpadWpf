﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ScratchpadWpf
{
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();
        //Convert to something - from string to image
        /// <summary>
        /// Convert a full path to a specific image type of a drive, folder or file
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Get the full path
            var path = (string)value;

            // If the path is null, ignore
            if (path == null)
                return null;

            // Get the name of the file/folder
            var name = MainWindow.GetFileFolderName(path);

            // By default, we presume an image
            var image = "Images/file.png";


            // If the name is blank, we presume it's a drive, as we cannot
            // have a blank file or folder name
            if (string.IsNullOrEmpty(name))
                image = "Images/drive.png";
            //do we have a directory?
            else if (new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory))
                image = "Images/folder-closed.png";


            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
