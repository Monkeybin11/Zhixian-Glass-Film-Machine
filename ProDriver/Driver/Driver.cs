using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProCommon.Communal;


/*************************************************************************************
    * CLR    Version：       4.0.30319.42000
    * Class     Name：       Driver
    * Machine   Name：       LAPTOP-KFCLDVVH
    * Name     Space：       ProDriver
    * File      Name：       Driver
    * Creating  Time：       4/29/2019 11:06:18 AM
    * Author    Name：       xYz_Albert
    * Description   ：       驱动封装类
    * Modifying Time：
    * Modifier  Name：
*************************************************************************************/

namespace ProDriver.Driver
{
    public delegate void DriverExceptionOccuredDel(string err);

    public delegate void CameraImageGrabbedDel(ProCommon.Communal.CameraProperty camProperty, HalconDotNet.HObject hoImage);

    #region 相机相关
   
    /// <summary>
    /// 相机操作接口
    /// </summary>
    public interface ICameraDriver
    {
        event CameraImageGrabbedDel CameraImageGrabbedEvt;
        bool IsImageGrabbed { set; get; }
        bool EnumerateCameraList();
        int GetCameraListCount();
        bool GetCameraByIdx(int index);
        string GetCameraSN(int index);
        bool GetCameraByName(string camName);
        bool GetCameraBySN(string camSN);       
        bool Open();
        bool Close();
        bool SetAcquisitionMode(ProCommon.Communal.AcquisitionMode acqmode, uint frameNum); 
        bool SetTriggerActivation(ProCommon.Communal.EffectiveSignal edge);
        bool StartGrab();
        bool PauseGrab();
        bool StopGrab();
        bool SoftTriggerOnce();

        /// <summary>
        /// 设置曝光时间
        /// </summary>
        /// <param name="exposuretime">曝光时间,单位毫秒</param>
        /// <returns></returns>
        bool SetExposureTime(float exposuretime);
        /// <summary>
        /// 设置相机增益
        /// </summary>
        /// <param name="gain"></param>
        /// <returns></returns>
        bool SetGain(float gain);
        /// <summary>
        /// 设置相机Gamma
        /// </summary>
        /// <param name="gamma"></param>
        /// <returns></returns>
        bool SetGamma(float gamma);
        bool SetFrameRate(float fps);

        /// <summary>
        /// 设置触发延迟时间
        /// </summary>
        /// <param name="trigdelay">延时,单位毫秒</param>
        /// <returns></returns>
        bool SetTriggerDelay(int idx,float trigdelay);

        /// <summary>
        /// 设置指定端口在外触发时的消抖时间
        /// </summary>
        /// <param name="idx">端口索引</param>
        /// <param name="debouncertime">机械电子信号过滤抖动时间,单位微秒</param>
        /// <returns></returns>
        bool SetDebouncerTime(int idx,float debouncertime);

        /// <summary>
        /// 获取相机输入端口状态
        /// </summary>
        /// <param name="idx"></param>
        /// <param name="onOff">
        /// true--导通,false--断开</param>
        /// <returns></returns>
        bool GetInPut(int idx,out bool onOff);

        /// <summary>
        /// 设置相机指定端口的状态
        /// </summary>
        /// <param name="idx"></param>
        /// <param name="onOff">
        /// true--导通状态--电平状态根据相机SDK确定--电平对应数字依据相机SDK确定:
        /// false--断开状态--电平状态根据相机SDK确定--电平对应数字依据相机SDK确定:
        /// </param>
        /// <returns></returns>
        bool SetOutPut(int idx,bool onOff);

        bool CreateCameraSetPage(System.IntPtr windowHandle, string promption);
        bool ShowCameraSetPage();
        bool RegisterExceptionCallBack();
        bool RegisterImageGrabbedCallBack();
        bool GetCameraConnectedState(out bool isConnected);
    }

    /// <summary>
    /// 相机操作类
    /// </summary>
    public abstract class CameraDriver : ICameraDriver
    {
        public abstract event CameraImageGrabbedDel CameraImageGrabbedEvt;
        public DriverExceptionOccuredDel DriverExceptionDel;
        public HalconDotNet.HObject HoImage;    
        protected CameraDriver(ProCommon.Communal.CameraProperty camProperty)
        {
            if(HoImage!=null
                && HoImage.IsInitialized())
                HoImage.Dispose();
            HalconDotNet.HOperatorSet.GenEmptyObj(out HoImage);

            DriverExceptionDel = new DriverExceptionOccuredDel(OnDriverExceptionOccured);
            this.CameraProperty = camProperty;         
        }

        private void OnDriverExceptionOccured(string err)
        {
            //什么都不做
        }

        #region 抽象成员(钩子函数)
        protected abstract bool DoEnumerateCameraList();
        protected abstract int DoGetCameraListCount();
        protected abstract bool DoGetCameraByIdx(int index);
        protected abstract string DoGetCameraSN(int index);
        protected abstract bool DoGetCameraByName(string camName);
        protected abstract bool DoGetCameraBySN(string camSN);
        protected abstract bool DoOpen();
        protected abstract bool DoClose();
        protected abstract bool DoSetAcquisitionMode(ProCommon.Communal.AcquisitionMode acqmode, uint frameNum);
        protected abstract bool DoSetTriggerActivation(ProCommon.Communal.EffectiveSignal edge);
        protected abstract bool DoStartGrab();
        protected abstract bool DoPauseGrab();
        protected abstract bool DoStopGrab();
        protected abstract bool DoSoftTriggerOnce();
        protected abstract bool DoSetExposureTime(float exposuretime);
        protected abstract bool DoSetGain(float gain);
        protected abstract bool DoSetGamma(float gamma);
        protected abstract bool DoSetFrameRate(float fps);
        protected abstract bool DoSetTriggerDelay(int idx,float trigdelay);
        protected abstract bool DoSetDebouncerTime(int idx,float debouncertime);
        protected abstract bool DoSetOutPut(int idx,bool onOff);
        protected abstract bool DoGetInPut(int idx, out bool onOff);
        protected abstract bool DoCreateCameraSetPage(System.IntPtr windowHandle, string promption);
        protected abstract bool DoShowCameraSetPage();
        protected abstract bool DoRegisterExceptionCallBack();
        protected abstract bool DoRegisterImageGrabbedCallBack();
        protected abstract bool DoGetCameraConnectedState(out bool isConnected);
        #endregion

