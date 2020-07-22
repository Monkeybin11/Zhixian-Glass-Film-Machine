using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

namespace ProLaminator.UI
{
    public partial class UIAccountManager : ProCommon.DerivedForm.FrmManageAccount
    {
        protected ProLaminator.Config.CfgManager _cfgMgr;
        protected ProLaminator.Config.CfgAccount  _cfgAcc;

        protected internal UIAccountManager(ProCommon.Communal.Language lan,ProCommon.Communal.Account crtAcc, ProLaminator.Config.CfgManager cfgMgr) :base(lan,crtAcc)
        {
            InitializeComponent();

            _cfgMgr = cfgMgr;
            if (cfgMgr!=null)
                _cfgAcc = cfgMgr.CfgAcc;
        }

        #region 覆写的函成员

        /// <summary>
        /// 初始化ComboBoxEdit账户列表控件
        /// </summary>
        protected override void UpdateComboBoxEditAccount()
        {
            if (_cfgAcc != null
                && _cfgAcc.AccList != null
              && _cfgAcc.AccList.Count > 0)
            {
                //注意:会引发SelectedIndexChanged事件
                this.cmbeAccountList.Properties.Items.Clear();

                if (!_isAccountListEventRegistered)
                {
                    this.cmbeAccountList.SelectedIndexChanged += CmbeAccountList_SelectedIndexChanged;
                    _isAccountListEventRegistered = true;
                }

                for (int i = 0; i < _cfgAcc.AccList.Count; i++)
                {
                    this.cmbeAccountList.Properties.Items.Add(_cfgAcc.AccList[i].Name);
                    if (_cfgAcc.AccList[i].Name == _currentAccount.Name)
                    {
                        _accountSelectedIndex = i;
                    }
                }

                if(_accountSelectedIndex>=0)
                {
                    this.cmbeAccountList.EditValue = _cfgAcc.AccList[_accountSelectedIndex].Name;
                    this.cmbeAccountList.SelectedIndex = _accountSelectedIndex;
                    this.txteAccountNameA.Text = _currentAccount.Name;
                    this.cmbeAccountAuthorityA.EditValue = AuthorityToString(_currentAccount.Authority);
                    this.cmbeAccountAuthorityA.SelectedIndex = AuthorityToInt(_currentAccount.Authority);
                    this.txteAccountPassWordA.Text = ProCommon.Communal.DESEncrypt.Decrypt(_currentAccount.PassWord);

                    this.txteAccountNameG.Text = _currentAccount.Name;
                    this.txteAccountAuthority.Text = AuthorityToString(_currentAccount.Authority);
                    this.txteAccountPassWordG.Text = ProCommon.Communal.DESEncrypt.Decrypt(_currentAccount.PassWord);
                }               
            }
        }

        /// <summary>
        /// 账户索引值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void CmbeAccountList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbeAccountList.SelectedIndex != -1)
            {
                _accountSelectedIndex = this.cmbeAccountList.SelectedIndex;
                _selectedAccount = _cfgAcc.AccList[_accountSelectedIndex];

                this.txteAccountNameA.Text = _selectedAccount.Name;
                this.cmbeAccountAuthorityA.SelectedItem = _selectedAccount.Authority;
                this.txteAccountPassWordA.Text = ProCommon.Communal.DESEncrypt.Decrypt(_selectedAccount.PassWord);
            }
        }
       
