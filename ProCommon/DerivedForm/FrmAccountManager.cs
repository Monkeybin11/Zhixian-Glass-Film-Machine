using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


namespace ProCommon.DerivedForm
{
    public partial class FrmManageAccount : DevExpress.XtraEditors.XtraForm
    {
        protected ProCommon.Communal.Account _currentAccount, _selectedAccount; //登录的账户，选择的账户
        protected int _accountSelectedIndex;
        protected bool _isAccountListEventRegistered; //账户列表控件选择项索引值改变事件是否注册过回调函数标记
        protected ProCommon.Communal.AccountOperation _accountOperation; //账户操作选择:新增,删除,修改

        /// <summary>
        /// 软件语言版本
        /// </summary>
        public ProCommon.Communal.Language LangVersion { protected set; get; }


        #region 可能覆写的成员

        /// <summary>
        /// 初始化ComboBoxEdit账户列表控件
        /// </summary>
        protected virtual void UpdateComboBoxEditAccount()
        {

        }

        /// <summary>
        /// 账户索引值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void CmbeAccountList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected virtual void Sbtn_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton sbtn = sender as DevExpress.XtraEditors.SimpleButton;
            if (sbtn != null
                && sbtn.Tag != null)
            {
                switch (sbtn.Tag.ToString())
                {
                    case "SBTN_CONFIRM":
                        ConfirmClick(sbtn);
                        break;                  
                    case "SBTN_CANCEL":
                        CancelClick(sbtn);
                        break;
                    default: break;
                }
            }
        }

        /// <summary>
        /// 确认单击事件
        /// </summary>
        /// <param name="sbtn"></param>
        protected virtual void ConfirmClick(DevExpress.XtraEditors.SimpleButton sbtn)
        {

        }

        /// <summary>
        /// 取消单击事件
        /// </summary>
        /// <param name="sbtn"></param>
        protected virtual void CancelClick(DevExpress.XtraEditors.SimpleButton sbtn)
        {

        }

        protected virtual void FrmAccountManager_Load(object sender, EventArgs e)
        {

        }

        #endregion

        private FrmManageAccount()
        {
            InitializeComponent();
        }

        protected internal FrmManageAccount(ProCommon.Communal.Language lan,Communal.Account us) : this()
        {
            _currentAccount = us;
            _selectedAccount = us;
            LangVersion = lan;
            this.Load += FrmAccountManager_Load;
        }

