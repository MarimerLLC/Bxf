using System;

namespace Bxf
{
  /// <summary>
  /// Shell implementation must implement this
  /// interface to be invoked by the shell
  /// manager.
  /// </summary>
  public interface IPresenter
  {
    /// <summary>
    /// Event raised when a view is to be displayed.
    /// </summary>
    event Action<IView, string> OnShowView;
    /// <summary>
    /// Event raised when an error is to be displayed.
    /// </summary>
    event Action<string, string> OnShowError;
    /// <summary>
    /// Event raised when a new status is to be displayed.
    /// </summary>
    event Action<Status> OnShowStatus;
    /// <summary>
    /// Event raised when a new IPrincipal object
    /// has been set.
    /// </summary>
    event Action OnNewUser;
  }
}
