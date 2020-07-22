using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       LaminatorProcessData
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProLaminator.Data
 * File      Name：       LaminatorProcessData
 * Creating  Time：       5/20/2020 3:53:43 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProLaminator.Data
{
    /// <summary>
    /// 智显定位贴附图像处理过程结果
    /// </summary>
    public class LaminatorProcessData:ProVision.Communal.ProcessData
    {
        /// <summary>
        /// 检测区域
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public HalconDotNet.HObject InspetcArea { set; get; }

        /// <summary>
        /// 结果区域
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public HalconDotNet.HObject ResultRegion { set; get; }

        /// <summary>
        /// Ok产品数量
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public int ProductOKNumber { set; get; }

        /// <summary>
        /// NG产品数量
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public int ProductNGNumber { set; get; }

        /// <summary>
        /// 产品总数
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public int ProductTotalNumber { set; get; }

        /// <summary>
        /// 产品良率
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public float ProductYieldRatio { set; get; }

        /// <summary>
        /// 相对模板位置的世界坐标偏移X
        /// </summary>
        public float DeltaX { set; get; }

        /// <summary>
        /// 相对模板位置的世界坐标偏移Y
        /// </summary>
        public float DeltaY { set; get; }

        /// <summary>
        /// 相对模板位置的角偏移
        /// [单位:度]
        /// </summary>
        public float DeltaAglDegree { set; get; }

        /// <summary>
        /// 匹配模板的实例像素坐标Row
        /// </summary>
        public HalconDotNet.HTuple Row { set; get; }

        /// <summary>
        /// 匹配模板的实例像素坐标Col
        /// </summary>
        public HalconDotNet.HTuple Col { set; get; }

        /// <summary>
        /// 匹配模板的实例相对模板的角偏移
        /// [单位:弧度]
        /// </summary>
        public HalconDotNet.HTuple DeltaAglRad { set; get; }

        /// <summary>
        /// 结果标记
        /// </summary>
        public int ResultFlag { set; get; }

    }
}
