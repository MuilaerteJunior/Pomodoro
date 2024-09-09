using System;
using System.Linq;
using System.Collections.Generic;
using System.Media;
using System.Threading;
using MediaPlayer;
using System.Windows;

namespace Pomodoro
{
    public class BeepProvider : IBeepProvider {
        private SoundPlayer _defaultAlarm;
        private SoundPlayer _tick;
        private bool _isTickEnabled = true;

        public BeepProvider() {
            _defaultAlarm = new SoundPlayer(@"Alarm_1.wav");
            _tick = new SoundPlayer(@"clock_ticking.wav");
            
        }

        public void DisableTickSound(bool disable)
        {
            _isTickEnabled = !disable;
        }

        public void BeepSound() {
            if ( _isTickEnabled)
                _tick.Play();
        }

        public void BeepBigSound() {            
            _defaultAlarm.Play();
        }
    }
}