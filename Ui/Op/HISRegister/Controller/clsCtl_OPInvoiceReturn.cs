using System;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HI;
using System.Data;
using System.Collections.Generic;
using com.digitalwave.iCare.middletier.HIS;

namespace com.digitalwave.iCare.gui.HIS
{
    public class clsCtl_OPInvoiceReturn : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ����
        clsDcl_InvoiceManage m_objManage = null;
        public string m_strReportID;
        public string m_strOperatorID;
        private string m_strOperateLevel = string.Empty;
        /// <summary>
        /// ��Ʊ������ID
        /// </summary>
        private string m_strCreatInvoEmpID = string.Empty;
        /// <summary>
        /// ��ǰ��Ʊ��
        /// </summary>
        string currInvoNo = string.Empty;

        #endregion

        #region ���캯��
        public clsCtl_OPInvoiceReturn()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            m_objManage = new clsDcl_InvoiceManage();
            m_strReportID = null;
            m_strOperatorID = "0000001";
            this.m_strOperateLevel = clsMain.m_strGetCollocate("3101");

        }
        #endregion

        #region ���ô������
        com.digitalwave.iCare.gui.HIS.frmOPInvoiceReturn m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmOPInvoiceReturn)frmMDI_Child_Base_in;
        }
        #endregion

        #region ��Ʊ�˻�
        /// <summary>
        /// ��Ʊ�˻�
        /// </summary>
        public void m_ReturnTicket()
        {
            //�����Ʊ��Ϊ���򷵻�
            if (m_objViewer.txtInvoice.Text.Trim() == "")
            {
                MessageBox.Show("�����뷢Ʊ�ţ�", "������ʾ��");
                return;
            }

            if (this.m_objViewer.LoginInfo != null)
            {
                m_strOperatorID = this.m_objViewer.LoginInfo.m_strEmpID;
            }
            //��֤��Ʊ�Ƿ����
            clsT_opr_outpatientrecipeinv_VO objResult = null;
            long lngRet = m_objManage.m_lngGetInfoByNoForReturn(m_objViewer.txtInvoice.Text.Trim(), out objResult);
            if (lngRet <= 0)
            {
                //��Ʊʧ�ܣ�
                MessageBox.Show(m_objViewer, "��Ʊʧ��!", "������ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (objResult == null || objResult.m_intSTATUS_INT != 1)
            {
                //��Ʊ������Ч�ķ�Ʊ����Ʊʧ�ܣ�
                MessageBox.Show(m_objViewer, "�˷�Ʊ������Ч�ķ�Ʊ����Ʊʧ��!", "������ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //��Ʊδ���,������Ʊ
            DataTable dt;
            lngRet = m_objManage.m_mthGetInvoiceAuditingInfo(m_objViewer.txtInvoice.Text.Trim(), out dt, 1);
            if (dt.Rows.Count == 0)
            {
                //��Ʊδ���,������Ʊ
                MessageBox.Show(m_objViewer, "��Ʊδ���,������Ʊ!", "������ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //��Ӷ�ҩƷ��ҩ�ļ��
            string m_strStatus = string.Empty;
            lngRet = m_objManage.m_lngReturnTicketCheckOutSendMed(m_objViewer.txtInvoice.Text.Trim(), out m_strStatus);
            if (lngRet > 0)
            {
                if (m_strStatus.Trim() == "1" || m_strStatus.Trim() == "2")
                {

                }
                else
                {
                    MessageBox.Show(m_objViewer, "��ǰ��Ʊ�Ѿ��䷢ҩ��������ҩ��", "������ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show(m_objViewer, "���ҩƷ��Ϣʧ�ܣ��������Ա��ϵ��", "������ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmInvoiceRefundReason frmR = new frmInvoiceRefundReason(1, this.currInvoNo, m_strOperatorID);
            if (frmR.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string Seqid = "";
            int intFlag = 0;
            lngRet = m_objManage.m_lngReturnTicket(m_objViewer.txtInvoice.Text.Trim(), m_strOperatorID, ref Seqid, intFlag);
            if (lngRet <= 0)
            {
                //��Ʊʧ�ܣ�
                MessageBox.Show(m_objViewer, "��Ʊʧ�ܣ�", "������ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                //����Ƕ��ʽ�籣�˷ѣ��������в�ѯ���籣���ݲ�ȥ���˷ѽӿڵĵ���
                //�������ľ����������� 711014_ZZZD03  --- 711014_ZZZD01 ����ϵͳ�ĵ�¼���ǽ�ȡ����6λ���������˷ѵ�ʱ��Ҫ����Щ��������
                string strEmpNo = this.m_objViewer.LoginInfo.m_strEmpNo;
                if (this.m_objViewer.LoginInfo.m_strEmpNo.Contains("ZZZD"))
                {
                    strEmpNo = "711014_" + strEmpNo;
                }
                clsCtl_YBChargeMZCancel clsYbChargeMZCancel = new clsCtl_YBChargeMZCancel();
                clsYbChargeMZCancel.m_lngCSYBChargeCancel(m_objViewer.txtCardID.Text.Trim(), strEmpNo);
                //��Ʊ�ɹ���
                MessageBox.Show(m_objViewer, "��Ʊ�ɹ���", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (IsPrintInvoice)
                {
                    this.m_objViewer.Cursor = Cursors.WaitCursor;
                    clsCalcPatientCharge objCalPatientCharge = new clsCalcPatientCharge(this.m_objComInfo.m_strGetHospitalTitle());
                    objCalPatientCharge.m_mthReprintinvoice(Seqid, this.m_objViewer.LoginInfo.m_strEmpID, 1);
                    this.m_objViewer.Cursor = Cursors.Default;
                }

            }
            //��շ�Ʊ��
            m_EmptyInput();
        }

        #endregion

        #region ���ݷ�Ʊ����ʾ�����
        /// <summary>
        /// ���ݷ�Ʊ����ʾ�����
        /// </summary>
        public void DisplaySeqNo()
        {
            //			m_objViewer.m_txtSEQID_CHR.Text ="";
            //
            //			//�����Ʊ��Ϊ���򷵻�
            //			if(m_objViewer.m_txtINVOICENO_VCHR.Text.Trim()=="")
            //				return;
            //
            //			clsT_opr_outpatientrecipeinv_VO objResult = null;
            //			long lngRet = m_objManage.m_lngGetInfoByNoForReturn(m_objViewer.m_txtINVOICENO_VCHR.Text.Trim(),out objResult);
            //			if(lngRet>0 && objResult.m_strSEQID_CHR!=null)
            //			{
            //				m_objViewer.m_txtSEQID_CHR.Text = objResult.m_strSEQID_CHR;
            //			}
        }
        #endregion

        #region �����������ʾ��Ʊ��
        public void DisplayInvoiceNo()
        {
            //			m_objViewer.m_txtINVOICENO_VCHR.Text="";
            //
            //			//��������Ϊ���򷵻�
            //			if(m_objViewer.m_txtSEQID_CHR.Text.Trim()=="")
            //				return;
            //
            //			clsT_opr_outpatientrecipeinv_VO objResult = null;
            //			long lngRet = m_objManage.m_lngGetInfoBySeqidForReturn(m_objViewer.m_txtSEQID_CHR.Text.Trim(),out objResult);
            //			if(lngRet>0 && objResult.m_strINVOICENO_VCHR!=null)
            //			{
            //				m_objViewer.m_txtINVOICENO_VCHR.Text = objResult.m_strINVOICENO_VCHR;
            //			}
        }
        #endregion

        #region ��ʾ��Ʊ��Ϣ
        public void m_DisplayInvoiceInfo()
        {
            //û�з�Ʊ���򣬲���ʾ��Ʊ��������
            if (m_objViewer.txtInvoice.Text.ToString().Trim() == "")
            {
                MessageBox.Show("��ѡ��Ʊ��", "ϵͳ��ʾ");
                return;
            }

            //��Ʊ������Ʊ
            if (m_objManage.m_blnChecksplit(m_objViewer.txtInvoice.Text.Trim()))
            {
                string invoinfo = "";
                DataTable dt1 = new DataTable();
                long l = m_objManage.m_lngGetsplitinvoinfo(m_objViewer.txtInvoice.Text.Trim(), out dt1);
                if (dt1.Rows.Count > 1)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        invoinfo += "\r\n\r\n��Ʊ" + (i + 1).ToString() + "�����ǣ�" + dt1.Rows[i]["INVOICENO_VCHR"].ToString();
                    }

                    if (MessageBox.Show("���ŷ�ƱΪ�ַ�Ʊ��ӡ, ��Ʊ����" + dt1.Rows.Count.ToString() + "��, ��ϸ����: " + invoinfo + "\r\n\r\n�Ƿ����������Ʊ��", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        this.m_objViewer.cmdReturn.Enabled = false;
                        this.m_objViewer.buttonXP1.Enabled = false;
                        return;
                    }
                }
            }

            //��Ʊδ���,������Ʊ
            DataTable dt;
            m_objManage.m_mthGetInvoiceAuditingInfo(m_objViewer.txtInvoice.Text.Trim(), out dt, 1);
            if (dt.Rows.Count > 0)
            {
                this.m_objViewer.lbeAuding.Text = "�����:" + dt.Rows[0]["LASTNAME_VCHR"].ToString().Trim();
            }
            else
            {
                this.m_objViewer.lbeAuding.Text = "δ���";
            }
            m_mthShowInfoByInvoiceNO(m_objViewer.txtInvoice.Text.ToString().Trim());
            this.m_strCreatInvoEmpID = string.Empty;
            this.currInvoNo = string.Empty;
            long lngRes = this.m_objManage.m_mthFindInvoiceByInvoNo(m_objViewer.txtInvoice.Text.ToString().Trim(), out this.m_strCreatInvoEmpID, out this.currInvoNo);
        }
        #endregion

        #region ���
        public void m_EmptyInput()
        {
            this.m_objViewer.txtCardID.Clear();
            this.m_objViewer.txtInvoice.Text = "";
            //this.m_objViewer.m_repInvoiceInfo.ReportSource = null;
            this.m_objViewer.m_lstItemsInfo.Items.Clear();
            this.m_objViewer.txtCardID.Focus();
            this.m_objViewer.lbeAuding.Text = "";
        }
        #endregion
        #region ���ݿ��Ų����Ʊ��
        public void m_mthFindInvoiceByCardID()
        {
            if (this.m_objViewer.txtCardID.Text.Trim() == "")
            {
                return;
            }
            DataTable dt;
            long lngRet = m_objManage.m_mthFindInvoiceByCardID(m_objViewer.txtCardID.Text.Trim(), out dt, 1, this.m_objViewer.cmbFind.SelectedIndex);
            this.m_objViewer.listView1.Items.Clear();
            if (lngRet > 0 && dt.Rows.Count > 0)
            {

                if (dt.Rows.Count == 1)
                {
                    this.m_objViewer.txtInvoice.Text = dt.Rows[0]["SEQID_CHR"].ToString().Trim();
                    this.m_objViewer.txtInvoice.Focus();
                    //�����������÷�Ʊ�ŵĴ���
                }
                else
                {
                    ListViewItem lv;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lv = new ListViewItem(dt.Rows[i]["INVOICENO_VCHR"].ToString().Trim());
                        lv.SubItems.Add(dt.Rows[i]["SEQID_CHR"].ToString().Trim());
                        if (dt.Rows[i]["repprninvono_vchr"] == DBNull.Value)
                            lv.SubItems.Add("");
                        else
                            lv.SubItems.Add(dt.Rows[i]["repprninvono_vchr"].ToString().Trim());
                        this.m_objViewer.listView1.Items.Add(lv);

                    }
                    this.m_objViewer.listView1.Show();
                    this.m_objViewer.listView1.BringToFront();
                    this.m_objViewer.listView1.Items[0].Selected = true;
                    this.m_objViewer.listView1.Focus();

                }
            }

        }
        #endregion
        #region ���ݷ�Ʊ����ʾ��Ϣ
        private clsPatientChargeCal objPC;
        private void m_mthShowInfoByInvoiceNO(string strInvoiceNO)
        {
            clsCalcPatientCharge objCalcPatienCharge = null;
            objCalcPatienCharge = new clsCalcPatientCharge("", "", 1, this.m_objComInfo.m_strGetHospitalTitle(), 0, 100);

            int setValue = clsPublic.m_intGetSysParm("0320");
            //��ʾ��Ʊ��ϸ��Ϣ
            objCalcPatienCharge.m_mthGetChargeItemByInvoiceID(strInvoiceNO, m_objViewer.m_lstItemsInfo, "1");
            if (setValue == 0)
            {

                //��ʾ��Ʊ��Ϣ			
                //m_objViewer.m_repInvoiceInfo.ReportSource = objCalcPatienCharge.m_mthPrintChargePreview(strInvoiceNO, "1", out objPC);
                objCalcPatienCharge.m_mthPrintChargePreview(strInvoiceNO, "1", out objPC);
                if (objPC.Reprintprninfo.Trim() != "")
                    objPC.Reprintprninfo = "*REPEAT(" + objPC.Reprintprninfo + ")*";
                m_objViewer.dwInvoice.Visible = false;
            }
            else
            {
                objCalcPatienCharge.m_mthPrintChargePreview(strInvoiceNO, "1", out objPC);

                //com.digitalwave.iCare.middletier.HIS.clsCalPatientChargeSvc domain =
                //    (clsCalPatientChargeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsCalPatientChargeSvc));

                DataTable chargeType = null;
                DataTable computation = null;
                if (objPC.Reprintprninfo.Trim() == "")
                    m_objManage.m_lngChargeItemTypeByInvoice(objPC.m_strInvoiceNO, out chargeType);
                else
                    m_objManage.m_lngChargeItemTypeByInvoice(objPC.Reprintprninfo, out chargeType);
                if (objPC.Reprintprninfo.Trim() != "")
                    objPC.Reprintprninfo = "*REPEAT(" + objPC.Reprintprninfo + ")*";
                m_objManage.m_lngComputationByScope("8", out computation);
                //domain.Dispose();
                //domain = null;

                m_objViewer.dwInvoice.LibraryList = Application.StartupPath + "\\pb_Invioce.pbl";
                m_objViewer.dwInvoice.DataWindowObject = "d_op_invoice_prt_new_diff";
                m_objViewer.dwInvoice.SetRedrawOff();
                m_objViewer.dwInvoice.Reset();
                m_objViewer.dwInvoice.InsertRow(0);


                List<String> arryText = clsNewInvoicPrint.s_arryGetInvoiceInfo(objPC, objCalcPatienCharge.ObjMain.m_mthCreatDataTable(objPC), chargeType, computation, -1);
                foreach (String text in arryText)
                {
                    m_objViewer.dwInvoice.Modify(text);
                }

                m_objViewer.dwInvoice.Visible = true;
            }
        }
        #endregion
        #region �Ƿ��ӡ��Ʊ
        private bool IsPrintInvoice = false;
        public void m_mthIsPrintInvoice()
        {
            clsDcl_OPCharge objTemp = new clsDcl_OPCharge();
            IsPrintInvoice = objTemp.m_mthIsCanDo("0006") == 1;
        }
        #endregion
        #region ���
        public void m_mthAudingInvoice()
        {
            //��֤��Ʊ�Ƿ����
            clsT_opr_outpatientrecipeinv_VO objResult = null;
            long lngRet = m_objManage.m_lngGetInfoByNoForReturn(m_objViewer.txtInvoice.Text.Trim(), out objResult);
            if (objResult == null || objResult.m_intSTATUS_INT != 1)
            {
                //��Ʊ������Ч�ķ�Ʊ����Ʊʧ�ܣ�
                MessageBox.Show(m_objViewer, "�˷�Ʊ������Ч�ķ�Ʊ!", "������ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //if (!string.IsNullOrEmpty(this.m_strCreatInvoEmpID) && m_strCreatInvoEmpID.Trim().CompareTo(this.m_objViewer.LoginInfo.m_strEmpID) !=0)
            //{
            //    switch (this.m_strOperateLevel)
            //    {
            //        case "0":
            //            break;
            //        case "1":
            //           if( MessageBox.Show("��Ʊ�����ڵ�ǰ��¼�û��������Ƿ�ȷ�����?","ϵͳ��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.Information,MessageBoxDefaultButton.Button2) !=DialogResult.Yes)
            //            {
            //                return;
            //            }
            //            break;
            //        case "2":
            //            MessageBox.Show("��Ʊ�����ڵ�ǰ��¼�û�������������˵�ǰ��Ʊ", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            return;
            //            //break;
            //        default:
            //            break;
            //    }
            //}

            frmAudingInvoice frmObj = new frmAudingInvoice(m_objViewer.txtInvoice.Text.Trim(), "1");
            frmObj.DataServer = this.m_objManage;
            frmObj.m_blnUseByInvoReturn = true;
            frmObj.m_strInvoCreatorID = m_strCreatInvoEmpID;
            frmObj.m_strLimitLevel = this.m_strOperateLevel;
            if (frmObj.ShowDialog() == DialogResult.OK)
            {
                this.m_objViewer.lbeAuding.Text = "�����:" + frmObj.AudingName;
            }
        }
        #endregion
    }
}
