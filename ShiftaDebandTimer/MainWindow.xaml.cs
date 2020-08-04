using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using HongliangSoft.Utilities.Gui;
using System.Diagnostics;
using System.Windows.Forms;

namespace ShiftaDebandTimer
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		DispatcherTimer dispatcherTimer = new DispatcherTimer();

		int shiftaTimeSec = 0;
		Stopwatch shiftaStopwatch = new Stopwatch();
		int debandTimeSec = 0;
		Stopwatch debandStopwatch = new Stopwatch();

        /// <summary>
        /// シフタのキー
        /// </summary>
        private Key ShiftaKey { get; set; } = Key.D6;

        /// <summary>
        /// デバンドのキー
        /// </summary>
        private Key DebandKey { get; set; } = Key.D7;

        public MainWindow()
		{
			InitializeComponent();

			KeyboardHook keyboardHook = new KeyboardHook();
			keyboardHook.KeyboardHooked += KeyboardHook_KeyboardHooked;

			//タイマー設定
			//10ミリ秒
			dispatcherTimer.Interval = new TimeSpan(100);
			dispatcherTimer.Tick += DispatcherTimer_Tick;
			dispatcherTimer.Start();
		}

		private void DispatcherTimer_Tick(object sender, EventArgs e)
		{
			int nokoriSec = shiftaTimeSec - (int)shiftaStopwatch.Elapsed.TotalSeconds;
			if (nokoriSec < 0)
			{
				nokoriSec = 0;
				shiftaStopwatch.Stop();
			}
			shiftaTextBlok.Text = $"{nokoriSec / 60}:{nokoriSec % 60:00}";

			nokoriSec = debandTimeSec - (int)debandStopwatch.Elapsed.TotalSeconds;
			if (nokoriSec < 0)
			{
				nokoriSec = 0;
				debandStopwatch.Stop();
			}
			debandTextBlok.Text = $"{nokoriSec / 60}:{nokoriSec % 60:00}";
		}

		private void KeyboardHook_KeyboardHooked(object sender, KeyboardHookedEventArgs e)
		{
			if ((int)e.KeyCode == KeyInterop.VirtualKeyFromKey(ShiftaKey) && e.UpDown == KeyboardUpDown.Down)
			{
				shiftaTimeSec = 180;
				shiftaStopwatch.Restart();
			}
			if ((int)e.KeyCode == KeyInterop.VirtualKeyFromKey(DebandKey) && e.UpDown == KeyboardUpDown.Down)
			{
				debandTimeSec = 180;
				debandStopwatch.Restart();
			}
		}

		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}

		private void MenuItem_CloseClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void MenuItem_SettingClick(object sender, RoutedEventArgs e)
		{
            SettingWindow settingWindow = new SettingWindow(ShiftaKey, DebandKey) { Owner = this };
            if (settingWindow.ShowDialog() == true)
            {
                ShiftaKey = settingWindow.ShiftaKey;
                DebandKey = settingWindow.DebandKey;
            }
		}
	}
}
