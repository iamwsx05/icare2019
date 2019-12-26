using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms;

namespace com.digitalwave.iCare.BIHOrder
{
    /// <summary>
    /// ��Ժ���ӵ��ݱ༭	�߼����Ʋ�
    /// ���ߣ� ����
    /// ����ʱ�䣺 2005-01-17
    /// </summary>
    public class clsCtl_AOOutHospital : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ����
        clsDcl_ExecuteOrder m_objManage = null;
        public string m_strReportID;
        public string m_strOperatorID;
        /// <summary>
        /// ���ӵ�����ˮ��
        /// </summary>
        public string m_strATTACHID_CHR = "";
        /// <summary>
        /// ��Ժ�Ǽ�ID
        /// </summary>
        public string m_strREGISTERID_CHR = "";
        #endregion
        #region ���캯��
        public clsCtl_AOOutHospital()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            m_objManage = new clsDcl_ExecuteOrder();
            m_strReportID = null;
        }
        #endregion
        #region ���ô������
        com.digitalwave.iCare.BIHOrder.frmAOOutHospital m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmAOOutHospital)frmMDI_Child_Base_in;

        }
        #endregion
        #region ��ʼ��
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void m_Initialization()
        {
            m_objViewer.m_cboPSTATUS_INT.SelectedIndex = 0;//����Ϊ��Ԥ��Ժ��
            m_objViewer.m_cboPSTATUS_INT.Enabled = false;
            switch (m_objViewer.m_intEditState)
            {
                case 0://����
                    m_objViewer.Text = "������Ժҽ�����ӵ���";
                    break;
                case 1://�༭
                    m_objViewer.Text = "�༭��Ժҽ�����ӵ���";
                    break;
                case 2://ֻ��
                    m_objViewer.Text = "�鿴��Ժҽ�����ӵ���";
                    m_SetReadOnly();
                    break;
                default://ֻ��
                    m_SetReadOnly();
                    break;
            }
        }
        #endregion

        #region ��Ϊֻ��
        /// <summary>
        /// ����ֻ��
        /// </summary>
        public void m_SetReadOnly()
        {
            m_objViewer.m_cboTYPE_INT.Enabled = false;
            m_objViewer.m_cboPSTATUS_INT.Enabled = false;
            m_objViewer.m_txtDESC_VCHR.Enabled = false;
            m_objViewer.cmdOK.Enabled = false;
            m_objViewer.cmdDel.Enabled = false;
        }
        #endregion

        #region ����
        /// <summary>
        /// ���벡�ˡ����ӵ�����Ϣ
        /// </summary>
        public void m_LoadData()
        {
            long lngRes = 0;

            //���벡����Ϣ	
            if (m_objViewer.m_strPatientID.Trim() == "") return;
            DataTable dtbResult = new DataTable();
            lngRes = m_objManage.lngGetOrderPatientBIHInfo(m_objViewer.m_strOrderID, out dtbResult);
            if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
            {
                m_objViewer.m_lblPATIENTNAME_CHR.Text = dtbResult.Rows[0]["NAME_VCHR"].ToString().Trim();
                m_objViewer.m_lblSEX_CHR.Text = dtbResult.Rows[0]["SEX_CHR"].ToString().Trim();
                m_objViewer.m_lblINPATIENTID_CHR.Text = dtbResult.Rows[0]["INPATIENTID_CHR"].ToString().Trim();
                m_objViewer.m_lblIDCARD_CHR.Text = dtbResult.Rows[0]["IDCARD_CHR"].ToString().Trim();
                //��Ժ�Ǽ���ˮ��
                m_strREGISTERID_CHR = dtbResult.Rows[0]["REGISTERID_CHR"].ToString().Trim();
                //����������
                m_objViewer.m_lblOUTAREAID_CHR.Text = dtbResult.Rows[0]["AreaName"].ToString().Trim();
                m_objViewer.m_lblOUTAREAID_CHR.Tag = dtbResult.Rows[0]["AREAID_CHR"].ToString().Trim();
                m_objViewer.m_lblOUTBEDID_CHR.Text = dtbResult.Rows[0]["BedCode"].ToString().Trim();
                m_objViewer.m_lblOUTBEDID_CHR.Tag = dtbResult.Rows[0]["BEDID_CHR"].ToString().Trim();
            }

            //���븽�ӵ�����Ϣ
            string strAttachID = m_objViewer.m_strAttachID.Trim();
            if (strAttachID == "") return;
            clsT_Opr_Bih_OrderAttach_Leave_Vo objResult = null;
            lngRes = m_objManage.m_lngGetOrderAttachLeaveByID(strAttachID, out objResult);
            if (lngRes > 0 && objResult != null)
            {
                m_strATTACHID_CHR = objResult.m_strLEAVEID_CHR;
                m_strREGISTERID_CHR = objResult.m_strREGISTERID_CHR;
                m_objViewer.m_cboTYPE_INT.SelectedIndex = objResult.m_intTYPE_INT;
                m_objViewer.m_lblOUTAREAID_CHR.Text = objResult.m_strOutAreaName;
                m_objViewer.m_lblOUTAREAID_CHR.Tag = objResult.m_strOUTAREAID_CHR;
                m_objViewer.m_lblOUTBEDID_CHR.Text = objResult.m_strOutBedNo;
                m_objViewer.m_lblOUTBEDID_CHR.Tag = objResult.m_strOUTBEDID_CHR;
                m_objViewer.m_cboPSTATUS_INT.SelectedIndex = objResult.m_intPSTATUS_INT;
                m_objViewer.m_lblSTATUS_INT.Text = objResult.m_strStatusName;
                m_objViewer.m_lblSTATUS_INT.Tag = objResult.m_intSTATUS_INT;
                m_objViewer.m_chkISACTIVE_INT.Checked = (objResult.m_intISACTIVE_INT == 1) ? true : false;
                m_objViewer.m_lblACTIVEEMPID_CHR.Text = objResult.m_strActiveEmpName;
                m_objViewer.m_lblACTIVEEMPID_CHR.Tag = objResult.m_strACTIVEEMPID_CHR;
                m_objViewer.m_lblACTIVEDATE_DAT.Text = objResult.m_strACTIVEDATE_DAT;
                m_objViewer.m_txtDESC_VCHR.Text = objResult.m_strDES_VCHR;
                if (objResult.m_intSTATUS_INT == 1 && objResult.m_intISACTIVE_INT != 1)
                {
                    m_objViewer.cmdBecomeEffective.Enabled = true;
                }
            }
        }
        #endregion

        #region �¼�
        /// <summary>
        /// ��|���¼�
        /// </summary>
        public void m_OK()
        {
            long lngRes = 0;
            if (!CheckInput()) return;
            clsT_Opr_Bih_OrderAttach_Leave_Vo objItem = null;
            SetVo(out objItem);
            if (m_objViewer.m_intEditState == 0)//����
            {
                string strRecordID = "";
                lngRes = m_objManage.m_lngAddNewOrderAttachLeave(out strRecordID, objItem);
                if (lngRes > 0)
                {
                    //���Ӹ��ӵ���Ӱ��--���
                    m_objViewer.m_strAttachID = strRecordID;
                    //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objTem = new com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService();
                    lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngAddAttachOrder(m_objViewer.m_strOrderID, strRecordID);
                }
            }
            else if (m_objViewer.m_intEditState == 1)//�༭
            {
                lngRes = m_objManage.m_lngModifyOrderAttachLeave(objItem);
            }

            //����������
            if (lngRes > 0)
                MessageBox.Show(m_objViewer, "�����ɹ���", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(m_objViewer, "����ʧ�ܣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            m_objViewer.Close();
        }
        /// <summary>
        /// ɾ���¼�	
        /// </summary>
        public void m_Del()
        {
            if (m_objViewer.m_strAttachID.Trim() == "") return;
            //�Ƿ����ɾ��
            if (!MayDelete()) return;

            long lngRes = 0;
            //ɾ�����ӵ���Ӱ��--��ɾ
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objTem = new com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService();
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngDeleteAttachOrder(m_objViewer.m_strAttachID);
            if (lngRes > 0)
            {
                lngRes = m_objManage.m_lngDeleteOrderAttachLeave(m_objViewer.m_strAttachID);
            }
            //����������
            if (lngRes > 0)
                MessageBox.Show(m_objViewer, "ɾ���ɹ���", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(m_objViewer, "ɾ��ʧ�ܣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            m_objViewer.Close();
        }
        /// <summary>
        /// �ύ�¼�
        /// </summary>
        public void m_BecomeEffective()
        {
            if (m_objViewer.m_strAttachID.Trim() == "") return;
            if (!MayBecomeEffective()) return;
            clsT_Opr_Bih_Leave_VO objItem;
            SetVoForLeave(out objItem);
            long lngRes = 0;
            lngRes = m_objManage.m_lngBecomeEffectiveOrderAttachLeave(m_objViewer.m_strAttachID, m_strOperatorID, objItem);
            //����������
            if (lngRes > 0)
                MessageBox.Show(m_objViewer, "��Ч�ɹ���", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(m_objViewer, "��Чʧ�ܣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            m_objViewer.Close();
        }
        #region ˽�з���
        /// <summary>
        /// ��֤����
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            if (m_strREGISTERID_CHR.Trim() == "")
            {
                MessageBox.Show(m_objViewer, "����סԺ��Ϣ������ȷȷ����", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (m_objViewer.m_cboTYPE_INT.SelectedIndex <= 0)
            {
                MessageBox.Show(m_objViewer, "��Ժԭ�����٣�", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                m_objViewer.m_cboTYPE_INT.Focus();
                return false;
            }
            if (m_objViewer.m_lblOUTAREAID_CHR.Tag.ToString().Trim() == "")
            {
                MessageBox.Show(m_objViewer, "��Ժ���������٣�", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (m_objViewer.m_lblOUTBEDID_CHR.Tag.ToString().Trim() == "")
            {
                MessageBox.Show(m_objViewer, "��Ժ���������٣�", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (m_objViewer.m_cboPSTATUS_INT.SelectedIndex < 0)
            {
                MessageBox.Show(m_objViewer, "��Ժ���Ͳ����٣�", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                m_objViewer.m_cboPSTATUS_INT.Focus();
                return false;
            }
            return true;
        }
        /// <summary>
        /// ��丽�ӵ���Vo����
        /// </summary>
        /// <param name="objItem"></param>
        private void SetVo(out clsT_Opr_Bih_OrderAttach_Leave_Vo objItem)
        {
            objItem = new clsT_Opr_Bih_OrderAttach_Leave_Vo();
            objItem.m_strLEAVEID_CHR = m_strATTACHID_CHR;
            objItem.m_strREGISTERID_CHR = m_strREGISTERID_CHR;
            objItem.m_intTYPE_INT = m_objViewer.m_cboTYPE_INT.SelectedIndex;
            objItem.m_strOUTAREAID_CHR = m_objViewer.m_lblOUTAREAID_CHR.Tag.ToString();
            objItem.m_strOUTBEDID_CHR = m_objViewer.m_lblOUTBEDID_CHR.Tag.ToString();
            objItem.m_strDES_VCHR = m_objViewer.m_txtDESC_VCHR.Text.Trim();
            objItem.m_intPSTATUS_INT = m_objViewer.m_cboPSTATUS_INT.SelectedIndex;
            //objItem.m_intSTATUS_INT = Int32.Parse(m_objViewer.m_lblSTATUS_INT.Tag.ToString());
            objItem.m_intSTATUS_INT = 0;//������״̬Ĭ��Ϊ0������ֻ��0״̬�����޸�
            objItem.m_intISACTIVE_INT = (m_objViewer.m_chkISACTIVE_INT.Checked) ? 1 : 0;
            //objItem.m_strACTIVEEMPID_CHR = m_objViewer.m_lblACTIVEEMPID_CHR.Tag.ToString();
            //objItem.m_strACTIVEDATE_DAT = Convert.ToDateTime(m_objViewer.m_lblACTIVEDATE_DAT.Text).ToString("yyyy-MM-dd HH:mm:ss").Trim();
            //������Ĭ��ΪNULL������ֻ��NUll�����޸�
            objItem.m_strACTIVEEMPID_CHR = null;
            objItem.m_strACTIVEDATE_DAT = null;
        }
        /// <summary>
        /// ��ȡ�Ƿ����ɾ��
        /// </summary>
        /// <returns></returns>
        private bool MayDelete()
        {
            if (m_objViewer.m_intEditState == 2) return false;
            try
            {
                //״̬��־{0=δ����;1=�ѷ���;2=�Ѿ��н����;}
                if (Int32.Parse(m_objViewer.m_lblSTATUS_INT.Tag.ToString()) > 0)
                {
                    MessageBox.Show(m_objViewer, "ֻ��ɾ��δ����״̬�ĸ��ӵ��ݣ�", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch { }
            return true;
        }
        /// <summary>
        /// �Ƿ��������Ч����
        /// </summary>
        /// <returns></returns>
        private bool MayBecomeEffective()
        {
            int IntState = -1;//״̬��־	{0=δ���ͣ�1=�ѷ��ͣ�2=���н���ˣ�}
            try
            {
                IntState = Int32.Parse(m_objViewer.m_lblSTATUS_INT.Tag.ToString());
            }
            catch { }
            if (IntState != 1)
            {
                MessageBox.Show(m_objViewer, "ֻ��״̬Ϊ���ѷ��͡���������Ч������", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        /// <summary>
        /// ���ó�ԺVo
        /// </summary>
        /// <param name="objItem"></param>
        /// <returns></returns>
        private void SetVoForLeave(out clsT_Opr_Bih_Leave_VO objItem)
        {
            objItem = new clsT_Opr_Bih_Leave_VO();
            //��Ժ�Ǽ���ˮ��(200409010001)
            objItem.m_strREGISTERID_CHR = m_strREGISTERID_CHR;
            //����{1=������Ժ2=תԺ3=����4=����}
            objItem.m_strTYPE_INT = m_objViewer.m_cboTYPE_INT.SelectedIndex.ToString();
            //��Ժ����
            objItem.m_strOUTDEPTID_CHR = m_objViewer.m_lblOUTAREAID_CHR.Tag.ToString();
            //��Ժ����
            objItem.m_strOUTAREAID_CHR = m_objViewer.m_lblOUTAREAID_CHR.Tag.ToString();
            //��Ժ����
            objItem.m_strOUTBEDID_CHR = m_objViewer.m_lblOUTBEDID_CHR.Tag.ToString();
            //��ע
            objItem.m_strDES_VCHR = m_objViewer.m_txtDESC_VCHR.Text;
            //������
            objItem.m_strOPERATORID_CHR = m_strOperatorID;
            //�޸����ڣ���������
            objItem.m_strMODIFY_DAT = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //״̬����1��ʷ��0-��Ч��1��Ч��
            objItem.m_intSTATUS_INT = 1;
            //��Ժ��ʽ	{0=Ԥ��Ժ;1=ʵ�ʳ�Ժ;}
            objItem.m_intPSTATUS_INT = m_objViewer.m_cboPSTATUS_INT.SelectedIndex;
        }
        #endregion
        #endregion
    }
}
