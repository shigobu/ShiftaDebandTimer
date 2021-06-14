using System;
using System.Collections.Generic;
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

        /// <summary>
        /// カウントダウンの秒数
        /// </summary>
        public int CountDownTime { get; set; } = 180;
    }
}
