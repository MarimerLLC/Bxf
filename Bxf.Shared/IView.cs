#if WINDOWS_UWP
using Windows.UI.Xaml.Controls;
#elif XAMARIN
using Xamarin.Forms;
#else
using System.Windows.Controls;
#endif

namespace Bxf
{
  /// <summary>
  /// Defines the members of a
  /// view.
  /// </summary>
  public interface IView
  {
    /// <summary>
    /// Gets the instance of the view.
    /// </summary>
#if XAMARIN
    VisualElement ViewInstance { get; }
#else
    UserControl ViewInstance { get; }
#endif
    /// <summary>
    /// Gets the name of the view used to create
    /// the view instance by the view factory.
    /// </summary>
    string ViewName { get; }
    /// <summary>
    /// Gets the name of the binding resource to which
    /// the model should be connected.
    /// </summary>
    string BindingResourceKey { get; }
    /// <summary>
    /// Gets the model to bind to the view.
    /// </summary>
    object Model { get; }
  }
}
