namespace ProCommon.DerivedForm
{
    partial class FrmLogIn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogIn));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.grpcOperationPrompt = new DevExpress.XtraEditors.GroupControl();
            this.lblErrorPrompt = new DevExpress.XtraEditors.LabelControl();
            this.sbtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnLogIn = new DevExpress.XtraEditors.SimpleButton();
            this.grpcAccountAndPassWord = new DevExpress.XtraEditors.GroupControl();
            this.txtePassWord = new DevExpress.XtraEditors.TextEdit();
            this.cmbeAccountList = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblPassWord = new DevExpress.XtraEditors.LabelControl();
            this.lblAccountSelection = new DevExpress.XtraEditors.LabelControl();
            this.pePassWord = new DevExpress.XtraEditors.PictureEdit();
            this.peAccount = new DevExpress.XtraEditors.PictureEdit();
            this.peLogo = new DevExpress.XtraEditors.PictureEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpcOperationPrompt)).BeginInit();
            this.grpcOperationPrompt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpcAccountAndPassWord)).BeginInit();
            this.grpcAccountAndPassWord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtePassWord.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeAccountList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pePassWord.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peAccount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peLogo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.grpcOperationPrompt);
            this.layoutControl1.Controls.Add(this.grpcAccountAndPassWord);
            this.layoutControl1.Controls.Add(this.peLogo);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(926, 161, 562, 500);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(564, 349);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // grpcOperationPrompt
            // 
            this.grpcOperationPrompt.Controls.Add(this.lblErrorPrompt);
            this.grpcOperationPrompt.Controls.Add(this.sbtnCancel);
            this.grpcOperationPrompt.Controls.Add(this.sbtnLogIn);
            this.grpcOperationPrompt.Location = new System.Drawing.Point(16, 211);
            this.grpcOperationPrompt.Name = "grpcOperationPrompt";
            this.grpcOperationPrompt.Size = new System.Drawing.Size(532, 108);
            this.grpcOperationPrompt.TabIndex = 6;
            this.grpcOperationPrompt.Tag = "GRPC_OPERATIONPROMPT";
            this.grpcOperationPrompt.Text = "操作提示";
            // 
            // lblErrorPrompt
            // 
            this.lblErrorPrompt.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorPrompt.Appearance.Options.UseFont = true;
            this.lblErrorPrompt.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblErrorPrompt.Location = new System.Drawing.Point(2, 88);
            this.lblErrorPrompt.Name = "lblErrorPrompt";
            this.lblErrorPrompt.Size = new System.Drawing.Size(35, 18);
            this.lblErrorPrompt.TabIndex = 1;
            this.lblErrorPrompt.Tag = "LBLC_ERRORPROMPT";
            this.lblErrorPrompt.Text = "提示:";
            // 
            // sbtnCancel
            // 
            this.sbtnCancel.Location = new System.Drawing.Point(382, 44);
            this.sbtnCancel.Name = "sbtnCancel";
            this.sbtnCancel.Size = new System.Drawing.Size(75, 30);
            this.sbtnCancel.TabIndex = 0;
            this.sbtnCancel.Tag = "SBTN_CANCEL";
            this.sbtnCancel.Text = "取消";
            // 
            // sbtnLogIn
            // 
            this.sbtnLogIn.Location = new System.Drawing.Point(114, 44);
            this.sbtnLogIn.Name = "sbtnLogIn";
            this.sbtnLogIn.Size = new System.Drawing.Size(75, 30);
            this.sbtnLogIn.TabIndex = 0;
            this.sbtnLogIn.Tag = "SBTN_LOGIN";
            this.sbtnLogIn.Text = "登录";
            // 
            // grpcAccountAndPassWord
            // 
            this.grpcAccountAndPassWord.Controls.Add(this.txtePassWord);
            this.grpcAccountAndPassWord.Controls.Add(this.cmbeAccountList);
            this.grpcAccountAndPassWord.Controls.Add(this.lblPassWord);
            this.grpcAccountAndPassWord.Controls.Add(this.lblAccountSelection);
            this.grpcAccountAndPassWord.Controls.Add(this.pePassWord);
            this.grpcAccountAndPassWord.Controls.Add(this.peAccount);
            this.grpcAccountAndPassWord.Location = new System.Drawing.Point(16, 80);
            this.grpcAccountAndPassWord.Name = "grpcAccountAndPassWord";
            this.grpcAccountAndPassWord.Size = new System.Drawing.Size(532, 125);
            this.grpcAccountAndPassWord.TabIndex = 5;
            this.grpcAccountAndPassWord.Tag = "GRPC_ACCOUNTPROMPT";
            this.grpcAccountAndPassWord.Text = "账户信息";
            // 
            // txtePassWord
            // 
            this.txtePassWord.Location = new System.Drawing.Point(190, 84);
            this.txtePassWord.Name = "txtePassWord";
            this.txtePassWord.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtePassWord.Properties.Appearance.Options.UseFont = true;
            this.txtePassWord.Properties.PasswordChar = '*';
            this.txtePassWord.Size = new System.Drawing.Size(267, 30);
            this.txtePassWord.TabIndex = 3;
            // 
            // cmbeAccountList
            // 
            this.cmbeAccountList.Location = new System.Drawing.Point(190, 41);
            this.cmbeAccountList.Name = "cmbeAccountList";
            this.cmbeAccountList.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbeAccountList.Properties.Appearance.Options.UseFont = true;
            this.cmbeAccountList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbeAccountList.Size = new System.Drawing.Size(267, 30);
            this.cmbeAccountList.TabIndex = 2;
            // 
            // lblPassWord
            // 
            this.lblPassWord.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassWord.Appearance.Options.UseFont = true;
            this.lblPassWord.Location = new System.Drawing.Point(24, 87);
            this.lblPassWord.Name = "lblPassWord";
            this.lblPassWord.Size = new System.Drawing.Size(47, 24);
            this.lblPassWord.TabIndex = 1;
            this.lblPassWord.Tag = "LBLC_PASSWORD";
            this.lblPassWord.Text = "密码:";
            // 
            // lblAccountSelection
            // 
            this.lblAccountSelection.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountSelection.Appearance.Options.UseFont = true;
            this.lblAccountSelection.Location = new System.Drawing.Point(24, 41);
            this.lblAccountSelection.Name = "lblAccountSelection";
            this.lblAccountSelection.Size = new System.Drawing.Size(47, 24);
            this.lblAccountSelection.TabIndex = 1;
            this.lblAccountSelection.Tag = "LBLC_ACCOUNTSELECTION";
            this.lblAccountSelection.Text = "账户:";
            // 
            // pePassWord
            // 
            this.pePassWord.Cursor = System.Windows.Forms.Cursors.Default;
            this.pePassWord.EditValue = ((object)(resources.GetObject("pePassWord.EditValue")));
            this.pePassWord.Location = new System.Drawing.Point(114, 76);
            this.pePassWord.Name = "pePassWord";
            this.pePassWord.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pePassWord.Size = new System.Drawing.Size(40, 40);
            this.pePassWord.TabIndex = 0;
            // 
            // peAccount
            // 
            this.peAccount.Cursor = System.Windows.Forms.Cursors.Default;
            this.peAccount.EditValue = ((object)(resources.GetObject("peAccount.EditValue")));
            this.peAccount.Location = new System.Drawing.Point(114, 30);
            this.peAccount.Name = "peAccount";
            this.peAccount.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peAccount.Size = new System.Drawing.Size(40, 40);
            this.peAccount.TabIndex = 0;
            // 
            // peLogo
            // 
            this.peLogo.Cursor = System.Windows.Forms.Cursors.Default;
            this.peLogo.Location = new System.Drawing.Point(16, 16);
            this.peLogo.Name = "peLogo";
            this.peLogo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peLogo.Size = new System.Drawing.Size(532, 55);
            this.peLogo.StyleController = this.layoutControl1;
            toolTipTitleItem1.Text = "供应商";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "深圳市汇众智慧科技有限公司";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.peLogo.SuperTip = superToolTip1;
            this.peLogo.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.simpleSeparator1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(564, 349);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.peLogo;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(538, 61);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 309);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(538, 14);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.Location = new System.Drawing.Point(0, 61);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(538, 3);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.grpcAccountAndPassWord;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 64);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(538, 131);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.grpcOperationPrompt;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 195);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(538, 114);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // FrmLogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 349);
            this.Controls.Add(this.layoutControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "FRM_LOGIN";
            this.Text = "用户登录";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpcOperationPrompt)).EndInit();
            this.grpcOperationPrompt.ResumeLayout(false);
            this.grpcOperationPrompt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpcAccountAndPassWord)).EndInit();
            this.grpcAccountAndPassWord.ResumeLayout(false);
            this.grpcAccountAndPassWord.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtePassWord.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeAccountList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pePassWord.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peAccount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peLogo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.PictureEdit peLogo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        protected internal DevExpress.XtraEditors.ComboBoxEdit cmbeAccountList;
        protected internal DevExpress.XtraEditors.TextEdit txtePassWord;
        protected internal DevExpress.XtraEditors.GroupControl grpcAccountAndPassWord;
        protected internal DevExpress.XtraEditors.LabelControl lblPassWord;
        protected internal DevExpress.XtraEditors.LabelControl lblAccountSelection;
        protected internal DevExpress.XtraEditors.PictureEdit pePassWord;
        protected internal DevExpress.XtraEditors.PictureEdit peAccount;
        protected internal DevExpress.XtraEditors.GroupControl grpcOperationPrompt;
        protected internal DevExpress.XtraEditors.SimpleButton sbtnCancel;
        protected internal DevExpress.XtraEditors.SimpleButton sbtnLogIn;
        protected internal DevExpress.XtraEditors.LabelControl lblErrorPrompt;
    }
}