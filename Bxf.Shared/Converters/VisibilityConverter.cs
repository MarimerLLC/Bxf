using System;
#if WINDOWS_UWP
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml;
#else
using System.Windows.Data;
using System.Windows;
#endif

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

#if WINDOWS_UWP
    /// <summary>
    /// Converts a bool to a Visibility value.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="targetType">Target type (ignored).</param>
    /// <param name="parameter">Parameter value (ignored).</param>
    /// <param name="language">Language(ignored).</param>
    public object Convert(object value, Type targetType, object parameter, string language)
#else
    /// <summary>
    /// Converts a bool to a Visibility value.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="targetType">Target type (ignored).</param>
    /// <param name="parameter">Parameter value (ignored).</param>
    /// <param name="culture">Culture (ignored).</param>
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
#endif
    {
      bool v = (bool)value;
      if (Invert) v = !v;
      if (v)
        return Visibility.Visible;
      else
        return Visibility.Collapsed;
    }

#if WINDOWS_UWP
    /// <summary>
    /// Returns null.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="targetType">Target type (ignored).</param>
    /// <param name="parameter">Parameter value (ignored).</param>
    /// <param name="language">Language(ignored).</param>
    public object ConvertBack(object value, Type targetType, object parameter, string language)
#else
    /// <summary>
    /// Returns null.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="targetType">Target type (ignored).</param>
    /// <param name="parameter">Parameter value (ignored).</param>
    /// <param name="culture">Culture (ignored).</param>
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
#endif
    {
      return null;
    }
  }
}
