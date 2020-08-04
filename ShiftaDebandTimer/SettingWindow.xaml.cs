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
using System.Windows.Shapes;
using System.Windows.Forms;

namespace ShiftaDebandTimer
{
    /// <summary>
    /// SettingWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingWindow : Window
    {
		internal Key shiftaKey;
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

		internal Key debandKey;
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

        public SettingWindow(Key shiftaKey, Key debandKey)
        {
            InitializeComponent();

            ShiftaKey = shiftaKey;
            DebandKey = debandKey;

            shiftaKeyText.Text = ShiftaKey.ToString();
            debandKeyText.Text = DebandKey.ToString();
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
