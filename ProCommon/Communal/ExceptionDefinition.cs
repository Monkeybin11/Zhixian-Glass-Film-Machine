using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ExceptionDefinition
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProCommon.Communal
 * File      Name：       ExceptionDefinition
 * Creating  Time：       4/21/2020 1:20:06 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProCommon.Communal
{
    #region 异常类

    /// <summary>
    ///  初始化异常类
    /// </summary>
    public class InitException : System.Exception
    {
        /// <summary>
        /// 字段：设备
        /// </summary>
        private string _deviceName;

        /// <summary>
        /// 字段：原因
        /// </summary>
        private string _reason;

        /// <summary>
        /// 是否中文标记
        /// [注:在中英文之间切换]
        /// </summary>
        private bool _isChinese;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="deviceName">设备名称</param>
        /// <param name="reason">异常原因</param>
        /// <param name="isChinese">是否中文标记</param>
        public InitException(string deviceName, string reason, bool isChinese)
            : base(reason)
        {
            _deviceName = deviceName;
            _reason = reason;
            _isChinese = isChinese;
        }

        /// <summary>
        /// 覆写：ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _isChinese ? string.Format("设备[{0}]初始化时发生错误:{1}", _deviceName, _reason)
                : string.Format("Error occured when device[{0}] initialized:{1}", _deviceName, _reason);
        }

    }

    /// <summary>
    /// 启动异常类
    /// </summary>
    public class StartException : System.Exception
    {
        /// <summary>
        /// 字段：设备
        /// </summary>
        private string _deviceName;

        /// <summary>
        /// 字段：原因
        /// </summary>
        private string _reason;

        /// <summary>
        /// 是否中文标记
        /// [注:在中英文之间切换]
        /// </summary>
        private bool _isChinese;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="deviceName">设备名称</param>
        /// <param name="reason">异常原因</param>
        /// <param name="isChinese">是否中文标记</param>
        public StartException(string deviceName, string reason, bool isChinese)
            : base(reason)
        {
            _deviceName = deviceName;
            _reason = reason;
            _isChinese = isChinese;
        }
        /// <summary>
        /// 覆写：ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _isChinese ? string.Format("设备[{0}]启动时发生错误:{1}", _deviceName, _reason)
              : string.Format("Error occured when device[{0}] started:{1}", _deviceName, _reason);
        }

    }

    /// <summary>
    /// 停止异常类
    /// </summary>
    public class StopException : System.Exception
    {
        /// <summary>
        /// 字段：设备
        /// </summary>
        private string _deviceName;

        /// <summary>
        /// 字段：原因
        /// </summary>
        private string _reason;

        /// <summary>
        /// 是否中文标记
        /// [注:在中英文之间切换]
        /// </summary>
        private bool _isChinese;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="deviceName">设备名称</param>
        /// <param name="reason">异常原因</param>
        /// <param name="isChinese">是否中文标记</param>
        public StopException(string deviceName, string reason, bool isChinese)
            : base(reason)
        {
            _deviceName = deviceName;
            _reason = reason;
            _isChinese = isChinese;
        }
        /// <summary>
        /// 覆写：ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _isChinese ? string.Format("设备[{0}]停止时发生错误:{1}", _deviceName, _reason)
               : string.Format("Error occured when device[{0}] stopped:{1}", _deviceName, _reason);
        }

    }

    /// <summary>
    /// 释放异常类
    /// </summary>
    public class ReleaseException : System.Exception
    {
        /// <summary>
        /// 字段：设备
        /// </summary>
        private string _deviceName;

        /// <summary>
        /// 字段：原因
        /// </summary>
        private string _reason;

        /// <summary>
        /// 是否中文标记
        /// [注:在中英文之间切换]
        /// </summary>
        private bool _isChinese;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="deviceName">设备名称</param>
        /// <param name="reason">异常原因</param>
        /// <param name="isChinese">是否中文标记</param>
        public ReleaseException(string deviceName, string reason, bool isChinese)
            : base(reason)
        {
            _deviceName = deviceName;
            _reason = reason;
            _isChinese = isChinese;
        }

        /// <summary>
        /// 覆写：ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _isChinese ? string.Format("设备[{0}]释放时发生错误:{1}", _deviceName, _reason)
              : string.Format("Error occured when device[{0}] released:{1}", _deviceName, _reason);
        }

    }

    /// <summary>
    /// 加载异常类
    /// </summary>
    public class LoadException : Exception
    {
        private string _configName;
        private string _reason;

        /// <summary>
        /// 是否中文标记
        /// [注:在中英文之间切换]
        /// </summary>
        private bool _isChinese;

        public LoadException(string configName, string reason, bool isChinese)
            : base(reason)
        {
            _configName = configName;
            _reason = reason;
            _isChinese = isChinese;
        }

        /// <summary>
        /// 覆写方法：Exception的ToString()方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _isChinese ? string.Format("配置[{0}]加载时发生错误:{1}", _configName, _reason)
             : string.Format("Error occured when config file [{0}] loaded:{1}", _configName, _reason);
        }
    }

    #endregion
}
