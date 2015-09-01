using System.ComponentModel;

namespace UsingBxfPan.Models
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
