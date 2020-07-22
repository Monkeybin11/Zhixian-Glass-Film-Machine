using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProCommon.Communal;

namespace ProDriver.Driver
{
    public class PlcDriver_PanisonicSerialPort : ProDriver.Driver.PlcDriver
    {
        private System.IO.Ports.SerialPort _serialPort;
        private object _comSyncObj;

        public PlcDriver_PanisonicSerialPort(System.IO.Ports.SerialPort serialport)
        {
            _comSyncObj = new object();
            _serialPort = serialport;
        }

        /// <summary>
        /// 获取单个控制变量对象状态
        /// </summary>
        /// <param name="inVarObj"></param>
        /// <param name="isOnOff">
        /// true--导通,false--断开</param>
        /// <returns></returns>
        protected override bool DoGetInvarStatus(InVarObj inVarObj, out bool isOnOff)
        {
            bool rt = false;
            isOnOff = false;

            try
            {
                if (inVarObj != null)
                {
                    // 站号--2字节
                    string stationNum = inVarObj.StationNumber;

                    // 变量类型--1字节
                    string varType = inVarObj.VarType.Trim().ToUpper();

                    //限定以下触点类型有效
                    if (!(varType == "X" || varType == "Y" || varType == "R" || varType == "T"
                       || varType == "C" || varType == "L"))
                    {
                        return rt;
                    }

                    //控制变量地址--4字节
                    string varAddress = inVarObj.Address.ToString().PadRight(4, '0');
                    string cmd = string.Format("%{0}#RCS{1}{2}",stationNum,varType,varAddress);
                    string cmdChk = ProCommon.Communal.ToolFunctions.AddCheckCode(cmd);
                    //向串口写入指令数据
                    stationNum = WriteData(cmdChk, 50);

                    //串口无返回结果,则获取输入变量对象状态失败,返回
                    if (string.IsNullOrEmpty(stationNum)) return rt;

                    //根据串口返回结果字符串--索引位置为3的字符:"$"--响应正常,"!"--响应异常
                    switch(stationNum.Substring(3,1))
                    {
                        case "$":
                            rt = true;
                            //根据串口返回结果字符串--索引位置为6的字符："0"--断开(OFF),"1"--导通(ON)
                            isOnOff = (stationNum.Substring(6, 1) == "1") ? true : false;
                            inVarObj.NewValue = isOnOff;
                            break;
                        default:break;
                    }
                }
            }
            catch (System.Exception ex) { }
            return rt;
        }

        /// <summary>
        /// 设置单个控制变量对象状态
        /// </summary>
        /// <param name="inVarObj"></param>
        /// <param name="isOnOff">
        /// true--导通,false--断开</param>
        /// <returns></returns>
        protected override bool DoSetInvarStatus(InVarObj inVarObj, bool isOnOff)
        {
            bool rt = false;
            try
            {
                if (inVarObj != null)
                {
                    // 站号--2字节
                    string stationNum = inVarObj.StationNumber;

                    // 变量类型--1字节
                    string varType = inVarObj.VarType.Trim().ToUpper();

                    //限定以下触点类型有效
                    if (!(varType == "X" || varType == "Y" || varType == "R" || varType == "T"
                       || varType == "C" || varType == "L"))
                    {
                        return rt;
                    }

                    //控制变量地址--4字节
                    string varAddress = inVarObj.Address.ToString().PadRight(4, '0');
                    //控制变量对象状态值--true--导通--1,false--断开--0
                    string level;
                 
                    if (isOnOff)
                        level = "1";
                    else
                        level = "0";

                    string cmd = string.Format("%{0}#WCS{1}{2}{3}", stationNum, varType, varAddress,level);
                    string cmdChk = ProCommon.Communal.ToolFunctions.AddCheckCode(cmd);

                    //向串口写入指令数据
                    stationNum = WriteData(cmdChk, 50);

                    //串口无返回结果,则获取输入变量对象状态失败,返回
                    if (string.IsNullOrEmpty(stationNum)) return rt;

                    //根据串口返回结果字符串--索引位置为3的字符:"$"--响应正常,"!"--响应异常
                    switch (stationNum.Substring(3, 1))
                    {
                        case "$":
                            rt = true;
                            inVarObj.NewValue = isOnOff;
                            break;
                        default: break;
                    }
                }
            }
            catch (System.Exception ex) { }           
            return rt;
        }

