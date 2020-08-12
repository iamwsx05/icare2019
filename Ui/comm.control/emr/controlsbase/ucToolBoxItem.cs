using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Common.Entity;

namespace Common.Controls.Emr
{
    public partial class ucToolBoxItem : UserControl
    {
        public Color InitColor { get; set; }

        public enumFormObject EnumEfObj { get; set; }

        public ucToolBoxItem()
        {
            InitializeComponent();
            this.BackColor = InitColor;
            this.lblCaption.Font = new System.Drawing.Font("宋体", 9F);
        }

        public string ItemName { get; set; }

        public Image CaptionImage
        {
            set { picCaption.Image = value; }
        }

        public string CaptionDesc
        {
            set { lblCaption.Text = value; }
        }

        private void lblCaption_MouseEnter(object sender, EventArgs e)
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.FromArgb(182, 189, 210);
        }

        private void lblCaption_MouseLeave(object sender, EventArgs e)
        {
            this.BorderStyle = BorderStyle.None;
            this.BackColor = this.InitColor;
        }
        
    }
}
