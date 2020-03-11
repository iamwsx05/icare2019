using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.emr.EMR_SynchronousCase.EditCaseControl
{
    /// <summary>
    /// 肿瘤专科病人治疗记录表
    /// </summary>
    public partial class ctlTumorInfo : UserControl, infSynchronousCaseControl
    {
        /// <summary>
        /// 肿瘤专科病人治疗记录表
        /// </summary>
        public ctlTumorInfo()
        {
            InitializeComponent();
            m_dgwTumorMedicine.AutoGenerateColumns = false;
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

            if (p_dsContent.Tables.Contains("HIS_BA6") && p_dsContent.Tables["HIS_BA6"].Rows.Count > 0)
            {
                DataRow drBA6 = p_dsContent.Tables["HIS_BA6"].Rows[0];
                if (drBA6["FFLFSBH"] != DBNull.Value)
                {
                    for (int iI = 0; iI < m_cboRadiationMode.Items.Count; iI++)
                    {
                        if (((clsGDCaseDictVO)m_cboRadiationMode.Items[iI]).m_strCode == drBA6["FFLFSBH"].ToString())
                        {
                            m_cboRadiationMode.SelectedIndex = iI;
                            break;
                        }
                    }
                }
                if (drBA6["FFLCXBH"] != DBNull.Value)
                {
                    for (int iI = 0; iI < m_cboRadiationForm.Items.Count; iI++)
                    {
                        if (((clsGDCaseDictVO)m_cboRadiationForm.Items[iI]).m_strCode == drBA6["FFLCXBH"].ToString())
                        {
                            m_cboRadiationForm.SelectedIndex = iI;
                            break;
                        }
                    }
                }
                if (drBA6["FFLZZBH"] != DBNull.Value)
                {
                    for (int iI = 0; iI < m_cboRadiationEquipment.Items.Count; iI++)
                    {
                        if (((clsGDCaseDictVO)m_cboRadiationEquipment.Items[iI]).m_strCode == drBA6["FFLZZBH"].ToString())
                        {
                            m_cboRadiationEquipment.SelectedIndex = iI;
                            break;
                        }
                    }
                }
                m_txtPrimaryTumorGY.Text = drBA6["FYJY"].ToString(); ;
                m_txtPrimaryTumorTimes.Text = drBA6["FYCS"].ToString();
                m_txtPrimaryTumorDay.Text = drBA6["FYTS"].ToString();
                DateTime dtmTemp = DateTime.MinValue;
                if (DateTime.TryParse(drBA6["FYRQ1"].ToString(),out dtmTemp))
                {
                    m_dtpPrimaryTumorBegin.Text = dtmTemp.ToString("yyyy年MM月dd日");
                }
                if (DateTime.TryParse(drBA6["FYRQ2"].ToString(), out dtmTemp))
                {
                    m_dtmPrimaryTumorEnd.Text = dtmTemp.ToString("yyyy年MM月dd日");
                }
                m_txtLymphadenGy.Text = drBA6["FQJY"].ToString();
                m_txtLymphadenTimes.Text = drBA6["FQCS"].ToString();
                m_txtLymphadenDay.Text = drBA6["FQTS"].ToString();
                if (DateTime.TryParse(drBA6["FQRQ1"].ToString(), out dtmTemp))
                {
                    m_dtpLymphadenBegin.Text = dtmTemp.ToString("yyyy年MM月dd日");
                }
                if (DateTime.TryParse(drBA6["FQRQ2"].ToString(), out dtmTemp))
                {
                    m_dtpLymphadenEnd.Text = dtmTemp.ToString("yyyy年MM月dd日");
                }
                m_txtMetastasisGy.Text = drBA6["FZJY"].ToString();
                m_txtMetastasisTimes.Text = drBA6["FZCS"].ToString();
                m_txtMetastasisDay.Text = drBA6["FZTS"].ToString();
                if (DateTime.TryParse(drBA6["FZRQ1"].ToString(), out dtmTemp))
                {
                    m_dtpMetastasisBegin.Text = dtmTemp.ToString("yyyy年MM月dd日");
                }
                if (DateTime.TryParse(drBA6["FZRQ2"].ToString(), out dtmTemp))
                {
                    m_dtpMetastasisEnd.Text = dtmTemp.ToString("yyyy年MM月dd日");
                }
                if (drBA6["FHLFSBH"] != DBNull.Value)
                {
                    for (int iI = 0; iI < m_cboChemotherapyMode.Items.Count; iI++)
                    {
                        if (((clsGDCaseDictVO)m_cboChemotherapyMode.Items[iI]).m_strCode == drBA6["FHLFSBH"].ToString())
                        {
                            m_cboChemotherapyMode.SelectedIndex = iI;
                            break;
                        }
                    }
                }
                if (drBA6["FHLFFBH"] != DBNull.Value)
                {
                    for (int iI = 0; iI < m_cboChemotherapyMethod.Items.Count; iI++)
                    {
                        if (((clsGDCaseDictVO)m_cboChemotherapyMethod.Items[iI]).m_strCode == drBA6["FHLFFBH"].ToString())
                        {
                            m_cboChemotherapyMethod.SelectedIndex = iI;
                            break;
                        }
                    }
                }
            }

            DataTable dtbTumor = new DataTable();
            dtbTumor.Columns.Add("curedate");
            dtbTumor.Columns.Add("medicine");
            dtbTumor.Columns.Add("treatment");
            dtbTumor.Columns.Add("field_cr");
            dtbTumor.Columns.Add("field_pr");
            dtbTumor.Columns.Add("field_mr");
            dtbTumor.Columns.Add("field_s");
            dtbTumor.Columns.Add("field_p");
            dtbTumor.Columns.Add("field_na");
            dtbTumor.Columns.Add("registerid_chr");
            dtbTumor.Columns.Add("result");
            m_dgwTumorMedicine.DataSource = dtbTumor;

            if (p_dsContent.Tables.Contains("HIS_BA7") && p_dsContent.Tables["HIS_BA7"].Rows.Count > 0)
            {
                DataTable dtbBA7 = p_dsContent.Tables["HIS_BA7"];
                int intRowsCount = dtbBA7.Rows.Count;
                DataRow drTemp = null;
                dtbTumor.BeginLoadData();
                DateTime dtmTemp = DateTime.MinValue;
                for (int iRow = 0; iRow < intRowsCount; iRow++)
                {
                    drTemp = dtbBA7.Rows[iRow];
                    DataRow drNew = dtbTumor.NewRow();
                    if (DateTime.TryParse(drTemp["FHLRQ1"].ToString(), out dtmTemp))
                    {
                        drNew["curedate"] = dtmTemp;
                    }
                    drNew["medicine"] = drTemp["FHLDRUG"].ToString();
                    drNew["treatment"] = drTemp["FHLPROC"].ToString();
                    drNew["result"] = drTemp["FHLLX"].ToString();
                    dtbTumor.LoadDataRow(drNew.ItemArray, true);
                }
                dtbTumor.EndLoadData();
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
            clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
            DataTable dtbResult = null;
            long lngRes = objDomain.m_lngGetChemotherapyInfo(p_strRegisterID, out dtbResult);
            if (dtbResult != null && dtbResult.Rows.Count > 0)
            {
                int intTemp = 0;
                DataRow drInfo = dtbResult.Rows[0];
                if (int.TryParse(drInfo["RTMODESEQ"].ToString(), out intTemp))
                {
                    m_cboRadiationMode.SelectedIndex = intTemp;
                }
                if (int.TryParse(drInfo["RTRULESEQ"].ToString(), out intTemp))
                {
                    m_cboRadiationForm.SelectedIndex = intTemp;
                }
                if (drInfo["RTCO"].ToString() == "1")
                {
                    m_cboRadiationEquipment.SelectedIndex = 0;
                }
                else if (drInfo["RTACCELERATOR"].ToString() == "1")
                {
                    m_cboRadiationEquipment.SelectedIndex = 1;
                }
                else if (drInfo["RTX_RAY"].ToString() == "1")
                {
                    m_cboRadiationEquipment.SelectedIndex = 2;
                }
                else if (drInfo["RTLACUNA"].ToString() == "1")
                {
                    m_cboRadiationEquipment.SelectedIndex = 3;
                }
                if (int.TryParse(drInfo["ORIGINALDISEASESEQ"].ToString(), out intTemp))
                {
                    m_cboPrimaryTumorTimes.SelectedIndex = intTemp;
                }
                m_txtPrimaryTumorGY.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drInfo["ORIGINALDISEASEGY"].ToString(), drInfo["ORIGINALDISEASEGYXML"].ToString());
                m_txtPrimaryTumorTimes.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drInfo["ORIGINALDISEASETIMES"].ToString(), drInfo["ORIGINALDISEASETIMESXML"].ToString());
                m_txtPrimaryTumorDay.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drInfo["ORIGINALDISEASEDAYS"].ToString(), drInfo["ORIGINALDISEASEDAYSXML"].ToString());

                DateTime dtmTemp = DateTime.MinValue;
                DateTime dtmIni = new DateTime(1900, 1, 1);
                if (DateTime.TryParse(drInfo["ORIGINALDISEASEBEGINDATE"].ToString(), out dtmTemp))
                {
                    if (dtmTemp != DateTime.MinValue && dtmTemp != dtmIni)
                    {
                        m_dtpPrimaryTumorBegin.Text = dtmTemp.ToString("yyyy年MM月dd日");
                    }
                }
                if (DateTime.TryParse(drInfo["ORIGINALDISEASEENDDATE"].ToString(), out dtmTemp))
                {
                    if (dtmTemp != DateTime.MinValue && dtmTemp != dtmIni)
                    {
                        m_dtmPrimaryTumorEnd.Text = dtmTemp.ToString("yyyy年MM月dd日");
                    }
                }
                if (int.TryParse(drInfo["LYMPHSEQ"].ToString(), out intTemp))
                {
                    m_cboLymphadenTimes.SelectedIndex = intTemp;
                }
                m_txtLymphadenGy.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drInfo["LYMPHGY"].ToString(), drInfo["LYMPHGYXML"].ToString());
                m_txtLymphadenTimes.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drInfo["LYMPHTIMES"].ToString(), drInfo["LYMPHTIMESXML"].ToString());
                m_txtLymphadenDay.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drInfo["LYMPHDAYS"].ToString(), drInfo["LYMPHDAYSXML"].ToString());

                if (DateTime.TryParse(drInfo["LYMPHBEGINDATE"].ToString(), out dtmTemp))
                {
                    if (dtmTemp != DateTime.MinValue && dtmTemp != dtmIni)
                    {
                        m_dtpLymphadenBegin.Text = dtmTemp.ToString("yyyy年MM月dd日");
                    }
                }
                if (DateTime.TryParse(drInfo["LYMPHENDDATE"].ToString(), out dtmTemp))
                {
                    if (dtmTemp != DateTime.MinValue && dtmTemp != dtmIni)
                    {
                        m_dtpLymphadenEnd.Text = dtmTemp.ToString("yyyy年MM月dd日");
                    }
                }
                m_txtMetastasisGy.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drInfo["METASTASISGY"].ToString(), drInfo["METASTASISGYXML"].ToString());
                m_txtMetastasisTimes.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drInfo["METASTASISTIMES"].ToString(), drInfo["METASTASISTIMESXML"].ToString());
                m_txtMetastasisDay.Text = com.digitalwave.controls.ctlRichTextBox.s_strGetRightText(drInfo["METASTASISDAYS"].ToString(), drInfo["METASTASISDAYSXML"].ToString());
                if (DateTime.TryParse(drInfo["METASTASISBEGINDATE"].ToString(), out dtmTemp))
                {
                    if (dtmTemp != DateTime.MinValue && dtmTemp != dtmIni)
                    {
                        m_dtpMetastasisBegin.Text = dtmTemp.ToString("yyyy年MM月dd日");
                    }
                }
                if (DateTime.TryParse(drInfo["METASTASISENDDATE"].ToString(), out dtmTemp))
                {
                    if (dtmTemp != DateTime.MinValue && dtmTemp != dtmIni)
                    {
                        m_dtpMetastasisEnd.Text = dtmTemp.ToString("yyyy年MM月dd日");
                    }
                }
                if (int.TryParse(drInfo["CHEMOTHERAPYMODESEQ"].ToString(), out intTemp))
                {
                    m_cboChemotherapyMode.SelectedIndex = intTemp;
                }
                if (drInfo["CHEMOTHERAPYWHOLEBODY"].ToString() == "1")
                {
                    m_cboChemotherapyMethod.SelectedIndex = 0;
                }
                //else if (drInfo["CHEMOTHERAPYLOCAL"].ToString() == "1")
                //{
                //    m_cboChemotherapyMethod.SelectedIndex = 1;
                //}
                else if (drInfo["CHEMOTHERAPYINTUBATE"].ToString() == "1")
                {
                    m_cboChemotherapyMethod.SelectedIndex = 1;
                }
                else if (drInfo["CHEMOTHERAPYTHORAX"].ToString() == "1")
                {
                    m_cboChemotherapyMethod.SelectedIndex = 2;
                }
                else if (drInfo["CHEMOTHERAPYABDOMEN"].ToString() == "1")
                {
                    m_cboChemotherapyMethod.SelectedIndex = 3;
                }
                else if (drInfo["CHEMOTHERAPYSPINAL"].ToString() == "1")
                {
                    m_cboChemotherapyMethod.SelectedIndex = 4;
                }
                else if (drInfo["CHEMOTHERAPYOTHER"].ToString() == "1")
                {
                    m_cboChemotherapyMethod.SelectedIndex = 5;
                }
            }

            DataTable dtbMedicine = null;
            lngRes = objDomain.m_lngGetChemotherapyMedicine(p_strRegisterID, out dtbMedicine);
            if (dtbMedicine != null)
            {
                dtbMedicine.Columns.Add("result");
                if (dtbMedicine.Rows.Count > 0)
                {
                    DataRow drTemp = null;
                    for (int iRow = 0; iRow < dtbMedicine.Rows.Count; iRow++)
                    {
                        drTemp = dtbMedicine.Rows[iRow];
                        if (drTemp["FIELD_CR"].ToString() == "1")
                        {
                            drTemp["result"] = "CR";
                        }
                        else if (drTemp["FIELD_PR"].ToString() == "1")
                        {
                            drTemp["result"] = "PR";
                        }
                        else if (drTemp["FIELD_MR"].ToString() == "1")
                        {
                            drTemp["result"] = "MR";
                        }
                        else if (drTemp["FIELD_S"].ToString() == "1")
                        {
                            drTemp["result"] = "S";
                        }
                        else if (drTemp["FIELD_P"].ToString() == "1")
                        {
                            drTemp["result"] = "P";
                        }
                        else if (drTemp["FIELD_NA"].ToString() == "1")
                        {
                            drTemp["result"] = "NA";
                        }
                    }
                }
                
                m_dgwTumorMedicine.DataSource = dtbMedicine;
            }
            m_blnHasInit = true;
            objDomain = null;
        } 
        #endregion

        #region 根据字典初始化界面固定选项值
        /// <summary>
        /// 根据字典初始化界面固定选项值
        /// </summary>
        /// <param name="p_dtbDict">字典</param>
        public void m_mthInitDict(DataTable p_dtbDict)
        {
            if (p_dtbDict == null || p_dtbDict.Rows.Count == 0)
            {
                return;
            }
            DataView drView = new DataView(p_dtbDict);
            //放疗方式
            drView.RowFilter = "fcode='GBFLFS'";
            drView.Sort = "fbh ASC";
            if (drView != null && drView.Count > 0)
            {
                m_cboRadiationMode.Items.Clear();
                clsGDCaseDictVO[] objDict = new clsGDCaseDictVO[drView.Count];
                for (int iM = 0; iM < drView.Count; iM++)
                {
                    objDict[iM] = new clsGDCaseDictVO();
                    objDict[iM].m_strCode = drView[iM]["fbh"].ToString();
                    objDict[iM].m_strName = drView[iM]["fmc"].ToString();
                }
                m_cboRadiationMode.Items.AddRange(objDict);
            }
            //放疗程式
            drView.RowFilter = "fcode='GBFLCS'";
            drView.Sort = "fbh ASC";
            if (drView != null && drView.Count > 0)
            {
                m_cboRadiationForm.Items.Clear();
                clsGDCaseDictVO[] objDict = new clsGDCaseDictVO[drView.Count];
                for (int iM = 0; iM < drView.Count; iM++)
                {
                    objDict[iM] = new clsGDCaseDictVO();
                    objDict[iM].m_strCode = drView[iM]["fbh"].ToString();
                    objDict[iM].m_strName = drView[iM]["fmc"].ToString();
                }
                m_cboRadiationForm.Items.AddRange(objDict);
            }
            //放疗装置
            drView.RowFilter = "fcode='GBFLZZ'";
            drView.Sort = "fbh ASC";
            if (drView != null && drView.Count > 0)
            {
                m_cboRadiationEquipment.Items.Clear();
                clsGDCaseDictVO[] objDict = new clsGDCaseDictVO[drView.Count];
                for (int iM = 0; iM < drView.Count; iM++)
                {
                    objDict[iM] = new clsGDCaseDictVO();
                    objDict[iM].m_strCode = drView[iM]["fbh"].ToString();
                    objDict[iM].m_strName = drView[iM]["fmc"].ToString();
                }
                m_cboRadiationEquipment.Items.AddRange(objDict);
            }
            //化疗方式
            drView.RowFilter = "fcode='GBHLFS'";
            drView.Sort = "fbh ASC";
            if (drView != null && drView.Count > 0)
            {
                m_cboChemotherapyMode.Items.Clear();
                clsGDCaseDictVO[] objDict = new clsGDCaseDictVO[drView.Count];
                for (int iM = 0; iM < drView.Count; iM++)
                {
                    objDict[iM] = new clsGDCaseDictVO();
                    objDict[iM].m_strCode = drView[iM]["fbh"].ToString();
                    objDict[iM].m_strName = drView[iM]["fmc"].ToString();
                }
                m_cboChemotherapyMode.Items.AddRange(objDict);
            }
            //化疗方法
            drView.RowFilter = "fcode='GBHLFF'";
            drView.Sort = "fbh ASC";
            if (drView != null && drView.Count > 0)
            {
                m_cboChemotherapyMethod.Items.Clear();
                clsGDCaseDictVO[] objDict = new clsGDCaseDictVO[drView.Count];
                for (int iM = 0; iM < drView.Count; iM++)
                {
                    objDict[iM] = new clsGDCaseDictVO();
                    objDict[iM].m_strCode = drView[iM]["fbh"].ToString();
                    objDict[iM].m_strName = drView[iM]["fmc"].ToString();
                }
                m_cboChemotherapyMethod.Items.AddRange(objDict);
            }
            //化疗疗效
            drView.RowFilter = "fcode='GBHLLX'";
            drView.Sort = "fbh ASC";
            if (drView != null && drView.Count > 0)
            {
                clmResult.Items.Clear();
                string[] strDict = new string[drView.Count];
                for (int iM = 0; iM < drView.Count; iM++)
                {
                    strDict[iM] = drView[iM]["fmc"].ToString();
                }
                clmResult.Items.AddRange(strDict);
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
            DataTable dtbBA6 = null;
            if (!p_dsCaseContent.Tables.Contains("HIS_BA6"))
            {
                dtbBA6 = new DataTable();
                dtbBA6.TableName = "HIS_BA6";
                dtbBA6.Columns.Add("FPRN");
                dtbBA6.Columns.Add("FTIMES");
                dtbBA6.Columns.Add("FFLFSBH");
                dtbBA6.Columns.Add("FFLFS");
                dtbBA6.Columns.Add("FFLCXBH");
                dtbBA6.Columns.Add("FFLCX");
                dtbBA6.Columns.Add("FFLZZBH");
                dtbBA6.Columns.Add("FFLZZ");
                dtbBA6.Columns.Add("FYJY");
                dtbBA6.Columns.Add("FYCS");
                dtbBA6.Columns.Add("FYTS");
                dtbBA6.Columns.Add("FYRQ1");
                dtbBA6.Columns.Add("FYRQ2");
                dtbBA6.Columns.Add("FQJY");
                dtbBA6.Columns.Add("FQCS");
                dtbBA6.Columns.Add("FQTS");
                dtbBA6.Columns.Add("FQRQ1");
                dtbBA6.Columns.Add("FQRQ2");
                dtbBA6.Columns.Add("FZNAME");
                dtbBA6.Columns.Add("FZJY");
                dtbBA6.Columns.Add("FZCS");
                dtbBA6.Columns.Add("FZTS");
                dtbBA6.Columns.Add("FZRQ1");
                dtbBA6.Columns.Add("FZRQ2");
                dtbBA6.Columns.Add("FHLFSBH");
                dtbBA6.Columns.Add("FHLFS");
                dtbBA6.Columns.Add("FHLFFBH");
                dtbBA6.Columns.Add("FHLFF");
                p_dsCaseContent.Tables.Add(dtbBA6);
            }
            else
            {
                dtbBA6 = p_dsCaseContent.Tables["HIS_BA6"];
            }

            DataTable dtbBA1 = p_dsCaseContent.Tables["HIS_BA1"];
            string strPRN = dtbBA1.Rows[0]["fprn"].ToString();
            string strTimes = dtbBA1.Rows[0]["FTIMES"].ToString();

            DataRow drBA6 = null;
            if (dtbBA6.Rows.Count > 0)
            {
                drBA6 = dtbBA6.Rows[0];
            }
            else
            {
                drBA6 = dtbBA6.NewRow();
            }
            drBA6["FPRN"] = strPRN;
            drBA6["FTIMES"] = strTimes;
            if (m_cboRadiationMode.SelectedItem != null)
            {
                drBA6["FFLFSBH"] = ((clsGDCaseDictVO)m_cboRadiationMode.SelectedItem).m_strCode;
                drBA6["FFLFS"] = m_cboRadiationMode.Text;
            }
            if (m_cboRadiationForm.SelectedItem != null)
            {
                drBA6["FFLCXBH"] = ((clsGDCaseDictVO)m_cboRadiationForm.SelectedItem).m_strCode;
                drBA6["FFLCX"] = m_cboRadiationForm.Text;
            }
            if (m_cboRadiationEquipment.SelectedItem != null)
            {
                drBA6["FFLZZBH"] = ((clsGDCaseDictVO)m_cboRadiationEquipment.SelectedItem).m_strCode;
                drBA6["FFLZZ"] = m_cboRadiationEquipment.Text;
            }
            drBA6["FYJY"] = m_txtPrimaryTumorGY.Text;
            drBA6["FYCS"] = m_txtPrimaryTumorTimes.Text;
            drBA6["FYCS"] = m_txtPrimaryTumorDay.Text;
            DateTime dtmTemp = DateTime.MinValue;
            if (DateTime.TryParse(m_dtpPrimaryTumorBegin.Text,out dtmTemp))
            {
                drBA6["FYRQ1"] = dtmTemp;
            }
            if (DateTime.TryParse(m_dtpMetastasisEnd.Text, out dtmTemp))
            {
                drBA6["FYRQ2"] = dtmTemp;
            }
            drBA6["FQJY"] = m_txtLymphadenGy.Text;
            drBA6["FQCS"] = m_txtLymphadenTimes.Text;
            drBA6["FQTS"] = m_txtLymphadenDay.Text;
            if (DateTime.TryParse(m_dtpLymphadenBegin.Text, out dtmTemp))
            {
                drBA6["FQRQ1"] = dtmTemp;
            }
            if (DateTime.TryParse(m_dtpLymphadenEnd.Text, out dtmTemp))
            {
                drBA6["FQRQ2"] = dtmTemp;
            }
            drBA6["FZJY"] = m_txtMetastasisGy.Text;
            drBA6["FZCS"] = m_txtMetastasisTimes.Text;
            drBA6["FZTS"] = m_txtMetastasisDay.Text;
            if (DateTime.TryParse(m_dtpMetastasisBegin.Text, out dtmTemp))
            {
                drBA6["FZRQ1"] = dtmTemp;
            }
            if (DateTime.TryParse(m_dtpMetastasisEnd.Text, out dtmTemp))
            {
                drBA6["FZRQ2"] = dtmTemp;
            }
            if (m_cboChemotherapyMode.SelectedItem != null)
            {
                drBA6["FHLFSBH"] = ((clsGDCaseDictVO)m_cboChemotherapyMode.SelectedItem).m_strCode;
                drBA6["FHLFS"] = m_cboChemotherapyMode.Text;
            }
            if (m_cboChemotherapyMethod.SelectedItem != null)
            {
                drBA6["FHLFFBH"] = ((clsGDCaseDictVO)m_cboChemotherapyMethod.SelectedItem).m_strCode;
                drBA6["FHLFF"] = m_cboChemotherapyMethod.Text;
            }

            DataTable dtbBA7 = null;
            if (!p_dsCaseContent.Tables.Contains("HIS_BA7"))
            {
                dtbBA7 = new DataTable();
                dtbBA7.TableName = "HIS_BA7";
                dtbBA7.Columns.Add("FPRN");
                dtbBA7.Columns.Add("FTIMES");
                dtbBA7.Columns.Add("FHLRQ1");
                dtbBA7.Columns.Add("FHLRQ2");
                dtbBA7.Columns.Add("FHLDRUG");
                dtbBA7.Columns.Add("FHLPROC");
                dtbBA7.Columns.Add("FHLLXBH");
                dtbBA7.Columns.Add("FHLLX");
                p_dsCaseContent.Tables.Add(dtbBA7);
            }
            else
            {
                dtbBA7 = p_dsCaseContent.Tables["HIS_BA7"];
            }

            DataTable dtbTumor = m_dgwTumorMedicine.DataSource as DataTable;
            if (dtbTumor != null && dtbTumor.Rows.Count > 0)
            {
                DataRow drTemp = null;
                for (int iRow = 0; iRow < dtbTumor.Rows.Count; iRow++)
                {
                    DataRow drNew = dtbBA7.NewRow();
                    drTemp = dtbTumor.Rows[iRow];
                    drNew["FPRN"] = strPRN;
                    drNew["FTIMES"] = strTimes;
                    if (DateTime.TryParse(drTemp["curedate"].ToString(), out dtmTemp))
                    {
                        drNew["FHLRQ1"] = dtmTemp;
                    }
                    drNew["FHLDRUG"] = drTemp["medicine"].ToString();
                    drNew["FHLPROC"] = drTemp["treatment"].ToString();
                    if (drTemp["result"].ToString() == "CR")
                    {
                        drNew["FHLLXBH"] = 1;
                    }
                    else if (drTemp["result"].ToString() == "PR")
                    {
                        drNew["FHLLXBH"] = 2;
                    }
                    else if (drTemp["result"].ToString() == "MR")
                    {
                        drNew["FHLLXBH"] = 3;
                    }
                    else if (drTemp["result"].ToString() == "S")
                    {
                        drNew["FHLLXBH"] = 4;
                    }
                    else if (drTemp["result"].ToString() == "P")
                    {
                        drNew["FHLLXBH"] = 5;
                    }
                    else if (drTemp["result"].ToString() == "NA")
                    {
                        drNew["FHLLXBH"] = 6;
                    }
                    
                    drNew["FHLLX"] = drTemp["result"].ToString();
                }
            }
        } 
        #endregion
        #endregion

        private void m_cmdAddTumorMedicine_Click(object sender, EventArgs e)
        {
            DataTable dtbICD = m_dgwTumorMedicine.DataSource as DataTable;
            if (dtbICD == null || dtbICD.Columns.Count == 0)
            {
                dtbICD = new DataTable();
                dtbICD.Columns.Add("curedate");
                dtbICD.Columns.Add("medicine");
                dtbICD.Columns.Add("treatment");
                dtbICD.Columns.Add("field_cr");
                dtbICD.Columns.Add("field_pr");
                dtbICD.Columns.Add("field_mr");
                dtbICD.Columns.Add("field_s");
                dtbICD.Columns.Add("field_p");
                dtbICD.Columns.Add("field_na");
                dtbICD.Columns.Add("registerid_chr");
                m_dgwTumorMedicine.DataSource = dtbICD;
            }

            DataRow drNew = dtbICD.NewRow();
            dtbICD.Rows.Add(drNew);
        }

        private void m_cmdDeleteTumorMedicine_Click(object sender, EventArgs e)
        {
            if (m_dgwTumorMedicine.SelectedCells.Count > 0)
            {
                DataRowView drCurrent = m_dgwTumorMedicine.Rows[m_dgwTumorMedicine.SelectedCells[0].RowIndex].DataBoundItem as DataRowView;
                DataTable dtbICD = m_dgwTumorMedicine.DataSource as DataTable;
                dtbICD.Rows.Remove(drCurrent.Row);
            }
        }
    }
}