        /// <summary>
        /// 初始化字段
        /// </summary>
        protected virtual void InitFieldAndProperty()
        {

        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        protected internal virtual void UpdateControl()
        {
            UpdateXtraTabControl();
            UpdateGroupControl();
            UpdateLabelControl();

            UpdateComboBoxEditAccount();
            UpdateComboBoxEditAuthority();

            UpdateSimpleButton();
            UpdateRadioButton();
        }

        /// <summary>
        /// 初始化XtraTabPage控件
        /// </summary>
        private void UpdateXtraTabControl()
        {
            for (int i = 0; i < this.xtbcManageAccount.TabPages.Count; i++)
            {
                UpdateXtraTabPage(this.xtbcManageAccount.TabPages[i], LangVersion,ProCommon.Properties.Resources.ResourceManager);
            }
        }

        protected internal void UpdateXtraTabPage(DevExpress.XtraTab.XtraTabPage xtbp,ProCommon.Communal.Language lan,System.Resources.ResourceManager resourceManager)
        {
            if(xtbp!=null
                && xtbp.Tag!=null)
            {
                if(resourceManager!=null)
                {
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = xtbp.Tag.ToString();
                    xtbp.Text = isChs ? 
                        resourceManager.GetString("chs_" + str) 
                        : resourceManager.GetString("en_" + str);
                }
            }
        }

        /// <summary>
        /// 初始化GroupControl控件
        /// </summary>
        private void UpdateGroupControl()
        {
            UpdateGroupControl(this.grpcOperationOption, LangVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateGroupControl(this.grpcAccountDetailA, LangVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateGroupControl(this.grpcAccountDetailG, LangVersion, ProCommon.Properties.Resources.ResourceManager);
        }

        protected void UpdateGroupControl(DevExpress.XtraEditors.GroupControl grpc,ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager)
        {
            if (grpc != null
               && grpc.Tag != null)
            {
                if (resourceManager != null)
                {
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = grpc.Tag.ToString();
                    grpc.Text = isChs ? 
                        resourceManager.GetString("chs_" + str) 
                        : resourceManager.GetString("en_" + str);
                }
            }
        }

        /// <summary>
        /// 初始化LabelControl控件
        /// </summary>
        private void UpdateLabelControl()
        {
            UpdateLabelControl(this.lblAccountList, LangVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblAccountNameA, LangVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblAccountAuthorityA, LangVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblAccountPassWordA, LangVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblAccountNameG, LangVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblAccountAuthorityG, LangVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblAccountPassWordG, LangVersion, ProCommon.Properties.Resources.ResourceManager);
        }

        protected void UpdateLabelControl(DevExpress.XtraEditors.LabelControl lblc, ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager)
        {
            if (lblc != null
              && lblc.Tag != null)
            {
                if (resourceManager != null)
                {
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = lblc.Tag.ToString();
                    lblc.Text = isChs ? 
                        resourceManager.GetString("chs_" + str) 
                        : resourceManager.GetString("en_" + str);
                }
            }
        }

        /// <summary>
        /// 初始化ComboBoxEdit控件
        /// </summary>
        private void UpdateComboBoxEditAuthority()
        {
            bool isChs = (LangVersion == ProCommon.Communal.Language.Chinese);
            string str = isChs ? "无权" : "None";

            //按枚举定义值的升序进行添加
            this.cmbeAccountAuthorityA.Properties.Items.Clear();          
            this.cmbeAccountAuthorityA.Properties.Items.Add(str);
            str = isChs ? "初级" : "Junior";
            this.cmbeAccountAuthorityA.Properties.Items.Add(str);
            str = isChs ? "高级" : "Senior";
            this.cmbeAccountAuthorityA.Properties.Items.Add(str);
            str = isChs ? "管理" : "Administrator";
            this.cmbeAccountAuthorityA.Properties.Items.Add(str);
        }

        /// <summary>
        /// 初始化SimpleButton控件
        /// </summary>
        private void UpdateSimpleButton()
        {
            UpdateSimpleButton(this.sbtnConfirmA, LangVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateSimpleButton(this.sbtnCancelA, LangVersion, ProCommon.Properties.Resources.ResourceManager);

            UpdateSimpleButton(this.sbtnConfirmG, LangVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateSimpleButton(this.sbtnCancelG, LangVersion, ProCommon.Properties.Resources.ResourceManager);
        }

        /// <summary>
        /// 更新SimpleButton控件
        /// </summary>
        /// <param name="sbtn"></param>
        /// <param name="lan"></param>
        protected void UpdateSimpleButton(DevExpress.XtraEditors.SimpleButton sbtn,ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager)
        {
            if(sbtn!=null
                && sbtn.Tag!=null)
            {
                if (resourceManager != null)
                {
                    sbtn.Click -= Sbtn_Click;
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = sbtn.Tag.ToString();
                    sbtn.Text = isChs ? resourceManager.GetString("chs_" + str) : resourceManager.GetString("en_" + str);
                    sbtn.Click += Sbtn_Click;
                }
            }
        }

        /// <summary>
        /// 初始化RadioButton控件
        /// </summary>
        private void UpdateRadioButton()
        {
            UpdateRadioButton(this.rbtnAdd, LangVersion, ProCommon.Properties.Resources.ResourceManager);
            this.rbtnAdd.Checked = false;

            UpdateRadioButton(this.rbtnDelete, LangVersion, ProCommon.Properties.Resources.ResourceManager);
            this.rbtnDelete.Checked = false;

            UpdateRadioButton(this.rbtnModify, LangVersion, ProCommon.Properties.Resources.ResourceManager);
            this.rbtnModify.Checked = true;

            UpdateControlStatus(ProCommon.Communal.AccountOperation.Modify);
        }

        protected void UpdateRadioButton(System.Windows.Forms.RadioButton rdbtn, ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager)
        {
            if (rdbtn != null
               && rdbtn.Tag != null)
            {
                if (resourceManager != null)
                {
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = rdbtn.Tag.ToString();
                    rdbtn.Text = isChs ? resourceManager.GetString("chs_" + str) : resourceManager.GetString("en_" + str);
                    rdbtn.CheckedChanged += Rbtn_CheckedChanged;
                }
            }
        }
        private void UpdateControlStatus(ProCommon.Communal.AccountOperation uo)
        {
            switch (uo)
            {
                case ProCommon.Communal.AccountOperation.Add:
                    {
                        this.txteAccountNameA.ReadOnly = false;
                        this.cmbeAccountAuthorityA.ReadOnly = false;
                        this.txteAccountPassWordA.ReadOnly = false;
                    }
                    break;
                case ProCommon.Communal.AccountOperation.Delete:
                    {
                        this.txteAccountNameA.ReadOnly = true;
                        this.cmbeAccountAuthorityA.ReadOnly = true;
                        this.txteAccountPassWordA.ReadOnly = true;
                    }
                    break;
                case ProCommon.Communal.AccountOperation.Modify:
                    {
                        this.txteAccountNameA.ReadOnly = true;
                        this.cmbeAccountAuthorityA.ReadOnly = (_currentAccount.Name == _selectedAccount.Name) ? false : true;
                        this.txteAccountPassWordA.ReadOnly = (_currentAccount.Name == _selectedAccount.Name) ? false : true;
                    }
                    break;
                case ProCommon.Communal.AccountOperation.NONE:
                default:
                    break;
            }

            this.txteAccountAuthority.ReadOnly = true;
            this.txteAccountNameG.ReadOnly = true;
        }
        protected virtual void Rbtn_CheckedChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.RadioButton rbtn = sender as System.Windows.Forms.RadioButton;

            if (rbtn!=null)
            {
                switch(rbtn.Tag.ToString())
                {
                    case "RBTN_ADD":
                        {
                            if (rbtn.Checked)
                                _accountOperation = ProCommon.Communal.AccountOperation.Add;
                        }
                        break;
                    case "RBTN_DELETE":
                        {
                            if (rbtn.Checked)
                                _accountOperation = ProCommon.Communal.AccountOperation.Delete;
                        }
                        break;
                    case "RBTN_MODIFY":                                              
                    default:
                        {
                            if (rbtn.Checked)
                                _accountOperation = ProCommon.Communal.AccountOperation.Modify;
                        }
                        break;
                }
                UpdateControlStatus(_accountOperation);
            }
        }      
        protected virtual void SwitchPage(ProCommon.Communal.Account us)
        {
            switch(us.Authority)
            {
                case ProCommon.Communal.AccountAuthority.Administrator:
                    {
                        this.xtbpManagerAuthority.PageVisible = true;
                        this.xtbpGeneralAuthority.PageVisible = false;
                    }
                    break;
                default:
                    {
                        this.xtbpManagerAuthority.PageVisible = false;
                        this.xtbpGeneralAuthority.PageVisible = true;
                    }
                    break;
            }
        }

        #region 辅助方法

        /// <summary>
        /// 账户权限转为换字符串
        /// </summary>
        /// <param name="au"></param>
        /// <returns></returns>
        protected string AuthorityToString(ProCommon.Communal.AccountAuthority au)
        {
            string s = "无权";
            switch (au)
            {
                case ProCommon.Communal.AccountAuthority.Administrator:
                    s = "管理";
                    break;
                case ProCommon.Communal.AccountAuthority.Junior:
                    s = "初级";
                    break;
                case ProCommon.Communal.AccountAuthority.Senior:
                    s = "高级";
                    break;
                default:
                    break;
            }
            return s;
        }

        /// <summary>
        /// 字符串转换为账户权限
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        protected ProCommon.Communal.AccountAuthority StringToAuthority(string s)
        {
            ProCommon.Communal.AccountAuthority a = ProCommon.Communal.AccountAuthority.None;
            if (s != null)
            {
                switch (s)
                {
                    case "初级":
                        a = ProCommon.Communal.AccountAuthority.Junior;
                        break;
                    case "高级":
                        a = ProCommon.Communal.AccountAuthority.Senior;
                        break;
                    case "管理":
                        a = ProCommon.Communal.AccountAuthority.Administrator;
                        break;
                    case "无权":
                    default:
                        break;
                }
            }
            return a;
        }

        protected virtual int AuthorityToInt(ProCommon.Communal.AccountAuthority au)
        {
            int i = 0;
            switch (au)
            {
                case ProCommon.Communal.AccountAuthority.Administrator:
                    i = 3;
                    break;
                case ProCommon.Communal.AccountAuthority.Junior:
                    i = 1;
                    break;
                case ProCommon.Communal.AccountAuthority.Senior:
                    i = 2;
                    break;
                default:
                    break;
            }
            return i;
        }
        protected virtual ProCommon.Communal.AccountAuthority IntToAuthority(int i)
        {
            ProCommon.Communal.AccountAuthority a = ProCommon.Communal.AccountAuthority.None;
            switch (i)
            {
                case 1:
                    a = ProCommon.Communal.AccountAuthority.Junior;
                    break;
                case 2:
                    a = ProCommon.Communal.AccountAuthority.Senior;
                    break;
                case 3:
                    a = ProCommon.Communal.AccountAuthority.Administrator;
                    break;
                default:
                    break;
            }
            return a;
        }

        #endregion
    }
}