        #region 实现接口

        public bool IsImageGrabbed { set; get; }

        public bool EnumerateCameraList()
        {
            return DoEnumerateCameraList();
        }

        public int GetCameraListCount()
        {
            return  DoGetCameraListCount();
        }

        public bool GetCameraByIdx(int index)
        {
            return DoGetCameraByIdx(index);
        }

        public string GetCameraSN(int index)
        {
            return DoGetCameraSN(index);
        }

        public bool GetCameraByName(string camName)
        {
            return DoGetCameraByName(camName);
        }

        public bool GetCameraBySN(string camSN)
        {
            return DoGetCameraBySN(camSN);
        }

        public bool Open()
        {
            return DoOpen();
        }

        public bool Close()
        {
            return DoClose();
        }

        public bool SetAcquisitionMode(ProCommon.Communal.AcquisitionMode acqmode, uint frameNum)
        {
            return DoSetAcquisitionMode(acqmode,frameNum);
        }  
        public bool SetTriggerActivation(ProCommon.Communal.EffectiveSignal edge)
        {
            return DoSetTriggerActivation(edge);
        }

        public bool StartGrab()
        {
            return DoStartGrab();
        }

        public bool PauseGrab()
        {
            return DoPauseGrab();
        }

        public bool StopGrab()
        {
            return DoStopGrab();
        }

        public bool SoftTriggerOnce()
        {
            return DoSoftTriggerOnce();
        }

        public bool SetExposureTime(float exposuretime)
        {
            return DoSetExposureTime(exposuretime);
        }

        public bool SetGain(float gain)
        {
            return DoSetGain(gain);
        }

        public bool SetGamma(float gamma)
        {
            return DoSetGamma(gamma);
        }

        public bool SetFrameRate(float fps)
        {
            return DoSetFrameRate(fps);
        }

        public bool SetTriggerDelay(int idx,float trigdelay)
        {
            return DoSetTriggerDelay(idx,trigdelay);
        }

        public bool SetDebouncerTime(int idx,float debouncertime)
        {
            return DoSetDebouncerTime(idx,debouncertime);
        }

        public bool SetOutPut(int idx,bool onOff)
        {
            return DoSetOutPut(idx,onOff);
        }

        public bool GetInPut(int idx,out bool onOff)
        {
            return DoGetInPut(idx, out onOff);
        }

        public bool CreateCameraSetPage(System.IntPtr windowHandle, string promption)
        {
            return DoCreateCameraSetPage(windowHandle, promption);
        }

        public bool ShowCameraSetPage()
        {
            return DoShowCameraSetPage();
        }

        public bool RegisterExceptionCallBack()
        {
            return DoRegisterExceptionCallBack();
        }

        public bool RegisterImageGrabbedCallBack()
        {
            return DoRegisterImageGrabbedCallBack();
        }

        public bool GetCameraConnectedState(out bool isConnected)
        {
            return DoGetCameraConnectedState(out isConnected);
        }

        #endregion

        #region 覆写并抽象化Object类的ToString()
        public abstract override string ToString();
        #endregion

        public ProCommon.Communal.CameraProperty CameraProperty { private set; get; }
    }

    #endregion

    #region 控制器相关

    /// <summary>
    /// 极限位
    /// </summary>
    public enum LimitType : uint
    {
        NegaLimit = 0,
        FwdLimit = 1
    }

    /// <summary>
    /// 正运动板卡轴外部信号
    /// </summary>
    public enum ZMotionAxisStatus : uint
    {
        NOERROR=0,                               //无异常(隐藏定义)
        FOLLOWUPERROR_EXTENDALM = 2,             //随动误差超限报警
        REMOTEAXISCOMMU_ERROR = 4,               //远程轴通信错误
        REMOTEDRIVER_ERROR = 8,                  //远程驱动器错误
        HARD_FWDLIMIT = 16,                      //正向硬限位
        HARD_NEGALIMIT = 32,                     //反向硬限位
        HARD_DATUMING = 64,                      //找原点中
        HOLDSPEED_KEEPIN = 128,                  //HOLD速度保持信号输入
        FOLLOWUPERROR_EXTENDERR = 256,           //随动误差超限错误
        SOFT_EXTENDFWDLIMIT = 512,               //超出正向软限位
        SOFT_EXTENDNEGLIMIT = 1024,              //超出反向软限位
        CANCEL = 2048,                           //执行Cancel
        EXTENDMAX_SPEED = 4096,                  //脉冲频率超限
        MACHANICALROBOT_COORDINATEERR = 16384,   //机械手坐标错误
        POWER_ERR = 262144,                      //电源异常
        ALARM_IN = 4194304,                      //告警信号输入
        AXIS_PAUSE = 8388608                     //轴进入暂停状态
    }

    /// <summary>
    /// 雷泰板卡轴运动状态
    /// </summary>
    public enum LeadShineAxisStatus : uint
    {
        Reserved0 = 0,
        Reserved1 = 1,
        Reserved2 = 2,
        Reserved3 = 3,
        Reserved4 = 4,
        Reserved5 = 5,
        Reserved6 = 6,
        Reserved7 = 7,
        FU = 8,         //正在加速
        FD = 9,         //正在减速
        FC = 10,        //正在低速运动
        ALM = 11,       //告警信号输入
        PEL = 12,       //正向硬限位信号输入
        MEL = 13,       //负向硬限位信号输入
        ORG = 14,       //原点硬限位信号输入
        SD = 15         //减速硬限位信号输入
    }

