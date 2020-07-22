using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

namespace ProCommon.DerivedForm
{
    /// <summary>
    /// 系统自检过程显示窗口
    /// [DevExpress.XtraSplahsScreen在DevExpress.XtraEditors动态链接库文件]
    /// </summary>
    public partial class FrmCheckProgress : DevExpress.XtraSplashScreen.SplashScreen
    {
        protected int _dotCount = 0;
        protected System.Windows.Forms.Timer _timer;
        public System.ComponentModel.BackgroundWorker BackgrdWorker;       

        #region 必须覆写的成员

        protected internal virtual void _backgrdWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.mqpbCtrlProcess.Text = e.ProgressPercentage.ToString() + ":" + e.UserState.ToString();
        }

        #endregion

        public FrmCheckProgress()
        {
            InitializeComponent();
            Init();
        }

        public void StopTimer()
        {
            _timer.Stop();
            _timer.Enabled = false;
        }

        protected virtual void Init()
        {
            InitField();
        }

        protected virtual void InitField()
        {
            this.mqpbCtrlProcess.Properties.ShowTitle = true; 
            BackgrdWorker = new BackgroundWorker();
            BackgrdWorker.WorkerSupportsCancellation = true;
            BackgrdWorker.WorkerReportsProgress = true;
            BackgrdWorker.ProgressChanged += _backgrdWorker_ProgressChanged;

            //BackgrdWorker.RunWorkerCompleted += _backgrdWorker_RunWorkerCompleted;
            //BackgrdWorker.DoWork += _backgrdWorker_DoWork;

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 200;
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Enabled = true;
            _timer.Start();
        }      

        protected virtual void _timer_Tick(object sender, EventArgs e)
        {
            if (_dotCount++ > 3) _dotCount = 0;
            this.lblCtrlPrompt.Text = string.Format("{0}{1}", "Starting", GetDot(_dotCount));
            this.lblCtrlRight.Text = string.Format("{0}{1}", "Copyright © 2015-", GetYear());
        }

        /// <summary>
        /// 获取当前年份
        /// </summary>
        /// <returns></returns>
        protected virtual int GetYear()
        {
            int rt = System.DateTime.Now.Year;
            return (rt < 2010 ? 2010 : rt);
        }

        /// <summary>
        /// 获取字符串(符号:点)
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        protected virtual string GetDot(int count)
        {
            string rt = string.Empty;
            for (int i = 0; i < count; i++)
            {
                rt += ".";
            }
            return rt;
        }
    }
}