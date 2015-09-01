﻿using System.Windows.Controls;

namespace UsingBxfNav.ViewModels
{
  public class NavigationShell : Bxf.Shell
  {
    public NavigationShell()
    {
      // use the custom view factory
      this.ViewFactory = new CustomViewFactory();
    }

    protected override void InitializeBindingResource(Bxf.IView view)
    {
      // do nothing since the view is "lazy loaded" by
      // the navigation engine and the viewmodel is created
      // later
    }
  }

  public class CustomViewFactory : Bxf.ViewFactory
  {
    protected override UserControl CreateUserControl(string viewName)
    {
      // don't create a view - the navigation engine does that
      return null;
    }
  }
}
