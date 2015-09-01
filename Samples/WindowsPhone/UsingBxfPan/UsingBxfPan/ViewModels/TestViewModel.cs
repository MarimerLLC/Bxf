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

namespace UsingBxfPan.ViewModels
{
  public class TestViewModel : ViewModelBase
  {
    public TestViewModel(string querystring)
    {
      Text = querystring;
      if (string.IsNullOrEmpty(Text))
        Text = "<null>";
    }

    private string _text;
    public string Text
    {
      get { return _text; }
      set { _text = value; OnPropertyChanged("Text"); }
    }

    public void Close()
    {
      Bxf.Shell.Instance.ShowView(null, "Dialog");
    }
  }
}