    /// <summary>
    /// 雷泰板卡轴外部信号
    /// </summary>
    public enum LeadShineAxisExStatus : uint
    {
        Reserved0 = 0,
        Reserved1 = 1,
        Reserved2 = 2,
        Reserved3 = 3,
        CSD = 4,         //同时减速信号,1=ON,0=OFF
        STA = 5,         //同时启动信号,1=ON,0=OFF
        STP = 6,         //同时停止信号,1=ON,0=OFF
        EMG = 7,         //紧急停止信号,1=ON,0=OFF
        PCS = 8,         //位置改变信号,1=ON,0=OFF
        ERC = 9,         //误差清除信号,1=ON,0=OFF
        EZ = 10,         //索引信号,1=ON,0=OFF
        DRPA = 11,       //+DR(PA)信号,1=ON,0=OFF
        DRPB = 12,       //-DR(PB)信号,1=ON,0=OFF
        Reserved13 = 13,
        SD = 14,         //减速信号,1=ON,0=OFF
        INP = 15,        //轴到位信号,1=ON,0=OFF
        DIR = 16,        //脉冲输出方向,1=负方向,0=正方向
        Reserved17 = 17,
        Reserved18 = 18,
        Reserved19 = 19,
        Reserved20 = 20,
        Reserved21 = 21,
        Reserved22 = 22,
        Reserved23 = 23,
        Reserved24 = 24,
        Reserved25 = 25,
        Reserved26 = 26,
        Reserved27 = 27,
        Reserved28 = 28,
        Reserved29 = 29,
        Reserved30 = 30,
        Reserved31 = 31
    }

    /// <summary>
    /// 板卡操作接口
    /// </summary>
    public interface IBoardDriver
    {

        /// <summary>
        /// 启连控制器(EtherNet)
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        bool ConnectCtrller(string ip,int port);       

        /// <summary>
        /// 断开连接控制器
        /// </summary>
        /// <returns></returns>
        bool DisconnectCtrller();

        /// <summary>
        /// 初始化控制器资源(PCI)
        /// </summary>
        /// <returns></returns>
        bool InitCtrllerSys();

        /// <summary>
        /// 设置主轴及联动轴列表
        /// [注:多轴联动插补时]
        /// </summary>
        /// <param name="axisNum"></param>
        /// <param name="piAxislist"></param>
        /// <returns></returns>
        bool SetBaseAxes(int axisNum, int[] piAxislist);

        /// <summary>
        /// 设置轴类型
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        bool SetAxisType(int naxis, int type);

        /// <summary>
        /// 设置指定轴的脉冲输出模式
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        bool SetAxisPulseOutMode(int naxis, int mode);

        /// <summary>
        /// 设置指定轴的软负限位值
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="fvalue"></param>
        /// <returns></returns>
        bool SetAxisSRevValue(int naxis, float fvalue);

        /// <summary>
        /// 设置指定轴软正限位值
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="fvalue"></param>
        /// <returns></returns>
        bool SetAxisSFwdValue(int naxis, float fvalue);

        /// <summary>
        /// 设置指定轴的脉冲当量
        /// [注:这里的描述为运行单位用户单位时的脉冲数;
        /// 例如运行单位mm需要的脉冲数]
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="units"></param>
        /// <returns></returns>
        bool SetAxisUnits(int naxis, float units);

        bool SetAxisCreep(int naxis, float creep);

        /// <summary>
        /// 设置指定轴T型曲线运动参数
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="
        /// "></param>
        /// <param name="maxspeed"></param>
        /// <param name="tacc"></param>
        /// <param name="tdec"></param>
        /// <returns></returns>
        bool SetAxisTrapeziumPara(int naxis, float lspeed, float maxspeed, float tacc, float tdec);

        /// <summary>
        /// 设置指定轴S型曲线运动参数
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="lspeed"></param>
        /// <param name="maxspeed"></param>
        /// <param name="tacc"></param>
        /// <param name="tdec"></param>
        /// <param name="sacc"></param>
        /// <param name="sdec"></param>
        /// <returns></returns>
        bool SetAxisSigmoidPara(int naxis, float lspeed, float maxspeed, float tacc, float tdec, int sacc, int sdec);

        /// <summary>
        /// 设置主轴电机转速模拟量
        /// </summary>
        /// <param name="ionum"></param>
        /// <param name="fValue"></param>
        /// <returns></returns>
        bool SetDA(int ionum, float fValue);

        //专用信号口设置函数

        /// <summary>
        /// 设置指定轴的轴减速信号有效电平
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="enable"></param>
        /// <param name="sdlevel"></param>
        /// <param name="sdmode"></param>
        /// <returns></returns>
        bool SetAxisSDEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel sdlevel, uint sdmode);

