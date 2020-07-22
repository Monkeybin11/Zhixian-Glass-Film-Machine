using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       CfgCamera
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProLaminator.Config
 * File      Name：       CfgCamera
 * Creating  Time：       5/19/2020 5:01:58 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProLaminator.Config
{
    [Serializable]
    public class CfgCamera : ProCommon.Communal.Config
    {
        public CfgCamera()
        {
            _propertyList = new ProCommon.Communal.CameraPropertyList();
        }

        /// <summary>
        /// 相机属性
        /// </summary>
        private ProCommon.Communal.CameraPropertyList _propertyList;

        /// <summary>
        /// 属性列表
        /// [注:实体增删改+查询]
        /// </summary>
        public ProCommon.Communal.CameraPropertyList PropertyList
        {
            set { _propertyList = value; }
            get { return _propertyList; }
        }



        private System.ComponentModel.BindingList<ProCommon.Communal.CameraProperty> _propertyBList;

        /// <summary>
        /// 属性列表
        /// [注:实体数据绑定+查询]
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public System.ComponentModel.BindingList<ProCommon.Communal.CameraProperty> PropertyBList
        {
            get
            {
                if (_propertyBList == null)
                    _propertyBList = new System.ComponentModel.BindingList<ProCommon.Communal.CameraProperty>();
                _propertyBList.Clear();

                for (int i = 0; i < _propertyList.Count; i++)
                {
                    _propertyBList.Add(_propertyList[i]);
                }
                return _propertyBList;
            }
        }
    }
}
