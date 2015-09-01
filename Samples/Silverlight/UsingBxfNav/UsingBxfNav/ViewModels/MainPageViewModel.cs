using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Navigation;
using System.Windows.Data;
using System.Collections;
using System.Collections.Generic;

namespace UsingBxfNav.ViewModels
{
  public class MainPageViewModel : DependencyObject
  {
    public MainPageViewModel()
    {
      InitializeShell();
      InitializeMenu();
    }

    private void InitializeMenu()
    {
      // initialize menu
      MenuItems = new ObservableCollection<MenuItem>
      {
        { new MenuItem { Name = "home", Uri = "Home" }},
        { new MenuItem { Name = "bad", Uri = "Bad" }},
        { new MenuItem { Name = "test", Uri = "Test", ViewModelResourceName = "testViewModelViewSource", ViewModelType = typeof(ViewModels.TestViewModel) }}
      };
    }

    private void InitializeShell()
    {
      // use custom shell and view factory
      Bxf.Shell.Instance = new NavigationShell();

      // initialize Bxf event handlers
      var presenter = (Bxf.IPresenter)Bxf.Shell.Instance;
      presenter.OnShowView += (view, region) =>
      {
        NextView = view.ViewName;
      };
      presenter.OnShowError += (message, title) =>
      {
        ChildWindow errorWin = new ErrorWindow(title, message);
        errorWin.Show();
      };
    }

    /// <summary>
    /// Updates viewmodel to reflect the navigation that just occurred
    /// either via Bxf or via the browser navigation buttons or address bar.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event args</param>
    public void Navigated(object sender, Bxf.Xaml.ExecuteEventArgs e)
    {
      var args = (NavigationEventArgs)e.TriggerParameter;
      var viewName = args.Uri.OriginalString;
      if (string.IsNullOrWhiteSpace(viewName))
        viewName = "Home";
      string queryString = null;
      var param = viewName.IndexOf("?");
      if (param > 0)
      {
        queryString = viewName.Substring(param + 1);
        viewName = viewName.Substring(0, param);
      }
      foreach (var child in this.MenuItems)
      {
        child.SetState(viewName);
        if (args.Content != null && child.Uri == viewName && !string.IsNullOrEmpty(child.ViewModelResourceName) && child.ViewModelType != null)
        {
          object vm;
          if (queryString == null)
            vm = Activator.CreateInstance(child.ViewModelType);
          else
            vm = Activator.CreateInstance(child.ViewModelType, queryString);
          var cvs = (CollectionViewSource)((Control)args.Content).Resources[child.ViewModelResourceName];
          if (vm is IEnumerable)
            cvs.Source = vm;
          else
            cvs.Source = new List<object> { vm };
        }
      }
    }

    /// <summary>
    /// Handle case where navigation has failed by invoking
    /// standard error/exception handling.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event args</param>
    public void NavigationFailed(object sender, Bxf.Xaml.ExecuteEventArgs e)
    {
      var args = (NavigationFailedEventArgs)e.TriggerParameter;
      args.Handled = true;
      Bxf.Shell.Instance.ShowError(string.Format("Page not found: {0}", args.Uri.OriginalString), "Navigation failure");
    }

    /// <summary>
    /// Gets or sets a list of menu items to be displayed in the menu.
    /// </summary>
    public static readonly DependencyProperty MenuItemsProperty =
      DependencyProperty.Register("MenuItems", typeof(ObservableCollection<MenuItem>), typeof(MainPageViewModel), null);
    /// <summary>
    /// Gets or sets a list of menu items to be displayed in the menu.
    /// </summary>
    public ObservableCollection<MenuItem> MenuItems
    {
      get { return (ObservableCollection<MenuItem>)GetValue(MenuItemsProperty); }
      set { SetValue(MenuItemsProperty, value); }
    }

    /// <summary>
    /// Gets or sets the URI for a view to which the UI
    /// should navigate. Intended for binding to the
    /// navigation frame via NavigationBinder.
    /// </summary>
    public static readonly DependencyProperty NextViewProperty =
      DependencyProperty.Register("NextView", typeof(string), typeof(MainPageViewModel), null);
    /// <summary>
    /// Gets or sets the URI for a view to which the UI
    /// should navigate. Intended for binding to the
    /// navigation frame via NavigationBinder.
    /// </summary>
    public string NextView
    {
      get { return (string)GetValue(NextViewProperty); }
      set { SetValue(NextViewProperty, value); }
    }

    /// <summary>
    /// Defines metadata for each menu item.
    /// </summary>
    public class MenuItem : DependencyObject
    {
      public void SetState(string uriPath)
      {
        if (uriPath == this.Uri)
          this.State = "ActiveLink";
        else
          this.State = "InactiveLink";
      }

      public string Uri { get; set; }
      public string Name { get; set; }
      public string ViewModelResourceName { get; set; }
      public Type ViewModelType { get; set; }

      public static readonly DependencyProperty StateProperty =
        DependencyProperty.Register("State", typeof(string), typeof(MenuItem), new PropertyMetadata("InactiveLink"));
      public string State
      {
        get { return (string)GetValue(StateProperty); }
        set { SetValue(StateProperty, value); }
      }
    }
  }
}
