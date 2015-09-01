using System;
using System.Windows.Data;

namespace Bxf.Converters
{
  /// <summary>
  /// Converts a bool to a Visibility value. By default
  /// true means Visible, false means Collapsed.
  /// </summary>
  public class VisibilityConverter : IValueConverter
  {
    /// <summary>
    /// Inverts the normal bool to Visibility conversion.
    /// </summary>
    public bool Invert { get; set; }

    /// <summary>
    /// Converts a bool to a Visibility value.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="targetType">Target type (ignored).</param>
    /// <param name="parameter">Parameter value (ignored).</param>
    /// <param name="culture">Culture (ignored).</param>
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      bool v = (bool)value;
      if (Invert) v = !v;
      if (v)
        return System.Windows.Visibility.Visible;
      else
        return System.Windows.Visibility.Collapsed;
    }

    /// <summary>
    /// Returns null.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="targetType">Target type (ignored).</param>
    /// <param name="parameter">Parameter value (ignored).</param>
    /// <param name="culture">Culture (ignored).</param>
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return null;
    }
  }
}
