using UsingBxfPan.Views;

namespace UsingBxfPan.ViewModels
{
  public class WelcomeViewModel : ViewModelBase
  {
    public WelcomeViewModel()
    {
      Header = "welcome";
    }

    public void EditCustomer()
    {
      Bxf.Shell.Instance.ShowView(
        typeof(EditCustomer).AssemblyQualifiedName,
        null,
        new ViewModels.CustomerViewModel(new Models.CustomerEdit { Id = 123, Name = "Rocky" }),
        "Pane2");
    }

    public void TestPage()
    {
      Bxf.Shell.Instance.ShowView(
        "/Views/TestPage.xaml", null, null, "Dialog");
    }
  }
}
