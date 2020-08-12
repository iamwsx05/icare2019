using System;
using weCare.Core.Entity;
using System.Windows.Forms;
using System.Data; 
using System.Drawing;
using System.Text;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using com.digitalwave.iCare.Template.Client;

namespace com.digitalwave.iCare.gui.RIS
{
	/// <summary>
	/// clsController_RISCardiogramReport 的摘要说明。
	/// 作者：Alex 
	/// 时间：2004-5-27
	/// </summary>
	public class clsController_RISCardiogramReport : com.digitalwave.GUI_Base.clsController_Base
	{
		public clsController_RISCardiogramReport(frmCardiogramReport infrmCardiogramReport)
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			m_objManage = new clsDomainController_RISCardiogramManage();
			m_objItem = null;
			m_strOperatorID = "0000001";
			

			//初始化模板信息
            m_objViewer = infrmCardiogramReport;
            m_mthInitializeTemplate();
            
            //m_mthInitializeTemplate(infrmCardiogramReport);
		}
      //   string m_str;
		clsDomainController_RISCardiogramManage m_objManage = null;

		public clsRIS_CardiogramReport_VO m_objItem;

		public string m_strOperatorID;

		public int m_intIs_InPatient = -1;

		public System.Windows.Forms.ListViewItem objlsvItem = null;

		private com.digitalwave.iCare.gui.Security.clsController_Security objController_Security = null;
		private com.digitalwave.iCare.gui.TemplateUtility.clsHRPTemplateUtility m_objTemplateUtility;
		private com.digitalwave.iCare.gui.RIS.clsController_RISCardiogramManage m_objControllerRISCManage = new clsController_RISCardiogramManage();
		private com.digitalwave.iCare.gui.RIS.frmCardiogramReportManage objfrmCardiogramReportManage = null;

		#region 设置窗体对象

		com.digitalwave.iCare.gui.RIS.frmCardiogramReport m_objViewer;

