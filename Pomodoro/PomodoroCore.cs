using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pomodoro {
    public enum CurrentStatus {
        None,
        Running,
        Paused,
        Finished
    }

    /// <summary>
    /// Method for Pomodoro technique looks like
    /// </summary>
    public class PomodoroCore {
        public IBeepProvider BeepProvider { get; set; }
        private CurrentStatus Status { get; set; }
        public int totalCount = 0;
        private DateTime _dtStart, _dtEnd;

        public PomodoroCore(IBeepProvider beepProvider) {
            BeepProvider = beepProvider ?? throw new ArgumentNullException();
            this.Status = CurrentStatus.None;
        }

        public void Start()
        {
            Console.WriteLine("Start");
            this.CountDown();
        }

        public void SetCountDown(double minutes) {
            if ( minutes <= 0 ) throw new NotSupportedException();
            if ( this.Status == CurrentStatus.Running ) return;

            Console.WriteLine("Setcountdown");

            _dtStart = DateTime.Now;
            _dtEnd = _dtStart.AddMinutes(minutes);
        }

        private void CountDown()
        {
            var autoEvent = new AutoResetEvent(false);
            using (var t = new Timer(TimerCallBack, autoEvent, 0, 1000))
            {
                autoEvent.WaitOne();
            }
            Console.WriteLine("Countdown end");
        }

        private void TimerCallBack(object state)
        {
            Console.WriteLine(DateTime.Now.ToString());
            this.Status = CurrentStatus.Running;
            var autoEvt = (AutoResetEvent) state;
            if (this.IsFinished())
            {
                this.BeepProvider.BeepBigSound();
                this.Status = CurrentStatus.Finished;
                autoEvt.Set();
            } else 
                this.BeepProvider.BeepSound();
        }

        private bool IsFinished()
        {
            return DateTime.Now.Subtract(_dtEnd).TotalMilliseconds >= 0;
        }

        public void PauseCountdown() {
            this.Status = CurrentStatus.Paused;

        }

        public void ResumeCountdown() {
            this.Status = CurrentStatus.Running;
        }

        internal void Rest()
        {
            Console.WriteLine("Rest start:" + DateTime.Now);
            Thread.Sleep(TimeSpan.FromMinutes(5));
            this.BeepProvider.BeepBigSound();
            Console.WriteLine("Rest finished:" + DateTime.Now);
        }
    }
}