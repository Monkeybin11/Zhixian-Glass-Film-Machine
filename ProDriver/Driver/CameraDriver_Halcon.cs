using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*************************************************************************************
    * CLR    Version：       4.0.30319.42000
    * Class     Name：       CameraDriver_Halcon
    * Machine   Name：       LAPTOP-KFCLDVVH
    * Name     Space：       ProDriver.Driver
    * File      Name：       CameraDriver_Halcon
    * Creating  Time：       4/29/2019 2:23:55 PM
    * Author    Name：       xYz_Albert
    * Description   ：       Halcon模拟相机操作封装类
    * Modifying Time：
    * Modifier  Name：
*************************************************************************************/

namespace ProDriver.Driver
{
    /// <summary>
    /// 模拟Halcon相机SDK二次包装驱动函数接口
    /// [注:二次包装驱动函数接口待完善
    /// 日期:200-06-20]
    /// </summary>
    public delegate void HalconImageGrabbed(HalconDotNet.HObject hobj);  
    public class CameraDriver_Halcon : CameraDriver
    {
        public override event CameraImageGrabbedDel CameraImageGrabbedEvt; //图像抓取到事件(统一事件) 

        public HalconDotNet.HTuple AcqHandle;                    //采集句柄       
        public event HalconImageGrabbed _SDKImageGrabbed;        //采集更新事件

        public string InterfaceName = "DirectShow";              //采集接口名称
        public HalconDotNet.HTuple ResolutionH = 1;              //图像水平分辨率(绝对值,或1->全分辨率,2->二分之一全分辨率,4->四分之一全分辨率)
        public HalconDotNet.HTuple ResolutionV = 1;              //图像垂直分辨率(绝对值,或1->全分辨率,2->二分之一全分辨率,4->四分之一全分辨率)
        public HalconDotNet.HTuple DesiredImgWidth = 0;          //预期图像宽度(绝对值,或0->水平分辨率-2*水平起始偏移)
        public HalconDotNet.HTuple DesiredImgHeight = 0;         //预期图像高度(绝对值,或0->垂直分辨率-2*垂直起始偏移)
        public HalconDotNet.HTuple StartRow = 0;                 //图像垂直起始偏移
        public HalconDotNet.HTuple StartCol = 0;                 //图像水平起始偏移
        public HalconDotNet.HTuple Field = "default";            //预期半图或全图("default"->默认,"first"->首张,"second"->第二张,"next"->下一张,"interlaced"->隔行,"progressive"->逐行)
        public HalconDotNet.HTuple BitPerChannel = -1;           //采集通道位深(绝对值,或-1->采集设备默认值)
        public HalconDotNet.HTuple ColorSpace = "default";       //采集设备输出颜色格式(单通道:"gray","raw";三通道:"rgb","yuv","default"->采集设备默认值)
        public HalconDotNet.HTuple Generic = -1;                 //采集设备自定义
        public HalconDotNet.HTuple ExternalTrigger = "default";  //采集设备的外触发开启("default"->采集设备默认值,"false"->关闭外触发,"true"->开启外触发)
        public HalconDotNet.HTuple CameraType = "default";       //采集设备的制式类型("default"->采集设备默认默认值,"ntsc","pal","auto")
        public HalconDotNet.HTuple DeviceIdentifier = "[0] Integrated Camera"; //设备标识符("default"->采集设备默认值,"-1","0","1","3",...)
        public HalconDotNet.HTuple Port = -1;                    //设备端口号(-1->采集设备默认值,0,1,2,3,...)
        public HalconDotNet.HTuple Line = -1;                    //设备多路转接器的线口号(-1->采集设备默认值,0,1,2,3,...)
        public HalconDotNet.HTuple GrabTimeOut = 5000;           //设备采集超时(毫秒)
      
        private HalconDotNet.HObject _imgData;

        public CameraDriver_Halcon(ProCommon.Communal.CameraProperty camProperty):base(camProperty)
        {

        }

