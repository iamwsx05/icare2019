using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms;
using com.digitalwave.iCare.BIHOrder.Control;

namespace com.digitalwave.iCare.BIHOrder
{
    /// <summary>
    ///  
    /// </summary>
    class clsCtl_ConfirmOrderBack : com.digitalwave.GUI_Base.clsController_Base
    {

        #region ����
        clsDcl_ConfirmOrderBack m_objManage = null;
        public string m_strReportID;
        public string m_strOperatorID;
        #endregion
        #region ���캯��
        public clsCtl_ConfirmOrderBack()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            m_objManage = new clsDcl_ConfirmOrderBack();

        }
        #endregion

        #region ���ô������
        com.digitalwave.iCare.BIHOrder.frmConfirmOrderBack m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmConfirmOrderBack)frmMDI_Child_Base_in;
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public void LoadData()
        {
            #region ��ɫ
            //����
            m_objViewer.m_lblLong.BackColor = clsOrderColor.BackColorLongOrder;
            //��ʱ
            m_objViewer.m_lblTemp.BackColor = clsOrderColor.BackColorTemOrder;
            //�½�
            m_objViewer.m_lblStatus0.BackColor = clsOrderColor.ForeColorOrderStatus0;
            //���ύ
            m_objViewer.m_lblStatus1.BackColor = clsOrderColor.ForeColorOrderStatus1;
            //ִ��
            m_objViewer.m_lblStatus2.BackColor = clsOrderColor.ForeColorOrderStatus2;
            //ֹͣ
            m_objViewer.m_lblStatus3.BackColor = clsOrderColor.ForeColorOrderStatus3;
            //����
            m_objViewer.m_lblStatus4.BackColor = clsOrderColor.ForeColorOrderStatus4;
            //����
            m_objViewer.m_lblStatus5.BackColor = clsOrderColor.ForeColorOrderStatus_1;

            m_objViewer.m_strOperateName = "[�˻�]";
            //��ҽ������
            m_objViewer.m_txbOrderName.Text = m_objViewer.m_strOrderName.ToString().Trim();
            //��������
            m_objViewer.m_txbPatientName.Text = m_objViewer.m_strPatientName.ToString().Trim();

            m_objViewer.m_lblPrompt.Text = m_objViewer.m_lblPrompt.Text.Replace("[]", m_objViewer.m_strOperateName);
            #endregion
            if (m_objViewer.m_strOrderID == string.Empty) return;

            //��ȡ���ҽ��
            clsBIHOrder[] objResultArr;
            long lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderByParentID(m_objViewer.m_strOrderID, out objResultArr);

            if (lngRes <= 0 || objResultArr == null || objResultArr.Length <= 0) return;

            //��ֵListView
            #region ��ֵ
            ListViewItem lviTemp = null;
            System.Drawing.Color clrBack, clrFore;
            for (int i1 = 0; i1 < objResultArr.Length; i1++)
            {
                if (objResultArr[i1].m_intStatus == 1 || objResultArr[i1].m_intStatus == 5)
                {


                    //���
                    lviTemp = new ListViewItem((i1 + 1).ToString());
                    //����
                    lviTemp.SubItems.Add(objResultArr[i1].m_intRecipenNo.ToString());
                    //��/��	
                    if (objResultArr[i1].m_intExecuteType == 1)
                    {
                        lviTemp.SubItems.Add("��");
                    }
                    else
                    {
                        if (objResultArr[i1].m_intExecuteType == 2)
                            lviTemp.SubItems.Add("��");
                        else
                            lviTemp.SubItems.Add("");
                    }
                    //����
                    lviTemp.SubItems.Add(objResultArr[i1].m_strName);
                    //�� ��
                    lviTemp.SubItems.Add(objResultArr[i1].m_dmlDosageRate.ToString() + objResultArr[i1].m_strDosageUnit);
                    //�� ��  
                    lviTemp.SubItems.Add(objResultArr[i1].m_dmlGet.ToString() + objResultArr[i1].m_strGetunit);
                    //ִ��Ƶ��	  
                    lviTemp.SubItems.Add(objResultArr[i1].m_strExecFreqName);
                    //�� ��	
                    lviTemp.SubItems.Add(objResultArr[i1].m_strDosetypeName);
                    //Ƥ		
                    if (objResultArr[i1].m_intISNEEDFEEL == 1)
                        lviTemp.SubItems.Add("��");
                    else
                        lviTemp.SubItems.Add("");//��
                    //����ҽ��
                    lviTemp.SubItems.Add(objResultArr[i1].m_strParentName);

                    lviTemp.Tag = objResultArr[i1];
                    m_objViewer.m_lsvDisplayOrder.Items.Add(lviTemp);
                    clsOrderStatus.s_mthGetColorByStatus(objResultArr[i1].m_intExecuteType, objResultArr[i1].m_intStatus, out clrBack, out clrFore);
                    m_objViewer.m_lsvDisplayOrder.Items[m_objViewer.m_lsvDisplayOrder.Items.Count - 1].ForeColor = clrFore;
                    m_objViewer.m_lsvDisplayOrder.Items[m_objViewer.m_lsvDisplayOrder.Items.Count - 1].BackColor = clrBack;
                }

            }

            #endregion
        }
        #endregion
    }
}
