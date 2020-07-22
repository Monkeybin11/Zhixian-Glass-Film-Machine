using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProLaminator.UI
{
    public partial class UICalibOnPoint : ProVision.Calibration.FrmCalibOnPoint
    {
        public UICalibOnPoint(ProVision.Communal.Language lan):base(lan)
        {
            InitializeComponent();
        }
    }
}
