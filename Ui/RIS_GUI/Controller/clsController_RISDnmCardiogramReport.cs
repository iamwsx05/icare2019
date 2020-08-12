using System;
using weCare.Core.Entity;
using System.Windows.Forms;
using System.Data; 
using System.Collections.Generic;
using com.digitalwave.iCare.Template.Client;

namespace com.digitalwave.iCare.gui.RIS
{
	/// <summary>
	/// clsController_RISCardiogramReport 的摘要说明。
	/// 作者： 
	/// 时间：
	/// </summary>
	public class clsController_RISDnmCardiogramReport : com.digitalwave.GUI_Base.clsController_Base
	{
		public clsController_RISDnmCardiogramReport(frmDnmCardiogramReport infrmDnmCardiogramReport)
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			m_objManage = new clsDomainController_RISCardiogramManage();
			m_objItem = null;
			m_strOperatorID = "0000001";

			//初始化模板信息
            m_objViewer = infrmDnmCardiogramReport;
            m_mthInitializeTemplate();
            //m_mthInitializeTemplate(infrmDnmCardiogramReport);
		}

		clsDomainController_RISCardiogramManage m_objManage = null;

		public clsRIS_DCardiogramReport_VO m_objItem;

		public string m_strOperatorID;

		private com.digitalwave.iCare.gui.Security.clsController_Security objController_Security = null;
		private com.digitalwave.iCare.gui.TemplateUtility.clsHRPTemplateUtility m_objTemplateUtility;
		private com.digitalwave.iCare.gui.RIS.clsController_RISCardiogramManage m_objControllerRISCManage = new clsController_RISCardiogramManage();
		private com.digitalwave.iCare.gui.RIS.frmCardiogramReportManage objfrmCardiogramReportManage = null;

		#region 设置窗体对象

