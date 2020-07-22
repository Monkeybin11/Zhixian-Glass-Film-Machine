using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProLaminator
{
    static class Program
    {
        [System.Runtime.InteropServices.DllImport("User32.dll", EntryPoint = "ShowWindow", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        //hWnd窗口句柄，cmdShow指示窗口如何被显示(如果窗口可见，则返回非零，如果窗口被隐藏，则返回零)
        private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        //找到某个窗口与给出的类别名和窗口名相同窗口
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [System.Runtime.InteropServices.DllImport("User32.dll", SetLastError = true)]
        //切换到窗口并把窗口置入前台,fAltTab代表窗口正在通过Alt+Tab被切换
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        private const int WS_SHOWNORMAL = 1;
        public static IntPtr formhwnd;
        private const int WS_RESTORE = 9;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Diagnostics.Process currentpro = System.Diagnostics.Process.GetCurrentProcess();                             //获取当前运行进程实例
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName(currentpro.ProcessName);     //根据当前进程的进程名获取进程资源集合
           
            //程序已经运行
            if (processes.Length > 1)
            {
                foreach (System.Diagnostics.Process pro in processes)
                {
                    if (pro.Id != currentpro.Id)
                    {
                        //进程关联的主窗口的句柄为0，代表没有找到该窗体，即该窗体被隐藏的情况
                        if (pro.MainWindowHandle.ToInt32() == 0)
                        {
                            MessageBox.Show("程序已经运行在托盘", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //获取窗体句柄
                            formhwnd = FindWindow(null, "FrmMain");
                            //显示指定窗口
                            ShowWindow(formhwnd, WS_RESTORE);
                            //重新显示该窗体并切换置入前台
                            SwitchToThisWindow(formhwnd, true);
                        }
                        else
                        {
                            //进程句柄非0，窗体未隐藏，则切换到该窗体并置入前台
                            MessageBox.Show("程序已经运行", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ShowWindow(formhwnd, WS_RESTORE);
                            SwitchToThisWindow(pro.MainWindowHandle, true);
                        }
                    }
                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                DevExpress.UserSkins.BonusSkins.Register();
                DevExpress.Skins.SkinManager.EnableFormSkins();
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Office 2016 Colorful");

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                //****系统程序主窗体在系统设备资源正常开启之前,不要显示:防止用户在操作界面时,系统设备资源并未开启，造成异常****//
                //--------------------------------------------------------------------------------------------------//

                ProLaminator.Logic.SystemManager.Instance.CheckSystem(true); //进行系统自检:true--显示进度窗口;false--不显示进度窗口   
                if(ProLaminator.Logic.SystemManager.Instance.IsRunAllowed)
                {
                    string txt = "";
                    string caption = "";
                    bool _chkLangIsChinese = ProLaminator.Logic.SystemManager.ChkLangIsChinese;

                    try
                    {
                        Application.ApplicationExit += Application_ApplicationExit;

                        //3--根据配置文件,显示配置语言版本下窗体
                        ProLaminator.UI.FrmMain.Instance.Init();

                        //4-显示主窗体                  
                        Application.Run(ProLaminator.UI.FrmMain.Instance);

                        //5-系统退出
                        Environment.Exit(0);

                    }
                    catch (ProCommon.Communal.InitException initEx)
                    {
                        txt = _chkLangIsChinese ? "设备资源初始化异常!\r\n" : "Initialize device resources error !\r\n";
                        caption = _chkLangIsChinese ? "错误信息" : "Error Message";

                        ProCommon.DerivedForm.FrmMsgBox.Show(txt + initEx.Message, caption,
                            ProCommon.DerivedForm.MyButtons.OK,
                            ProCommon.DerivedForm.MyIcon.Error, _chkLangIsChinese);
                    }
                    catch (ProCommon.Communal.StartException startEx)
                    {
                        txt = _chkLangIsChinese ? "设备资源启动异常!\r\n" : "Start device resources error !\r\n";
                        caption = _chkLangIsChinese ? "错误信息" : "Error Message";

                        ProCommon.DerivedForm.FrmMsgBox.Show(txt + startEx.Message, caption,
                            ProCommon.DerivedForm.MyButtons.OK,
                            ProCommon.DerivedForm.MyIcon.Error, _chkLangIsChinese);
                    }
                    catch (System.Exception ex)
                    {
                        txt = _chkLangIsChinese ? "应用程序异常!\r\n" : "Application error !\r\n";
                        caption = _chkLangIsChinese ? "错误信息" : "Error Message";

                        ProCommon.Communal.LogWriter.WriteException(ProLaminator.Config.CfgManager.DirectoryBase
                          + ProLaminator.Config.CfgManager.DIRECTORY_NAME_FOR_LOG
                          + "\\" + ProLaminator.Config.CfgManager.EXCEPTION_LOG_FILE_NAME, ex);

                        ProCommon.Communal.LogWriter.WriteLog(ProLaminator.Config.CfgManager.DirectoryBase
                            + ProLaminator.Config.CfgManager.DIRECTORY_NAME_FOR_LOG
                            + "\\" + ProLaminator.Config.CfgManager.SYSTEM_LOG_FILE_NAME, "应用程序异常");

                        ProCommon.DerivedForm.FrmMsgBox.Show(txt + ex.Message, caption,
                            ProCommon.DerivedForm.MyButtons.OK,
                            ProCommon.DerivedForm.MyIcon.Error, _chkLangIsChinese);
                    }
                }          
            }
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            ProLaminator.Config.CfgManager.Instance.Save();
            ProLaminator.Device.CameraManager.Instance.Stop();
            ProLaminator.Device.CameraManager.Instance.Release();
        }
    }
}
