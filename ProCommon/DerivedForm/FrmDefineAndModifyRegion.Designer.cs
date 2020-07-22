namespace ProCommon.DerivedForm
{
    partial class FrmDefineAndModifyRegion
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
            this.components = new System.ComponentModel.Container();
            this.layoutControlRoot = new DevExpress.XtraLayout.LayoutControl();
            this.lstbRegion = new DevExpress.XtraEditors.ListBoxControl();
            this.hwndcDisplay = new HalconDotNet.HWindowControl();
            this.layoutControlGroupRoot = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.pumMain = new DevExpress.XtraBars.PopupMenu(this.components);
            this.bbiLoadImage = new DevExpress.XtraBars.BarButtonItem();
            this.bbiLine = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRectangle1 = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRectangle2 = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCircle = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCircularArc = new DevExpress.XtraBars.BarButtonItem();
            this.bbiAnnulus = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDeleteActiveROI = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDeleteAllROI = new DevExpress.XtraBars.BarButtonItem();
            this.bbiClearIconic = new DevExpress.XtraBars.BarButtonItem();
            this.bbiResetWindow = new DevExpress.XtraBars.BarButtonItem();
            this.bchkiNone = new DevExpress.XtraBars.BarCheckItem();
            this.bchkiZoom = new DevExpress.XtraBars.BarCheckItem();
            this.bchkiMove = new DevExpress.XtraBars.BarCheckItem();
            this.bchkiMagnify = new DevExpress.XtraBars.BarCheckItem();
            this.bbiUpdateRegion = new DevExpress.XtraBars.BarButtonItem();
            this.bchkiModifyRegion = new DevExpress.XtraBars.BarCheckItem();
            this.bmgrMain = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.bbiNone = new DevExpress.XtraBars.BarButtonItem();
            this.bbiZoom = new DevExpress.XtraBars.BarButtonItem();
            this.bbiMove = new DevExpress.XtraBars.BarButtonItem();
            this.bbiMagnify = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlRoot)).BeginInit();
            this.layoutControlRoot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstbRegion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pumMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bmgrMain)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlRoot
            // 
            this.layoutControlRoot.Controls.Add(this.lstbRegion);
            this.layoutControlRoot.Controls.Add(this.hwndcDisplay);
            this.layoutControlRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlRoot.Location = new System.Drawing.Point(0, 0);
            this.layoutControlRoot.Name = "layoutControlRoot";
            this.layoutControlRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(887, 448, 812, 500);
            this.layoutControlRoot.Root = this.layoutControlGroupRoot;
            this.layoutControlRoot.Size = new System.Drawing.Size(1033, 724);
            this.layoutControlRoot.TabIndex = 0;
            this.layoutControlRoot.Text = "layoutControl1";
            // 
            // lstbRegion
            // 
            this.lstbRegion.Location = new System.Drawing.Point(821, 16);
            this.lstbRegion.Name = "lstbRegion";
            this.lstbRegion.Size = new System.Drawing.Size(196, 692);
            this.lstbRegion.StyleController = this.layoutControlRoot;
            this.lstbRegion.TabIndex = 5;
            // 
            // hwndcDisplay
            // 
            this.hwndcDisplay.BackColor = System.Drawing.Color.Black;
            this.hwndcDisplay.BorderColor = System.Drawing.Color.Black;
            this.hwndcDisplay.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hwndcDisplay.Location = new System.Drawing.Point(16, 16);
            this.hwndcDisplay.Name = "hwndcDisplay";
            this.bmgrMain.SetPopupContextMenu(this.hwndcDisplay, this.pumMain);
            this.hwndcDisplay.Size = new System.Drawing.Size(799, 692);
            this.hwndcDisplay.TabIndex = 4;
            this.hwndcDisplay.WindowSize = new System.Drawing.Size(799, 692);
            // 
            // layoutControlGroupRoot
            // 
            this.layoutControlGroupRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupRoot.GroupBordersVisible = false;
            this.layoutControlGroupRoot.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroupRoot.Name = "Root";
            this.layoutControlGroupRoot.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroupRoot.Size = new System.Drawing.Size(1033, 724);
            this.layoutControlGroupRoot.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.hwndcDisplay;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(805, 698);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lstbRegion;
            this.layoutControlItem2.Location = new System.Drawing.Point(805, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(202, 698);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // pumMain
            // 
            this.pumMain.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiLoadImage, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiLine, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiRectangle1),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiRectangle2),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiCircle),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiCircularArc),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiAnnulus),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiDeleteActiveROI, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiDeleteAllROI),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiClearIconic),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiResetWindow),
            new DevExpress.XtraBars.LinkPersistInfo(this.bchkiNone, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bchkiZoom),
            new DevExpress.XtraBars.LinkPersistInfo(this.bchkiMove),
            new DevExpress.XtraBars.LinkPersistInfo(this.bchkiMagnify),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiUpdateRegion, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bchkiModifyRegion)});
            this.pumMain.Manager = this.bmgrMain;
            this.pumMain.Name = "pumMain";
            // 
            // bbiLoadImage
            // 
            this.bbiLoadImage.Caption = "加载图像";
            this.bbiLoadImage.Id = 0;
            this.bbiLoadImage.Name = "bbiLoadImage";
            this.bbiLoadImage.Tag = "BBI_LOADIMAGE";
            // 
            // bbiLine
            // 
            this.bbiLine.Caption = "矢量线段";
            this.bbiLine.Id = 1;
            this.bbiLine.Name = "bbiLine";
            this.bbiLine.Tag = "BBI_LINE";
            // 
            // bbiRectangle1
            // 
            this.bbiRectangle1.Caption = "齐轴矩形";
            this.bbiRectangle1.Id = 2;
            this.bbiRectangle1.Name = "bbiRectangle1";
            this.bbiRectangle1.Tag = "BBI_RECTANGLE1";
            // 
            // bbiRectangle2
            // 
            this.bbiRectangle2.Caption = "仿射矩形";
            this.bbiRectangle2.Id = 3;
            this.bbiRectangle2.Name = "bbiRectangle2";
            this.bbiRectangle2.Tag = "BBI_RECTANGLE2";
            // 
            // bbiCircle
            // 
            this.bbiCircle.Caption = "闭合圆形";
            this.bbiCircle.Id = 4;
            this.bbiCircle.Name = "bbiCircle";
            this.bbiCircle.Tag = "BBI_CIRCLE";
            // 
            // bbiCircularArc
            // 
            this.bbiCircularArc.Caption = "有向圆弧";
            this.bbiCircularArc.Id = 5;
            this.bbiCircularArc.Name = "bbiCircularArc";
            this.bbiCircularArc.Tag = "BBI_CIRCULARARC";
            // 
            // bbiAnnulus
            // 
            this.bbiAnnulus.Caption = "闭合圆环";
            this.bbiAnnulus.Id = 6;
            this.bbiAnnulus.Name = "bbiAnnulus";
            this.bbiAnnulus.Tag = "BBI_ANNULUS";
            // 
            // bbiDeleteActiveROI
            // 
            this.bbiDeleteActiveROI.Caption = "删除活动区域";
            this.bbiDeleteActiveROI.Id = 11;
            this.bbiDeleteActiveROI.Name = "bbiDeleteActiveROI";
            this.bbiDeleteActiveROI.Tag = "BBI_DELETEACTIVEROI";
            // 
            // bbiDeleteAllROI
            // 
            this.bbiDeleteAllROI.Caption = "删除所有区域";
            this.bbiDeleteAllROI.Id = 12;
            this.bbiDeleteAllROI.Name = "bbiDeleteAllROI";
            this.bbiDeleteAllROI.Tag = "BBI_DELETEALLROI";
            // 
            // bbiClearIconic
            // 
            this.bbiClearIconic.Caption = "清空图形";
            this.bbiClearIconic.Id = 21;
            this.bbiClearIconic.Name = "bbiClearIconic";
            this.bbiClearIconic.Tag = "BBI_CLEARICONIC";
            // 
            // bbiResetWindow
            // 
            this.bbiResetWindow.Caption = "重置窗体";
            this.bbiResetWindow.Id = 18;
            this.bbiResetWindow.Name = "bbiResetWindow";
            this.bbiResetWindow.Tag = "BBI_RESETWINDOW";
            // 
            // bchkiNone
            // 
            this.bchkiNone.BindableChecked = true;
            this.bchkiNone.Caption = "无操作";
            this.bchkiNone.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.bchkiNone.Checked = true;
            this.bchkiNone.CheckStyle = DevExpress.XtraBars.BarCheckStyles.Radio;
            this.bchkiNone.GroupIndex = 1;
            this.bchkiNone.Id = 14;
            this.bchkiNone.Name = "bchkiNone";
            this.bchkiNone.Tag = "BCHKI_NONE";
            // 
            // bchkiZoom
            // 
            this.bchkiZoom.Caption = "缩放图形";
            this.bchkiZoom.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.bchkiZoom.CheckStyle = DevExpress.XtraBars.BarCheckStyles.Radio;
            this.bchkiZoom.GroupIndex = 1;
            this.bchkiZoom.Id = 15;
            this.bchkiZoom.Name = "bchkiZoom";
            this.bchkiZoom.Tag = "BCHKI_ZOOM";
            // 
            // bchkiMove
            // 
            this.bchkiMove.Caption = "移动图形";
            this.bchkiMove.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.bchkiMove.CheckStyle = DevExpress.XtraBars.BarCheckStyles.Radio;
            this.bchkiMove.GroupIndex = 1;
            this.bchkiMove.Id = 16;
            this.bchkiMove.Name = "bchkiMove";
            this.bchkiMove.Tag = "BCHKI_MOVE";
            // 
            // bchkiMagnify
            // 
            this.bchkiMagnify.Caption = "局部放大";
            this.bchkiMagnify.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.bchkiMagnify.CheckStyle = DevExpress.XtraBars.BarCheckStyles.Radio;
            this.bchkiMagnify.GroupIndex = 1;
            this.bchkiMagnify.Id = 17;
            this.bchkiMagnify.Name = "bchkiMagnify";
            this.bchkiMagnify.Tag = "BCHKI_MAGNIFY";
            // 
            // bbiUpdateRegion
            // 
            this.bbiUpdateRegion.Caption = "更新区域";
            this.bbiUpdateRegion.Id = 20;
            this.bbiUpdateRegion.Name = "bbiUpdateRegion";
            this.bbiUpdateRegion.Tag = "BBI_UPDATEREGION";
            this.bbiUpdateRegion.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // bchkiModifyRegion
            // 
            this.bchkiModifyRegion.Caption = "修改区域";
            this.bchkiModifyRegion.Id = 19;
            this.bchkiModifyRegion.Name = "bchkiModifyRegion";
            this.bchkiModifyRegion.Tag = "BCHKI_MODIFYREGION";
            this.bchkiModifyRegion.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // bmgrMain
            // 
            this.bmgrMain.DockControls.Add(this.barDockControlTop);
            this.bmgrMain.DockControls.Add(this.barDockControlBottom);
            this.bmgrMain.DockControls.Add(this.barDockControlLeft);
            this.bmgrMain.DockControls.Add(this.barDockControlRight);
            this.bmgrMain.Form = this;
            this.bmgrMain.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiLoadImage,
            this.bbiLine,
            this.bbiRectangle1,
            this.bbiRectangle2,
            this.bbiCircle,
            this.bbiCircularArc,
            this.bbiAnnulus,
            this.bbiNone,
            this.bbiZoom,
            this.bbiMove,
            this.bbiMagnify,
            this.bbiDeleteActiveROI,
            this.bbiDeleteAllROI,
            this.bbiResetWindow,
            this.bchkiNone,
            this.bchkiZoom,
            this.bchkiMove,
            this.bchkiMagnify,
            this.bchkiModifyRegion,
            this.bbiUpdateRegion,
            this.bbiClearIconic});
            this.bmgrMain.MaxItemId = 22;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.bmgrMain;
            this.barDockControlTop.Size = new System.Drawing.Size(1033, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 724);
            this.barDockControlBottom.Manager = this.bmgrMain;
            this.barDockControlBottom.Size = new System.Drawing.Size(1033, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.bmgrMain;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 724);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1033, 0);
            this.barDockControlRight.Manager = this.bmgrMain;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 724);
            // 
            // bbiNone
            // 
            this.bbiNone.Caption = "恢复初态";
            this.bbiNone.Id = 7;
            this.bbiNone.Name = "bbiNone";
            this.bbiNone.Tag = "BBI_NONE";
            // 
            // bbiZoom
            // 
            this.bbiZoom.Caption = "缩放图形";
            this.bbiZoom.Id = 8;
            this.bbiZoom.Name = "bbiZoom";
            this.bbiZoom.Tag = "BBI_ZOOM";
            // 
            // bbiMove
            // 
            this.bbiMove.Caption = "移动图形";
            this.bbiMove.Id = 9;
            this.bbiMove.Name = "bbiMove";
            this.bbiMove.Tag = "BBI_MOVE";
            // 
            // bbiMagnify
            // 
            this.bbiMagnify.Caption = "局部放大";
            this.bbiMagnify.Id = 10;
            this.bbiMagnify.Name = "bbiMagnify";
            this.bbiMagnify.Tag = "BBI_MAGNIFY";
            // 
            // FrmDefineAndModifyRegion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 724);
            this.Controls.Add(this.layoutControlRoot);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmDefineAndModifyRegion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "FRM_DEFINEANDMODIFYREGION";
            this.Text = "定义与修改区域";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlRoot)).EndInit();
            this.layoutControlRoot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstbRegion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pumMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bmgrMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlRoot;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRoot;
        private DevExpress.XtraEditors.ListBoxControl lstbRegion;
        private HalconDotNet.HWindowControl hwndcDisplay;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraBars.BarManager bmgrMain;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.PopupMenu pumMain;
        private DevExpress.XtraBars.BarButtonItem bbiLoadImage;
        private DevExpress.XtraBars.BarButtonItem bbiLine;
        private DevExpress.XtraBars.BarButtonItem bbiRectangle1;
        private DevExpress.XtraBars.BarButtonItem bbiRectangle2;
        private DevExpress.XtraBars.BarButtonItem bbiCircle;
        private DevExpress.XtraBars.BarButtonItem bbiCircularArc;
        private DevExpress.XtraBars.BarButtonItem bbiAnnulus;
        private DevExpress.XtraBars.BarButtonItem bbiNone;
        private DevExpress.XtraBars.BarButtonItem bbiZoom;
        private DevExpress.XtraBars.BarButtonItem bbiMove;
        private DevExpress.XtraBars.BarButtonItem bbiMagnify;
        private DevExpress.XtraBars.BarButtonItem bbiDeleteActiveROI;
        private DevExpress.XtraBars.BarButtonItem bbiDeleteAllROI;
        private DevExpress.XtraBars.BarCheckItem bchkiNone;
        private DevExpress.XtraBars.BarCheckItem bchkiZoom;
        private DevExpress.XtraBars.BarCheckItem bchkiMove;
        private DevExpress.XtraBars.BarCheckItem bchkiMagnify;
        private DevExpress.XtraBars.BarButtonItem bbiResetWindow;
        private DevExpress.XtraBars.BarCheckItem bchkiModifyRegion;
        private DevExpress.XtraBars.BarButtonItem bbiUpdateRegion;
        private DevExpress.XtraBars.BarButtonItem bbiClearIconic;
    }
}