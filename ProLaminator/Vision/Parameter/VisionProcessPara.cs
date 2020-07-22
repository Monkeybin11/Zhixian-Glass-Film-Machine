using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       VisionProcessPara
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProLaminator.Vision.Parameter
 * File      Name：       VisionProcessPara
 * Creating  Time：       5/19/2020 3:50:44 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProLaminator.Vision.Parameter
{
    /// <summary>
    /// 视觉参数公共基类
    /// </summary>
    [Serializable]
    public abstract class VisionProcessPara
    {
        public VisionProcessPara()
        {
            IsSaveImageAll = false;
            IsSaveImageOK = false;
            IsSaveImageNG = false;
            PathForSaveImageAll = null;
            PathForSaveImageOK = null;
            PathForSaveImageNG = null;
        }

        /// <summary>
        /// 是否保存所有图像
        /// </summary>
        public bool IsSaveImageAll { set; get; }

        /// <summary>
        /// 是否保存OK图像
        /// </summary>
        public bool IsSaveImageOK { set; get; }

        /// <summary>
        /// 是否保存NG图像
        /// </summary>
        public bool IsSaveImageNG { set; get; }

        /// <summary>
        /// 保存所有图像的目录路径
        /// </summary>
        public string PathForSaveImageAll { set; get; }

        /// <summary>
        /// 保存OK图像的目录路径
        /// </summary>
        public string PathForSaveImageOK { set; get; }

        /// <summary>
        /// 保存NG图像的目录路径
        /// </summary>
        public string PathForSaveImageNG { set; get; }

        /// <summary>
        /// 保存定位参数的目录路径
        /// </summary>
        public string PathForSaveLocPara { set; get; }
    }

    /// <summary>
    /// 玻璃定位纠偏视觉算法参数
    /// </summary>
    [Serializable]
    public class VisionParaForGlass: ProLaminator.Vision.Parameter.VisionProcessPara
    {

        public VisionParaForGlass()
        {
            CameraExposure =0.5f;
            CameraGain = 1.5f;
            Gamma = 1.0f;
            ExternalTriggerDelay =50;
            ExternalTriggerDebouncerTime = 50;
        }

        /// <summary>
        /// 曝光时间
        /// [时间单位:毫秒]
        /// </summary>
        public float CameraExposure { get; set; }

        /// <summary>
        /// 相机增益
        /// [物理增益]
        /// </summary>
        public float CameraGain { get; set; }

        /// <summary>
        /// Gamma
        /// </summary>
        public float Gamma { get; set; }

        /// <summary>
        /// 外触发延时
        /// [时间单位:毫秒]
        /// </summary>
        public float ExternalTriggerDelay { get; set; }

        /// <summary>
        /// 外触发消抖时间
        /// [时间单位:微秒]
        /// </summary>
        public float ExternalTriggerDebouncerTime { get; set; }
    }

    /// <summary>
    /// 膜1定位纠偏视觉算法参数
    /// </summary>
    [Serializable]
    public class VisionParaForMembrane1 : ProLaminator.Vision.Parameter.VisionProcessPara
    {
        public VisionParaForMembrane1()
        {
            CameraExposure = 0.5f;
            CameraGain = 1.5f;
            Gamma = 1.0f;
            ExternalTriggerDelay = 50;
            ExternalTriggerDebouncerTime = 50;
        }

        /// <summary>
        /// 曝光时间
        /// [时间单位:毫秒]
        /// </summary>
        public float CameraExposure { get; set; }

        /// <summary>
        /// 相机增益
        /// [物理增益]
        /// </summary>
        public float CameraGain { get; set; }

        /// <summary>
        /// Gamma
        /// </summary>
        public float Gamma { get; set; }

        /// <summary>
        /// 外触发延时
        /// [时间单位:毫秒]
        /// </summary>
        public float ExternalTriggerDelay { get; set; }

        /// <summary>
        /// 外触发消抖时间
        /// [时间单位:微秒]
        /// </summary>
        public float ExternalTriggerDebouncerTime { get; set; }
    }

    /// <summary>
    /// 膜2定位纠偏视觉算法参数
    /// </summary>
    [Serializable]
    public class VisionParaForMembrane2 : ProLaminator.Vision.Parameter.VisionProcessPara
    {
        public VisionParaForMembrane2()
        {
            CameraExposure = 0.5f;
            CameraGain = 1.5f;
            Gamma = 1.0f;
            ExternalTriggerDelay = 50;
            ExternalTriggerDebouncerTime = 50;
        }

        /// <summary>
        /// 曝光时间
        /// [时间单位:毫秒]
        /// </summary>
        public float CameraExposure { get; set; }

        /// <summary>
        /// 相机增益
        /// [物理增益]
        /// </summary>
        public float CameraGain { get; set; }
        /// <summary>
        /// Gamma
        /// </summary>
        public float Gamma { get; set; }

        /// <summary>
        /// 外触发延时
        /// [时间单位:毫秒]
        /// </summary>
        public float ExternalTriggerDelay { get; set; }

        /// <summary>
        /// 外触发消抖时间
        /// [时间单位:微秒]
        /// </summary>
        public float ExternalTriggerDebouncerTime { get; set; }
    }

}
