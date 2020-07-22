using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PLCData
{

    /// <summary>
    /// Y线圈
    /// </summary>
    public class OutputStatus
    {
        internal readonly InterDriver _driver;

        private BitArray bitArray = null;

        private List<NumberData<bool>> write = new List<NumberData<bool>>();

        // 恶心人，突然出现了一个排位很靠后的数据，为了效率先这样写，后期改动
        private List<NumberData<bool>> SingleCoilsRead = new List<NumberData<bool>>();

        public void AddSynchroSingleCoils(string numberID)
        {
            int num = _driver.Convert_PLCNumberID_To_ModubsAddress(numberID);
            SingleCoilsRead.Add(new NumberData<bool>(num, false));
        }



        internal OutputStatus(InterDriver driver, int length)
        {
            this._driver = driver;
            bitArray = new BitArray(length);
        }


        Stopwatch stopwatch = new Stopwatch();
        public long time = 0;

        public void Synchro()
        {
            stopwatch.Restart();
            WriteCoils();
            ReadCoils();
            stopwatch.Stop();
            time = stopwatch.ElapsedMilliseconds;
        }

        private void WriteCoils()
        {
            NumberData<bool>[] writebuff = write.ToArray();
            write.Clear();

            Array.Sort(writebuff);
            for (int i = 0; i < writebuff.Length; i++)
            {
                List<NumberData<bool>> buff = new List<NumberData<bool>>();
                buff.Add(writebuff[i]);
                int continuousSubscript = i;
                while (continuousSubscript + 1 < writebuff.Length &&
                    writebuff[continuousSubscript].Num + 1 == writebuff[continuousSubscript + 1].Num &&
                    buff.Count < PLCDevice.Read_Write_Bool_Max_Number)
                {
                    buff.Add(writebuff[continuousSubscript + 1]);
                    continuousSubscript++; 
                    i++;
                }

                WriteMultipleCoils(_driver.Slave, (ushort)buff[0].Num, buff.ConvertAll<bool>(io => io.Value).ToArray());
            }

        }

        public void ReadCoils()
        {
            ushort number = 0;
            while (number < bitArray.Length)
            {
                if (number + PLCDevice.Read_Write_Bool_Max_Number < bitArray.Length)
                {
                    ReadCoils(number, PLCDevice.Read_Write_Bool_Max_Number);
                }
                else
                {
                    ReadCoils(number, (ushort)(bitArray.Length - number));
                }
                number += PLCDevice.Read_Write_Bool_Max_Number;
            }


            for (int i = 0; i < SingleCoilsRead.Count; i++)
            {
                bool[] inputs = ReadCoils(_driver.Slave, (ushort)SingleCoilsRead[i].Num, 1);
                SingleCoilsRead[i] = new NumberData<bool>(SingleCoilsRead[i].Num, inputs[0]);
            }


        }

        private void ReadCoils(ushort address, ushort number)
        {
            if (number < 1) return;
            bool[] inputs = ReadCoils(_driver.Slave, address, number);
            for (int i = 0; i < inputs.Length; i++)
            {
                bitArray.Set(address + i, inputs[i]);
            }
        }

        protected virtual void WriteMultipleCoils(byte slaveAddress, ushort startAddress, bool[] data)
        {
            _driver.Master.WriteMultipleCoils(slaveAddress, startAddress, data);
        }

        protected virtual bool[] ReadCoils(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            return _driver.Master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
        }

        /// <summary>
        /// 按照编号获取设置线圈
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
            set
            {
                if (number < 0 || number >= bitArray.Length)
                {
                    throw new ArgumentOutOfRangeException("number");
                }

                if (this[number] != value)
                {
                    bitArray.Set(number, value);
                    NumberData<bool> data = new NumberData<bool>(number, value);
                    if (write.Contains(data))
                    {
                        write[write.IndexOf(data)] = data;
                    }
                    else
                    {
                        write.Add(new NumberData<bool>(number, value));
                    }

                }
            }
        }

        /// <summary>
        /// 根据PLC的标号获取设置线圈
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool this[string numberID]
        {
            get
            {
                int num = _driver.Convert_PLCNumberID_To_ModubsAddress(numberID);

                for (int i = 0; i < SingleCoilsRead.Count; i++)
                {
                    NumberData<bool> data = SingleCoilsRead[i];
                    if (data.Num == num)
                    {
                        return data.Value;
                    }
                }

                return this[num];
            }
            set
            {
                int num = _driver.Convert_PLCNumberID_To_ModubsAddress(numberID);

                for (int i = 0; i < SingleCoilsRead.Count; i++)
                {
                    NumberData<bool> data = SingleCoilsRead[i];
                    if (data.Num == num)
                    {
                        if (data.Value != value)
                        {
                            data.Value = value;
                            SingleCoilsRead[i] = data;
                          
                            if (write.Contains(data))
                            {
                                write[write.IndexOf(data)] = data;
                            }
                            else
                            {
                                write.Add(data);
                            }
                        }

                        return;
                    }
                }

                this[num] = value;
            }
        }



        public List<NumberData<bool>> NumberDatas
        {
            get
            {
                List<NumberData<bool>> datas = new List<NumberData<bool>>();
                for (int i = 0; i < bitArray.Length; i++)
                {
                    datas.Add(new NumberData<bool>(i, bitArray[i]));
                }
                for (int i = 0; i < SingleCoilsRead.Count; i++)
                {
                    datas.Add(SingleCoilsRead[i]);
                }
                return datas;
            }
        }

        internal void SetNumberDatas(List<NumberData<bool>> datas)
        {
            int i = 0;
            for (; i < bitArray.Length; i++)
            {
                this[i] = datas[i].Value;
            }
            for (; i < datas.Count; i++)
            {
                write.Add(datas[i]);
            }
 
        }
    }

    /// <summary>
    /// R线圈
    /// </summary>
    public class CoilStatus : OutputStatus
    {
        private const ushort InnerCoilNo = 2048;

        internal CoilStatus(InterDriver driver, int length) : base(driver, length)
        {

        }

        protected override void WriteMultipleCoils(byte slaveAddress, ushort startAddress, bool[] data)
        {
            startAddress = (ushort)(startAddress + InnerCoilNo);
            base.WriteMultipleCoils(slaveAddress, startAddress, data);
        }

        protected override bool[] ReadCoils(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            startAddress = (ushort)(startAddress + InnerCoilNo);
            return base.ReadCoils(slaveAddress, startAddress, numberOfPoints);
        }

    }
}
