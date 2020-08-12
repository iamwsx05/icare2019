using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using com.digitalwave.emr.EMR_SynchronousCase;
using com.digitalwave.Emr.Signature_gui;
using weCare.Core.Entity;

namespace com.digitalwave.emr.EMR_SynchronousCase.EditCaseControl
{
    /// <summary>
    /// 病人基本资料
    /// </summary>
    public partial class ctlPatientInfo : UserControl, infSynchronousCaseControl
    {
        /// <summary>
        /// 签名工具类
        /// </summary>
        private clsEmrSignToolCollection m_objSign = null;
        /// <summary>
        /// 入院登记号
        /// </summary>
        private string m_strRegisterID = string.Empty;
        /// <summary>
        /// 基础字典
        /// </summary>
        private DataTable m_dtbDict = null;

        /// <summary>
        /// 病人基本资料
        /// </summary>
        public ctlPatientInfo()
        {
            InitializeComponent();
            //入院诊断删除
            //m_dgwInDiagnosis.AutoGenerateColumns = false;
            
            m_objSign = new clsEmrSignToolCollection();
            m_objSign.m_mthBindEmployeeSign(m_cmdDoctor, txtDoctor, 1, false);
        }

        #region infSynchronousCaseControl 成员
        /// <summary>
        /// 是否已初始化
        /// </summary>
        private bool m_blnHasInit = false;
        /// <summary>
        /// 是否已初始化
        /// </summary>
        public bool m_BlnHasInit
        {
            get
            {
                return m_blnHasInit;
            }
            set
            {
                m_blnHasInit = value;
            }
        }

        #region 初始化病案内容
        /// <summary>
        /// 初始化病案内容
        /// </summary>
        /// <param name="p_dsContent">数据库中获取的已保存数据</param>
        public void m_mthInitCase(DataSet p_dsContent)
        {
            if (p_dsContent == null)
            {
                return;
            }

            DataTable dtbICD = new DataTable();
            dtbICD.Columns.Add("name");
            dtbICD.Columns.Add("code");
            dtbICD.Columns.Add("outinfo");
            //m_dgwInDiagnosis.DataSource = dtbICD;

            string strFirstTransDeptName = string.Empty;
            if (p_dsContent.Tables.Contains("HIS_BA1") && p_dsContent.Tables["HIS_BA1"].Rows.Count > 0)
            {
                DataRow drBA1 = p_dsContent.Tables["HIS_BA1"].Rows[0];
                if (drBA1["FFBBH"] != DBNull.Value)
                {
                    for (int iI = 0; iI < m_cboPayType.Items.Count; iI++)
                    {
                        if (((clsGDCaseDictVO)m_cboPayType.Items[iI]).m_strCode == drBA1["FFBBH"].ToString())
                        {
                            m_cboPayType.SelectedIndex = iI;
                            break;
                        }
                    }
                }
                if (drBA1["FFBBHNEW"] != DBNull.Value)
                {
                    for (int iI = 0; iI < m_cboPayType.Items.Count; iI++)
                    {
                        if (((clsGDCaseDictVO)m_cboPayType.Items[iI]).m_strCode == drBA1["FFBBHNEW"].ToString())
                        {
                            m_cboPayType.SelectedIndex = iI;
                            break;
                        }
                    }
                }
                m_strRegisterID = drBA1["FZYID"].ToString();
                m_lblInTimes.Text = string.Format(m_lblInTimes.Text, drBA1["FTIMES"].ToString());
                m_txtInsuranceNO.Text = drBA1["FASCARD1"].ToString();
                //m_txtInsuranceNO.Text = drPatient["INSURANCENUM"].ToString();//INSURANCENUM
                m_lblInPatientID.Text = string.Format(m_lblInPatientID.Text, drBA1["FPRN"].ToString());
                m_lblName.Text = string.Format(m_lblName.Text, drBA1["fname"].ToString());
                m_lblSex.Text = string.Format(m_lblSex.Text, drBA1["fsex"].ToString());
                m_lblBirthday.Text = string.Format(m_lblBirthday.Text, Convert.ToDateTime(drBA1["fbirthday"]).ToString("yyyy年MM月dd日"));
                m_lblAge.Text = string.Format(m_lblAge.Text, drBA1["FAGE"].ToString());
                if (drBA1["FSTATUSBH"] != DBNull.Value)
                {
                    for (int iI = 0; iI < m_cboMarriage.Items.Count; iI++)
                    {
                        if (((clsGDCaseDictVO)m_cboMarriage.Items[iI]).m_strCode == drBA1["FSTATUSBH"].ToString())
                        {
                            m_cboMarriage.SelectedIndex = iI;
                            break;
                        }
                    }
                }
                m_lblInDays.Text = string.Format(m_lblInDays.Text, drBA1["FDAYS"].ToString());
                if (drBA1["FJOB"] != DBNull.Value)
                {
                    m_txtOccupation.m_mthSelectItem(drBA1["FJOB"].ToString());
                }
                m_txtBirthPlace.Text = drBA1["fbirthplace"].ToString();
                m_txtRace.m_mthSelectItem(drBA1["fnationality"].ToString());
                m_txtNation.m_mthSelectItem(drBA1["fcountry"].ToString());
                m_txtIDCard.Text = drBA1["fidcard"].ToString();
                //新增字段FNATIVE
                m_txtNative.Text = drBA1["FNATIVE"].ToString();
                m_txtNowAddr.Text = drBA1["FCURRADDR"].ToString();
                m_txtNowPhone.Text = drBA1["FCURRTELE"].ToString();
                m_txtNowPC.Text = drBA1["FCURRPOST"].ToString();

                m_txtOfficeName.Text = drBA1["fdwname"].ToString();
                m_txtOfficeAddr.Text = drBA1["fdwaddr"].ToString();
                m_txtOfficePhone.Text = drBA1["fdwtele"].ToString();
                if (string.IsNullOrEmpty(m_txtOfficePhone.Text.Trim()) && p_dsContent.Tables["HIS_BA1"].Columns.Contains("HOMEPHONE_VCHR"))
                {
                    m_txtOfficePhone.Text = drBA1["HOMEPHONE_VCHR"].ToString();
                }
                m_txtOfficePC.Text = drBA1["fdwpost"].ToString();
                m_txtResidenceAddr.Text = drBA1["fhkaddr"].ToString();
                if (drBA1["FSOURCEBH"] != DBNull.Value)
                {
                    for (int iI = 0; iI < m_cboPatientSource.Items.Count; iI++)
                    {
                        if (((clsGDCaseDictVO)m_cboPatientSource.Items[iI]).m_strCode == drBA1["FSOURCEBH"].ToString())
                        {
                            m_cboPatientSource.SelectedIndex = iI;
                            break;
                        }
                    }
                }
                m_txtResidencePC.Text = drBA1["fhkpost"].ToString();
                m_txtContactPersonName.Text = drBA1["flxname"].ToString();
                if (drBA1["frelate"] != DBNull.Value)
                {
                    m_txtPatientRelation.m_mthSelectItem(drBA1["frelate"].ToString());
                }
                m_txtContactPersonAddr.Text = drBA1["FLXADDR"].ToString();
                m_txtContactPersonPhone.Text = drBA1["flxtele"].ToString();
                m_lblInDate.Text = string.Format(m_lblInDate.Text, Convert.ToDateTime(drBA1["frydate"]).ToString("yyyy-MM-dd") + " " + drBA1["FRYTIME"]);
                m_lblInDept.Text = string.Format(m_lblInDept.Text, drBA1["FRYDEPT"].ToString());
                m_lblInDept.Tag = drBA1["FRYTYKH"].ToString();
                m_lblOutDate.Text = string.Format(m_lblOutDate.Text, Convert.ToDateTime(drBA1["fcydate"]).ToString("yyyy-MM-dd") + " " + drBA1["FCYTIME"]);
                m_lblOutDept.Text = string.Format(m_lblOutDept.Text, drBA1["FCYDEPT"].ToString());
                m_lblOutDept.Tag = drBA1["FCYTYKH"].ToString();
                m_txtOutPatientDiagICD.Text = drBA1["FMZZDBH"].ToString();
                m_txtOutPatientDiag.Text = drBA1["FMZZD"].ToString();
                if (drBA1["FMZDOCTBH"] != DBNull.Value)
                {
                    clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
                    objDomain.m_mthAddSignToTextBoxByEmpNO(new TextBoxBase[] { txtDoctor }, new string[] { drBA1["FMZDOCTBH"].ToString() }, new bool[] { true });                    
                }
                //if (drBA1["FRYINFOBH"] != DBNull.Value)
                //{
                //    for (int iI = 0; iI < m_cboInInfo.Items.Count; iI++)
                //    {
                //        if (((clsGDCaseDictVO)m_cboInInfo.Items[iI]).m_strCode == drBA1["FRYINFOBH"].ToString())
                //        {
                //            m_cboInInfo.SelectedIndex = iI;
                //            break;
                //        }
                //    }
                //}
                //入院确诊日期删除
                //m_dtpInConfirmDate.Value = Convert.ToDateTime(drBA1["FQZDATE"]);
                if (drBA1["FZKDATE"] != DBNull.Value)
                {
                    ListViewItem lsi = new ListViewItem(drBA1["FRYDEPT"].ToString());
                    lsi.SubItems.Add(Convert.ToDateTime(drBA1["FZKDATE"]).ToString("yyyy-MM-dd") + " " + drBA1["FZKTIME"]);
                    lsi.SubItems.Add(drBA1["FZKDEPT"].ToString());
                    lsi.SubItems.Add(drBA1["FZKTYKH"].ToString());
                    m_lblTransferDept.Items.Add(lsi);
                    strFirstTransDeptName = drBA1["FZKDEPT"].ToString();
                }

                if (drBA1["FRYZDBH"] != DBNull.Value)
                {
                    DataRow drNew = dtbICD.NewRow();
                    drNew["name"] = drBA1["FRYZD"].ToString();
                    drNew["code"] = drBA1["FRYZDBH"].ToString();
                    dtbICD.Rows.Add(drNew);
                }
            }

            if (p_dsContent.Tables.Contains("HIS_BA2") && p_dsContent.Tables["HIS_BA2"].Rows.Count > 0 && m_lblTransferDept.Items.Count == 1)//转科表有数据的前提是必须有首次转科记录
            {
                DataTable dtbBA2 = p_dsContent.Tables["HIS_BA2"];
                List<ListViewItem> lstLsi = new List<ListViewItem>();
                DataRow drTemp = null;
                for (int iRow = 0; iRow < dtbBA2.Rows.Count; iRow++)
                {
                    drTemp = dtbBA2.Rows[iRow];
                    ListViewItem lsi = null;
                    if (iRow == 0)
                    {
                        lsi = new ListViewItem(strFirstTransDeptName);
                    }
                    else
                    {
                        lsi = new ListViewItem(dtbBA2.Rows[iRow - 1]["FZKDEPT"].ToString());
                    }
                    lsi.SubItems.Add(Convert.ToDateTime(drTemp["FZKDATE"]).ToString("yyyy-MM-dd") + " " + drTemp["FZKTIME"]);
                    lsi.SubItems.Add(drTemp["FZKDEPT"].ToString());
                    lsi.SubItems.Add(drTemp["FZKTYKH"].ToString());
                }
            }
            m_blnHasInit = true;
        }

