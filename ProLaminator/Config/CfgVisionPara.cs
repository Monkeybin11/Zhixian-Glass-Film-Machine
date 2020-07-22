using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       CfgVisionPara
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProLaminator.Config
 * File      Name：       CfgVisionPara
 * Creating  Time：       5/19/2020 5:04:52 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProLaminator.Config
{
    /// <summary>
    /// 视觉算法参数配置
    /// </summary>
    [Serializable]
    public class CfgVisionPara:ProCommon.Communal.Config
    {

        public CfgVisionPara()
        {
            //默认参数作为项目用配置
            RoutineDirectory = null;
            RoutineName = null;
            CoordinateReferenceMode = 1;

            ParaForGlass = new Vision.Parameter.VisionParaForGlass();
            ParaForMembrane1 = new Vision.Parameter.VisionParaForMembrane1();
            ParaForMembrane2 = new Vision.Parameter.VisionParaForMembrane2();
        }

        /// <summary>
        /// 参数作为程式时保存文件夹路径
        /// </summary>
        public string RoutineDirectory { set; get; }

        /// <summary>
        /// 参数作为程式时的文件名
        /// [不含扩展名]
        /// </summary>
        public string RoutineName { set; get; }

        /// <summary>
        /// 坐标参考模式
        /// [0--坐标以相对指定模板的偏移描述;
        ///  1--坐标以相对相机中心的偏移描述]
        /// </summary>
        public int CoordinateReferenceMode { set; get; }

        /// <summary>
        /// 玻璃定位纠偏视觉参数
        /// </summary>
        public ProLaminator.Vision.Parameter.VisionParaForGlass ParaForGlass { set; get; }

        /// <summary>
        /// 膜1定位纠偏视觉参数
        /// </summary>
        public ProLaminator.Vision.Parameter.VisionParaForMembrane1 ParaForMembrane1 { set; get; }

        /// <summary>
        /// 膜2定位纠偏视觉参数
        /// </summary>
        public ProLaminator.Vision.Parameter.VisionParaForMembrane2 ParaForMembrane2 { set; get; }
    }
}
