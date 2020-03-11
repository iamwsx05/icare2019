using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Utils;

namespace test
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();

            string time = "2020-02-07 22:00:00";
            DateTime dt = Function.Datetime(time);
            double a = dt.AddHours(-2).TimeOfDay.TotalSeconds ;
            int index =  (int)(a) / (4 * 3600);
            this.textEdit1.Text = index.ToString();
        }


    }
}
