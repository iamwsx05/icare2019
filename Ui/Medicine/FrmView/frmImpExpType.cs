using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmImpExpType : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        public frmImpExpType()
        {
            InitializeComponent();
        }

        public override void CreateController()
        {
            this.objController = new com.digitalwave.iCare.gui.HIS.clsControlImpExpType();
            objController.Set_GUI_Apperance(this);
        }

        private void frmImpExpType_Load(object sender, EventArgs e)
        {
            ((clsControlImpExpType)this.objController).m_mthInit();
        }

        private void cmdNew_Click(object sender, EventArgs e)
        {
            ((clsControlImpExpType)this.objController).m_mthNew();
            this.txtName.Focus();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            ((clsControlImpExpType)this.objController).m_mthSave();
        }

        private void radShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radShowAll.Checked)
            {
                ((clsControlImpExpType)this.objController).m_mthShow(1);
            }
        }

        private void radShowDel_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radShowDel.Checked)
            {
                ((clsControlImpExpType)this.objController).m_mthShow(0);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            ((clsControlImpExpType)this.objController).m_mthDelete();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lsvTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lsvTypes.SelectedItems != null && this.lsvTypes.SelectedItems.Count == 1)
            {
                ((clsControlImpExpType)this.objController).m_mthShowData(this.lsvTypes.SelectedItems[0].Index);
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            ((clsControlImpExpType)this.objController).m_mthNew();
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cobFlag.Focus();
                this.cobFlag.DroppedDown = true;
            }
        }

        private void cobStorgeflag_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cmdSave.Focus();
            }
        }

        private void cobFlag_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cobStorgeflag.Focus();
                this.cobStorgeflag.DroppedDown = true;
            }
        }

    }
}