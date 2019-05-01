using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Game_UI.Tools
{
    /// <summary>
    /// Extension methods
    /// </summary>
    public static class ExtentionMethods
    {
        /// <summary>
        /// Refresh immediately a given portion of the user interface
        /// </summary>
        /// <param name="uiElement">UI element needed to refresh</param>
        /// <param name="laps">pause time laps in "ms"</param>
        public static void Refresh(this UIElement uiElement)
        {
            new Task(() => uiElement.UpdateLayout());
        }
    }
}
