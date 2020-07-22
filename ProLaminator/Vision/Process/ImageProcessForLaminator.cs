using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ImageProcessForShotSpotter
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProLaminator.Vision.Process
 * File      Name：       ImageProcessForShotSpotter
 * Creating  Time：       5/19/2020 4:00:56 PM
 * Author    Name：       xYz_Albert
 * Description   ：       应用项目可能需要多个图像处理过程，每个图像处理过程可能用到多种算法;
 *                        图像处理过程由应用项目决定,过程用到的算法有应用项目专用,也有视觉库通用;
 *                        这里暂时都算作应用项目专用.
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProLaminator.Vision.Process
{
    /// <summary>
    /// 图像处理过程
    /// [玻璃定位纠偏]
    /// </summary>
    public class ImageProcess_LocateGlass : ProVision.Communal.ImageProcess
    {
        #region 字段和属性:算法变量以及算法参数
        /// <summary>
        /// 模板匹配模型参数
        /// </summary>
        private ProVision.Communal.ShapeModelParameter _matchPara;

        /// <summary>
        /// 模板匹配助手
        /// </summary>
        private ProVision.MatchModel.ShapeModelAssistant _matchModelAssistant;

        public HalconDotNet.HObject InspectArea;
        public ProLaminator.Vision.Parameter.VisionParaForGlass Parameter;

        /// <summary>
        /// 模板位姿Row坐标
        /// </summary>
        public HalconDotNet.HTuple ModelRow { private set; get; }

        /// <summary>
        /// 模板位姿Col坐标
        /// </summary>
        public HalconDotNet.HTuple ModelCol { private set; get; }

        /// <summary>
        /// 模板位姿Angle
        /// </summary>
        public HalconDotNet.HTuple ModelAgl { private set; get; }

        /// <summary>
        /// 实例特征像素坐标行
        /// </summary>
        public HalconDotNet.HTuple Row { private set; get; }

        /// <summary>
        /// 实例特征像素坐标列
        /// </summary>
        public HalconDotNet.HTuple Col { private set; get; }

        /// <summary>
        /// 实例特征相对模板特征偏移角
        /// [弧度]
        /// </summary>
        public HalconDotNet.HTuple Agl { private set; get; }

        #endregion

        public ImageProcess_LocateGlass()
        {
            _matchModelAssistant = new ProVision.MatchModel.ShapeModelAssistant();
            _matchPara = new ProVision.Communal.ShapeModelParameter();
        }

        public override void InitProcess()
        {
            UpdateParameter();
            _IsLaunchAllowed = LoadAlgorithmFile();           
        }

        public override bool Process(HalconDotNet.HObject hobjRaw)
        {
            bool rt = false;
            ResultOK = false;

            try
            {
                if (_IsLaunchAllowed)
                {
                    _RawImage = hobjRaw;

                    if (_IsEnableAlgorithm)
                    {
                        //1-算法需要的参数是否有效，进行图像处理
                        if (InspectArea != null
                            && InspectArea.IsInitialized())
                        {
                            //2-进行算法处理,更新函数返回值 
                            if (LocationGlass())
                            {
                                //3-更新图像处理结果标记:根据是否达标,更新图像处理结果标记
                                //4-显示图像处理结果图形变量,信息变量
                                Row = _matchModelAssistant.Result.Row;
                                Col = _matchModelAssistant.Result.Col;
                                Agl = _matchModelAssistant.Result.Angle;

                                ResultOK = rt = true;
                            }
                        }
                    }
                }
            }
            catch (HalconDotNet.HalconException hex) { ErrorMessage = hex.Message; }
            catch (System.Exception ex) { ErrorMessage = ex.Message; }
            finally { }           
            return rt;
        }

        /// <summary>
        /// 匹配定位
        /// </summary>
        /// <returns></returns>
        private bool LocationGlass()
        {
            bool rt = false;

            if(_matchModelAssistant!=null)
            {
                _matchModelAssistant.IsDetectInTrainImage = false;
                _matchModelAssistant.SetTestImage(_RawImage);
                _matchModelAssistant.Result.Reset();
                _matchModelAssistant.DetectShapeModel();

                rt = _matchModelAssistant.Result.MatchCount > 0;
            }

            return rt;
        }

        public override void SetEnableAlgorithm(bool enable)
        {
            _IsEnableAlgorithm = enable;
        }

        protected override bool LoadAlgorithmFile()
        {
            bool rt = false;
            try
            {
                if (!string.IsNullOrEmpty(Parameter.PathForSaveLocPara))
                {
                    if(_matchModelAssistant!=null)
                    {
                        if (_matchModelAssistant.LoadShapeModel(Parameter.PathForSaveLocPara))
                            rt = true;
                        else { ErrorMessage = IsChinese ? "定位参数文件加载失败!" : "Load parameter file for location failed!"; }

                        //更新搜索区域
                        if (rt)
                        {
                            if (InspectArea != null
                                 && InspectArea.IsInitialized())
                                InspectArea.Dispose();
                            InspectArea = _matchModelAssistant.ModelSearchRegion;

                            ModelRow = _matchModelAssistant.ModelPose[0];
                            ModelCol = _matchModelAssistant.ModelPose[1];
                            ModelAgl = _matchModelAssistant.ModelPose[2];                          
                        }
                    }
                    else { ErrorMessage = IsChinese ? "模板匹配助手为空!" : "Match model assistant is null !"; }                    
                }
                else { ErrorMessage = IsChinese ? "定位参数文件路径异常!" : "No file path for location is valid !"; }
            }
            catch (HalconDotNet.HalconException hex) { ErrorMessage = hex.Message; }
            catch (System.Exception ex) { ErrorMessage = ex.Message; }
            finally { }
            return rt;
        }

        /// <summary>
        /// 根据算法参数更新算法变量
        /// </summary>
        protected override void UpdateParameter()
        {
            if (Parameter != null)
            {

            }
        }
    }

    /// <summary>
    /// 图像处理过程
    /// [膜1定位纠偏]
    /// </summary>
    public class ImageProcess_LocatingMembrane1 : ProVision.Communal.ImageProcess
    {
        #region 字段和属性:算法变量以及算法参数
        /// <summary>
        /// 模板匹配模型参数
        /// </summary>
        private ProVision.Communal.ShapeModelParameter _matchPara;

        /// <summary>
        /// 模板匹配助手
        /// </summary>
        private ProVision.MatchModel.ShapeModelAssistant _matchModelAssistant;

        public HalconDotNet.HObject InspectArea;
        public ProLaminator.Vision.Parameter.VisionParaForMembrane1 Parameter;

        /// <summary>
        /// 模板位姿Row坐标
        /// </summary>
        public HalconDotNet.HTuple ModelRow { private set; get; }

        /// <summary>
        /// 模板位姿Col坐标
        /// </summary>
        public HalconDotNet.HTuple ModelCol { private set; get; }

        /// <summary>
        /// 模板位姿Angle
        /// </summary>
        public HalconDotNet.HTuple ModelAgl { private set; get; }

        /// <summary>
        /// 实例特征像素坐标行
        /// </summary>
        public HalconDotNet.HTuple Row { private set; get; }

        /// <summary>
        /// 实例特征像素坐标列
        /// </summary>
        public HalconDotNet.HTuple Col { private set; get; }

        /// <summary>
        /// 实例特征相对模板特征偏移角
        /// [弧度]
        /// </summary>
        public HalconDotNet.HTuple Agl { private set; get; }

        #endregion

        public ImageProcess_LocatingMembrane1()
        {
            _matchModelAssistant = new ProVision.MatchModel.ShapeModelAssistant();
            _matchPara = new ProVision.Communal.ShapeModelParameter();
        }

        public override void InitProcess()
        {
            UpdateParameter();
            _IsLaunchAllowed = LoadAlgorithmFile();
        }

        public override bool Process(HalconDotNet.HObject hobjRaw)
        {
            bool rt = false;
            ResultOK = false;

            try
            {
                if (_IsLaunchAllowed)
                {
                    _RawImage = hobjRaw;

                    if (_IsEnableAlgorithm)
                    {
                        //1-算法需要的参数是否有效，进行图像处理
                        if (InspectArea != null
                            && InspectArea.IsInitialized())
                        {
                            //2-进行算法处理,更新函数返回值 
                            if (LocationMembrane1())
                            {
                                //3-更新图像处理结果标记:根据是否达标,更新图像处理结果标记
                                //4-显示图像处理结果图形变量,信息变量
                                Row = _matchModelAssistant.Result.Row;
                                Col = _matchModelAssistant.Result.Col;
                                Agl = _matchModelAssistant.Result.Angle;

                                ResultOK = rt = true;
                            }
                        }
                    }
                }
            }
            catch (HalconDotNet.HalconException hex) { ErrorMessage = hex.Message; }
            catch (System.Exception ex) { ErrorMessage = ex.Message; }
            finally { }
            return rt;
        }

        /// <summary>
        /// 识别靶环算法
        /// </summary>
        /// <returns></returns>
        private bool LocationMembrane1()
        {
            bool rt = false;

            if (_matchModelAssistant != null)
            {
                _matchModelAssistant.IsDetectInTrainImage = false;
                _matchModelAssistant.SetTestImage(_RawImage);
                _matchModelAssistant.Result.Reset();
                _matchModelAssistant.DetectShapeModel();

                rt = _matchModelAssistant.Result.MatchCount > 0;
            }

            return rt;
        }

        public override void SetEnableAlgorithm(bool enable)
        {
            _IsEnableAlgorithm = enable;
        }

        protected override bool LoadAlgorithmFile()
        {
            bool rt = false;
            try
            {
                if (!string.IsNullOrEmpty(Parameter.PathForSaveLocPara))
                {
                    if (_matchModelAssistant != null)
                    {
                        if (_matchModelAssistant.LoadShapeModel(Parameter.PathForSaveLocPara))
                            rt = true;
                        else { ErrorMessage = IsChinese ? "定位参数文件加载失败!" : "Load parameter file for location failed!"; }

                        //更新搜索区域
                        if (rt)
                        {
                            if (InspectArea != null
                                 && InspectArea.IsInitialized())
                                InspectArea.Dispose();
                            InspectArea = _matchModelAssistant.ModelSearchRegion;

                            ModelRow = _matchModelAssistant.ModelPose[0];
                            ModelCol = _matchModelAssistant.ModelPose[1];
                            ModelAgl = _matchModelAssistant.ModelPose[2];
                        }
                    }
                    else { ErrorMessage = IsChinese ? "模板匹配助手为空!" : "Match model assistant is null !"; }
                }
                else { ErrorMessage = IsChinese ? "定位参数文件路径异常!" : "No file path for location is valid !"; }
            }
            catch (HalconDotNet.HalconException hex) { ErrorMessage = hex.Message; }
            catch (System.Exception ex) { ErrorMessage = ex.Message; }
            finally { }
            return rt;
        }

        /// <summary>
        /// 根据算法参数更新算法变量
        /// </summary>
        protected override void UpdateParameter()
        {
            if (Parameter != null)
            {

            }
        }
    }

    /// <summary>
    /// 图像处理过程
    /// [膜2定位纠偏]
    /// </summary>
    public class ImageProcess_LocatingMembrane2 : ProVision.Communal.ImageProcess
    {
        #region 字段和属性:算法变量以及算法参数
        /// <summary>
        /// 模板匹配模型参数
        /// </summary>
        private ProVision.Communal.ShapeModelParameter _matchPara;

        /// <summary>
        /// 模板匹配助手
        /// </summary>
        private ProVision.MatchModel.ShapeModelAssistant _matchModelAssistant;

        public HalconDotNet.HObject InspectArea;
        public ProLaminator.Vision.Parameter.VisionParaForMembrane2 Parameter;

        /// <summary>
        /// 模板位姿Row坐标
        /// </summary>
        public HalconDotNet.HTuple ModelRow { private set; get; }

        /// <summary>
        /// 模板位姿Col坐标
        /// </summary>
        public HalconDotNet.HTuple ModelCol { private set; get; }

        /// <summary>
        /// 模板位姿Angle
        /// </summary>
        public HalconDotNet.HTuple ModelAgl { private set; get; }

        /// <summary>
        /// 实例特征像素坐标行
        /// </summary>
        public HalconDotNet.HTuple Row { private set; get; }

        /// <summary>
        /// 实例特征像素坐标列
        /// </summary>
        public HalconDotNet.HTuple Col { private set; get; }

        /// <summary>
        /// 实例特征相对模板特征偏移角
        /// [弧度]
        /// </summary>
        public HalconDotNet.HTuple Agl { private set; get; }

        #endregion

        public ImageProcess_LocatingMembrane2()
        {
            _matchModelAssistant = new ProVision.MatchModel.ShapeModelAssistant();
            _matchPara = new ProVision.Communal.ShapeModelParameter();
        }

        public override void InitProcess()
        {
            UpdateParameter();
            _IsLaunchAllowed = LoadAlgorithmFile();
        }

        public override bool Process(HalconDotNet.HObject hobjRaw)
        {
            bool rt = false;
            ResultOK = false;

            try
            {
                if (_IsLaunchAllowed)
                {
                    _RawImage = hobjRaw;

                    if (_IsEnableAlgorithm)
                    {
                        //1-算法需要的参数是否有效，进行图像处理
                        if (InspectArea != null
                            && InspectArea.IsInitialized())
                        {
                            //2-进行算法处理,更新函数返回值 
                            if (LocationMembrane2())
                            {
                                //3-更新图像处理结果标记:根据是否达标,更新图像处理结果标记
                                //4-显示图像处理结果图形变量,信息变量
                                Row = _matchModelAssistant.Result.Row;
                                Col = _matchModelAssistant.Result.Col;
                                Agl = _matchModelAssistant.Result.Angle;

                                ResultOK = rt = true;
                            }
                        }
                    }
                }
            }
            catch (HalconDotNet.HalconException hex) { ErrorMessage = hex.Message; }
            catch (System.Exception ex) { ErrorMessage = ex.Message; }
            finally { }
            return rt;
        }

        /// <summary>
        /// 识别靶环算法
        /// </summary>
        /// <returns></returns>
        private bool LocationMembrane2()
        {
            bool rt = false;

            if (_matchModelAssistant != null)
            {
                _matchModelAssistant.IsDetectInTrainImage = false;
                _matchModelAssistant.SetTestImage(_RawImage);
                _matchModelAssistant.Result.Reset();
                _matchModelAssistant.DetectShapeModel();

                rt = _matchModelAssistant.Result.MatchCount > 0;
            }

            return rt;
        }

        public override void SetEnableAlgorithm(bool enable)
        {
            _IsEnableAlgorithm = enable;
        }

        protected override bool LoadAlgorithmFile()
        {
            bool rt = false;
            try
            {
                if (!string.IsNullOrEmpty(Parameter.PathForSaveLocPara))
                {
                    if (_matchModelAssistant != null)
                    {
                        if (_matchModelAssistant.LoadShapeModel(Parameter.PathForSaveLocPara))
                            rt = true;
                        else { ErrorMessage = IsChinese ? "定位参数文件加载失败!" : "Load parameter file for location failed!"; }

                        //更新搜索区域
                        if (rt)
                        {
                            if (InspectArea != null
                                 && InspectArea.IsInitialized())
                                InspectArea.Dispose();
                            InspectArea = _matchModelAssistant.ModelSearchRegion;

                            ModelRow = _matchModelAssistant.ModelPose[0];
                            ModelCol = _matchModelAssistant.ModelPose[1];
                            ModelAgl = _matchModelAssistant.ModelPose[2];
                        }
                    }
                    else { ErrorMessage = IsChinese ? "模板匹配助手为空!" : "Match model assistant is null !"; }
                }
                else { ErrorMessage = IsChinese ? "定位参数文件路径异常!" : "No file path for location is valid !"; }
            }
            catch (HalconDotNet.HalconException hex) { ErrorMessage = hex.Message; }
            catch (System.Exception ex) { ErrorMessage = ex.Message; }
            finally { }
            return rt;
        }

        /// <summary>
        /// 根据算法参数更新算法变量
        /// </summary>
        protected override void UpdateParameter()
        {
            if (Parameter != null)
            {

            }
        }
    }
}
