using System;
using weCare.Core.Entity;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;
using com.digitalwave.iCare.Template.Client;

namespace com.digitalwave.iCare.gui.RIS
{
	/// <summary>
	/// clsController_RISEEGReport 的摘要说明。
	/// </summary>
	public class clsController_RISEEGReport : com.digitalwave.GUI_Base.clsController_Base
	{
		public clsController_RISEEGReport(frmRISEEGReport infrmEEGReport)
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			m_objManage = new clsDomainController_RISEEGManage();
            m_objManage1=new clsDomainController_RISCardiogramManage();
			m_objItem = null;
			m_strOperatorID = "0000001";

			//初始化模板信息
            m_objViewer = infrmEEGReport;
            m_mthInitializeTemplate();
			//m_mthInitializeTemplate(infrmEEGReport);
		}
        clsDomainController_RISCardiogramManage m_objManage1=null;
		clsDomainController_RISEEGManage m_objManage = null;

		public clsRIS_EEG_REPORT_VO m_objItem;

		public string m_strOperatorID;

		public int m_intIs_InPatient = -1;

		public System.Windows.Forms.ListViewItem objlsvItem = null;

		private com.digitalwave.iCare.gui.Security.clsController_Security objController_Security = null;
		private com.digitalwave.iCare.gui.TemplateUtility.clsHRPTemplateUtility m_objTemplateUtility;
		private com.digitalwave.iCare.gui.RIS.clsController_RISEEGManage m_objControllerRISCManage = new  clsController_RISEEGManage();
		private com.digitalwave.iCare.gui.RIS.frmRISEEGReportNamage objfrmEEGReportManage = null;

		#region 设置窗体对象

		private com.digitalwave.iCare.gui.RIS.frmRISEEGReport m_objViewer;

