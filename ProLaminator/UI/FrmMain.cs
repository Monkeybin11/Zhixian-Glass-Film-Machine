using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProLaminator.UI
{
    public partial class FrmMain : DevExpress.XtraEditors.XtraForm
    {
        #region 静态单例
        static object _syncObj = new object();
        static FrmMain _instance;
        public static FrmMain Instance
        {
            get
            {
                lock (_syncObj)
                {
                    if (_instance == null)
                    { _instance = new ProLaminator.UI.FrmMain(); }
                }

                return _instance;
            }
            set { _instance = value; }
        }

        private FrmMain()
        {
            InitializeComponent();                
            this.Load += FrmMain_Load;           
        }       

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.bstiCurrentTime.ItemAppearance.Normal.BackColor = Color.YellowGreen;
            _timer.Start();

            //为每个相机采集到图像和图像处理完成注册回调--窗体加载完成后--分屏窗体要在加载完成后确定位置和尺寸,才能分屏实例化,因此只能在窗体加载完成后注册回调
            for (int i = 0; i < _viewerNumber; i++)
            {               
                switch(i)
                {
                    case 0:
                        CameraMgr.UpdateRunGlassIconicEvent += _multiViewModule.IconicResultViewers[i].UpdateIconicResult;
                        CameraMgr.UpdateDebugGlassIconicEvent += _multiViewModule.CameraStationSet.UpdateIconicResult;
                        break;
                    case 1:
                        CameraMgr.UpdateRunMembrane1IconicEvent += _multiViewModule.IconicResultViewers[i].UpdateIconicResult;
                        CameraMgr.UpdateDebugMembrane1IconicEvent += _multiViewModule.CameraStationSet.UpdateIconicResult;
                        break;
                    case 2:
                        CameraMgr.UpdateRunMembrane2IconicEvent += _multiViewModule.IconicResultViewers[i].UpdateIconicResult;
                        CameraMgr.UpdateDebugMembrane2IconicEvent += _multiViewModule.CameraStationSet.UpdateIconicResult;
                        break;
                    default:break;
                }

                _multiViewModule.IconicResultViewers[i].EnableControl(false);
            }

            //*****定义一个设备管理器的管理器,通过统一接口进行调用:暂时用各自类型设备管理器进行操作
            CameraMgr.CameraStateChangedEvent += CameraMgr_CameraStateChangedEvent;

            //1--初始化设备
            CameraMgr.Init();

            //2--设备启动
            CameraMgr.Start();
        }

        public void CameraMgr_CameraStateChangedEvent(Device.Camera cam, Device.DeviceActionEventArgs e)
        {
            try
            {
                if (cam != null)
                {
                    this.BeginInvoke(new System.Windows.Forms.MethodInvoker(
                        ()=>
                        {
                            for(int i=0;i<this.bsbiCameraStatus.Manager.Items.Count;i++)
                            {
                                DevExpress.XtraBars.BarEditItem bei = this.bsbiCameraStatus.Manager.Items[i] as DevExpress.XtraBars.BarEditItem;

                                if (bei != null
                                   && bei.Tag != null)
                                {
                                    if (bei.Tag.ToString() == cam.Property.ID)
                                    {
                                        bei.EditValue = cam.Property.IsConnected;
                                        break;
                                    }
                                }
                            }
                        }));
                }
            }
            catch (System.Exception ex) { }
            
        }

        #endregion

        private bool _IsChinese;
        private ProCommon.Communal.Language _lanVersion;

        /// <summary>
        /// 软件语言版本
        /// </summary>
        public ProCommon.Communal.Language LangVersion
        {
            private set
            {
                _lanVersion = value;
                _IsChinese=_lanVersion == ProCommon.Communal.Language.Chinese;
            }
            get { return _lanVersion; }
        }

        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string AppName
        {
            get {
                return _IsChinese ?
                ProLaminator.Properties.Resources.ResourceManager.GetString("chs_APPNAME") :
                ProLaminator.Properties.Resources.ResourceManager.GetString("en_APPNAME");
            }
        }

        /// <summary>
        /// 开发商信息
        /// </summary>
        public string VendorInfo
        {
            get
            {
                return _IsChinese?
                      ProLaminator.Properties.Resources.ResourceManager.GetString("chs_VENDORINFO") :
                      ProLaminator.Properties.Resources.ResourceManager.GetString("en_VENDORINFO");
            }
        }

        /// <summary>
        /// 配置管理器
        /// </summary>
        public ProLaminator.Config.CfgManager _cfgMgr { private set; get; }

        /// <summary>
        /// 当前登录账户
        /// </summary>
        public ProCommon.Communal.Account CurrentAccount { set; get; }

        /// <summary>
        /// 相机设备管理器
        /// </summary>
        public ProLaminator.Device.CameraManager CameraMgr { private set; get; }

        /// <summary>
        /// 玻璃工位相机
        /// </summary>
        public ProLaminator.Device.Camera CameraForGlass { private set; get; }

        /// <summary>
        /// 膜1工位相机
        /// </summary>
        public ProLaminator.Device.Camera CameraForMembrane1 { private set; get; }

        /// <summary>
        /// 膜2工位相机
        /// </summary>
        public ProLaminator.Device.Camera CameraForMembrane2 { private set; get; }

        /// <summary>
        /// 视觉参数配置
        /// </summary>       
        private ProLaminator.Config.CfgVisionPara _cfgVisionPara;
        private int _viewerNumber;

        private ProLaminator.UI.DerivedControl.MultiViewModule _multiViewModule;

        /// <summary>
        /// 定时器
        /// </summary>
        private System.Windows.Forms.Timer _timer;

        /// <summary>
        /// 自启动
        /// </summary>
        private ProCommon.Communal.AutoLaunch _autoLaunch;

        /// <summary>
        /// 警告内容和标题
        /// </summary>
        private string _warningText, _warningCaption;

        public System.ComponentModel.BindingList<ProLaminator.Data.Log> LogList;

        /// <summary>
        /// 程式名称和目录
        /// </summary>
        private string _routineName, _routineDirectory;     

        /// <summary>
        /// 窗体标题栏显示信息
        /// </summary>
        public void ShowInfo()
        {
            this.HtmlText ="["+ AppName + "]--"+VendorInfo;
        }

        public void Init()
        {
            InitFieldAndProperty();
            InitSkinGallery();
            InitControl();          
            OnAccountChanged(CurrentAccount);
            LoadInitRoutine(_cfgMgr.CfgSys.EnableLastRoutine, _cfgMgr.CfgSys.LastRoutinePath);
        }

        /// <summary>
        /// 初始化字段和属性
        /// </summary>
        private void InitFieldAndProperty()
        {
            _cfgMgr = ProLaminator.Config.CfgManager.Instance;

            LangVersion = _cfgMgr.CfgSys.LanguageVersion;
            if (_cfgMgr.CfgCam != null)
                _viewerNumber = _cfgMgr.CfgCam.PropertyBList.Count;

            if (_multiViewModule == null)
            {
                _multiViewModule = new DerivedControl.MultiViewModule(LangVersion, _cfgMgr);
                _multiViewModule.Parent = this.pcRoot;
                _multiViewModule.Dock = System.Windows.Forms.DockStyle.Fill;
                this.pcRoot.Controls.Add(_multiViewModule);
                _multiViewModule.ViewerStateChangedEvent += MultiViewModule_ViewerStateChangedEvent;
            }

            if (CurrentAccount==null)
                CurrentAccount = new ProCommon.Communal.Account(0,"acc_annoymous");

            CurrentAccount.Name = _IsChinese ? "匿名者" : "Annoymous";
            CurrentAccount.Authority = ProCommon.Communal.AccountAuthority.None;
            CurrentAccount.PassWord = string.Empty;

            CameraMgr = ProLaminator.Device.CameraManager.Instance;
            CameraForGlass = ProLaminator.Device.CameraManager.Instance.CamForGlass;
            CameraForMembrane1 = ProLaminator.Device.CameraManager.Instance.CamForMembrane1;
            CameraForMembrane2 = ProLaminator.Device.CameraManager.Instance.CamForMembrane2;           

              _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 500;
            _timer.Tick += Timer_Tick;
            _timer.Enabled = true;

            if(_autoLaunch==null)
                _autoLaunch = new ProCommon.Communal.AutoLaunch();
            _autoLaunch.QuickName= AppName;
            _autoLaunch.SetAutoStart(ProLaminator.Config.CfgManager.Instance.CfgSys.EnableAutoLaunch);

            _warningText = _IsChinese ?
              "运行状态！请停止运行后访问" : "Running!Acess to it after stop";
            _warningCaption = _IsChinese ?
                "警告信息" : "Warning Message";
            LogList = new BindingList<Data.Log>();
        }

        /// <summary>
        /// 视图状态切换回调函数
        /// </summary>
        /// <param name="isMultiViewer"></param>
        private void MultiViewModule_ViewerStateChangedEvent(bool isMultiViewer)
        {
            this.bsbiRoutine.Enabled = isMultiViewer;
            this.bsbiSet.Enabled = isMultiViewer;

            this.bsbiAccount.Enabled = isMultiViewer;
            this.bsbiAbout.Enabled = isMultiViewer;          
            this.bbiRunContinue.Enabled = isMultiViewer;
            this.bbiRunStop.Enabled = isMultiViewer;
            this.bbiExitSystem.Enabled = isMultiViewer;
           
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
           UpdateBarStaticItem(this.bstiCurrentTime, LangVersion, DateTime.Now.ToString());
        }

        /// <summary>
        /// 初始化皮肤
        /// </summary>
        private void InitSkinGallery()
        {

        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;            
            UpdateControl();

            string itm = _IsChinese ? "软件系统" : "Application";
            string description = _IsChinese ? "更新控件" : "Update controls";
            UpdateLog(new Data.Log("", itm, description));
        }

        private void UpdateControl()
        {
            ShowInfo();
            UpdateMenuBarItem(LangVersion);
            UpdateResultIetm(LangVersion);
            UpdateStatusBarItem(LangVersion);
        }

        /// <summary>
        /// 更新菜单栏控件
        /// </summary>
        /// <param name="lan"></param>
        private void UpdateMenuBarItem(ProCommon.Communal.Language lan)
        {
            UpdateBarSubItem(this.bsbiRoutine, lan);
            UpdateBarButtonItem(this.bbiNewRoutine, lan);
            UpdateBarButtonItem(this.bbiLoadRoutine, lan);
            UpdateBarButtonItem(this.bbiManageRoutine, lan);

            UpdateBarSubItem(this.bsbiSet, lan);
            UpdateBarButtonItem(this.bbiSetSystem, lan);           

            UpdateBarSubItem(this.bsbiAccount, lan);
            UpdateBarButtonItem(this.bbiLogIn, lan);
            UpdateBarButtonItem(this.bbiManageAccount, lan);
            UpdateBarButtonItem(this.bbiLogOut, lan);

            UpdateBarSubItem(this.bsbiAbout, lan);
            UpdateBarButtonItem(this.bbiVendorInfo, lan);
            UpdateBarButtonItem(this.bbiAppInfo, lan);
        
            UpdateBarButtonItem(this.bbiRunContinue, lan);
            UpdateBarButtonItem(this.bbiRunStop, lan);
            UpdateBarButtonItem(this.bbiExitSystem, lan);
         

            UpdateBarEditItem(this.beiCurrentRoutine, lan);

        }

        /// <summary>
        /// BarButton单击事件--待完善
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bbi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string txt1, txt2, caption;
            switch (e.Item.Tag.ToString())
            {
                #region 新建程式
                case "BBI_NEWROUTINE":
                    {
                        if (!ProLaminator.Logic.SystemManager.Instance.IsRunning)
                        {
                            if (CurrentAccount != null
                                && CurrentAccount.Authority == ProCommon.Communal.AccountAuthority.Administrator)
                            {
                                UI.FrmNewRoutine frmNewRoutine = new FrmNewRoutine(LangVersion);
                                frmNewRoutine.ShowDialog();
                                if (frmNewRoutine.IsConfirmed)
                                {
                                    _routineName = frmNewRoutine.NewRoutineName;
                                    _routineDirectory = _cfgMgr.CfgSys.RoutineDirectory + "\\" + _routineName;

                                    if (System.IO.Directory.Exists(_routineDirectory))
                                    {
                                        txt1 = _IsChinese ? "系统已经存在相同名称项目\r\n是否覆盖[" : "Exist the same project!Would you recover [";
                                        txt2 = _IsChinese ? "]?" : "]?";
                                        caption = _IsChinese ? "询问信息" : "Question Message";

                                        if (ProCommon.DerivedForm.FrmMsgBox.Show(txt1 + _routineName + txt2, caption,
                                            ProCommon.DerivedForm.MyButtons.YesNo, ProCommon.DerivedForm.MyIcon.Question, _IsChinese) == DialogResult.Yes)
                                        {
                                            System.IO.Directory.Delete(_routineDirectory, true);                                          
                                        }
                                        else { return; }
                                    }

                                    string itm = _IsChinese ? "用户操作" : "User Operation";
                                    string description = _IsChinese ? "新建程式" : "Create new routine";
                                    UpdateLog(new Data.Log("", itm, description));
                                   
                                    _cfgVisionPara = null;
                                    try
                                    {
                                        System.IO.Directory.CreateDirectory(_routineDirectory); //程式名称命名的文件夹

                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\Image_All\\Glass"); //程式名称命名文件夹保存全部图像文件夹
                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\Image_OK\\Glass");//程式名称命名文件夹下保存OK图像的文件夹
                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\Image_NG\\Glass");//程式名称命名文件夹下保存NG图像的文件夹
                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\LocationPara\\Glass");//程式名称命名文件夹下保存定位参数的文件夹

                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\Image_All\\Membrane1"); //程式名称命名文件夹保存全部图像文件夹
                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\Image_OK\\Membrane1");//程式名称命名文件夹下保存OK图像的文件夹
                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\Image_NG\\Membrane1");//程式名称命名文件夹下保存NG图像的文件夹
                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\LocationPara\\Membrane1");//程式名称命名文件夹下保存定位参数的文件夹

                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\Image_All\\Membrane2"); //程式名称命名文件夹保存全部图像文件夹
                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\Image_OK\\Membrane2");//程式名称命名文件夹下保存OK图像的文件夹
                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\Image_NG\\Membrane2");//程式名称命名文件夹下保存NG图像的文件夹
                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\LocationPara\\Membrane2");//程式名称命名文件夹下保存定位参数的文件夹

                                        this.beiCurrentRoutine.EditValue = _routineName;

                                        //创建一个视觉参数配置类实例
                                        if(_cfgVisionPara==null)
                                            _cfgVisionPara = new Config.CfgVisionPara();

                                        _cfgVisionPara.RoutineDirectory = _routineDirectory;
                                        _cfgVisionPara.RoutineName = _routineName;

                                        _cfgVisionPara.ParaForGlass.PathForSaveImageAll = _routineDirectory + "\\Image_All\\Glass";
                                        _cfgVisionPara.ParaForGlass.PathForSaveImageNG = _routineDirectory + "\\Image_NG\\Glass";
                                        _cfgVisionPara.ParaForGlass.PathForSaveImageOK = _routineDirectory + "\\Image_OK\\Glass";
                                        _cfgVisionPara.ParaForGlass.PathForSaveLocPara = _routineDirectory + "\\LocationPara\\Glass";
                                        _cfgVisionPara.ParaForGlass.CameraExposure = 0.8f;
                                        _cfgVisionPara.ParaForGlass.CameraGain = 1.0f;
                                        _cfgVisionPara.ParaForGlass.Gamma = 1.0f;

                                        _cfgVisionPara.ParaForMembrane1.PathForSaveImageAll = _routineDirectory + "\\Image_All\\Membrane1";
                                        _cfgVisionPara.ParaForMembrane1.PathForSaveImageNG = _routineDirectory + "\\Image_NG\\Membrane1";
                                        _cfgVisionPara.ParaForMembrane1.PathForSaveImageOK = _routineDirectory + "\\Image_OK\\Membrane1";
                                        _cfgVisionPara.ParaForMembrane1.PathForSaveLocPara = _routineDirectory + "\\LocationPara\\Membrane1";
                                        _cfgVisionPara.ParaForMembrane1.CameraExposure = 0.5f;
                                        _cfgVisionPara.ParaForMembrane1.CameraGain = 1.0f;
                                        _cfgVisionPara.ParaForMembrane1.Gamma = 1.0f;

                                        _cfgVisionPara.ParaForMembrane2.PathForSaveImageAll = _routineDirectory + "\\Image_All\\Membrane2";
                                        _cfgVisionPara.ParaForMembrane2.PathForSaveImageNG = _routineDirectory + "\\Image_NG\\Membrane2";
                                        _cfgVisionPara.ParaForMembrane2.PathForSaveImageOK = _routineDirectory + "\\Image_OK\\Membrane2";
                                        _cfgVisionPara.ParaForMembrane2.PathForSaveLocPara = _routineDirectory + "\\LocationPara\\Membrane2";
                                        _cfgVisionPara.ParaForMembrane2.CameraExposure = 0.5f;
                                        _cfgVisionPara.ParaForMembrane2.CameraGain = 1.0f;
                                        _cfgVisionPara.ParaForMembrane2.Gamma = 1.0f;

                                        _cfgMgr.CfgVsPara= _cfgVisionPara;
                                    }
                                    catch (Exception ex)
                                    {
                                        _cfgVisionPara = null;
                                        txt1 = _IsChinese ? "创建程式[" : "Create routine[";
                                        txt2 = _IsChinese ? "]失败!\r\n异常描述:\r\n" : "]failed!\r\n Error description:\r\n";
                                        caption = _IsChinese ? "错误信息" : "Error Message";
                                        ProCommon.DerivedForm.FrmMsgBox.Show(txt1 + _routineName + txt2 + ex.Message, caption,
                                            ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Error, _IsChinese);
                                    }
                                    finally
                                    {
                                    }
                                }
                            }
                            else
                            {
                                txt1 = _IsChinese ? "无法操作!\r\n未登录或未达权限。" : "Denied!\r\n No account or authority .";
                                caption = _IsChinese ? "警告信息" : "Warning Message";
                                ProCommon.DerivedForm.FrmMsgBox.Show(txt1, caption,
                                          ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                            }
                        }
                        else
                        {
                            ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                        ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                        }
                    }
                    break;
                #endregion

                #region 加载程式
                case "BBI_LOADROUTINE":
                    {
                        if (!ProLaminator.Logic.SystemManager.Instance.IsRunning)
                        {
                            if (CurrentAccount != null
                                && CurrentAccount.Authority != ProCommon.Communal.AccountAuthority.None)
                            {

                                System.Windows.Forms.OpenFileDialog ofd = new OpenFileDialog();
                                ofd.Title = _IsChinese ? "选择运行程式" : "Select the routine";
                                ofd.InitialDirectory = ProLaminator.Config.CfgManager.Instance.CfgSys.RoutineDirectory;
                                ofd.Filter = _IsChinese ? "运行程式(*.pro)|*.pro" : "Run routine(*.pro)|*.pro";
                                ofd.Multiselect = false;
                                if (ofd.ShowDialog() == DialogResult.OK)
                                {
                                    _routineName = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
                                    _routineDirectory = System.IO.Path.GetDirectoryName(ofd.FileName);
                                    this.beiCurrentRoutine.EditValue = _routineName;

                                    _cfgVisionPara = null;

                                    //加载指定文件并反序列化为视觉参数类实例                                   
                                    try
                                    {
                                        using (var fs = new System.IO.FileStream(_routineDirectory + "\\" + _routineName + ".pro", System.IO.FileMode.Open))
                                        {
                                            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                                            _cfgVisionPara = (ProLaminator.Config.CfgVisionPara)bf.Deserialize(fs);
                                        }

                                        _cfgMgr.CfgVsPara= _cfgVisionPara;

                                        //增加设置相机参数--2020-07-21
                                        CameraMgr.SetIsInitProcess(true);
                                        CameraForGlass.API.SetExposureTime(_cfgVisionPara.ParaForGlass.CameraExposure);
                                        CameraForGlass.API.SetGain(_cfgVisionPara.ParaForGlass.CameraGain);
                                        CameraForGlass.API.SetGamma(_cfgVisionPara.ParaForGlass.Gamma);
                                        System.Threading.Thread.Sleep(10);

                                        CameraForMembrane1.API.SetExposureTime(_cfgVisionPara.ParaForMembrane1.CameraExposure);
                                        CameraForMembrane1.API.SetGain(_cfgVisionPara.ParaForMembrane1.CameraGain);
                                        CameraForMembrane1.API.SetGamma(_cfgVisionPara.ParaForMembrane1.Gamma);
                                        System.Threading.Thread.Sleep(10);

                                        CameraForMembrane2.API.SetExposureTime(_cfgVisionPara.ParaForMembrane2.CameraExposure);
                                        CameraForMembrane2.API.SetGain(_cfgVisionPara.ParaForMembrane2.CameraGain);
                                        CameraForMembrane2.API.SetGamma(_cfgVisionPara.ParaForMembrane2.Gamma);


                                        string itm = _IsChinese ? "用户操作" : "User Operation";
                                        string description = _IsChinese ? "加载程式" : "Load existed routine";
                                        UpdateLog(new Data.Log("", itm, description));
                                    }
                                    catch (System.Exception ex)
                                    {
                                        txt1 = _IsChinese ? "加载程式参数失败!\r\n" : "Load routine parameter failed !\r\n";
                                        caption = _IsChinese ? "错误信息" : "Error Message";
                                        ProCommon.DerivedForm.FrmMsgBox.Show(txt1 + ex.Message, caption,
                                        ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Error, _IsChinese);
                                    }
                                }
                            }
                            else
                            {
                                txt1 = _IsChinese ? "无法操作!\r\n未登录或未达权限。" : "Denied!\r\n No account or authority .";
                                caption = _IsChinese ? "警告信息" : "Warning Message";
                                ProCommon.DerivedForm.FrmMsgBox.Show(txt1, caption,
                                          ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                            }
                        }
                        else
                        {
                            ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                        ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                        }
                    }
                    break;
                #endregion

                #region 程式管理
                case "BBI_MANAGEROUTINE":
                    {
                        if (!ProLaminator.Logic.SystemManager.Instance.IsRunning)
                        {
                            if (CurrentAccount != null
                                && CurrentAccount.Authority == ProCommon.Communal.AccountAuthority.Administrator)
                            {
                                this.Hide();
                                UI.FrmRoutineManager frm = new FrmRoutineManager(LangVersion, _cfgMgr.CfgSys);
                                frm.ShowDialog();

                                if (frm.IsNewCommand)
                                {
                                    _routineName = frm.RoutineName;
                                    _routineDirectory = frm.RoutineDirectorySelected;
                                    this.beiCurrentRoutine.EditValue = _routineName;

                                    _cfgVisionPara = null;

                                    try
                                    {
                                        System.IO.Directory.CreateDirectory(_routineDirectory); //程式名称命名的文件夹

                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\Image_All\\Glass"); //程式名称命名文件夹保存全部图像文件夹
                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\Image_OK\\Glass");//程式名称命名文件夹下保存OK图像的文件夹
                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\Image_NG\\Glass");//程式名称命名文件夹下保存NG图像的文件夹
                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\LocationPara\\Glass");//程式名称命名文件夹下保存定位参数的文件夹

                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\Image_All\\Membrane1"); //程式名称命名文件夹保存全部图像文件夹
                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\Image_OK\\Membrane1");//程式名称命名文件夹下保存OK图像的文件夹
                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\Image_NG\\Membrane1");//程式名称命名文件夹下保存NG图像的文件夹
                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\LocationPara\\Membrane1");//程式名称命名文件夹下保存定位参数的文件夹

                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\Image_All\\Membrane2"); //程式名称命名文件夹保存全部图像文件夹
                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\Image_OK\\Membrane2");//程式名称命名文件夹下保存OK图像的文件夹
                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\Image_NG\\Membrane2");//程式名称命名文件夹下保存NG图像的文件夹
                                        System.IO.Directory.CreateDirectory(_routineDirectory + "\\LocationPara\\Membrane2");//程式名称命名文件夹下保存定位参数的文件夹

                                      
                                        //创建一个视觉参数配置类实例
                                        if (_cfgVisionPara == null)
                                            _cfgVisionPara = new Config.CfgVisionPara();

                                        _cfgVisionPara.RoutineDirectory = _routineDirectory;
                                        _cfgVisionPara.RoutineName = _routineName;

                                        _cfgVisionPara.ParaForGlass.PathForSaveImageAll = _routineDirectory + "\\Image_All\\Glass";
                                        _cfgVisionPara.ParaForGlass.PathForSaveImageNG = _routineDirectory + "\\Image_NG\\Glass";
                                        _cfgVisionPara.ParaForGlass.PathForSaveImageOK = _routineDirectory + "\\Image_OK\\Glass";
                                        _cfgVisionPara.ParaForGlass.PathForSaveLocPara = _routineDirectory + "\\LocationPara\\Glass";
                                        _cfgVisionPara.ParaForGlass.CameraExposure = 0.8f;
                                        _cfgVisionPara.ParaForGlass.CameraGain = 1.0f;
                                        _cfgVisionPara.ParaForGlass.Gamma = 1.0f;

                                        _cfgVisionPara.ParaForMembrane1.PathForSaveImageAll = _routineDirectory + "\\Image_All\\Membrane1";
                                        _cfgVisionPara.ParaForMembrane1.PathForSaveImageNG = _routineDirectory + "\\Image_NG\\Membrane1";
                                        _cfgVisionPara.ParaForMembrane1.PathForSaveImageOK = _routineDirectory + "\\Image_OK\\Membrane1";
                                        _cfgVisionPara.ParaForMembrane1.PathForSaveLocPara = _routineDirectory + "\\LocationPara\\Membrane1";
                                        _cfgVisionPara.ParaForMembrane1.CameraExposure = 0.5f;
                                        _cfgVisionPara.ParaForMembrane1.CameraGain = 1.0f;
                                        _cfgVisionPara.ParaForMembrane1.Gamma = 1.0f;

                                        _cfgVisionPara.ParaForMembrane2.PathForSaveImageAll = _routineDirectory + "\\Image_All\\Membrane2";
                                        _cfgVisionPara.ParaForMembrane2.PathForSaveImageNG = _routineDirectory + "\\Image_NG\\Membrane2";
                                        _cfgVisionPara.ParaForMembrane2.PathForSaveImageOK = _routineDirectory + "\\Image_OK\\Membrane2";
                                        _cfgVisionPara.ParaForMembrane2.PathForSaveLocPara = _routineDirectory + "\\LocationPara\\Membrane2";
                                        _cfgVisionPara.ParaForMembrane2.CameraExposure = 0.5f;
                                        _cfgVisionPara.ParaForMembrane2.CameraGain = 1.0f;
                                        _cfgVisionPara.ParaForMembrane2.Gamma = 1.0f;

                                        _cfgMgr.CfgVsPara = _cfgVisionPara;
                                    }
                                    catch (Exception ex)
                                    {
                                        _cfgVisionPara = null;
                                        txt1 = _IsChinese ? "创建程式[" : "Create routine[";
                                        txt2 = _IsChinese ? "]失败!\r\n异常描述:\r\n" : "]failed!\r\n Error description:\r\n";
                                        caption = _IsChinese ? "错误信息" : "Error Message";
                                        ProCommon.DerivedForm.FrmMsgBox.Show(txt1 + _routineName + txt2 + ex.Message, caption,
                                            ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Error, _IsChinese);
                                    }
                                    finally
                                    {
                                    }

                                    string itm = _IsChinese ? "用户操作" : "User Operation";
                                    string description = _IsChinese ? "新建程式" : "Create new routine";
                                    UpdateLog(new Data.Log("", itm, description));
                                }                               
                                else if (frm.IsLoadCommand)
                                {
                                    //加载指定文件并反序列化为视觉参数类实例
                                    _routineName = frm.RoutineName;
                                    _routineDirectory = frm.RoutineDirectorySelected;
                                    this.beiCurrentRoutine.EditValue = _routineName;

                                    _cfgVisionPara = null;
                                    if (System.IO.File.Exists(_routineDirectory + "\\" + _routineName + ".pro"))
                                    {
                                        try
                                        {
                                            using (var fs = new System.IO.FileStream(_routineDirectory + "\\" + _routineName + ".pro", System.IO.FileMode.Open))
                                            {
                                                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                                                _cfgVisionPara = (ProLaminator.Config.CfgVisionPara)bf.Deserialize(fs);
                                            }

                                            _cfgMgr.CfgVsPara = _cfgVisionPara;

                                            //增加设置相机参数--2020-07-21

                                            CameraMgr.SetIsInitProcess(true);
                                            CameraForGlass.API.SetExposureTime(_cfgVisionPara.ParaForGlass.CameraExposure);
                                            CameraForGlass.API.SetGain(_cfgVisionPara.ParaForGlass.CameraGain);
                                            CameraForGlass.API.SetGamma(_cfgVisionPara.ParaForGlass.Gamma);                                           
                                            System.Threading.Thread.Sleep(10);

                                            CameraForMembrane1.API.SetExposureTime(_cfgVisionPara.ParaForMembrane1.CameraExposure);
                                            CameraForMembrane1.API.SetGain(_cfgVisionPara.ParaForMembrane1.CameraGain);
                                            CameraForMembrane1.API.SetGamma(_cfgVisionPara.ParaForMembrane1.Gamma);
                                            System.Threading.Thread.Sleep(10);

                                            CameraForMembrane2.API.SetExposureTime(_cfgVisionPara.ParaForMembrane2.CameraExposure);
                                            CameraForMembrane2.API.SetGain(_cfgVisionPara.ParaForMembrane2.CameraGain);
                                            CameraForMembrane2.API.SetGamma(_cfgVisionPara.ParaForMembrane2.Gamma);                                           

                                            string itm = _IsChinese ? "用户操作" : "User Operation";
                                            string description = _IsChinese ? "加载程式" : "Load existed routine";
                                            UpdateLog(new Data.Log("", itm, description));

                                        }
                                        catch (System.Exception ex)
                                        {
                                            txt1 = _IsChinese ? "加载程式参数失败!\r\n" : "Load routine parameter failed !\r\n";
                                            caption = _IsChinese ? "错误信息" : "Error Message";
                                            ProCommon.DerivedForm.FrmMsgBox.Show(txt1 + ex.Message, caption,
                                            ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Error, _IsChinese);
                                        }
                                    }
                                    else
                                    {
                                        txt1 = _IsChinese ? "选择程式未创建参数文件!是否现在创建?" : "Routine selected has no parameter file ! Create it now ?";
                                        caption = _IsChinese ? "询问信息" : "Question Message";
                                        if (ProCommon.DerivedForm.FrmMsgBox.Show(txt1, caption,
                                            ProCommon.DerivedForm.MyButtons.YesNo, ProCommon.DerivedForm.MyIcon.Question, _IsChinese) == DialogResult.Yes)
                                        {
                                            //用上次程式的参数作为初始参数进行创建
                                            _cfgVisionPara = _cfgMgr.CfgVsPara;
                                        }
                                    }
                                }
                                else if (frm.IsDeleteCommand)
                                {
                                    //判断删除的程式是否当前显示的程式，若是,则当前显示的程式为空
                                    this.beiCurrentRoutine.EditValue = null;
                                    _cfgVisionPara = null;
                                    try
                                    {
                                        if (System.IO.File.Exists(_routineDirectory + "\\" + _routineName + ".pro"))
                                        {
                                            System.IO.File.Delete(_routineDirectory + "\\" + _routineName + ".pro");
                                        }

                                        string itm = _IsChinese ? "用户操作" : "User Operation";
                                        string description = _IsChinese ? "删除程式" : "Delete existed routine";
                                        UpdateLog(new Data.Log("", itm, description));
                                    }
                                    catch (System.Exception ex)
                                    {
                                        txt1 = _IsChinese ? "删除程式参数失败!\r\n" : "Delete routine parameter failed !\r\n";
                                        caption = _IsChinese ? "错误信息" : "Error Message";
                                        ProCommon.DerivedForm.FrmMsgBox.Show(txt1 + ex.Message, caption,
                                         ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Error, _IsChinese);
                                    }
                                }
                                else if (frm.IsClearCommand)
                                {
                                    this.beiCurrentRoutine.EditValue = null;
                                    _cfgVisionPara = null;
                                    try
                                    {
                                        if (System.IO.Directory.Exists(_routineDirectory))
                                        {
                                            System.IO.Directory.Delete(_routineDirectory, true);
                                        }

                                        string itm = _IsChinese ? "用户操作" : "User Operation";
                                        string description = _IsChinese ? "清空程式" : "Clear all routines";
                                        UpdateLog(new Data.Log("", itm, description));
                                    }
                                    catch (System.Exception ex)
                                    {
                                        txt1 = _IsChinese ? "清空程式参数失败!\r\n" : "Clear routine parameter failed !\r\n";
                                        caption = _IsChinese ? "错误信息" : "Error Message";
                                        ProCommon.DerivedForm.FrmMsgBox.Show(txt1 + ex.Message, caption,
                                         ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Error, _IsChinese);
                                    }
                                }

                                this.Show();
                            }
                            else
                            {
                                txt1 = _IsChinese ? "无法操作!\r\n未登录或未达权限。" : "Denied!\r\n No account or authority .";
                                caption = _IsChinese ? "警告信息" : "Warning Message";
                                ProCommon.DerivedForm.FrmMsgBox.Show(txt1, caption,
                                         ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                            }
                        }
                        else
                        {
                            ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                        ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                        }
                    }
                    break;
                #endregion

                #region 系统设置
                case "BBI_SETSYSTEM":
                    {
                        if (!ProLaminator.Logic.SystemManager.Instance.IsRunning)
                        {
                            if (CurrentAccount != null
                               && CurrentAccount.Authority == ProCommon.Communal.AccountAuthority.Administrator)
                            {
                                //根据当前语言版本,显示系统设置窗体,隐藏主窗体,系统设置完成后,显示主窗体

                                /*

                                ProArmband.UI.UI_SetConfigs uiSetConfig = new UI_SetConfigs(LanguageVersion, Manager.ConfigManager.Instance);
                                uiSetConfig.DevBoard = (ProArmband.Device.Device_HZZHBoardCtrller)ProArmband.Manager.DeviceManager.Instance.DeviceList[ProCommon.Communal.CtrllerCategory.Board];
                                uiSetConfig.SpecifiedBoard = ProArmband.Manager.SystemManager.Instance.BoardCtrllerForArmband;
                                uiSetConfig.DevCamera = (ProArmband.Device.Device_Camera)ProArmband.Manager.DeviceManager.Instance.DeviceList[ProCommon.Communal.CtrllerCategory.Camera];
                                uiSetConfig.Show(this);

                                */

                                string itm = _IsChinese ? "用户操作" : "User Operation";
                                string description = _IsChinese ? "设置系统参数" : "Set system parameter";
                                UpdateLog(new Data.Log("", itm, description));
                            }
                            else
                            {
                                txt1 = _IsChinese ? "无法操作!\r\n未登录或未达权限。" : "Denied!\r\n No account or authority .";
                                caption = _IsChinese ? "警告信息" : "Warning Message";
                                ProCommon.DerivedForm.FrmMsgBox.Show(txt1, caption,
                                          ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                            }
                        }
                        else
                        {
                            ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                        ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                        }
                    }
                    break;
                #endregion

                #region 运控设置
                case "BBI_SETMOTION":
                    {
                        if (!ProLaminator.Logic.SystemManager.Instance.IsRunning)
                        {
                            if (CurrentAccount != null
                                && CurrentAccount.Authority != ProCommon.Communal.AccountAuthority.None)
                            {
                                /*
                                ProArmband.UI.UI_MotionJog uiMotionJog = new UI_MotionJog(LanguageVersion,
                                   BoardForArmband.AxisList, BoardForArmband.InVarObjList, BoardForArmband.OutVarObjList);
                                uiMotionJog.SpecifiedBoard = this.BoardForArmband;
                                uiMotionJog.DevBoard = this.DevBoard;
                                uiMotionJog.Show(this);

                                */
                            }
                            else
                            {
                                txt1 = _IsChinese ? "无法操作!\r\n未登录或未达权限。" : "Denied!\r\n No account or authority .";
                                caption = _IsChinese ? "警告信息" : "Warning Message";
                                ProCommon.DerivedForm.FrmMsgBox.Show(txt1, caption,
                                           ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                            }
                        }
                        else
                        {
                            ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                       ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                        }
                    }
                    break;
                #endregion

                #region 登录
                case "BBI_LOGIN":
                    {
                        if (!ProLaminator.Logic.SystemManager.Instance.IsRunning)
                        {
                            //根据当前语言版本,显示登录窗体,隐藏主窗体,登录成功后,显示主窗体,根据登录账户更新显示控件(账户操作权限分配待做)
                            UI.UILogIn uiLogin = new UILogIn(LangVersion, _cfgMgr.CfgAcc);

                            if (uiLogin.ShowDialog(this)!= DialogResult.OK)
                            {
                                if (CurrentAccount == null)
                                {
                                    CurrentAccount.Authority = ProCommon.Communal.AccountAuthority.None;
                                    CurrentAccount.Name = _IsChinese?"匿名":"Annoymous";
                                    CurrentAccount.PassWord = string.Empty;
                                }
                            }
                            else
                            {
                                CurrentAccount.Name = uiLogin.CurrentAccount.Name;
                                CurrentAccount.Authority = uiLogin.CurrentAccount.Authority;
                                CurrentAccount.PassWord = uiLogin.CurrentAccount.PassWord;
                            }
                                                     
                            OnAccountChanged(CurrentAccount);
                        }
                        else
                        {
                            ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                        ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                        }
                    }
                    break;
                #endregion

                #region 账户管理
                case "BBI_MANAGEACCOUNT":
                    {
                        if (!ProLaminator.Logic.SystemManager.Instance.IsRunning)
                        {
                            //根据当前语言版本,显示账户管理窗体,隐藏主窗体,账户管理完成后,显示主窗体
                            if (CurrentAccount != null)
                            {
                                UI.UIAccountManager uiAccountManager = new UIAccountManager(LangVersion, CurrentAccount, _cfgMgr);
                                uiAccountManager.ShowDialog(this);
                            }
                        }
                        else
                        {
                            ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                        ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                        }
                    }
                    break;
                #endregion

                #region 登出
                case "BBI_LOGOUT":
                    {
                        if (!ProLaminator.Logic.SystemManager.Instance.IsRunning)
                        {

                            //账户恢复默认账户,根据登录账户更新显示控件(账户操作权限分配待做)
                            if (CurrentAccount != null)
                            {
                                CurrentAccount.Authority = ProCommon.Communal.AccountAuthority.None;
                                CurrentAccount.Name = _IsChinese?"匿名":"Annoymous";
                                CurrentAccount.PassWord = string.Empty;
                            }

                            OnAccountChanged(CurrentAccount);
                        }
                        else
                        {
                            ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                        ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                        }
                    }
                    break;
                #endregion

                #region 厂商信息
                case "BBI_VENDORINFO":
                    {
                        if (!ProLaminator.Logic.SystemManager.Instance.IsRunning)
                        {

                            //调用WebBrowswer，访问厂商主页  
                            ProCommon.DerivedForm.FrmMsgBox.Show("错误图标", "测试窗口1", ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Error, true);

                            ProCommon.DerivedForm.FrmMsgBox.Show("错误图标", "测试窗口2", ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Error, true);
                        }
                        else
                        {
                            ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                         ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                        }
                    }
                    break;
                #endregion

                #region 软件信息
                case "BBI_APPINFO":
                    {
                        if (!ProLaminator.Logic.SystemManager.Instance.IsRunning)
                        {

                            //根据当前语言版本,显示软件帮助窗体

                        }
                        else
                        {
                            ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                       ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                        }
                    }
                    break;
                #endregion

                #region 单次运行
                case "BBI_RUNONCE":
                    {
                        if (!ProLaminator.Logic.SystemManager.Instance.IsRunning)
                        {
                            if (CurrentAccount != null
                                && CurrentAccount.Authority != ProCommon.Communal.AccountAuthority.None)
                            {
                                if (!string.IsNullOrEmpty(_routineName))
                                {
                                    if (_cfgVisionPara!= null)
                                    {
                                        try
                                        {
                                            ProLaminator.Logic.SystemManager.Instance.IsRunOnce = true;
                                            ProLaminator.Logic.SystemManager.Instance.IsRunning = true;

                                            CameraMgr.SetImageToMultiViewer(); 
                                            CameraMgr.SetIsInitProcess(true);

                                            CameraForGlass.API.SetExposureTime(_cfgVisionPara.ParaForGlass.CameraExposure);
                                            CameraForGlass.API.SetGain(_cfgVisionPara.ParaForGlass.CameraGain);
                                            CameraForGlass.API.SetGamma(_cfgVisionPara.ParaForGlass.Gamma);
                                            CameraForGlass.API.SetAcquisitionMode(ProCommon.Communal.AcquisitionMode.SoftTrigger, 1);
                                            CameraForGlass.API.SoftTriggerOnce();
                                            System.Threading.Thread.Sleep(100);

                                            CameraForMembrane1.API.SetExposureTime(_cfgVisionPara.ParaForMembrane1.CameraExposure);
                                            CameraForMembrane1.API.SetGain(_cfgVisionPara.ParaForMembrane1.CameraGain);
                                            CameraForMembrane1.API.SetGamma(_cfgVisionPara.ParaForMembrane1.Gamma);
                                            CameraForMembrane1.API.SetAcquisitionMode(ProCommon.Communal.AcquisitionMode.SoftTrigger, 1);
                                            CameraForMembrane1.API.SoftTriggerOnce();
                                            System.Threading.Thread.Sleep(100);

                                            CameraForMembrane2.API.SetExposureTime(_cfgVisionPara.ParaForMembrane2.CameraExposure);
                                            CameraForMembrane2.API.SetGain(_cfgVisionPara.ParaForMembrane2.CameraGain);
                                            CameraForMembrane2.API.SetGamma(_cfgVisionPara.ParaForMembrane2.Gamma);
                                            CameraForMembrane2.API.SetAcquisitionMode(ProCommon.Communal.AcquisitionMode.SoftTrigger, 1);
                                            CameraForMembrane2.API.SoftTriggerOnce();

                                            this.bsbiRoutine.Enabled = true;
                                            this.bsbiSet.Enabled = true;
                                            this.bsbiAccount.Enabled = true;
                                            this.bsbiAbout.Enabled = true;                                         
                                            this.bbiRunContinue.Enabled = true;
                                            this.bbiRunStop.Enabled = true;
                                            this.bbiExitSystem.Enabled = true;
                                        }
                                        catch (System.Exception ex)
                                        {
                                            txt1 = _IsChinese ? "加载程式参数失败!\r\n" : "Load routine parameter failed !\r\n";
                                            caption = _IsChinese ? "错误信息" : "Error Message";
                                            ProCommon.DerivedForm.FrmMsgBox.Show(txt1 + ex.Message, caption,
                                            ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Error, _IsChinese);
                                        }
                                    }
                                    else
                                    {
                                        txt1 = _IsChinese ? "无法操作!\r\n未选择程式。" : "Denied!\r\n No routine selected .";
                                        caption = _IsChinese ? "警告信息" : "Warning Message";
                                        ProCommon.DerivedForm.FrmMsgBox.Show(txt1, caption,
                                        ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                                    }
                                }
                                else
                                {
                                    txt1 = _IsChinese ? "未加载程式!\r\n" : "No routine loaded !\r\n";
                                    caption = _IsChinese ? "错误信息" : "Error Message";
                                    ProCommon.DerivedForm.FrmMsgBox.Show(txt1, caption,
                                          ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Error, _IsChinese);
                                }
                            }
                            else
                            {
                                txt1 = _IsChinese ? "无法操作!\r\n未登录或未达权限。" : "Denied!\r\n No account or authority .";
                                caption = _IsChinese ? "警告信息" : "Warning Message";
                                ProCommon.DerivedForm.FrmMsgBox.Show(txt1, caption,
                                          ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                            }
                        }
                        else
                        {
                            ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                        ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                        }
                    }
                    break;
                #endregion

                #region 连续运行
                case "BBI_RUNCONTINUE":
                    {
                        if (!ProLaminator.Logic.SystemManager.Instance.IsRunning)
                        {
                            if (CurrentAccount != null
                                && CurrentAccount.Authority != ProCommon.Communal.AccountAuthority.None)
                            {
                                if (!string.IsNullOrEmpty(_routineName))
                                {
                                    if (_cfgVisionPara!= null)
                                    {
                                        try
                                        {
                                            ProLaminator.Logic.SystemManager.Instance.IsRunOnce = false;
                                            ProLaminator.Logic.SystemManager.Instance.IsRunning = true;

                                            int cnt = _multiViewModule.IconicResultViewers.Length;
                                            for(int i=0;i<cnt;i++)
                                                _multiViewModule.IconicResultViewers[i].EnableControl(false);

                                            CameraMgr.SetImageToMultiViewer();
                                            CameraMgr.SetIsInitProcess(true);

                                            CameraForGlass.API.SetExposureTime(_cfgVisionPara.ParaForGlass.CameraExposure);
                                            CameraForGlass.API.SetGain(_cfgVisionPara.ParaForGlass.CameraGain);
                                            CameraForGlass.API.SetGamma(_cfgVisionPara.ParaForGlass.Gamma);
                                            CameraForGlass.API.SetAcquisitionMode(ProCommon.Communal.AcquisitionMode.ExternalTrigger, 1);
                                            CameraForGlass.Property.EnableAlgorithm = true;

                                            CameraForMembrane1.API.SetExposureTime(_cfgVisionPara.ParaForMembrane1.CameraExposure);
                                            CameraForMembrane1.API.SetGain(_cfgVisionPara.ParaForMembrane1.CameraGain);
                                            CameraForMembrane1.API.SetGamma(_cfgVisionPara.ParaForMembrane1.Gamma);
                                            CameraForMembrane1.API.SetAcquisitionMode(ProCommon.Communal.AcquisitionMode.ExternalTrigger, 1);
                                            CameraForMembrane1.Property.EnableAlgorithm = true;

                                            CameraForMembrane2.API.SetExposureTime(_cfgVisionPara.ParaForMembrane2.CameraExposure);
                                            CameraForMembrane2.API.SetGain(_cfgVisionPara.ParaForMembrane2.CameraGain);
                                            CameraForMembrane2.API.SetGamma(_cfgVisionPara.ParaForMembrane2.Gamma);
                                            CameraForMembrane2.API.SetAcquisitionMode(ProCommon.Communal.AcquisitionMode.ExternalTrigger, 1);
                                            CameraForMembrane2.Property.EnableAlgorithm = true;

                                            this.bsbiRoutine.Enabled = false;
                                            this.bsbiSet.Enabled = false;
                                            this.bsbiAccount.Enabled = false;
                                            this.bsbiAbout.Enabled = false;                                         
                                            this.bbiRunContinue.Enabled = true;
                                            this.bbiRunStop.Enabled = true;
                                            this.bbiExitSystem.Enabled = false;
                                        }
                                        catch (System.Exception ex)
                                        {
                                            txt1 = _IsChinese ? "连续运行失败!\r\n" : "Run continue failed !\r\n";
                                            caption = _IsChinese ? "错误信息" : "Error Message";
                                            ProCommon.DerivedForm.FrmMsgBox.Show(txt1 + ex.Message, caption,
                                            ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Error, _IsChinese);
                                        }
                                    }
                                    else
                                    {
                                        txt1 = _IsChinese ? "无法操作!\r\n未选择程式。" : "Denied!\r\n No routine selected .";
                                        caption = _IsChinese ? "警告信息" : "Warning Message";
                                        ProCommon.DerivedForm.FrmMsgBox.Show(txt1, caption,
                                          ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                                    }
                                }
                                else
                                {
                                    txt1 = _IsChinese ? "未加载程式!\r\n" : "No routine loaded !\r\n";
                                    caption = _IsChinese ? "错误信息" : "Error Message";
                                    ProCommon.DerivedForm.FrmMsgBox.Show(txt1, caption,
                                         ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Error, _IsChinese);
                                }
                            }
                            else
                            {
                                txt1 = _IsChinese ? "无法操作!\r\n未登录或未达权限。" : "Denied!\r\n No account or authority .";
                                caption = _IsChinese ? "警告信息" : "Warning Message";
                                ProCommon.DerivedForm.FrmMsgBox.Show(txt1, caption,
                                          ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                            }
                        }
                        else
                        {
                            ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                        ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                        }
                    }
                    break;

                #endregion

                #region 停止运行
                case "BBI_RUNSTOP":
                    {
                        if (CurrentAccount != null
                               && CurrentAccount.Authority != ProCommon.Communal.AccountAuthority.None)
                        {
                            ProLaminator.Logic.SystemManager.Instance.IsRunOnce = true;
                            ProLaminator.Logic.SystemManager.Instance.IsRunning = false;                                                      

                            //停止,使相机暂时不工作
                            //迈德威视相机停止相机会释放相机资源,故只需要切换采集模式即可(非程序工作时的采集模式)
                            if (!CameraForGlass.API.SetAcquisitionMode(ProCommon.Communal.AcquisitionMode.SoftTrigger, 1))
                            {
                                txt1 = _IsChinese ? "相机连接异常!" : "Camera connection lost !";
                                caption = _IsChinese ? "警告信息" : "Warning Message";
                                ProCommon.DerivedForm.FrmMsgBox.Show(txt1, caption,
                                          ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                            }

                            int cnt = _multiViewModule.IconicResultViewers.Length;
                            for (int i = 0; i < cnt; i++)
                                _multiViewModule.IconicResultViewers[i].EnableControl(true);

                            this.bsbiRoutine.Enabled = true;
                            this.bsbiSet.Enabled = true;
                            this.bsbiAccount.Enabled = true;
                            this.bsbiAbout.Enabled = true;                          
                            this.bbiRunContinue.Enabled = true;
                            this.bbiRunStop.Enabled = true;                          
                            this.bbiExitSystem.Enabled = true;
                        }
                        else
                        {
                            txt1 = _IsChinese ? "无法操作!\r\n未登录或未达权限。" : "Denied!\r\n No account or authority .";
                            caption = _IsChinese ? "警告信息" : "Warning Message";
                            ProCommon.DerivedForm.FrmMsgBox.Show(txt1, caption,
                                          ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                        }
                    }
                    break;
                #endregion

                #region 产品统计
                case "BBI_PRODUCTSTATISTIC":
                    {
                        if (!ProLaminator.Logic.SystemManager.Instance.IsRunning)
                        {

                        }
                        else
                        {
                            ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                        ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                        }
                    }
                    break;
                #endregion

                #region 系统退出
                case "BBI_EXITSYSTEM":
                    {
                        if (!ProLaminator.Logic.SystemManager.Instance.IsRunning)
                        {
                            txt1 = _IsChinese ? "确认退出系统吗?" : "Exit the application ?";
                            caption = _IsChinese ? "询问信息" : "Question Message";
                            if (ProCommon.DerivedForm.FrmMsgBox.Show(txt1, caption,
                                ProCommon.DerivedForm.MyButtons.YesNo, ProCommon.DerivedForm.MyIcon.Question,_IsChinese) == DialogResult.Yes)
                            {
                                _cfgMgr.Save();                              
                                CameraMgr.Stop();
                                CameraMgr.Release();
                                this.Close();
                            }
                        }
                        else
                        {
                            ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                        ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, _IsChinese);
                        }
                    }
                    break;
                #endregion

                default:
                    break;
            }
        }

        /// <summary>
        /// 更新结果栏控件
        /// [注:只是更新提示,非结果]
        /// </summary>
        /// <param name="lan"></param>
        private void UpdateResultIetm(ProCommon.Communal.Language lan)
        {
           
        }

        /// <summary>
        /// SimpleButton单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sbtn_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton sbtn = sender as DevExpress.XtraEditors.SimpleButton;
            if (sbtn != null)
            {
                bool isChs = LangVersion == ProCommon.Communal.Language.Chinese;

                switch (sbtn.Tag.ToString())
                {
                    case "SBTN_COUNTCLEAR":
                        {
                            if (!ProLaminator.Logic.SystemManager.Instance.IsRunning)
                            {
                                if (CurrentAccount != null
                                    && CurrentAccount.Authority == ProCommon.Communal.AccountAuthority.Administrator)
                                {
                                   
                                }
                                else
                                {
                                    _warningText = isChs ? "无法操作!\r\n未登录或未达权限。" : "Denied!\r\n No account or authority .";
                                    ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                       ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, isChs);
                                }
                            }
                            else
                            {
                                ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                       ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, isChs);
                            }
                        }
                        break;
                    default: break;
                }
            }
        }

        /// <summary>
        /// 更新状态栏控件
        /// </summary>
        /// <param name="lan"></param>
        private void UpdateStatusBarItem(ProCommon.Communal.Language lan)
        {
            UpdateBarStaticItem(this.bstiClientName, lan, _cfgMgr.CfgSys.ClientName);
            UpdateBarStaticItem(this.bstiCurrentAccount, lan, CurrentAccount.Name);
            UpdateBarStaticItem(this.bstiCurrentTime, lan, null);

            #region 相机状态组
            UpdateBarSubItem(this.bsbiCameraStatus, lan, ProCommon.Communal.DeviceCategory.Camera, _cfgMgr);
            #endregion

            #region 板卡状态组          
            UpdateBarSubItem(this.bsbiBoardStatus, lan, ProCommon.Communal.DeviceCategory.Board, _cfgMgr);
            #endregion

            #region 串口状态组
            UpdateBarSubItem(this.bsbiSerialPortStatus, lan, ProCommon.Communal.DeviceCategory.SerialPort, _cfgMgr);
            #endregion
        }


        #region 辅助功能

        /// <summary>
        /// 更新BarStaticItem控件
        /// </summary>
        /// <param name="bsti"></param>
        /// <param name="lan"></param>
        /// <param name="prefix"></param>
        private void UpdateBarStaticItem(DevExpress.XtraBars.BarStaticItem bsti, ProCommon.Communal.Language lan, string prefix)
        {
            if (bsti != null
              && bsti.Tag != null)
            {
                bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                string str = bsti.Tag.ToString();
                bsti.Caption = (isChs ? ProLaminator.Properties.Resources.ResourceManager.GetString("chs_" + str) 
                    : ProLaminator.Properties.Resources.ResourceManager.GetString("en_" + str)) 
                    + ":" + prefix;
            }
        }

        /// <summary>
        /// 更新BarSubItem控件
        /// </summary>
        /// <param name="bsbi"></param>
        /// <param name="lan"></param>
        private void UpdateBarSubItem(DevExpress.XtraBars.BarSubItem bsbi, ProCommon.Communal.Language lan)
        {
            if (bsbi != null
              && bsbi.Tag != null)
            {
                bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                string str = bsbi.Tag.ToString();
                bsbi.Caption = isChs ? 
                    ProLaminator.Properties.Resources.ResourceManager.GetString("chs_" + str) 
                    : ProLaminator.Properties.Resources.ResourceManager.GetString("en_" + str);
            }
        }

        /// <summary>
        /// 更新BarSubItem控件
        /// </summary>
        /// <param name="bsbi"></param>
        /// <param name="lan"></param>
        /// <param name="ctrlCategory"></param>
        /// <param name="cfgManager"></param>
        private void UpdateBarSubItem(DevExpress.XtraBars.BarSubItem bsbi, ProCommon.Communal.Language lan, ProCommon.Communal.DeviceCategory ctrlCategory, ProLaminator.Config.CfgManager cfgManager)
        {
            UpdateBarSubItem(bsbi, lan);
            if (bsbi != null
                && cfgManager != null)
            {
                int ctrlCount = 0;
                bsbi.ItemLinks.Clear();
                switch (ctrlCategory)
                {
                    #region 相机
                    case ProCommon.Communal.DeviceCategory.Camera:
                        {
                            if(cfgManager.CfgCam!=null)
                            {
                                ctrlCount = cfgManager.CfgCam.PropertyList.Count;
                                if (ctrlCount > 0)
                                {
                                    for (int i = 0; i < ctrlCount; i++)
                                    {
                                        DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rpiImgCmbe = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
                                        UpdateRepositoryItemImgCmbe(rpiImgCmbe, lan);
                                        DevExpress.XtraBars.BarEditItem bei = new DevExpress.XtraBars.BarEditItem(this.bmgrMain, rpiImgCmbe);
                                        bei.Tag = cfgManager.CfgCam.PropertyList[i].ID;
                                        bei.EditValue = false;
                                        bei.EditWidth = 80;
                                        bei.Caption = cfgManager.CfgCam.PropertyList[i].Name;
                                        bsbi.AddItem(bei);
                                    }
                                }
                                else
                                {
                                    bsbi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                                }
                            }                           
                            else
                            {
                                bsbi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }
                        }
                        break;
                    #endregion

                    #region 板卡
                    case ProCommon.Communal.DeviceCategory.Board:
                        {
                            if(cfgManager.CfgBrd!=null)
                            {
                                ctrlCount = cfgManager.CfgBrd.PropertyList.Count;
                                if (ctrlCount > 0)
                                {
                                    for (int i = 0; i < ctrlCount; i++)
                                    {
                                        DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rpiImgCmbe = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
                                        UpdateRepositoryItemImgCmbe(rpiImgCmbe, lan);
                                        DevExpress.XtraBars.BarEditItem bei = new DevExpress.XtraBars.BarEditItem(this.bmgrMain, rpiImgCmbe);
                                        bei.Tag = cfgManager.CfgBrd.PropertyList[i].ID;
                                        bei.EditValue = false;
                                        bei.EditWidth = 80;
                                        bei.Caption = cfgManager.CfgBrd.PropertyList[i].Name;
                                        bsbi.AddItem(bei);
                                    }
                                }
                                else
                                {
                                    bsbi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                                }
                            }
                            else
                            {
                                bsbi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }
                        }
                        break;
                    #endregion

                    #region Socket
                    case ProCommon.Communal.DeviceCategory.Socket:
                        {
                            if(cfgManager.CfgSkt!=null)
                            {
                                ctrlCount = cfgManager.CfgSkt.PropertyList.Count;
                                if (ctrlCount > 0)
                                {
                                    for (int i = 0; i < ctrlCount; i++)
                                    {
                                        DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rpiImgCmbe = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
                                        UpdateRepositoryItemImgCmbe(rpiImgCmbe, lan);
                                        DevExpress.XtraBars.BarEditItem bei = new DevExpress.XtraBars.BarEditItem(this.bmgrMain, rpiImgCmbe);
                                        bei.Tag = cfgManager.CfgSkt.PropertyList[i].ID;
                                        bei.EditValue = false;
                                        bei.EditWidth = 120;
                                        bei.Caption = cfgManager.CfgSkt.PropertyList[i].Name;
                                        bsbi.AddItem(bei);
                                    }
                                }
                                else
                                {
                                    bsbi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                                }
                            }
                            else
                            {
                                bsbi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }
                        }
                        break;
                    #endregion

                    #region WebService
                    case ProCommon.Communal.DeviceCategory.Web:
                        {
                            if(cfgManager.CfgWb!=null)
                            {
                                ctrlCount = cfgManager.CfgWb.PropertyList.Count;
                                if (ctrlCount > 0)
                                {
                                    for (int i = 0; i < ctrlCount; i++)
                                    {
                                        DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rpiImgCmbe = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
                                        UpdateRepositoryItemImgCmbe(rpiImgCmbe, lan);
                                        DevExpress.XtraBars.BarEditItem bei = new DevExpress.XtraBars.BarEditItem(this.bmgrMain, rpiImgCmbe);
                                        bei.Tag = cfgManager.CfgWb.PropertyList[i].ID;
                                        bei.EditValue = false;
                                        bei.EditWidth = 120;
                                        bei.Caption = cfgManager.CfgWb.PropertyList[i].Name;
                                        bsbi.AddItem(bei);
                                    }
                                }
                                else
                                {
                                    bsbi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                                }
                            }
                            else
                            {
                                bsbi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }
                        }
                        break;
                    #endregion

                    #region PLC
                    case ProCommon.Communal.DeviceCategory.PLC:
                        break;
                    #endregion

                    #region SerialPort
                    case ProCommon.Communal.DeviceCategory.SerialPort:
                        {
                            if(cfgManager.CfgSrlPort!=null)
                            {
                                ctrlCount = cfgManager.CfgSrlPort.PropertyList.Count;
                                if (ctrlCount > 0)
                                {
                                    for (int i = 0; i < ctrlCount; i++)
                                    {
                                        DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rpiImgCmbe = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
                                        UpdateRepositoryItemImgCmbe(rpiImgCmbe, lan);
                                        DevExpress.XtraBars.BarEditItem bei = new DevExpress.XtraBars.BarEditItem(this.bmgrMain, rpiImgCmbe);
                                        bei.Tag = cfgManager.CfgSrlPort.PropertyList[i].ID;
                                        bei.EditValue = false;
                                        bei.EditWidth = 80;
                                        bei.Caption = cfgManager.CfgSrlPort.PropertyList[i].Name;
                                        bsbi.AddItem(bei);
                                    }
                                }
                                else
                                {
                                    bsbi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                                }
                            }
                            else
                            {
                                bsbi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }
                        }
                        break;
                    #endregion

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 更新RepositoryItemImageComboBoxEdit
        /// </summary>
        /// <param name="rpiImgCmbe"></param>
        /// <param name="lan"></param>
        private void UpdateRepositoryItemImgCmbe(DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rpiImgCmbe, ProCommon.Communal.Language lan)
        {
            if (rpiImgCmbe != null)
            {
                bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                rpiImgCmbe.Items.Clear();

                rpiImgCmbe.SmallImages = this.imgColSmall;
                rpiImgCmbe.LargeImages = this.imgColLarge;

                string onStr = isChs ? 
                    ProLaminator.Properties.Resources.ResourceManager.GetString("chs_ONLINE") 
                    : Properties.Resources.ResourceManager.GetString("en_ONLINE");
                string offStr = isChs ? 
                    ProLaminator.Properties.Resources.ResourceManager.GetString("chs_OFFLINE") 
                    : Properties.Resources.ResourceManager.GetString("en_OFFLINE");

                DevExpress.XtraEditors.Controls.ImageComboBoxItem imgCmbi1 = new DevExpress.XtraEditors.Controls.ImageComboBoxItem(offStr, false, 10);
                DevExpress.XtraEditors.Controls.ImageComboBoxItem imgCmbi2 = new DevExpress.XtraEditors.Controls.ImageComboBoxItem(onStr, true, 11);

                rpiImgCmbe.Items.Add(imgCmbi1);
                rpiImgCmbe.Items.Add(imgCmbi2);
                rpiImgCmbe.ReadOnly = true;
            }
        }

        /// <summary>
        /// 更新BarButtonItem控件
        /// </summary>
        /// <param name="bbi"></param>
        /// <param name="lan"></param>
        private void UpdateBarButtonItem(DevExpress.XtraBars.BarButtonItem bbi, ProCommon.Communal.Language lan)
        {
            if (bbi != null
              && bbi.Tag != null)
            {
                bbi.ItemClick -= Bbi_ItemClick;
                bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                string str = bbi.Tag.ToString();
                bbi.Caption = isChs ? 
                    ProLaminator.Properties.Resources.ResourceManager.GetString("chs_" + str) 
                    : ProLaminator.Properties.Resources.ResourceManager.GetString("en_" + str);
                bbi.ItemClick += Bbi_ItemClick;
            }
        }

        /// <summary>
        /// 更新BarEditItem控件
        /// </summary>
        /// <param name="bei"></param>
        /// <param name="lan"></param>
        private void UpdateBarEditItem(DevExpress.XtraBars.BarEditItem bei, ProCommon.Communal.Language lan)
        {
            if (bei != null
              && bei.Tag != null)
            {
                bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                string str = bei.Tag.ToString();
                bei.Caption = isChs ? 
                    ProLaminator.Properties.Resources.ResourceManager.GetString("chs_" + str) 
                    : ProLaminator.Properties.Resources.ResourceManager.GetString("en_" + str);
            }
        }

        /// <summary>
        /// 更新LabelControl控件
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="lan"></param>
        private void UpdateLabelControl(DevExpress.XtraEditors.LabelControl lbl, ProCommon.Communal.Language lan)
        {
            if (lbl != null
             && lbl.Tag != null)
            {
                bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                string str = lbl.Tag.ToString();
                lbl.Text = isChs ? 
                    ProLaminator.Properties.Resources.ResourceManager.GetString("chs_" + str) 
                    : ProLaminator.Properties.Resources.ResourceManager.GetString("en_" + str);
            }
        }

        private void UpdateSimpleButton(DevExpress.XtraEditors.SimpleButton sbtn, ProCommon.Communal.Language lan)
        {
            if (sbtn != null
             && sbtn.Tag != null)
            {
                sbtn.Click -= Sbtn_Click;
                bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                string str = sbtn.Tag.ToString();
                sbtn.Text = isChs ? Properties.Resources.ResourceManager.GetString("chs_" + str) : Properties.Resources.ResourceManager.GetString("en_" + str);
                sbtn.Click += Sbtn_Click;
            }
        }

        #endregion

        /// <summary>
        /// 账户变更处理
        /// </summary>
        /// <param name="currentAccount"></param>
        private void OnAccountChanged(ProCommon.Communal.Account currentAccount)
        {
            if (currentAccount != null)
            {
                this.bsbiAccount.Enabled = true;

                switch (currentAccount.Authority)
                {
                    case ProCommon.Communal.AccountAuthority.Junior:
                        {
                            this.bbiManageAccount.Enabled = false;
                            _multiViewModule.IsSwitchViewAllowed = false;                           
                        }
                        break;
                    case ProCommon.Communal.AccountAuthority.Senior:
                        {
                            this.bbiManageAccount.Enabled = false;
                            _multiViewModule.IsSwitchViewAllowed = false;
                        }
                        break;
                    case ProCommon.Communal.AccountAuthority.Administrator:
                        {
                            this.bbiManageAccount.Enabled = true;
                            _multiViewModule.IsSwitchViewAllowed = true;
                        }
                        break;
                    case ProCommon.Communal.AccountAuthority.None:
                    default:
                        {
                            this.bbiManageAccount.Enabled = false;
                            _multiViewModule.IsSwitchViewAllowed = false;
                        }
                        break;
                }

                this.bstiCurrentAccount.Caption = currentAccount.Name;
            }           
        }

        /// <summary>
        /// 加载上次程式
        /// </summary>
        /// <param name="enableLastRoutine"></param>
        /// <param name="lastRoutinePath"></param>
        private void LoadInitRoutine(bool enableLastRoutine, string lastRoutinePath)
        {
            string txt1, caption;
            if (enableLastRoutine)
            {
                if (!string.IsNullOrEmpty(lastRoutinePath))
                {
                    if (System.IO.File.Exists(lastRoutinePath))
                    {
                        _routineName = System.IO.Path.GetFileNameWithoutExtension(lastRoutinePath);
                        _routineDirectory = System.IO.Path.GetDirectoryName(lastRoutinePath);
                        this.beiCurrentRoutine.EditValue = _routineName;

                        _cfgVisionPara = null;

                        //加载指定文件并反序列化为视觉参数类实例                                   
                        try
                        {
                            using (var fs = new System.IO.FileStream(_routineDirectory + "\\" + _routineName + ".pro", System.IO.FileMode.Open))
                            {
                                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                                _cfgVisionPara = (ProLaminator.Config.CfgVisionPara)bf.Deserialize(fs);
                            }                         
                            _cfgMgr.CfgVsPara= _cfgVisionPara;
                        }
                        catch (System.Exception ex)
                        {
                            txt1 = _IsChinese ? "加载程式参数失败!\r\n" : "Load routine parameter failed !\r\n";
                            caption = _IsChinese ? "错误信息" : "Error Message";
                            ProCommon.DerivedForm.FrmMsgBox.Show(txt1, caption,
                               ProCommon.DerivedForm.MyButtons.OK,
                               ProCommon.DerivedForm.MyIcon.Error, _IsChinese);
                        }
                    }
                    else
                    {
                        txt1 = _IsChinese ? "加载程式参数失败!\r\n上次程式文件不存在!" : "Load routine parameter failed !\r\n No last routine file exists !";
                        caption = _IsChinese ? "错误信息" : "Error Message";                      
                        ProCommon.DerivedForm.FrmMsgBox.Show(txt1, caption,
                            ProCommon.DerivedForm.MyButtons.OK,
                            ProCommon.DerivedForm.MyIcon.Error, _IsChinese);
                    }
                }
            }
        }

        public void UpdateLog(ProLaminator.Data.Log lg)
        {
           
        }
    }
}