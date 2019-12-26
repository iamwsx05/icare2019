using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MFZ
{
    public partial class frmAddRoom : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        private string m_strDeptId;
        private string m_strDeptName;
        private string m_strAreaName;
        private int m_intAreaId;

        public frmAddRoom(int areaId,string deptId,string deptName,string areaName)
        {
            InitializeComponent();

            this.m_intAreaId = areaId;
            this.m_strDeptId = deptId;
            this.m_strDeptName = deptName;
            this.m_strAreaName = areaName;
        }

        private void m_cmdSubmit_Click(object sender, EventArgs e)
        {
            if (this.m_txtRoom.Text.Trim()==string.Empty)
            {
                DialogResult = DialogResult.None;
                return;
            }
            clsMFZRoomVO objRoom = new clsMFZRoomVO();
            objRoom.m_strRoomName = this.m_txtRoom.Text.Trim();
            objRoom.m_strSummary = this.m_txtSummary.Text;
            objRoom.m_strDeptID = m_strDeptId;
            objRoom.m_intAreaId = m_intAreaId;
            long lngRes = clsTmdRoomSmp.s_object.m_lngInsert(objRoom);
            DialogResult = DialogResult.Yes;
        }


        private void frmAddRoom_Load(object sender, EventArgs e)
        {
            this.m_lblAreaName.Text = this.m_strAreaName;
            this.m_lblDeptName.Text = this.m_strDeptName;

            this.m_txtRoom.Select();
            this.m_txtRoom.Focus();
        }

        private void frmAddRoom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                m_cmdSubmit_Click(sender, e);
            }
        }
    }
}