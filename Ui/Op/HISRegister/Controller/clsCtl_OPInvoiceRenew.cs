using System;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HI;
using System.Data;
using System.Collections.Generic;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// ���﷢Ʊ�˻�
	/// ���ߣ� ����
	/// ����ʱ�䣺 Aug 26, 2004
	/// </summary>
	public class clsCtl_OPInvoiceRenew: com.digitalwave.GUI_Base.clsController_Base
	{
		#region ����
		clsDcl_InvoiceManage m_objManage = null;
		public string m_strReportID;
		public string m_strOperatorID;
		#endregion 

		#region ���캯��
		public clsCtl_OPInvoiceRenew()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			m_objManage = new clsDcl_InvoiceManage();
			m_strReportID = null;
			m_strOperatorID = "0000001";
		}
		#endregion 

		#region ���ô������
		com.digitalwave.iCare.gui.HIS.frmOPInvoiceRenew m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmOPInvoiceRenew)frmMDI_Child_Base_in;
		}
		#endregion

		#region �ָ���Ʊ
		/// <summary>
		/// �ָ���Ʊ
		/// </summary>
		public void m_ResumeTicket()
		{
			//�����Ʊ��Ϊ���򷵻�
			if(m_objViewer.txtInvoice.Text.Trim()=="")
			{
				MessageBox.Show(m_objViewer,"��Ʊ�Ų�������˷�Ʊ������Ʊ��","������ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			if(this.m_objViewer.LoginInfo!=null)
			{
				m_strOperatorID=this.m_objViewer.LoginInfo.m_strEmpID;
			}
			//��֤��Ʊ�Ƿ����
			clsT_opr_outpatientrecipeinv_VO objResult = null;
			long lngRet = m_objManage.m_lngGetInfoByNoForResume(m_objViewer.txtInvoice.Text.Trim(),out objResult);
			if(lngRet<=0)
			{
				//��Ʊʧ�ܣ�
				MessageBox.Show(m_objViewer,"�ָ�ʧ��!","������ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return ;
			}
			if(objResult ==null || objResult.m_intSTATUS_INT != 2)//��Ʊ״̬  [��Ʊ״̬��1-��Ч��0-���ϡ�2-��Ʊ]
			{
				//��Ʊ�����Ѿ��˵ķ�Ʊ���ָ�ʧ�ܣ�
				MessageBox.Show(m_objViewer,"�˷�Ʊ������Ʊ���ָ�ʧ��!","������ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			//��Ʊδ���,������Ʊ
			DataTable dt;
			lngRet =m_objManage.m_mthGetInvoiceAuditingInfo(m_objViewer.txtInvoice.Text.Trim(),out dt,2);
			if(dt.Rows.Count==0)
			{
				//��Ʊδ���,������Ʊ
				MessageBox.Show(m_objViewer,"��Ʊδ���,���ָܻ�!","������ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
            //��Ʊ�������ҩƷ��������Ʊ
            bool blContains=false;
            lngRet = m_objManage.m_CheckIsContainMed(m_objViewer.txtInvoice.Text.Trim(),ref blContains);
            if (lngRet <= 0)
            {
                MessageBox.Show(m_objViewer, "�ָ�ʧ��!", "������ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (blContains)
                {
                    MessageBox.Show(m_objViewer, "��Ʊ����ҩƷ�����ָܻ�", "������ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            string Seqid = "";
			lngRet = m_objManage.m_lngResumeTicket(m_objViewer.txtInvoice.Text.Trim(),m_strOperatorID, ref Seqid);
			if(lngRet<=0)
			{
				//��Ʊʧ�ܣ�
				MessageBox.Show(m_objViewer,"�ָ�ʧ�ܣ�","������ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return ;
			}
			else
			{
				//��Ʊ�ɹ���
				MessageBox.Show(m_objViewer,"��Ʊ�ָ��ɹ���","��ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Information);
				if(IsPrintInvoice)
				{
                    this.m_objViewer.Cursor = Cursors.WaitCursor;
                    clsCalcPatientCharge objCalPatientCharge = new clsCalcPatientCharge(this.m_objComInfo.m_strGetHospitalTitle());
                    objCalPatientCharge.m_mthReprintinvoice(Seqid, this.m_objViewer.LoginInfo.m_strEmpID, 2);
                    this.m_objViewer.Cursor = Cursors.Default;
				}
			}
			//��շ�Ʊ��
			m_EmptyInput();
		}
		#endregion 

		#region ���ݷ�Ʊ����ʾ�����
		public void DisplaySeqNo()
		{
//			m_objViewer.m_txtSEQID_CHR.Text ="";
//
//			//�����Ʊ��Ϊ���򷵻�
//			if(m_objViewer.m_txtINVOICENO_VCHR.Text.Trim()=="")
//				return;
//
//			clsT_opr_outpatientrecipeinv_VO objResult = null;
//			long lngRet = m_objManage.m_lngGetInfoByNoForResume(m_objViewer.m_txtINVOICENO_VCHR.Text.Trim(),out objResult);
//			if(lngRet>0 && objResult.m_strSEQID_CHR!=null)
//			{
//				m_objViewer.m_txtSEQID_CHR.Text = objResult.m_strSEQID_CHR;
//			}
		}
		#endregion

		#region �����������ʾ��Ʊ��
		public void DisplayInvoiceNo()
		{
			m_objViewer.txtInvoice.Text="";

//			//��������Ϊ���򷵻�
//			if(m_objViewer.m_txtSEQID_CHR.Text.Trim()=="")
//				return;
//
//			clsT_opr_outpatientrecipeinv_VO objResult = null;
//			long lngRet = m_objManage.m_lngGetInfoBySeqidForResume(m_objViewer.m_txtSEQID_CHR.Text.Trim(),out objResult);
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
			if(m_objViewer.txtInvoice.Text.ToString().Trim()=="" )
			{
				MessageBox.Show("��ѡ��Ʊ��","��ʾ");
				return;
			}
			DataTable dt;
			m_objManage.m_mthGetInvoiceAuditingInfo(m_objViewer.txtInvoice.Text.Trim(),out dt,2);
			if(dt.Rows.Count>0)
			{
				this.m_objViewer.lbeAuding.Text ="�����:"+dt.Rows[0]["LASTNAME_VCHR"].ToString().Trim();
			}
			else
			{
				this.m_objViewer.lbeAuding.Text ="δ���";
			}
			m_mthShowInfoByInvoiceNO(m_objViewer.txtInvoice.Text.Trim());
		}
		#endregion 

		#region ���
		public void m_EmptyInput()
		{
			this.m_objViewer.txtCardID.Clear();
			this.m_objViewer.txtInvoice.Text="";
			//this.m_objViewer.m_repInvoiceInfo.ReportSource=null;
			this.m_objViewer.m_lstItemsInfo.Items.Clear();
			this.m_objViewer.txtCardID.Focus();
			this.m_objViewer.lbeAuding.Text ="";
		}
		#endregion 
		#region ���ݿ��Ų����Ʊ��
		public void m_mthFindInvoiceByCardID()
		{
			if(this.m_objViewer.txtCardID.Text.Trim()=="")
			{
				return ;			
			}
			DataTable dt;
			long lngRet = m_objManage.m_mthFindInvoiceByCardID(m_objViewer.txtCardID.Text.Trim(),out dt,2,this.m_objViewer.cmbFind.SelectedIndex);
			this.m_objViewer.listView1.Items.Clear();
			if(dt.Rows.Count==0)
			{
			return;
			}
			if(dt.Rows.Count==1)
			{
				this.m_objViewer.txtInvoice.Text=dt.Rows[0]["SEQID_CHR"].ToString().Trim();
				this.m_objViewer.txtInvoice.Focus();
				//�����������÷�Ʊ�ŵĴ���
			}
			else
			{
				ListViewItem lv;
				for(int i=0;i<dt.Rows.Count;i++)
				{
					lv=new ListViewItem(dt.Rows[i]["INVOICENO_VCHR"].ToString().Trim());
					lv.SubItems.Add(dt.Rows[i]["SEQID_CHR"].ToString().Trim());
					this.m_objViewer.listView1.Items.Add(lv);
                        					
				}
				this.m_objViewer.listView1.Show();
				this.m_objViewer.listView1.BringToFront();
				this.m_objViewer.listView1.Items[0].Selected=true;
				this.m_objViewer.listView1.Focus();
									
			}
		}
		#endregion
		#region ���ݷ�Ʊ����ʾ��Ϣ
		private clsPatientChargeCal objPC;
		private void m_mthShowInfoByInvoiceNO(string strInvoiceNO)
		{
			clsCalcPatientCharge objCalcPatienCharge =null;
            objCalcPatienCharge = new clsCalcPatientCharge("", "", 1, this.m_objComInfo.m_strGetHospitalTitle(), 0, 100);
             int setValue = clsPublic.m_intGetSysParm("0320");
             //��ʾ��Ʊ��ϸ��Ϣ
             objCalcPatienCharge.m_mthGetChargeItemByInvoiceID(strInvoiceNO, m_objViewer.m_lstItemsInfo, "2");
             if (setValue == 0)
             {
                //��ʾ��Ʊ��Ϣ			
                //m_objViewer.m_repInvoiceInfo.ReportSource = objCalcPatienCharge.m_mthPrintChargePreview(strInvoiceNO, "2", out objPC);
                objCalcPatienCharge.m_mthPrintChargePreview(strInvoiceNO, "2", out objPC);
                if (objPC.Reprintprninfo.Trim() != "")
                     objPC.Reprintprninfo = "*REPEAT(" + objPC.Reprintprninfo + ")*";
                 m_objViewer.dwInvoice.Visible = false;
             }
             else
             {
                 objCalcPatienCharge.m_mthPrintChargePreview(strInvoiceNO, "2", out objPC);

                 DataTable chargeType = null;
                 DataTable computation = null;
                 if (objPC.Reprintprninfo.Trim() == "")
                     m_objManage.m_lngChargeItemTypeByInvoice(objPC.m_strInvoiceNO, out chargeType);
                 else
                     m_objManage.m_lngChargeItemTypeByInvoice(objPC.Reprintprninfo, out chargeType);
                 if (objPC.Reprintprninfo.Trim() != "")
                     objPC.Reprintprninfo = "*REPEAT(" + objPC.Reprintprninfo + ")*";
                 m_objManage.m_lngComputationByScope("8", out computation);
                

                 m_objViewer.dwInvoice.LibraryList = Application.StartupPath + "\\pb_Invioce.pbl";
                 m_objViewer.dwInvoice.DataWindowObject = "d_op_invoice_prt_new";
                 m_objViewer.dwInvoice.SetRedrawOff();
                 m_objViewer.dwInvoice.Reset();
                 m_objViewer.dwInvoice.InsertRow(0);


                 List<String> arryText = clsNewInvoicPrint.s_arryGetInvoiceInfo(objPC, objCalcPatienCharge.ObjMain.m_mthCreatDataTable(objPC), chargeType, computation,-1);
                 foreach (String text in arryText)
                 {
                     m_objViewer.dwInvoice.Modify(text);
                 }

                 m_objViewer.dwInvoice.Visible = true;
             }
		}
		#endregion
		#region �Ƿ��ӡ��Ʊ
		private bool IsPrintInvoice=false;
		public void m_mthIsPrintInvoice()
		{
			clsDcl_OPCharge objTemp =new clsDcl_OPCharge();
			IsPrintInvoice =objTemp.m_mthIsCanDo("0006")==1;
		}
		#endregion
		#region ���
		public void m_mthAudingInvoice()
		{
			//��֤��Ʊ�Ƿ����
			clsT_opr_outpatientrecipeinv_VO objResult = null;
			long lngRet = m_objManage.m_lngGetInfoByNoForResume(m_objViewer.txtInvoice.Text.Trim(),out objResult);
			if(objResult ==null || objResult.m_intSTATUS_INT != 2)
			{
				//��Ʊ������Ч�ķ�Ʊ����Ʊʧ�ܣ�
				MessageBox.Show(m_objViewer,"�˷�Ʊ������Ч�ķ�Ʊ!","������ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			frmAudingInvoice frmObj =new frmAudingInvoice(m_objViewer.txtInvoice.Text.Trim(),"2");
			frmObj.DataServer =this.m_objManage;
			if(frmObj.ShowDialog()==DialogResult.OK)
			{
				this.m_objViewer.lbeAuding.Text ="�����:"+frmObj.AudingName;
			}
		}
		#endregion
	}
}
