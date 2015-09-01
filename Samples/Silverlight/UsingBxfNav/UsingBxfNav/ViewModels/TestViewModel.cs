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
using System.ComponentModel;

namespace UsingBxfNav.ViewModels
{
  public class TestViewModel : INotifyPropertyChanged
  {
    private Models.TestModel _model;

    public TestViewModel()
    {
      _model = new Models.TestModel(0);
    }

    public TestViewModel(string args)
    {
      var id = Int32.Parse(args.Substring(args.IndexOf("=") + 1));
      _model = new Models.TestModel(id);
    }

    public void ShowItem()
    {
      Bxf.Shell.Instance.ShowView("Test?id=987", null, null, null);
    }

    public int Id
    {
      get { return _model.Id; }
      set { _model.Id = value; OnPropertyChanged("Id"); }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
      if (PropertyChanged != null)
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
