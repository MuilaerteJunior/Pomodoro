using System;
using System.Linq;
using System.Collections.Generic;
using System.Media;
using System.Threading;

namespace Pomodoro
{
    /// <summary>
    /// Interface for define which sound you would like to play between each clock's tick
    /// and also for when the timer rings
    /// </summary>
    public interface IBeepProvider {
        void BeepSound();
        void BeepBigSound();
    }
}