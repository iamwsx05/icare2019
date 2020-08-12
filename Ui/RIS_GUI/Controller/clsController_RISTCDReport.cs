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
	/// 作者：Alex 
	/// 时间：2004-5-27
	/// </summary>
	public class clsController_RISTCDReport : com.digitalwave.GUI_Base.clsController_Base
	{
		public clsController_RISTCDReport(frmRISTCDReport infrmTCDReport)
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			m_objManage = new clsDomainController_RISEEGManage();
		m_objManage1 =new clsDomainController_RISCardiogramManage();
			m_objItem = null;
			m_strOperatorID = "0000001";

			//初始化模板信息
            m_objViewer = infrmTCDReport;
            m_mthInitializeTemplate();
			//m_mthInitializeTemplate(infrmTCDReport);
		}
		clsDomainController_RISCardiogramManage m_objManage1;
        
		clsDomainController_RISEEGManage m_objManage = null;

		public clsRIS_TCD_REPORT_VO m_objItem;

		public string m_strOperatorID;

		public int m_intIs_InPatient = -1;

		public System.Windows.Forms.ListViewItem objlsvItem = null;

		private com.digitalwave.iCare.gui.Security.clsController_Security objController_Security = null;
		private com.digitalwave.iCare.gui.TemplateUtility.clsHRPTemplateUtility m_objTemplateUtility;
		private com.digitalwave.iCare.gui.RIS.clsController_RISEEGManage m_objControllerRISCManage = new  clsController_RISEEGManage();
		private com.digitalwave.iCare.gui.RIS.frmRISEEGReportNamage objfrmTCDReportManage = null;

		#region 设置窗体对象

		private com.digitalwave.iCare.gui.RIS.frmRISTCDReport m_objViewer;

		/// <summary>
		/// 设置窗体对象
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmRISTCDReport)frmMDI_Child_Base_in;
		}

		public void SetParentApperance(com.digitalwave.iCare.gui.RIS.frmRISEEGReportNamage infrmRISTCDReportManage)
		{
			objfrmTCDReportManage = infrmRISTCDReportManage;
			m_objViewer.LoginInfo=infrmRISTCDReportManage.LoginInfo;
		}

		
		#endregion

		#region 设置基本信息
		/// <summary>
		/// 设置基本信息
		/// </summary>
		public void m_mthSetViewerCommInfo()
		{
			clsAIDDICT_VO [] objResultArr = null;
			//			com.digitalwave.iCare.middletier.common.clsCommonInfoSvc objSvc = new com.digitalwave.iCare.middletier.common.clsCommonInfoSvc();
			//			objSvc.m_lngGetAID_DICTArr(null,10,out objResultArr);
			m_objComInfo.m_mthGetAID_DICT_InfoArr((int)enmAID_DICT_Category.sex,out objResultArr);
			for(int i1=0;i1<objResultArr.Length;i1++)
			{
				m_objViewer.m_cboSEX_CHR.Items.Add(objResultArr[i1].m_strDICTNAME_VCHR);
			}
			m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag = "0000174";
		}
		#endregion

		public void m_mthSetReport(clsRIS_TCD_REPORT_VO p_objItem)
		{
			m_objItem = p_objItem;

			if(m_objItem == null)
			{
				m_objViewer.m_cmdDelete.Enabled = false;
				return;
			}

			#region    根据 卡号 检索病人ID       
			
			long lng=-1;
			if(m_objItem.m_strCARD_ID_CHR!="")
			{
				DataTable  tbPat = new DataTable();
				lng=m_objManage1.m_lngGetPat(m_objItem.m_strCARD_ID_CHR,out tbPat);
				if(lng>0)
					m_objViewer.m_txtPATIENT_NAME_VCHR.Tag=tbPat.Rows[0]["PATIENTID_CHR"].ToString();
			}
			# endregion
			m_objViewer.carID.Text = m_objItem.m_strCARD_ID_CHR;
			m_objViewer.m_txtREPORT_NO_CHR.Tag = m_objItem.m_strREPORT_ID_CHR;
			m_objViewer.m_txtREPORT_NO_CHR.Text = m_objItem.m_strREPORT_NO_CHR;
			m_objViewer.m_txtPATIENT_NO_CHR.Tag = m_objItem.m_strPATIENT_ID_CHR;
			m_objViewer.m_txtPATIENT_NO_CHR.Text = m_objItem.m_strPATIENT_NO_CHR;
			m_objViewer.m_txtINPATIENT_NO_CHR.Text = m_objItem.m_strINPATIENT_NO_CHR;
			m_objViewer.m_txtPATIENT_NAME_VCHR.Text = m_objItem.m_strPATIENT_NAME_VCHR;
			m_objViewer.m_cboSEX_CHR.Text = m_objItem.m_strSEX_CHR;
            
//			string strTextAge =m_objItem.m_strAGE_FLT.ToString().Substring(0,m_objItem.m_strAGE_FLT.ToString().Trim().Length-1);//年龄
//			string strCmbAge=m_objItem.m_strAGE_FLT.ToString().Substring(strTextAge.Trim().Length,1);//年龄单位
//			m_objViewer.m_txtAGE_FLT.Text = strTextAge;
//			m_objViewer.m_cmbAge.Text=strCmbAge;
//			m_objViewer.m_txtAGE_FLT.Text = m_objItem.m_strAGE_FLT.ToString();
			this.m_mthAgeChange(m_objItem.m_strAGE_FLT.ToString().Trim());
			m_objViewer.m_dtpCHECK_DAT.Value = Convert.ToDateTime(m_objItem.m_strCHECK_DAT.ToString());
			m_objViewer.m_dtpREPORT_DAT.Value = Convert.ToDateTime(m_objItem.m_strREPORT_DAT.ToString());
			m_objViewer.m_txtDEPT_NAME_VCHR.Tag =Int32.Parse(m_objItem.m_strDEPT_ID_CHR);
			m_objViewer.m_txtDEPT_NAME_VCHR.txtValuse = m_objItem.m_strDEPT_NAME_VCHR;
			m_intIs_InPatient = m_objItem.m_intIS_INPATIENT_INT;
			m_objViewer.m_txtBED_NO_CHR.Tag = m_objItem.m_strBED_ID_CHR;
			m_objViewer.m_txtBED_NO_CHR.Text = m_objItem.m_strBED_NO_CHR;
			m_objViewer.m_txtSUMMARY1_VCHR.m_mthSetNewText(m_objItem.m_strSUMMARY1_VCHR,m_objItem.m_strSUMMARY1_XML_VCHR);
			m_objViewer.m_txtSUMMARY2_VCHR.m_mthSetNewText(m_objItem.m_strSUMMARY2_VCHR,m_objItem.m_strSUMMARY2_XML_VCHR);
			m_objViewer.m_txtDiagnose.m_mthSetNewText(m_objItem.m_strDIAGNOSE_VCHR,m_objItem.m_strDIAGNOSE_XML_VCHR);
			m_objViewer.m_txtCureCircs.m_mthSetNewText(m_objItem.m_strCURE_CIRCS_VCHR,m_objItem.m_strCURE_CIRCS_XML_VCHR);
			m_objViewer.m_txtCTResult.m_mthSetNewText(m_objItem.m_strCT_RESULT_VCHR,m_objItem.m_strCT_RESULT_XML_VCHR);
			m_objViewer.m_txtMRIResult.m_mthSetNewText(m_objItem.m_strMRI_RESULT_VCHR,m_objItem.m_strMRI_RESULT_XML_VCHR);
			m_objViewer.m_txtXRayResult.m_mthSetNewText(m_objItem.m_strX_RAY_RESULT_VCHR,m_objItem.m_strX_RAY_RESULT_XML_VCHR);
			m_objViewer.m_txtEKGResult.m_mthSetNewText(m_objItem.m_strEKG_RESULT_VCHR,m_objItem.m_strEKG_RESULT_XML_VCHR);
			m_objViewer.m_txtBUCResult.m_mthSetNewText(m_objItem.m_strBUS_RESULT_VCHR,m_objItem.m_strBUS_RESULT_XML_VCHR);
			//			m_objViewer.m_txtSUMMARY1_VCHR.m_mthSetNewText(m_objItem.m_strSUMMARY1_VCHR,"</>");
			//			m_objViewer.m_txtSUMMARY2_VCHR.m_mthSetNewText(m_objItem.m_strSUMMARY2_VCHR,"</>");
		
			m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag = Int32.Parse(m_objItem.m_strREPORTOR_ID_CHR);
			m_objViewer.m_txtREPORTOR_NAME_VCHR.txtValuse = m_objItem.m_strREPORTOR_NAME_VCHR;

            if (m_objItem.m_strCONFIRMER_ID_CHR != "") //判断是否已审核
            {
                this.m_objViewer.m_cmdSave.Enabled = false;
                this.m_objViewer.m_cmdConfirm.Enabled = false;

            }
            else
            {
                this.m_objViewer.m_cmdSave.Enabled = true;
                this.m_objViewer.m_cmdConfirm.Enabled = true;
            }
		}
		
		public void m_mthDoSave()
		{
			if(!m_bolCheckValuePass())
				return;
			if(m_objItem == null)
			{
				m_mthDoAddNew();
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
		public void m_mthDoAddNew()
		{

			clsRIS_TCD_REPORT_VO objItem = new clsRIS_TCD_REPORT_VO();
			objItem.m_strREPORT_ID_CHR = "";
			objItem.m_strMODIFY_DAT = DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
			objItem.m_strREPORT_NO_CHR = m_objViewer.m_txtREPORT_NO_CHR.Text.Trim();
			if(m_objViewer.m_txtPATIENT_NAME_VCHR.Tag != null)
				objItem.m_strPATIENT_ID_CHR = m_objViewer.m_txtPATIENT_NAME_VCHR.Tag.ToString().Trim();
			objItem.m_strPATIENT_NO_CHR = m_objViewer.m_txtPATIENT_NO_CHR.Text.Trim();
			objItem.m_strINPATIENT_NO_CHR = m_objViewer.m_txtINPATIENT_NO_CHR.Text.Trim();
			objItem.m_strPATIENT_NAME_VCHR = m_objViewer.m_txtPATIENT_NAME_VCHR.Text.Trim();
			objItem.m_strSEX_CHR = m_objViewer.m_cboSEX_CHR.Text.Trim();
//			objItem.m_strAGE_FLT = m_objViewer.m_txtAGE_FLT.Text.Trim()+m_objViewer.m_cmbAge.Text.Trim();
			objItem.m_strAGE_FLT = m_objViewer.m_cmbAge.Tag.ToString()+m_objViewer.m_txtAGE_FLT.Text.Trim();
			objItem.m_strCHECK_DAT = m_objViewer.m_dtpCHECK_DAT.Value.ToString("yyyy-MM-dd HH:mm:ss");
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

			objItem.m_strSUMMARY1_VCHR = m_objViewer.m_txtSUMMARY1_VCHR.Text;
			objItem.m_strSUMMARY1_XML_VCHR =m_objViewer.m_txtSUMMARY1_VCHR.m_strGetXmlText();
			objItem.m_strSUMMARY2_VCHR = m_objViewer.m_txtSUMMARY2_VCHR.Text;
			objItem.m_strSUMMARY2_XML_VCHR=m_objViewer.m_txtSUMMARY2_VCHR.m_strGetXmlText();
			objItem.m_strDIAGNOSE_VCHR = m_objViewer.m_txtDiagnose.Text;
			objItem.m_strDIAGNOSE_XML_VCHR =m_objViewer.m_txtDiagnose.m_strGetXmlText();
			objItem.m_strCURE_CIRCS_VCHR = m_objViewer.m_txtCureCircs.Text;
			objItem.m_strCURE_CIRCS_XML_VCHR=m_objViewer.m_txtCureCircs.m_strGetXmlText();
			if(m_objViewer.m_txtCTResult.Text=="")
			{
				objItem.m_strCT_RESULT_VCHR = m_objViewer.m_txtCTResult.Text;
				objItem.m_strCT_RESULT_XML_VCHR ="<r><D /><U><UI D=\"\" N=\"\" S=\"1\" M=\"2004-07-13 15:13:10\" C=\"-16777216\" /></U></r>";
			}
			else
			{
				objItem.m_strCT_RESULT_VCHR = m_objViewer.m_txtCTResult.Text;
				objItem.m_strCT_RESULT_XML_VCHR =m_objViewer.m_txtCTResult.m_strGetXmlText();
			}
			if(m_objViewer.m_txtMRIResult.Text=="")
			{
				objItem.m_strMRI_RESULT_VCHR = m_objViewer.m_txtMRIResult.Text;
				objItem.m_strMRI_RESULT_XML_VCHR="<r><D /><U><UI D=\"\" N=\"\" S=\"1\" M=\"2004-07-13 15:13:10\" C=\"-16777216\" /></U></r>";
			}
			else
			{
				objItem.m_strMRI_RESULT_VCHR = m_objViewer.m_txtMRIResult.Text;
				objItem.m_strMRI_RESULT_XML_VCHR=m_objViewer.m_txtMRIResult.m_strGetXmlText();
			}
			if(m_objViewer.m_txtXRayResult.Text=="")
			{
				objItem.m_strX_RAY_RESULT_VCHR = m_objViewer.m_txtXRayResult.Text;
				objItem.m_strX_RAY_RESULT_XML_VCHR ="<r><D /><U><UI D=\"\" N=\"\" S=\"1\" M=\"2004-07-13 15:13:10\" C=\"-16777216\" /></U></r>";
			}
			else
			{
				objItem.m_strX_RAY_RESULT_VCHR = m_objViewer.m_txtXRayResult.Text;
				objItem.m_strX_RAY_RESULT_XML_VCHR =m_objViewer.m_txtXRayResult.m_strGetXmlText();
			}
			if(m_objViewer.m_txtEKGResult.Text=="")
			{
				objItem.m_strEKG_RESULT_VCHR = m_objViewer.m_txtEKGResult.Text;
				objItem.m_strEKG_RESULT_XML_VCHR="<r><D /><U><UI D=\"\" N=\"\" S=\"1\" M=\"2004-07-13 15:13:10\" C=\"-16777216\" /></U></r>";
			}
			else
			{
				objItem.m_strEKG_RESULT_VCHR = m_objViewer.m_txtEKGResult.Text;
				objItem.m_strEKG_RESULT_XML_VCHR=m_objViewer.m_txtEKGResult.m_strGetXmlText();
			}
			if(m_objViewer.m_txtBUCResult.Text=="")
			{
				objItem.m_strBUS_RESULT_VCHR = m_objViewer.m_txtBUCResult.Text.Trim();
				objItem.m_strBUS_RESULT_XML_VCHR ="<r><D /><U><UI D=\"\" N=\"\" S=\"1\" M=\"2004-07-13 15:13:10\" C=\"-16777216\" /></U></r>";
			}
			else
			{
				objItem.m_strBUS_RESULT_VCHR = m_objViewer.m_txtBUCResult.Text;
				objItem.m_strBUS_RESULT_XML_VCHR =m_objViewer.m_txtBUCResult.m_strGetXmlText();
			}
			

			if(m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag != null)
				objItem.m_strREPORTOR_ID_CHR = m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag.ToString().Trim();
			else
				objItem.m_strREPORTOR_ID_CHR = "";
			objItem.m_strREPORTOR_NAME_VCHR = m_objViewer.m_txtREPORTOR_NAME_VCHR.txtValuse.Trim();
            objItem.m_strCONFIRMER_ID_CHR = "";
            objItem.m_strCONFIRMER_NAME_VCHR = "";
            //objItem.m_strCONFIRMER_ID_CHR = m_objViewer.LoginInfo.m_strEmpID.Trim();
            //objItem.m_strCONFIRMER_NAME_VCHR = m_objViewer.LoginInfo.m_strEmpName.Trim();
			objItem.m_intSTATUS_INT = 1;
			objItem.m_strOPERATOR_ID_CHR = m_objViewer.LoginInfo.m_strEmpID.Trim();
			objItem.m_strREPORT_ID_CHR=this.m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag.ToString();
			objItem.m_strDEPT_ID_CHR=this.m_objViewer.m_txtDEPT_NAME_VCHR.Tag.ToString();
			string strReportID = "";
			long lngRes=m_objManage.m_lngDoAddNewTCDmReport(out strReportID,objItem);
			if(lngRes>0)
				MessageBox.Show("添加成功");
			else
				MessageBox.Show("添加失败");

			objItem.m_strREPORT_ID_CHR = strReportID;
			m_objItem = objItem;

            m_objViewer.m_txtREPORT_NO_CHR.Tag = m_objItem.m_strREPORT_ID_CHR;
            m_objControllerRISCManage.Set_GUI_Apperance(objfrmTCDReportManage);
			m_objControllerRISCManage.m_mthGetTCDReportArr();
//            m_objViewer.m_cmdSave.Enabled=false;
		}
		#endregion

		#region 修改
		/// <summary>
		/// 修改
		/// </summary>
		public void m_mthDoModify()
		{

			clsRIS_TCD_REPORT_VO objItem = new clsRIS_TCD_REPORT_VO();
			objItem.m_strREPORT_ID_CHR = m_objItem.m_strREPORT_ID_CHR;
			objItem.m_strMODIFY_DAT = DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
			objItem.m_strREPORT_NO_CHR = m_objViewer.m_txtREPORT_NO_CHR.Text.Trim();
			if(m_objViewer.m_txtPATIENT_NAME_VCHR.Tag != null)
				objItem.m_strPATIENT_ID_CHR = m_objViewer.m_txtPATIENT_NAME_VCHR.Tag.ToString().Trim();
			else
				objItem.m_strPATIENT_ID_CHR = "";
			objItem.m_strPATIENT_NO_CHR = m_objViewer.m_txtPATIENT_NO_CHR.Text.Trim();
			objItem.m_strINPATIENT_NO_CHR = m_objViewer.m_txtINPATIENT_NO_CHR.Text.Trim();
			objItem.m_strPATIENT_NAME_VCHR = m_objViewer.m_txtPATIENT_NAME_VCHR.Text.Trim();
			objItem.m_strSEX_CHR = m_objViewer.m_cboSEX_CHR.Text.Trim();
//			objItem.m_strAGE_FLT =m_objViewer.m_txtAGE_FLT.Text.Trim()+m_objViewer.m_cmbAge.Text.Trim();
			objItem.m_strAGE_FLT = m_objViewer.m_cmbAge.Tag.ToString()+m_objViewer.m_txtAGE_FLT.Text.Trim();
			objItem.m_strCHECK_DAT = m_objViewer.m_dtpCHECK_DAT.Value.ToString("yyyy-MM-dd HH:mm:ss");
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
		
			objItem.m_strSUMMARY1_VCHR = m_objViewer.m_txtSUMMARY1_VCHR.Text;
			objItem.m_strSUMMARY1_XML_VCHR =m_objViewer.m_txtSUMMARY1_VCHR.m_strGetXmlText();
			objItem.m_strSUMMARY2_VCHR = m_objViewer.m_txtSUMMARY2_VCHR.Text;
			objItem.m_strSUMMARY2_XML_VCHR=m_objViewer.m_txtSUMMARY2_VCHR.m_strGetXmlText();
			objItem.m_strDIAGNOSE_VCHR = m_objViewer.m_txtDiagnose.Text;
			objItem.m_strDIAGNOSE_XML_VCHR =m_objViewer.m_txtDiagnose.m_strGetXmlText();
			objItem.m_strCURE_CIRCS_VCHR = m_objViewer.m_txtCureCircs.Text;
			objItem.m_strCURE_CIRCS_XML_VCHR=m_objViewer.m_txtCureCircs.m_strGetXmlText();
			if(m_objViewer.m_txtCTResult.Text=="")
			{
				objItem.m_strCT_RESULT_VCHR = m_objViewer.m_txtCTResult.Text;
				objItem.m_strCT_RESULT_XML_VCHR ="<r><D /><U><UI D=\"\" N=\"\" S=\"1\" M=\"2004-07-13 15:13:10\" C=\"-16777216\" /></U></r>";
			}
			else
			{
                objItem.m_strCT_RESULT_VCHR = m_objViewer.m_txtCTResult.Text;
			    objItem.m_strCT_RESULT_XML_VCHR =m_objViewer.m_txtCTResult.m_strGetXmlText();
			}
			if(m_objViewer.m_txtMRIResult.Text=="")
			{
				objItem.m_strMRI_RESULT_VCHR = m_objViewer.m_txtMRIResult.Text;
				objItem.m_strMRI_RESULT_XML_VCHR="<r><D /><U><UI D=\"\" N=\"\" S=\"1\" M=\"2004-07-13 15:13:10\" C=\"-16777216\" /></U></r>";
			}
			else
			{
				objItem.m_strMRI_RESULT_VCHR = m_objViewer.m_txtMRIResult.Text;
				objItem.m_strMRI_RESULT_XML_VCHR=m_objViewer.m_txtMRIResult.m_strGetXmlText();
			}
			if(m_objViewer.m_txtXRayResult.Text=="")
			{
				objItem.m_strX_RAY_RESULT_VCHR = m_objViewer.m_txtXRayResult.Text;
				objItem.m_strX_RAY_RESULT_XML_VCHR ="<r><D /><U><UI D=\"\" N=\"\" S=\"1\" M=\"2004-07-13 15:13:10\" C=\"-16777216\" /></U></r>";
			}
			else
			{
				objItem.m_strX_RAY_RESULT_VCHR = m_objViewer.m_txtXRayResult.Text;
				objItem.m_strX_RAY_RESULT_XML_VCHR =m_objViewer.m_txtXRayResult.m_strGetXmlText();
			}
			if(m_objViewer.m_txtEKGResult.Text=="")
			{
				objItem.m_strEKG_RESULT_VCHR = m_objViewer.m_txtEKGResult.Text;
				objItem.m_strEKG_RESULT_XML_VCHR="<r><D /><U><UI D=\"\" N=\"\" S=\"1\" M=\"2004-07-13 15:13:10\" C=\"-16777216\" /></U></r>";
			}
			else
			{
				objItem.m_strEKG_RESULT_VCHR = m_objViewer.m_txtEKGResult.Text;
				objItem.m_strEKG_RESULT_XML_VCHR=m_objViewer.m_txtEKGResult.m_strGetXmlText();
			}
			if(m_objViewer.m_txtBUCResult.Text=="")
			{
				objItem.m_strBUS_RESULT_VCHR = m_objViewer.m_txtBUCResult.Text;
				objItem.m_strBUS_RESULT_XML_VCHR ="<r><D /><U><UI D=\"\" N=\"\" S=\"1\" M=\"2004-07-13 15:13:10\" C=\"-16777216\" /></U></r>";
			}
			else
			{
				objItem.m_strBUS_RESULT_VCHR = m_objViewer.m_txtBUCResult.Text;
				objItem.m_strBUS_RESULT_XML_VCHR =m_objViewer.m_txtBUCResult.m_strGetXmlText();
			}
			if(m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag != null)
				objItem.m_strREPORTOR_ID_CHR = m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag.ToString().Trim();
			else
				objItem.m_strREPORTOR_ID_CHR = "";
            objItem.m_strREPORTOR_NAME_VCHR = m_objViewer.m_txtREPORTOR_NAME_VCHR.txtValuse.Trim();
            //objItem.m_strCONFIRMER_NAME_VCHR = m_objViewer.LoginInfo.m_strEmpName.Trim();
            //objItem.m_strCONFIRMER_ID_CHR = m_objViewer.LoginInfo.m_strEmpID.Trim();
            objItem.m_strCONFIRMER_NAME_VCHR = "";
            objItem.m_strCONFIRMER_ID_CHR = "";
            
			objItem.m_intSTATUS_INT = 1;
			objItem.m_strOPERATOR_ID_CHR = m_objViewer.LoginInfo.m_strEmpID.Trim();
//			objItem.m_strREPORT_ID_CHR=this.m_objViewer.m_txtREPORTOR_NAME_VCHR.m_StrEmployeeID;
			objItem.m_strDEPT_ID_CHR=this.m_objViewer.m_txtDEPT_NAME_VCHR.Tag.ToString();
//			MessageBox.Show(objItem.m_strREPORT_ID_CHR);
			long lngRes=m_objManage.m_lngDoModifyTCDReport(objItem);
            if (lngRes > 0)
            {

                new clsController_RISEEGManage().m_mthQueryReportNew(objfrmTCDReportManage);
                MessageBox.Show("修改成功");

            }
            else
                MessageBox.Show("修改失败");
			m_objItem = objItem;
			
			m_objControllerRISCManage.Set_GUI_Apperance(objfrmTCDReportManage);
			//m_objControllerRISCManage.m_mthGetTCDReportArr();
		}
		#endregion

		#region 删除
		/// <summary>
		/// 删除
		/// </summary>
		public void m_mthDoDelete()
		{
			if(m_objItem==null)
			{
			return;
			}
			if(MessageBox.Show("是否要删除记录?","ICare",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)==DialogResult.No)
			{
				return;
			}
			clsRIS_TCD_REPORT_VO objItem = new clsRIS_TCD_REPORT_VO();
			objItem.m_strREPORT_ID_CHR = m_objItem.m_strREPORT_ID_CHR;
			objItem.m_strMODIFY_DAT = DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
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
			objItem.m_strCHECK_DAT = m_objViewer.m_dtpCHECK_DAT.Value.ToString("yyyy-MM-dd HH:mm:ss");
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

			objItem.m_strSUMMARY1_VCHR = m_objViewer.m_txtSUMMARY1_VCHR.Text.Trim();
			objItem.m_strSUMMARY1_XML_VCHR =m_objViewer.m_txtSUMMARY1_VCHR.m_strGetXmlText().Trim();
			objItem.m_strSUMMARY2_VCHR = m_objViewer.m_txtSUMMARY2_VCHR.Text.Trim();
			objItem.m_strSUMMARY2_XML_VCHR=m_objViewer.m_txtSUMMARY2_VCHR.m_strGetXmlText().Trim();

			if(m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag != null)
				objItem.m_strREPORTOR_ID_CHR = m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag.ToString().Trim();
			else
				objItem.m_strREPORTOR_ID_CHR = "";
			objItem.m_strREPORTOR_NAME_VCHR = m_objViewer.m_txtREPORTOR_NAME_VCHR.txtValuse.Trim();
			objItem.m_strCONFIRMER_ID_CHR = m_objViewer.LoginInfo.m_strEmpID.Trim();
			objItem.m_strCONFIRMER_NAME_VCHR = m_objViewer.LoginInfo.m_strEmpName.Trim();
			objItem.m_intSTATUS_INT = 0;
			objItem.m_strOPERATOR_ID_CHR = m_objViewer.LoginInfo.m_strEmpID.Trim();

			long lngRes=m_objManage.m_lngDoDeleteTCDReport(objItem);
			if(lngRes>0)
			{
			//MessageBox.Show("删除成功");
				m_mthClear();
                this.m_objViewer.m_cmdSave.Enabled = true;
			}
				
			else
			{
				MessageBox.Show("删除失败");
			}
					
			m_objControllerRISCManage.Set_GUI_Apperance(objfrmTCDReportManage);
			m_objControllerRISCManage.m_mthGetTCDReportArr();
		}
		#endregion
		
		#region 清空
		/// <summary>
		/// 清空
		/// </summary>
		public void m_mthClear()
		{
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
			m_intIs_InPatient = -1;
			m_objViewer.m_txtBED_NO_CHR.Tag = null;
			m_objViewer.m_txtBED_NO_CHR.Clear();
			m_objViewer.m_txtSUMMARY1_VCHR.m_mthClearText();
			m_objViewer.m_txtSUMMARY2_VCHR.m_mthClearText();
			m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag = null;
			m_objViewer.m_txtREPORTOR_NAME_VCHR.txtValuse = "";
			m_objViewer.m_txtCTResult.Text="";
			m_objViewer.m_txtMRIResult.Text="";
			m_objViewer.m_txtEKGResult.Text="";
			m_objViewer.m_txtBUCResult.Text="";
			m_objViewer.m_txtXRayResult.Text="";
			m_objViewer.m_txtDiagnose.Text="";
			m_objViewer.m_txtCureCircs.Text="";
			this.m_objItem=null;
			this.m_objViewer.m_txtREPORT_NO_CHR.Select();

		}
		#endregion

		#region 审核
		public void m_mthDoConfirm()
		{
            string strRordID = (string)this.m_objViewer.m_txtREPORT_NO_CHR.Tag;
            long lngRes = m_objManage.m_lngConfigTCDReport(strRordID, this.m_objViewer.LoginInfo.m_strEmpID, this.m_objViewer.LoginInfo.m_strEmpName);
            if (lngRes > 0)
            {
                MessageBox.Show("审核成功！", "提示");
                #region 刷新列表

                m_objControllerRISCManage.m_mthQueryReportNew(this.m_objViewer.m_objfrmCardiogramReportManage);
                #endregion
                this.m_objViewer.m_cmdConfirm.Enabled = false;
                this.m_objViewer.m_cmdSave.Enabled = false;
            }
		}
		#endregion

        #region 将printPreviewDialog最大化,并将比例设置为100% 2010.6.30 ADD
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

		#region 打印
		public void m_mthPrintReport(frmRISTCDReport infrmRISTCDReport)
		{
			try
			{
				infrmRISTCDReport.m_printPrevDlg.m_objItem=m_objItem;
				infrmRISTCDReport.m_printPrevDlg.printDoc = infrmRISTCDReport.m_printDoc;
				infrmRISTCDReport.m_printPrevDlg.Document=infrmRISTCDReport.m_printDoc;

                // 2010.6.30
                //clsController_RISCardiogramReport cls = new clsController_RISCardiogramReport(null);
                //cls.m_mthSetPrintPreviewDialogZoom10(infrmRISTCDReport.m_printPrevDlg);
                //cls = null;

                m_mthSetPrintPreviewDialogZoom10(infrmRISTCDReport.m_printPrevDlg);

				infrmRISTCDReport.m_printPrevDlg.ShowDialog();
				
				//				DialogResult objDialogRes = infrmRISTCDReport.m_printDlg.ShowDialog();
				//				if(objDialogRes == DialogResult.OK)
				//				{
				//					infrmRISTCDReport.m_printDoc.Print();
				//				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(infrmRISTCDReport,"打印机故障！"+ex.Message,"iCare",MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
		}

		public void m_mthPrintDocBeginPrint()
		{
		}

		public void m_mthPrintDocPrintPage(System.Drawing.Printing.PrintPageEventArgs p_objPrintPageEventArgs)
		{
			try
			{
				clsPrint_RISTCDReport objPrintReport = new clsPrint_RISTCDReport(2);
				objPrintReport.objReportVO = m_objItem;
				objPrintReport.m_mthInitPrintTool(null);
				objPrintReport.m_mthPrintPage(p_objPrintPageEventArgs);
			}
			catch
			{
			}
		}

		public void m_mthPrintDocEndPrint()
		{
		}
		#endregion

		#region 模板
        private clsTemplateClient m_objTemplate;
		//初始化模板
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
            return list;
        }
        public void m_mthEditTemplate()
        {
            m_objTemplate.m_mthManageTemplate();
        }
      
		#endregion
		

		/// <summary>
		/// 校验输入值
		/// </summary>
		/// <returns></returns>
		private bool m_bolCheckValuePass()
		{
			bool bolReturn = true;

			if(m_objViewer.m_txtREPORT_NO_CHR.Text.Trim() == "")
			{
				MessageBox.Show("TCD号不能为空");
				m_objViewer.m_txtREPORT_NO_CHR.Select();
				return false;
			}

			if(m_objViewer.m_txtPATIENT_NAME_VCHR.Text.Trim() == "")
			{
				MessageBox.Show("患者姓名不能为空");
				m_objViewer.m_txtPATIENT_NAME_VCHR.Select();
				return false;
			}

			if(m_objViewer.m_txtAGE_FLT.Text.Trim() == "")
			{
				MessageBox.Show("年龄不能为空");
				m_objViewer.m_txtAGE_FLT.Select();
				return false;
			}
			if(m_objViewer.m_cmbAge.SelectedIndex<0)
			{
				MessageBox.Show("请选择病人年龄单位!");
				m_objViewer.m_cmbAge.Select();
				return false;
			}
			if(m_objViewer.m_txtDEPT_NAME_VCHR.txtValuse.Trim() == "")
			{
				MessageBox.Show("科室不能为空");
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
            else if (m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag == null)
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

			if(!bolReturn)
			{
				m_ephHandler.m_mthShowControlsErrorProvider();
				m_ephHandler.m_mthClearControl();
			}
			else
			{
				m_ephHandler.m_mthClearControl();
			}
			return bolReturn;
		}

		/// <summary>
		/// 年龄转换
		/// </summary>
		/// <param name="strage"></param>
		private void m_mthAgeChange(string strage)
		{
		int length =strage.Length;
		string  strTextAge="1";
		string strCmbAge="年";
		strCmbAge=strage.Substring(0,1);//年龄单位
			switch(strCmbAge.Trim())
			{
				case "C":
					strCmbAge="岁";
					break;
				case "B":
					strCmbAge="月";
					break;
				case "A":
					strCmbAge="天";
					break;
			}
		strTextAge=strage.Substring(1,length-1);
			m_objViewer.m_txtAGE_FLT.Text = strTextAge;
			m_objViewer.m_cmbAge.Text=strCmbAge;
		}
		#region 获取病人资料
		/// <summary>
		/// 获取病人资料
		/// </summary>
		public void m_lngGetPat()
		{
			DataTable bt=new DataTable();
			clsDomainController_RISCardiogramManage m_objManage=new clsDomainController_RISCardiogramManage();
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
                clsDomainController_RISEEGManage svc = new clsDomainController_RISEEGManage();
                string strResult = "";
                svc.m_strDiagByCardId(this.m_objViewer.carID.Text, out strResult);
                this.m_objViewer.m_txtDiagnose.Text = strResult;

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
					}
					else
					{
						int month=newDate.Month-birth.Month;
						int year=newDate.Year-birth.Year;
						this.m_objViewer.m_txtAGE_FLT.Text=year.ToString();

					}
				}
				else
				{
					this.m_objViewer.m_txtAGE_FLT.Text="";
				}
			}

		}

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
                    }
                    else
                    {
                        int month = newDate.Month - birth.Month;
                        int year = newDate.Year - birth.Year;
                        this.m_objViewer.m_txtAGE_FLT.Text = year.ToString();

                    }
                }
                else
                {
                    this.m_objViewer.m_txtAGE_FLT.Text = "";
                }
            }
        }

		#endregion

        #region 初始化医生列表
        /// <summary>
        /// 初始化医生列表
        /// </summary>
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

        #endregion
	}
}