        #region 实现抽象函数

        /// <summary>
        /// 枚举设备列表
        /// </summary>
        /// <returns></returns>
        protected override bool DoEnumerateCameraList()
        {
            bool rt = false;
            try
            {
                rt = true;
            }
            catch (HalconDotNet.HalconException hex)
            {

            }
            finally
            {

            }
            return rt;
        }

        protected override int DoGetCameraListCount()
        {
            return 0;
        }

        /// <summary>
        /// 获取指定的设备
        /// </summary>
        /// <param name="index">设备索引号</param>
        /// <returns></returns>
        protected override bool DoGetCameraByIdx(int index)
        {
            bool rt = false;
            try
            {
                rt = true;
            }
            catch (HalconDotNet.HalconException hex)
            {

            }
            finally
            {

            }
            return rt;
        }

        /// <summary>
        /// 获取索引指定相机的名称
        /// </summary>
        /// <param name="index">相机索引</param>
        /// <returns></returns>
        protected override string DoGetCameraSN(int index)
        {
            if (DoGetCameraByIdx(index))
                return "HalconSN";
            return string.Empty;
        }

        /// <summary>
        /// 获取指定的设备
        /// </summary>
        /// <param name="camName">设备名称</param>
        /// <returns></returns>
        protected override bool DoGetCameraByName(string camName)
        {
            bool rt = false;
            try
            {
                rt = true;
            }
            catch (HalconDotNet.HalconException hex)
            {

            }
            finally
            {

            }
            return rt;
        }

        /// <summary>
        /// 获取指定的设备
        /// </summary>
        /// <param name="camSN">设备SN</param>
        /// <returns></returns>
        protected override bool DoGetCameraBySN(string camSN)
        {
            bool rt = false;
            try
            {
                rt = true;
            }
            catch (HalconDotNet.HalconException hex)
            {

            }
            finally
            {

            }
            return rt;
        }