        /// <summary>
        /// 设置指定轴位置改变信号有效电平
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="enable"></param>
        /// <param name="pcslevel"></param>
        /// <returns></returns>
        bool SetAxisPCSEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel pcslevel);

        /// <summary>
        /// 设置指定轴位置到达信号的有效电平
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="enable"></param>
        /// <param name="inplevel"></param>
        /// <returns></returns>
        bool SetAxisINPEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel inplevel);

        /// <summary>
        /// 指定轴的误差清除信号有效电平
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="enable"></param>
        /// <param name="erclevel"></param>
        /// <param name="ercwidth"></param>
        /// <param name="ercofftime"></param>
        /// <returns></returns>
        bool SetAxisERCEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel erclevel, uint ercwidth, uint ercofftime);

        /// <summary>
        /// 设置指定轴的ERC信号状态
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="sel"></param>
        /// <returns></returns>
        bool SetAxisERC(int naxis, uint sel);

        /// <summary>
        /// 设置指定轴的报警信号有效电平
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="almlevel"></param>
        /// <param name="actionmode"></param>
        /// <returns></returns>
        bool SetAxisALMEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel almlevel, uint actionmode);

        /// <summary>
        /// 设置指定轴编码器复位信号有效电平
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="ezlevel"></param>
        /// <param name="actionmode"></param>
        /// <returns></returns>
        bool SetAxisEZEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel ezlevel, uint actionmode);

        bool SetAxisLTCEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel ltclevel, uint actionmode);

        bool SetAxisELEffectiveLevel(int naxis, uint actionmode);

        bool SetAxisDatumEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel datumlevel, uint filter);

        bool SetAxisServo(int naxis, bool onoff);

        bool SetAxisEMGEffectiveLevel(uint enable, ProCommon.Communal.ElectricalLevel emglevel);


        /// <summary>
        /// 设置指定轴的报警信号输入端口位
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="inputno"></param>
        /// <returns></returns>
        bool SetAxisALMIn(int naxis, int inputno);

        /// <summary>
        /// 获取指定轴的报警信号输入端口位
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="inputno"></param>
        /// <returns></returns>
        bool GetAxisALMIn(int naxis, ref int inputno);

        /// <summary>
        /// 设置指定轴的硬负限位信号端口位
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="inputno"></param>
        /// <returns></returns>
        bool SetAxisHRevIn(int naxis, int inputno);

        /// <summary>
        /// 获取指定轴的硬负限位信号端口位
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="inputno"></param>
        /// <returns></returns>
        bool GetAxisHRevIn(int naxis, ref int inputno);

        /// <summary>
        /// 设置指定轴的硬原点信号端口位
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="inputno"></param>
        /// <returns></returns>
        bool SetAxisDatumIn(int naxis, int inputno);

        /// <summary>
        /// 获取指定轴的硬原点限位信号端口位
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="inputno"></param>
        /// <returns></returns>
        bool GetAxisDatumIn(int naxis, ref int inputno);

        /// <summary>
        /// 设置指定轴的硬正限位信号端口位
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="inputno"></param>
        /// <returns></returns>
        bool SetAxisHFwdIn(int naxis, int inputno);

        /// <summary>
        /// 获取指定轴的硬正限位信号端口位
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="inputno"></param>
        /// <returns></returns>
        bool GetAxisHFwdIn(int naxis, ref int inputno);

        /// <summary>
        /// 设置指定端口位的有效逻辑电平
        /// </summary>
        /// <param name="inputno"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        bool SetPortInEffectiveLevel(int inputno, ProCommon.Communal.ElectricalLevel level);

        /// <summary>
        /// 指定轴单轴找原点
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="moveDir"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        bool FindDatum(int naxis, ProCommon.Communal.MoveDirection moveDir, int mode);

        /// <summary>
        /// 指定轴单轴连续运动
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="movedir"></param>
        /// <returns></returns>
        bool SingleContinueMove(int naxis, ProCommon.Communal.MoveDirection movedir);

        /// <summary>
        /// 指定轴单轴相对运动
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        bool SingleRelMove(int naxis, float pos);

        /// <summary>
        /// 指定轴单轴绝对运动
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        bool SingleAbsMove(int naxis, float pos);

        bool CancelAxisList(int[] axispos);

        /// <summary>
        /// 指定轴单轴停止运动
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        bool SingleCancelMove(int naxis, int mode);

        /// <summary>
        /// 设置指定轴的当前位置
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="curpos"></param>
        /// <returns></returns>
        bool SetAxisCurPos(int naxis, float curpos);

        /// <summary>
        /// 获取指定轴的当前位置
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="curpos"></param>
        /// <returns></returns>
        bool GetAxisCurPos(int naxis, ref float curpos);

        /// <summary>
        /// 获取指定轴的当前速度
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="curspeed"></param>
        /// <returns></returns>
        bool GetAxisCurspeed(int naxis, ref float curspeed);

        /// <summary>
        /// 检测指定轴是否停止
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="stopped"></param>
        /// <returns></returns>
        bool ChekcAxisIfStop(int naxis, ref bool stopped);

        /// <summary>
        /// 获取指定轴的轴状态
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="axisStatus"></param>
        /// <returns></returns>
        bool GetAxisStatus(int naxis, ref int axisStatus);

        /// <summary>
        /// 检测指定轴是否正常
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="normal"></param>
        /// <returns></returns>
        bool CheckAxisIfNormal(int naxis, ref bool normal);

        /// <summary>
        /// 立即停止(轴列表指定的所有轴)
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        bool RapidStop(int mode);

        /// <summary>
        /// 两轴直线插补运动
        /// </summary>
        /// <param name="axespos"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        bool Line2Move(float[] axespos, int mode);

        /// <summary>
        /// 基于三点的两轴圆弧插补运动
        /// </summary>
        /// <param name="midpos"></param>
        /// <param name="dstpos"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        bool PointsBasedArc2Move(float[] midpos, float[] dstpos, int mode);

        /// <summary>
        /// 基于圆心的两轴圆弧插补运动
        /// </summary>
        /// <param name="dstpos"></param>
        /// <param name="cenpos"></param>
        /// <param name="dir"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        bool CenterBasedArc2Move(float[] dstpos, float[] cenpos, int dir, int mode);

        /// <summary>
        /// 设置指定轴的减速运动模式
        /// </summary>
        /// <param name="axisNum"></param>
        /// <param name="deceleratemode"></param>
        /// <returns></returns>
        bool SetCornerDecelerateMode(int axisNum, int deceleratemode);

        /// <summary>
        /// 设置指定轴减速运动模式的减速角范围
        /// </summary>
        /// <param name="axisNum"></param>
        /// <param name="decelanglerange"></param>
        /// <returns></returns>
        bool SetCornerDecelerateAngleRange(int axisNum, float[] decelanglerange);

        /// <summary>
        /// 设置指定轴减速运动模式的减速半径
        /// </summary>
        /// <param name="axisNum"></param>
        /// <param name="decelradius"></param>
        /// <returns></returns>
        bool SetCornerDecelerateRadius(int axisNum, float decelradius);

        /// <summary>
        /// 设置指定轴减速运动模式的倒角半径
        /// </summary>
        /// <param name="axisNum"></param>
        /// <param name="soomthradius"></param>
        /// <returns></returns>
        bool SetCornerSoomthRadius(int axisNum, float soomthradius);

        /// <summary>
        /// 设置指定输出端口输出逻辑值
        /// </summary>
        /// <param name="nbit"></param>
        /// <param name="onoff"></param>
        /// <returns></returns>
        bool SetOutBitLogicValue(int nbit, bool onoff);

        /// <summary>
        /// 获取指定输出端口位的电平
        /// </summary>
        /// <param name="nbit"></param>
        /// <param name="onoff"></param>
        /// <returns></returns>
        bool GetOutBitLogicValue(int nbit, ref bool onoff);

        /// <summary>
        /// 获取指定输入端口位的电平
        /// </summary>
        /// <param name="nbit"></param>
        /// <param name="onoff"></param>
        /// <returns></returns>
        bool GetInBitLogicValue(int nbit, ref bool onoff);
       
        /// <summary>
        /// 等待指定轴找原点
        /// [注：找原点+轴停止+轴到指定位置]
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="waitfordatum"></param>
        /// <param name="sleepsecond"></param>
        /// <param name="timeout"></param>
        /// <param name="enablepause"></param>
        /// <param name="limitdistance"></param>
        /// <param name="specifiedpos"></param>
        /// <returns></returns>
        bool WaitForAxisFindDatum(int naxis, bool waitfordatum = true, double sleepsecond = 0.01,
                                  double timeout = 20, bool enablepause = true, float limitdistance = -1, float specifiedpos = 0.0f);

        /// <summary>
        /// 等待指定轴停止
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="sleepsecond"></param>
        /// <param name="timeout"></param>
        /// <param name="enablepause"></param>
        /// <returns></returns>
        bool WaitForAxisStop(int naxis, double sleepsecond = 0.01, double timeout = 20, bool enablepause = true);

        /// <summary>
        /// 等待指定轴到指定限位
        /// </summary>
        /// <param name="naxis"></param>
        /// <param name="limittype"></param>
        /// <param name="waitforstatus"></param>
        /// <param name="sleepsecond"></param>
        /// <param name="timeout"></param>
        /// <param name="enablepause"></param>
        /// <returns></returns>
        bool WaitForAxisLimit(int naxis, LimitType limittype, bool waitforstatus = true, double sleepsecond = 0.01, double timeout = 20, bool enablepause = true);
    }

    /// <summary>
    /// 板卡操作类
    /// </summary>
    public abstract class BoardDriver : IBoardDriver
    {
        public DriverExceptionOccuredDel ExceptionOccuredDel;

        public BoardDriver()
        {
            ExceptionOccuredDel = new DriverExceptionOccuredDel(OnDirverExceptionOccured);
        }
        private void OnDirverExceptionOccured(string err)
        {
            //什么都不做
        }

        #region 抽象成员(钩子函数)
        protected abstract bool DoConnectCtrller(string ip,int port);
        protected abstract bool DoDisconnectCtrller();
        protected abstract bool DoInitCtrllerSys();
        protected abstract bool DoSetBaseAxes(int axisNum, int[] piAxislist);
        protected abstract bool DoSetAxisType(int naxis, int type);
        protected abstract bool DoSetAxisPulseOutMode(int naxis, int mode);

        protected abstract bool DoSetAxisALMIn(int naxis, int inputno);
        protected abstract bool DoGetAxisALMIn(int naxis, ref int inputno);
        protected abstract bool DoSetAxisHRevIn(int naxis, int inputno);
        protected abstract bool DoGetAxisHRevIn(int naxis, ref int inputno);
        protected abstract bool DoSetAxisSRevValue(int naxis, float fvalue);
        protected abstract bool DoSetAxisDatumIn(int naxis, int inputno);
        protected abstract bool DoGetAxisDatumIn(int naxis, ref int inputno);
        protected abstract bool DoSetAxisHFwdIn(int naxis, int inputno);
        protected abstract bool DoGetAxisHFwdIn(int naxis, ref int inputno);
        protected abstract bool DoSetAxisSFwdValue(int naxis, float fvalue);
        protected abstract bool DoSetPortInEffectiveLevel(int inputno, ProCommon.Communal.ElectricalLevel level);
        protected abstract bool DoSetAxisUnits(int naxis, float units);
        protected abstract bool DoSetAxisCreep(int naxis, float creep);
        protected abstract bool DoSetAxisTrapeziumPara(int naxis, float lspeed, float maxspeed, float tacc, float tdec);

        protected abstract bool DoSetDA(int ionum, float fValue);
        protected abstract bool DoSetAxisSigmoidPara(int naxis, float lspeed, float maxspeed, float tacc, float tdec, int sacc, int sdec);


        protected abstract bool DoSetAxisSDEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel sdlevel, uint sdmode);
        protected abstract bool DoSetAxisPCSEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel pcslevel);
        protected abstract bool DoSetAxisINPEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel inplevel);
        protected abstract bool DoSetAxisERCEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel erclevel, uint ercwidth, uint ercofftime);
        protected abstract bool DoSetAxisERC(int naxis, uint sel);
        protected abstract bool DoSetAxisALMEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel almlevel, uint actionmode);
        protected abstract bool DoSetAxisEZEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel ezlevel, uint actionmode);
        protected abstract bool DoSetAxisLTCEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel ltclevel, uint actionmode);
        protected abstract bool DoSetAxisELEffectiveLevel(int naxis, uint actionmode);
        protected abstract bool DoSetAxisDatumEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel datumlevel, uint filter);
        protected abstract bool DoSetAxisServo(int naxis, bool onoff);
        protected abstract bool DoSetAxisEMGEffectiveLevel(uint enable, ProCommon.Communal.ElectricalLevel emglevel);


        protected abstract bool DoFindDatum(int naxis, ProCommon.Communal.MoveDirection moveDir, int mode);
        protected abstract bool DoSingleContinueMove(int naxis, ProCommon.Communal.MoveDirection movedir);
        protected abstract bool DoSingleRelMove(int naxis, float pos);
        protected abstract bool DoSingleAbsMove(int naxis, float pos);
        protected abstract bool DoCancelAxisList(int[] axispos);
        protected abstract bool DoSingleCancelMove(int naxis, int mode);
        protected abstract bool DoSetAxisCurPos(int naxis, float curpos);
        protected abstract bool DoGetAxisCurPos(int naxis, ref float curpos);
        protected abstract bool DoGetAxisCurspeed(int naxis, ref float curspeed);
        protected abstract bool DoChekcAxisIfStop(int naxis, ref bool stopped);
        protected abstract bool DoGetAxisStatus(int naxis, ref int axisStatus);
        protected abstract bool DoCheckAxisIfNormal(int naxis, ref bool normal);
        protected abstract bool DoRapidStop(int mode);
        protected abstract bool DoLine2Move(float[] axespos, int mode);
        protected abstract bool DoPointsBasedArc2Move(float[] midpos, float[] dstpos, int mode);
        protected abstract bool DoCenterBasedArc2Move(float[] dstpos, float[] cenpos, int dir, int mode);

        protected abstract bool DoSetCornerDecelerateMode(int axisNum, int deceleratemode);
        protected abstract bool DoSetCornerDecelerateAngleRange(int axisNum, float[] decelanglerange);
        protected abstract bool DoSetCornerDecelerateRadius(int axisNum, float decelradius);
        protected abstract bool DoSetCornerSoomthRadius(int axisNum, float soomthradius);

        protected abstract bool DoSetOutBitLogicValue(int nbit, bool onoff);
        protected abstract bool DoGetOutBitLogicValue(int nbit, ref bool onoff);
        protected abstract bool DoGetInBitLogicValue(int nbit, ref bool onoff);

        protected abstract bool DoWaitForAxisFindDatum(int naxis, bool waitfordatum = true, double sleepsecond = 0.01,
             double timeout = 20, bool enablepause = true, float limitdistance = -1, float specifiedpos = 0.0f);
        protected abstract bool DoWaitForAxisStop(int naxis, double sleepsecond = 0.01, double timeout = 20, bool enablepause = true);
        protected abstract bool DoWaitForAxisLimit(int naxis, LimitType limittype, bool waitforstatus = true, double sleepsecond = 0.01,
             double timeout = 20, bool enablepause = true);
        #endregion

        #region 实现接口
        public bool ConnectCtrller(string ip,int port)
        {
            return DoConnectCtrller(ip,port);
        }
      
        public bool DisconnectCtrller()
        {
            return DoDisconnectCtrller();
        }
        public bool InitCtrllerSys()
        {
            return DoInitCtrllerSys();
        }
        public bool SetBaseAxes(int axisNum, int[] piAxislist)
        {
            return DoSetBaseAxes(axisNum, piAxislist);
        }
        public bool SetAxisType(int naxis, int type)
        {
            return DoSetAxisType(naxis, type);
        }
        public bool SetAxisPulseOutMode(int naxis, int mode)
        {
            return DoSetAxisPulseOutMode(naxis, mode);
        }
        public bool SetAxisALMIn(int naxis, int inputno)
        {
            return DoSetAxisALMIn(naxis, inputno);
        }

        public bool GetAxisALMIn(int naxis, ref int inputno)
        {
           return DoGetAxisALMIn(naxis, ref inputno);
        }

        public bool GetAxisHRevIn(int naxis, ref int inputno)
        {
            return DoGetAxisHRevIn(naxis, ref inputno);
        }

        public bool GetAxisDatumIn(int naxis, ref int inputno)
        {
            return DoGetAxisDatumIn(naxis, ref inputno);
        }

        public bool GetAxisHFwdIn(int naxis, ref int inputno)
        {
            return DoGetAxisHFwdIn(naxis, ref inputno);
        }

        public bool SetAxisHRevIn(int naxis, int inputno)
        {
            return DoSetAxisHRevIn(naxis, inputno);
        }
        public bool SetAxisSRevValue(int naxis, float fvalue)
        {
            return DoSetAxisSRevValue(naxis, fvalue);
        }
        public bool SetAxisDatumIn(int naxis, int inputno)
        {
            return DoSetAxisDatumIn(naxis, inputno);
        }
        public bool SetAxisHFwdIn(int naxis, int inputno)
        {
            return DoSetAxisHFwdIn(naxis, inputno);
        }
        public bool SetAxisSFwdValue(int naxis, float fvalue)
        {
            return DoSetAxisSFwdValue(naxis, fvalue);
        }
        public bool SetPortInEffectiveLevel(int inputno, ProCommon.Communal.ElectricalLevel level)
        {
            return DoSetPortInEffectiveLevel(inputno, level);
        }
        public bool SetAxisUnits(int naxis, float units)
        {
            return DoSetAxisUnits(naxis, units);
        }

        public bool SetAxisCreep(int naxis, float creep)
        {
            return DoSetAxisCreep(naxis, creep);
        }
        public bool SetAxisTrapeziumPara(int naxis, float lspeed, float maxspeed, float tacc, float tdec)
        {
            return DoSetAxisTrapeziumPara(naxis, lspeed, maxspeed, tacc, tdec);
        }

        public  bool SetDA(int ionum, float fValue)
        {
            return DoSetDA(ionum, fValue);
        }
        public bool SetAxisSigmoidPara(int naxis, float lspeed, float maxspeed, float tacc, float tdec, int sacc, int sdec)
        {
            return DoSetAxisSigmoidPara(naxis, lspeed, maxspeed, tacc, tdec, sacc, sdec);
        }
        public bool FindDatum(int naxis, ProCommon.Communal.MoveDirection moveDir, int mode)
        {
            return DoFindDatum(naxis, moveDir, mode);
        }
        public bool SingleContinueMove(int naxis, ProCommon.Communal.MoveDirection movedir)
        {
            return DoSingleContinueMove(naxis, movedir);
        }
        public bool SingleRelMove(int naxis, float pos)
        {
            return DoSingleRelMove(naxis, pos);
        }
        public bool SingleAbsMove(int naxis, float pos)
        {
            return DoSingleAbsMove(naxis, pos);
        }

        public bool CancelAxisList(int[] axispos)
        {
            return DoCancelAxisList(axispos);
        }
        public bool SingleCancelMove(int naxis, int mode)
        {
            return DoSingleCancelMove(naxis, mode);
        }
        public bool SetAxisCurPos(int naxis, float curpos)
        {
            return DoSetAxisCurPos(naxis, curpos);
        }
        public bool GetAxisCurPos(int naxis, ref float curpos)
        {
            return DoGetAxisCurPos(naxis, ref curpos);
        }
        public bool GetAxisCurspeed(int naxis, ref float curspeed)
        {
            return DoGetAxisCurspeed(naxis, ref curspeed);
        }
        public bool ChekcAxisIfStop(int naxis, ref bool stopped)
        {
            return DoChekcAxisIfStop(naxis, ref stopped);
        }
        public bool GetAxisStatus(int naxis, ref int axisStatus)
        {
            return DoGetAxisStatus(naxis, ref axisStatus);
        }
        public bool CheckAxisIfNormal(int naxis, ref bool normal)
        {
            return DoCheckAxisIfNormal(naxis, ref normal);
        }
        public bool RapidStop(int mode)
        {
            return DoRapidStop(mode);
        }
        public bool Line2Move(float[] axespos, int mode)
        {
            return DoLine2Move(axespos, mode);
        }
        public bool PointsBasedArc2Move(float[] midpos, float[] dstpos, int mode)
        {
            return DoPointsBasedArc2Move(midpos, dstpos, mode);
        }

        public bool CenterBasedArc2Move(float[] dstpos, float[] cenpos, int dir, int mode)
        {
            return DoCenterBasedArc2Move(dstpos, cenpos, dir, mode);
        }
        public bool SetCornerDecelerateMode(int axisNum, int deceleratemode)
        {
            return DoSetCornerDecelerateMode(axisNum, deceleratemode);
        }
        public bool SetCornerDecelerateAngleRange(int axisNum, float[] decelanglerange)
        {
            return SetCornerDecelerateAngleRange(axisNum, decelanglerange);
        }
        public bool SetCornerDecelerateRadius(int axisNum, float decelradius)
        {
            return SetCornerDecelerateRadius(axisNum, decelradius);
        }
        public bool SetCornerSoomthRadius(int axisNum, float smoothradius)
        {
            return SetCornerSoomthRadius(axisNum, smoothradius);
        }

        public bool SetOutBitLogicValue(int nbit, bool onoff)
        {
            return DoSetOutBitLogicValue(nbit, onoff);
        }
        public bool GetOutBitLogicValue(int nbit, ref bool onoff)
        {
            return DoGetOutBitLogicValue(nbit, ref onoff);
        }
        public bool GetInBitLogicValue(int nbit, ref bool onoff)
        {
            return DoGetInBitLogicValue(nbit, ref onoff);
        }
        public bool WaitForAxisFindDatum(int naxis, bool waitfordatum = true, double sleepsecond = 0.01, double timeout = 20, bool enablepause = true, float limitdistance = -1, float specifiedpos = 0.0f)
        {
            return DoWaitForAxisFindDatum(naxis, waitfordatum, sleepsecond, timeout, enablepause, limitdistance, specifiedpos);
        }
        public bool WaitForAxisStop(int naxis, double sleepsecond = 0.01, double timeout = 20, bool enablepause = true)
        {
            return DoWaitForAxisStop(naxis, sleepsecond, timeout, enablepause);
        }
        public bool WaitForAxisLimit(int naxis, LimitType limittype, bool waitforstatus = true, double sleepsecond = 0.01, double timeout = 20, bool enablepause = true)
        {
            return DoWaitForAxisLimit(naxis, limittype, waitforstatus, sleepsecond, timeout, enablepause);
        }


        public bool SetAxisSDEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel sdlevel, uint sdmode)
        {
            return DoSetAxisSDEffectiveLevel(naxis, enable, sdlevel, sdmode);
        }
        public bool SetAxisPCSEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel pcslevel)
        {
            return DoSetAxisPCSEffectiveLevel(naxis, enable, pcslevel);
        }
        public bool SetAxisINPEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel inplevel)
        {
            return DoSetAxisINPEffectiveLevel(naxis, enable, inplevel);
        }
        public bool SetAxisERCEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel erclevel, uint ercwidth, uint ercofftime)
        {
            return DoSetAxisERCEffectiveLevel(naxis, enable, erclevel, ercwidth, ercofftime);
        }
        public bool SetAxisERC(int naxis, uint sel)
        {
            return DoSetAxisERC(naxis, sel);
        }
        public bool SetAxisALMEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel almlevel, uint actionmode)
        {
            return DoSetAxisALMEffectiveLevel(naxis, almlevel, actionmode);
        }
        public bool SetAxisEZEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel ezlevel, uint actionmode)
        {
            return DoSetAxisEZEffectiveLevel(naxis, ezlevel, actionmode);
        }
        public bool SetAxisLTCEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel ltclevel, uint actionmode)
        {
            return DoSetAxisLTCEffectiveLevel(naxis, ltclevel, actionmode);
        }
        public bool SetAxisELEffectiveLevel(int naxis, uint actionmode)
        {
            return DoSetAxisELEffectiveLevel(naxis, actionmode);
        }
        public bool SetAxisDatumEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel datumlevel, uint filter)
        {
            return DoSetAxisDatumEffectiveLevel(naxis, datumlevel, filter);
        }
        public bool SetAxisServo(int naxis, bool onoff)
        {
            return DoSetAxisServo(naxis, onoff);
        }
        public bool SetAxisEMGEffectiveLevel(uint enable, ProCommon.Communal.ElectricalLevel emglevel)
        {
            return DoSetAxisEMGEffectiveLevel(enable, emglevel);
        }

        #endregion

        #region 覆写并抽象化Object类的ToString()
        public abstract override string ToString();       
        #endregion
    }


    /// <summary>
    /// PLC操作接口
    /// </summary>
    public interface IPLCDriver
    {
        /// <summary>
        /// 获取单个输入控制变量对象的状态
        /// </summary>
        /// <param name="inVarObj">输入控制变量对象</param>
        /// <param name="isOnOff">输入变量对象的状态:
        /// true--导通,false--断开</param>
        /// <returns></returns>
        bool GetInvarStatus(ProCommon.Communal.InVarObj inVarObj,out bool isOnOff);

        /// <summary>
        /// 设置单个输入控制变量对象的状态
        /// </summary>
        /// <param name="inVarObj">输入控制变量对象</param>
        /// <param name="isOnOff">输入变量对象的状态:
        /// true--导通,false--断开</param>
        /// <returns></returns>
        bool SetInvarStatus(ProCommon.Communal.InVarObj inVarObj, bool isOnOff);

        /// <summary>
        /// 获取多个输入控制变量对象的状态
        /// </summary>
        /// <param name="inVarObjList"></param>
        /// <param name="isOnOff"></param>
        /// <returns></returns>
        bool GetPluralInvarStatus(ProCommon.Communal.InVarObjList inVarObjList,out bool[] isOnOff);

        /// <summary>
        /// 设置多个输入变量对象的状态
        /// </summary>
        /// <param name="inVarObjList"></param>
        /// <param name="isOnOff"></param>
        /// <returns></returns>
        bool SetPluralInvarStatus(ProCommon.Communal.InVarObjList inVarObjList,bool[] isOnOff);

        /// <summary>
        /// 获取输入控制变量地址起始,指定个数数据寄存器的数据
        /// </summary>
        /// <param name="inVarObj">输入变量对象</param>
        /// <param name="dData">数据寄存器数据值</param>
        /// <returns></returns>
        bool GetInvarData(ProCommon.Communal.InVarObj inVarObj, int num, out double[] dData);

        bool SetInvarData(ProCommon.Communal.InVarObj inVarObj, int num,double[] dData);
    }

    /// <summary>
    /// PLC访问接口函数类
    /// </summary>
    public abstract class PlcDriver : IPLCDriver
    {
        public DriverExceptionOccuredDel ExceptionOccuredDel;
        public PlcDriver()
        {
            ExceptionOccuredDel = new DriverExceptionOccuredDel(OnDirverExceptionOccured);
        }
        private void OnDirverExceptionOccured(string err)
        {
            //什么都不做
        }

        #region 钩子函数
        protected abstract bool DoGetInvarStatus(InVarObj inVarObj, out bool isOnOff);
        protected abstract bool DoSetInvarStatus(InVarObj inVarObj, bool isOnOff);

        protected abstract bool DoGetInvarData(ProCommon.Communal.InVarObj inVarObj,int num, out double[] dData);

        protected abstract bool DoSetInvarData(ProCommon.Communal.InVarObj inVarObj, int num,double[] dData);

        protected abstract bool DoGetPluralInvarStatus(InVarObjList inVarObjList, out bool[] isOnOff);

        protected abstract bool DoSetPluralInvarStatus(InVarObjList inVarObjList, bool[] isOnOff);

        #endregion

        #region 实现接口
        public bool GetInvarStatus(InVarObj inVarObj, out bool isOnOff)
        {
            return DoGetInvarStatus(inVarObj,out isOnOff);
        }

        public bool SetInvarStatus(InVarObj inVarObj, bool isOnOff)
        {
            return DoSetInvarStatus(inVarObj, isOnOff);
        }

        public bool GetInvarData(ProCommon.Communal.InVarObj inVarObj, int num, out double[] dData)
        {
            return DoGetInvarData(inVarObj, num,out dData);
        }

        public bool SetInvarData(ProCommon.Communal.InVarObj inVarObj, int num,double[] dData)
        {
            return DoSetInvarData(inVarObj,num, dData);
        }

        public bool GetPluralInvarStatus(InVarObjList inVarObjList, out bool[] isOnOff)
        {
            return DoGetPluralInvarStatus(inVarObjList, out isOnOff);
        }

        public bool SetPluralInvarStatus(InVarObjList inVarObjList, bool[] isOnOff)
        {
            return DoSetPluralInvarStatus(inVarObjList, isOnOff);
        }
        #endregion
    }

    #endregion
}
