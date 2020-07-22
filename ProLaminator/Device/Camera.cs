using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       Camera
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProLaminator.Device
 * File      Name：       Camera
 * Creating  Time：       5/20/2020 4:04:00 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProLaminator.Device
{
    public class Camera
    {
        public Camera(ProCommon.Communal.CameraProperty camPro)
        {
            Property = camPro;
            API = new ProDriver.API.CameraAPI(Property);
            CalSolutionList = new ProVision.Communal.CalibrationSolutionList();
        }

        /// <summary>
        /// 相机属性类
        /// </summary>
        public ProCommon.Communal.CameraProperty Property { private set; get; }

        /// <summary>
        /// 标定方案
        /// [有效且激活的标定方案]
        /// </summary>
        public ProVision.Communal.CalibrationSolution CalSolution
        {
            get
            {
                ProVision.Communal.CalibrationSolution calsol = null;
                if (CalSolutionList != null)
                {
                    int cnt = CalSolutionList.Count;
                    for (int i = 0; i < cnt; i++)
                    {
                        if (CalSolutionList[i].IsEffective
                            && CalSolutionList[i].IsActive)
                        {
                            calsol = CalSolutionList[i];
                            break;
                        }
                    }
                }
                return calsol;
            }
        }

        /// <summary>
        /// 属性：标定方案列表实体(用于实体删减+查询)
        /// </summary>
        public ProVision.Communal.CalibrationSolutionList CalSolutionList { set; get; }

        private System.ComponentModel.BindingList<ProVision.Communal.CalibrationSolution> _calBList;
        /// <summary>
        /// 属性:标定方案实体列表(用于数据绑定+查询)
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public System.ComponentModel.BindingList<ProVision.Communal.CalibrationSolution> CalSolutionBList
        {
            get
            {
                if (_calBList == null)
                    _calBList = new System.ComponentModel.BindingList<ProVision.Communal.CalibrationSolution>();
                _calBList.Clear();
              
                for (int i = 0; i < this.CalSolutionList.Count; i++)
                {
                    _calBList.Add(this.CalSolutionList[i]);
                }
                return _calBList;
            }
        }

        /// <summary>
        /// 相机函数接口
        /// </summary>
        public ProDriver.API.CameraAPI API { private set; get; }
    }
}
