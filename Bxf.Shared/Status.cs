#if WINDOWS_UWP
using Windows.UI.Xaml;
#elif XAMARIN
using Xamarin.Forms;
#else
using System.Windows;
#endif

namespace Bxf
{
    /// <summary>
    /// Defines members to describe status information
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Status"/> class.
        /// </summary>
        public Status()
        {
            IsOk = true;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is ok.
        /// </summary>
        /// <remarks>
        /// Intended to indicate whether the operation (to which this instance of
        /// status information relates) was successful or not.
        /// </remarks>
        /// <value><c>true</c> if this instance is ok; otherwise, <c>false</c>.</value>
        public bool IsOk { get; set; }

        /// <summary>
        /// Gets or sets the associated visual element for this instance.
        /// </summary>
        /// <remarks>
        /// This is optional.
        /// </remarks>
        /// <value>The visual element.</value>
#if XAMARIN
        public VisualElement Visual { get; set; }
#else
        public FrameworkElement Visual { get; set; }
#endif
    }
}