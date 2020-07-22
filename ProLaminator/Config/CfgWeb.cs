using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       CfgWeb
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProLaminator.Config
 * File      Name：       CfgWeb
 * Creating  Time：       5/20/2020 3:46:37 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProLaminator.Config
{
    public class CfgWeb : ProCommon.Communal.Config
    {
        public CfgWeb()
        {
            _propertyList = new ProCommon.Communal.WebPropertyList();
        }

        private ProCommon.Communal.WebPropertyList _propertyList;

        /// <summary>
        /// 属性列表
        /// [注:实体增删改+查询]
        /// </summary>
        public ProCommon.Communal.WebPropertyList PropertyList
        {
            set { _propertyList = value; }
            get { return _propertyList; }
        }

        private System.ComponentModel.BindingList<ProCommon.Communal.WebProperty> _propertyBList;

        /// <summary>
        /// 属性列表
        /// [注:实体数据绑定+查询]
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public System.ComponentModel.BindingList<ProCommon.Communal.WebProperty> PropertyBList
        {
            get
            {
                if (_propertyBList == null)
                    _propertyBList = new System.ComponentModel.BindingList<ProCommon.Communal.WebProperty>();
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
