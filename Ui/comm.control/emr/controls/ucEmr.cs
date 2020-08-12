using Common.Entity;
using weCare.Core.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Common.Controls.Emr
{
    public partial class ucEmr : UserControl
    {
        public ucEmr()
        {
            InitializeComponent();
        }

        public List<EntityFormCtrl> EfCtrls { get; set; }
    }
}
