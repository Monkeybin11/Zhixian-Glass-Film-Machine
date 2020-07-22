namespace ProLaminator.UI
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.bmgrMain = new DevExpress.XtraBars.BarManager(this.components);
            this.bMenu = new DevExpress.XtraBars.Bar();
            this.bsbiRoutine = new DevExpress.XtraBars.BarSubItem();
            this.bbiNewRoutine = new DevExpress.XtraBars.BarButtonItem();
            this.bbiLoadRoutine = new DevExpress.XtraBars.BarButtonItem();
            this.bbiManageRoutine = new DevExpress.XtraBars.BarButtonItem();
            this.bsbiSet = new DevExpress.XtraBars.BarSubItem();
            this.bbiSetSystem = new DevExpress.XtraBars.BarButtonItem();
            this.bsbiAccount = new DevExpress.XtraBars.BarSubItem();
            this.bbiLogIn = new DevExpress.XtraBars.BarButtonItem();
            this.bbiManageAccount = new DevExpress.XtraBars.BarButtonItem();
            this.bbiLogOut = new DevExpress.XtraBars.BarButtonItem();
            this.bsbiAbout = new DevExpress.XtraBars.BarSubItem();
            this.bbiVendorInfo = new DevExpress.XtraBars.BarButtonItem();
            this.bbiAppInfo = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRunContinue = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRunStop = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExitSystem = new DevExpress.XtraBars.BarButtonItem();
            this.beiCurrentRoutine = new DevExpress.XtraBars.BarEditItem();
            this.rpibeiCurrentRoutine = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.bStatus = new DevExpress.XtraBars.Bar();
            this.bstiClientName = new DevExpress.XtraBars.BarStaticItem();
            this.bstiCurrentAccount = new DevExpress.XtraBars.BarStaticItem();
            this.bsbiCameraStatus = new DevExpress.XtraBars.BarSubItem();
            this.bsbiBoardStatus = new DevExpress.XtraBars.BarSubItem();
            this.bsbiSerialPortStatus = new DevExpress.XtraBars.BarSubItem();
            this.bstiCurrentTime = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imgColSmall = new DevExpress.Utils.ImageCollection(this.components);
            this.bbiClearLog = new DevExpress.XtraBars.BarButtonItem();
            this.imgColLarge = new DevExpress.Utils.ImageCollection(this.components);
            this.bmgrLog = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.pupmLog = new DevExpress.XtraBars.PopupMenu(this.components);
            this.pcRoot = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.bmgrMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpibeiCurrentRoutine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgColSmall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgColLarge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bmgrLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pupmLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcRoot)).BeginInit();
            this.SuspendLayout();
            // 
            // bmgrMain
            // 
            this.bmgrMain.AllowShowToolbarsPopup = false;
            this.bmgrMain.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bMenu,
            this.bStatus});
            this.bmgrMain.DockControls.Add(this.barDockControlTop);
            this.bmgrMain.DockControls.Add(this.barDockControlBottom);
            this.bmgrMain.DockControls.Add(this.barDockControlLeft);
            this.bmgrMain.DockControls.Add(this.barDockControlRight);
            this.bmgrMain.Form = this;
            this.bmgrMain.Images = this.imgColSmall;
            this.bmgrMain.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bsbiRoutine,
            this.bsbiSet,
            this.bsbiAccount,
            this.bsbiAbout,
            this.bbiRunContinue,
            this.bbiRunStop,
            this.bbiExitSystem,
            this.beiCurrentRoutine,
            this.bstiClientName,
            this.bstiCurrentAccount,
            this.bsbiCameraStatus,
            this.bsbiBoardStatus,
            this.bsbiSerialPortStatus,
            this.bstiCurrentTime,
            this.bbiClearLog,
            this.bbiNewRoutine,
            this.bbiLoadRoutine,
            this.bbiManageRoutine,
            this.bbiSetSystem,
            this.bbiLogIn,
            this.bbiManageAccount,
            this.bbiLogOut,
            this.bbiVendorInfo,
            this.bbiAppInfo});
            this.bmgrMain.LargeImages = this.imgColLarge;
            this.bmgrMain.MainMenu = this.bMenu;
            this.bmgrMain.MaxItemId = 37;
            this.bmgrMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rpibeiCurrentRoutine});
            this.bmgrMain.ShowFullMenusAfterDelay = false;
            this.bmgrMain.StatusBar = this.bStatus;
            // 
            // bMenu
            // 
            this.bMenu.BarName = "主菜单";
            this.bMenu.DockCol = 0;
            this.bMenu.DockRow = 0;
            this.bMenu.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bsbiRoutine),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsbiSet),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsbiAccount),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsbiAbout),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiRunContinue),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiRunStop),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExitSystem),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.beiCurrentRoutine, "", false, true, true, 230)});
            this.bMenu.OptionsBar.DrawBorder = false;
            this.bMenu.OptionsBar.DrawDragBorder = false;
            this.bMenu.OptionsBar.MultiLine = true;
            this.bMenu.OptionsBar.UseWholeRow = true;
            this.bMenu.Text = "主菜单";
            // 
            // bsbiRoutine
            // 
            this.bsbiRoutine.Caption = "程式";
            this.bsbiRoutine.Id = 0;
            this.bsbiRoutine.ImageOptions.ImageIndex = 19;
            this.bsbiRoutine.ImageOptions.LargeImageIndex = 19;
            this.bsbiRoutine.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiNewRoutine),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiLoadRoutine),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiManageRoutine)});
            this.bsbiRoutine.Name = "bsbiRoutine";
            this.bsbiRoutine.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bsbiRoutine.Tag = "BSBI_ROUTINE";
            // 
            // bbiNewRoutine
            // 
            this.bbiNewRoutine.Caption = "新建";
            this.bbiNewRoutine.Id = 26;
            this.bbiNewRoutine.ImageOptions.ImageUri.Uri = "New;Colored";
            this.bbiNewRoutine.Name = "bbiNewRoutine";
            this.bbiNewRoutine.Tag = "BBI_NEWROUTINE";
            // 
            // bbiLoadRoutine
            // 
            this.bbiLoadRoutine.Caption = "加载";
            this.bbiLoadRoutine.Id = 27;
            this.bbiLoadRoutine.ImageOptions.ImageIndex = 18;
            this.bbiLoadRoutine.ImageOptions.LargeImageIndex = 18;
            this.bbiLoadRoutine.Name = "bbiLoadRoutine";
            this.bbiLoadRoutine.Tag = "BBI_LOADROUTINE";
            // 
            // bbiManageRoutine
            // 
            this.bbiManageRoutine.Caption = "管理";
            this.bbiManageRoutine.Id = 28;
            this.bbiManageRoutine.ImageOptions.ImageUri.Uri = "Paste;Colored";
            this.bbiManageRoutine.Name = "bbiManageRoutine";
            this.bbiManageRoutine.Tag = "BBI_MANAGEROUTINE";
            // 
            // bsbiSet
            // 
            this.bsbiSet.Caption = "设置";
            this.bsbiSet.Id = 1;
            this.bsbiSet.ImageOptions.ImageIndex = 26;
            this.bsbiSet.ImageOptions.LargeImageIndex = 26;
            this.bsbiSet.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSetSystem)});
            this.bsbiSet.Name = "bsbiSet";
            this.bsbiSet.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bsbiSet.Tag = "BSBI_SET";
            // 
            // bbiSetSystem
            // 
            this.bbiSetSystem.Caption = "系统";
            this.bbiSetSystem.Id = 29;
            this.bbiSetSystem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbiSetSystem.ImageOptions.Image")));
            this.bbiSetSystem.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbiSetSystem.ImageOptions.LargeImage")));
            this.bbiSetSystem.Name = "bbiSetSystem";
            this.bbiSetSystem.Tag = "BBI_SETSYSTEM";
            // 
            // bsbiAccount
            // 
            this.bsbiAccount.Caption = "账户";
            this.bsbiAccount.Id = 2;
            this.bsbiAccount.ImageOptions.ImageIndex = 0;
            this.bsbiAccount.ImageOptions.LargeImageIndex = 0;
            this.bsbiAccount.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiLogIn),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiManageAccount),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiLogOut)});
            this.bsbiAccount.Name = "bsbiAccount";
            this.bsbiAccount.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bsbiAccount.Tag = "BSBI_ACCOUNT";
            // 
            // bbiLogIn
            // 
            this.bbiLogIn.Caption = "登录";
            this.bbiLogIn.Id = 32;
            this.bbiLogIn.ImageOptions.ImageIndex = 14;
            this.bbiLogIn.ImageOptions.LargeImageIndex = 14;
            this.bbiLogIn.Name = "bbiLogIn";
            this.bbiLogIn.Tag = "BBI_LOGIN";
            // 
            // bbiManageAccount
            // 
            this.bbiManageAccount.Caption = "管理";
            this.bbiManageAccount.Id = 33;
            this.bbiManageAccount.ImageOptions.ImageIndex = 0;
            this.bbiManageAccount.ImageOptions.LargeImageIndex = 0;
            this.bbiManageAccount.Name = "bbiManageAccount";
            this.bbiManageAccount.Tag = "BBI_MANAGEACCOUNT";
            // 
            // bbiLogOut
            // 
            this.bbiLogOut.Caption = "登出";
            this.bbiLogOut.Id = 34;
            this.bbiLogOut.ImageOptions.ImageIndex = 15;
            this.bbiLogOut.ImageOptions.LargeImageIndex = 15;
            this.bbiLogOut.Name = "bbiLogOut";
            this.bbiLogOut.Tag = "BBI_LOGOUT";
            // 
            // bsbiAbout
            // 
            this.bsbiAbout.Caption = "关于";
            this.bsbiAbout.Id = 3;
            this.bsbiAbout.ImageOptions.ImageIndex = 5;
            this.bsbiAbout.ImageOptions.LargeImageIndex = 5;
            this.bsbiAbout.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiVendorInfo),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiAppInfo)});
            this.bsbiAbout.Name = "bsbiAbout";
            this.bsbiAbout.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bsbiAbout.Tag = "BSBI_ABOUT";
            // 
            // bbiVendorInfo
            // 
            this.bbiVendorInfo.Caption = "厂商信息";
            this.bbiVendorInfo.Id = 35;
            this.bbiVendorInfo.ImageOptions.ImageIndex = 6;
            this.bbiVendorInfo.ImageOptions.LargeImageIndex = 6;
            this.bbiVendorInfo.Name = "bbiVendorInfo";
            this.bbiVendorInfo.Tag = "BBI_VENDORINFO";
            this.bbiVendorInfo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // bbiAppInfo
            // 
            this.bbiAppInfo.Caption = "软件信息";
            this.bbiAppInfo.Id = 36;
            this.bbiAppInfo.ImageOptions.ImageUri.Uri = "Pie;Size16x16;Colored";
            this.bbiAppInfo.Name = "bbiAppInfo";
            this.bbiAppInfo.Tag = "BBI_APPINFO";
            // 
            // bbiRunContinue
            // 
            this.bbiRunContinue.Caption = "连续";
            this.bbiRunContinue.Id = 5;
            this.bbiRunContinue.ImageOptions.ImageIndex = 28;
            this.bbiRunContinue.ImageOptions.LargeImageIndex = 28;
            this.bbiRunContinue.Name = "bbiRunContinue";
            this.bbiRunContinue.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbiRunContinue.Tag = "BBI_RUNCONTINUE";
            // 
            // bbiRunStop
            // 
            this.bbiRunStop.Caption = "停止";
            this.bbiRunStop.Id = 6;
            this.bbiRunStop.ImageOptions.ImageIndex = 29;
            this.bbiRunStop.ImageOptions.LargeImageIndex = 29;
            this.bbiRunStop.Name = "bbiRunStop";
            this.bbiRunStop.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbiRunStop.Tag = "BBI_RUNSTOP";
            // 
            // bbiExitSystem
            // 
            this.bbiExitSystem.Caption = "退出";
            this.bbiExitSystem.Id = 7;
            this.bbiExitSystem.ImageOptions.ImageIndex = 4;
            this.bbiExitSystem.ImageOptions.LargeImageIndex = 4;
            this.bbiExitSystem.Name = "bbiExitSystem";
            this.bbiExitSystem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbiExitSystem.Tag = "BBI_EXITSYSTEM";
            // 
            // beiCurrentRoutine
            // 
            this.beiCurrentRoutine.Caption = "当前程式";
            this.beiCurrentRoutine.Edit = this.rpibeiCurrentRoutine;
            this.beiCurrentRoutine.Id = 8;
            this.beiCurrentRoutine.Name = "beiCurrentRoutine";
            this.beiCurrentRoutine.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.beiCurrentRoutine.Tag = "BEI_CURRENTROUTINE";
            // 
            // rpibeiCurrentRoutine
            // 
            this.rpibeiCurrentRoutine.AutoHeight = false;
            this.rpibeiCurrentRoutine.Name = "rpibeiCurrentRoutine";
            this.rpibeiCurrentRoutine.ReadOnly = true;
            this.rpibeiCurrentRoutine.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // bStatus
            // 
            this.bStatus.BarName = "状态栏";
            this.bStatus.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bStatus.DockCol = 0;
            this.bStatus.DockRow = 0;
            this.bStatus.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bStatus.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bstiClientName),
            new DevExpress.XtraBars.LinkPersistInfo(this.bstiCurrentAccount),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsbiCameraStatus),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsbiBoardStatus),
            new DevExpress.XtraBars.LinkPersistInfo(this.bsbiSerialPortStatus),
            new DevExpress.XtraBars.LinkPersistInfo(this.bstiCurrentTime)});
            this.bStatus.OptionsBar.AllowQuickCustomization = false;
            this.bStatus.OptionsBar.DrawBorder = false;
            this.bStatus.OptionsBar.DrawDragBorder = false;
            this.bStatus.OptionsBar.UseWholeRow = true;
            this.bStatus.Text = "状态栏";
            // 
            // bstiClientName
            // 
            this.bstiClientName.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
            this.bstiClientName.Caption = "客户名称";
            this.bstiClientName.Id = 19;
            this.bstiClientName.Name = "bstiClientName";
            this.bstiClientName.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bstiClientName.Size = new System.Drawing.Size(300, 0);
            this.bstiClientName.Tag = "BSTI_CLIENTNAME";
            this.bstiClientName.Width = 300;
            // 
            // bstiCurrentAccount
            // 
            this.bstiCurrentAccount.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
            this.bstiCurrentAccount.Caption = "当前账户";
            this.bstiCurrentAccount.Id = 20;
            this.bstiCurrentAccount.Name = "bstiCurrentAccount";
            this.bstiCurrentAccount.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bstiCurrentAccount.Size = new System.Drawing.Size(200, 0);
            this.bstiCurrentAccount.Tag = "BSTI_CURRENTACCOUNT";
            this.bstiCurrentAccount.Width = 200;
            // 
            // bsbiCameraStatus
            // 
            this.bsbiCameraStatus.Caption = "相机状态";
            this.bsbiCameraStatus.Id = 21;
            this.bsbiCameraStatus.Name = "bsbiCameraStatus";
            this.bsbiCameraStatus.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bsbiCameraStatus.Size = new System.Drawing.Size(200, 0);
            this.bsbiCameraStatus.Tag = "BSBI_CAMERASTATUS";
            // 
            // bsbiBoardStatus
            // 
            this.bsbiBoardStatus.Caption = "板卡状态";
            this.bsbiBoardStatus.Id = 22;
            this.bsbiBoardStatus.Name = "bsbiBoardStatus";
            this.bsbiBoardStatus.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bsbiBoardStatus.Size = new System.Drawing.Size(200, 0);
            this.bsbiBoardStatus.Tag = "BSBI_BOARDSTATUS";
            // 
            // bsbiSerialPortStatus
            // 
            this.bsbiSerialPortStatus.Caption = "串口状态";
            this.bsbiSerialPortStatus.Id = 23;
            this.bsbiSerialPortStatus.Name = "bsbiSerialPortStatus";
            this.bsbiSerialPortStatus.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bsbiSerialPortStatus.Size = new System.Drawing.Size(200, 0);
            this.bsbiSerialPortStatus.Tag = "BSBI_SERIALPORTSTATUS";
            // 
            // bstiCurrentTime
            // 
            this.bstiCurrentTime.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
            this.bstiCurrentTime.Caption = "当前时间";
            this.bstiCurrentTime.Id = 24;
            this.bstiCurrentTime.Name = "bstiCurrentTime";
            this.bstiCurrentTime.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bstiCurrentTime.Size = new System.Drawing.Size(120, 0);
            this.bstiCurrentTime.Tag = "BSTI_CURRENTTIME";
            this.bstiCurrentTime.Width = 120;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.bmgrMain;
            this.barDockControlTop.Size = new System.Drawing.Size(1312, 30);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 691);
            this.barDockControlBottom.Manager = this.bmgrMain;
            this.barDockControlBottom.Size = new System.Drawing.Size(1312, 32);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 30);
            this.barDockControlLeft.Manager = this.bmgrMain;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 661);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1312, 30);
            this.barDockControlRight.Manager = this.bmgrMain;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 661);
            // 
            // imgColSmall
            // 
            this.imgColSmall.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgColSmall.ImageStream")));
            this.imgColSmall.Images.SetKeyName(0, "AccountManager_16.png");
            this.imgColSmall.Images.SetKeyName(1, "AlarmRed_16.png");
            this.imgColSmall.Images.SetKeyName(2, "AlarmYellow_16.png");
            this.imgColSmall.Images.SetKeyName(3, "BoardCtrller_16.png");
            this.imgColSmall.Images.SetKeyName(4, "ExitSystem_16.png");
            this.imgColSmall.Images.SetKeyName(5, "Help_16.png");
            this.imgColSmall.Images.SetKeyName(6, "Home_16.png");
            this.imgColSmall.Images.SetKeyName(7, "IOMonitor_16.png");
            this.imgColSmall.Images.SetKeyName(8, "JogAntiClockWise_16.png");
            this.imgColSmall.Images.SetKeyName(9, "JogClockWise_16.png");
            this.imgColSmall.Images.SetKeyName(10, "LightBlack_16.png");
            this.imgColSmall.Images.SetKeyName(11, "LightGreen_16.png");
            this.imgColSmall.Images.SetKeyName(12, "LightRed_16.png");
            this.imgColSmall.Images.SetKeyName(13, "LightYellow_16.png");
            this.imgColSmall.Images.SetKeyName(14, "LogIn_16.png");
            this.imgColSmall.Images.SetKeyName(15, "LogOut_16.png");
            this.imgColSmall.Images.SetKeyName(16, "Navigation_16.png");
            this.imgColSmall.Images.SetKeyName(17, "NewFile_16.png");
            this.imgColSmall.Images.SetKeyName(18, "OpenFile_16.png");
            this.imgColSmall.Images.SetKeyName(19, "RunLog_16.png");
            this.imgColSmall.Images.SetKeyName(20, "Save_16.png");
            this.imgColSmall.Images.SetKeyName(21, "SaveAs_16.png");
            this.imgColSmall.Images.SetKeyName(22, "SerialPort_16.png");
            this.imgColSmall.Images.SetKeyName(23, "SetBoardCrtller_16.png");
            this.imgColSmall.Images.SetKeyName(24, "SetMotor_16.png");
            this.imgColSmall.Images.SetKeyName(25, "SetSerialPort_16.png");
            this.imgColSmall.Images.SetKeyName(26, "SetSystem_16.png");
            this.imgColSmall.Images.SetKeyName(27, "StartReset_16.png");
            this.imgColSmall.Images.SetKeyName(28, "StartRun_16.png");
            this.imgColSmall.Images.SetKeyName(29, "StopRun_16.png");
            this.imgColSmall.Images.SetKeyName(30, "Timer_16.png");
            this.imgColSmall.Images.SetKeyName(31, "View_16.png");
            // 
            // bbiClearLog
            // 
            this.bbiClearLog.Caption = "清空日志";
            this.bbiClearLog.Id = 25;
            this.bbiClearLog.Name = "bbiClearLog";
            this.bbiClearLog.Tag = "BBI_CLEARLOG";
            // 
            // imgColLarge
            // 
            this.imgColLarge.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgColLarge.ImageStream")));
            this.imgColLarge.Images.SetKeyName(0, "AccountManager_32.png");
            this.imgColLarge.Images.SetKeyName(1, "AlarmRed_32.png");
            this.imgColLarge.Images.SetKeyName(2, "AlarmYellow_32.png");
            this.imgColLarge.Images.SetKeyName(3, "BoardCtrller_32.png");
            this.imgColLarge.Images.SetKeyName(4, "ExitSystem_32.png");
            this.imgColLarge.Images.SetKeyName(5, "Help_32.png");
            this.imgColLarge.Images.SetKeyName(6, "Home_32.png");
            this.imgColLarge.Images.SetKeyName(7, "IOMonitor_32.png");
            this.imgColLarge.Images.SetKeyName(8, "JogAntiClockWise_32.png");
            this.imgColLarge.Images.SetKeyName(9, "JogClockWise_32.png");
            this.imgColLarge.Images.SetKeyName(10, "LightBlack_32.png");
            this.imgColLarge.Images.SetKeyName(11, "LightGreen_32.png");
            this.imgColLarge.Images.SetKeyName(12, "LightRed_32.png");
            this.imgColLarge.Images.SetKeyName(13, "LightYellow_32.png");
            this.imgColLarge.Images.SetKeyName(14, "LogIn_32.png");
            this.imgColLarge.Images.SetKeyName(15, "LogOut_32.png");
            this.imgColLarge.Images.SetKeyName(16, "Navigation_32.png");
            this.imgColLarge.Images.SetKeyName(17, "NewFile_32.png");
            this.imgColLarge.Images.SetKeyName(18, "OpenFile_32.png");
            this.imgColLarge.Images.SetKeyName(19, "RunLog_32.png");
            this.imgColLarge.Images.SetKeyName(20, "Save_32.png");
            this.imgColLarge.Images.SetKeyName(21, "SaveAs_32.png");
            this.imgColLarge.Images.SetKeyName(22, "SerialPort_32.png");
            this.imgColLarge.Images.SetKeyName(23, "SetBoardCrtller_32.png");
            this.imgColLarge.Images.SetKeyName(24, "SetMotor_32.png");
            this.imgColLarge.Images.SetKeyName(25, "SetSerialPort_32.png");
            this.imgColLarge.Images.SetKeyName(26, "SetSystem_32.png");
            this.imgColLarge.Images.SetKeyName(27, "StartReset_32.png");
            this.imgColLarge.Images.SetKeyName(28, "StartRun_32.png");
            this.imgColLarge.Images.SetKeyName(29, "StopRun_48.png");
            this.imgColLarge.Images.SetKeyName(30, "Timer_32.png");
            this.imgColLarge.Images.SetKeyName(31, "View_32.png");
            // 
            // bmgrLog
            // 
            this.bmgrLog.DockControls.Add(this.barDockControl1);
            this.bmgrLog.DockControls.Add(this.barDockControl2);
            this.bmgrLog.DockControls.Add(this.barDockControl3);
            this.bmgrLog.DockControls.Add(this.barDockControl4);
            this.bmgrLog.Form = this;
            this.bmgrLog.MaxItemId = 0;
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 0);
            this.barDockControl1.Manager = this.bmgrLog;
            this.barDockControl1.Size = new System.Drawing.Size(1312, 0);
            // 
            // barDockControl2
            // 
            this.barDockControl2.CausesValidation = false;
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(0, 723);
            this.barDockControl2.Manager = this.bmgrLog;
            this.barDockControl2.Size = new System.Drawing.Size(1312, 0);
            // 
            // barDockControl3
            // 
            this.barDockControl3.CausesValidation = false;
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(0, 0);
            this.barDockControl3.Manager = this.bmgrLog;
            this.barDockControl3.Size = new System.Drawing.Size(0, 723);
            // 
            // barDockControl4
            // 
            this.barDockControl4.CausesValidation = false;
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(1312, 0);
            this.barDockControl4.Manager = this.bmgrLog;
            this.barDockControl4.Size = new System.Drawing.Size(0, 723);
            // 
            // pupmLog
            // 
            this.pupmLog.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiClearLog)});
            this.pupmLog.Manager = this.bmgrLog;
            this.pupmLog.Name = "pupmLog";
            // 
            // pcRoot
            // 
            this.pcRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcRoot.Location = new System.Drawing.Point(0, 30);
            this.pcRoot.Name = "pcRoot";
            this.pcRoot.Size = new System.Drawing.Size(1312, 661);
            this.pcRoot.TabIndex = 8;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1312, 723);
            this.ControlBox = false;
            this.Controls.Add(this.pcRoot);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "FRM_MAIN";
            this.Text = "插胶壳视觉处理系统";
            ((System.ComponentModel.ISupportInitialize)(this.bmgrMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpibeiCurrentRoutine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgColSmall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgColLarge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bmgrLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pupmLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcRoot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager bmgrMain;
        private DevExpress.XtraBars.Bar bMenu;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarSubItem bsbiRoutine;
        private DevExpress.XtraBars.BarSubItem bsbiSet;
        private DevExpress.XtraBars.BarSubItem bsbiAccount;
        private DevExpress.XtraBars.BarSubItem bsbiAbout;
        private DevExpress.XtraBars.BarButtonItem bbiRunContinue;
        private DevExpress.XtraBars.BarButtonItem bbiRunStop;
        private DevExpress.XtraBars.BarButtonItem bbiExitSystem;
        private DevExpress.XtraBars.BarEditItem beiCurrentRoutine;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rpibeiCurrentRoutine;
        private DevExpress.XtraBars.Bar bStatus;
        private DevExpress.XtraBars.BarStaticItem bstiClientName;
        private DevExpress.XtraBars.BarStaticItem bstiCurrentAccount;
        private DevExpress.XtraBars.BarSubItem bsbiCameraStatus;
        private DevExpress.XtraBars.BarSubItem bsbiBoardStatus;
        private DevExpress.XtraBars.BarSubItem bsbiSerialPortStatus;
        private DevExpress.XtraBars.BarStaticItem bstiCurrentTime;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarManager bmgrLog;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarButtonItem bbiClearLog;
        private DevExpress.XtraBars.PopupMenu pupmLog;
        private DevExpress.XtraBars.BarButtonItem bbiNewRoutine;
        private DevExpress.XtraBars.BarButtonItem bbiLoadRoutine;
        private DevExpress.XtraBars.BarButtonItem bbiManageRoutine;
        private DevExpress.XtraBars.BarButtonItem bbiSetSystem;
        private DevExpress.XtraBars.BarButtonItem bbiLogIn;
        private DevExpress.XtraBars.BarButtonItem bbiManageAccount;
        private DevExpress.XtraBars.BarButtonItem bbiLogOut;
        private DevExpress.XtraBars.BarButtonItem bbiVendorInfo;
        private DevExpress.XtraBars.BarButtonItem bbiAppInfo;
        protected DevExpress.Utils.ImageCollection imgColSmall;
        protected DevExpress.Utils.ImageCollection imgColLarge;
        public DevExpress.XtraEditors.PanelControl pcRoot;
    }
}