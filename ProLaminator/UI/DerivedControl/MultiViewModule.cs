using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProLaminator.UI.DerivedControl
{
    public delegate void ViewerStateChangedDel(bool isMultiViewer);
    public partial class MultiViewModule : ProLaminator.UI.DerivedControl.BaseModule
    {
        public event ViewerStateChangedDel ViewerStateChangedEvent;

        protected internal int ViewNumber
        {
            private set; get;
        }

        /// <summary>
        /// 控件添加过标记
        /// </summary>
        private bool _isControlAdded;
        public bool IsSwitchViewAllowed;

        /// <summary>
        /// 设置相机对应工位的识别或其他参数界面
        /// </summary>
        public ProLaminator.UI.DerivedControl.CameraStationSet CameraStationSet;

        /// <summary>
        /// 图形结果视图界面组
        /// </summary>
        public ProLaminator.UI.DerivedControl.IconicResultViewer[] IconicResultViewers;

        /// <summary>
        /// 配置管理器
        /// </summary>
        public ProLaminator.Config.CfgManager CfgMgr { private set; get; }

        /// <summary>
        /// 警告内容和标题
        /// </summary>
        private string _warningText, _warningCaption;

        private MultiViewModule()
        {
            InitializeComponent();
        }

        public MultiViewModule(ProCommon.Communal.Language lan,ProLaminator.Config.CfgManager cfgMgr) :this()
        {
            LangVersion = lan;
            int viewNum = 0;

            if (cfgMgr != null
                && cfgMgr.CfgCam!=null)
                viewNum = cfgMgr.CfgCam.PropertyList.Count;

            ViewNumber = viewNum;
            CfgMgr = cfgMgr;

            InitFieldAndProperty();
            InitControl();
        }

        private void InitFieldAndProperty()
        {
            _isControlAdded = false;
            IsSwitchViewAllowed = false;

            IconicResultViewers = null;
            CameraStationSet = null;
            ViewerStateChangedEvent = new ViewerStateChangedDel(OnViewerStateChanged);
        }

        private void OnViewerStateChanged(bool isMultiViewer)
        {
           
        }

        private void InitControl()
        {
            CameraStationSet = new CameraStationSet(LangVersion,this);
            CameraStationSet.Parent = this.pcRoot;
            CameraStationSet.Dock = System.Windows.Forms.DockStyle.Fill;

            this.Load += MultiViewModule_Load;
            this.tbllpRoot.SizeChanged += TbllpRoot_SizeChanged; 
                 
            UpdateControl();
        }

        private void UpdateControl()
        {

        }

        /// <summary>
        /// 窗体尺寸改变,重新分屏
        /// [注:分屏的大小和位置]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbllpRoot_SizeChanged(object sender, EventArgs e)
        {
           if(_isControlAdded)
                SplitMultiViewer(ViewNumber);            
        }

        private void MultiViewModule_Load(object sender, EventArgs e)
        {
            SplitMultiViewer(ViewNumber);
            ShowMultiViewer();
        }


        /// <summary>
        /// 显示多个图形结果视图
        /// </summary>
        public void ShowMultiViewer()
        {
            if (CameraStationSet != null)
                CameraStationSet.Hide();

            this.tbllpRoot.Show();
            this.tbllpRoot.BringToFront();
            if (ViewerStateChangedEvent != null)
                ViewerStateChangedEvent(true);
        }

        /// <summary>
        /// 显示单个图形结果视图
        /// </summary>
        public void ShowSingleViewer(string camID)
        {
            if (!ProLaminator.Logic.SystemManager.Instance.IsRunning)
            {
                //管理员权限时(即未登录前是不允许切换的,也就不会改变系统运行状态),允许切换
                if (IsSwitchViewAllowed)
                {
                    ProLaminator.Logic.SystemManager.Instance.IsRunOnce = false;
                    ProLaminator.Logic.SystemManager.Instance.IsRunning = true;

                    this.tbllpRoot.Hide();

                    if (CameraStationSet != null)
                    {
                        CameraStationSet.Show();
                        CameraStationSet.BringToFront();

                        if (ViewerStateChangedEvent != null)
                            ViewerStateChangedEvent(false);

                        UpdateCameraStationSet(camID);
                    }
                }
            }
            else
            {
                bool isChs = LangVersion == ProCommon.Communal.Language.Chinese;

                _warningText = isChs ? "无法操作!\r\n系统在运行模式!" : "Denied!\r\n Application is on running mode.";
                _warningCaption = isChs ? "警告信息" : "Warning Message";
                ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                   ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, isChs);
            }
        }

        public void UpdateCameraStationSet(string camID)
        {
            if (CameraStationSet != null)
            {
                //显示相机与工位信息
                if (CfgMgr != null
                    && CfgMgr.CfgCam != null)
                    CameraStationSet.CameraID = camID;
            }
        }

        /// <summary>
        /// 根据指定屏数目,多屏布局
        /// </summary>
        /// <param name="viewerNum"></param>
        public void SplitMultiViewer(int viewerNum)
        {
            this.tbllpRoot.ColumnStyles.Clear();
            this.tbllpRoot.RowStyles.Clear();
            this.tbllpRoot.Controls.Clear();
            this.tbllpRoot.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
            this.tbllpRoot.Dock = System.Windows.Forms.DockStyle.Fill;

            _isControlAdded = false;
            switch (viewerNum)
            {
                case 1:
                    SplitOneView();
                    break;
                case 2:
                    SplitTwoView();
                    break;
                case 3:
                    SplitThreeView();
                    break;
                case 4:
                    SplitFourView();
                    break;               
                default:
                    break;
            }
        }

        /// <summary>
        /// 单个图形结果视图
        /// </summary>
        private void SplitOneView()
        {
            int tmpViewNum = 1, rowNum = 1, colNum = 1;

            if (IconicResultViewers == null)
                IconicResultViewers = new IconicResultViewer[tmpViewNum];

            this.tbllpRoot.RowCount = rowNum;
            this.tbllpRoot.ColumnCount = colNum;
            this.tbllpRoot.Refresh();

            //IconicResultViewer的Margin设置为3(默认),因此需要空格6*列数,6*行数
            int viewerWidth = (this.tbllpRoot.Width - 6 * colNum) / colNum;
            int viewerHeight = (this.tbllpRoot.Height - 6 * rowNum) / rowNum;

            int rowIdx, colIdx;//单元格的行列索引

            for (int i = 0; i < tmpViewNum; i++)
            {
                // 按先行后列的顺序排
                rowIdx = i / rowNum; //控件所在行索引为视图序数的商数
                colIdx = i % colNum; //控件所在列索引为视图序数的余数

                if (CfgMgr != null
                    && CfgMgr.CfgCam != null)
                    if (IconicResultViewers[i] == null)
                        IconicResultViewers[i] = new IconicResultViewer(LangVersion, this, CfgMgr.CfgCam.PropertyList[i].ID);

                IconicResultViewers[i].Size = new Size(viewerWidth, viewerHeight);

                //未添加过,则在非空时添加
                if (!_isControlAdded)
                    if (IconicResultViewers[i] != null)
                    {
                        this.tbllpRoot.Controls.Add(IconicResultViewers[i], colIdx, rowIdx);
                        IconicResultViewers[i].Parent = this.tbllpRoot;
                        IconicResultViewers[i].Dock = System.Windows.Forms.DockStyle.Fill;
                    }
            }

            //未添加过,则在非空时添加
            if (!_isControlAdded)
                if (CameraStationSet != null)
                    this.pcRoot.Controls.Add(CameraStationSet);

            _isControlAdded = true;
        }

        /// <summary>
        /// 两个图形结果视图
        /// </summary>
        private void SplitTwoView()
        {
            int tmpViewNum = 2, rowNum = 1, colNum = 2;

            if (IconicResultViewers == null)
                IconicResultViewers = new IconicResultViewer[tmpViewNum];

            this.tbllpRoot.RowCount = rowNum;
            this.tbllpRoot.ColumnCount = colNum;
            this.tbllpRoot.Refresh();

            //IconicResultViewer的Margin设置为3(默认),因此需要空格6*列数,6*行数
            int viewerWidth = (this.tbllpRoot.Width - 6 * colNum) / colNum;
            int viewerHeight = (this.tbllpRoot.Height - 6 * rowNum) / rowNum;

            int rowIdx, colIdx;//单元格的行列索引

            for (int i = 0; i < tmpViewNum; i++)
            {
                // 按先行后列的顺序排
                switch (i)
                {
                    case 1:
                        rowIdx = 0;
                        colIdx = 1;
                        break;
                    default:
                        rowIdx = 0;
                        colIdx = 0;
                        break;
                }

                if (CfgMgr != null
                     && CfgMgr.CfgCam != null)
                    if (IconicResultViewers[i] == null)
                        IconicResultViewers[i] = new IconicResultViewer(LangVersion, this, CfgMgr.CfgCam.PropertyList[i].ID);

                IconicResultViewers[i].Size = new Size(viewerWidth, viewerHeight);

                //未添加过,则在非空时添加
                if (!_isControlAdded)
                    if (IconicResultViewers[i] != null)
                    {
                        this.tbllpRoot.Controls.Add(IconicResultViewers[i], colIdx, rowIdx);
                        IconicResultViewers[i].Parent = this.tbllpRoot;
                        IconicResultViewers[i].Dock = System.Windows.Forms.DockStyle.Fill;
                    }
            }

            //未添加过,则在非空时添加
            if (!_isControlAdded)
                if (CameraStationSet != null)
                    this.pcRoot.Controls.Add(CameraStationSet);

            _isControlAdded = true;
        }

        /// <summary>
        /// 三个图形结果视图
        /// </summary>
        private void SplitThreeView()
        {
            int tmpViewNum = 3, rowNum = 1, colNum = 3;

            if (IconicResultViewers == null)
                IconicResultViewers = new IconicResultViewer[tmpViewNum];

            this.tbllpRoot.RowCount = rowNum;
            this.tbllpRoot.ColumnCount = colNum;
            this.tbllpRoot.Refresh();

            //IconicResultViewer的Margin设置为3(默认),因此需要空格6*列数,6*行数
            int viewerWidth = (this.tbllpRoot.Width - 6 * colNum) / colNum;
            int viewerHeight = (this.tbllpRoot.Height - 6 * rowNum) / rowNum;

            int rowIdx, colIdx;//单元格的行列索引

            for (int i = 0; i < tmpViewNum; i++)
            {
                // 按先行后列的顺序排
                switch (i)
                {
                    case 1:
                        rowIdx = 0;
                        colIdx = 1;
                        break;
                    case 2:
                        rowIdx = 0;
                        colIdx = 2;
                        break;
                    default:
                        rowIdx = 0;
                        colIdx = 0;
                        break;
                }

                if (CfgMgr != null
                    && CfgMgr.CfgCam != null)
                    if (IconicResultViewers[i] == null)
                        IconicResultViewers[i] = new IconicResultViewer(LangVersion, this, CfgMgr.CfgCam.PropertyList[i].ID);

                IconicResultViewers[i].Size = new Size(viewerWidth, viewerHeight);

                //未添加过,则在非空时添加
                if (!_isControlAdded)
                    if (IconicResultViewers[i] != null)
                    {
                        this.tbllpRoot.Controls.Add(IconicResultViewers[i], colIdx, rowIdx);
                        IconicResultViewers[i].Parent = this.tbllpRoot;
                        IconicResultViewers[i].Dock = System.Windows.Forms.DockStyle.Fill;
                    }
            }

            //未添加过,则在非空时添加
            if (!_isControlAdded)
                if (CameraStationSet != null)
                    this.pcRoot.Controls.Add(CameraStationSet);

            _isControlAdded = true;
        }

        /// <summary>
        /// 四个图形结果视图
        /// </summary>
        private void SplitFourView()
        {
            int tmpViewNum = 4, rowNum = 2, colNum = 2;

            if (IconicResultViewers == null)
                IconicResultViewers = new IconicResultViewer[tmpViewNum];

            this.tbllpRoot.RowCount = rowNum;
            this.tbllpRoot.ColumnCount = colNum;
            this.tbllpRoot.Refresh();

            //IconicResultViewer的Margin设置为3(默认),因此需要空格6*列数,6*行数
            int viewerWidth = (this.tbllpRoot.Width - 6 * colNum) / colNum;
            int viewerHeight = (this.tbllpRoot.Height - 6 * rowNum) / rowNum;

            int rowIdx, colIdx;//单元格的行列索引

            for (int i = 0; i < tmpViewNum; i++)
            {
                // 按先行后列的顺序排
                rowIdx = i / rowNum; //控件所在行索引为视图序数的商数
                colIdx = i % colNum; //控件所在列索引为视图序数的余数

                if (CfgMgr != null
                    && CfgMgr.CfgCam != null)
                    if (IconicResultViewers[i] == null)
                        IconicResultViewers[i] = new IconicResultViewer(LangVersion, this, CfgMgr.CfgCam.PropertyList[i].ID);

                IconicResultViewers[i].Size = new Size(viewerWidth, viewerHeight);

                //未添加过,则在非空时添加
                if (!_isControlAdded)
                    if (IconicResultViewers[i] != null)
                    {
                        this.tbllpRoot.Controls.Add(IconicResultViewers[i], colIdx, rowIdx);
                        IconicResultViewers[i].Parent = this.tbllpRoot;
                        IconicResultViewers[i].Dock = System.Windows.Forms.DockStyle.Fill;
                    }
            }

            //未添加过,则在非空时添加
            if (!_isControlAdded)
                if (CameraStationSet != null)
                    this.pcRoot.Controls.Add(CameraStationSet);
            _isControlAdded = true;
        }
    }
}
