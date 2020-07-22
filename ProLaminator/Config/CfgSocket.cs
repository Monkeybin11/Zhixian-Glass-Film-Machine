using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       CfgSocket
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProLaminator.Config
 * File      Name：       CfgSocket
 * Creating  Time：       5/20/2020 3:43:47 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProLaminator.Config
{
    /// <summary>
    /// Socket配置
    /// </summary>
    [Serializable]
    public class CfgSocket : ProCommon.Communal.Config
    {
        public CfgSocket()
        {
            _propertyList = new ProCommon.Communal.SocketPropertyList();
        }

        private ProCommon.Communal.SocketPropertyList _propertyList;

        /// <summary>
        /// 属性列表
        /// [注:实体增删改+查询]
        /// </summary>
        public ProCommon.Communal.SocketPropertyList PropertyList
        {
            set { _propertyList = value; }
            get { return _propertyList; }
        }

        private System.ComponentModel.BindingList<ProCommon.Communal.SocketProperty> _propertyBList;

        /// <summary>
        /// 属性列表
        /// [注:实体数据绑定+查询]
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public System.ComponentModel.BindingList<ProCommon.Communal.SocketProperty> PropertyBList
        {
            get
            {
                if (_propertyBList == null)
                    _propertyBList = new System.ComponentModel.BindingList<ProCommon.Communal.SocketProperty>();
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
