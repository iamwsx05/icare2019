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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            shhow();
        }
        


        public void shhow()
        {
            //ctlTest a = new ctlTest();
            //a.Dispose();

            //MessageBox.Show(a.Dispose.ToString());
            string s1 = "2021-12-12";
            string s2 = "2021-01-12" ;
            string s3 = "2021-01-06";

            this.label1.Text = Function.Datetime(s1).ToString("yyyyMd") ;
            this.label2.Text = Function.Datetime(s2).ToString("yyyyMd");
            this.label3.Text = Function.Datetime(s3).ToString("yyyyMd");

        }
    }
}
