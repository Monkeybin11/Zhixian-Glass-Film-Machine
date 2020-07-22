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
    public partial class CameraStationSet : DevExpress.XtraEditors.XtraUserControl
    {
        public ProCommon.Communal.Language LangVersion { private set; get; }

        /// <summary>
        /// 所属模块
        /// </summary>
        /// </summary>
        private ProLaminator.UI.DerivedControl.MultiViewModule _ownerModule;
        private ProVision.MatchModel.FrmMatchModel _frmMatch;

        /// <summary>
        /// 图形结果管理器
        /// </summary>
        private ProVision.InteractiveROI.HWndCtrller _hwndCtrller;

        /// <summary>
        /// ROI管理器
        /// </summary>
        private ProVision.InteractiveROI.ROIManager _roiMgr;

        private HalconDotNet.HObject _hoImage;
        private HalconDotNet.HTuple _imgWidth, _imgHeight;
        private ProVision.InteractiveROI.ROIRectangle1 _rectangle1;
        private ProVision.InteractiveROI.ROIRectangle2 _rectangle2;
        private ProVision.InteractiveROI.ROICircle _circle;
        private ProVision.InteractiveROI.ROICross _cross;

        /// <summary>
        /// 警告内容和标题
        /// </summary>
        private string _warningText, _warningCaption;
        private HalconDotNet.HTuple _imgFiles, _imgIndex;
        private HalconDotNet.HObject _tstImage;

        private int _cameraFlag;
        //相机设备
        private ProLaminator.Device.Camera _cameraSpecidied;
        private ProLaminator.Config.CfgCalibration _cfgCalibration;
        private string _cameraID;

        private float _exposureTime = 0, _cameraGain = 0, _externalTriggerDelay = 0, _externalDebouncerTime = 0;
        private float _rowUnit = 0.0f, _colUnit = 0.0f, _gamma = 0.0f;

        public string CameraID
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _cameraID = value;
                    _cameraSpecidied = null;

                    if (ProLaminator.Device.CameraManager.Instance != null
                        && ProLaminator.Device.CameraManager.Instance.CameraList != null)
                    {
                        //首先设置所有相机采集模式为非工作时的采集模式(工作时用外触发,此时设置为软触发),以便于快速响应
                        ProLaminator.Device.CameraManager.Instance.SetCameraWorkMode(false);

                        //然后相机工位为调试模式
                        ProLaminator.Device.CameraManager.Instance.IsCameraDebugList[_cameraID] = true;
                        _cameraSpecidied = ProLaminator.Device.CameraManager.Instance.CameraList[_cameraID];
                    }

                    if (_cameraSpecidied != null)
                    {
                        this.tlstrptxtCamerID.Text = _cameraSpecidied.Property.Name;
                        this.tlstrptxtCamerID.BackColor = System.Drawing.Color.GreenYellow;
                    }

                    _cfgVisionPara = _ownerModule.CfgMgr.CfgVsPara;
                    _cfgCalibration = _ownerModule.CfgMgr.CfgCal;

                    _rowUnit = 0.0f;
                    _colUnit = 0.0f;

                    if (_cfgCalibration != null
                        && _cfgCalibration.CalSolutionList != null)
                    {
                        _rowUnit = _cfgCalibration.CalSolutionList[_cameraID].RowUnit;
                        _colUnit = _cfgCalibration.CalSolutionList[_cameraID].ColUnit;
                    }

                    _cameraFlag = -1;

                    if (_cameraID == ProLaminator.Logic.SystemManager.Instance.CamPropertyForGlass.ID)
                    {
                        _cameraFlag = 0;
                        _pathForSaveLocPara = _cfgVisionPara.ParaForGlass.PathForSaveLocPara;
                    }
                    else if (_cameraID == ProLaminator.Logic.SystemManager.Instance.CamPropertyForMembrane1.ID)
                    {
                        _cameraFlag = 1;
                        _pathForSaveLocPara = _cfgVisionPara.ParaForMembrane1.PathForSaveLocPara;

                    }
                    else if (_cameraID == ProLaminator.Logic.SystemManager.Instance.CamPropertyForMembrane2.ID)
                    {
                        _cameraFlag = 2;
                        _pathForSaveLocPara = _cfgVisionPara.ParaForMembrane2.PathForSaveLocPara;
                    }

                    //更新参数相关控件
                    UpdateParameterControl(_cameraFlag);
                }
            }

            get { return _cameraID; }
        }

        /// <summary>
        /// 更新参数相关控件
        /// </summary>
        /// <param name="camFlag"></param>
        private void UpdateParameterControl(int camFlag)
        {
            switch (camFlag)
            {
                case 0:
                    if (_cfgVisionPara != null
                        && _cfgVisionPara.ParaForGlass != null)
                    {
                        this.chkeSaveAll.Checked = _cfgVisionPara.ParaForGlass.IsSaveImageAll;
                        this.chkeSaveOK.Checked = _cfgVisionPara.ParaForGlass.IsSaveImageOK;
                        this.chkeSaveNG.Checked = _cfgVisionPara.ParaForGlass.IsSaveImageNG;

                        _exposureTime = _cfgVisionPara.ParaForGlass.CameraExposure;
                        _cameraGain = _cfgVisionPara.ParaForGlass.CameraGain;
                        _gamma = _cfgVisionPara.ParaForGlass.Gamma;
                        _externalTriggerDelay = _cfgVisionPara.ParaForGlass.ExternalTriggerDelay;
                        _externalDebouncerTime = _cfgVisionPara.ParaForGlass.ExternalTriggerDebouncerTime;
                    }

                    break;
                case 1:
                    if (_cfgVisionPara != null
                       && _cfgVisionPara.ParaForMembrane1 != null)
                    {
                        this.chkeSaveAll.Checked = _cfgVisionPara.ParaForMembrane1.IsSaveImageAll;
                        this.chkeSaveOK.Checked = _cfgVisionPara.ParaForMembrane1.IsSaveImageOK;
                        this.chkeSaveNG.Checked = _cfgVisionPara.ParaForMembrane1.IsSaveImageNG;

                        _exposureTime = _cfgVisionPara.ParaForMembrane1.CameraExposure;
                        _cameraGain = _cfgVisionPara.ParaForMembrane1.CameraGain;
                        _gamma = _cfgVisionPara.ParaForMembrane1.Gamma;
                        _externalTriggerDelay = _cfgVisionPara.ParaForMembrane1.ExternalTriggerDelay;
                        _externalDebouncerTime = _cfgVisionPara.ParaForMembrane1.ExternalTriggerDebouncerTime;
                    }
                    break;
                case 2:
                    if (_cfgVisionPara != null
                       && _cfgVisionPara.ParaForMembrane2 != null)
                    {
                        this.chkeSaveAll.Checked = _cfgVisionPara.ParaForMembrane2.IsSaveImageAll;
                        this.chkeSaveOK.Checked = _cfgVisionPara.ParaForMembrane2.IsSaveImageOK;
                        this.chkeSaveNG.Checked = _cfgVisionPara.ParaForMembrane2.IsSaveImageNG;

                        _exposureTime = _cfgVisionPara.ParaForMembrane2.CameraExposure;
                        _cameraGain = _cfgVisionPara.ParaForMembrane2.CameraGain;
                        _gamma = _cfgVisionPara.ParaForMembrane2.Gamma;
                        _externalTriggerDelay = _cfgVisionPara.ParaForMembrane2.ExternalTriggerDelay;
                        _externalDebouncerTime = _cfgVisionPara.ParaForMembrane2.ExternalTriggerDebouncerTime;
                    }
                    break;
                default: break;
            }


            this.speExposureTime.EditValue = _exposureTime;
            this.speGain.EditValue = _cameraGain;
            this.speGamma.EditValue = _gamma;

            this.speTriggerDelay.EditValue = _externalTriggerDelay;
            this.speDebouncerTime.EditValue = _externalDebouncerTime;
            this.speRowUnit.EditValue = _rowUnit;
            this.speColUnit.EditValue = _colUnit;
            
            if(_cfgVisionPara!=null)
            {
                if (_cfgVisionPara.CoordinateReferenceMode == 0)
                    this.chkeModelReference.Checked = true;
                else if (_cfgVisionPara.CoordinateReferenceMode == 1)
                    this.chkeCameraCenterReference.Checked = true;
            }
                       
        }

        /// <summary>
        /// 视觉参数
        /// </summary>
        private ProLaminator.Config.CfgVisionPara _cfgVisionPara;

        /// <summary>
        /// 保存程式定位参数的目录路径
        /// </summary>
        private string _pathForSaveLocPara;

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

        /// <summary>
        /// 灰度值
        /// </summary>
        private HalconDotNet.HTuple _grayValue;

        /// <summary>
        /// 图像通道数量
        /// </summary>
        private HalconDotNet.HTuple _channelCount;

        /// <summary>
        /// 像素坐标
        /// </summary>
        private int _coordX, _coordY;

        private CameraStationSet()
        {
            InitializeComponent();
        }

        public CameraStationSet(ProCommon.Communal.Language lan, ProLaminator.UI.DerivedControl.MultiViewModule ownerModule) : this()
        {
            LangVersion = lan;
            _ownerModule = ownerModule;
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

            //this.hwndctrlDisplay.HMouseDown += HWndcMain_HMouseDown;
            this.hwndctrlDisplay.HMouseMove += HWndcMain_HMouseMove;
            this.hwndctrlDisplay.Tag = "CameraStationSet";

            _warningText = LangVersion == ProCommon.Communal.Language.Chinese ? "禁止操作!" : "Operation forbbided!";
            _warningCaption = LangVersion == ProCommon.Communal.Language.Chinese ? "警告信息" : "Warning Message";
            _imgFiles = new HalconDotNet.HTuple();
            _imgIndex = new HalconDotNet.HTuple(0);
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

            UpdateSimpleButton(this.sbtnAcquireContinuous, LangVersion, null);
            UpdateSimpleButton(this.sbtnSetCamera, LangVersion, null);
            UpdateSimpleButton(this.sbtnLoadImage, LangVersion, null);
            UpdateSimpleButton(this.sbtnLastImage, LangVersion, null);
            UpdateSimpleButton(this.sbtnStopAcquire, LangVersion, null);
            UpdateSimpleButton(this.sbtnAcquireOnce, LangVersion, null);
            UpdateSimpleButton(this.sbtnTestOffline, LangVersion, null);
            UpdateSimpleButton(this.sbtnNextImage, LangVersion, null);
            UpdateSimpleButton(this.sbtnSaveParameter, LangVersion, null);
            UpdateSimpleButton(this.sbtnExitSet, LangVersion, null);
            UpdateSimpleButton(this.sbtnSetMatchModel, LangVersion, null);

            UpdateCheckEdit(this.chkeSaveAll, LangVersion, null);
            UpdateCheckEdit(this.chkeSaveOK, LangVersion, null);
            UpdateCheckEdit(this.chkeSaveNG, LangVersion, null);
            UpdateCheckEdit(this.chkeEnableAlgorithm, LangVersion, null);
            UpdateCheckEdit(this.chkeModelReference, LangVersion, null);
            UpdateCheckEdit(this.chkeCameraCenterReference, LangVersion, null);

            UpdateSpinEdit(this.speExposureTime, LangVersion, null);
            UpdateSpinEdit(this.speGain, LangVersion, null);
            UpdateSpinEdit(this.speGamma, LangVersion, null);
            UpdateSpinEdit(this.speTriggerDelay, LangVersion, null);
            UpdateSpinEdit(this.speDebouncerTime, LangVersion, null);
            UpdateSpinEdit(this.speRowUnit, LangVersion, null);
            UpdateSpinEdit(this.speColUnit, LangVersion, null);

            UpdateLabelControl(this.lblcCoordXPrompt, LangVersion, null);
            UpdateLabelControl(this.lblcCoordYPrompt, LangVersion, null);
            UpdateLabelControl(this.lblcCoordRPrompt, LangVersion, null);
            UpdateLabelControl(this.lblcFlagPrompt, LangVersion, null);
            UpdateLabelControl(this.lblcGlassPrompt, LangVersion, null);
            UpdateLabelControl(this.lblcMembrane1Prompt, LangVersion, null);
            UpdateLabelControl(this.lblcMembrane2Prompt, LangVersion, null);

            UpdateSimpleButton(this.sbtnWriteDataGlass, LangVersion, null);
            UpdateSimpleButton(this.sbtnWriteDataMembrane1, LangVersion, null);
            UpdateSimpleButton(this.sbtnWriteDataMembrane2, LangVersion, null);
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
                bool isChs = LangVersion == ProCommon.Communal.Language.Chinese;
                float x = 0.0f, y = 0.0f, r = 0.0f;
                int ix = 0, iy = 0, ir = 0;
                ushort f = 0;
                switch (sbtn.Tag.ToString())
                {
                    case "SBTN_ACQUIRECONTINUOUS":
                        {
                            if (_cameraSpecidied != null)
                            {
                                this.sbtnAcquireContinuous.Enabled = false;
                                this.sbtnSetCamera.Enabled = true;
                                this.sbtnLoadImage.Enabled = false;
                                this.sbtnLastImage.Enabled = false;
                                this.sbtnStopAcquire.Enabled = true;
                                this.sbtnAcquireOnce.Enabled = false;
                                this.sbtnTestOffline.Enabled = false;
                                this.sbtnNextImage.Enabled = false;

                                ProLaminator.Device.CameraManager.Instance.SetIsInitProcess(true);

                                _cameraSpecidied.API.SetExposureTime(_exposureTime);
                                _cameraSpecidied.API.SetGain(_cameraGain);
                                _cameraSpecidied.API.SetGamma(_gamma);                               
                                _cameraSpecidied.API.SetAcquisitionMode(ProCommon.Communal.AcquisitionMode.Continue, 1);                              
                            }
                            else
                            {
                                _warningText = isChs ? "无法操作!\r\n相机操作句柄异常!" : "Denied!\r\n Error occured on camera handle .";
                                ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                   ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, isChs);
                            }
                        }
                        break;
                    case "SBTN_SETCAMERA":
                        if (_cameraSpecidied != null)
                        {
                            this.sbtnAcquireContinuous.Enabled = true;
                            this.sbtnSetCamera.Enabled = true;
                            this.sbtnLoadImage.Enabled = true;
                            this.sbtnLastImage.Enabled = true;
                            this.sbtnStopAcquire.Enabled = true;
                            this.sbtnAcquireOnce.Enabled = true;
                            this.sbtnTestOffline.Enabled = true;
                            this.sbtnNextImage.Enabled = true;

                        }
                        else
                        {
                            _warningText = isChs ? "无法操作!\r\n相机操作句柄异常!" : "Denied!\r\n Error occured on camera handle .";
                            ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                               ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, isChs);
                        }
                        break;
                    case "SBTN_LOADIMAGE":
                        {
                            this.sbtnAcquireContinuous.Enabled = false;
                            this.sbtnSetCamera.Enabled = true;
                            this.sbtnLoadImage.Enabled = true;
                            this.sbtnLastImage.Enabled = true;
                            this.sbtnStopAcquire.Enabled = true;
                            this.sbtnAcquireOnce.Enabled = false;
                            this.sbtnTestOffline.Enabled = true;
                            this.sbtnNextImage.Enabled = true;

                            System.Windows.Forms.FolderBrowserDialog fbd = new FolderBrowserDialog();
                            fbd.SelectedPath = System.Environment.CurrentDirectory;

                            if (fbd.ShowDialog() == DialogResult.OK)
                            {
                                try
                                {
                                    _imgFiles = new HalconDotNet.HTuple();
                                    if (ProVision.Communal.Functions.ListImageFile(fbd.SelectedPath, new HalconDotNet.HTuple(), new HalconDotNet.HTuple(), out _imgFiles))
                                    {
                                        if (_tstImage != null
                                            && _tstImage.IsInitialized())
                                            _tstImage.Dispose();

                                        if (_imgFiles.TupleNotEqual(new HalconDotNet.HTuple()))
                                        {
                                            _imgIndex = 0;
                                            HalconDotNet.HOperatorSet.ReadImage(out _tstImage, _imgFiles.TupleSelect(_imgIndex));
                                            _hoImage = _tstImage;
                                        }

                                        if (_hwndCtrller != null)
                                        {
                                            _hwndCtrller.ClearEntities();
                                            _hwndCtrller.AddHobjEntity(_tstImage);
                                            _hwndCtrller.Repaint();
                                        }
                                    }
                                }
                                catch (HalconDotNet.HalconException hex)
                                {
                                    _warningText = isChs ? "加载图像异常!\r\n" : "An error occured while loading an image!";
                                    _warningCaption = isChs ? "错误信息" : "Error Message";
                                    ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                    ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Error, isChs);
                                }
                            }
                        }
                        break;
                    case "SBTN_LASTIMAGE":
                        {
                            if (_imgFiles.TupleNotEqual(new HalconDotNet.HTuple()))
                            {
                                if (_imgIndex.TupleLessEqual(0))
                                    _imgIndex = _imgFiles.TupleLength() - 1;
                                else
                                    _imgIndex -= 1;

                                HalconDotNet.HOperatorSet.ReadImage(out _tstImage, _imgFiles.TupleSelect(_imgIndex));
                                _hoImage = _tstImage;
                                if (_hwndCtrller != null)
                                {
                                    _hwndCtrller.ClearEntities();
                                    _hwndCtrller.AddHobjEntity(_tstImage);
                                    _hwndCtrller.Repaint();
                                }
                            }
                        }
                        break;
                    case "SBTN_STOPACQUIRE":
                        {
                            if (_cameraSpecidied != null)
                            {
                                this.sbtnAcquireContinuous.Enabled = true;
                                this.sbtnSetCamera.Enabled = true;
                                this.sbtnLoadImage.Enabled = true;
                                this.sbtnLastImage.Enabled = true;
                                this.sbtnStopAcquire.Enabled = true;
                                this.sbtnAcquireOnce.Enabled = true;
                                this.sbtnTestOffline.Enabled = true;
                                this.sbtnNextImage.Enabled = true;

                                _cameraSpecidied.API.SetAcquisitionMode(ProCommon.Communal.AcquisitionMode.SoftTrigger, 1);
                            }
                            else
                            {
                                _warningText = isChs ? "无法操作!\r\n相机操作句柄异常!" : "Denied!\r\n Error occured on camera handle .";
                                _warningCaption = isChs ? "警告信息" : "Warning Message";
                                ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                   ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, isChs);
                            }
                        }
                        break;
                    case "SBTN_ACQUIREONCE":
                        {
                            if (_cameraSpecidied != null)
                            {
                                this.sbtnAcquireContinuous.Enabled = false;
                                this.sbtnSetCamera.Enabled = true;
                                this.sbtnLoadImage.Enabled = false;
                                this.sbtnLastImage.Enabled = false;
                                this.sbtnStopAcquire.Enabled = true;
                                this.sbtnAcquireOnce.Enabled = true;
                                this.sbtnTestOffline.Enabled = false;
                                this.sbtnNextImage.Enabled = false;

                                ProLaminator.Device.CameraManager.Instance.SetIsInitProcess(true);

                                _cameraSpecidied.API.SetExposureTime(_exposureTime);
                                _cameraSpecidied.API.SetGain(_cameraGain);
                                _cameraSpecidied.API.SetGamma(_gamma);

                                _cameraSpecidied.API.SetAcquisitionMode(ProCommon.Communal.AcquisitionMode.SoftTrigger, 1);
                                _cameraSpecidied.API.SoftTriggerOnce();
                            }
                            else
                            {
                                _warningText = isChs ? "无法操作!\r\n相机操作句柄异常!" : "Denied!\r\n Error occured on camera handle .";
                                _warningCaption = isChs ? "警告信息" : "Warning Message";
                                ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                   ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, isChs);
                            }
                        }
                        break;
                    case "SBTN_TESTOFFLINE":
                        break;
                    case "SBTN_NEXTIMAGE":
                        {
                            if (_imgFiles.TupleNotEqual(new HalconDotNet.HTuple()))
                            {
                                if (_imgIndex.TupleLess(_imgFiles.TupleLength() - 1))
                                    _imgIndex += 1;
                                else
                                    _imgIndex = 0;

                                HalconDotNet.HOperatorSet.ReadImage(out _tstImage, _imgFiles.TupleSelect(_imgIndex));
                                _hoImage = _tstImage;
                                if (_hwndCtrller != null)
                                {
                                    _hwndCtrller.ClearEntities();
                                    _hwndCtrller.AddHobjEntity(_tstImage);
                                    _hwndCtrller.Repaint();
                                }
                            }
                        }
                        break;
                    case "SBTN_SETMATCHMODEL":
                        {
                            if (_tstImage != null
                                && _tstImage.IsInitialized())
                            {
                                if (_cfgVisionPara != null)
                                {
                                    if (!string.IsNullOrEmpty(_pathForSaveLocPara))
                                    {
                                        if (_frmMatch == null)
                                        {
                                            _frmMatch = new ProVision.MatchModel.FrmMatchModel(_tstImage, _pathForSaveLocPara);
                                            _frmMatch.Disposed += _frmMatch_Disposed;
                                        }

                                        _frmMatch.ShowDialog();
                                        _frmMatch.Close();
                                    }
                                    else
                                    {
                                        _warningText = isChs ? "程式定位参数路径为空!" : "No routine directory!";
                                        _warningCaption = isChs ? "警告信息" : "Warning Message";
                                        ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                           ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, isChs);
                                    }
                                }
                                else
                                {
                                    _warningText = isChs ? "未加载程式!" : "No routine loaded!";
                                    _warningCaption = isChs ? "警告信息" : "Warning Message";
                                    ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                       ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, isChs);
                                }

                                _tstImage.Dispose();
                            }
                            else
                            {
                                _warningText = isChs ? "未加载模板图像!" : "No model image loaded!";
                                _warningCaption = isChs ? "警告信息" : "Warning Message";
                                ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                   ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, isChs);
                            }
                        }
                        break;
                    case "SBTN_SAVEPARAMETER":
                        {
                            if (_cfgVisionPara != null)
                            {
                                if (!string.IsNullOrEmpty(_cfgVisionPara.RoutineDirectory))
                                {
                                    if (!string.IsNullOrEmpty(_cfgVisionPara.RoutineName))
                                    {
                                        if (!System.IO.Directory.Exists(_cfgVisionPara.RoutineDirectory))
                                        {
                                            System.IO.Directory.CreateDirectory(_cfgVisionPara.RoutineDirectory);
                                        }

                                        //删除该文件夹下所有存在的程式文件
                                        string[] files = System.IO.Directory.GetFiles(_cfgVisionPara.RoutineDirectory);
                                        if (files != null
                                            && files.Length > 0)
                                        {
                                            string fileName = System.IO.Path.GetFileNameWithoutExtension(_cfgVisionPara.RoutineDirectory + "\\" + _cfgVisionPara.RoutineName);
                                            string extention = System.IO.Path.GetExtension(_cfgVisionPara.RoutineDirectory + "\\" + _cfgVisionPara.RoutineName);

                                            System.IO.File.Delete(_cfgVisionPara.RoutineDirectory + "\\" + fileName + extention);
                                        }
                                        try
                                        {
                                            //更新变量
                                            switch (_cameraFlag)
                                            {
                                                case 0:
                                                    {
                                                        _cfgVisionPara.ParaForGlass.CameraExposure = _exposureTime;
                                                        _cfgVisionPara.ParaForGlass.CameraGain = _cameraGain;
                                                        _cfgVisionPara.ParaForGlass.Gamma = _gamma;
                                                        _cfgVisionPara.ParaForGlass.ExternalTriggerDelay = _externalTriggerDelay;
                                                        _cfgVisionPara.ParaForGlass.ExternalTriggerDebouncerTime = _externalDebouncerTime;
                                                    }
                                                    break;
                                                case 1:
                                                    {
                                                        _cfgVisionPara.ParaForMembrane1.CameraExposure = _exposureTime;
                                                        _cfgVisionPara.ParaForMembrane1.CameraGain = _cameraGain;
                                                        _cfgVisionPara.ParaForMembrane1.Gamma = _gamma;
                                                        _cfgVisionPara.ParaForMembrane1.ExternalTriggerDelay = _externalTriggerDelay;
                                                        _cfgVisionPara.ParaForMembrane1.ExternalTriggerDebouncerTime = _externalDebouncerTime;
                                                    }
                                                    break;
                                                case 2:
                                                    {
                                                        _cfgVisionPara.ParaForMembrane2.CameraExposure = _exposureTime;
                                                        _cfgVisionPara.ParaForMembrane2.CameraGain = _cameraGain;
                                                        _cfgVisionPara.ParaForMembrane2.Gamma = _gamma;
                                                        _cfgVisionPara.ParaForMembrane2.ExternalTriggerDelay = _externalTriggerDelay;
                                                        _cfgVisionPara.ParaForMembrane2.ExternalTriggerDebouncerTime = _externalDebouncerTime;
                                                    }
                                                    break;
                                                default: break;
                                            }

                                            using (var fs = new System.IO.FileStream(_cfgVisionPara.RoutineDirectory + "\\" + _cfgVisionPara.RoutineName + ".pro", System.IO.FileMode.Create))
                                            {
                                                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                                                bf.Serialize(fs, _cfgVisionPara);
                                            }

                                            //保存参数
                                            ProLaminator.Config.CfgManager.Instance.Save();

                                            _warningText = isChs ? "程式保存成功!" : "Save parameter successfully!";
                                            _warningCaption = isChs ? "提示信息" : "Warning Message";
                                            ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                               ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Information, isChs);

                                        }
                                        catch (System.Exception ex)
                                        {

                                        }
                                    }
                                    else
                                    {
                                        _warningText = isChs ? "程式名称异常!" : "No routine name!";
                                        _warningCaption = isChs ? "警告信息" : "Warning Message";
                                        ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                           ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, isChs);
                                    }
                                }
                                else
                                {
                                    _warningText = isChs ? "程式路径异常!" : "No routine directory!";
                                    _warningCaption = isChs ? "警告信息" : "Warning Message";
                                    ProCommon.DerivedForm.FrmMsgBox.Show(_warningText, _warningCaption,
                                       ProCommon.DerivedForm.MyButtons.OK, ProCommon.DerivedForm.MyIcon.Warning, isChs);
                                }
                            }
                        }
                        break;
                    case "SBTN_EXITSET":
                        {
                            //设置所有相机工位非调试模式
                            if (ProLaminator.Device.CameraManager.Instance != null
                                && ProLaminator.Device.CameraManager.Instance.CameraList != null)
                                ProLaminator.Device.CameraManager.Instance.SetImageToMultiViewer();

                            ProLaminator.Logic.SystemManager.Instance.IsRunOnce = true;
                            ProLaminator.Logic.SystemManager.Instance.IsRunning = false;
                            if (_ownerModule != null)
                                _ownerModule.ShowMultiViewer();
                        }
                        break;
                    case "SBTN_WRITEDATAGLASS":
                        {
                            x = Convert.ToSingle(this.speCoordXGlass.EditValue) * 100;
                            y = Convert.ToSingle(this.speCoordYGlass.EditValue) * 100;
                            r = Convert.ToSingle(this.speCoordRGlass.EditValue) * 100;
                            ix = Convert.ToInt32(x);
                            iy = Convert.ToInt32(y);
                            ir = Convert.ToInt32(r);

                            f = Convert.ToUInt16(this.speFlagGlass.EditValue);

                            if (ProLaminator.Device.CameraManager.Instance.PanisonicPlc != null)
                            {
                                if (ProLaminator.Device.CameraManager.Instance.PanisonicPlc.Connected)
                                {
                                    ProLaminator.Device.CameraManager.Instance.PanisonicPlc.Registers.SetValue(1101, ix);
                                    ProLaminator.Device.CameraManager.Instance.PanisonicPlc.Registers.SetValue(1103, iy);
                                    ProLaminator.Device.CameraManager.Instance.PanisonicPlc.Registers.SetValue(1105, ir);
                                    ProLaminator.Device.CameraManager.Instance.PanisonicPlc.Registers.SetValue(1100, f);
                                }
                            }
                        }
                        break;
                    case "SBTN_WRITEDATAMEMBRANE1":
                        {
                            x = Convert.ToSingle(this.speCoordXMembrane1.EditValue) * 100;
                            y = Convert.ToSingle(this.speCoordYMembrane1.EditValue) * 100;
                            r = Convert.ToSingle(this.speCoordRMembrane1.EditValue) * 100;
                            ix = Convert.ToInt32(x);
                            iy = Convert.ToInt32(y);
                            ir = Convert.ToInt32(r);

                            f = Convert.ToUInt16(this.speFlagMembrane1.EditValue);
                            if (ProLaminator.Device.CameraManager.Instance.PanisonicPlc != null)
                            {
                                if (ProLaminator.Device.CameraManager.Instance.PanisonicPlc.Connected)
                                {
                                    ProLaminator.Device.CameraManager.Instance.PanisonicPlc.Registers.SetValue(1201, ix);
                                    ProLaminator.Device.CameraManager.Instance.PanisonicPlc.Registers.SetValue(1203, iy);
                                    ProLaminator.Device.CameraManager.Instance.PanisonicPlc.Registers.SetValue(1205, ir);
                                    ProLaminator.Device.CameraManager.Instance.PanisonicPlc.Registers.SetValue(1200, f);
                                }
                            }
                        }
                        break;
                    case "SBTN_WRITEDATAMEMBRANE2":
                        {
                            x = Convert.ToSingle(this.speCoordXMembrane2.EditValue) * 100;
                            y = Convert.ToSingle(this.speCoordYMembrane2.EditValue) * 100;
                            r = Convert.ToSingle(this.speCoordRMembrane2.EditValue) * 100;
                            ix = Convert.ToInt32(x);
                            iy = Convert.ToInt32(y);
                            ir = Convert.ToInt32(r);

                            f = Convert.ToUInt16(this.speFlagMembrane2.EditValue);
                            if (ProLaminator.Device.CameraManager.Instance.PanisonicPlc != null)
                            {
                                if (ProLaminator.Device.CameraManager.Instance.PanisonicPlc.Connected)
                                {
                                    ProLaminator.Device.CameraManager.Instance.PanisonicPlc.Registers.SetValue(1301, ix);
                                    ProLaminator.Device.CameraManager.Instance.PanisonicPlc.Registers.SetValue(1303, iy);
                                    ProLaminator.Device.CameraManager.Instance.PanisonicPlc.Registers.SetValue(1305, ir);
                                    ProLaminator.Device.CameraManager.Instance.PanisonicPlc.Registers.SetValue(1300, f);
                                }
                            }
                        }
                        break;
                    default: break;
                }
            }
        }

        private void _frmMatch_Disposed(object sender, EventArgs e)
        {
            _frmMatch = null;
        }

        /// <summary>
        /// 鼠标移动事件回调
        /// [显示坐标和灰度值]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HWndcMain_HMouseMove(object sender, HalconDotNet.HMouseEventArgs e)
        {
            if (_tstImage != null
                 && _tstImage.IsInitialized())
            {
                try
                {
                    _coordX = Convert.ToInt32(e.X);
                    _coordY = Convert.ToInt32(e.Y);
                    this.tlstrptxtCoordinate.Text = "X:" + _coordX.ToString("00") + ",Y:" + _coordY.ToString("00");

                    HalconDotNet.HOperatorSet.GetGrayval(_tstImage,
                        new HalconDotNet.HTuple(_coordY),
                        new HalconDotNet.HTuple(_coordX), out _grayValue);
                    _channelCount = _grayValue.TupleLength();

                    if (_channelCount.TupleEqual(1))
                    {
                        this.tlstrptxtGrayValue.Text = _grayValue.I.ToString("00");
                    }
                    else if (_channelCount.TupleEqual(3))
                    {
                        this.tlstrptxtGrayValue.Text = "R:" + _grayValue[0].I.ToString("00")
                                                      + "G:" + _grayValue[1].I.ToString("00")
                                                      + "B:" + _grayValue[2].I.ToString("00");
                    }
                }
                catch (HalconDotNet.HalconException hex)
                {
                    this.tlstrptxtGrayValue.Text = hex.Message;
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
                if (ProLaminator.Device.CameraManager.Instance != null
                       && ProLaminator.Device.CameraManager.Instance.CameraList != null)
                    ProLaminator.Device.CameraManager.Instance.SetImageToMultiViewer();

                ProLaminator.Logic.SystemManager.Instance.IsRunOnce = true;
                ProLaminator.Logic.SystemManager.Instance.IsRunning = false;
                //左键双击，则切换窗口
                if (_ownerModule != null)
                    _ownerModule.ShowMultiViewer();
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
                              }
                              ));
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

        private void Chke_CheckedChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit chke = sender as DevExpress.XtraEditors.CheckEdit;
            if (chke != null
              && chke.Tag != null)
            {
                switch (chke.Tag.ToString())
                {
                    case "CHKE_SAVEALL":
                        {
                            switch (_cameraFlag)
                            {
                                case 0:
                                    _cfgVisionPara.ParaForGlass.IsSaveImageAll = chke.Checked;
                                    break;
                                case 1:
                                    _cfgVisionPara.ParaForMembrane1.IsSaveImageAll = chke.Checked;
                                    break;
                                case 2:
                                    _cfgVisionPara.ParaForMembrane2.IsSaveImageAll = chke.Checked;
                                    break;
                                default: break;
                            }
                        }
                        break;
                    case "CHKE_SAVEOK":
                        {
                            switch (_cameraFlag)
                            {
                                case 0:
                                    _cfgVisionPara.ParaForGlass.IsSaveImageOK = chke.Checked;
                                    break;
                                case 1:
                                    _cfgVisionPara.ParaForMembrane1.IsSaveImageOK = chke.Checked;
                                    break;
                                case 2:
                                    _cfgVisionPara.ParaForMembrane2.IsSaveImageOK = chke.Checked;
                                    break;
                                default: break;
                            }
                        }
                        break;
                    case "CHKE_SAVENG":
                        {
                            switch (_cameraFlag)
                            {
                                case 0:
                                    _cfgVisionPara.ParaForGlass.IsSaveImageNG = chke.Checked;
                                    break;
                                case 1:
                                    _cfgVisionPara.ParaForMembrane1.IsSaveImageNG = chke.Checked;
                                    break;
                                case 2:
                                    _cfgVisionPara.ParaForMembrane2.IsSaveImageNG = chke.Checked;
                                    break;
                                default: break;
                            }
                        }
                        break;
                    case "CHKE_ENABLEALGORITHM":
                        {
                            if (_cameraSpecidied != null
                              && _cameraSpecidied.Property.IsConnected)
                                _cameraSpecidied.Property.EnableAlgorithm = chke.Checked;
                        }
                        break;
                    case "CHKE_MODELREFERENCE":
                        {
                            if(chke.Checked)
                                _cfgVisionPara.CoordinateReferenceMode = 0;
                        }
                        break;
                    case "CHKE_CAMERACENTERREFERENCE":
                        {
                            if (chke.Checked)
                                _cfgVisionPara.CoordinateReferenceMode = 1;
                        }
                        break;
                    default: break;
                }
            }
        }

        private void Spe_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SpinEdit spe = sender as DevExpress.XtraEditors.SpinEdit;
            if (spe != null
                && spe.Tag != null)
            {
                switch (spe.Tag.ToString())
                {
                    case "SPE_EXPOSURETIME":
                        {
                            _exposureTime = Convert.ToSingle(spe.EditValue);
                            if (_cameraSpecidied != null
                                && _cameraSpecidied.Property.IsConnected)
                                _cameraSpecidied.API.SetExposureTime(_exposureTime);
                        }
                        break;
                    case "SPE_GAIN":
                        {
                            _cameraGain = Convert.ToSingle(spe.EditValue);
                            if (_cameraSpecidied != null
                               && _cameraSpecidied.Property.IsConnected)
                                _cameraSpecidied.API.SetGain(_cameraGain);
                        }
                        break;
                    case "SPE_TRIGGERDELAY":
                        {
                            _externalTriggerDelay = Convert.ToSingle(spe.EditValue);
                            if (_cameraSpecidied != null
                                && _cameraSpecidied.Property.IsConnected)
                                _cameraSpecidied.API.SetTriggerDelay(0, _externalTriggerDelay);
                        }
                        break;
                    case "SPE_DEBOUNCETIME":
                        {
                            _externalDebouncerTime = Convert.ToSingle(spe.EditValue);
                            if (_cameraSpecidied != null
                                && _cameraSpecidied.Property.IsConnected)
                                _cameraSpecidied.API.SetDebouncerTime(0, _externalDebouncerTime);
                        }
                        break;
                    case "SPE_GAMMA":
                        {
                            _gamma = Convert.ToSingle(spe.EditValue);
                            if (_cameraSpecidied != null
                              && _cameraSpecidied.Property.IsConnected)
                                _cameraSpecidied.API.SetGamma(_gamma);
                        }
                        break;
                    case "SPE_ROWUNIT":
                        {
                            _rowUnit = Convert.ToSingle(spe.EditValue);
                            if (_cfgCalibration != null
                               && _cfgCalibration.CalSolutionList != null)
                                _cfgCalibration.CalSolutionList[_cameraID].RowUnit = _rowUnit;
                        }
                        break;
                    case "SPE_COLUNIT":
                        {
                            _colUnit = Convert.ToSingle(spe.EditValue);
                            if (_cfgCalibration != null
                                && _cfgCalibration.CalSolutionList != null)
                                _cfgCalibration.CalSolutionList[_cameraID].ColUnit = _colUnit;
                        }
                        break;
                    default: break;
                }
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

        private void UpdateSpinEdit(DevExpress.XtraEditors.SpinEdit spe, ProCommon.Communal.Language lan, System.Resources.ResourceManager resrcMgr)
        {
            if (spe != null
                && spe.Tag != null)
            {
                spe.EditValueChanged -= Spe_EditValueChanged;
                spe.EditValueChanged += Spe_EditValueChanged;
            }
        }

        private void UpdateCheckEdit(DevExpress.XtraEditors.CheckEdit chke, ProCommon.Communal.Language lan, System.Resources.ResourceManager resrcMgr)
        {
            if (chke != null
               && chke.Tag != null)
            {
                chke.CheckedChanged -= Chke_CheckedChanged;
                if (resrcMgr != null)
                {
                    bool isChs = (lan == ProCommon.Communal.Language.Chinese);
                    string str = chke.Tag.ToString();
                    chke.Text = isChs ? resrcMgr.GetString("chs_" + str) : resrcMgr.GetString("en_" + str);
                }
                chke.CheckedChanged += Chke_CheckedChanged;
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

        #endregion
    }
}
