using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pomodoro {
    public class OutputPanelInfo {
        public string Output { get; set; }
    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Developed by Muilaerte Junior
    /// </summary>
    public partial class MainWindow : Window {
        private PomodoroCore _pomodoro = null;
        private static ObservableCollection<OutputPanelInfo> _output;
        private static int numberOfTicks;
        private BeepProvider _beepProvider;

        public MainWindow() {
            StartupDefaultLayout();
            InitializeComponent();

            SetupGrid();

            SetupComboboxes();

            SetNotStartedStateLayout();
            _beepProvider =new BeepProvider();
            _pomodoro = new PomodoroCore(_beepProvider);
        }

        private void SetupGrid()
        {
            _output = new ObservableCollection<OutputPanelInfo>();
            dataGridOutput.DataContext = _output;
        }

        #region Layouts
        private void StartupDefaultLayout() {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void SetNotStartedStateLayout() {
            cmbIntervalDuration.IsEnabled = 
			    cmbCheckmarkAmount.IsEnabled = true;
            btnPause.IsEnabled = 
                btnResume.IsEnabled = false;
            btnStart.IsEnabled = true;
        }
        
        private void SetStartedStateLayout() {
            btnStart.IsEnabled = false;
            btnPause.IsEnabled = true;
            cmbIntervalDuration.IsEnabled = 
                cmbCheckmarkAmount.IsEnabled = false;

            UpdatePomodoroInfoStart();
        }

        private void SetFinishedStateLayout()
        {
            UpdatePomodoroInfoRest();
        }
        #endregion

        private void SetupComboboxes() {
#if DEBUG
            cmbIntervalDuration.Items.Add(1);
#endif
            cmbIntervalDuration.Items.Add(15);
            cmbIntervalDuration.Items.Add(20);
            cmbIntervalDuration.Items.Add(25);
            cmbIntervalDuration.SelectedValue = 25;

            cmbCheckmarkAmount.Items.Add(1);
            cmbCheckmarkAmount.Items.Add(2);
            cmbCheckmarkAmount.Items.Add(3);
            cmbCheckmarkAmount.Items.Add(4);
            cmbCheckmarkAmount.Items.Add(5);
            cmbCheckmarkAmount.SelectedValue = 3;
        }


        public void WriteInDataGrid(string content)
        {
            if (string.IsNullOrEmpty(content))
                return;

            _output.Add(new OutputPanelInfo { Output = content });
        }

        private void UpdatePomodoroInfoStart()
        {
            numberOfTicks = --numberOfTicks;
            var startedDate = DateTime.Now;
            WriteInDataGrid(string.Format("Started date: {0}", startedDate.ToString("dd/MM/yyyy HH:mm:ss")));
            WriteInDataGrid(string.Format("Checkmarks remaining: {0} ", numberOfTicks));
            if (numberOfTicks <= 0)
            {
                SetNotStartedStateLayout();
                numberOfTicks = 0;
            } 
        }

        private void UpdatePomodoroInfoRest()
        {
            WriteInDataGrid(string.Format("Countdown finished."));
        }

        #region Buttons
        private void btnStart_Click(object _btnStart, RoutedEventArgs e)
        {
            var minutesInDouble = (int)cmbIntervalDuration.SelectedValue;
            numberOfTicks = (int)cmbCheckmarkAmount.SelectedValue;

            cmbIntervalDuration.IsEnabled = false;
            cmbCheckmarkAmount.IsEnabled = false;

            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(() =>
            {
                try
                {
                    var t = Task.Run(() =>
                    {
                        try
                        {
                            for (var index = 0; index < numberOfTicks; index++)
                            {
                                Console.WriteLine("for " + index);
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    SetStartedStateLayout();
                                });

                                _pomodoro.SetCountDown(minutesInDouble);
                                _pomodoro.Start();

                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    SetFinishedStateLayout();
                                    WriteInDataGrid("Rest moment, waiting 5 minutes");
                                });

                                _pomodoro.Rest();

                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    WriteInDataGrid("Rest moment finished");
                                });
                            }

                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                WriteInDataGrid("Finished");
                                SetNotStartedStateLayout();
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erro: " + ex.ToString());
                            Console.Error.WriteLine(ex.ToString());
                        }
                    }); 
                } catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.ToString());
                    Console.Error.WriteLine(ex.ToString());
                }
            }));
        }
        private void btnPause_Click(object _btnPause, RoutedEventArgs e) {
            _pomodoro.PauseCountdown();

            btnResume.IsEnabled = true;
            ((Button) _btnPause).IsEnabled = false;
        }
        private void btnResume_Click(object _btnResume, RoutedEventArgs e) {
            _pomodoro.ResumeCountdown();

            btnPause.IsEnabled = true;
            ((Button) _btnResume).IsEnabled = false;
        }
        #endregion

        private void chkDisableTickSound_OnClick(object sender, RoutedEventArgs e)
        {
            _beepProvider.DisableTickSound(((CheckBox) sender).IsChecked ?? false);
        }
    }
}
