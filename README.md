Basic Xaml Framework
====================
[![Dependabot Status](https://api.dependabot.com/badges/status?host=github&repo=MarimerLLC/Bxf)](https://dependabot.com)

Basic Xaml Framework (Bxf) is a simple, streamlined set of UI components designed to demonstrate the minimum framework functionality required to make MVVM work well while leveraging the Visual Studio 2010 XAML designer ("Cider"). Bxf works with Silverlight, WPF and WP7.

Bxf was transferred from [CodePlex](https://bxf.codeplex.com) to GitHub September 1, 2015.

The Bxf download includes the framework binaries, along with sample application code for WPF, Silverlight and Windows Phone.

* Bxf was used in some [Microsoft Tech Ed demos](http://www.lhotka.net/Article.aspx?id=389bb59d-2760-4309-aee2-f55a976ef100).
* Bxf is discussed as part of [this video](http://www.lhotka.net/files/MvvmIntro.wmv).
* The CSLA .NET SimpleNTier sample uses Bxf. It is the Samples\Net\cs\SimpleNTier folder in the [Samples download](http://www.lhotka.net/cslanet/download.aspx).

---

In its simplest form, Bxf acts as a message or request router from application code to a presenter handler.

The idea is that your application code, typically your viewmodel code, needs to do a set of basic things:

1. Show views 
1. Show status information 
1. Show error information

At runtime these need to be actually displayed to a user. At test time they should not be displayed, but instead the fact that they should have been displayed needs to be recorded, so the unit test can verify that the attempt was made.

To this end, Bxf has two primary interfaces: IShell and IPresenter. And it has a singleton instance of the IShell/IPresenter implementation, accessed through the Bxf.Shell.Instance static property.

Bxf.Shell.Instance exposes the IShell interface by default, so application code can easily call its ShowView, ShowStatus and ShowError methods.

But before the application invokes those methods, you need to create a presenter that actually makes those methods do something useful. The presenter casts Bxf.Shell.Instance to IPresenter and handles three events: OnShowView, OnShowStatus and OnShowError.

A runtime presenter is usually an object that is bound to the main form or main page - basically the main UI shell element - of your application. In the constructor of this presenter object, I have handlers for these three events, and in those handlers is where my code (indirectly or directly) causes the UI shell to display the view, status or error information.

Once the presenter events are being handled, all the rest of the application code can use call the three methods on Bxf.Shell.Instance to show views, status and error information.

In my unit test project I also have a presenter. This is something I typically set up in the test initialization method, again handling the three events. For testing though, I just store the view, region, error, title and status information in private fields so my unit test methods can check those fields to see if the appropriate view, status or error information would have been displayed were the application actually running.
