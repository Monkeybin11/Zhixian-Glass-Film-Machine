using Modbus.Device;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCData
{
    internal class InterDriver
    {
        public ModbusMaster Master;
        public byte Slave;

        public void Dispose()
        {
            if (Master != null)
            {
                Master.Dispose();
            }
        }


        public int Convert_PLCNumberID_To_ModubsAddress(string numberID)
        {
            string name = numberID.PadLeft(4, '0');
            if (name.Length != 4)
            {
                throw new ArgumentException("NumberID length must less than 4，error NumberID：" + numberID, "numberID");
            }

            string id = Convert.ToUInt32(name.Remove(3)).ToString("X") + name[3];
            return Convert.ToInt32(id, 16);
        }
    }


    /// <summary>
    /// X接点
    /// </summary>
    public class InputStatus
    {
        private readonly InterDriver _driver;

        private BitArray bitArray = null;

        internal InputStatus(InterDriver driver, int length)
        {
            this._driver = driver;
            bitArray = new BitArray(length);
        }

        Stopwatch stopwatch = new Stopwatch();
        public long time = 0;

        public void Synchro()
        {
            stopwatch.Restart();

            ushort number = 0;
            while (number < bitArray.Length)
            {
                if (number + PLCDevice.Read_Write_Bool_Max_Number < bitArray.Length)
                {
                    ReadInputs(number, PLCDevice.Read_Write_Bool_Max_Number);
                }
                else
                {
                    ReadInputs(number, (ushort)(bitArray.Length - number));
                }
                number += PLCDevice.Read_Write_Bool_Max_Number;
            }
            stopwatch.Stop();
            time = stopwatch.ElapsedMilliseconds;
        }

        private void ReadInputs(ushort address, ushort number)
        {
            if (number < 1) return;
            bool[] inputs = _driver.Master.ReadInputs(_driver.Slave, address, number);
            for (int i = 0; i < inputs.Length; i++)
            {
                bitArray.Set(address + i, inputs[i]);
            }
        }

        /// <summary>
        /// 按照编号获取输入
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool this[int number]
        {
            get
            {
                if (number < 0 || number >= bitArray.Length)
                {
                    throw new ArgumentOutOfRangeException("number");
                }

                return bitArray[number];
            }
        }

        /// <summary>
        /// 根据PLC的标号获取输入
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool this[string numberID]
        {
            get
            {
                int num = _driver.Convert_PLCNumberID_To_ModubsAddress(numberID);//                 Convert.ToInt32(number, 16);
                return this[num];
            }
        }
    }




}
