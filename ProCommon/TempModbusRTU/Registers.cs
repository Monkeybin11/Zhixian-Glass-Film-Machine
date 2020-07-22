using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCData
{
    /// <summary>
    /// 寄存器
    /// </summary>
    public class Registers
    {
        private readonly InterDriver _driver;

        public List<NumberData<ushort>> RegisterBlock = new List<NumberData<ushort>>();

        private List<NumberData<ushort>> writeBlock = new List<NumberData<ushort>>();

        internal Registers(InterDriver driver)
        {
            this._driver = driver;
        }

        public void ReadRegisters()
        {
            for (int i = 0; i < RegisterBlock.Count; i++)
            {
                List<NumberData<ushort>> buff = new List<NumberData<ushort>>();
                buff.Add(RegisterBlock[i]);
                int continuousSubscript = i;
                //const int ReadInv = 10;
                while (continuousSubscript + 1 < RegisterBlock.Count &&                                                     // 限制在数组内部
                    RegisterBlock[continuousSubscript].Num + 1 == RegisterBlock[continuousSubscript + 1].Num &&             // 表示寄存器下标连续
                    buff.Count < PLCDevice.Read_Write_Ushort_Max_Number)                                                    // 读取的寄存器个数小于限制
                {
                    //for (int j = RegisterBlock[continuousSubscript].Num + 1; j <= RegisterBlock[continuousSubscript + 1].Num; j++)
                    //{ 

                    //}
                    buff.Add(RegisterBlock[continuousSubscript + 1]);
                    continuousSubscript++;
                    i++;
                }

                var data = _driver.Master.ReadHoldingRegisters(_driver.Slave, (ushort)buff[0].Num, (ushort)buff.Count);
                for (int j = 0; j < buff.Count; j++)
                {
                    ReadChangeRegisters((ushort)buff[j].Num, data[j]);
                }
            }
        }

        private void ReadChangeRegisters(ushort address, ushort value)
        {
            for (int i = 0; i < RegisterBlock.Count; i++)
            {
                if (RegisterBlock[i].Num == address)
                {
                    RegisterBlock[i] = new NumberData<ushort>(RegisterBlock[i].Num, value);
                    return;
                }
            }
        }

        private void WriteRegisters()
        {
            List<NumberData<ushort>> memory = writeBlock.ToList();
            writeBlock.Clear();
            for (int i = 0; i < memory.Count; i++)
            {
                List<NumberData<ushort>> buff = new List<NumberData<ushort>>();
                buff.Add(memory[i]);
                int continuousSubscript = i;
                while (continuousSubscript + 1 < memory.Count &&
                    memory[continuousSubscript].Num + 1 == memory[continuousSubscript + 1].Num &&
                    buff.Count < PLCDevice.Read_Write_Ushort_Max_Number)
                {
                    buff.Add(memory[continuousSubscript + 1]);
                    continuousSubscript++;
                    i++;
                }

                _driver.Master.WriteMultipleRegisters(_driver.Slave, (ushort)buff[0].Num, buff.ConvertAll(data => data.Value).ToArray());
            }
        }

        Stopwatch stopwatch = new Stopwatch();
        public long time = 0;

        public void Synchro()
        {
            stopwatch.Restart();

            WriteRegisters();
            ReadRegisters();

            stopwatch.Stop();
            time = stopwatch.ElapsedMilliseconds;
        }

        #region 获取
        public ushort GetValueUshort(ushort address)
        {
            NumberData<ushort> data = new NumberData<ushort>(address, 0);
            int findIndex = RegisterBlock.IndexOf(data);
            if (findIndex == -1)
            {
                throw new ArgumentException("there is no address in read adress list, error address" + address, "address");
            }

            return RegisterBlock[findIndex].Value;
        }

        public int GetValueInt(ushort address)
        {
            ushort data1 = GetValueUshort(address);
            ushort data2 = GetValueUshort((ushort)(address + 1));

            return GetInt(data2, data1);
        }

        public float GetValueFloat(ushort address)
        {
            ushort data1 = GetValueUshort(address);
            ushort data2 = GetValueUshort((ushort)(address + 1));

            return GetFloat(data2, data1);
        }

        public UInt32 GetValueUInt(ushort address)
        {
            ushort data1 = GetValueUshort(address);
            ushort data2 = GetValueUshort((ushort)(address + 1));

            return GetUInt(data2, data1);
        }

        #endregion

        #region 写入

        public void SetValue(ushort address, ushort value)
        {
            ushort data1 = BitConverter.ToUInt16(BitConverter.GetBytes(value), 0);
            WriteChangeRegisters(address, data1);
        }

        public void SetValue(ushort address, UInt32 value)
        {
            ushort data1 = BitConverter.ToUInt16(BitConverter.GetBytes(value), 0);
            ushort data2 = BitConverter.ToUInt16(BitConverter.GetBytes(value), 2);
            WriteChangeRegisters(address, data1);
            WriteChangeRegisters((ushort)(address + 1), data2);
        }

        public void SetValue(ushort address, int value)
        {
            ushort data1 = BitConverter.ToUInt16(BitConverter.GetBytes(value), 0);
            ushort data2 = BitConverter.ToUInt16(BitConverter.GetBytes(value), 2);
            WriteChangeRegisters(address, data1);
            WriteChangeRegisters((ushort)(address + 1), data2);
        }

        public void SetValue(ushort address, float value)
        {
            ushort data1 = BitConverter.ToUInt16(BitConverter.GetBytes(value), 0);
            ushort data2 = BitConverter.ToUInt16(BitConverter.GetBytes(value), 2);
            WriteChangeRegisters(address, data1);
            WriteChangeRegisters((ushort)(address + 1), data2);
        }




        private void WriteChangeRegisters(ushort address, ushort value)
        {
            NumberData<ushort> data = new NumberData<ushort>(address, value);

            int readIndex = RegisterBlock.IndexOf(data);
            if (readIndex > -1)
            {
                RegisterBlock[readIndex] = data;
            }

            int writeIndex = writeBlock.IndexOf(data);
            if (writeIndex == -1)
            {
                writeBlock.Add(data);
            }
            else
            {
                writeBlock[writeIndex] = data;
            }
        }
        #endregion

        #region 添加监控

        public void Add(ushort address, Type type)
        {
            if (type == typeof(ushort) || type == typeof(short))
            {
                Add(new NumberData<ushort>(address, 0));
            }
            else if (type == typeof(int) || type == typeof(float))
            {
                Add(new NumberData<ushort>(address, 0));
                Add(new NumberData<ushort>(address + 1, 0));
            }
            else
            {
                throw new ArgumentException("type error", "type");
            }
          
        }

        private void Add(NumberData<ushort> data)
        {
            if (RegisterBlock.Contains(data))
            {
                throw new ArgumentException("address error");
            }

            RegisterBlock.Add(data);
            RegisterBlock.Sort();
        }

        #endregion





        private float GetFloat(ushort highOrderValue, ushort lowOrderValue)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(lowOrderValue).Concat(BitConverter.GetBytes(highOrderValue)).ToArray(), 0);
        }

        private int GetInt(ushort highOrderValue, ushort lowOrderValue)
        {
            return BitConverter.ToInt32(BitConverter.GetBytes(lowOrderValue).Concat(BitConverter.GetBytes(highOrderValue)).ToArray(), 0);
        }

        private UInt32 GetUInt(ushort highOrderValue, ushort lowOrderValue)
        {
            return BitConverter.ToUInt32(BitConverter.GetBytes(lowOrderValue).Concat(BitConverter.GetBytes(highOrderValue)).ToArray(), 0);
        }



        public List<NumberData<ushort>> NumberDatas
        {
            get
            {
                List<NumberData<ushort>> datas = new List<NumberData<ushort>>();
                for (int i = 0; i < RegisterBlock.Count; i++)
                {
                    datas.Add(RegisterBlock[i]);
                }
                return datas;
            }
        }
    }
}
