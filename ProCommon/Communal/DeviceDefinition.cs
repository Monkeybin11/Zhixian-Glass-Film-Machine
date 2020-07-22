using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       DeviceProperty
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProCommon.Communal
 * File      Name：       DeviceDefinition
 * Creating  Time：       4/21/2020 1:16:26 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProCommon.Communal
{
    /// <summary>
    /// 设备类别
    /// </summary>
    public enum DeviceCategory : uint
    {
        None = 0x0000,
        PC = 0x0001,
        Board = 0x0002,
        PLC = 0x0003,
        Camera = 0x0004,
        Socket = 0x0005,
        Web = 0x0006,
        SerialPort = 0x0007
    }

    /// <summary>
    /// 控制器品牌
    /// </summary>
    public enum DeviceBrand : uint
    {
        None = 0x0000,
        Microsoft = 0x0001,
        LeadShine = 0x0002,
        ZMotion = 0x0003,
        HikVision = 0x0004,
        Dalsa = 0x0005,
        Baumer = 0x0006,
        Computar = 0x0007,
        Imaging = 0x0008,
        Mitsubishi = 0x0009,
        Panasonic = 0x000A,
        Delta = 0x000B,
        MindVision = 0x000C,
        Halcon = 0x000D,
        ICPDAS = 0x000E,
        Basler = 0x000F,
        DaHua = 0x0010,
        HZZH = 0x0011
    }

    /// <summary>
    /// 设备管理器通用功能接口
    /// </summary>
    public interface IDeviceManager
    {
        /// <summary>
        /// 函数：初始化
        /// </summary>
        bool Init();

        /// <summary>
        /// 函数：启动
        /// </summary>
        bool Start();

        /// <summary>
        /// 函数：停止
        /// </summary>
        bool Stop();

        /// <summary>
        /// 函数：释放
        /// </summary>
        bool Release();
    }

    /// <summary>
    /// 相机采集图像模式
    /// </summary>
    public enum AcquisitionMode : uint
    {
        Continue = 0,
        SoftTrigger = 1,
        ExternalTrigger = 2
    }
   

    /// <summary>
    /// 设备管理对象
    /// </summary>
    public abstract class DeviceManager : ProCommon.Communal.IDeviceManager
    {
        #region 无需序列化的部分

        protected bool _IsChinese;

        //是否在显示提示对话框
        protected bool _IsShowing;      

        //系统日志文件路径
        private string _systemLogFilePath;
        public string SystemLogFilePath
        {
            protected set { _systemLogFilePath = value; }
            get { return _systemLogFilePath; }
        }

        //异常日志文件路径
        private string _exceptionLogFilePath;
        public string ExceptionLogFilePath
        {
            protected set { _exceptionLogFilePath = value; }
            get { return _exceptionLogFilePath; }
        }

        protected DeviceManager()
        {
            _IsShowing = false;
        }

        public DeviceManager(string sysLogFilePath, string exLogFilePath) : this()
        {
            _systemLogFilePath = sysLogFilePath;
            _exceptionLogFilePath = exLogFilePath;
        }

        #region 抽象成员(钩子函数)
        protected abstract bool DoInit();
        protected abstract bool DoStart();
        protected abstract bool DoStop();
        protected abstract bool DoRelease();
        #endregion

        #region 实现IDevice接口       

        /// <summary>
        /// 方法：初始化
        /// </summary>
        public bool Init() { return DoInit(); }


        /// <summary>
        /// 方法：开启
        /// </summary>
        public bool Start() { return DoStart(); }

        /// <summary>
        /// 方法：停止
        /// </summary>
        public bool Stop() { return DoStop(); }

        /// <summary>
        /// 方法：释放
        /// </summary>
        public bool Release() { return DoRelease(); }

        #endregion

        #region 覆写并抽象化Object类的ToString()
        public abstract override string ToString();
        #endregion

        #endregion
    }

    /// <summary>
    /// 设备对象通用属性
    /// </summary>
    [Serializable]
    public abstract class DeviceProperty : System.ComponentModel.INotifyPropertyChanged
    {
        public virtual event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(name));
            }
        }

        #region 需要序列化的部分

        /// <summary>
        /// 属性:设备的类别
        /// </summary>
        public DeviceCategory Category
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:设备的品牌
        /// </summary>
        public DeviceBrand Brand
        {
            set;
            get;
        }

        /// <summary>
        /// 属性:设备的编号     
        /// </summary>      
        public int Number
        {
            set; get;
        }

        /// <summary>
        /// 属性：设备的ID
        /// 唯一标识控制器的类别,品牌,类型及编号
        /// [格式:类别-品牌-类型-名称(含编号)]
        /// [注:同品牌同类型的控制器的编号不允许相同]
        /// </summary>   
        public string ID { set; get; }

        /// <summary>
        /// 所属工位名称
        /// </summary>
        public string StationName { set; get; }

        /// <summary>
        /// 属性：设备的名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 属性：设备重链间隔(毫秒)
        /// </summary>
        public int ReconnectInterval { set; get; }

        /// <summary>
        /// 设备对象是否激活
        /// </summary>
        public bool IsActive { set; get; }

        /// <summary>
        /// 启用算法标记
        /// [注:在调试算法参数窗口,允许在调试模式下选择是否启用图像处理算法
        /// true--启用算法,false--不启用算法]
        /// </summary>
        public bool EnableAlgorithm { set; get; }

        /// <summary>
        /// 是否中文标记
        /// [注:在中英文之间切换]
        /// </summary>
        public bool IsChinese { set; get; }

        #endregion

        #region 不需要序列化的部分

        /// <summary>
        /// 属性:相机是否连接
        /// </summary>
        private bool _isConnected;
        [System.Xml.Serialization.XmlIgnore]
        public bool IsConnected
        {
            set
            {
                //不能直接赋值，否则一直触发属性值事件(虽然未改变)
                if (_isConnected != value)
                {
                    this._isConnected = value;
                    //调用方法：通知属性值改变
                    this.NotifyPropertyChanged("IsConnected");
                }
            }
            get
            {
                return _isConnected;
            }
        }

        #endregion
    }

    #region 相机设备属性

    /// <summary>
    /// 相机属性
    /// </summary>
    [Serializable]
    public class CameraProperty : ProCommon.Communal.DeviceProperty
    {
        private CameraProperty()
        {
            this.Category = ProCommon.Communal.DeviceCategory.Camera;
        }

        /// <summary>
        /// 实例化相机属性
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="number"></param>
        /// <param name="id"></param>
        public CameraProperty(ProCommon.Communal.DeviceBrand brand, int number, string id) : this()
        {
            this.Brand = brand;
            this.Number = number;
            this.ID = id;
        }

        /// <summary>
        /// 属性：相机序列号
        /// </summary>
        public string SerialNo { set; get; }

        /// <summary>
        /// 属性：相机的视频格式
        /// </summary>
        public string VideoFormat { set; get; }

        /// <summary>
        /// 属性：相机帧率(Frame Per Second)
        /// </summary>
        public float FPS { set; get; }

        /// <summary>
        /// 属性：曝光时间(毫秒)
        /// </summary>
        public float ExposureTime { set; get; }

        /// <summary>
        /// 属性：增益
        /// </summary>
        public float Gain { set; get; }

        /// <summary>
        /// 外触发延时
        /// [时间单位:毫秒]
        /// </summary>
        public float TriggerDelay { set; get; }

        /// <summary>
        /// 外触发消抖时间
        /// [时间单位:]
        /// </summary>
        public float DebouncerTime { set; get; }

        /// <summary>
        /// 相机Gamma
        /// </summary>
        public float Gamma { set; get; }
    }

    /// <summary>
    /// 相机属性列表
    /// </summary>
    [Serializable]
    public class CameraPropertyList : System.Collections.ICollection
    {
        private System.Collections.SortedList _list;

        public CameraPropertyList()
        {
            _list = new System.Collections.SortedList();
        }

        public void Add(ProCommon.Communal.CameraProperty camProperty)
        {
            if (!_list.ContainsKey(camProperty.ID))
                _list.Add(camProperty.ID, camProperty);
        }

        public void Delelet(ProCommon.Communal.CameraProperty camProperty)
        {
            if (_list.ContainsKey(camProperty.ID))
                _list.Remove(camProperty.ID);
        }

        public void Clear()
        {
            if (_list != null)
                _list.Clear();
        }

        public ProCommon.Communal.CameraProperty this[int indx]
        {
            get
            {
                ProCommon.Communal.CameraProperty cam = null;
                if (_list.Count > 0
                   && indx >= 0
                   && indx < _list.Count)
                    cam = (ProCommon.Communal.CameraProperty)_list.GetByIndex(indx);
                return cam;
            }
        }

        public ProCommon.Communal.CameraProperty this[string camPropertyID]
        {
            get
            {
                ProCommon.Communal.CameraProperty cam = null;
                if (_list.ContainsKey(camPropertyID))
                    cam = (ProCommon.Communal.CameraProperty)_list[camPropertyID];
                return cam;
            }
        }

        public void CopyTo(Array arr, int indx)
        {
            _list.CopyTo(arr, indx);
        }

        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return this; }

        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }

    #endregion

    #region 控制卡设备属性

    /// <summary>
    /// 控制卡属性
    /// </summary>
    [Serializable]
    public class BoardProperty : ProCommon.Communal.DeviceProperty
    {
        private BoardProperty()
        {
            this.Category = ProCommon.Communal.DeviceCategory.Board;
            this._axisList = new AxisList();
            this._inVarObjList = new InVarObjList();
            this._outVarObjList = new OutVarObjList();
        }

        public BoardProperty(ProCommon.Communal.DeviceBrand brand, int number, string id) : this()
        {
            this.Brand = brand;
            this.Number = number;
            this.ID = id;
        }       
      
        /// <summary>
        /// 网口控制卡的IP地址
        /// </summary>
        public string EtherNetIP { set; get; }

        /// <summary>
        /// 网口控制卡的端口号
        /// </summary>
        public int EtherNetPort { set; get; }

        /// <summary>
        /// 控制卡错误代码
        /// [注:报警信号组]
        /// </summary>
        public int[] ErrorCode { set; get; }

        /// <summary>
        /// 控制卡错误等级
        /// [注:报警信号等级]
        /// </summary>
        public int ErrorLevel { set; get; }

        /// <summary>
        /// 设备状态
        /// [注:设备的运行状态记录在控制器内]
        /// </summary>
        public int MachineStatus { set; get; }

        private AxisList _axisList;
        /// <summary>
        /// 属性：轴列表实体(用于实体删减+查询)
        /// </summary>
        public AxisList AxisList
        {
            set { _axisList = value; }
            get { return _axisList; }
        }

        private System.ComponentModel.BindingList<Axis> _axisBList;

        /// <summary>
        /// 属性：轴实体的列表(用于数据绑定+查询)
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public System.ComponentModel.BindingList<Axis> AxisBList
        {
            get
            {
                if (_axisBList == null)
                    _axisBList = new System.ComponentModel.BindingList<Axis>();
                _axisBList.Clear();
              
                for (int i = 0; i <_axisList.Count; i++)
                {
                    _axisBList.Add(_axisList[i]);
                }
                return _axisBList;
            }
        }

        private InVarObjList _inVarObjList;
        /// <summary>
        /// 属性：控制变量列表实体（用于实体删减+查询）
        /// </summary>
        public InVarObjList InVarObjList
        {
            set { _inVarObjList = value; }
            get { return _inVarObjList; }
        }

        private System.ComponentModel.BindingList<InVarObj> _inVarOjBList;
        /// <summary>
        /// 属性：控制变量实体的列表(用于数据绑定+查询)
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public System.ComponentModel.BindingList<InVarObj> InVarObjBList
        {
            get
            {
                if (_inVarOjBList == null)
                    _inVarOjBList = new System.ComponentModel.BindingList<InVarObj>();
                _inVarOjBList.Clear();

                for (int i = 0; i <_inVarObjList.Count; i++)
                {
                    _inVarOjBList.Add(_inVarObjList[i]);
                }

                return _inVarOjBList;
            }
        }

        private OutVarObjList _outVarObjList;
        /// <summary>
        /// 属性：控制变量列表实体（用于实体删减+查询）
        /// </summary>
        public OutVarObjList OutVarObjList
        {
            set { _outVarObjList = value; }
            get { return _outVarObjList; }
        }

        private System.ComponentModel.BindingList<OutVarObj> _outVarObjBList;
        /// <summary>
        /// 属性：控制变量实体的列表(用于数据绑定+查询)
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public System.ComponentModel.BindingList<OutVarObj> OutVarObjBList
        {
            get
            {
                if (_outVarObjBList == null)
                    _outVarObjBList = new System.ComponentModel.BindingList<OutVarObj>();
                _outVarObjBList.Clear();

                for (int i = 0; i <_outVarObjList.Count; i++)
                {
                    _outVarObjBList.Add(_outVarObjList[i]);
                }

                return _outVarObjBList;
            }
        }

    }

    /// <summary>
    /// 控制卡属性列表
    /// </summary>
    [Serializable]
    public class BoardPropertyList : System.Collections.ICollection
    {
        private System.Collections.SortedList _list;

        public BoardPropertyList()
        {
            _list = new System.Collections.SortedList();
        }

        public void Add(ProCommon.Communal.BoardProperty brdProperty)
        {
            if (!_list.ContainsKey(brdProperty.ID))
                _list.Add(brdProperty.ID, brdProperty);
        }

        public void Delelet(ProCommon.Communal.BoardProperty brdProperty)
        {
            if (_list.ContainsKey(brdProperty.ID))
                _list.Remove(brdProperty.ID);
        }

        public void Clear()
        {
            if (_list != null)
                _list.Clear();
        }

        public void CopyTo(Array arr, int indx)
        {
            _list.CopyTo(arr, indx);
        }

        public ProCommon.Communal.BoardProperty this[int indx]
        {
            get
            {
                ProCommon.Communal.BoardProperty brd = null;
                if (_list.Count > 0
                   && indx >= 0
                   && indx < _list.Count)
                    brd = (ProCommon.Communal.BoardProperty)_list.GetByIndex(indx);
                return brd;
            }
        }

        public ProCommon.Communal.BoardProperty this[string brdPropertyID]
        {
            get
            {
                ProCommon.Communal.BoardProperty brd = null;
                if (_list.ContainsKey(brdPropertyID))
                    brd = (ProCommon.Communal.BoardProperty)_list[brdPropertyID];
                return brd;
            }
        }

        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return this; }

        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }

    #endregion

    #region 串口设备属性
    /// <summary>
    /// 串口设备属性
    /// </summary>
    [Serializable]
    public class SerialPortProperty:ProCommon.Communal.DeviceProperty
    {
        private SerialPortProperty()
        {
            this.Category = ProCommon.Communal.DeviceCategory.SerialPort;
        }

        /// <summary>
        /// 实例化串口设备属性
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="number"></param>
        /// <param name="id"></param>
        public SerialPortProperty(ProCommon.Communal.DeviceBrand brand, int number, string id) : this()
        {
            this.Brand = ProCommon.Communal.DeviceBrand.Microsoft;
            this.Number = number;
            this.ID = id;
        }

        /// <summary>
        /// 属性：发送超时(毫秒)
        /// </summary>
        public int SendTimeOut
        {
            set;
            get;
        }

        /// <summary>
        /// 属性：接收超时(毫秒)
        /// </summary>
        public int ReceiveTimeOut
        {
            set;
            get;
        }

        /// <summary>
        /// 握手协议
        /// </summary>
        public System.IO.Ports.Handshake HandShake { set; get; }

        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate { set; get; }

        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits { set; get; }

        /// <summary>
        /// 停止位
        /// </summary>
        public System.IO.Ports.StopBits StopBits { set; get; }

        /// <summary>
        /// 校验
        /// </summary>
        public System.IO.Ports.Parity Parity { set; get; }

        /// <summary>
        /// 换行符
        /// </summary>
        public string NewLine { get; set; }

        /// <summary>
        /// 接收字节数阈值
        /// [接收到阈值指定字节数，触发接收到字节事件]
        /// </summary>
        public int ReceivedBytesThreshold { set; get; }

        public bool DtrEnable { get; set; }
        public bool RtsEnable { get; set; }
    }

    /// <summary>
    /// 串口设备属性列表
    /// </summary>
    [Serializable]
    public class SerialPortPropertyList : System.Collections.ICollection
    {
        private System.Collections.SortedList _list;
        public SerialPortPropertyList() { _list = new System.Collections.SortedList(); }

        public void Add(ProCommon.Communal.SerialPortProperty serialPortProperty)
        {
            if (!_list.ContainsKey(serialPortProperty.ID))
                _list.Add(serialPortProperty.ID, serialPortProperty);
        }

        public void Delelet(ProCommon.Communal.SerialPortProperty serialPortProperty)
        {
            if (_list.ContainsKey(serialPortProperty.ID))
                _list.Remove(serialPortProperty.ID);
        }

        public void Clear()
        {
            if (_list != null)
                _list.Clear();
        }

        public ProCommon.Communal.SerialPortProperty this[int indx]
        {
            get
            {
                ProCommon.Communal.SerialPortProperty comSerialPort = null;
                if (_list.Count > 0
                   && indx >= 0
                   && indx < _list.Count)
                    comSerialPort = (ProCommon.Communal.SerialPortProperty)_list.GetByIndex(indx);
                return comSerialPort;
            }
        }

        public ProCommon.Communal.SerialPortProperty this[string serialPortPropertyID]
        {
            get
            {
                ProCommon.Communal.SerialPortProperty cam = null;
                if (_list.ContainsKey(serialPortPropertyID))
                    cam = (ProCommon.Communal.SerialPortProperty)_list[serialPortPropertyID];
                return cam;
            }
        }

        public void CopyTo(Array arr, int indx)
        {
            _list.CopyTo(arr, indx);
        }

        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return this; }

        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }

    #endregion

    #region Socket设备属性
    /// <summary>
    /// Socket设备属性
    /// </summary>
    [Serializable]
    public class SocketProperty:ProCommon.Communal.DeviceProperty
    {
        private SocketProperty()
        {
            this.Category = ProCommon.Communal.DeviceCategory.Socket;
        }

        /// <summary>
        /// 实例化Socket设备属性
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="number"></param>
        /// <param name="id"></param>
        public SocketProperty(ProCommon.Communal.DeviceBrand brand, int number, string id) : this()
        {
            this.Brand = ProCommon.Communal.DeviceBrand.Microsoft;
            this.Number = number;
            this.ID = id;
        }

        /// <summary>
        /// 属性:协议类型
        /// </summary>
        public System.Net.Sockets.ProtocolType ProtocolType
        {
            set;
            get;
        }

        public string IP
        {
            set;
            get;
        }

        public int Port
        {
            set;
            get;
        }

        /// <summary>
        /// 属性：发送超时(毫秒)
        /// </summary>
        public int SendTimeOut
        {
            set;
            get;
        }

        /// <summary>
        /// 属性：接收超时(毫秒)
        /// </summary>
        public int ReceiveTimeOut
        {
            set;
            get;
        }
    }

    /// <summary>
    /// Socket设备属性列表
    /// </summary>
    [Serializable]
    public class SocketPropertyList : System.Collections.ICollection
    {
        private System.Collections.SortedList _list;
        public SocketPropertyList() { _list = new System.Collections.SortedList(); }

        public void Add(ProCommon.Communal.SocketProperty sktProperty)
        {
            if (!_list.ContainsKey(sktProperty.ID))
                _list.Add(sktProperty.ID, sktProperty);
        }

        public void Delelet(ProCommon.Communal.SocketProperty sktProperty)
        {
            if (_list.ContainsKey(sktProperty.ID))
                _list.Remove(sktProperty.ID);
        }

        public void Clear()
        {
            if (_list != null)
                _list.Clear();
        }

        public ProCommon.Communal.SocketProperty this[int indx]
        {
            get
            {
                ProCommon.Communal.SocketProperty sktProperty = null;
                if (_list.Count > 0
                   && indx >= 0
                   && indx < _list.Count)
                    sktProperty = (ProCommon.Communal.SocketProperty)_list.GetByIndex(indx);
                return sktProperty;
            }
        }

        public ProCommon.Communal.SocketProperty this[string socketPropertyID]
        {
            get
            {
                ProCommon.Communal.SocketProperty cam = null;
                if (_list.ContainsKey(socketPropertyID))
                    cam = (ProCommon.Communal.SocketProperty)_list[socketPropertyID];
                return cam;
            }
        }

        public void CopyTo(Array arr, int indx)
        {
            _list.CopyTo(arr, indx);
        }

        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return this; }

        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }

    #endregion

    #region Web设备属性

    /// <summary>
    /// Web设备属性
    /// </summary>
    [Serializable]
    public class WebProperty:ProCommon.Communal.DeviceProperty
    {
        private WebProperty()
        {
            this.Category = ProCommon.Communal.DeviceCategory.Web;
        }

        /// <summary>
        /// 实例化Web设备属性
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="number"></param>
        /// <param name="id"></param>
        public WebProperty(ProCommon.Communal.DeviceBrand brand, int number, string id) : this()
        {
            this.Brand = ProCommon.Communal.DeviceBrand.Microsoft;
            this.Number = number;
            this.ID = id;
        }

        /// <summary>
        /// 属性：输出库文件路径
        /// </summary>
        public string OutDllFileName
        {
            set;
            get;
        }

        /// <summary>
        /// 属性：代理类名称
        /// </summary>
        public string ProxyClassName
        {
            set;
            get;
        }

        /// <summary>
        /// 属性：服务链接
        /// </summary>
        public string URL
        {
            set;
            get;
        }
    }

    /// <summary>
    /// Web设备属性列表
    /// </summary>
    [Serializable]
    public class WebPropertyList : System.Collections.ICollection
    {
        private System.Collections.SortedList _list;
        public WebPropertyList() { _list = new System.Collections.SortedList(); }

        public void Add(ProCommon.Communal.WebProperty webProperty)
        {
            if (!_list.ContainsKey(webProperty.ID))
                _list.Add(webProperty.ID, webProperty);
        }

        public void Delelet(ProCommon.Communal.WebProperty webProperty)
        {
            if (_list.ContainsKey(webProperty.ID))
                _list.Remove(webProperty.ID);
        }

        public void Clear()
        {
            if (_list != null)
                _list.Clear();
        }

        public ProCommon.Communal.WebProperty this[int indx]
        {
            get
            {
                ProCommon.Communal.WebProperty webProperty = null;
                if (_list.Count > 0
                   && indx >= 0
                   && indx < _list.Count)
                    webProperty = (ProCommon.Communal.WebProperty)_list.GetByIndex(indx);
                return webProperty;
            }
        }

        public ProCommon.Communal.WebProperty this[string webPropertyID]
        {
            get
            {
                ProCommon.Communal.WebProperty cam = null;
                if (_list.ContainsKey(webPropertyID))
                    cam = (ProCommon.Communal.WebProperty)_list[webPropertyID];
                return cam;
            }
        }

        public void CopyTo(Array arr, int indx)
        {
            _list.CopyTo(arr, indx);
        }

        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return this; }

        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
    #endregion

    #region PLC设备属性

    /// <summary>
    /// PLC属性
    /// </summary>
    [Serializable]
    public class PlcProperty : ProCommon.Communal.DeviceProperty
    {
        private PlcProperty()
        {
            this.Category = ProCommon.Communal.DeviceCategory.PLC;
        }

        /// <summary>
        /// 实例化PLC属性
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="number"></param>
        /// <param name="id"></param>
        public PlcProperty(ProCommon.Communal.DeviceBrand brand, int number, string id) : this()
        {
            this.Brand = brand;
            this.Number = number;
            this.ID = id;
        }

        private InVarObjList _inVarObjList;
        /// <summary>
        /// 属性：控制变量列表实体（用于实体删减+查询）
        /// </summary>
        public InVarObjList InVarObjList
        {
            set { _inVarObjList = value; }
            get { return _inVarObjList; }
        }

        private System.ComponentModel.BindingList<InVarObj> _inVarOjBList;
        /// <summary>
        /// 属性：控制变量实体的列表(用于数据绑定+查询)
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public System.ComponentModel.BindingList<InVarObj> InVarObjBList
        {
            get
            {
                if (_inVarOjBList == null)
                    _inVarOjBList = new System.ComponentModel.BindingList<InVarObj>();
                _inVarOjBList.Clear();

                for (int i = 0; i < _inVarObjList.Count; i++)
                {
                    _inVarOjBList.Add(_inVarObjList[i]);
                }

                return _inVarOjBList;
            }
        }

        private OutVarObjList _outVarObjList;
        /// <summary>
        /// 属性：控制变量列表实体（用于实体删减+查询）
        /// </summary>
        public OutVarObjList OutVarObjList
        {
            set { _outVarObjList = value; }
            get { return _outVarObjList; }
        }

        private System.ComponentModel.BindingList<OutVarObj> _outVarObjBList;
        /// <summary>
        /// 属性：控制变量实体的列表(用于数据绑定+查询)
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public System.ComponentModel.BindingList<OutVarObj> OutVarObjBList
        {
            get
            {
                if (_outVarObjBList == null)
                    _outVarObjBList = new System.ComponentModel.BindingList<OutVarObj>();
                _outVarObjBList.Clear();

                for (int i = 0; i < _outVarObjList.Count; i++)
                {
                    _outVarObjBList.Add(_outVarObjList[i]);
                }

                return _outVarObjBList;
            }
        }
    }

    /// <summary>
    /// PLC属性列表
    /// </summary>
    [Serializable]
    public class PlcPropertyList : System.Collections.ICollection
    {
        private System.Collections.SortedList _list;

        public PlcPropertyList()
        {
            _list = new System.Collections.SortedList();
        }

        public void Add(ProCommon.Communal.PlcProperty plcProperty)
        {
            if (!_list.ContainsKey(plcProperty.ID))
                _list.Add(plcProperty.ID, plcProperty);
        }

        public void Delelet(ProCommon.Communal.PlcProperty plcProperty)
        {
            if (_list.ContainsKey(plcProperty.ID))
                _list.Remove(plcProperty.ID);
        }

        public void Clear()
        {
            if (_list != null)
                _list.Clear();
        }

        public void CopyTo(Array arr, int indx)
        {
            _list.CopyTo(arr, indx);
        }

        public ProCommon.Communal.PlcProperty this[int indx]
        {
            get
            {
                ProCommon.Communal.PlcProperty plc = null;
                if (_list.Count > 0
                   && indx >= 0
                   && indx < _list.Count)
                    plc = (ProCommon.Communal.PlcProperty)_list.GetByIndex(indx);
                return plc;
            }
        }

        public ProCommon.Communal.PlcProperty this[string plcPropertyID]
        {
            get
            {
                ProCommon.Communal.PlcProperty brd = null;
                if (_list.ContainsKey(plcPropertyID))
                    brd = (ProCommon.Communal.PlcProperty)_list[plcPropertyID];
                return brd;
            }
        }

        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return this; }

        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }

    #endregion
}
