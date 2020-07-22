using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       CfgAccount
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProLaminator.Config
 * File      Name：       CfgAccount
 * Creating  Time：       5/20/2020 3:38:16 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProLaminator.Config
{
    /// <summary>
    /// 账户配置
    /// </summary>
    [Serializable]
    public class CfgAccount : ProCommon.Communal.Config
    {
        public CfgAccount()
        {
            _accountList = new ProCommon.Communal.AccountList();
        }

        private ProCommon.Communal.AccountList _accountList;

        /// <summary>
        /// 账户列表(实体增删改+查询)
        /// </summary>
        public ProCommon.Communal.AccountList AccList
        {
            set { _accountList = value; }
            get { return _accountList; }
        }

        private System.ComponentModel.BindingList<ProCommon.Communal.Account> _accBList;
        /// <summary>
        /// 账户列表(实体数据绑定+查询)
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public System.ComponentModel.BindingList<ProCommon.Communal.Account> AccBList
        {
            get
            {
                if (_accBList == null)
                    _accBList = new System.ComponentModel.BindingList<ProCommon.Communal.Account>();
                _accBList.Clear();

                for (int i = 0; i < _accountList.Count; i++)
                {
                    _accBList.Add(_accountList[i]);
                }
                return _accBList;
            }
        }
    }
}
