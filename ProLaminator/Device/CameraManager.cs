using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       CameraManager
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProLaminator.Device
 * File      Name：       CameraManager
 * Creating  Time：       5/20/2020 4:04:26 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProLaminator.Device
{
    public delegate void CameraActionedEventHandle(ProLaminator.Device.Camera cam,ProLaminator.Device.DeviceActionEventArgs e);

    /// <summary>
    /// 在指定视图上更新图形数据
    /// </summary>
    /// <param name="viewerIdx"></param>
    /// <param name="laminatorProData"></param>
    public delegate void UpdateIconicDel(ProLaminator.Data.LaminatorProcessData laminatorProData);   

    public class CameraManager:ProCommon.Communal.DeviceManager
    {
        #region 静态单例

        static object _syncObj = new object();
        static CameraManager _instance;
        public static CameraManager Instance
        {
            get
            {
                lock (_syncObj)
                {
                    if (_instance == null)
                    {
                        _instance = new CameraManager();
                    }
                }

                return _instance;
            }
        }

        private CameraManager()
        {
            CameraStateChangedEvent = new CameraActionedEventHandle(OnCameraStateChanged);
            CameraInitializedEvent = new CameraActionedEventHandle(OnCameraInitialized);
            CameraStartedEvent = new CameraActionedEventHandle(OnCameraStarted);
            CameraStoppedEvent = new CameraActionedEventHandle(OnCameraStopped);
            CameraReleasedEvent = new CameraActionedEventHandle(OnCameraReleased);

            UpdateRunGlassIconicEvent = new UpdateIconicDel(OnUpdateIconic);
            UpdateDebugGlassIconicEvent=new UpdateIconicDel(OnUpdateIconic);

            UpdateRunMembrane1IconicEvent = new UpdateIconicDel(OnUpdateIconic);
            UpdateDebugMembrane1IconicEvent = new UpdateIconicDel(OnUpdateIconic);

            UpdateRunMembrane2IconicEvent = new UpdateIconicDel(OnUpdateIconic);
            UpdateDebugMembrane2IconicEvent = new UpdateIconicDel(OnUpdateIconic);

            ErrorMessage = new StringBuilder();
            ErrorMessage.Clear();

            _imgListForGlass = new System.Collections.ArrayList();
            _imgListForMembrane1 = new System.Collections.ArrayList();
            _imgListForMembrane2 = new System.Collections.ArrayList();

            _mtxNewImgForGlass = new System.Threading.Mutex();
            _mtxNewImgForMembrane1 = new System.Threading.Mutex();
            _mtxNewImgForMembrane2 = new System.Threading.Mutex();

            _mtxResultForGlass = new System.Threading.Mutex();
            _mtxResultForMembrane1 = new System.Threading.Mutex();
            _mtxResultForMembrane2 = new System.Threading.Mutex();

            _arsteNewImgForGlass = new System.Threading.AutoResetEvent(false);
            _arsteNewImgForMembrane1 = new System.Threading.AutoResetEvent(false);
            _arsteNewImgForMembrane2 = new System.Threading.AutoResetEvent(false);

            _mrsteSystemCancel = new System.Threading.ManualResetEvent(false);

            _processDataForGlass = new Data.LaminatorProcessData();
            _processDataForMembrane1 = new Data.LaminatorProcessData();
            _processDataForMembrane2 = new Data.LaminatorProcessData();
            _isReconnect = false;
        }

        /// <summary>
        /// 在指定视图更新图形数据
        /// </summary>
        /// <param name="viewerIdx"></param>
        /// <param name="laminatorProData"></param>
        private void OnUpdateIconic(ProLaminator.Data.LaminatorProcessData laminatorProData)
        {
            
        }

        private void OnCameraReleased(Camera cam, ProLaminator.Device.DeviceActionEventArgs e)
        {

        }

        private void OnCameraStopped(Camera cam, ProLaminator.Device.DeviceActionEventArgs e)
        {

        }

        private void OnCameraStarted(Camera cam, ProLaminator.Device.DeviceActionEventArgs e)
        {

        }

        private void OnCameraInitialized(Camera cam, ProLaminator.Device.DeviceActionEventArgs e)
        {

        }

        private void OnCameraStateChanged(ProLaminator.Device.Camera cam, ProLaminator.Device.DeviceActionEventArgs e)
        {

        }

        #endregion

        public event ProLaminator.Device.CameraActionedEventHandle CameraStateChangedEvent;
        public event ProLaminator.Device.CameraActionedEventHandle CameraInitializedEvent;
        public event ProLaminator.Device.CameraActionedEventHandle CameraStartedEvent;
        public event ProLaminator.Device.CameraActionedEventHandle CameraStoppedEvent;
        public event ProLaminator.Device.CameraActionedEventHandle CameraReleasedEvent;

        public bool IsAllConnected { private set; get; }
        public bool IsAllStarted { private set; get; }
        public bool IsAllStopped { private set; get; }
        public bool IsAllReleased { private set; get; }

        public event ProLaminator.Device.UpdateIconicDel UpdateRunGlassIconicEvent;
        public event ProLaminator.Device.UpdateIconicDel UpdateDebugGlassIconicEvent;

        public event ProLaminator.Device.UpdateIconicDel UpdateRunMembrane1IconicEvent;
        public event ProLaminator.Device.UpdateIconicDel UpdateDebugMembrane1IconicEvent;

        public event ProLaminator.Device.UpdateIconicDel UpdateRunMembrane2IconicEvent;
        public event ProLaminator.Device.UpdateIconicDel UpdateDebugMembrane2IconicEvent;

        public System.Text.StringBuilder ErrorMessage { private set; get; }

        private ProLaminator.Config.CfgManager _cfgMgr;
        /// <summary>
        /// 配置管理器
        /// </summary>
        public ProLaminator.Config.CfgManager CfgMgr
        {
            get { return _cfgMgr; }
            set
            {
                if(value!=null)
                {
                    _cfgMgr = value;
                    _cfgSys = _cfgMgr.CfgSys;
                    if(_cfgSys!=null)
                    {
                        _IsChinese = _cfgSys.LanguageVersion == ProCommon.Communal.Language.Chinese;
                        SystemLogFilePath = _cfgSys.SystemLogFilePath;
                        ExceptionLogFilePath = _cfgSys.ExceptionLogFilePath;
                    }

                    _cameraPropertyList = _cfgMgr.CfgCam.PropertyList;

                    //项目用相机属性的分配:通过系统管理器分配

                    //玻璃工位相机属性
                    if (_camProForGlass == null)
                        _camProForGlass = ProLaminator.Logic.SystemManager.Instance.CamPropertyForGlass;

                    //膜1工位相机属性
                    if(_camProForMembrane1==null)
                        _camProForMembrane1 = ProLaminator.Logic.SystemManager.Instance.CamPropertyForMembrane1;

                    //膜2工位相机属性
                    if (_camProForMembrane2 == null)
                        _camProForMembrane2 = ProLaminator.Logic.SystemManager.Instance.CamPropertyForMembrane2;

                    if(CameraList==null)
                        CameraList = new System.Collections.Generic.SortedList<string, Camera>();
                    CameraList.Clear();

                    if (_camImageGrabbedList == null)
                        _camImageGrabbedList = new SortedList<string, bool>();
                    _camImageGrabbedList.Clear();

                    if (IsCameraDebugList==null)
                        IsCameraDebugList = new SortedList<string, bool>();
                    IsCameraDebugList.Clear();

                    if (IsInitProcessList == null)
                        IsInitProcessList = new SortedList<string, bool>();
                    IsInitProcessList.Clear();

                    if (_cameraPropertyList!=null
                        && _cameraPropertyList.Count>0)
                    {
                        //--------------------相机驱动若无断线重连功能,则启用定时器重新连接-----------------------------//
                        InitTimer();   //初始化定时器 
                        string id = null;
                        int cnt = _cameraPropertyList.Count;

                        for(int i=0;i<cnt;i++)
                        {
                            //从相机配置获取属性并更新相机属性
                            id = _cameraPropertyList[i].ID;

                            if (!IsCameraDebugList.ContainsKey(id))
                                IsCameraDebugList.Add(id, false);

                            if (!IsInitProcessList.ContainsKey(id))
                                IsInitProcessList.Add(id, true);

                            if (!CameraList.ContainsKey(id))
                            {
                                ProLaminator.Device.Camera cam = new Camera(_cameraPropertyList[i]);
                                cam.Property.IsActive = false;
                                cam.Property.IsChinese = _IsChinese;
                                cam.Property.IsConnected = false;
                                cam.Property.PropertyChanged += Property_PropertyChanged;

                                //从标定方案配置获取标定方案并更新相机标定方案
                                ProVision.Communal.CalibrationSolution cal = null;
                                int num = _cfgMgr.CfgCal.CalSolutionList.Count;
                                for (int j = 0; j < num; j++)
                                {
                                    cal = _cfgMgr.CfgCal.CalSolutionList[j];
                                    //同一个相机的多个标定方案(有效且激活的方案只有一个)
                                    if (cal.CameraName == cam.Property.Name)
                                        cam.CalSolutionList.Add(cal);
                                }

                                CameraList.Add(id, cam);
                                CameraList[id].API.ImageGrabbedEvt += Camera_ImageGrabbedEvt;
                                _camImageGrabbedList.Add(id, false);

                                //玻璃工位相机
                                if(_camProForGlass!=null)
                                    if (_camProForGlass.ID == cam.Property.ID)
                                        CamForGlass = cam;

                                //膜1工位相机
                                if (_camProForMembrane1 != null)
                                    if (_camProForMembrane1.ID == cam.Property.ID)
                                        CamForMembrane1 = cam;

                                //膜2工位相机
                                if (_camProForMembrane2 != null)
                                    if (_camProForMembrane2.ID == cam.Property.ID)
                                        CamForMembrane2 = cam;

                            }
                        }
                    }

                    
                    if (_spForPlc == null)
                        _spForPlc = new System.IO.Ports.SerialPort(_cfgSys.SerialPortNameForPlc);

                    _spForPlc.WriteTimeout = _cfgMgr.CfgSrlPort.PropertyList[0].SendTimeOut;
                    _spForPlc.ReadTimeout = _cfgMgr.CfgSrlPort.PropertyList[0].ReceiveTimeOut;

                    _spForPlc.BaudRate = _cfgMgr.CfgSrlPort.PropertyList[0].BaudRate;
                    _spForPlc.DataBits = _cfgMgr.CfgSrlPort.PropertyList[0].DataBits;
                    _spForPlc.Parity = _cfgMgr.CfgSrlPort.PropertyList[0].Parity;
                    _spForPlc.StopBits = _cfgMgr.CfgSrlPort.PropertyList[0].StopBits;

                    PanisonicPlc = new PLCData.PLCDevice(_spForPlc, 1);                    
                }
            }
        }

        private ProLaminator.Config.CfgSystem _cfgSys;
        private ProCommon.Communal.CameraPropertyList _cameraPropertyList;
        private bool _isReconnect;

        /// <summary>
        /// 松下通信PLC
        /// </summary>
        public PLCData.PLCDevice PanisonicPlc { private set; get; }
        private System.IO.Ports.SerialPort _spForPlc;


        #region 项目用相机属性,相机设备,相机内参,相机外参,畸变校正映射图像,标定转换矩阵,图像列表

        /// <summary>
        /// 贴附定位玻璃工位相机属性
        /// </summary>
        private ProCommon.Communal.CameraProperty _camProForGlass;

        /// <summary>
        /// 贴附定位膜1工位相机属性
        /// </summary>
        private ProCommon.Communal.CameraProperty _camProForMembrane1;

        /// <summary>
        /// 贴附定位膜2工位相机属性
        /// </summary>
        private ProCommon.Communal.CameraProperty _camProForMembrane2;

        /// <summary>
        /// 贴附定位玻璃工位相机
        /// </summary>
        public ProLaminator.Device.Camera CamForGlass { private set; get; }

        /// <summary>
        /// 贴附定位膜1工位相机
        /// </summary>
        public ProLaminator.Device.Camera CamForMembrane1 { private set; get; }

        /// <summary>
        /// 贴附定位膜2工位相机
        /// </summary>
        public ProLaminator.Device.Camera CamForMembrane2 { private set; get; }

        public HalconDotNet.HTuple InternalParaForCamGlass { private set; get; }

        public HalconDotNet.HTuple ExternalParaForCamGlass { private set; get; }

        public HalconDotNet.HObject RectifyMapImageForCamGlass { private set; get; }

        public HalconDotNet.HTuple Hom2DForCamGlass { private set; get; }

        public HalconDotNet.HTuple Hom2DForCamMembrane1 { private set; get; }

        public HalconDotNet.HTuple Hom2DForCamMembrane2 { private set; get; }

        /// <summary>
        /// 玻璃工位相机图像列表
        /// </summary>
        private System.Collections.ArrayList _imgListForGlass;

        /// <summary>
        /// 膜1工位相机图像列表
        /// </summary>
        private System.Collections.ArrayList _imgListForMembrane1;

        /// <summary>
        /// 膜2工位相机图像列表
        /// </summary>
        private System.Collections.ArrayList _imgListForMembrane2;


        private System.Threading.Mutex _mtxNewImgForGlass;
        private System.Threading.Mutex _mtxNewImgForMembrane1;
        private System.Threading.Mutex _mtxNewImgForMembrane2;

        private System.Threading.Mutex _mtxResultForGlass;
        private System.Threading.Mutex _mtxResultForMembrane1;
        private System.Threading.Mutex _mtxResultForMembrane2;

        private System.Threading.AutoResetEvent _arsteNewImgForGlass;
        private System.Threading.AutoResetEvent _arsteNewImgForMembrane1;
        private System.Threading.AutoResetEvent _arsteNewImgForMembrane2;     

        private System.Threading.ManualResetEvent _mrsteSystemCancel;
     
        private const int MAX_IMG_BUFFERS = 5;

        private ProLaminator.Data.LaminatorProcessData _processDataForGlass;
        private ProLaminator.Data.LaminatorProcessData _processDataForMembrane1;
        private ProLaminator.Data.LaminatorProcessData _processDataForMembrane2;

        private ProLaminator.Vision.Process.ImageProcess_LocateGlass _imgProcessForGlass;
        private ProLaminator.Vision.Process.ImageProcess_LocatingMembrane1 _imgProcessForMembrane1;
        private ProLaminator.Vision.Process.ImageProcess_LocatingMembrane2 _imgProcessForMembrane2;

        public HalconDotNet.HWindowControl HWndcForSheller;

        #endregion

        public void InitImgPro()
        {
            _arsteNewImgForGlass.Reset();
            _arsteNewImgForMembrane1.Reset();
            _arsteNewImgForMembrane2.Reset();         

            int cnt = _imgListForGlass.Count;
            HalconDotNet.HObject hoImg = null;
            for(int i=0;i<cnt;i++)
            {
                hoImg = (HalconDotNet.HObject)_imgListForGlass[i];
                _imgListForGlass.Remove(hoImg);
                hoImg.Dispose();
            }

            cnt = _imgListForMembrane1.Count;
            for (int i = 0; i < cnt; i++)
            {
                hoImg = (HalconDotNet.HObject)_imgListForMembrane1[i];
                _imgListForMembrane1.Remove(hoImg);
                hoImg.Dispose();
            }

            cnt = _imgListForMembrane2.Count;
            for (int i = 0; i < cnt; i++)
            {
                hoImg = (HalconDotNet.HObject)_imgListForMembrane2[i];
                _imgListForMembrane2.Remove(hoImg);
                hoImg.Dispose();
            }
        }

        /// <summary>
        /// 玻璃工位处理函数
        /// </summary>
        /// <param name="obj"></param>
        private void RunImgProForGlass(object obj)
        {
            HalconDotNet.HTuple tStart, tEnd,imgWidth=0,imgHeight=0;
            HalconDotNet.HObject img;
            ProVision.Communal.CalibrationSolutionList calList=null;
            bool processOk = false, resultOK = false;
            string runstateR = _IsChinese ? "运行" : "RUN";
            string runstateS = _IsChinese ? "停止" : "STOP";

            double dx=0, dy=0, dr=0;
            float x = 0f, y = 0f, r = 0f;
            int ix = 0, iy = 0, ir = 0;

            ushort f = 1; //结果异常--1，正常--2
            HalconDotNet.HObject cross;
            HalconDotNet.HOperatorSet.GenEmptyObj(out cross);
            cross.Dispose();

            while (_arsteNewImgForGlass.WaitOne())
            {
                //获取图像
                _mtxNewImgForGlass.WaitOne();
                img = null;

                if (_imgListForGlass!=null
                    && _imgListForGlass.Count>0)
                {
                    img = (HalconDotNet.HObject)_imgListForGlass[0];
                    _imgListForGlass.Remove(img);
                }
               
                _mtxNewImgForGlass.ReleaseMutex();

                processOk = false;
                resultOK = false;

                if (_imgProcessForGlass == null)
                {
                    _imgProcessForGlass = new Vision.Process.ImageProcess_LocateGlass();
                    _imgProcessForGlass.IsChinese = _IsChinese;
                    _imgProcessForGlass.Parameter = _cfgMgr.CfgVsPara.ParaForGlass;
                    _imgProcessForGlass.InitProcess();
                }

                if (IsInitProcessList[_camProForGlass.ID])
                {
                    if (calList == null)
                        calList = new ProVision.Communal.CalibrationSolutionList();
                    calList.Clear();

                    if (_cfgMgr != null
                        && _cfgMgr.CfgCal != null
                        && _cfgMgr.CfgCal.CalSolutionList != null)
                    {
                        int cnt = _cfgMgr.CfgCal.CalSolutionList.Count;
                        ProVision.Communal.CalibrationSolution calSolution = null;
                        for (int i = 0; i < cnt; i++)
                        {
                            calSolution = _cfgMgr.CfgCal.CalSolutionList[i];
                            if (calSolution.CameraName == CamForGlass.Property.Name)
                                calList.Add(calSolution);
                        }
                    }

                    CamForGlass.CalSolutionList = calList;

                    if (CamForGlass.CalSolution != null)
                    {
                        switch(CamForGlass.CalSolution.CalibrationType)
                        {
                            case ProVision.Communal.CalibrationType.CALIBRATION_CAMERABOARD:
                                break;
                            case ProVision.Communal.CalibrationType.CALIBRATION_POINTS:
                                double[] homMat2DArr = CamForGlass.CalSolution.ResultOfCaliPoint.PC2WCHomMat2D;
                                Hom2DForCamGlass = new HalconDotNet.HTuple(homMat2DArr);
                                break;
                        }
                    }

                    HalconDotNet.HOperatorSet.GetImageSize(img, out imgWidth,out imgHeight);

                    _imgProcessForGlass.InitProcess();                 
                    IsInitProcessList[_camProForGlass.ID] = false;
                }


                if (img != null
                    && img.IsInitialized())
                {
                    _imgProcessForGlass.SetEnableAlgorithm(CamForGlass.Property.EnableAlgorithm);

                    HalconDotNet.HOperatorSet.CountSeconds(out tStart); //起始时间 
                    processOk = _imgProcessForGlass.Process(img);
                    HalconDotNet.HOperatorSet.CountSeconds(out tEnd); //结束时间
                    _processDataForGlass.ElapseTime = (1000 * (tEnd - tStart).D);

                    if (processOk)
                        resultOK = _imgProcessForGlass.ResultOK;

                    _cfgSys.GlassProTotal += 1;
                    _processDataForGlass.Row = -1;
                    _processDataForGlass.Col = -1;
                    _processDataForGlass.DeltaAglRad = -1;

                    x = -1;
                    y = -1;
                    r = -1;
                    f = 1;

                    //OK时，更新处理结果
                    if (resultOK)
                    {
                        //当前项目:像素Row与世界坐标系X平齐,像素Col与世界坐标系Y平齐

                        if(_cfgMgr.CfgVsPara.CoordinateReferenceMode==0)
                        {
                            dx = (_imgProcessForGlass.Row - _imgProcessForGlass.ModelRow).D * CamForGlass.CalSolution.RowUnit;
                            x = Convert.ToSingle(System.Math.Round(dx, 2));

                            dy = (_imgProcessForGlass.Col - _imgProcessForGlass.ModelCol).D * CamForGlass.CalSolution.ColUnit;
                            y = Convert.ToSingle(System.Math.Round(dy, 2));
                        }
                        else if (_cfgMgr.CfgVsPara.CoordinateReferenceMode == 1)
                        {
                            dx = (_imgProcessForGlass.Row - imgHeight / 2).D * CamForGlass.CalSolution.RowUnit;
                            x = Convert.ToSingle(System.Math.Round(dx, 2));

                            dy = (_imgProcessForGlass.Col - imgWidth / 2).D * CamForGlass.CalSolution.ColUnit;
                            y = Convert.ToSingle(System.Math.Round(dy, 2));
                        }

                        dr = _imgProcessForGlass.Agl.D * 180 / System.Math.PI;
                        if (dr > 180.0)
                            dr -= 360.0;

                        r = Convert.ToSingle(System.Math.Round(dr, 2));
                        f = 2;

                        ix = Convert.ToInt32(x*100);
                        iy = Convert.ToInt32(y*100);
                        ir = Convert.ToInt32(r*100);
                        _cfgSys.GlassProOK += 1;
                    }
                    else
                    {
                        _cfgSys.GlassProNG += 1;
                    }

                    if (_cfgSys.GlassProTotal == 0)
                        dx = 0;
                    else
                        dx = _cfgSys.GlassProOK /(double)_cfgSys.GlassProTotal;

                    _cfgSys.GlassProYieldRatio = Convert.ToSingle(System.Math.Round(dx, 4));

                    if (PanisonicPlc != null)
                    {
                        if (PanisonicPlc.Connected)
                        {
                            PanisonicPlc.Registers.SetValue(1101, iy);
                            PanisonicPlc.Registers.SetValue(1103, ix);
                            PanisonicPlc.Registers.SetValue(1105, ir);
                            PanisonicPlc.Registers.SetValue(1100, f);
                        }
                    }

                    #region 更新结果到窗体                  

                    //更新结果变量
                    _processDataForGlass.RawImage = img;
                    _processDataForGlass.InspetcArea = _imgProcessForGlass.InspectArea;

                    _processDataForGlass.ImgProcessOK = processOk;
                    _processDataForGlass.ImgResultOK = resultOK;
                    _processDataForGlass.ResultFlag = f;

                    _processDataForGlass.ProductTotalNumber = _cfgSys.GlassProTotal;
                    _processDataForGlass.ProductOKNumber = _cfgSys.GlassProOK;
                    _processDataForGlass.ProductNGNumber = _cfgSys.GlassProNG;
                    _processDataForGlass.ProductYieldRatio = _cfgSys.GlassProYieldRatio;
                  

                    _processDataForGlass.RunState = ProLaminator.Logic.SystemManager.Instance.IsRunning ? runstateR : runstateS;

                    if (resultOK)
                    {
                        HalconDotNet.HOperatorSet.GenCrossContourXld(out cross, 
                            _imgProcessForGlass.Row, 
                            _imgProcessForGlass.Col, 
                            20, _imgProcessForGlass.Agl);

                        _processDataForGlass.Row = _imgProcessForGlass.Row;
                        _processDataForGlass.Col = _imgProcessForGlass.Col;
                        _processDataForGlass.DeltaAglRad = _imgProcessForGlass.Agl;
                    }

                    _processDataForGlass.ResultRegion = cross;                   

                    //计算世界坐标系内相对偏移
                    _processDataForGlass.DeltaX = x;
                    _processDataForGlass.DeltaY = y;
                    _processDataForGlass.DeltaAglDegree = r;

                    //非调试模式,更新到多视图对应窗口;调试模式,更新到单视图对应窗口
                    if (!IsCameraDebugList[_camProForGlass.ID])
                        UpdateRunGlassIconicEvent(_processDataForGlass);
                    else
                        UpdateDebugGlassIconicEvent(_processDataForGlass);
                 

                    #endregion

                    #region 图片保存选项

                    if (processOk)
                    {
                        //图像处理结果达到要求
                        if (resultOK)
                        {
                            if (_imgProcessForGlass.Parameter != null
                                   && _imgProcessForGlass.Parameter.IsSaveImageOK)
                            {
                                if (!System.IO.Directory.Exists(_imgProcessForGlass.Parameter.PathForSaveImageOK))
                                    System.IO.Directory.CreateDirectory(_imgProcessForGlass.Parameter.PathForSaveImageOK);
                                HalconDotNet.HOperatorSet.WriteImage(img, new HalconDotNet.HTuple("jpeg"), new HalconDotNet.HTuple(0),
                                    new HalconDotNet.HTuple(_imgProcessForGlass.Parameter.PathForSaveImageOK + "\\ImgOK_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")));
                            }
                        }
                        else
                        {
                            if (_imgProcessForGlass.Parameter != null
                                && _imgProcessForGlass.Parameter.IsSaveImageNG)
                            {
                                if (!System.IO.Directory.Exists(_imgProcessForGlass.Parameter.PathForSaveImageNG))
                                    System.IO.Directory.CreateDirectory(_imgProcessForGlass.Parameter.PathForSaveImageNG);
                                HalconDotNet.HOperatorSet.WriteImage(img, new HalconDotNet.HTuple("jpeg"), new HalconDotNet.HTuple(0),
                                    new HalconDotNet.HTuple(_imgProcessForGlass.Parameter.PathForSaveImageNG + "\\ImgNG_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")));
                            }
                        }
                    }
                    else
                    {
                        if (_imgProcessForGlass.Parameter != null
                           && _imgProcessForGlass.Parameter.IsSaveImageNG)
                        {
                            if (!System.IO.Directory.Exists(_imgProcessForGlass.Parameter.PathForSaveImageNG))
                                System.IO.Directory.CreateDirectory(_imgProcessForGlass.Parameter.PathForSaveImageNG);
                            HalconDotNet.HOperatorSet.WriteImage(img, new HalconDotNet.HTuple("jpeg"), new HalconDotNet.HTuple(0),
                                new HalconDotNet.HTuple(_imgProcessForGlass.Parameter.PathForSaveImageNG + "\\ImgNG_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")));
                        }
                    }

                    if (_imgProcessForGlass.Parameter != null
                            && _imgProcessForGlass.Parameter.IsSaveImageAll)
                    {
                        if (!System.IO.Directory.Exists(_imgProcessForGlass.Parameter.PathForSaveImageAll))
                            System.IO.Directory.CreateDirectory(_imgProcessForGlass.Parameter.PathForSaveImageAll);
                        HalconDotNet.HOperatorSet.WriteImage(img, new HalconDotNet.HTuple("jpeg"), new HalconDotNet.HTuple(0),
                            new HalconDotNet.HTuple(_imgProcessForGlass.Parameter.PathForSaveImageAll + "\\ImgAll_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")));
                    }

                    #endregion

                    img.Dispose();
                    cross.Dispose();
                }

                if (_mrsteSystemCancel.WaitOne(0, true)) break;
            }
        }

        /// <summary>
        /// 膜1工位处理函数
        /// </summary>
        /// <param name="state"></param>
        private void RunImgProForMembrane1(object state)
        {
            HalconDotNet.HTuple tStart, tEnd, imgWidth = 0, imgHeight = 0;
            HalconDotNet.HObject img;
            ProVision.Communal.CalibrationSolutionList calList = null;
            bool processOk = false, resultOK = false;
            string runstateR = _IsChinese ? "运行" : "RUN";
            string runstateS = _IsChinese ? "停止" : "STOP";
            double dx = 0, dy = 0, dr = 0;
            float x = 0.0f, y = 0.0f, r = 0.0f;
            int ix = 0, iy = 0, ir = 0;
            ushort f = 1; //结果异常--1，正常--2
            HalconDotNet.HObject cross;
            HalconDotNet.HOperatorSet.GenEmptyObj(out cross);
            cross.Dispose();

            while (_arsteNewImgForMembrane1.WaitOne())
            {
                //获取图像
                _mtxNewImgForMembrane1.WaitOne();
                img = (HalconDotNet.HObject)_imgListForMembrane1[0];
                _imgListForMembrane1.Remove(img);
                _mtxNewImgForMembrane1.ReleaseMutex();              

                processOk = false;
                resultOK = false;
               

                if (_imgProcessForMembrane1 == null)
                {
                    _imgProcessForMembrane1 = new Vision.Process.ImageProcess_LocatingMembrane1();
                    _imgProcessForMembrane1.IsChinese = _IsChinese;
                    _imgProcessForMembrane1.Parameter = _cfgMgr.CfgVsPara.ParaForMembrane1;
                    _imgProcessForMembrane1.InitProcess();
                }

                if (IsInitProcessList[_camProForMembrane1.ID])
                {
                    if (calList == null)
                        calList = new ProVision.Communal.CalibrationSolutionList();
                    calList.Clear();

                    if (_cfgMgr != null
                        && _cfgMgr.CfgCal != null
                        && _cfgMgr.CfgCal.CalSolutionList != null)
                    {
                        int cnt = _cfgMgr.CfgCal.CalSolutionList.Count;
                        ProVision.Communal.CalibrationSolution calSolution = null;
                        for (int i = 0; i < cnt; i++)
                        {
                            calSolution = _cfgMgr.CfgCal.CalSolutionList[i];
                            if (calSolution.CameraName == CamForMembrane1.Property.Name)
                                calList.Add(calSolution);
                        }
                    }

                    CamForMembrane1.CalSolutionList = calList;

                    if (CamForMembrane1.CalSolution != null)
                    {
                        switch (CamForMembrane1.CalSolution.CalibrationType)
                        {
                            case ProVision.Communal.CalibrationType.CALIBRATION_CAMERABOARD:
                                break;
                            case ProVision.Communal.CalibrationType.CALIBRATION_POINTS:
                                double[] homMat2DArr = CamForMembrane1.CalSolution.ResultOfCaliPoint.PC2WCHomMat2D;
                                Hom2DForCamMembrane1 = new HalconDotNet.HTuple(homMat2DArr);
                                break;
                        }
                    }
                    HalconDotNet.HOperatorSet.GetImageSize(img, out imgWidth, out imgHeight);

                    _imgProcessForMembrane1.InitProcess();
                    IsInitProcessList[_camProForMembrane1.ID]= false;
                }

                if (img != null
                    && img.IsInitialized())
                {
                    _imgProcessForMembrane1.SetEnableAlgorithm(CamForMembrane1.Property.EnableAlgorithm);

                    HalconDotNet.HOperatorSet.CountSeconds(out tStart); //起始时间 
                    processOk = _imgProcessForMembrane1.Process(img);
                    HalconDotNet.HOperatorSet.CountSeconds(out tEnd); //结束时间
                    _processDataForMembrane1.ElapseTime = (1000 * (tEnd - tStart).D);

                    if (processOk)
                        resultOK = _imgProcessForMembrane1.ResultOK;

                    _cfgSys.Membrane1ProTotal += 1;
                    _processDataForMembrane1.Row = -1;
                    _processDataForMembrane1.Col = -1;
                    _processDataForMembrane1.DeltaAglRad = -1;

                    x = -1;
                    y = -1;
                    r = -1;
                    f = 1;

                    //OK时，更新处理结果
                    if (resultOK)
                    {
                        //当前项目:像素Row与世界坐标系X平齐,像素Col与世界坐标系Y平齐                     

                        if (_cfgMgr.CfgVsPara.CoordinateReferenceMode == 0)
                        {
                            dx = (_imgProcessForMembrane1.Row - _imgProcessForMembrane1.ModelRow).D * CamForMembrane1.CalSolution.RowUnit;
                            x = Convert.ToSingle(System.Math.Round(dx, 2));

                            dy = (_imgProcessForMembrane1.Col - _imgProcessForMembrane1.ModelCol).D * CamForMembrane1.CalSolution.ColUnit;
                            y = Convert.ToSingle(System.Math.Round(dy, 2));
                        }
                        else if (_cfgMgr.CfgVsPara.CoordinateReferenceMode == 1)
                        {
                            dx = (_imgProcessForMembrane1.Row - imgHeight / 2).D * CamForMembrane1.CalSolution.RowUnit;
                            x = Convert.ToSingle(System.Math.Round(dx, 2));

                            dy = (_imgProcessForMembrane1.Col - imgWidth / 2).D * CamForMembrane1.CalSolution.ColUnit;
                            y = Convert.ToSingle(System.Math.Round(dy, 2));
                        }

                        dr = _imgProcessForMembrane1.Agl.D * 180 / System.Math.PI;
                        if (dr > 180.0)
                            dr -= 360;
                        r = Convert.ToSingle(System.Math.Round(dr, 2));
                        f = 2;

                        ix = Convert.ToInt32(x * 100);
                        iy = Convert.ToInt32(y * 100);
                        ir = Convert.ToInt32(r * 100);
                        _cfgSys.Membrane1ProOK += 1;
                    }
                    else
                    {
                        _cfgSys.Membrane1ProNG += 1;
                    }

                    if (_cfgSys.Membrane1ProTotal == 0)
                        dx = 0;
                    else
                        dx = _cfgSys.Membrane1ProOK / (double)_cfgSys.Membrane1ProTotal;

                    _cfgSys.Membrane1ProYieldRatio = Convert.ToSingle(System.Math.Round(dx, 4));

                    if (PanisonicPlc != null)
                    {
                        if (PanisonicPlc.Connected)
                        {
                            PanisonicPlc.Registers.SetValue(1201, iy);
                            PanisonicPlc.Registers.SetValue(1203, ix);
                            PanisonicPlc.Registers.SetValue(1205, ir);
                            PanisonicPlc.Registers.SetValue(1200, f);
                        }
                    }

                    #region 更新结果到窗体                 

                    //更新结果变量
                    _processDataForMembrane1.RawImage = img;
                    _processDataForMembrane1.InspetcArea = _imgProcessForMembrane1.InspectArea;

                    _processDataForMembrane1.ImgProcessOK = processOk;
                    _processDataForMembrane1.ImgResultOK = resultOK;
                    _processDataForMembrane1.ResultFlag = f;

                    _processDataForMembrane1.ProductTotalNumber = _cfgSys.Membrane1ProTotal;
                    _processDataForMembrane1.ProductOKNumber = _cfgSys.Membrane1ProOK;
                    _processDataForMembrane1.ProductNGNumber = _cfgSys.Membrane1ProNG;
                    _processDataForMembrane1.ProductYieldRatio = _cfgSys.Membrane1ProYieldRatio;
                    _processDataForMembrane1.RunState = ProLaminator.Logic.SystemManager.Instance.IsRunning ? runstateR : runstateS;

                    if (resultOK)
                    {
                        HalconDotNet.HOperatorSet.GenCrossContourXld(out cross,
                            _imgProcessForMembrane1.Row,
                            _imgProcessForMembrane1.Col,
                            20, _imgProcessForMembrane1.Agl);

                        _processDataForMembrane1.Row = _imgProcessForMembrane1.Row;
                        _processDataForMembrane1.Col = _imgProcessForMembrane1.Col;
                        _processDataForMembrane1.DeltaAglRad = _imgProcessForMembrane1.Agl;                      
                    }

                    _processDataForMembrane1.ResultRegion = cross;

                    //计算世界坐标系内相对偏移
                    _processDataForMembrane1.DeltaX = x;
                    _processDataForMembrane1.DeltaY = y;
                    _processDataForMembrane1.DeltaAglDegree = r;                  

                    //非调试模式,更新到多视图对应窗口;调试模式,更新到单视图对应窗口
                    if (!IsCameraDebugList[_camProForMembrane1.ID])
                        UpdateRunMembrane1IconicEvent(_processDataForMembrane1);
                    else
                        UpdateDebugMembrane1IconicEvent(_processDataForMembrane1);                 

                    #endregion

                    #region 图片保存选项

                    if (processOk)
                    {
                        //图像处理结果达到要求
                        if (resultOK)
                        {
                            if (_imgProcessForMembrane1.Parameter != null
                                   && _imgProcessForMembrane1.Parameter.IsSaveImageOK)
                            {
                                if (!System.IO.Directory.Exists(_imgProcessForMembrane1.Parameter.PathForSaveImageOK))
                                    System.IO.Directory.CreateDirectory(_imgProcessForMembrane1.Parameter.PathForSaveImageOK);
                                HalconDotNet.HOperatorSet.WriteImage(img, new HalconDotNet.HTuple("jpeg"), new HalconDotNet.HTuple(0),
                                    new HalconDotNet.HTuple(_imgProcessForMembrane1.Parameter.PathForSaveImageOK + "\\ImgOK_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")));
                            }
                        }
                        else
                        {
                            if (_imgProcessForMembrane1.Parameter != null
                                && _imgProcessForMembrane1.Parameter.IsSaveImageNG)
                            {
                                if (!System.IO.Directory.Exists(_imgProcessForMembrane1.Parameter.PathForSaveImageNG))
                                    System.IO.Directory.CreateDirectory(_imgProcessForMembrane1.Parameter.PathForSaveImageNG);
                                HalconDotNet.HOperatorSet.WriteImage(img, new HalconDotNet.HTuple("jpeg"), new HalconDotNet.HTuple(0),
                                    new HalconDotNet.HTuple(_imgProcessForMembrane1.Parameter.PathForSaveImageNG + "\\ImgNG_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")));
                            }
                        }
                    }
                    else
                    {
                        if (_imgProcessForMembrane1.Parameter != null
                           && _imgProcessForMembrane1.Parameter.IsSaveImageNG)
                        {
                            if (!System.IO.Directory.Exists(_imgProcessForMembrane1.Parameter.PathForSaveImageNG))
                                System.IO.Directory.CreateDirectory(_imgProcessForMembrane1.Parameter.PathForSaveImageNG);
                            HalconDotNet.HOperatorSet.WriteImage(img, new HalconDotNet.HTuple("jpeg"), new HalconDotNet.HTuple(0),
                                new HalconDotNet.HTuple(_imgProcessForMembrane1.Parameter.PathForSaveImageNG + "\\ImgNG_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")));
                        }
                    }

                    if (_imgProcessForMembrane1.Parameter != null
                            && _imgProcessForMembrane1.Parameter.IsSaveImageAll)
                    {
                        if (!System.IO.Directory.Exists(_imgProcessForMembrane1.Parameter.PathForSaveImageAll))
                            System.IO.Directory.CreateDirectory(_imgProcessForMembrane1.Parameter.PathForSaveImageAll);
                        HalconDotNet.HOperatorSet.WriteImage(img, new HalconDotNet.HTuple("jpeg"), new HalconDotNet.HTuple(0),
                            new HalconDotNet.HTuple(_imgProcessForMembrane1.Parameter.PathForSaveImageAll + "\\ImgAll_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")));
                    }

                    #endregion

                    img.Dispose();
                    cross.Dispose();
                }

                if (_mrsteSystemCancel.WaitOne(0, true)) break;
            }
        }

        /// <summary>
        /// 膜1工位处理函数
        /// </summary>
        /// <param name="state"></param>
        private void RunImgProForMembrane2(object state)
        {
            HalconDotNet.HTuple tStart, tEnd, imgWidth = 0, imgHeight = 0;
            HalconDotNet.HObject img;
            ProVision.Communal.CalibrationSolutionList calList = null;
            bool processOk = false, resultOK = false;
            string runstateR = _IsChinese ? "运行" : "RUN";
            string runstateS = _IsChinese ? "停止" : "STOP";
            double dx = 0, dy = 0, dr = 0;
            float x = 0.0f, y = 0.0f, r = 0.0f;
            int ix = 0, iy =0, ir = 0;
            ushort f = 1; //结果异常--1，正常--2
            HalconDotNet.HObject cross;
            HalconDotNet.HOperatorSet.GenEmptyObj(out cross);
            cross.Dispose();

            while (_arsteNewImgForMembrane2.WaitOne())
            {
                //获取图像
                _mtxNewImgForMembrane2.WaitOne();
                img = (HalconDotNet.HObject)_imgListForMembrane2[0];
                _imgListForMembrane2.Remove(img);
                _mtxNewImgForMembrane2.ReleaseMutex();

                #region 多相机属于同一工位,需要所有相机都采集到图像再处理的情形

                /*
                 * 
                bool rt = true;
                int cnt = _camImageGrabbedList.Count;
                for (int i = 0; i < cnt; i++)
                    rt &= _camImageGrabbedList[_cameraPropertyList[i].ID];

                if (rt)
                {
                    ImageProcessUnderRunning();//

                    for (int i = 0; i < cnt; i++)
                        _camImageGrabbedList[_cameraPropertyList[i].ID] = false;
                }

                */
                #endregion

                processOk = false;
                resultOK = false;               

                if (_imgProcessForMembrane2 == null)
                {
                    _imgProcessForMembrane2 = new Vision.Process.ImageProcess_LocatingMembrane2();
                    _imgProcessForMembrane2.IsChinese = _IsChinese;
                    _imgProcessForMembrane2.Parameter = _cfgMgr.CfgVsPara.ParaForMembrane2;
                    _imgProcessForMembrane2.InitProcess();
                }

                if (IsInitProcessList[_camProForMembrane2.ID])
                {
                    if (calList == null)
                        calList = new ProVision.Communal.CalibrationSolutionList();
                    calList.Clear();

                    if (_cfgMgr != null
                        && _cfgMgr.CfgCal != null
                        && _cfgMgr.CfgCal.CalSolutionList != null)
                    {
                        int cnt = _cfgMgr.CfgCal.CalSolutionList.Count;
                        ProVision.Communal.CalibrationSolution calSolution = null;
                        for (int i = 0; i < cnt; i++)
                        {
                            calSolution = _cfgMgr.CfgCal.CalSolutionList[i];
                            if (calSolution.CameraName == CamForMembrane2.Property.Name)
                                calList.Add(calSolution);
                        }
                    }

                    CamForMembrane2.CalSolutionList = calList;

                    if (CamForMembrane2.CalSolution != null)
                    {
                        switch (CamForMembrane2.CalSolution.CalibrationType)
                        {
                            case ProVision.Communal.CalibrationType.CALIBRATION_CAMERABOARD:
                                break;
                            case ProVision.Communal.CalibrationType.CALIBRATION_POINTS:
                                double[] homMat2DArr = CamForMembrane2.CalSolution.ResultOfCaliPoint.PC2WCHomMat2D;
                                Hom2DForCamMembrane2 = new HalconDotNet.HTuple(homMat2DArr);
                                break;
                        }
                    }
                    HalconDotNet.HOperatorSet.GetImageSize(img, out imgWidth, out imgHeight);

                    _imgProcessForMembrane2.InitProcess();
                    IsInitProcessList[_camProForMembrane2.ID] = false;
                }

                if (img != null
                    && img.IsInitialized())
                {
                    _imgProcessForMembrane2.SetEnableAlgorithm(CamForMembrane2.Property.EnableAlgorithm);

                    HalconDotNet.HOperatorSet.CountSeconds(out tStart); //起始时间 
                    processOk = _imgProcessForMembrane2.Process(img);
                    HalconDotNet.HOperatorSet.CountSeconds(out tEnd); //结束时间
                    _processDataForMembrane2.ElapseTime = (1000 * (tEnd - tStart).D);

                    if (processOk)
                        resultOK = _imgProcessForMembrane2.ResultOK;

                    _cfgSys.Membrane2ProTotal += 1;
                    _processDataForMembrane2.Row = -1;
                    _processDataForMembrane2.Col = -1;
                    _processDataForMembrane2.DeltaAglRad = -1;

                    x = -1;
                    y = -1;
                    r = -1;
                    f = 1;

                    //OK时，更新处理结果
                    if (resultOK)
                    {
                        //当前项目:像素Row与世界坐标系X平齐,像素Col与世界坐标系Y平齐                     

                        if (_cfgMgr.CfgVsPara.CoordinateReferenceMode == 0)
                        {
                            dx = (_imgProcessForMembrane2.Row - _imgProcessForMembrane2.ModelRow).D * CamForMembrane2.CalSolution.RowUnit;
                            x = Convert.ToSingle(System.Math.Round(dx, 2));

                            dy = (_imgProcessForMembrane2.Col - _imgProcessForMembrane2.ModelCol).D * CamForMembrane2.CalSolution.ColUnit;
                            y = Convert.ToSingle(System.Math.Round(dy, 2));
                        }
                        else if (_cfgMgr.CfgVsPara.CoordinateReferenceMode == 1)
                        {
                            dx = (_imgProcessForMembrane2.Row - imgHeight / 2).D * CamForMembrane2.CalSolution.RowUnit;
                            x = Convert.ToSingle(System.Math.Round(dx, 2));

                            dy = (_imgProcessForMembrane2.Col - imgWidth / 2).D * CamForMembrane2.CalSolution.ColUnit;
                            y = Convert.ToSingle(System.Math.Round(dy, 2));
                        }

                        dr = _imgProcessForMembrane2.Agl.D * 180 / System.Math.PI;
                        if (dr > 180.0)
                            dr -= 360;
                        r = Convert.ToSingle(System.Math.Round(dr, 2));
                        f = 2;

                        ix = Convert.ToInt32(x * 100);
                        iy = Convert.ToInt32(y * 100);
                        ir = Convert.ToInt32(r * 100);

                        _cfgSys.Membrane2ProOK += 1;
                    }
                    else
                    {
                        _cfgSys.Membrane2ProNG += 1;
                    }

                    if (_cfgSys.Membrane2ProTotal == 0)
                        dx = 0;
                    else
                        dx = _cfgSys.Membrane2ProOK / (double)_cfgSys.Membrane2ProTotal;

                    _cfgSys.Membrane2ProYieldRatio = Convert.ToSingle(System.Math.Round(dx,4));

                    if (PanisonicPlc != null)
                    {
                        if (PanisonicPlc.Connected)
                        {
                            PanisonicPlc.Registers.SetValue(1301, iy);
                            PanisonicPlc.Registers.SetValue(1303, ix);
                            PanisonicPlc.Registers.SetValue(1305, ir);
                            PanisonicPlc.Registers.SetValue(1300, f);
                        }
                    }

                    #region 更新结果到窗体                   

                    //更新结果变量
                    _processDataForMembrane2.RawImage = img;
                    _processDataForMembrane2.InspetcArea = _imgProcessForMembrane2.InspectArea;

                    _processDataForMembrane2.ImgProcessOK = processOk;
                    _processDataForMembrane2.ImgResultOK = resultOK;
                    _processDataForMembrane2.ResultFlag = f;

                    _processDataForMembrane2.ProductTotalNumber = _cfgSys.Membrane2ProTotal;
                    _processDataForMembrane2.ProductOKNumber = _cfgSys.Membrane2ProOK;
                    _processDataForMembrane2.ProductNGNumber = _cfgSys.Membrane2ProNG;
                    _processDataForMembrane2.ProductYieldRatio = _cfgSys.Membrane2ProYieldRatio;
                    _processDataForMembrane2.RunState = ProLaminator.Logic.SystemManager.Instance.IsRunning ? runstateR : runstateS;

                    if (resultOK)
                    {
                        HalconDotNet.HOperatorSet.GenCrossContourXld(out cross,
                            _imgProcessForMembrane2.Row,
                            _imgProcessForMembrane2.Col,
                            20, _imgProcessForMembrane2.Agl);

                        _processDataForMembrane2.Row = _imgProcessForMembrane2.Row;
                        _processDataForMembrane2.Col = _imgProcessForMembrane2.Col;
                        _processDataForMembrane2.DeltaAglRad = _imgProcessForMembrane2.Agl;                      
                    }

                    _processDataForMembrane2.ResultRegion = cross;

                    //计算世界坐标系内相对偏移
                    _processDataForMembrane2.DeltaX = x;
                    _processDataForMembrane2.DeltaY = y;
                    _processDataForMembrane2.DeltaAglDegree = r;

                    //非调试模式,更新到多视图对应窗口;调试模式,更新到单视图对应窗口
                    if (!IsCameraDebugList[_camProForMembrane2.ID])
                        UpdateRunMembrane2IconicEvent(_processDataForMembrane2);
                    else
                        UpdateDebugMembrane2IconicEvent(_processDataForMembrane2);

                    #endregion

                    #region 图片保存选项

                    if (processOk)
                    {
                        //图像处理结果达到要求
                        if (resultOK)
                        {
                            if (_imgProcessForMembrane2.Parameter != null
                                   && _imgProcessForMembrane2.Parameter.IsSaveImageOK)
                            {
                                if (!System.IO.Directory.Exists(_imgProcessForMembrane2.Parameter.PathForSaveImageOK))
                                    System.IO.Directory.CreateDirectory(_imgProcessForMembrane2.Parameter.PathForSaveImageOK);
                                HalconDotNet.HOperatorSet.WriteImage(img, new HalconDotNet.HTuple("jpeg"), new HalconDotNet.HTuple(0),
                                    new HalconDotNet.HTuple(_imgProcessForMembrane2.Parameter.PathForSaveImageOK + "\\ImgOK_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")));
                            }
                        }
                        else
                        {
                            if (_imgProcessForMembrane2.Parameter != null
                                && _imgProcessForMembrane2.Parameter.IsSaveImageNG)
                            {
                                if (!System.IO.Directory.Exists(_imgProcessForMembrane2.Parameter.PathForSaveImageNG))
                                    System.IO.Directory.CreateDirectory(_imgProcessForMembrane2.Parameter.PathForSaveImageNG);
                                HalconDotNet.HOperatorSet.WriteImage(img, new HalconDotNet.HTuple("jpeg"), new HalconDotNet.HTuple(0),
                                    new HalconDotNet.HTuple(_imgProcessForMembrane2.Parameter.PathForSaveImageNG + "\\ImgNG_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")));
                            }
                        }
                    }
                    else
                    {
                        if (_imgProcessForMembrane2.Parameter != null
                           && _imgProcessForMembrane2.Parameter.IsSaveImageNG)
                        {
                            if (!System.IO.Directory.Exists(_imgProcessForMembrane2.Parameter.PathForSaveImageNG))
                                System.IO.Directory.CreateDirectory(_imgProcessForMembrane2.Parameter.PathForSaveImageNG);
                            HalconDotNet.HOperatorSet.WriteImage(img, new HalconDotNet.HTuple("jpeg"), new HalconDotNet.HTuple(0),
                                new HalconDotNet.HTuple(_imgProcessForMembrane2.Parameter.PathForSaveImageNG + "\\ImgNG_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")));
                        }
                    }

                    if (_imgProcessForMembrane2.Parameter != null
                            && _imgProcessForMembrane2.Parameter.IsSaveImageAll)
                    {
                        if (!System.IO.Directory.Exists(_imgProcessForMembrane2.Parameter.PathForSaveImageAll))
                            System.IO.Directory.CreateDirectory(_imgProcessForMembrane2.Parameter.PathForSaveImageAll);
                        HalconDotNet.HOperatorSet.WriteImage(img, new HalconDotNet.HTuple("jpeg"), new HalconDotNet.HTuple(0),
                            new HalconDotNet.HTuple(_imgProcessForMembrane2.Parameter.PathForSaveImageAll + "\\ImgAll_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")));
                    }

                    #endregion

                    img.Dispose();
                    cross.Dispose();
                }

                if (_mrsteSystemCancel.WaitOne(0, true)) break;
            }
        }     

        public System.Collections.Generic.SortedList<string, ProLaminator.Device.Camera> CameraList { private set; get; }

        private System.Collections.Generic.SortedList<string, bool> _camImageGrabbedList;

        private bool _isCameraInitialized;

        /// <summary>
        /// 相机工位是否调试模式标记列表
        /// </summary>
        public System.Collections.Generic.SortedList<string, bool> IsCameraDebugList { private set; get; }

        /// <summary>
        /// 是否重新加载过程参数标记列表
        /// </summary>
        public System.Collections.Generic.SortedList<string, bool> IsInitProcessList { private set; get; } 

        /// <summary>
        /// 设置是否初始化过程
        /// </summary>
        /// <param name="isInit"></param>
        public void SetIsInitProcess(bool isInit)
        {
            if (IsInitProcessList!=null)
            {
                int cnt = IsInitProcessList.Count;
                string[] keyArr = new string[cnt];
                int k = 0;
                foreach (var itm in IsInitProcessList)
                {
                    keyArr[k] = itm.Key;
                    k++;
                }

                for (int i = 0; i < cnt; i++)
                    IsInitProcessList[keyArr[i]] = isInit;

            } 
        } 
       
        /// <summary>
        /// 设置相机工作模式为非调试模式,进而多视图显示
        /// </summary>
        public void SetImageToMultiViewer()
        {
            if (IsCameraDebugList != null)
            {
                int cnt = IsCameraDebugList.Count;
                string[] keyArr = new string[cnt];
                int k = 0;
                foreach (var itm in IsCameraDebugList)
                {
                    keyArr[k] = itm.Key;
                    k++;
                }
                              
                for (int i=0;i<cnt;i++)
                    IsCameraDebugList[keyArr[i]] = false;
            }  
        }

        /// <summary>
        /// 设置相机在工作或调试模式下的采集方式
        /// </summary>
        /// <param name="isWorkmode"></param>
        public void SetCameraWorkMode(bool isWorkmode)
        {
            if (_cameraPropertyList != null
                   && _cameraPropertyList.Count > 0)
            {
                string id = null;
                int cnt = _cameraPropertyList.Count;
                ProCommon.Communal.AcquisitionMode workMode = ProCommon.Communal.AcquisitionMode.ExternalTrigger;
                for (int i = 0; i < cnt; i++)
                {
                    id = _cameraPropertyList[i].ID;
                    workMode = isWorkmode ? 
                        ProCommon.Communal.AcquisitionMode.ExternalTrigger :
                        ProCommon.Communal.AcquisitionMode.SoftTrigger;

                    if (!CameraList[id].API.SetAcquisitionMode(workMode, 1))
                        continue;                   
                }
            }
        }

        /// <summary>
        /// 相机连接状态属性改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Property_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //将相机连接状态传递给上位调用           
            ProCommon.Communal.CameraProperty camPro = sender as ProCommon.Communal.CameraProperty;           
            if (camPro != null)
            {
                ProLaminator.Device.Camera cam = CameraList[camPro.ID];

                if (CameraStateChangedEvent != null)
                    CameraStateChangedEvent(cam, new ProLaminator.Device.DeviceActionEventArgs(cam.Property.IsConnected));
            }
        }

        /// <summary>
        /// 采集到图像事件回调
        /// [只需要负责将采集到的图像添加到图像列表]
        /// </summary>
        /// <param name="camProperty"></param>
        /// <param name="hoImage"></param>
        private void Camera_ImageGrabbedEvt(ProCommon.Communal.CameraProperty camProperty, HalconDotNet.HObject hoImage)
        {
            //系统在运行状态下:响应图像采集到事件;停止状态下,不响应(防止硬触发造成图像异常处理)
            if (ProLaminator.Logic.SystemManager.Instance.IsRunning)
            {
                //玻璃工位相机
                if (_camProForGlass != null)
                {
                    if (camProperty.ID == _camProForGlass.ID)
                    {
                        _mtxNewImgForGlass.WaitOne();

                        if (IsInitProcessList[_camProForGlass.ID])
                        {
                            int cnt = _imgListForGlass.Count;
                            HalconDotNet.HObject hoImg = null;
                            for (int i = 0; i < cnt; i++)
                            {
                                hoImg = (HalconDotNet.HObject)_imgListForGlass[i];
                                _imgListForGlass.Remove(hoImg);
                                hoImg.Dispose();
                            }
                        }

                        if (_imgListForGlass.Count >= MAX_IMG_BUFFERS)
                        {
                            HalconDotNet.HObject img = (HalconDotNet.HObject)_imgListForGlass[0];
                            _imgListForGlass.Remove(img);
                            img.Dispose();
                        }
                        else
                        {
                            _imgListForGlass.Add(hoImage.Clone());
                            _camImageGrabbedList[camProperty.ID] = true;
                        }

                        _mtxNewImgForGlass.ReleaseMutex();
                        _arsteNewImgForGlass.Set();
                    }
                }

                //膜1工位相机
                if (_camProForMembrane1 != null)
                {
                    if (camProperty.ID == _camProForMembrane1.ID)
                    {
                        _mtxNewImgForMembrane1.WaitOne();

                        if (IsInitProcessList[_camProForMembrane1.ID])
                        {
                            int cnt = _imgListForMembrane1.Count;
                            HalconDotNet.HObject hoImg = null;
                            for (int i = 0; i < cnt; i++)
                            {
                                hoImg = (HalconDotNet.HObject)_imgListForMembrane1[i];
                                _imgListForMembrane1.Remove(hoImg);
                                hoImg.Dispose();
                            }
                        }

                        if (_imgListForMembrane1.Count >= MAX_IMG_BUFFERS)
                        {
                            HalconDotNet.HObject img = (HalconDotNet.HObject)_imgListForMembrane1[0];
                            _imgListForMembrane1.Remove(img);
                            img.Dispose();
                        }
                        else
                        {
                            _imgListForMembrane1.Add(hoImage.Clone());
                            _camImageGrabbedList[camProperty.ID] = true;
                        }

                        _mtxNewImgForMembrane1.ReleaseMutex();
                        _arsteNewImgForMembrane1.Set();
                    }
                }

                //膜2工位相机
                if (_camProForMembrane2 != null)
                {
                    if (camProperty.ID == _camProForMembrane2.ID)
                    {
                        _mtxNewImgForMembrane2.WaitOne();

                        if (IsInitProcessList[_camProForMembrane2.ID])
                        {
                            int cnt = _imgListForMembrane2.Count;
                            HalconDotNet.HObject hoImg = null;
                            for (int i = 0; i < cnt; i++)
                            {
                                hoImg = (HalconDotNet.HObject)_imgListForMembrane2[i];
                                _imgListForMembrane2.Remove(hoImg);
                                hoImg.Dispose();
                            }
                        }

                        if (_imgListForMembrane2.Count >= MAX_IMG_BUFFERS)
                        {
                            HalconDotNet.HObject img = (HalconDotNet.HObject)_imgListForMembrane2[0];
                            _imgListForMembrane2.Remove(img);
                            img.Dispose();
                        }
                        else
                        {
                            _imgListForMembrane2.Add(hoImage.Clone());
                            _camImageGrabbedList[camProperty.ID] = true;
                        }

                        _mtxNewImgForMembrane2.ReleaseMutex();
                        _arsteNewImgForMembrane2.Set();
                    }
                }

                if(ProLaminator.Logic.SystemManager.Instance.IsRunOnce)
                    ProLaminator.Logic.SystemManager.Instance.IsRunning = false;
            }          
        }
        

        /// <summary>
        /// 所有相机设备初始化
        /// </summary>
        /// <returns></returns>
        protected override bool DoInit()
        {
            ProLaminator.Device.Camera tmpCam = null;
            bool rt = false;
            ErrorMessage.Clear();
            try
            {
                if (_cameraPropertyList != null
                   && _cameraPropertyList.Count > 0)
                {
                    string id = null;
                    int cnt = _cameraPropertyList.Count;
                    IsAllConnected = true;
                    for (int i = 0; i < cnt; i++)
                    {
                        id = _cameraPropertyList[i].ID;

                        //相机未连接
                        if (!CameraList[id].Property.IsConnected)
                        {
                            //在重连之前,是否需要释放已占用资源?
                            if(_isReconnect)
                                CameraList[id].API.Close();

                            CameraList[id].API.EnumerateCameraList();
                            if (CameraList[id].API.GetCameraBySN(CameraList[id].Property.SerialNo))
                            {
                                if (!CameraList[id].API.Open())
                                {
                                    IsAllConnected = false;
                                    continue;
                                }
                                else
                                {
                                    CameraList[id].Property.IsActive = true;
                                    CameraList[id].Property.IsConnected = true;
                                }
                            }
                            else
                            {
                                CameraList[id].Property.IsActive = false;
                                CameraList[id].Property.IsConnected = false;
                                IsAllConnected = false;
                                continue;
                            }
                        }

                        IsAllConnected &= CameraList[id].Property.IsConnected;
                    }

                    //所有相机都连接上,才进行初始设置
                    if (IsAllConnected)
                    {
                        if (!_isCameraInitialized)
                        {
                            InitImgPro();

                            InitCamera();
                            _isCameraInitialized = true;
                        }
                    }
                    ProCommon.Communal.InitException e = new ProCommon.Communal.InitException(ToString(), _IsChinese ? "无" : "No", _IsChinese);
                    if (CameraInitializedEvent != null)
                        CameraInitializedEvent(null, new DeviceActionEventArgs(e, IsAllConnected));

                    //开启定时器,心跳监测相机连接状态

                    /*
                    if (!_timer.Enabled)
                        StartTimer();
                    */

                    _isReconnect = true;
                    rt = IsAllConnected;
                }
            }
            catch (System.Exception ex)
            {
                string txtlog = _IsChinese ? "错误：初始化相机设备失败!\n异常描述:{0}" : "Error:Initialize camera failed!\r\n Description:{0}";
                string txtException = _IsChinese ? "初始化异常:\n" : "Initialize device failed:\n";
                ProCommon.Communal.LogWriter.WriteException(ExceptionLogFilePath, ex);
                ProCommon.Communal.LogWriter.WriteLog(SystemLogFilePath, string.Format(txtlog, ex.Message));
                ErrorMessage.Append(txtException);
                ProCommon.Communal.InitException e = new ProCommon.Communal.InitException(ToString(), txtException + ex.Message, _IsChinese);
                if (CameraInitializedEvent != null)
                    CameraInitializedEvent(tmpCam, new DeviceActionEventArgs(e, false));
                throw e;
            }

            return rt;
        }

        /// <summary>
        /// 初始设置相机
        /// </summary>
        private void InitCamera()
        {
            if (_cameraPropertyList != null
                   && _cameraPropertyList.Count > 0)
            {
                string id = null;
                int cnt = _cameraPropertyList.Count;
                for (int i = 0; i < cnt; i++)
                {
                    id = _cameraPropertyList[i].ID;

                    //设置采集模式:触发采集,外触发每次1帧
                    if (!CameraList[id].API.SetAcquisitionMode(ProCommon.Communal.AcquisitionMode.ExternalTrigger, 1))
                        continue;

                    //设置触发信号边缘:硬触发时启用
                    if (!CameraList[id].API.SetTriggerActivation(ProCommon.Communal.EffectiveSignal.RaiseEdge))
                        continue;

                    //设置采集帧率
                    if (!CameraList[id].API.SetFrameRate(_cameraPropertyList[i].FPS))
                        continue;
                    //设置相机曝光时间
                    if (!CameraList[id].API.SetExposureTime(CameraList[id].Property.ExposureTime))
                        continue;
                    //设置相机增益:
                    if (!CameraList[id].API.SetGain(CameraList[id].Property.Gain))
                        continue;

                    //设置相机外触发采集延时
                    if (!CameraList[id].API.SetTriggerDelay(0, 50.0f))
                        continue;

                    //设置相机外触发消抖时间
                    if (!CameraList[id].API.SetDebouncerTime(0, 50))
                        continue;

                    //注册相机图像采集到事件回调函数
                    if (!CameraList[id].API.RegisterImageGrabbedCallBack())
                        continue;
                    //注意:HikVision相机提供断线重连功能,Baumer相机暂无.因此,HikVision相机不需要定时器重连相机
                    if (!CameraList[id].API.RegisterExceptionCallBack())
                        continue;
                    //设置相机开始采集
                    if (!CameraList[id].API.StartGrab())
                        continue;
                }
            }
        }

        /// <summary>
        /// 所有相机设备开启
        /// </summary>
        /// <returns></returns>
        protected override bool DoStart()
        {
            ProLaminator.Device.Camera tmpCam = null;
            bool rt = false;
            ErrorMessage.Clear();
            try
            {
                if (_cameraPropertyList != null
                   && _cameraPropertyList.Count > 0)
                {
                    string camName = string.Empty;
                    string id = null;
                    int cnt = _cameraPropertyList.Count;
                    IsAllStarted = true;                  
                    for (int i = 0; i < cnt; i++)
                    {
                        id = _cameraPropertyList[i].ID;
                        tmpCam = CameraList[id];
                        if (!CameraList[id].Property.IsConnected)
                        {
                            camName += "\n" + CameraList[id].Property.Name;
                        }

                        IsAllStarted &= CameraList[id].Property.IsConnected;
                    }

                    if (!string.IsNullOrEmpty(camName)
                        && (!_IsShowing))
                    {
                        _IsShowing = true;
                        string txt1 = _IsChinese ? "相机:" : "Camera:";
                        string txt2 = _IsChinese ? "\n连接失败 !" : "\nConnect falied !";
                        string caption = _IsChinese ? "警告信息" : "Warning Message";

                        ProCommon.DerivedForm.FrmMsgBox.Show(txt1 + camName + txt2, caption,
                            ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                        
                    }
                    else
                    {
                        SetImageToMultiViewer();
                        SetIsInitProcess(true);

                        ProCommon.Communal.StartException e = new ProCommon.Communal.StartException(ToString(), _IsChinese ? "无" : "No", _IsChinese);
                        if (CameraStartedEvent != null)
                            CameraStartedEvent(null, new DeviceActionEventArgs(e, IsAllStarted));
                    }

                    //线程池--并行处理多个相机采集到的图像
                    
                    System.Threading.ThreadPool.QueueUserWorkItem(RunImgProForGlass);
                    System.Threading.ThreadPool.QueueUserWorkItem(RunImgProForMembrane1);
                    System.Threading.ThreadPool.QueueUserWorkItem(RunImgProForMembrane2);  
                }

                rt = true;
            }
            catch(System.Exception ex)
            {
                string txtlog = _IsChinese ? "错误：启动相机设备失败!\n异常描述:{0}" : "Error:Start camera failed!\r\n Description:{0}";
                string txtException = _IsChinese ? "启动异常:\n" : "Start device failed:\n";
                ProCommon.Communal.LogWriter.WriteException(ExceptionLogFilePath, ex);
                ProCommon.Communal.LogWriter.WriteLog(SystemLogFilePath, string.Format(txtlog, ex.Message));
                ErrorMessage.Append(txtException);
                ProCommon.Communal.StartException e = new ProCommon.Communal.StartException(ToString(), txtException + ex.Message, _IsChinese);
                if (CameraStartedEvent != null)
                    CameraStartedEvent(tmpCam, new DeviceActionEventArgs(e, false));
                throw e;
            }

            return rt;
        }       

        /// <summary>
        /// 所有相机设备停止
        /// </summary>
        /// <returns></returns>
        protected override bool DoStop()
        {
            ProLaminator.Device.Camera tmpCam = null;
            bool rt = false;
            ErrorMessage.Clear();
            try
            {
                _mrsteSystemCancel.Set();
                StopTimer();
                if (_cameraPropertyList != null
                    && _cameraPropertyList.Count > 0)
                {
                    string id = null;
                    int cnt = _cameraPropertyList.Count;
                    IsAllStopped = true;
                    for (int i = 0; i < cnt; i++)
                    {
                        id = _cameraPropertyList[i].ID;
                        tmpCam = CameraList[id];
                        if (CameraList[id].Property.IsConnected)
                        {
                            IsAllStopped &= CameraList[id].API.StopGrab();
                            CameraList[id].Property.IsConnected = false;
                        }
                    }
                }

                ProCommon.Communal.StopException e = new ProCommon.Communal.StopException(ToString(), _IsChinese ? "无" : "No", _IsChinese);
                if (CameraStoppedEvent != null)
                    CameraStoppedEvent(null, new DeviceActionEventArgs(e, IsAllStopped));
                rt = true;
            }
            catch (Exception ex)
            {
                string txtlog = _IsChinese ? "错误：停止相机设备失败!\n异常描述:{0}" : "Error:Stop camera failed!\r\n Description:{0}";
                string txtException = _IsChinese ? "停止异常:\n" : "Stop device failed:\n";
                ProCommon.Communal.LogWriter.WriteException(ExceptionLogFilePath, ex);
                ProCommon.Communal.LogWriter.WriteLog(SystemLogFilePath, string.Format(txtlog, ex.Message));
                ErrorMessage.Append(txtException);
                ProCommon.Communal.StopException e = new ProCommon.Communal.StopException(ToString(), txtException + ex.Message, _IsChinese);
                if (CameraStoppedEvent != null)
                    CameraStoppedEvent(tmpCam, new DeviceActionEventArgs(e, false));
                throw e;
            }

            return rt;
        }

        /// <summary>
        /// 所有相机设备资源释放
        /// </summary>
        /// <returns></returns>
        protected override bool DoRelease()
        {
            ProLaminator.Device.Camera tmpCam = null;
            bool rt = false;
            ErrorMessage.Clear();
            try
            {
                if (_cameraPropertyList != null
                    && _cameraPropertyList.Count > 0)
                {
                    string id = null;
                    int cnt = _cameraPropertyList.Count;
                    IsAllReleased = true;
                    for (int i = 0; i < cnt; i++)
                    {
                        id = _cameraPropertyList[i].ID;
                        tmpCam = CameraList[id];
                        if (!CameraList[id].Property.IsConnected)
                        {
                            IsAllReleased &= CameraList[id].API.StopGrab();
                            CameraList[id].Property.IsConnected = false;
                        }
                    }
                }

                ProCommon.Communal.ReleaseException e = new ProCommon.Communal.ReleaseException(ToString(), _IsChinese ? "无" : "No", _IsChinese);
                if (CameraReleasedEvent != null)
                    CameraReleasedEvent(null, new DeviceActionEventArgs(e, IsAllReleased));

                rt = true;
            }
            catch (Exception ex)
            {
                string txtlog = _IsChinese ? "错误：释放相机设备失败!\n异常描述:{0}" : "Error:Release camera failed!\r\n Description:{0}";
                string txtException = _IsChinese ? "释放异常:\n" : "Release device failed:\n";
                ProCommon.Communal.LogWriter.WriteException(ExceptionLogFilePath, ex);
                ProCommon.Communal.LogWriter.WriteLog(SystemLogFilePath, string.Format(txtlog, ex.Message));
                ErrorMessage.Append(txtException);
                ProCommon.Communal.ReleaseException e = new ProCommon.Communal.ReleaseException(ToString(), txtException + ex.Message, _IsChinese);
                if (CameraReleasedEvent != null)
                    CameraReleasedEvent(tmpCam, new DeviceActionEventArgs(e, false));
                throw e;
            }

            return rt;
        }

        public override string ToString()
        {
            return "CameraManager";
        }

        #region  定时器
        private System.Timers.Timer _timer = new System.Timers.Timer();
        private string id = string.Empty;
        private bool isConnected = false;

        /// <summary>
        /// 初始化定时器
        /// </summary>
        public void InitTimer()
        {
            _timer.Interval = 1000;
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
            _timer.Enabled = false;
        }

        /// <summary>
        /// 定时器间隔到达事件处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (_cameraPropertyList != null
                  && _cameraPropertyList.Count > 0)
                {
                    for (int i = 0; i < _cameraPropertyList.Count; i++)
                    {
                        id = _cameraPropertyList[i].ID;
                        CameraList[id].API.GetCameraConnectedState(out isConnected);

                        CameraList[id].Property.IsConnected = isConnected;

                        if (!isConnected)
                        {
                            //停止定时器，进行重连
                            StopTimer();

                            DoInit();
                            break;
                        }
                    }
                }

            }
            catch { }
        }

        /// <summary>
        /// 启用定时器
        /// </summary>
        public void StartTimer()
        {
            _timer.Enabled = false;
            _timer.Enabled = true;
        }

        /// <summary>
        /// 停用定时器
        /// </summary>
        public void StopTimer()
        {
            _timer.Enabled = false;
        }

        #endregion
    }
}
