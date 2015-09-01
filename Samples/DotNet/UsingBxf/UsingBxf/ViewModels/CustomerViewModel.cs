using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UsingBxf.ViewModels
{
  public class CustomerViewModel : ViewModelBase
  {
    public CustomerViewModel()
      : this(new Models.CustomerEdit())
    { }

    public CustomerViewModel(Models.CustomerEdit customer)
    {
      _customer = customer;
      // cascade model changed events through VM to View
      _customer.PropertyChanged += (o, e) => OnPropertyChanged(e.PropertyName);
    }

    public void ShowAnError()
    {
      Bxf.Shell.Instance.ShowStatus(new Bxf.Status { IsOk = false, Text = "This is an error" });
      Bxf.Shell.Instance.ShowError("This is an error", "Error title");
    }

    public void ShowSomeStatus()
    {
      Bxf.Shell.Instance.ShowStatus(new Bxf.Status { Text = "Some status" });
    }

    private Models.CustomerEdit _customer;
    public int Id
    {
      get { return _customer.Id; }
      set { _customer.Id = value; }
    }

    public string Name
    {
      get { return _customer.Name; }
      set { _customer.Name = value; }
    }
  }
}
