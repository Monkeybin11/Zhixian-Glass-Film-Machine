using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       AccountDefinition
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProCommon.Communal
 * File      Name：       AccountDefinition
 * Creating  Time：       4/21/2020 1:19:21 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProCommon.Communal
{
    #region 账户与操作
    /// <summary>
    /// 账户权限
    /// </summary>
    public enum AccountAuthority : uint
    {
        None = 0,
        Junior = 1, //初级权限,仅打开、浏览,及运行默认参数下的程式
        Senior = 2, //高级权限,打开、浏览,及运行修改参数下的程式(允许修改参数)
        Administrator = 3 //管理员权限,打开、浏览及运行修改参数后的程式,另外允许增减用户
    }

    /// <summary>
    /// 账户操作
    /// </summary>
    public enum AccountOperation : uint
    {
        NONE = 0,
        Add = 1,
        Delete = 2,
        Modify = 3
    }

    /// <summary>
    /// 账户
    /// </summary>
    [Serializable]
    public class Account : System.ComponentModel.INotifyPropertyChanged
    {
        public virtual event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        private Account() { }       

        /// <summary>
        /// 创建账户
        /// [注:账户的ID由调用者分配
        /// 账户ID唯一,否则存储列表时无法存储]
        /// </summary>
        /// <param name="number">编号</param>
        /// <param name="accID">唯一标识符</param>
        public Account(int number,string accID):this()
        {
            this.Number = number;
            this.ID = accID;
        }

        /// <summary>
        /// ID--只允许访问,但为序列化暂时允许设置和访问
        /// </summary>
        public string ID
        {
            set; get;
        }

        public int Number
        {
            set; get;
        }

        public string Name { set; get; }

        public AccountAuthority Authority { set; get; }

        public string PassWord { set; get; }
    }

    /// <summary>
    /// 账户列表
    /// </summary>
    [Serializable]
    public class AccountList : System.Collections.ICollection
    {
        private System.Collections.SortedList _list;
        public AccountList() { _list = new System.Collections.SortedList(); }

        public void Add(ProCommon.Communal.Account acc)
        {
            if (!_list.ContainsKey(acc.ID))
            {
                _list.Add(acc.ID, acc);
            }
        }

        public void Delete(ProCommon.Communal.Account acc)
        {
            if (_list.ContainsKey(acc.ID))
            {
                _list.Remove(acc.ID);
            }
        }

        public void Clear()
        {
            if (_list != null)
                _list.Clear();
        }

        public ProCommon.Communal.Account this[int indx]
        {
            get
            {
                ProCommon.Communal.Account acc = null;
                if (_list.Count > 0
                    && indx >= 0
                    && indx < _list.Count)
                {
                    acc = (Account)_list.GetByIndex(indx);
                }
                return acc;
            }
        }

        public ProCommon.Communal.Account this[string accID]
        {
            get
            {
                ProCommon.Communal.Account acc = null;
                if (_list.ContainsKey(accID))
                {
                    acc = (ProCommon.Communal.Account)_list[accID];
                }
                return acc;
            }
        }

        public void CopyTo(Array arr, int indx)
        {
            _list.CopyTo(arr, indx);
        }

        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return this; }

        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }

    #endregion
}
