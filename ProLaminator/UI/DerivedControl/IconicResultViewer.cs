using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProLaminator.UI.DerivedControl
{
    public partial class IconicResultViewer : DevExpress.XtraEditors.XtraUserControl
    {
        private IconicResultViewer()
        {
            InitializeComponent();
            this.Load += IconicResultViewer_Load;
        }

        public IconicResultViewer(ProCommon.Communal.Language lan, ProLaminator.UI.DerivedControl.MultiViewModule ownerModule, string camId) : this()
        {
            LangVersion = lan;
            _ownerModule = ownerModule;
            _camerID = camId;
            this.Tag = _camerID;
        }

        public ProCommon.Communal.Language LangVersion { private set; get; }

        /// <summary>
        /// 所属模块
        /// </summary>
        private ProLaminator.UI.DerivedControl.MultiViewModule _ownerModule;

        /// <summary>
        /// 图形结果管理器
        /// </summary>
        private ProVision.InteractiveROI.HWndCtrller _hwndCtrller;

        /// <summary>
        /// ROI管理器
        /// </summary>
        private ProVision.InteractiveROI.ROIManager _roiMgr;

        private HalconDotNet.HObject _hoImage;
        private HalconDotNet.HObject _tstImage;

        private HalconDotNet.HTuple _imgWidth, _imgHeight;
        private ProVision.InteractiveROI.ROIRectangle1 _rectangle1;
        private ProVision.InteractiveROI.ROIRectangle2 _rectangle2;
        private ProVision.InteractiveROI.ROICircle _circle;
        private ProVision.InteractiveROI.ROICross _cross;
        private System.Collections.Generic.List<DisplayResult> _displayResultList;

        private ProLaminator.Device.Camera _cameraSpecidied;
        private ProLaminator.Config.CfgVisionPara _cfgVisionPara;
        private string _camerID;

        /// <summary>
        /// 是否单击标记
        /// </summary>
        private bool _isSingleClick;

        /// <summary>
        /// 窗体Tag
        /// </summary>
        private string _hWndTag;

        /// <summary>
        /// 最近时刻
        /// </summary>
        private System.DateTime _dateTimeLast;

        private void IconicResultViewer_Load(object sender, EventArgs e)
        {
            InitFieldAndProperty();
            InitConrtol();
        }

        private void InitFieldAndProperty()
        {
            if (_hwndCtrller == null)
                _hwndCtrller = new ProVision.InteractiveROI.HWndCtrller(this.hwndctrlDisplay);

            if (_roiMgr == null)
                _roiMgr = new ProVision.InteractiveROI.ROIManager();

            _hwndCtrller.RegisterROICtroller(_roiMgr);
            _hwndCtrller.SetViewMode(ProVision.InteractiveROI.HWndCtrller.VIEW_MODE_NONE);
            _hwndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_DRAWMODE, "margin");

            _hwndCtrller.RegisterHwndCtrlMouseEvents();
            this.hwndctrlDisplay.HMouseDown += HWndcMain_HMouseDown;
            this.hwndctrlDisplay.Tag = this.Tag;
        }

        private void InitConrtol()
        {
            UpdateControl();
        }

        private void UpdateControl()
        {
            UpdateBarButtonItem(this.bbiRectangle1, LangVersion, null);
            UpdateBarButtonItem(this.bbiRectangle2, LangVersion, null);
            UpdateBarButtonItem(this.bbiCircle, LangVersion, null);
            UpdateBarButtonItem(this.bbiCenterCross, LangVersion, null);
            UpdateBarButtonItem(this.bbiClearIconic, LangVersion, null);

            UpdateLabelControl(this.lblcProOK, LangVersion, null);
            UpdateLabelControl(this.lblcProNG, LangVersion, null);
            UpdateLabelControl(this.lblcProTotal, LangVersion, null);
            UpdateLabelControl(this.lblcProYieldRatio, LangVersion, null);
            UpdateLabelControl(this.lblcElapseTime, LangVersion, null);

            UpdateSimpleButton(this.sbtnAcquireOnce, LangVersion, null);
            UpdateSimpleButton(this.sbtnCountClear, LangVersion, null);
        }

        private void Bbi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (e.Item.Tag.ToString())
            {
                case "BBI_RECTANGLE1":
                    if (_tstImage != null
                       && _tstImage.IsInitialized())
                    {
                        if (_roiMgr != null)
                        {
                            _roiMgr.ROIList.Clear();
                            _rectangle1 = null;
                            _rectangle2 = null;
                            _circle = null;
                            _cross = null;

                            if (_rectangle1 == null)
                                _rectangle1 = new ProVision.InteractiveROI.ROIRectangle1();

                            HalconDotNet.HOperatorSet.GetImageSize(_tstImage, out _imgWidth, out _imgHeight);
                            _rectangle1.CreateROI(_imgHeight / 2, _imgWidth / 2);
                            _roiMgr.SetROIShape(_rectangle1);

                            _hwndCtrller.Repaint();
                        }
                    }

                    break;
                case "BBI_RECTANGLE2":
                    if (_tstImage != null
                        && _tstImage.IsInitialized())
                    {
                        if (_roiMgr != null)
                        {
                            _roiMgr.ROIList.Clear();
                            _rectangle1 = null;
                            _rectangle2 = null;
                            _circle = null;
                            _cross = null;

                            if (_rectangle2 == null)
                                _rectangle2 = new ProVision.InteractiveROI.ROIRectangle2();

                            HalconDotNet.HOperatorSet.GetImageSize(_tstImage, out _imgWidth, out _imgHeight);
                            _rectangle2.CreateROI(_imgHeight / 2, _imgWidth / 2);
                            _roiMgr.SetROIShape(_rectangle2);

                            _hwndCtrller.Repaint();
                        }
                    }

                    break;
                case "BBI_CIRCLE":
                    if (_tstImage != null
                       && _tstImage.IsInitialized())
                    {
                        if (_roiMgr != null)
                        {
                            _roiMgr.ROIList.Clear();
                            _rectangle1 = null;
                            _rectangle2 = null;
                            _circle = null;
                            _cross = null;

                            if (_circle == null)
                                _circle = new ProVision.InteractiveROI.ROICircle();

                            HalconDotNet.HOperatorSet.GetImageSize(_tstImage, out _imgWidth, out _imgHeight);
                            _circle.CreateROI(_imgHeight / 2, _imgWidth / 2);
                            _roiMgr.SetROIShape(_circle);

                            _hwndCtrller.Repaint();
                        }
                    }

                    break;
                case "BBI_CENTERCROSS":
                    if (_tstImage != null
                       && _tstImage.IsInitialized())
                    {
                        if (_roiMgr != null)
                        {
                            _roiMgr.ROIList.Clear();
                            _rectangle1 = null;
                            _rectangle2 = null;
                            _circle = null;
                            _cross = null;

                            if (_cross == null)
                                _cross = new ProVision.InteractiveROI.ROICross();

                            HalconDotNet.HOperatorSet.GetImageSize(_tstImage, out _imgWidth, out _imgHeight);
                            _cross.CreateROI(_imgHeight / 2, _imgWidth / 2);
                            _roiMgr.SetROIShape(_cross);

                            _hwndCtrller.Repaint();
                        }
                    }
                    break;
                case "BBI_CLEARICONIC":
                    if (_roiMgr != null)
                    {
                        _roiMgr.ROIList.Clear();
                        _rectangle1 = null;
                        _rectangle2 = null;
                        _circle = null;
                        _cross = null;

                        _hwndCtrller.Repaint();
                    }
                    break;
                default: break;
            }
        }

        private void Sbtn_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton sbtn = sender as DevExpress.XtraEditors.SimpleButton;
            if (sbtn != null)
            {
                switch (sbtn.Tag.ToString())
                {
                    case "SBTN_COUNTCLEAR":
                        {
                            this.btneProOK.EditValue = 0;
                            this.btneProNG.EditValue = 0;
                            this.btneProTotal.EditValue = 0;
                            this.btneProYieldRatio.EditValue = 0;
                            this.btneElapse.Text = "";

                            if (_camerID == ProLaminator.Logic.SystemManager.Instance.CamPropertyForGlass.ID)
                            {
                                ProLaminator.Config.CfgManager.Instance.CfgSys.GlassProTotal = 0;
                                ProLaminator.Config.CfgManager.Instance.CfgSys.GlassProOK = 0;
                                ProLaminator.Config.CfgManager.Instance.CfgSys.GlassProNG = 0;
                                ProLaminator.Config.CfgManager.Instance.CfgSys.GlassProYieldRatio = 0.0f;

                            }
                            else if (_camerID == ProLaminator.Logic.SystemManager.Instance.CamPropertyForMembrane1.ID)
                            {
                                ProLaminator.Config.CfgManager.Instance.CfgSys.Membrane1ProTotal = 0;
                                ProLaminator.Config.CfgManager.Instance.CfgSys.Membrane1ProOK = 0;
                                ProLaminator.Config.CfgManager.Instance.CfgSys.Membrane1ProNG = 0;
                                ProLaminator.Config.CfgManager.Instance.CfgSys.Membrane1ProYieldRatio = 0.0f;

                            }
                            else if (_camerID == ProLaminator.Logic.SystemManager.Instance.CamPropertyForMembrane2.ID)
                            {
                                ProLaminator.Config.CfgManager.Instance.CfgSys.Membrane2ProTotal = 0;
                                ProLaminator.Config.CfgManager.Instance.CfgSys.Membrane2ProOK = 0;
                                ProLaminator.Config.CfgManager.Instance.CfgSys.Membrane2ProNG = 0;
                                ProLaminator.Config.CfgManager.Instance.CfgSys.Membrane2ProYieldRatio = 0.0f;
                            }
                        }
                        break;
                    case "SBTN_ACQUIREONCE":
                        {
                            ProLaminator.Logic.SystemManager.Instance.IsRunOnce = true;
                            ProLaminator.Logic.SystemManager.Instance.IsRunning = true;

                            _cameraSpecidied = ProLaminator.Device.CameraManager.Instance.CameraList[_camerID];
                            _cfgVisionPara = _ownerModule.CfgMgr.CfgVsPara;

                            ProLaminator.Device.CameraManager.Instance.SetIsInitProcess(true);
                            float expt = 0.0f, gain = 0.0f, gamma = 0.0f;

                            if (_camerID == ProLaminator.Logic.SystemManager.Instance.CamPropertyForGlass.ID)
                            {
                                expt = _cfgVisionPara.ParaForGlass.CameraExposure;
                                gain = _cfgVisionPara.ParaForGlass.CameraGain;
                                gamma = _cfgVisionPara.ParaForGlass.Gamma;
                            }
                            else if (_camerID == ProLaminator.Logic.SystemManager.Instance.CamPropertyForMembrane1.ID)
                            {
                                expt = _cfgVisionPara.ParaForMembrane1.CameraExposure;
                                gain = _cfgVisionPara.ParaForMembrane1.CameraGain;
                                gamma = _cfgVisionPara.ParaForMembrane1.Gamma;
                            }
                            else if (_camerID == ProLaminator.Logic.SystemManager.Instance.CamPropertyForMembrane2.ID)
                            {
                                expt = _cfgVisionPara.ParaForMembrane2.CameraExposure;
                                gain = _cfgVisionPara.ParaForMembrane2.CameraGain;
                                gamma = _cfgVisionPara.ParaForMembrane2.Gamma;
                            }

                            _cameraSpecidied.API.SetExposureTime(expt);
                            _cameraSpecidied.API.SetGain(gain);
                            _cameraSpecidied.Property.EnableAlgorithm = true;
                            _cameraSpecidied.API.SetAcquisitionMode(ProCommon.Communal.AcquisitionMode.SoftTrigger, 1);
                            _cameraSpecidied.API.SoftTriggerOnce();
                        }
                        break;
                    default: break;
                }
            }
        }

        /// <summary>
        /// 鼠标按下事件回调
        /// [切换多视图与单视图]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HWndcMain_HMouseDown(object sender, HalconDotNet.HMouseEventArgs e)
        {
            if (ConvertHWndCtrlMouseSingleToDoubleClick(sender, e))
            {
                //左键双击，则切换窗口
                if (_ownerModule != null)
                    _ownerModule.ShowSingleViewer(_camerID);
            }
        }

        /// <summary>
        /// 更新结果图形变量
        /// [原始图像,搜索区域或跟随区域]
        /// </summary>
        /// <param name="resultData"></param>
        public void UpdateIconicResult(ProLaminator.Data.LaminatorProcessData resultData)
        {
            try
            {
                if (_hwndCtrller != null)
                {
                    if (resultData != null)
                    {
                        this.Invoke(
                            new System.Windows.Forms.MethodInvoker(
                                () =>
                                {
                                    _hwndCtrller.ClearEntities();
                                    _hoImage = resultData.RawImage;

                                    if (_tstImage != null
                                     && _tstImage.IsInitialized())
                                        _tstImage.Dispose();

                                    _tstImage = _hoImage.Clone();
                                    _hwndCtrller.AddHobjEntity(_tstImage);

                                    _hwndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_COLOR, "red");
                                    _hwndCtrller.AddHobjEntity(resultData.InspetcArea);

                                    _hwndCtrller.ChangeGraphicSettings(ProVision.InteractiveROI.GraphicContext.GC_COLOR, "green");
                                    _hwndCtrller.AddHobjEntity(resultData.ResultRegion);

                                    _hwndCtrller.Repaint();

                                    this.btneProOK.EditValue = resultData.ProductOKNumber;
                                    this.btneProNG.EditValue = resultData.ProductNGNumber;
                                    this.btneProTotal.EditValue = resultData.ProductTotalNumber;
                                    this.btneProYieldRatio.EditValue = resultData.ProductYieldRatio * 100;
                                    this.btneElapse.Text = System.Math.Round(resultData.ElapseTime, 2).ToString();

                                    this.lblRunState.Text = resultData.RunState;
                                    this.lblInspectResult.Text = resultData.ImgResultOK ? "OK" : "NG";
                                    this.lblInspectResult.ForeColor = resultData.ImgResultOK ? System.Drawing.Color.Green : System.Drawing.Color.Red;

                                    string r = System.Math.Round(resultData.Row.D, 2).ToString();
                                    string c = System.Math.Round(resultData.Col.D, 2).ToString();
                                    string a = System.Math.Round(resultData.DeltaAglRad.D, 2).ToString();

                                    string x = resultData.DeltaX.ToString();
                                    string y = resultData.DeltaY.ToString();
                                    string da = resultData.DeltaAglDegree.ToString();
                                    string flg = resultData.ResultFlag.ToString("00");

                                    if (_displayResultList == null)
                                        _displayResultList = new List<DisplayResult>();

                                    if (_displayResultList.Count >0)
                                    {
                                        _displayResultList.Insert(0, new DisplayResult() { Row = r, COl = c, Angle = a, DeltaX = x, DeltaY = y, DeltaA = da, Flag = flg });

                                        if (_displayResultList.Count >3)
                                            _displayResultList.RemoveAt(3);
                                    }
                                    else
                                    {
                                        _displayResultList.Add(new DisplayResult() { Row = r, COl = c, Angle = a, DeltaX = x, DeltaY = y, DeltaA = da, Flag = flg });
                                    }                                 

                                    for(int i=0;i< _displayResultList.Count;i++)
                                    {
                                        if(i==0)
                                        {
                                            this.meLog.Text = ">>>";
                                            this.meLog.Text += "定位坐标R:" + _displayResultList[i].Row + ",坐标C:" + _displayResultList[i].COl + ",弧角A:" + _displayResultList[i].Angle + "\r\n";
                                            this.meLog.Text += "相对偏移X:" + _displayResultList[i].DeltaX + ",Y:" + _displayResultList[i].DeltaY + "角D:" + _displayResultList[i].DeltaA + "\r\n";
                                            this.meLog.Text += "结果标记:" + _displayResultList[i].Flag;
                                        }
                                        else
                                        {
                                            this.meLog.Text += "\r\n>>>";
                                            this.meLog.Text += "定位坐标R:" + _displayResultList[i].Row + ",坐标C:" + _displayResultList[i].COl + ",弧角A:" + _displayResultList[i].Angle + "\r\n";
                                            this.meLog.Text += "相对偏移X:" + _displayResultList[i].DeltaX + ",Y:" + _displayResultList[i].DeltaY + "角D:" + _displayResultList[i].DeltaA + "\r\n";
                                            this.meLog.Text += "结果标记:" + _displayResultList[i].Flag;
                                        }
                                    }
                                }));
                    }
                }
            }
            catch (HalconDotNet.HalconException hex)
            {

            }
            catch (System.Exception ex)
            {

            }
        }

        #region 辅助函数

        /// <summary>
        /// 更新BarButtonItem控件
        /// </summary>
        /// <param name="bbi"></param>
        /// <param name="lan"></param>
        private void UpdateBarButtonItem(DevExpress.XtraBars.BarButtonItem bbi, ProCommon.Communal.Language lan, System.Resources.ResourceManager resrcMgr)
        {
            if (bbi != null
              && bbi.Tag != null)
            {
                bbi.ItemClick -= Bbi_ItemClick;

                if (resrcMgr != null)
                {
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = bbi.Tag.ToString();
                    bbi.Caption = isChs ? resrcMgr.GetString("chs_" + str) : resrcMgr.GetString("en_" + str);
                }
                bbi.ItemClick += Bbi_ItemClick;
            }
        }

        /// <summary>
        /// 更新LabelControl控件
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="lan"></param>
        private void UpdateLabelControl(DevExpress.XtraEditors.LabelControl lbl, ProCommon.Communal.Language lan, System.Resources.ResourceManager resrcMgr)
        {
            if (lbl != null
             && lbl.Tag != null)
            {
                if (resrcMgr != null)
                {
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = lbl.Tag.ToString();
                    lbl.Text = isChs ? resrcMgr.GetString("chs_" + str) : resrcMgr.GetString("en_" + str);
                }
            }
        }

        private void UpdateSimpleButton(DevExpress.XtraEditors.SimpleButton sbtn, ProCommon.Communal.Language lan, System.Resources.ResourceManager resrcMgr)
        {
            if (sbtn != null
             && sbtn.Tag != null)
            {
                sbtn.Click -= Sbtn_Click;
                if (resrcMgr != null)
                {
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = sbtn.Tag.ToString();
                    sbtn.Text = isChs ? resrcMgr.GetString("chs_" + str) : resrcMgr.GetString("en_" + str);
                }

                sbtn.Click += Sbtn_Click;
            }
        }

        private bool ConvertHWndCtrlMouseSingleToDoubleClick(object sender, HalconDotNet.HMouseEventArgs e)
        {
            HalconDotNet.HWindowControl hwdctrl = sender as HalconDotNet.HWindowControl;
            if (hwdctrl != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (_isSingleClick)//左键单击,已记录单击一次
                    {
                        if (_hWndTag == hwdctrl.Tag.ToString()) //再次单击时，同一个窗体
                        {
                            TimeSpan span = DateTime.Now - _dateTimeLast;
                            if (span.Milliseconds + 100 <= SystemInformation.DoubleClickTime) //与上次单击记录的时间间隔小于系统定义双击时间,则重置单击记录，判定为双击
                            {
                                _isSingleClick = false; //左键单击记录复位
                                return true;
                            }
                            else
                            { return false; }
                        }
                        else { return false; }

                    } //左键单击，未记录单击一次，则当前记录单击一次，判定为非双击
                    else
                    {
                        _isSingleClick = true; //单击一次
                        _dateTimeLast = DateTime.Now;
                        _hWndTag = hwdctrl.Tag.ToString();
                        return false;
                    }
                }//非左键单击,判定为非双击
                else { return false; }
            }
            else { return false; }
        }

        /// <summary>
        /// 切换控件使能状态
        /// </summary>
        /// <param name="isEnable"></param>
        public void EnableControl(bool isEnable)
        {
            this.sbtnAcquireOnce.Enabled = isEnable;
            this.sbtnCountClear.Enabled = isEnable;
        }
        #endregion
    }

    internal class DisplayResult
    {
        public string Row;
        public string COl;
        public string Angle;
        public string DeltaX;
        public string DeltaY;
        public string DeltaA;
        public string Flag;
    }
}
