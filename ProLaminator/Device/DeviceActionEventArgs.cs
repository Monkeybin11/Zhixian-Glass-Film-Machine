using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       DeviceActionEventArgs
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProLaminator.Device
 * File      Name：       DeviceActionEventArgs
 * Creating  Time：       5/21/2020 3:35:09 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProLaminator.Device
{
    public class DeviceActionEventArgs : System.EventArgs
    {
        public bool IsConnected { private set; get; }

        /// <summary>
        /// 初始化正常标记
        /// </summary>
        public bool IsInitOK { private set; get; }

        /// <summary>
        /// 启动正常标记
        /// </summary>
        public bool IsStartOK { private set; get; }

        /// <summary>
        /// 停止正常标记
        /// </summary>
        public bool IsStopOK { private set; get; }

        /// <summary>
        /// 释放正常标记
        /// </summary>
        public bool IsReleaseOK { private set; get; }

        public ProCommon.Communal.InitException InitError { private set; get; }

        public ProCommon.Communal.StartException StartError { private set; get; }

        public ProCommon.Communal.StopException StopError { private set; get; }

        public ProCommon.Communal.ReleaseException ReleaseError { private set; get; }

        public DeviceActionEventArgs(bool isConnected)
        { IsConnected = isConnected; }

        public DeviceActionEventArgs(ProCommon.Communal.InitException initError, bool isInitOK)
        {
            InitError = initError;
            IsInitOK = isInitOK;
        }

        public DeviceActionEventArgs(ProCommon.Communal.StartException startError, bool isStartOK)
        {
            StartError = startError;
            IsStartOK = isStartOK;
        }

        public DeviceActionEventArgs(ProCommon.Communal.StopException stopError, bool isStopOK)
        {
            StopError = stopError;
            IsStopOK = isStopOK;
        }

        public DeviceActionEventArgs(ProCommon.Communal.ReleaseException releaseError, bool isReleaseOK)
        {
            ReleaseError = releaseError;
            IsReleaseOK = isReleaseOK;
        }
    }
}