        /// <summary>
        /// 初始化病案内容
        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_strPatientID">病人ID</param>
        public void m_mthInitCase(string p_strRegisterID, string p_strPatientID)
        {
            m_strRegisterID = p_strPatientID;
            int intDeptType = 1;
            com.digitalwave.iCare.common.clsCommmonInfo objCommon = new com.digitalwave.iCare.common.clsCommmonInfo();
            string strDeptType = objCommon.m_lonGetModuleInfo("3017");
            int.TryParse(strDeptType, out intDeptType);
            objCommon = null;
            //获取病人住院基本资料
            clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
            DataTable dtbResult = null;
            long lngRes = objDomain.m_lngGetSynchronousData_reg(intDeptType, p_strRegisterID, out dtbResult);
            if (dtbResult == null || dtbResult.Rows.Count == 0)
            {
                return;
            }

            DataRow drPatient = dtbResult.Rows[0];
            int intSelectIndex = 5;
            if (int.TryParse(drPatient["ba_paytypeid_chr"].ToString(), out intSelectIndex))
            {
                m_cboPayType.SelectedIndex = intSelectIndex - 1;
            }
            else
            {
                m_cboPayType.SelectedIndex = 5;
            }
            m_lblInTimes.Text = string.Format(m_lblInTimes.Text, drPatient["ftimes"].ToString());
           // m_txtInsuranceNO.Text = drPatient["INSURANCENUM"].ToString();//INSURANCENUM
            m_lblInPatientID.Text = string.Format(m_lblInPatientID.Text, drPatient["fprn"].ToString());
            m_lblName.Text = string.Format(m_lblName.Text, drPatient["fname"].ToString());
            m_lblSex.Text = string.Format(m_lblSex.Text, drPatient["fsex"].ToString());
            m_lblBirthday.Text = string.Format(m_lblBirthday.Text, Convert.ToDateTime(drPatient["fbirthday"]).ToString("yyyy年MM月dd日"));
            m_lblAge.Text = string.Format(m_lblAge.Text, com.digitalwave.Emr.StaticObject.clsConsts.m_strCalAge(drPatient["fbirthday"].ToString(), Convert.ToDateTime(drPatient["frydate"])));
            if (drPatient["fstatus"].ToString() == "未婚")
            {
                m_cboMarriage.SelectedIndex = 0;
            }
            else if (drPatient["fstatus"].ToString() == "已婚")
            {
                m_cboMarriage.SelectedIndex = 1;
            }
            else if (drPatient["fstatus"].ToString() == "离婚")
            {
                m_cboMarriage.SelectedIndex = 2;
            }
            else if (drPatient["fstatus"].ToString() == "丧偶")
            {
                m_cboMarriage.SelectedIndex = 3;
            }
            int intDays = 0;
            int intHours = 0;
            int intMinutes = 0;
            com.digitalwave.iCare.common.clsCommmonInfo objComm = new com.digitalwave.iCare.common.clsCommmonInfo();
            objComm.m_mthGetInHospitalDays(Convert.ToDateTime(drPatient["frydate"]), Convert.ToDateTime(drPatient["fcydate"]), out intDays, out intHours, out intMinutes);
            m_lblInDays.Text = string.Format(m_lblInDays.Text, intDays.ToString());
            if (drPatient["fjob"] != DBNull.Value)
            {
                m_txtOccupation.m_mthSelectItem(drPatient["fjob"].ToString());
            }
            if (string.IsNullOrEmpty(m_txtOccupation.Text))
            {
                m_txtOccupation.m_mthSelectItem("不便分类的其他从业人员");
            }
            m_txtBirthPlace.Text = drPatient["fbirthplace"].ToString();
            if (drPatient["fnationality"] != DBNull.Value)
            {
                m_txtRace.m_mthSelectItem(drPatient["fnationality"].ToString());
            }
            if (string.IsNullOrEmpty(m_txtRace.Text))
            {
                m_txtRace.m_mthSelectItem("汉族");
            }
            if (drPatient["fcountry"] != DBNull.Value)
            {
                m_txtNation.m_mthSelectItem(drPatient["fcountry"].ToString());
            }
            if (string.IsNullOrEmpty(m_txtNation.Text))
            {
                m_txtNation.m_mthSelectItem("中国");
            }
            m_txtIDCard.Text = drPatient["fidcard"].ToString();

            //新版添加字段 su.liang
            m_txtNowAddr.Text = drPatient["FCURRADDR"].ToString();
            m_txtNowPhone.Text = drPatient["FCURRTELE"].ToString();
            m_txtNowPC.Text = drPatient["FCURRPOST"].ToString();

            m_txtOfficeName.Text = drPatient["fdwname"].ToString();
            m_txtOfficeAddr.Text = drPatient["fdwaddr"].ToString();
            m_txtOfficePhone.Text = drPatient["fdwtele"].ToString();
            
            m_txtNative.Text = drPatient["FNATIVE"].ToString();//FNATIVE
            if (string.IsNullOrEmpty(m_txtOfficePhone.Text.Trim()) && dtbResult.Columns.Contains("HOMEPHONE_VCHR"))
            {
                m_txtOfficePhone.Text = drPatient["HOMEPHONE_VCHR"].ToString();
            }
            m_txtOfficePC.Text = drPatient["fdwpost"].ToString();
            m_txtResidenceAddr.Text = drPatient["fhkaddr"].ToString();
            for (int iSour = 0; iSour < m_cboPatientSource.Items.Count; iSour++)
            {
                if (((clsGDCaseDictVO)(m_cboPatientSource.Items[iSour])).m_strName == drPatient["patientsources_vchr"].ToString())
                {
                    m_cboPatientSource.SelectedIndex = iSour;
                    break;
                }
            }
            if (m_cboPatientSource.SelectedItem == null)
            {
                m_cboPatientSource.SelectedIndex = 0;
            }
            m_txtResidencePC.Text = drPatient["fhkpost"].ToString();
            m_txtContactPersonName.Text = drPatient["flxname"].ToString();
            if (drPatient["frelate"] != DBNull.Value)
            {
                m_txtPatientRelation.m_mthSelectItem(drPatient["frelate"].ToString());
            }
            if (string.IsNullOrEmpty(m_txtPatientRelation.Text))
            {
                m_txtPatientRelation.m_mthSelectItem("无");
            }
            m_txtContactPersonAddr.Text = drPatient["FLXADDR"].ToString();
            m_txtContactPersonPhone.Text = drPatient["flxtele"].ToString();
            m_lblInDate.Text = string.Format(m_lblInDate.Text, Convert.ToDateTime(drPatient["frydate"]).ToString("yyyy-MM-dd HH:mm"));
            m_lblOutDate.Text = string.Format(m_lblOutDate.Text, Convert.ToDateTime(drPatient["fcydate"]).ToString("yyyy-MM-dd HH:mm"));
            //获取病人出入转信息
            DataTable dtbTransfer = null;         
            lngRes = objDomain.m_lngGetTransferInfo(drPatient["registerid_chr"].ToString(), out dtbTransfer);
            DataView dvT = new DataView(dtbTransfer);
            dvT.RowFilter = "TYPE_INT = 5";
            string strInDeptName = string.Empty;
            if (dvT.Count > 0)
            {
                strInDeptName = dvT[0]["deptname_vchr"].ToString();
                m_lblInDept.Text = string.Format(m_lblInDept.Text, dvT[0]["deptname_vchr"].ToString());
                m_lblInDept.Tag = dvT[0]["ba_deptnum"].ToString();
            }
            dvT.RowFilter = "TYPE_INT = 7 or TYPE_INT = 6";
            if (dvT.Count > 0)
            {
                m_lblOutDept.Text = string.Format(m_lblOutDept.Text, dvT[0]["deptname_vchr"].ToString());
                m_lblOutDept.Tag = dvT[0]["ba_deptnum"].ToString();
            }
            dvT.RowFilter = "TYPE_INT = 3";
            dvT.Sort = "modify_dat asc";
            if (dvT.Count > 0)
            {
                List<ListViewItem> lstItems = new List<ListViewItem>();
                //lstItems.Clear();
                m_lblTransferDept.Items.Clear();
                for (int iL = 0; iL < dvT.Count; iL++)
                {
                    ListViewItem lvi = null;
                    if (iL > 0)
                    {
                        if (dvT[iL]["ba_deptnum"].ToString() == dvT[iL-1]["ba_deptnum"].ToString())//只是转区，未转科
                        {
                            continue;
                        }
                        lvi = new ListViewItem(dvT[iL - 1]["deptname_vchr"].ToString());
                    }
                    else//首次转科的源科室为入院科室
                    {
                        lvi = new ListViewItem(strInDeptName);
                    }
                    lvi.SubItems.Add(Convert.ToDateTime(dvT[iL]["modify_dat"]).ToString("yyyy-MM-dd HH:mm"));
                    lvi.SubItems.Add(dvT[iL]["deptname_vchr"].ToString());
                    lvi.SubItems.Add(dvT[iL]["ba_deptnum"].ToString());
                    lstItems.Add(lvi);
                }               
                m_lblTransferDept.Items.AddRange(lstItems.ToArray());
            }
            //获取入院诊断
            DataTable dtbDiag = null;
            lngRes = objDomain.m_lngGetDiagnosis(drPatient["registerid_chr"].ToString(), "1", out dtbDiag);
            //m_dgwInDiagnosis.DataSource = dtbDiag;
            //获取病案首页入院情况内容
            DataTable dtbInInfo = null;
            //((clsGDCaseDictVO)m_cboPayType.SelectedItem).m_strCode = "7";
            //((clsGDCaseDictVO)m_cboPayType.SelectedItem).m_strName = "全自费";
            lngRes = objDomain.m_lngGetPatientInInfo(drPatient["registerid_chr"].ToString(), out dtbInInfo);


            //int intSelectIndex = 9;
            //if (int.TryParse(drPatient["ba_paytypeid_chr"].ToString(), out intSelectIndex))
            //{
            //    m_cboPayType.SelectedIndex = intSelectIndex - 1;
            //}
            //else
            //{
            //    m_cboPayType.SelectedIndex = 9;
            //}


            if (dtbInInfo != null && dtbInInfo.Rows.Count > 0)
            {
                int intRYTemp = -1;

                if (dtbInInfo.Rows[0]["MODEOFPAYMENT"].ToString() == "城镇职工基本医疗保险")
                {
                    ((clsGDCaseDictVO)m_cboPayType.SelectedItem).m_strCode = "1";
                    ((clsGDCaseDictVO)m_cboPayType.SelectedItem).m_strName = "城镇职工基本医疗保险";
                    m_cboPayType.SelectedIndex =0;
                }
                else if (dtbInInfo.Rows[0]["MODEOFPAYMENT"].ToString() == "城镇居民基本医疗保险")
                {
                    ((clsGDCaseDictVO)m_cboPayType.SelectedItem).m_strCode = "2";
                    ((clsGDCaseDictVO)m_cboPayType.SelectedItem).m_strName = "城镇居民基本医疗保险";
                    m_cboPayType.SelectedIndex = 1;
                }
                else if (dtbInInfo.Rows[0]["MODEOFPAYMENT"].ToString() == "新型农村合作医疗")
                {
                    ((clsGDCaseDictVO)m_cboPayType.SelectedItem).m_strCode = "3";
                    ((clsGDCaseDictVO)m_cboPayType.SelectedItem).m_strName = "新型农村合作医疗";
                    m_cboPayType.SelectedIndex = 2;
                }
                else if (dtbInInfo.Rows[0]["MODEOFPAYMENT"].ToString() == "贫困救助")
                {
                    ((clsGDCaseDictVO)m_cboPayType.SelectedItem).m_strCode = "4";
                    ((clsGDCaseDictVO)m_cboPayType.SelectedItem).m_strName = "贫困救助";
                    m_cboPayType.SelectedIndex = 3;
                }
                else if (dtbInInfo.Rows[0]["MODEOFPAYMENT"].ToString() == "商业医疗保险")
                {
                    ((clsGDCaseDictVO)m_cboPayType.SelectedItem).m_strCode = "5";
                    ((clsGDCaseDictVO)m_cboPayType.SelectedItem).m_strName = "商业医疗保险";
                    m_cboPayType.SelectedIndex = 4;
                }
                else if (dtbInInfo.Rows[0]["MODEOFPAYMENT"].ToString() == "全公费")
                {
                    ((clsGDCaseDictVO)m_cboPayType.SelectedItem).m_strCode = "6";
                    ((clsGDCaseDictVO)m_cboPayType.SelectedItem).m_strName = "全公费";
                    m_cboPayType.SelectedIndex = 5;
                }
                else if (dtbInInfo.Rows[0]["MODEOFPAYMENT"].ToString() == "全自费")
                {
                    ((clsGDCaseDictVO)m_cboPayType.SelectedItem).m_strCode = "7";
                    ((clsGDCaseDictVO)m_cboPayType.SelectedItem).m_strName = "全自费";
                    m_cboPayType.SelectedIndex = 6;
                }
                else if (dtbInInfo.Rows[0]["MODEOFPAYMENT"].ToString() == "其他社会保险")
                {
                    ((clsGDCaseDictVO)m_cboPayType.SelectedItem).m_strCode = "8";
                    ((clsGDCaseDictVO)m_cboPayType.SelectedItem).m_strName = "其他社会保险";
                    m_cboPayType.SelectedIndex = 7;
                }
                else
                {
                    ((clsGDCaseDictVO)m_cboPayType.SelectedItem).m_strCode = "9";
                    ((clsGDCaseDictVO)m_cboPayType.SelectedItem).m_strName = "其他";
                    m_cboPayType.SelectedIndex = 8;
                }
                m_txtInsuranceNO.Text = dtbInInfo.Rows[0]["INSURANCENUM"].ToString();//INSURANCENUM
                m_txtOutPatientDiagICD.Text = dtbInInfo.Rows[0]["mzicd10"].ToString();
                m_txtOutPatientDiag.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(dtbInInfo.Rows[0]["diagnosis"].ToString(), dtbInInfo.Rows[0]["diagnosisxml"].ToString());
                objDomain.m_mthAddSignToTextBoxByEmpID(new TextBoxBase[] { txtDoctor }, new string[] { dtbInInfo.Rows[0]["doctor"].ToString() }, new bool[] { true });
                //新增病例分型、入院途径 、临床路径 su.liang
                m_txtNewBornWeight.Text = dtbInInfo.Rows[0]["newbabyweight"].ToString();
                m_txtInhospitalWeight.Text = dtbInInfo.Rows[0]["newbabyinhostpitalweight"].ToString();
                if (dtbInInfo.Rows[0]["inhospitalway"].ToString() == "1")//急诊
                {
                    m_gpbInhospitalWay.Tag = "1";
                    m_rdbEmergency.Checked = true;
                }
                else if (dtbInInfo.Rows[0]["inhospitalway"].ToString() == "2")//门诊
                {
                    m_gpbInhospitalWay.Tag = "2";
                    m_rdbOutpatient.Checked = true;
                }
                else if (dtbInInfo.Rows[0]["inhospitalway"].ToString() == "3")//其他医疗机构转入
                {
                    m_gpbInhospitalWay.Tag = "3";
                    m_rdbOtherHospital.Checked = true;
                }
                else
                {
                    m_gpbInhospitalWay.Tag = "9";
                    m_rdbOther.Checked = true;
                }

                if (dtbInInfo.Rows[0]["CONDICTIONWHENIN"].ToString() == "0")
                {
                    m_gpbblfx.Tag = "1";
                    m_rdbGeneral.Checked = true;
                }
                else if (dtbInInfo.Rows[0]["CONDICTIONWHENIN"].ToString() == "1")
                {
                    m_gpbblfx.Tag = "2";
                    m_rdbUrgent.Checked = true;
                }
                else if (dtbInInfo.Rows[0]["CONDICTIONWHENIN"].ToString() == "2")
                {
                    m_gpbblfx.Tag = "3";
                    m_rdbTroubleshooting.Checked = true;
                }
                else if (dtbInInfo.Rows[0]["CONDICTIONWHENIN"].ToString() == "3")
                {
                    m_gpbblfx.Tag = "4";
                    m_rdbCriticallyill.Checked = true;
                }

                if (dtbInInfo.Rows[0]["PATH"].ToString() == "1")
                    m_txtFYCLJ.Text = "是";
                else
                    m_txtFYCLJ.Text = "否";
            }
            objDomain = null;
            m_blnHasInit = true;
        } 
        #endregion

