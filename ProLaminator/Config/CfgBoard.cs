using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       CfgBoard
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProLaminator.Config
 * File      Name：       CfgBoard
 * Creating  Time：       5/20/2020 3:40:22 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProLaminator.Config
{
    /// <summary>
    /// 控制板卡配置
    /// </summary>
    [Serializable]
    public class CfgBoard : ProCommon.Communal.Config
    {
        public CfgBoard()
        {
            _propertyList = new ProCommon.Communal.BoardPropertyList();
        }

        private ProCommon.Communal.BoardPropertyList _propertyList;

        /// <summary>
        /// 属性列表
        /// [注:实体增删改+查询]
        /// </summary>
        public ProCommon.Communal.BoardPropertyList PropertyList
        {
            set { _propertyList = value; }
            get { return _propertyList; }
        }      

        private System.ComponentModel.BindingList<ProCommon.Communal.BoardProperty> _propertyBList;

        /// <summary>
        /// 属性列表
        /// [注:实体数据绑定+查询]
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public System.ComponentModel.BindingList<ProCommon.Communal.BoardProperty> PropertyBList
        {
            get
            {
                if (_propertyBList == null)
                    _propertyBList = new System.ComponentModel.BindingList<ProCommon.Communal.BoardProperty>();
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