        /// <summary>
        ///确认单击事件
        /// </summary>
        /// <param name="sbtn"></param>
        protected override void ConfirmClick(DevExpress.XtraEditors.SimpleButton sbtn)
        {
            bool isChs = (LangVersion == ProCommon.Communal.Language.Chinese) ? true : false;
            string txt = string.Empty;
            string caption =string.Empty;
            string accName = string.Empty;
            string accID = string.Empty;

            switch (_accountOperation)
            {
                case ProCommon.Communal.AccountOperation.Add:
                    {
                        if(_currentAccount.Authority==ProCommon.Communal.AccountAuthority.Administrator)
                        {
                            txt = isChs ? "是否新增账户？" : "Would you like to add new account?";
                            caption = isChs ? "询问信息" : "Question Information";

                            if (ProCommon.DerivedForm.FrmMsgBox.Show(txt, caption,
                                ProCommon.DerivedForm.MyButtons.YesNo,
                                ProCommon.DerivedForm.MyIcon.Question, isChs) == System.Windows.Forms.DialogResult.Yes)
                            {
                                accName = this.txteAccountNameA.Text.Trim();
                                if (!string.IsNullOrEmpty(accName))
                                {
                                    int num = 0;
                                    if (_cfgAcc != null
                                        && _cfgAcc.AccList != null)
                                        num = _cfgAcc.AccList.Count;

                                    //注:新账户的添加,以账户列表中最后一个账户对应的编号再累加一,作为账户ID

                                    ProCommon.Communal.Account us = new ProCommon.Communal.Account(num + 1, accName);
                                    accName = this.cmbeAccountAuthorityA.SelectedItem.ToString();
                                    us.Authority = StringToAuthority(accName);
                                    accName = this.txteAccountPassWordA.Text.Trim();
                                    us.PassWord = ProCommon.Communal.DESEncrypt.Encrypt(accName);
                                    _cfgAcc.AccList.Add(us);
                                                                      
                                    _cfgMgr.Save();

                                    UpdateComboBoxEditAccount();
                                }
                                else
                                {
                                    txt = isChs ? "账户为空！" : "An empty account!";
                                    caption = isChs ? "警告信息" : "Warning Information";
                                    ProCommon.DerivedForm.FrmMsgBox.Show(txt, caption,
                                        ProCommon.DerivedForm.MyButtons.OK,
                                        ProCommon.DerivedForm.MyIcon.Warning, isChs);
                                }
                            }
                        }
                    }
                    break;
                case ProCommon.Communal.AccountOperation.Delete:
                    {
                        if (_currentAccount.Authority == ProCommon.Communal.AccountAuthority.Administrator)
                        {
                            txt = isChs ? "是否删除账户？" : "Would you like to delete the selected account?";
                            caption = isChs ? "询问信息" : "Question Information";

                            if (ProCommon.DerivedForm.FrmMsgBox.Show(txt, caption,
                                        ProCommon.DerivedForm.MyButtons.YesNo,
                                        ProCommon.DerivedForm.MyIcon.Question, isChs) == System.Windows.Forms.DialogResult.Yes)
                            {
                                //是否删除的是自身
                                if (_selectedAccount.ID != _currentAccount.ID)
                                {
                                    _cfgAcc.AccList.Delete(_selectedAccount);

                                    _cfgMgr.Save();
                                  
                                    UpdateComboBoxEditAccount();
                                }
                                else
                                {
                                    txt = isChs ? "无法删除自身账户！" : "Can not delete the account which was logged in !";
                                    caption = isChs ? "询问信息" : "Question Information";

                                    ProCommon.DerivedForm.FrmMsgBox.Show(txt, caption,
                                       ProCommon.DerivedForm.MyButtons.OK,
                                       ProCommon.DerivedForm.MyIcon.Warning, isChs);
                                }
                            }
                        }
                    }
                    break;
                case ProCommon.Communal.AccountOperation.Modify:
                    {
                        if (_currentAccount.Authority == ProCommon.Communal.AccountAuthority.Administrator)
                        {
                            txt = isChs ? "是否修改账户？" : "Would you like to modify the selected account?";
                            caption = isChs ? "询问信息" : "Question Information";

                            if (ProCommon.DerivedForm.FrmMsgBox.Show(txt, caption,
                                      ProCommon.DerivedForm.MyButtons.YesNo,
                                      ProCommon.DerivedForm.MyIcon.Question, isChs) == System.Windows.Forms.DialogResult.Yes)
                            {
                                accName = this.txteAccountNameA.Text.Trim();

                                //是否修改的是自身
                                if (_selectedAccount.ID != _currentAccount.ID)
                                {
                                    if (!string.IsNullOrEmpty(accName))
                                    {
                                        accID = _selectedAccount.ID;

                                        //新增修改后的用户
                                        _cfgAcc.AccList[accID].Name = accName;

                                        _selectedAccount.Name = accName;
                                        accName = this.cmbeAccountAuthorityA.SelectedItem.ToString();
                                        _cfgAcc.AccList[accID].Authority = StringToAuthority(accName);
                                        accName = this.txteAccountPassWordA.Text.Trim();
                                        _cfgAcc.AccList[accID].PassWord = ProCommon.Communal.DESEncrypt.Encrypt(accName);

                                        _cfgMgr.Save();
                                    }
                                    else
                                    {
                                        txt = isChs ? "账户为空！" : "An empty account!";
                                        caption = isChs ? "警告信息" : "Warning Information";
                                        ProCommon.DerivedForm.FrmMsgBox.Show(txt, caption,
                                           ProCommon.DerivedForm.MyButtons.OK,
                                           ProCommon.DerivedForm.MyIcon.Warning, isChs);
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(accName))
                                    {
                                        string id = _currentAccount.ID;

                                        accName = this.cmbeAccountAuthorityA.SelectedItem.ToString();
                                        _currentAccount.Authority = StringToAuthority(accName);
                                        _cfgAcc.AccList[id].Authority = StringToAuthority(accName);

                                        accName = this.txteAccountPassWordA.Text.Trim();
                                        _currentAccount.PassWord = ProCommon.Communal.DESEncrypt.Encrypt(accName);
                                        _cfgAcc.AccList[id].PassWord = ProCommon.Communal.DESEncrypt.Encrypt(accName);

                                        _cfgMgr.Save();
                                    }
                                    else
                                    {
                                        txt = isChs ? "账户为空！" : "An empty account!";
                                        caption = isChs ? "警告信息" : "Warning Information";
                                        ProCommon.DerivedForm.FrmMsgBox.Show(txt, caption,
                                          ProCommon.DerivedForm.MyButtons.OK,
                                          ProCommon.DerivedForm.MyIcon.Warning, isChs);
                                    }
                                }
                            }
                        }
                        else
                        {
                            accID = _currentAccount.ID;
                            accName = this.txteAccountPassWordG.Text.Trim();

                            if (!string.IsNullOrEmpty(accName))
                                _cfgAcc.AccList[accID].PassWord = ProCommon.Communal.DESEncrypt.Encrypt(accName);
                            else
                            {
                                txt = isChs ? "密码不能为空！" : "An empty pass word!";
                                caption = isChs ? "警告信息" : "Warning Information";
                                ProCommon.DerivedForm.FrmMsgBox.Show(txt, caption,
                                         ProCommon.DerivedForm.MyButtons.OK,
                                         ProCommon.DerivedForm.MyIcon.Warning, isChs);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
      

        /// <summary>
        /// 取消单击事件
        /// </summary>
        /// <param name="sbtn"></param>
        protected override void CancelClick(DevExpress.XtraEditors.SimpleButton sbtn)
        {
            this.Close();
        }
    

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void FrmAccountManager_Load(object sender, EventArgs e)
        {
            base.FrmAccountManager_Load(sender,e);
            InitFieldAndProperty();
            UpdateControl();
            SwitchPage(_currentAccount);
        }

        #endregion

        protected override void InitFieldAndProperty()
        {
            base.InitFieldAndProperty();
            this.DialogResult = System.Windows.Forms.DialogResult.None;

            _accountSelectedIndex = -1;
            _isAccountListEventRegistered = false;
            _accountOperation = ProCommon.Communal.AccountOperation.Modify;
        }

        protected override void UpdateControl()
        {
            base.UpdateControl();

            bool isChs = (LangVersion == ProCommon.Communal.Language.Chinese) ? true : false;
            string authority = isChs? "无权":"None";
            switch (_currentAccount.Authority)
            {
                case ProCommon.Communal.AccountAuthority.Administrator:                   
                    authority = isChs ? "管理" : "Administrator";
                    break;
                case ProCommon.Communal.AccountAuthority.Junior:                  
                    authority = isChs ? "初级" : "Junior";
                    break;
                case ProCommon.Communal.AccountAuthority.Senior:                   
                    authority = isChs ? "高级" : "Senior";
                    break;
                default:
                    break;
            }

            this.HtmlText = (isChs ? 
                ("当前账户:" +_currentAccount.Name + "\\权限：") 
                : ("CurrentAccount:" + _currentAccount.Name + "\\Authority:"))
                + authority; 
        }
    }
}
