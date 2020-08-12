using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Common.Controls
{
    public partial class ctlPicbutton : System.Windows.Forms.PictureBox
    {
        public ctlPicbutton()
        {
            if (!DesignMode)
            {
                this.Cursor = Cursors.Hand;
                this.SizeMode = PictureBoxSizeMode.AutoSize;
                if (!IsShowBorder) this.BorderStyle = BorderStyle.None;
            }
        }

        public Image PictureImage
        {
            get
            {
                return this.Image;
            }
        }

        public void SetOnFocusImage()
        {
            if (this.OnFocusImg != null)
            {
                this.Image = this.OnFocusImg;
            }
        }

        public bool IsResetImg { get; set; }

        public Image OnFocusImg { get; set; }

        public Image LostFocusImg { get; set; }

        public Image DefaultImg { get; set; }

        public bool IsShowBorder { get; set; }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (IsShowBorder)
            {
                this.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                this.Location = new Point(this.Location.X - 1, this.Location.Y - 1);
                this.Size = new Size(this.Size.Width + 2, this.Height + 2);
            }
            if (this.DefaultImg == null) this.DefaultImg = this.Image;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (IsShowBorder)
            {
                this.BorderStyle = BorderStyle.None;
            }
            else
            {
                this.Location = new Point(this.Location.X + 1, this.Location.Y + 1);
                this.Size = new Size(this.Size.Width - 2, this.Height - 2);
            }
            base.OnMouseLeave(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (OnFocusImg != null)
            {
                Control ctlParent = this.Parent;
                if (ctlParent.HasChildren)
                {
                    foreach (Control ctl in ctlParent.Controls)
                    {
                        if (ctl is ctlPicbutton && ((ctlPicbutton)ctl).IsResetImg && ((ctlPicbutton)ctl).DefaultImg != null)
                        {
                            if (ctl != this)
                            {
                                ((PictureBox)ctl).Image = ((ctlPicbutton)ctl).DefaultImg;
                            }
                        }
                    }
                }

                this.Image = OnFocusImg;
            }
            base.OnMouseClick(e);
        }
    }
}
