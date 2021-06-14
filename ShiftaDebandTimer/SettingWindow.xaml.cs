using System.Windows;
using System.Windows.Input;

namespace ShiftaDebandTimer
{
    /// <summary>
    /// SettingWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingWindow : Window
    {
		private Key shiftaKey;
		/// <summary>
		/// シフタキー
		/// </summary>
		internal Key ShiftaKey
		{
			get
			{
				return shiftaKey;
			}
			set
			{
				if (shiftaKey != value)
				{
					shiftaKey = value;
					shiftaKeyText.Text = shiftaKey.ToString();
				}
			}
		}

        private Key debandKey;
		/// <summary>
		/// デバンドキー
		/// </summary>
		internal Key DebandKey
		{
			get
			{
				return debandKey;
			}
			set
			{
				if (debandKey != value)
				{
					debandKey = value;
					debandKeyText.Text = debandKey.ToString();
				}
			}
		}

        /// <summary>
        /// カウントダウン時間(秒)
        /// </summary>
        internal int CountDownSec
        {
            get
            {
                int.TryParse(countDownTimeTextBox.Text, out int temp);
                return temp;
            }
            set
            {
                countDownTimeTextBox.Text = value.ToString();
            }
        }

        public SettingWindow(Key shiftaKey, Key debandKey, int countDownSec)
        {
            InitializeComponent();

            ShiftaKey = shiftaKey;
            DebandKey = debandKey;
            CountDownSec = countDownSec;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

		private void ShiftaKeyButton_Click(object sender, RoutedEventArgs e)
		{
			KeyValueGetWindow keyValueGetWindow = new KeyValueGetWindow() { Owner = this };
			if (keyValueGetWindow.ShowDialog() == true)
			{
				ShiftaKey = keyValueGetWindow.Key;
			}
		}

		private void DebandKeyButton_Click(object sender, RoutedEventArgs e)
		{
			KeyValueGetWindow keyValueGetWindow = new KeyValueGetWindow() { Owner = this };
			if (keyValueGetWindow.ShowDialog() == true)
			{
				DebandKey = keyValueGetWindow.Key;
			}
		}
	}
}
