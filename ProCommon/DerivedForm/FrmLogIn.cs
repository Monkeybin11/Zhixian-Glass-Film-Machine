using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;

namespace ProCommon.DerivedForm
{
    public partial class FrmLogIn : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 当前登录账户
        /// </summary>
        public ProCommon.Communal.Account CurrentAccount { protected set; get; } 
         
        protected int _selectedIndex = -1;      

        /// <summary>
        /// 软件语言版本s
        /// </summary>
        public ProCommon.Communal.Language LangVersion { private set; get; }

        #region 必须覆写的成员        
      
        #endregion

        private FrmLogIn()
        {
            InitializeComponent();
        }

        protected internal FrmLogIn(ProCommon.Communal.Language lan):this()
        {
            LangVersion = lan;         
            this.Load += FrmLogIn_Load;
        }

        /// <summary>
        /// 初始化字段
        /// </summary>
        protected virtual void InitFieldAndProperty()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmbeAccountList.SelectedIndex = -1;
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        protected virtual void UpdateControl()
        {
            string str = this.Tag.ToString();
            bool isChs = LangVersion == ProCommon.Communal.Language.Chinese;
            this.HtmlText = isChs ? 
                ProCommon.Properties.Resources.ResourceManager.GetString("chs_"+str)
                : ProCommon.Properties.Resources.ResourceManager.GetString("en_" + str);

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            UpdateGroupControl();
            UpdateLabelControl();
            UpdateSimpleButton();
        }

        /// <summary>
        /// 更新GroupControl控件
        /// </summary>
        private void UpdateGroupControl()
        {
            UpdateGroupControl(this.grpcAccountAndPassWord, LangVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateGroupControl(this.grpcOperationPrompt, LangVersion, ProCommon.Properties.Resources.ResourceManager);
        }

        protected void UpdateGroupControl(DevExpress.XtraEditors.GroupControl grpc, ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager)
        {
            if (grpc != null
               && grpc.Tag != null)
            {
                if (resourceManager != null)
                {
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = grpc.Tag.ToString();
                    grpc.Text = isChs ? resourceManager.GetString("chs_" + str) : resourceManager.GetString("en_" + str);
                }
            }
        }

        /// <summary>
        /// 更新LabelControl控件
        /// </summary>
        private void UpdateLabelControl()
        {
            UpdateLabelControl(this.lblAccountSelection,LangVersion,ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblPassWord, LangVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateLabelControl(this.lblErrorPrompt, LangVersion, ProCommon.Properties.Resources.ResourceManager);
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
                    lblc.Text = isChs ? resourceManager.GetString("chs_" + str) : resourceManager.GetString("en_" + str);
                }
            }
        }

        private void UpdateSimpleButton()
        {
            UpdateSimpleButton(this.sbtnLogIn, LangVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateSimpleButton(this.sbtnCancel, LangVersion, ProCommon.Properties.Resources.ResourceManager);
        }

        /// <summary>
        /// 更新SimpleButton控件
        /// </summary>
        /// <param name="sbtn"></param>
        /// <param name="lan"></param>
        protected internal void UpdateSimpleButton(DevExpress.XtraEditors.SimpleButton sbtn, ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager)
        {
            if (sbtn != null
                && sbtn.Tag != null)
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

        private void Sbtn_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton sbtn = sender as DevExpress.XtraEditors.SimpleButton;
            if(sbtn!=null)
            {
                bool isChs = (LangVersion == ProCommon.Communal.Language.Chinese);
                switch (sbtn.Tag.ToString())
                {
                    case "SBTN_LOGIN":
                        {
                            if (this.cmbeAccountList.SelectedIndex != -1)
                            {
                                string psw = ProCommon.Communal.DESEncrypt.Encrypt(this.txtePassWord.Text);
                                if (psw.Length != CurrentAccount.PassWord.Length)
                                {
                                    this.lblErrorPrompt.Appearance.BackColor = Color.Red;
                                    this.lblErrorPrompt.Text = isChs? " 提示:密码错误!":"Promotion:Wrong PassWord !";
                                }
                                else
                                {
                                    if (psw == CurrentAccount.PassWord)
                                    {
                                        this.lblErrorPrompt.Appearance.BackColor = Color.Green;
                                        this.lblErrorPrompt.Text = isChs ? " 提示:登录成功!":"Promotion:Login Successfully !";
                                        this.DialogResult = System.Windows.Forms.DialogResult.OK; 

                                        //this.cmbeAccountList.Properties.Items.Clear();
                                        //this.txtePassWord.EditValue = null;
                                        this.Close();
                                    }
                                    else
                                    {
                                        this.lblErrorPrompt.Appearance.BackColor = Color.Red;
                                        this.lblErrorPrompt.Text = isChs ? " 提示:密码错误!" : "Promotion:Wrong PassWord !";                                      
                                    }
                                }
                            }
                            else
                            {
                                this.lblErrorPrompt.Appearance.BackColor = Color.Yellow;
                                this.lblErrorPrompt.Text = isChs ? " 提示:未选择账户!":"Promotion:No Account Selected !";
                            }
                        }
                        break;
                    case "SBTN_CANCEL":
                        this.Close();
                        break;
                }
            }           
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void FrmLogIn_Load(object sender, EventArgs e)
        {
           
        }
    }
}