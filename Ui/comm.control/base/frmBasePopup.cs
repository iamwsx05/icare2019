using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;
using Common.Entity;

namespace Common.Controls
{
    public partial class frmBasePopup : frmBase
    {
        public frmBasePopup()
        {
            InitializeComponent();           
        }

        private void frmBasePopup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
