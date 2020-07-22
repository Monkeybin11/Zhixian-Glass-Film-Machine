using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;

namespace ProCommon.DerivedForm
{
    public partial class FrmDefineAndModifyRegion : DevExpress.XtraEditors.XtraForm
    {
        private FrmDefineAndModifyRegion()
        {
            InitializeComponent();

            InitFieldAndProperty();
            InitControl();
        }

        public FrmDefineAndModifyRegion(ProCommon.Communal.Language lan) : this()
        {
            LanguageVersion = lan;
        }

        public FrmDefineAndModifyRegion(ProCommon.Communal.Language lan, HalconDotNet.HObject hoImage) : this(lan)
        {
            _hoImage = hoImage;
            this.bbiLoadImage.Enabled = false;
        }

        public FrmDefineAndModifyRegion(ProCommon.Communal.Language lan, HalconDotNet.HObject hoImage, HalconDotNet.HObject rawRegion) : this(lan, hoImage)
        {
            this.bbiLine.Enabled = false;
            this.bbiRectangle1.Enabled = false;
            this.bbiRectangle2.Enabled = false;
            this.bbiCircle.Enabled = false;
            this.bbiCircularArc.Enabled = false;
            this.bbiAnnulus.Enabled = false;
            this.bbiDeleteActiveROI.Enabled = false;
            this.bbiDeleteAllROI.Enabled = false;

            _hoRawRegion = rawRegion;
            _isTransmitted = true;
        }

        public ProCommon.Communal.Language LanguageVersion { protected set; get; }

        private ProVision.InteractiveROI.ROIManager _ROIManager;
        private ProVision.InteractiveROI.HWndCtrller _HwndCtrller;

        /// <summary>
        /// 图像
        /// </summary>
        private HalconDotNet.HObject _hoImage;

        /// <summary>
        /// 初始区域
        /// [注:可能是数组]
        /// </summary>
        private HalconDotNet.HObject _hoRawRegion;

        /// <summary>
        /// 笔刷区域
        /// </summary>
        private HalconDotNet.HObject _hoBrushRegion;

        /// <summary>
        /// 选择区域
        /// </summary>
        private HalconDotNet.HObject _hoSelectedRegion;

        /// <summary>
        /// 修改后区域
        /// </summary>
        private HalconDotNet.HObject _hoModifiedRegion;

        /// <summary>
        /// 初始区域颜色
        /// </summary>
        private string _colorForRawRegion;

        /// <summary>
        /// 笔刷颜色
        /// </summary>
        private string _colorForBrushRegion;

        /// <summary>
        /// 选择区域颜色
        /// </summary>
        private string _colorForSelectedRegion;

        /// <summary>
        /// 修改后区域颜色
        /// </summary>
        private string _colorForModifiedRegion;

        private double _zoomFactorForBrush;

        private HalconDotNet.HTuple _imgWidth, _imgHeight;
        private bool _isCtrlKeyPressed, _isAltKeyPressed;
        private System.Collections.ArrayList _roiList;

        /// <summary>
        /// 是否传入待修改区域
        /// </summary>
        private bool _isTransmitted;

        /// <summary>
        /// 是否ROI更新为固定区域
        /// </summary>
        private bool _isUpdated;

        /// <summary>
        /// 是否正在修改
        /// </summary>
        private bool _isModifying;

        /// <summary>
        /// 是否修改完成
        /// </summary>
        private bool _isModified;      

