using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Text.RegularExpressions;

namespace com.digitalwave.emr.EMR_SynchronousCase
{
    /// <summary>
    /// 病案接口
    /// </summary>
    public partial class frmEMR_SynchronousCase : Form
    {
        #region 全局变量
        /// <summary>
        /// 查询结果
        /// </summary>
        private DataTable m_dtbPatient = new DataTable();
        /// <summary>
        /// 作为显示至界面的数据源

        /// </summary>
        private DataTable m_dtbShow = new DataTable();
        /// <summary>
        /// 病人查询结果
        /// </summary>
        private string m_strPatientCount = "共查询出病人{0}人次";
        /// <summary>
        /// 需同步病案内容
        /// </summary>
        private clsEMR_SynchronousCaseValue[] m_objCaseValueArr = null;
        /// <summary>
        /// 是否重新查询病案数据
        /// </summary>
        private bool m_blnIsCaseDataChange = false;
        private DataTable m_dtbHasSynchronousCasePatient = null;
        #endregion

        #region 构造函数

        /// <summary>
        /// 病案接口
        /// </summary>
        public frmEMR_SynchronousCase()
        {
            InitializeComponent();

            this.m_dgvOutPatient.AutoGenerateColumns = false;
            this.m_dtpStart.Value = DateTime.Now.AddDays(-1);
            this.m_lblPatientCount.Text = string.Format(m_strPatientCount, "0");
        } 
        #endregion

        #region 事件
        private void m_cmdGetData_Click(object sender, EventArgs e)
        {
            m_mthClear();

            try
            {
                this.Cursor = Cursors.WaitCursor;

                m_mthGetDeptList();

                m_mthGetCaseData();
            }
            catch (Exception Ex)
            {
                string strEx = Ex.Message;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void m_chkChooseDept_CheckedChanged(object sender, EventArgs e)
        {
            if (m_chkChooseDept.Checked)
            {
                m_lblDeptTips.Visible = true;
                m_cmdShowDeptList.Visible = true;

                m_lblDeptTips.Text = "共有病区" + m_lsvDeptList.Items.Count.ToString() + "/已选择病区" + m_lsvDeptList.CheckedItems.Count.ToString();                
            }
            else
            {
                m_lblDeptTips.Visible = false;
                m_cmdShowDeptList.Visible = false;
            }
            m_mthSetDataToUI();
            m_lblPatientCount.Text = string.Format(m_strPatientCount, m_dtbShow.Rows.Count);
        }

        /// <summary>
        /// 是否需要过滤显示病人(在首次显示科室列表时使用控制)
        /// </summary>
        private bool m_blnNeedFilterPatient = true;
        private void m_cmdShowDeptList_Click(object sender, EventArgs e)
        {
            m_blnNeedFilterPatient = false;
            if (m_lsvDeptList.Items.Count > 0)
            {
                m_lsvDeptList.Location = new System.Drawing.Point(m_lblDeptTips.Location.X, m_lblDeptTips.Location.Y + m_lblDeptTips.Size.Height - 1);
                m_lsvDeptList.Visible = true;

                m_lsvDeptList.Focus();
            }
            m_blnNeedFilterPatient = true;
        }

        private void m_lsvDeptList_Leave(object sender, EventArgs e)
        {
            m_lsvDeptList.Visible = false;
        }

        private void m_lsvDeptList_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (m_blnNeedFilterPatient)
            {
                m_mthFilterPatientDataByDept();

                m_lblDeptTips.Text = "共有病区" + m_lsvDeptList.Items.Count.ToString() + "/已选择病区" + m_lsvDeptList.CheckedItems.Count.ToString();
            }            
        }

        /// <summary>
        /// 是否需要刷新已选择病人数

        /// </summary>
        private bool m_blnIsNeedShowSelectedCount = true;
        private void m_ctlSC_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPageIndex == 1 && e.Action == TabControlAction.Selected && m_blnIsCaseDataChange)
            {
                m_blnIsCaseDataChange = false;
                m_blnIsNeedShowSelectedCount = false;
                m_mthSetCaseValueToPatientSelect();
                m_blnIsNeedShowSelectedCount = true;
            }
        }

        private void m_cmdSelectAll_Click(object sender, EventArgs e)
        {
            if (m_lsvSelectPatient.Items.Count > 0)
            {
                m_blnIsNeedShowSelectedCount = false;
                foreach (ListViewItem lsi in m_lsvSelectPatient.Items)
                {
                    lsi.Checked = true;
                }
                m_blnIsNeedShowSelectedCount = true;
                m_txtSelectedPatientCount.Text = m_lsvSelectPatient.CheckedItems.Count.ToString();
            }
        }

        private void m_cmdUnSelectAll_Click(object sender, EventArgs e)
        {
            if (m_lsvSelectPatient.Items.Count > 0)
            {
                m_blnIsNeedShowSelectedCount = false;
                foreach (ListViewItem lsi in m_lsvSelectPatient.CheckedItems)
                {
                    lsi.Checked = false;
                }
                m_blnIsNeedShowSelectedCount = true;
                m_txtSelectedPatientCount.Text = m_lsvSelectPatient.CheckedItems.Count.ToString();
            }
        }

