using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       CfgManager
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProLaminator.Config
 * File      Name：       CfgManager
 * Creating  Time：       5/19/2020 5:53:33 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProLaminator.Config
{
    public class CfgManager
    {
        #region 静态单例

        static object _syncObj = new object();
        static CfgManager _instance;
        public static CfgManager Instance
        {
            get
            {
                lock (_syncObj)
                {
                    if (_instance == null)
                    { _instance = new CfgManager(); }
                }

                return _instance;
            }
        }

        private CfgManager()
        {
            _isEventRegistered = false;
            _isContained = false;
        }

        #endregion

        /// <summary>
        /// 参考根目录
        /// [应用程序所在目录,包括"\"]
        /// </summary>
        public static string DirectoryBase { get { return System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase; } }

        public static string ConfigDirectory { get { return DirectoryBase + DIRECTORY_NAME_FOR_CONFIG; } }

        #region 目录及目录下的文件
        public const string DIRECTORY_NAME_FOR_CONFIG = "Config";
        public const string DIRECTORY_NAME_FOR_LOG = "Log";
        public const string DIRECTORY_NAME_FOR_DATABASE = "DataBase";
        public const string DIRECTORY_NAME_FOR_ROUTINE = "Routine";       

        /// <summary>
        /// 获取基础目录名称
        /// </summary>
        public static string[] DirectoryNames
        {
            get
            {
                return new string[]
                {   DIRECTORY_NAME_FOR_CONFIG,
                    DIRECTORY_NAME_FOR_DATABASE,
                    DIRECTORY_NAME_FOR_LOG,
                    DIRECTORY_NAME_FOR_ROUTINE,                   
                };
            }
        }

        public const string SYSTEM_CONFIG_FILE_NAME = "VisionSystem.cfg";
        public const string ACCOUNT_CONFIG_FILE_NAME = "Account.cfg";
        public const string ALARM_CONFIG_FILE_NAME = "Alarm.cfg";
        public const string CAMERA_CONFIG_FILE_NAME = "Camera.cfg";
        public const string CALIBRATIONSOLUTION_CONFIG_FILE_NAME = "CalibrationSolution.cfg";
        public const string BOARD_CONFIG_FILE_NAME = "Board.cfg";     
        public const string COMSERIAL_CONFIG_FILE_NAME = "SerialPort.cfg";
        public const string COMSOCKET_CONFIG_FILE_NAME = "Socket.cfg";
        public const string COMWEB_CONFIG_FILE_NAME = "Web.cfg";
        public const string VISIONPARA_CONFIG_FILE_NAME = "VisionPara.cfg";
        public const string MOTIONPARA_CONFIG_FILE_NAME = "MotionPara.cfg";

        /// <summary>
        /// 获取配置文件名称组
        /// </summary>
        public static string[] ConfigFileNames
        {
            get
            {
                return new string[]
                {
                  SYSTEM_CONFIG_FILE_NAME,
                  ACCOUNT_CONFIG_FILE_NAME,
                  ALARM_CONFIG_FILE_NAME,
                  CAMERA_CONFIG_FILE_NAME,
                  CALIBRATIONSOLUTION_CONFIG_FILE_NAME,
                  BOARD_CONFIG_FILE_NAME,                 
                  COMSERIAL_CONFIG_FILE_NAME,
                  COMSOCKET_CONFIG_FILE_NAME,
                  COMWEB_CONFIG_FILE_NAME,
                  VISIONPARA_CONFIG_FILE_NAME,
                  MOTIONPARA_CONFIG_FILE_NAME
                };
            }
        }

        public const string SYSTEM_LOG_FILE_NAME = "LogSystem.txt";
        public const string EXCEPTION_LOG_FILE_NAME = "LogException.txt";

        /// <summary>
        /// 获取日志文件名称组
        /// </summary>
        public static string[] LogFileNames
        {
            get { return new string[] { SYSTEM_LOG_FILE_NAME, EXCEPTION_LOG_FILE_NAME }; }
        }


        public const string DATA_AND_LOG_FILE_NAME = "DataAndLog.accdb";
        /// <summary>
        /// 获取数据库文件名称组
        /// [注:存储流水结果,流水日志,报警日志的数据库文件]
        /// </summary>
        public static string DataAndLogFileName
        {
            get { return DATA_AND_LOG_FILE_NAME; }
        }

        public const string RUNDATA_SHEET_NAME = "RunData";
        public const string RUNLOG_SHEET_NAME = "RunLog";
        public const string ALARMLOG_SHEET_NAME = "AlarmLog";

        /// <summary>
        /// 数据表名称组
        /// [注:流水结果表名称,流水日志表名称,报警日志表名称,存储于数据库]
        /// </summary>
        public static string[] DataAndLogSheetNames
        {
            get { return new string[] { RUNDATA_SHEET_NAME, RUNLOG_SHEET_NAME, ALARMLOG_SHEET_NAME }; }
        }

        #endregion

        private bool _isEventRegistered;
        private bool _isContained;

        /// <summary>
        /// 系统配置
        /// </summary>
        public ProLaminator.Config.CfgSystem CfgSys { set; get; }

        /// <summary>
        /// 账户配置
        /// </summary>
        public ProLaminator.Config.CfgAccount CfgAcc { set; get; }

        /// <summary>
        /// 控制卡配置
        /// </summary>
        public ProLaminator.Config.CfgBoard CfgBrd { set; get; }

        /// <summary>
        /// 相机配置
        /// </summary>
        public ProLaminator.Config.CfgCamera CfgCam { set; get; }

        /// <summary>
        /// 标定方案配置
        /// </summary>
        public ProLaminator.Config.CfgCalibration CfgCal { set; get; }

        /// <summary>
        /// 运控参数配置
        /// </summary>
        public ProLaminator.Config.CfgMotionPara CfgMtPara { set; get; }

        /// <summary>
        /// 串口配置
        /// </summary>
        public ProLaminator.Config.CfgSerialPort CfgSrlPort { set; get; }

        /// <summary>
        /// Socket配置
        /// </summary>
        public ProLaminator.Config.CfgSocket CfgSkt { set; get; }

        /// <summary>
        /// 视觉参数配置
        /// </summary>
        public ProLaminator.Config.CfgVisionPara CfgVsPara { set; get; }

        /// <summary>
        /// Web配置
        /// </summary>
        public ProLaminator.Config.CfgWeb CfgWb { set; get; }

        public void Load()
        {
            CfgSys = ProCommon.Communal.CfgAPI.Load<ProLaminator.Config.CfgSystem>(ConfigDirectory + "\\" + SYSTEM_CONFIG_FILE_NAME);
            CfgAcc= ProCommon.Communal.CfgAPI.Load<ProLaminator.Config.CfgAccount>(ConfigDirectory + "\\" + ACCOUNT_CONFIG_FILE_NAME);
            if(CfgSys!=null)
            {
                _isContained = false;
                foreach (ProCommon.Communal.DeviceCategory dc in CfgSys.CtrllerCategoryArray)
                {
                    if (dc == ProCommon.Communal.DeviceCategory.Board)
                    { _isContained = true; break; }
                }
                if (_isContained)
                {
                    CfgBrd = ProCommon.Communal.CfgAPI.Load<ProLaminator.Config.CfgBoard>(ConfigDirectory + "\\" + BOARD_CONFIG_FILE_NAME);
                    CfgMtPara = ProCommon.Communal.CfgAPI.Load<ProLaminator.Config.CfgMotionPara>(ConfigDirectory + "\\" + MOTIONPARA_CONFIG_FILE_NAME);
                }

                _isContained = false;
                foreach (ProCommon.Communal.DeviceCategory dc in CfgSys.CtrllerCategoryArray)
                {
                    if (dc == ProCommon.Communal.DeviceCategory.Camera)
                    { _isContained = true; break; }
                }
                if (_isContained)
                {
                    CfgCam = ProCommon.Communal.CfgAPI.Load<ProLaminator.Config.CfgCamera>(ConfigDirectory + "\\" + CAMERA_CONFIG_FILE_NAME);
                    CfgVsPara = ProCommon.Communal.CfgAPI.Load<ProLaminator.Config.CfgVisionPara>(ConfigDirectory + "\\" + VISIONPARA_CONFIG_FILE_NAME);
                    CfgCal = ProCommon.Communal.CfgAPI.Load<ProLaminator.Config.CfgCalibration>(ConfigDirectory + "\\" + CALIBRATIONSOLUTION_CONFIG_FILE_NAME);
                }              

                _isContained = false;
                foreach (ProCommon.Communal.DeviceCategory dc in CfgSys.CtrllerCategoryArray)
                {
                    if (dc == ProCommon.Communal.DeviceCategory.SerialPort)
                    { _isContained = true; break; }
                }
                if (_isContained)
                    CfgSrlPort = ProCommon.Communal.CfgAPI.Load<ProLaminator.Config.CfgSerialPort>(ConfigDirectory + "\\" + COMSERIAL_CONFIG_FILE_NAME);

                _isContained = false;
                foreach (ProCommon.Communal.DeviceCategory dc in CfgSys.CtrllerCategoryArray)
                {
                    if (dc == ProCommon.Communal.DeviceCategory.Socket)
                    { _isContained = true; break; }
                }
                if (_isContained)
                    CfgSkt = ProCommon.Communal.CfgAPI.Load<ProLaminator.Config.CfgSocket>(ConfigDirectory + "\\" + COMSOCKET_CONFIG_FILE_NAME);
              

                _isContained = false;
                foreach (ProCommon.Communal.DeviceCategory dc in CfgSys.CtrllerCategoryArray)
                {
                    if (dc == ProCommon.Communal.DeviceCategory.Web)
                    { _isContained = true; break; }
                }
                if (_isContained)
                    CfgWb = ProCommon.Communal.CfgAPI.Load<ProLaminator.Config.CfgWeb>(ConfigDirectory + "\\" + COMWEB_CONFIG_FILE_NAME);

                if (!_isEventRegistered)
                {
                    int cnt = 0;

                    //控制卡连接状态改变(更新UI界面控制卡连接状态)
                    if (CfgBrd != null)
                    {
                        for (int i = 0; i < cnt; i++)
                        {

                        }
                    }

                    //相机连接状态改变(更新UI界面相机连接状态)
                    if (CfgCam != null
                        && CfgCam.PropertyList.Count > 0)
                    {
                        cnt = CfgCam.PropertyList.Count;
                        for (int i = 0; i < cnt; i++)
                        {
                            //相机的连接状态改变事件注册到主窗体对应控件更新
                            CfgCam.PropertyList[i].PropertyChanged += null;
                        }
                    }

                    //串口连接状态改变(更新UI界面串口连接状态)
                    if (CfgSrlPort!=null)
                    {

                    }

                    //Socket连接状态改变(更新UI界面Socket连接状态)
                    if (CfgSkt!=null)
                    {

                    }

                    if(CfgWb!=null)
                    {

                    }

                    _isEventRegistered = true;
                }
            }
        }

        public void Save()
        {
            if (CfgSys != null)
                ProCommon.Communal.CfgAPI.Save<ProLaminator.Config.CfgSystem>(CfgSys,ConfigDirectory + "\\" + SYSTEM_CONFIG_FILE_NAME);

            if (CfgAcc!=null)
                ProCommon.Communal.CfgAPI.Save<ProLaminator.Config.CfgAccount>(CfgAcc,ConfigDirectory + "\\" + ACCOUNT_CONFIG_FILE_NAME);

            if (CfgBrd != null)
                ProCommon.Communal.CfgAPI.Save<ProLaminator.Config.CfgBoard>(CfgBrd, ConfigDirectory + "\\" + BOARD_CONFIG_FILE_NAME);

            if(CfgMtPara!=null)
                ProCommon.Communal.CfgAPI.Save<ProLaminator.Config.CfgMotionPara>(CfgMtPara, ConfigDirectory + "\\" + MOTIONPARA_CONFIG_FILE_NAME);           

            if (CfgCam != null)
                ProCommon.Communal.CfgAPI.Save<ProLaminator.Config.CfgCamera>(CfgCam, ConfigDirectory + "\\" + CAMERA_CONFIG_FILE_NAME);

            if (CfgVsPara != null)
                ProCommon.Communal.CfgAPI.Save<ProLaminator.Config.CfgVisionPara>(CfgVsPara, ConfigDirectory + "\\" + VISIONPARA_CONFIG_FILE_NAME);

            if (CfgCal != null)
                ProCommon.Communal.CfgAPI.Save<ProLaminator.Config.CfgCalibration>(CfgCal, ConfigDirectory + "\\" + CALIBRATIONSOLUTION_CONFIG_FILE_NAME);

            if (CfgSrlPort != null)
                ProCommon.Communal.CfgAPI.Save<ProLaminator.Config.CfgSerialPort>(CfgSrlPort, ConfigDirectory + "\\" + COMSERIAL_CONFIG_FILE_NAME);

            if (CfgSkt != null)
                ProCommon.Communal.CfgAPI.Save<ProLaminator.Config.CfgSocket>(CfgSkt, ConfigDirectory + "\\" + COMSOCKET_CONFIG_FILE_NAME);

            if (CfgWb != null)
                ProCommon.Communal.CfgAPI.Save<ProLaminator.Config.CfgWeb>(CfgWb, ConfigDirectory + "\\" + COMWEB_CONFIG_FILE_NAME);
        }
    }
}
