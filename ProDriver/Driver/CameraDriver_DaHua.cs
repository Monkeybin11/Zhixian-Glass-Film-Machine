using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       CameraDriver_DaHua
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProDriver.Driver
 * File      Name：       CameraDriver_DaHua
 * Creating  Time：       2/22/2020 11:52:11 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProDriver.Driver
{
    /// <summary>
    /// 大华(奥普特)相机SDK二次包装驱动函数接口
    /// [注:二次包装驱动函数接口待完善
    /// 日期:200-06-20]
    /// </summary>
    class CameraDriver_DaHua : CameraDriver
    {
        public override event CameraImageGrabbedDel CameraImageGrabbedEvt; //图像抓取到事件(统一事件)

        System.Collections.Generic.List<ThridLibray.IDeviceInfo> _deviceInfoList;  //设备描述信息列表
        private ThridLibray.IDeviceInfo _deviceInfo;                               //当前设备描述信息
        private ThridLibray.IDevice _deviceRef;                                    //当前设备的资源引用         

        public CameraDriver_DaHua(ProCommon.Communal.CameraProperty cam) : base(cam)
        {
           
        }

        /// <summary>
        /// 图像采集到事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StreamGrabber_ImageGrabbed(object sender, ThridLibray.GrabbedEventArgs e)
        {
            if (HoImage != null
              && HoImage.IsInitialized())
            {
                HoImage.Dispose();
                System.Threading.Thread.Sleep(10);
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                IsImageGrabbed = false;
            }

            #region 大华相机SDK内部像素格式转换
            ThridLibray.IGrabbedRawData frame = e.GrabResult.Clone();
            if(frame!=null)
            {
                System.Drawing.Bitmap btmp=null;
                if (frame.PixelFmt==ThridLibray.GvspPixelFormatType.gvspPixelMono8
                    || frame.PixelFmt == ThridLibray.GvspPixelFormatType.gvspPixelMono10)
                    btmp = frame.ToBitmap(false);
                else if(frame.PixelFmt == ThridLibray.GvspPixelFormatType.gvspPixelRGB8
                    || frame.PixelFmt == ThridLibray.GvspPixelFormatType.gvspPixelRGB10)
                {
                    btmp = frame.ToBitmap(true);
                }

                if(btmp!=null)
                {
                    BitmapToHObject(btmp, out HoImage);
                    btmp.Dispose();
                }
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

        /// <summary>
        /// 相机断连
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _deviceRef_ConnectionLost(object sender, EventArgs e)
        {

        }

        #region 实现抽象函数

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
                _deviceInfoList = ThridLibray.Enumerator.EnumerateDevices();

                if(_deviceInfoList!=null)
                    rt = true;

                if (!rt)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机枚举设备失败!\n错误代码:{0:X8}",99));
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
            return (int)_deviceInfoList.Count;
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
                if (_deviceInfoList.Count > 0)
                {
                    if (index < _deviceInfoList.Count)
                    {
                        _deviceRef = ThridLibray.Enumerator.GetDeviceByIndex(index);

                        if (_deviceRef!=null)
                            rt = true;
                    }
                    else
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大华相机获取设备失败!\n索引:{0}\n异常描述:{1}", index, "超出设备索引范围"));
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机获取设备失败!\n索引:{0}\n异常描述:{1}", index, "设备列表空"));
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
        /// 获取索引指定相机的名称
        /// </summary>
        /// <param name="index">相机索引</param>
        /// <returns></returns>
        protected override string DoGetCameraSN(int index)
        {
            string strRT = string.Empty;
            try
            {
                if (_deviceInfoList.Count > 0)
                {
                    if (index>=0
                        && (index < _deviceInfoList.Count))
                    {
                        strRT=_deviceInfoList[index].SerialNumber;
                    }
                    else
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大华相机获取设备失败!\n索引:{0}\n异常描述:{1}", index, "超出设备索引范围"));
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机获取设备失败!\n索引:{0}\n异常描述:{1}", index, "设备列表空"));
                }
            }
            catch
            {

            }
            finally
            {
            }

            return strRT;
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
                if (_deviceInfoList.Count > 0)
                {
                    ThridLibray.IDeviceInfo tmpdevice;
                    for (int i = 0; i < _deviceInfoList.Count; i++)
                    {
                        tmpdevice = _deviceInfoList[i];
                        if(tmpdevice.Name== camName)
                        {
                            _deviceInfo = tmpdevice;
                            if (DoGetCameraByIdx(i))
                            {
                                rt = true;
                                break;
                            }
                        }
                    }

                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大华相机获取设备失败!\n设备名称:{0}\n异常描述:{1}", camName, "指定名称不匹配"));
                    }
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
        /// 根据相机SN地址获取相机
        /// </summary>
        /// <param name="camSN"></param>
        /// <returns></returns>
        protected override bool DoGetCameraBySN(string camSN)
        {
            bool rt = false;
            try
            {
                if (_deviceInfoList.Count > 0)
                {
                    ThridLibray.IDeviceInfo tmpdevice;
                    for (int i = 0; i < _deviceInfoList.Count; i++)
                    {
                        tmpdevice = _deviceInfoList[i];
                        if (tmpdevice.SerialNumber == camSN)
                        {
                            _deviceInfo = tmpdevice;
                            if (DoGetCameraByIdx(i))
                            {
                                rt = true;
                                break;
                            }
                        }
                    }

                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大华相机获取设备失败!\n设备SN:{0}\n异常描述:{1}", camSN, "指定SN不匹配"));
                    }
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
        /// 打开设备
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
                    {
                        rt=_deviceRef.Open();
                    }

                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大华相机打开失败!\n错误代码:{0:X8}", 99));
                    }
                    else
                    {
                        /* 设置缓存个数为8（默认值为16） */
                        /* set buffer count to 8 (default 16) */
                        _deviceRef.StreamGrabber.SetBufferCount(8);

                        /* 设置图像格式 */
                        /* set PixelFormat */
                        using (ThridLibray.IEnumParameter p = _deviceRef.ParameterCollection[ThridLibray.ParametrizeNameSet.ImagePixelFormat])
                        {
                            p.SetValue("Mono8");
                        }
                    }
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
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        protected override bool DoClose()
        {
            bool rt = false;
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
            try
            {
                if (_deviceRef != null)
                {
                    _deviceRef.StreamGrabber.ImageGrabbed -= StreamGrabber_ImageGrabbed; /* 反注册回调 | unregister grab event callback */
                    _deviceRef.ShutdownGrab();                                           /* 停止码流 | stop grabbing */
                    _deviceRef.Close();                                                  /* 关闭相机 | close camera */
                    rt = true;
                }
                               
                if (!rt)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机关闭设备失败!\n错误代码:{0:X8}", nRet));
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
        /// 方法：设置采集模式
        /// </summary>
        /// <param name="acqmode"></param>
        /// <param name="frameNum"></param>
        /// <returns></returns>
        protected override bool DoSetAcquisitionMode(ProCommon.Communal.AcquisitionMode acqmode, uint frameNum)
        {
            bool rt = false;
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
            try
            {
                if (_deviceRef != null)
                {
                    switch (acqmode)
                    {
                        case ProCommon.Communal.AcquisitionMode.Continue:
                            if (SetFreeRun())
                                rt = SetContinueRun();
                            break;
                        case ProCommon.Communal.AcquisitionMode.ExternalTrigger:
                            if (SetExternalTrigger(1, 1, 50))
                                rt = SetFrameNumber(frameNum);                         
                            break;
                        case ProCommon.Communal.AcquisitionMode.SoftTrigger:
                            if (SetInternalTrigger())
                                rt = SetFrameNumber(frameNum);                        
                            break;
                        default: break;
                    }

                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大华相机采集模式设置失败!\n错误代码:{0:X8}", nRet));
                    }
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
            try
            {
                if (_deviceRef != null)
                {
                    rt = true;
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机设置自由采集失败!\n:{0}", "设备未连接"));
                }
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：大华相机设置自由采集失败!\n错误描述:{0}", ex.Message));
            }
            finally
            {

            }
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
                    rt = _deviceRef.TriggerSet.Close();//关闭触发采集(开启连续采集)
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机设置连续采集失败!\n:{0}", "设备未连接"));
                }
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：大华相机设置连续采集失败!\n错误描述:{0}", ex.Message));
            }
            finally
            {

            }
            return rt;
        }

        /// <summary>
        /// 设置内部触发采集(软触发)
        /// 0-Line0,1-Line1,2-Line2,3-Line3,4-Counter,7-Software
        /// </summary>
        /// <returns></returns>
        private bool SetInternalTrigger()
        {
            bool rt = false;          
            try
            {
                if (_deviceRef != null)
                {
                    rt=_deviceRef.TriggerSet.Open(ThridLibray.TriggerSourceEnum.Software); //开启触发模式:软触发
                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大华相机内触发源(Software)设置失败!\n错误代码:{0:X8}", 99));
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机设置内触发采集失败!\n:{0}", "设备未连接"));
                }
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：大华相机设置内触发采集失败!\n错误描述:{0}", ex.Message));
            }
            finally
            {

            }
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
                if (_deviceRef != null)
                {
                    using (ThridLibray.IIntegraParameter p = _deviceRef.ParameterCollection[new ThridLibray.IntegerName("AcquisitionFrameCount")])
                    {
                        rt=p.SetValue(frameNum);
                    }
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机设置采集帧数失败!\n:{0}", "设备未连接"));
                }
            }
            catch (Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：大华相机设置采集帧数失败!\n错误描述:{0}", ex.Message));
            }

            return rt;
        }
       
        private bool SetExternalTrigger(int lineIdx, float delaytime, float debouncertime)
        {
            bool rt = false;
            try
            {
                if (_deviceRef != null)
                {
                    string src = ThridLibray.TriggerSourceEnum.Line1;
                    switch(lineIdx)
                    {
                        case 2:
                            src = ThridLibray.TriggerSourceEnum.Line2;
                            break;
                        case 3:
                            src = ThridLibray.TriggerSourceEnum.Line3;
                            break;
                        case 4:
                            src = ThridLibray.TriggerSourceEnum.Line4;
                            break;
                        case 5:
                            src = ThridLibray.TriggerSourceEnum.Line5;
                            break;
                        case 6:
                            src = ThridLibray.TriggerSourceEnum.Line6;
                            break;
                        default:
                            src = ThridLibray.TriggerSourceEnum.Line1;
                            break;
                    }
                    rt = _deviceRef.TriggerSet.Open(src);//开启触发模式:外触发
                    if (!rt)
                    {
                        if (DriverExceptionDel != null)
                            DriverExceptionDel(string.Format("错误：大华相机外触发源(Line0)设置失败!\n错误代码:{0:X8}", 99));
                    }
                    //else
                    //{
                    //    rt = SetTriggerDelay(lineIdx, delaytime);
                    //    if (rt)
                    //        rt=SetDebouncerTime(lineIdx, debouncertime);
                    //}
                }
                else
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机设置外触发采集失败!\n:{0}", "设备未连接"));
                }
            }
            catch (System.Exception ex)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：大华相机设置外触发采集失败!\n错误描述:{0}", ex.Message));
            }
            finally
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
                            {
                                using (ThridLibray.IEnumParameter p = _deviceRef.ParameterCollection[ThridLibray.ParametrizeNameSet.TriggerActivation])
                                {
                                    rt = p.SetValue("FallingEdge");
                                }
                            }
                            break;
                        case ProCommon.Communal.EffectiveSignal.RaiseEdge:
                            {
                                using (ThridLibray.IEnumParameter p = _deviceRef.ParameterCollection[ThridLibray.ParametrizeNameSet.TriggerActivation])
                                {
                                    rt = p.SetValue("RisingEdge");
                                }
                            }
                            break;                     
                        default:
                            break;
                    }
                }

                rt = true;
                if (!rt)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机触信号边沿设置失败!\n错误代码:{0:X8}", 99));
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

        protected override bool DoStartGrab()
        {
            bool rt = false;          
            try
            {
                if (_deviceRef != null)
                    rt = _deviceRef.GrabUsingGrabLoopThread();

                if (!rt)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机开启采集失败!\n错误代码:{0:X8}", 99));
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
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
            try
            {
                if (_deviceRef != null)
                {
                    rt= _deviceRef.ShutdownGrab();
                }

                rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                if (!rt)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机停止采集失败!\n错误代码:{0:X8}", nRet));
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

        protected override bool DoSoftTriggerOnce()
        {
            bool rt = false;
            int nRet = MvCamCtrl.NET.MyCamera.MV_E_UNKNOW;
            try
            {
                if (_deviceRef != null)
                {
                    rt = _deviceRef.ExecuteSoftwareTrigger();
                }

                rt = (MvCamCtrl.NET.MyCamera.MV_OK == nRet) ? true : false;
                if (!rt)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机触发失败!\n错误代码:{0:X8}", nRet));
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

        protected override bool DoSetExposureTime(float exposuretime)
        {
            bool rt = false;          
            try
            {
                if (_deviceRef != null)
                {
                    using (ThridLibray.IFloatParameter p = _deviceRef.ParameterCollection[ThridLibray.ParametrizeNameSet.ExposureTime])
                    {
                       rt=p.SetValue(exposuretime);                       
                    }
                }
                             
                if (!rt)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机设置曝光失败!\n错误代码:{0:X8}", 99));
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

        protected override bool DoSetGain(float gain)
        {
            bool rt = false;          
            try
            {
                if (_deviceRef != null)
                {
                    using (ThridLibray.IFloatParameter p = _deviceRef.ParameterCollection[ThridLibray.ParametrizeNameSet.GainRaw])
                    {
                        rt = p.SetValue(gain);
                    }
                }
                              
                if (!rt)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机设置增益失败!\n错误代码:{0:X8}", 99));
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
        /// 设置Gamma
        /// </summary>
        /// <param name="gamma"></param>
        /// <returns></returns>
        protected override bool DoSetGamma(float gamma)
        {
            bool rt = false;

            return rt;
        }

        protected override bool DoSetFrameRate(float fps)
        {
            bool rt = false;          
            try
            {
                if (_deviceRef != null)
                {
                    using (ThridLibray.IFloatParameter p = _deviceRef.ParameterCollection[ThridLibray.ParametrizeNameSet.AcquisitionFrameRate])
                    {
                        rt = p.SetValue(fps);
                    }
                }
                            
                if (!rt)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机设置帧率失败!\n错误代码:{0:X8}", 99));
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
        /// 设置相机触发延时
        /// </summary>
        /// <param name="lineIdx">外触发端口</param>
        /// <param name="trigdelay">延时时间,单位毫秒</param>
        /// <returns></returns>
        protected override bool DoSetTriggerDelay(int lineIdx,float trigdelay)
        {
            bool rt = false;         
            try
            {
                if (_deviceRef != null)
                {
                    //SDK--固定外触发输入,无需指定输入端口,时间单位微秒
                    using (ThridLibray.IFloatParameter p = _deviceRef.ParameterCollection[ThridLibray.ParametrizeNameSet.TriggerDelay])
                    {
                        trigdelay *= 1000;
                        rt = p.SetValue(trigdelay);
                    }
                }
                             
                if (!rt)
                {
                    if (DriverExceptionDel != null)
                        DriverExceptionDel(string.Format("错误：大华相机设置触发延时失败!\n错误代码:{0:X8}", 99));
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

        protected override bool DoSetDebouncerTime(int lineIdx, float debouncertime)
        {
            throw new NotImplementedException();
        }

        protected override bool DoGetInPut(int lineIdx, out bool onOff)
        {
            throw new NotImplementedException();
        }

        protected override bool DoGetCameraConnectedState(out bool isConnected)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 方法：注册异常回调函数(大华)
        /// </summary>
        /// <returns></returns>
        protected override bool DoRegisterExceptionCallBack()
        {
            bool rt = false;          
            if (_deviceRef != null)
            {
                _deviceRef.ConnectionLost += _deviceRef_ConnectionLost;
                rt = true;
            }
         
            if (!rt)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：大华相机注册异常回调失败!\n错误代码:{0:X8}", 99));
            }

            return rt;
        }

        /// <summary>
        /// 方法:注册采集数据更新回调(大华)
        /// </summary>
        /// <returns></returns>
        protected override bool DoRegisterImageGrabbedCallBack()
        {
            bool rt = false;          

            if (_deviceRef != null)
            {
                _deviceRef.StreamGrabber.ImageGrabbed += StreamGrabber_ImageGrabbed;
                rt = true;
            }
          
            if (!rt)
            {
                if (DriverExceptionDel != null)
                    DriverExceptionDel(string.Format("错误：大华相机注册采集回调失败!\n错误代码:{0:X8}", 99));
            }

            return rt;
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
                    DriverExceptionDel(string.Format("错误：大华相机设置输出信号失败!\n错误描述:{0}", ex.Message));
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

        public override string ToString()
        {
            return "CameraDriver[HikVision]";
        }

        #endregion

        #region 大华相机SDK官方函数

        #endregion

        #region BitMap转HObject 方法

        /// <summary>
        /// Bitmap转HObject
        /// </summary>
        /// <param name="bmp">24位Bitmap</param>
        /// <param name="hobj"></param>
        /// <returns></returns>
        private bool BitmapBpp24ToHObject(System.Drawing.Bitmap bmp, out HalconDotNet.HObject hobj)
        {
            bool rt = false;
            HalconDotNet.HOperatorSet.GenEmptyObj(out hobj);

            try
            {
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
                System.Drawing.Imaging.BitmapData srcBmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                HalconDotNet.HOperatorSet.GenImageInterleaved(out hobj, srcBmpData.Scan0, "bgr", bmp.Width, bmp.Height, 0, "byte", 0, 0, 0, 0, -1, 0);
                bmp.UnlockBits(srcBmpData);
                rt = true;
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
        /// Bitmap转HObject
        /// </summary>
        /// <param name="bmp">8位Bitmap</param>
        /// <param name="hobj"></param>
        /// <returns></returns>
        private bool BitmapBpp8ToHObject(System.Drawing.Bitmap bmp, out HalconDotNet.HObject hobj)
        {
            bool rt = false;
            HalconDotNet.HOperatorSet.GenEmptyObj(out hobj);

            try
            {
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
                System.Drawing.Imaging.BitmapData srcBmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                HalconDotNet.HOperatorSet.GenImage1(out hobj, "byte", bmp.Width, bmp.Height, srcBmpData.Scan0);
                bmp.UnlockBits(srcBmpData);
                rt = true;
            }
            catch
            {
            }
            finally
            {
            }
            return rt;
        }

        private bool BitmapToHObject(System.Drawing.Bitmap bmp, out HalconDotNet.HObject hobj)
        {
            bool rt = false;
            HalconDotNet.HOperatorSet.GenEmptyObj(out hobj);

            try
            {
                switch (bmp.PixelFormat)
                {
                    case System.Drawing.Imaging.PixelFormat.Format16bppArgb1555:
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format16bppGrayScale:
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format16bppRgb555:
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format16bppRgb565:
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format1bppIndexed:
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                        if (BitmapBpp24ToHObject(bmp, out hobj))
                            rt = true;
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format32bppPArgb:
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format32bppRgb:
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format48bppRgb:
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format4bppIndexed:
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format64bppArgb:
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format64bppPArgb:
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
                        if (BitmapBpp8ToHObject(bmp, out hobj))
                            rt = true;
                        break;
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

        #endregion

    }
}
