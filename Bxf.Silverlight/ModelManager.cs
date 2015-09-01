using System.Windows;

namespace Bxf
{
  /// <summary>
  /// Manages a model or viewmodel object, wrapping it
  /// for access through a XAML resource dictionary.
  /// </summary>
  public class ModelManager : FrameworkElement
  {
    /// <summary>
    /// Gets or sets the model or viewmodel value.
    /// </summary>
    public static readonly DependencyProperty DataProperty =
        DependencyProperty.Register("Data", typeof(object), typeof(ModelManager), null);

    /// <summary>
    /// Gets or sets the model or viewmodel value.
    /// </summary>
    public object Data
    {
      get { return (object)GetValue(DataProperty); }
      set { SetValue(DataProperty, value); }
    }
  }
}
