using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using weCare.Core.Entity;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Data;
using com.digitalwave.Emr.Signature_gui;
using com.digitalwave.Utility.Controls;
using iCare;

namespace iCare
{
    public partial class frmIMR_GestationMisbirthsthrees_F2 : iCare.frmBaseCaseHistory
    {
        private clsEmrSignToolCollection m_objSign;
        /// <summary>
        /// ��������������¼������һ��
        /// </summary>
        public frmIMR_GestationMisbirthsthrees_F2()
        {
            InitializeComponent();
            m_objSign = new clsEmrSignToolCollection();
            m_mthSetRichTextBoxAttribInControl(this);
         //   iCare.frmInpatMedRecBase ba = new frmInpatMedRecBase();
            m_objSign.m_mthBindEmployeeSign(m_cmdSurgeryer, m_txtSurgeryer, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);
            m_objSign.m_mthBindEmployeeSign(m_cmdDealer, m_txtDealer, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);
        }


        protected override void m_mthPerformSessionChanged(clsEmrPatientSessionInfo_VO p_objSelectedSession, int p_intIndex)
        {
            if (p_objSelectedSession == null) return;
            base.m_mthPerformSessionChanged(p_objSelectedSession, p_intIndex);
            if (p_objSelectedSession != null)
            {
                this.m_lblInpatientDate.Text = p_objSelectedSession.m_dtmHISInpatientDate.ToString("yyyy��MM��dd�� HHʱmm��ss��");
            }
        }





    }
}