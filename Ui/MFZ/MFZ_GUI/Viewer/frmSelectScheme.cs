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

    /// <summary>
    /// 班次计划
    /// </summary>
    public partial class frmSelectScheme : Form
    {

        #region 构造函数

        public frmSelectScheme(int p_areaId, int p_schemeId)
        {
            InitializeComponent();
            areaId = p_areaId;
            schemeId = p_schemeId;
        }
 
        #endregion

        #region 私有成员

        private int areaId = -1;
        private int schemeId = -1; 
        
        #endregion

        #region 辅助方法

        private void UpdateRooms(clsMFZDoctorVO[] importDoctors)
        {
            clsTmdDoctorSmp.s_object.m_lngDelete(areaId, schemeId);

            foreach (clsMFZDoctorVO importDoct in importDoctors)
            {
                clsMFZDoctorVO tempDoct = importDoct;
                tempDoct.m_intSchemeSeq = schemeId;
                clsTmdDoctorSmp.s_object.m_lngInsert(tempDoct);
            }
        } 

        #endregion

        #region 事件实现

        private void frmSelectScheme_Load(object sender, EventArgs e)
        {

        }

        private void m_cmdImport_Click(object sender, EventArgs e)
        {
            int importSchemeId = this.ctlCboScheme.SchemeID;
            clsMFZDoctorVO[] importDoctors;
            clsMFZDoctorVO[] doctors;
            clsTmdDoctorSmp.s_object.m_lngFindDoctorsByAreaID(areaId, importSchemeId, out importDoctors);
            clsTmdDoctorSmp.s_object.m_lngFindDoctorsByAreaID(areaId, schemeId, out doctors);
            if (importDoctors == null)
            {
                importDoctors = new clsMFZDoctorVO[0];
            }
            if (doctors == null)
            {
                doctors = new clsMFZDoctorVO[0];
            }

            if (importDoctors.Length == 0)
            {
                MessageBox.Show("导入的安排为空!");
                return;
            }

            if (doctors.Length > 0)
            {
                if (MessageBox.Show("导入会覆盖原来的数据!是否继续?", "导入提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    UpdateRooms(importDoctors);
                }
            }
            else
            {
                UpdateRooms(importDoctors);
            }
            this.DialogResult = DialogResult.Yes;
            this.Hide();
        }

        private void m_cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        } 

        #endregion
    }
}