        /// <summary>
        /// 获取多个输入变量对象的状态
        /// </summary>
        /// <param name="inVarObjList"></param>
        /// <param name="isOnOff"></param>
        /// <returns></returns>
        protected override bool DoGetPluralInvarStatus(InVarObjList inVarObjList, out bool[] isOnOff)
        {
            bool rt = false;
            if (inVarObjList != null)
            {
                int cnt = inVarObjList.Count;
                isOnOff = new bool[cnt];               
                try
                {
                    //最多8个触点
                    if(cnt<9)
                    {
                        // 站号--2字节
                        string stationNum = string.Empty;

                        // 变量类型--1字节
                        string varType = string.Empty;

                        //控制变量地址--4字节
                        string varAddress = string.Empty;
                        string sectionArr = string.Empty;

                        for (int i = 0; i < cnt; i++)
                        {
                            if (i == 0)
                                stationNum = inVarObjList[i].StationNumber;

                            varType = inVarObjList[i].VarType.Trim().ToUpper();

                            //限定以下触点类型有效
                            if (!(varType == "X" || varType == "Y" || varType == "R" || varType == "T"
                               || varType == "C" || varType == "L"))
                            {
                                return rt;
                            }

                            varAddress = inVarObjList[i].Address.ToString().PadRight(4, '0');
                            sectionArr += (varType + varAddress);
                        }

                        string cmd = string.Format("%{0}#RCP{1}{2}", stationNum, cnt.ToString(), sectionArr);
                        string cmdChk = ProCommon.Communal.ToolFunctions.AddCheckCode(cmd);
                        //向串口写入指令数据
                        stationNum = WriteData(cmdChk, 50);

                        //串口无返回结果,则获取输入变量对象状态失败,返回
                        if (string.IsNullOrEmpty(stationNum)) return rt;

                        //根据串口返回结果字符串--索引位置为3的字符:"$"--响应正常,"!"--响应异常
                        switch (stationNum.Substring(3, 1))
                        {
                            case "$":
                                rt = true;
                                //根据串口返回结果字符串--索引位置为6的字符："0"--断开,"1"--导通
                                for (int i = 0; i < cnt; i++)
                                {
                                    //根据串口返回结果字符串--索引位置为6的字符："0"--断开(OFF),"1"--导通(ON)
                                    isOnOff[i] = (stationNum.Substring(6 + i, 1) == "1") ? true : false;
                                    inVarObjList[i].NewValue = isOnOff[i];
                                }
                                break;
                            default: break;
                        }
                    }
                }
                catch (System.Exception ex) { }
            }
            else isOnOff = null;
            
            return rt;
        }

        /// <summary>
        /// 设置多个输入变量对象的状态
        /// </summary>
        /// <param name="inVarObjList"></param>
        /// <param name="isOnOff"></param>
        /// <returns></returns>
        protected override bool DoSetPluralInvarStatus(InVarObjList inVarObjList, bool[] isOnOff)
        {
            bool rt = false;
            if (inVarObjList != null)
            {
                int cnt = inVarObjList.Count;
                int tnc = isOnOff.Length;

                try
                {
                    if (cnt == tnc)
                    {
                        //最多8个触点
                        if (cnt < 9)
                        {
                            // 站号--2字节
                            string stationNum = string.Empty;

                            // 变量类型--1字节
                            string varType = string.Empty;

                            //控制变量地址--4字节
                            string varAddress = string.Empty;
                            //控制变量状态
                            string state = string.Empty;
                            string sectionArr = string.Empty;

                            for (int i = 0; i < cnt; i++)
                            {
                                if (i == 0)
                                    stationNum = inVarObjList[i].StationNumber;

                                varType = inVarObjList[i].VarType.Trim().ToUpper();

                                //限定以下触点类型有效
                                if (!(varType == "X" || varType == "Y" || varType == "R" || varType == "T"
                                   || varType == "C" || varType == "L"))
                                {
                                    return rt;
                                }

                                varAddress = inVarObjList[i].Address.ToString().PadRight(4, '0');
                                state = isOnOff[i] ? "1" : "0";
                                sectionArr += (varType + varAddress+ state);
                            }

                            string cmd = string.Format("%{0}#WCP{1}{2}", stationNum, cnt.ToString(), sectionArr);
                            string cmdChk = ProCommon.Communal.ToolFunctions.AddCheckCode(cmd);
                            //向串口写入指令数据
                            stationNum = WriteData(cmdChk, 50);

                            //串口无返回结果,则获取输入变量对象状态失败,返回
                            if (string.IsNullOrEmpty(stationNum)) return rt;

                            //根据串口返回结果字符串--索引位置为3的字符:"$"--响应正常,"!"--响应异常
                            switch (stationNum.Substring(3, 1))
                            {
                                case "$":
                                    rt = true;                                  
                                    for (int i = 0; i < cnt; i++)
                                        inVarObjList[i].NewValue = isOnOff[i];
                                    break;
                                default: break;
                            }
                        }
                    }
                }
                catch (System.Exception ex) { }
            }
            return rt;
        }

