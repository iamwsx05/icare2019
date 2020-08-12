using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Common.Controls
{
    public partial class ctlPicBase : DevExpress.XtraEditors.PictureEdit
    {
        public ctlPicBase()
        {
            this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.AllowDrop = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        }
      
    }
}
