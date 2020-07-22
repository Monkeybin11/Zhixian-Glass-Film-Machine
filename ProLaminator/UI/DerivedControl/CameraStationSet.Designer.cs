namespace ProLaminator.UI.DerivedControl
{
    partial class CameraStationSet
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.layoutControlRoot = new DevExpress.XtraLayout.LayoutControl();
            this.sbtnExitSet = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnSaveParameter = new DevExpress.XtraEditors.SimpleButton();
            this.tlstrpStatus = new System.Windows.Forms.ToolStrip();
            this.tlstrplblCoordinatePrompt = new System.Windows.Forms.ToolStripLabel();
            this.tlstrptxtCoordinate = new System.Windows.Forms.ToolStripTextBox();
            this.tlstrplblGrayValuePrompt = new System.Windows.Forms.ToolStripLabel();
            this.tlstrptxtGrayValue = new System.Windows.Forms.ToolStripTextBox();
            this.tlstrptxtCamerID = new System.Windows.Forms.ToolStripTextBox();
            this.sbtnNextImage = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnLastImage = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnTestOffline = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnLoadImage = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnAcquireOnce = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnSetCamera = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnStopAcquire = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnAcquireContinuous = new DevExpress.XtraEditors.SimpleButton();
            this.xtbcFunction = new DevExpress.XtraTab.XtraTabControl();
            this.xtbpPreprocess = new DevExpress.XtraTab.XtraTabPage();
            this.xtbpProcessParameter = new DevExpress.XtraTab.XtraTabPage();
            this.speColUnit = new DevExpress.XtraEditors.SpinEdit();
            this.speRowUnit = new DevExpress.XtraEditors.SpinEdit();
            this.speGamma = new DevExpress.XtraEditors.SpinEdit();
            this.speDebouncerTime = new DevExpress.XtraEditors.SpinEdit();
            this.speTriggerDelay = new DevExpress.XtraEditors.SpinEdit();
            this.speGain = new DevExpress.XtraEditors.SpinEdit();
            this.speExposureTime = new DevExpress.XtraEditors.SpinEdit();
            this.bmgrMain = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.bbiRectangle1 = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRectangle2 = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCircle = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCenterCross = new DevExpress.XtraBars.BarButtonItem();
            this.bbiClearIconic = new DevExpress.XtraBars.BarButtonItem();
            this.hwndctrlDisplay = new HalconDotNet.HWindowControl();
            this.ppmMain = new DevExpress.XtraBars.PopupMenu(this.components);
            this.lblColUnit = new DevExpress.XtraEditors.LabelControl();
            this.lblRowUnit = new DevExpress.XtraEditors.LabelControl();
            this.lblcGamma = new DevExpress.XtraEditors.LabelControl();
            this.lblcDebouncerTime = new DevExpress.XtraEditors.LabelControl();
            this.lblcTriggerDelay = new DevExpress.XtraEditors.LabelControl();
            this.lblcGain = new DevExpress.XtraEditors.LabelControl();
            this.lblExposureTime = new DevExpress.XtraEditors.LabelControl();
            this.chkeEnableAlgorithm = new DevExpress.XtraEditors.CheckEdit();
            this.chkeSaveNG = new DevExpress.XtraEditors.CheckEdit();
            this.chkeSaveOK = new DevExpress.XtraEditors.CheckEdit();
            this.chkeSaveAll = new DevExpress.XtraEditors.CheckEdit();
            this.sbtnSetMatchModel = new DevExpress.XtraEditors.SimpleButton();
            this.xtbpInteractiveCommunication = new DevExpress.XtraTab.XtraTabPage();
            this.speFlagMembrane2 = new DevExpress.XtraEditors.SpinEdit();
            this.speFlagMembrane1 = new DevExpress.XtraEditors.SpinEdit();
            this.speFlagGlass = new DevExpress.XtraEditors.SpinEdit();
            this.sbtnWriteDataMembrane2 = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnWriteDataMembrane1 = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnWriteDataGlass = new DevExpress.XtraEditors.SimpleButton();
            this.speCoordRMembrane2 = new DevExpress.XtraEditors.SpinEdit();
            this.speCoordYMembrane2 = new DevExpress.XtraEditors.SpinEdit();
            this.speCoordRMembrane1 = new DevExpress.XtraEditors.SpinEdit();
            this.speCoordYMembrane1 = new DevExpress.XtraEditors.SpinEdit();
            this.speCoordXMembrane2 = new DevExpress.XtraEditors.SpinEdit();
            this.speCoordRGlass = new DevExpress.XtraEditors.SpinEdit();
            this.speCoordXMembrane1 = new DevExpress.XtraEditors.SpinEdit();
            this.speCoordYGlass = new DevExpress.XtraEditors.SpinEdit();
            this.speCoordXGlass = new DevExpress.XtraEditors.SpinEdit();
            this.lblcFlagPrompt = new DevExpress.XtraEditors.LabelControl();
            this.lblcCoordRPrompt = new DevExpress.XtraEditors.LabelControl();
            this.lblcCoordYPrompt = new DevExpress.XtraEditors.LabelControl();
            this.lblcCoordXPrompt = new DevExpress.XtraEditors.LabelControl();
            this.lblcMembrane2Prompt = new DevExpress.XtraEditors.LabelControl();
            this.lblcMembrane1Prompt = new DevExpress.XtraEditors.LabelControl();
            this.lblcGlassPrompt = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroupRoot = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.chkeModelReference = new DevExpress.XtraEditors.CheckEdit();
            this.chkeCameraCenterReference = new DevExpress.XtraEditors.CheckEdit();
            this.lblCoordinateMode = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlRoot)).BeginInit();
            this.layoutControlRoot.SuspendLayout();
            this.tlstrpStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtbcFunction)).BeginInit();
            this.xtbcFunction.SuspendLayout();
            this.xtbpProcessParameter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speColUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speRowUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speGamma.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speDebouncerTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speTriggerDelay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speGain.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speExposureTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bmgrMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppmMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkeEnableAlgorithm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkeSaveNG.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkeSaveOK.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkeSaveAll.Properties)).BeginInit();
            this.xtbpInteractiveCommunication.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speFlagMembrane2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speFlagMembrane1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speFlagGlass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speCoordRMembrane2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speCoordYMembrane2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speCoordRMembrane1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speCoordYMembrane1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speCoordXMembrane2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speCoordRGlass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speCoordXMembrane1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speCoordYGlass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speCoordXGlass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkeModelReference.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkeCameraCenterReference.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlRoot
            // 
            this.layoutControlRoot.Controls.Add(this.sbtnExitSet);
            this.layoutControlRoot.Controls.Add(this.sbtnSaveParameter);
            this.layoutControlRoot.Controls.Add(this.tlstrpStatus);
            this.layoutControlRoot.Controls.Add(this.sbtnNextImage);
            this.layoutControlRoot.Controls.Add(this.sbtnLastImage);
            this.layoutControlRoot.Controls.Add(this.sbtnTestOffline);
            this.layoutControlRoot.Controls.Add(this.sbtnLoadImage);
            this.layoutControlRoot.Controls.Add(this.sbtnAcquireOnce);
            this.layoutControlRoot.Controls.Add(this.sbtnSetCamera);
            this.layoutControlRoot.Controls.Add(this.sbtnStopAcquire);
            this.layoutControlRoot.Controls.Add(this.sbtnAcquireContinuous);
            this.layoutControlRoot.Controls.Add(this.xtbcFunction);
            this.layoutControlRoot.Controls.Add(this.hwndctrlDisplay);
            this.layoutControlRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlRoot.Location = new System.Drawing.Point(0, 0);
            this.layoutControlRoot.Name = "layoutControlRoot";
            this.layoutControlRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(284, 295, 812, 500);
            this.layoutControlRoot.Root = this.layoutControlGroupRoot;
            this.layoutControlRoot.Size = new System.Drawing.Size(1081, 759);
            this.layoutControlRoot.TabIndex = 0;
            this.layoutControlRoot.Text = "layoutControl1";
            // 
            // sbtnExitSet
            // 
            this.sbtnExitSet.Location = new System.Drawing.Point(849, 716);
            this.sbtnExitSet.Name = "sbtnExitSet";
            this.sbtnExitSet.Size = new System.Drawing.Size(216, 27);
            this.sbtnExitSet.StyleController = this.layoutControlRoot;
            this.sbtnExitSet.TabIndex = 16;
            this.sbtnExitSet.Tag = "SBTN_EXITSET";
            this.sbtnExitSet.Text = "退出设置";
            // 
            // sbtnSaveParameter
            // 
            this.sbtnSaveParameter.Location = new System.Drawing.Point(673, 716);
            this.sbtnSaveParameter.Name = "sbtnSaveParameter";
            this.sbtnSaveParameter.Size = new System.Drawing.Size(170, 27);
            this.sbtnSaveParameter.StyleController = this.layoutControlRoot;
            this.sbtnSaveParameter.TabIndex = 15;
            this.sbtnSaveParameter.Tag = "SBTN_SAVEPARAMETER";
            this.sbtnSaveParameter.Text = "保存参数";
            // 
            // tlstrpStatus
            // 
            this.tlstrpStatus.AutoSize = false;
            this.tlstrpStatus.Dock = System.Windows.Forms.DockStyle.None;
            this.tlstrpStatus.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tlstrpStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlstrplblCoordinatePrompt,
            this.tlstrptxtCoordinate,
            this.tlstrplblGrayValuePrompt,
            this.tlstrptxtGrayValue,
            this.tlstrptxtCamerID});
            this.tlstrpStatus.Location = new System.Drawing.Point(16, 16);
            this.tlstrpStatus.Name = "tlstrpStatus";
            this.tlstrpStatus.Size = new System.Drawing.Size(645, 25);
            this.tlstrpStatus.TabIndex = 14;
            this.tlstrpStatus.Text = "toolStrip1";
            // 
            // tlstrplblCoordinatePrompt
            // 
            this.tlstrplblCoordinatePrompt.Name = "tlstrplblCoordinatePrompt";
            this.tlstrplblCoordinatePrompt.Size = new System.Drawing.Size(43, 22);
            this.tlstrplblCoordinatePrompt.Tag = "TLSTRPLBL_COORDINATEPROMPT";
            this.tlstrplblCoordinatePrompt.Text = "坐标:";
            // 
            // tlstrptxtCoordinate
            // 
            this.tlstrptxtCoordinate.Name = "tlstrptxtCoordinate";
            this.tlstrptxtCoordinate.ReadOnly = true;
            this.tlstrptxtCoordinate.Size = new System.Drawing.Size(100, 25);
            this.tlstrptxtCoordinate.Tag = "TLSTRPTXT_COORDINATE";
            // 
            // tlstrplblGrayValuePrompt
            // 
            this.tlstrplblGrayValuePrompt.Name = "tlstrplblGrayValuePrompt";
            this.tlstrplblGrayValuePrompt.Size = new System.Drawing.Size(43, 22);
            this.tlstrplblGrayValuePrompt.Tag = "TLSTRPLBL_GRAYVALUEPROMPT";
            this.tlstrplblGrayValuePrompt.Text = "灰度:";
            // 
            // tlstrptxtGrayValue
            // 
            this.tlstrptxtGrayValue.Name = "tlstrptxtGrayValue";
            this.tlstrptxtGrayValue.ReadOnly = true;
            this.tlstrptxtGrayValue.Size = new System.Drawing.Size(100, 25);
            this.tlstrptxtGrayValue.Tag = "TLSTRPTXT_GRAYVALUE";
            // 
            // tlstrptxtCamerID
            // 
            this.tlstrptxtCamerID.Name = "tlstrptxtCamerID";
            this.tlstrptxtCamerID.ReadOnly = true;
            this.tlstrptxtCamerID.Size = new System.Drawing.Size(100, 25);
            // 
            // sbtnNextImage
            // 
            this.sbtnNextImage.Location = new System.Drawing.Point(490, 716);
            this.sbtnNextImage.Name = "sbtnNextImage";
            this.sbtnNextImage.Size = new System.Drawing.Size(177, 27);
            this.sbtnNextImage.StyleController = this.layoutControlRoot;
            this.sbtnNextImage.TabIndex = 13;
            this.sbtnNextImage.Tag = "SBTN_NEXTIMAGE";
            this.sbtnNextImage.Text = "下张图像";
            // 
            // sbtnLastImage
            // 
            this.sbtnLastImage.Location = new System.Drawing.Point(491, 683);
            this.sbtnLastImage.Name = "sbtnLastImage";
            this.sbtnLastImage.Size = new System.Drawing.Size(170, 27);
            this.sbtnLastImage.StyleController = this.layoutControlRoot;
            this.sbtnLastImage.TabIndex = 12;
            this.sbtnLastImage.Tag = "SBTN_LASTIMAGE";
            this.sbtnLastImage.Text = "上张图像";
            // 
            // sbtnTestOffline
            // 
            this.sbtnTestOffline.Location = new System.Drawing.Point(333, 716);
            this.sbtnTestOffline.Name = "sbtnTestOffline";
            this.sbtnTestOffline.Size = new System.Drawing.Size(151, 27);
            this.sbtnTestOffline.StyleController = this.layoutControlRoot;
            this.sbtnTestOffline.TabIndex = 11;
            this.sbtnTestOffline.Tag = "SBTN_TESTOFFLINE";
            this.sbtnTestOffline.Text = "离线测试";
            // 
            // sbtnLoadImage
            // 
            this.sbtnLoadImage.Location = new System.Drawing.Point(333, 683);
            this.sbtnLoadImage.Name = "sbtnLoadImage";
            this.sbtnLoadImage.Size = new System.Drawing.Size(152, 27);
            this.sbtnLoadImage.StyleController = this.layoutControlRoot;
            this.sbtnLoadImage.TabIndex = 10;
            this.sbtnLoadImage.Tag = "SBTN_LOADIMAGE";
            this.sbtnLoadImage.Text = "加载图像";
            // 
            // sbtnAcquireOnce
            // 
            this.sbtnAcquireOnce.Location = new System.Drawing.Point(174, 716);
            this.sbtnAcquireOnce.Name = "sbtnAcquireOnce";
            this.sbtnAcquireOnce.Size = new System.Drawing.Size(153, 27);
            this.sbtnAcquireOnce.StyleController = this.layoutControlRoot;
            this.sbtnAcquireOnce.TabIndex = 9;
            this.sbtnAcquireOnce.Tag = "SBTN_ACQUIREONCE";
            this.sbtnAcquireOnce.Text = "单次采集";
            // 
            // sbtnSetCamera
            // 
            this.sbtnSetCamera.Location = new System.Drawing.Point(174, 683);
            this.sbtnSetCamera.Name = "sbtnSetCamera";
            this.sbtnSetCamera.Size = new System.Drawing.Size(153, 27);
            this.sbtnSetCamera.StyleController = this.layoutControlRoot;
            this.sbtnSetCamera.TabIndex = 8;
            this.sbtnSetCamera.Tag = "SBTN_SETCAMERA";
            this.sbtnSetCamera.Text = "设置相机";
            // 
            // sbtnStopAcquire
            // 
            this.sbtnStopAcquire.Location = new System.Drawing.Point(16, 716);
            this.sbtnStopAcquire.Name = "sbtnStopAcquire";
            this.sbtnStopAcquire.Size = new System.Drawing.Size(152, 27);
            this.sbtnStopAcquire.StyleController = this.layoutControlRoot;
            this.sbtnStopAcquire.TabIndex = 7;
            this.sbtnStopAcquire.Tag = "SBTN_STOPACQUIRE";
            this.sbtnStopAcquire.Text = "停止采集";
            // 
            // sbtnAcquireContinuous
            // 
            this.sbtnAcquireContinuous.Location = new System.Drawing.Point(16, 683);
            this.sbtnAcquireContinuous.Name = "sbtnAcquireContinuous";
            this.sbtnAcquireContinuous.Size = new System.Drawing.Size(152, 27);
            this.sbtnAcquireContinuous.StyleController = this.layoutControlRoot;
            this.sbtnAcquireContinuous.TabIndex = 6;
            this.sbtnAcquireContinuous.Tag = "SBTN_ACQUIRECONTINUOUS";
            this.sbtnAcquireContinuous.Text = "连续采集";
            // 
            // xtbcFunction
            // 
            this.xtbcFunction.Location = new System.Drawing.Point(673, 16);
            this.xtbcFunction.Name = "xtbcFunction";
            this.xtbcFunction.SelectedTabPage = this.xtbpPreprocess;
            this.xtbcFunction.Size = new System.Drawing.Size(392, 694);
            this.xtbcFunction.TabIndex = 5;
            this.xtbcFunction.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtbpPreprocess,
            this.xtbpProcessParameter,
            this.xtbpInteractiveCommunication});
            // 
            // xtbpPreprocess
            // 
            this.xtbpPreprocess.Name = "xtbpPreprocess";
            this.xtbpPreprocess.Size = new System.Drawing.Size(385, 658);
            this.xtbpPreprocess.Tag = "XTBP_PREPROCESS";
            this.xtbpPreprocess.Text = "图像预处理";
            // 
            // xtbpProcessParameter
            // 
            this.xtbpProcessParameter.Controls.Add(this.lblCoordinateMode);
            this.xtbpProcessParameter.Controls.Add(this.chkeCameraCenterReference);
            this.xtbpProcessParameter.Controls.Add(this.chkeModelReference);
            this.xtbpProcessParameter.Controls.Add(this.speColUnit);
            this.xtbpProcessParameter.Controls.Add(this.speRowUnit);
            this.xtbpProcessParameter.Controls.Add(this.speGamma);
            this.xtbpProcessParameter.Controls.Add(this.speDebouncerTime);
            this.xtbpProcessParameter.Controls.Add(this.speTriggerDelay);
            this.xtbpProcessParameter.Controls.Add(this.speGain);
            this.xtbpProcessParameter.Controls.Add(this.speExposureTime);
            this.xtbpProcessParameter.Controls.Add(this.lblColUnit);
            this.xtbpProcessParameter.Controls.Add(this.lblRowUnit);
            this.xtbpProcessParameter.Controls.Add(this.lblcGamma);
            this.xtbpProcessParameter.Controls.Add(this.lblcDebouncerTime);
            this.xtbpProcessParameter.Controls.Add(this.lblcTriggerDelay);
            this.xtbpProcessParameter.Controls.Add(this.lblcGain);
            this.xtbpProcessParameter.Controls.Add(this.lblExposureTime);
            this.xtbpProcessParameter.Controls.Add(this.chkeEnableAlgorithm);
            this.xtbpProcessParameter.Controls.Add(this.chkeSaveNG);
            this.xtbpProcessParameter.Controls.Add(this.chkeSaveOK);
            this.xtbpProcessParameter.Controls.Add(this.chkeSaveAll);
            this.xtbpProcessParameter.Controls.Add(this.sbtnSetMatchModel);
            this.xtbpProcessParameter.Name = "xtbpProcessParameter";
            this.xtbpProcessParameter.Size = new System.Drawing.Size(385, 658);
            this.xtbpProcessParameter.Tag = "XTBP_PROCESSPARAMETER";
            this.xtbpProcessParameter.Text = "处理参数";
            // 
            // speColUnit
            // 
            this.speColUnit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speColUnit.Location = new System.Drawing.Point(175, 563);
            this.speColUnit.Name = "speColUnit";
            this.speColUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.speColUnit.Properties.DisplayFormat.FormatString = "F3";
            this.speColUnit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speColUnit.Properties.EditFormat.FormatString = "F3";
            this.speColUnit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speColUnit.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.speColUnit.Properties.MaxValue = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.speColUnit.Size = new System.Drawing.Size(101, 24);
            this.speColUnit.TabIndex = 3;
            this.speColUnit.Tag = "SPE_COLUNIT";
            // 
            // speRowUnit
            // 
            this.speRowUnit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speRowUnit.Location = new System.Drawing.Point(175, 518);
            this.speRowUnit.Name = "speRowUnit";
            this.speRowUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.speRowUnit.Properties.DisplayFormat.FormatString = "F3";
            this.speRowUnit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speRowUnit.Properties.EditFormat.FormatString = "F3";
            this.speRowUnit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speRowUnit.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.speRowUnit.Properties.MaxValue = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.speRowUnit.Size = new System.Drawing.Size(101, 24);
            this.speRowUnit.TabIndex = 3;
            this.speRowUnit.Tag = "SPE_ROWUNIT";
            // 
            // speGamma
            // 
            this.speGamma.EditValue = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.speGamma.Location = new System.Drawing.Point(175, 468);
            this.speGamma.Name = "speGamma";
            this.speGamma.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.speGamma.Properties.DisplayFormat.FormatString = "F2";
            this.speGamma.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speGamma.Properties.EditFormat.FormatString = "F2";
            this.speGamma.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speGamma.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.speGamma.Properties.MaxValue = new decimal(new int[] {
            40,
            0,
            0,
            65536});
            this.speGamma.Properties.MinValue = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            this.speGamma.Size = new System.Drawing.Size(101, 24);
            this.speGamma.TabIndex = 3;
            this.speGamma.Tag = "SPE_GAMMA";
            // 
            // speDebouncerTime
            // 
            this.speDebouncerTime.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speDebouncerTime.Location = new System.Drawing.Point(175, 420);
            this.speDebouncerTime.Name = "speDebouncerTime";
            this.speDebouncerTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.speDebouncerTime.Properties.DisplayFormat.FormatString = "F2";
            this.speDebouncerTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speDebouncerTime.Properties.EditFormat.FormatString = "F2";
            this.speDebouncerTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speDebouncerTime.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.speDebouncerTime.Properties.MaxValue = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.speDebouncerTime.Size = new System.Drawing.Size(101, 24);
            this.speDebouncerTime.TabIndex = 3;
            this.speDebouncerTime.Tag = "SPE_DEBOUNCETIME";
            // 
            // speTriggerDelay
            // 
            this.speTriggerDelay.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speTriggerDelay.Location = new System.Drawing.Point(175, 372);
            this.speTriggerDelay.Name = "speTriggerDelay";
            this.speTriggerDelay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.speTriggerDelay.Properties.DisplayFormat.FormatString = "F2";
            this.speTriggerDelay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speTriggerDelay.Properties.EditFormat.FormatString = "F2";
            this.speTriggerDelay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speTriggerDelay.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.speTriggerDelay.Properties.MaxValue = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.speTriggerDelay.Size = new System.Drawing.Size(101, 24);
            this.speTriggerDelay.TabIndex = 3;
            this.speTriggerDelay.Tag = "SPE_TRIGGERDELAY";
            // 
            // speGain
            // 
            this.speGain.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speGain.Location = new System.Drawing.Point(175, 324);
            this.speGain.Name = "speGain";
            this.speGain.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.speGain.Properties.DisplayFormat.FormatString = "F2";
            this.speGain.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speGain.Properties.EditFormat.FormatString = "F2";
            this.speGain.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speGain.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.speGain.Properties.MaxValue = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.speGain.Size = new System.Drawing.Size(101, 24);
            this.speGain.TabIndex = 3;
            this.speGain.Tag = "SPE_GAIN";
            // 
            // speExposureTime
            // 
            this.speExposureTime.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speExposureTime.Location = new System.Drawing.Point(175, 276);
            this.speExposureTime.MenuManager = this.bmgrMain;
            this.speExposureTime.Name = "speExposureTime";
            this.speExposureTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.speExposureTime.Properties.DisplayFormat.FormatString = "F2";
            this.speExposureTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speExposureTime.Properties.EditFormat.FormatString = "F2";
            this.speExposureTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speExposureTime.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.speExposureTime.Properties.MaxValue = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.speExposureTime.Size = new System.Drawing.Size(101, 24);
            this.speExposureTime.TabIndex = 3;
            this.speExposureTime.Tag = "SPE_EXPOSURETIME";
            // 
            // bmgrMain
            // 
            this.bmgrMain.DockControls.Add(this.barDockControlTop);
            this.bmgrMain.DockControls.Add(this.barDockControlBottom);
            this.bmgrMain.DockControls.Add(this.barDockControlLeft);
            this.bmgrMain.DockControls.Add(this.barDockControlRight);
            this.bmgrMain.Form = this;
            this.bmgrMain.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiRectangle1,
            this.bbiRectangle2,
            this.bbiCircle,
            this.bbiCenterCross,
            this.bbiClearIconic});
            this.bmgrMain.MaxItemId = 5;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.bmgrMain;
            this.barDockControlTop.Size = new System.Drawing.Size(1081, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 759);
            this.barDockControlBottom.Manager = this.bmgrMain;
            this.barDockControlBottom.Size = new System.Drawing.Size(1081, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.bmgrMain;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 759);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1081, 0);
            this.barDockControlRight.Manager = this.bmgrMain;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 759);
            // 
            // bbiRectangle1
            // 
            this.bbiRectangle1.Caption = "齐轴矩形";
            this.bbiRectangle1.Id = 0;
            this.bbiRectangle1.Name = "bbiRectangle1";
            this.bbiRectangle1.Tag = "BBI_RECTANGLE1";
            // 
            // bbiRectangle2
            // 
            this.bbiRectangle2.Caption = "仿射矩形";
            this.bbiRectangle2.Id = 1;
            this.bbiRectangle2.Name = "bbiRectangle2";
            this.bbiRectangle2.Tag = "BBI_RECTANGLE2";
            // 
            // bbiCircle
            // 
            this.bbiCircle.Caption = "闭合圆形";
            this.bbiCircle.Id = 2;
            this.bbiCircle.Name = "bbiCircle";
            this.bbiCircle.Tag = "BBI_CIRCLE";
            // 
            // bbiCenterCross
            // 
            this.bbiCenterCross.Caption = "中心十字";
            this.bbiCenterCross.Id = 3;
            this.bbiCenterCross.Name = "bbiCenterCross";
            this.bbiCenterCross.Tag = "BBI_CENTERCROSS";
            // 
            // bbiClearIconic
            // 
            this.bbiClearIconic.Caption = "清空图形";
            this.bbiClearIconic.Id = 4;
            this.bbiClearIconic.Name = "bbiClearIconic";
            this.bbiClearIconic.Tag = "BBI_CLEARICONIC";
            // 
            // hwndctrlDisplay
            // 
            this.hwndctrlDisplay.BackColor = System.Drawing.Color.Black;
            this.hwndctrlDisplay.BorderColor = System.Drawing.Color.Black;
            this.hwndctrlDisplay.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hwndctrlDisplay.Location = new System.Drawing.Point(16, 47);
            this.hwndctrlDisplay.Name = "hwndctrlDisplay";
            this.bmgrMain.SetPopupContextMenu(this.hwndctrlDisplay, this.ppmMain);
            this.hwndctrlDisplay.Size = new System.Drawing.Size(645, 630);
            this.hwndctrlDisplay.TabIndex = 4;
            this.hwndctrlDisplay.WindowSize = new System.Drawing.Size(645, 630);
            // 
            // ppmMain
            // 
            this.ppmMain.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiRectangle1),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiRectangle2),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiCircle),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiCenterCross),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiClearIconic)});
            this.ppmMain.Manager = this.bmgrMain;
            this.ppmMain.Name = "ppmMain";
            // 
            // lblColUnit
            // 
            this.lblColUnit.Location = new System.Drawing.Point(48, 566);
            this.lblColUnit.Name = "lblColUnit";
            this.lblColUnit.Size = new System.Drawing.Size(49, 18);
            this.lblColUnit.TabIndex = 2;
            this.lblColUnit.Text = "Col系数";
            // 
            // lblRowUnit
            // 
            this.lblRowUnit.Location = new System.Drawing.Point(48, 521);
            this.lblRowUnit.Name = "lblRowUnit";
            this.lblRowUnit.Size = new System.Drawing.Size(57, 18);
            this.lblRowUnit.TabIndex = 2;
            this.lblRowUnit.Text = "Row系数";
            // 
            // lblcGamma
            // 
            this.lblcGamma.Location = new System.Drawing.Point(48, 471);
            this.lblcGamma.Name = "lblcGamma";
            this.lblcGamma.Size = new System.Drawing.Size(60, 18);
            this.lblcGamma.TabIndex = 2;
            this.lblcGamma.Text = "相机伽马";
            // 
            // lblcDebouncerTime
            // 
            this.lblcDebouncerTime.Location = new System.Drawing.Point(48, 423);
            this.lblcDebouncerTime.Name = "lblcDebouncerTime";
            this.lblcDebouncerTime.Size = new System.Drawing.Size(87, 18);
            this.lblcDebouncerTime.TabIndex = 2;
            this.lblcDebouncerTime.Text = "消抖时间(us)";
            // 
            // lblcTriggerDelay
            // 
            this.lblcTriggerDelay.Location = new System.Drawing.Point(48, 375);
            this.lblcTriggerDelay.Name = "lblcTriggerDelay";
            this.lblcTriggerDelay.Size = new System.Drawing.Size(92, 18);
            this.lblcTriggerDelay.TabIndex = 2;
            this.lblcTriggerDelay.Text = "触发延时(ms)";
            // 
            // lblcGain
            // 
            this.lblcGain.Location = new System.Drawing.Point(48, 327);
            this.lblcGain.Name = "lblcGain";
            this.lblcGain.Size = new System.Drawing.Size(60, 18);
            this.lblcGain.TabIndex = 2;
            this.lblcGain.Text = "相机增益";
            // 
            // lblExposureTime
            // 
            this.lblExposureTime.Location = new System.Drawing.Point(48, 279);
            this.lblExposureTime.Name = "lblExposureTime";
            this.lblExposureTime.Size = new System.Drawing.Size(92, 18);
            this.lblExposureTime.TabIndex = 2;
            this.lblExposureTime.Text = "曝光时间(ms)";
            // 
            // chkeEnableAlgorithm
            // 
            this.chkeEnableAlgorithm.Location = new System.Drawing.Point(48, 219);
            this.chkeEnableAlgorithm.Name = "chkeEnableAlgorithm";
            this.chkeEnableAlgorithm.Properties.Caption = "启用算法";
            this.chkeEnableAlgorithm.Size = new System.Drawing.Size(89, 22);
            this.chkeEnableAlgorithm.TabIndex = 1;
            this.chkeEnableAlgorithm.Tag = "CHKE_ENABLEALGORITHM";
            // 
            // chkeSaveNG
            // 
            this.chkeSaveNG.Location = new System.Drawing.Point(48, 176);
            this.chkeSaveNG.Name = "chkeSaveNG";
            this.chkeSaveNG.Properties.Caption = "保存NG";
            this.chkeSaveNG.Size = new System.Drawing.Size(89, 22);
            this.chkeSaveNG.TabIndex = 1;
            this.chkeSaveNG.Tag = "CHKE_SAVENG";
            // 
            // chkeSaveOK
            // 
            this.chkeSaveOK.Location = new System.Drawing.Point(48, 134);
            this.chkeSaveOK.Name = "chkeSaveOK";
            this.chkeSaveOK.Properties.Caption = "保存OK";
            this.chkeSaveOK.Size = new System.Drawing.Size(89, 22);
            this.chkeSaveOK.TabIndex = 1;
            this.chkeSaveOK.Tag = "CHKE_SAVEOK";
            // 
            // chkeSaveAll
            // 
            this.chkeSaveAll.Location = new System.Drawing.Point(48, 93);
            this.chkeSaveAll.MenuManager = this.bmgrMain;
            this.chkeSaveAll.Name = "chkeSaveAll";
            this.chkeSaveAll.Properties.Caption = "保存All";
            this.chkeSaveAll.Size = new System.Drawing.Size(89, 22);
            this.chkeSaveAll.TabIndex = 1;
            this.chkeSaveAll.Tag = "CHKE_SAVEALL";
            // 
            // sbtnSetMatchModel
            // 
            this.sbtnSetMatchModel.Location = new System.Drawing.Point(175, 208);
            this.sbtnSetMatchModel.Name = "sbtnSetMatchModel";
            this.sbtnSetMatchModel.Size = new System.Drawing.Size(111, 33);
            this.sbtnSetMatchModel.TabIndex = 0;
            this.sbtnSetMatchModel.Tag = "SBTN_SETMATCHMODEL";
            this.sbtnSetMatchModel.Text = "设置匹配模型";
            // 
            // xtbpInteractiveCommunication
            // 
            this.xtbpInteractiveCommunication.Controls.Add(this.speFlagMembrane2);
            this.xtbpInteractiveCommunication.Controls.Add(this.speFlagMembrane1);
            this.xtbpInteractiveCommunication.Controls.Add(this.speFlagGlass);
            this.xtbpInteractiveCommunication.Controls.Add(this.sbtnWriteDataMembrane2);
            this.xtbpInteractiveCommunication.Controls.Add(this.sbtnWriteDataMembrane1);
            this.xtbpInteractiveCommunication.Controls.Add(this.sbtnWriteDataGlass);
            this.xtbpInteractiveCommunication.Controls.Add(this.speCoordRMembrane2);
            this.xtbpInteractiveCommunication.Controls.Add(this.speCoordYMembrane2);
            this.xtbpInteractiveCommunication.Controls.Add(this.speCoordRMembrane1);
            this.xtbpInteractiveCommunication.Controls.Add(this.speCoordYMembrane1);
            this.xtbpInteractiveCommunication.Controls.Add(this.speCoordXMembrane2);
            this.xtbpInteractiveCommunication.Controls.Add(this.speCoordRGlass);
            this.xtbpInteractiveCommunication.Controls.Add(this.speCoordXMembrane1);
            this.xtbpInteractiveCommunication.Controls.Add(this.speCoordYGlass);
            this.xtbpInteractiveCommunication.Controls.Add(this.speCoordXGlass);
            this.xtbpInteractiveCommunication.Controls.Add(this.lblcFlagPrompt);
            this.xtbpInteractiveCommunication.Controls.Add(this.lblcCoordRPrompt);
            this.xtbpInteractiveCommunication.Controls.Add(this.lblcCoordYPrompt);
            this.xtbpInteractiveCommunication.Controls.Add(this.lblcCoordXPrompt);
            this.xtbpInteractiveCommunication.Controls.Add(this.lblcMembrane2Prompt);
            this.xtbpInteractiveCommunication.Controls.Add(this.lblcMembrane1Prompt);
            this.xtbpInteractiveCommunication.Controls.Add(this.lblcGlassPrompt);
            this.xtbpInteractiveCommunication.Name = "xtbpInteractiveCommunication";
            this.xtbpInteractiveCommunication.Size = new System.Drawing.Size(385, 658);
            this.xtbpInteractiveCommunication.Tag = "XTBP_INTERACTIVECOMMINICATION";
            this.xtbpInteractiveCommunication.Text = "交互通信";
            // 
            // speFlagMembrane2
            // 
            this.speFlagMembrane2.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speFlagMembrane2.Location = new System.Drawing.Point(259, 229);
            this.speFlagMembrane2.Name = "speFlagMembrane2";
            this.speFlagMembrane2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.speFlagMembrane2.Size = new System.Drawing.Size(95, 24);
            this.speFlagMembrane2.TabIndex = 4;
            // 
            // speFlagMembrane1
            // 
            this.speFlagMembrane1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speFlagMembrane1.Location = new System.Drawing.Point(152, 229);
            this.speFlagMembrane1.Name = "speFlagMembrane1";
            this.speFlagMembrane1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.speFlagMembrane1.Size = new System.Drawing.Size(95, 24);
            this.speFlagMembrane1.TabIndex = 4;
            // 
            // speFlagGlass
            // 
            this.speFlagGlass.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speFlagGlass.Location = new System.Drawing.Point(45, 229);
            this.speFlagGlass.MenuManager = this.bmgrMain;
            this.speFlagGlass.Name = "speFlagGlass";
            this.speFlagGlass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.speFlagGlass.Size = new System.Drawing.Size(95, 24);
            this.speFlagGlass.TabIndex = 4;
            // 
            // sbtnWriteDataMembrane2
            // 
            this.sbtnWriteDataMembrane2.Location = new System.Drawing.Point(259, 279);
            this.sbtnWriteDataMembrane2.Name = "sbtnWriteDataMembrane2";
            this.sbtnWriteDataMembrane2.Size = new System.Drawing.Size(95, 30);
            this.sbtnWriteDataMembrane2.TabIndex = 3;
            this.sbtnWriteDataMembrane2.Tag = "SBTN_WRITEDATAMEMBRANE2";
            this.sbtnWriteDataMembrane2.Text = "写入数据";
            // 
            // sbtnWriteDataMembrane1
            // 
            this.sbtnWriteDataMembrane1.Location = new System.Drawing.Point(152, 279);
            this.sbtnWriteDataMembrane1.Name = "sbtnWriteDataMembrane1";
            this.sbtnWriteDataMembrane1.Size = new System.Drawing.Size(95, 30);
            this.sbtnWriteDataMembrane1.TabIndex = 3;
            this.sbtnWriteDataMembrane1.Tag = "SBTN_WRITEDATAMEMBRANE1";
            this.sbtnWriteDataMembrane1.Text = "写入数据";
            // 
            // sbtnWriteDataGlass
            // 
            this.sbtnWriteDataGlass.Location = new System.Drawing.Point(45, 279);
            this.sbtnWriteDataGlass.Name = "sbtnWriteDataGlass";
            this.sbtnWriteDataGlass.Size = new System.Drawing.Size(95, 30);
            this.sbtnWriteDataGlass.TabIndex = 3;
            this.sbtnWriteDataGlass.Tag = "SBTN_WRITEDATAGLASS";
            this.sbtnWriteDataGlass.Text = "写入数据";
            // 
            // speCoordRMembrane2
            // 
            this.speCoordRMembrane2.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speCoordRMembrane2.Location = new System.Drawing.Point(259, 183);
            this.speCoordRMembrane2.Name = "speCoordRMembrane2";
            this.speCoordRMembrane2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.speCoordRMembrane2.Properties.DisplayFormat.FormatString = "F2";
            this.speCoordRMembrane2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speCoordRMembrane2.Properties.EditFormat.FormatString = "F2";
            this.speCoordRMembrane2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speCoordRMembrane2.Size = new System.Drawing.Size(95, 24);
            this.speCoordRMembrane2.TabIndex = 2;
            // 
            // speCoordYMembrane2
            // 
            this.speCoordYMembrane2.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speCoordYMembrane2.Location = new System.Drawing.Point(259, 129);
            this.speCoordYMembrane2.Name = "speCoordYMembrane2";
            this.speCoordYMembrane2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.speCoordYMembrane2.Properties.DisplayFormat.FormatString = "F2";
            this.speCoordYMembrane2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speCoordYMembrane2.Properties.EditFormat.FormatString = "F2";
            this.speCoordYMembrane2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speCoordYMembrane2.Size = new System.Drawing.Size(95, 24);
            this.speCoordYMembrane2.TabIndex = 2;
            // 
            // speCoordRMembrane1
            // 
            this.speCoordRMembrane1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speCoordRMembrane1.Location = new System.Drawing.Point(152, 183);
            this.speCoordRMembrane1.Name = "speCoordRMembrane1";
            this.speCoordRMembrane1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.speCoordRMembrane1.Properties.DisplayFormat.FormatString = "F2";
            this.speCoordRMembrane1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speCoordRMembrane1.Properties.EditFormat.FormatString = "F2";
            this.speCoordRMembrane1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speCoordRMembrane1.Size = new System.Drawing.Size(95, 24);
            this.speCoordRMembrane1.TabIndex = 2;
            // 
            // speCoordYMembrane1
            // 
            this.speCoordYMembrane1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speCoordYMembrane1.Location = new System.Drawing.Point(152, 129);
            this.speCoordYMembrane1.Name = "speCoordYMembrane1";
            this.speCoordYMembrane1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.speCoordYMembrane1.Properties.DisplayFormat.FormatString = "F2";
            this.speCoordYMembrane1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speCoordYMembrane1.Properties.EditFormat.FormatString = "F2";
            this.speCoordYMembrane1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speCoordYMembrane1.Size = new System.Drawing.Size(95, 24);
            this.speCoordYMembrane1.TabIndex = 2;
            // 
            // speCoordXMembrane2
            // 
            this.speCoordXMembrane2.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speCoordXMembrane2.Location = new System.Drawing.Point(259, 76);
            this.speCoordXMembrane2.Name = "speCoordXMembrane2";
            this.speCoordXMembrane2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.speCoordXMembrane2.Properties.DisplayFormat.FormatString = "F2";
            this.speCoordXMembrane2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speCoordXMembrane2.Properties.EditFormat.FormatString = "F2";
            this.speCoordXMembrane2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speCoordXMembrane2.Size = new System.Drawing.Size(95, 24);
            this.speCoordXMembrane2.TabIndex = 2;
            // 
            // speCoordRGlass
            // 
            this.speCoordRGlass.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speCoordRGlass.Location = new System.Drawing.Point(45, 183);
            this.speCoordRGlass.Name = "speCoordRGlass";
            this.speCoordRGlass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.speCoordRGlass.Properties.DisplayFormat.FormatString = "F2";
            this.speCoordRGlass.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speCoordRGlass.Properties.EditFormat.FormatString = "F2";
            this.speCoordRGlass.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speCoordRGlass.Size = new System.Drawing.Size(95, 24);
            this.speCoordRGlass.TabIndex = 2;
            // 
            // speCoordXMembrane1
            // 
            this.speCoordXMembrane1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speCoordXMembrane1.Location = new System.Drawing.Point(152, 76);
            this.speCoordXMembrane1.Name = "speCoordXMembrane1";
            this.speCoordXMembrane1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.speCoordXMembrane1.Properties.DisplayFormat.FormatString = "F2";
            this.speCoordXMembrane1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speCoordXMembrane1.Properties.EditFormat.FormatString = "F2";
            this.speCoordXMembrane1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speCoordXMembrane1.Size = new System.Drawing.Size(95, 24);
            this.speCoordXMembrane1.TabIndex = 2;
            // 
            // speCoordYGlass
            // 
            this.speCoordYGlass.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speCoordYGlass.Location = new System.Drawing.Point(45, 129);
            this.speCoordYGlass.Name = "speCoordYGlass";
            this.speCoordYGlass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.speCoordYGlass.Properties.DisplayFormat.FormatString = "F2";
            this.speCoordYGlass.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speCoordYGlass.Properties.EditFormat.FormatString = "F2";
            this.speCoordYGlass.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speCoordYGlass.Size = new System.Drawing.Size(95, 24);
            this.speCoordYGlass.TabIndex = 2;
            // 
            // speCoordXGlass
            // 
            this.speCoordXGlass.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speCoordXGlass.Location = new System.Drawing.Point(45, 76);
            this.speCoordXGlass.MenuManager = this.bmgrMain;
            this.speCoordXGlass.Name = "speCoordXGlass";
            this.speCoordXGlass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.speCoordXGlass.Properties.DisplayFormat.FormatString = "F2";
            this.speCoordXGlass.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speCoordXGlass.Properties.EditFormat.FormatString = "F2";
            this.speCoordXGlass.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.speCoordXGlass.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.speCoordXGlass.Size = new System.Drawing.Size(95, 24);
            this.speCoordXGlass.TabIndex = 2;
            // 
            // lblcFlagPrompt
            // 
            this.lblcFlagPrompt.Location = new System.Drawing.Point(15, 235);
            this.lblcFlagPrompt.Name = "lblcFlagPrompt";
            this.lblcFlagPrompt.Size = new System.Drawing.Size(13, 18);
            this.lblcFlagPrompt.TabIndex = 1;
            this.lblcFlagPrompt.Tag = "LBLC_FLAGPROMPT";
            this.lblcFlagPrompt.Text = "F:";
            // 
            // lblcCoordRPrompt
            // 
            this.lblcCoordRPrompt.Location = new System.Drawing.Point(15, 184);
            this.lblcCoordRPrompt.Name = "lblcCoordRPrompt";
            this.lblcCoordRPrompt.Size = new System.Drawing.Size(14, 18);
            this.lblcCoordRPrompt.TabIndex = 1;
            this.lblcCoordRPrompt.Tag = "LBLC_COORDRPROMPT";
            this.lblcCoordRPrompt.Text = "R:";
            // 
            // lblcCoordYPrompt
            // 
            this.lblcCoordYPrompt.Location = new System.Drawing.Point(16, 132);
            this.lblcCoordYPrompt.Name = "lblcCoordYPrompt";
            this.lblcCoordYPrompt.Size = new System.Drawing.Size(15, 18);
            this.lblcCoordYPrompt.TabIndex = 1;
            this.lblcCoordYPrompt.Tag = "LBLC_COORDYPROMPT";
            this.lblcCoordYPrompt.Text = "Y:";
            // 
            // lblcCoordXPrompt
            // 
            this.lblcCoordXPrompt.Location = new System.Drawing.Point(16, 82);
            this.lblcCoordXPrompt.Name = "lblcCoordXPrompt";
            this.lblcCoordXPrompt.Size = new System.Drawing.Size(14, 18);
            this.lblcCoordXPrompt.TabIndex = 1;
            this.lblcCoordXPrompt.Tag = "LBLC_COORDXPROMPT";
            this.lblcCoordXPrompt.Text = "X:";
            // 
            // lblcMembrane2Prompt
            // 
            this.lblcMembrane2Prompt.Location = new System.Drawing.Point(259, 30);
            this.lblcMembrane2Prompt.Name = "lblcMembrane2Prompt";
            this.lblcMembrane2Prompt.Size = new System.Drawing.Size(60, 18);
            this.lblcMembrane2Prompt.TabIndex = 0;
            this.lblcMembrane2Prompt.Tag = "LBLC_MEMBRANE2PROMPT";
            this.lblcMembrane2Prompt.Text = "膜右工位";
            // 
            // lblcMembrane1Prompt
            // 
            this.lblcMembrane1Prompt.Location = new System.Drawing.Point(152, 30);
            this.lblcMembrane1Prompt.Name = "lblcMembrane1Prompt";
            this.lblcMembrane1Prompt.Size = new System.Drawing.Size(60, 18);
            this.lblcMembrane1Prompt.TabIndex = 0;
            this.lblcMembrane1Prompt.Tag = "LBLC_MEMBRANE1PROMPT";
            this.lblcMembrane1Prompt.Text = "膜左工位";
            // 
            // lblcGlassPrompt
            // 
            this.lblcGlassPrompt.Location = new System.Drawing.Point(45, 30);
            this.lblcGlassPrompt.Name = "lblcGlassPrompt";
            this.lblcGlassPrompt.Size = new System.Drawing.Size(60, 18);
            this.lblcGlassPrompt.TabIndex = 0;
            this.lblcGlassPrompt.Tag = "LBLC_GLASSPROMPT";
            this.lblcGlassPrompt.Text = "玻璃工位";
            // 
            // layoutControlGroupRoot
            // 
            this.layoutControlGroupRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupRoot.GroupBordersVisible = false;
            this.layoutControlGroupRoot.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.splitterItem1,
            this.layoutControlItem4,
            this.layoutControlItem3,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem11,
            this.layoutControlItem12,
            this.layoutControlItem13});
            this.layoutControlGroupRoot.Name = "Root";
            this.layoutControlGroupRoot.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroupRoot.Size = new System.Drawing.Size(1081, 759);
            this.layoutControlGroupRoot.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.hwndctrlDisplay;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 31);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(651, 636);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.xtbcFunction;
            this.layoutControlItem2.Location = new System.Drawing.Point(657, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(398, 700);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(651, 0);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(6, 700);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.sbtnStopAcquire;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 700);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(158, 33);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sbtnAcquireContinuous;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 667);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(158, 33);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.sbtnSetCamera;
            this.layoutControlItem5.Location = new System.Drawing.Point(158, 667);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(159, 33);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.sbtnAcquireOnce;
            this.layoutControlItem6.Location = new System.Drawing.Point(158, 700);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(159, 33);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.sbtnLoadImage;
            this.layoutControlItem7.Location = new System.Drawing.Point(317, 667);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(158, 33);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.sbtnTestOffline;
            this.layoutControlItem8.Location = new System.Drawing.Point(317, 700);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(157, 33);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.sbtnLastImage;
            this.layoutControlItem9.Location = new System.Drawing.Point(475, 667);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(176, 33);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.sbtnNextImage;
            this.layoutControlItem10.Location = new System.Drawing.Point(474, 700);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(183, 33);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.tlstrpStatus;
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(651, 31);
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.sbtnSaveParameter;
            this.layoutControlItem12.Location = new System.Drawing.Point(657, 700);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(176, 33);
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.sbtnExitSet;
            this.layoutControlItem13.Location = new System.Drawing.Point(833, 700);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(222, 33);
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextVisible = false;
            // 
            // chkeModelReference
            // 
            this.chkeModelReference.Location = new System.Drawing.Point(175, 133);
            this.chkeModelReference.MenuManager = this.bmgrMain;
            this.chkeModelReference.Name = "chkeModelReference";
            this.chkeModelReference.Properties.Caption = "参考模板";
            this.chkeModelReference.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.chkeModelReference.Properties.RadioGroupIndex = 1;
            this.chkeModelReference.Size = new System.Drawing.Size(111, 22);
            this.chkeModelReference.TabIndex = 4;
            this.chkeModelReference.Tag = "CHKE_MODELREFERENCE";
            // 
            // chkeCameraCenterReference
            // 
            this.chkeCameraCenterReference.Location = new System.Drawing.Point(175, 176);
            this.chkeCameraCenterReference.Name = "chkeCameraCenterReference";
            this.chkeCameraCenterReference.Properties.Caption = "相机中心";
            this.chkeCameraCenterReference.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.chkeCameraCenterReference.Properties.RadioGroupIndex = 1;
            this.chkeCameraCenterReference.Size = new System.Drawing.Size(111, 22);
            this.chkeCameraCenterReference.TabIndex = 4;
            this.chkeCameraCenterReference.TabStop = false;
            this.chkeCameraCenterReference.Tag = "CHKE_CAMERACENTERREFERENCE";
            // 
            // lblCoordinateMode
            // 
            this.lblCoordinateMode.Location = new System.Drawing.Point(175, 95);
            this.lblCoordinateMode.Name = "lblCoordinateMode";
            this.lblCoordinateMode.Size = new System.Drawing.Size(90, 18);
            this.lblCoordinateMode.TabIndex = 5;
            this.lblCoordinateMode.Text = "位置坐标模式";
            // 
            // CameraStationSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControlRoot);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "CameraStationSet";
            this.Size = new System.Drawing.Size(1081, 759);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlRoot)).EndInit();
            this.layoutControlRoot.ResumeLayout(false);
            this.tlstrpStatus.ResumeLayout(false);
            this.tlstrpStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtbcFunction)).EndInit();
            this.xtbcFunction.ResumeLayout(false);
            this.xtbpProcessParameter.ResumeLayout(false);
            this.xtbpProcessParameter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speColUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speRowUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speGamma.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speDebouncerTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speTriggerDelay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speGain.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speExposureTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bmgrMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppmMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkeEnableAlgorithm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkeSaveNG.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkeSaveOK.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkeSaveAll.Properties)).EndInit();
            this.xtbpInteractiveCommunication.ResumeLayout(false);
            this.xtbpInteractiveCommunication.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speFlagMembrane2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speFlagMembrane1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speFlagGlass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speCoordRMembrane2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speCoordYMembrane2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speCoordRMembrane1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speCoordYMembrane1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speCoordXMembrane2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speCoordRGlass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speCoordXMembrane1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speCoordYGlass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speCoordXGlass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkeModelReference.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkeCameraCenterReference.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlRoot;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRoot;
        private HalconDotNet.HWindowControl hwndctrlDisplay;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraTab.XtraTabControl xtbcFunction;
        private DevExpress.XtraTab.XtraTabPage xtbpPreprocess;
        private DevExpress.XtraTab.XtraTabPage xtbpProcessParameter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton sbtnNextImage;
        private DevExpress.XtraEditors.SimpleButton sbtnLastImage;
        private DevExpress.XtraEditors.SimpleButton sbtnTestOffline;
        private DevExpress.XtraEditors.SimpleButton sbtnLoadImage;
        private DevExpress.XtraEditors.SimpleButton sbtnAcquireOnce;
        private DevExpress.XtraEditors.SimpleButton sbtnSetCamera;
        private DevExpress.XtraEditors.SimpleButton sbtnStopAcquire;
        private DevExpress.XtraEditors.SimpleButton sbtnAcquireContinuous;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private System.Windows.Forms.ToolStrip tlstrpStatus;
        private System.Windows.Forms.ToolStripLabel tlstrplblCoordinatePrompt;
        private System.Windows.Forms.ToolStripTextBox tlstrptxtCoordinate;
        private System.Windows.Forms.ToolStripLabel tlstrplblGrayValuePrompt;
        private System.Windows.Forms.ToolStripTextBox tlstrptxtGrayValue;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraBars.BarManager bmgrMain;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.PopupMenu ppmMain;
        private DevExpress.XtraTab.XtraTabPage xtbpInteractiveCommunication;
        private DevExpress.XtraEditors.SimpleButton sbtnSetMatchModel;
        private DevExpress.XtraEditors.CheckEdit chkeEnableAlgorithm;
        private DevExpress.XtraEditors.CheckEdit chkeSaveNG;
        private DevExpress.XtraEditors.CheckEdit chkeSaveOK;
        private DevExpress.XtraEditors.CheckEdit chkeSaveAll;
        private DevExpress.XtraEditors.SimpleButton sbtnExitSet;
        private DevExpress.XtraEditors.SimpleButton sbtnSaveParameter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraBars.BarButtonItem bbiRectangle1;
        private DevExpress.XtraBars.BarButtonItem bbiRectangle2;
        private DevExpress.XtraBars.BarButtonItem bbiCircle;
        private DevExpress.XtraBars.BarButtonItem bbiCenterCross;
        private DevExpress.XtraBars.BarButtonItem bbiClearIconic;
        private System.Windows.Forms.ToolStripTextBox tlstrptxtCamerID;
        private DevExpress.XtraEditors.LabelControl lblcGlassPrompt;
        private DevExpress.XtraEditors.LabelControl lblcCoordRPrompt;
        private DevExpress.XtraEditors.LabelControl lblcCoordYPrompt;
        private DevExpress.XtraEditors.LabelControl lblcCoordXPrompt;
        private DevExpress.XtraEditors.LabelControl lblcMembrane2Prompt;
        private DevExpress.XtraEditors.LabelControl lblcMembrane1Prompt;
        private DevExpress.XtraEditors.SpinEdit speCoordRMembrane2;
        private DevExpress.XtraEditors.SpinEdit speCoordYMembrane2;
        private DevExpress.XtraEditors.SpinEdit speCoordRMembrane1;
        private DevExpress.XtraEditors.SpinEdit speCoordYMembrane1;
        private DevExpress.XtraEditors.SpinEdit speCoordXMembrane2;
        private DevExpress.XtraEditors.SpinEdit speCoordRGlass;
        private DevExpress.XtraEditors.SpinEdit speCoordXMembrane1;
        private DevExpress.XtraEditors.SpinEdit speCoordYGlass;
        private DevExpress.XtraEditors.SpinEdit speCoordXGlass;
        private DevExpress.XtraEditors.SimpleButton sbtnWriteDataMembrane2;
        private DevExpress.XtraEditors.SimpleButton sbtnWriteDataMembrane1;
        private DevExpress.XtraEditors.SimpleButton sbtnWriteDataGlass;
        private DevExpress.XtraEditors.SpinEdit speFlagMembrane2;
        private DevExpress.XtraEditors.SpinEdit speFlagMembrane1;
        private DevExpress.XtraEditors.SpinEdit speFlagGlass;
        private DevExpress.XtraEditors.LabelControl lblcFlagPrompt;
        private DevExpress.XtraEditors.SpinEdit speGamma;
        private DevExpress.XtraEditors.SpinEdit speDebouncerTime;
        private DevExpress.XtraEditors.SpinEdit speTriggerDelay;
        private DevExpress.XtraEditors.SpinEdit speGain;
        private DevExpress.XtraEditors.SpinEdit speExposureTime;
        private DevExpress.XtraEditors.LabelControl lblcGamma;
        private DevExpress.XtraEditors.LabelControl lblcDebouncerTime;
        private DevExpress.XtraEditors.LabelControl lblcTriggerDelay;
        private DevExpress.XtraEditors.LabelControl lblcGain;
        private DevExpress.XtraEditors.LabelControl lblExposureTime;
        private DevExpress.XtraEditors.SpinEdit speColUnit;
        private DevExpress.XtraEditors.SpinEdit speRowUnit;
        private DevExpress.XtraEditors.LabelControl lblColUnit;
        private DevExpress.XtraEditors.LabelControl lblRowUnit;
        private DevExpress.XtraEditors.LabelControl lblCoordinateMode;
        private DevExpress.XtraEditors.CheckEdit chkeCameraCenterReference;
        private DevExpress.XtraEditors.CheckEdit chkeModelReference;
    }
}
