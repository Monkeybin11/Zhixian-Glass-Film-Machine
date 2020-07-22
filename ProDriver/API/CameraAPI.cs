using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       CameraAPI
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProDriver.API
 * File      Name：       CameraAPI
 * Creating  Time：       4/21/2020 5:27:18 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProDriver.API
{
    /// <summary>
    /// 相机函数接口
    /// [注:与相机品牌无关的统一接口
    /// 目前已实现Basler,MindVision,HikVision,Dahua]
    /// </summary>
    public class CameraAPI
    {
        public HalconDotNet.HObject HoImage;
        public event ProDriver.Driver.CameraImageGrabbedDel ImageGrabbedEvt;

        private ProCommon.Communal.CameraProperty _cameraProperty;

        public CameraAPI(ProCommon.Communal.CameraProperty camProperty)
        {
            if (camProperty != null)
            {
                _cameraProperty = camProperty;
                switch (camProperty.Brand)
                {
                    case ProCommon.Communal.DeviceBrand.Baumer:
                        break;
                    case ProCommon.Communal.DeviceBrand.Dalsa:
                        break;
                    case ProCommon.Communal.DeviceBrand.Imaging:
                        break;
                    case ProCommon.Communal.DeviceBrand.MindVision:
                        ProDriver.Driver.CameraDriver_MindVision camdriver_mindvision = new ProDriver.Driver.CameraDriver_MindVision(camProperty);
                        ICameraDriverable = (camdriver_mindvision as ProDriver.Driver.ICameraDriver);
                        break;
                    case ProCommon.Communal.DeviceBrand.Basler:
                        ProDriver.Driver.CameraDriver_Basler camdriver_basler = new ProDriver.Driver.CameraDriver_Basler(camProperty);
                        ICameraDriverable = (camdriver_basler as ProDriver.Driver.ICameraDriver);
                        break;
                    case ProCommon.Communal.DeviceBrand.HikVision:
                        ProDriver.Driver.CameraDriver_HikVision camdriver_hikvision = new ProDriver.Driver.CameraDriver_HikVision(camProperty);
                        ICameraDriverable = (camdriver_hikvision as ProDriver.Driver.ICameraDriver);
                        break;
                    case ProCommon.Communal.DeviceBrand.DaHua:
                        ProDriver.Driver.CameraDriver_DaHua camdriver_dahua = new ProDriver.Driver.CameraDriver_DaHua(camProperty);
                        ICameraDriverable = (camdriver_dahua as ProDriver.Driver.ICameraDriver);
                        break;
                    default:
                        break;
                }
            }
        }

        private void OnCameraImageGrabbed(ProCommon.Communal.CameraProperty cam, HalconDotNet.HObject hoImage)
        {
            if (hoImage != null
                && hoImage.IsInitialized())
            {
                if (HoImage != null
                    && HoImage.IsInitialized())
                    HoImage.Dispose();

                HoImage = hoImage;
                if (ImageGrabbedEvt != null)
                    ImageGrabbedEvt(cam, HoImage);
            }
        }

        public ProDriver.Driver.ICameraDriver ICameraDriverable
        {
            private set;
            get;
        }

        public bool IsImageGrabbed
        {
            set
            {
                if (ICameraDriverable != null)
                {
                    ICameraDriverable.IsImageGrabbed = value;
                }
            }
            get
            {
                if (ICameraDriverable != null)
                    return ICameraDriverable.IsImageGrabbed;
                return false;
            }
        }

        /// <summary>
        /// 枚举在线相机
        /// </summary>
        /// <returns></returns>
        public bool EnumerateCameraList()
        {
            bool rt = false;
            if (ICameraDriverable != null)
                rt = ICameraDriverable.EnumerateCameraList();
            return rt;
        }

        /// <summary>
        /// 选择相机
        /// </summary>
        /// <param name="indx">相机索引</param>
        /// <returns></returns>
        public bool GetCameraByIdx(int indx)
        {
            bool rt = false;
            if (ICameraDriverable != null)
                rt = ICameraDriverable.GetCameraByIdx(indx);
            return rt;
        }

        /// <summary>
        /// 选择相机
        /// </summary>
        /// <param name="camNmae">相机名称</param>
        /// <returns></returns>
        public bool GetCameraByName(string camNmae)
        {
            bool rt = false;
            if (ICameraDriverable != null)
                rt = ICameraDriverable.GetCameraByName(camNmae);
            return rt;
        }

        /// <summary>
        /// 选择相机
        /// </summary>
        /// <param name="camSN">相机序列号</param>
        /// <returns></returns>
        public bool GetCameraBySN(string camSN)
        {
            bool rt = false;
            if (ICameraDriverable != null)
                rt = ICameraDriverable.GetCameraBySN(camSN);
            return rt;
        }

        /// <summary>
        /// 打开设备
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            bool rt = false;
            if (ICameraDriverable != null)
                rt = ICameraDriverable.Open();
            return rt;
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            bool rt = false;
            if (ICameraDriverable != null)
                rt = ICameraDriverable.Close();
            return rt;
        }
        public bool SetAcquisitionMode(ProCommon.Communal.AcquisitionMode acqmode, uint frameNum)
        {
            bool rt = false;
            if (ICameraDriverable != null)
                rt = ICameraDriverable.SetAcquisitionMode(acqmode, frameNum);
            return rt;
        }

        /// <summary>
        /// 方法:设置相机输出信号
        /// </summary>
        /// <param name="idx">输入端口号</param>
        /// <param name="triglogic">触发信号逻辑</param>
        /// <param name="delaytime">边沿信号时的延时,单位毫秒</param>
        /// <returns></returns>
        public bool SetOutPut(int idx,ProCommon.Communal.EffectiveSignal triglogic, int delaytime)
        {
            bool rt = false;
            if (ICameraDriverable != null)
            {
                switch (triglogic)
                {
                    case ProCommon.Communal.EffectiveSignal.FallEdge:
                        rt = ICameraDriverable.SetOutPut(idx,true);
                        if (rt)
                        {
                            System.Threading.Thread.Sleep(delaytime);
                            rt = ICameraDriverable.SetOutPut(idx,false);
                        }
                        break;
                    case ProCommon.Communal.EffectiveSignal.LogicFalse:
                        rt = ICameraDriverable.SetOutPut(idx,false);
                        break;
                    case ProCommon.Communal.EffectiveSignal.LogicTrue:
                        rt = ICameraDriverable.SetOutPut(idx,true);
                        break;
                    case ProCommon.Communal.EffectiveSignal.RaiseEdge:
                        rt = ICameraDriverable.SetOutPut(idx,false);
                        if (rt)
                        {
                            System.Threading.Thread.Sleep(delaytime);
                            rt = ICameraDriverable.SetOutPut(idx,true);
                        }
                        break;
                    default: break;
                }
            }
            return rt;
        }

        /// <summary>
        /// 方法:获取相机输入端口状态
        /// </summary>
        /// <param name="lineIdx">输入端口号</param>
        /// <param name="onOff">输入端口状态</param>
        /// <returns></returns>
        public bool GetInPut(int lineIdx,out bool onOff)
        {
            bool rt = false; onOff = false;
            if (ICameraDriverable != null)
            {
                rt = ICameraDriverable.GetInPut(lineIdx, out onOff);
            }
            return rt;
        }

      

        /// <summary>
        /// 方法：设置触发边沿信号模式
        /// </summary>
        /// <param name="edgemode">边缘模式</param>
        /// <returns></returns>
        public bool SetTriggerActivation(ProCommon.Communal.EffectiveSignal edgemode)
        {
            bool rt = false;
            if (ICameraDriverable != null)
                rt = ICameraDriverable.SetTriggerActivation(edgemode);
            return rt;
        }

        /// <summary>
        /// 开启采集
        /// </summary>
        /// <returns></returns>
        public bool StartGrab()
        {
            bool rt = false;
            if (ICameraDriverable != null)
            {
                rt = ICameraDriverable.StartGrab();
            }
            return rt;
        }

        /// <summary>
        /// 停止采集
        /// </summary>
        /// <returns></returns>
        public bool StopGrab()
        {
            bool rt = false;
            if (ICameraDriverable != null)
                rt = ICameraDriverable.StopGrab();
            return rt;
        }

        /// <summary>
        /// 软件触发
        /// </summary>
        /// <returns></returns>
        public bool SoftTriggerOnce()
        {
            bool rt = false;
            if (ICameraDriverable != null)
                rt = ICameraDriverable.SoftTriggerOnce();
            return rt;
        }

        /// <summary>
        /// 设置曝光时间
        /// </summary>
        /// <param name="exposuretime"></param>
        /// <returns></returns>
        public bool SetExposureTime(float exposuretime)
        {
            bool rt = false;
            if (ICameraDriverable != null)
                rt = ICameraDriverable.SetExposureTime(exposuretime);
            return rt;
        }

        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="gain"></param>
        /// <returns></returns>
        public bool SetGain(float gain)
        {
            bool rt = false;
            if (ICameraDriverable != null)
                rt = ICameraDriverable.SetGain(gain);
            return rt;
        }

        /// <summary>
        /// 设置Bayer格式Gamma
        /// </summary>
        /// <param name="gamma"></param>
        /// <returns></returns>

        public bool SetGamma(float gamma)
        {
            bool rt = false;
            if (ICameraDriverable != null)
                rt = ICameraDriverable.SetGamma(gamma);
            return rt;
        }

        /// <summary>
        /// 设置帧率
        /// </summary>
        /// <param name="fps"></param>
        /// <returns></returns>
        public bool SetFrameRate(float fps)
        {
            bool rt = false;
            if (ICameraDriverable != null)
                rt = ICameraDriverable.SetFrameRate(fps);
            return rt;
        }

        /// <summary>
        /// 设置触发延迟时间
        /// </summary>
        /// <param name="lineIdx">外触发端口号</param>
        /// <param name="trigdelay"></param>
        /// <returns></returns>
        public bool SetTriggerDelay(int lineIdx,float trigdelay)
        {
            bool rt = false;
            if (ICameraDriverable != null)
                rt = ICameraDriverable.SetTriggerDelay(lineIdx,trigdelay);
            return rt;
        }

        public bool SetDebouncerTime(int lineIdx, float debouncertime)
        {
            bool rt = false;
            if (ICameraDriverable != null)
                rt = ICameraDriverable.SetDebouncerTime(lineIdx, debouncertime);
            return rt;
        }

        /// <summary>
        /// 创建相机参数设置窗口
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <param name="promption"></param>
        /// <returns></returns>
        public bool CreateCameraSetPage(System.IntPtr windowHandle, string promption)
        {
            bool rt = false;
            if (ICameraDriverable != null)
                rt = ICameraDriverable.CreateCameraSetPage(windowHandle, promption);
            return rt;
        }

        public bool GetCameraConnectedState(out bool isConnected)
        {
            bool rt = false; isConnected = false;
            if (ICameraDriverable != null)
                rt = ICameraDriverable.GetCameraConnectedState(out isConnected);
            return rt;
        }

        /// <summary>
        /// 显示相机参数设置窗口
        /// </summary>
        /// <returns></returns>
        public bool ShowCameraSetPage()
        {
            bool rt = false;
            if (ICameraDriverable != null)
                rt = ICameraDriverable.ShowCameraSetPage();
            return rt;
        }

        /// <summary>
        /// 注册相机异常委托
        /// </summary>
        /// <returns></returns>
        public bool RegisterExceptionCallBack()
        {
            bool rt = false;
            if (ICameraDriverable != null)
                rt = ICameraDriverable.RegisterExceptionCallBack();
            return rt;
        }

        /// <summary>
        /// 注册相机采集到图像委托
        /// </summary>
        /// <returns></returns>
        public bool RegisterImageGrabbedCallBack()
        {
            bool rt = false;
            if (ICameraDriverable != null)
            {
                ICameraDriverable.CameraImageGrabbedEvt += new ProDriver.Driver.CameraImageGrabbedDel(OnCameraImageGrabbed);
                rt = ICameraDriverable.RegisterImageGrabbedCallBack();
            }
            return rt;
        }

    }
}
