using Modbus.Device;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace PLCData
{
    public class PLCDevice
    {
        InterDriver driver = new InterDriver();
        public const int Read_Write_Bool_Max_Number = 1000;
        public const int Read_Write_Ushort_Max_Number = 100;
        private SerialPort serialPort;
        private readonly BackgroundWorker RunThread;
        /// <summary>
        /// X输入
        /// </summary>
        public readonly InputStatus X;
        /// <summary>
        /// Y输出
        /// </summary>
        public readonly OutputStatus Y;
        /// <summary>
        /// 内部R线圈
        /// </summary>
        public readonly CoilStatus R;
        /// <summary>
        /// 寄存器
        /// </summary>
        public readonly Registers Registers;

        public PLCDevice(SerialPort _sPort, byte _slave = 1)
        {
            Debug.Assert(_sPort != null, "Argument serialPort cannot be null.");
            serialPort = _sPort;
            driver.Slave = _slave;

            X = new InputStatus(this.driver, 1);
            Y = new OutputStatus(this.driver, 1);
            R = new CoilStatus(this.driver, 1);
            Registers = new Registers(this.driver);
            InitReadReisters();

            RunThread = new BackgroundWorker();
            RunThread.WorkerSupportsCancellation = true;
            RunThread.DoWork += RunThread_DoWork;
            RunThread.RunWorkerAsync();
        }

        private void RunThread_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                ConnectLogic();
            }
        }


        private int step;
        public bool Connected { get; private set; }


        Stopwatch stopwatch = new Stopwatch();
        public long time = 0;

        private void ConnectLogic()
        {
            switch (step)
            {
                case 0: // 打开串口，链接PLC
                    try
                    {
                        InitializeDriver();
                        step = 1;
                    }
                    catch(Exception ex)
                    {
                        step = 0;
                        driver.Dispose();
                        Thread.Sleep(100);
                    }
                    break;

                case 1: // 初始化读一遍PLC的数据
                    try
                    {
                        X.Synchro();
                        Y.ReadCoils();
                        R.ReadCoils();
                        Registers.ReadRegisters();
                        step = 2;
                    }
                    catch
                    {
                        driver.Dispose();
                        Connected = false;
                        step = 4;
                    }
                    break;
                case 2: // 一直循环通讯
                    try
                    {
                        stopwatch.Restart();
                        X.Synchro();
                        Y.Synchro();
                        R.Synchro();
                        Registers.Synchro();
                        stopwatch.Stop();
                        time = stopwatch.ElapsedMilliseconds;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        driver.Dispose();
                        Connected = false;
                        step = 4;
                    }
                    break;

                case 3:
                    break;

                case 4: // 重连
                    try
                    {
                        InitializeDriver();
                        step = 2;
                    }
                    catch (Exception ex)
                    {
                        step = 4;
                        driver.Dispose();
                        Thread.Sleep(1000);
                    }
                    break;
            }
        }

        private void InitializeDriver()
        {
            try
            {
                if (serialPort.IsOpen == false)
                {
                    serialPort.Open();
                }

                driver.Master = ModbusSerialMaster.CreateRtu(serialPort);
                driver.Master.Transport.Retries = 0;   //don't have to do retries
                driver.Master.Transport.ReadTimeout = 300; //milliseconds
                driver.Master.ReadHoldingRegisters(driver.Slave, 0, 1);
                Connected = true;
            }
            catch
            {
                throw;
            }
        }

        private void InitReadReisters()
        {
            //需要读取的寄存器地址

            Registers.Add(1100, typeof(ushort));
            Registers.Add(1101, typeof(float));
            Registers.Add(1103, typeof(float));
            Registers.Add(1105, typeof(float));

            //R.AddSynchroSingleCoils("5040");
            //R.AddSynchroSingleCoils("5041");

            //R.AddSynchroSingleCoils("5045");
            //R.AddSynchroSingleCoils("5046");
            //R.AddSynchroSingleCoils("504a");
            //R.AddSynchroSingleCoils("5047");
            //R.AddSynchroSingleCoils("5048");
            //R.AddSynchroSingleCoils("504c");
            //R.AddSynchroSingleCoils("504e");




            //Registers.Add(12, typeof(float));
            //Registers.Add(30000, typeof(float));
            //Registers.Add(30002, typeof(float));
            //Registers.Add(30004, typeof(float));
            //Registers.Add(30006, typeof(float));

            //for (int i = 32400; i < 32644; i += 2)
            //{
            //    Registers.Add((ushort)i, typeof(float));
            //}
        }

        public void SavePLCData(string fileName)
        {
            PLCData pLCData = new PLCData();
            pLCData.R = this.R.NumberDatas;
            pLCData.Registers = this.Registers.NumberDatas;

            FileInfo file = new FileInfo(fileName);
            if (file.Directory.Exists == false)
            {
                file.Directory.Create();
            }

            using (Stream stream = file.Create())
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(pLCData.GetType());
                xs.Serialize(stream, pLCData);
            }
            
        }

        public void LoadPLCData(string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            if (file.Exists == false)
            {
                throw new FileNotFoundException();
            }

            PLCData pLCData = null;
            using (Stream stream = file.OpenRead())
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(PLCData));
                pLCData = xs.Deserialize(stream) as PLCData;
            }

            if (pLCData != null)
            {
                this.R.SetNumberDatas(pLCData.R);
                for (int i = 0; i < pLCData.Registers.Count; i++)
                {
                    this.Registers.SetValue((ushort)pLCData.Registers[i].Num, pLCData.Registers[i].Value);
                }
            }
        }

        private class PLCData
        {
            public List<NumberData<bool>> R;
            public List<NumberData<ushort>> Registers;
        }
    }
}
