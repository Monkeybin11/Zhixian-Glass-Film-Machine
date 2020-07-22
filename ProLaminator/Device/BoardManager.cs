using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       BoardManager
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProLaminator.Device
 * File      Name：       BoardManager
 * Creating  Time：       5/20/2020 4:02:45 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProLaminator.Device
{
    public delegate void BoardActionedEventHandle(ProLaminator.Device.Camera cam, ProLaminator.Device.DeviceActionEventArgs e);

    public class BoardManager
    {
        #region 静态单例

        static object _syncObj = new object();
        static BoardManager _instance;
        public static BoardManager Instance
        {
            get
            {
                lock (_syncObj)
                {
                    if (_instance == null)
                    {
                        _instance = new BoardManager();
                    }
                }

                return _instance;
            }
        }

        private BoardManager()
        {
            BoardStateChangedEvent = new BoardActionedEventHandle(OnBoardStateChanged);
            BoardInitializedEvent = new BoardActionedEventHandle(OnBoardInitialized);
            BoardStartedEvent = new BoardActionedEventHandle(OnBoardStarted);
            BoardStoppedEvent = new BoardActionedEventHandle(OnBoardStopped);
            BoardReleasedEvent = new BoardActionedEventHandle(OnBoardReleased);
            ErrorMessage = new StringBuilder();
            ErrorMessage.Clear();
        }

        private void OnBoardReleased(object sender, DeviceActionEventArgs e)
        {

        }

        private void OnBoardStopped(object sender, DeviceActionEventArgs e)
        {

        }

        private void OnBoardStarted(object sender, DeviceActionEventArgs e)
        {

        }

        private void OnBoardInitialized(object sender, DeviceActionEventArgs e)
        {

        }

        private void OnBoardStateChanged(object sender, DeviceActionEventArgs e)
        {

        }

        #endregion

        public event ProLaminator.Device.BoardActionedEventHandle BoardStateChangedEvent;
        public event ProLaminator.Device.BoardActionedEventHandle BoardInitializedEvent;
        public event ProLaminator.Device.BoardActionedEventHandle BoardStartedEvent;
        public event ProLaminator.Device.BoardActionedEventHandle BoardStoppedEvent;
        public event ProLaminator.Device.BoardActionedEventHandle BoardReleasedEvent;

        public bool IsAllConnected { private set; get; }
        public bool IsAllStarted { private set; get; }
        public bool IsAllStopped { private set; get; }
        public bool IsAllReleased { private set; get; }
        public System.Text.StringBuilder ErrorMessage { private set; get; }
        public ProLaminator.Device.Board BoardForSheller { get; private set; }
    }
}
