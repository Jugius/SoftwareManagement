﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SoftwareManager.Helpers
{
    public class FileSizeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return StringsFormatter.FormatBytes((ulong)value, 1, true);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
