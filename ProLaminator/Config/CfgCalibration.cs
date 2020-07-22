using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       CfgCalibration
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProLaminator.Config
 * File      Name：       CfgCalibration
 * Creating  Time：       5/21/2020 11:48:48 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProLaminator.Config
{
    public class CfgCalibration : ProCommon.Communal.Config
    {
        public CfgCalibration()
        {
            _calSolutionList = new ProVision.Communal.CalibrationSolutionList();
        }

        /// <summary>
        /// 标定方案列表
        /// </summary>
        private ProVision.Communal.CalibrationSolutionList _calSolutionList;

        /// <summary>
        /// 方案列表
        /// [注:实体增删改+查询]
        /// </summary>
        public ProVision.Communal.CalibrationSolutionList CalSolutionList
        {
            get { return _calSolutionList; }
            set { _calSolutionList = value; }
        }

        private System.ComponentModel.BindingList<ProVision.Communal.CalibrationSolution> _calSolutionBList;

        /// <summary>
        /// 方案列表
        /// [注:实体数据绑定+查询]
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public System.ComponentModel.BindingList<ProVision.Communal.CalibrationSolution> CalSolutionBList
        {
            get
            {
                if(_calSolutionBList==null)
                    _calSolutionBList= new System.ComponentModel.BindingList<ProVision.Communal.CalibrationSolution>();
                _calSolutionBList.Clear();

                for (int i = 0; i < _calSolutionList.Count; i++)
                {
                    _calSolutionBList.Add(_calSolutionList[i]);
                }
                return _calSolutionBList;
            }
        }
    }
}
