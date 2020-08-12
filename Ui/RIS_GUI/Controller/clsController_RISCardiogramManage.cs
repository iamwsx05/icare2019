using System;
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
    public class clsController_RISCardiogramManage : com.digitalwave.GUI_Base.clsController_Base
    {
        public clsController_RISCardiogramManage()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            m_objManage = new clsDomainController_RISCardiogramManage();
            m_strOperatorID = "0000001";
        }

        clsDomainController_RISCardiogramManage m_objManage = null;

        public string m_strOperatorID;
        //		private iCareData.clsApplyReportList_VO[] ReportArr;
        private clsApplyRecord[] ReportArr;
        #region 设置窗体对象

        com.digitalwave.iCare.gui.RIS.frmCardiogramReportManage m_objViewer;

        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            m_objViewer = (frmCardiogramReportManage)frmMDI_Child_Base_in;
        }
        #endregion

        #region 获得心电图报告
        /// <summary>
        /// 获得心电图报告
        /// </summary>
        public void m_mthGetCardiogramReportArr()
        {
            m_objViewer.m_lsvCardiogramReportList.Items.Clear();
            clsRIS_CardiogramReport_VO[] objResultArr = null;
            clsafmt_report_VO[] p_objResultArrSport = new clsafmt_report_VO[0];
            m_objManage.m_mthGetCardiogramReportArr(out objResultArr);
            if (objResultArr == null || objResultArr.Length == 0)
                return;

            ListViewItem lviTemp = null;

            //			m_objViewer.m_lsvCardiogramReportList.Items.Clear();

            for (int i1 = 0; i1 < objResultArr.Length; i1++)
            {

                lviTemp = new ListViewItem(objResultArr[i1].m_strREPORT_NO_CHR);
                lviTemp.SubItems.Add(objResultArr[i1].m_strPATIENT_NAME_VCHR);
                lviTemp.SubItems.Add(objResultArr[i1].m_strSEX_CHR);
                string strAge = objResultArr[i1].m_strAGE_FLT.ToString().Replace(" ", "");
                lviTemp.SubItems.Add(strAge);
                lviTemp.SubItems.Add(objResultArr[i1].m_strPATIENT_NO_CHR);
                lviTemp.SubItems.Add(objResultArr[i1].m_strINPATIENT_NO_CHR);
                lviTemp.SubItems.Add(objResultArr[i1].m_strDEPT_NAME_VCHR);
                lviTemp.SubItems.Add(objResultArr[i1].m_strSUMMARY2_VCHR.Trim());
                lviTemp.SubItems.Add(Convert.ToDateTime(objResultArr[i1].m_strREPORT_DAT).ToString());
                lviTemp.SubItems.Add("0");
                lviTemp.Tag = objResultArr[i1];
                if (objResultArr[i1].m_intIsSpicalPatient == 1)
                {
                    lviTemp.ForeColor = Color.Red;
                }
                m_objViewer.m_lsvCardiogramReportList.Items.Add(lviTemp);
            }
            m_objViewer.m_lsvCardiogramReportList.Refresh();
        }
        #endregion

        #region 获得平板运动数据
        /// <summary>
        /// 获得平板运动数据
        /// </summary>
        public void m_mthGetSportReportArr()
        {
            m_objViewer.lisvSport.Items.Clear();
            clsafmt_report_VO[] p_objResultArrSport = new clsafmt_report_VO[0];
            m_objManage.m_lngGetSportReportArr(out p_objResultArrSport);
            if (p_objResultArrSport == null || p_objResultArrSport.Length == 0)
                return;

            ListViewItem lviTemp = null;
            for (int i1 = 0; i1 < p_objResultArrSport.Length; i1++)
            {
                lviTemp = new ListViewItem(p_objResultArrSport[i1].m_strREPORT_NO_CHR);
                lviTemp.SubItems.Add(p_objResultArrSport[i1].m_strPATIENT_NAME_VCHR);
                lviTemp.SubItems.Add(p_objResultArrSport[i1].m_strSEX_CHR);
                string strAge = p_objResultArrSport[i1].m_strAGE_FLT.ToString().Replace(" ", "");
                lviTemp.SubItems.Add(strAge);
                lviTemp.SubItems.Add(p_objResultArrSport[i1].m_strPATIENT_NO_CHR);
                lviTemp.SubItems.Add(p_objResultArrSport[i1].m_strINPATIENT_NO_CHR);
                lviTemp.SubItems.Add(p_objResultArrSport[i1].m_strDEPT_NAME_VCHR);
                //lviTemp.SubItems.Add(p_objResultArrSport[i1].m_strACTIVE_RESULT_VCHR.Trim()+","+p_objResultArrSport[i1].m_strTEST_RESULT_VCHR.Trim());
                lviTemp.SubItems.Add(p_objResultArrSport[i1].m_strTEST_RESULT_VCHR.Trim());
                lviTemp.SubItems.Add(Convert.ToDateTime(p_objResultArrSport[i1].m_strREPORT_DAT).ToString(/*"yyyy-MM-dd"*/));
                lviTemp.SubItems.Add("1");
                lviTemp.Tag = p_objResultArrSport[i1];
                m_objViewer.lisvSport.Items.Add(lviTemp);
            }

        }


        #endregion

        #region 显示符合查询条件的活动平板运动心电图
        /// <summary>
        /// 显示符合查询条件的活动平板运动心电图
        /// </summary>
        public void m_mthGetSportReportArrFind(clsafmt_report_VO[] p_objResultArrSport)
        {
            m_objViewer.lisvSport.Items.Clear();
            ListViewItem lviTemp = null;
            for (int i1 = 0; i1 < p_objResultArrSport.Length; i1++)
            {
                lviTemp = new ListViewItem(p_objResultArrSport[i1].m_strREPORT_NO_CHR);
                lviTemp.SubItems.Add(p_objResultArrSport[i1].m_strPATIENT_NAME_VCHR);
                lviTemp.SubItems.Add(p_objResultArrSport[i1].m_strSEX_CHR);
                string strAge = p_objResultArrSport[i1].m_strAGE_FLT.ToString().Replace(" ", "");
                lviTemp.SubItems.Add(strAge);
                lviTemp.SubItems.Add(p_objResultArrSport[i1].m_strPATIENT_NO_CHR);
                lviTemp.SubItems.Add(p_objResultArrSport[i1].m_strINPATIENT_NO_CHR);
                lviTemp.SubItems.Add(p_objResultArrSport[i1].m_strDEPT_NAME_VCHR);
                //lviTemp.SubItems.Add(p_objResultArrSport[i1].m_strACTIVE_RESULT_VCHR.Trim()+","+p_objResultArrSport[i1].m_strTEST_RESULT_VCHR.Trim());
                lviTemp.SubItems.Add(p_objResultArrSport[i1].m_strTEST_RESULT_VCHR.Trim());
                lviTemp.SubItems.Add(Convert.ToDateTime(p_objResultArrSport[i1].m_strREPORT_DAT).ToString(/*"yyyy-MM-dd"*/));
                lviTemp.SubItems.Add("1");
                lviTemp.Tag = p_objResultArrSport[i1];
                m_objViewer.lisvSport.Items.Add(lviTemp);
            }

        }
        #endregion

        #region 显示心电图报告
        /// <summary>
        /// 显示心电图报告
        /// </summary>
        /// <param name="infrmCardiogramReportManage"></param>
        public void m_mthShowCardiogramReport(frmCardiogramReportManage infrmCardiogramReportManage)
        {
            if (m_objViewer.m_lsvCardiogramReportList.Items.Count <= 0 ||
                m_objViewer.m_lsvCardiogramReportList.SelectedItems.Count <= 0)
                return;
            clsRIS_CardiogramReport_VO objItem;
            objItem = (clsRIS_CardiogramReport_VO)m_objViewer.m_lsvCardiogramReportList.SelectedItems[0].Tag;

            frmCardiogramReport objViewer = new frmCardiogramReport();
            // objViewer.m_objMainFormManage = infrmCardiogramReportManage;
            objViewer.m_cmdSave.Tag = "OK";
            objViewer.m_strApplyID = objItem.m_intApplyID.ToString();
            objViewer.m_mthSetReport(objItem);
            objViewer.m_mthSetParentApperance(infrmCardiogramReportManage);
            objViewer.ShowDialog();
        }
        #endregion

        #region 刷新列表

        #endregion

        #region 新增心电图报告
        /// <summary>
        /// 新增心电图报告
        /// </summary>
        /// <param name="infrmCardiogramReportManage"></param>
        public void m_mthShowCardiogramReportAddNew(frmCardiogramReportManage infrmCardiogramReportManage)
        {
            clsApplyRecord objVO1 = (clsApplyRecord)m_objViewer.lisvAcct.SelectedItems[0].Tag;
            frmCardiogramReport objViewer1 = new frmCardiogramReport();
            objViewer1.m_strDoctorName = objVO1.m_strDoctorName;
            objViewer1.m_txtPATIENT_NAME_VCHR.Tag = "";
            objViewer1.m_txtREPORT_NO_CHR.Text = objVO1.m_strAreaID;
            objViewer1.m_txtINPATIENT_NO_CHR.Text = objVO1.m_strBIHNO;
            objViewer1.m_txtPATIENT_NO_CHR.Text = objVO1.m_strClinicNO;
            objViewer1.m_strApplyID = objVO1.m_strApplyID;
            objViewer1.m_txtApplyDoctor.txtValuse = objVO1.m_strDoctorName;
            objViewer1.m_txtApplyDoctor.Tag = objVO1.m_strDoctorID;
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
            objViewer1.m_txtDept.txtValuse = objVO1.m_strDepartment;
            objViewer1.m_txtDept.Tag = objVO1.m_strDeptID;
            objViewer1.m_txtBED_NO_CHR.Text = objVO1.m_strBedNO;
            #region    根据 卡号 检索病人ID
            objViewer1.carID.Text = objVO1.m_strCardNO;
            string m_strPatientCardID = objViewer1.carID.Text;
            string m_strInpatientNo = objViewer1.m_txtINPATIENT_NO_CHR.Text;
            DataTable m_objTabPatientInfo = new DataTable();

            if (m_strPatientCardID.Trim() != string.Empty)
            {
                m_objManage.m_lngGetPat(m_strPatientCardID, out m_objTabPatientInfo);
            }
            else if (m_strInpatientNo.Trim() != string.Empty)
            {
                m_objManage.m_lngGetPatientInfo(m_strInpatientNo, out m_objTabPatientInfo);
            }
            if (m_objTabPatientInfo.Rows.Count > 0)
            {
                objViewer1.m_txtPATIENT_NAME_VCHR.Tag = m_objTabPatientInfo.Rows[0]["PATIENTID_CHR"].ToString();
            }
            else
            {
                objViewer1.m_txtPATIENT_NAME_VCHR.Tag = string.Empty;
            }
            # endregion
            objViewer1.m_cmdSave.Tag = "NO";
            objViewer1.m_mthSetParentApperance(infrmCardiogramReportManage);
            objViewer1.Show();

        }
        #endregion

        #region 新增动态心电图报告
        /// <summary>
        /// 新增动态心电图报告
        /// </summary>
        /// <param name="infrmCardiogramReportManage"></param>
        public void m_mthShowCardiogramReportSportAddNew(frmCardiogramReportManage infrmCardiogramReportManage)
        {
            clsApplyRecord objVO1 = (clsApplyRecord)m_objViewer.lisvAcct.SelectedItems[0].Tag;
            frmDnmCardiogramReport objViewer1 = new frmDnmCardiogramReport();

            objViewer1.m_txtPATIENT_NAME_VCHR.Tag = "";
            objViewer1.m_txtREPORT_NO_CHR.Text = objVO1.m_strAreaID;
            objViewer1.m_txtINPATIENT_NO_CHR.Text = objVO1.m_strBIHNO;
            objViewer1.m_txtPATIENT_NO_CHR.Text = objVO1.m_strClinicNO;
            objViewer1.m_strApplyID = objVO1.m_strApplyID;
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
                lng = m_objManage.m_lngGetPat(objViewer1.carID.Text, out tbPat);
                if (lng > 0)
                    objViewer1.m_txtPATIENT_NAME_VCHR.Tag = tbPat.Rows[0]["PATIENTID_CHR"].ToString();
            }
            # endregion
            objViewer1.SetParentApperance(infrmCardiogramReportManage);
            objViewer1.Show();

        }
        #endregion

        #region 新增平板运动心电图报告
        /// <summary>
        /// 新增平板运动心电图报告
        /// </summary>
        /// <param name="infrmCardiogramReportManage"></param>
        public void m_mthShowCardiogramReportPenBanSportAddNew(frmCardiogramReportManage infrmCardiogramReportManage)
        {
            //			clsApplyReportList_VO objVO = (clsApplyReportList_VO)m_objViewer.lisvAcct.SelectedItems[0].Tag;
            //			frmFlatAndSportReport objViewer2 = new frmFlatAndSportReport();
            //			objViewer2.m_txtPATIENT_NAME_VCHR.Tag = objVO.m_StrPatientID;
            //			objViewer2.m_txtREPORT_NO_CHR.Text=objVO.m_strRecordID;
            //			//objViewer2.m_txtPATIENT_NO_CHR.Text=objVO.m_StrPatientCardID;
            //			objViewer2.m_txtINPATIENT_NO_CHR.Text=objVO.m_StrInPatientID;
            //			if(objVO.m_StrPatientAge.Length > 0)
            //			{
            //				try
            //				{
            //					objViewer2.m_txtAGE_FLT.Text=int.Parse(objVO.m_StrPatientAge).ToString();
            //				}
            //				catch
            //				{
            //					objViewer2.m_txtAGE_FLT.Text=objVO.m_StrPatientAge.Substring(0,objVO.m_StrPatientAge.Length -1);
            //					switch(objVO.m_StrPatientAge.Substring(objVO.m_StrPatientAge.Length -1,1))
            //					{
            //						case "日":
            //							objViewer2.m_cmbAge.Text = "天";
            //							break;
            //						case "岁":
            //							objViewer2.m_cmbAge.Text = "年";
            //							break;
            //						case "月":
            //							objViewer2.m_cmbAge.Text = "月";
            //							break;
            //					}
            //				}
            //			}
            //			
            //			objViewer2.m_txtPATIENT_NAME_VCHR.Text=objVO.m_StrPatientName;
            //			objViewer2.m_cboSEX_CHR.Text=objVO.m_StrPatientSex;
            //			objViewer2.m_txtDEPT_NAME_VCHR.Text=objVO.m_StrDeptName;
            //			objViewer2.m_txtBED_NO_CHR.Text=objVO.m_StrBedName;
            //			objViewer2.carID.Text = objVO.m_StrPatientCardID;
            //
            ////				DataRow AddRow=dtAcct.NewRow();
            ////				AddRow = (DataRow)m_objViewer.lisvAcct.SelectedItems[0].Tag;
            ////				frmFlatAndSportReport objViewer3 = new frmFlatAndSportReport();
            ////				if(AddRow["病人卡号ID"].ToString()!="")
            ////					objViewer3.carID.Text=AddRow["病人卡号ID"].ToString();
            ////			        objViewer3.m_txtPATIENT_NAME_VCHR.Tag=AddRow["病人ID"].ToString();
            ////				foreach(DataColumn dc in dtAcct.Columns)
            ////				{
            ////					for(int i1=0;i1<arIndexNameValues.Count/2;i1++)
            ////					{
            ////						if(arIndexNameValues[i1+i1].ToString().Trim()==dc.ColumnName.Trim())
            ////						{
            ////							
            ////							if(arIndexNameValues[i1+1].ToString().Trim()=="m_txtREPORT_NO_CHR")
            ////							{
            ////								objViewer3.m_txtREPORT_NO_CHR.Text=AddRow[arIndexNameValues[i1].ToString()].ToString();
            ////							}
            ////							if(arIndexNameValues[i1+1].ToString().Trim()=="m_txtPATIENT_NO_CHR")
            ////							{
            ////								objViewer3.m_txtPATIENT_NO_CHR.Text=AddRow[arIndexNameValues[i1].ToString()].ToString();
            ////							}
            ////							if(arIndexNameValues[i1+1].ToString().Trim()=="m_txtINPATIENT_NO_CHR")
            ////							{
            ////								objViewer3.m_txtINPATIENT_NO_CHR.Text=AddRow[arIndexNameValues[i1].ToString()].ToString();
            ////							}
            ////							if(arIndexNameValues[i1+1].ToString().Trim()=="m_txtAGE_FLT")
            ////							{
            ////								objViewer3.m_txtAGE_FLT.Text=AddRow[arIndexNameValues[i1].ToString()].ToString();
            ////							}
            ////							if(arIndexNameValues[i1+1].ToString().Trim()=="m_txtPATIENT_NAME_VCHR")
            ////							{
            ////								objViewer3.m_txtPATIENT_NAME_VCHR.Text=AddRow[arIndexNameValues[i1].ToString()].ToString();
            ////							}
            ////							if(arIndexNameValues[i1+1].ToString().Trim()=="m_cboSEX_CHR")
            ////							{
            ////								objViewer3.m_cboSEX_CHR.Text=AddRow[arIndexNameValues[i1].ToString()].ToString();
            ////							}
            ////							if(arIndexNameValues[i1+1].ToString().Trim()=="m_txtDEPT_NAME_VCHR")
            ////							{
            ////								objViewer3.m_txtDEPT_NAME_VCHR.Text=AddRow[arIndexNameValues[i1].ToString()].ToString();
            ////							}
            ////							if(arIndexNameValues[i1+1].ToString().Trim()=="m_txtBED_NO_CHR")
            ////							{
            ////								objViewer3.m_txtBED_NO_CHR.Text=AddRow[arIndexNameValues[i1].ToString()].ToString();
            ////							}
            ////						}
            ////					}
            //				
            //				objViewer2.m_mthSetParentApperance(infrmCardiogramReportManage);
            //				objViewer2.Show();

            clsApplyRecord objVO1 = (clsApplyRecord)m_objViewer.lisvAcct.SelectedItems[0].Tag;
            frmFlatAndSportReport objViewer1 = new frmFlatAndSportReport();

            objViewer1.m_txtPATIENT_NAME_VCHR.Tag = "";
            objViewer1.m_txtREPORT_NO_CHR.Text = objVO1.m_strAreaID;
            objViewer1.m_txtINPATIENT_NO_CHR.Text = objVO1.m_strBIHNO;
            objViewer1.m_txtPATIENT_NO_CHR.Text = objVO1.m_strClinicNO;
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
                lng = m_objManage.m_lngGetPat(objViewer1.carID.Text, out tbPat);
                if (lng > 0)
                    objViewer1.m_txtPATIENT_NAME_VCHR.Tag = tbPat.Rows[0]["PATIENTID_CHR"].ToString();
            }
            # endregion
            objViewer1.m_mthSetParentApperance(infrmCardiogramReportManage);
            objViewer1.Show();

        }
        #endregion

        #region 显示心电图报告(运动报告）
        /// <summary>
        /// 显示心电图报告(运动报告）
        /// </summary>
        /// <param name="infrmCardiogramReportManage"></param>
        public void m_mthShowSportReport(frmCardiogramReportManage infrmCardiogramReportManage)
        {
            if (m_objViewer.lisvSport.Items.Count <= 0 ||
                m_objViewer.lisvSport.SelectedItems.Count <= 0)
                return;
            clsafmt_report_VO objItem;

            objItem = (clsafmt_report_VO)m_objViewer.lisvSport.SelectedItems[0].Tag;
            frmFlatAndSportReport objViewer = new frmFlatAndSportReport();
            // objViewer.m_objMainFormManage = infrmCardiogramReportManage;
            objViewer.m_cheIsNew.Checked = true;
            objViewer.m_cheIsNew.Tag = objItem.m_strREPORT_ID_CHR;
            objViewer.m_mthSetReport(objItem);
            objViewer.m_mthSetParentApperance(infrmCardiogramReportManage);
            objViewer.Show();
        }
        #endregion

        #region 显示根据条件组合查询的心电图报告
        public void m_mthShowCardiogramReportByCondition(clsRIS_CardiogramReport_VO[] p_objResultArr)
        {
            if (p_objResultArr == null || p_objResultArr.Length == 0)
                return;

            ListViewItem lviTemp = null;

            m_objViewer.m_lsvCardiogramReportList.Items.Clear();

            for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
            {
                lviTemp = new ListViewItem(p_objResultArr[i1].m_strREPORT_NO_CHR);
                lviTemp.SubItems.Add(p_objResultArr[i1].m_strPATIENT_NAME_VCHR);
                lviTemp.SubItems.Add(p_objResultArr[i1].m_strSEX_CHR);
                string strAge = p_objResultArr[i1].m_strAGE_FLT.ToString().Replace(" ", "");
                lviTemp.SubItems.Add(strAge);
                //				lviTemp.SubItems.Add(p_objResultArr[i1].m_strAGE_FLT.ToString());
                lviTemp.SubItems.Add(p_objResultArr[i1].m_strPATIENT_NO_CHR);
                lviTemp.SubItems.Add(p_objResultArr[i1].m_strINPATIENT_NO_CHR);
                lviTemp.SubItems.Add(p_objResultArr[i1].m_strDEPT_NAME_VCHR);
                lviTemp.SubItems.Add(p_objResultArr[i1].m_strSUMMARY2_VCHR.Trim());
                lviTemp.SubItems.Add(Convert.ToDateTime(p_objResultArr[i1].m_strREPORT_DAT).ToString(/*"yyyy-MM-dd"*/));
                lviTemp.Tag = p_objResultArr[i1];
                if (p_objResultArr[i1].m_intIsSpicalPatient == 1)
                {
                    lviTemp.ForeColor = Color.Red;
                }

                m_objViewer.m_lsvCardiogramReportList.Items.Add(lviTemp);
            }
        }
        #endregion

        #region 显示根据条件组合查询的动态心电图报告
        /// <summary>
        /// 显示根据条件组合查询的动态心电图报告
        /// </summary>
        /// <param name="p_objResultArr"></param>
        public void m_mthShowDCardiogramReportByCondition(clsRIS_DCardiogramReport_VO[] p_objResultArr)
        {
            if (p_objResultArr == null || p_objResultArr.Length == 0)
                return;

            ListViewItem lviTemp = null;

            m_objViewer.m_lsvDCardiogramReportList.Items.Clear();

            for (int i1 = 0; i1 < p_objResultArr.Length; i1++)
            {
                lviTemp = new ListViewItem(p_objResultArr[i1].m_strREPORT_NO_CHR);
                lviTemp.SubItems.Add(p_objResultArr[i1].m_strPATIENT_NAME_VCHR);
                lviTemp.SubItems.Add(p_objResultArr[i1].m_strSEX_CHR);
                string strAge = p_objResultArr[i1].m_strAGE_FLT.ToString().Replace(" ", "");
                lviTemp.SubItems.Add(strAge);
                //				lviTemp.SubItems.Add(p_objResultArr[i1].m_strAGE_FLT.ToString());
                lviTemp.SubItems.Add(p_objResultArr[i1].m_strPATIENT_NO_CHR);
                lviTemp.SubItems.Add(p_objResultArr[i1].m_strINPATIENT_NO_CHR);
                lviTemp.SubItems.Add(p_objResultArr[i1].m_strDEPT_NAME_VCHR);
                lviTemp.SubItems.Add(p_objResultArr[i1].m_strSUMMARY2_VCHR);
                lviTemp.SubItems.Add(Convert.ToDateTime(p_objResultArr[i1].m_strREPORT_DAT).ToString(/*"yyyy-MM-dd"*/));
                lviTemp.Tag = p_objResultArr[i1];
                if (p_objResultArr[i1].m_intIsSpicalPatient == 1)
                {
                    lviTemp.ForeColor = Color.Red;
                }
                m_objViewer.m_lsvDCardiogramReportList.Items.Add(lviTemp);
            }
        }
        #endregion

        #region 获得动态心电图报告
        public void m_mthGetDCardiogramReportArr()
        {
            m_objViewer.m_lsvDCardiogramReportList.Items.Clear();

            clsRIS_DCardiogramReport_VO[] objResultArr = null;
            m_objManage.m_mthGetDCardiogramReportArr(out objResultArr);
            if (objResultArr == null || objResultArr.Length == 0)
                return;

            ListViewItem lviTemp = null;


            for (int i1 = 0; i1 < objResultArr.Length; i1++)
            {
                lviTemp = new ListViewItem(objResultArr[i1].m_strREPORT_NO_CHR);
                lviTemp.SubItems.Add(objResultArr[i1].m_strPATIENT_NAME_VCHR);
                lviTemp.SubItems.Add(objResultArr[i1].m_strSEX_CHR);
                string strAge = objResultArr[i1].m_strAGE_FLT.ToString().Replace(" ", "");
                lviTemp.SubItems.Add(strAge);
                //				lviTemp.SubItems.Add(objResultArr[i1].m_strAGE_FLT.ToString());
                lviTemp.SubItems.Add(objResultArr[i1].m_strPATIENT_NO_CHR);
                lviTemp.SubItems.Add(objResultArr[i1].m_strINPATIENT_NO_CHR);
                lviTemp.SubItems.Add(objResultArr[i1].m_strDEPT_NAME_VCHR);
                lviTemp.SubItems.Add(objResultArr[i1].m_strSUMMARY2_VCHR);
                lviTemp.SubItems.Add(Convert.ToDateTime(objResultArr[i1].m_strREPORT_DAT).ToString(/*"yyyy-MM-dd"*/));
                lviTemp.Tag = objResultArr[i1];
                if (objResultArr[i1].m_intIsSpicalPatient == 1)
                {
                    lviTemp.ForeColor = Color.Red;
                }
                m_objViewer.m_lsvDCardiogramReportList.Items.Add(lviTemp);
            }
        }
        #endregion

        #region 显示动态心电图报告
        public void m_mthShowDCardiogramReport(frmCardiogramReportManage infrmCardiogramReportManage)
        {
            if (m_objViewer.m_lsvDCardiogramReportList.Items.Count <= 0 ||
                m_objViewer.m_lsvDCardiogramReportList.SelectedItems.Count <= 0)
                return;

            clsRIS_DCardiogramReport_VO objItem = (clsRIS_DCardiogramReport_VO)m_objViewer.m_lsvDCardiogramReportList.SelectedItems[0].Tag;


            frmDnmCardiogramReport objViewer = new frmDnmCardiogramReport();
            //objViewer.m_objMainFormManage = infrmCardiogramReportManage;
            objViewer.m_mthSetReport(objItem);
            objViewer.SetParentApperance(infrmCardiogramReportManage);

            objViewer.Show();
        }
        #endregion

        #region 获取心电图申请数据
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
        public void m_mthShowAcctData(clsApplyRecord[] ReportArr)
        {
            if (ReportArr == null || ReportArr.Length == 0)
                return;
            this.m_objViewer.lisvAcct.Items.Clear();
            string strResult = "";
            new clsController_RISEEGManage().m_mthShowOrNot("8004", "0008", out strResult);
            string strModuleResult = this.m_objComInfo.m_lonGetModuleInfo("8007");
            this.m_objViewer.lisvAcct.BeginUpdate();
            for (int i1 = 0; i1 < ReportArr.Length; i1++)
            {

                ListViewItem item = new ListViewItem(ReportArr[i1].m_strCardNO);
                item.SubItems.Add(ReportArr[i1].m_strName);
                item.SubItems.Add(ReportArr[i1].m_strSex);
                item.SubItems.Add(ReportArr[i1].m_strAge);
                item.SubItems.Add(ReportArr[i1].m_strDepartment);
                item.SubItems.Add(ReportArr[i1].m_strBedNO);
                item.SubItems.Add(ReportArr[i1].m_strBIHNO);
                item.SubItems.Add(ReportArr[i1].m_datApplyDate.ToString());
                item.SubItems.Add("");
                if (ReportArr[i1].m_strStatus_int1 != "1" || ReportArr[i1].m_intChargeStatus == 3)
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
                            if (strResult == "0")//不显示未缴费
                                continue;
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
                {
                    if (ReportArr[i1].m_intIsGreen == 1)
                    {
                        item.SubItems.Add("先诊疗后结算");
                    }
                    else
                    {
                        item.SubItems.Add("已缴费");
                    }
                }
                item.SubItems.Add(ReportArr[i1].m_strApplyTitle);
                item.SubItems.Add("");
                item.Tag = ReportArr[i1];
                if (ReportArr[i1].m_strSTATUS_INT.Trim() == "2")
                {
                    item.ForeColor = Color.White;
                    item.BackColor = Color.DarkGreen;
                }

                if (ReportArr[i1].m_intIsGreen == 1)
                {
                    item.BackColor = Color.Orange;
                }

                this.m_objViewer.lisvAcct.Items.Add(item);
            }
            this.m_objViewer.lisvAcct.EndUpdate();
        }
        public void m_lngGetAcctData(DateTime m_dtBegin, DateTime m_dtEnd)
        {
            string typeID = null;
            m_objManage.m_mthGetApplTypeID(out typeID);
            com.digitalwave.GLS_WS.clsApplyForm Aps = new com.digitalwave.GLS_WS.clsApplyForm();

            if (typeID != null)
                ReportArr = Aps.GetApplyRecordByDate(DateTime.Parse(m_dtBegin.ToString("yyyy-MM-dd 00:00:00")), DateTime.Parse(m_dtEnd.ToString("yyyy-MM-dd 23:59:59")), typeID);

            if (ReportArr == null || ReportArr.Length == 0)
                return;
            this.m_objViewer.lisvAcct.Items.Clear();
            string strResult = "";
            new clsController_RISEEGManage().m_mthShowOrNot("8004", "0008", out strResult);
            string strModuleResult = this.m_objComInfo.m_lonGetModuleInfo("8007");
            this.m_objViewer.lisvAcct.BeginUpdate();
            for (int i1 = 0; i1 < ReportArr.Length; i1++)
            {
                com.digitalwave.iCare.common.clsCheckChargeInfo checkInfo = new com.digitalwave.iCare.common.clsCheckChargeInfo();
                //bool blIsCheck = checkInfo.m_mthCheckIsCharge(ReportArr[i1].m_strApplyID, ApplyOrigin.PACSS);
                ListViewItem item = new ListViewItem(ReportArr[i1].m_strCardNO);
                item.SubItems.Add(ReportArr[i1].m_strName);
                item.SubItems.Add(ReportArr[i1].m_strSex);
                item.SubItems.Add(ReportArr[i1].m_strAge);
                item.SubItems.Add(ReportArr[i1].m_strDepartment);
                item.SubItems.Add(ReportArr[i1].m_strBedNO);
                item.SubItems.Add(ReportArr[i1].m_strBIHNO);
                item.SubItems.Add(ReportArr[i1].m_datApplyDate.ToString());
                item.SubItems.Add("");
                if (ReportArr[i1].m_strStatus_int1 != "1" || ReportArr[i1].m_intChargeStatus == 3)
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
                            if (strResult == "0")//不显示未缴费
                                continue;
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
                {
                    if (ReportArr[i1].m_intIsGreen == 1)
                    {
                        item.SubItems.Add("先诊疗后结算");
                    }
                    else
                    {
                        item.SubItems.Add("已缴费");
                    }
                }
                item.SubItems.Add(ReportArr[i1].m_strApplyTitle);
                item.SubItems.Add("");
                item.Tag = ReportArr[i1];
                if (ReportArr[i1].m_strSTATUS_INT.Trim() == "2")
                {
                    item.ForeColor = Color.White;
                    item.BackColor = Color.DarkGreen;
                }

                if (ReportArr[i1].m_intIsGreen == 1)
                {
                    item.BackColor = Color.Orange;
                }

                this.m_objViewer.lisvAcct.Items.Add(item);
            }
            this.m_objViewer.lisvAcct.EndUpdate();
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
            //						if(p_objSyncInfoArr[i1].m_objTarget_GUI.m_strTarget_Form_Desc.Trim()=="心电图报告单"||p_objSyncInfoArr[i1].m_objTarget_GUI.m_strTarget_Form_Desc.Trim()=="动态心电图报告单"||p_objSyncInfoArr[i1].m_objTarget_GUI.m_strTarget_Form_Desc.Trim()=="平板运动心电图报告单")
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
            //											    arIndexNameValues.Add(objReader.GetAttribute("CONTROLID"));
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
            //									dtAcct.Columns.Add(arIndexNameValues[k4+k4].ToString());
            //								}
            //							}
            //							catch
            //							{
            //							}
            //							DataRow newRow=dtAcct.NewRow();
            //							newRow["申请日期"]=p_objSyncInfoArr[i1].m_dtmCreatedDate;
            //							newRow["病人ID"]=p_objSyncInfoArr[i1].m_strPatientID;
            //							newRow["申请类型"]=p_objSyncInfoArr[i1].m_objTarget_GUI.m_strTarget_Form_Desc;
            //							newRow["病人卡号ID"]=p_objSyncInfoArr[i1].m_strPatientCardID;
            //							for(int h3=0;h3<arIndexName.Count;h3++)
            //							{
            //								for(int p1=0;p1<arValues.Count;p1++)
            //								{
            //									if(arIndexName[h3].ToString().Trim()==arIndexValues[p1].ToString().Trim())
            //									{
            //			
            //											newRow[arIndexNameValues[h3+h3].ToString()]=arValues[p1].ToString();
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
            //						  newItem.SubItems.Add(dtAcct.Rows[t1][k8].ToString());
            //					}
            //					newItem.Tag=dtAcct.Rows[t1];
            //					this.m_objViewer.lisvAcct.Items.Add(newItem);
            //				}
            //			}
            #endregion
        }
        #endregion

        #region 打开申请单
        /// <summary>
        ///  打开申请单
        /// </summary>
        /// <param name="p_strApplyId"></param>
        public void m_mthOpenApplyFormByApplyId(string p_strApplyId)
        {
            if (p_strApplyId != "")
            {
                com.digitalwave.GLS_WS.clsApplyForm objfrm2 = new com.digitalwave.GLS_WS.clsApplyForm();
                objfrm2.m_intHeartType = 1;
                objfrm2.OpenForm(p_strApplyId);
            }
        }
        #endregion

        #region 打开心电图申请单
        /// <summary>
        ///  打开心电图申请单
        /// </summary>
        /// <param name="p_strApplyId"></param>
        public void m_mthOpenHeartApplyFormByApplyId(ListView p_lsv)
        {
            if (p_lsv.SelectedItems.Count > 0)
            {
                clsApplyRecord clsApp = (clsApplyRecord)p_lsv.SelectedItems[0].Tag;
                m_mthOpenApplyFormByApplyId(clsApp.m_strApplyID);
            }
            else
            {
                MessageBox.Show("请在列表中选检查申请记录");
            }
        }
        #endregion

        #region 列表重新查询

        /// <summary>
        ///  列表重新查询
        /// </summary>
        /// <param name="p_strApplyId"></param>
        public void m_mthQueryReportNew(frmCardiogramReportManage p_objfrmCardiogramReportManage)
        {
            frmCardiogramReportManage m_objViewer = p_objfrmCardiogramReportManage;
            long lngRes = -1;
            if (frmCardiogramReportManage.strOPQueryButtonName == "查询")
            {
                if (m_objViewer.m_tbcMain.SelectedIndex == 1)
                {
                    clsRIS_CardiogramReport_VO[] objResultArr = null;
                    lngRes = m_objManage.m_lngGetCardiogramReportByCondition(m_objViewer.strFromDat1, m_objViewer.strToDat1, m_objViewer.strPatientNo1, m_objViewer.strInPatientNo1, m_objViewer.strPatientName1, m_objViewer.strDept1, m_objViewer.strReportNo1
                    , m_objViewer.strIsSpecail1, m_objViewer.strReporter1, out objResultArr);
                    if (lngRes > 0 && objResultArr != null)
                    {
                        if (objResultArr.Length > 0)
                        {
                            //m_mthShowCardiogramReportByCondition(objResultArr);
                            #region 显示根据条件组合查询的心电图报告
                            if (objResultArr == null || objResultArr.Length == 0)
                                return;
                            ListViewItem lviTemp = null;
                            m_objViewer.m_lsvCardiogramReportList.Items.Clear();
                            for (int i1 = 0; i1 < objResultArr.Length; i1++)
                            {
                                lviTemp = new ListViewItem(objResultArr[i1].m_strREPORT_NO_CHR);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strPATIENT_NAME_VCHR);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strSEX_CHR);
                                string strAge = objResultArr[i1].m_strAGE_FLT.ToString().Replace(" ", "");
                                lviTemp.SubItems.Add(strAge);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strPATIENT_NO_CHR);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strINPATIENT_NO_CHR);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strDEPT_NAME_VCHR);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strSUMMARY2_VCHR.Trim());
                                lviTemp.SubItems.Add(Convert.ToDateTime(objResultArr[i1].m_strREPORT_DAT).ToString(/*"yyyy-MM-dd"*/));
                                lviTemp.Tag = objResultArr[i1];
                                if (objResultArr[i1].m_intIsSpicalPatient == 1)
                                {
                                    lviTemp.ForeColor = Color.Red;
                                }
                                m_objViewer.m_lsvCardiogramReportList.Items.Add(lviTemp);
                            }
                            #endregion
                        }
                    }
                }
                if (m_objViewer.m_tbcMain.SelectedIndex == 2)
                {
                    clsRIS_DCardiogramReport_VO[] objResultArr = null;
                    lngRes = m_objManage.m_lngGetDCardiogramReportByCondition(m_objViewer.strFromDat2, m_objViewer.strToDat2, m_objViewer.strPatientNo2, m_objViewer.strInPatientNo2, m_objViewer.strPatientName2, m_objViewer.strDept2, m_objViewer.strReportNo2, m_objViewer.strIsSpecail2
                        , m_objViewer.strReporter2, out objResultArr);
                    if (lngRes > 0 && objResultArr != null)
                    {
                        if (objResultArr.Length > 0)
                        {
                            //m_mthShowDCardiogramReportByCondition(objResultArr);
                            #region 显示根据条件组合查询的动态心电图报告
                            if (objResultArr == null || objResultArr.Length == 0)
                                return;
                            ListViewItem lviTemp = null;
                            m_objViewer.m_lsvDCardiogramReportList.Items.Clear();
                            for (int i1 = 0; i1 < objResultArr.Length; i1++)
                            {
                                lviTemp = new ListViewItem(objResultArr[i1].m_strREPORT_NO_CHR);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strPATIENT_NAME_VCHR);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strSEX_CHR);
                                string strAge = objResultArr[i1].m_strAGE_FLT.ToString().Replace(" ", "");
                                lviTemp.SubItems.Add(strAge);
                                //				lviTemp.SubItems.Add(p_objResultArr[i1].m_strAGE_FLT.ToString());
                                lviTemp.SubItems.Add(objResultArr[i1].m_strPATIENT_NO_CHR);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strINPATIENT_NO_CHR);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strDEPT_NAME_VCHR);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strSUMMARY2_VCHR);
                                lviTemp.SubItems.Add(Convert.ToDateTime(objResultArr[i1].m_strREPORT_DAT).ToString(/*"yyyy-MM-dd"*/));
                                lviTemp.Tag = objResultArr[i1];
                                if (objResultArr[i1].m_intIsSpicalPatient == 1)
                                {
                                    lviTemp.ForeColor = Color.Red;
                                }
                                m_objViewer.m_lsvDCardiogramReportList.Items.Add(lviTemp);
                            }
                            #endregion
                        }
                    }
                }
                if (m_objViewer.m_tbcMain.SelectedIndex == 3)
                {
                    clsafmt_report_VO[] objResultArr = null;
                    lngRes = new clsDomainController_RISCardiogramManage().m_lngGetSportReportByCondition(m_objViewer.strFromDat3, m_objViewer.strToDat3, m_objViewer.strPatientNo3, m_objViewer.strInPatientNo3, m_objViewer.strPatientName3, m_objViewer.strDept3, m_objViewer.strReportNo3, m_objViewer.strIsSpecail3
                        , m_objViewer.strReporter3, out objResultArr);
                    if (lngRes > 0 && objResultArr != null)
                    {
                        if (objResultArr.Length > 0)
                        {
                            //   m_mthGetSportReportArrFind(objResultArr);
                            #region 显示符合查询条件的活动平板运动心电图
                            m_objViewer.lisvSport.Items.Clear();
                            ListViewItem lviTemp = null;
                            for (int i1 = 0; i1 < objResultArr.Length; i1++)
                            {
                                lviTemp = new ListViewItem(objResultArr[i1].m_strREPORT_NO_CHR);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strPATIENT_NAME_VCHR);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strSEX_CHR);
                                string strAge = objResultArr[i1].m_strAGE_FLT.ToString().Replace(" ", "");
                                lviTemp.SubItems.Add(strAge);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strPATIENT_NO_CHR);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strINPATIENT_NO_CHR);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strDEPT_NAME_VCHR);
                                lviTemp.SubItems.Add(objResultArr[i1].m_strACTIVE_RESULT_VCHR.Trim() + "," + objResultArr[i1].m_strTEST_RESULT_VCHR.Trim());
                                lviTemp.SubItems.Add(objResultArr[i1].m_strTEST_RESULT_VCHR.Trim());
                                lviTemp.SubItems.Add(Convert.ToDateTime(objResultArr[i1].m_strREPORT_DAT).ToString(/*"yyyy-MM-dd"*/));
                                lviTemp.SubItems.Add("1");
                                lviTemp.Tag = objResultArr[i1];
                                m_objViewer.lisvSport.Items.Add(lviTemp);
                            }
                            #endregion
                        }
                    }
                }
            }
            else if (frmCardiogramReportManage.strOPQueryButtonName == "当天")
            {
                m_objViewer.m_cmdRefresh_Click(null, null);
            }
        }
        #endregion
    }
}