        /// <summary>
        /// 打开设备
        /// </summary>
        /// <returns></returns>
        protected override bool DoOpen()
        {
            bool rt = false;
            try
            {
                HalconDotNet.HOperatorSet.OpenFramegrabber(InterfaceName, ResolutionH, ResolutionV, DesiredImgWidth, DesiredImgHeight, StartRow, StartCol,
                    Field, BitPerChannel, ColorSpace, -1, ExternalTrigger, CameraType, DeviceIdentifier, Port, Line, out AcqHandle);
                rt = true;
            }
            catch (HalconDotNet.HalconException hex)
            {               
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：Halcon模拟相机打开设备失败!\n错误代码:{0}",hex.GetErrorCode()));
            }
            finally
            {

            }
            return rt;
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        protected override bool DoClose()
        {
            bool rt = false;
            try
            {
                if (!AcqHandle.TupleEqual(new HalconDotNet.HTuple()))
                {
                    HalconDotNet.HOperatorSet.CloseFramegrabber(AcqHandle);
                    AcqHandle = null;
                    rt = true;
                }
            }
            catch (HalconDotNet.HalconException hex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：Halcon模拟相机关闭设备失败!\n错误代码:{0}",hex.GetErrorCode()));
            }
            finally
            {

            }
            return rt;
        }



        /// <summary>
        /// 方法：设置采集模式
        /// </summary>
        /// <param name="acqmode"></param>
        /// 0-连续模式，触发采集[1-单帧模式，2-多帧模式]
        /// <param name="frameNum">
        /// 多帧模式下的帧数</param>
        /// <returns></returns>
        protected override bool DoSetAcquisitionMode(ProCommon.Communal.AcquisitionMode acqmode, uint frameNum)
        {
            bool rt = false;
            try
            {
                if (!AcqHandle.TupleEqual(new HalconDotNet.HTuple()))
                {
                    string v = "false";
                    switch(acqmode)
                    {
                        case ProCommon.Communal.AcquisitionMode.Continue:
                            v = "true";
                            break;
                        case ProCommon.Communal.AcquisitionMode.SoftTrigger:
                        case ProCommon.Communal.AcquisitionMode.ExternalTrigger:
                            v = "false";
                            break;
                        default:                           
                            break;
                    }
                    HalconDotNet.HOperatorSet.SetFramegrabberParam(AcqHandle, new HalconDotNet.HTuple("continuous_grabbing"), new HalconDotNet.HTuple(v));
                    rt = true;
                }

            }
            catch (HalconDotNet.HalconException hex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：Halcon模拟相机设置采集模式失败!\n错误代码:{0}",hex.GetErrorCode()));
            }
            finally
            {

            }
            return rt;
        }

        /// <summary>
        /// 方法:设置触发信号边缘
        /// </summary>
        /// <param name="degemode"></param>
        /// <returns></returns>
        protected override bool DoSetTriggerActivation(ProCommon.Communal.EffectiveSignal edge)
        {
            bool rt = false;
            try
            {
                if (!AcqHandle.TupleEqual(new HalconDotNet.HTuple()))
                {
                    rt = true;
                }
            }
            catch (HalconDotNet.HalconException hex)
            {

            }
            finally
            {

            }
            return rt;
        }

        protected override bool DoStartGrab()
        {
            bool rt = false;
            try
            {
                if (!AcqHandle.TupleEqual(new HalconDotNet.HTuple()))
                {
                    //硬件执行开始采集指令
                    HalconDotNet.HOperatorSet.GrabImageStart(AcqHandle, GrabTimeOut);
                    _imgData = new HalconDotNet.HObject();
                    rt = true;
                }
            }
            catch (HalconDotNet.HalconException hex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：Halcon模拟相机开启异步采集失败!\n错误代码:{0}",hex.GetErrorCode()));
            }
            finally
            {

            }
            return rt;
        }

        protected override bool DoPauseGrab()
        {
            bool rt = false;
            try
            {

            }
            catch
            {

            }
            finally
            {

            }

            return rt;
        }

        protected override bool DoStopGrab()
        {
            bool rt = false;
            try
            {
                if (!AcqHandle.TupleEqual(new HalconDotNet.HTuple()))
                {
                    rt = true;
                }
            }
            catch (HalconDotNet.HalconException hex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：Halcon模拟相机停止异步采集失败!\n错误代码:{0}",hex.GetErrorCode()));
            }
            finally
            {

            }
            return rt;
        }

        protected override bool DoSoftTriggerOnce()
        {
            bool rt = false;
            try
            {
                if (!AcqHandle.TupleEqual(new HalconDotNet.HTuple()))
                {
                    if (!_imgData.IsInitialized())
                        _imgData = new HalconDotNet.HObject();

                    _imgData.Dispose();
                    HalconDotNet.HOperatorSet.GrabImageAsync(out _imgData, AcqHandle, GrabTimeOut);

                    if (_imgData != null)
                    {
                        //触发图像采集完成事件
                        OnImgDataOut();
                        rt = true;
                    }
                }
            }
            catch (HalconDotNet.HalconException hex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：Halcon模拟相机异步抓取失败!\n错误代码:{0}",hex.GetErrorCode()));
            }
            finally
            {

            }
            return rt;
        }

        /// <summary>
        /// 设置曝光
        /// </summary>
        /// <param name="exposuretime"></param>
        /// <returns></returns>
        protected override bool DoSetExposureTime(float exposuretime)
        {
            bool rt = false;
            try
            {
                if (!AcqHandle.TupleEqual(new HalconDotNet.HTuple()))
                {
                    //HOperatorSet.SetFramegrabberParam(AcqHandle, "exposure", exposuretime);
                    rt = true;
                }
            }
            catch (HalconDotNet.HalconException hex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：Halcon模拟相机设置曝光时间失败!\n错误代码:{0}",hex.GetErrorCode()));
            }
            finally
            {

            }
            return rt;
        }

        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="gain"></param>
        /// <returns></returns>
        protected override bool DoSetGain(float gain)
        {
            bool rt = false;
            try
            {
                if (!AcqHandle.TupleEqual(new HalconDotNet.HTuple()))
                {
                    rt = true;
                }
            }
            catch (HalconDotNet.HalconException hex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：Halcon模拟相机设置增益失败!\n错误代码:{0}",hex.GetErrorCode()));
            }
            finally
            {

            }
            return rt;
        }

        /// <summary>
        /// 设置Gamma
        /// </summary>
        /// <param name="gamma"></param>
        /// <returns></returns>
        protected override bool DoSetGamma(float gamma)
        {
            bool rt = false;

            return rt;
        }

        /// <summary>
        /// 设置帧率
        /// </summary>
        /// <param name="fps"></param>
        /// <returns></returns>
        protected override bool DoSetFrameRate(float fps)
        {
            bool rt = false;
            try
            {
                HalconDotNet.HOperatorSet.SetFramegrabberParam(AcqHandle, new HalconDotNet.HTuple("frame_rate"), new HalconDotNet.HTuple(fps));
                rt = true;
            }
            catch (HalconDotNet.HalconException hex)
            {               
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：Halcon模拟相机设置帧率失败!\n错误代码:{0}",hex.GetErrorCode()));
            }
            finally
            {

            }
            return rt;
        }

        protected override bool DoSetTriggerDelay(int lineIdx,float trigdelay)
        {
            bool rt = false;
            try
            {
                if (!AcqHandle.TupleEqual(new HalconDotNet.HTuple()))
                {
                    rt = true;
                }
            }
            catch (HalconDotNet.HalconException hex)
            {

            }
            finally
            {

            }
            return rt;
        }

        protected override bool DoSetDebouncerTime(int idx, float debouncertime)
        {
            throw new NotImplementedException();
        }

        protected override bool DoGetInPut(int idx, out bool onOff)
        {
            throw new NotImplementedException();
        }
        protected override bool DoGetCameraConnectedState(out bool isConnected)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 注册采集异常回调
        /// </summary>
        /// <returns></returns>
        protected override bool DoRegisterExceptionCallBack()
        {
            bool rt = false;
            try
            {
                if (!AcqHandle.TupleEqual(new HalconDotNet.HTuple()))
                {
                    rt = true;
                }
            }
            catch (HalconDotNet.HalconException hex)
            {

            }
            finally
            {

            }
            return rt;
        }

        /// <summary>
        ///  注册采集数据更新回调
        /// </summary>
        /// <returns></returns>
        protected override bool DoRegisterImageGrabbedCallBack()
        {
            bool rt = false;
            try
            {
                if (!AcqHandle.TupleEqual(new HalconDotNet.HTuple()))
                {
                    rt = true;
                }
            }
            catch (HalconDotNet.HalconException hex)
            {

            }
            finally
            {

            }
            return rt;
        }

        public override string ToString()
        {
            return "CameraDriver[SimulateHalcon]";
        }

        #endregion

        void OnImgDataOut()
        {
            if (HoImage != null
                && HoImage.IsInitialized())
            {
                HoImage.Dispose();
            }
                     
            HoImage = _imgData.Clone();

            if (HoImage != null
                  && HoImage.IsInitialized())
            {
                System.Threading.Thread.Sleep(10);
                if (CameraImageGrabbedEvt != null)
                    CameraImageGrabbedEvt(CameraProperty, HoImage);
            }
        }

        protected override bool DoSetOutPut(int idx, bool onOff)
        {
            bool rt = false;
            try
            {

            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：模拟相机设置输出信号失败!\n错误描述:{0}",ex.Message));
            }
            finally
            {
            }
            return rt;
        }

        protected override bool DoCreateCameraSetPage(System.IntPtr windowHandle, string promption)
        {
            return false;
        }

        protected override bool DoShowCameraSetPage()
        {
            return false;
        }

    }
}
