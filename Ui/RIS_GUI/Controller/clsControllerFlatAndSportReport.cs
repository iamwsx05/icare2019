using System;
using weCare.Core.Entity;
using System.Windows.Forms;
using System.Data;
using com.digitalwave.iCare.Template.Client; 
using System.Collections.Generic;

namespace com.digitalwave.iCare.gui.RIS
{
	/// <summary>
	/// clsControllerFlatAndSportReport ��ժҪ˵����
	/// </summary>
	public class clsControllerFlatAndSportReport:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControllerFlatAndSportReport(frmFlatAndSportReport infrmCardiogramReport)
		{
			//��ʼ��ģ����Ϣ
            m_objViewer = infrmCardiogramReport;
            m_mthInitializeTemplate();
            //m_mthInitializeTemplate(infrmCardiogramReport);

		}
		#region ���ô������
		private com.digitalwave.iCare.gui.Security.clsController_Security objController_Security = null;
		private com.digitalwave.iCare.gui.TemplateUtility.clsHRPTemplateUtility m_objTemplateUtility;
		public System.Windows.Forms.ListViewItem objlsvItem = null;
		private com.digitalwave.iCare.gui.RIS.frmCardiogramReportManage objfrmCardiogramReportManage = null;
		com.digitalwave.iCare.gui.RIS.frmFlatAndSportReport m_objViewer;
		public clsafmt_report_VO m_objItem;
		clsDomainController_RISCardiogramManage m_objManage = new clsDomainController_RISCardiogramManage();
			private com.digitalwave.iCare.gui.RIS.clsController_RISCardiogramManage m_objControllerRISCManage = new clsController_RISCardiogramManage();
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmFlatAndSportReport)frmMDI_Child_Base_in;
		}

		public void SetParentApperance(com.digitalwave.iCare.gui.RIS.frmCardiogramReportManage infrmCardiogramReportManage)
		{
			objfrmCardiogramReportManage = infrmCardiogramReportManage;
			m_objViewer.LoginInfo=infrmCardiogramReportManage.LoginInfo;//�û���Ϣ����
		}
		#endregion
		
		#region ���TXTBOX
		/// <summary>
		/// ���TXTBOX
		/// </summary>
		/// <param name="p_objItem"></param>
		public void m_mthSetReport(clsafmt_report_VO p_objItem)
		{
			m_objItem = p_objItem;

			if(m_objItem == null)
			{
				m_objViewer.m_cmdDelete.Enabled = false;
				return;
			}
			m_objViewer.carID.Text=m_objItem.m_strCARD_ID_CHR;
			m_objViewer.m_txtREPORT_NO_CHR.Tag = m_objItem.m_strREPORT_ID_CHR;
			m_objViewer.m_txtREPORT_NO_CHR.Text = m_objItem.m_strREPORT_NO_CHR;
			m_objViewer.m_txtPATIENT_NAME_VCHR.Tag = m_objItem.m_strPATIENT_ID_CHR;
			m_objViewer.m_txtPATIENT_NO_CHR.Text = m_objItem.m_strPATIENT_NO_CHR;
			m_objViewer.m_txtINPATIENT_NO_CHR.Text = m_objItem.m_strINPATIENT_NO_CHR;
			m_objViewer.m_txtPATIENT_NAME_VCHR.Text = m_objItem.m_strPATIENT_NAME_VCHR;
			m_objViewer.m_cboSEX_CHR.Text = m_objItem.m_strSEX_CHR;
			string [] strAgeArr=m_objItem.m_strAGE_FLT.Split(" ".ToCharArray(),2);
			string strTextAge="1";
			string strCmbAge="��";
			//////////////////��һ������
			try
			{
				if(strAgeArr[0].Substring(strAgeArr[0].Length-1,1)=="ʱ")
				{
					strTextAge =strAgeArr[0].Substring(0,strAgeArr[0].Length-2);//����
					strCmbAge=strAgeArr[0].Substring(strTextAge.Trim().Length,2);//���䵥λ
				}
				else
				{
					strTextAge =strAgeArr[0].Substring(0,strAgeArr[0].Length-1);//����
					strCmbAge=strAgeArr[0].Substring(strTextAge.Trim().Length,1);//���䵥λ
				}
				m_objViewer.m_txtAGE_FLT.Text = strTextAge;
				for(int i =0;i<m_objViewer.m_cmbAge.Items.Count;i++)
				{
					if(m_objViewer.m_cmbAge.Items[i].ToString().Trim()==strCmbAge)
					{
						m_objViewer.m_cmbAge.SelectedIndex=i;
						break;
					}
				}
				//				m_objViewer.m_cmbAge.Text=strCmbAge;
				/////////////�ڶ�������
				if(strAgeArr[1].Substring(strAgeArr[1].Length-1,1)=="ʱ")
				{
					strTextAge =strAgeArr[1].Substring(0,strAgeArr[1].Length-2);//����
					//					strCmbAge=strAgeArr[1].Substring(strTextAge.Trim().Length,2);//���䵥λ
				}
				else
				{
					strTextAge =strAgeArr[1].Substring(0,strAgeArr[1].Length-1);//����
					//					strCmbAge=strAgeArr[1].Substring(strTextAge.Trim().Length,1);//���䵥λ
				}
				m_objViewer.m_txtSubAGE_FLT.Text = strTextAge;
				//				m_objViewer.m_labSubAge.Text=strCmbAge;
			}
			catch
			{
			
				//		MessageBox.Show(ex.Message );
			}
			m_objViewer.m_dtpCHECK_DAT.Value = Convert.ToDateTime(m_objItem.m_strCHECK_DAT.ToString());
			m_objViewer.m_dtpREPORT_DAT.Value = Convert.ToDateTime(m_objItem.m_strREPORT_DAT.ToString());
			m_objViewer.m_txtDEPT_NAME_VCHR.Tag = m_objItem.m_strDEPT_ID_CHR;
			m_objViewer.m_txtDEPT_NAME_VCHR.txtValuse = m_objItem.m_strDEPT_NAME_VCHR;
			m_objViewer.m_txtBED_NO_CHR.Tag = m_objItem.m_strBED_ID_CHR;
			m_objViewer.m_txtBED_NO_CHR.Text = m_objItem.m_strBED_NO_CHR;
			m_objViewer.m_txtRHYTHM_VCHR.Text = m_objItem.m_strRHYTHM_VCHR;
			m_objViewer.m_txtP_R_VCHR.Text = m_objItem.m_strP_R_VCHR;
			m_objViewer.m_txtQRS_VCHR.Text = m_objItem.m_strQRS_VCHR;
			m_objViewer.m_txtQ_T_VCHR.Text = m_objItem.m_strQ_T_VCHR;
			m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag = m_objItem.m_strREPORTOR_ID_CHR;
			m_objViewer.m_txtREPORTOR_NAME_VCHR.txtValuse = m_objItem.m_strREPORTOR_NAME_VCHR;

			m_objViewer.m_txtRHYTHM_VCHR.Text=m_objItem.m_strRHYTHM_VCHR.Trim();
			m_objViewer.m_txtLIE_PST_VCHR.Text=m_objItem.m_strLIE_PST_VCHR.Trim();
			m_objViewer.txtSTAND_PST_VCHR.Text=m_objItem.m_strSTAND_PST_VCHR.Trim();
			m_objViewer.txtDEEP_BREATH_VCHR.Text=m_objItem.m_strDEEP_BREATH_VCHR.Trim();
			m_objViewer.m_txtP_R_VCHR.Text=m_objItem.m_strP_R_VCHR.Trim();
			m_objViewer.m_txtQRS_VCHR.Text=m_objItem.m_strQRS_VCHR.Trim();
			m_objViewer.m_txtQ_T_VCHR.Text=m_objItem.m_strQ_T_VCHR.Trim();
			m_objViewer.textBoxTyped1.Text=m_objItem.m_strBEFORE_ACTIVE_VCHR.Trim();//.Trim();
			m_objViewer.txtBED_NO_CHR.Text=m_objItem.m_strCLINICAL_DIAGNOSE_VCHR.Trim();
			m_objViewer.m_txtBED_NO_CHR.Text=m_objItem.m_strBED_NO_CHR.Trim();
			m_objViewer.txtFORECAST_QTY_VCHR.Text=m_objItem.m_strFORECAST_QTY_VCHR.Trim();
			if(m_objItem.m_intFORECAST_QTY_INT==0)
				m_objViewer.rdbFORECAST_QTY_INT1.Checked=true;
			else
				m_objViewer.rdbFORECAST_QTY_INT2.Checked=true;
			m_objViewer.cboTEST_PLAN_VCHR.Text=m_objItem.m_strTEST_PLAN_VCHR.Trim();//.Trim();
			m_objViewer.txtACTIVE_LOAD_LEVEL_VCHR.Text=m_objItem.m_strACTIVE_LOAD_LEVEL_VCHR.Trim();
			m_objViewer.m_txtREPORTOR_NAME_VCHR.Text=m_objItem.m_strREPORTOR_NAME_VCHR.Trim();

			//			objItem.m_strSUMMARY2_XML_VCHR=m_objViewer.m_txtSUMMARY2_VCHR.m_strGetXmlText().Trim();
			m_objViewer.txtACTIVE_LOAD_MPH_VCHR.Text=m_objItem.m_strACTIVE_LOAD_MPH_VCHR.Trim();
			m_objViewer.textBoxTyped2.Text=m_objItem.m_strACTIVE_LOAD_PER_VCHR.Trim();
			
			string delimStr = ":";
			char [] delimiter = delimStr.ToCharArray();
			string[] strArr=new string[2];
			strArr=m_objItem.m_strACTIVE_TOTAL_TIME_VCHR.Split(delimiter,2);
			m_objViewer.txtACTIVE_TOTAL_TIME_VCHR1.Text=strArr[0].ToString();
			m_objViewer.txtACTIVE_TOTAL_TIME_VCHR2.Text=strArr[1].ToString();

			 m_objViewer.textBoxTyped6.Text=m_objItem.m_strHR_TOP_VCHR.Trim();
			m_objViewer.txtHR_PER_VCHR.Text=m_objItem.m_strHR_PER_VCHR.Trim();
			m_objViewer.txtHR_MAX_WORK_VCHR.Text=m_objItem.m_strHR_MAX_WORK_VCHR.Trim();
			m_objViewer.cboSTOP_REASON_VCHR.Text=m_objItem.m_strSTOP_REASON_VCHR.Trim();
			 m_objViewer.cboACTIVE_ST_VCHR.Text=m_objItem.m_strACTIVE_ST_VCHR.Trim();
			if(m_objItem.m_intACTIVE_ST_INT ==1)
			{
				m_objViewer.ACTIVE_ST_MAX_INT1.Checked=true;
			}
			else
				m_objViewer.ACTIVE_ST_MAX_INT2.Checked=true;
			m_objViewer.COMACTIVE_ST_MODE_VCHR.Text=m_objItem.m_strACTIVE_ST_MODE_VCHR.Trim() ;
			m_objViewer.txtAPPEAR_LED_VCHR.m_mthSetNewText(m_objItem.m_strAPPEAR_LED_VCHR,m_objItem.m_strAPPEAR_LED_XML_VCHR);
			 m_objViewer.txtHR_SCOPE_VCHR.m_mthSetNewText(m_objItem.m_strHR_SCOPE_VCHR,m_objItem.m_strHR_SCOPE_XML_VCHR);
			m_objViewer.txtTIME_SCOPE_VCHR.m_mthSetNewText(m_objItem.m_strTIME_SCOPE_VCHR,m_objItem.m_strTIME_SCOPE_XML_VCHR);

			m_objViewer.txtACTIVE_ST_VCHR.Text=m_objItem.m_strACTIVE_ST_MAX_VCHR.Trim();
			m_objViewer.txtHR_daolink_VCHR.Text=m_objItem.m_strACTIVE_ST_MAX_LED_VCHR.Trim();
			m_objViewer.txtHR_time_VCHR.Text=m_objItem.m_strACTIVE_ST_MAX_TIME_VCHR.Trim();
			 m_objViewer.txtHR_WRONG_VCHR.m_mthSetNewText(m_objItem.m_strHR_WRONG_VCHR,m_objItem.m_strHR_WRONG_XML_VCHR.Trim());//.Trim();
			 m_objViewer.txtACTIVED_BP_VCHR.Text=m_objItem.m_strACTIVED_BP_VCHR.Trim();
			m_objViewer.txtACTIVE_RESULT_VCHR.m_mthSetNewText(m_objItem.m_strACTIVE_RESULT_VCHR,m_objItem.m_strACTIVE_RESULT_XML_VCHR.Trim());//.Trim();
			m_objViewer.txtTEST_RESULT_VCHR.m_mthSetNewText(m_objItem.m_strTEST_RESULT_VCHR,m_objItem.m_strTEST_RESULT_XML_VCHR.Trim());
            if (m_objItem.m_strCONFIRMER_ID_CHR != "")
            {
                m_objViewer.m_cmdConfirm.Enabled = false;
                m_objViewer.m_cmdSave.Enabled = false;
            }
            else
            {
                m_objViewer.m_cmdConfirm.Enabled = true;
                m_objViewer.m_cmdSave.Enabled = true;
            }
        
        }
		public void m_mthSetReport(clsApplyReportList_VO p_objItem)
		{
			//m_objItem.m_intIS_INPATIENT_INT = 1;// p_objItem;
			m_objItem.m_strAGE_FLT = p_objItem.m_StrPatientAge;
			m_objItem.m_strBED_ID_CHR = p_objItem.m_StrBedID;
			m_objItem.m_strBED_NO_CHR = p_objItem.m_StrBedName;
			m_objItem.m_strCLINICAL_DIAGNOSE_VCHR = p_objItem.m_StrClinicDiagnose;
			m_objItem.m_strDEPT_ID_CHR = p_objItem.m_StrDeptID;
			m_objItem.m_strDEPT_NAME_VCHR = p_objItem.m_StrDeptName;
			m_objItem.m_strINPATIENT_NO_CHR = p_objItem.m_StrInPatientID;
			m_objItem.m_strPATIENT_ID_CHR = p_objItem.m_StrPatientID;
			m_objItem.m_strPATIENT_NAME_VCHR = p_objItem.m_StrPatientName;
			m_objItem.m_strPATIENT_NO_CHR = p_objItem.m_StrPatientCardID;
			m_objItem.m_strSEX_CHR = p_objItem.m_StrPatientSex;
			m_mthSetReport(m_objItem);			
		}
		#endregion

		#region ɾ������
		public void m_lngDeleData()
		{
			string strRordID=(string)this.m_objViewer.m_txtREPORT_NO_CHR.Tag;
			long lngRes=m_objManage.m_lngDeleteSportReport(strRordID);
			if(lngRes>0)
			{
				MessageBox.Show("ɾ���ɹ���","��ʾ");
				m_mthClear();
				m_objControllerRISCManage.Set_GUI_Apperance(objfrmCardiogramReportManage);
				m_objControllerRISCManage.m_mthGetSportReportArr();
                this.m_objViewer.m_cmdSave.Enabled = true;
			}

		}
		#endregion

		#region ����
		public void m_mthDoSaveSport()
		{
			if(!m_bolCheckValuePass())
				return;
//			if(this.m_objViewer.m_txtPATIENT_NAME_VCHR.Tag=="")
//			{
//				MessageBox.Show("  ����ID����Ϊ��","���ڿ��Ŵ����س���ѡ��");
//				this.m_objViewer.carID.Focus();
//				return;
//			}
			if(!m_objViewer.m_cheIsNew.Checked)
			{
				m_mthDoAddNew();
				m_objViewer.m_cheIsNew.Checked=true;
			}
			else
			{
				m_lngModifiy();
			}
			m_objViewer.m_cmdDelete.Enabled = true;
			m_objViewer.m_cmdConfirm.Enabled = true;
			m_objViewer.m_cmdPrint.Enabled = true;
		
		}
		#endregion

		#region ��ӡ
		/// <summary>
		/// ��ӡ
		/// </summary>
		/// <param name="infrmCardiogramReport"></param>
		public void m_mthPrintReport(frmFlatAndSportReport infrmCardiogramReport)
		{
			try
			{
				infrmCardiogramReport.m_printPrevDlg.Document = infrmCardiogramReport.m_printDoc;
                //clsController_RISCardiogramReport cls = new clsController_RISCardiogramReport(null);
                //cls.m_mthSetPrintPreviewDialogZoom10(infrmCardiogramReport.m_printPrevDlg);
                m_mthSetPrintPreviewDialogZoom10(infrmCardiogramReport.m_printPrevDlg);
				infrmCardiogramReport.m_printPrevDlg.ShowDialog();
			}
			catch
			{
				MessageBox.Show(infrmCardiogramReport,"��ӡ�����ϣ�","iCare",MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
		}

        #region ��printPreviewDialog���,������������Ϊ100%
        /// <summary>
        /// ��printPreviewDialog���,������������Ϊ100%
        /// </summary>
        /// <param name="p_printPreviewDialog"></param>
        public void m_mthSetPrintPreviewDialogZoom10(PrintPreviewDialog p_printPreviewDialog)
        {
            #region ��printPreviewDialog���,������������Ϊ100%
            PrintPreviewControl ppc = null;
            Form obj = (Form)p_printPreviewDialog.Controls.Owner;
            if (obj != null)
            {
                obj.WindowState = FormWindowState.Maximized;
            }
            foreach (System.Windows.Forms.Control c in p_printPreviewDialog.Controls)
            {
                if (c is PrintPreviewControl)
                {
                    ppc = (PrintPreviewControl)c;
                    break;
                }
            }
            ppc.Zoom = 1;
            #endregion
        }
        #endregion

		#endregion

		public void m_mthPrintDocPrintPage(System.Drawing.Printing.PrintPageEventArgs p_objPrintPageEventArgs)
		{
			clsPrint_RISSportReport objPrintReport = new clsPrint_RISSportReport();
			objPrintReport.objReportVO = m_objItem;
			objPrintReport.m_mthInitPrintTool(null);
			objPrintReport.m_mthPrintPage(p_objPrintPageEventArgs);
		}

		/// <summary>
		/// У������ֵ
		/// </summary>
		/// <returns></returns>
		private bool m_bolCheckValuePass()
		{
			bool bolReturn = true;

			if(m_objViewer.m_txtREPORT_NO_CHR.Text.Trim() == "")
			{
				MessageBox.Show("�ĵ�ͼ�Ų���Ϊ��!");
				m_objViewer.m_txtREPORT_NO_CHR.Select();
				return false;
			}

			if(m_objViewer.m_txtPATIENT_NAME_VCHR.Text.Trim() == "")
			{
				MessageBox.Show("�������Ʋ���Ϊ��!");
				m_objViewer.m_txtPATIENT_NAME_VCHR.Select();
				return false;
			
			}

			if(m_objViewer.m_txtAGE_FLT.Text.Trim() == "")
			{
				MessageBox.Show("�������䲻��Ϊ��!");
				m_objViewer.m_txtAGE_FLT.Select();
				return false;
	
			}
			if(m_objViewer.m_cmbAge.SelectedIndex <0)
			{
				MessageBox.Show("��ѡ�������䵥λ!");
				m_objViewer.m_cmbAge.Select();
				return false;
			}
			if(m_objViewer.m_txtDEPT_NAME_VCHR.txtValuse == "")
			{
				MessageBox.Show("���Ҳ���Ϊ��!");
				m_objViewer.m_txtDEPT_NAME_VCHR.Select();
                m_objViewer.m_txtDEPT_NAME_VCHR.Focus();
				return false;
			}
            else if (m_objViewer.m_txtDEPT_NAME_VCHR.Tag == null)
            {
                MessageBox.Show("��ȷ���������ҽԺ�������Ŀ���!");
                m_objViewer.m_txtDEPT_NAME_VCHR.Select();
                m_objViewer.m_txtDEPT_NAME_VCHR.Focus();
                return false;
            }
            else
            {
                int intJ = 0;
                for (int intI = 0; intI < m_objViewer.m_txtDEPT_NAME_VCHR.m_GetDataTable.Rows.Count; intI++)
                {
                    if (m_objViewer.m_txtDEPT_NAME_VCHR.m_GetDataTable.Rows[intI][1].ToString() == m_objViewer.m_txtDEPT_NAME_VCHR.txtValuse.Trim())
                    {
                        intJ++;
                    }
                }
                if (intJ == 0)
                {
                    MessageBox.Show("��ȷ���������ҽԺ�������Ŀ���!");
                    m_objViewer.m_txtDEPT_NAME_VCHR.Select();
                    m_objViewer.m_txtDEPT_NAME_VCHR.Focus();
                    return false;
                }
            }

            if (m_objViewer.m_txtREPORTOR_NAME_VCHR.txtValuse == "")
            {
                MessageBox.Show("����ҽʦ����Ϊ��!");
                m_objViewer.m_txtREPORTOR_NAME_VCHR.Select();
                m_objViewer.m_txtREPORTOR_NAME_VCHR.Focus();
                return false;
            }
            else if (m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag == null)
            {
                MessageBox.Show("��ȷ�����������Ժҽʦ!");
                m_objViewer.m_txtREPORTOR_NAME_VCHR.Select();
                m_objViewer.m_txtREPORTOR_NAME_VCHR.Focus();
                return false;
            }
            else
            {
                int intJ = 0;
                for (int intI = 0; intI < m_objViewer.m_txtREPORTOR_NAME_VCHR.m_GetDataTable.Rows.Count; intI++)
                {
                    if (m_objViewer.m_txtREPORTOR_NAME_VCHR.m_GetDataTable.Rows[intI][1].ToString() == m_objViewer.m_txtREPORTOR_NAME_VCHR.txtValuse.Trim())
                    {
                        intJ++;
                    }
                }
                if (intJ == 0)
                {
                    MessageBox.Show("��ȷ�����������Ժҽʦ!");
                    m_objViewer.m_txtREPORTOR_NAME_VCHR.Select();
                    m_objViewer.m_txtREPORTOR_NAME_VCHR.Focus();
                    return false;
                }
            }

			if(!bolReturn)
			{
				m_ephHandler.m_mthShowControlsErrorProvider();
				m_ephHandler.m_mthClearControl();
			}
			return bolReturn;
		}

		#region ģ��
        private clsTemplateClient m_objTemplate;
        //��ʼ��ģ��
        //public void m_mthInitializeTemplate(frmCardiogramReport infrmCardiogramReport)
        public void m_mthInitializeTemplate()
        {
            List<Control> list = m_lstGetRegisterControls();
            clsCommonTools common = new clsCommonTools();
            common.m_mthRegisterTemplate(m_objViewer, list);

            objController_Security = new com.digitalwave.iCare.gui.Security.clsController_Security();

            m_objTemplate = new clsTemplateClient(m_objViewer, objController_Security.objGetCurrentLoginEmployee().strEmpID, objController_Security.objGetCurrentLoginDepartment().strDeptID);
        }
        //����ģ��
        public void m_mthCreateTemplate()
        {
            m_objTemplate.m_mthCreateTemplate();
        }
        public List<System.Windows.Forms.Control> m_lstGetRegisterControls()
        {
            List<Control> list = new List<Control>();
            // frmCardiogramReport frm = objfrmCardiogramReportManage as frmCardiogramReport;
            list.Add(m_objViewer.txtHR_WRONG_VCHR);
            list.Add(m_objViewer.txtACTIVE_RESULT_VCHR);
            list.Add(m_objViewer.txtTEST_RESULT_VCHR);
            //list.Add(frm.);
            //list.Add(frm.m_txtImpressDiagnose);
            return list;
        }
        public void m_mthEditTemplate()
        {
            m_objTemplate.m_mthManageTemplate();
        }
		//��ʼ��ģ��
        //public void m_mthInitializeTemplate(frmFlatAndSportReport infrmCardiogramReport)
        //{
        //    objController_Security = new com.digitalwave.iCare.gui.Security.clsController_Security();
        //    m_objTemplateUtility = new com.digitalwave.iCare.gui.TemplateUtility.clsHRPTemplateUtility(objController_Security.objGetCurrentLoginPrincipal(), objController_Security.objGetCurrentLoginEmployee(), objController_Security.objGetCurrentLoginDepartment(), infrmCardiogramReport, true);
        //}
        ////����ģ��
        //public void m_mthCreateTemplate(frmFlatAndSportReport infrmCardiogramReport)
        //{
        //    com.digitalwave.iCare.middletier.DepartmentSvc.clsDeptSvc objDeptSvc = (com.digitalwave.iCare.middletier.DepartmentSvc.clsDeptSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.DepartmentSvc.clsDeptSvc));
        //    com.digitalwave.iCare.ValueObject.clsDepartmentVO[] objDeptList = null;
        //    long lngRes = objDeptSvc.m_lngGetDeptListByFussCriteria(objController_Security.objGetCurrentLoginPrincipal(), 1, "", "", "DEPTID_CHR", true, out objDeptList);

        //    com.digitalwave.iCare.gui.TemplateUtility.clsController_NewTemplate objController_NewTemplate = new clsController_NewTemplate(objController_Security.objGetCurrentLoginPrincipal());
        //    objController_NewTemplate.m_ObjDepartmentArr = objDeptList;
        //    objController_NewTemplate.m_ObjCurrDepartment = objController_Security.objGetCurrentLoginDepartment();
        //    objController_NewTemplate.m_ObjOperator = objController_Security.objGetCurrentLoginEmployee();
        //    objController_NewTemplate.m_mthShowNewTemplateDialog(infrmCardiogramReport, infrmCardiogramReport.Name, false);
        //}
		#endregion

		#region ���
		/// <summary>
		/// ���
		/// </summary>
		public void m_mthDoAddNew()
		{
			if(m_objViewer.m_txtREPORT_NO_CHR.Text=="")
			{
				MessageBox.Show("�������ĵ�ͼ��");
				return;
			}
			if(m_objViewer.m_txtPATIENT_NAME_VCHR.Text=="")
			{
				MessageBox.Show("����������");
				return;
			}
			if(m_objViewer.m_txtAGE_FLT.Text=="")
			{
				MessageBox.Show("����������");
				return;
			}
			clsafmt_report_VO objItem=new clsafmt_report_VO();
			m_lngGetVo(out objItem);
			string strReportID;
			long lngRes=m_objManage.m_lngDoAddNewSportReport(out strReportID,objItem);
			if(lngRes>0)
			{
				MessageBox.Show("��ӳɹ�");
			}
			else
				MessageBox.Show("���ʧ��");
			m_objViewer.m_cheIsNew.Tag=strReportID;
			objItem.m_strREPORT_ID_CHR = strReportID;
			m_objItem = objItem;
			m_objControllerRISCManage.Set_GUI_Apperance(objfrmCardiogramReportManage);
			m_objControllerRISCManage.m_mthGetSportReportArr();
			
			objfrmCardiogramReportManage.m_tbcMain.SelectedIndex=2;
		}
		#endregion

		#region �޸�����
		/// <summary>
		/// �޸�����
		/// </summary>

		public void m_lngModifiy()
		{
			clsafmt_report_VO objItem=new clsafmt_report_VO();
			m_lngGetVo(out objItem);
			objItem.m_strREPORT_ID_CHR=(string)this.m_objViewer.m_cheIsNew.Tag;
			

			long lngRes=m_objManage.m_lngModifySportReport(objItem);
			if(lngRes>0)
			{
				
				m_objItem = objItem;
				m_objControllerRISCManage.Set_GUI_Apperance(objfrmCardiogramReportManage);
			//	m_objControllerRISCManage.m_mthGetSportReportArr();
                //objfrmCardiogramReportManage.m_tbcMain.SelectedIndex=2;
                #region ˢ���б�
                m_objControllerRISCManage.m_mthQueryReportNew(objfrmCardiogramReportManage);
                #endregion
                MessageBox.Show("�޸ĳɹ�");
			}
			else
			{
				MessageBox.Show("�޸�ʧ��");
				this.m_objViewer.m_cheIsNew.Tag=null;
				this.m_objViewer.m_cheIsNew.Checked=false;
			}
		}
		#endregion

		#region ���û�����������䵽VO
		/// <summary>
		/// ���û�����������䵽VO
		/// </summary>
		/// <param name="objItem"></param>
		private void m_lngGetVo(out clsafmt_report_VO objItem)
		{
			objItem = new clsafmt_report_VO();
			objItem.m_strMODIFY_DAT = DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
			objItem.m_strOPERATOR_ID_CHR=m_objViewer.LoginInfo.m_strEmpID;
			objItem.m_strREPORT_NO_CHR = m_objViewer.m_txtREPORT_NO_CHR.Text.Trim();
			objItem.m_strFORECAST_QTY_VCHR=m_objViewer.txtFORECAST_QTY_VCHR.Text.Trim();
			objItem.m_strPATIENT_ID_CHR = (string)this.m_objViewer.m_txtPATIENT_NAME_VCHR.Tag;
			if(this.m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag!=null)
			objItem.m_strREPORT_ID_CHR=(string)this.m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag;
			objItem.m_strPATIENT_NO_CHR = m_objViewer.m_txtPATIENT_NO_CHR.Text.Trim();
			objItem.m_strINPATIENT_NO_CHR = m_objViewer.m_txtINPATIENT_NO_CHR.Text.Trim();
			objItem.m_strPATIENT_NAME_VCHR = m_objViewer.m_txtPATIENT_NAME_VCHR.Text.Trim();
			objItem.m_strSEX_CHR = m_objViewer.m_cboSEX_CHR.Text.Trim();
			string strSubAge="";
			string strSubAge2="";
			if(m_objViewer.m_txtSubAGE_FLT.Text.Trim()!=""&&m_objViewer.m_txtSubAGE_FLT.Text.Trim()!="0")
			{
				strSubAge= m_objViewer.m_labSubAge.Text;
				strSubAge2=m_objViewer.m_txtSubAGE_FLT.Text;
			}
			objItem.m_strAGE_FLT = m_objViewer.m_txtAGE_FLT.Text.Trim()+m_objViewer.m_cmbAge.Text.Trim()+" "+strSubAge2+strSubAge;
			objItem.m_strCHECK_DAT = m_objViewer.m_dtpCHECK_DAT.Value.ToString("yyyy-MM-dd HH:mm:ss");
			objItem.m_strREPORT_DAT = m_objViewer.m_dtpREPORT_DAT.Value.ToString("yyyy-MM-dd HH:mm:ss");
//			if(m_objViewer.m_txtDEPT_NAME_VCHR.Tag != null&&m_objViewer.m_txtDEPT_NAME_VCHR.Tag.ToString()!="")
//				objItem.m_strDEPT_ID_CHR = ((clsDepartmentVO)m_objViewer.m_txtDEPT_NAME_VCHR.Tag).strDeptID;
//			else
				objItem.m_strDEPT_ID_CHR = "";
			objItem.m_strDEPT_NAME_VCHR = m_objViewer.m_txtDEPT_NAME_VCHR.txtValuse.ToString().Trim();
			objItem.m_intIS_INPATIENT_INT = -1;
			objItem.m_strBED_NO_CHR= m_objViewer.m_txtBED_NO_CHR.Text;
			objItem.m_strCLINICAL_DIAGNOSE_VCHR =m_objViewer.txtBED_NO_CHR.Text;
			objItem.m_strRHYTHM_VCHR = m_objViewer.m_txtRHYTHM_VCHR.Text.Trim();
			objItem.m_strLIE_PST_VCHR = m_objViewer.m_txtLIE_PST_VCHR.Text.Trim();
			objItem.m_strSTAND_PST_VCHR = m_objViewer.txtSTAND_PST_VCHR.Text.Trim();
			objItem.m_strDEEP_BREATH_VCHR = m_objViewer.txtDEEP_BREATH_VCHR.Text.Trim();
			objItem.m_strP_R_VCHR = m_objViewer.m_txtP_R_VCHR.Text.Trim();
			objItem.m_strQRS_VCHR = m_objViewer.m_txtQRS_VCHR.Text.Trim();
			objItem.m_strQ_T_VCHR = m_objViewer.m_txtQ_T_VCHR.Text.Trim();
			objItem.m_strBEFORE_ACTIVE_VCHR = m_objViewer.textBoxTyped1.Text;//.Trim();
			if(m_objViewer.rdbFORECAST_QTY_INT1.Checked==true)
				objItem.m_intFORECAST_QTY_INT =0;
			else
				objItem.m_intFORECAST_QTY_INT =1;
			objItem.m_strTEST_PLAN_VCHR = m_objViewer.cboTEST_PLAN_VCHR.Text;//.Trim();
			objItem.m_strACTIVE_LOAD_LEVEL_VCHR = m_objViewer.txtACTIVE_LOAD_LEVEL_VCHR.Text;
			objItem.m_strREPORTOR_NAME_VCHR=m_objViewer.m_txtREPORTOR_NAME_VCHR.txtValuse;
			objItem.m_strACTIVE_LOAD_MPH_VCHR = m_objViewer.txtACTIVE_LOAD_MPH_VCHR.Text;
			objItem.m_strACTIVE_LOAD_PER_VCHR = m_objViewer.textBoxTyped2.Text.Trim();
			objItem.m_strACTIVE_TOTAL_TIME_VCHR = m_objViewer.txtACTIVE_TOTAL_TIME_VCHR1.Text.Trim()+":"+ m_objViewer.txtACTIVE_TOTAL_TIME_VCHR2.Text.Trim();
			objItem.m_strHR_TOP_VCHR = m_objViewer.textBoxTyped6.Text.Trim();
			objItem.m_strHR_PER_VCHR = m_objViewer.txtHR_PER_VCHR.Text.Trim();
			objItem.m_strHR_MAX_WORK_VCHR = m_objViewer.txtHR_MAX_WORK_VCHR.Text.Trim();
			objItem.m_strSTOP_REASON_VCHR = m_objViewer.cboSTOP_REASON_VCHR.Text.Trim();
			objItem.m_strACTIVE_ST_VCHR = m_objViewer.cboACTIVE_ST_VCHR.Text.Trim();
			if(m_objViewer.ACTIVE_ST_MAX_INT1.Checked==true)
				objItem.m_intACTIVE_ST_INT =1;
			if(m_objViewer.ACTIVE_ST_MAX_INT2.Checked==true)
				objItem.m_intACTIVE_ST_INT =2;
			objItem.m_strACTIVE_ST_MODE_VCHR = m_objViewer.COMACTIVE_ST_MODE_VCHR.Text.Trim();
			objItem.m_strAPPEAR_LED_VCHR = m_objViewer.txtAPPEAR_LED_VCHR.Text.Trim();
			objItem.m_strAPPEAR_LED_XML_VCHR = m_objViewer.txtAPPEAR_LED_VCHR.m_strGetXmlText().Trim();
			objItem.m_strHR_SCOPE_VCHR = m_objViewer.txtHR_SCOPE_VCHR.Text.Trim();
			objItem.m_strHR_SCOPE_XML_VCHR = m_objViewer.txtHR_SCOPE_VCHR.m_strGetXmlText().Trim();
			objItem.m_strTIME_SCOPE_VCHR = m_objViewer.txtTIME_SCOPE_VCHR.Text.Trim();
			objItem.m_strTIME_SCOPE_XML_VCHR = m_objViewer.txtTIME_SCOPE_VCHR.m_strGetXmlText().Trim();
			objItem.m_strACTIVE_ST_MAX_VCHR = m_objViewer.txtACTIVE_ST_VCHR.Text.Trim();
			objItem.m_strACTIVE_ST_MAX_LED_VCHR = m_objViewer.txtHR_daolink_VCHR.Text.Trim();
			objItem.m_strACTIVE_ST_MAX_TIME_VCHR = m_objViewer.txtHR_time_VCHR.Text.Trim();
			objItem.m_strHR_WRONG_VCHR = m_objViewer.txtHR_WRONG_VCHR.Text;//.Trim();
			objItem.m_strHR_WRONG_XML_VCHR = m_objViewer.txtHR_WRONG_VCHR.m_strGetXmlText().Trim();
			objItem.m_strACTIVED_BP_VCHR =m_objViewer.txtACTIVED_BP_VCHR.Text;
			objItem.m_strACTIVE_RESULT_VCHR = m_objViewer.txtACTIVE_RESULT_VCHR.Text;//.Trim();
			objItem.m_strACTIVE_RESULT_XML_VCHR = m_objViewer.txtACTIVE_RESULT_VCHR.m_strGetXmlText().Trim();
			objItem.m_strTEST_RESULT_VCHR = m_objViewer.txtTEST_RESULT_VCHR.Text;
			objItem.m_strTEST_RESULT_XML_VCHR = m_objViewer.txtTEST_RESULT_VCHR.m_strGetXmlText().Trim();
			objItem.m_strREPORT_ID_CHR=this.m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag.ToString();
			objItem.m_strDEPT_ID_CHR=this.m_objViewer.m_txtDEPT_NAME_VCHR.Tag.ToString();
		}
		#endregion

		#region ���(ƽ���˶�)
		/// <summary>
		///��� 
		/// </summary>
		public void m_lngEmp()
		{
			string strRordID=(string)this.m_objViewer.m_txtREPORT_NO_CHR.Tag;
			long lngRes=m_objManage.m_lngDeleteSportReportEmp(strRordID,this.m_objViewer.LoginInfo.m_strEmpID,this.m_objViewer.LoginInfo.m_strEmpName);
			if(lngRes>0)
			{
				MessageBox.Show("��˳ɹ���","��ʾ");
                #region ˢ���б�
                m_objControllerRISCManage.m_mthQueryReportNew(objfrmCardiogramReportManage);
                #endregion
				this.m_objViewer.m_cmdConfirm.Enabled=false;
                this.m_objViewer.m_cmdSave.Enabled = false;
			}

		}
		#endregion

		#region ���
		/// <summary>
		/// ���
		/// </summary>
		public void m_mthClear()
		{
			m_objViewer.m_txtREPORT_NO_CHR.Clear();
			m_objViewer.m_cheIsNew.Checked=false;
			m_objViewer.m_cmdDelete.Enabled=false;
			m_objViewer.m_cmdPrint.Enabled=false;
			m_objViewer.m_txtREPORT_NO_CHR.Tag = null;
			m_objViewer.m_txtREPORT_NO_CHR.Clear();
			m_objViewer.m_txtPATIENT_NO_CHR.Tag = null;
			m_objViewer.m_txtPATIENT_NO_CHR.Clear();
			m_objViewer.m_txtINPATIENT_NO_CHR.Clear();
			m_objViewer.m_txtPATIENT_NAME_VCHR.Clear();
			m_objViewer.m_cboSEX_CHR.Text = "";
			m_objViewer.m_txtAGE_FLT.Clear();
			m_objViewer.m_dtpCHECK_DAT.Value = DateTime.Now;
			m_objViewer.m_dtpREPORT_DAT.Value = DateTime.Now;
			m_objViewer.m_txtDEPT_NAME_VCHR.Tag = null;
			m_objViewer.m_txtDEPT_NAME_VCHR.txtValuse="";
			m_objViewer.m_txtBED_NO_CHR.Tag = null;
			m_objViewer.m_txtBED_NO_CHR.Clear();
			m_objViewer.m_txtRHYTHM_VCHR.SelectedIndex = 0;
			m_objViewer.m_txtP_R_VCHR.m_mthClearText();
			m_objViewer.m_txtQRS_VCHR.Clear();
			m_objViewer.m_txtQ_T_VCHR.Clear();
			m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag = null;
			m_objViewer.m_txtREPORTOR_NAME_VCHR.Text = "";
			m_objViewer.m_txtREPORT_NO_CHR.Select();
			m_objViewer.m_txtSubAGE_FLT.Clear();

			m_objViewer.cboTEST_PLAN_VCHR.Text="";
			m_objViewer.txtBED_NO_CHR.Clear();
			m_objViewer.m_txtRHYTHM_VCHR.Text="";
			m_objViewer.m_txtLIE_PST_VCHR.Clear();
			m_objViewer.txtSTAND_PST_VCHR.Clear();
			m_objViewer.txtDEEP_BREATH_VCHR.Clear();
			m_objViewer.m_txtP_R_VCHR.Clear();
			m_objViewer.m_txtQ_T_VCHR.Text = "";
			m_objViewer.textBoxTyped1.Clear();
			m_objViewer.txtFORECAST_QTY_VCHR.Clear();
			m_objViewer.txtACTIVE_LOAD_LEVEL_VCHR.Clear();
			m_objViewer.txtACTIVE_LOAD_MPH_VCHR.Clear();
			m_objViewer.textBoxTyped2.Clear();
			m_objViewer.txtACTIVE_TOTAL_TIME_VCHR1.Clear();
			m_objViewer.txtACTIVE_TOTAL_TIME_VCHR2.Clear();
			m_objViewer.textBoxTyped6.Clear();
			m_objViewer.txtHR_PER_VCHR.Clear();
			m_objViewer.m_txtQ_T_VCHR.Clear();
			m_objViewer.txtHR_MAX_WORK_VCHR.Clear();
			m_objViewer.cboSTOP_REASON_VCHR.Text = "";
			m_objViewer.cboACTIVE_ST_VCHR.Text="";
			m_objViewer.COMACTIVE_ST_MODE_VCHR.Text="";
            m_objViewer.m_cboSEX_CHR.Text="";
			 m_objViewer.m_cmbAge.Text="";
			m_objViewer.txtAPPEAR_LED_VCHR.Clear();
			m_objViewer.txtHR_SCOPE_VCHR.Clear();
			m_objViewer.txtTIME_SCOPE_VCHR.Clear();
			m_objViewer.txtACTIVE_ST_VCHR.Clear();
			m_objViewer.txtHR_daolink_VCHR.Clear();
			m_objViewer.txtHR_time_VCHR.Clear();
			m_objViewer.txtHR_WRONG_VCHR.Clear();
			m_objViewer.txtACTIVED_BP_VCHR.Clear();
			m_objViewer.txtACTIVE_RESULT_VCHR.Clear();
			m_objViewer.txtTEST_RESULT_VCHR.Clear();

		}
		#endregion

		#region ��ȡ��������
		/// <summary>
		/// ��ȡ��������
		/// </summary>
		public void m_lngGetPat()
		{
			DataTable bt=new DataTable();
			long lngRes=m_objManage.m_lngGetPat(this.m_objViewer.carID.Text.Trim(),out bt);
			if(lngRes<=0)
			{
				MessageBox.Show("��ȡ������Ϣ����","ϵͳ��ʾ");
				return;
			}
			if(bt.Rows.Count==0)
			{
				MessageBox.Show("�˿��Ż�û�еǼǣ�","��ʾ");
				//this.m_objViewer.carID.Focus();
				this.m_objViewer.m_txtREPORT_NO_CHR.Focus();
				return;
			}
			else
			{
				
				this.m_objViewer.m_txtINPATIENT_NO_CHR.Text=bt.Rows[0]["INPATIENTID_CHR"].ToString();
				this.m_objViewer.m_txtPATIENT_NAME_VCHR.Text=bt.Rows[0]["LASTNAME_VCHR"].ToString();
				this.m_objViewer.m_txtPATIENT_NAME_VCHR.Tag=bt.Rows[0]["PATIENTID_CHR"].ToString();
				this.m_objViewer.m_cboSEX_CHR.Text=bt.Rows[0]["SEX_CHR"].ToString();
				this.m_objViewer.m_txtREPORT_NO_CHR.Focus();
				if(bt.Rows[0]["BIRTH_DAT"].ToString()!="")
				{
					DateTime birth=DateTime.Parse(bt.Rows[0]["BIRTH_DAT"].ToString());
					DateTime newDate=DateTime.Now;
					if(birth.Month>newDate.Month)
					{
						int month=(newDate.Month+12)-birth.Month;
						int year=newDate.Year-1-birth.Year;
						this.m_objViewer.m_txtAGE_FLT.Text=year.ToString();
						this.m_objViewer.m_txtSubAGE_FLT.Text=month.ToString();
					}
					else
					{
						int month=newDate.Month-birth.Month;
						int year=newDate.Year-birth.Year;
						this.m_objViewer.m_txtAGE_FLT.Text=year.ToString();
						this.m_objViewer.m_txtSubAGE_FLT.Text=month.ToString();
					}
				}
				else
				{
					this.m_objViewer.m_txtAGE_FLT.Text="";
					this.m_objViewer.m_txtSubAGE_FLT.Text="";
				}
			}

		}

        /// <summary>
        /// ����סԺ�Ż�ȡ������Ϣ
        /// </summary>
        public void m_lngGetPatByInPatientID()
        {
            DataTable dtResult = new DataTable();
            long lngRes = m_objManage.m_lngGetPatByInPatientID(this.m_objViewer.m_txtINPATIENT_NO_CHR.Text.Trim(), out dtResult);
            if (lngRes <= 0)
            {
                MessageBox.Show("��ȡ������Ϣ����", "ϵͳ��ʾ");
                return;
            }
            if (dtResult.Rows.Count == 0)
            {
                MessageBox.Show("��סԺ�Ż�û�еǼǣ�", "��ʾ");
                this.m_objViewer.m_txtREPORT_NO_CHR.Focus();
                return;
            }
            else
            {
                System.Data.DataRow objDataRow = dtResult.Rows[0];
                // this.m_objViewer.m_txtINPATIENT_NO_CHR.Text = objDataRow["INPATIENTID_CHR"].ToString();
                this.m_objViewer.m_txtPATIENT_NAME_VCHR.Text = objDataRow["lastname_vchr"].ToString();
                this.m_objViewer.m_txtPATIENT_NAME_VCHR.Tag = objDataRow["patientid_chr"].ToString();
                this.m_objViewer.m_cboSEX_CHR.Text = objDataRow["sex_chr"].ToString();
                this.m_objViewer.m_txtDEPT_NAME_VCHR.txtValuse = objDataRow["deptname_vchr"].ToString();
                this.m_objViewer.m_txtDEPT_NAME_VCHR.Tag = objDataRow["deptid_chr"].ToString();
                if (!string.IsNullOrEmpty(objDataRow["bed_no"].ToString()))
                {
                    string strBedNo = objDataRow["bed_no"].ToString();
                    if (strBedNo.Length < 2 && strBedNo.Length > 0)
                        strBedNo = "00" + strBedNo;
                    this.m_objViewer.m_txtBED_NO_CHR.Text = strBedNo.Substring(strBedNo.Length - 2);
                    this.m_objViewer.m_txtBED_NO_CHR.Tag = objDataRow["bedid_chr"].ToString();
                }
                //this.m_objViewer.m_txtREPORT_NO_CHR.Focus();
                this.m_objViewer.m_txtREPORT_NO_CHR.Focus();

                if (objDataRow["birth_dat"].ToString() != "")
                {
                    DateTime birth = DateTime.Parse(objDataRow["birth_dat"].ToString());
                    DateTime newDate = DateTime.Now;
                    if (birth.Month > newDate.Month)
                    {
                        int month = (newDate.Month + 12) - birth.Month;
                        int year = newDate.Year - 1 - birth.Year;
                        this.m_objViewer.m_txtAGE_FLT.Text = year.ToString();
                        this.m_objViewer.m_txtSubAGE_FLT.Text = month.ToString();
                    }
                    else
                    {
                        int month = newDate.Month - birth.Month;
                        int year = newDate.Year - birth.Year;
                        this.m_objViewer.m_txtAGE_FLT.Text = year.ToString();
                        this.m_objViewer.m_txtSubAGE_FLT.Text = month.ToString();
                    }
                }
                else
                {
                    this.m_objViewer.m_txtAGE_FLT.Text = "";
                    this.m_objViewer.m_txtSubAGE_FLT.Text = "";
                }

            }

        }


		#endregion

        public void m_mthInitData()
        {
            DataTable dtValue = null;
            long lngRes = (new weCare.Proxy.ProxyRIS()).Service.m_lngGetDeptData(out dtValue);
            if (lngRes > 0)
            {
                m_objViewer.m_txtDEPT_NAME_VCHR.m_GetDataTable = dtValue;
            }
            DataTable dtDoctor = null;
            lngRes = (new weCare.Proxy.ProxyRIS()).Service.m_lngGetDoctorData(out dtDoctor, m_objViewer.LoginInfo.m_strDepartmentID, false);
            if (lngRes > 0)
            {
                m_objViewer.m_txtREPORTOR_NAME_VCHR.m_GetDataTable = dtDoctor;
            }
        }


	}


}
