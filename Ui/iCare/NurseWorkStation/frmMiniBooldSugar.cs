using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
namespace iCare
{
    /// <summary>
    /// ΢��Ѫ�Ǽ������¼����
    /// </summary>
    public partial class frmMiniBooldSugar : iCare.frmRecordsBase
    {
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public frmMiniBooldSugar()
        {
            InitializeComponent();
            //datagridview�п����
            dgvRecords.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; ;
        } 
        #endregion

        #region �ֶ�
        #endregion

        #region ����
        #endregion

        #region ����
        /// <summary>
        /// ��дtreeview�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void m_trvInPatientDate_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //clsMiniBloodSugarChk_GXServ objservice =
            //    (clsMiniBloodSugarChk_GXServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsMiniBloodSugarChk_GXServ));


            try
            {
                //��ղ��˼�¼��Ϣ				
                m_mthClearPatientRecordInfo();

                if (m_trvInPatientDate.SelectedNode == null || m_trvInPatientDate.SelectedNode == m_trvInPatientDate.Nodes[0] || m_objCurrentPatient == null)
                {
                    return;
                }

                m_objCurrentPatient.m_DtmSelectedInDate = DateTime.Parse(m_trvInPatientDate.SelectedNode.Text);

                //��ȡ���˼�¼�б�
                clsTransDataInfo[] objTansDataInfoArr;
                string m_strInPatientID = m_objCurrentPatient.m_StrInPatientID;
                string m_strInPatientDate = m_trvInPatientDate.SelectedNode.Text; //m_objCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate.ToString("yyyy-MM-dd HH:mm:ss");
                //��ȡָ����Ժʱ��ļ�¼�б�
                DataTable dt = new DataTable();
                long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetRecoedByInPatient(  m_strInPatientID, DateTime.Parse(m_strInPatientDate), out dt);
                if (dt!=null)
                {
                    ��dgvRecords.DataSource = dt;   
                }
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
        
  
        /// <summary>
        /// �����Ҽ���������޸ļ�¼���������¼�����
        /// </summary>
        /// <param name="p_intRecordType"></param>
        /// <returns></returns>
        protected override frmDiseaseTrackBase m_frmGetRecordForm(int p_intRecordType)
        {
            switch ((enmDiseaseTrackType)p_intRecordType)
            {
                case enmDiseaseTrackType.GeneralNurseRecord_GXCon:
                    return new frmMiniBooldSugarContent(true);
            }

            return null;
        }
       
        #endregion

        #region �¼�
        /// <summary>
        /// �Ҽ���Ӽ�¼�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmAdd_Click(object sender, EventArgs e)
        {
            m_mthAddNewRecord((int)enmDiseaseTrackType.GeneralNurseRecord_GXCon);
        }
        /// <summary>
        /// �Ҽ��޸ļ�¼�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmModify_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// �Ҽ�ɾ����¼�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDelete_Click(object sender, EventArgs e)
        {

        } 
        #endregion
    }
}