		com.digitalwave.iCare.gui.RIS.frmDnmCardiogramReport m_objViewer;

		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmDnmCardiogramReport)frmMDI_Child_Base_in;
		}

		public void SetParentApperance(com.digitalwave.iCare.gui.RIS.frmCardiogramReportManage infrmCardiogramReportManage)
		{
			objfrmCardiogramReportManage = infrmCardiogramReportManage;
			m_objViewer.LoginInfo=infrmCardiogramReportManage.LoginInfo;//用户信息传递
		}
		#endregion

		public void m_mthSetReport(clsRIS_DCardiogramReport_VO p_objItem)
		{
			m_objItem = p_objItem;

			if(m_objItem == null)
			{
				m_objViewer.m_cmdDelete.Enabled = false;
				return;
			} 
			

			m_objViewer.carID.Text=m_objItem.m_strCARD_ID_CHR;
			#region    根据 卡号 检索病人ID       
			
			long lng=-1;
			if(m_objViewer.carID.Text!="")
			{
				DataTable  tbPat = new DataTable();
				lng=m_objManage.m_lngGetPat(m_objViewer.carID.Text,out tbPat);
				if(lng>0)
					m_objViewer.m_txtPATIENT_NAME_VCHR.Tag=tbPat.Rows[0]["PATIENTID_CHR"].ToString();
			}
			# endregion
			m_objViewer.m_txtREPORT_NO_CHR.Tag = m_objItem.m_strREPORT_ID_CHR;
			m_objViewer.m_txtREPORT_NO_CHR.Text = m_objItem.m_strREPORT_NO_CHR;
			m_objViewer.m_txtPATIENT_NO_CHR.Tag = m_objItem.m_strPATIENT_ID_CHR;
			m_objViewer.m_txtPATIENT_NO_CHR.Text = m_objItem.m_strPATIENT_NO_CHR;
			m_objViewer.m_txtINPATIENT_NO_CHR.Text = m_objItem.m_strINPATIENT_NO_CHR;
			m_objViewer.m_txtPATIENT_NAME_VCHR.Text = m_objItem.m_strPATIENT_NAME_VCHR;
			m_objViewer.m_cboSEX_CHR.Text = m_objItem.m_strSEX_CHR;
			string [] strAgeArr=m_objItem.m_strAGE_FLT.Split(" ".ToCharArray(),2);
			string strTextAge="1";
			string strCmbAge="岁";
			//////////////////第一段年龄
			try
			{
				if(strAgeArr[0].Substring(strAgeArr[0].Length-1,1)=="时")
				{
					strTextAge =strAgeArr[0].Substring(0,strAgeArr[0].Length-2);//年龄
					strCmbAge=strAgeArr[0].Substring(strTextAge.Trim().Length,2);//年龄单位
				}
				else
				{
					strTextAge =strAgeArr[0].Substring(0,strAgeArr[0].Length-1);//年龄
					strCmbAge=strAgeArr[0].Substring(strTextAge.Trim().Length,1);//年龄单位
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
				/////////////第二段年龄
				if(strAgeArr[1].Substring(strAgeArr[1].Length-1,1)=="时")
				{
					strTextAge =strAgeArr[1].Substring(0,strAgeArr[1].Length-2);//年龄
					//					strCmbAge=strAgeArr[1].Substring(strTextAge.Trim().Length,2);//年龄单位
				}
				else
				{
					strTextAge =strAgeArr[1].Substring(0,strAgeArr[1].Length-1);//年龄
					//					strCmbAge=strAgeArr[1].Substring(strTextAge.Trim().Length,1);//年龄单位
				}
				m_objViewer.m_txtSubAGE_FLT.Text = strTextAge;
				//				m_objViewer.m_labSubAge.Text=strCmbAge;
			}
			catch
			{
			
				//		MessageBox.Show(ex.Message );
			}
			m_objViewer.m_dtpREPORT_DAT.Value = Convert.ToDateTime(m_objItem.m_strREPORT_DAT.ToString());
			m_objViewer.m_txtDEPT_NAME_VCHR.Tag = m_objItem.m_strDEPT_ID_CHR;
			m_objViewer.m_txtDEPT_NAME_VCHR.txtValuse = m_objItem.m_strDEPT_NAME_VCHR;
			m_objViewer.m_txtBED_NO_CHR.Tag = m_objItem.m_strBED_ID_CHR;
			m_objViewer.m_txtBED_NO_CHR.Text = m_objItem.m_strBED_NO_CHR;
			//m_objViewer.m_txtCLINICAL_DIAGNOSE_VCHR.Text = m_objItem.m_strCLINICAL_DIAGNOSE_VCHR;
			//m_objViewer.m_txtSUMMARY1_VCHR.Text = m_objItem.m_strSUMMARY1_VCHR;
			//m_objViewer.m_txtSUMMARY2_VCHR.Text = m_objItem.m_strSUMMARY2_VCHR;
			m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag = m_objItem.m_strREPORTOR_ID_CHR;
			m_objViewer.m_txtREPORTOR_NAME_VCHR.txtValuse = m_objItem.m_strREPORTOR_NAME_VCHR;

			m_objViewer.m_dtpCHECKFROM_DAT.Value = Convert.ToDateTime(m_objItem.m_strCHECKFROM_DAT.ToString());
			m_objViewer.m_dtpCHECKTO_DAT.Value = Convert.ToDateTime(m_objItem.m_strCHECKTO_DAT.ToString());

			//m_objViewer.m_txtCHECK_CHANNELS_VCHR.Text = m_objItem.m_strCHECK_CHANNELS_VCHR;
			m_objViewer.m_txtCLINICAL_DIAGNOSE_VCHR.m_mthSetNewText(m_objItem.m_strCLINICAL_DIAGNOSE_VCHR,m_objItem.m_strCLINICAL_DIAGNOSE_XML_VCHR);
			m_objViewer.m_txtCHECK_CHANNELS_VCHR.m_mthSetNewText(m_objItem.m_strCHECK_CHANNELS_VCHR,m_objItem.m_strCHECK_CHANNELS_XML_VCHR);
			m_objViewer.m_txtSUMMARY1_VCHR.m_mthSetNewText(m_objItem.m_strSUMMARY1_VCHR,m_objItem.m_strSUMMARY1_XML_VCHR);
			m_objViewer.m_txtSUMMARY2_VCHR.m_mthSetNewText(m_objItem.m_strSUMMARY2_VCHR,m_objItem.m_strSUMMARY2_XML_VCHR);

			m_mthSetGraphType(m_objItem.m_intGRAPH_TYPE_INT);
			m_objViewer.m_txtQRS_TOTAL_CHR.Text = m_objItem.m_strQRS_TOTAL_CHR;
			m_objViewer.m_txtHEARTRATE_AVERAGE_INT.Text = m_objItem.m_intHEARTRATE_AVERAGE_INT.ToString();
			m_objViewer.m_txtHEARTRATE_MAX_INT.Text = m_objItem.m_intHEARTRATE_MAX_INT.ToString();
			m_objViewer.m_txtHEARTRATE_MIN_INT.Text = m_objItem.m_intHEARTRATE_MIN_INT.ToString();
			m_objViewer.m_dtpHEARTRATE_MAX_DAT.Value = Convert.ToDateTime(m_objItem.m_strHEARTRATE_MAX_DAT.ToString());
			m_objViewer.m_dtpHEARTRATE_MIN_DAT.Value = Convert.ToDateTime(m_objItem.m_strHEARTRATE_MIN_DAT.ToString());
			m_objViewer.m_cheIsSpical.Checked=m_objItem.m_intIsSpicalPatient==1;
//			this.m_objViewer.m_cmbHEARTRATE_BASE_INT.SelectedIndex=m_objItem.m_intHEARTRATE_BASE_INT;
			this.m_objViewer.m_cmbHEARTRATE_BASE_INT.Text=m_objItem.m_strHEARTRATE_BASE_VCHR;
//			m_mthSetHeartRateType(m_objItem.m_intHEARTRATE_BASE_INT);

            if (m_objItem.m_strCONFIRMER_ID_CHR == "")
            {
                m_objViewer.m_cmdAddNew.Enabled = true;
                m_objViewer.m_cmdConfirm.Enabled = true;
            }
            else
            {
                m_objViewer.m_cmdAddNew.Enabled = false;
                m_objViewer.m_cmdConfirm.Enabled=false;
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
		private void m_mthSetGraphType(int p_intType)
		{
			switch(p_intType)
			{
				case 0:
					m_objViewer.m_rdbGRAPH_TYPE_INT0.Checked = true;
					break;
				case 1:
					m_objViewer.m_rdbGRAPH_TYPE_INT1.Checked = true;
					break;
				case 2:
					m_objViewer.m_rdbGRAPH_TYPE_INT2.Checked = true;
					break;
				case 3:
					m_objViewer.m_rdbGRAPH_TYPE_INT3.Checked = true;
					break;

			}
		}

		private void m_mthSetHeartRateType(int p_intType)
		{
			switch(p_intType)
			{
				case 0:
					m_objViewer.m_rdbHEARTRATE_BASE_INT0.Checked = true;
					break;
				case 1:
					m_objViewer.m_rdbHEARTRATE_BASE_INT1.Checked = true;
					break;
				case 2:
					m_objViewer.m_rdbHEARTRATE_BASE_INT2.Checked = true;
					break;
				case 3:
					m_objViewer.m_rdbHEARTRATE_BASE_INT3.Checked = true;
					break;

			}
		}

		public void m_mthDoSave()
		{
			 if(!m_bolCheckValuePass())
				return;
//			if(this.m_objViewer.m_txtPATIENT_NAME_VCHR.Tag=="")
//			{
//				MessageBox.Show("    病人ID不能为空","请在卡号处按回车键选择");
//				this.m_objViewer.carID.Focus();
//				return;
//			}
			if(m_objItem == null||m_objViewer.m_cheIsNew.Checked)
			{
				m_mthDoAddNew(out m_objItem);
				m_objViewer.m_cheIsNew.Checked=false;
				m_objViewer.m_txtREPORT_NO_CHR.Tag=m_objItem.m_strREPORT_ID_CHR;
			}
			else
			{
				m_mthDoModify();
			}
			m_objViewer.m_cmdDelete.Enabled = true;
			m_objViewer.m_cmdConfirm.Enabled = true;
			m_objViewer.m_cmdPrint.Enabled = true;
		}

		#region 添加
		/// <summary>
		/// 添加
		/// </summary>
		public void m_mthDoAddNew(out clsRIS_DCardiogramReport_VO objItem)
		{
			objItem = new clsRIS_DCardiogramReport_VO();
			objItem.m_strREPORT_ID_CHR = "";
			objItem.m_strMODIFY_DAT = "";
			objItem.m_strREPORT_NO_CHR = m_objViewer.m_txtREPORT_NO_CHR.Text.Trim();
            objItem.m_strREPORT_ID_CHR = this.m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag.ToString(); ;
            objItem.m_strDEPT_ID_CHR = this.m_objViewer.m_txtDEPT_NAME_VCHR.Tag.ToString();
            //objItem.m_strDEPT_ID_CHR=this.m_objViewer.m_txtDEPT_NAME_VCHR.m_StrDeptID;
			if(m_objViewer.m_txtPATIENT_NAME_VCHR.Tag != null)
				objItem.m_strPATIENT_ID_CHR = m_objViewer.m_txtPATIENT_NAME_VCHR.Tag.ToString().Trim();
			else
				objItem.m_strPATIENT_ID_CHR = "";
			objItem.m_strPATIENT_NO_CHR = m_objViewer.m_txtPATIENT_NO_CHR.Text.Trim();
			objItem.m_strINPATIENT_NO_CHR = m_objViewer.m_txtINPATIENT_NO_CHR.Text.Trim();
			objItem.m_strPATIENT_NAME_VCHR = m_objViewer.m_txtPATIENT_NAME_VCHR.Text.Trim();
			objItem.m_strSEX_CHR = m_objViewer.m_cboSEX_CHR.Text.Trim();
//			objItem.m_strAGE_FLT = m_objViewer.m_txtAGE_FLT.Text.Trim()+m_objViewer.m_cmbAge.Text;
			string strSubAge="";
			string strSubAge2="";
			if(m_objViewer.m_txtSubAGE_FLT.Text.Trim()!=""&&m_objViewer.m_txtSubAGE_FLT.Text.Trim()!="0")
			{
				strSubAge= m_objViewer.m_labSubAge.Text;
				strSubAge2=m_objViewer.m_txtSubAGE_FLT.Text;
			}
			objItem.m_strAGE_FLT = m_objViewer.m_txtAGE_FLT.Text.Trim()+m_objViewer.m_cmbAge.Text.Trim()+" "+strSubAge2+strSubAge;
			objItem.m_strREPORT_DAT = m_objViewer.m_dtpREPORT_DAT.Value.ToString("yyyy-MM-dd HH:mm:ss");
			if(m_objViewer.m_txtDEPT_NAME_VCHR.Tag != null&&m_objViewer.m_txtDEPT_NAME_VCHR.Tag.ToString()!="")
                //objItem.m_strDEPT_ID_CHR = ((clsDepartmentVO)m_objViewer.m_txtDEPT_NAME_VCHR.Tag).strDeptID.Trim();
                objItem.m_strDEPT_ID_CHR = m_objViewer.m_txtDEPT_NAME_VCHR.Tag.ToString();
			else
				objItem.m_strDEPT_ID_CHR = "";
            //objItem.m_strDEPT_NAME_VCHR = m_objViewer.m_txtDEPT_NAME_VCHR.Text.Trim();
            objItem.m_strDEPT_NAME_VCHR = m_objViewer.m_txtDEPT_NAME_VCHR.txtValuse;
			objItem.m_intIS_INPATIENT_INT = -1;
			if(m_objViewer.m_txtBED_NO_CHR.Tag != null&&m_objViewer.m_txtBED_NO_CHR.Tag.ToString()!="")
				objItem.m_strBED_ID_CHR = m_objViewer.m_txtBED_NO_CHR.Tag.ToString().Trim();
			else
				objItem.m_strBED_ID_CHR = "";
			objItem.m_strBED_NO_CHR = m_objViewer.m_txtBED_NO_CHR.Text.Trim();
			//objItem.m_strCLINICAL_DIAGNOSE_VCHR = m_objViewer.m_txtCLINICAL_DIAGNOSE_VCHR.Text.Trim();
			//objItem.m_strSUMMARY1_VCHR = m_objViewer.m_txtSUMMARY1_VCHR.Text.Trim();
			//objItem.m_strSUMMARY2_VCHR = m_objViewer.m_txtSUMMARY2_VCHR.Text.Trim();
			if(m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag != null&&m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag.ToString()!="")
                //objItem.m_strREPORTOR_ID_CHR = ((clsEmployeeVO)m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag).strEmpID.Trim();
                objItem.m_strREPORTOR_ID_CHR = m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag.ToString();
			else
				objItem.m_strREPORTOR_ID_CHR = "";

			objItem.m_strREPORTOR_NAME_VCHR = m_objViewer.m_txtREPORTOR_NAME_VCHR.txtValuse.Trim();
			objItem.m_intSTATUS_INT = 1;
			objItem.m_strOPERATOR_ID_CHR = m_objViewer.LoginInfo.m_strEmpID.Trim();
			objItem.m_strCHECKFROM_DAT = m_objViewer.m_dtpCHECKFROM_DAT.Value.ToString("yyyy-MM-dd HH:mm:ss");
			objItem.m_strCHECKTO_DAT = m_objViewer.m_dtpCHECKTO_DAT.Value.ToString("yyyy-MM-dd HH:mm:ss");
			//objItem.m_strCHECK_CHANNELS_VCHR = m_objViewer.m_txtCHECK_CHANNELS_VCHR.Text.Trim();
			
			objItem.m_strCLINICAL_DIAGNOSE_VCHR = m_objViewer.m_txtCLINICAL_DIAGNOSE_VCHR.Text.Trim();
			objItem.m_strSUMMARY1_VCHR = m_objViewer.m_txtSUMMARY1_VCHR.Text;//.Trim();
			objItem.m_strSUMMARY2_VCHR = m_objViewer.m_txtSUMMARY2_VCHR.Text;//.Trim();
			objItem.m_strCHECK_CHANNELS_VCHR = m_objViewer.m_txtCHECK_CHANNELS_VCHR.Text.Trim();

			objItem.m_strCLINICAL_DIAGNOSE_XML_VCHR = m_objViewer.m_txtCLINICAL_DIAGNOSE_VCHR.m_strGetXmlText().Trim();
			objItem.m_strSUMMARY1_XML_VCHR = m_objViewer.m_txtSUMMARY1_VCHR.m_strGetXmlText().Trim();
			objItem.m_strSUMMARY2_XML_VCHR = m_objViewer.m_txtSUMMARY2_VCHR.m_strGetXmlText().Trim();
			objItem.m_strCHECK_CHANNELS_XML_VCHR = m_objViewer.m_txtCHECK_CHANNELS_VCHR.m_strGetXmlText().Trim();

			objItem.m_intGRAPH_TYPE_INT = m_intGetGraphTypeInt();
			objItem.m_strQRS_TOTAL_CHR = m_objViewer.m_txtQRS_TOTAL_CHR.Text.Trim();
			objItem.m_intHEARTRATE_AVERAGE_INT = Convert.ToInt16(m_objViewer.m_txtHEARTRATE_AVERAGE_INT.Text.Trim());
			objItem.m_intHEARTRATE_MAX_INT = Convert.ToInt16(m_objViewer.m_txtHEARTRATE_MAX_INT.Text.Trim());
			objItem.m_intHEARTRATE_MIN_INT = Convert.ToInt16(m_objViewer.m_txtHEARTRATE_MIN_INT.Text.Trim());
			objItem.m_strHEARTRATE_MAX_DAT = m_objViewer.m_dtpHEARTRATE_MAX_DAT.Value.ToString("yyyy-MM-dd HH:mm:ss");
			objItem.m_strHEARTRATE_MIN_DAT = m_objViewer.m_dtpHEARTRATE_MIN_DAT.Value.ToString("yyyy-MM-dd HH:mm:ss");
//			objItem.m_intHEARTRATE_BASE_INT=m_objViewer.m_cmbHEARTRATE_BASE_INT.SelectedIndex;
			objItem.m_strHEARTRATE_BASE_VCHR=m_objViewer.m_cmbHEARTRATE_BASE_INT.Text.Trim();
//			objItem.m_intHEARTRATE_BASE_INT = m_intGetHeartRateBaseInt();
            objItem.m_strREPORT_ID_CHR = this.m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag.ToString() ;
            objItem.m_strDEPT_ID_CHR = this.m_objViewer.m_txtDEPT_NAME_VCHR.Tag.ToString(); ;
            //objItem.m_strDEPT_ID_CHR=this.m_objViewer.m_txtDEPT_NAME_VCHR.m_StrDeptID;
			if(m_objViewer.m_cheIsSpical.Checked)
			{
				objItem.m_intIsSpicalPatient=1;
			}
			else
			{
				objItem.m_intIsSpicalPatient=0;
			}

			string strReportID = "";
			long lngRes=m_objManage.m_lngDoAddNewDCardiogramReport(out strReportID,objItem);
			if(lngRes>0)
			{
				MessageBox.Show("添加成功");
			}
			else
			{
				MessageBox.Show("添加失败");
			}

			objItem.m_strREPORT_ID_CHR = strReportID;
			m_objItem = objItem;
			m_objControllerRISCManage.Set_GUI_Apperance(objfrmCardiogramReportManage);
			m_objControllerRISCManage.m_mthGetDCardiogramReportArr();
			objfrmCardiogramReportManage.m_tbcMain.SelectedIndex=1;
		}
		#endregion

		#region 打印报告单
		public void m_mthPrintReport(frmDnmCardiogramReport infrmDnmCardiogramReport)
		{
			try
			{
				infrmDnmCardiogramReport.m_printPrevDlg.Document = infrmDnmCardiogramReport.m_printDoc;
                ////clsController_RISCardiogramReport cls = new clsController_RISCardiogramReport(null);
                //cls.m_mthSetPrintPreviewDialogZoom10(infrmDnmCardiogramReport.m_printPrevDlg);
                //cls = null;
                m_mthSetPrintPreviewDialogZoom10(infrmDnmCardiogramReport.m_printPrevDlg);
				infrmDnmCardiogramReport.m_printPrevDlg.ShowDialog();
			}
			catch
			{
				MessageBox.Show(infrmDnmCardiogramReport,"打印机故障！","iCare",MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
		}

		public void m_mthPrintPage(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
		{
			clsPrint_RISDCardiogramReport objRISDCReport = new clsPrint_RISDCardiogramReport();
			objRISDCReport.objReportVO = m_objItem;
			objRISDCReport.m_mthInitPrintTool(null);
			objRISDCReport.m_mthPrintPage(p_objPrintArg);
		}

        #region 将printPreviewDialog最大化,并将比例设置为100%
        /// <summary>
        /// 将printPreviewDialog最大化,并将比例设置为100%
        /// </summary>
        /// <param name="p_printPreviewDialog"></param>
        public void m_mthSetPrintPreviewDialogZoom10(PrintPreviewDialog p_printPreviewDialog)
        {
            #region 将printPreviewDialog最大化,并将比例设置为100%
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

		#region 模板 肖华锋 2008.9.24
        private clsTemplateClient m_objTemplate;
        //初始化模板
        //public void m_mthInitializeTemplate(frmCardiogramReport infrmCardiogramReport)
        public void m_mthInitializeTemplate()
        {
            List<Control> list = m_lstGetRegisterControls();
            clsCommonTools common = new clsCommonTools();
            common.m_mthRegisterTemplate(m_objViewer, list);

            objController_Security = new com.digitalwave.iCare.gui.Security.clsController_Security();

            m_objTemplate = new clsTemplateClient(m_objViewer, objController_Security.objGetCurrentLoginEmployee().strEmpID, objController_Security.objGetCurrentLoginDepartment().strDeptID);
        }
        //生成模板
        public void m_mthCreateTemplate()
        {
            m_objTemplate.m_mthCreateTemplate();
        }
        public List<System.Windows.Forms.Control> m_lstGetRegisterControls()
        {
            List<Control> list = new List<Control>();
            // frmCardiogramReport frm = objfrmCardiogramReportManage as frmCardiogramReport;
            list.Add(m_objViewer.m_txtSUMMARY1_VCHR);
            list.Add(m_objViewer.m_txtSUMMARY2_VCHR);
            //list.Add(frm.);
            //list.Add(frm.m_txtImpressDiagnose);
            return list;
        }
        public void m_mthEditTemplate()
        {
            m_objTemplate.m_mthManageTemplate();
        }
        //#region 初始化模板
        //public void m_mthInitializeTemplate(frmDnmCardiogramReport infrmDnmCardiogramReport)
        //{
        //    objController_Security = new com.digitalwave.iCare.gui.Security.clsController_Security();
        //    m_objTemplateUtility = new com.digitalwave.iCare.gui.TemplateUtility.clsHRPTemplateUtility(objController_Security.objGetCurrentLoginPrincipal(), objController_Security.objGetCurrentLoginEmployee(), objController_Security.objGetCurrentLoginDepartment(), infrmDnmCardiogramReport, true);
        //}
        //#endregion

        //#region 生成模板
        //public void m_mthCreateTemplate(frmDnmCardiogramReport infrmDnmCardiogramReport)
        //{
        //    com.digitalwave.iCare.middletier.DepartmentSvc.clsDeptSvc objDeptSvc = (com.digitalwave.iCare.middletier.DepartmentSvc.clsDeptSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.DepartmentSvc.clsDeptSvc));
        //    com.digitalwave.iCare.ValueObject.clsDepartmentVO[] objDeptList = null;
        //    long lngRes = objDeptSvc.m_lngGetDeptListByFussCriteria(objController_Security.objGetCurrentLoginPrincipal(), 1, "", "", "DEPTID_CHR", true, out objDeptList);

        //    com.digitalwave.iCare.gui.TemplateUtility.clsController_NewTemplate objController_NewTemplate = new clsController_NewTemplate(objController_Security.objGetCurrentLoginPrincipal());
        //    objController_NewTemplate.m_ObjDepartmentArr = objDeptList;
        //    objController_NewTemplate.m_ObjCurrDepartment = objController_Security.objGetCurrentLoginDepartment();
        //    objController_NewTemplate.m_ObjOperator = objController_Security.objGetCurrentLoginEmployee();
        //    objController_NewTemplate.m_mthShowNewTemplateDialog(infrmDnmCardiogramReport, infrmDnmCardiogramReport.Name, false);
        //}
        //#endregion
		#endregion

		private int m_intGetGraphTypeInt()
		{
			if(m_objViewer.m_rdbGRAPH_TYPE_INT0.Checked)
				return 0;
			else if(m_objViewer.m_rdbGRAPH_TYPE_INT1.Checked)
				return 1;
			else if(m_objViewer.m_rdbGRAPH_TYPE_INT2.Checked)
				return 2;
			else if(m_objViewer.m_rdbGRAPH_TYPE_INT3.Checked)
				return 3;

			return -1;
		}

		private int m_intGetHeartRateBaseInt()
		{
			if(m_objViewer.m_rdbHEARTRATE_BASE_INT0.Checked)
				return 0;
			else if(m_objViewer.m_rdbHEARTRATE_BASE_INT1.Checked)
				return 1;
			else if(m_objViewer.m_rdbHEARTRATE_BASE_INT2.Checked)
				return 2;
			else if(m_objViewer.m_rdbHEARTRATE_BASE_INT3.Checked)
				return 3;

			return -1;
		}

		#region 修改
		/// <summary>
		/// 修改
		/// </summary>
		public void m_mthDoModify()
		{
			clsRIS_DCardiogramReport_VO objItem = new clsRIS_DCardiogramReport_VO();
			objItem.m_strREPORT_ID_CHR =(string)m_objViewer.m_txtREPORT_NO_CHR.Tag;
			objItem.m_strMODIFY_DAT = "";
			objItem.m_strREPORT_NO_CHR = m_objViewer.m_txtREPORT_NO_CHR.Text.Trim();
			if(m_objViewer.m_txtPATIENT_NAME_VCHR.Tag != null)
				objItem.m_strPATIENT_ID_CHR = m_objViewer.m_txtPATIENT_NAME_VCHR.Tag.ToString().Trim();
			else
				objItem.m_strPATIENT_ID_CHR = "";
			objItem.m_strPATIENT_NO_CHR = m_objViewer.m_txtPATIENT_NO_CHR.Text.Trim();
			objItem.m_strINPATIENT_NO_CHR = m_objViewer.m_txtINPATIENT_NO_CHR.Text.Trim();
			objItem.m_strPATIENT_NAME_VCHR = m_objViewer.m_txtPATIENT_NAME_VCHR.Text.Trim();
			objItem.m_strSEX_CHR = m_objViewer.m_cboSEX_CHR.Text.Trim();
//			objItem.m_strAGE_FLT = m_objViewer.m_txtAGE_FLT.Text.Trim()+m_objViewer.m_cmbAge.Text;
			string strSubAge="";
			string strSubAge2="";
			if(m_objViewer.m_txtSubAGE_FLT.Text.Trim()!=""&&m_objViewer.m_txtSubAGE_FLT.Text.Trim()!="0")
			{
				strSubAge= m_objViewer.m_labSubAge.Text;
				strSubAge2=m_objViewer.m_txtSubAGE_FLT.Text;
			}
			objItem.m_strAGE_FLT = m_objViewer.m_txtAGE_FLT.Text.Trim()+m_objViewer.m_cmbAge.Text.Trim()+" "+strSubAge2+strSubAge;
			objItem.m_strREPORT_DAT = m_objViewer.m_dtpREPORT_DAT.Value.ToString("yyyy-MM-dd HH:mm:ss");
            if (m_objViewer.m_txtDEPT_NAME_VCHR.Tag != null)
                objItem.m_strDEPT_ID_CHR = m_objViewer.m_txtDEPT_NAME_VCHR.Tag.ToString().Trim();
            else
                objItem.m_strDEPT_ID_CHR = "";
            objItem.m_strDEPT_NAME_VCHR = m_objViewer.m_txtDEPT_NAME_VCHR.txtValuse.Trim();
            //objItem.m_strDEPT_NAME_VCHR = m_objViewer.m_txtDEPT_NAME_VCHR.Text.Trim();
			objItem.m_intIS_INPATIENT_INT = -1;
			if(m_objViewer.m_txtBED_NO_CHR.Tag != null)
				objItem.m_strBED_ID_CHR = m_objViewer.m_txtBED_NO_CHR.Tag.ToString().Trim();
			else
				objItem.m_strBED_ID_CHR = "";
			objItem.m_strBED_NO_CHR = m_objViewer.m_txtBED_NO_CHR.Text.Trim();
			//objItem.m_strCLINICAL_DIAGNOSE_VCHR = m_objViewer.m_txtCLINICAL_DIAGNOSE_VCHR.Text.Trim();
			//objItem.m_strSUMMARY1_VCHR = m_objViewer.m_txtSUMMARY1_VCHR.Text.Trim();
			//objItem.m_strSUMMARY2_VCHR = m_objViewer.m_txtSUMMARY2_VCHR.Text.Trim();

            if (m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag != null)
                objItem.m_strREPORTOR_ID_CHR = m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag.ToString().Trim();
            else
                objItem.m_strREPORTOR_ID_CHR = "";
            objItem.m_strREPORTOR_ID_CHR = m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag.ToString();

			objItem.m_strREPORTOR_NAME_VCHR = m_objViewer.m_txtREPORTOR_NAME_VCHR.txtValuse.Trim();
			objItem.m_intSTATUS_INT = 1;
			objItem.m_strOPERATOR_ID_CHR = m_objViewer.LoginInfo.m_strEmpID.Trim();
			objItem.m_strCHECKFROM_DAT = m_objViewer.m_dtpCHECKFROM_DAT.Value.ToString("yyyy-MM-dd HH:mm:ss");
			objItem.m_strCHECKTO_DAT = m_objViewer.m_dtpCHECKTO_DAT.Value.ToString("yyyy-MM-dd HH:mm:ss");
			//objItem.m_strCHECK_CHANNELS_VCHR = m_objViewer.m_txtCHECK_CHANNELS_VCHR.Text.Trim();

			objItem.m_strCLINICAL_DIAGNOSE_VCHR = m_objViewer.m_txtCLINICAL_DIAGNOSE_VCHR.Text.Trim();
			objItem.m_strSUMMARY1_VCHR = m_objViewer.m_txtSUMMARY1_VCHR.Text;//.Trim();
			objItem.m_strSUMMARY2_VCHR = m_objViewer.m_txtSUMMARY2_VCHR.Text;//.Trim();
			objItem.m_strCHECK_CHANNELS_VCHR = m_objViewer.m_txtCHECK_CHANNELS_VCHR.Text.Trim();

			objItem.m_strCLINICAL_DIAGNOSE_XML_VCHR = m_objViewer.m_txtCLINICAL_DIAGNOSE_VCHR.m_strGetXmlText().Trim();
			objItem.m_strSUMMARY1_XML_VCHR = m_objViewer.m_txtSUMMARY1_VCHR.m_strGetXmlText().Trim();
			objItem.m_strSUMMARY2_XML_VCHR = m_objViewer.m_txtSUMMARY2_VCHR.m_strGetXmlText().Trim();
			objItem.m_strCHECK_CHANNELS_XML_VCHR = m_objViewer.m_txtCHECK_CHANNELS_VCHR.m_strGetXmlText().Trim();

			objItem.m_intGRAPH_TYPE_INT = m_intGetGraphTypeInt();
			objItem.m_strQRS_TOTAL_CHR = m_objViewer.m_txtQRS_TOTAL_CHR.Text.Trim();
			objItem.m_intHEARTRATE_AVERAGE_INT = Convert.ToInt16(m_objViewer.m_txtHEARTRATE_AVERAGE_INT.Text.Trim());
			objItem.m_intHEARTRATE_MAX_INT = Convert.ToInt16(m_objViewer.m_txtHEARTRATE_MAX_INT.Text.Trim());
			objItem.m_intHEARTRATE_MIN_INT = Convert.ToInt16(m_objViewer.m_txtHEARTRATE_MIN_INT.Text.Trim());
			objItem.m_strHEARTRATE_MAX_DAT = m_objViewer.m_dtpHEARTRATE_MAX_DAT.Value.ToString("yyyy-MM-dd HH:mm:ss");
			objItem.m_strHEARTRATE_MIN_DAT = m_objViewer.m_dtpHEARTRATE_MIN_DAT.Value.ToString("yyyy-MM-dd HH:mm:ss");
//			objItem.m_intHEARTRATE_BASE_INT=m_objViewer.m_cmbHEARTRATE_BASE_INT.SelectedIndex;
			objItem.m_strHEARTRATE_BASE_VCHR=m_objViewer.m_cmbHEARTRATE_BASE_INT.Text.Trim();

            objItem.m_strDEPT_ID_CHR = this.m_objViewer.m_txtDEPT_NAME_VCHR.Tag.ToString();
            //objItem.m_strDEPT_ID_CHR=this.m_objViewer.m_txtDEPT_NAME_VCHR.m_StrDeptID;
			objItem.m_strOPERATOR_ID_CHR = m_objViewer.LoginInfo.m_strEmpID.Trim();
			if(m_objViewer.m_cheIsSpical.Checked)
			{
				objItem.m_intIsSpicalPatient=1;
			}
			else
			{
				objItem.m_intIsSpicalPatient=0;
			}
			long lngRes=m_objManage.m_lngDoModifyDCardiogramReport(objItem);
			if(lngRes>0)
			{
				
                #region 刷新列表
                m_objControllerRISCManage.m_mthQueryReportNew(objfrmCardiogramReportManage);
                #endregion
                MessageBox.Show("修改成功");
			}
			else
			{
				MessageBox.Show("修改失败");
			}
			m_objItem = objItem;
;
			m_objControllerRISCManage.Set_GUI_Apperance(objfrmCardiogramReportManage);
			//m_objControllerRISCManage.m_mthGetDCardiogramReportArr();
			//objfrmCardiogramReportManage.m_tbcMain.SelectedIndex=1;
		
		}
		#endregion

		#region 删除
		/// <summary>
		/// 删除
		/// </summary>
		public void m_mthDoDelete()
		{
			if(MessageBox.Show(m_objViewer,"是否要删除该条记录","注意",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)==DialogResult.No)
			{
				return;
			}
			clsRIS_DCardiogramReport_VO objItem = new clsRIS_DCardiogramReport_VO();
			objItem.m_strREPORT_ID_CHR = m_objItem.m_strREPORT_ID_CHR;
			objItem.m_strMODIFY_DAT = "";
			objItem.m_strREPORT_NO_CHR = m_objViewer.m_txtREPORT_NO_CHR.Text.Trim();
			if(m_objViewer.m_txtPATIENT_NO_CHR.Tag != null)
				objItem.m_strPATIENT_ID_CHR = m_objViewer.m_txtPATIENT_NO_CHR.Tag.ToString().Trim();
			else
				objItem.m_strPATIENT_ID_CHR = "";
			objItem.m_strPATIENT_NO_CHR = m_objViewer.m_txtPATIENT_NO_CHR.Text.Trim();
			objItem.m_strINPATIENT_NO_CHR = m_objViewer.m_txtINPATIENT_NO_CHR.Text.Trim();
			objItem.m_strPATIENT_NAME_VCHR = m_objViewer.m_txtPATIENT_NAME_VCHR.Text.Trim();
			objItem.m_strSEX_CHR = m_objViewer.m_cboSEX_CHR.Text.Trim();
			objItem.m_strAGE_FLT = m_objViewer.m_txtAGE_FLT.Text.Trim();
			objItem.m_strREPORT_DAT = m_objViewer.m_dtpREPORT_DAT.Value.ToString("yyyy-MM-dd HH:mm:ss");
			if(m_objViewer.m_txtDEPT_NAME_VCHR.Tag != null)
				objItem.m_strDEPT_ID_CHR = m_objViewer.m_txtDEPT_NAME_VCHR.Tag.ToString().Trim();
			else
				objItem.m_strDEPT_ID_CHR = "";
			objItem.m_strDEPT_NAME_VCHR = m_objViewer.m_txtDEPT_NAME_VCHR.txtValuse.Trim();
			objItem.m_intIS_INPATIENT_INT = -1;
			if(m_objViewer.m_txtBED_NO_CHR.Tag != null)
				objItem.m_strBED_ID_CHR = m_objViewer.m_txtBED_NO_CHR.Tag.ToString().Trim();
			else
				objItem.m_strBED_ID_CHR = "";
			objItem.m_strBED_NO_CHR = m_objViewer.m_txtBED_NO_CHR.Text.Trim();
			//objItem.m_strCLINICAL_DIAGNOSE_VCHR = m_objViewer.m_txtCLINICAL_DIAGNOSE_VCHR.Text.Trim();
			//objItem.m_strSUMMARY1_VCHR = m_objViewer.m_txtSUMMARY1_VCHR.Text.Trim();
			//objItem.m_strSUMMARY2_VCHR = m_objViewer.m_txtSUMMARY2_VCHR.Text.Trim();
			if(m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag != null)
				objItem.m_strREPORTOR_ID_CHR = m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag.ToString().Trim();
			else
				objItem.m_strREPORTOR_ID_CHR = "";

			objItem.m_strREPORTOR_NAME_VCHR = m_objViewer.m_txtREPORTOR_NAME_VCHR.txtValuse.Trim();
			objItem.m_intSTATUS_INT = 0;
			objItem.m_strOPERATOR_ID_CHR = m_objViewer.LoginInfo.m_strEmpID.Trim();
			objItem.m_strCHECKFROM_DAT = m_objViewer.m_dtpCHECKFROM_DAT.Value.ToString("yyyy-MM-dd HH:mm:ss");
			objItem.m_strCHECKTO_DAT = m_objViewer.m_dtpCHECKTO_DAT.Value.ToString("yyyy-MM-dd HH:mm:ss");
			//objItem.m_strCHECK_CHANNELS_VCHR = m_objViewer.m_txtCHECK_CHANNELS_VCHR.Text.Trim();

			objItem.m_strCLINICAL_DIAGNOSE_VCHR = m_objViewer.m_txtCLINICAL_DIAGNOSE_VCHR.Text.Trim();
			objItem.m_strSUMMARY1_VCHR = m_objViewer.m_txtSUMMARY1_VCHR.Text.Trim();
			objItem.m_strSUMMARY2_VCHR = m_objViewer.m_txtSUMMARY2_VCHR.Text.Trim();
			objItem.m_strCHECK_CHANNELS_VCHR = m_objViewer.m_txtCHECK_CHANNELS_VCHR.Text.Trim();

			objItem.m_strCLINICAL_DIAGNOSE_XML_VCHR = m_objViewer.m_txtCLINICAL_DIAGNOSE_VCHR.m_strGetXmlText().Trim();
			objItem.m_strSUMMARY1_XML_VCHR = m_objViewer.m_txtSUMMARY1_VCHR.m_strGetXmlText().Trim();
			objItem.m_strSUMMARY2_XML_VCHR = m_objViewer.m_txtSUMMARY2_VCHR.m_strGetXmlText().Trim();
			objItem.m_strCHECK_CHANNELS_XML_VCHR = m_objViewer.m_txtCHECK_CHANNELS_VCHR.m_strGetXmlText().Trim();

			objItem.m_intGRAPH_TYPE_INT = m_intGetGraphTypeInt();
			objItem.m_strQRS_TOTAL_CHR = m_objViewer.m_txtQRS_TOTAL_CHR.Text.Trim();
			objItem.m_intHEARTRATE_AVERAGE_INT = Convert.ToInt16(m_objViewer.m_txtHEARTRATE_AVERAGE_INT.Text.Trim());
			objItem.m_intHEARTRATE_MAX_INT = Convert.ToInt16(m_objViewer.m_txtHEARTRATE_MAX_INT.Text.Trim());
			objItem.m_intHEARTRATE_MIN_INT = Convert.ToInt16(m_objViewer.m_txtHEARTRATE_MIN_INT.Text.Trim());
			objItem.m_strHEARTRATE_MAX_DAT = m_objViewer.m_dtpHEARTRATE_MAX_DAT.Value.ToString("yyyy-MM-dd HH:mm:ss");
			objItem.m_strHEARTRATE_MIN_DAT = m_objViewer.m_dtpHEARTRATE_MIN_DAT.Value.ToString("yyyy-MM-dd HH:mm:ss");
			objItem.m_intHEARTRATE_BASE_INT=m_objViewer.m_cmbHEARTRATE_BASE_INT.SelectedIndex;
//			objItem.m_intHEARTRATE_BASE_INT = m_intGetHeartRateBaseInt();
			objItem.m_strOPERATOR_ID_CHR = m_objViewer.LoginInfo.m_strEmpID.Trim();

			long lngRes=m_objManage.m_lngDoDeleteDCardiogramReport(objItem);
			if(lngRes>0)
			{
				MessageBox.Show("删除成功");
                this.m_objViewer.m_cmdAddNew.Enabled = true;
			}
			else
			{
				MessageBox.Show("删除失败");
			}
			m_objItem = null;

			m_mthClear();

			m_objControllerRISCManage.Set_GUI_Apperance(objfrmCardiogramReportManage);
			m_objControllerRISCManage.m_mthGetDCardiogramReportArr();
			objfrmCardiogramReportManage.m_tbcMain.SelectedIndex=1;
			m_objViewer.m_cmdPrint.Enabled=false;
		
		}
		#endregion
		
		#region 清空
		/// <summary>
		/// 清空
		/// </summary>
		public void m_mthClear()
		{
            m_objViewer.m_cmdDelete.Enabled=false;
			m_objViewer.m_cmdPrint.Enabled=false;
			m_objViewer.m_txtREPORT_NO_CHR.Clear();
			m_objViewer.m_txtPATIENT_NO_CHR.Tag = null;
			m_objViewer.m_txtPATIENT_NO_CHR.Clear();
			m_objViewer.m_txtINPATIENT_NO_CHR.Clear();
			m_objViewer.m_txtPATIENT_NAME_VCHR.Clear();
			m_objViewer.m_cboSEX_CHR.Text = "";
			m_objViewer.m_txtAGE_FLT.Clear();
			m_objViewer.m_dtpREPORT_DAT.Value = DateTime.Now;
			m_objViewer.m_txtDEPT_NAME_VCHR.Tag = null;
			m_objViewer.m_txtDEPT_NAME_VCHR.txtValuse="";
			
			m_objViewer.m_txtBED_NO_CHR.Tag = null;
			m_objViewer.m_txtBED_NO_CHR.Clear();
			m_objViewer.m_txtCLINICAL_DIAGNOSE_VCHR.Clear();
			m_objViewer.m_txtSUMMARY1_VCHR.m_mthClearText();
			m_objViewer.m_txtSUMMARY2_VCHR.m_mthClearText();
			m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag = null;
			m_objViewer.m_txtREPORTOR_NAME_VCHR.Text = "";

			m_objViewer.m_dtpCHECKFROM_DAT.Value = DateTime.Now;
			m_objViewer.m_dtpCHECKTO_DAT.Value = DateTime.Now;
			m_objViewer.m_txtCHECK_CHANNELS_VCHR.Text="CM5、CM1、CMF";
			m_objViewer.m_rdbGRAPH_TYPE_INT0.Checked = true;
			m_objViewer.m_txtQRS_TOTAL_CHR.Clear();
			m_objViewer.m_txtHEARTRATE_AVERAGE_INT.Clear();
			m_objViewer.m_txtHEARTRATE_MAX_INT.Clear();
			m_objViewer.m_txtHEARTRATE_MIN_INT.Clear();
			m_objViewer.m_dtpHEARTRATE_MAX_DAT.Value = DateTime.Now;
			m_objViewer.m_dtpHEARTRATE_MIN_DAT.Value = DateTime.Now;
			m_objViewer.m_rdbHEARTRATE_BASE_INT0.Checked = true;

			m_objItem = null;
			m_objViewer.m_txtREPORT_NO_CHR.Select();
			m_objViewer.m_cheIsSpical.Checked=false;
		}
		#endregion

		public void m_mthShowBetweenTime()
		{
			TimeSpan tspTemp = m_objViewer.m_dtpCHECKTO_DAT.Value - m_objViewer.m_dtpCHECKFROM_DAT.Value;
			if(tspTemp.TotalMinutes <=0)
			{
				m_objViewer.m_txtTotalHour.Text = "无效时间";
				return;
			}
			int intTotalMinutes = Convert.ToInt32(tspTemp.TotalMinutes);
			int intHours = intTotalMinutes / 60;
			int intMinutes = intTotalMinutes - intHours * 60;
			string strMinutes = "";
			if(intMinutes == 0)
			{
				strMinutes = "";
			}
			else if(intMinutes < 10 && intMinutes > 0)
			{
				strMinutes = ":0" + intMinutes.ToString();
			}
			else
			{
				strMinutes = ":" + intMinutes.ToString();
			}
			m_objViewer.m_txtTotalHour.Text = intHours.ToString() + strMinutes;

		}

		#region 校验输入值
		/// <summary>
		/// 校验输入值
		/// </summary>
		/// <returns></returns>
		private bool m_bolCheckValuePass()
		{
			bool bolReturn = true;

			if(m_objViewer.m_txtREPORT_NO_CHR.Text.Trim() == "")
			{
				MessageBox.Show("动态号不能为空!");
//				m_ephHandler.m_mthAddControl(m_objViewer.m_txtREPORT_NO_CHR);
				m_objViewer.m_txtREPORT_NO_CHR.Select();
				return false;
			}

			if(m_objViewer.m_txtPATIENT_NAME_VCHR.Text.Trim() == "")
			{
				MessageBox.Show("病人姓名不能为空!");
//				m_ephHandler.m_mthAddControl(m_objViewer.m_txtPATIENT_NAME_VCHR);
				m_objViewer.m_txtPATIENT_NAME_VCHR.Select();
				return false;
			}

			if(m_objViewer.m_txtAGE_FLT.Text.Trim() == "")
			{
				MessageBox.Show("病人年龄不能为空!");
//				m_ephHandler.m_mthAddControl(m_objViewer.m_txtAGE_FLT);
				m_objViewer.m_txtAGE_FLT.Select();
				return false;
			}

			if(m_objViewer.m_cmbAge.SelectedIndex <0)
			{
				MessageBox.Show("请选择病人年龄单位!");
				//				m_ephHandler.m_mthAddControl(m_objViewer.m_txtAGE_FLT);
				m_objViewer.m_cmbAge.Select();
				return false;
			}
			if(m_objViewer.m_txtDEPT_NAME_VCHR.txtValuse == "")
			{
				MessageBox.Show("科室不能为空!");
				m_objViewer.m_txtDEPT_NAME_VCHR.Select();
                m_objViewer.m_txtDEPT_NAME_VCHR.Focus();
				return false;
			}
            else if (m_objViewer.m_txtDEPT_NAME_VCHR.Tag == null)
            {
                MessageBox.Show("请确认输入的是医院已设立的科室!");
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
                    MessageBox.Show("请确认输入的是医院已设立的科室!");
                    m_objViewer.m_txtDEPT_NAME_VCHR.Select();
                    m_objViewer.m_txtDEPT_NAME_VCHR.Focus();
                    return false;
                }
            }
            if (m_objViewer.m_txtREPORTOR_NAME_VCHR.txtValuse == "")
            {
                MessageBox.Show("报告医师不能为空!");
                m_objViewer.m_txtREPORTOR_NAME_VCHR.Select();
                m_objViewer.m_txtREPORTOR_NAME_VCHR.Focus();
                return false;
            }
            else if (m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag==null)
            {
                MessageBox.Show("请确认输入的是在院医师!");
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
                    MessageBox.Show("请确认输入的是在院医师!");
                    m_objViewer.m_txtREPORTOR_NAME_VCHR.Select();
                    m_objViewer.m_txtREPORTOR_NAME_VCHR.Focus();
                    return false;
                }
            }

			if(m_objViewer.m_txtCLINICAL_DIAGNOSE_VCHR.Text.Trim() == "")
			{
				//m_ephHandler.m_mthAddControl(m_objViewer.m_txtCLINICAL_DIAGNOSE_VCHR);
				//bolReturn = false;
			}

			if(m_objViewer.m_txtSUMMARY1_VCHR.Text.Trim() == "")
			{
				//m_ephHandler.m_mthAddControl(m_objViewer.m_txtSUMMARY1_VCHR);
				//bolReturn = false;
			}

			if(m_objViewer.m_txtSUMMARY2_VCHR.Text.Trim() == "")
			{
				//m_ephHandler.m_mthAddControl(m_objViewer.m_txtSUMMARY2_VCHR);
				//bolReturn = false;
			}


			if(m_objViewer.m_txtCHECK_CHANNELS_VCHR.Text.Trim() == "")
			{
				//m_ephHandler.m_mthAddControl(m_objViewer.m_txtCHECK_CHANNELS_VCHR);
				//bolReturn = false;
			}

			if(m_objViewer.m_txtQRS_TOTAL_CHR.Text.Trim() == "")
			{
				m_ephHandler.m_mthAddControl(m_objViewer.m_txtQRS_TOTAL_CHR);
				bolReturn = false;
			}

			if(m_objViewer.m_txtHEARTRATE_AVERAGE_INT.Text.Trim() == "")
			{
				m_ephHandler.m_mthAddControl(m_objViewer.m_txtHEARTRATE_AVERAGE_INT);
				bolReturn = false;
			}

			if(m_objViewer.m_txtHEARTRATE_MAX_INT.Text.Trim() == "")
			{
				m_ephHandler.m_mthAddControl(m_objViewer.m_txtHEARTRATE_MAX_INT);
				bolReturn = false;
			}

			if(m_objViewer.m_txtHEARTRATE_MIN_INT.Text.Trim() == "")
			{
				m_ephHandler.m_mthAddControl(m_objViewer.m_txtHEARTRATE_MIN_INT);
				bolReturn = false;
			}

			if(!bolReturn)
			{
				m_ephHandler.m_mthShowControlsErrorProvider();
				m_ephHandler.m_mthClearControl();
			}
			return bolReturn;
		}
		#endregion

		#region 获取病人资料
		/// <summary>
		/// 获取病人资料
		/// </summary>
		public void m_lngGetPat()
		{
			DataTable bt=new DataTable();
			long lngRes=m_objManage.m_lngGetPat(this.m_objViewer.carID.Text.Trim(),out bt);
			if(lngRes<=0)
			{
				MessageBox.Show("获取病人信息出错！","系统提示");
				return;
			}
			if(bt.Rows.Count==0)
			{
				MessageBox.Show("此卡号还没有登记！","提示");
				//this.m_objViewer.carID.Focus();
				this.m_objViewer.m_txtREPORT_NO_CHR.Focus();
				return;
			}
			else
			{
				this.m_objViewer.m_txtINPATIENT_NO_CHR.Text=bt.Rows[0]["inpatientid_chr"].ToString();
				this.m_objViewer.m_txtPATIENT_NAME_VCHR.Text=bt.Rows[0]["lastname_vchr"].ToString();
				this.m_objViewer.m_txtPATIENT_NAME_VCHR.Tag=bt.Rows[0]["patientid_chr"].ToString();
				this.m_objViewer.m_cboSEX_CHR.Text=bt.Rows[0]["sex_chr"].ToString();
				this.m_objViewer.m_txtREPORT_NO_CHR.Focus();
				if(bt.Rows[0]["birth_dat"].ToString()!="")
				{
					DateTime birth=DateTime.Parse(bt.Rows[0]["birth_dat"].ToString());
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
		#endregion

        /// <summary>
        /// 根据住院号获取病人信息
        /// </summary>
        public void m_lngGetPatByInPatientID()
        {
            DataTable dtResult = new DataTable();
            long lngRes = m_objManage.m_lngGetPatByInPatientID(this.m_objViewer.m_txtINPATIENT_NO_CHR.Text.Trim(), out dtResult);
            if (lngRes <= 0)
            {
                MessageBox.Show("获取病人信息出错！", "系统提示");
                return;
            }
            if (dtResult.Rows.Count == 0)
            {
                MessageBox.Show("此住院号还没有登记！", "提示");
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

        #region 审核(动态心电图)

        public void m_mthDmnConfirm()
        {
            string strRordID = (string)this.m_objViewer.m_txtREPORT_NO_CHR.Tag;
            long lngRes = m_objManage.m_lngConfigDmnCardiogramReport(strRordID, this.m_objViewer.LoginInfo.m_strEmpID, this.m_objViewer.LoginInfo.m_strEmpName);
            if (lngRes > 0)
            {
                MessageBox.Show("审核成功！", "提示");
                #region 刷新列表
                m_objControllerRISCManage.m_mthQueryReportNew(objfrmCardiogramReportManage);
                #endregion
                this.m_objViewer.m_cmdConfirm.Enabled = false;
                this.m_objViewer.m_cmdAddNew.Enabled = false;
            }
        }
        #endregion
	}
}
