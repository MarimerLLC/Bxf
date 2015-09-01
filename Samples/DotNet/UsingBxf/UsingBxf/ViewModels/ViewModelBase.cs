using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UsingBxf.ViewModels
{
  public class ViewModelBase : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
      var temp = PropertyChanged;
      if (temp != null)
        temp(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