        protected virtual void InitFieldAndProperty()
        {
            _isCtrlKeyPressed = false;
            _isAltKeyPressed = false;

            _isTransmitted = false;
            _isUpdated = false;
            _isModifying = false;
            _isModified = false;          

            _hoRawRegion = null;
            _hoBrushRegion = null;
            _hoSelectedRegion = null;
            _hoModifiedRegion = null;

            _colorForRawRegion = "blue";
            _colorForBrushRegion = "magenta";
            _colorForSelectedRegion = "green";
            _colorForModifiedRegion = "yellow";

            _zoomFactorForBrush = 50;

            _ROIManager = new ProVision.InteractiveROI.ROIManager();
            _ROIManager.SetActiveROISign(ProVision.InteractiveROI.ROIManager.ROI_MODE_POS);

            _HwndCtrller = new ProVision.InteractiveROI.HWndCtrller(this.hwndcDisplay);
            _HwndCtrller.RegisterROICtroller(_ROIManager);
            _HwndCtrller.RegisterHwndCtrlMouseEvents();
            _HwndCtrller.SetViewMode(ProVision.InteractiveROI.HWndCtrller.VIEW_MODE_NONE);

            //设置画模式:边缘/填充
            _HwndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_DRAWMODE, "margin");

            //设置区域颜色:蓝色
            _HwndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_COLOR, _colorForRawRegion);

