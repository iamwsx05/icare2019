using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms;

namespace com.digitalwave.iCare.BIHOrder
{
    /// <summary>
    /// ת�����ӵ��ݱ༭	�߼����Ʋ�
    /// ���ߣ� ����
    /// ����ʱ�䣺 2005-01-18
    /// </summary>
    public class clsCtl_AOTransfer : com.digitalwave.GUI_Base.clsController_Base
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
        public clsCtl_AOTransfer()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            m_objManage = new clsDcl_ExecuteOrder();
            m_strReportID = null;
        }
        #endregion
        #region ���ô������
        com.digitalwave.iCare.BIHOrder.frmAOTransfer m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmAOTransfer)frmMDI_Child_Base_in;

        }
        #endregion
        #region ��ʼ��
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void m_Initialization()
        {
            switch (m_objViewer.m_intEditState)
            {
                case 0://����
                    m_objViewer.Text = "����ת��ҽ�����ӵ���";
                    break;
                case 1://�༭
                    m_objViewer.Text = "�༭ת��ҽ�����ӵ���";
                    break;
                case 2://ֻ��
                    m_objViewer.Text = "�鿴ת��ҽ�����ӵ���";
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
            m_objViewer.m_txtTARGETAREAID_CHR.Enabled = false;
            m_objViewer.m_txtDES_VCHR.Enabled = false;
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
                m_objViewer.m_txtSOURCEAREAID_CHR.Text = dtbResult.Rows[0]["AreaName"].ToString().Trim();
                m_objViewer.m_txtSOURCEAREAID_CHR.Tag = dtbResult.Rows[0]["AREAID_CHR"].ToString().Trim();
                m_objViewer.m_txtSOURCEBEDID_CHR.Text = dtbResult.Rows[0]["BedCode"].ToString().Trim();
                m_objViewer.m_txtSOURCEBEDID_CHR.Tag = dtbResult.Rows[0]["BEDID_CHR"].ToString().Trim();
            }

            //���븽�ӵ�����Ϣ
            string strAttachID = m_objViewer.m_strAttachID.Trim();
            if (strAttachID == "") return;
            clsT_Opr_Bih_OrderAttach_Transfer_Vo objResult = null;
            lngRes = m_objManage.m_lngGetOrderAttachTransferByID(strAttachID, out objResult);
            if (lngRes > 0 && objResult != null)
            {
                m_strATTACHID_CHR = objResult.m_strTRANSFERID_CHR;
                m_strREGISTERID_CHR = objResult.m_strREGISTERID_CHR;
                m_objViewer.m_txtSOURCEAREAID_CHR.Text = objResult.m_strSourceAreaName;
                m_objViewer.m_txtSOURCEAREAID_CHR.Tag = objResult.m_strSOURCEAREAID_CHR;
                m_objViewer.m_txtSOURCEBEDID_CHR.Text = objResult.m_strSourceBedNo;
                m_objViewer.m_txtSOURCEBEDID_CHR.Tag = objResult.m_strSOURCEBEDID_CHR;
                m_objViewer.m_txtTARGETAREAID_CHR.Text = objResult.m_strTargetAreaName;
                m_objViewer.m_txtTARGETAREAID_CHR.Tag = objResult.m_strTARGETAREAID_CHR;
                m_objViewer.m_txtSTATUS_INT.Text = objResult.m_strStatusName;
                m_objViewer.m_txtSTATUS_INT.Tag = objResult.m_intSTATUS_INT;
                m_objViewer.m_txtCREATEDATE_DAT.Text = objResult.m_strCREATEDATE_DAT;
                m_objViewer.m_chkISACTIVE_INT.Checked = (objResult.m_intISACTIVE_CHR == 1) ? true : false;
                m_objViewer.m_txtACTIVEEMPID_CHR.Text = objResult.m_strActiveEmpName;
                m_objViewer.m_txtACTIVEEMPID_CHR.Tag = objResult.m_strACTIVEEMPID_CHR;
                m_objViewer.m_txtACTIVEDATE_DAT.Text = objResult.m_strACTIVEDATE_DAT;
                m_objViewer.m_txtDES_VCHR.Text = objResult.m_strDES_VCHR;
                if (objResult.m_intSTATUS_INT == 1 && objResult.m_intISACTIVE_CHR != 1)
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
            clsT_Opr_Bih_OrderAttach_Transfer_Vo objItem = null;
            SetVo(out objItem);
            if (m_objViewer.m_intEditState == 0)//����
            {
                string strRecordID = "";
                lngRes = m_objManage.m_lngAddNewOrderAttachTransfer(out strRecordID, objItem);
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
                lngRes = m_objManage.m_lngModifyOrderAttachTransfer(objItem);
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
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objTem =new com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService();
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngDeleteAttachOrder(m_objViewer.m_strAttachID);
            if (lngRes > 0)
            {
                lngRes = m_objManage.m_lngDeleteOrderAttachTransfer(m_objViewer.m_strAttachID);
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
        /// 
        public void m_BecomeEffective()
        {
            if (m_objViewer.m_strAttachID.Trim() == "") return;
            if (!MayBecomeEffective()) return;
            clsT_Opr_Bih_Transfer_VO objItem;
            SetVoForTransfer(out objItem);
            long lngRes = 0;
            lngRes = m_objManage.m_lngBecomeEffectiveOrderAttachTransfer(m_objViewer.m_strAttachID, m_strOperatorID, objItem);
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
            if (m_objViewer.m_txtTARGETAREAID_CHR.Tag.ToString().Trim() == "")
            {
                MessageBox.Show(m_objViewer, "Ŀ�겡�������٣�", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        /// <summary>
        /// ��丽�ӵ���Vo����
        /// </summary>
        /// <param name="objItem"></param>
        private void SetVo(out clsT_Opr_Bih_OrderAttach_Transfer_Vo objItem)
        {
            objItem = new clsT_Opr_Bih_OrderAttach_Transfer_Vo();
            objItem.m_strTRANSFERID_CHR = m_strATTACHID_CHR;
            objItem.m_strREGISTERID_CHR = m_strREGISTERID_CHR;
            objItem.m_strSOURCEAREAID_CHR = m_objViewer.m_txtSOURCEAREAID_CHR.Tag.ToString();
            objItem.m_strSOURCEBEDID_CHR = m_objViewer.m_txtSOURCEBEDID_CHR.Tag.ToString();
            objItem.m_strTARGETAREAID_CHR = m_objViewer.m_txtTARGETAREAID_CHR.Tag.ToString();
            objItem.m_strCREATEDATE_DAT = m_objViewer.m_txtCREATEDATE_DAT.Text.Trim();
            objItem.m_strDES_VCHR = m_objViewer.m_txtDES_VCHR.Text.Trim();
            //objItem.m_intSTATUS_INT = Int32.Parse(m_objViewer.m_txtSTATUS_INT.Tag.ToString());
            objItem.m_intSTATUS_INT = 0;//������״̬Ĭ��Ϊ0������ֻ��0״̬�����޸�
            objItem.m_intISACTIVE_CHR = (m_objViewer.m_chkISACTIVE_INT.Checked) ? 1 : 0;
            //objItem.m_strACTIVEEMPID_CHR = m_objViewer.m_txtACTIVEEMPID_CHR.Tag.ToString();
            //objItem.m_strACTIVEDATE_DAT = Convert.ToDateTime(m_objViewer.m_txtACTIVEDATE_DAT.Text).ToString("yyyy-MM-dd HH:mm:ss").Trim();
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
                if (Int32.Parse(m_objViewer.m_txtSTATUS_INT.Tag.ToString()) > 0)
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
                IntState = Int32.Parse(m_objViewer.m_txtSTATUS_INT.Tag.ToString());
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
        /// ����ת����Vo
        /// </summary>
        /// <param name="objItem"></param>
        /// <returns></returns>
        private void SetVoForTransfer(out clsT_Opr_Bih_Transfer_VO objItem)
        {
            objItem = new clsT_Opr_Bih_Transfer_VO();
            //Դ����id
            objItem.m_strSOURCEDEPTID_CHR = m_objViewer.m_txtSOURCEAREAID_CHR.Tag.ToString();
            //Դ����id
            objItem.m_strSOURCEAREAID_CHR = m_objViewer.m_txtSOURCEAREAID_CHR.Tag.ToString();
            //Դ����id
            objItem.m_strSOURCEBEDID_CHR = m_objViewer.m_txtSOURCEBEDID_CHR.Tag.ToString();
            //Ŀ�����id
            //objItem.m_strTARGETDEPTID_CHR =m_objViewer.m_txtTARGETAREAID_CHR.Tag.ToString();
            //Ŀ�겡��id
            objItem.m_strTARGETAREAID_CHR = m_objViewer.m_txtTARGETAREAID_CHR.Tag.ToString();
            //Ŀ�겡��id
            objItem.m_strTARGETBEDID_CHR = "";
            //��������{1=ת��2=����3=ת��+����4=��Ժ����}
            objItem.m_intTYPE_INT = 3;
            //��ע
            objItem.m_strDES_VCHR = m_objViewer.m_txtDES_VCHR.Text;
            //������
            objItem.m_strOPERATORID_CHR = m_strOperatorID;
            //��Ժ�Ǽ���ˮ��(200409010001)
            objItem.m_strREGISTERID_CHR = m_strREGISTERID_CHR;
            //�޸����ڣ���������
            objItem.m_strMODIFY_DAT = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        #endregion
        #endregion
    }
}
