using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Game_UI.Tools
{
    public static class ExtentionMethods
    {
        //private static Action EmptyDelegate = delegate () { };
        public static void Refresh(this UIElement uiElement, int laps)
        {
            //uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
            //Thread.Sleep(50);
            var actualTask = new Task(() => uiElement.UpdateLayout());
            actualTask.Wait(laps);
        }
    }
}
