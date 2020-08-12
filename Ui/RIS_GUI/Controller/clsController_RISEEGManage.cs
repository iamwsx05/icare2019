using System;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using weCare.Core.Entity;
using System.Windows.Forms;
using System.Data; 
using System.Drawing;
using System.Text;
using System.Xml;
using System.Collections;

namespace com.digitalwave.iCare.gui.RIS
{
    /// <summary>
    /// clsController_RISCardiogramReport 的摘要说明。
    /// 作者： 
    /// 时间：
    /// </summary>
    public class clsController_RISEEGManage : com.digitalwave.GUI_Base.clsController_Base
    {
        public clsController_RISEEGManage()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            m_objManage = new clsDomainController_RISEEGManage();
            m_objManage1 = new clsDomainController_RISCardiogramManage();
            m_strOperatorID = "0000001";
        }
        clsDomainController_RISCardiogramManage m_objManage1 = null;
        clsDomainController_RISEEGManage m_objManage = null;

        public string m_strOperatorID;

        #region 设置窗体对象

        com.digitalwave.iCare.gui.RIS.frmRISEEGReportNamage m_objViewer;

        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            m_objViewer = (frmRISEEGReportNamage)frmMDI_Child_Base_in;
        }
        #endregion

        #region 获取脑电图申单请数据
        /// <summary>
        /// 获取心电图申请数据
        /// </summary>
        DataTable dtAcct = new DataTable();
        /// <summary>
        /// 保存字段中文名及对应的控件名
        /// </summary>
        ArrayList arIndexNameValues = new ArrayList();
        /// <summary>
        /// 控件名 
        /// </summary>
        ArrayList arIndexName = new ArrayList();
        /// <summary>
        /// 值控件名
        /// </summary>
        ArrayList arIndexValues = new ArrayList();
        /// <summary>
        /// 控件名值
        /// </summary>
        ArrayList arValues = new ArrayList();

        //		private iCareData.clsApplyReportList_VO[] ReportArr;
        private clsApplyRecord[] ReportArr;
        public void m_lngGetAcctData(bool isCurDay)
        {
            //if (ReportArr == null)
            //{
            string typeID = "10,40";
            // m_objManage.m_mthGetApplTypeIDRISEEGR(out typeID);

            com.digitalwave.GLS_WS.clsApplyForm Aps = new com.digitalwave.GLS_WS.clsApplyForm();

            if (typeID != null)
                ReportArr = Aps.m_mthGetApplyRecordByDate(DateTime.Parse(DateTime.Now.ToString("yyyy-MM dd 00:00:00")), DateTime.Parse(DateTime.Now.ToString("yyyy-MM dd 23:59:59")), typeID);
            //}
            if (ReportArr == null || ReportArr.Length == 0)
                return;
            this.m_objViewer.lisvAcct.Items.Clear();
            string strResult = "";


            m_mthShowOrNot("8005", "0008", out strResult);
            string strModuleResult = this.m_objComInfo.m_lonGetModuleInfo("8008");
            for (int i1 = 0; i1 < ReportArr.Length; i1++)
            {
                com.digitalwave.iCare.common.clsCheckChargeInfo checkInfo = new com.digitalwave.iCare.common.clsCheckChargeInfo();
                bool blIsCheck = checkInfo.m_mthCheckIsCharge(ReportArr[i1].m_strApplyID, com.digitalwave.iCare.common.ApplyOrigin.PACSS);

                ListViewItem item = new ListViewItem(ReportArr[i1].m_strApplyTitle);
                if (blIsCheck == false || ReportArr[i1].m_intChargeStatus == 3)
                {
                    switch (ReportArr[i1].m_intChargeStatus)
                    {
                        case -1:
                            item.SubItems.Add("");
                            break;
                        case 0:
                            item.SubItems.Add("不记录缴费信息");
                            break;
                        case 1:
                            if (ReportArr[i1].m_strBedNO != null && ReportArr[i1].m_strBedNO != "")
                            {

                            }
                            else
                            {
                                if (strResult == "0")//不显示未缴费
                                    continue;
                            }
                            item.SubItems.Add("未缴费");
                            item.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(192)), ((System.Byte)(192)));
                            break;
                        case 2:
                            item.SubItems.Add("已缴费");
                            break;
                        case 3:
                            if (strModuleResult == "0")//不显示已退费
                            {
                                continue;
                            }
                            item.SubItems.Add("已退费");
                            item.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(150)), ((System.Byte)(150)));
                            break;
                    }
                }
                else
                    item.SubItems.Add("已缴费");
                item.SubItems.Add(ReportArr[i1].m_datApplyDate.ToString());
                item.SubItems.Add(ReportArr[i1].m_strCardNO);
                item.SubItems.Add(ReportArr[i1].m_strDepartment);
                item.SubItems.Add(ReportArr[i1].m_strBedNO);
                item.SubItems.Add(ReportArr[i1].m_strAge);
                item.SubItems.Add(ReportArr[i1].m_strSex);
                item.SubItems.Add(ReportArr[i1].m_strName);
                item.SubItems.Add(ReportArr[i1].m_strBIHNO);
                item.SubItems.Add("");
                item.SubItems.Add("");

                if (ReportArr[i1].m_intIsGreen == 1)
                {
                    item.BackColor = Color.Orange;
                }

                item.Tag = ReportArr[i1];
                this.m_objViewer.lisvAcct.Items.Add(item);
            }
            #region

            //			clsCustom_SyncInfo[] p_objSyncInfoArr=new clsCustom_SyncInfo[0];
            //			long lngRes=m_objManage.m_lngGetAllSyncInfoForInPatient(out p_objSyncInfoArr);
            //			if(lngRes==1&&p_objSyncInfoArr.Length!=0)
            //			{
            //
            //				for(int i1=0;i1<p_objSyncInfoArr.Length;i1++)
            //				{
            //					if(p_objSyncInfoArr[i1].m_objTarget_GUI!=null)
            //					{
            //						if(p_objSyncInfoArr[i1].m_objTarget_GUI.m_strTarget_Form_Desc.Trim()=="EEG脑电图报告单"||p_objSyncInfoArr[i1].m_objTarget_GUI.m_strTarget_Form_Desc.Trim()=="TCD脑电图报告单")
            //						{
            //							XmlParserContext objXmlParser = new XmlParserContext(null,null,null,XmlSpace.None,System.Text.Encoding.Unicode);
            //							XmlTextReader objReader = new XmlTextReader(p_objSyncInfoArr[i1].m_strSyncField,XmlNodeType.Element,objXmlParser);
            //							objReader.WhitespaceHandling = WhitespaceHandling.None;
            //							while(objReader.Read())
            //							{
            //								switch(objReader.NodeType)
            //								{
            //									case XmlNodeType.Element:
            //								
            //										if(objReader.Name.IndexOf("ctlRichTextBox",0)==0)
            //										{
            //											arIndexName.Add(objReader.Name);
            //											arIndexNameValues.Add(objReader.GetAttribute("FIELDNAME"));
            //											if(objReader.GetAttribute("CONTROLID")==null)
            //												arIndexNameValues.Add("");
            //											else
            //												arIndexNameValues.Add(objReader.GetAttribute("CONTROLID"));
            //										}
            //										break;
            //								}
            //							}
            //							XmlParserContext objXmlGetValues = new XmlParserContext(null,null,null,XmlSpace.None,System.Text.Encoding.Unicode);
            //							XmlTextReader objReaderGet = new XmlTextReader(p_objSyncInfoArr[i1].m_strSyncData,XmlNodeType.Element,objXmlParser);
            //							objReader.WhitespaceHandling = WhitespaceHandling.None;
            //							while(objReaderGet.Read())
            //							{
            //								switch(objReaderGet.NodeType)
            //								{
            //									case XmlNodeType.Element:
            //										for(int f2=0;f2<arIndexName.Count;f2++)
            //										{
            //											if(objReaderGet.Name.IndexOf("ctlRichTextBox",0)==0)
            //											{
            //												arIndexValues.Add(objReaderGet.Name);
            //												arValues.Add(objReaderGet.GetAttribute("VALUE"));
            //											}
            //											break;
            //										}
            //										break;
            //								}
            //							}
            //							try
            //							{
            //								dtAcct.Columns.Add("病人ID");
            //								dtAcct.Columns.Add("申请类型");
            //								dtAcct.Columns.Add("申请日期");
            //								dtAcct.Columns.Add("病人卡号ID");
            //								for(int k4=0;k4<arIndexNameValues.Count*2;k4++)
            //								{
            //									
            //									dtAcct.Columns.Add(arIndexNameValues[k4+k4].ToString());
            //								}
            //							}
            //							catch
            //							{
            //							}
            //							DataRow newRow=dtAcct.NewRow();
            //							newRow["申请日期"]=p_objSyncInfoArr[i1].m_dtmCreatedDate;
            //							newRow["申请类型"]=p_objSyncInfoArr[i1].m_objTarget_GUI.m_strTarget_Form_Desc;
            //							newRow["病人ID"]=p_objSyncInfoArr[i1].m_strPatientID;
            //							newRow["病人卡号ID"]=p_objSyncInfoArr[i1].m_strPatientCardID;
            //							for(int h3=0;h3<arIndexName.Count;h3++)
            //							{
            //								for(int p1=0;p1<arValues.Count;p1++)
            //								{
            //									if(arIndexName[h3].ToString().Trim()==arIndexValues[p1].ToString().Trim())
            //									{
            //			
            //										newRow[arIndexNameValues[h3+h3].ToString()]=arValues[p1].ToString();
            //										
            //									}
            //								}
            //							}
            //
            //							dtAcct.Rows.Add(newRow);
            //						}
            //					}
            //				}
            //				foreach(DataColumn dc in dtAcct.Columns)
            //				{
            //					if(dc.ColumnName!="病人ID")
            //					{
            //						if(dc.ColumnName=="申请日期")
            //							this.m_objViewer.lisvAcct.Columns.Add(dc.ColumnName.Trim(),150,HorizontalAlignment.Center);
            //						else
            //							this.m_objViewer.lisvAcct.Columns.Add(dc.ColumnName.Trim(),100,HorizontalAlignment.Center);
            //					}
            //				}
            //				for(int t1=0;t1<dtAcct.Rows.Count;t1++)
            //				{
            //					ListViewItem newItem=new ListViewItem(dtAcct.Rows[t1][1].ToString());
            //					for(int k8=2;k8<dtAcct.Columns.Count;k8++)
            //					{
            //						newItem.SubItems.Add(dtAcct.Rows[t1][k8].ToString());
            //					}
            //					newItem.Tag=dtAcct.Rows[t1];
            //					this.m_objViewer.lisvAcct.Items.Add(newItem);
            //				}
            //			}
            #endregion
        }
        #endregion

        #region 新增TCD心电图报告
        /// <summary>
        /// 新增心电图报告
        /// </summary>
        /// <param name="infrmCardiogramReportManage"></param>
        public void m_mthShowCardiogramReportAddNew(frmRISEEGReportNamage infrmRISEEGReportNamage)
        {
            clsApplyRecord objVO1 = (clsApplyRecord)m_objViewer.lisvAcct.SelectedItems[0].Tag;
            frmRISTCDReport objViewer1 = new frmRISTCDReport();

            objViewer1.m_txtPATIENT_NAME_VCHR.Tag = "";
            objViewer1.m_txtREPORT_NO_CHR.Text = objVO1.m_strAreaID;
            objViewer1.m_txtINPATIENT_NO_CHR.Text = objVO1.m_strBIHNO;
            objViewer1.m_txtPATIENT_NO_CHR.Text = objVO1.m_strClinicNO;
            objViewer1.m_txtDiagnose.Text = objVO1.m_strDiagnose;
            if (objVO1.m_strAge.Length > 0)
            {
                try
                {
                    objViewer1.m_txtAGE_FLT.Text = int.Parse(objVO1.m_strAge).ToString();
                }
                catch
                {
                    objViewer1.m_txtAGE_FLT.Text = objVO1.m_strAge.Substring(0, objVO1.m_strAge.Length - 1);
                    switch (objVO1.m_strAge.Substring(objVO1.m_strAge.Length - 1, 1))
                    {
                        case "日":
                            objViewer1.m_cmbAge.Text = "天";
                            break;
                        case "岁":
                            objViewer1.m_cmbAge.Text = "年";
                            break;
                        case "月":
                            objViewer1.m_cmbAge.Text = "月";
                            break;
                    }
                }
            }
            objViewer1.m_txtPATIENT_NAME_VCHR.Text = objVO1.m_strName;
            objViewer1.m_cboSEX_CHR.Text = objVO1.m_strSex;
            objViewer1.m_txtDEPT_NAME_VCHR.Text = objVO1.m_strDepartment;
            objViewer1.m_txtBED_NO_CHR.Text = objVO1.m_strBedNO;
            objViewer1.carID.Text = objVO1.m_strCardNO;
            #region    根据 卡号 检索病人ID

            long lng = -1;
            if (objViewer1.carID.Text != "")
            {
                DataTable tbPat = new DataTable();
                lng = m_objManage1.m_lngGetPat(objViewer1.carID.Text, out tbPat);
                if (lng > 0)
                    objViewer1.m_txtPATIENT_NAME_VCHR.Tag = tbPat.Rows[0]["PATIENTID_CHR"].ToString();
            }
            # endregion
            #region
            //			DataRow AddRow=dtAcct.NewRow();
            //			AddRow = (DataRow)m_objViewer.lisvAcct.SelectedItems[0].Tag;
            //			frmRISTCDReport objViewer1 = new frmRISTCDReport();
            //			if(AddRow["病人卡号ID"].ToString()!="")
            //				objViewer1.carID.Text=AddRow["病人卡号ID"].ToString();
            //			    objViewer1.m_txtPATIENT_NAME_VCHR.Tag=AddRow["病人ID"].ToString();
            //			foreach(DataColumn dc in dtAcct.Columns)
            //			{
            //				for(int i1=0;i1<arIndexNameValues.Count/2;i1++)
            //				{
            //					if(arIndexNameValues[i1+i1].ToString().Trim()==dc.ColumnName.Trim())
            //					{
            //						if(arIndexNameValues[i1+1].ToString().Trim()=="m_txtREPORT_NO_CHR")
            //						{
            //							objViewer1.m_txtREPORT_NO_CHR.Text=AddRow[arIndexNameValues[i1].ToString()].ToString();
            //						}
            //						if(arIndexNameValues[i1+1].ToString().Trim()=="m_txtPATIENT_NO_CHR")
            //						{
            //							objViewer1.m_txtPATIENT_NO_CHR.Text=AddRow[arIndexNameValues[i1].ToString()].ToString();
            //						}
            //						if(arIndexNameValues[i1+1].ToString().Trim()=="m_txtINPATIENT_NO_CHR")
            //						{
            //							objViewer1.m_txtINPATIENT_NO_CHR.Text=AddRow[arIndexNameValues[i1].ToString()].ToString();
            //						}
            //						if(arIndexNameValues[i1+1].ToString().Trim()=="m_txtAGE_FLT")
            //						{
            //							objViewer1.m_txtAGE_FLT.Text=AddRow[arIndexNameValues[i1].ToString()].ToString();
            //						}
            //						if(arIndexNameValues[i1+1].ToString().Trim()=="m_txtPATIENT_NAME_VCHR")
            //						{
            //							objViewer1.m_txtPATIENT_NAME_VCHR.Text=AddRow[arIndexNameValues[i1].ToString()].ToString();
            //						}
            //						if(arIndexNameValues[i1+1].ToString().Trim()=="m_cboSEX_CHR")
            //						{
            //							objViewer1.m_cboSEX_CHR.Text=AddRow[arIndexNameValues[i1].ToString()].ToString();
            //						}
            //						if(arIndexNameValues[i1+1].ToString().Trim()=="m_txtDEPT_NAME_VCHR")
            //						{
            //							objViewer1.m_txtDEPT_NAME_VCHR.Text=AddRow[arIndexNameValues[i1].ToString()].ToString();
            //						}
            //						if(arIndexNameValues[i1+1].ToString().Trim()=="m_txtBED_NO_CHR")
            //						{
            //							objViewer1.m_txtBED_NO_CHR.Text=AddRow[arIndexNameValues[i1].ToString()].ToString();
            //						}
            //					}
            //				}
            //				
            //			}
            #endregion
            objViewer1.m_mthSetParentApperance(infrmRISEEGReportNamage);
            objViewer1.Show();
        }
        #endregion

        #region 新增EEG心电图报告
        /// <summary>
        /// 新增心电图报告
        /// </summary>
        /// <param name="infrmCardiogramReportManage"></param>
        public void m_mthShowRISEEGReportAddNew(frmRISEEGReportNamage infrmRISEEGReportNamage)
        {
            //			clsApplyReportList_VO objVO = (clsApplyReportList_VO)m_objViewer.lisvAcct.SelectedItems[0].Tag;
            //			frmRISEEGReport objViewer1 = new frmRISEEGReport();
            //			objViewer1.m_txtPATIENT_NAME_VCHR.Tag = objVO.m_StrPatientID;
            //			objViewer1.m_txtREPORT_NO_CHR.Text=objVO.m_strRecordID;
            //			//objViewer1.m_txtPATIENT_NO_CHR.Text=objVO.m_StrPatientCardID;
            //			objViewer1.m_txtINPATIENT_NO_CHR.Text=objVO.m_StrInPatientID;
            //			
            //			if(objVO.m_StrPatientAge.Length > 0)
            //			{
            //				try
            //				{
            //					objViewer1.m_txtAGE_FLT.Text=int.Parse(objVO.m_StrPatientAge).ToString();
            //				}
            //				catch
            //				{
            //					objViewer1.m_txtAGE_FLT.Text=objVO.m_StrPatientAge.Substring(0,objVO.m_StrPatientAge.Length -1);
            //					switch(objVO.m_StrPatientAge.Substring(objVO.m_StrPatientAge.Length -1,1))
            //					{
            //						case "日":
            //							objViewer1.m_cmbAge.Text = "天";
            //							break;
            //						case "岁":
            //							objViewer1.m_cmbAge.Text = "年";
            //							break;
            //						case "月":
            //							objViewer1.m_cmbAge.Text = "月";
            //							break;
            //					}
            //				}
            //			}
            //			objViewer1.m_txtPATIENT_NAME_VCHR.Text=objVO.m_StrPatientName;
            //			objViewer1.m_cboSEX_CHR.Text=objVO.m_StrPatientSex;
            //			objViewer1.m_txtDEPT_NAME_VCHR.Text=objVO.m_StrDeptName;
            //			objViewer1.m_txtBED_NO_CHR.Text=objVO.m_StrBedName;
            //			objViewer1.carID.Text = objVO.m_StrPatientCardID;
            //			#region
            ////			DataRow AddRow=dtAcct.NewRow();
            ////			AddRow = (DataRow)m_objViewer.lisvAcct.SelectedItems[0].Tag;
            ////			frmRISEEGReport objViewer1 = new frmRISEEGReport();
            ////			if(AddRow["病人卡号ID"].ToString()!="")
            ////				objViewer1.carID.Text=AddRow["病人卡号ID"].ToString();
            ////			    objViewer1.m_txtPATIENT_NAME_VCHR.Tag=AddRow["病人ID"].ToString();
            ////			foreach(DataColumn dc in dtAcct.Columns)
            ////			{
            ////				for(int i1=0;i1<arIndexNameValues.Count/2;i1++)
            ////				{
            ////					if(arIndexNameValues[i1+i1].ToString().Trim()==dc.ColumnName.Trim())
            ////					{
            ////						if(arIndexNameValues[i1+1].ToString().Trim()=="m_txtREPORT_NO_CHR")
            ////						{
            ////							objViewer1.m_txtREPORT_NO_CHR.Text=AddRow[arIndexNameValues[i1].ToString()].ToString();
            ////						}
            ////						if(arIndexNameValues[i1+1].ToString().Trim()=="m_txtPATIENT_NO_CHR")
            ////						{
            ////							objViewer1.m_txtPATIENT_NO_CHR.Text=AddRow[arIndexNameValues[i1].ToString()].ToString();
            ////						}
            ////						if(arIndexNameValues[i1+1].ToString().Trim()=="m_txtINPATIENT_NO_CHR")
            ////						{
            ////							objViewer1.m_txtINPATIENT_NO_CHR.Text=AddRow[arIndexNameValues[i1].ToString()].ToString();
            ////						}
            ////						if(arIndexNameValues[i1+1].ToString().Trim()=="m_txtAGE_FLT")
            ////						{
            ////							objViewer1.m_txtAGE_FLT.Text=AddRow[arIndexNameValues[i1].ToString()].ToString();
            ////						}
            ////						if(arIndexNameValues[i1+1].ToString().Trim()=="m_txtPATIENT_NAME_VCHR")
            ////						{
            ////							objViewer1.m_txtPATIENT_NAME_VCHR.Text=AddRow[arIndexNameValues[i1].ToString()].ToString();
            ////						}
            ////						if(arIndexNameValues[i1+1].ToString().Trim()=="m_cboSEX_CHR")
            ////						{
            ////							objViewer1.m_cboSEX_CHR.Text=AddRow[arIndexNameValues[i1].ToString()].ToString();
            ////						}
            ////						if(arIndexNameValues[i1+1].ToString().Trim()=="m_txtDEPT_NAME_VCHR")
            ////						{
            ////							objViewer1.m_txtDEPT_NAME_VCHR.Text=AddRow[arIndexNameValues[i1].ToString()].ToString();
            ////						}
            ////						if(arIndexNameValues[i1+1].ToString().Trim()=="m_txtBED_NO_CHR")
            ////						{
            ////							objViewer1.m_txtBED_NO_CHR.Text=AddRow[arIndexNameValues[i1].ToString()].ToString();
            ////						}
            ////					}
            ////				}
            //				
            ////			}
            //			#endregion
            //			objViewer1.m_mthSetParentApperance(infrmRISEEGReportNamage);
            //			objViewer1.Show();


            clsApplyRecord objVO1 = (clsApplyRecord)m_objViewer.lisvAcct.SelectedItems[0].Tag;
            frmRISEEGReport objViewer1 = new frmRISEEGReport();

            objViewer1.m_txtPATIENT_NAME_VCHR.Tag = "";
            objViewer1.m_txtREPORT_NO_CHR.Text = objVO1.m_strAreaID;
            objViewer1.m_txtINPATIENT_NO_CHR.Text = objVO1.m_strBIHNO;
            objViewer1.m_txtPATIENT_NO_CHR.Text = objVO1.m_strClinicNO;
            objViewer1.m_txtDIAGNOSE_VCHR.Text = objVO1.m_strDiagnose;
            if (objVO1.m_strAge.Length > 0)
            {
                try
                {
                    objViewer1.m_txtAGE_FLT.Text = int.Parse(objVO1.m_strAge).ToString();
                }
                catch
                {
                    objViewer1.m_txtAGE_FLT.Text = objVO1.m_strAge.Substring(0, objVO1.m_strAge.Length - 1);
                    switch (objVO1.m_strAge.Substring(objVO1.m_strAge.Length - 1, 1))
                    {
                        case "日":
                            objViewer1.m_cmbAge.Text = "天";
                            break;
                        case "岁":
                            objViewer1.m_cmbAge.Text = "年";
                            break;
                        case "月":
                            objViewer1.m_cmbAge.Text = "月";
                            break;
                    }
                }
            }
            objViewer1.m_txtPATIENT_NAME_VCHR.Text = objVO1.m_strName;
            objViewer1.m_cboSEX_CHR.Text = objVO1.m_strSex;
            objViewer1.m_txtDEPT_NAME_VCHR.Text = objVO1.m_strDepartment;
            objViewer1.m_txtBED_NO_CHR.Text = objVO1.m_strBedNO;
            objViewer1.carID.Text = objVO1.m_strCardNO;
            #region    根据 卡号 检索病人ID

            long lng = -1;
            if (objViewer1.carID.Text != "")
            {
                DataTable tbPat = new DataTable();
                lng = m_objManage1.m_lngGetPat(objViewer1.carID.Text, out tbPat);
                if (lng > 0)
                    objViewer1.m_txtPATIENT_NAME_VCHR.Tag = tbPat.Rows[0]["PATIENTID_CHR"].ToString();
            }
            # endregion
            objViewer1.m_mthSetParentApperance(infrmRISEEGReportNamage);
            objViewer1.Show();
        }
        #endregion

        #region 获得心电图报告
        public void m_mthGetTCDReportArr()
        {
            m_objViewer.m_lsvTCDReportList.Items.Clear();
            clsRIS_TCD_REPORT_VO[] objResultArr = null;
            m_objManage.m_mthGetTCDReportArr(out objResultArr);
            if (objResultArr == null || objResultArr.Length == 0)
                return;

            ListViewItem lviTemp = null;
            for (int i1 = 0; i1 < objResultArr.Length; i1++)
            {
                lviTemp = new ListViewItem(objResultArr[i1].m_strREPORT_NO_CHR);
                lviTemp.SubItems.Add(objResultArr[i1].m_strPATIENT_NAME_VCHR);
                lviTemp.SubItems.Add(objResultArr[i1].m_strSEX_CHR);
                lviTemp.SubItems.Add(m_mthAgeChange(objResultArr[i1].m_strAGE_FLT.ToString()));
                lviTemp.SubItems.Add(objResultArr[i1].m_strPATIENT_NO_CHR);
                lviTemp.SubItems.Add(objResultArr[i1].m_strINPATIENT_NO_CHR);
                lviTemp.SubItems.Add(objResultArr[i1].m_strDEPT_NAME_VCHR);
                lviTemp.SubItems.Add(Convert.ToDateTime(objResultArr[i1].m_strCHECK_DAT).ToString(/*"yyyy-MM-dd"*/));
                lviTemp.SubItems.Add(Convert.ToDateTime(objResultArr[i1].m_strREPORT_DAT).ToString(/*"yyyy-MM-dd"*/));
                lviTemp.Tag = objResultArr[i1];
                m_objViewer.m_lsvTCDReportList.Items.Add(lviTemp);
            }
        }
        #endregion

        #region 显示心电图报告
        public void m_mthShowCardiogramReport(frmRISEEGReportNamage infrmCardiogramReportManage)
        {
            if (m_objViewer.m_lsvTCDReportList.Items.Count <= 0 ||
                m_objViewer.m_lsvTCDReportList.SelectedItems.Count <= 0)
                return;

            clsRIS_TCD_REPORT_VO objItem = (clsRIS_TCD_REPORT_VO)m_objViewer.m_lsvTCDReportList.SelectedItems[0].Tag;

            frmRISTCDReport objViewer = new frmRISTCDReport();
            objViewer.m_objfrmCardiogramReportManage = infrmCardiogramReportManage;
            objViewer.m_mthSetReport(objItem);
            objViewer.m_mthSetParentApperance(infrmCardiogramReportManage);

            objViewer.Show();
        }
        #endregion

        #region 获得动态心电图报告
        public void m_mthGetDCardiogramReportArr()
        {
            //			clsRIS_DCardiogramReport_VO[] objResultArr = null;
            //			m_objManage.m_mthGetDCardiogramReportArr(out objResultArr);
            //			if(objResultArr == null || objResultArr.Length == 0)
            //				return;
            //
            //			ListViewItem lviTemp = null;
            //
            //			m_objViewer.m_lsvDCardiogramReportList.Items.Clear();
            //
            //			for(int i1= 0 ;i1<objResultArr.Length;i1++)
            //			{
            //				lviTemp = new ListViewItem(objResultArr[i1].m_strREPORT_NO_CHR);
            //				lviTemp.SubItems.Add(objResultArr[i1].m_strPATIENT_NAME_VCHR);
            //				lviTemp.SubItems.Add(objResultArr[i1].m_strSEX_CHR);
            //				lviTemp.SubItems.Add(objResultArr[i1].m_fltAGE_FLT.ToString());
            //				lviTemp.SubItems.Add(objResultArr[i1].m_strPATIENT_NO_CHR);
            //				lviTemp.SubItems.Add(objResultArr[i1].m_strINPATIENT_NO_CHR);
            //				lviTemp.SubItems.Add(objResultArr[i1].m_strDEPT_NAME_VCHR);
            //				lviTemp.SubItems.Add(Convert.ToDateTime(objResultArr[i1].m_strREPORT_DAT).ToString("yyyy-MM-dd"));
            //				lviTemp.Tag = objResultArr[i1];
            //				m_objViewer.m_lsvDCardiogramReportList.Items.Add(lviTemp);
            //			}
        }
        #endregion

        #region 获得EEG报告
        public void m_mthGetEEGReportArr()
        {
            m_objViewer.m_lsvEEGReportList.Items.Clear();
            clsRIS_EEG_REPORT_VO[] objResultArr = null;
            m_objManage.m_mthGetEEGReportArr(out objResultArr);
            if (objResultArr == null || objResultArr.Length == 0)
                return;

            ListViewItem lviTemp = null;



            for (int i1 = 0; i1 < objResultArr.Length; i1++)
            {
                lviTemp = new ListViewItem(objResultArr[i1].m_strREPORT_NO_CHR);
                lviTemp.SubItems.Add(objResultArr[i1].m_strPATIENT_NAME_VCHR);
                lviTemp.SubItems.Add(objResultArr[i1].m_strSEX_CHR);
                lviTemp.SubItems.Add(m_mthAgeChange(objResultArr[i1].m_strAGE_FLT.ToString()));
                lviTemp.SubItems.Add(objResultArr[i1].m_strPATIENT_NO_CHR);
                lviTemp.SubItems.Add(objResultArr[i1].m_strINPATIENT_NO_CHR);
                lviTemp.SubItems.Add(objResultArr[i1].m_strDEPT_NAME_VCHR);
                lviTemp.SubItems.Add(Convert.ToDateTime(objResultArr[i1].m_strCHECK_DAT).ToString(/*"yyyy-MM-dd"*/));
                lviTemp.SubItems.Add(Convert.ToDateTime(objResultArr[i1].m_strREPORT_DAT).ToString(/*"yyyy-MM-dd"*/));
                lviTemp.Tag = objResultArr[i1];
                m_objViewer.m_lsvEEGReportList.Items.Add(lviTemp);
            }
        }
        #endregion
        #region 显示心电图报告
        public void m_mthShowEEGCardiogramReport(frmRISEEGReportNamage infrmCardiogramReportManage)
        {
            if (m_objViewer.m_lsvEEGReportList.Items.Count <= 0 ||
                m_objViewer.m_lsvEEGReportList.SelectedItems.Count <= 0)
                return;

            clsRIS_EEG_REPORT_VO objItem = (clsRIS_EEG_REPORT_VO)m_objViewer.m_lsvEEGReportList.SelectedItems[0].Tag;

            frmRISEEGReport objViewer = new frmRISEEGReport();
            objViewer.m_objfrmCardiogramReportManage = infrmCardiogramReportManage;

            objViewer.m_mthSetParentApperance(infrmCardiogramReportManage);
            objViewer.m_mthSetReport(objItem);
            objViewer.Show();
        }
        #endregion
        #region 显示根据条件组合查询的脑电图报告
        public void m_mthShowEEGReportByCondition(clsRIS_EEG_REPORT_VO[] p_objResultArr)
        {
            if (p_objResultArr == null || p_objResultArr.Length == 0)
                return;

            ListViewItem lviTemp = null;

            m_objViewer.m_lsvEEGReportList.Items.Clear();

            for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
            {
                lviTemp = new ListViewItem(p_objResultArr[i1].m_strREPORT_NO_CHR);
                lviTemp.SubItems.Add(p_objResultArr[i1].m_strPATIENT_NAME_VCHR);
                lviTemp.SubItems.Add(p_objResultArr[i1].m_strSEX_CHR);
                lviTemp.SubItems.Add(m_mthAgeChange(p_objResultArr[i1].m_strAGE_FLT.ToString()));
                lviTemp.SubItems.Add(p_objResultArr[i1].m_strPATIENT_NO_CHR);
                lviTemp.SubItems.Add(p_objResultArr[i1].m_strINPATIENT_NO_CHR);
                lviTemp.SubItems.Add(p_objResultArr[i1].m_strDEPT_NAME_VCHR);
                lviTemp.SubItems.Add(Convert.ToDateTime(p_objResultArr[i1].m_strCHECK_DAT).ToString(/*"yyyy-MM-dd"*/));
                lviTemp.SubItems.Add(Convert.ToDateTime(p_objResultArr[i1].m_strREPORT_DAT).ToString(/*"yyyy-MM-dd"*/));
                lviTemp.Tag = p_objResultArr[i1];
                m_objViewer.m_lsvEEGReportList.Items.Add(lviTemp);
            }
        }
        #endregion
        #region 显示根据条件组合查询的脑电图报告
        public void m_mthShowTCDReportByCondition(clsRIS_TCD_REPORT_VO[] p_objResultArr)
        {
            m_objViewer.m_lsvTCDReportList.Items.Clear();
            if (p_objResultArr == null || p_objResultArr.Length == 0)
                return;

            ListViewItem lviTemp = null;



            for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
            {
                lviTemp = new ListViewItem(p_objResultArr[i1].m_strREPORT_NO_CHR);
                lviTemp.SubItems.Add(p_objResultArr[i1].m_strPATIENT_NAME_VCHR);
                lviTemp.SubItems.Add(p_objResultArr[i1].m_strSEX_CHR);
                lviTemp.SubItems.Add(m_mthAgeChange(p_objResultArr[i1].m_strAGE_FLT.ToString()));
                lviTemp.SubItems.Add(p_objResultArr[i1].m_strPATIENT_NO_CHR);
                lviTemp.SubItems.Add(p_objResultArr[i1].m_strINPATIENT_NO_CHR);
                lviTemp.SubItems.Add(p_objResultArr[i1].m_strDEPT_NAME_VCHR);
                lviTemp.SubItems.Add(Convert.ToDateTime(p_objResultArr[i1].m_strCHECK_DAT).ToString(/*"yyyy-MM-dd"*/));
                lviTemp.SubItems.Add(Convert.ToDateTime(p_objResultArr[i1].m_strREPORT_DAT).ToString(/*"yyyy-MM-dd"*/));
                lviTemp.Tag = p_objResultArr[i1];
                m_objViewer.m_lsvTCDReportList.Items.Add(lviTemp);
            }
        }
        #endregion

        #region 年龄转换
        /// <summary>
        /// 年龄转换
        /// </summary>
        /// <param name="strage"></param>
        private string m_mthAgeChange(string strage)
        {
            int length = strage.Length;
            string strTextAge = "1";
            string strCmbAge = "年";
            strCmbAge = strage.Substring(0, 1);//年龄单位
            switch (strCmbAge.Trim())
            {
                case "C":
                    strCmbAge = "岁";
                    break;
                case "B":
                    strCmbAge = "月";
                    break;
                case "A":
                    strCmbAge = "天";
                    break;
            }
            strTextAge = strage.Substring(1, length - 1);
            return strTextAge + strCmbAge;
        }
        #endregion

        #region 是否显示没收费的申请人
        /// <summary>
        /// 是否显示没收费的申请人
        /// </summary>
        /// <param name="strage"></param>
        public void m_mthShowOrNot(string p_strsetid_chr, string p_strModuledid, out string p_strResult)
        {
            clsDomainController_RISCardiogramManage clsdomain = new clsDomainController_RISCardiogramManage();
            long lngres = clsdomain.m_strGetsetstatusFromt_sys_setting(p_strsetid_chr, p_strModuledid, out p_strResult);
            if (lngres < 0)
            {
                MessageBox.Show("取数据失败");
            }
        }
        #endregion

        #region 列表重新查询

        /// <summary>
        ///  列表重新查询
        /// </summary>
        /// <param name="p_strApplyId"></param>
        public void m_mthQueryReportNew(frmRISEEGReportNamage p_objfrmRISEEGReportNamage)
        {
            frmRISEEGReportNamage m_objViewer = p_objfrmRISEEGReportNamage;
            long lngRes = -1;
            if (frmRISEEGReportNamage.strOPQueryButtonName == "查询")
            {
                if (m_objViewer.m_tbcMain.SelectedIndex == 1)
                {
                    clsRIS_TCD_REPORT_VO[] objResultArr = null;
                    lngRes = m_objManage.m_lngGetTCDReportByCondition(m_objViewer.strFromDat1, m_objViewer.strToDat1, m_objViewer.strPatientNo1, m_objViewer.strInPatientNo1, m_objViewer.strPatientName1, m_objViewer.strDept1, m_objViewer.strReportNo1, m_objViewer.strReporter1
                    , out objResultArr);
                    if (lngRes > 0 && objResultArr != null)
                    {
                        if (objResultArr.Length > 0)
                        {
                            //  m_mthShowTCDReportByCondition(objResultArr);
                            #region bind lv
                            m_objViewer.m_lsvTCDReportList.Items.Clear();
                            if (objResultArr == null || objResultArr.Length == 0)
                                return;
                            ListViewItem lviTemp = null;
                            for (int i1 = 0; i1 < objResultArr.Length; i1++)
                            {
                                lviTemp = new ListViewItem(objResultArr[i1].m_strREPORT_NO_CHR);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strPATIENT_NAME_VCHR);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strSEX_CHR);
                                lviTemp.SubItems.Add(m_mthAgeChange(objResultArr[i1].m_strAGE_FLT.ToString()));
                                lviTemp.SubItems.Add(objResultArr[i1].m_strPATIENT_NO_CHR);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strINPATIENT_NO_CHR);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strDEPT_NAME_VCHR);
                                lviTemp.SubItems.Add(Convert.ToDateTime(objResultArr[i1].m_strCHECK_DAT).ToString(/*"yyyy-MM-dd"*/));
                                lviTemp.SubItems.Add(Convert.ToDateTime(objResultArr[i1].m_strREPORT_DAT).ToString(/*"yyyy-MM-dd"*/));
                                lviTemp.Tag = objResultArr[i1];
                                m_objViewer.m_lsvTCDReportList.Items.Add(lviTemp);
                            }
                            #endregion
                        }
                    }
                }
                if (m_objViewer.m_tbcMain.SelectedIndex == 2)
                {
                    clsRIS_EEG_REPORT_VO[] objResultArr = null;
                    lngRes = m_objManage.m_lngGetEEGReportByCondition(m_objViewer.strFromDat2, m_objViewer.strToDat2, m_objViewer.strPatientNo2, m_objViewer.strInPatientNo2, m_objViewer.strPatientName2, m_objViewer.strDept2, m_objViewer.strReportNo2, m_objViewer.strReporter2, out objResultArr);
                    if (lngRes > 0 && objResultArr != null)
                    {
                        if (objResultArr.Length > 0)
                        {
                            //m_mthShowEEGReportByCondition(objResultArr);
                            #region bind lv
                            if (objResultArr == null || objResultArr.Length == 0)
                                return;
                            ListViewItem lviTemp = null;
                            m_objViewer.m_lsvEEGReportList.Items.Clear();
                            for (int i1 = 0; i1 < objResultArr.Length; i1++)
                            {
                                lviTemp = new ListViewItem(objResultArr[i1].m_strREPORT_NO_CHR);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strPATIENT_NAME_VCHR);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strSEX_CHR);
                                lviTemp.SubItems.Add(m_mthAgeChange(objResultArr[i1].m_strAGE_FLT.ToString()));
                                lviTemp.SubItems.Add(objResultArr[i1].m_strPATIENT_NO_CHR);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strINPATIENT_NO_CHR);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strDEPT_NAME_VCHR);
                                lviTemp.SubItems.Add(Convert.ToDateTime(objResultArr[i1].m_strCHECK_DAT).ToString(/*"yyyy-MM-dd"*/));
                                lviTemp.SubItems.Add(Convert.ToDateTime(objResultArr[i1].m_strREPORT_DAT).ToString(/*"yyyy-MM-dd"*/));
                                lviTemp.Tag = objResultArr[i1];
                                m_objViewer.m_lsvEEGReportList.Items.Add(lviTemp);
                            }
                            #endregion
                        }
                    }
                }
            }
            else if (frmRISEEGReportNamage.strOPQueryButtonName == "当天")
            {
                try
                {
                    m_objViewer.m_cmdRefresh_Click(null, null);
                }
                catch
                {
                }
            }
        }
        #endregion
    }
}
