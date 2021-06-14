using System;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using HongliangSoft.Utilities.Gui;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;

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
        /// 設定ファイル名
        /// </summary>
        private const string ConfigurationFileName = "configuration";

        /// <summary>
        /// 設定ファイル拡張子
        /// </summary>
        private const string ConfigurationFileExt = ".xml";

        private Documents Documents { get; set; }

        private string ConfigurationFilePath
        {
            get
            {
                Assembly assembly = Assembly.GetEntryAssembly();
                string assemblyPath = assembly.Location;
                string assemblyDir = Path.GetDirectoryName(assemblyPath);
                return Path.Combine(assemblyDir, ConfigurationFileName + ConfigurationFileExt);
            }
        }

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

            //Documents作成
            Documents = new Documents();

            //XmlSerializerオブジェクトを作成
            //オブジェクトの型を指定する
            XmlSerializer serializer = new XmlSerializer(typeof(Documents));
            //読み込むファイルを開く
            if (File.Exists(ConfigurationFilePath))
            {
                using (StreamReader streamReader = new StreamReader(ConfigurationFilePath, new UTF8Encoding(false)))
                {
                    //XMLファイルから読み込み、逆シリアル化する
                    Documents = (Documents)serializer.Deserialize(streamReader);
                }
            }
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
			if ((int)e.KeyCode == KeyInterop.VirtualKeyFromKey(Documents.ShiftaKey) && e.UpDown == KeyboardUpDown.Down)
			{
				shiftaTimeSec = Documents.CountDownTime;
				shiftaStopwatch.Restart();
			}
			if ((int)e.KeyCode == KeyInterop.VirtualKeyFromKey(Documents.DebandKey) && e.UpDown == KeyboardUpDown.Down)
			{
				debandTimeSec = Documents.CountDownTime;
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
            SettingWindow settingWindow = new SettingWindow(Documents.ShiftaKey, Documents.DebandKey, Documents.CountDownTime) { Owner = this };
            if (settingWindow.ShowDialog() == true)
            {
                Documents.ShiftaKey = settingWindow.ShiftaKey;
                Documents.DebandKey = settingWindow.DebandKey;
                Documents.CountDownTime = settingWindow.CountDownSec;
            }
		}

        private void Window_Closed(object sender, EventArgs e)
        {
            // XmlSerializerオブジェクトを作成
            //オブジェクトの型を指定する
            XmlSerializer serializer = new XmlSerializer(typeof(Documents));
            //書き込むファイルを開く（UTF-8 BOM無し）
            using (StreamWriter streamWriter = new System.IO.StreamWriter(ConfigurationFilePath, false, new UTF8Encoding(false)))
            {
                //シリアル化し、XMLファイルに保存する
                serializer.Serialize(streamWriter, Documents);
            }
        }
    }
}