        #region 根据字典初始化界面固定选项值
        /// <summary>
        /// 根据字典初始化界面固定选项值
        /// </summary>
        /// <param name="p_dtbDict">字典</param>
        public void m_mthInitDict(DataTable p_dtbDict)
        {
            //MessageBox.Show("m_mthInitDict");
            if (p_dtbDict == null || p_dtbDict.Rows.Count == 0)
            {
                return;
            }
            m_dtbDict = p_dtbDict;
            DataView drView = new DataView(p_dtbDict);
            //医疗付款方式
            drView.RowFilter = "fcode='GBFKFSNEW'";
            drView.Sort = "fbh ASC";

            if (drView != null && drView.Count > 0)
            {
                m_cboPayType.Items.Clear();
                clsGDCaseDictVO[] objPayType = new clsGDCaseDictVO[drView.Count];
                for (int iM = 0; iM < drView.Count; iM++)
                {
                    objPayType[iM] = new clsGDCaseDictVO();
                    objPayType[iM].m_strCode = drView[iM]["fbh"].ToString();
                    objPayType[iM].m_strName = drView[iM]["fmc"].ToString();
                }
                m_cboPayType.Items.AddRange(objPayType);
            }
            //婚姻状况
            drView.RowFilter = "fcode='GBHYZK'";
            drView.Sort = "fbh ASC";
            if (drView != null && drView.Count > 0)
            {
                m_cboMarriage.Items.Clear();
                clsGDCaseDictVO[] objMarriage = new clsGDCaseDictVO[drView.Count];
                for (int iM = 0; iM < drView.Count; iM++)
                {
                    objMarriage[iM] = new clsGDCaseDictVO();
                    objMarriage[iM].m_strCode = drView[iM]["fbh"].ToString();
                    objMarriage[iM].m_strName = drView[iM]["fmc"].ToString();
                }
                m_cboMarriage.Items.AddRange(objMarriage);
            }
            //职业
            drView.RowFilter = "fcode='GBVOCATIONNEW'";
            drView.Sort = "fbh ASC";
            if (drView != null && drView.Count > 0)
            {
                DataTable dtbOccupation = drView.ToTable();
                dtbOccupation.Columns.Remove("fcode");
                dtbOccupation.Columns["fzjc"].Caption = "助词码";
                dtbOccupation.Columns["fmc"].Caption = "名称";
                dtbOccupation.Columns["fbh"].Caption = string.Empty;
                m_txtOccupation.m_mthBindingSource_Defined(dtbOccupation);
            }
            //民族
            drView.RowFilter = "fcode='GBNATIONALITY'";
            drView.Sort = "fbh ASC";
            if (drView != null && drView.Count > 0)
            {
                DataTable dtbNation = drView.ToTable();
                dtbNation.Columns.Remove("fcode");
                dtbNation.Columns["fzjc"].Caption = "助词码";
                dtbNation.Columns["fmc"].Caption = "名称";
                dtbNation.Columns["fbh"].Caption = string.Empty;
                m_txtRace.m_mthBindingSource_Defined(dtbNation);
            }
            //国籍
            drView.RowFilter = "fcode='GBCOUNTRY'";
            drView.Sort = "fbh ASC";
            if (drView != null && drView.Count > 0)
            {
                DataTable dtbContry = drView.ToTable();
                dtbContry.Columns.Remove("fcode");
                dtbContry.Columns["fzjc"].Caption = "助词码";
                dtbContry.Columns["fmc"].Caption = "名称";
                dtbContry.Columns["fbh"].Caption = string.Empty;
                m_txtNation.m_mthBindingSource_Defined(dtbContry);
            }
            //关系
            drView.RowFilter = "fcode='GBRELATION'";
            drView.Sort = "fbh ASC";
            if (drView != null && drView.Count > 0)
            {
                DataTable dtbRelation = drView.ToTable();
                dtbRelation.Columns.Remove("fcode");
                dtbRelation.Columns["fzjc"].Caption = "助词码";
                dtbRelation.Columns["fmc"].Caption = "名称";
                dtbRelation.Columns["fbh"].Caption = string.Empty;
                m_txtPatientRelation.m_mthBindingSource_Defined(dtbRelation);
            }
            //入院时情况
            //drView.RowFilter = "fcode='GBRYQK'";
            //drView.Sort = "fbh ASC";
            //if (drView != null && drView.Count > 0)
            //{
            //    clsGDCaseDictVO[] objInInfo = new clsGDCaseDictVO[drView.Count];
            //    for (int iM = 0; iM < drView.Count; iM++)
            //    {
            //        objInInfo[iM] = new clsGDCaseDictVO();
            //        objInInfo[iM].m_strCode = drView[iM]["fbh"].ToString();
            //        objInInfo[iM].m_strName = drView[iM]["fmc"].ToString();
            //    }
            //    m_cboInInfo.Items.AddRange(objInInfo);
            //}
            //病人来源
            drView.RowFilter = "fcode='GBSOURCE'";
            drView.Sort = "fbh ASC";
            if (drView != null && drView.Count > 0)
            {
                m_cboPatientSource.Items.Clear();
                clsGDCaseDictVO[] objInInfo = new clsGDCaseDictVO[drView.Count];
                for (int iM = 0; iM < drView.Count; iM++)
                {
                    objInInfo[iM] = new clsGDCaseDictVO();
                    objInInfo[iM].m_strCode = drView[iM]["fbh"].ToString();
                    objInInfo[iM].m_strName = drView[iM]["fmc"].ToString();
                }
                m_cboPatientSource.Items.AddRange(objInInfo);
            }
        }
        #endregion

