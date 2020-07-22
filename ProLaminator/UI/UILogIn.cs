using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProLaminator.UI
{
    public partial class UILogIn : ProCommon.DerivedForm.FrmLogIn
    {
        protected internal ProLaminator.Config.CfgAccount _cfgAccount;

        #region 覆写的成员

        protected override void FrmLogIn_Load(object sender, EventArgs e)
        {
            InitFieldAndProperty();
            UpdateControl();
        }

        protected override void UpdateControl()
        {
            base.UpdateControl();

            if (_cfgAccount != null
                && _cfgAccount.AccList.Count > 0)
            {
                this.cmbeAccountList.Properties.Items.Clear();

                for (int i = 0; i < _cfgAccount.AccList.Count; i++)
                {
                    this.cmbeAccountList.Properties.Items.Add(_cfgAccount.AccList[i].Name);
                }

                this.cmbeAccountList.SelectedIndexChanged += cmbeAccountList_SelectedIndexChanged;

                this.cmbeAccountList.EditValue = _cfgAccount.AccList[0].Name;
                this.cmbeAccountList.SelectedIndex = 0;               
            }
        }     

        #endregion

        private UILogIn(ProCommon.Communal.Language lan) :base(lan)
        {
            InitializeComponent();
        }

        public UILogIn(ProCommon.Communal.Language lan, ProLaminator.Config.CfgAccount cfgAcc):this(lan)
        {
            _cfgAccount = cfgAcc;
        }

        protected virtual void cmbeAccountList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbeAccountList.SelectedIndex != -1)
            {
                _selectedIndex = this.cmbeAccountList.SelectedIndex;
                CurrentAccount = _cfgAccount.AccList[_selectedIndex];
            }
        }
    }
}
