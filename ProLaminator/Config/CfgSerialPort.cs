using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       CfgSerialPort
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProLaminator.Config
 * File      Name：       CfgSerialPort
 * Creating  Time：       5/20/2020 3:46:00 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProLaminator.Config
{
    [Serializable]
    public class CfgSerialPort:ProCommon.Communal.Config
    {
        public CfgSerialPort()
        {
            _propertyList = new ProCommon.Communal.SerialPortPropertyList();
        }

        private ProCommon.Communal.SerialPortPropertyList _propertyList;

        /// <summary>
        /// 属性列表
        /// [注:实体增删改+查询]
        /// </summary>
        public ProCommon.Communal.SerialPortPropertyList PropertyList
        {
            set { _propertyList = value; }
            get { return _propertyList; }
        }

        private System.ComponentModel.BindingList<ProCommon.Communal.SerialPortProperty> _propertyBList;

        /// <summary>
        /// 属性列表
        /// [注:实体数据绑定+查询]
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public System.ComponentModel.BindingList<ProCommon.Communal.SerialPortProperty> PropertyBList
        {
            get
            {
                if (_propertyBList == null)
                    _propertyBList = new System.ComponentModel.BindingList<ProCommon.Communal.SerialPortProperty>();
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