            //设置区域线段:1.5
            _HwndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_LINEWIDTH, 1.5);

        }

        protected virtual void InitControl()
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += FrmDefineAndModifyRegion_Load;
            this.SizeChanged += FrmDefineAndModifyRegion_SizeChanged;
            this.FormClosing += FrmDefineAndModifyRegion_FormClosing;

            this.KeyPreview = true; //允许接收键盘事件
            this.KeyDown += FrmDefineAndModifyRegion_KeyDown;
            this.KeyUp += FrmDefineAndModifyRegion_KeyUp;
        }

        protected internal virtual void UpdateControl()
        {
            bool isChs = (LanguageVersion == ProCommon.Communal.Language.Chinese);
            string str = this.Tag.ToString();
            this.Text = isChs ? ProCommon.Properties.Resources.ResourceManager.GetString("chs_" + str) : ProCommon.Properties.Resources.ResourceManager.GetString("en_" + str);

            UpdateBarButtonItem(this.bbiLoadImage, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarButtonItem(this.bbiLine, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarButtonItem(this.bbiRectangle1, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarButtonItem(this.bbiRectangle2, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarButtonItem(this.bbiCircle, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarButtonItem(this.bbiCircularArc, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarButtonItem(this.bbiAnnulus, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarButtonItem(this.bbiDeleteActiveROI, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarButtonItem(this.bbiDeleteAllROI, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarButtonItem(this.bbiResetWindow, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarButtonItem(this.bbiClearIconic, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);

            UpdateBarCheckItem(this.bchkiNone, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarCheckItem(this.bchkiMove, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarCheckItem(this.bchkiZoom, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarCheckItem(this.bchkiMagnify, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarButtonItem(this.bbiUpdateRegion, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);
            UpdateBarCheckItem(this.bchkiModifyRegion, LanguageVersion, ProCommon.Properties.Resources.ResourceManager);


            this.lstbRegion.SelectedIndexChanged -= LstbRegion_SelectedIndexChanged;
            this.lstbRegion.SelectedIndexChanged += LstbRegion_SelectedIndexChanged;

            if (_hoImage != null
                && _hoImage.IsInitialized())
            {
                _HwndCtrller.ClearEntities();
                if (_ROIManager != null)
                    _ROIManager.ROIList.Clear();

                HalconDotNet.HOperatorSet.GetImageSize(_hoImage, out _imgWidth, out _imgHeight);
                HalconDotNet.HOperatorSet.SetSystem("tsp_height", _imgHeight);
                HalconDotNet.HOperatorSet.SetSystem("tsp_width", _imgWidth);

                _HwndCtrller.AddHobjEntity(_hoImage);
                _HwndCtrller.Repaint();
            }
        }

        /// <summary>
        /// 获取区域列表
        /// </summary>
        /// <returns></returns>
        public System.Collections.ArrayList GetRegionList()
        {
            if (_ROIManager != null)
                return _ROIManager.ROIList;
            else
                return null;
        }

        /// <summary>
        /// 获取修改后区域
        /// </summary>
        /// <returns></returns>
        public HalconDotNet.HObject GetModifiedRegion()
        {
            return _hoModifiedRegion;
        }

        private void Bbi_Click(object sender, ItemClickEventArgs e)
        {
            switch (e.Item.Tag.ToString())
            {
                case "BBI_LOADIMAGE":
                    LoadImage();
                    break;
                case "BBI_LINE":
                    if (_hoImage != null
                         && _hoImage.IsInitialized())
                        if (_ROIManager != null)
                            _ROIManager.SetROIShape(new ProVision.InteractiveROI.ROILine()); //定义区域时,矢量线段不响应;主要用在定义测量
                    break;
                case "BBI_RECTANGLE1":
                    if (_hoImage != null
                         && _hoImage.IsInitialized())
                        if (_ROIManager != null)
                            _ROIManager.SetROIShape(new ProVision.InteractiveROI.ROIRectangle1());
                    break;
                case "BBI_RECTANGLE2":
                    if (_hoImage != null
                         && _hoImage.IsInitialized())
                        if (_ROIManager != null)
                            _ROIManager.SetROIShape(new ProVision.InteractiveROI.ROIRectangle2());
                    break;
                case "BBI_CIRCULARARC":
                    if (_hoImage != null
                         && _hoImage.IsInitialized())
                        if (_ROIManager != null)
                            _ROIManager.SetROIShape(new ProVision.InteractiveROI.ROICircularArc()); //定义区域时,有向圆弧不响应;主要用在定义测量
                    break;
                case "BBI_CIRCLE":
                    if (_hoImage != null
                         && _hoImage.IsInitialized())
                        if (_ROIManager != null)
                            _ROIManager.SetROIShape(new ProVision.InteractiveROI.ROICircle());
                    break;
                case "BBI_ANNULUS":
                    if (_hoImage != null
                         && _hoImage.IsInitialized())
                        if (_ROIManager != null)
                            _ROIManager.SetROIShape(new ProVision.InteractiveROI.ROIAnnulus());
                    break;
                case "BBI_DELETEACTIVEROI":
                    if (_hoImage != null
                         && _hoImage.IsInitialized())
                        if (_ROIManager != null)
                            _ROIManager.RemoveActiveROI();
                    break;
                case "BBI_DELETEALLROI":
                    if (_hoImage != null
                         && _hoImage.IsInitialized())
                        if (_ROIManager != null)
                        {
                            _HwndCtrller.ResetAll();
                            _HwndCtrller.Repaint();
                        }
                    break;
                case "BBI_RESETWINDOW":
                    if (_hoImage != null
                         && _hoImage.IsInitialized())
                        if (_ROIManager != null)
                        {
                            _HwndCtrller.ResetWindow();
                            _HwndCtrller.Repaint();
                        }
                    break;
                case "BBI_CLEARICONIC":
                    if (_hoImage != null
                        && _hoImage.IsInitialized())
                        if (_ROIManager != null)
                        {
                            _HwndCtrller.ResetAll();

                            if (_hoModifiedRegion != null
                                && _hoModifiedRegion.IsInitialized())
                                _hoModifiedRegion.Dispose();

                            if (_hoSelectedRegion != null
                               && _hoSelectedRegion.IsInitialized())
                                _hoSelectedRegion.Dispose();

                            this.bchkiModifyRegion.Checked = false;
                            this.lstbRegion.Items.Clear();
                            _isUpdated = false;
                        }
                    break;
                case "BBI_UPDATEREGION":

                    _isUpdated = false;
                    if (_hoImage != null
                        && _hoImage.IsInitialized())
                    {
                        //若非传入区域
                        if (!_isTransmitted)
                        {
                            if (_ROIManager != null)
                            {
                                if (_hoRawRegion != null
                                   && _hoRawRegion.IsInitialized())
                                    _hoRawRegion.Dispose();
                                HalconDotNet.HOperatorSet.GenEmptyObj(out _hoRawRegion);

                                _roiList = _ROIManager.ROIList;

                                if (_roiList != null
                                   && _roiList.Count > 0)
                                {
                                    this.lstbRegion.Items.Clear();
                                    string roiType = "";

                                    for (int i = 0; i < _roiList.Count; i++)
                                    {
                                        HalconDotNet.HOperatorSet.ConcatObj(_hoRawRegion, ((ProVision.InteractiveROI.ROI)_roiList[i]).GetModelRegion(), out _hoRawRegion);
                                        roiType = ((ProVision.InteractiveROI.ROI)_roiList[i]).ROIShape.ToString();
                                        this.lstbRegion.Items.Add(i.ToString("00") + "[" + roiType + "]");
                                    }

                                    if (_hoModifiedRegion != null
                                        && _hoModifiedRegion.IsInitialized())
                                        _hoModifiedRegion.Dispose();

                                    _hoModifiedRegion = _hoRawRegion.Clone();
                                    this.lstbRegion.SelectedIndex = 0;
                                    HalconDotNet.HOperatorSet.SelectObj(_hoModifiedRegion, out _hoSelectedRegion, 1);
                                    _isUpdated = true;
                                }
                            }
                        }
                        else
                        {
                            //传入区域--作为单一区域
                            this.lstbRegion.Items.Clear();
                            this.lstbRegion.Items.Add("00" + "[Arbitrary]");
                            if (_hoModifiedRegion != null
                                  && _hoModifiedRegion.IsInitialized())
                                _hoModifiedRegion.Dispose();

                            //修改区域与选择区域为同一指向
                            HalconDotNet.HOperatorSet.SelectObj(_hoRawRegion, out _hoModifiedRegion, 1);
                            _hoSelectedRegion = _hoModifiedRegion;
                        }
                    }
                    break;
                default: break;
            }
        }

        private void Bchki_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraBars.BarCheckItem bchki = sender as DevExpress.XtraBars.BarCheckItem;
            if (bchki != null)
            {
                switch (e.Item.Tag.ToString())
                {
                    case "BCHKI_NONE":
                        if (bchki.Checked)
                            if (_hoImage != null
                                && _hoImage.IsInitialized())
                                if (_HwndCtrller != null)
                                    _HwndCtrller.SetViewMode(ProVision.InteractiveROI.HWndCtrller.VIEW_MODE_NONE);
                        break;
                    case "BCHKI_MOVE":
                        if (bchki.Checked)
                            if (_hoImage != null
                                 && _hoImage.IsInitialized())
                                if (_HwndCtrller != null)
                                    _HwndCtrller.SetViewMode(ProVision.InteractiveROI.HWndCtrller.VIEW_MODE_MOVE);
                        break;
                    case "BCHKI_ZOOM":
                        if (bchki.Checked)
                            if (_hoImage != null
                                && _hoImage.IsInitialized())
                                if (_HwndCtrller != null)
                                    _HwndCtrller.SetViewMode(ProVision.InteractiveROI.HWndCtrller.VIEW_MODE_ZOOM);
                        break;
                    case "BCHKI_MAGNIFY":
                        if (bchki.Checked)
                            if (_hoImage != null
                                && _hoImage.IsInitialized())
                                if (_HwndCtrller != null)
                                    _HwndCtrller.SetViewMode(ProVision.InteractiveROI.HWndCtrller.VIEW_MODE_MAGNIFY);
                        break;
                    case "BCHKI_MODIFYREGION":
                        if (bchki.Checked)
                        {
                            if (_hoImage != null
                                  && _hoImage.IsInitialized())
                                if (_HwndCtrller != null)
                                    _HwndCtrller.UnRegisterHwndCtrlMouseEvents();

                            //有可修改区域
                            if (_isUpdated)
                            {
                                _isModifying = true;
                                _isModified = false;
                                ModifyRegion();
                                bchki.Checked = false;
                            }
                            else
                            {
                                bool isChs = LanguageVersion == ProCommon.Communal.Language.Chinese;
                                string txt = isChs ? "未选择修改区域 ！" : "No valid selected area !";
                                string caption = isChs ? "警告信息" : "Warning Message";
                                ProCommon.DerivedForm.FrmMsgBox.Show(txt, caption,
                                    ProCommon.DerivedForm.MyButtons.OK,
                                    ProCommon.DerivedForm.MyIcon.Warning, isChs);
                            }
                        }
                        else
                        {
                            if (_hoImage != null
                                && _hoImage.IsInitialized())
                            {
                                if (_HwndCtrller != null)
                                    _HwndCtrller.RegisterHwndCtrlMouseEvents();

                                _isModifying = false;
                                UpdateRegionForDisplay();
                            }
                        }
                        break;
                    default: break;
                }
            }
        }

        private void LstbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_hoModifiedRegion != null
                && _hoModifiedRegion.IsInitialized())
            {
                if (this.lstbRegion.SelectedIndex != -1)
                {
                    HalconDotNet.HOperatorSet.SelectObj(_hoModifiedRegion, out _hoSelectedRegion, this.lstbRegion.SelectedIndex + 1);
                    UpdateRegionForDisplay();
                }
            }
        }

        protected void UpdateBarButtonItem(DevExpress.XtraBars.BarBaseButtonItem bbi, ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager)
        {
            if (bbi != null
               && bbi.Tag != null)
            {
                if (resourceManager != null)
                {
                    bbi.ItemClick -= Bbi_Click;
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = bbi.Tag.ToString();
                    bbi.Caption = isChs ? resourceManager.GetString("chs_" + str) : resourceManager.GetString("en_" + str);
                    bbi.ItemClick += Bbi_Click;
                }
            }
        }
        protected void UpdateBarCheckItem(DevExpress.XtraBars.BarCheckItem bchki, ProCommon.Communal.Language lan, System.Resources.ResourceManager resourceManager)
        {
            if (bchki != null
              && bchki.Tag != null)
            {
                if (resourceManager != null)
                {
                    bchki.CheckedChanged -= Bchki_CheckedChanged;
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = bchki.Tag.ToString();
                    bchki.Caption = isChs ? resourceManager.GetString("chs_" + str) : resourceManager.GetString("en_" + str);
                    bchki.CheckedChanged += Bchki_CheckedChanged;
                }
            }
        }

        private void FrmDefineAndModifyRegion_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Modifiers)
            {
                case Keys.Alt:
                    _isAltKeyPressed = false;
                    break;
                case Keys.Control:
                    _isCtrlKeyPressed = false;
                    break;
                default:
                    break;
            }
        }

        private void FrmDefineAndModifyRegion_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Modifiers)
            {
                case Keys.Alt:
                    _isAltKeyPressed = true;
                    break;
                case Keys.Control:
                    _isCtrlKeyPressed = true;
                    break;
                default:
                    break;
            }
        }

        private void FrmDefineAndModifyRegion_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool isChs = LanguageVersion == ProCommon.Communal.Language.Chinese;
            string txt = isChs ? "修改区域未完成!\r\n是否离开?" : "Modifying area not finished!\r\n Exit now ?";
            string caption = isChs ? "询问信息" : "Question Message";

            if (_isTransmitted)
            {
                if (!_isModified)
                {
                    if (!(ProCommon.DerivedForm.FrmMsgBox.Show(txt, caption,
                         ProCommon.DerivedForm.MyButtons.YesNo,
                         ProCommon.DerivedForm.MyIcon.Question, isChs) == DialogResult.Yes))
                        e.Cancel = true;
                }
                else
                {
                    txt = isChs ? "修改区域完成!" : "Erasing area finished!";
                    caption = isChs ? "提示信息" : "Prompt Message";
                    ProCommon.DerivedForm.FrmMsgBox.Show(txt, caption,
                         ProCommon.DerivedForm.MyButtons.OK,
                         ProCommon.DerivedForm.MyIcon.Information, isChs);
                }
            }
        }

        private void FrmDefineAndModifyRegion_SizeChanged(object sender, EventArgs e)
        {
            bool isChs = (LanguageVersion == ProCommon.Communal.Language.Chinese);
            string txt = isChs ? "窗体尺寸变化" : "Form size changed";
            string caption = isChs ? "错误信息" : "Error Message";
            try
            {
                switch (this.WindowState)
                {
                    case FormWindowState.Maximized:
                    case FormWindowState.Normal:
                        {
                            if (_hoImage != null
                                && _hoImage.IsInitialized())
                            {
                                if (_HwndCtrller != null)
                                    _HwndCtrller.Repaint();
                            }
                        }
                        break;
                    default: break;
                }

            }
            catch (HalconDotNet.HalconException hex)
            {

            }
        }

        private void FrmDefineAndModifyRegion_Load(object sender, EventArgs e)
        {
            UpdateControl();
        }

        private void LoadImage()
        {
            bool isChs = (LanguageVersion == ProCommon.Communal.Language.Chinese);

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            string txt = isChs ? "图像文件" : "Image File";
            string caption = isChs ? "错误信息" : "Error Message";

            ofd.Filter = txt + "(*.BMP,*.JPG,*.JPEG,*.TIF)|*.bmp;*.jpg;*.jpeg;*.tif";
            ofd.FilterIndex = 0;
            txt = isChs ? "请选择一张图像文件" : "Select an image file";
            ofd.Title = txt;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (_hoImage != null
                    && _hoImage.IsInitialized())
                    _hoImage.Dispose();

                try
                {
                    if (_HwndCtrller != null)
                    {
                        _HwndCtrller.ClearEntities();
                        if (_ROIManager != null)
                            _ROIManager.ROIList.Clear();

                        HalconDotNet.HOperatorSet.ReadImage(out _hoImage, new HalconDotNet.HTuple(ofd.FileName));

                        HalconDotNet.HOperatorSet.GetImageSize(_hoImage, out _imgWidth, out _imgHeight);
                        HalconDotNet.HOperatorSet.SetSystem("tsp_height", _imgHeight);
                        HalconDotNet.HOperatorSet.SetSystem("tsp_width", _imgWidth);

                        _HwndCtrller.AddHobjEntity(_hoImage);
                        _HwndCtrller.Repaint();
                    }

                }
                catch (HalconDotNet.HalconException hex)
                {
                    txt = isChs ? "加载图像失败!\r\n" : "Load image failed!\r\n";
                    ProCommon.DerivedForm.FrmMsgBox.Show(txt + hex.Message, caption,
                        ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Error, isChs);
                }
            }
        }

        private void ModifyRegion()
        {
            bool isChs = LanguageVersion == ProCommon.Communal.Language.Chinese;
            string txt = isChs ? "修改区域异常!" : "Error occured when modify region!";
            string caption = isChs ? "警告信息" : "Warning Message";
            HalconDotNet.HTuple mr, mc, mbtntype;
            HalconDotNet.HObject tmpModify = null;
            HalconDotNet.HObject tmpKeep = null, tmpSelect = null;

            HalconDotNet.HOperatorSet.GenEmptyObj(out tmpKeep);
            HalconDotNet.HOperatorSet.GenEmptyObj(out tmpSelect);

            int cnt = 0;
            this.hwndcDisplay.Focus();
            UpdateRegionForDisplay();

            while (_isModifying)
            {
                try
                {
                    HalconDotNet.HOperatorSet.GetMposition(this.hwndcDisplay.HalconWindow, out mr, out mc, out mbtntype);

                    if (_hoBrushRegion != null
                               && _hoBrushRegion.IsInitialized())
                        _hoBrushRegion.Dispose();

                    HalconDotNet.HOperatorSet.GenRectangle1(out _hoBrushRegion, mr - _zoomFactorForBrush, mc - _zoomFactorForBrush, mr + _zoomFactorForBrush, mc + _zoomFactorForBrush);

                    UpdateRegionForDisplay();
                    if (mbtntype.TupleEqual(new HalconDotNet.HTuple(1))) //按下鼠标左键
                    {
                        HalconDotNet.HOperatorSet.Difference(_hoSelectedRegion, _hoBrushRegion, out tmpModify);

                        _hoSelectedRegion.Dispose();
                        _hoSelectedRegion = tmpModify;

                        //若初始区域为数组型
                        cnt = _hoModifiedRegion.CountObj();

                        for (int i = 0; i < cnt; i++)
                        {
                            tmpSelect.Dispose();

                            //未选择的区域,不修改,需要保留
                            if (i!= this.lstbRegion.SelectedIndex)
                            {
                                HalconDotNet.HOperatorSet.SelectObj(_hoModifiedRegion, out tmpSelect, i + 1);
                            }
                            //选择的区域,更新为上述修改后的区域
                            else
                            {
                                HalconDotNet.HOperatorSet.CopyObj(_hoSelectedRegion, out tmpSelect, 1, 1);
                            }

                            HalconDotNet.HOperatorSet.ConcatObj(tmpKeep, tmpSelect, out tmpKeep);
                        }

                        tmpSelect.Dispose();

                        _hoModifiedRegion.Dispose();
                        _hoModifiedRegion = tmpKeep;

                        HalconDotNet.HOperatorSet.WaitSeconds(0.001);

                    }
                    else if (mbtntype.TupleEqual(new HalconDotNet.HTuple(4))) //按下鼠标右键
                    {
                        _isModifying = false;

                        if (_hoBrushRegion != null
                              && _hoBrushRegion.IsInitialized())
                            _hoBrushRegion.Dispose();

                        //UpdateRegionForDisplay();

                        _isModified = true;
                    }

                }
                catch (HalconDotNet.HalconException hex)
                {
                    //_isModifying = false;
                    //ProCommon.DerivedForm.FrmMsgBox.Show(txt + "\r\n" + hex.Message, caption,
                    //    ProCommon.DerivedForm.MyButtons.OK,
                    //    ProCommon.DerivedForm.MyIcon.Error, isChs);
                }

                //System.Threading.Thread.Sleep(1);
                Application.DoEvents();
            }            
        }

        /// <summary>
        /// 更新显示图形变量
        /// [图像,修改后区域,选择区域,以及笔刷区域]
        /// </summary>
        private void UpdateRegionForDisplay()
        {
            if (_HwndCtrller != null)
            {
                //图形变量清空
                _HwndCtrller.ClearEntities();

                //重新添加图像
                _HwndCtrller.AddHobjEntity(_hoImage);

                //修改模式下,清空ROIList,只显示不响应鼠标事件的图形变量
                if (_isUpdated
                    ||_isModifying
                    || _isModified)
                {
                    //窗体复位,ROI清空
                    _HwndCtrller.ResetAll();

                    //修改后区域和其颜色
                    if (_hoModifiedRegion != null
                      && _hoModifiedRegion.IsInitialized())
                    {
                        _HwndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_COLOR, _colorForModifiedRegion);
                        _HwndCtrller.AddHobjEntity(_hoModifiedRegion);
                    }

                    //选择区域(被修改的部分)和其颜色
                    if (_hoSelectedRegion != null
                      && _hoSelectedRegion.IsInitialized())
                    {
                        _HwndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_COLOR, _colorForSelectedRegion);
                        _HwndCtrller.AddHobjEntity(_hoSelectedRegion);
                    }

                    //笔刷区域和其颜色
                    if (_hoBrushRegion != null
                      && _hoBrushRegion.IsInitialized())
                    {
                        _HwndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_COLOR, _colorForBrushRegion);
                        _HwndCtrller.AddHobjEntity(_hoBrushRegion);
                    }
                }

                _HwndCtrller.Repaint();
            }
        }
    }
}