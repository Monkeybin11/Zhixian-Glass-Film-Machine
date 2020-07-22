using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       ToolFunctions
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProCommon.Communal
 * File      Name：       ToolFunctions
 * Creating  Time：       4/21/2020 1:32:29 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProCommon.Communal
{
    public class ToolFunctions
    {
        /// <summary>
        /// 方法：字节数组转换为整型指针
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static System.IntPtr BytesToIntptr(byte[] bytes)
        {
            int size = bytes.Length;
            System.IntPtr bfIntPtr = System.Runtime.InteropServices.Marshal.AllocHGlobal(size);
            System.Runtime.InteropServices.Marshal.Copy(bytes, 0, bfIntPtr, size);
            return bfIntPtr;
        }

        /// <summary>
        /// 方法:十六进制字符串转十进制数
        /// </summary>
        /// <param name="hexstr"></param>
        /// <returns></returns>
        public static double HexadecimalToDecimal(string hexstr)
        {
            double rt = 0;
            string HEXSTR = hexstr.ToUpper();
            for (int i = 0; i < HEXSTR.Length; i++)
            {
                string temp = HEXSTR.Substring(HEXSTR.Length - i - 1, 1);//从右往左，低位到高位
                switch (temp)
                {
                    case "0":
                        rt += Math.Pow(16, i) * 0;
                        break;
                    case "1":
                        rt += Math.Pow(16, i) * 1;
                        break;
                    case "2":
                        rt += Math.Pow(16, i) * 2;
                        break;
                    case "3":
                        rt += Math.Pow(16, i) * 3;
                        break;
                    case "4":
                        rt += Math.Pow(16, i) * 4;
                        break;
                    case "5":
                        rt += Math.Pow(16, i) * 5;
                        break;
                    case "6":
                        rt += Math.Pow(16, i) * 6;
                        break;
                    case "7":
                        rt += Math.Pow(16, i) * 7;
                        break;
                    case "8":
                        rt += Math.Pow(16, i) * 8;
                        break;
                    case "9":
                        rt += Math.Pow(16, i) * 9;
                        break;
                    case "A":
                        rt += Math.Pow(16, i) * 10;
                        break;
                    case "B":
                        rt += Math.Pow(16, i) * 11;
                        break;
                    case "C":
                        rt += Math.Pow(16, i) * 12;
                        break;
                    case "D":
                        rt += Math.Pow(16, i) * 13;
                        break;
                    case "E":
                        rt += Math.Pow(16, i) * 14;
                        break;
                    case "F":
                        rt += Math.Pow(16, i) * 15;
                        break;
                }
            }
            return rt;
        }

        /// <summary>
        /// 方法:数据字符串高低位互换
        /// 注：4个字符作为一个有效数据
        /// </summary>
        /// <param name="str">数据字符串</param>
        /// <returns></returns>
        public static string[] ReverseHighLow(string str)
        {
            string[] rt = null;
            if (!string.IsNullOrEmpty(str))
            {
                int num = str.Length;
                if (num % 4 == 0)                                          //字符串字符个数是4的整数倍
                {
                    int k = (int)(num / 4);
                    string[] tempstr = new string[k];
                    for (int i = 0; i < k; i++)                            //每四个字符作为一个数据
                    {
                        string H8 = str.Substring(2 + i*4, 2);             //提取返回字符串指定位置字符（高低位互换）高位
                        string L8 = str.Substring(i*4, 2);                 //提取返回字符串指定位置字符（高低位互换）低位
                        tempstr[i] = H8 + L8;                              //临时结果字符串
                    }
                    rt = tempstr;
                }
            }
            return rt;
        }

        /// <summary>
        /// 方法:字符串添加校验码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string AddCheckCode(string str)
        {
            string r = "**";
            try
            {
                byte[] arrbyte = System.Text.ASCIIEncoding.ASCII.GetBytes(str);
                byte temp = arrbyte[0];
                for (int i = 1; i < arrbyte.Length; i++)
                {
                    temp ^= arrbyte[i];
                }

                r = Convert.ToString(temp, 16).PadLeft(2, '0').ToUpper();
            }
            catch { }
            return str + r;
        }

        /// <summary>
        /// 方法：边沿信号判断
        /// </summary>
        /// <param name="last"></param>
        /// <param name="now"></param>
        /// <returns></returns>
        public static ProCommon.Communal.EffectiveSignal JudgeEdge(bool last, bool now)
        {
            ProCommon.Communal.EffectiveSignal edge = ProCommon.Communal.EffectiveSignal.RaiseEdge;
            if (!last)
            {
                if (now)
                    edge = ProCommon.Communal.EffectiveSignal.RaiseEdge;
                else
                    edge = ProCommon.Communal.EffectiveSignal.LogicFalse;
            }
            else
            {
                if (!now)
                    edge = ProCommon.Communal.EffectiveSignal.FallEdge;
                else
                    edge = ProCommon.Communal.EffectiveSignal.LogicTrue;
            }

            return edge;
        }

        /// <summary>
        /// 方法：获取byte[]中指定起始和长度的字节段
        /// </summary>
        /// <param name="data"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] GetSubData(byte[] data, int startIndex, int length)
        {
            byte[] ret = new byte[length];
            System.Array.Copy(data, startIndex, ret, 0, length);
            return ret;
        }

        /// <summary>
        /// 根据Int类型的值,返回用1或0(对应true或false)填充的数组
        /// 注:从右侧开始向左索引(0~31)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static System.Collections.Generic.IEnumerable<bool> GetBitList(int value)
        {
            var list = new System.Collections.Generic.List<bool>(32);
            for (var i = 0; i <= 31; i++)
            {
                var val = 1 << i;
                list.Add((value & val) == val);
            }

            return list;
        }

        /// <summary>
        /// 返回Int数据中某一位是否为1
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index">
        /// 32位数据的从右向左的偏移位索引(0~31)</param>
        /// <returns>位置值
        /// true,1
        /// false,0</returns>
        public static bool GetBitValue(int value, ushort index)
        {
            if (index > 31) throw new ArgumentOutOfRangeException("index");

            var val = 1 << index;
            return ((value & val) == val);
        }

        /// <summary>
        /// 设置Int数据中的某一位的值
        /// </summary>
        /// <param name="value">位设置前的值</param>
        /// <param name="index">
        /// 32位数据的从右向左的偏移位索引(0~31)</param>
        /// <param name="bitValue">设置值
        /// true,设置1
        /// false,设置0</param>
        /// <returns>返回位设置后的值</returns>
        public static int SetBitValue(int value, ushort index, bool bitValue)
        {
            if (index > 31) throw new ArgumentOutOfRangeException("index");
            var val = 1 << index;
            return bitValue ? (value | val) : (value & ~val);
        }

        public static System.Reflection.PropertyInfo[] GetProperties<T>(T t)
        {
            System.Reflection.PropertyInfo[] properties =
                t.GetType().GetProperties(System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.Public);
            return properties;
        }

        /// <summary>
        /// 轴物理位置转换为脉冲数
        /// </summary>
        /// <param name="axis">轴</param>
        /// <param name="position">轴的物理位置</param>
        /// <returns></returns>
        public static int TransferPhysicalPositionToPulse(ProCommon.Communal.Axis axis, float position)
        {
            int pls = 0;
            if (axis != null)
            {
                axis.PulseUnit = axis.PulsePerRound / axis.HelicalPitch;
                pls = Convert.ToInt32(axis.PulseUnit * position);
            }
            return pls;
        }

        /// <summary>
        /// 轴脉冲位数转换为物理位置
        /// </summary>
        /// <param name="axis">轴</param>
        /// <param name="pulse">轴脉冲数</param>
        /// <returns></returns>
        public static float TransferPulseToPhysicalPosition(ProCommon.Communal.Axis axis, int pulse)
        {
            float pos = 0.0f;
            if (axis != null)
            {
                axis.PulseUnit = axis.PulsePerRound / axis.HelicalPitch;
                pos = Convert.ToSingle((float)pulse / axis.PulseUnit);
            }
            return pos;
        }
    }
}
