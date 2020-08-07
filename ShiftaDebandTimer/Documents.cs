using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShiftaDebandTimer
{
    [Serializable]
    public class Documents
    {
        /// <summary>
        /// シフタのキー
        /// </summary>
        public Key ShiftaKey { get; set; } = Key.D6;

        /// <summary>
        /// デバンドのキー
        /// </summary>
        public Key DebandKey { get; set; } = Key.D7;
    }
}
