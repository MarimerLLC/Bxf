using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Microsoft.Phone.Controls;
using Bxf;
using System.Windows.Navigation;

namespace UsingBxfPan.ViewModels
{
  public class MainViewModel : DependencyObject, INotifyPropertyChanged
  {
    private StatusDisplay _status = new StatusDisplay();
    private System.Windows.Threading.DispatcherTimer _timer = new System.Windows.Threading.DispatcherTimer();

    public MainViewModel()
    {
      // initialize panorama with containers
      this.PanoramaItems = new ObservableCollection<PanoramaItem>
        {
          new PanoramaItem(),
          new PanoramaItem()
        };

      // set up timer to make status display disappear
      _timer.Stop();
      _timer.Interval = new TimeSpan(0, 0, 2);
      _timer.Tick += (o, e) =>
      {
        _timer.Stop();
        Shell.Instance.ShowStatus(null);
      };

      // use custom shell for integration with navigation
      Shell.Instance = new NavigationShell();

      // provide App with reference to this so it can delegate
      // Navigated event here
      App.MainViewModel = this;

      // set up presenter event handlers
      var presenter = (IPresenter)Shell.Instance;
      presenter.OnShowError += (message, title) =>
        {
          MessageBox.Show(message, title, MessageBoxButton.OK);
        };
      presenter.OnShowStatus += (status) =>
        {
          if (status == null)
          {
            StatusItem = null;
          }
          else
          {
            _timer.Stop();
            _status.DataContext = status;
            StatusItem = _status;
            _timer.Start();
          }
        };
      presenter.OnShowView += (view, region) =>
        {
          if (region == "Dialog")
          {
            // Dialog region means showing a full page
            // instead of the panorama
            if (view != null && !string.IsNullOrEmpty(view.ViewName))
            {
              // navigate to new page
              App.RootFrame.Navigate(new Uri(view.ViewName, UriKind.Relative));
            }
            else
            {
              // navigate back, or to main page
              if (App.RootFrame.CanGoBack)
                App.RootFrame.GoBack();
              else
                App.RootFrame.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
          }
          else
          {
            // PaneX refers to a pane in the panorama
            PanoramaItem pane = null;
            if (region == "Pane1")
              pane = PanoramaItems[0];
            else if (region == "Pane2")
              pane = PanoramaItems[1];

            if (pane != null)
            {
              pane.Content = view.ViewInstance;
              var vmb = view.Model as ViewModelBase;
              if (vmb != null)
                pane.Header = vmb.Header;
            }
          }
        };
 

      if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
      {
        // show initial content
        Shell.Instance.ShowView(
          typeof(Views.Welcome).AssemblyQualifiedName,
          null,
          new ViewModels.WelcomeViewModel(),
          "Pane1");
      }
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
      if (args != null && args.Content != null)
      {
        var viewName = args.Uri.OriginalString;
        if (string.IsNullOrEmpty(viewName))
          viewName = "/MainPage.xaml";
        string queryString = null;
        var param = viewName.IndexOf("?");
        if (param > 0)
        {
          queryString = viewName.Substring(param + 1);
          viewName = viewName.Substring(0, param);
        }

        object viewmodel = null;
        // setup viewmodel for pages
        if (viewName.Contains("/TestPage.xaml"))
          viewmodel = new ViewModels.TestViewModel(queryString);
        ((Control)args.Content).DataContext = viewmodel;
      }
    }

    public static readonly DependencyProperty StatusItemProperty =
        DependencyProperty.Register("StatusItem", typeof(UserControl), typeof(MainViewModel), null);

    public UserControl StatusItem
    {
      get { return (UserControl)GetValue(StatusItemProperty); }
      set { SetValue(StatusItemProperty, value); }
    }
    
    /// <summary>
    /// A collection for PanoramaItem objects.
    /// </summary>
    public ObservableCollection<PanoramaItem> PanoramaItems { get; private set; }

    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(String propertyName)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (null != handler)
      {
        handler(this, new PropertyChangedEventArgs(propertyName));
      }
    }
  }
}