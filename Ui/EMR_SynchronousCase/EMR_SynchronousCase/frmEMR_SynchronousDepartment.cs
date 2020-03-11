using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.emr.EMR_SynchronousCase
{
    /// <summary>
    /// 病案同步科室配置
    /// </summary>
    public partial class frmEMR_SynchronousDepartment : Form
    {
        #region 变量
        /// <summary>
        /// iCare病案科室同步配置,=0专业组,=1科室
        /// </summary>
        int intDeptType;
        #endregion

        /// <summary>
        /// 病案同步科室配置构造函数

        /// </summary>
        public frmEMR_SynchronousDepartment()
        {
            InitializeComponent();
            clsEMR_SynchronousDeptDomain objDomain = new clsEMR_SynchronousDeptDomain();
            objDomain.m_lngGetBASynchronousSet("3017", out intDeptType);
            if (intDeptType == 0)
            {
                groupBox1.Text = "iCare专业组";
            }
            else if (intDeptType == 1)
            {
                groupBox1.Text = "iCare科室";
            }
        }

        #region 事件
        private void m_cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_cmdSave_Click(object sender, EventArgs e)
        {
            m_mthSaveDeptExtendID();
        }

        private void frmEMR_SynchronousDepartment_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        #region 后台线程操作
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            clsEMR_SynchronousDeptDomain objDomain = new clsEMR_SynchronousDeptDomain();
             clsEmrDept_VO[] objIcareDept = null;
             clsEmrDept_VO[] objBADept = null;

            long lngRes = objDomain.m_lngGetICareDeptList(intDeptType, out objIcareDept);
            lngRes = objDomain.m_lngGetBADeptList(out objBADept);
            objDomain = null;

            System.Collections.ArrayList arrDept = new System.Collections.ArrayList();
            arrDept.Add(objIcareDept);
            arrDept.Add(objBADept);

            e.Result = arrDept;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            System.Collections.ArrayList arrDept = e.Result as System.Collections.ArrayList;

             clsEmrDept_VO[] objIcareDept = arrDept[0] as  clsEmrDept_VO[];
             clsEmrDept_VO[] objBADept = arrDept[1] as  clsEmrDept_VO[];

            if (objIcareDept != null)
            {
                try
                {
                    m_lsvIcareDept.BeginUpdate();
                    ListViewItem lsi = null;
                    List<ListViewItem> lsiList = new List<ListViewItem>();
                    for (int iI = 0; iI < objIcareDept.Length; iI++)
                    {
                        lsi = new ListViewItem(objIcareDept[iI].m_strDEPTNAME_VCHR);
                        lsi.Tag = objIcareDept[iI];
                        lsiList.Add(lsi);
                    }
                    m_lsvIcareDept.Items.AddRange(lsiList.ToArray());
                }
                catch (Exception Ex)
                {
                    string strEx = Ex.Message;
                }
                finally
                {
                    m_lsvIcareDept.EndUpdate();
                }
            }

            if (objBADept != null)
            {
                ListViewItem lsi = null;
                for (int iI = 0; iI < objBADept.Length; iI++)
                {
                    lsi = new ListViewItem(objBADept[iI].m_strDEPTNAME_VCHR);
                    lsi.Tag = objBADept[iI];
                    m_lsvBADept.Items.Add(lsi);
                }
            }

            if (m_lsvIcareDept.Items.Count > 0)
            {
                m_lsvIcareDept.Focus();
                m_lsvIcareDept.Items[0].Selected = true;
            }
        }
        #endregion

        private void m_lsvBADept_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_mthSelectedRelateDept();
        }

        private void m_lsvIcareDept_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                bool blnHasRelation = m_blnCheckHasRelation(e.Item);

                if (blnHasRelation)
                {
                    MessageBox.Show("此iCare专业组已关联其它病案科室！", "科室同步", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Item.Checked = false;
                    return;
                }
            }
        }
        #endregion

        #region 保存扩展ID修改
        /// <summary>
        /// 保存扩展ID修改
        /// </summary>
        private void m_mthSaveDeptExtendID()
        {
            if (m_lsvBADept.Items.Count <= 0 || m_lsvBADept.SelectedItems.Count != 1 || m_lsvBADept.SelectedItems[0].Tag == null)
            {
                return;
            }

           clsEmrDept_VO objDeptVO_BA = m_lsvBADept.SelectedItems[0].Tag as clsEmrDept_VO;
            clsEMR_SynchronousDeptDomain objDomain = new clsEMR_SynchronousDeptDomain();
            long lngRes = 0;

            string[] strIcare_GroupID = null;
            string[] strIcare_DeptName = null;

            int intItmesCount = m_lsvIcareDept.CheckedItems.Count;
            if (intItmesCount > 0)
            {
                strIcare_GroupID = new string[intItmesCount];
                strIcare_DeptName = new string[intItmesCount];
               clsEmrDept_VO objDeptVO_Icare = null;
                for (int iI = 0; iI < intItmesCount; iI++)
                {
                    objDeptVO_Icare = m_lsvIcareDept.CheckedItems[iI].Tag as clsEmrDept_VO;
                    strIcare_GroupID[iI] = objDeptVO_Icare.m_strDEPTID_CHR;
                    strIcare_DeptName[iI] = objDeptVO_Icare.m_strDEPTNAME_VCHR;
                }
            }
            lngRes = objDomain.m_lngModiftGroupRelation(strIcare_GroupID, objDeptVO_BA.m_strDEPTID_CHR, intDeptType, strIcare_DeptName, objDeptVO_BA.m_strDEPTNAME_VCHR);
            objDomain = null;
            if (lngRes > 0)
            {
                MessageBox.Show("保存成功！", "科室同步", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("保存失败！", "科室同步", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 选中相关联的病案科室
        /// <summary>
        /// 选中相关联的病案科室
        /// </summary>
        private void m_mthSelectedRelateDept()
        {
            foreach (ListViewItem lsi in m_lsvIcareDept.CheckedItems)
            {
                lsi.Checked = false;
            }

            if (m_lsvBADept.Items.Count <= 0 || m_lsvBADept.SelectedItems.Count <= 0 || m_lsvIcareDept.Items.Count <= 0)
            {
                return;
            }

            clsEmrDept_VO objDeptVO_Icare = null;
           clsEmrDept_VO objDeptVO_BA = m_lsvBADept.SelectedItems[0].Tag as clsEmrDept_VO;

            string[] strIcare_GroupIDArr = null;
            clsEMR_SynchronousDeptDomain objDomain = new clsEMR_SynchronousDeptDomain();
            long lngRes = objDomain.m_lngGetICare_GroupID(objDeptVO_BA.m_strDEPTID_CHR, intDeptType, out strIcare_GroupIDArr);
            objDomain = null;

            if (strIcare_GroupIDArr != null && strIcare_GroupIDArr.Length > 0)
            {
                for (int iItem = 0; iItem < strIcare_GroupIDArr.Length; iItem++)
                {
                    foreach (ListViewItem lsi in m_lsvIcareDept.Items)
                    {
                        objDeptVO_Icare = lsi.Tag as clsEmrDept_VO;
                        if (objDeptVO_Icare.m_strDEPTID_CHR.Trim() == strIcare_GroupIDArr[iItem])
                        {
                            lsi.Checked = true;
                        }
                    }
                }
            }
        }
        #endregion

        #region 是否已关联于其它项

        /// <summary>
        /// 是否已关联于其它项

        /// </summary>
        /// <param name="p_lsiGroupItem">选中的专业组类别</param>
        /// <returns></returns>
        private bool m_blnCheckHasRelation(ListViewItem p_lsiGroupItem)
        {
            if (p_lsiGroupItem == null || p_lsiGroupItem.Tag == null)
            {
                return true;
            }

            if (m_lsvBADept.Items.Count <= 0 || m_lsvBADept.SelectedItems.Count != 1 || m_lsvBADept.SelectedItems[0].Tag == null)
            {
                return true;
            }

             clsEmrDept_VO objDeptVO_BA = m_lsvBADept.SelectedItems[0].Tag as  clsEmrDept_VO;
            clsEmrDept_VO objDeptVO_Icare = p_lsiGroupItem.Tag as  clsEmrDept_VO;

            clsEMR_SynchronousDeptDomain objDomain = new clsEMR_SynchronousDeptDomain();
            string strBADeptNum = string.Empty;
            long lngRes = objDomain.m_lngGetBADeptNum(objDeptVO_Icare.m_strDEPTID_CHR, intDeptType, out strBADeptNum);
            if (!string.IsNullOrEmpty(strBADeptNum) && strBADeptNum != objDeptVO_BA.m_strDEPTID_CHR)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}