		/// <summary>
		/// 设置窗体对象
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmCardiogramReport)frmMDI_Child_Base_in;
		}

		public void SetParentApperance(com.digitalwave.iCare.gui.RIS.frmCardiogramReportManage infrmCardiogramReportManage)
		{
			objfrmCardiogramReportManage = infrmCardiogramReportManage;
			m_objViewer.LoginInfo=infrmCardiogramReportManage.LoginInfo;//用户信息传递
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

			//m_objViewer.m_txtREPORTOR_NAME_VCHR.m_StrDeptID = "0000180";
		}
		#endregion

		public void m_mthSetReport(clsRIS_CardiogramReport_VO p_objItem)
		{
			m_objItem = p_objItem;

			if(m_objItem == null)
			{
				m_objViewer.m_cmdDelete.Enabled = false;
				return;
			}
			#region    根据 卡号 检索病人ID       
			string carID=m_objItem.m_strCARD_ID_CHR;
			long lng=-1;
			if(carID!="")
			{
				DataTable  tbPat = new DataTable();
				lng=m_objManage.m_lngGetPat(carID,out tbPat);
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
            m_objViewer.m_txtApplyDoctor.txtValuse = m_objItem.m_strApplyDoctorName;
			
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
			

//			m_objViewer.m_txtAGE_FLT.Text = m_objItem.m_strAGE_FLT.ToString();
			try
			{
				m_objViewer.m_dtpCHECK_DAT.Value = Convert.ToDateTime(m_objItem.m_strCHECK_DAT.ToString());
				m_objViewer.m_dtpREPORT_DAT.Value = Convert.ToDateTime(m_objItem.m_strREPORT_DAT.ToString());
			}
			catch
			{
			}
			m_objViewer.m_txtDept.Tag = m_objItem.m_strDEPT_ID_CHR;
			m_objViewer.m_txtDept.txtValuse = m_objItem.m_strDEPT_NAME_VCHR;
			m_intIs_InPatient = m_objItem.m_intIS_INPATIENT_INT;
			m_objViewer.m_txtBED_NO_CHR.Tag = m_objItem.m_strBED_ID_CHR;
			m_objViewer.m_txtBED_NO_CHR.Text = m_objItem.m_strBED_NO_CHR;
			m_objViewer.m_txtRHYTHM_VCHR.Text = m_objItem.m_strRHYTHM_VCHR;
			m_objViewer.m_txtHEART_RATE_VCHR.Text = m_objItem.m_strHEART_RATE_VCHR;
			m_objViewer.m_txtP_R_VCHR.Text = m_objItem.m_strP_R_VCHR;
			m_objViewer.m_txtQRS_VCHR.Text = m_objItem.m_strQRS_VCHR;
			m_objViewer.m_txtQ_T_VCHR.Text = m_objItem.m_strQ_T_VCHR;
			m_objViewer.m_txtSUMMARY1_VCHR.m_mthSetNewText(m_objItem.m_strSUMMARY1_VCHR,m_objItem.m_strSUMMARY1_XML_VCHR);
			m_objViewer.m_txtSUMMARY2_VCHR.m_mthSetNewText(m_objItem.m_strSUMMARY2_VCHR,m_objItem.m_strSUMMARY2_XML_VCHR);
		
			m_objViewer.m_txtDoctor.Tag = m_objItem.m_strREPORTOR_ID_CHR;
			m_objViewer.m_txtDoctor.txtValuse = m_objItem.m_strREPORTOR_NAME_VCHR;
			m_objViewer.m_txtHEART_ROOM_VCHR.Text = m_objItem.m_strHEART_ROOM_VCHR;
			m_objViewer.m_cheIsSpical.Checked=m_objItem.m_intIsSpicalPatient==1;
            m_objViewer.m_txt_E_Axes.Text = m_objItem.m_strE_Axes_vchr;
            

            if (m_objItem.m_strCONFIRMER_ID_CHR == "")
            {
                this.m_objViewer.m_cmdSave.Enabled = true;
                this.m_objViewer.m_cmdConfirm.Enabled = true;
            }
            else
            {
                this.m_objViewer.m_cmdSave.Enabled = false;
                this.m_objViewer.m_cmdConfirm.Enabled = false;
            
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
            m_objItem.m_strApplyDoctorName = p_objItem.m_StrDoctor;
			m_mthSetReport(m_objItem);			
		}
		public void m_mthDoSave()
		{
			if(!m_bolCheckValuePass())
				return;
//			if(this.m_objViewer.m_txtPATIENT_NAME_VCHR.Tag=="")
//			{
//				MessageBox.Show("   请先登记病人卡号","提示：");
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
		public void m_mthDoAddNew(out clsRIS_CardiogramReport_VO objItem)
		{
			objItem = new clsRIS_CardiogramReport_VO();
			if(m_objViewer.m_txtREPORT_NO_CHR.Text=="")
			{
				MessageBox.Show("请输入心电图号");
				return;
			}
			if(m_objViewer.m_txtPATIENT_NAME_VCHR.Text=="")
			{
				MessageBox.Show("请输入姓名");
				return;
			}
			if(m_objViewer.m_txtAGE_FLT.Text=="")
			{
				MessageBox.Show("请输入年龄");
				return;
			}
			
			objItem.m_strREPORT_ID_CHR = "";
			
			objItem.m_strMODIFY_DAT = DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
			objItem.m_strREPORT_NO_CHR = m_objViewer.m_txtREPORT_NO_CHR.Text.Trim();
			objItem.m_strREPORT_ID_CHR = this.m_objViewer.m_txtDoctor.Tag.ToString();
            objItem.m_strDEPT_ID_CHR = this.m_objViewer.m_txtDept.Tag.ToString();
			if(m_objViewer.m_txtPATIENT_NAME_VCHR.Tag != null)
				objItem.m_strPATIENT_ID_CHR = m_objViewer.m_txtPATIENT_NAME_VCHR.Tag.ToString().Trim();
			else
				objItem.m_strPATIENT_ID_CHR = "";
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
            if (m_objViewer.m_txtDept.Tag != null && m_objViewer.m_txtDept.Tag.ToString() != "")
                objItem.m_strDEPT_ID_CHR = m_objViewer.m_txtDept.Tag.ToString();
			else
				objItem.m_strDEPT_ID_CHR = "";
			objItem.m_strDEPT_NAME_VCHR = m_objViewer.m_txtDept.txtValuse.Trim();
			objItem.m_intIS_INPATIENT_INT = -1;
			if(m_objViewer.m_txtBED_NO_CHR.Tag != null)
				objItem.m_strBED_ID_CHR = m_objViewer.m_txtBED_NO_CHR.Tag.ToString().Trim();
			else
				objItem.m_strBED_ID_CHR = "";
			objItem.m_strBED_NO_CHR = m_objViewer.m_txtBED_NO_CHR.Text.Trim();
			objItem.m_strCLINICAL_DIAGNOSE_VCHR = "";
			objItem.m_strRHYTHM_VCHR = m_objViewer.m_txtRHYTHM_VCHR.Text.Trim();
			objItem.m_strHEART_RATE_VCHR = m_objViewer.m_txtHEART_RATE_VCHR.Text.Trim();
			objItem.m_strP_R_VCHR = m_objViewer.m_txtP_R_VCHR.Text.Trim();
			objItem.m_strQRS_VCHR = m_objViewer.m_txtQRS_VCHR.Text.Trim();
			objItem.m_strQ_T_VCHR = m_objViewer.m_txtQ_T_VCHR.Text.Trim();
			objItem.m_strSUMMARY1_VCHR = m_objViewer.m_txtSUMMARY1_VCHR.Text;//.Trim();
			objItem.m_strSUMMARY1_XML_VCHR =m_objViewer.m_txtSUMMARY1_VCHR.m_strGetXmlText().Trim();
			objItem.m_strSUMMARY2_VCHR = m_objViewer.m_txtSUMMARY2_VCHR.Text;//.Trim();
			objItem.m_strSUMMARY2_XML_VCHR=m_objViewer.m_txtSUMMARY2_VCHR.m_strGetXmlText().Trim();
            if (!string.IsNullOrEmpty(m_objViewer.m_strApplyID))
            {
                objItem.m_intApplyID = Convert.ToInt32(m_objViewer.m_strApplyID.Trim());
            }
            else
            {
                objItem.m_intApplyID = 0;
            }

			if(m_objViewer.m_txtDoctor.Tag != null&&m_objViewer.m_txtDoctor.Tag.ToString()!="")
				objItem.m_strREPORTOR_ID_CHR = m_objViewer.m_txtDoctor.Tag.ToString();
			else
				objItem.m_strREPORTOR_ID_CHR = "";
			objItem.m_strREPORTOR_NAME_VCHR = m_objViewer.m_txtDoctor.txtValuse.Trim();
            //objItem.m_strCONFIRMER_ID_CHR = m_objViewer.LoginInfo.m_strEmpID.Trim();
            //objItem.m_strCONFIRMER_NAME_VCHR = m_objViewer.LoginInfo.m_strEmpName.Trim();
            objItem.m_strCONFIRMER_ID_CHR = "";
            objItem.m_strCONFIRMER_NAME_VCHR = "";
			objItem.m_strHEART_ROOM_VCHR = m_objViewer.m_txtHEART_ROOM_VCHR.Text.Trim();
			objItem.m_intSTATUS_INT = 1;
			objItem.m_strOPERATOR_ID_CHR = m_objViewer.LoginInfo.m_strEmpID.Trim();
			if(m_objViewer.m_cheIsSpical.Checked)//是否是特殊病人
			{
				objItem.m_intIsSpicalPatient=1;
			}
			else
			{
			objItem.m_intIsSpicalPatient=0;
			}
            objItem.m_strE_Axes_vchr = m_objViewer.m_txt_E_Axes.Text.Trim();
            objItem.m_strApplyDoctorName = m_objViewer.m_txtApplyDoctor.txtValuse;
            if (m_objViewer.m_txtApplyDoctor.Tag != null)
            {
                objItem.m_strApplyDoctorID = m_objViewer.m_txtApplyDoctor.Tag.ToString();
            }
			string strReportID = "";
			long lngRes=m_objManage.m_lngDoAddNewCardiogramReport(out strReportID,objItem);
            if (lngRes > 0)
            {
                //m_objControllerRISCManage.m_lngGetAcctData(System.DateTime.Now.AddDays(-2), System.DateTime.Now);
                //frmCardiogramReportManage objfrm = new frmCardiogramReportManage();
                //objfrm.m_cmdRefreshClick();
                MessageBox.Show("添加成功");
                this.m_objViewer.m_cmdSave.Tag = "OK";
               
            }

            else
                MessageBox.Show("添加失败");

			objItem.m_strREPORT_ID_CHR = strReportID;
       
			m_objItem = objItem;

			m_objControllerRISCManage.Set_GUI_Apperance(objfrmCardiogramReportManage);
			m_objControllerRISCManage.m_mthGetCardiogramReportArr();
            m_objControllerRISCManage.m_lngGetAcctData(System.DateTime.Now.AddDays(-2), System.DateTime.Now);
			//objfrmCardiogramReportManage.m_tbcMain.SelectedIndex=0;
		}
		#endregion
    
		#region 修改
		/// <summary>
		/// 修改
		/// </summary>
		public void m_mthDoModify()
		{

			clsRIS_CardiogramReport_VO objItem = new clsRIS_CardiogramReport_VO();
			objItem.m_strREPORT_ID_CHR =(string)m_objViewer.m_txtREPORT_NO_CHR.Tag;
			objItem.m_strMODIFY_DAT = DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
			objItem.m_strREPORT_NO_CHR = m_objViewer.m_txtREPORT_NO_CHR.Text.Trim();
			objItem.m_strDEPT_ID_CHR=this.m_objViewer.m_txtDept.Tag.ToString();
			if(m_objViewer.m_txtPATIENT_NAME_VCHR.Tag != null)
				objItem.m_strPATIENT_ID_CHR = m_objViewer.m_txtPATIENT_NAME_VCHR.Tag.ToString().Trim();
			else
				objItem.m_strPATIENT_ID_CHR = "";

			objItem.m_strPATIENT_NO_CHR = m_objViewer.m_txtPATIENT_NO_CHR.Text.Trim();
			objItem.m_strINPATIENT_NO_CHR = m_objViewer.m_txtINPATIENT_NO_CHR.Text.Trim();
			objItem.m_strPATIENT_NAME_VCHR = m_objViewer.m_txtPATIENT_NAME_VCHR.Text.Trim();
			objItem.m_strSEX_CHR = m_objViewer.m_cboSEX_CHR.Text.Trim();
//			objItem.m_strAGE_FLT = m_objViewer.m_txtAGE_FLT.Text.Trim()+m_objViewer.m_cmbAge.Text.Trim();
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
			if(m_objViewer.m_txtDept.Tag != null)
				objItem.m_strDEPT_ID_CHR = m_objViewer.m_txtDept.Tag.ToString().Trim();
			else
				objItem.m_strDEPT_ID_CHR = "";
			objItem.m_strDEPT_NAME_VCHR = m_objViewer.m_txtDept.txtValuse.Trim();
			objItem.m_intIS_INPATIENT_INT = -1;
			if(m_objViewer.m_txtBED_NO_CHR.Tag != null)
				objItem.m_strBED_ID_CHR = m_objViewer.m_txtBED_NO_CHR.Tag.ToString().Trim();
			else
				objItem.m_strBED_ID_CHR = "";
			objItem.m_strBED_NO_CHR = m_objViewer.m_txtBED_NO_CHR.Text.Trim();
			objItem.m_strCLINICAL_DIAGNOSE_VCHR = "";
			objItem.m_strRHYTHM_VCHR = m_objViewer.m_txtRHYTHM_VCHR.Text.Trim();
			objItem.m_strHEART_RATE_VCHR = m_objViewer.m_txtHEART_RATE_VCHR.Text.Trim();
			objItem.m_strP_R_VCHR = m_objViewer.m_txtP_R_VCHR.Text.Trim();
			objItem.m_strQRS_VCHR = m_objViewer.m_txtQRS_VCHR.Text.Trim();
			objItem.m_strQ_T_VCHR = m_objViewer.m_txtQ_T_VCHR.Text.Trim();
		
			objItem.m_strSUMMARY1_VCHR = m_objViewer.m_txtSUMMARY1_VCHR.Text;//.Trim();
			objItem.m_strSUMMARY1_XML_VCHR =m_objViewer.m_txtSUMMARY1_VCHR.m_strGetXmlText();//.Trim();
			objItem.m_strSUMMARY2_VCHR = m_objViewer.m_txtSUMMARY2_VCHR.Text;//.Trim();
			objItem.m_strSUMMARY2_XML_VCHR=m_objViewer.m_txtSUMMARY2_VCHR.m_strGetXmlText();//.Trim();

			if(m_objViewer.m_txtDoctor.Tag != null)
				objItem.m_strREPORTOR_ID_CHR = m_objViewer.m_txtDoctor.Tag.ToString().Trim();
			else
				objItem.m_strREPORTOR_ID_CHR = "";
			objItem.m_strREPORTOR_NAME_VCHR = m_objViewer.m_txtDoctor.txtValuse.Trim();
            //objItem.m_strCONFIRMER_ID_CHR = m_objViewer.LoginInfo.m_strEmpID.Trim();
            //objItem.m_strCONFIRMER_NAME_VCHR = m_objViewer.LoginInfo.m_strEmpName.Trim();
            objItem.m_strCONFIRMER_ID_CHR = "";
            objItem.m_strCONFIRMER_NAME_VCHR = "";
			objItem.m_strHEART_ROOM_VCHR = m_objViewer.m_txtHEART_ROOM_VCHR.Text.Trim();
			objItem.m_intSTATUS_INT = 1;
			objItem.m_strOPERATOR_ID_CHR = m_objViewer.LoginInfo.m_strEmpID.Trim();
			if(m_objViewer.m_cheIsSpical.Checked)//是否是特殊病人
			{
				objItem.m_intIsSpicalPatient=1;
			}
			else
			{
				objItem.m_intIsSpicalPatient=0;
			}
            if (!string.IsNullOrEmpty(m_objViewer.m_strApplyID))
            {
                objItem.m_intApplyID = Convert.ToInt32(m_objViewer.m_strApplyID.Trim());
            }
            else
            {
                m_objItem.m_intApplyID = 0;
            }
            objItem.m_strE_Axes_vchr = m_objViewer.m_txt_E_Axes.Text.Trim();
            objItem.m_strApplyDoctorName = m_objViewer.m_txtApplyDoctor.txtValuse;
            if (m_objViewer.m_txtApplyDoctor.Tag != null)
            {
                objItem.m_strApplyDoctorID = m_objViewer.m_txtApplyDoctor.Tag.ToString();
            }
			long lngRes=m_objManage.m_lngDoModifyCardiogramReport(objItem);
            if (lngRes > 0)
            {
               
                #region 刷新列表

                m_objControllerRISCManage.m_mthQueryReportNew(objfrmCardiogramReportManage);
                #endregion
                MessageBox.Show("修改成功");
                this.m_objViewer.m_cmdSave.Tag = "OK";

            }
            else
                MessageBox.Show("修改失败");
			m_objItem = objItem;
			
			m_objControllerRISCManage.Set_GUI_Apperance(objfrmCardiogramReportManage);
			//m_objControllerRISCManage.m_mthGetCardiogramReportArr();
			//objfrmCardiogramReportManage.m_tbcMain.SelectedIndex=0;
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
			clsRIS_CardiogramReport_VO objItem = new clsRIS_CardiogramReport_VO();
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
			if(m_objViewer.m_txtDept.Tag != null)
				objItem.m_strDEPT_ID_CHR = m_objViewer.m_txtDept.Tag.ToString().Trim();
			else
				objItem.m_strDEPT_ID_CHR = "";
			objItem.m_strDEPT_NAME_VCHR = m_objViewer.m_txtDept.txtValuse.Trim();
			objItem.m_intIS_INPATIENT_INT = -1;
			if(m_objViewer.m_txtBED_NO_CHR.Tag != null)
				objItem.m_strBED_ID_CHR = m_objViewer.m_txtBED_NO_CHR.Tag.ToString().Trim();
			else
				objItem.m_strBED_ID_CHR = "";
			objItem.m_strBED_NO_CHR = m_objViewer.m_txtBED_NO_CHR.Text.Trim();
			objItem.m_strCLINICAL_DIAGNOSE_VCHR = "";
			objItem.m_strRHYTHM_VCHR = m_objViewer.m_txtRHYTHM_VCHR.Text.Trim();
			objItem.m_strHEART_RATE_VCHR = m_objViewer.m_txtHEART_RATE_VCHR.Text.Trim();
			objItem.m_strP_R_VCHR = m_objViewer.m_txtP_R_VCHR.Text.Trim();
			objItem.m_strQRS_VCHR = m_objViewer.m_txtQRS_VCHR.Text.Trim();
			objItem.m_strQ_T_VCHR = m_objViewer.m_txtQ_T_VCHR.Text.Trim();

			objItem.m_strSUMMARY1_VCHR = m_objViewer.m_txtSUMMARY1_VCHR.Text.Trim();
			objItem.m_strSUMMARY1_XML_VCHR =m_objViewer.m_txtSUMMARY1_VCHR.m_strGetXmlText().Trim();
			objItem.m_strSUMMARY2_VCHR = m_objViewer.m_txtSUMMARY2_VCHR.Text.Trim();
			objItem.m_strSUMMARY2_XML_VCHR=m_objViewer.m_txtSUMMARY2_VCHR.m_strGetXmlText().Trim();

			if(m_objViewer.m_txtDoctor.Tag != null)
				objItem.m_strREPORTOR_ID_CHR = m_objViewer.m_txtDoctor.Tag.ToString().Trim();
			else
				objItem.m_strREPORTOR_ID_CHR = "";
			objItem.m_strREPORTOR_NAME_VCHR = m_objViewer.m_txtDoctor.txtValuse.Trim();
			objItem.m_strCONFIRMER_ID_CHR = m_objViewer.LoginInfo.m_strEmpID.Trim();
			objItem.m_strCONFIRMER_NAME_VCHR = m_objViewer.LoginInfo.m_strEmpName.Trim();
			objItem.m_strHEART_ROOM_VCHR = m_objViewer.m_txtHEART_ROOM_VCHR.Text.Trim();
			objItem.m_intSTATUS_INT = 0;
			objItem.m_strOPERATOR_ID_CHR = m_objViewer.LoginInfo.m_strEmpID.Trim();

			long lngRes=m_objManage.m_lngDoDeleteCardiogramReport(objItem);
			if(lngRes>0)
				MessageBox.Show("删除成功");
			else
				MessageBox.Show("删除失败");
			m_objItem = null;

			m_mthClear();
		
			m_objControllerRISCManage.Set_GUI_Apperance(objfrmCardiogramReportManage);
			m_objControllerRISCManage.m_mthGetCardiogramReportArr();
			m_objViewer.m_cmdPrint.Enabled=false;
			//objfrmCardiogramReportManage.m_tbcMain.SelectedIndex=0;
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
			m_objViewer.m_txtDept.Tag = null;
			m_objViewer.m_txtDept.txtValuse = string.Empty;
			m_intIs_InPatient = -1;
			m_objViewer.m_txtBED_NO_CHR.Tag = null;
			m_objViewer.m_txtBED_NO_CHR.Clear();
			m_objViewer.m_txtRHYTHM_VCHR.SelectedIndex = 0;
			m_objViewer.m_txtHEART_RATE_VCHR.Clear();
		//	m_objViewer.m_txtP_R_VCHR.m_mthClearText();
			m_objViewer.m_txtQRS_VCHR.Clear();
			m_objViewer.m_txtQ_T_VCHR.Clear();
			m_objViewer.m_txtP_R_VCHR.Clear();
			m_objViewer.m_txtSUMMARY1_VCHR.m_mthClearText();
			m_objViewer.m_txtSUMMARY2_VCHR.m_mthClearText();
			m_objViewer.m_txtDoctor.Tag = null;
			m_objViewer.m_txtDoctor.txtValuse = string.Empty;
			m_objViewer.m_txtHEART_ROOM_VCHR.Clear();
			m_objItem = null;
			m_objViewer.m_txtREPORT_NO_CHR.Select();
			m_objViewer.m_txtSubAGE_FLT.Clear();
			m_objViewer.m_cheIsSpical.Checked=false;

		}
		#endregion

		#region 审核(心电图)
        
		public void m_mthDoConfirm()
		{
            string strRordID = (string)this.m_objViewer.m_txtREPORT_NO_CHR.Tag;
            long lngRes = m_objManage.m_lngConfigCardiogramReport(strRordID, this.m_objViewer.LoginInfo.m_strEmpID, this.m_objViewer.LoginInfo.m_strEmpName);
            if (lngRes > 0)
            {
                if(!string.IsNullOrEmpty(m_objViewer.m_strApplyID))
                m_objManage.m_mthUpdateApply(this.m_objViewer.m_strApplyID.Trim());
                MessageBox.Show("审核成功！", "提示");
                #region 刷新列表

                m_objControllerRISCManage.m_mthQueryReportNew(objfrmCardiogramReportManage);
                if (this.objfrmCardiogramReportManage != null)
                {
                    if(this.objfrmCardiogramReportManage.lisvAcct.SelectedItems.Count>0)
                    {
                        this.objfrmCardiogramReportManage.lisvAcct.SelectedItems[0].Remove();
                    }
                }
                #endregion
                this.m_objViewer.m_cmdConfirm.Enabled = false;
            }
		}
		#endregion

		#region 打印
		public void m_mthPrintReport(frmCardiogramReport infrmCardiogramReport)
		{
			try
			{
				infrmCardiogramReport.m_printPrevDlg.Document = infrmCardiogramReport.m_printDoc;
                m_mthSetPrintPreviewDialogZoom10(infrmCardiogramReport.m_printPrevDlg);
				infrmCardiogramReport.m_printPrevDlg.ShowDialog();
			}
			catch
			{
				MessageBox.Show(infrmCardiogramReport,"打印机故障！","iCare",MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
		}

		public void m_mthPrintDocBeginPrint()
		{
		}

        #region 将printPreviewDialog最大化,并将比例设置为100%
        /// <summary>
        /// 将printPreviewDialog最大化,并将比例设置为100%
        /// </summary>
        /// <param name="p_printPreviewDialog"></param>
        public  void m_mthSetPrintPreviewDialogZoom10(PrintPreviewDialog p_printPreviewDialog)
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

		public void m_mthPrintDocPrintPage(System.Drawing.Printing.PrintPageEventArgs p_objPrintPageEventArgs)
		{
			clsPrint_RISCardiogramReport objPrintReport = new clsPrint_RISCardiogramReport();
			objPrintReport.objReportVO = m_objItem;
            objPrintReport.log = m_objViewer.m_picLog.Image;
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
		//初始化模板
        //public void m_mthInitializeTemplate(frmCardiogramReport infrmCardiogramReport)
        //{
        //    objController_Security = new com.digitalwave.iCare.gui.Security.clsController_Security();
        //    m_objTemplateUtility = new com.digitalwave.iCare.gui.TemplateUtility.clsHRPTemplateUtility(objController_Security.objGetCurrentLoginPrincipal(), objController_Security.objGetCurrentLoginEmployee(), objController_Security.objGetCurrentLoginDepartment(), infrmCardiogramReport, true);
        //}
        ////生成模板
        //public void m_mthCreateTemplate(frmCardiogramReport infrmCardiogramReport)
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

		/// <summary>
		/// 校验输入值
		/// </summary>
		/// <returns></returns>
		private bool m_bolCheckValuePass()
		{
			bool bolReturn = true;

			if(m_objViewer.m_txtREPORT_NO_CHR.Text.Trim() == "")
			{
				MessageBox.Show("心电图号不能为空!");
//				m_ephHandler.m_mthAddControl(m_objViewer.m_txtREPORT_NO_CHR);
				m_objViewer.m_txtREPORT_NO_CHR.Select();
				return false;
			}

			if(m_objViewer.m_txtPATIENT_NAME_VCHR.Text.Trim() == "")
			{
				MessageBox.Show("病人名称不能为空!");
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
			if(m_objViewer.m_txtDept.txtValuse.Trim() == "")
			{
				MessageBox.Show("科室不能为空!");
				m_objViewer.m_txtDept.Select();
                m_objViewer.m_txtDept.Focus();
				return false;
			}
            else if (m_objViewer.m_txtDept.Tag == null)
            {
                MessageBox.Show("请确认输入的是医院已设立的科室!");
                m_objViewer.m_txtDept.Select();
                m_objViewer.m_txtDept.Focus();
                return false;
            }
            else
            {
                int intJ = 0;
                for(int intI=0;intI<m_objViewer.m_txtDept.m_GetDataTable.Rows.Count;intI++)
                {
                     if(m_objViewer.m_txtDept.m_GetDataTable.Rows[intI][1].ToString()==m_objViewer.m_txtDept.txtValuse.Trim())
                      {
                          intJ++;
                      }
                 }
                 if (intJ == 0)
                 {
                     MessageBox.Show("请确认输入的是医院已设立的科室!");
                     m_objViewer.m_txtDept.Select();
                     m_objViewer.m_txtDept.Focus();
                     return false;
                 }
            }
            if (m_objViewer.m_txtDoctor.txtValuse.Trim() == "")
            {
                MessageBox.Show("报告医师不能为空!");
                m_objViewer.m_txtDoctor.Select();
                m_objViewer.m_txtDoctor.Focus();
                return false;
            }
            else if (m_objViewer.m_txtDoctor.Tag == null)
            {
                MessageBox.Show("请确认输入的是在院医师!");
                m_objViewer.m_txtDoctor.Select();
                m_objViewer.m_txtDoctor.Focus();
                return false;
            }
            else
            {
                int intJ = 0;
                for (int intI = 0; intI < m_objViewer.m_txtDoctor.m_GetDataTable.Rows.Count; intI++)
                {
                    if (m_objViewer.m_txtDoctor.m_GetDataTable.Rows[intI][1].ToString() == m_objViewer.m_txtDoctor.txtValuse.Trim())
                    {
                        intJ++;
                    }
                }
                if (intJ == 0)
                {
                    MessageBox.Show("请确认输入的是在院医师!");
                    m_objViewer.m_txtDoctor.Select();
                    m_objViewer.m_txtDoctor.Focus();
                    return false;
                }
            }

			if(m_objViewer.m_txtSUMMARY1_VCHR.Text.Trim() == "")
			{
				
			}

			if(m_objViewer.m_txtSUMMARY2_VCHR.Text.Trim() == "")
			{
				
			}

			if(!bolReturn)
			{
				m_ephHandler.m_mthShowControlsErrorProvider();
				m_ephHandler.m_mthClearControl();
			}
			return bolReturn;
		}
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
                this.m_objViewer.m_txtDept.txtValuse = objDataRow["deptname_vchr"].ToString();
                this.m_objViewer.m_txtDept.Tag = objDataRow["deptid_chr"].ToString();
                if (!string.IsNullOrEmpty(objDataRow["bed_no"].ToString()))
                {
                    string strBedNo = objDataRow["bed_no"].ToString();
                    if (strBedNo.Length < 2 && strBedNo.Length > 0)
                        strBedNo = "00" + strBedNo;
                    this.m_objViewer.m_txtBED_NO_CHR.Text = strBedNo.Substring(strBedNo.Length - 2);
                    this.m_objViewer.m_txtBED_NO_CHR.Tag = objDataRow["bedid_chr"].ToString();
                }
                //this.m_objViewer.m_txtREPORT_NO_CHR.Focus();
                this.m_objViewer.m_txtDoctor.Focus();

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
                m_objViewer.m_txtDept.m_GetDataTable = dtValue;
            }
            DataTable dtDoctor = null;
            lngRes = (new weCare.Proxy.ProxyRIS()).Service.m_lngGetDoctorData(out dtDoctor, m_objViewer.LoginInfo.m_strDepartmentID, false);
            if (lngRes > 0)
            {
                m_objViewer.m_txtDoctor.m_GetDataTable = dtDoctor;
            }
            DataTable dtValue2 = null;
            lngRes = (new weCare.Proxy.ProxyRIS()).Service.m_lngGetDoctorData(out dtValue2, "", false);
            if (lngRes > 0)
            {
                m_objViewer.m_txtApplyDoctor.m_GetDataTable = dtValue2;
            }
        }

	}
}
