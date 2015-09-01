using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bxf;
using System.Windows;
using System.Windows.Controls;

namespace UsingBxf.ViewModels
{
  public class MainWindowViewModel : DependencyObject
  {
    public MainWindowViewModel()
    {
      // get presenter
      var presenter = (IPresenter)Shell.Instance;

      // show views
      presenter.OnShowView += (view, region) =>
        {
          if (region == "MainContent")
            MainContent = view.ViewInstance;
          else if (region == "ErrorContent")
            ErrorContent = view.ViewInstance;
          else if (region == "StatusContent")
            StatusContent = view.ViewInstance;
          else
            throw new ArgumentException("region");
        };
      // show errors
      presenter.OnShowError += (message, title) =>
        {
          if (message != null)
            Shell.Instance.ShowView(
              typeof(ErrorDisplay).AssemblyQualifiedName,
              "errorViewModelViewSource",
              new ViewModels.ErrorViewModel(message, title),
              "ErrorContent");
          else
            ErrorContent = null;
        };
      // show status
      presenter.OnShowStatus += (status) =>
        {
          Shell.Instance.ShowView(
            typeof(StatusDisplay).AssemblyQualifiedName,
            "statusViewSource",
            status,
            "StatusContent");
          if (status.IsOk)
            Shell.Instance.ShowError(null, null);
        };

      // show initial status
      Shell.Instance.ShowStatus(new Status { IsOk = true, Text = "Ready" });

      // show initial content
      Shell.Instance.ShowView(
        typeof(Welcome).AssemblyQualifiedName,
        "welcomeViewModelViewSource",
        new ViewModels.WelcomeViewModel(),
        "MainContent");
    }

    public static readonly DependencyProperty MainContentProperty =
      DependencyProperty.Register("MainContent", typeof(UserControl), typeof(MainWindowViewModel), null);

    public UserControl MainContent
    {
      get { return (UserControl)GetValue(MainContentProperty); }
      set { SetValue(MainContentProperty, value); }
    }

    public static readonly DependencyProperty ErrorContentProperty =
      DependencyProperty.Register("ErrorContent", typeof(UserControl), typeof(MainWindowViewModel), null);

    public UserControl ErrorContent
    {
      get { return (UserControl)GetValue(ErrorContentProperty); }
      set { SetValue(ErrorContentProperty, value); }
    }

    public static readonly DependencyProperty StatusContentProperty =
      DependencyProperty.Register("StatusContent", typeof(UserControl), typeof(MainWindowViewModel), null);

    public UserControl StatusContent
    {
      get { return (UserControl)GetValue(StatusContentProperty); }
      set { SetValue(StatusContentProperty, value); }
    }
  }
}