        #region 获取病案内容
        /// <summary>
        /// 获取病案内容
        /// </summary>
        /// <param name="p_dsCaseContent">病案内容</param>
        public void m_mthGetCaseContent(System.Data.DataSet p_dsCaseContent)
        {
            if (p_dsCaseContent == null)
            {
                p_dsCaseContent = new DataSet("CaseContent");
            }
            DataTable dtbBA1 = null;
            if (!p_dsCaseContent.Tables.Contains("HIS_BA1"))
            {
                clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
                long lngRes = objDomain.m_lngGetHIS_BA1Schema(out dtbBA1);
                dtbBA1.TableName = "HIS_BA1";
                p_dsCaseContent.Tables.Add(dtbBA1);
            }
            else
            {
                dtbBA1 = p_dsCaseContent.Tables["HIS_BA1"];
            }

            DataRow drBA1 = null;
            if (dtbBA1.Rows.Count == 0)
            {
                drBA1 = dtbBA1.NewRow();
                dtbBA1.Rows.Add(drBA1);
            }
            else
            {
                drBA1 = dtbBA1.Rows[0];
            }

            drBA1["Fifinput"] = 0;
            drBA1["FPRN"] = m_lblInPatientID.Text.Replace("病案号:", string.Empty);
            drBA1["FTIMES"] = m_lblInTimes.Text.Replace("次住院", string.Empty).Replace("第", string.Empty);
            drBA1["FICDVersion"] = 10;
            drBA1["FZYID"] = m_strRegisterID;
            drBA1["FAGE"] = m_lblAge.Text.Replace("年龄:", string.Empty);
            drBA1["FNAME"] = m_lblName.Text.Replace("姓名:", string.Empty);
            string strSex = m_lblSex.Text.Replace("性别:", string.Empty);
            if (strSex.Trim() == "男")
            {
                drBA1["FSEXBH"] = "1";
            }
            else if (strSex.Trim() == "女")
            {
                drBA1["FSEXBH"] = "2";
            }
            drBA1["FSEX"] = strSex;
            drBA1["FBIRTHDAY"] = Convert.ToDateTime(m_lblBirthday.Text.Replace("出生:", string.Empty));
            drBA1["FBIRTHPLACE"] = m_txtBirthPlace.Text;
            drBA1["FIDCard"] = m_txtIDCard.Text;
            if (!string.IsNullOrEmpty(m_txtNation.StrItemId))
            {
                DataRow[] drD = m_dtbDict.Select("FCODE='GBCOUNTRY' and fmc='" + m_txtNation.StrItemId + "'");
                if (drD != null && drD.Length > 0)
                {
                    drBA1["fcountrybh"] = drD[0]["fbh"].ToString();
                    drBA1["fcountry"] = drD[0]["fmc"].ToString();
                }
            }
            drBA1["FNATIVE"] = m_txtNative.Text.ToString().Trim();//su.liang
            //drBA1["FCURRADDR"] = drD[0]["FCURRADDR"].ToString();
            //drBA1["FCURRTELE"] = drD[0]["FCURRTELE"].ToString();
            //drBA1["FCURRPOST"] = drD[0]["FCURRPOST"].ToString();
            if (!string.IsNullOrEmpty(m_txtRace.StrItemId))
            {
                DataRow[] drD = m_dtbDict.Select("FCODE='GBNATIONALITY' and fmc='" + m_txtRace.StrItemId + "'");
                if (drD != null && drD.Length > 0)
                {
                    drBA1["fnationalitybh"] = drD[0]["fbh"].ToString();
                    drBA1["fnationality"] = drD[0]["fmc"].ToString();
                }
            }
            //drBA1["FJOB"] = m_txtOccupation.Text; su.liang
            if (!string.IsNullOrEmpty(m_txtOccupation.StrItemId))
            {
                DataRow[] drD = m_dtbDict.Select("FCODE='GBVOCATIONNEW' and fmc='" + m_txtOccupation.StrItemId + "'");
                if (drD != null && drD.Length > 0)
                {
                    drBA1["FJOBBH"] = drD[0]["FBH"].ToString();
                    drBA1["FJOB"] = drD[0]["FMC"].ToString();
                }
            }
            if (m_cboMarriage.SelectedItem != null && m_cboMarriage.SelectedItem is clsGDCaseDictVO)
            {
                drBA1["FSTATUSBH"] = ((clsGDCaseDictVO)m_cboMarriage.SelectedItem).m_strCode;
                drBA1["FSTATUS"] = ((clsGDCaseDictVO)m_cboMarriage.SelectedItem).m_strName;
            }
            
            drBA1["FDWNAME"] = m_txtOfficeName.Text;
            drBA1["FDWADDR"] = m_txtOfficeAddr.Text;
            drBA1["FDWTELE"] = m_txtOfficePhone.Text;
            drBA1["FDWPOST"] = m_txtOfficePC.Text;
            drBA1["FHKADDR"] = m_txtResidenceAddr.Text;
            drBA1["FHKPOST"] = m_txtResidencePC.Text;
            drBA1["FLXNAME"] = m_txtContactPersonName.Text;
            drBA1["FRELATE"] = m_txtPatientRelation.Text;
            drBA1["FLXADDR"] = m_txtContactPersonAddr.Text;
            drBA1["FLXTELE"] = m_txtContactPersonPhone.Text;
            drBA1["FASCARD1"] = m_txtInsuranceNO.Text;
            
            
            DateTime dtmTemp = DateTime.MinValue;
            if (DateTime.TryParse(m_lblInDate.Text.Replace("入院日期:", string.Empty), out dtmTemp))
            {
                drBA1["FRYDATE"] = dtmTemp.Date;
                drBA1["FRYTIME"] = dtmTemp.ToString("HH:mm");
            }
            else
            {
                //时间只有小时部分，补齐后再格式化
                dtmTemp = Convert.ToDateTime(m_lblInDate.Text.Replace("入院日期:", string.Empty) + ":00:00");
                drBA1["FRYDATE"] = dtmTemp.Date;
                drBA1["FRYTIME"] = dtmTemp.ToString("HH:mm");
            }
            

            drBA1["FRYTYKH"] = m_lblInDept.Tag != null ? m_lblInDept.Tag.ToString() : string.Empty;
            drBA1["FRYDEPT"] = m_lblInDept.Text.Replace("入院科别:", string.Empty);
            if (DateTime.TryParse(m_lblOutDate.Text.Replace("出院日期:", string.Empty),out dtmTemp))
            {
                drBA1["FCYDATE"] = dtmTemp.Date;
                drBA1["FCYTIME"] = dtmTemp.ToString("HH:mm");
            }
            else
            {
                //时间只有小时部分，补齐后再格式化
                dtmTemp = Convert.ToDateTime(m_lblOutDate.Text.Replace("出院日期:", string.Empty) + ":00:00");
                drBA1["FCYDATE"] = dtmTemp.Date;
                drBA1["FCYTIME"] = dtmTemp.ToString("HH:mm");
            }

            drBA1["FCYTYKH"] = m_lblOutDept.Tag != null ? m_lblOutDept.Tag.ToString() : string.Empty;
            drBA1["FCYDEPT"] = m_lblOutDept.Text.Replace("出院科别:", string.Empty);
            drBA1["FDAYS"] = m_lblInDays.Text.Replace("实际住院", string.Empty).Replace("天", string.Empty);
            drBA1["FMZZDBH"] = m_txtOutPatientDiagICD.Text;
            drBA1["FMZZD"] = m_txtOutPatientDiag.Text;
            if (txtDoctor.Tag != null)
            {
                drBA1["FMZDOCTBH"] = (( clsEmrEmployeeBase_VO)txtDoctor.Tag).m_strEMPNO_CHR;
                drBA1["FMZDOCT"] = (( clsEmrEmployeeBase_VO)txtDoctor.Tag).m_strLASTNAME_VCHR;
            }
            //if (m_cboInInfo.SelectedItem != null)
            //{
            //    drBA1["FRYINFOBH"] = ((clsGDCaseDictVO)m_cboInInfo.SelectedItem).m_strCode;
            //    drBA1["FRYINFO"] = ((clsGDCaseDictVO)m_cboInInfo.SelectedItem).m_strName;
            //}

            //if (m_dgwInDiagnosis.Rows.Count > 0)
            //{
            //    DataRowView drInD = m_dgwInDiagnosis.Rows[0].DataBoundItem as DataRowView;
            //    drBA1["FRYZDBH"] = drInD["code"].ToString();
            //    drBA1["FRYZD"] = drInD["name"].ToString();
            //}
            //drBA1["FQZDATE"] = m_dtpInConfirmDate.Value;
            if (m_lblTransferDept.Items.Count > 0)
            {
                drBA1["FZKTYKH"] = m_lblTransferDept.Items[0].SubItems[3].Text;
                drBA1["FZKDEPT"] = m_lblTransferDept.Items[0].SubItems[2].Text;
                if (DateTime.TryParse(m_lblTransferDept.Items[0].SubItems[1].Text,out dtmTemp))
                {
                    drBA1["FZKDATE"] = dtmTemp.Date;
                    drBA1["FZKTIME"] = dtmTemp.Hour;
                }
                else
                {
                    //时间只有小时部分，补齐后再格式化
                    dtmTemp = Convert.ToDateTime(m_lblTransferDept.Items[0].SubItems[1].Text + ":00:00");
                    drBA1["FZKDATE"] = dtmTemp.Date;
                    drBA1["FZKTIME"] = dtmTemp.Hour;
                }        
            }
            if (m_cboPatientSource.SelectedItem != null)
            {
                drBA1["FSOURCEBH"] = ((clsGDCaseDictVO)m_cboPatientSource.SelectedItem).m_strCode;
                drBA1["FSOURCE"] = ((clsGDCaseDictVO)m_cboPatientSource.SelectedItem).m_strName;
            }
            //新增字段
            drBA1["FCURRADDR"] = m_txtNowAddr.Text;
            drBA1["FCURRTELE"] = m_txtNowPhone.Text;
            drBA1["FCURRPOST"] = m_txtNowPC.Text;
            double dbResult = 0.0;
            if (double.TryParse(m_txtNewBornWeight.Text.ToString(), out dbResult))
                drBA1["FCSTZ"] = dbResult;//m_txtNewBornWeight.Text;
            else
                drBA1["FCSTZ"] = 0.0;

            if (double.TryParse(m_txtInhospitalWeight.Text.ToString(), out dbResult))
                drBA1["FRYTZ"] = dbResult;//m_txtNewBornWeight.Text;
            else
                drBA1["FRYTZ"] = 0.0;
            //drBA1["FRYTZ"] = m_txtInhospitalWeight.Text;
            //新增字段
            drBA1["FYCLJ"] = m_txtFYCLJ.Text;
            if (m_txtFYCLJ.Text.ToString() == "是")
                drBA1["FYCLJBH"] = "1";
            else
                drBA1["FYCLJBH"] = "2";
            if (m_gpbInhospitalWay.Tag != null)
            {
                drBA1["FRYTJBH"] = m_gpbInhospitalWay.Tag.ToString();//.Text;//FRYTJBH
                if (m_gpbInhospitalWay.Tag.ToString() == "1")
                    drBA1["FRYTJ"] = "急诊";
                else if (m_gpbInhospitalWay.Tag.ToString() == "2")
                    drBA1["FRYTJ"] = "门诊";
                else if (m_gpbInhospitalWay.Tag.ToString() == "3")
                    drBA1["FRYTJ"] = "其他医疗机构转入";
                else
                    drBA1["FRYTJ"] = "其他";
            }
            // drBA1["FJBFX"] = m_gpbblfx.TabIndex.ToString();//.Text;//FJBFXBH
            if (m_gpbblfx.Tag != null)
            {
                if (m_gpbblfx.Tag.ToString() == "1")
                {
                    drBA1["FJBFX"] = "一般";
                    drBA1["FJBFXBH"] = "1";
                }
                else if (m_gpbblfx.Tag.ToString() == "2")
                {
                    drBA1["FJBFX"] = "急";
                    drBA1["FJBFXBH"] = "2";
                }
                else if (m_gpbblfx.Tag.ToString() == "3")
                {
                    drBA1["FJBFX"] = "疑难";
                    drBA1["FJBFXBH"] = "3";
                }
                else if (m_gpbblfx.Tag.ToString() == "4")
                {
                    drBA1["FJBFX"] = "危重";
                    drBA1["FJBFXBH"] = "4";
                }
            }

            if (m_cboPayType.SelectedItem != null && m_cboPayType.SelectedItem is clsGDCaseDictVO)
            {
                drBA1["FFBBH"] = ((clsGDCaseDictVO)m_cboPayType.SelectedItem).m_strCode;
                drBA1["FFB"] = ((clsGDCaseDictVO)m_cboPayType.SelectedItem).m_strName;
            }
            if (m_cboPayType.SelectedItem != null && m_cboPayType.SelectedItem is clsGDCaseDictVO)
            {
                drBA1["FFBBHNEW"] = ((clsGDCaseDictVO)m_cboPayType.SelectedItem).m_strCode;
                drBA1["FFBNEW"] = ((clsGDCaseDictVO)m_cboPayType.SelectedItem).m_strName;
            }
            DataTable dtbBA2 = null;
            if (!p_dsCaseContent.Tables.Contains("HIS_BA2"))
            {
                dtbBA2 = new DataTable();
                dtbBA2.TableName = "HIS_BA2";
                dtbBA2.Columns.Add("fprn");
                dtbBA2.Columns.Add("FTIMES");
                dtbBA2.Columns.Add("FZKTYKH");
                dtbBA2.Columns.Add("FZKDEPT");
                dtbBA2.Columns.Add("FZKDATE");
                dtbBA2.Columns.Add("FZKTIME");
                p_dsCaseContent.Tables.Add(dtbBA2);
            }
            else
            {
                dtbBA2 = p_dsCaseContent.Tables["HIS_BA2"];
            }
            if (m_lblTransferDept.Items.Count > 1)
            {
                dtbBA2.BeginLoadData();
                for (int iL = 1; iL < m_lblTransferDept.Items.Count; iL++)
                {
                    DataRow dr = dtbBA2.NewRow();
                    dr["fprn"] = drBA1["fprn"].ToString();
                    dr["FTIMES"] = drBA1["FTIMES"].ToString();
                    dr["FZKTYKH"] = m_lblTransferDept.Items[iL].SubItems[3].Text;
                    dr["FZKDEPT"] = m_lblTransferDept.Items[iL].SubItems[2].Text;
                    if (DateTime.TryParse(m_lblTransferDept.Items[1].SubItems[1].Text, out dtmTemp))
                    {
                        dr["FZKDATE"] = dtmTemp.Date;
                        dr["FZKTIME"] = dtmTemp.Hour;
                    }
                    else
                    {
                        //时间只有小时部分，补齐后再格式化
                        dtmTemp = Convert.ToDateTime(m_lblTransferDept.Items[1].SubItems[1].Text + ":00:00");
                        dr["FZKDATE"] = dtmTemp.Date;
                        dr["FZKTIME"] = dtmTemp.Hour;
                    }
                    dtbBA2.LoadDataRow(dr.ItemArray, true);
                }
                dtbBA2.EndLoadData();
            }
        } 
        #endregion

