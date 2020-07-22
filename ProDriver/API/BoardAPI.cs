using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       BoardAPI
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProDriver.API
 * File      Name：       BoardAPI
 * Creating  Time：       4/21/2020 5:37:41 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProDriver.API
{
    /// <summary>
    /// 板卡控制器函数接口
    /// [注:与控制器品牌无关的统一函数接口,
    /// 目前已实现正运动控制器,]
    /// </summary>
    public class BoardAPI
    {
        public BoardAPI(ProCommon.Communal.BoardProperty boardProperty)
        {
            switch (boardProperty.Brand)
            {
                case ProCommon.Communal.DeviceBrand.LeadShine:
                    {
                    }
                    break;
                case ProCommon.Communal.DeviceBrand.ZMotion:
                    {
                        Property = boardProperty;
                        ProDriver.Driver.BoardDriver_ZMotion boarddriverZm = new ProDriver.Driver.BoardDriver_ZMotion();
                        IBoardDriverable = (boarddriverZm as ProDriver.Driver.IBoardDriver);
                    }
                    break;
                case ProCommon.Communal.DeviceBrand.ICPDAS:
                    break;
            }
        }

        public ProCommon.Communal.BoardProperty Property
        {
            private set;
            get;
        }

        public ProDriver.Driver.IBoardDriver IBoardDriverable { private set; get; }       

        #region 调用接口
        public bool ConnectCtrller(string ip, int port = 8089)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.ConnectCtrller(ip, port);
            return rt;
        }

        public bool DisconnectCtrller()
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.DisconnectCtrller();
            return rt;
        }

        public bool InitCtrllerSys()
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.InitCtrllerSys();
            return rt;
        }

        public bool SetBaseAxes(int axisNum, int[] piAxislist)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetBaseAxes(axisNum, piAxislist);
            return rt;
        }

        public bool SetAxisType(int naxis, int type)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetAxisType(naxis, type);
            return rt;
        }

        public bool SetAxisPulseOutMode(int naxis, int mode)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetAxisPulseOutMode(naxis, mode);
            return rt;
        }

        public bool SetAxisALMIn(int naxis, int inputno)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetAxisALMIn(naxis, inputno);
            return rt;
        }

        public bool SetAxisHRevIn(int naxis, int inputno)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetAxisHRevIn(naxis, inputno);
            return rt;
        }

        public bool SetAxisSRevValue(int naxis, float fvalue)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetAxisSRevValue(naxis, fvalue);
            return rt;
        }

        public bool SetAxisDatumIn(int naxis, int inputno)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetAxisDatumIn(naxis, inputno);
            return rt;
        }

        public bool SetAxisHFwdIn(int naxis, int inputno)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetAxisHFwdIn(naxis, inputno);
            return rt;
        }

        public bool SetAxisSFwdValue(int naxis, float fvalue)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetAxisSFwdValue(naxis, fvalue);
            return rt;
        }

        public bool SetPortInEffectiveLevel(int inputno, ProCommon.Communal.ElectricalLevel level)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetPortInEffectiveLevel(inputno, level);
            return rt;
        }

        public bool SetAxisUnits(int naxis, float units)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetAxisUnits(naxis, units);
            return rt;
        }

        public bool SetAxisTrapeziumPara(int naxis, float lspeed, float maxspeed, float tacc, float tdec)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetAxisTrapeziumPara(naxis, lspeed, maxspeed, tacc, tdec);
            return rt;
        }

        public bool SetAxisSigmoidPara(int naxis, float lspeed, float maxspeed, float tacc, float tdec, int sacc, int sdec)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetAxisSigmoidPara(naxis, lspeed, maxspeed, tacc, tdec, sacc, sdec);
            return rt;
        }

        public bool SetAxisSDEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel sdlevel, uint sdmode)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetAxisSDEffectiveLevel(naxis, enable, sdlevel, sdmode);
            return rt;
        }

        public bool SetAxisPCSEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel pcslevel)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetAxisPCSEffectiveLevel(naxis, enable, pcslevel);
            return rt;
        }

        public bool SetAxisINPEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel inplevel)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetAxisINPEffectiveLevel(naxis, enable, inplevel);
            return rt;
        }

        public bool SetAxisERCEffectiveLevel(int naxis, uint enable, ProCommon.Communal.ElectricalLevel erclevel, uint ercwidth, uint ercofftime)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetAxisERCEffectiveLevel(naxis, enable, erclevel, ercwidth, ercofftime);
            return rt;
        }

        public bool SetAxisERC(int naxis, uint sel)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetAxisERC(naxis, sel);
            return rt;
        }

        public bool SetAxisALMEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel almlevel, uint actionmode)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetAxisALMEffectiveLevel(naxis, almlevel, actionmode);
            return rt;
        }

        public bool SetAxisEZEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel ezlevel, uint actionmode)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetAxisEZEffectiveLevel(naxis, ezlevel, actionmode);
            return rt;
        }

        public bool SetAxisLTCEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel ltclevel, uint actionmode)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetAxisLTCEffectiveLevel(naxis, ltclevel, actionmode);
            return rt;
        }

        public bool SetAxisELLevel(int naxis, uint actionmode)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetAxisELEffectiveLevel(naxis, actionmode);
            return rt;
        }

        public bool SetAxisDatumEffectiveLevel(int naxis, ProCommon.Communal.ElectricalLevel datumlevel, uint filter)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetAxisDatumEffectiveLevel(naxis, datumlevel, filter);
            return rt;
        }

        public bool SetAxisServo(int naxis, bool onoff)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetAxisServo(naxis, onoff);
            return rt;
        }

        public bool SetAxisEMGEffectiveLevel(uint enable, ProCommon.Communal.ElectricalLevel emglevel)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetAxisEMGEffectiveLevel(enable, emglevel);
            return rt;
        }

        public bool FindSingleDatum(int naxis, ProCommon.Communal.MoveDirection moveDir, int mode)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.FindDatum(naxis, moveDir, mode);
            return rt;
        }

        public bool SingleContinueMove(int naxis, ProCommon.Communal.MoveDirection movedir)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SingleContinueMove(naxis, movedir);
            return rt;
        }

        public bool SingleRelMove(int naxis, float pos)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SingleRelMove(naxis, pos);
            return rt;
        }

        public bool SingleAbsMove(int naxis, float pos)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SingleAbsMove(naxis, pos);
            return rt;
        }

        public bool SingleCancelMove(int naxis, int mode)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SingleCancelMove(naxis, mode);
            return rt;
        }

        public bool Line2Move(float[] axespos, int mode)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.Line2Move(axespos, mode);
            return rt;
        }

        public bool CenterBasedArc2Move(float[] dstpos, float[] cenpos, int dir, int mode)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.CenterBasedArc2Move(dstpos, cenpos, dir, mode);
            return rt;
        }

        public bool PointsBasedArc2Move(float[] midpos, float[] dstpos, int mode)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.PointsBasedArc2Move(midpos, dstpos, mode);
            return rt;
        }

        public bool SetAxisCurPos(int naxis, float curpos)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetAxisCurPos(naxis, curpos);
            return rt;
        }

        public bool GetAxisCurPos(int naxis, ref float curpos)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.GetAxisCurPos(naxis, ref curpos);
            return rt;
        }

        public bool GetAxisCurspeed(int naxis, ref float curspeed)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.GetAxisCurspeed(naxis, ref curspeed);
            return rt;
        }

        public bool ChekcAxisIfStop(int naxis, ref bool stopped)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.ChekcAxisIfStop(naxis, ref stopped);
            return rt;
        }

        public bool GetAxisStatus(int naxis, ref int axisStatus)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.GetAxisStatus(naxis, ref axisStatus);
            return rt;
        }

        public bool CheckAxisIfNormal(int naxis, ref bool normal)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.CheckAxisIfNormal(naxis, ref normal);
            return rt;
        }

        public bool RapidStop(int mode)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.RapidStop(mode);
            return rt;
        }

        public bool SetOutBitLogicValue(int nbit, bool level)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.SetOutBitLogicValue(nbit, level);
            return rt;
        }

        public bool GetOutBitLogicValue(int nport, ref bool onoff)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.GetOutBitLogicValue(nport, ref onoff);
            return rt;
        }

        public bool GetInBitLogicValue(int nport, ref bool onoff)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.GetInBitLogicValue(nport, ref onoff);
            return rt;
        }

        public bool WaitForAxisFindDatum(int naxis, bool waitfordatum = true, double sleepsecond = 0.01, double timeout = 50, bool enablepause = true, float limitdistance = -1, float specifiedpos = 0.0f)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.WaitForAxisFindDatum(naxis, waitfordatum, sleepsecond, timeout, enablepause, limitdistance, specifiedpos);
            return rt;
        }

        public bool WaitForAxisStop(int naxis, double sleepsecond = 0.01, double timeout = 50, bool enablepause = true)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.WaitForAxisStop(naxis, sleepsecond, timeout, enablepause);
            return rt;
        }

        public bool WaitForAxisLimit(int naxis, ProDriver.Driver.LimitType limittype, bool waitforstatus = true, double sleepsecond = 0.01, double timeout = 20, bool enablepause = true)
        {
            bool rt = false;
            if (IBoardDriverable != null)
                rt = IBoardDriverable.WaitForAxisLimit(naxis, limittype, waitforstatus, sleepsecond, timeout, enablepause);
            return rt;
        }
        #endregion      

        /// <summary>
        /// 控制器所有轴伺服使能或禁止
        /// </summary>
        /// <param name="onoff">
        /// true,使能
        /// false,禁止</param>
        public void SwitchBoardCtrllerServo(bool onoff)
        {
            if (Property != null)
            {
                for (int i = 0; i < Property.AxisList.Count; i++)
                {
                    SetAxisServo(Property.AxisList[i].Number, onoff);
                }
            }
        }

        /// <summary>
        /// 初始化控制器输入口有效电平
        /// </summary>
        public void InitInPortEffectiveLevel()
        {
            if (Property != null
                && Property.InVarObjList != null)
            {
                for (int i = 0; i < Property.InVarObjList.Count; i++)
                {
                    SetPortInEffectiveLevel(Property.InVarObjList[i].Address,
                        Property.InVarObjList[i].EffectiveLevel);
                }
            }
        }

        /// <summary>
        /// 初始化控制器输入输出变量
        /// </summary>
        /// <summary>
        /// 初始化输入输出变量对象
        /// </summary>
        public void InitInAndOutPutVarObj()
        {
            if (Property != null
               && Property.InVarObjList != null)
            {
                for (int i = 0; i < Property.InVarObjList.Count; i++)
                {
                    //增加:设置输入端口逻辑状态
                    Property.InVarObjList[i].NewValue = false;
                }
            }

            if (Property != null
               && Property.OutVarObjList != null)
            {
                for (int i = 0; i < Property.OutVarObjList.Count; i++)
                {
                    //增加:设置输出端口逻辑状态
                    SetOutBitLogicValue(Property.OutVarObjList[i].Address, false);
                    System.Threading.Thread.Sleep(5);
                    Property.OutVarObjList[i].NewValue = false;
                }
            }
        }

        /// <summary>
        /// 控制器所有轴参数更新
        /// </summary>
        /// <param name="runmode"></param>
        public void RefreshBoardCtrllerAxesPara(string runmode)
        {
            for (int i = 0; i < Property.AxisList.Count; i++)
            {
                #region 轴基本参数
                SetAxisType(Property.AxisList[i].Number, Property.AxisList[i].AxisType);
                SetAxisUnits(Property.AxisList[i].Number,Property.AxisList[i].PulseUnit);
                SetAxisPulseOutMode(Property.AxisList[i].Number,Property.AxisList[i].PulseOutMode);
                #endregion

                #region 轴信号参数

                //通用输入口作轴信号口 ([注:]有些板卡有专用信号口,有的板卡利用通用输入输出口做信号口)
                SetAxisALMIn(Property.AxisList[i].Number, Property.AxisList[i].AlarmInBitNumber);
                SetPortInEffectiveLevel(Property.AxisList[i].AlarmInBitNumber, Property.AxisList[i].AlarmInLevel);     //轴报警信号口及有效电平
                SetAxisHRevIn(Property.AxisList[i].Number, Property.AxisList[i].HardReverseInBitNumber);
                SetPortInEffectiveLevel(Property.AxisList[i].HardReverseInBitNumber, Property.AxisList[i].HardReverseInLevel);   //轴负向硬限位信号口及有效电平
                SetAxisDatumIn(Property.AxisList[i].Number, Property.AxisList[i].DatumInBitNumber);
                SetPortInEffectiveLevel(Property.AxisList[i].DatumInBitNumber, Property.AxisList[i].DatumInLevel); //轴原点限位信号口及有效电平
                SetAxisHFwdIn(Property.AxisList[i].Number, Property.AxisList[i].HardForwarInBitNumber);
                SetPortInEffectiveLevel(Property.AxisList[i].HardForwarInBitNumber, Property.AxisList[i].HardForwardInLevel);   //轴正向硬限位信号口及有效电平 

                //专用输入口作轴信号口 ([注:]有些板卡有专用信号口,有的板卡利用通用输入输出口做信号口)
                //SetAxisALMLevel(BoardCtrller.AxisList[i].Num, 0, 0);       //设置指定轴的报警信号逻辑电平(低电平)
                //SetAxisELLevel(BoardCtrller.AxisList[i].Num, 0);           //设置指定轴的限位信号逻辑电平和制动方式(低电平+立即停止)
                //SetAxisDatumLevel(BoardCtrller.AxisList[i].Num, 0, 1);     //设置指定轴的原点信号逻辑电平(低电平+过滤)
                //SetAxisSDLevel(BoardCtrller.AxisList[i].Num, 0, 0, 1);     //设置指定轴的减速信号逻辑电平(低电平)
                //SetAxisPCSLevel(BoardCtrller.AxisList[i].Num, 0, 0);       //设置指定轴的位置改变信号逻辑电平(低电平)
                //SetAxisINPLevel(BoardCtrller.AxisList[i].Num, 0, 0);       //设置指定轴的到位信号逻辑电平(低电平)
                //SetAxisERCLevel(BoardCtrller.AxisList[i].Num, 3, 0, 1, 1); //设置指定轴的误差清除信号逻辑电平(低电平+有效输出宽度102us+关断时间12us)
                //SetAxisEZLevel(BoardCtrller.AxisList[i].Num, 0, 1);        //设置指定轴的编码器复位信号逻辑电平(低电平+计数器复位信号)
                //SetAxisLTCLevel(BoardCtrller.AxisList[i].Num, 0, 1);       //设置指定轴的锁存器锁存触发信号逻辑电平(低电平)
                #endregion

                #region 轴运行速度
                switch (runmode.ToUpper().Trim())
                {
                    case "RESET":
                        SetAxisTrapeziumPara(Property.AxisList[i].Number, 10.0f, 50.0f, 0.5f, 0.5f);
                        break;
                    default:
                        float tacc = (Property.AxisList[i].RunSpeed -Property.AxisList[i].StartSpeed) / Property.AxisList[i].Accel;
                        SetAxisTrapeziumPara(Property.AxisList[i].Number,Property.AxisList[i].StartSpeed, Property.AxisList[i].RunSpeed, tacc, tacc);
                        break;
                }
                #endregion
            }
        }

        /// <summary>
        /// 等控制器所有轴停止
        /// </summary>
        public bool WaitAxesStop()
        {
            bool rt = false;

            if (Property.IsConnected)
            {
                for (int i = 0; i <Property.AxisList.Count; i++)
                {
                    if (!WaitForAxisStop(Property.AxisList[i].Number))
                    {
                        System.Windows.Forms.MessageBox.Show("等轴[" +Property.AxisList[i].Name + "]停止失败!", "错误信息",
                           System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        break;
                    }
                }
                rt = true;
            }

            return rt;
        }

        #region 获取输入控制变量对象

        /// <summary>
        /// 根据输入控制变量获取输入控制变量对象
        /// </summary>
        /// <param name="invarObjID">输入变量ID</param>
        /// <returns></returns>
        public ProCommon.Communal.InVarObj GetInVarObj(string invarObjID)
        {
            System.Collections.IEnumerable ie = from InVarObjs in Property.InVarObjBList
                                                where InVarObjs.ID == invarObjID
                                                select InVarObjs;
            System.Collections.Generic.List<ProCommon.Communal.InVarObj> inVarObjList = ie.Cast<ProCommon.Communal.InVarObj>().ToList();

            if (inVarObjList.Count > 0)
            {
                return inVarObjList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据输入控制变量获取输入控制变量对象
        /// 根据是否更新NewValue以及EditValue，执行相应操作
        /// </summary>
        /// <param name="invarObjID">输入变量ID</param>
        /// <param name="setNew">是否更新NewValue</param>
        /// <param name="setEdit">是否更新EditValue</param>
        /// <returns></returns>
        public ProCommon.Communal.InVarObj GetInVarObj(string invarObjID, bool setNew, bool setEdit)
        {
            bool onoff = false;
            if (Property.IsConnected)
                GetInVarObjStatus(invarObjID, out onoff);


            System.Collections.IEnumerable ie = from InVarObjs in Property.InVarObjBList
                                                where InVarObjs.ID == invarObjID
                                                select InVarObjs;
            System.Collections.Generic.List<ProCommon.Communal.InVarObj> inVarObjList = ie.Cast<ProCommon.Communal.InVarObj>().ToList();

            if (inVarObjList.Count > 0)
            {
                if (setNew)
                    inVarObjList[0].NewValue = onoff;

                if (setEdit)
                    inVarObjList[0].EditValue = onoff;
                return inVarObjList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取单个输入控制变量对象的状态
        /// </summary>
        /// <param name="invarObjID"></param>
        /// <param name="onoff"></param>
        /// <returns></returns>
        public bool GetInVarObjStatus(string invarObjID,out bool onoff)
        {
            System.Collections.IEnumerable ie = from InVarObjs in Property.InVarObjBList
                                                where InVarObjs.ID == invarObjID
                                                select InVarObjs;
            System.Collections.Generic.List<ProCommon.Communal.InVarObj> inVarObjList = ie.Cast<ProCommon.Communal.InVarObj>().ToList();
            if (inVarObjList.Count > 0)
            {
                return GetInVarObjStatus(inVarObjList[0].Address, out onoff);
            }
            else
            {
                return onoff = false;
            }          
        }

        /// <summary>
        /// 获取单个输入控制变量对象的状态
        /// </summary>
        /// <param name="invarObjAddress">输入变量地址</param>
        /// <param name="onoff">输入变量对象是否有效</param>
        /// <returns>true,执行正常;false执行异常</returns>
        public bool GetInVarObjStatus(int invarObjAddress, out bool onoff)
        {
            bool rt = false;
            onoff = false;          
            if (invarObjAddress != -1)
                rt = GetInBitLogicValue(invarObjAddress, ref onoff);
            return rt;
        }

        #endregion

        #region 获取输出控制变量对象

        /// <summary>
        /// 根据输出控制变量获取输出控制变量对象
        /// </summary>
        /// <param name="outvarObjID">输出变量ID</param>
        /// <returns></returns>
        public ProCommon.Communal.OutVarObj GetOutVarObj(string outvarObjID)
        {
            System.Collections.IEnumerable ie = from OutVarObjs in Property.OutVarObjBList
                                                where OutVarObjs.ID == outvarObjID
                                                select OutVarObjs;
            System.Collections.Generic.List<ProCommon.Communal.OutVarObj> OutVarObjList = ie.Cast<ProCommon.Communal.OutVarObj>().ToList();

            if (OutVarObjList.Count > 0)
            {
                return OutVarObjList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据输出控制变量获取输出控制变量对象
        /// 根据是否更新NewValue以及EditValue，执行相应操作
        /// </summary>
        /// <param name="outvarObjID">输出变量ID</param>
        /// <param name="setNew">是否更新NewValue</param>
        /// <param name="setEdit">是否更新EditValue</param>
        /// <returns></returns>
        public ProCommon.Communal.OutVarObj GetOutVarObj(string outvarObjID, bool setNew, bool setEdit)
        {
            bool onoff = false;
            if (Property.IsConnected)
                GetOutVarObjStatus(outvarObjID,out onoff);

            System.Collections.IEnumerable ie = from OutVarObjs in Property.OutVarObjBList
                                                where OutVarObjs.ID == outvarObjID
                                                select OutVarObjs;
            System.Collections.Generic.List<ProCommon.Communal.OutVarObj> OutVarObjList = ie.Cast<ProCommon.Communal.OutVarObj>().ToList();

            if (OutVarObjList.Count > 0)
            {
                if (setNew)
                    OutVarObjList[0].NewValue = onoff;

                if (setEdit)
                    OutVarObjList[0].EditValue = onoff;
                return OutVarObjList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取单个输出控制变量对象的状态
        /// </summary>
        /// <param name="outvarObjID"></param>
        /// <param name="onoff"></param>
        /// <returns></returns>
        public bool GetOutVarObjStatus(string outvarObjID,out bool onoff)
        {
            bool rt = false;
            System.Collections.IEnumerable ie = from OutVarObjs in Property.OutVarObjBList
                                                where OutVarObjs.ID == outvarObjID
                                                select OutVarObjs;
            System.Collections.Generic.List<ProCommon.Communal.OutVarObj> OutVarObjList = ie.Cast<ProCommon.Communal.OutVarObj>().ToList();

            if (OutVarObjList.Count > 0)
            {
                return GetOutVarObjStatus(OutVarObjList[0].Address, out onoff);
            }
            else
            {
                return onoff = false;
            }           
        }

        /// <summary>
        /// 获取单个输出控制变量对象的状态
        /// </summary>
        /// <param name="outvarObjAddress"></param>
        /// <param name="onoff"></param>
        /// <returns></returns>
        public bool GetOutVarObjStatus(int outvarObjAddress, out bool onoff)
        {
            bool rt = false;
            onoff = false;          
            if (outvarObjAddress != -1)
                rt = GetOutBitLogicValue(outvarObjAddress, ref onoff);
            return rt;
        }

        #endregion

        #region 设置输出控制变量对象

        /// <summary>
        /// 设置指定值到输出口
        /// </summary>
        /// <param name="outVar">输出控制变量</param>
        /// <param name="onoff">指定值</param>
        /// <returns></returns>
        public bool SetOutVarObjStatus(string outvarObjID, bool onoff)
        {
            bool rt = false;
            ProCommon.Communal.OutVarObj outVarObj = GetOutVarObj(outvarObjID);
            if (outVarObj.VarType.Trim().ToUpper() != "OUT")
                return rt;

            rt = SetOutBitLogicValue(outVarObj.Address, onoff);
            return rt;
        }

        public bool SetOutVarObjStatus(int outvarObjAddress, bool onoff)
        {
            bool rt = false;
            if (outvarObjAddress != -1)
                rt = SetOutBitLogicValue(outvarObjAddress,onoff);
            return rt;
        }

        #endregion
    }
}
