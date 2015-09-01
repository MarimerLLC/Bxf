using System;
using System.Windows.Data;

namespace Bxf.Converters
{
  /// <summary>
  /// Inverts a bool value, returning true for false 
  /// and false for true.
  /// </summary>
  public class NotConverter : IValueConverter
  {
    /// <summary>
    /// Converts a bool value to its opposite.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="targetType">Target type (ignored).</param>
    /// <param name="parameter">Parameter value (ignored).</param>
    /// <param name="culture">Culture (ignored).</param>
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return !(bool)value;
    }

    /// <summary>
    /// Converts a bool value to its opposite.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="targetType">Target type (ignored).</param>
    /// <param name="parameter">Parameter value (ignored).</param>
    /// <param name="culture">Culture (ignored).</param>
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return !(bool)value;
    }
  }
}
