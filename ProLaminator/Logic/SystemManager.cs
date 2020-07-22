#define Chinese

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       SystemManager
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProLaminator.Logic
 * File      Name：       SystemManager
 * Creating  Time：       5/19/2020 4:47:05 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProLaminator.Logic
{
    public class SystemManager
    {
        #region 静态单例

        static object _syncObj = new object();
        static SystemManager _instance;

        public static SystemManager Instance
        {
            get
            {
                lock (_syncObj)
                {
                    if (_instance == null)
                    { _instance = new SystemManager(); }
                }

                return _instance;
            }
        }

        private SystemManager()
        {
            ChkLangIsChinese = false;

#if Chinese
            ChkLangIsChinese = true;
#endif
            InitInternal();
        }

        #endregion

        /// <summary>
        /// 系统自检时的语言版本判断
        /// [可以获取客户操作系统的语言配置,判断显示中文或英文]
        /// </summary>
        public static bool ChkLangIsChinese { private set; get; }
        public bool IsRunning, IsStopped, IsScram, IsStandBy;//是否自动运行，是否自动停止,是否急停，是否待机
        public bool IsResetting, IsReseted;//是否正在复位,是否完成复位      
        public bool IsRunAllowed; //false-运行不允许,true-运行允许
        public bool IsAlarmed; //是否有轴报警
        public bool IsRunOnce;//是否单次运行

        /// <summary>
        /// 系统自检结果标记
        /// </summary>
        public bool SysChkOK { private set; get; }

        /// <summary>
        /// 系统自检异常信息
        /// </summary>
        public string SysChkError { private set; get; }

        /// <summary>
        /// 系统自检步骤标号
        /// </summary>
        public int SysChkStepNumber { private set; get; }

        /// <summary>
        /// 系统配置
        /// [临时系统配置,是方便更新系统的特殊定义符号]
        /// </summary>
        private ProLaminator.Config.CfgSystem _cfgSys,_cfgSysTmp;
        private string _defaultCameraName, _defaultBoardName;

        /// <summary>
        /// 系统日志文件路径,异常日志文件路径
        /// </summary>
        private string _sysLogFilePath, _excpLogFilePath;

#region 根据项目定义设备配置

        private ProLaminator.Config.CfgCamera _cfgCam;
        private ProLaminator.Config.CfgBoard _cfgBoard;
        private ProLaminator.Config.CfgSerialPort _cfgSrlPort;

        /// <summary>
        /// 玻璃工位相机属性
        /// </summary>
        public ProCommon.Communal.CameraProperty CamPropertyForGlass { set; get; }

        /// <summary>
        /// 膜1工位相机属性
        /// </summary>
        public ProCommon.Communal.CameraProperty CamPropertyForMembrane1 { set; get; }

        /// <summary>
        /// 膜2工位相机属性
        /// </summary>
        public ProCommon.Communal.CameraProperty CamPropertyForMembrane2 { set; get; }       

#endregion

        private void InitField()
        {
            IsRunAllowed = false;
            IsRunning = false;
            IsStopped = false;
            IsScram = false;
            IsStandBy = false;
            IsResetting = false;
            IsReseted = false;
            IsAlarmed = false;
            IsRunOnce = true;
            SysChkOK = true;
        }

        private void InitInternal()
        {
            InitField();
        }

        /// <summary>
        /// 系统自检完成后资源配置
        /// </summary>
        private void InitExternal()
        {
            //加载本地配置文件到内存
            ProLaminator.Config.CfgManager.Instance.Load();

            _cfgSys = ProLaminator.Config.CfgManager.Instance.CfgSys;

            int count = 0;

#region 分配项目用设备


            //分配相机
            _cfgCam = ProLaminator.Config.CfgManager.Instance.CfgCam;
            if (_cfgCam!=null)
            {
                count = _cfgCam.PropertyList.Count;
                ProCommon.Communal.CameraProperty camPro = null;
               
                for(int i=0;i<count;i++)
                {
                    camPro= _cfgCam.PropertyList[i];

                    //分配玻璃工位相机
                    if (_cfgSys.GlassStationName == camPro.StationName)
                        CamPropertyForGlass = camPro;

                    //分配膜1工位相机
                    if (_cfgSys.Membrane1StationName == camPro.StationName)
                        CamPropertyForMembrane1 = camPro;

                    //分配膜2工位相机
                    if (_cfgSys.Membrane2StationName == camPro.StationName)
                        CamPropertyForMembrane2 = camPro;

                    //分配其他功能相机
                }
            }

            ProLaminator.Device.CameraManager.Instance.CfgMgr = ProLaminator.Config.CfgManager.Instance;

            //分配其他设备

#endregion

            _sysLogFilePath = ProLaminator.Config.CfgManager.Instance.CfgSys.SystemLogFilePath;
            _excpLogFilePath = ProLaminator.Config.CfgManager.Instance.CfgSys.ExceptionLogFilePath;

        }

#region 系统自检(恢复默认)

        private ProCommon.DerivedForm.FrmCheckProgress _frmCheckProcess;
        /// <summary>
        /// 系统自检
        /// [检查系统运行所需资源是否存在，若存在则正常运行,否则提示异常并退出]
        /// </summary>
        /// <param name="showProcess"></param>
        public void CheckSystem(bool showProcess)
        {
            if (_frmCheckProcess == null)
            {
                _frmCheckProcess = new ProCommon.DerivedForm.FrmCheckProgress();
                _frmCheckProcess.BackgrdWorker.DoWork += BackgrdWorker_DoWork;
                _frmCheckProcess.BackgrdWorker.RunWorkerCompleted += BackgrdWorker_RunWorkerCompleted;
            }

            _frmCheckProcess.BackgrdWorker.RunWorkerAsync();

            //显示模态窗口,才能使程序先进入自检过程           
            _frmCheckProcess.ShowDialog();
        }

        /// <summary>
        /// 后台线程执行完毕--初始化系统设备资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgrdWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            string txt = "";
            string caption = "";

            if (_frmCheckProcess != null)
            {
                _frmCheckProcess.BackgrdWorker.CancelAsync();
                _frmCheckProcess.StopTimer();

                _frmCheckProcess.Close();
                _frmCheckProcess = null;
            }

            if (SysChkOK)
            {
                //根据配置文件,进行资源配置
                InitExternal();
                IsRunAllowed = true;
            }
            else
            {
                txt = ChkLangIsChinese ? "系统自检异常!\r\n" : "Check system error !\r\n";
                caption = ChkLangIsChinese ? "错误信息" : "Error Message";

                ProCommon.DerivedForm.FrmMsgBox.Show(txt+SysChkError, caption,
                    ProCommon.DerivedForm.MyButtons.OK,
                    ProCommon.DerivedForm.MyIcon.Error, ChkLangIsChinese);
                Environment.Exit(-1);
            }
        }

        /// <summary>
        /// 后台线程执行过程--检测配置文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgrdWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            SysChkStepNumber = 1;
            string txt = ChkLangIsChinese ? "开始系统自检" : "Start system check";
            _frmCheckProcess.BackgrdWorker.ReportProgress(SysChkStepNumber, txt);
            System.Threading.Thread.Sleep(500);

            SysChkStepNumber = 2;
            txt = ChkLangIsChinese ? "检测系统配置" : "Check system config";
            _frmCheckProcess.BackgrdWorker.ReportProgress(SysChkStepNumber, txt);
            System.Threading.Thread.Sleep(500);

            CheckDirectoryAndFiles(_frmCheckProcess.BackgrdWorker);

            if(SysChkOK)
            {
                SysChkStepNumber +=1;
                txt = ChkLangIsChinese ? "检测系统配置完成" : "Check system config finished";
                _frmCheckProcess.BackgrdWorker.ReportProgress(SysChkStepNumber, txt);
                System.Threading.Thread.Sleep(200);

                SysChkStepNumber += 1;
                txt = ChkLangIsChinese ? "允许系统程序运行" : "Allow application run  ";
                _frmCheckProcess.BackgrdWorker.ReportProgress(SysChkStepNumber, txt);               
            }
            else
            {
                SysChkStepNumber += 1;
                txt = ChkLangIsChinese ? "检测系统配置失败" : "Check system config failed";
                _frmCheckProcess.BackgrdWorker.ReportProgress(SysChkStepNumber, txt);
                System.Threading.Thread.Sleep(200);

                SysChkStepNumber += 1;
                txt = ChkLangIsChinese ? "不允许系统程序运行" : "No allow application run  ";
                _frmCheckProcess.BackgrdWorker.ReportProgress(SysChkStepNumber, txt);
            }
        }

        /// <summary>
        /// 检测系统文件夹和文件夹下文件
        /// </summary>
        private void CheckDirectoryAndFiles(System.ComponentModel.BackgroundWorker bkgrdWorker)
        {
            foreach (var itm in ProLaminator.Config.CfgManager.DirectoryNames)
            {
                if (!SysChkOK)
                    break;

                CheckDirectory(ProLaminator.Config.CfgManager.DirectoryBase + itm, bkgrdWorker);
                System.Threading.Thread.Sleep(500);
                switch (itm)
                {
                    case ProLaminator.Config.CfgManager.DIRECTORY_NAME_FOR_CONFIG:
                        {
                            foreach (var itm1 in ProLaminator.Config.CfgManager.ConfigFileNames)
                            {
                                if (!CheckConfigFile((ProLaminator.Config.CfgManager.DirectoryBase + itm), itm1, bkgrdWorker))
                                {
                                    SysChkOK = false;
                                    break;
                                }
                                System.Threading.Thread.Sleep(200);
                            }
                        }
                        break;
                    case ProLaminator.Config.CfgManager.DIRECTORY_NAME_FOR_DATABASE:
                        {
                            //需要创建数据库文件及数据表
                        }
                        break;
                    case ProLaminator.Config.CfgManager.DIRECTORY_NAME_FOR_LOG:
                        {
                            foreach (var itm2 in ProLaminator.Config.CfgManager.LogFileNames)
                            {
                                if (!CheckLogFile(ProLaminator.Config.CfgManager.DirectoryBase + itm + "\\" + itm2, bkgrdWorker))
                                {
                                    SysChkOK = false;
                                    break;
                                }
                                System.Threading.Thread.Sleep(200);
                            }
                        }
                        break;
                    case ProLaminator.Config.CfgManager.DIRECTORY_NAME_FOR_ROUTINE:
                        break;                  
                }
            }
        }

        /// <summary>
        /// 检测配置文件夹
        /// </summary>
        /// <param name="directroy"></param>
        /// <param name="bkgrdWorker"></param>
        private void CheckDirectory(string directroy, System.ComponentModel.BackgroundWorker bkgrdWorker)
        {
            SysChkStepNumber += 1;
            int startIdx = directroy.LastIndexOf('\\');
            string directoryName = directroy.Substring(startIdx + 1, directroy.Length - startIdx - 1);

            if (!System.IO.Directory.Exists(directroy))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(directroy);
                    SysChkError = ChkLangIsChinese?
                        ("创建文件夹[" + directoryName + "]成功!")
                        : ("Create folder[" + directoryName + "]successfully!");
                    if (bkgrdWorker != null)
                        bkgrdWorker.ReportProgress(SysChkStepNumber, SysChkError);
                }
                catch (System.Exception ex)
                {
                    SysChkError = (ChkLangIsChinese ?("创建文件夹[" + directoryName + "]失败!\r\n")
                        : ("Create folder[" + directoryName + "]failed!\r\n")) +ex.Message;
                    if (bkgrdWorker != null)
                        bkgrdWorker.ReportProgress(SysChkStepNumber, SysChkError);
                }
            }
            else
            {
                SysChkError = ChkLangIsChinese ?("找到文件夹[" + directoryName + "]")
                    :("Find folder[" + directoryName + "]");
                if (bkgrdWorker != null)
                    bkgrdWorker.ReportProgress(SysChkStepNumber, SysChkError);
            }
        }

        /// <summary>
        /// 检测日志文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="bkgrdWorker"></param>
        private bool CheckLogFile(string filePath, System.ComponentModel.BackgroundWorker bkgrdWorker)
        {
            bool rt = false;
            SysChkStepNumber += 1;
            string fileNameEx = System.IO.Path.GetFileName(filePath); //包含扩展名的文件名
            string fileName = fileNameEx.Substring(0, fileNameEx.LastIndexOf('.'));

            if (!System.IO.File.Exists(filePath))
            {
                try
                {
                    System.IO.File.Create(filePath);
                    SysChkError = ChkLangIsChinese ?("创建文件[" + fileName + "]成功!")
                        : ("Create file[" + fileName + "]successfully!");
                    if (bkgrdWorker != null)
                        bkgrdWorker.ReportProgress(SysChkStepNumber, SysChkError);
                    rt = true;
                }
                catch (System.Exception ex)
                {
                    SysChkError = ChkLangIsChinese ? ("创建文件[" + fileName + "]失败!异常描述:\r\n")
                        :("Create file[" + fileName + "]failed!Description:\r\n" )+ ex.Message;
                    if (bkgrdWorker != null)
                        bkgrdWorker.ReportProgress(SysChkStepNumber, SysChkError);
                }

            }
            else
            {
                SysChkError = ChkLangIsChinese ? ("找到文件[" + fileName + "]"): ("Find file[" + fileName + "]");
                if (bkgrdWorker != null)
                    bkgrdWorker.ReportProgress(SysChkStepNumber,SysChkError);
                rt = true;
            }

            return rt;
        }

        /// <summary>
        /// 检测配置文件
        /// </summary>
        /// <param name="fileDirectory"></param>
        /// <param name="fileName"></param>
        /// <param name="bkgrdWorker"></param>
        private bool CheckConfigFile(string fileDirectory, string fileName, System.ComponentModel.BackgroundWorker bkgrdWorker)
        {
            SysChkStepNumber += 1;
            bool rt = false;
            if (!System.IO.File.Exists(fileDirectory + "\\" + fileName))
            {
                try
                {
                    switch (fileName)
                    {
#region 系统配置
                        case ProLaminator.Config.CfgManager.SYSTEM_CONFIG_FILE_NAME:
                            {
                                _cfgSysTmp = new ProLaminator.Config.CfgSystem();
                                _cfgSysTmp.LanguageVersion = ProCommon.Communal.Language.Chinese;                              
                                _cfgSysTmp.EnableAutoLaunch = false;
                                _cfgSysTmp.GlassStationName = "GLASS";
                                _cfgSysTmp.Membrane1StationName = "UPMEMBRANE";
                                _cfgSysTmp.Membrane2StationName = "DOWNMEMBRANE";
                                _cfgSysTmp.SerialPortNameForPlc = "COM1";
                             
                                _cfgSysTmp.SpeedLow = 10.0f;
                                _cfgSysTmp.SpeedMedium = 30.0f;
                                _cfgSysTmp.SpeedHigh = 60.0f;
                                _cfgSysTmp.ToolCenterOffsetX = 0.0f;
                                _cfgSysTmp.ToolCenterOffsetY = 0.0f;

                                _cfgSysTmp.ClientName = "深圳市智显科技有限公司";
                                _cfgSysTmp.CtrllerCategoryArray = new ProCommon.Communal.DeviceCategory[] {
                                    ProCommon.Communal.DeviceCategory.Camera,
                                    //ProCommon.Communal.DeviceCategory.Board,
                                    ProCommon.Communal.DeviceCategory.SerialPort
                                    //ProCommon.Communal.DeviceCategory.Socket,
                                    //ProCommon.Communal.DeviceCategory.Web
                                };

                                _cfgSysTmp.EnableLastRoutine = false; //运行软件时不启用上次生产程式
                                _cfgSysTmp.LastRoutinePath = null;    //上次生产程式(默认为空)
                                _cfgSysTmp.RoutineDirectory = null;//生产程式目录(转到默认)
                                _cfgSysTmp.DataBaseDirectory = null;//数据和日志目录(数据库,转到默认)

                                _cfgSysTmp.RunDataSheetName = null;//流水结果表名称(转到默认)
                                _cfgSysTmp.EnableSaveRunData = false;
                                _cfgSysTmp.SaveRunDataDays = 7;
                                _cfgSysTmp.DisplayRunDataCount = 10;

                                _cfgSysTmp.RunLogSheetName = null;//流水日志表名称(转到默认)
                                _cfgSysTmp.EnableSaveRunLog = false;
                                _cfgSysTmp.SaveRunLogDays = 7;
                                _cfgSysTmp.DisplayRunLogCount = 15;

                                _cfgSysTmp.AlarmLogSheetName = null;//报警日志表名称(转到默认)
                                _cfgSysTmp.EnableSaveAlarmLog = false;
                                _cfgSysTmp.SaveAlarmLogDays = 7;

                                _cfgSysTmp.SystemLogFilePath = null;//系统日志文件路径(转到默认)
                                _cfgSysTmp.ExceptionLogFilePath = null;//异常日志文件路径(转到默认)                                

                                ProCommon.Communal.CfgAPI.Save<ProLaminator.Config.CfgSystem>(_cfgSysTmp,
                                    ProLaminator.Config.CfgManager.ConfigDirectory 
                                    + "\\" 
                                    + ProLaminator.Config.CfgManager.SYSTEM_CONFIG_FILE_NAME);

                            }
                            break;
#endregion

#region 用户配置
                        case ProLaminator.Config.CfgManager.ACCOUNT_CONFIG_FILE_NAME:
                            {
                                //新增加账户时，按照如下规则:账户列表中最后一个账户的Number,然后加1                                
                                ProCommon.Communal.Account acc = new ProCommon.Communal.Account(0,"ACC_0000");
                                acc.Name = "Admin";
                                acc.Authority = ProCommon.Communal.AccountAuthority.Administrator;
                                acc.PassWord = ProCommon.Communal.DESEncrypt.Encrypt("123");

                                ProLaminator.Config.CfgAccount cfgAcc = new Config.CfgAccount();
                                cfgAcc.AccList.Add(acc);
                                ProCommon.Communal.CfgAPI.Save<ProLaminator.Config.CfgAccount>(cfgAcc,
                                    ProLaminator.Config.CfgManager.ConfigDirectory 
                                    + "\\" 
                                    + ProLaminator.Config.CfgManager.ACCOUNT_CONFIG_FILE_NAME);
                            }
                            break;
#endregion

#region 报警配置
                        case ProLaminator.Config.CfgManager.ALARM_CONFIG_FILE_NAME:
                            break;
#endregion

#region 相机配置
                        case ProLaminator.Config.CfgManager.CAMERA_CONFIG_FILE_NAME:
                            {
                               
                                if (_cfgSysTmp != null
                                    && _cfgSysTmp.CtrllerCategoryArray != null)
                                {
                                    bool isCreate = false;
                                    foreach (ProCommon.Communal.DeviceCategory cc in _cfgSysTmp.CtrllerCategoryArray)
                                    {
                                        if (cc == ProCommon.Communal.DeviceCategory.Camera)
                                        {
                                            isCreate = true;
                                            break;
                                        }
                                    }

                                    if (isCreate)
                                    {
                                        if(_cfgCam==null)
                                            _cfgCam = new Config.CfgCamera();

                                        _defaultCameraName = "玻璃工位";

                                        //根据项目需要,创建建默的相机配置文件
                                        //新增加相机属性时，按照如下规则:相机属性列表中最后一个相机属性的Number,然后加1 
                                        ProCommon.Communal.CameraProperty camProGlass = new ProCommon.Communal.CameraProperty(ProCommon.Communal.DeviceBrand.HikVision, 0, "Cam_00");
                                        camProGlass.Name = _defaultCameraName;
                                        camProGlass.ExposureTime = 0.5f;
                                        camProGlass.FPS = 15;
                                        camProGlass.Gain = 1.0f;
                                        camProGlass.ReconnectInterval = 500;
                                        camProGlass.SerialNo = "00D22898505";
                                        camProGlass.StationName = _cfgSysTmp.GlassStationName;
                                        _cfgCam.PropertyList.Add(camProGlass);

                                        
                                        _defaultCameraName = "左片工位";                                       
                                        ProCommon.Communal.CameraProperty camProMembrane1 = new ProCommon.Communal.CameraProperty(ProCommon.Communal.DeviceBrand.HikVision, 1, "Cam_01");
                                        camProMembrane1.Name = _defaultCameraName;
                                        camProMembrane1.ExposureTime = 0.35f;
                                        camProMembrane1.FPS = 15;
                                        camProMembrane1.Gain = 1.0f;
                                        camProMembrane1.ReconnectInterval = 500;
                                        camProMembrane1.SerialNo = "00D22898327";
                                        camProMembrane1.StationName = _cfgSysTmp.Membrane1StationName;
                                        _cfgCam.PropertyList.Add(camProMembrane1);

                                        _defaultCameraName = "右片工位";
                                        ProCommon.Communal.CameraProperty camProMembrane2 = new ProCommon.Communal.CameraProperty(ProCommon.Communal.DeviceBrand.HikVision, 2, "Cam_02");
                                        camProMembrane2.Name = _defaultCameraName;
                                        camProMembrane2.ExposureTime = 0.35f;
                                        camProMembrane2.FPS = 15;
                                        camProMembrane2.Gain = 1.0f;
                                        camProMembrane2.ReconnectInterval = 500;
                                        camProMembrane2.SerialNo = "00D22898383";
                                        camProMembrane2.StationName = _cfgSysTmp.Membrane2StationName;

                                        _cfgCam.PropertyList.Add(camProMembrane2);
                                        

                                        ProCommon.Communal.CfgAPI.Save<ProLaminator.Config.CfgCamera>(_cfgCam, ProLaminator.Config.CfgManager.ConfigDirectory
                                            + "\\"
                                            + ProLaminator.Config.CfgManager.CAMERA_CONFIG_FILE_NAME);                                       
                                    }
                                }                              
                            }
                            break;
#endregion

#region 标定方案配置
                        case ProLaminator.Config.CfgManager.CALIBRATIONSOLUTION_CONFIG_FILE_NAME:
                            {
                                if (_cfgSysTmp != null
                                  && _cfgSysTmp.CtrllerCategoryArray != null)
                                {
                                    bool isCreate = false;
                                    foreach (ProCommon.Communal.DeviceCategory cc in _cfgSysTmp.CtrllerCategoryArray)
                                    {
                                        if (cc == ProCommon.Communal.DeviceCategory.Camera)
                                            isCreate = true;
                                    }

                                    if (isCreate)
                                    {
                                        ProLaminator.Config.CfgCalibration cfgCal = new ProLaminator.Config.CfgCalibration();
                                        //根据项目需要,创建建默的标定方案配置文件
                                        //新增加标定方案时，按照如下规则:标定方案ID与相机ID一致
                                        if(_cfgCam!=null
                                            && _cfgCam.PropertyList!=null)
                                        {
                                            int cnt = _cfgCam.PropertyList.Count;

                                            for(int i=0;i<cnt;i++)
                                            {
                                                ProVision.Communal.CalibrationSolution calSl = new ProVision.Communal.CalibrationSolution(ProVision.Communal.CalibrationType.CALIBRATION_UNIT, _cfgCam.PropertyList[i].Name,_cfgCam.PropertyList[i].ID);
                                                calSl.IsActive = true;
                                                calSl.IsEffective = true;
                                                calSl.RowUnit = 0.0f;
                                                calSl.ColUnit = 0.0f;
                                                cfgCal.CalSolutionList.Add(calSl);
                                            }
                                        }

                                        ProCommon.Communal.CfgAPI.Save<ProLaminator.Config.CfgCalibration>(cfgCal, ProLaminator.Config.CfgManager.ConfigDirectory
                                            + "\\"
                                            + ProLaminator.Config.CfgManager.CALIBRATIONSOLUTION_CONFIG_FILE_NAME);                                      
                                    }
                                }
                            }
                            break;
#endregion

#region 控制卡配置
                        case ProLaminator.Config.CfgManager.BOARD_CONFIG_FILE_NAME:
                            {
                                if (_cfgSysTmp != null
                                    && _cfgSysTmp.CtrllerCategoryArray != null)
                                {
                                    bool isCreate = false;
                                    foreach (ProCommon.Communal.DeviceCategory cc in _cfgSysTmp.CtrllerCategoryArray)
                                    {
                                        if (cc == ProCommon.Communal.DeviceCategory.Board)
                                        {
                                            isCreate = true;
                                            break;
                                        }
                                    }

                                    if (isCreate)
                                    {
                                        if(_cfgBoard==null)
                                            _cfgBoard = new ProLaminator.Config.CfgBoard();

                                        //根据项目需要,创建建默的控制卡配置文件
                                        //新增加控制卡属性时，按照如下规则:控制卡属性列表中最后一个控制卡属性的Number,然后加1 
                                        ProCommon.Communal.BoardProperty boardPro = new ProCommon.Communal.BoardProperty(ProCommon.Communal.DeviceBrand.ZMotion, 0, "Brd_0000");
                                        boardPro.IsConnected = false;
                                        boardPro.ReconnectInterval = 500;
                                        boardPro.EtherNetIP = "192.168.1.30";
                                        boardPro.EtherNetPort = 8089;
                                        boardPro.ReconnectInterval = 50;
                                        boardPro.Name = "BrdForShller";
                                        boardPro.StationName = _cfgSysTmp.GlassStationName;

                                        //创建硬件配置时的板卡轴配置
                                        ProCommon.Communal.AxisList axisList = new ProCommon.Communal.AxisList();
                                        CreateAxesInstance(ref axisList);
                                        for (int i = 0; i < axisList.Count; i++)
                                            boardPro.AxisList.Add(axisList[i]);

                                        _cfgBoard.PropertyList.Add(boardPro);

                                        ProCommon.Communal.CfgAPI.Save<ProLaminator.Config.CfgBoard>(_cfgBoard,
                                               ProLaminator.Config.CfgManager.ConfigDirectory 
                                               + "\\" 
                                               + ProLaminator.Config.CfgManager.BOARD_CONFIG_FILE_NAME);
                                    }
                                }
                            }
                            break;
#endregion

#region 通信Socket配置
                        case ProLaminator.Config.CfgManager.COMSOCKET_CONFIG_FILE_NAME:
                            {
                                if (_cfgSysTmp != null
                                   && _cfgSysTmp.CtrllerCategoryArray != null)
                                {
                                    bool isCreate = false;
                                    foreach (ProCommon.Communal.DeviceCategory cc in _cfgSysTmp.CtrllerCategoryArray)
                                    {
                                        if (cc == ProCommon.Communal.DeviceCategory.Socket)
                                        {
                                            isCreate = true;
                                            break;
                                        }
                                    }

                                    if (isCreate)
                                    {
                                        //根据项目需要,创建建默的Socket配置文件
                                        //新增加Socket属性时，按照如下规则:Socekt属性列表中最后一个Socket属性的Number,然后加1 
                                        ProLaminator.Config.CfgSocket cfgSkt = new ProLaminator.Config.CfgSocket();

                                        ProCommon.Communal.SocketProperty sktPro = new ProCommon.Communal.SocketProperty(ProCommon.Communal.DeviceBrand.Microsoft, 0, "Skt_0000");                                      
                                        sktPro.IP = "192.168.1.11";
                                        sktPro.Name = "测试网口";
                                        sktPro.StationName = _cfgSysTmp.GlassStationName;
                                        cfgSkt.PropertyList.Add(sktPro);

                                        ProCommon.Communal.CfgAPI.Save<ProLaminator.Config.CfgSocket>(cfgSkt, ProLaminator.Config.CfgManager.ConfigDirectory
                                           + "\\"
                                           + ProLaminator.Config.CfgManager.COMSOCKET_CONFIG_FILE_NAME);
                                    }
                                }
                               
                            }
                            break;
#endregion

#region 通信Serial配置
                        case ProLaminator.Config.CfgManager.COMSERIAL_CONFIG_FILE_NAME:
                            {
                                if (_cfgSysTmp != null
                                    && _cfgSysTmp.CtrllerCategoryArray != null)
                                {
                                    bool isCreate = false;
                                    foreach (ProCommon.Communal.DeviceCategory cc in _cfgSysTmp.CtrllerCategoryArray)
                                    {
                                        if (cc == ProCommon.Communal.DeviceCategory.SerialPort)
                                        {
                                            isCreate = true;
                                            break;
                                        }
                                    }

                                    if (isCreate)
                                    {
                                        ProLaminator.Config.CfgSerialPort cfgSrlPt = new ProLaminator.Config.CfgSerialPort();
                                        //根据项目需要,创建建默的SerialPort配置文件
                                        //新增加SerialPort属性时，按照如下规则:SerialPort属性列表中最后一个SerialPort属性的Number,然后加1                                      
                                        ProCommon.Communal.SerialPortProperty srlPro = new ProCommon.Communal.SerialPortProperty(ProCommon.Communal.DeviceBrand.Microsoft, 0, "Srl_0000");
                                        srlPro.ReceiveTimeOut = 500;
                                        srlPro.ReconnectInterval = 1000;
                                        srlPro.SendTimeOut = 500;
                                        srlPro.HandShake = System.IO.Ports.Handshake.None;
                                        srlPro.BaudRate = 115200;
                                        srlPro.DataBits = 8;
                                        srlPro.StopBits = System.IO.Ports.StopBits.One;
                                        srlPro.Parity = System.IO.Ports.Parity.Even;
                                        srlPro.DtrEnable = true;
                                        srlPro.RtsEnable = true;
                                        srlPro.ReceivedBytesThreshold = 8;
                                        srlPro.NewLine = "/r/n";
                                        srlPro.StationName = _cfgSysTmp.SerialPortNameForPlc;

                                        cfgSrlPt.PropertyList.Add(srlPro);
                                        ProCommon.Communal.CfgAPI.Save<ProLaminator.Config.CfgSerialPort>(cfgSrlPt, ProLaminator.Config.CfgManager.ConfigDirectory
                                           + "\\"
                                           + ProLaminator.Config.CfgManager.COMSERIAL_CONFIG_FILE_NAME);
                                    }
                                }                               
                            }
                            break;
#endregion

#region 通信WebService配置
                        case ProLaminator.Config.CfgManager.COMWEB_CONFIG_FILE_NAME:
                            {
                                if (_cfgSysTmp != null
                                    && _cfgSysTmp.CtrllerCategoryArray != null)
                                {
                                    bool isCreate = false;
                                    foreach (ProCommon.Communal.DeviceCategory cc in _cfgSysTmp.CtrllerCategoryArray)
                                    {
                                        if (cc == ProCommon.Communal.DeviceCategory.Web)
                                        {
                                            isCreate = true;
                                            break;
                                        }
                                    }

                                    if (isCreate)
                                    {
                                        //根据项目需要,创建建默的Web配置文件
                                        //新增加Web属性时，按照如下规则:Web属性列表中最后一个Web属性的Number,然后加1 

                                        ProLaminator.Config.CfgWeb cfgWb = new ProLaminator.Config.CfgWeb();

                                        ProCommon.Communal.WebProperty webPro = new ProCommon.Communal.WebProperty(ProCommon.Communal.DeviceBrand.Microsoft, 0, "Web_00");
                                        webPro.Name = "测试网络";
                                        webPro.StationName = _cfgSysTmp.GlassStationName;

                                        cfgWb.PropertyList.Add(webPro);
                                        ProCommon.Communal.CfgAPI.Save<ProLaminator.Config.CfgWeb>(cfgWb,ProLaminator.Config.CfgManager.ConfigDirectory 
                                            + "\\" 
                                            + ProLaminator.Config.CfgManager.COMWEB_CONFIG_FILE_NAME);
                                    }
                                }
                            }
                            break;
#endregion

#region 视觉参数配置
                        case ProLaminator.Config.CfgManager.VISIONPARA_CONFIG_FILE_NAME:
                            {
                                ProLaminator.Config.CfgVisionPara cfgVsPara = new ProLaminator.Config.CfgVisionPara();
                                ProCommon.Communal.CfgAPI.Save<ProLaminator.Config.CfgVisionPara>(cfgVsPara,ProLaminator.Config.CfgManager.ConfigDirectory 
                                    + "\\" 
                                    + ProLaminator.Config.CfgManager.VISIONPARA_CONFIG_FILE_NAME);
                            }
                            break;
#endregion

#region 运控参数配置
                        case ProLaminator.Config.CfgManager.MOTIONPARA_CONFIG_FILE_NAME:
                            {
                            }
                            break;
#endregion

                        default:
                            break;
                    }

                    SysChkError = ChkLangIsChinese?("创建文件[" + fileName + "]成功!")
                        : ("Create file[" + fileName + "]successfully!");
                    if (bkgrdWorker != null)
                        bkgrdWorker.ReportProgress(SysChkStepNumber,SysChkError);
                    rt = true;
                }
                catch (System.Exception ex)
                {
                    SysChkError = (ChkLangIsChinese?("创建文件[" + fileName + "]失败!异常描述:\r\n")
                        : ("Create file[" + fileName + "]failed!Description:\r\n")) + ex.Message;
                    if (bkgrdWorker != null)
                        bkgrdWorker.ReportProgress(SysChkStepNumber, SysChkError);
                }
            }
            else
            {
                //若系统配置文件存在,加载系统配置文件，为其他依赖系统配置的配置做准备
                if (fileName == ProLaminator.Config.CfgManager.SYSTEM_CONFIG_FILE_NAME)
                {
                    _cfgSysTmp = ProCommon.Communal.CfgAPI.Load<ProLaminator.Config.CfgSystem>(ProLaminator.Config.CfgManager.ConfigDirectory 
                        + "\\" 
                        + ProLaminator.Config.CfgManager.SYSTEM_CONFIG_FILE_NAME);
                }

                SysChkError = ChkLangIsChinese ?("找到文件[" + fileName + "]"): ("Find file[" + fileName + "]");
                if (bkgrdWorker != null)
                    bkgrdWorker.ReportProgress(SysChkStepNumber, SysChkError);
                rt = true;
            }
            return rt;
        }

        /// <summary>
        /// 创建硬件配置时的板卡轴配置
        /// [需要根据项目情况重定义]
        /// </summary>
        /// <param name="axesList"></param>      
        /// <returns></returns>
        private bool CreateAxesInstance(ref ProCommon.Communal.AxisList axesList)
        {
            bool rt = false;
            if (axesList != null)
            {
                axesList.Clear();
                int tmpServoOnPort = 0, tmpALMIn = 0, tmpDatumIn = 0, tmpHFwdIn = 0, tmpHRevIn = 0, tmpALMCLROut = 0;
                ProCommon.Communal.Axis axis = null;
                for (int i = 0; i < 3; i++)
                {
                    axis = new ProCommon.Communal.Axis("Axis_"+i.ToString("00"));                   
                    axis.AxisType = 1;
                    axis.PulseOutMode = 0;
                    axis.ServoOnLevel = ProCommon.Communal.ElectricalLevel.Low;
                    axis.PulseUnit = 100;
                    switch (i)
                    {
                        case 0:
                            {
                                axis.Name = "X";
                                tmpServoOnPort =16;
                                tmpALMIn =40;
                                tmpDatumIn = 16;
                                tmpHFwdIn = 20;
                                tmpHRevIn = 21;
                                tmpALMCLROut = 17;
                                axis.HelicalPitch = 10.0f;
                                axis.PulsePerRound = 4000;
                                axis.PulseUnit = 400;
                            }
                            break;
                        case 1:
                            {
                                axis.Name = "Y";
                                tmpServoOnPort = 18;
                                tmpALMIn = 41;
                                tmpDatumIn = 17;
                                tmpHFwdIn = 22;
                                tmpHRevIn =23;
                                tmpALMCLROut =19;
                                axis.HelicalPitch = 20.0f;
                                axis.PulsePerRound = 4000;
                                axis.PulseUnit = 200;
                            }
                            break;
                        case 2:
                            {
                                axis.Name = "R";
                                tmpServoOnPort = 20;
                                tmpALMIn = 42;
                                tmpDatumIn = 18;
                                tmpHFwdIn = 24;
                                tmpHRevIn = 25;
                                tmpALMCLROut = 21;
                                axis.HelicalPitch = 360.0f;
                                axis.PulsePerRound = 4000;
                                axis.PulseUnit = 11.11f;
                            }
                            break;
                        case 3:
                            {
                                axis.Name = "U";
                                tmpServoOnPort = 22;
                                tmpALMIn = 43;
                                tmpDatumIn = 19;
                                tmpHFwdIn = 26;
                                tmpHRevIn = 27;
                                tmpALMCLROut = 23;
                            }
                            break;
                        case 4:
                            {
                                tmpServoOnPort = 24;
                                tmpALMIn = 44;
                                tmpDatumIn = 28;
                                tmpHFwdIn = 32;
                                tmpHRevIn = 33;
                                tmpALMCLROut = 25;
                            }
                            break;
                        case 5:
                            {
                                tmpServoOnPort = 26;
                                tmpALMIn = 45;
                                tmpDatumIn = 29;
                                tmpHFwdIn = 34;
                                tmpHRevIn = 35;
                                tmpALMCLROut = 27;
                            }
                            break;
                        case 6:
                            {
                                tmpServoOnPort = 28;
                                tmpALMIn = 46;
                                tmpDatumIn = 30;
                                tmpHFwdIn = 36;
                                tmpHRevIn = 37;
                                tmpALMCLROut = 29;
                            }
                            break;
                        case 7:
                            {
                                tmpServoOnPort = 30;
                                tmpALMIn = 47;
                                tmpDatumIn = 31;
                                tmpHFwdIn = 38;
                                tmpHRevIn = 39;
                                tmpALMCLROut = 31;
                            }
                            break;
                    }

                    //通用输入或输出口做专用信号                  
                    axis.ServoOnOutBitNumber = tmpServoOnPort;
                    axis.AlarmInBitNumber = tmpALMIn;
                    axis.AlarmInLevel = ProCommon.Communal.ElectricalLevel.High;
                    axis.AlarmClearOutBitNumber = tmpALMCLROut;
                    axis.AlarmClearOutLevel = ProCommon.Communal.ElectricalLevel.Low;
                    axis.StatusALM = false;
                    axis.DatumInBitNumber = tmpDatumIn;
                    axis.DatumInLevel = ProCommon.Communal.ElectricalLevel.Low;
                    axis.StatusOfHardDatum = false;
                    axis.HardForwarInBitNumber = tmpHFwdIn;
                    axis.HardForwardInLevel = ProCommon.Communal.ElectricalLevel.Low;
                    axis.StatusOfHardForward = false;
                    axis.HardReverseInBitNumber = tmpHRevIn;
                    axis.HardReverseInLevel = ProCommon.Communal.ElectricalLevel.Low;
                    axis.StatusOfHardReverse = false;
                    axis.DatumDirection= "负向";
                    axis.DatumMode = 14;

                    axis.StartSpeed = 50.0f;
                    axis.RunSpeed = 500.0f;
                    axis.Accel = 1000.0f;
                    axis.Decel = 1000.0f;
                    axis.DatumCreepSpeed = 20.0f;
                    axis.DatumSpeed = 100.0f;
                    axis.EndSpeed = 200.0f;

                    axis.SoftReverseLimit = 0.0f;
                    axis.SoftForwardLimit = 200.0f;

                    axis.CurrentPos = 0.0f;
                    axis.FirstPos = 10.0f;
                    axis.SecondPos = 50.0f;
                    axis.ThirdPos = 80.0f;

                    axesList.Add(axis);
                }

                rt = true;
            }
            return rt;
        }

#endregion
    }
}
