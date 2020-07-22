using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace ProLaminator.UI
{
    public partial class FrmNewRoutine : DevExpress.XtraEditors.XtraForm
    {
        public bool IsConfirmed;
        public string NewRoutineName; 

        public ProCommon.Communal.Language LangVersion { protected set; get; }
        private FrmNewRoutine()
        {
            InitializeComponent();
        }

        public FrmNewRoutine(ProCommon.Communal.Language lan):this()
        {
            LangVersion = lan;
            this.Load += FrmNewRoutine_Load;
        }

        protected internal virtual void FrmNewRoutine_Load(object sender, EventArgs e)
        {
            Init();
        }

        protected internal void Init()
        {
            InitFieldAndProperty();
            InitControl();
        }

        protected internal virtual void InitFieldAndProperty()
        {
            IsConfirmed = false;
            NewRoutineName = null;
        }

        protected internal virtual void InitControl()
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;

            bool isChs = (LangVersion == ProCommon.Communal.Language.Chinese);
            string str = this.Tag.ToString();           
            this.Text = isChs ? 
                ProLaminator.Properties.Resources.ResourceManager.GetString("chs_"+str) 
                : ProLaminator.Properties.Resources.ResourceManager.GetString("en_" + str);

            str = this.lblPromptNewRoutineName.Tag.ToString();
            this.lblPromptNewRoutineName.Text = isChs ? 
                ProLaminator.Properties.Resources.ResourceManager.GetString("chs_" + str) 
                : ProLaminator.Properties.Resources.ResourceManager.GetString("en_" + str);

            UpdateSimpleButton(this.sbtnConfirm, LangVersion, ProLaminator.Properties.Resources.ResourceManager);
            UpdateSimpleButton(this.sbtnCancel, LangVersion, ProLaminator.Properties.Resources.ResourceManager);
        }

        private void UpdateSimpleButton(DevExpress.XtraEditors.SimpleButton sbtn, ProCommon.Communal.Language lan,System.Resources.ResourceManager rscMgr)
        {
            if (sbtn != null
            && sbtn.Tag != null)
            {
                sbtn.Click -= Sbtn_Click;
                if(rscMgr!=null)
                {
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = sbtn.Tag.ToString();
                    sbtn.Text = isChs ? rscMgr.GetString("chs_" + str) : rscMgr.GetString("en_" + str);
                }
                sbtn.Click += Sbtn_Click;
            }
        }

        protected internal virtual void Sbtn_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton sbtn = sender as DevExpress.XtraEditors.SimpleButton;
            if(sbtn!=null
                && sbtn.Tag!=null)
            {
                bool isChs = (LangVersion == ProCommon.Communal.Language.Chinese);
                switch (sbtn.Tag.ToString())
                {
                    case "SBTN_CONFIRM":
                        {
                            if(!string.IsNullOrEmpty(this.txteNewRoutineName.Text))
                            {
                                NewRoutineName = this.txteNewRoutineName.Text;
                                IsConfirmed = true;
                                this.Close();
                            }
                            else
                            {
                                IsConfirmed = false;
                                string txt = isChs ? "输入新程式的名称为空!" : "Empty name for new routine !";
                                string caption = isChs ? "警示信息" : "Warning Message";
                                                              
                                ProCommon.DerivedForm.FrmMsgBox.Show(txt, caption,
                                    ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, isChs);
                            }                            
                        }
                        break;
                    case "SBTN_CANCEL":
                        {
                            IsConfirmed = false;
                            this.Close();
                        }
                        break;
                    default:break;
                }
            }
        }
    }
}