﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Scheduler.Utils
{
    public class FileNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameterm, CultureInfo culture)
        {
            if (value is string path)
            {
                return Path.GetFileName(path);            
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