        #endregion

        #region 删除入院诊断
        //private void m_cmdAddInDiag_Click(object sender, EventArgs e)
        //{
        //    DataTable dtbICD = m_dgwInDiagnosis.DataSource as DataTable;
        //    if (dtbICD != null && dtbICD.Columns.Count > 0)
        //    {
        //        DataRow drNew = dtbICD.NewRow();
        //        dtbICD.Rows.Add(drNew);
        //    }
        //    else
        //    {
        //        dtbICD = new DataTable();
        //        dtbICD.Columns.Add("name");
        //        dtbICD.Columns.Add("code");
        //        dtbICD.Columns.Add("outinfo");
        //        m_dgwInDiagnosis.DataSource = dtbICD;
        //    }
        //    m_dgwInDiagnosis.CurrentCell = m_dgwInDiagnosis.Rows[m_dgwInDiagnosis.Rows.Count - 1].Cells[0];
        //    clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
        //    bool blnHasGetICD = objDomain.m_blnAddICDToDataGridView(m_dgwInDiagnosis);
        //    if (!blnHasGetICD)
        //    {
        //        dtbICD.Rows.RemoveAt(dtbICD.Rows.Count - 1);
        //    }
        //    objDomain = null;
        //}

        //private void m_cmdDeleteInDiag_Click(object sender, EventArgs e)
        //{
        //    if (m_dgwInDiagnosis.SelectedCells.Count>0)
        //    {
        //        DataRowView drCurrent = m_dgwInDiagnosis.Rows[m_dgwInDiagnosis.SelectedCells[0].RowIndex].DataBoundItem as DataRowView;
        //        DataTable dtbICD = m_dgwInDiagnosis.DataSource as DataTable;
        //        dtbICD.Rows.Remove(drCurrent.Row);
        //    }
        //}
        #endregion

