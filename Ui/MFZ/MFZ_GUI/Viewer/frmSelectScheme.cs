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
    /// ��μƻ�
    /// </summary>
    public partial class frmSelectScheme : Form
    {

        #region ���캯��

        public frmSelectScheme(int p_areaId, int p_schemeId)
        {
            InitializeComponent();
            areaId = p_areaId;
            schemeId = p_schemeId;
        }
 
        #endregion

        #region ˽�г�Ա

        private int areaId = -1;
        private int schemeId = -1; 
        
        #endregion

        #region ��������

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

        #region �¼�ʵ��

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
                MessageBox.Show("����İ���Ϊ��!");
                return;
            }

            if (doctors.Length > 0)
            {
                if (MessageBox.Show("����Ḳ��ԭ��������!�Ƿ����?", "������ʾ", MessageBoxButtons.YesNo) == DialogResult.Yes)
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