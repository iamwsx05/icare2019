using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.BIHOrder
{
    public partial class frmBloodPopup : Form
    {
        public frmBloodPopup(EntityBloodApply _vo)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, 0);
            this.VO = _vo;
        }

        EntityBloodApply VO { get; set; }

        private void frmBloodPopup_Load(object sender, EventArgs e)
        {
            if (this.VO.fappclass == 1)
            {
                this.ucBloodApplyForm1.Location = new Point(8, 5);
                this.ucBloodApplyForm1.Visible = true;
                this.ucBloodApplyForm2.Visible = false;
                this.ucBloodApplyForm1.Tag = this.VO;
                this.ucBloodApplyForm1.SetContent(weCare.Core.Utils.Function.ReadXmlNodes(this.VO.fappxml, "appData"));
            }
            else
            {
                this.ucBloodApplyForm2.Location = new Point(8, 5);
                this.ucBloodApplyForm2.Visible = true;
                this.ucBloodApplyForm1.Visible = false;
                this.ucBloodApplyForm2.Tag = this.VO;
                this.ucBloodApplyForm2.SetContent(weCare.Core.Utils.Function.ReadXmlNodes(this.VO.fappxml, "appData"));
            }
        }
    }
}