        private void m_txtOutPatientDiag_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9 )
            {
                clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
                objDomain.m_mthAddICDToTextBox(m_txtOutPatientDiag, m_txtOutPatientDiagICD);
                objDomain = null;
            }
        }

        private void m_txtOutPatientDiagICD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
                objDomain.m_mthAddICDToTextBox(m_txtOutPatientDiag, m_txtOutPatientDiagICD);
                objDomain = null;
            }
        }

        private void m_rdbGeneral_Click(object sender, EventArgs e)
        {
            m_gpbblfx.Tag = "1";
        }

        private void m_rdbUrgent_Click(object sender, EventArgs e)
        {
            m_gpbblfx.Tag = "2";
        }

        private void m_rdbTroubleshooting_Click(object sender, EventArgs e)
        {
            m_gpbblfx.Tag = "3";
        }

        private void m_rdbCriticallyill_Click(object sender, EventArgs e)
        {
            m_gpbblfx.Tag = "4";
        }

        #region 删除入院诊断
        //private void m_dgwInDiagnosis_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        //{
        //    //取得被表示的控件
        //    DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;
        //    //事件处理器删除
        //    tb.KeyDown -= new KeyEventHandler(DataGridViewTextBoxCell_KeyDown);

        //    //检测对应列
        //    if (m_dgwInDiagnosis.CurrentCell.OwningColumn.Name == "clmIndiagnosis" || m_dgwInDiagnosis.CurrentCell.OwningColumn.Name == "clmInDiagICD")
        //    {
        //        // KeyDown事件处理器追加
        //        tb.KeyDown += new KeyEventHandler(DataGridViewTextBoxCell_KeyDown);
        //    }

        //}

        //private void DataGridViewTextBoxCell_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.F9 && m_dgwInDiagnosis.SelectedCells.Count > 0)
        //    {
        //        clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
        //        objDomain.m_blnAddICDToDataGridView(m_dgwInDiagnosis);
        //        objDomain = null;
        //    }
        //}
        #endregion
    }
}
