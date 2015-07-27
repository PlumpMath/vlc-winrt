﻿using System;
using Windows.Storage;
using Windows.UI.Xaml.Data;

namespace VLC_WinRT.Converters
{
    public class StorageTypeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is StorageFolder)
            {
                return App.Current.Resources["FolderSymbol"];
            }
            else
            {
                return App.Current.Resources["MultimediaFileSymbol"].ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
