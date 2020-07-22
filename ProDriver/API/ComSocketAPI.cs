using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ComSocketAPI
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProDriver.API
 * File      Name：       ComSocketAPI
 * Creating  Time：       4/22/2020 10:44:53 AM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProDriver.API
{
    //接收数据委托（字符串型 接收数据,字节数组型 接收数据）
    public delegate void ComSocketReceivedDel(ProCommon.Communal.SocketProperty comSokectProperty, string msgStr, byte[] msgByte);

    //连接委托（整型 错误代码，字符串型 错误信息）
    public delegate void ComSocketConnectedDel(ProCommon.Communal.SocketProperty comSokectProperty, int iErrorCode, string strErrorMsg);

    //关闭委托（整型 错误代码，字符串型 错误信息）  
    public delegate void ComSocketClosedDel(ProCommon.Communal.SocketProperty comSokectProperty, int iErrorCode, string strErrorMsg);

    /// <summary>
    /// 通信Socket函数接口
    /// [注:异步]
    /// </summary>  
    public class SocketAsyncAPI
    {
        public SocketAsyncAPI(ProCommon.Communal.SocketProperty comSokectProperty)
        {
            ReceivedData = new StringBuilder();
            _dataBuffer = new byte[1024 * 1024 * 10]; //预设接收字节10MB
            SktProperty = comSokectProperty;
            ConnectedEvt = new ComSocketConnectedDel(OnConnected);
            ReceivedEvt = new ComSocketReceivedDel(OnReceived);
            ClosedEvt = new ComSocketClosedDel(OnClosed);
        }

        #region ComSocket的事件回调函数
        void OnClosed(ProCommon.Communal.SocketProperty comSokectProperty, int iErrorCode, string strErrorMsg)
        {

        }

        void OnReceived(ProCommon.Communal.SocketProperty comSokectProperty, string msgStr, byte[] msgByte)
        {

        }

        void OnConnected(ProCommon.Communal.SocketProperty comSokectProperty, int iErrorCode, string strErrorMsg)
        {

        }

        #endregion

        public event ComSocketConnectedDel ConnectedEvt;
        public event ComSocketReceivedDel ReceivedEvt;
        public event ComSocketClosedDel ClosedEvt;

        public ProCommon.Communal.SocketProperty SktProperty { private set; get; }

        public System.Net.Sockets.Socket ISocket { private set; get; }

        /// <summary>
        /// 接收字节转换成字符串
        /// </summary>
        public System.Text.StringBuilder ReceivedData
        {
            private set;
            get;
        }

        /// <summary>
        /// 接收字节缓存
        /// </summary>
        private byte[] _dataBuffer;

        #region Socket连接
        public bool Connect()
        {
            bool rt = false;
            try
            {
                //若Socket已经打开，则关闭
                if (ISocket != null && ISocket.Connected)
                {
                    ISocket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                    System.Threading.Thread.Sleep(100);
                    ISocket.Close();
                }

                //创建Socket对象(此处以TCP/IP协议创建对象,其他协议下创建对象的方法待完善)
                ISocket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork,
                    System.Net.Sockets.SocketType.Stream, SktProperty.ProtocolType);
                //定义服务器连接套接字
                System.Net.IPEndPoint iPEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(SktProperty.IP), SktProperty.Port);

                //非阻止模式连接到服务器端
                ISocket.Blocking = false;
                System.AsyncCallback OnConnected = new System.AsyncCallback(ConnectedCallBack);

                //异步请求连接到服务器端，当完成连接后回调:OnConnected委托
                ISocket.BeginConnect(iPEndPoint, OnConnected, ISocket);
                rt = true;
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
            return rt;
        }

        private void ConnectedCallBack(IAsyncResult ar)
        {
            int errorCode = 0;
            string errorMsg = "连接成功";
            System.Net.Sockets.Socket socket = (System.Net.Sockets.Socket)ar.AsyncState;

            // Check if socket was connected
            try
            {
                if (socket != null && socket.Connected)
                {
                    //结束异步连接请求（避免内存泄漏)
                    socket.EndConnect(ar);

                    errorMsg = string.Format("服务器:[{0}]{1}", socket.RemoteEndPoint.ToString(), errorMsg);
                    SktProperty.IsConnected = true; //改变连接属性
                }
                else
                {
                    errorCode = 1; errorMsg = "服务器:[" + SktProperty.IP + "]未连接";
                    SktProperty.IsConnected = false; //改变连接属性
                }
            }
            catch (Exception ex)
            {
                Close();
                SktProperty.IsConnected = false; //改变连接属性
                errorCode = 1; errorMsg = "服务器:[" + SktProperty.IP + "]连接失败";
                if (socket != null && ConnectedEvt != null)
                {
                    System.Windows.Forms.Control target = ConnectedEvt.Target as System.Windows.Forms.Control;
                    if (target != null && target.InvokeRequired)
                        //非创建控件线程同步调用事件：SocketConnected
                        target.Invoke(ConnectedEvt, new object[] { errorCode, errorMsg });
                    else
                        //创建控件线程调用事件
                        ConnectedEvt(SktProperty, errorCode, errorMsg);
                }
            }
            finally { }
        }
        #endregion

        #region Socket接收
        public bool Receive()
        {
            bool rt = false;
            try
            {
                //若Socket已经打开
                if (ISocket != null && ISocket.Connected)
                {
                    ISocket.Blocking = false;
                    AsyncCallback OnReceived = new AsyncCallback(ReceivedCallBack);
                    //异步请求接收服务器数据，完成后回调：OnReceived委托
                    ISocket.BeginReceive(_dataBuffer, 0, _dataBuffer.Length, System.Net.Sockets.SocketFlags.None, OnReceived, ISocket);
                    rt = true;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
            return rt;
        }

        private void ReceivedCallBack(IAsyncResult ar)
        {
            int errorCode = 0;
            string errorMsg = "接收成功";
            System.Net.Sockets.Socket socket = (System.Net.Sockets.Socket)ar.AsyncState;

            // Check if socket was connected           
            if (socket != null && socket.Connected)
            {
                try
                {
                    //结束异步接收请求（避免内存泄漏)
                    int nBytesRec = socket.EndReceive(ar);

                    #region 接收到数据
                    if (nBytesRec > 0)
                    {
                        errorMsg = string.Format("服务器:[{0}]{1}", socket.RemoteEndPoint, errorMsg);

                        ReceivedData.Clear();
                        string strtemp = Encoding.Default.GetString(_dataBuffer, 0, nBytesRec);
                        ReceivedData.Append(strtemp);

                        if (ReceivedEvt != null)
                        {
                            System.Windows.Forms.Control target = ReceivedEvt.Target as System.Windows.Forms.Control;
                            if (target != null && target.InvokeRequired)
                                //非创建控件线程同步调用事件：SocketConnected
                                target.Invoke(ReceivedEvt, new object[] { ReceivedData.ToString(),
                                    ProCommon.Communal.ToolFunctions.GetSubData(_dataBuffer, 0, nBytesRec) });
                            else
                                //创建控件线程调用事件
                                ReceivedEvt(SktProperty, ReceivedData.ToString(),
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
                                ClosedEvt(SktProperty, errorCode, errorMsg);
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
                                ClosedEvt(SktProperty, errorCode, errorMsg);
                        }
                    }
                    catch { }
                }
            }
            else
            {
                SktProperty.IsConnected = false; //改变连接属性
            }
        }

        #endregion

        #region Socket发送
        public bool Send(string strcmd)
        {
            bool rt = false;
            int errorCode = 0; string errorMsg = "发送成功";
            try
            {
                if ((ISocket == null) || (!ISocket.Connected))
                {
                    errorCode = 1;
                    if (ISocket == null)
                    {
                        errorMsg = "Socket对象为空";
                        errorMsg = string.Format("服务器:[{0}]{1}", ISocket.RemoteEndPoint, errorMsg);
                    }
                    if (!ISocket.Connected)
                    {
                        errorMsg = "Socket对象未连接";
                        errorMsg = string.Format("服务器:[{0}]{1}", SktProperty.IP + ":" + SktProperty.Port, errorMsg);
                    }
                    return rt;
                }
                // Convert to byte array and send.   
                byte[] cmdBuffer = Encoding.Default.GetBytes(strcmd);
                int rint = ISocket.Send(cmdBuffer, cmdBuffer.Length, System.Net.Sockets.SocketFlags.None);
                rt = true;
            }
            catch (Exception ex)
            {
                Close();
                SktProperty.IsConnected = false; //改变连接属性
                if (ClosedEvt != null)
                {
                    System.Windows.Forms.Control target = ClosedEvt.Target as System.Windows.Forms.Control;
                    errorCode = 1;
                    errorMsg = string.Format("服务器[{0}]{1}", ISocket.RemoteEndPoint, ex.Message);
                    if (target != null && target.InvokeRequired)
                        //非创建控件线程同步调用事件：SocketClosed
                        target.Invoke(ClosedEvt, new object[] { errorCode, errorMsg });
                    else
                        //创建控件线程调用事件
                        ClosedEvt(SktProperty, errorCode, errorMsg);
                }
            }
            finally
            {
            }
            return rt;
        }

        public bool Send(byte[] data)
        {
            bool rt = false;
            int errorCode = 0; string errorMsg = "发送成功";

            try
            {

                if ((ISocket == null) || (!ISocket.Connected))
                {
                    errorCode = 1;
                    if (ISocket == null)
                    {
                        errorMsg = "Socket对象为空";
                        errorMsg = string.Format("服务器:[{0}]{1}", ISocket.RemoteEndPoint, errorMsg);
                    }
                    if (!ISocket.Connected)
                    {
                        errorMsg = "Socket对象未连接";
                        errorMsg = string.Format("服务器:[{0}]{1}", SktProperty.IP + ":" + SktProperty.Port, errorMsg);
                    }
                    return rt;
                }

                int rint = ISocket.Send(data, data.Length, System.Net.Sockets.SocketFlags.None);
                rt = true;
            }
            catch (Exception ex)
            {
                Close();
                SktProperty.IsConnected = false; //改变连接属性

                errorCode = 1;
                errorMsg = string.Format("服务器[{0}]{1}", ISocket.RemoteEndPoint, ex.Message);
                if (ClosedEvt != null)
                {
                    System.Windows.Forms.Control target = ClosedEvt.Target as System.Windows.Forms.Control;
                    errorCode = 1;
                    errorMsg = string.Format("服务器[{0}]{1}", ISocket.RemoteEndPoint, ex.Message);
                    if (target != null && target.InvokeRequired)
                        //非创建控件线程同步调用事件：SocketClosed
                        target.Invoke(ClosedEvt, new object[] { errorCode, errorMsg });
                    else
                        //创建控件线程调用事件
                        ClosedEvt(SktProperty, errorCode, errorMsg);
                }
            }
            finally
            {

            }
            return rt;
        }
        #endregion

        #region Socket关闭
        public bool Close()
        {
            bool rt = false;
            int errorCode = 0; string errorMsg = "关闭成功";
            try
            {
                //若Socket已经打开，则关闭
                if (ISocket != null && ISocket.Connected)
                {
                    ISocket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                    System.Threading.Thread.Sleep(100);
                    ISocket.Close();
                }
                rt = true;
            }
            catch (Exception ex)
            {
                errorCode = 1;
                errorMsg = string.Format("服务器[{0}]{1}", ISocket.RemoteEndPoint, ex.Message);
                if (ClosedEvt != null)
                {
                    System.Windows.Forms.Control target = ClosedEvt.Target as System.Windows.Forms.Control;
                    errorCode = 1;
                    errorMsg = string.Format("服务器[{0}]{1}", ISocket.RemoteEndPoint, errorMsg);
                    if (target != null && target.InvokeRequired)
                        //非创建控件线程同步调用事件：SocketClosed
                        target.Invoke(ClosedEvt, new object[] { errorCode, errorMsg });
                    else
                        //创建控件线程调用事件
                        ClosedEvt(SktProperty, errorCode, errorMsg);
                }
                ISocket.Dispose();
                ISocket = null;
            }
            finally { }
            return rt;
        }
        #endregion
    }

    /// <summary>
    /// 通信Socket函数接口
    /// [注:同步]
    /// </summary>
    public class SocketSyncAPI
    {

    }
}
