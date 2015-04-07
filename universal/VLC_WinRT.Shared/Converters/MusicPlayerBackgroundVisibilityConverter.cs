﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using VLC_WinRT.ViewModels;
using VLC_WinRT.Model;

namespace VLC_WinRT.Converters
{
    public class MusicPlayerBackgroundVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (Locator.NavigationService.CurrentPage == VLCPage.MusicPlayerPage)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
