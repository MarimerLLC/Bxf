using System.ComponentModel;

namespace UsingBxfPan.ViewModels
{
  public class ViewModelBase : INotifyPropertyChanged
  {
    private string _header = null;
    public string Header
    {
      get { return _header; }
      set
      {
        _header = value;
        OnPropertyChanged("Header");
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
      var temp = PropertyChanged;
      if (temp != null)
        temp(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
