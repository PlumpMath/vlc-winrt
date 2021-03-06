﻿using System;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace VLC_WinRT.Converters
{
    public class CurrentTrackEnhancifierConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool && (bool)value)
            {
                if (parameter is string)
                {
                    if ((string) parameter == "fontweight")
                    {
                        return FontWeights.Bold;
                    }
                    if ((string) parameter == "visibility")
                    {
                        return Visibility.Visible;
                    }
                }
                if (language == "color")
                {
                    return App.Current.Resources["MainColor"];
                }
            }
            if (parameter is string)
            {
                if ((string) parameter == "fontweight")
                {
                    return FontWeights.Normal;
                }
                if ((string) parameter == "visibility")
                {
                    return Visibility.Collapsed;
                }
            }
            if (language == "color")
            {
                return parameter;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