		/// <summary>
		/// 设置窗体对象
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmRISEEGReport)frmMDI_Child_Base_in;
		}

		public void SetParentApperance(com.digitalwave.iCare.gui.RIS.frmRISEEGReportNamage infrmRISEEGReportManage)
		{
			objfrmEEGReportManage = infrmRISEEGReportManage;
			m_objViewer.LoginInfo=infrmRISEEGReportManage.LoginInfo;
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

		public void m_mthSetReport(clsRIS_EEG_REPORT_VO p_objItem)
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
              m_objViewer.carID.Text=m_objItem.m_strCARD_ID_CHR;
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
			this.m_mthAgeChange(m_objItem.m_strAGE_FLT.ToString().Trim());
//			m_objViewer.m_txtAGE_FLT.Text = m_objItem.m_strAGE_FLT.ToString();
			m_objViewer.m_dtpCHECK_DAT.Value = Convert.ToDateTime(m_objItem.m_strCHECK_DAT.ToString());
			m_objViewer.m_dtpREPORT_DAT.Value = Convert.ToDateTime(m_objItem.m_strREPORT_DAT.ToString());
			m_objViewer.m_txtDEPT_NAME_VCHR.Tag =Int32.Parse(m_objItem.m_strDEPT_ID_CHR);
			m_objViewer.m_txtDEPT_NAME_VCHR.txtValuse = m_objItem.m_strDEPT_NAME_VCHR;
			m_intIs_InPatient = m_objItem.m_intIS_INPATIENT_INT;
			m_objViewer.m_txtBED_NO_CHR.Tag = m_objItem.m_strBED_ID_CHR;
			m_objViewer.m_txtBED_NO_CHR.Text = m_objItem.m_strBED_NO_CHR;
			m_objViewer.m_txtSUMMARY1_VCHR.m_mthSetNewText(m_objItem.m_strSUMMARY1_VCHR,m_objItem.m_strSUMMARY1_XML_VCHR);
			m_objViewer.m_txtSUMMARY2_VCHR.m_mthSetNewText(m_objItem.m_strSUMMARY2_VCHR,m_objItem.m_strSUMMARY2_XML_VCHR);
            m_objViewer.m_cboLEFT_RIGHT.Text=m_objItem.m_strLEFT_RIGHT;
			m_objViewer.m_txtBEFORE_CHECK.Text=m_objItem.m_strBEFORE_CHECK;
			m_objViewer.m_txtBODY_STAT.Text=m_objItem.m_strBODY_STAT;
			m_objViewer.m_txtSENSE_STAT.Text=m_objItem.m_strSENSE_STAT;
			m_objViewer.m_txtDRUG_STAT.Text=m_objItem.m_strDRUG_STAT;
			m_objViewer.m_txtDIAGNOSE_VCHR.m_mthSetNewText(m_objItem.m_strDIAGNOSE_VCHR,m_objItem.m_strDIAGNOSE_XML_VCHR);
			
			//			m_objViewer.m_txtSUMMARY1_VCHR.m_mthSetNewText(m_objItem.m_strSUMMARY1_VCHR,"</>");
			//			m_objViewer.m_txtSUMMARY2_VCHR.m_mthSetNewText(m_objItem.m_strSUMMARY2_VCHR,"</>");
		
			m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag =Int32.Parse(m_objItem.m_strREPORTOR_ID_CHR);
			m_objViewer.m_txtREPORTOR_NAME_VCHR.txtValuse = m_objItem.m_strREPORTOR_NAME_VCHR;

            if (m_objItem.m_strCONFIRMER_ID_CHR != "") //判断是否已经审核
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

			clsRIS_EEG_REPORT_VO objItem = new clsRIS_EEG_REPORT_VO();
			objItem.m_strREPORT_ID_CHR = "";
			objItem.m_strMODIFY_DAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			objItem.m_strREPORT_NO_CHR = m_objViewer.m_txtREPORT_NO_CHR.Text.Trim();
			if(m_objViewer.m_txtPATIENT_NAME_VCHR.Tag != null)
				objItem.m_strPATIENT_ID_CHR = m_objViewer.m_txtPATIENT_NAME_VCHR.Tag.ToString().Trim();
			else
				objItem.m_strPATIENT_ID_CHR = "";
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
			objItem.m_strDEPT_NAME_VCHR = m_objViewer.m_txtDEPT_NAME_VCHR.txtValuse.ToString().Trim();
			objItem.m_intIS_INPATIENT_INT = -1;
			if(m_objViewer.m_txtBED_NO_CHR.Tag != null)
				objItem.m_strBED_ID_CHR = m_objViewer.m_txtBED_NO_CHR.Tag.ToString().Trim();
			else
				objItem.m_strBED_ID_CHR = "";
			objItem.m_strBED_NO_CHR = m_objViewer.m_txtBED_NO_CHR.Text.Trim();

			objItem.m_strSUMMARY1_VCHR = m_objViewer.m_txtSUMMARY1_VCHR.Text;
			objItem.m_strSUMMARY1_XML_VCHR =m_objViewer.m_txtSUMMARY1_VCHR.m_strGetXmlText();
			objItem.m_strSUMMARY2_VCHR = m_objViewer.m_txtSUMMARY2_VCHR.Text;
			objItem.m_strSUMMARY2_XML_VCHR =m_objViewer.m_txtSUMMARY2_VCHR.m_strGetXmlText();
			objItem.m_strDIAGNOSE_VCHR = m_objViewer.m_txtDIAGNOSE_VCHR.Text.Trim();
			objItem.m_strDIAGNOSE_XML_VCHR =m_objViewer.m_txtDIAGNOSE_VCHR.m_strGetXmlText();
			objItem.m_strLEFT_RIGHT = m_objViewer.m_cboLEFT_RIGHT.Text.Trim();
			objItem.m_strBEFORE_CHECK = m_objViewer.m_txtBEFORE_CHECK.Text.Trim();
            objItem.m_strBODY_STAT = m_objViewer.m_txtBODY_STAT.Text.Trim();
            objItem.m_strSENSE_STAT = m_objViewer.m_txtSENSE_STAT.Text.Trim();
			objItem.m_strDRUG_STAT = m_objViewer.m_txtDRUG_STAT.Text.Trim();
			
			
			

			if(m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag != null)
				objItem.m_strREPORTOR_ID_CHR = m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag.ToString().Trim();
			else
				objItem.m_strREPORTOR_ID_CHR = "";
			objItem.m_strREPORTOR_NAME_VCHR = m_objViewer.m_txtREPORTOR_NAME_VCHR.txtValuse.ToString().Trim();
			objItem.m_strCONFIRMER_ID_CHR ="";
			objItem.m_strCONFIRMER_NAME_VCHR ="";
            //objItem.m_strCONFIRMER_ID_CHR = m_objViewer.LoginInfo.m_strEmpID.Trim();
            //objItem.m_strCONFIRMER_NAME_VCHR = m_objViewer.LoginInfo.m_strEmpName.Trim();
			objItem.m_intSTATUS_INT = 1;
//			objItem.m_strOPERATOR_ID_CHR = m_strOperatorID;
			objItem.m_strOPERATOR_ID_CHR = m_objViewer.LoginInfo.m_strEmpID.Trim();
            objItem.m_strREPORTOR_ID_CHR = this.m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag.ToString(); ;
            objItem.m_strDEPT_ID_CHR = this.m_objViewer.m_txtDEPT_NAME_VCHR.Tag.ToString(); ;

			string strReportID = "";
			long lngRes=m_objManage.m_lngDoAddNewEEGmReport(out strReportID,objItem);
			if(lngRes>0)
				MessageBox.Show("添加成功");
			else
				MessageBox.Show("添加失败");

			objItem.m_strREPORT_ID_CHR = strReportID;
			m_objItem = objItem;

            m_objViewer.m_txtREPORT_NO_CHR.Tag = m_objItem.m_strREPORT_ID_CHR;
            m_objControllerRISCManage.Set_GUI_Apperance(objfrmEEGReportManage);
			m_objControllerRISCManage.m_mthGetEEGReportArr();
		}
		#endregion

		#region 修改
		/// <summary>
		/// 修改
		/// </summary>
		public void m_mthDoModify()
		{

			clsRIS_EEG_REPORT_VO objItem = new clsRIS_EEG_REPORT_VO();
			objItem.m_strREPORT_ID_CHR = m_objItem.m_strREPORT_ID_CHR;
			objItem.m_strMODIFY_DAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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
			objItem.m_strDEPT_NAME_VCHR = m_objViewer.m_txtDEPT_NAME_VCHR.txtValuse.ToString().Trim();
			objItem.m_intIS_INPATIENT_INT = -1;
			if(m_objViewer.m_txtBED_NO_CHR.Tag != null)
				objItem.m_strBED_ID_CHR = m_objViewer.m_txtBED_NO_CHR.Tag.ToString().Trim();
			else
				objItem.m_strBED_ID_CHR = "";
			objItem.m_strBED_NO_CHR = m_objViewer.m_txtBED_NO_CHR.Text.Trim();
		
			objItem.m_strSUMMARY1_VCHR = m_objViewer.m_txtSUMMARY1_VCHR.Text;
			objItem.m_strSUMMARY1_XML_VCHR =m_objViewer.m_txtSUMMARY1_VCHR.m_strGetXmlText();
			objItem.m_strSUMMARY2_VCHR = m_objViewer.m_txtSUMMARY2_VCHR.Text;
			objItem.m_strSUMMARY2_XML_VCHR =m_objViewer.m_txtSUMMARY2_VCHR.m_strGetXmlText();
			objItem.m_strDIAGNOSE_VCHR = m_objViewer.m_txtDIAGNOSE_VCHR.Text;
			objItem.m_strDIAGNOSE_XML_VCHR =m_objViewer.m_txtDIAGNOSE_VCHR.m_strGetXmlText();
			objItem.m_strLEFT_RIGHT = m_objViewer.m_cboLEFT_RIGHT.Text.Trim();
			objItem.m_strBEFORE_CHECK = m_objViewer.m_txtBEFORE_CHECK.Text.Trim();
			objItem.m_strBODY_STAT = m_objViewer.m_txtBODY_STAT.Text.Trim();
			objItem.m_strSENSE_STAT = m_objViewer.m_txtSENSE_STAT.Text.Trim();
			objItem.m_strDRUG_STAT = m_objViewer.m_txtDRUG_STAT.Text.Trim();
			

			if(m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag != null)
				objItem.m_strREPORTOR_ID_CHR = m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag.ToString().Trim();
			else
				objItem.m_strREPORTOR_ID_CHR = "";
			objItem.m_strREPORTOR_NAME_VCHR = m_objViewer.m_txtREPORTOR_NAME_VCHR.txtValuse.ToString().Trim();
            //objItem.m_strCONFIRMER_ID_CHR = m_objViewer.LoginInfo.m_strEmpID.Trim();
            //objItem.m_strCONFIRMER_NAME_VCHR = m_objViewer.LoginInfo.m_strEmpName.Trim();
            objItem.m_strCONFIRMER_ID_CHR = "";
            objItem.m_strCONFIRMER_NAME_VCHR = "";
			
			objItem.m_intSTATUS_INT = 1;
			objItem.m_strOPERATOR_ID_CHR = m_objViewer.LoginInfo.m_strEmpID.Trim();
            objItem.m_strREPORTOR_ID_CHR = this.m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag.ToString() ;
			objItem.m_strDEPT_ID_CHR=this.m_objViewer.m_txtDEPT_NAME_VCHR.Tag.ToString();

			long lngRes=m_objManage.m_lngDoModifyEEGReport(objItem);
            if (lngRes > 0)
            {
                new clsController_RISEEGManage().m_mthQueryReportNew(objfrmEEGReportManage);

                MessageBox.Show("修改成功");
            }
            else
                MessageBox.Show("修改失败");
			m_objItem = objItem;
			
			m_objControllerRISCManage.Set_GUI_Apperance(objfrmEEGReportManage);
			//m_objControllerRISCManage.m_mthGetEEGReportArr();
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
			clsRIS_EEG_REPORT_VO objItem = new clsRIS_EEG_REPORT_VO();
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
			objItem.m_strDEPT_NAME_VCHR = m_objViewer.m_txtDEPT_NAME_VCHR.txtValuse.ToString().Trim();
			objItem.m_intIS_INPATIENT_INT = -1;
			if(m_objViewer.m_txtBED_NO_CHR.Tag != null)
				objItem.m_strBED_ID_CHR = m_objViewer.m_txtBED_NO_CHR.Tag.ToString().Trim();
			else
				objItem.m_strBED_ID_CHR = "";
			objItem.m_strBED_NO_CHR = m_objViewer.m_txtBED_NO_CHR.Text.Trim();

			objItem.m_strSUMMARY1_VCHR = m_objViewer.m_txtSUMMARY1_VCHR.Text.Trim();
			objItem.m_strSUMMARY1_XML_VCHR =m_objViewer.m_txtSUMMARY1_VCHR.m_strGetXmlText().Trim();
			objItem.m_strSUMMARY2_VCHR = m_objViewer.m_txtSUMMARY2_VCHR.Text.Trim();
			objItem.m_strSUMMARY2_XML_VCHR =m_objViewer.m_txtSUMMARY2_VCHR.m_strGetXmlText().Trim();
			if(m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag != null)
				objItem.m_strREPORTOR_ID_CHR = m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag.ToString().Trim();
			else
				objItem.m_strREPORTOR_ID_CHR = "";
			objItem.m_strREPORTOR_NAME_VCHR = m_objViewer.m_txtREPORTOR_NAME_VCHR.txtValuse.ToString().Trim();
			objItem.m_strCONFIRMER_ID_CHR = m_objViewer.LoginInfo.m_strEmpID.Trim();
			objItem.m_strCONFIRMER_NAME_VCHR = m_objViewer.LoginInfo.m_strEmpName.Trim();
			objItem.m_intSTATUS_INT = 0;
			objItem.m_strOPERATOR_ID_CHR = m_objViewer.LoginInfo.m_strEmpID.Trim();

			long lngRes=m_objManage.m_lngDoDeleteEEGReport(objItem);
			if(lngRes>0)
			{
//			MessageBox.Show("删除成功");
					m_mthClear();
                    this.m_objViewer.m_cmdSave.Enabled = true;
			}
			else
			{
			MessageBox.Show("不删除成功");
			}
				
//			m_objItem = null;

		
		
			m_objControllerRISCManage.Set_GUI_Apperance(objfrmEEGReportManage);
			m_objControllerRISCManage.m_mthGetEEGReportArr();
		}
		#endregion
		
		#region 清空
		/// <summary>
		/// 清空
		/// </summary>
		public void m_mthClear()
		{
			m_objViewer.m_cmdDelete.Enabled=false;
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
            m_objViewer.m_txtDEPT_NAME_VCHR.txtValuse = "";
			m_intIs_InPatient = -1;
			m_objViewer.m_txtBED_NO_CHR.Tag = null;
			m_objViewer.m_txtBED_NO_CHR.Clear();
			m_objViewer.m_txtSUMMARY1_VCHR.m_mthClearText();
			m_objViewer.m_txtSUMMARY2_VCHR.m_mthClearText();
			m_objViewer.m_txtREPORTOR_NAME_VCHR.Tag = null;
			m_objViewer.m_txtREPORTOR_NAME_VCHR.Text = "";
			m_objViewer.m_cboLEFT_RIGHT.Text="";
			m_objViewer.m_txtBEFORE_CHECK.Text="";
			m_objViewer.m_txtBODY_STAT.Text="";
			m_objViewer.m_txtSENSE_STAT.Text="";
			m_objViewer.m_txtDRUG_STAT.Text="";
			m_objViewer.m_txtDIAGNOSE_VCHR.Clear();
			this.m_objItem=null;
			m_objViewer.m_txtREPORT_NO_CHR.Select();
		}
		#endregion

		#region 审核
		public void m_mthDoConfirm()
		{
            string strRordID = (string)this.m_objViewer.m_txtREPORT_NO_CHR.Tag;
            long lngRes = m_objManage.m_lngConfigEEGReport(strRordID, this.m_objViewer.LoginInfo.m_strEmpID, this.m_objViewer.LoginInfo.m_strEmpName);
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

		#region 打印
		public void m_mthPrintReport(frmRISEEGReport infrmRISEEGReport)
		{
			try
			{
				infrmRISEEGReport.m_printPrevDlg.printDoc = infrmRISEEGReport.m_printDoc;
				infrmRISEEGReport.m_printPrevDlg.m_objItem=this.m_objItem;
				infrmRISEEGReport.m_printPrevDlg.Document=infrmRISEEGReport.m_printDoc;

                //clsController_RISCardiogramReport cls = new clsController_RISCardiogramReport(null);
                //cls.m_mthSetPrintPreviewDialogZoom10(infrmRISEEGReport.m_printPrevDlg);
                m_mthSetPrintPreviewDialogZoom10(infrmRISEEGReport.m_printPrevDlg);
				infrmRISEEGReport.m_printPrevDlg.ShowDialog();
			}
			catch
			{
				MessageBox.Show(infrmRISEEGReport,"打印机故障！","iCare",MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
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

		public void m_mthPrintDocBeginPrint()
		{
		}

		public void m_mthPrintDocPrintPage(System.Drawing.Printing.PrintPageEventArgs p_objPrintPageEventArgs)
		{
			clsPrint_RISEEGReport objPrintReport = new clsPrint_RISEEGReport(2);
			objPrintReport.objReportVO = m_objItem;
			objPrintReport.m_mthInitPrintTool(null);
			objPrintReport.m_mthPrintPage(p_objPrintPageEventArgs);
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
            //list.Add(frm.);
            //list.Add(frm.m_txtImpressDiagnose);
            return list;
        }
        public void m_mthEditTemplate()
        {
            m_objTemplate.m_mthManageTemplate();
        }
//        public void m_mthInitializeTemplate(frmRISEEGReport infrmRISEEGReport)
//        {
//            objController_Security = new com.digitalwave.iCare.gui.Security.clsController_Security();
////			string strEmployeeID;
////			try
////			{
////				strEmployeeID =this.m_objViewer.LoginInfo.m_strEmpID;
////			}
////			catch
////			{
////				strEmployeeID="0001";
////			}
//            m_objTemplateUtility = new com.digitalwave.iCare.gui.TemplateUtility.clsHRPTemplateUtility(objController_Security.objGetCurrentLoginPrincipal(), objController_Security.objGetCurrentLoginEmployee(), objController_Security.objGetCurrentLoginDepartment(), infrmRISEEGReport, true);
//        }
//        //生成模板
//        public void m_mthCreateTemplate(frmRISEEGReport infrmRISEEGReport)
//        {
//            com.digitalwave.iCare.middletier.DepartmentSvc.clsDeptSvc objDeptSvc = (com.digitalwave.iCare.middletier.DepartmentSvc.clsDeptSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.DepartmentSvc.clsDeptSvc));
//            com.digitalwave.iCare.ValueObject.clsDepartmentVO[] objDeptList = null;
//            long lngRes = objDeptSvc.m_lngGetDeptListByFussCriteria(objController_Security.objGetCurrentLoginPrincipal(), 1, "", "", "DEPTID_CHR", true, out objDeptList);

//            com.digitalwave.iCare.gui.TemplateUtility.clsController_NewTemplate objController_NewTemplate = new clsController_NewTemplate(objController_Security.objGetCurrentLoginPrincipal());
//            objController_NewTemplate.m_ObjDepartmentArr = objDeptList;
//            objController_NewTemplate.m_ObjCurrDepartment = objController_Security.objGetCurrentLoginDepartment();
//            objController_NewTemplate.m_ObjOperator = objController_Security.objGetCurrentLoginEmployee();
//            objController_NewTemplate.m_mthShowNewTemplateDialog(infrmRISEEGReport, infrmRISEEGReport.Name, false);
//        }
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
//				m_ephHandler.m_mthAddControl(m_objViewer.m_txtREPORT_NO_CHR);
				MessageBox.Show("脑电图号不能为空");
				m_objViewer.m_txtREPORT_NO_CHR.Select();
				return false;
			}

			if(m_objViewer.m_txtPATIENT_NAME_VCHR.Text.Trim() == "")
			{
//				m_ephHandler.m_mthAddControl(m_objViewer.m_txtPATIENT_NAME_VCHR);
				MessageBox.Show("姓名不能为空");
				m_objViewer.m_txtPATIENT_NAME_VCHR.Select();
				return false;
			}

			if(m_objViewer.m_txtAGE_FLT.Text.Trim() == "")
			{
//				m_ephHandler.m_mthAddControl(m_objViewer.m_txtAGE_FLT);
				MessageBox.Show("年龄不能为空");
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
			if(m_objViewer.m_txtDEPT_NAME_VCHR.txtValuse.ToString().Trim() == "")
			{
//				m_ephHandler.m_mthAddControl(m_objViewer.m_txtDEPT_NAME_VCHR);
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

			if(!bolReturn)
			{
				m_ephHandler.m_mthShowControlsErrorProvider();
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
                 this.m_objViewer.m_txtDIAGNOSE_VCHR.Text = strResult;


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
