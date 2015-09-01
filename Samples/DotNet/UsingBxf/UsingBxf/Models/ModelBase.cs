using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UsingBxf.Models
{
  public class ModelBase : INotifyPropertyChanged
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
