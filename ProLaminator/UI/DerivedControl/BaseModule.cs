using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProLaminator.UI.DerivedControl
{
    public partial class BaseModule : ProLaminator.UI.DerivedControl.AuxiliaryControl
    {
        public BaseModule()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// 语言版本
        /// </summary>
        protected ProCommon.Communal.Language LangVersion { set; get; }

        /// <summary>
        /// 打印组件
        /// </summary>
        protected virtual DevExpress.XtraPrinting.IPrintable PrintableComponent { get; set; }

        /// <summary>
        /// 导出组件
        /// </summary>
        protected virtual DevExpress.XtraPrinting.IPrintable ExportComponent { get; set; }

        private string _moduleName;

        /// <summary>
        /// 模块名称
        /// </summary>
        protected virtual string ModuleName { get { return _moduleName; } }

        private void Init()
        {
            InitFieldAndProperty();
        }

        private void InitFieldAndProperty()
        {
            _moduleName = string.Empty;
        }

        /// <summary>
        /// 模块所属主窗体
        /// [注:一般定义为项目界面主窗体]
        /// </summary>
        public ProLaminator.UI.FrmMain OwnerForm { get { return this.FindForm() as ProLaminator.UI.FrmMain; } }

        protected virtual void ShowModule(bool isFirstShow)
        {
            if (OwnerForm == null) return;
            ShowInfo();
        }

        protected virtual void ShowInfo()
        {
            if (OwnerForm == null) return;
            else
            {
                OwnerForm.ShowInfo();
            }
        }

        protected virtual void InitModule(DevExpress.Utils.Menu.IDXMenuManager manager, object data)
        {
            SetMenuManager(this.Controls, manager);
        }

        /// <summary>
        /// 为控件添加管理器
        /// </summary>
        /// <param name="controls"></param>
        /// <param name="manager"></param>
        private void SetMenuManager(ControlCollection controls, DevExpress.Utils.Menu.IDXMenuManager manager)
        {
            foreach (System.Windows.Forms.Control ctrl in controls)
            {
                //当前控件满足属于某种类型的控件,则为其添加管理器

                //当前控件的子控件,添加管理器
                SetMenuManager(ctrl.Controls, manager);
            }
        }
    }
}
