﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basler.Pylon;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       CameraDriver_Basler
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProDriver.Driver
 * File      Name：       CameraDriver_Basler
 * Creating  Time：       10/30/2019 5:58:32 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProDriver.Driver
{
    /// <summary>
    /// 巴斯勒相机SDK二次包装驱动函数接口
    /// [注:二次包装驱动函数接口待测试
    /// 日期:200-06-20]
    /// </summary>
    public class CameraDriver_Basler:CameraDriver
    {
        /// <summary>
        /// if >= Sfnc2_0_0,说明是ＵＳＢ３的相机
        /// </summary>
        public static Version SFNC2_0_0 = new Version(2, 0, 0);
        public override event CameraImageGrabbedDel CameraImageGrabbedEvt; //图像抓取到事件(统一事件)       

        private List<Basler.Pylon.ICameraInfo> _deviceInfoList;  //设备描述信息列表
        private Basler.Pylon.ICameraInfo _deviceInfo;            //当前设备描述信息
        private Basler.Pylon.Camera _deviceRef;                  //当前设备的资源引用
        private long _imageWidth = 0;         // 图像宽
        private long _imageHeight = 0;        // 图像高
        //private System.Diagnostics.Stopwatch _stopWatch;
        private IntPtr _latestFrameAddress;
        private Basler.Pylon.PixelDataConverter _converter;      //像素格式转换       
      
        public CameraDriver_Basler(ProCommon.Communal.CameraProperty cam):base(cam)
        {
            _latestFrameAddress = IntPtr.Zero;
            _converter = new Basler.Pylon.PixelDataConverter();
        }

        /// <summary>
        /// 图像采集到事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeviceStreamGrabber_ImageGrabbedEvent(object sender, Basler.Pylon.ImageGrabbedEventArgs e)
        {
            if (HoImage != null
               && HoImage.IsInitialized())
            {
                HoImage.Dispose();
                System.Threading.Thread.Sleep(10);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                IsImageGrabbed = false;
            }

            #region 巴斯勒相机SDK内部像素格式转换
            Basler.Pylon.IGrabResult grabResult = e.GrabResult;
            if (grabResult.GrabSucceeded)
            {
                Basler.Pylon.PixelType pixelType = grabResult.PixelTypeValue;
                if (pixelType == Basler.Pylon.PixelType.Mono8)
                {
                    //allocate the m_stream_size amount of bytes in non-managed environment 
                    if (_latestFrameAddress == IntPtr.Zero)
                    {
                        _latestFrameAddress = System.Runtime.InteropServices.Marshal.AllocHGlobal((Int32)grabResult.PayloadSize);
                    }

                    _converter.OutputPixelFormat = Basler.Pylon.PixelType.Mono8;
                    _converter.Convert(_latestFrameAddress, grabResult.PayloadSize, grabResult);
                    HalconDotNet.HOperatorSet.GenImage1(out HoImage, "byte", (HalconDotNet.HTuple)grabResult.Width, (HalconDotNet.HTuple)grabResult.Height, (HalconDotNet.HTuple)_latestFrameAddress);
                }
                else if (grabResult.PixelTypeValue == Basler.Pylon.PixelType.BayerBG8
                         || grabResult.PixelTypeValue == Basler.Pylon.PixelType.BayerGB8
                         || grabResult.PixelTypeValue == Basler.Pylon.PixelType.BayerRG8
                         || grabResult.PixelTypeValue == Basler.Pylon.PixelType.BayerGR8
                         || grabResult.PixelTypeValue == Basler.Pylon.PixelType.BayerRG12Packed
                         || grabResult.PixelTypeValue == Basler.Pylon.PixelType.YUV422packed
                         || grabResult.PixelTypeValue == Basler.Pylon.PixelType.YUV422_YUYV_Packed
                         || grabResult.PixelTypeValue == Basler.Pylon.PixelType.BayerRG12
                         )
                {
                    if (_latestFrameAddress == IntPtr.Zero)
                    {
                        _latestFrameAddress = System.Runtime.InteropServices.Marshal.AllocHGlobal((Int32)(3 * grabResult.Width * grabResult.Height));
                    }

                    _converter.OutputPixelFormat = Basler.Pylon.PixelType.BGR8packed;// 根据bayer格式不同切换以下代码                       
                    _converter.Convert(_latestFrameAddress, 3 * grabResult.Width * grabResult.Height, grabResult);
                    HalconDotNet.HOperatorSet.GenImageInterleaved(out HoImage, _latestFrameAddress, "bgr", (HalconDotNet.HTuple)_imageWidth, (HalconDotNet.HTuple)_imageHeight, -1, "byte", (HalconDotNet.HTuple)_imageWidth, (HalconDotNet.HTuple)_imageHeight, 0, 0, -1, 0);
                }

                System.Runtime.InteropServices.Marshal.FreeHGlobal(_latestFrameAddress);
                _latestFrameAddress = IntPtr.Zero;
            }
            #endregion

            if (HoImage != null
                && HoImage.IsInitialized())
            {
                IsImageGrabbed = true;
                if (CameraImageGrabbedEvt != null)
                    CameraImageGrabbedEvt(CameraProperty, HoImage);
            }
        }

        private void Device_ConnectionLost(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 枚举在线相机
        /// </summary>
        /// <returns></returns>
        protected override bool DoEnumerateCameraList()
        {
            bool rt = false;
            try
            {
                System.GC.Collect();              

                if (_deviceInfoList == null)
                    _deviceInfoList = Basler.Pylon.CameraFinder.Enumerate();

                if (_deviceInfoList != null
                    && _deviceInfoList.Count() > 0)
                    rt = true;
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机枚举设备失败!\n错误代码:{0:X8}", 99));
                }
            }
            catch
            {

            }
            finally
            {

            }

            return rt;
        }

        /// <summary>
        /// 计算在线相机数量
        /// </summary>
        /// <returns></returns>
        protected override int DoGetCameraListCount()
        {
            return _deviceInfoList.Count();
        }

        /// <summary>
        /// 根据相机索引获取相机
        /// [相机索引号由其上电顺序得来，非固定]
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override bool DoGetCameraByIdx(int index)
        {
            bool rt = false;
            try
            {
                if (_deviceInfoList != null)
                {
                    if (index >= 0 && index < _deviceInfoList.Count())
                    {
                        foreach (Basler.Pylon.ICameraInfo cameraInfo in _deviceInfoList)
                        {
                            string s = cameraInfo[Basler.Pylon.CameraInfoKey.DeviceIdx];
                            if(!string.IsNullOrEmpty(s))
                            {
                                if (index == System.Convert.ToInt32(s.Substring(2, s.Length - 2), 10))
                                {
                                    _deviceInfo = cameraInfo; //此语句仅仅是为了保持一致性(迈德威视)
                                    _deviceRef = new Basler.Pylon.Camera(cameraInfo);
                                    rt = true;
                                    break;
                                }
                            }                           
                        }

                        if (!rt)
                        {
                            if (DriverExceptionDel != null)
                                DriverExceptionDel(string.Format("错误：巴斯勒相机获取设备失败!\n索引:{0}\n异常描述:{1}",
                                   index, "未找到指定索引相机(字符串形式索引)"));
                        }
                    }
                    else
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：巴斯勒相机获取设备失败!\n索引:{0}\n异常描述:{1}",
                                  index, "超出设备索引范围"));
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机获取索引对应设备失败!\n:{0}",
                               "设备列表为空"));
                }
            }
            catch
            {

            }
            finally
            {

            }

            return rt;
        }

        /// <summary>
        /// 获取索引指定相机的序列号
        /// </summary>
        /// <param name="index">相机索引</param>
        /// <returns></returns>
        protected override string DoGetCameraSN(int index)
        {
            if (DoGetCameraByIdx(index))
            {
                if (!_deviceRef.IsConnected)
                    DoOpen();
                return _deviceRef.CameraInfo[Basler.Pylon.CameraInfoKey.SerialNumber];
            }

            return string.Empty;
        }

        /// <summary>
        /// 根据相机名称获取相机
        /// </summary>
        /// <param name="camName"></param>
        /// <returns></returns>
        protected override bool DoGetCameraByName(string camName)
        {
            bool rt = false;
            try
            {
                if (_deviceInfoList != null)
                {
                    foreach (Basler.Pylon.ICameraInfo cameraInfo in _deviceInfoList)
                    {
                        if (camName == cameraInfo[Basler.Pylon.CameraInfoKey.UserDefinedName])
                        {
                            _deviceInfo = cameraInfo;//此语句仅仅是为了保持一致性(迈德威视)
                            _deviceRef = new Basler.Pylon.Camera(cameraInfo);
                            rt = true;
                            break;
                        }
                    }

                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：巴斯勒相机获取设备失败!\n设备名称:{0}\n异常描述{1}",
                                camName, "指定名称不匹配"));
                    }                   
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机获取索引对应设备失败!\n:{0}",
                               "设备列表为空"));
                }
            }
            catch
            {

            }
            finally
            {

            }

            return rt;
        }

        /// <summary>
        /// 根据相机序列号获取相机
        /// </summary>
        /// <param name="camSN"></param>
        /// <returns></returns>
        protected override bool DoGetCameraBySN(string camSN)
        {
            bool rt = false;
            try
            {
                if (_deviceInfoList != null)
                {
                    foreach (Basler.Pylon.ICameraInfo cameraInfo in _deviceInfoList)
                    {
                        if (camSN == cameraInfo[Basler.Pylon.CameraInfoKey.SerialNumber])
                        {
                            _deviceInfo = cameraInfo;//此语句仅仅是为了保持一致性(迈德威视)
                            _deviceRef = new Basler.Pylon.Camera(cameraInfo);
                            rt = true;
                            break;
                        }
                    }
                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：巴斯勒相机获取设备失败!\n设备序列号:{0}\n异常描述{1}",
                                camSN, "指定序列号不匹配"));
                    }                   
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机获取索引对应设备失败!\n:{0}",
                               "设备列表为空"));
                }
            }
            catch
            {

            }
            finally
            {

            }

            return rt;
        }

        /// <summary>
        /// 打开相机
        /// </summary>
        /// <returns></returns>
        protected override bool DoOpen()
        {
            bool rt = false;
            try
            {
                if (_deviceRef != null)
                {
                    if(!_deviceRef.IsOpen)
                        _deviceRef.Open();

                    if(_deviceRef.IsOpen)
                    {
                        _deviceRef.Parameters[Basler.Pylon.PLCameraInstance.MaxNumBuffer].TrySetValue(10);             //设置内存中接收图像缓冲区大小
                        if (_deviceRef.GetSfncVersion() < SFNC2_0_0)
                        {
                            _deviceRef.Parameters[Basler.Pylon.PLGigECamera.GevHeartbeatTimeout].TrySetValue(1000);    //设置网口相机的心跳时间1000毫秒                         
                            _imageWidth = _deviceRef.Parameters[Basler.Pylon.PLCamera.Width].GetValue();               // 获取图像宽 
                            _imageHeight = _deviceRef.Parameters[Basler.Pylon.PLCamera.Height].GetValue();             // 获取图像高  
                        }
                        else
                        {
                            _imageWidth = _deviceRef.Parameters[Basler.Pylon.PLUsbCamera.Width].GetValue();
                            _imageHeight = _deviceRef.Parameters[Basler.Pylon.PLUsbCamera.Height].GetValue();
                        }

                        rt = true;
                    } 
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机打开失败!\n:{0}",
                        "设备未连接"));
                }
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：巴斯勒相机打开失败!\n错误描述:{0}",
                       ex.Message));
            }
            finally
            {

            }

            return rt;
        }

        /// <summary>
        /// 关闭相机
        /// </summary>
        /// <returns></returns>
        protected override bool DoClose()
        {
            bool rt = false;
            try
            {
                if (_deviceRef != null)
                {
                    SetFreeRun();                   
                    _deviceRef.ConnectionLost -= Device_ConnectionLost;
                    _deviceRef.StreamGrabber.ImageGrabbed -= DeviceStreamGrabber_ImageGrabbedEvent;
                    _deviceRef.Close();
                    _deviceRef.Dispose();
                    rt = true;
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机关闭失败!\n:{0}",
                        "设备未连接"));
                }
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：巴斯勒相机关闭失败!\n错误描述:{0}",
                       ex.Message));
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
        /// <param name="frameNum"></param>
        /// <returns></returns>
        protected override bool DoSetAcquisitionMode(ProCommon.Communal.AcquisitionMode acqmode, uint frameNum)
        {
            bool rt = false;
            try
            {
                if (_deviceRef != null)
                {
                    DoStopGrab();
                    switch (acqmode)
                    {
                        case ProCommon.Communal.AcquisitionMode.Continue:
                            if (SetFreeRun())
                                rt = SetContinueRun();
                            break;
                        case ProCommon.Communal.AcquisitionMode.ExternalTrigger:
                            //外触发时的默认设置
                            if (SetExternalTrigger(1,1,50))
                                rt = SetFrameNumber(frameNum);
                            break;
                        case ProCommon.Communal.AcquisitionMode.SoftTrigger:
                            if (SetInternalTrigger())
                                rt = SetFrameNumber(frameNum);
                            break;
                        default: break;
                    }

                    if (rt)
                    {
                        System.Threading.Thread.Sleep(10);
                        DoStartGrab();
                    }
                }
                else
                { 
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机设置设备采集模式失败!\n:{0}", "设备未连接"));
                }
            }
            catch
            {

            }
            finally
            {

            }

            return rt;
        }

        /// <summary>
        /// 自由采集模式
        /// [不采集]
        /// </summary>
        /// <returns></returns>
        private bool SetFreeRun()
        {
            bool rt = false;
            if (_deviceRef.GetSfncVersion() < SFNC2_0_0)
            {
                if (_deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.AcquisitionStart))
                {
                    if (_deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.FrameStart))
                    {
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.AcquisitionStart);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerMode].TrySetValue(Basler.Pylon.PLCamera.TriggerMode.Off);

                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.FrameStart);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerMode].TrySetValue(Basler.Pylon.PLCamera.TriggerMode.Off);                        
                    }
                    else
                    {
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.AcquisitionStart);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerMode].TrySetValue(Basler.Pylon.PLCamera.TriggerMode.Off);                       
                    }
                    rt = true;
                }
                else
                {                   
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机设置设备自由采集模式失败!\n:{0}", "获取配置采集开启的触发器异常"));
                }
            }
            else
            {
                if (_deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.FrameBurstStart))
                {
                    if (_deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.FrameStart))
                    {
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.FrameBurstStart);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerMode].TrySetValue(Basler.Pylon.PLCamera.TriggerMode.Off);

                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.FrameStart);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerMode].TrySetValue(Basler.Pylon.PLCamera.TriggerMode.Off);                       
                    }
                    else
                    {
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.FrameBurstStart);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerMode].TrySetValue(Basler.Pylon.PLCamera.TriggerMode.Off);                       
                    }
                    rt = true;
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机设置设备自由采集模式失败!\n:{0}", "获取配置采集开启的触发器异常"));
                }
            }
            //if (rt) _stopWatch.Reset();
            return rt;
        }

        /// <summary>
        /// 连续采集模式
        /// </summary>
        /// <returns></returns>
        private bool SetContinueRun()
        {
            bool rt = false;
            try
            {
                if (_deviceRef != null)
                {
                    _deviceRef.Parameters[Basler.Pylon.PLCamera.AcquisitionMode].TrySetValue(Basler.Pylon.PLCamera.AcquisitionMode.Continuous);
                    rt = true;
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机设置连续采集失败!\n:{0}", "设备未连接"));
                }
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：巴斯勒相机设置连续采集失败!\n错误描述:{0}", ex.Message));
            }
            finally
            {

            }
            return rt;
        }

        /// <summary>
        /// 设置内部触发采集(软触发)
        /// </summary>
        /// <returns></returns>
        private bool SetInternalTrigger()
        {
            bool rt = false;
            if (_deviceRef.GetSfncVersion() < SFNC2_0_0)
            {
                if (_deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.AcquisitionStart))
                {
                    if (_deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.FrameStart))
                    {
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.AcquisitionStart);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerMode].TrySetValue(Basler.Pylon.PLCamera.TriggerMode.Off);

                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.FrameStart);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerMode].TrySetValue(Basler.Pylon.PLCamera.TriggerMode.On);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSource].TrySetValue(Basler.Pylon.PLCamera.TriggerSource.Software);
                    }
                    else
                    {
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.AcquisitionStart);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerMode].TrySetValue(Basler.Pylon.PLCamera.TriggerMode.On);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSource].TrySetValue(Basler.Pylon.PLCamera.TriggerSource.Software);
                    }
                    rt = true;
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机设置设备内触发采集模式失败!\n:{0}", "获取配置采集开启的触发器异常"));
                }
            }
            else
            {
                if (_deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.FrameBurstStart))
                {
                    if (_deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.FrameStart))
                    {
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.FrameBurstStart);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerMode].TrySetValue(Basler.Pylon.PLCamera.TriggerMode.Off);

                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.FrameStart);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerMode].TrySetValue(Basler.Pylon.PLCamera.TriggerMode.On);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSource].TrySetValue(Basler.Pylon.PLCamera.TriggerSource.Software);
                    }
                    else
                    {
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.FrameBurstStart);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerMode].TrySetValue(Basler.Pylon.PLCamera.TriggerMode.On);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSource].TrySetValue(Basler.Pylon.PLCamera.TriggerSource.Software);
                    }
                    rt = true; 
                }
                else
                {                  
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机设置设备内触发采集模式失败!\n:{0}", "获取配置采集开启的触发器异常"));
                }
            }
            //if(rt) _stopWatch.Reset();
            return rt;
        }

        /// <summary>
        /// 设置外部触发采集(硬触发)
        /// </summary>
        /// <param name="lineIdx">外触发输入端口</param>
        /// <param name="delaytime">外触发延时,单位毫秒</param>
        /// <param name="debouncertime">外触发消抖时间,单位微秒</param>
        /// <returns></returns>
        private bool SetExternalTrigger(int lineIdx,float delaytime,float debouncertime)
        {
            bool rt = false;
            string lineSource = Basler.Pylon.PLCamera.LineSelector.Line1;
            switch (lineIdx)
            {
                case 2:
                    lineSource = Basler.Pylon.PLCamera.LineSelector.Line2;
                    break;
                case 3:
                    lineSource = Basler.Pylon.PLCamera.LineSelector.Line3;
                    break;
                case 4:
                    lineSource = Basler.Pylon.PLCamera.LineSelector.Line4;
                    break;
                case 5:
                    lineSource = Basler.Pylon.PLCamera.LineSelector.Line5;
                    break;
                case 6:
                    lineSource = Basler.Pylon.PLCamera.LineSelector.Line6;
                    break;
                case 7:
                    lineSource = Basler.Pylon.PLCamera.LineSelector.Line7;
                    break;
                case 8:
                    lineSource = Basler.Pylon.PLCamera.LineSelector.Line8;
                    break;
                default:
                    break;
            }

            if (_deviceRef.GetSfncVersion() < SFNC2_0_0)
            {
                if (_deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.AcquisitionStart))
                {
                    if (_deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.FrameStart))
                    {
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.AcquisitionStart);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerMode].TrySetValue(Basler.Pylon.PLCamera.TriggerMode.Off);

                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.FrameStart);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerMode].TrySetValue(Basler.Pylon.PLCamera.TriggerMode.On);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSource].TrySetValue(Basler.Pylon.PLCamera.TriggerSource.Line1);
                    }
                    else
                    {
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.AcquisitionStart);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerMode].TrySetValue(Basler.Pylon.PLCamera.TriggerMode.On);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSource].TrySetValue(Basler.Pylon.PLCamera.TriggerSource.Line1);
                    }
                    rt = true;
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机设置设备外触发采集模式失败!\n:{0}", "获取配置采集开启的触发器异常"));
                }
            }
            else
            {
                if (_deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.FrameBurstStart))
                {
                    if (_deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.FrameStart))
                    {
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.FrameBurstStart);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerMode].TrySetValue(Basler.Pylon.PLCamera.TriggerMode.Off);

                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.FrameStart);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerMode].TrySetValue(Basler.Pylon.PLCamera.TriggerMode.On);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSource].TrySetValue(Basler.Pylon.PLCamera.TriggerSource.Line1);
                    }
                    else
                    {
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSelector].TrySetValue(Basler.Pylon.PLCamera.TriggerSelector.FrameBurstStart);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerMode].TrySetValue(Basler.Pylon.PLCamera.TriggerMode.On);
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerSource].TrySetValue(Basler.Pylon.PLCamera.TriggerSource.Line1);
                    }
                    rt = true;                    
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机设置设备外触发采集模式失败!\n:{0}", "获取配置采集开启的触发器异常"));
                }
            }

            //if (rt) _stopWatch.Reset();
            return rt;
        }

        /// <summary>
        /// 设置触发采集时的帧数
        /// </summary>
        /// <param name="frameNum"></param>
        /// <returns></returns>
        private bool SetFrameNumber(uint frameNum)
        {
            bool rt = false;
            try
            {
                if (frameNum == 1)
                {
                    //_device.Parameters[Basler.Pylon.PLCamera.AcquisitionMode].SetValue(Basler.Pylon.PLCamera.AcquisitionMode.SingleFrame);
                    rt = true;

                }
                else
                {
                    //_device.Parameters[Basler.Pylon.PLCamera.AcquisitionMode].SetValue(Basler.Pylon.PLCamera.AcquisitionMode.MultiFrame);
                    rt = true;
                }
                rt = true;
            }catch(Exception ex)
            {

            }
           
            return rt;
        }

        /// <summary>
        /// 方法:设置触发信号边缘
        /// [注:用于触发源为硬触发]
        /// </summary>
        /// <param name="dege">边缘信号</param>
        /// <returns></returns>
        protected override bool DoSetTriggerActivation(ProCommon.Communal.EffectiveSignal edge)
        {
            bool rt = false;
            try
            {
                if (_deviceRef != null)
                {
                    switch (edge)
                    {
                        case ProCommon.Communal.EffectiveSignal.FallEdge:
                            _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerActivation].TrySetValue(Basler.Pylon.PLCamera.TriggerActivation.FallingEdge);
                            rt = true;
                            break;
                        case ProCommon.Communal.EffectiveSignal.LogicFalse:
                            //注意此处巴斯勒用的是高低电平，非有效逻辑或无效逻辑(按照国内习惯,高电平为无效电平)
                            _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerActivation].TrySetValue(Basler.Pylon.PLCamera.TriggerActivation.LevelHigh);
                            rt = true;
                            break;
                        case ProCommon.Communal.EffectiveSignal.LogicTrue:
                            //注意此处巴斯勒用的是高低电平，非有效逻辑或无效逻辑(按照国内习惯,低电平为有效电平)
                            _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerActivation].TrySetValue(Basler.Pylon.PLCamera.TriggerActivation.LevelLow);
                            rt = true;
                            break;
                        case ProCommon.Communal.EffectiveSignal.RaiseEdge:
                            _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerActivation].TrySetValue(Basler.Pylon.PLCamera.TriggerActivation.RisingEdge);
                            rt = true;
                            break;
                        default:break;
                    }                  
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机设置设备触发信号边沿失败!\n:{0}","设备未连接"));
                }
            }
            catch (System.Exception ex)
            {

            }
            finally
            {

            }

            return rt;
        }

        /// <summary>
        /// 开启采集
        /// </summary>
        /// <returns></returns>
        protected override bool DoStartGrab()
        {
            bool rt = false;
            try
            {
                if (_deviceRef != null)
                {
                    if (!_deviceRef.StreamGrabber.IsGrabbing)
                    {
                        _deviceRef.StreamGrabber.Start(Basler.Pylon.GrabStrategy.LatestImages, Basler.Pylon.GrabLoop.ProvidedByStreamGrabber);
                        /*_stopWatch.Restart();*/    // ****  重启采集时间计时器   ****
                    }
                    rt = true;
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机开启采集失败!\n:{0}","设备未连接"));
                }
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：巴斯勒相机开启采集失败!\n错误描述:{0}",ex.Message));
            }
            finally
            {

            }

            return rt;
        }

        /// <summary>
        /// 暂停采集
        /// </summary>
        /// <returns></returns>
        protected override bool DoPauseGrab()
        {
            bool rt = false;
            try
            {
                if (_deviceRef != null)
                {
                    rt = true; //巴斯勒相机暂停采集(无此功能)
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机暂停采集失败!\n:{0}","设备未连接"));
                }
            }
            catch
            {

            }
            finally
            {

            }

            return rt;
        }

        /// <summary>
        /// 停止采集
        /// </summary>
        /// <returns></returns>
        protected override bool DoStopGrab()
        {
            bool rt = false;
            try
            {
                if (_deviceRef != null)
                {
                    if (_deviceRef.StreamGrabber.IsGrabbing)
                    {
                        _deviceRef.StreamGrabber.Stop();
                    }
                    rt = true;
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机停止采集失败!\n:{0}","设备未连接"));
                }
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：巴斯勒相机停止采集失败!\n错误描述:{0}",ex.Message));
            }
            finally
            {

            }

            return rt;
        }

        /// <summary>
        /// 软触发一次
        /// </summary>
        /// <returns></returns>
        protected override bool DoSoftTriggerOnce()
        {
            bool rt = false;
            try
            {
                if (_deviceRef != null)
                {
                    if (_deviceRef.WaitForFrameTriggerReady(1000, Basler.Pylon.TimeoutHandling.ThrowException))
                    {
                        _deviceRef.ExecuteSoftwareTrigger();
                        //_stopWatch.Restart();
                        rt = true;
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机单次软触发采集失败!\n:{0}","设备未连接"));
                }
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：巴斯勒相机单次软触发采集失败!\n错误描述:{0}",ex.Message));
            }
            finally
            {

            }

            return rt;
        }

        /// <summary>
        /// 注册掉线异常回调函数
        /// </summary>
        /// <returns></returns>
        protected override bool DoRegisterExceptionCallBack()
        {
            bool rt = false;
            try
            {
                if (_deviceRef != null)
                {
                    _deviceRef.ConnectionLost += Device_ConnectionLost;
                    rt = true; //巴斯勒相机断线重连功能
                }
            }
            catch (System.Exception ex)
            {

            }
            finally
            {

            }

            return rt;
        }

        /// <summary>
        /// 注册图像采集到回调函数
        /// </summary>
        /// <returns></returns>
        protected override bool DoRegisterImageGrabbedCallBack()
        {
            bool rt = false;
            try
            {
                if (_deviceRef != null)
                {
                    _deviceRef.StreamGrabber.ImageGrabbed += DeviceStreamGrabber_ImageGrabbedEvent; // 绑定采集回调函数
                    rt = true; 
                }
                else
                {                  
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机注册图像采集到事件失败!\n:{0}","设备未连接"));
                }
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：巴斯勒相机注册图像采集到事件失败!\n错误描述:{0}",ex.Message));
            }
            finally
            {

            }

            return rt;
        }

        /// <summary>
        /// 设置曝光时间      
        /// </summary>
        /// <param name="exposuretime">曝光时间,单位毫秒</param>
        /// <returns></returns>
        protected override bool DoSetExposureTime(float exposuretime)
        {
            bool rt = false;
            try
            {
                if (_deviceRef != null)
                {
                    _deviceRef.Parameters[Basler.Pylon.PLCamera.ExposureAuto].TrySetValue(Basler.Pylon.PLCamera.ExposureAuto.Off);
                    _deviceRef.Parameters[Basler.Pylon.PLCamera.ExposureMode].TrySetValue(Basler.Pylon.PLCamera.ExposureMode.Timed);
                    if (_deviceRef.GetSfncVersion() < SFNC2_0_0)
                    {
                        //SDK-时间单位微秒,毫秒转成微秒
                        long exptL = Convert.ToInt64(exposuretime)*1000;                
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.ExposureTimeAbs].SetValue(exptL);
                    }
                    else
                    {
                        //SDK-时间单位微秒,毫秒转成微秒
                        double exptD = Convert.ToDouble(exposuretime)*1000;
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.ExposureTime].SetValue(exptD);
                    }                                        
                    rt = true;                   
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机设置曝光时间失败!\n:{0}","设备未连接"));
                }
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：巴斯勒相机设置曝光时间失败!\n错误描述:{0}",ex.Message));
            }
            finally
            {

            }

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
                if (_deviceRef != null)
                {
                    
                    if (_deviceRef.GetSfncVersion() < SFNC2_0_0)
                    {
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.AcquisitionFrameRateAbs].SetValue((double)fps);
                    }
                    else
                    {
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.AcquisitionFrameRate].SetValue((double)fps);                       
                    }
                    _deviceRef.Parameters[Basler.Pylon.PLCamera.AcquisitionFrameRateEnable].SetValue(true);
                    rt = true;
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机设置帧率失败!\n:{0}","设备未连接"));
                }               
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：巴斯勒相机设置帧率失败!\n错误描述:{0}",ex.Message));
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
                if (_deviceRef != null)
                {
                    _deviceRef.Parameters[Basler.Pylon.PLCamera.GainAuto].TrySetValue(Basler.Pylon.PLCamera.GainAuto.Off);
                    if (_deviceRef.GetSfncVersion() < SFNC2_0_0)
                    {
                        _deviceRef.Parameters[Basler.Pylon.PLCamera.GainRaw].SetValue((long)gain);
                    }
                    else
                    {
                        _deviceRef.Parameters[Basler.Pylon.PLUsbCamera.Gain].SetValue((double)gain);
                    }
                    
                    rt = true;
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机设置增益失败!\n:{0}","设备未连接"));
                }
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：巴斯勒相机设置增益败!\n错误描述:{0}",ex.Message));
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
        /// 设置触发延时
        /// </summary>
        /// <param name="idx">外触发端口</param>
        /// <param name="trigdelay">触发延迟时间,单位毫秒</param>
        /// <returns></returns>
        protected override bool DoSetTriggerDelay(int idx,float trigdelay)
        {
            bool rt = false;
            try
            {
                if (_deviceRef != null)
                {
                    string lineSource = Basler.Pylon.PLCamera.LineSelector.Line1;
                    switch (idx)
                    {
                        case 2:
                            lineSource = Basler.Pylon.PLCamera.LineSelector.Line2;
                            break;
                        case 3:
                            lineSource = Basler.Pylon.PLCamera.LineSelector.Line3;
                            break;
                        case 4:
                            lineSource = Basler.Pylon.PLCamera.LineSelector.Line4;
                            break;
                        case 5:
                            lineSource = Basler.Pylon.PLCamera.LineSelector.Line5;
                            break;
                        case 6:
                            lineSource = Basler.Pylon.PLCamera.LineSelector.Line6;
                            break;
                        case 7:
                            lineSource = Basler.Pylon.PLCamera.LineSelector.Line7;
                            break;
                        case 8:
                            lineSource = Basler.Pylon.PLCamera.LineSelector.Line8;
                            break;
                        default:                          
                            break;
                    }

                    _deviceRef.Parameters[Basler.Pylon.PLCamera.LineSelector].TrySetValue(lineSource);
                    _deviceRef.Parameters[Basler.Pylon.PLCamera.LineMode].TrySetValue(Basler.Pylon.PLCamera.LineMode.Input);
                    //SDK--Sets the trigger delay time in microseconds.
                    trigdelay *= 1000;
                    _deviceRef.Parameters[Basler.Pylon.PLCamera.TriggerDelay].SetValue(trigdelay);  
                    rt = true;
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机设置触发延迟时间失败!\n:{0}","设备未连接"));
                }
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：巴斯勒相机设置触发延迟时间失败!\n错误描述:{0}",ex.Message));
            }
            finally
            {

            }

            return rt;
        }

        /// <summary>
        /// 设置消抖时间
        /// </summary>
        /// <param name="idx"></param>
        /// <param name="debouncertime">消抖时间,单位微秒</param>
        /// <returns></returns>
        protected override bool DoSetDebouncerTime(int idx, float debouncertime)
        {
            bool rt = false;
            string lineSource = Basler.Pylon.PLCamera.LineSelector.Line1;
            switch (idx)
            {
                case 2:
                    lineSource = Basler.Pylon.PLCamera.LineSelector.Line2;
                    break;
                case 3:
                    lineSource = Basler.Pylon.PLCamera.LineSelector.Line3;
                    break;
                case 4:
                    lineSource = Basler.Pylon.PLCamera.LineSelector.Line4;
                    break;
                case 5:
                    lineSource = Basler.Pylon.PLCamera.LineSelector.Line5;
                    break;
                case 6:
                    lineSource = Basler.Pylon.PLCamera.LineSelector.Line6;
                    break;
                case 7:
                    lineSource = Basler.Pylon.PLCamera.LineSelector.Line7;
                    break;
                case 8:
                    lineSource = Basler.Pylon.PLCamera.LineSelector.Line8;
                    break;
                default:
                    break;
            }

            #region 未确定是否区分GigE或USB3.0
            /*
            if (_deviceRef.GetSfncVersion() < SFNC2_0_0)
            {
                //Sets the absolute value of the selected line debouncer time in microseconds
                _deviceRef.Parameters[Basler.Pylon.PLCamera.LineSelector].TrySetValue(lineSource);
                _deviceRef.Parameters[Basler.Pylon.PLCamera.LineMode].TrySetValue(Basler.Pylon.PLCamera.LineMode.Input);
                _deviceRef.Parameters[Basler.Pylon.PLCamera.LineDebouncerTimeAbs].SetValue(debouncertime);       // 设置去抖延时(微妙)过滤触发信号干扰
                rt = true;
            }
            else
            {
                //Sets the absolute value of the selected line debouncer time in microseconds
                _deviceRef.Parameters[Basler.Pylon.PLCamera.LineSelector].TrySetValue(lineSource);
                _deviceRef.Parameters[Basler.Pylon.PLCamera.LineMode].TrySetValue(Basler.Pylon.PLCamera.LineMode.Input);
                _deviceRef.Parameters[Basler.Pylon.PLCamera.LineDebouncerTime].SetValue(debouncertime);       // 设置去抖延时，过滤触发信号干扰 
                rt = true;
            }
            */

            #endregion
            try
            {
                //Sets the absolute value of the selected line debouncer time in microseconds
                _deviceRef.Parameters[Basler.Pylon.PLCamera.LineSelector].TrySetValue(lineSource);
                _deviceRef.Parameters[Basler.Pylon.PLCamera.LineMode].TrySetValue(Basler.Pylon.PLCamera.LineMode.Input);
                _deviceRef.Parameters[Basler.Pylon.PLCamera.LineDebouncerTimeAbs].SetValue(debouncertime);       // 设置去抖延时(微妙)过滤触发信号干扰
                rt = true;
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：巴斯勒相机设置消抖时间失败!\n错误描述:{0}", ex.Message));
            }
            finally
            {

            }

            return rt;
        }

        /// <summary>
        /// 获取相机输入端口状态
        /// [待测试_xYz_2020-06-19]
        /// </summary>
        /// <param name="idx"></param>
        /// <param name="onOff"></param>
        /// <returns></returns>
        protected override bool DoGetInPut(int idx, out bool onOff)
        {
            bool rt = false; onOff = false;
            string lineSource = Basler.Pylon.PLCamera.LineSelector.Line1;
            switch (idx)
            {
                case 2:
                    lineSource = Basler.Pylon.PLCamera.LineSelector.Line2;
                    break;
                case 3:
                    lineSource = Basler.Pylon.PLCamera.LineSelector.Line3;
                    break;
                case 4:
                    lineSource = Basler.Pylon.PLCamera.LineSelector.Line4;
                    break;
                case 5:
                    lineSource = Basler.Pylon.PLCamera.LineSelector.Line5;
                    break;
                case 6:
                    lineSource = Basler.Pylon.PLCamera.LineSelector.Line6;
                    break;
                case 7:
                    lineSource = Basler.Pylon.PLCamera.LineSelector.Line7;
                    break;
                case 8:
                    lineSource = Basler.Pylon.PLCamera.LineSelector.Line8;
                    break;
                default:                  
                    break;
            }

            #region 未确定是否区分GigE或USB3.0
            /*
            if (_deviceRef.GetSfncVersion() < SFNC2_0_0)
            {
                _deviceRef.Parameters[Basler.Pylon.PLCamera.LineSelector].TrySetValue(lineSource);
                _deviceRef.Parameters[Basler.Pylon.PLCamera.LineMode].TrySetValue(Basler.Pylon.PLCamera.LineMode.Input);
                onOff= _deviceRef.Parameters[Basler.Pylon.PLCamera.LineStatus].GetValue();  
                rt = true;
            }
            else
            {
                _deviceRef.Parameters[Basler.Pylon.PLCamera.LineSelector].TrySetValue(lineSource);
                _deviceRef.Parameters[Basler.Pylon.PLCamera.LineMode].TrySetValue(Basler.Pylon.PLCamera.LineMode.Input);
                onOff = _deviceRef.Parameters[Basler.Pylon.PLCamera.LineStatus].GetValue();
                rt = true;
            }

            */
            #endregion

            try
            {
                _deviceRef.Parameters[Basler.Pylon.PLCamera.LineSelector].TrySetValue(lineSource);
                _deviceRef.Parameters[Basler.Pylon.PLCamera.LineMode].TrySetValue(Basler.Pylon.PLCamera.LineMode.Input);
                onOff = _deviceRef.Parameters[Basler.Pylon.PLCamera.LineStatus].GetValue();
                rt = true;
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：巴斯勒相机获取端口状态失败!\n错误描述:{0}", ex.Message));
            }
            finally
            {

            }
            return rt;
        }

        /// <summary>
        /// 获取相机的连接状态
        /// </summary>
        /// <param name="isConnected"></param>
        /// <returns></returns>
        protected override bool DoGetCameraConnectedState(out bool isConnected)
        {
            bool rt = false; isConnected = false;
            try
            {
                if (_deviceRef != null)
                {
                    if (_deviceRef.GetSfncVersion() < SFNC2_0_0)
                    {
                        //获取相机的连接状态
                       // _deviceRef.Parameters[Basler.Pylon.PLCamera.];
                    }
                    else
                    {
                        //_deviceRef.Parameters[Basler.Pylon.PLUsbCamera.Gain];
                    }

                    rt = true;
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：巴斯勒相机获取连接状态失败!\n:{0}", "设备未连接"));
                }
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：巴斯勒相机获取连接状态失败!\n错误描述:{0}", ex.Message));
            }
            finally
            {

            }

            return rt;
        }

        /// <summary>
        /// 设置相机的输出信号
        /// </summary>
        /// <param name="idx">输出端口号</param>
        /// <param name="onOff">true--导通,false--断开</param>
        /// <returns></returns>
        protected override bool DoSetOutPut(int idx,bool onOff)
        {
            bool rt = false;
            try
            {
                if(_deviceRef!=null)
                {
                    string outPutSource = Basler.Pylon.PLCamera.UserOutputSelector.UserOutput0;
                    switch (idx)
                    {
                        case 1:
                            outPutSource = Basler.Pylon.PLCamera.UserOutputSelector.UserOutput1;
                            break;
                        case 2:
                            outPutSource = Basler.Pylon.PLCamera.UserOutputSelector.UserOutput2;
                            break;
                        case 3:
                            outPutSource = Basler.Pylon.PLCamera.UserOutputSelector.UserOutput3;
                            break;
                        case 4:
                            outPutSource = Basler.Pylon.PLCamera.UserOutputSelector.UserOutput4;
                            break;
                        case 5:
                            outPutSource = Basler.Pylon.PLCamera.UserOutputSelector.UserOutput5;
                            break;
                        case 6:
                            outPutSource = Basler.Pylon.PLCamera.UserOutputSelector.UserOutput6;
                            break;
                        case 7:
                            outPutSource = Basler.Pylon.PLCamera.UserOutputSelector.UserOutput7;
                            break;
                        case 8:
                            outPutSource = Basler.Pylon.PLCamera.UserOutputSelector.UserOutput8;
                            break;
                        default:                           
                            break;
                    }

                    _deviceRef.Parameters[Basler.Pylon.PLCamera.UserOutputSelector].TrySetValue(outPutSource);
                    _deviceRef.Parameters[Basler.Pylon.PLCamera.UserOutputValue].SetValue(onOff);
                    rt = true;                  
                }
            }
            catch(System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：巴斯勒相机设置输出信号失败!\n错误描述:{0}",ex.Message));
            }
            finally
            {
            }
            return rt;
        }

        /// <summary>
        /// 创建参数设置窗口
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <param name="promption"></param>
        /// <returns></returns>
        protected override bool DoCreateCameraSetPage(System.IntPtr windowHandle, string promption)
        {
            bool rt = false;
            try
            {

            }
            catch (System.Exception ex)
            {

            }
            finally
            {

            }

            return rt;
        }

        /// <summary>
        /// 显示参数设置窗口
        /// </summary>
        /// <returns></returns>
        protected override bool DoShowCameraSetPage()
        {
            bool rt = false;
            try
            {

            }
            catch (System.Exception ex)
            {

            }
            finally
            {

            }

            return rt;
        }

        public override string ToString()
        {
            return "CameraDriver[Basler]";
        }
    }
}
