using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UsingBxf.ViewModels
{
  public class WelcomeViewModel
  {
    public void EditCustomer()
    {
      Bxf.Shell.Instance.ShowView(
        typeof(EditCustomer).AssemblyQualifiedName,
        "customerViewModelViewSource",
        new ViewModels.CustomerViewModel(new Models.CustomerEdit { Id = 123, Name = "Rocky" }),
        "MainContent");
    }
  }
}
