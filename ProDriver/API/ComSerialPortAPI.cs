using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ComSerialPortAPI
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProDriver.API
 * File      Name：       ComSerialPortAPI
 * Creating  Time：       4/22/2020 11:01:11 AM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProDriver.API
{
    //接收数据委托（字符串型 接收数据,字节数组型 接收数据）
    public delegate void ComSerialPortReceivedDel(ProCommon.Communal.SerialPortProperty comSerialPortProperty, string msgStr, byte[] msgByte);

    //连接委托（整型 错误代码，字符串型 错误信息）
    public delegate void ComSerialPortConnectedDel(ProCommon.Communal.SerialPortProperty comSerialPortProperty, int iErrorCode, string strErrorMsg);

    //关闭委托（整型 错误代码，字符串型 错误信息）  
    public delegate void ComSerialPortClosedDel(ProCommon.Communal.SerialPortProperty comSerialPortProperty, int iErrorCode, string strErrorMsg);

    /// <summary>
    /// SerialPort异步操作接口函数
    /// [注:待完善_2020-03-03]
    /// </summary>
    public class SerialAsyncAPI
    {
        public SerialAsyncAPI(ProCommon.Communal.SerialPortProperty comSerialPortProperty)
        {
            ReceivedData = new StringBuilder();
            ComSerialPort = comSerialPortProperty;
            ConnectedEvt = new ComSerialPortConnectedDel(OnConnected);
            ReceivedEvt = new ComSerialPortReceivedDel(OnReceived);
            ClosedEvt = new ComSerialPortClosedDel(OnClosed);
        }

        #region ComSerialPort的事件回调函数
        void OnClosed(ProCommon.Communal.SerialPortProperty comSerialPortProperty, int iErrorCode, string strErrorMsg)
        {

        }

        void OnReceived(ProCommon.Communal.SerialPortProperty comSerialPortProperty, string msgStr, byte[] msgByte)
        {

        }

        void OnConnected(ProCommon.Communal.SerialPortProperty comSerialPortProperty, int iErrorCode, string strErrorMsg)
        {

        }

        #endregion

        public event ComSerialPortConnectedDel ConnectedEvt;
        public event ComSerialPortReceivedDel ReceivedEvt;
        public event ComSerialPortClosedDel ClosedEvt;

        public ProCommon.Communal.SerialPortProperty ComSerialPort { private set; get; }
        public System.Text.StringBuilder ReceivedData
        {
            private set;
            get;
        }
    }

    /// <summary>
    /// SerialPort同步操作接口函数  
    /// </summary>
    public class SerialSyncAPI
    {
        public SerialSyncAPI(ProCommon.Communal.SerialPortProperty comSerialPortProperty)
        {
            ReceivedData = new StringBuilder();
            Property = comSerialPortProperty;
            ConnectedEvt = new ComSerialPortConnectedDel(OnConnected);
            ReceivedEvt = new ComSerialPortReceivedDel(OnReceived);
            ClosedEvt = new ComSerialPortClosedDel(OnClosed);
        }

        #region ComSerialPort的事件回调函数
        void OnClosed(ProCommon.Communal.SerialPortProperty comSerialPortProperty, int iErrorCode, string strErrorMsg)
        {

        }

        void OnReceived(ProCommon.Communal.SerialPortProperty comSerialPortProperty, string msgStr, byte[] msgByte)
        {

        }

        void OnConnected(ProCommon.Communal.SerialPortProperty comSerialPortProperty, int iErrorCode, string strErrorMsg)
        {

        }

        #endregion

        public event ComSerialPortConnectedDel ConnectedEvt;
        public event ComSerialPortReceivedDel ReceivedEvt;
        public event ComSerialPortClosedDel ClosedEvt;

        public ProCommon.Communal.SerialPortProperty Property { private set; get; }

        public System.IO.Ports.SerialPort ISerialPort { private set; get; }    

        public System.Text.StringBuilder ReceivedData
        {
            private set;
            get;
        }

        public string[] SerialPortNameList { private set; get; }

        public bool EnumerateSerialPortList()
        {
            bool rt = false;
            try
            {
                SerialPortNameList = System.IO.Ports.SerialPort.GetPortNames();
                if (SerialPortNameList != null
                    && SerialPortNameList.Length > 0)
                    rt = true;
            }
            catch (System.Exception ex)
            {

            }
            return rt;
        }

        public bool GetSerialPortByName(string protName)
        {
            bool rt = false;
            try
            {
                if (SerialPortNameList != null
                    && SerialPortNameList.Length > 0)
                {
                    for (int i = 0; i < SerialPortNameList.Length; i++)
                    {
                        if (protName == SerialPortNameList[i])
                        {
                            ISerialPort = new System.IO.Ports.SerialPort(protName);
                            rt = true; break;
                        }
                    }
                }

                if (!rt)
                {
                    if (ISerialPort != null)
                    {
                        if (ISerialPort.IsOpen)
                            ISerialPort.Close();

                        ISerialPort.Dispose();
                        ISerialPort = null;
                    }
                }
            }
            catch (System.Exception ex)
            {

            }
            return rt;
        }

        #region SerialPort连接
        public bool Connect()
        {
            bool rt = false;
            try
            {
                if (ISerialPort != null)
                {
                 
                    if (ISerialPort.IsOpen)
                    {
                        ISerialPort.DataReceived -= SerialPort_DataReceived;
                        ISerialPort.Close();
                        System.Threading.Thread.Sleep(100);
                        ISerialPort.Dispose();
                    }

                    ISerialPort.BaudRate = Property.BaudRate;
                    ISerialPort.Parity = Property.Parity;
                    ISerialPort.DataBits = Property.DataBits;
                    ISerialPort.StopBits = Property.StopBits;
                    //ISerialPort.ReceivedBytesThreshold = ComSerialPort.ReceivedBytesThreshold;
                    ISerialPort.NewLine = Property.NewLine;
                    ISerialPort.ReadTimeout = Property.ReceiveTimeOut;
                    ISerialPort.DtrEnable = Property.DtrEnable;
                    ISerialPort.RtsEnable = Property.RtsEnable;
                    ISerialPort.DataReceived += SerialPort_DataReceived;
                    System.AsyncCallback OnConnected = new System.AsyncCallback(ConnectedCallBack);

                    //当完成连接后回调:OnConnected委托              
                    ISerialPort.Open();
                    OnConnected(null);
                    rt = true;
                }
            }
            catch (Exception ex) { }
            finally { }
            return rt;
        }
        private void ConnectedCallBack(IAsyncResult ar)
        {
            int errorCode = 0;
            string errorMsg = "连接成功";

            // Check if SerialPort was connected
            try
            {
                if (ISerialPort != null && ISerialPort.IsOpen)
                {
                    errorMsg = string.Format("服务器:[{0}]{1}", ISerialPort.PortName, errorMsg);
                    Property.IsConnected = true; //改变连接属性
                }
                else
                {
                    errorCode = 1; errorMsg = "服务器:[" + ISerialPort.PortName + "]未连接";
                    Property.IsConnected = false; //改变连接属性
                }
            }
            catch (Exception ex)
            {
                errorCode = 1; errorMsg = "服务器:[" + ISerialPort.PortName + "]连接失败";
                Close();
                Property.IsConnected = false;
            }
            finally
            {
                if (ISerialPort != null && ConnectedEvt != null)
                {
                    System.Windows.Forms.Control target = ConnectedEvt.Target as System.Windows.Forms.Control;
                    if (target != null && target.InvokeRequired)
                        //非创建控件线程同步调用事件：SerialPortConnectedEvt
                        target.Invoke(ConnectedEvt, new object[] { errorCode, errorMsg });
                    else
                        //创建控件线程调用事件
                        ConnectedEvt(Property, errorCode, errorMsg);
                }
            }
        }

        /// <summary>
        /// 串口接收到数据回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            Receive();
        }
        #endregion

        #region SerialPort接收
        public bool Receive()
        {
            bool rt = false;
            try
            {               
                if (ISerialPort!=null
                    && ISerialPort.IsOpen)
                {
                    AsyncCallback OnReceived = new AsyncCallback(ReceivedCallBack);
                    //请求接收数据，完成后回调：OnReceived委托
                    OnReceived(null);
                    rt = true;
                }
            }
            catch (Exception ex) { }
            finally { }
            return rt;
        }

        private void ReceivedCallBack(IAsyncResult ar)
        {
            int errorCode = 0;
            string errorMsg = "接收成功";
            //若SerialPort已经打开
            if (ISerialPort != null && ISerialPort.IsOpen)
            {
                try
                {
                    #region 接收数据

                    int nBytesRec = ISerialPort.BytesToRead;
                    byte[] _dataBuffer = new byte[nBytesRec];
                    ISerialPort.Read(_dataBuffer, 0, nBytesRec);

                    if (nBytesRec > 0)
                    {
                        errorMsg = string.Format("串口:[{0}]{1}", ISerialPort.PortName, errorMsg);

                        ReceivedData.Clear();
                        string strtemp = Encoding.Default.GetString(_dataBuffer, 0, nBytesRec);
                        ReceivedData.Append(strtemp);

                        if (ReceivedEvt != null)
                        {
                            System.Windows.Forms.Control target = ReceivedEvt.Target as System.Windows.Forms.Control;
                            if (target != null && target.InvokeRequired)
                                //非创建控件线程同步调用事件：SerialPortConnected
                                target.Invoke(ReceivedEvt, new object[] { ReceivedData.ToString(),
                                    ProCommon.Communal.ToolFunctions.GetSubData(_dataBuffer, 0, nBytesRec) });
                            else
                                //创建控件线程调用事件
                                ReceivedEvt(Property, ReceivedData.ToString(),
                                    ProCommon.Communal.ToolFunctions.GetSubData(_dataBuffer, 0, nBytesRec));
                        }

                    }
                    else
                    {
                        errorCode = 1; errorMsg = "接收数据为空";
                        //Common.SocketClosedEventHandler d=new Common.SocketClosedEventHandler();
                        if (ClosedEvt != null)
                        {
                            System.Windows.Forms.Control target = ClosedEvt.Target as System.Windows.Forms.Control;
                            if (target != null && target.InvokeRequired)
                                //非创建控件线程同步调用事件：SocketClosed
                                target.Invoke(ClosedEvt, new object[] { errorCode, errorMsg });
                            else
                                //创建控件线程调用事件
                                ClosedEvt(Property, errorCode, errorMsg);
                        }
                    }
                    #endregion
                }
                catch
                {
                    try
                    {
                        if (ClosedEvt != null)
                        {
                            System.Windows.Forms.Control target = ClosedEvt.Target as System.Windows.Forms.Control;
                            if (target != null && target.InvokeRequired)
                                //非创建控件线程同步调用事件：SocketClosed
                                target.Invoke(ClosedEvt, new object[] { errorCode, errorMsg });
                            else
                                //创建控件线程调用事件
                                ClosedEvt(Property, errorCode, errorMsg);
                        }
                    }
                    catch { }
                }
            }
            else
            {
                Property.IsConnected = false; //改变连接属性
            }
        }

        #endregion

        #region SerialPort发送
        public bool Send(string strcmd)
        {
            bool rt = false;
            int errorCode = 0; string errorMsg = "发送成功";
            try
            {
                if (ISerialPort == null || (!ISerialPort.IsOpen))
                {
                    errorCode = 1;
                    if (ISerialPort == null)
                    {
                        errorMsg = "SerialPort对象为空";
                        errorMsg = string.Format("串口:[{0}]{1}", ISerialPort.PortName, errorMsg);
                    }
                    if (!ISerialPort.IsOpen)
                    {
                        errorMsg = "SerialPort对象未连接";
                        errorMsg = string.Format("串口:[{0}]{1}", ISerialPort.PortName, errorMsg);
                    }
                    return rt;
                }

                // Convert to byte array and send.   
                byte[] cmdBuffer = Encoding.Default.GetBytes(strcmd);
                ISerialPort.Write(cmdBuffer, 0, cmdBuffer.Length);
                rt = true;
            }
            catch (Exception ex)
            {
                Close();
                Property.IsConnected = false;
                if (ClosedEvt != null)
                {
                    System.Windows.Forms.Control target = ClosedEvt.Target as System.Windows.Forms.Control;
                    errorCode = 1;
                    errorMsg = string.Format("串口[{0}]{1}", ISerialPort.PortName, ex.Message);
                    if (target != null && target.InvokeRequired)
                        //非创建控件线程同步调用事件：SerialPortClosed
                        target.Invoke(ClosedEvt, new object[] { errorCode, errorMsg });
                    else
                        //创建控件线程调用事件
                        ClosedEvt(Property, errorCode, errorMsg);
                }
            }
            finally
            {
            }
            return rt;
        }

        public bool Send(byte[] byteArrCmd)
        {
            bool rt = false;
            int errorCode = 0; string errorMsg = "发送成功";
            try
            {
                if (ISerialPort == null || (!ISerialPort.IsOpen))
                {
                    errorCode = 1;
                    if (ISerialPort == null)
                    {
                        errorMsg = "SerialPort对象为空";
                        errorMsg = string.Format("串口:[{0}]{1}", ISerialPort.PortName, errorMsg);
                    }
                    if (!ISerialPort.IsOpen)
                    {
                        errorMsg = "SerialPort对象未连接";
                        errorMsg = string.Format("串口:[{0}]{1}", ISerialPort.PortName, errorMsg);
                    }

                    return rt;                  
                }

                ISerialPort.Write(byteArrCmd, 0, byteArrCmd.Length);
                rt = true;
            }
            catch (Exception ex)
            {
                Close();
                Property.IsConnected = false;
                if (ClosedEvt != null)
                {
                    System.Windows.Forms.Control target = ClosedEvt.Target as System.Windows.Forms.Control;
                    errorCode = 1;
                    errorMsg = string.Format("串口[{0}]{1}", ISerialPort.PortName, ex.Message);
                    if (target != null && target.InvokeRequired)
                        //非创建控件线程同步调用事件：SerialPortClosed
                        target.Invoke(ClosedEvt, new object[] { errorCode, errorMsg });
                    else
                        //创建控件线程调用事件
                        ClosedEvt(Property, errorCode, errorMsg);
                }
            }
            finally { }
            return rt;
        }
        #endregion

        #region SerialPort关闭
        public bool Close()
        {
            bool rt = false;
            int errorCode = 0; string errorMsg = "关闭成功";
            try
            {
                if (ISerialPort != null)
                {
                    if (ISerialPort.IsOpen)
                    {
                        ISerialPort.Close();
                        System.Threading.Thread.Sleep(100);
                    }
                    rt = true;
                }
            }
            catch (Exception ex)
            {
                Property.IsConnected = false;
                errorMsg = ex.Message;
            }
            finally
            {
                if (ClosedEvt != null)
                {
                    System.Windows.Forms.Control target = ClosedEvt.Target as System.Windows.Forms.Control;
                    errorCode = 1;
                    errorMsg = string.Format("串口[{0}]{1}", ISerialPort.PortName, errorMsg);
                    if (target != null && target.InvokeRequired)
                        //非创建控件线程同步调用事件：SerialPortClosed
                        target.Invoke(ClosedEvt, new object[] { errorCode, errorMsg });
                    else
                        //创建控件线程调用事件
                        ClosedEvt(Property, errorCode, errorMsg);
                }
                if (ISerialPort != null)
                {
                    ISerialPort.Dispose();
                    ISerialPort = null;
                }
            }
            return rt;
        }
        #endregion

    }
}
