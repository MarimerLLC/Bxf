using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace UsingBxfNav.Xaml
{
  public class NavigationBinder : DependencyObject
  {
    public static string GetUriProperty(DependencyObject obj)
    {
      return (string)obj.GetValue(UriProperty);
    }

    public static void SetUriProperty(DependencyObject obj, string value)
    {
      obj.SetValue(UriProperty, value);
    }

    public static readonly DependencyProperty UriProperty =
      DependencyProperty.RegisterAttached(
      "UriProperty",
      typeof(string),
      typeof(NavigationBinder),
      new PropertyMetadata((s, e) =>
      {
        var ctrl = s as Frame;
        if (ctrl == null)
          throw new InvalidOperationException("This attached property only supports types derived from Frame.");

        var uriString = (string)e.NewValue;
        //if (uriString.Substring(0, 1) != "/")
        //  uriString = "/" + uriString;
        var targetUri = new System.Uri(uriString, UriKind.Relative);
        ctrl.Navigate(targetUri);
      }));
  }
}
