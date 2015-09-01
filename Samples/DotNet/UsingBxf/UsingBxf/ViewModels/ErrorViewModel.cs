using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UsingBxf.ViewModels
{
  public class ErrorViewModel : ViewModelBase
  {
    public ErrorViewModel(string message, string title)
    {
      Message = message;
      Title = title;
    }

    private string _message;
    public string Message
    {
      get { return _message; }
      protected set { _message = value; OnPropertyChanged("Message"); }
    }

    private string _title;
    public string Title
    {
      get { return _title; }
      protected set { _title = value; OnPropertyChanged("Title"); }
    }
  }
}
