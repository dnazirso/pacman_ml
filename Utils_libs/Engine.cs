using System;
using System.Windows.Threading;

namespace utils_libs
{
    /// <summary>
    /// the game general event handler and timer
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// timer ruling the game ticks
        /// </summary>
        private DispatcherTimer _timer;
        public bool HasBegun { get; set; }

        /// <summary>
        /// Initialize all needed fields and properties
        /// </summary>
        public void SubscribeEvent(EventHandler eventHandler)
        {
            if (_timer == null)
            {
                _timer = new DispatcherTimer();
                _timer.Interval = new TimeSpan(10000);
            }
            _timer.Tick += eventHandler;
        }

        /// <summary>
        /// Launcher
        /// </summary>
        public void Launch()
        {
            if (_timer != null)
            {
                _timer.Start();
            }
        }
    }
}
