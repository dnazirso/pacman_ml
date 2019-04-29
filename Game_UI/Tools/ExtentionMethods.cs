using System;
using System.Windows;
using System.Windows.Threading;

namespace Game_UI.Tools
{
    public static class ExtentionMethods
    {
        private static Action EmptyDelegate = delegate () { };
        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }

    }
}
