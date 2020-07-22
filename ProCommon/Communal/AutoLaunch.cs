using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       AutoLaunch
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProCommon.Communal
 * File      Name：       AutoLaunch
 * Creating  Time：       4/21/2020 1:26:06 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProCommon.Communal
{
    public class AutoLaunch
    {
        #region 程序开机启动的方法一:将程序的快捷方式创建到计算机的自动启动目录下(不需要管理员权限)

        /// <summary>
        /// 快捷方式的名称--任意自定义
        /// </summary>
        private string _quickName;
        public string QuickName
        {
            set { _quickName = value; }
            get
            {
                if (!string.IsNullOrEmpty(_quickName))
                    return _quickName;
                else
                    return "自启动用户程序";
            }
        }

        /// <summary>
        ///获取系统开机自启动目录
        ///[注:电脑登录账户]
        /// </summary>
        private string systemUserStartPath { get { return System.Environment.GetFolderPath(System.Environment.SpecialFolder.Startup); } }

        /// <summary>
        /// 获取系统开机自启动目录
        /// [注意:所有账户,此文件夹下的操作会被系统拒绝]
        /// </summary>
        private string systemCommonStartPath { get { return System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonStartup); } }


        /// <summary>
        /// 目标程序完整路径
        /// [注:WinForm应用程序*.exe]
        /// </summary>
        private string destinationFilePath;
        public string DestinationFilePath
        {
            set { destinationFilePath = value; }
            get
            {
                if (System.IO.File.Exists(destinationFilePath))
                    return destinationFilePath;
                else
                {
                    return System.Windows.Forms.Application.ExecutablePath;
                }
            }
        }

        /// <summary>
        /// 获取桌面目录
        /// </summary>
        private string desktopPath { get { return System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory); } }

        /// <summary>
        /// 设置开启自动启动
        /// [注:默认是开启]
        /// </summary>
        /// <param name="onOff"></param>
        public void SetAutoStart(bool onOff = true)
        {
            System.Collections.Generic.List<string> shortcutPaths = GetShortCutFromFolder(systemUserStartPath, DestinationFilePath);

            if (onOff)
            {
                if (shortcutPaths.Count >= 2)
                {
                    for (int i = 1; i < shortcutPaths.Count; i++)
                        DeleteFile(shortcutPaths[i]);
                }
                else if (shortcutPaths.Count == 0)
                {
                    CreateShortcut(systemUserStartPath, QuickName, DestinationFilePath, "自定义快捷方式");
                }
            }
            else
            {
                if (shortcutPaths.Count > 0)
                {
                    for (int i = 0; i < shortcutPaths.Count; i++)
                        DeleteFile(shortcutPaths[i]);
                }
            }
        }

        /// <summary>
        /// 向目标路径创建指定文件的快捷方式
        /// </summary>
        /// <param name="directory">快捷方式的目标目录</param>
        /// <param name="shortcutName">快捷方式名称</param>
        /// <param name="targetPath">快捷方式的指向文件路径</param>
        /// <param name="description">快捷方式的备注描述</param>
        /// <param name="iconLocation">快捷方式的图标路径</param>
        /// <returns></returns>
        private bool CreateShortcut(string directory, string shortcutName, string targetPath, string description = null, string iconLocation = null)
        {
            bool rt = false;
            try
            {
                //目录不存在，则创建目录
                if (!System.IO.Directory.Exists(directory)) System.IO.Directory.CreateDirectory(directory);

                string shortcutPath = System.IO.Path.Combine(directory, string.Format("{0}.lnk", shortcutName));

                //添加:Com 中搜索Windows Script Host Object Model
                IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutPath);

                //快捷方式的目标路径
                shortcut.TargetPath = targetPath;
                //快捷方式的起始位置
                shortcut.WorkingDirectory = System.IO.Path.GetDirectoryName(targetPath);
                //快捷方式的运行窗口,默认为常规窗口
                shortcut.WindowStyle = 1;
                //快捷方式的备注描述
                shortcut.Description = description;
                //快捷方式的图标路径
                shortcut.IconLocation = string.IsNullOrEmpty(iconLocation) ? targetPath : iconLocation;

                shortcut.Save();
                rt = true;

            }
            catch (System.Exception ex)
            {  }
            return rt;
        }

        /// <summary>
        /// 获取指定目录下指向目标路径的快捷方式
        /// </summary>
        /// <param name="directory">指定目录</param>
        /// <param name="targetPath">目标路径</param>
        /// <returns>指向目标文件路径的快捷方式集合</returns>
        private System.Collections.Generic.List<string> GetShortCutFromFolder(string directory, string targetPath)
        {
            System.Collections.Generic.List<string> shortcutList = new List<string>();
            shortcutList.Clear();

            string tempStr = string.Empty;
            string[] files = System.IO.Directory.GetFiles(directory, "*.lnk");
            if (files != null
                && files.Length >= 1)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    tempStr = GetAppPathFromShortCut(files[i]);
                    if (tempStr.Equals(targetPath))
                        shortcutList.Add(files[i]);
                }
            }
            return shortcutList;
        }

        /// <summary>
        /// 获取快捷方式指向的目标文件路径
        /// </summary>
        /// <param name="shortcutPath">快捷方式路径</param>
        /// <returns>快捷方式指向的目标文件路径</returns>
        private string GetAppPathFromShortCut(string shortcutPath)
        {
            if (System.IO.File.Exists(shortcutPath))
            {
                IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutPath);

                return shortcut.TargetPath;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 删除文件
        /// [注:用于取消开机自动启动时，从计算机自动启动目录中删除应用程序的快捷方式]
        /// </summary>
        /// <param name="filePath"></param>
        private void DeleteFile(string filePath)
        {
            System.IO.FileAttributes attr = System.IO.File.GetAttributes(filePath);
            if (attr == System.IO.FileAttributes.Directory)
            {
                System.IO.Directory.Delete(filePath, true);
            }
            else { System.IO.File.Delete(filePath); }
        }
        #endregion
    }
}