        private void m_lsvSelectPatient_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            //不使用此事件，此事件将在每个Item添加之后都自行激发一次，严重影响运行速度
            if (!m_blnIsNeedShowSelectedCount)
            {
                return;
            }
            m_txtSelectedPatientCount.Text = m_lsvSelectPatient.CheckedItems.Count.ToString();
        }

        private void m_lsvSelectPatient_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!m_blnIsNeedShowSelectedCount)
            {
                return;
            }

            int intCheckCount = m_lsvSelectPatient.CheckedItems.Count;
            if (e.NewValue == CheckState.Checked)
            {
                intCheckCount++;
                if (m_lsvSelectPatient.Focused)
                {
                    m_lsvSelectPatient.Items[e.Index].Selected = true;
                }                
            }
            else
            {
                intCheckCount--;
            }
            m_txtSelectedPatientCount.Text = intCheckCount.ToString();
        }

        private void m_cmdSendData_Click(object sender, EventArgs e)
        {
            m_mthSynchronousCase();
        }

        private void m_chkUnShowSend_CheckedChanged(object sender, EventArgs e)
        {
            if (m_lsvSelectPatient.Items.Count > 0 && m_chkUnShowSend.Checked)
            {
                m_mthRemoveHasSynchronousPatient();
            }
            else if (!m_chkUnShowSend.Checked)
            {
                m_blnIsNeedShowSelectedCount = false;
                m_mthSetCaseValueToPatientSelect();
                m_blnIsNeedShowSelectedCount = true;
            }
        }

        private void m_lsvSelectPatient_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if ( m_lsvSelectPatient.SelectedItems.Count > 0)
            {
                m_chkAllowChange.Checked = false;
                int intSelectedIndex = m_lsvSelectPatient.SelectedItems[0].Index;
                if (m_objCaseValueArr != null && intSelectedIndex >= 0 && intSelectedIndex < m_objCaseValueArr.Length)
                {
                    clsEMR_SynchronousCaseValue objvalue = m_lsvSelectPatient.SelectedItems[0].Tag as clsEMR_SynchronousCaseValue;
                    m_mthSetValueToForm(objvalue);
                }
            }            
        }

        private void m_lsvSelectPatient_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected && m_chkAllowChange.Checked)
            {
                ListViewItem lsi = e.Item;
                clsEMR_SynchronousCaseValue objValue = lsi.Tag as clsEMR_SynchronousCaseValue;
                m_mthSaveValueChange(objValue);
            }
        }

        private void frmEMR_SynchronousCase_Load(object sender, EventArgs e)
        {
            foreach (Control ctl in panel1.Controls)
            {
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).ReadOnly = true;
                }
            }
        }

        private void m_chkAllowChange_CheckedChanged(object sender, EventArgs e)
        {
            if (m_chkAllowChange.Checked)
            {
                foreach (Control ctl in panel1.Controls)
                {
                    if (ctl is TextBox)
                    {
                        ((TextBox)ctl).ReadOnly = false;
                    }
                }
            }
            else
            {
                foreach (Control ctl in panel1.Controls)
                {
                    if (ctl is TextBox)
                    {
                        ((TextBox)ctl).ReadOnly = true;
                    }
                }
            }
        }
        #endregion

        #region 获取科室列表
        /// <summary>
        /// 获取科室列表
        /// </summary>
        private void m_mthGetDeptList()
        {
            if (m_lsvDeptList.Items.Count > 0)
            {
                return;
            }

            clsEmrDept_VO[] objDeptArr = null;
            clsEMR_SynchronousCaseDomain objDomain = new clsEMR_SynchronousCaseDomain();
            long lngRes = objDomain.m_lngGetSynchronousDeptList(out objDeptArr);
            objDomain = null;

            //try
            //{
                if (objDeptArr == null)
                {
                    return;
                }

                int intDeptCount = objDeptArr.Length;
                //m_lsvDeptList.BeginUpdate();

                //List<ListViewItem> lsiArr = new List<ListViewItem>();
                ListViewItem lsi = null;
                for (int iR = 0; iR < intDeptCount; iR++)
                {
                    lsi = new ListViewItem(objDeptArr[iR].m_strDEPTNAME_VCHR);
                    lsi.Tag = objDeptArr[iR].m_strDEPTID_CHR;
                    lsi.Checked = true;
                    //lsiArr.Add(lsi);
                    m_lsvDeptList.Items.Add(lsi);
                }
                //m_lsvDeptList.Items.AddRange(lsiArr.ToArray());
            //}
            //catch (Exception)
            //{
            //    m_lsvDeptList.EndUpdate();
            //}
        }
        #endregion

        #region 获取病人数据
        /// <summary>
        /// 获取病人数据
        /// </summary>
        private void m_mthGetCaseData()
        {
            clsEMR_SynchronousCaseDomain objDomain = new clsEMR_SynchronousCaseDomain();
            DateTime dtmStrat = Convert.ToDateTime(m_dtpStart.Value.ToString("yyyy-MM-dd 00:00:00"));
            DateTime dtmEnd = Convert.ToDateTime(m_dtpEnd.Value.ToString("yyyy-MM-dd 23:59:59"));
            long lngRes = objDomain.m_lngGetSynchronousData(dtmStrat, dtmEnd, out m_dtbPatient);
            //lngRes = objDomain.m_lngGetHasSynchronousPatien(dtmStrat, dtmEnd, out m_dtbHasSynchronousCasePatient);
            objDomain = null;

            m_mthSetDataToUI();
            m_lblPatientCount.Text = string.Format(m_strPatientCount, m_dtbShow.Rows.Count);
        } 
        #endregion

        #region 检测费用是否都为数字

        /// <summary>
        /// 检测费用是否都为数字

        /// </summary>
        /// <returns></returns>
        private bool m_blnCheckFeeIsNum()
        {
            string RegexText = @"^(-?\d+)(\.\d+)?$";
            if (m_txtSum1.Text.Trim() != string.Empty && !Regex.IsMatch(this.m_txtSum1.Text, RegexText))
            {
                m_mthShowNumberErrorDialog(m_txtSum1.AccessibleDescription);
                m_txtSum1.Focus();
                return false;
            }
            if (m_txtBedAmt.Text.Trim() != string.Empty && !Regex.IsMatch(this.m_txtBedAmt.Text, RegexText))
            {
                m_mthShowNumberErrorDialog(m_txtBedAmt.AccessibleDescription);
                m_txtBedAmt.Focus();
                return false;
            }
            if (m_txtWMAmt.Text.Trim() != string.Empty && !Regex.IsMatch(this.m_txtWMAmt.Text, RegexText))
            {
                m_mthShowNumberErrorDialog(m_txtWMAmt.AccessibleDescription);
                m_txtWMAmt.Focus();
                return false;
            }
            if (m_txtCMAmt.Text.Trim() != string.Empty && !Regex.IsMatch(this.m_txtCMAmt.Text, RegexText))
            {
                m_mthShowNumberErrorDialog(m_txtCMAmt.AccessibleDescription);
                m_txtCMAmt.Focus();
                return false;
            }
            if (m_txtCMSemiFinishedAmt.Text.Trim() != string.Empty && !Regex.IsMatch(this.m_txtCMSemiFinishedAmt.Text, RegexText))
            {
                m_mthShowNumberErrorDialog(m_txtCMSemiFinishedAmt.AccessibleDescription);
                m_txtCMSemiFinishedAmt.Focus();
                return false;
            }
            if (m_txtCMFinishedAmt.Text.Trim() != string.Empty && !Regex.IsMatch(this.m_txtCMFinishedAmt.Text, RegexText))
            {
                m_mthShowNumberErrorDialog(m_txtCMFinishedAmt.AccessibleDescription);
                m_txtCMFinishedAmt.Focus();
                return false;
            }
            if (m_txtCheckAmt.Text.Trim() != string.Empty && !Regex.IsMatch(this.m_txtCheckAmt.Text, RegexText))
            {
                m_mthShowNumberErrorDialog(m_txtCheckAmt.AccessibleDescription);
                m_txtCheckAmt.Focus();
                return false;
            }
            if (m_txtTreatmentAmt.Text.Trim() != string.Empty && !Regex.IsMatch(this.m_txtTreatmentAmt.Text, RegexText))
            {
                m_mthShowNumberErrorDialog(m_txtTreatmentAmt.AccessibleDescription);
                m_txtTreatmentAmt.Focus();
                return false;
            }
            if (m_txtRadiationAmt.Text.Trim() != string.Empty && !Regex.IsMatch(this.m_txtRadiationAmt.Text, RegexText))
            {
                m_mthShowNumberErrorDialog(m_txtRadiationAmt.AccessibleDescription);
                m_txtRadiationAmt.Focus();
                return false;
            }
            if (m_txtOperationAmt.Text.Trim() != string.Empty && !Regex.IsMatch(this.m_txtOperationAmt.Text, RegexText))
            {
                m_mthShowNumberErrorDialog(m_txtOperationAmt.AccessibleDescription);
                m_txtOperationAmt.Focus();
                return false;
            }
            if (m_txtAssayAmt.Text.Trim() != string.Empty && !Regex.IsMatch(this.m_txtAssayAmt.Text, RegexText))
            {
                m_mthShowNumberErrorDialog(m_txtAssayAmt.AccessibleDescription);
                m_txtAssayAmt.Focus();
                return false;
            }
            if (m_txtNurseAmt.Text.Trim() != string.Empty && !Regex.IsMatch(this.m_txtNurseAmt.Text, RegexText))
            {
                m_mthShowNumberErrorDialog(m_txtNurseAmt.AccessibleDescription);
                m_txtNurseAmt.Focus();
                return false;
            }
            if (m_txtO2Amt.Text.Trim() != string.Empty && !Regex.IsMatch(this.m_txtO2Amt.Text, RegexText))
            {
                m_mthShowNumberErrorDialog(m_txtO2Amt.AccessibleDescription);
                m_txtO2Amt.Focus();
                return false;
            }
            if (m_txtDeliveryChildAmt.Text.Trim() != string.Empty && !Regex.IsMatch(this.m_txtDeliveryChildAmt.Text, RegexText))
            {
                m_mthShowNumberErrorDialog(m_txtDeliveryChildAmt.AccessibleDescription);
                m_txtDeliveryChildAmt.Focus();
                return false;
            }
            if (m_txtOtherAmt.Text.Trim() != string.Empty && !Regex.IsMatch(this.m_txtOtherAmt.Text, RegexText))
            {
                m_mthShowNumberErrorDialog(m_txtOtherAmt.AccessibleDescription);
                m_txtOtherAmt.Focus();
                return false;
            }
            if (m_txtBloodAmt.Text.Trim() != string.Empty && !Regex.IsMatch(this.m_txtBloodAmt.Text, RegexText))
            {
                m_mthShowNumberErrorDialog(m_txtBloodAmt.AccessibleDescription);
                m_txtBloodAmt.Focus();
                return false;
            }
            if (m_txtAnaethesiaAmt.Text.Trim() != string.Empty && !Regex.IsMatch(this.m_txtAnaethesiaAmt.Text, RegexText))
            {
                m_mthShowNumberErrorDialog(m_txtAnaethesiaAmt.AccessibleDescription);
                m_txtAnaethesiaAmt.Focus();
                return false;
            }
            if (m_txtBabyAmt.Text.Trim() != string.Empty && !Regex.IsMatch(this.m_txtBabyAmt.Text, RegexText))
            {
                m_mthShowNumberErrorDialog(m_txtBabyAmt.AccessibleDescription);
                m_txtBabyAmt.Focus();
                return false;
            }
            if (m_txtAccompanyAmt.Text.Trim() != string.Empty && !Regex.IsMatch(this.m_txtAccompanyAmt.Text, RegexText))
            {
                m_mthShowNumberErrorDialog(m_txtAccompanyAmt.AccessibleDescription);
                m_txtAccompanyAmt.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 显示数字错误对话框

        /// </summary>
        /// <param name="p_strControlDescription">控件描述</param>
        private void m_mthShowNumberErrorDialog(string p_strControlDescription)
        {
            if (string.IsNullOrEmpty(p_strControlDescription))
            {
                MessageBox.Show("相关限制输入数字的文本框输入了非数字，请检查！", "病案同步", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(p_strControlDescription + "只能填写数字！", "病案同步", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 清空界面
        private void m_mthClear()
        {
            m_dtbPatient.Rows.Clear();
        } 
        #endregion

        #region 设置数据至界面

        /// <summary>
        /// 设置数据至界面

        /// </summary>
        private void m_mthSetDataToUI()
        {
            m_blnIsCaseDataChange = true;
            if (m_chkChooseDept.Checked)
            {
                m_mthFilterPatientDataByDept();
            }
            else
            {
                m_dtbShow = m_dtbPatient.Copy();
                m_dgvOutPatient.DataSource = m_dtbShow;
            }

            m_mthGetCaseValue();
        }
        #endregion

        #region 通过科室过滤病人
        /// <summary>
        /// 通过科室过滤病人
        /// </summary>
        private void m_mthFilterPatientDataByDept()
        {
            if (m_dtbPatient == null)
            {
                return;
            }

            int intRowsCount = m_dtbPatient.Rows.Count;
            if (intRowsCount <= 0)
            {
                return;
            }

            string strSelectedDept = string.Empty;
            StringBuilder stbDept = new StringBuilder(100);
            for (int ii = 0; ii < m_lsvDeptList.CheckedItems.Count; ii++)
            {
                if (ii == 0)
                {
                    stbDept.Append("outdeptid = '");
                    stbDept.Append(m_lsvDeptList.CheckedItems[ii].Tag.ToString());
                    stbDept.Append("'");
                }
                else
                {
                    stbDept.Append(" or ");
                    stbDept.Append("outdeptid = '");
                    stbDept.Append(m_lsvDeptList.CheckedItems[ii].Tag.ToString());
                    stbDept.Append("'");
                }
            }
            strSelectedDept = stbDept.ToString();

            if (string.IsNullOrEmpty(strSelectedDept))
            {
                m_dtbShow.Rows.Clear();
            }
            else
            {
                DataView dvTemp = new DataView(m_dtbPatient);
                dvTemp.RowFilter = strSelectedDept;
                m_dtbShow = dvTemp.ToTable();
                m_dgvOutPatient.DataSource = m_dtbShow;
            }
        } 
        #endregion

        #region 从界面获取同步数据的VO
        /// <summary>
        /// 从界面获取同步数据的VO
        /// </summary>
        private void m_mthGetCaseValue()
        {
            m_objCaseValueArr = null;
            if (m_dtbShow == null)
            {
                return;
            }

            int intRowsCount = m_dtbShow.Rows.Count;
            if (intRowsCount <= 0)
            {
                return;
            }

            bool blnHasPayTypeDict = false;
            DataTable dtbPayType = null;
            clsEMR_SynchronousPayTypeDomain objDomain = new clsEMR_SynchronousPayTypeDomain();
            long lngCheck = objDomain.m_lngGetRelationPayTypeList(out dtbPayType);
            if (dtbPayType != null && dtbPayType.Rows.Count > 0)
            {
                blnHasPayTypeDict = true;
            }

            m_objCaseValueArr = new clsEMR_SynchronousCaseValue[intRowsCount];
            DataRow drCurrent = null;
            clsPeopleInfo objPeo = new clsPeopleInfo();
            DateTime dtmTemp = DateTime.MinValue;
            double dblTemp = 0D;
            DataRow[] drTemp = null;
            for (int iR = 0; iR < intRowsCount; iR++)
            {
                drCurrent = m_dtbShow.Rows[iR];
                if (DateTime.TryParse(drCurrent["BIRTHDAY"].ToString(), out dtmTemp))
                {
                    objPeo.m_DtmBirth = dtmTemp;
                }
                else
                {
                    dtmTemp = DateTime.MinValue;
                }

                m_objCaseValueArr[iR] = new clsEMR_SynchronousCaseValue();
                m_objCaseValueArr[iR].m_strPRN = drCurrent["PRN"].ToString();
                m_objCaseValueArr[iR].m_strNAME = drCurrent["NAME"].ToString();
                m_objCaseValueArr[iR].m_strTIMES = drCurrent["TIMES"].ToString();
                m_objCaseValueArr[iR].m_strSEX = drCurrent["SEX"].ToString();
                if (drCurrent["PAYTYPEID_CHR"] != DBNull.Value)
                {
                    if (blnHasPayTypeDict)
                    {
                        drTemp = dtbPayType.Select("paytypeid_chr = " + drCurrent["PAYTYPEID_CHR"].ToString());
                        if (drTemp != null && drTemp.Length > 0)
                        {
                            m_objCaseValueArr[iR].m_strFB = drTemp[0]["ba_paytypeid_chr"].ToString();
                        }
                    }
                }                
                if (dtmTemp != DateTime.MinValue)
                {
                    m_objCaseValueArr[iR].m_strBIRTHDAY = dtmTemp.ToString("yyyy/MM/dd");
                }
                m_objCaseValueArr[iR].m_strBIRTHPL = drCurrent["BIRTHPL"].ToString();
                m_objCaseValueArr[iR].m_strIDCARD = drCurrent["IDCARD"].ToString();
                m_objCaseValueArr[iR].m_strNATIVE = drCurrent["NATIVE"].ToString();
                m_objCaseValueArr[iR].m_strNATION = drCurrent["NATION"].ToString();
                if (!string.IsNullOrEmpty(objPeo.m_StrYearOfAge))
                {
                    m_objCaseValueArr[iR].m_strAGE = "Y" + objPeo.m_StrYearOfAge;
                }
                else if (!string.IsNullOrEmpty(objPeo.m_StrMonthOfAge))
                {
                    m_objCaseValueArr[iR].m_strAGE = "M" + objPeo.m_StrMonthOfAge;
                }
                else if (!string.IsNullOrEmpty(objPeo.m_StrDayOfAge))
                {
                    m_objCaseValueArr[iR].m_strAGE = "D" + objPeo.m_StrDayOfAge;
                }
                m_objCaseValueArr[iR].m_strJOB = drCurrent["JOB"].ToString();
                m_objCaseValueArr[iR].m_strSTATU = drCurrent["STATU"].ToString();
                m_objCaseValueArr[iR].m_strDWNAME = drCurrent["DWNAME"].ToString();
                m_objCaseValueArr[iR].m_strDWADDR = drCurrent["DWADDR"].ToString();
                m_objCaseValueArr[iR].m_strDWTELE = drCurrent["DWTELE"].ToString();
                m_objCaseValueArr[iR].m_strDWPOST = drCurrent["DWPOST"].ToString();
                m_objCaseValueArr[iR].m_strHKADDR = drCurrent["HKADDR"].ToString();
                m_objCaseValueArr[iR].m_strHKPOST = drCurrent["HKPOST"].ToString();
                m_objCaseValueArr[iR].m_strLXNAME = drCurrent["LXNAME"].ToString();
                m_objCaseValueArr[iR].m_strRELATE = drCurrent["RELATE"].ToString();
                m_objCaseValueArr[iR].m_strLXADDR = drCurrent["LXADDR"].ToString();
                m_objCaseValueArr[iR].m_strLXTELE = drCurrent["LXTELE"].ToString();
                if (DateTime.TryParse(drCurrent["RYDATE"].ToString(),out dtmTemp))
                {
                    m_objCaseValueArr[iR].m_strRYDATE = dtmTemp.ToString("yyyy/MM/dd");
                    //把原来时间格式HHMMSS改为HH.
                    m_objCaseValueArr[iR].m_strRYTIME = dtmTemp.Hour.ToString();
                }                
                m_objCaseValueArr[iR].m_strRYINFO = drCurrent["RYINFO"].ToString();
                m_objCaseValueArr[iR].m_strRYNUM = drCurrent["RYNUM"].ToString();
                m_objCaseValueArr[iR].m_strRYDEPT = drCurrent["RYDEPT"].ToString();
                m_objCaseValueArr[iR].m_strZKNUM = drCurrent["ZKNUM"].ToString();
                m_objCaseValueArr[iR].m_strZKDEPT = drCurrent["ZKDEPT"].ToString();
                m_objCaseValueArr[iR].m_strCYNUM = drCurrent["CYNUM"].ToString();
                m_objCaseValueArr[iR].m_strCYDEPT = drCurrent["CYDEPT"].ToString();
                if (DateTime.TryParse(drCurrent["CYDATE"].ToString(),out dtmTemp))
                {
                    m_objCaseValueArr[iR].m_strCYDATE = dtmTemp.ToString("yyyy/MM/dd");
                    //把原来时间格式HHMMSS改为HH.
                    m_objCaseValueArr[iR].m_strCYTIME = dtmTemp.Hour.ToString();
                }
                if (DateTime.TryParse(drCurrent["RYDATE"].ToString(), out dtmTemp))
                {
                    System.TimeSpan diff = Convert.ToDateTime(drCurrent["CYDATE"]).Subtract(dtmTemp);
                    m_objCaseValueArr[iR].m_strDAYS = ((int)diff.TotalDays + 1).ToString();
                }
                m_objCaseValueArr[iR].m_strMZZD = drCurrent["MZZD"].ToString();
                m_objCaseValueArr[iR].m_strRYZD = drCurrent["RYZD"].ToString();
                if (DateTime.TryParse(drCurrent["QZDATE"].ToString(),out dtmTemp))
                {
                    m_objCaseValueArr[iR].m_strQZDATE = dtmTemp.ToString("yyyy/MM/dd");
                }
                m_objCaseValueArr[iR].m_strPHZD = drCurrent["PATHOLOGYDIAGNOSIS"].ToString();
                m_objCaseValueArr[iR].m_strGMYW = drCurrent["SENSITIVE"].ToString();
                m_objCaseValueArr[iR].m_strBLOOD = drCurrent["BLOODTYPE"].ToString();
                bool blnIsSave = false;
                bool blnAllSaveSuc = false;
                double dblSaveTimes = 0D;
                if (double.TryParse(drCurrent["SALVETIMES"].ToString(),out dblTemp))
                {
                    m_objCaseValueArr[iR].m_strQJTIMES = dblTemp.ToString();
                    dblSaveTimes = dblTemp;
                    blnIsSave = true;
                }
                if (double.TryParse(drCurrent["SALVESUCCESS"].ToString(), out dblTemp))
                {
                    m_objCaseValueArr[iR].m_strSUCTIMES = dblTemp.ToString();
                    if (dblTemp < dblSaveTimes)
                    {
                        blnAllSaveSuc = false;
                    }
                    else
                    {
                        blnAllSaveSuc = true;
                    }
                }
                if (drCurrent["FOLLOW_YEAR"] != DBNull.Value && double.TryParse(drCurrent["FOLLOW_YEAR"].ToString(),out dblTemp))
                {
                    m_objCaseValueArr[iR].m_strSZQX = dblTemp.ToString();
                }
                else if (drCurrent["FOLLOW_MONTH"] != DBNull.Value && double.TryParse(drCurrent["FOLLOW_MONTH"].ToString(), out dblTemp))
                {
                    m_objCaseValueArr[iR].m_strSZQX = dblTemp.ToString();
                }
                else if (drCurrent["FOLLOW_WEEK"] != DBNull.Value && double.TryParse(drCurrent["FOLLOW_WEEK"].ToString(), out dblTemp))
                {
                    m_objCaseValueArr[iR].m_strSZQX = dblTemp.ToString();
                }
                else if (drCurrent["FOLLOW_YEAR"].ToString() == "长期" || drCurrent["FOLLOW_MONTH"].ToString() == "长期" || drCurrent["FOLLOW_WEEK"].ToString() == "长期")
                {
                    m_objCaseValueArr[iR].m_strSZQX = "长期";
                }
                if (drCurrent["CORPSECHECK"].ToString() == "1" || drCurrent["CORPSECHECK"].ToString() == "True")
                {
                    m_objCaseValueArr[iR].m_strBODY = "1";
                }
                else
                {
                    m_objCaseValueArr[iR].m_strBODY = "2";
                }
                if (drCurrent["MODELCASE"].ToString() == "1")
                {
                    m_objCaseValueArr[iR].m_strSAMPLE = "1";
                }
                else
                {
                    m_objCaseValueArr[iR].m_strSAMPLE = "2";
                }
                m_objCaseValueArr[iR].m_strZRDOCT = drCurrent["directorname"].ToString();
                m_objCaseValueArr[iR].m_strZZDOCT = drCurrent["dtname"].ToString();
                m_objCaseValueArr[iR].m_strZYDOCT = drCurrent["inhospitaldtname"].ToString();
                m_objCaseValueArr[iR].m_strSXDOCT = drCurrent["INTERN"].ToString();
                m_objCaseValueArr[iR].m_strBMY = drCurrent["codername"].ToString();
                m_objCaseValueArr[iR].m_strMZACCO = drCurrent["ACCORDWITHOUTHOSPITAL"].ToString();
                m_objCaseValueArr[iR].m_strRYACCO = drCurrent["ACCORDINWITHOUT"].ToString();
                m_objCaseValueArr[iR].m_strOPACCO = drCurrent["ACCORDBEFOREOPERATIONWITHAFTER"].ToString();
                m_objCaseValueArr[iR].m_strQJBR = blnIsSave ? "y":"n";
                m_objCaseValueArr[iR].m_strQJSUC = blnIsSave ? (blnAllSaveSuc ? "y":"n"):"";
                if (drCurrent["BLOODTRANSACTOIN"].ToString() == "1" || drCurrent["BLOODTRANSACTOIN"].ToString() == "True")
                {
                    m_objCaseValueArr[iR].m_strSXFY = "1";
                }
                else
                {
                    m_objCaseValueArr[iR].m_strSXFY = "2";
                }

                m_objCaseValueArr[iR].m_strRegisterID = drCurrent["registerid_chr"].ToString();
                m_objCaseValueArr[iR].m_strPatientID = drCurrent["patientid_chr"].ToString();
            }
        }  
        #endregion

        #region 设置内容至同步资料页
        /// <summary>
        /// 设置内容至同步资料页
        /// </summary>
        private void m_mthSetCaseValueToPatientSelect()
        {
            m_lsvSelectPatient.Items.Clear();
            if (m_objCaseValueArr == null || m_objCaseValueArr.Length <= 0)
            {
                return;
            }

            List<ListViewItem> lsiList = new List<ListViewItem>();
            for (int iC = 0; iC < m_objCaseValueArr.Length; iC++)
            {
                ListViewItem lsi = new ListViewItem(m_objCaseValueArr[iC].m_strPRN);
                lsi.SubItems.Add(m_objCaseValueArr[iC].m_strNAME);
                lsi.Tag = m_objCaseValueArr[iC];
                lsiList.Add(lsi);
            }
            m_lsvSelectPatient.Items.AddRange(lsiList.ToArray());

            m_txtAllPatientCount.Text = m_lsvSelectPatient.Items.Count.ToString();
            m_txtSelectedPatientCount.Text = m_lsvSelectPatient.CheckedItems.Count.ToString();
        }
        #endregion

        #region 清空病人信息
        /// <summary>
        /// 清空病人信息
        /// </summary>
        private void m_mthClearPatientInfo()
        {
            foreach (Control ctl in panel1.Controls)
            {
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).Clear();
                }
            }
        } 
        #endregion

        #region 将选定病人的病案同步内容显示至界面
        /// <summary>
        /// 将选定病人的病案同步内容显示至界面
        /// </summary>
        /// <param name="p_objvalue">选定病人的病案同步内容</param>
        private void m_mthSetValueToForm(clsEMR_SynchronousCaseValue p_objvalue)
        {
            m_mthClearPatientInfo();
            if (p_objvalue == null)
            {
                return;
            }

            clsEMR_SynchronousCaseDomain objDomain = new clsEMR_SynchronousCaseDomain();
            
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_objvalue.m_strSUM1))
            {
                m_mthGetCharge(p_objvalue);
            }
            else
            {
                m_mthSetChargeToUIDirect(p_objvalue);
            }
            objDomain = null;

            m_txtName.Text = p_objvalue.m_strNAME;
            m_txtPRN.Text = p_objvalue.m_strPRN;
            m_txtSex.Text = p_objvalue.m_strSEX;
            m_txtAge.Text = p_objvalue.m_strAGE;
            m_txtTimes.Text = p_objvalue.m_strTIMES;
            m_txtFB.Text = p_objvalue.m_strFB;
            m_txtBirthDay.Text = p_objvalue.m_strBIRTHDAY;
            m_txtMarried.Text = p_objvalue.m_strSTATU;
            m_txtIDCard.Text = p_objvalue.m_strIDCARD;
            m_txtNative.Text = p_objvalue.m_strNATIVE;
            m_txtBirthPL.Text = p_objvalue.m_strBIRTHPL;
            m_txtNation.Text = p_objvalue.m_strNATION;
            m_txtJob.Text = p_objvalue.m_strJOB;
            m_txtOfficeName.Text = p_objvalue.m_strDWNAME;
            m_txtOfficePC.Text = p_objvalue.m_strDWPOST;
            m_txtOfficeAddr.Text = p_objvalue.m_strDWADDR;
            m_txtOfficePhone.Text = p_objvalue.m_strDWTELE;
            m_txtHomeAddr.Text = p_objvalue.m_strHKADDR;
            m_txtHomePC.Text = p_objvalue.m_strHKPOST;
            m_txtLinkMan.Text = p_objvalue.m_strLXNAME;
            m_txtRelation.Text = p_objvalue.m_strRELATE;
            m_txtLinkManPhone.Text = p_objvalue.m_strLXTELE;
            m_txtPatientSource.Text = p_objvalue.m_strSOURCE;
            m_txtLinkManAddr.Text = p_objvalue.m_strLXADDR;
            m_txtInInstance.Text = p_objvalue.m_strRYINFO;
            m_txtInDate.Text = p_objvalue.m_strRYDATE;
            m_txtInTime.Text = p_objvalue.m_strRYTIME;
            m_txtInDeptID.Text = p_objvalue.m_strRYNUM;
            m_txtInDeptName.Text = p_objvalue.m_strRYDEPT;
            m_txtTransDeptID.Text = p_objvalue.m_strZKNUM;
            m_txtTransDeptName.Text = p_objvalue.m_strZKDEPT;
            m_txtConfirmDate.Text = p_objvalue.m_strQZDATE;
            m_txtInDays.Text = p_objvalue.m_strDAYS;
            m_txtOutDate.Text = p_objvalue.m_strCYDATE;
            m_txtOutTime.Text = p_objvalue.m_strCYTIME;
            m_txtOutDeptID.Text = p_objvalue.m_strCYNUM;
            m_txtOutDeptName.Text = p_objvalue.m_strCYDEPT;
            m_txtClinicDiagnosis.Text = p_objvalue.m_strMZZD;
            m_txtFollowSurvey.Text = p_objvalue.m_strSZQX;
            m_txtInDiagnosis.Text = p_objvalue.m_strRYZD;
            m_txtComplications.Text = p_objvalue.m_strBFZ;
            m_txtInfection.Text = p_objvalue.m_strYNGR;
            m_txtBloodType.Text = p_objvalue.m_strBLOOD;
            m_txtRescueTimes.Text = p_objvalue.m_strQJTIMES;
            m_txtRescueSuccess.Text = p_objvalue.m_strSUCTIMES;
            m_txtBodyCheck.Text = p_objvalue.m_strBODY;
            m_txtPathology.Text = p_objvalue.m_strPHZD;
            m_txtAllergyDrug.Text = p_objvalue.m_strGMYW;
            m_txtDirectorDt.Text = p_objvalue.m_strZRDOCT;
            m_txtDoctor.Text = p_objvalue.m_strZZDOCT;
            m_txtAttachmentDt.Text = p_objvalue.m_strSXDOCT;
            m_txtInHospitalDt.Text = p_objvalue.m_strZYDOCT;
            m_txtCoder.Text = p_objvalue.m_strBMY;
            m_txtAccordClinic.Text = p_objvalue.m_strMZACCO;
            m_txtAccordIn.Text = p_objvalue.m_strRYACCO;
            m_txtAccordOperation.Text = p_objvalue.m_strOPACCO;
            m_txtIfSave.Text = p_objvalue.m_strQJBR;
            m_txtIfSaveSuccess.Text = p_objvalue.m_strQJSUC;
            m_txtIfSomeCase.Text = p_objvalue.m_strTWILL;
            m_txtIfModerateburn.Text = p_objvalue.m_strIFZDSS;
            m_txtBabyCount.Text = p_objvalue.m_strBABYNUM;
            m_txtTransfusionReaction.Text = p_objvalue.m_strSXFY;
            m_txtOxygenReaction.Text = p_objvalue.m_strSYFY;
            m_txtIsSingleCase.Text = p_objvalue.m_strIFBZZL;
        }
        #endregion

        #region 获取并显示费用信息

        /// <summary>
        /// 获取并显示费用信息

        /// </summary>
        /// <param name="p_objvalue">选定病人的病案同步内容</param>
        private void m_mthGetCharge(clsEMR_SynchronousCaseValue p_objvalue)
        {
            if (p_objvalue == null)
            {
                return;
            }

            //su.liang 2012-12-21
            //clsEMR_SynchronousCaseDomain objDomain = new clsEMR_SynchronousCaseDomain();
            //clsInHospitalMainCharge[] objChargeArr = null;

            //long lngRes = objDomain.m_lngGetCHRCATE(p_objvalue.m_strRegisterID, p_objvalue.m_strPatientID, out objChargeArr);

            //费用
            clsEMR_SynchronousCaseDomain objDomainExpense = new clsEMR_SynchronousCaseDomain();
           clsInHospitalMainCharge[] objChargeArr = null;
            clsInHospitalMainRecord_Content p_objRecordcontent;
            //long lngRes = objDomainExpense.m_lngGetCHRCATE(p_strRegisterID, p_strPatientID, out objChargeArr);

            DataTable m_strBBRegisterID = null;
            long lngRes = 0;
            //入院时间大于更新时间，采用新版获取费用方式否则手填
            m_strBBRegisterID = objDomainExpense.m_lngGetRgisterIDByInpatientID(p_objvalue.m_strRegisterID);
            //if (m_strBBRegisterID.Rows.Count < 1)
                lngRes = objDomainExpense.m_lngGetCHRCATE(p_objvalue.m_strRegisterID, p_objvalue.m_strPatientID, out objChargeArr);
            //else
            //    lngRes = objDomainExpense.m_lngGetChargeChanKe(p_objvalue.m_strRegisterID, m_strBBRegisterID, out objChargeArr);

            lngRes = objDomainExpense.m_lngGetSelfPay(p_objvalue.m_strRegisterID, out p_objRecordcontent);

            
            if (objChargeArr == null || objChargeArr.Length <= 0)
            {
                p_objvalue.m_strSUM1 = "0.00";
            }
            else
            {
                double dblSum = 0D;
                for (int iC = 0; iC < objChargeArr.Length; iC++)
                {
                    m_mthSetMoneyValueToUI(objChargeArr[iC].m_dblMoney, objChargeArr[iC].m_strTypeName, p_objvalue, ref dblSum);
                }
                m_txtSum1.Text = dblSum.ToString();
                p_objvalue.m_strSUM1 = dblSum.ToString();

                double dblCH = 0D;
                double dblTemp = 0D;
                if (double.TryParse(p_objvalue.m_strZCHF, out dblTemp))
                {
                    dblCH += dblTemp;
                }
                if (double.TryParse(p_objvalue.m_strZCYF, out dblTemp))
                {
                    dblCH += dblTemp;
                }
                p_objvalue.m_strZYF = dblCH.ToString();
                m_txtCMAmt.Text = dblCH.ToString();
            }
            objDomainExpense = null;
        }
        #endregion

        #region 直接显示费用至界面

        /// <summary>
        /// 直接显示费用至界面

        /// </summary>
        /// <param name="p_objvalue"></param>
        private void m_mthSetChargeToUIDirect(clsEMR_SynchronousCaseValue p_objvalue)
        {
            if (p_objvalue == null)
            {
                return;
            }

            m_txtOperationAmt.Text = p_objvalue.m_strSSF;
            m_txtDeliveryChildAmt.Text = p_objvalue.m_strJSF;
            m_txtCheckAmt.Text = p_objvalue.m_strJCF;
            m_txtAnaethesiaAmt.Text = p_objvalue.m_strMZF;
            m_txtBabyAmt.Text = p_objvalue.m_strYEF;
            m_txtAccompanyAmt.Text = p_objvalue.m_strPCF;
            m_txtOtherAmt.Text = p_objvalue.m_strQTF;
            m_txtTreatmentAmt.Text = p_objvalue.m_strZLF;
            m_txtBedAmt.Text = p_objvalue.m_strCWF;
            m_txtNurseAmt.Text = p_objvalue.m_strHLF;
            m_txtRadiationAmt.Text = p_objvalue.m_strFSF;
            m_txtAssayAmt.Text = p_objvalue.m_strHYF;
            m_txtO2Amt.Text = p_objvalue.m_strSYF;
            m_txtBloodAmt.Text = p_objvalue.m_strSXF;
            m_txtWMAmt.Text = p_objvalue.m_strXYF;
            m_txtCMSemiFinishedAmt.Text = p_objvalue.m_strZCYF;
            m_txtCMFinishedAmt.Text = p_objvalue.m_strZCHF;
            m_txtCMAmt.Text = p_objvalue.m_strZYF;
            m_txtSum1.Text = p_objvalue.m_strSUM1;
        }  
        #endregion

        #region 设置费用至界面

        /// <summary>
        /// 设置费用至界面

        /// </summary>
        /// <param name="p_dblMoney">费用金额</param>
        /// <param name="p_strChargeName">费用名称</param>
        /// <param name="p_objvalue">选定病人的病案同步内容</param>
        /// <param name="p_dblSum">总和</param>
        private void m_mthSetMoneyValueToUI(double p_dblMoney, string p_strChargeName,clsEMR_SynchronousCaseValue p_objvalue, ref double p_dblSum)
        {
            p_dblSum += p_dblMoney;
            if (string.IsNullOrEmpty(p_strChargeName) || p_objvalue == null)
            {
                return;
            }

            switch (p_strChargeName)
            {
                case "手术费":
                    m_txtOperationAmt.Text = p_dblMoney.ToString();
                    p_objvalue.m_strSSF = p_dblMoney.ToString();
                    break;
                case "接生费":
                    m_txtDeliveryChildAmt.Text = p_dblMoney.ToString();
                    p_objvalue.m_strJSF = p_dblMoney.ToString();
                    break;
                case "检查费":
                    m_txtCheckAmt.Text = p_dblMoney.ToString();
                    p_objvalue.m_strJCF = p_dblMoney.ToString();
                    break;
                case "麻醉费":
                    m_txtAnaethesiaAmt.Text = p_dblMoney.ToString();
                    p_objvalue.m_strMZF = p_dblMoney.ToString();
                    break;
                case "婴儿费":
                    m_txtBabyAmt.Text = p_dblMoney.ToString();
                    p_objvalue.m_strYEF = p_dblMoney.ToString();
                    break;
                case "陪床费":
                    m_txtAccompanyAmt.Text = p_dblMoney.ToString();
                    p_objvalue.m_strPCF = p_dblMoney.ToString();
                    break;
                case "其他费":
                    m_txtOtherAmt.Text = p_dblMoney.ToString();
                    p_objvalue.m_strQTF = p_dblMoney.ToString();
                    break;
                case "诊疗费":
                    m_txtTreatmentAmt.Text = p_dblMoney.ToString();
                    p_objvalue.m_strZLF = p_dblMoney.ToString();
                    break;
                case "床位费":
                    m_txtBedAmt.Text = p_dblMoney.ToString();
                    p_objvalue.m_strCWF = p_dblMoney.ToString();
                    break;
                case "护理费":
                    m_txtNurseAmt.Text = p_dblMoney.ToString();
                    p_objvalue.m_strHLF = p_dblMoney.ToString();
                    break;
                case "放射费":
                    m_txtRadiationAmt.Text = p_dblMoney.ToString();
                    p_objvalue.m_strFSF = p_dblMoney.ToString();
                    break;
                case "化验费":
                    m_txtAssayAmt.Text = p_dblMoney.ToString();
                    p_objvalue.m_strHYF = p_dblMoney.ToString();
                    break;
                case "输氧费":
                    m_txtO2Amt.Text = p_dblMoney.ToString();
                    p_objvalue.m_strSYF = p_dblMoney.ToString();
                    break;
                case "输血费":
                    m_txtBloodAmt.Text = p_dblMoney.ToString();
                    p_objvalue.m_strSXF = p_dblMoney.ToString();
                    break;
                case "西药费":
                    m_txtWMAmt.Text = p_dblMoney.ToString();
                    p_objvalue.m_strXYF = p_dblMoney.ToString();
                    break;
                case "中草药费":
                    m_txtCMSemiFinishedAmt.Text = p_dblMoney.ToString();
                    p_objvalue.m_strZCYF = p_dblMoney.ToString();
                    break;
                case "中成药费":
                    m_txtCMFinishedAmt.Text = p_dblMoney.ToString();
                    p_objvalue.m_strZCHF = p_dblMoney.ToString();
                    break;
            }
        }
        #endregion

        #region 设置费用到病案内容

        /// <summary>
        /// 设置费用到病案内容

        /// </summary>
        /// <param name="p_dblMoney">费用金额</param>
        /// <param name="p_strChargeName">费用名称</param>
        /// <param name="p_objvalue">选定病人的病案同步内容</param>
        /// <param name="p_dblSum">总和</param>
        private void m_mthSetMoneyToValue(double p_dblMoney, string p_strChargeName, clsEMR_SynchronousCaseValue p_objvalue, ref double p_dblSum)
        {
            p_dblSum += p_dblMoney;
            if (string.IsNullOrEmpty(p_strChargeName) || p_objvalue == null)
            {
                return;
            }

            switch (p_strChargeName)
            {
                case "手术费":
                    p_objvalue.m_strSSF = p_dblMoney.ToString();
                    break;
                case "接生费":
                    p_objvalue.m_strJSF = p_dblMoney.ToString();
                    break;
                case "检查费":
                    p_objvalue.m_strJCF = p_dblMoney.ToString();
                    break;
                case "麻醉费":
                    p_objvalue.m_strMZF = p_dblMoney.ToString();
                    break;
                case "婴儿费":
                    p_objvalue.m_strYEF = p_dblMoney.ToString();
                    break;
                case "陪床费":
                    p_objvalue.m_strPCF = p_dblMoney.ToString();
                    break;
                case "其他费":
                    p_objvalue.m_strQTF = p_dblMoney.ToString();
                    break;
                case "诊疗费":
                    p_objvalue.m_strZLF = p_dblMoney.ToString();
                    break;
                case "床位费":
                    p_objvalue.m_strCWF = p_dblMoney.ToString();
                    break;
                case "护理费":
                    p_objvalue.m_strHLF = p_dblMoney.ToString();
                    break;
                case "放射费":
                    p_objvalue.m_strFSF = p_dblMoney.ToString();
                    break;
                case "化验费":
                    p_objvalue.m_strHYF = p_dblMoney.ToString();
                    break;
                case "输氧费":
                    p_objvalue.m_strSYF = p_dblMoney.ToString();
                    break;
                case "输血费":
                    p_objvalue.m_strSXF = p_dblMoney.ToString();
                    break;
                case "西药费":
                    p_objvalue.m_strXYF = p_dblMoney.ToString();
                    break;
                case "中草药费":
                    p_objvalue.m_strZCYF = p_dblMoney.ToString();
                    break;
                case "中成药费":
                    p_objvalue.m_strZCHF = p_dblMoney.ToString();
                    break;
            }
        }        
        #endregion

        #region 保存修改后的值

        /// <summary>
        /// 保存修改后的值

        /// </summary>
        /// <param name="p_objvalue">病案内容</param>
        private void m_mthSaveValueChange(clsEMR_SynchronousCaseValue p_objvalue)
        {
            if (p_objvalue == null)
            {
                p_objvalue = new clsEMR_SynchronousCaseValue();
            }

            p_objvalue.m_strSSF = m_txtOperationAmt.Text;
            p_objvalue.m_strJSF = m_txtDeliveryChildAmt.Text;
            p_objvalue.m_strJCF = m_txtCheckAmt.Text;
            p_objvalue.m_strMZF = m_txtAnaethesiaAmt.Text;
            p_objvalue.m_strYEF = m_txtBabyAmt.Text;
            p_objvalue.m_strPCF = m_txtAccompanyAmt.Text;
            p_objvalue.m_strQTF = m_txtOtherAmt.Text;
            p_objvalue.m_strZLF = m_txtTreatmentAmt.Text;
            p_objvalue.m_strCWF = m_txtBedAmt.Text;
            p_objvalue.m_strHLF = m_txtNurseAmt.Text;
            p_objvalue.m_strFSF = m_txtRadiationAmt.Text;
            p_objvalue.m_strHYF = m_txtAssayAmt.Text;
            p_objvalue.m_strSYF = m_txtO2Amt.Text;
            p_objvalue.m_strSXF = m_txtBloodAmt.Text;
            p_objvalue.m_strXYF = m_txtWMAmt.Text;
            p_objvalue.m_strZCYF = m_txtCMSemiFinishedAmt.Text;
            p_objvalue.m_strZCHF = m_txtCMFinishedAmt.Text;
            p_objvalue.m_strZYF = m_txtCMAmt.Text;
            p_objvalue.m_strSUM1 = m_txtSum1.Text;

            p_objvalue.m_strNAME = m_txtName.Text;
            p_objvalue.m_strPRN = m_txtPRN.Text;
            p_objvalue.m_strSEX = m_txtSex.Text;
            p_objvalue.m_strAGE = m_txtAge.Text;
            p_objvalue.m_strTIMES = m_txtTimes.Text;
            p_objvalue.m_strFB = m_txtFB.Text;
            p_objvalue.m_strBIRTHDAY = m_txtBirthDay.Text;
            p_objvalue.m_strSTATU = m_txtMarried.Text;
            p_objvalue.m_strIDCARD = m_txtIDCard.Text;
            p_objvalue.m_strNATIVE = m_txtNative.Text;
            p_objvalue.m_strBIRTHPL = m_txtBirthPL.Text;
            p_objvalue.m_strNATION = m_txtNation.Text;
            p_objvalue.m_strJOB = m_txtJob.Text;
            p_objvalue.m_strDWNAME = m_txtOfficeName.Text;
            p_objvalue.m_strDWPOST = m_txtOfficePC.Text;
            p_objvalue.m_strDWADDR = m_txtOfficeAddr.Text;
            p_objvalue.m_strDWTELE = m_txtOfficePhone.Text;
            p_objvalue.m_strHKADDR = m_txtHomeAddr.Text;
            p_objvalue.m_strHKPOST = m_txtHomePC.Text;
            p_objvalue.m_strLXNAME = m_txtLinkMan.Text;
            p_objvalue.m_strRELATE = m_txtRelation.Text;
            p_objvalue.m_strLXTELE = m_txtLinkManPhone.Text;
            p_objvalue.m_strSOURCE = m_txtPatientSource.Text;
            p_objvalue.m_strLXADDR = m_txtLinkManAddr.Text;
            p_objvalue.m_strRYINFO = m_txtInInstance.Text;
            p_objvalue.m_strRYDATE = m_txtInDate.Text;
            p_objvalue.m_strRYTIME = m_txtInTime.Text;
            p_objvalue.m_strRYNUM = m_txtInDeptID.Text;
            p_objvalue.m_strRYDEPT = m_txtInDeptName.Text;
            p_objvalue.m_strZKNUM = m_txtTransDeptID.Text;
            p_objvalue.m_strZKDEPT = m_txtTransDeptName.Text;
            p_objvalue.m_strQZDATE = m_txtConfirmDate.Text;
            p_objvalue.m_strDAYS = m_txtInDays.Text;
            p_objvalue.m_strCYDATE = m_txtOutDate.Text;
            p_objvalue.m_strCYTIME = m_txtOutTime.Text;
            p_objvalue.m_strCYNUM = m_txtOutDeptID.Text;
            p_objvalue.m_strCYDEPT = m_txtOutDeptName.Text;
            p_objvalue.m_strMZZD = m_txtClinicDiagnosis.Text;
            p_objvalue.m_strSZQX = m_txtFollowSurvey.Text;
            p_objvalue.m_strRYZD = m_txtInDiagnosis.Text;
            p_objvalue.m_strBFZ = m_txtComplications.Text;
            p_objvalue.m_strYNGR = m_txtInfection.Text;
            p_objvalue.m_strBLOOD = m_txtBloodType.Text;
            p_objvalue.m_strQJTIMES = m_txtRescueTimes.Text;
            p_objvalue.m_strSUCTIMES = m_txtRescueSuccess.Text;
            p_objvalue.m_strBODY = m_txtBodyCheck.Text;
            p_objvalue.m_strPHZD = m_txtPathology.Text;
            p_objvalue.m_strGMYW = m_txtAllergyDrug.Text;
            p_objvalue.m_strZRDOCT = m_txtDirectorDt.Text;
            p_objvalue.m_strZZDOCT = m_txtDoctor.Text;
            p_objvalue.m_strSXDOCT = m_txtAttachmentDt.Text;
            p_objvalue.m_strZYDOCT = m_txtInHospitalDt.Text;
            p_objvalue.m_strBMY = m_txtCoder.Text;
            p_objvalue.m_strMZACCO = m_txtAccordClinic.Text;
            p_objvalue.m_strRYACCO = m_txtAccordIn.Text;
            p_objvalue.m_strOPACCO = m_txtAccordOperation.Text;
            p_objvalue.m_strQJBR = m_txtIfSave.Text;
            p_objvalue.m_strQJSUC = m_txtIfSaveSuccess.Text;
            p_objvalue.m_strTWILL = m_txtIfSomeCase.Text;
            p_objvalue.m_strIFZDSS = m_txtIfModerateburn.Text;
            p_objvalue.m_strBABYNUM = m_txtBabyCount.Text;
            p_objvalue.m_strSXFY = m_txtTransfusionReaction.Text;
            p_objvalue.m_strSYFY = m_txtOxygenReaction.Text;
            p_objvalue.m_strIFBZZL = m_txtIsSingleCase.Text;
        }
        #endregion

        #region 同步病案内容
        /// <summary>
        /// 同步病案内容
        /// </summary>
        private void m_mthSynchronousCase()
        {
            if (m_objCaseValueArr == null || m_lsvSelectPatient.Items.Count <= 0)
            {
                MessageBox.Show("没有要同步的病案资料", "病案同步", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (m_lsvSelectPatient.CheckedItems.Count <= 0)
            {
                MessageBox.Show("请先选择要同步病案资料的病人", "病案同步", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            clsEMR_SynchronousCaseDomain objDomain = new clsEMR_SynchronousCaseDomain();

            try
            {
                this.Cursor = Cursors.WaitCursor;
                List<clsEMR_SynchronousCaseValue> objList = new List<clsEMR_SynchronousCaseValue>();
                List<clsEMR_SynchronousCaseValue> objHasSynchronous = new List<clsEMR_SynchronousCaseValue>();
                long lngRes = 0;
                int intItemsCount = m_lsvSelectPatient.CheckedItems.Count;
                clsEMR_SynchronousCaseValue objValue = null;
                clsInHospitalMainCharge[] objChargeArr = null;

                for (int iI = 0; iI < intItemsCount; iI++)
                {
                    objValue = m_lsvSelectPatient.CheckedItems[iI].Tag as clsEMR_SynchronousCaseValue;
                    if (string.IsNullOrEmpty(objValue.m_strSUM1))
                    {
                        #region 同步并获取费用信息

                        lngRes = objDomain.m_lngGetCHRCATE(objValue.m_strRegisterID, objValue.m_strPatientID, out objChargeArr);


                        //费用
                        //clsEMR_SynchronousCaseDomain objDomainExpense = new clsEMR_SynchronousCaseDomain();
                        //iCareData.clsInHospitalMainCharge[] objChargeArr = null;
                        clsInHospitalMainRecord_Content p_objRecordcontent;
                        //long lngRes = objDomainExpense.m_lngGetCHRCATE(p_strRegisterID, p_strPatientID, out objChargeArr);

                        DataTable m_strBBRegisterID = null;
                        //long lngRes = 0;
                        //入院时间大于更新时间，采用新版获取费用方式否则手填
                        m_strBBRegisterID = objDomain.m_lngGetRgisterIDByInpatientID(objValue.m_strRegisterID);
                        //if (m_strBBRegisterID.Rows.Count < 1)
                            lngRes = objDomain.m_lngGetCHRCATE(objValue.m_strRegisterID, objValue.m_strPatientID, out objChargeArr);
                        //else
                        //    lngRes = objDomain.m_lngGetChargeChanKe(objValue.m_strRegisterID, m_strBBRegisterID, out objChargeArr);

                        lngRes = objDomain.m_lngGetSelfPay(objValue.m_strRegisterID, out p_objRecordcontent);


                        if (objChargeArr == null || objChargeArr.Length <= 0)
                        {
                            objValue.m_strSUM1 = "0.00";
                        }
                        else
                        {
                            double dblSum = 0D;
                            for (int iC = 0; iC < objChargeArr.Length; iC++)
                            {
                                m_mthSetMoneyToValue(objChargeArr[iC].m_dblMoney, objChargeArr[iC].m_strTypeName, objValue, ref dblSum);
                            }
                            objValue.m_strSUM1 = dblSum.ToString();

                            double dblCH = 0D;
                            double dblTemp = 0D;
                            if (double.TryParse(objValue.m_strZCHF, out dblTemp))
                            {
                                dblCH += dblTemp;
                            }
                            if (double.TryParse(objValue.m_strZCYF, out dblTemp))
                            {
                                dblCH += dblTemp;
                            }
                            objValue.m_strZYF = dblCH.ToString();
                        }
                        #endregion
                    }
                    bool blnTemp = false;
                    lngRes = objDomain.m_lngCheckHasSynchronous(objValue.m_strPRN,objValue.m_strTIMES,out blnTemp);
                    if (blnTemp)
                    {
                        objHasSynchronous.Add(objValue);
                        continue;
                    }
                    lngRes = objDomain.m_lngCommitToBATemp(objValue);
                    if (lngRes < 0)
                    {
                        objList.Add(objValue);
                    }
                }

                string strTips = string.Empty;
                int intFailCount = objList.Count;
                int intUnSynchronous = objHasSynchronous.Count;

                if (intFailCount > 0)
                {
                    if (intFailCount == intItemsCount)
                    {
                        strTips = "资料传送失败";
                    }
                    else
                    {
                        System.Text.StringBuilder stbPatient = new StringBuilder(100);
                        stbPatient.Append("资料传送完毕，以下病人资料未能成功传送");
                        stbPatient.Append(System.Environment.NewLine);
                        for (int iPa = 0; iPa < intFailCount; iPa++)
                        {
                            objValue = objList[iPa];
                            stbPatient.Append(objValue.m_strPRN);
                            stbPatient.Append("    ");
                            stbPatient.Append(objValue.m_strNAME);
                            stbPatient.Append(Environment.NewLine);
                        }
                        strTips = stbPatient.ToString();
                    }
                }
                else if (intUnSynchronous<= 0)
                {
                    strTips = "资料传送完毕";
                }

                if (!string.IsNullOrEmpty(strTips))
                {
                    MessageBox.Show(strTips, "病案同步", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }                

                if (intUnSynchronous > 0)
                {
                    System.Text.StringBuilder stbPatient = new StringBuilder(100);
                    stbPatient.Append("传送资料操作已完成，以下病人资料未传送");
                    stbPatient.Append(System.Environment.NewLine);
                    stbPatient.Append("原因：【病案系统已存在下述病人资料】");
                    stbPatient.Append(System.Environment.NewLine);
                    stbPatient.Append("是否再次传送？");
                    stbPatient.Append(System.Environment.NewLine);
                    stbPatient.Append(System.Environment.NewLine);
                    for (int iPa = 0; iPa < intUnSynchronous; iPa++)
                    {
                        objValue = objHasSynchronous[iPa];
                        stbPatient.Append(objValue.m_strPRN);
                        stbPatient.Append("    ");
                        stbPatient.Append(objValue.m_strNAME);
                        stbPatient.Append(Environment.NewLine);
                    }
                    strTips = stbPatient.ToString();
                    //DialogResult dr = MessageBox.Show(strTips, "病案同步", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    frmEMR_WarningDialog frmDialog = new frmEMR_WarningDialog();
                    frmDialog.m_txtWarningText.Text = strTips;
                    frmDialog.ShowDialog();
                    if (frmDialog.DialogResult == DialogResult.Yes)
                    {
                        objList.Clear();
                        for (int iH = 0; iH < intUnSynchronous; iH++)
                        {
                            objValue = objHasSynchronous[iH];
                             lngRes = objDomain.m_lngCommitToBATemp(objValue);
                             if (lngRes < 0)
                             {
                                 objList.Add(objHasSynchronous[iH]);
                             }
                        }
                        if (objList.Count > 0)
                        {
                            MessageBox.Show("传送失败", "病案同步", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("传送完毕", "病案同步", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                string strEx = Ex.Message;
            }
            finally
            {
                objDomain = null;
                this.Cursor = Cursors.Default;
            }
        } 
        #endregion

        #region 移除已同步病人

        /// <summary>
        /// 移除已同步病人

        /// </summary>
        private void m_mthRemoveHasSynchronousPatient()
        {
            m_dtbHasSynchronousCasePatient = null;
            DateTime dtmStrat = Convert.ToDateTime(m_dtpStart.Value.ToString("yyyy-MM-dd 00:00:00"));
            DateTime dtmEnd = Convert.ToDateTime(m_dtpEnd.Value.ToString("yyyy-MM-dd 23:59:59"));
            clsEMR_SynchronousCaseDomain objDomain = new clsEMR_SynchronousCaseDomain();
            long lngRes = objDomain.m_lngGetHasSynchronousPatien(dtmStrat, dtmEnd, out m_dtbHasSynchronousCasePatient);
            objDomain = null;

            if (m_dtbHasSynchronousCasePatient == null)
            {
                return;
            }

            int intRowsCount = m_dtbHasSynchronousCasePatient.Rows.Count;
            if (intRowsCount <= 0)
            {
                return;
            }

            int intItemsCount = m_lsvSelectPatient.Items.Count;

            clsEMR_SynchronousCaseValue objValue = null;
            DataRow[] drArr = null;
            string strSQL = "prn='{0}' and times='{1}'";
            List<ListViewItem> lsiArr = new List<ListViewItem>();
            for (int iI = 0; iI < intItemsCount; iI++)
            {
                objValue = m_lsvSelectPatient.Items[iI].Tag as clsEMR_SynchronousCaseValue;
                drArr = m_dtbHasSynchronousCasePatient.Select(string.Format(strSQL, objValue.m_strPRN, objValue.m_strTIMES));
                if (drArr != null && drArr.Length > 0)
                {
                    lsiArr.Add(m_lsvSelectPatient.Items[iI]);
                }
            }

            if (lsiArr.Count > 0)
            {
                foreach (ListViewItem lsi in lsiArr)
                {
                    m_lsvSelectPatient.Items.Remove(lsi);
                }
            }

            m_txtAllPatientCount.Text = m_lsvSelectPatient.Items.Count.ToString();
            m_txtSelectedPatientCount.Text = m_lsvSelectPatient.CheckedItems.Count.ToString();
        } 
        #endregion
    }
}