        /// <summary>
        /// 获取输入控制变量地址起始,指定个数数据寄存器的数据
        /// </summary>
        /// <param name="inVarObj"></param>
        /// <param name="dData"></param>
        /// <returns></returns>
        protected override bool DoGetInvarData(InVarObj inVarObj, int num, out double[] dData)
        {
            bool rt = false;
            dData = new double[num];
            try
            {
                if (inVarObj != null)
                {
                    // 站号--2字节
                    string stationNum = inVarObj.StationNumber;

                    // 变量类型--1字节
                    string varType = inVarObj.VarType.Trim().ToUpper();

                    //限定以下触点类型有效
                    if (!(varType == "D" || varType == "L" || varType == "F"))
                    {
                        return rt;
                    }

                    //控制变量地址--5字节
                    string varAddressStart = inVarObj.Address.ToString().PadRight(5,'0');
                    string varAddressEnd = (inVarObj.Address+num).ToString().PadRight(5,'0');

                    string cmd = string.Format("%{0}#RD{1}{2}", stationNum, varType, varAddressStart+varAddressEnd);
                    string cmdChk = ProCommon.Communal.ToolFunctions.AddCheckCode(cmd);
                    //向串口写入指令数据
                    stationNum = WriteData(cmdChk, 50);

                    //串口无返回结果,则获取输入变量对象状态失败,返回
                    if (string.IsNullOrEmpty(stationNum)) return rt;

                    //根据串口返回结果字符串--索引位置为3的字符:"$"--响应正常,"!"--响应异常
                    switch (stationNum.Substring(3, 1))
                    {
                        case "$":
                            rt = true;
                            string dt = string.Empty;
                            string[] dtArr = null;
                            dt = stationNum.Substring(6, stationNum.Length-6-2); //从索引6开始,获取除开始6个字符以及2个结尾校验字符后的子字符串
                            dtArr = ProCommon.Communal.ToolFunctions.ReverseHighLow(dt); //高低位呼唤后的字符串数组
                            if(dtArr!=null)
                                for (int i = 0; i < dtArr.Length; i++)
                                    dData[i] = ProCommon.Communal.ToolFunctions.HexadecimalToDecimal(dtArr[i]);
                            break;
                        default: break;
                    }
                }
            }
            catch (System.Exception ex) { }
            return rt;
        }

        /// <summary>
        /// 设置输入控制变量地址起始,指定个数数据寄存器的数据
        /// </summary>
        /// <param name="inVarObj"></param>
        /// <param name="num"></param>
        /// <param name="dData"></param>
        /// <returns></returns>
        protected override bool DoSetInvarData(InVarObj inVarObj, int num, double[] dData)
        {
            bool rt = false;           
            try
            {
                if (inVarObj != null
                    && dData!=null)
                {
                    if(dData.Length==num)
                    {
                        // 站号--2字节
                        string stationNum = inVarObj.StationNumber;

                        // 变量类型--1字节
                        string varType = inVarObj.VarType.Trim().ToUpper();

                        //限定以下触点类型有效
                        if (!(varType == "D" || varType == "L" || varType == "F"))
                        {
                            return rt;
                        }

                        //控制变量地址--5字节
                        string varAddressStart = inVarObj.Address.ToString().PadRight(5, '0');
                        string varAddressEnd = (inVarObj.Address + num).ToString().PadRight(5, '0');

                        string cmd = string.Format("%{0}#WD{1}{2}", stationNum, varType, varAddressStart + varAddressEnd);
                        string cmdChk = ProCommon.Communal.ToolFunctions.AddCheckCode(cmd);

                        //向串口写入指令数据
                        stationNum = WriteData(cmdChk, 50);

                        //串口无返回结果,则获取输入变量对象状态失败,返回
                        if (string.IsNullOrEmpty(stationNum)) return rt;

                        //根据串口返回结果字符串--索引位置为3的字符:"$"--响应正常,"!"--响应异常
                        switch (stationNum.Substring(3, 1))
                        {
                            case "$":
                                rt = true;
                                string dt = string.Empty;
                                string[] dtArr = null;
                                dt = stationNum.Substring(6, stationNum.Length - 6 - 2); //从索引6开始,获取除开始6个字符以及2个结尾校验字符后的子字符串
                                dtArr = ProCommon.Communal.ToolFunctions.ReverseHighLow(dt); //高低位呼唤后的字符串数组
                                if (dtArr != null)
                                    for (int i = 0; i < dtArr.Length; i++)
                                        dData[i] = ProCommon.Communal.ToolFunctions.HexadecimalToDecimal(dtArr[i]);
                                break;
                            default: break;
                        }
                    }
                }
            }
            catch (System.Exception ex) { }
            return rt;
        }

        public override string ToString()
        {
            return "PlcDriver[SerialPort]";
        }

        /// <summary>
        /// 串口写入指令数据
        /// </summary>
        /// <param name="cmdData">指令数据</param>
        /// <param name="delayTime">写后延时再读取,单位毫秒</param>
        /// <returns></returns>
        private string WriteData(string cmdData,int delayTime)
        {
            string rt = string.Empty;
            lock(_comSyncObj)
            {
                try
                {
                    if(_serialPort!=null)
                    {
                        _serialPort.WriteLine(cmdData);
                        System.Threading.Thread.Sleep(delayTime);
                        rt = _serialPort.ReadLine();
                    }
                }catch(System.Exception ex)
                {
                    if (ExceptionOccuredDel != null)
                        ExceptionOccuredDel("Write command error.\r\n" + ex.Message);
                }
            }

            return rt;
        }

       
    }
}
