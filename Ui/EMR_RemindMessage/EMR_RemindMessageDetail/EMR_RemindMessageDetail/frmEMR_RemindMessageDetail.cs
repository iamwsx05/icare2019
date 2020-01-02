using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.Emr.StaticObject;
using iCare;

namespace com.digitalwave.iCare.RemindMessage
{
    /// <summary>
    /// 提示详细信息窗体
    /// </summary>
    public partial class frmEMR_RemindMessageDetail : Form
    {
        /// <summary>
        /// 会诊内容
        /// </summary>
        private clsConsultationRecordContent[] m_objConsultationContentArr = null;
        /// <summary>
        /// 病案查阅申请审批答复内容
        /// </summary>
        private clsEMR_CaseSubscribeValue[] m_objCaseSubscribeContentArr = null;
        /// <summary>
        /// 病案查阅申请内容
        /// </summary>
        private clsEMR_CaseSubscribeValue[] m_objCaseSubscribeRequestArr = null;

        /// <summary>
        /// 提示详细信息
        /// </summary>
        public frmEMR_RemindMessageDetail()
        {
            InitializeComponent();

            m_objConsultationContentArr = null;
            m_objCaseSubscribeContentArr = null;
        }

        weCare.Proxy.ProxyEmr06 proxy
        {
            get { return new weCare.Proxy.ProxyEmr06(); }
        }

        #region 事件
        private void m_cmdRefresh_Click(object sender, EventArgs e)
        {
            m_mthClearUI();
            m_objConsultationContentArr = null;
            m_objCaseSubscribeContentArr = null;
            m_lsvDetail.Items.Clear();
            if (m_chkConsultation.Checked)
            {
                m_blnHasConsultation();
            }
            if (m_chkCaseHistoryArchiving.Checked)
            {
                m_blnHasArchivedAgreeInfo();
            }
            if ((!m_blnOnlyShowCaseArchivingRequest && m_blnCheckHasClinicRight()) || m_blnOnlyShowCaseArchivingRequest)
            {
                m_blnHasArchivedRequest();
            }
        }

        private void m_cmdClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmEMR_RemindMessageDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void m_lsvDetail_DoubleClick(object sender, EventArgs e)
        {
            if (m_lsvDetail.SelectedItems.Count == 1)
            {
                if (m_lsvDetail.SelectedItems[0].Group == m_lsvDetail.Groups["Consultation"])
                {
                    m_mthShowFrmConsultationSearch();
                }
                else if (m_lsvDetail.SelectedItems[0].Group == m_lsvDetail.Groups["CaseArchivedAgree"])
                {
                    m_mthShowFrmBorrowCase();
                }
                else if (m_lsvDetail.SelectedItems[0].Group == m_lsvDetail.Groups["CaseArchivedRequest"])
                {
                    m_mthShowFrmApproveCase();
                }
                this.Hide();
            }
        }

