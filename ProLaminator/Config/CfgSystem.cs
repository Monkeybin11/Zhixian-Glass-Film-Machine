using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       CfgSystem
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProLaminator.Config
 * File      Name：       CfgSystem
 * Creating  Time：       5/19/2020 5:56:13 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProLaminator.Config
{
    [Serializable]
    public class CfgSystem:ProCommon.Communal.Config
    {
        #region 系统配置  

        /// <summary>
        /// 系统语言版本
        /// </summary>
        public ProCommon.Communal.Language LanguageVersion { set; get; }


        /// <summary>
        /// 客户名称
        /// </summary>
        public string ClientName { set; get; }


        /// <summary>
        /// 自启动
        /// </summary>
        public bool EnableAutoLaunch { set; get; }

        /// <summary>
        /// 当前系统控制器种类列表
        /// </summary>
        public ProCommon.Communal.DeviceCategory[] CtrllerCategoryArray { set; get; }

        

        private string _routineDirectory;
        private static string _defaultRoutineDirectory = ProLaminator.Config.CfgManager.DirectoryBase
                                                   + ProLaminator.Config.CfgManager.DIRECTORY_NAME_FOR_ROUTINE;
        /// <summary>
        /// 程式目录
        /// </summary>
        public string RoutineDirectory
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _routineDirectory = value;
            }
            get
            {
                return string.IsNullOrEmpty(_routineDirectory) ? _defaultRoutineDirectory : _routineDirectory;
            }
        }

        #region 流水结果,流水日志,报警日志的数据库存储

        private string _dataBaseDirectory;
        private static string _defaultDataBaseDirectory = ProLaminator.Config.CfgManager.DirectoryBase
                                                   + ProLaminator.Config.CfgManager.DIRECTORY_NAME_FOR_DATABASE;

        /// <summary>
        /// 数据和日志目录
        /// [注:包括流水结果,流水日志,报警日志的数据库]
        /// </summary>
        public string DataBaseDirectory
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _dataBaseDirectory = value;
            }
            get
            {
                return string.IsNullOrEmpty(_dataBaseDirectory) ? _defaultDataBaseDirectory : _dataBaseDirectory;
            }
        }

        //---------------------------------------------------------------------------------------

        private string _runDataSheetName;
        private static string _defaultRunDataSheetName = ProLaminator.Config.CfgManager.RUNDATA_SHEET_NAME;

        /// <summary>
        /// 流水结果表名称
        /// </summary>
        public string RunDataSheetName
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _runDataSheetName = value;
            }
            get
            {
                return string.IsNullOrEmpty(_runDataSheetName) ? _defaultRunDataSheetName : _runDataSheetName;
            }
        }

        /// <summary>
        /// 是否保存结果数据
        /// </summary>
        public bool EnableSaveRunData { set; get; }

        /// <summary>
        /// 保存数据结果的天数
        /// </summary>
        public int SaveRunDataDays { set; get; }

        /// <summary>
        /// 显示数据结果的记录条行数
        /// </summary>
        public int DisplayRunDataCount { set; get; }

        //--------------------------------------------------------------------------------------

        private string _runLogSheetName;
        private static string _defaultRunLogSheetName = ProLaminator.Config.CfgManager.RUNLOG_SHEET_NAME;

        /// <summary>
        /// 流水日志表名称
        /// </summary>
        public string RunLogSheetName
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _runLogSheetName = value;
            }
            get
            {
                return string.IsNullOrEmpty(_runLogSheetName) ? _defaultRunLogSheetName : _runLogSheetName;
            }
        }

        /// <summary>
        /// 是否保存流水日志
        /// [注:生产工艺的记录]
        /// </summary>
        public bool EnableSaveRunLog { set; get; }

        /// <summary>
        /// 保存流水日志的天数
        /// </summary>
        public int SaveRunLogDays { set; get; }

        /// <summary>
        /// 显示流水日志的记录条行数
        /// </summary>
        public int DisplayRunLogCount { set; get; }

        //------------------------------------------------------------------------------------

        private string _alarmLogSheetName;
        private static string _defaultAlarmLogSheetName = ProLaminator.Config.CfgManager.ALARMLOG_SHEET_NAME;

        /// <summary>
        /// 流水日志表名称
        /// </summary>
        public string AlarmLogSheetName
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _alarmLogSheetName = value;
            }
            get
            {
                return string.IsNullOrEmpty(_alarmLogSheetName) ? _defaultAlarmLogSheetName : _alarmLogSheetName;
            }
        }

        /// <summary>
        /// 是否保存报警日志
        /// </summary>
        public bool EnableSaveAlarmLog { set; get; }

        /// <summary>
        /// 保存报警日志的天数
        /// </summary>
        public int SaveAlarmLogDays { set; get; }

        /// <summary>
        /// 显示报警日志的记录条行数
        /// </summary>
        public int DisplayAlarmLogCount { set; get; }

        #endregion

        private string _sysLogFilePath; //系统日志文件路径
        private static string _defaultSysLogFilePath = ProLaminator.Config.CfgManager.DirectoryBase
                                                    + ProLaminator.Config.CfgManager.DIRECTORY_NAME_FOR_LOG
                                                    + "\\" + ProLaminator.Config.CfgManager.SYSTEM_LOG_FILE_NAME;

        /// <summary>
        /// 系统日志文件路径
        /// </summary>
        public string SystemLogFilePath
        {
            set
            {
                if (value != null)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        int idx = value.LastIndexOf('.');
                        if (idx < 0)
                        {
                            _sysLogFilePath = value + ".txt";
                        }
                        else
                        {
                            _sysLogFilePath = value;
                        }
                    }
                    else
                    {
                        _sysLogFilePath = _defaultSysLogFilePath;
                    }
                }
            }
            get
            {
                return string.IsNullOrEmpty(_sysLogFilePath) ? _defaultSysLogFilePath : _sysLogFilePath;
            }
        }

        private string _exLogFilePath; //异常日志文件路径       
        private static string _defaultExLogFilePath = ProLaminator.Config.CfgManager.DirectoryBase
                                                    + ProLaminator.Config.CfgManager.DIRECTORY_NAME_FOR_LOG
                                                    + "\\" + ProLaminator.Config.CfgManager.EXCEPTION_LOG_FILE_NAME;

        /// <summary>
        /// 异常日志文件路径
        /// </summary>
        public string ExceptionLogFilePath
        {
            set
            {
                if (value != null)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        int idx = value.LastIndexOf('.');
                        if (idx < 0)
                        {
                            _exLogFilePath = value + ".txt";
                        }
                        else
                        {
                            _exLogFilePath = value;
                        }
                    }
                    else
                    {
                        _exLogFilePath = _defaultExLogFilePath;
                    }
                }
            }
            get
            {
                return string.IsNullOrEmpty(_exLogFilePath) ? _defaultExLogFilePath : _exLogFilePath;
            }
        }

        /// <summary>
        /// 启用上次生产程式
        /// [注:]
        /// </summary>
        public bool EnableLastRoutine { set; get; }

        /// <summary>
        /// 上次生产程式路径
        /// </summary>
        public string LastRoutinePath { set; get; }        

        #endregion

        #region 项目相关专用属性       

        /// <summary>
        /// 与PLC通信串口号
        /// [通过系统配置选配]
        /// </summary>
        public string SerialPortNameForPlc { set; get; }

        /// <summary>
        /// 串口通信字符串有效开始符
        /// </summary>
        public string SerialPortPackHeadStr { set; get; }

        /// <summary>
        /// 串口通信字符串结束符
        /// </summary>
        public string SerialPortPackEndStr { set; get; }

        /// <summary>
        /// 玻璃工位名称
        /// </summary>
        public string GlassStationName { set; get; }

        /// <summary>
        /// 膜1工位名称
        /// </summary>
        public string Membrane1StationName { set; get; }

        /// <summary>
        /// 膜2工位名称
        /// </summary>
        public string Membrane2StationName { set; get; }

        /// <summary>
        /// 运控速度低档
        /// </summary>
        public float SpeedLow { set; get; }

        /// <summary>
        /// 运控速度中档
        /// </summary>
        public float SpeedMedium { set; get; }

        /// <summary>
        /// 运控速度高档
        /// </summary>
        public float SpeedHigh { set; get; }

        /// <summary>
        /// 工具坐标系偏移X
        /// [注:工具坐标系到特征点描述坐标系的偏移量]
        /// </summary>
        public float ToolCenterOffsetX { set; get; }

        /// <summary>
        /// 工具坐标系偏移Y
        /// [注:工具坐标系到特征点描述坐标系的偏移量]
        /// </summary>
        public float ToolCenterOffsetY { set; get; }


        /// <summary>
        /// 玻璃总数
        /// </summary>
        public int GlassProTotal { get; set; }

        /// <summary>
        /// 玻璃OK数
        /// </summary>
        public int GlassProOK { get; set; }

        /// <summary>
        /// 玻璃NG数
        /// </summary>
        public int GlassProNG { get; set; }

        /// <summary>
        /// 玻璃良率
        /// </summary>
        public float GlassProYieldRatio { get; set; }



        /// <summary>
        /// 左膜总数
        /// </summary>
        public int Membrane1ProTotal { get;set; }
        /// <summary>
        /// 左膜Ok数
        /// </summary>
        public int Membrane1ProOK { get; set; }
        /// <summary>
        /// 左膜NG数
        /// </summary>
        public int Membrane1ProNG { get; set; }
        /// <summary>
        /// 左膜良率
        /// </summary>
        public float Membrane1ProYieldRatio { get;set; }

        /// <summary>
        /// 右膜总数
        /// </summary>
        public int Membrane2ProTotal { get; set; }

        /// <summary>
        /// 右膜OK数
        /// </summary>
        public int Membrane2ProOK { get; set; }

        /// <summary>
        /// 右膜NG数
        /// </summary>
        public int Membrane2ProNG { get; set; }

        /// <summary>
        /// 右膜良率
        /// </summary>
        public float Membrane2ProYieldRatio { get; set; }

        #endregion
    }
}
