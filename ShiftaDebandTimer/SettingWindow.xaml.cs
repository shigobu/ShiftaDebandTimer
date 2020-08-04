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
        /// <summary>
        /// シフタキー
        /// </summary>
        internal Key ShiftaKey { get; set; }

        /// <summary>
        /// デバンドキー
        /// </summary>
        internal Key DebandKey { get; set; }

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
    }
}
