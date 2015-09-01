using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UsingBxf.Models
{
  public class CustomerEdit : ModelBase
  {
    private int _id;
    public int Id
    {
      get { return _id; }
      set { _id = value; OnPropertyChanged("Id"); }
    }

    private string _name;
    public string Name
    {
      get { return _name; }
      set { _name = value; OnPropertyChanged("Name"); }
    }
  }
}