        private void m_lsvDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_lsvDetail.SelectedItems.Count == 1)
            {
                if (m_lsvDetail.SelectedItems[0].SubItems[3].Text == "")
                {
                    if (m_lsvDetail.SelectedItems[0].Group == m_lsvDetail.Groups["Consultation"])
                    {
                        clsConsultationRecordContent objRecord = m_lsvDetail.SelectedItems[0].Tag as clsConsultationRecordContent;
                        if (objRecord == null)
                            return;
                        m_lsvDetail.SelectedItems[0].SubItems[3].Text = m_strTips(objRecord);
                        m_lsvDetail.SelectedItems[0].ToolTipText = m_lsvDetail.SelectedItems[0].SubItems[3].Text;
                    }
                    else if (m_lsvDetail.SelectedItems[0].Group == m_lsvDetail.Groups["CaseArchivedAgree"])
                    {
                        clsEMR_CaseSubscribeValue objSubscribe = m_lsvDetail.SelectedItems[0].Tag as clsEMR_CaseSubscribeValue;
                        if (objSubscribe == null)
                        {
                            return;
                        }
                        m_lsvDetail.SelectedItems[0].SubItems[3].Text = "申请日期:" + objSubscribe.m_dtmCREATEDATE_DAT.ToString("yyyy年MM月dd日") + System.Environment.NewLine
                            + "开始日期:" + objSubscribe.m_dtmBEGINDATE_DAT.ToString("yyyy年MM月dd日") + System.Environment.NewLine
                            + "结束日期:" + objSubscribe.m_dtmENDDATE_DAT.ToString("yyyy年MM月dd日");
                        m_lsvDetail.SelectedItems[0].ToolTipText = m_lsvDetail.SelectedItems[0].SubItems[3].Text;
                    }
                    else if (m_lsvDetail.SelectedItems[0].Group == m_lsvDetail.Groups["CaseArchivedRequest"])
                    {
                        clsEMR_CaseSubscribeValue objSubscribe = m_lsvDetail.SelectedItems[0].Tag as clsEMR_CaseSubscribeValue;
                        if (objSubscribe == null)
                        {
                            return;
                        }
                        m_lsvDetail.SelectedItems[0].SubItems[3].Text = "申请用途:" + objSubscribe.m_strACCOUNTFOR_VCHR;
                        m_lsvDetail.SelectedItems[0].ToolTipText = m_lsvDetail.SelectedItems[0].SubItems[3].Text;
                    }
                }
            }
        }

        private void m_chkConsultation_CheckedChanged(object sender, EventArgs e)
        {
            if (m_chkConsultation.Checked)
            {
                m_mthSetConsultation(m_objConsultationContentArr);
            }
            else
            {
                List<ListViewItem> lsiArr = new List<ListViewItem>();
                for (int i = 0; i < m_lsvDetail.Items.Count; i++)
                {
                    if (m_lsvDetail.Items[i].Group == m_lsvDetail.Groups["Consultation"])
                    {
                        lsiArr.Add(m_lsvDetail.Items[i]);
                    }
                }
                foreach (ListViewItem lsi in lsiArr)
                {
                    m_lsvDetail.Items.Remove(lsi);
                }
            }
        }

        private void m_chkCaseHistoryArchiving_CheckedChanged(object sender, EventArgs e)
        {
            if (m_chkCaseHistoryArchiving.Checked)
            {
                m_mthSetCaseSubscribe(m_objCaseSubscribeContentArr);
            }
            else
            {
                List<ListViewItem> lsiArr = new List<ListViewItem>();
                for (int i = 0; i < m_lsvDetail.Items.Count; i++)
                {
                    if (m_lsvDetail.Items[i].Group == m_lsvDetail.Groups["CaseArchivedAgree"])
                    {
                        lsiArr.Add(m_lsvDetail.Items[i]);
                    }
                }
                foreach (ListViewItem lsi in lsiArr)
                {
                    m_lsvDetail.Items.Remove(lsi);
                }
            }
        }
        #endregion

        #region 方法与属性
        private bool m_blnOnlyShowCaseArchivingRequest = false;
        /// <summary>
        /// 获取或设置是否只显示病案查阅申请提示信息
        /// </summary>
        public bool m_BlnOnlyShowCaseArchivingRequest
        {
            get { return m_blnOnlyShowCaseArchivingRequest; }
            set
            {
                m_blnOnlyShowCaseArchivingRequest = value;
                if (value)
                {
                    m_BlnShowConsultation = false;
                    m_BlnShowCaseArchivingApprove = false;
                }
            }
        }
        private bool m_blnShowConsultation = false;
        /// <summary>
        /// 获取或设置是否显示会诊提示信息
        /// </summary>
        public bool m_BlnShowConsultation
        {
            get { return m_blnShowConsultation; }
            set
            {
                m_blnShowConsultation = value;
                m_chkConsultation.Checked = value;
                if (value)
                {
                    m_blnOnlyShowCaseArchivingRequest = false;
                }
            }
        }

        private bool m_blnShowCaseArchivingApprove = false;
        /// <summary>
        /// 获取或设置是否显示病案申请审批答复提示信息
        /// </summary>
        public bool m_BlnShowCaseArchivingApprove
        {
            get { return m_blnShowCaseArchivingApprove; }
            set
            {
                m_blnShowCaseArchivingApprove = value;
                m_chkCaseHistoryArchiving.Checked = value;
                if (value)
                {
                    m_blnOnlyShowCaseArchivingRequest = false;
                }
            }
        }
        #region 清空界面
        /// <summary>
        /// 清空界面
        /// </summary>
        private void m_mthClearUI()
        {
            m_lsvDetail.Items.Clear();
        }
        #endregion

        #region 检查是否显示窗体
        /// <summary>
        /// 检查是否显示窗体
        /// </summary>
        /// <returns></returns>
        public bool m_blnCanShowForm()
        {
            bool blnCanShow = true;
            m_lsvDetail.Items.Clear();
            if (m_chkConsultation.Checked)
            {
                m_blnHasConsultation();
            }
            if (m_chkCaseHistoryArchiving.Checked)
            {
                m_blnHasArchivedAgreeInfo();
            }
            if ((!m_blnOnlyShowCaseArchivingRequest && m_blnCheckHasClinicRight()) || m_blnOnlyShowCaseArchivingRequest)
            {
                m_blnHasArchivedRequest();
            }
            if (m_lsvDetail.Items.Count <= 0)
            {
                blnCanShow = false;
            }
            return blnCanShow;
        }
        #endregion

        #region 会诊
        #region 设置会诊通知至界面
        /// <summary>
        /// 设置会诊通知至界面
        /// </summary>
        /// <param name="objContentArr">会诊内容</param>
        private void m_mthSetConsultation(clsConsultationRecordContent[] objContentArr)
        {
            if (objContentArr != null)
            {
                m_lsvDetail.BeginUpdate();
                for (int i = 0; i < objContentArr.Length; i++)
                {
                    if (objContentArr[i].m_strConsultationDoctorIDArr != null
                            && objContentArr[i].m_strConsultationDoctorIDArr.Length > 0)
                    {
                        continue;
                    }

                    ListViewItem item = new ListViewItem(new string[] { m_strConsultationStatus(objContentArr[i].m_intConsultationTime),
                        objContentArr[i].m_strAskConsultationDeptName + "  " +objContentArr[i].m_strMainDoctorName,
                        objContentArr[i].m_dtmConsultationDate.ToString("yyyy-MM-dd HH:mm"),""});
                    item.Tag = objContentArr[i];
                    m_lsvDetail.Items.Add(item);
                    m_lsvDetail.Groups["Consultation"].Items.Add(item);
                }
                m_lsvDetail.EndUpdate();
                objContentArr = null;
                Application.DoEvents();
            }
        }
        #endregion

        #region 会诊状态
        /// <summary>
        /// 会诊状态
        /// </summary>
        /// <param name="p_intStatus">状态值</param>
        /// <returns></returns>
        private string m_strConsultationStatus(int p_intStatus)
        {
            switch (p_intStatus)
            {
                case 1:
                    return "请即时会诊";
                case 2:
                    return "请在二十四小时内会诊";
                default:
                    return "一般会诊";
            }
        }
        #endregion

        #region 查询员工所在科室是否有会诊通知
        /// <summary>
        /// 查询员工所在科室是否有会诊通知
        /// </summary>
        private bool m_blnHasConsultation()
        {
            string strConsultationStatus = string.Empty;

            try
            {
                long lngConfig = (new weCare.Proxy.ProxyEmr()).Service.clsPublicMiddleTier_m_lngGetConfigBySettingID("3009", out strConsultationStatus);
            }
            catch (Exception ex)
            {
                string exMsg = ex.Message;
            }

            if (string.IsNullOrEmpty(strConsultationStatus))
                return false;

            string[] DeptIDArr = null;

            if (strConsultationStatus == "0")
            {
                if (clsEMR_StaticObject.s_ObjEmpDeptAndAreaArr != null)
                {
                    DeptIDArr = new string[clsEMR_StaticObject.s_ObjEmpDeptAndAreaArr.Length];
                    for (int i = 0; i < clsEMR_StaticObject.s_ObjEmpDeptAndAreaArr.Length; i++)
                    {
                        DeptIDArr[i] = clsEMR_StaticObject.s_ObjEmpDeptAndAreaArr[i].m_strDEPTID_CHR;
                    }
                }
                else
                    return false;
            }
            else if (strConsultationStatus == "1")
            {
                if (clsEMR_StaticObject.s_ObjEmpDeptAndAreaArr != null && clsEMR_StaticObject.s_ObjEmpDeptAndAreaArr.Length > 0)
                {
                    if (clsEMR_StaticObject.s_ObjEmpDeptAndAreaArr[0].m_intDEFAULT_INPATIENT_DEPT_INT == 0)
                    {
                        return false;
                    }
                    DeptIDArr = new string[1];
                    DeptIDArr[0] = clsEMR_StaticObject.s_ObjEmpDeptAndAreaArr[0].m_strDEPTID_CHR;
                }
            }
            else
                return false;

             long lngRes = (new weCare.Proxy.ProxyEmr03()).Service.m_lngGetUnSignContent(DeptIDArr, out m_objConsultationContentArr);

            if (lngRes > 0 && m_objConsultationContentArr != null)
            {
                m_mthSetConsultation(m_objConsultationContentArr);
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 设置会诊信息提示
        /// <summary>
        /// 设置会诊信息提示
        /// </summary>
        /// <param name="objRecord">会诊内容</param>
        /// <returns></returns>
        private string m_strTips(clsConsultationRecordContent objRecord)
        {
            if (objRecord == null)
                return "";

            //查新表，打开相应表单同步病人信息
            string strHISID = "";
             long lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetHISIDByEMRID(objRecord.m_strInPatientID, out strHISID);

            if (string.IsNullOrEmpty(strHISID))
            {
                return "";
            }
            clsPatient objPatient = new clsPatient(objRecord.m_strInPatientID, strHISID, null);
            string strTips = string.Empty;
            try
            {
                strTips = "病人信息：" + objPatient.m_ObjPeopleInfo.m_StrLastName + " "
                        + objPatient.m_ObjPeopleInfo.m_StrSex + " " + objPatient.m_ObjPeopleInfo.m_StrAge
                        + System.Environment.NewLine + "双击打开【会诊通知】查看其它详细信息。";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return strTips;
        }
        #endregion

        #region 显示会诊通知具体内容
        /// <summary>
        /// 显示会诊通知具体内容
        /// </summary>
        private void m_mthShowFrmConsultationSearch()
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom(Application.StartupPath + "\\" + "EMR_iCareComponent.dll");
            object frm = asm.CreateInstance("iCare.frmConsultationSearch");
            Type type = frm.GetType();
            Type[] typeArray = new Type[1];
            typeArray.SetValue(typeof(string), 0);
            System.Reflection.MethodInfo mth = type.GetMethod("m_mthOpenThis", typeArray);
            mth.Invoke(frm, new object[] { ((clsConsultationRecordContent)m_lsvDetail.SelectedItems[0].Tag).m_strInPatientID });
        }
        #endregion
        #endregion

        #region 病案查阅申请
        #region 查询是否有已获批准的病案查阅申请
        /// <summary>
        /// 查询是否有已获批准的病案查阅申请
        /// </summary>
        /// <returns></returns>
        private bool m_blnHasArchivedAgreeInfo()
        {
            bool blnIsHas = false;
            try
            {
                long lngRes = (new weCare.Proxy.ProxyEmr06()).Service.m_lngGetApprovedRequestHistory( clsEMR_StaticObject.s_ObjCurrentEmployee.m_strEMPID_CHR, out m_objCaseSubscribeContentArr);
                if (m_objCaseSubscribeContentArr != null && m_objCaseSubscribeContentArr.Length > 0)
                {
                    m_mthSetCaseSubscribe(m_objCaseSubscribeContentArr);
                    return true;
                }
            }
            catch (Exception Ex)
            {
                string strEx = Ex.Message;
            }
            return blnIsHas;
        }
        #endregion

        #region 设置病案申请内容至ListView
        /// <summary>
        /// 设置病案申请内容至ListView
        /// </summary>
        /// <param name="p_objCaseSubscribeContentArr">病案申请内容</param>
        private void m_mthSetCaseSubscribe(clsEMR_CaseSubscribeValue[] p_objCaseSubscribeContentArr)
        {
            if (p_objCaseSubscribeContentArr != null)
            {
                m_lsvDetail.BeginUpdate();
                for (int i = 0; i < p_objCaseSubscribeContentArr.Length; i++)
                {
                    ListViewItem item = new ListViewItem(new string[] { "病案查阅申请已批准",
                        "病案室",p_objCaseSubscribeContentArr[i].m_dtmAPPROVEDDATE_DAT.ToString("yyyy-MM-dd HH:mm"),""});
                    item.Tag = p_objCaseSubscribeContentArr[i];
                    m_lsvDetail.Items.Add(item);
                    m_lsvDetail.Groups["CaseArchivedAgree"].Items.Add(item);
                }
                m_lsvDetail.EndUpdate();
                p_objCaseSubscribeContentArr = null;
                Application.DoEvents();
            }
        }
        #endregion

        #region 显示病案查阅记录窗体
        /// <summary>
        /// 显示病案查阅记录窗体
        /// </summary>
        private void m_mthShowFrmBorrowCase()
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom(Application.StartupPath + "\\" + "EMR_CaseArchiving_GUI.dll");
            object frm = asm.CreateInstance("com.digitalwave.emr.EMR_CaseArchiving.frmBorrowCase");
            Type type = frm.GetType();
            System.Reflection.MethodInfo mth = type.GetMethod("m_mthOpenThis");
            mth.Invoke(frm, null);
        }
        #endregion 
        #endregion

        #region 检查是否属于病案室
        /// <summary>
        /// 检查是否属于病案室
        /// </summary>
        /// <returns></returns>
        private bool m_blnCheckHasClinicRight()
        {
            bool blnHasRight = false;
            string strRoleID = string.Empty;
            long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetRoleIDByRoleName("病案室", out strRoleID);
            if (clsEMR_StaticObject.s_ObjCurrentEmployee.m_strRoleIDArr != null)
            {
                for (int i1 = 0; i1 < clsEMR_StaticObject.s_ObjCurrentEmployee.m_strRoleIDArr.Length; i1++)
                {
                    if (clsEMR_StaticObject.s_ObjCurrentEmployee.m_strRoleIDArr[i1] == strRoleID)
                    {
                        blnHasRight = true;
                        break;
                    }
                }
            }
            return blnHasRight;
        }
        #endregion

        #region 检查是否有未审批的病案申请
        /// <summary>
        /// 检查是否有未审批的病案申请
        /// </summary>
        private bool m_blnHasArchivedRequest()
        {
            bool blnIsHas = false;
            try
            {
                long lngRes = proxy.Service.m_lngGetUnApprovedArchivedCaseInfo(out m_objCaseSubscribeRequestArr);
                if (m_objCaseSubscribeRequestArr != null && m_objCaseSubscribeRequestArr.Length > 0)
                {
                    m_mthSetCaseSubscribeRequest(m_objCaseSubscribeRequestArr);
                    return true;
                }
            }
            catch (Exception Ex)
            {
                string strEx = Ex.Message;
            }
            return blnIsHas;
        }
        #endregion

        #region 设置未审批的病案申请至界面
        /// <summary>
        /// 设置未审批的病案申请至界面
        /// </summary>
        /// <param name="p_objCaseSubscribeContentArr"></param>
        private void m_mthSetCaseSubscribeRequest(clsEMR_CaseSubscribeValue[] p_objCaseSubscribeContentArr)
        {
            if (p_objCaseSubscribeContentArr != null)
            {
                m_lsvDetail.BeginUpdate();
                for (int i = 0; i < p_objCaseSubscribeContentArr.Length; i++)
                {
                    ListViewItem item = new ListViewItem(new string[] { "病案查阅申请",
                        p_objCaseSubscribeContentArr[i].m_strDefaultDeptName + "    " + p_objCaseSubscribeContentArr[i].m_strSUBSCRIBERNAME,
                        p_objCaseSubscribeContentArr[i].m_dtmCREATEDATE_DAT.ToString("yyyy-MM-dd HH:mm"),""});
                    item.Tag = p_objCaseSubscribeContentArr[i];
                    m_lsvDetail.Items.Add(item);
                    m_lsvDetail.Groups["CaseArchivedRequest"].Items.Add(item);
                }
                m_lsvDetail.EndUpdate();
                p_objCaseSubscribeContentArr = null;
                Application.DoEvents();
            }
        }
        #endregion

        #region 显示病案审批窗体
        /// <summary>
        /// 显示病案审批窗体
        /// </summary>
        private void m_mthShowFrmApproveCase()
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom(Application.StartupPath + "\\" + "EMR_CaseArchiving_GUI.dll");
            object frm = asm.CreateInstance("com.digitalwave.emr.EMR_CaseArchiving.frmEMR_ApproveArchivedCase");
            Type type = frm.GetType();
            System.Reflection.MethodInfo mth = type.GetMethod("m_mthOpenThis");
            mth.Invoke(frm, null);
        }
        #endregion
        #endregion
    }
}