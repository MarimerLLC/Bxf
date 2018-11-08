using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
  /// Responsible for creating a fully populated IView
  /// object.
  /// </summary>
  public class ViewFactory : IViewFactory
  {
    /// <summary>
    /// Creates a populated IView object, including
    /// the instance of the view.
    /// </summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="bindingResourceKey">Name of the binding resource.</param>
    /// <param name="model">Reference to the model or viewmodel for the view.</param>
    public virtual IView GetView(string viewName, string bindingResourceKey, object model)
    {
      return new View(viewName, CreateUserControl(viewName), bindingResourceKey, model);
    }

    /// <summary>
    /// Creates an instance of the view.
    /// </summary>
    /// <param name="viewName">Name of the view.</param>
#if XAMARIN
    protected virtual VisualElement CreateUserControl(string viewName)
#else
    protected virtual UserControl CreateUserControl(string viewName)
#endif
    {
      if (string.IsNullOrEmpty(viewName))
        return null;

      var t = GetType(viewName);
      if (t == null)
        throw new ArgumentException(string.Format("viewName ({0})", viewName));
#if XAMARIN
      return (VisualElement)Activator.CreateInstance(t);
#else
      return (UserControl)Activator.CreateInstance(t);
#endif
    }

    private static Type GetType(string typeName)
    {
#if XAMARIN
      return Type.GetType(typeName, true);
#else
      return Type.GetType(typeName, true, false);
#endif
    }
  }
}
