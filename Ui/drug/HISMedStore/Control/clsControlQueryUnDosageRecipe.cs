using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.iCare.middletier.HIS;	//his_svc.dll
using weCare.Core.Entity;
using System.Collections;
using com.digitalwave.iCare.gui.HIS;
using System.Xml;
using System.IO;
using System.Drawing.Printing;
using com.digitalwave.iCare.middletier.HI; 
using System.Collections.Generic;
using com.digitalwave.Utility;
using Sybase.DataWindow;


namespace com.digitalwave.iCare.gui.HIS
{   
    /// <summary>
    /// ��ҩ���Ʋ�
    /// </summary>
    public class clsControlQueryUnDosageRecipe : com.digitalwave.GUI_Base.clsController_Base
    {   
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsControlQueryUnDosageRecipe()
        {
            m_objDomain = new clsDomainControlOPMedStore();
        }
        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <param name="m_strMedStoreid"></param>
        public void m_mthInitialGUI(string m_strMedStoreid)
        {
            long lngRes = -1;
            DataTable dt = new DataTable();
            lngRes = m_objDomain.m_lngGetMedStoreInfo(m_strMedStoreid, out dt);
            if(lngRes>0)
            {
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("�����ҩ��ID���󣬲����ڸ�ҩ��ID��");
                    return;
                }
                else
                {
                    m_strMedStoreName = dt.Rows[0]["medstorename_vchr"].ToString();
                    this.m_objViewer.Text += "{"+dt.Rows[0]["medstorename_vchr"].ToString()+"}";
                }
            }
        }
        private string m_strMedStoreName;
        #region ���ô������
        frmQueryUnDosageRecipe m_objViewer;
        clsDomainControlOPMedStore m_objDomain;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmQueryUnDosageRecipe)frmMDI_Child_Base_in;
        }
        #endregion
        /// <summary>
        /// ��ȡ��ҩ��Ϣ
        /// </summary>
        public void m_mthGetUnDosageRecipeInfo()
        {
            DataTable m_dtTemp = new DataTable();
            DataTable  m_dtSendMed = new DataTable();
            long lngRes = -1;
            string m_strBeginDate=this.m_objViewer.m_dtmBeginDate.Value.ToString("yyyy-MM-dd");
            string m_strEndDate = this.m_objViewer.m_dtmEndDate.Value.ToString("yyyy-MM-dd");
            lngRes = m_objDomain.m_lngGetQueryUnDosageRecipeInfo(this.m_objViewer.m_strMedStoreid, m_strBeginDate, m_strEndDate, out m_dtTemp);
            if (lngRes > 0 && m_dtTemp != null)
            {
                m_dtSendMed = m_dtTemp.Clone();
                DataRow[] drArr = m_dtTemp.Select("pstauts_int<>-2");
                m_dtSendMed.BeginLoadData();
                for (int i = 0; i < drArr.Length; i++)
                {
                    m_dtSendMed.LoadDataRow(drArr[i].ItemArray, true);
                }
                m_dtSendMed.EndLoadData();
                this.m_objViewer.m_dgvSendMed.DataSource = m_dtSendMed;
                if (this.m_objViewer.m_dgvSendMed.Rows.Count == 0)
                    this.m_objViewer.m_dgvDetail.DataSource = null;
             
            }
        }
        /// <summary>
        ///  ��δ����ϸ��Ϣ
        /// </summary>
        public void m_mthBindDetailData()
        {
            DataTable m_dtRecipeDetail = new DataTable();
            long lngRes = -1;

            string m_strOutPatientRecipeid = this.m_objViewer.m_dgvSendMed.Rows[this.m_objViewer.m_dgvSendMed.CurrentCell.RowIndex].Cells["m_txtOutpatientRecipeNo"].Value.ToString();
            lngRes = m_objDomain.m_lngGetUnDosageRecipeDetailByid(m_strOutPatientRecipeid, this.m_objViewer.m_strMedStoreid, out m_dtRecipeDetail);
            if (lngRes > 0)
            {
                this.m_objViewer.m_dgvDetail.DataSource = m_dtRecipeDetail;
                this.m_objViewer.m_dgvDetail.Refresh();
            }


        }
    

    }
}
