using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.emr.EMR_SynchronousCase
{
    /// <summary>
    /// 病案费别同步
    /// </summary>
    public partial class frmEMR_SynchronousPayType : Form
    {
        /// <summary>
        /// 病案费别同步构造函数

        /// </summary>
        public frmEMR_SynchronousPayType()
        {
            InitializeComponent();
        }

        #region 事件
        private void m_cmdSave_Click(object sender, EventArgs e)
        {
            m_mthSavePayTypeRelation();
        }

        private void m_cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_lsvBA_PayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_mthSelectedBAPayType();
        }

        private void frmEMR_SynchronousPayType_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void m_lsvICarePayType_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                bool blnHasRelation = m_mthCheckHasRelation(e.Item);

                if (blnHasRelation)
                {
                    MessageBox.Show("此费别已关联其它病案费别！","费别同步",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    e.Item.Checked = false;
                }
            }            
        }

        #region 后台线程操作
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            clsEMR_SynchronousPayTypeDomain objDomain = new clsEMR_SynchronousPayTypeDomain();
            DataTable dtbIcare = null;
            DataTable dtbBA = null;
            long lngRes = objDomain.m_lngGetICarePayTypeList(out dtbIcare);
            lngRes = objDomain.m_lngGetBAPayTypeList(out dtbBA);

            System.Collections.ArrayList arrPay = new System.Collections.ArrayList();
            arrPay.Add(dtbIcare);
            arrPay.Add(dtbBA);

            e.Result = arrPay;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            m_lsvBA_PayType.Items.Clear();
            m_lsvICarePayType.Items.Clear();

            System.Collections.ArrayList arrPay = e.Result as System.Collections.ArrayList;

            DataTable dtbIcare = arrPay[0] as DataTable;
            DataTable dtbBA = arrPay[1] as DataTable;

            if (dtbIcare != null)
            {
                try
                {
                    ListViewItem lsi = null;
                    int intRowsCount = dtbIcare.Rows.Count;
                    DataRow drCurrent = null;
                    for (int iI = 0; iI < intRowsCount; iI++)
                    {
                        drCurrent = dtbIcare.Rows[iI];
                        lsi = new ListViewItem(drCurrent["name"].ToString());
                        lsi.Tag = drCurrent["code"].ToString();
                        m_lsvICarePayType.Items.Add(lsi);
                    }
                }
                catch (Exception Ex)
                {
                    string strEx = Ex.Message;
                }
            }

            if (dtbBA != null)
            {
                ListViewItem lsi = null;
                int intRowsCount = dtbBA.Rows.Count;
                DataRow drCurrent = null;
                for (int iI = 0; iI < intRowsCount; iI++)
                {
                    drCurrent = dtbBA.Rows[iI];
                    lsi = new ListViewItem(drCurrent["name"].ToString());
                    lsi.Tag = drCurrent["code"].ToString();
                    m_lsvBA_PayType.Items.Add(lsi);
                }
            }

            if (m_lsvBA_PayType.Items.Count > 0)
            {
                m_lsvBA_PayType.Focus();
                m_lsvBA_PayType.Items[0].Selected = true;
            }
        }
        #endregion 
        #endregion

        #region 选中关联的iCare系统费别
        /// <summary>
        /// 选中关联的iCare系统费别
        /// </summary>
        private void m_mthSelectedBAPayType()
        {
            foreach (ListViewItem lsi in m_lsvICarePayType.CheckedItems)
            {
                lsi.Checked = false;
            }

            if (m_lsvBA_PayType.Items.Count <= 0 || m_lsvBA_PayType.SelectedItems.Count != 1 || m_lsvICarePayType.Items.Count <= 0)
            {
                return;
            }

            if (m_lsvBA_PayType.SelectedItems[0].Tag == null)
            {
                return;
            }

            clsEMR_SynchronousPayTypeDomain objDomain = new clsEMR_SynchronousPayTypeDomain();
            long lngRes = 0;
            string[] strPayTypeArr = null;

            lngRes = objDomain.m_lngGetICarePayType(m_lsvBA_PayType.SelectedItems[0].Tag.ToString(), out strPayTypeArr);
            objDomain = null;
            if (strPayTypeArr == null || strPayTypeArr.Length <= 0)
            {
                return;
            }

            foreach (ListViewItem lsi in m_lsvICarePayType.Items)
            {
                if (lsi.Tag == null)
                {
                    continue;
                }
                string strPayTypeID = lsi.Tag.ToString();
                for (int iT = 0; iT < strPayTypeArr.Length; iT++)
                {
                    if (strPayTypeID == strPayTypeArr[iT])
                    {
                        lsi.Checked = true;
                    }
                }
            }
        }
        #endregion

        #region 保存费别设置
        /// <summary>
        /// 保存费别设置
        /// </summary>
        private void m_mthSavePayTypeRelation()
        {
            if (m_lsvBA_PayType.Items.Count <= 0 || m_lsvBA_PayType.SelectedItems.Count != 1 || m_lsvBA_PayType.SelectedItems[0].Tag == null)
            {
                return;
            }

            clsEMR_SynchronousPayTypeDomain objDomain = new clsEMR_SynchronousPayTypeDomain();
            long lngRes = 0;

            string[] strPayTypeIDArr = null;
            int intCheckCount = m_lsvICarePayType.CheckedItems.Count;

            if (intCheckCount <= 0)
            {
                lngRes = objDomain.m_lngSavePayTypeConfig(m_lsvBA_PayType.SelectedItems[0].Tag.ToString(), null);
            }
            else
            {
                List<string> lstType = new List<string>();
                for (int iT = 0; iT < intCheckCount; iT++)
                {
                    if (m_lsvICarePayType.CheckedItems[iT].Tag != null)
                    {
                        lstType.Add(m_lsvICarePayType.CheckedItems[iT].Tag.ToString());
                    }
                }
                strPayTypeIDArr = lstType.ToArray();
                lngRes = objDomain.m_lngSavePayTypeConfig(m_lsvBA_PayType.SelectedItems[0].Tag.ToString(), strPayTypeIDArr);
            }
            objDomain = null;

            if (lngRes > 0)
            {
                MessageBox.Show("保存成功！", "费别同步", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("保存失败！", "费别同步", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 是否已关联于其它项

        /// <summary>
        /// 是否已关联于其它项

        /// </summary>
        /// <param name="p_lsiPay">选中iCare费别项</param>
        /// <returns></returns>
        private bool m_mthCheckHasRelation(ListViewItem p_lsiPay)
        {
            if (p_lsiPay == null || p_lsiPay.Tag == null)
            {
                return true;
            }

            if (m_lsvBA_PayType.Items.Count <= 0 || m_lsvBA_PayType.SelectedItems.Count != 1 || m_lsvBA_PayType.SelectedItems[0].Tag == null)
            {
                return true;
            }

            string strBAPayType = m_lsvBA_PayType.SelectedItems[0].Tag.ToString();
            string strIcarePayType = p_lsiPay.Tag.ToString();

            clsEMR_SynchronousPayTypeDomain objDomain = new clsEMR_SynchronousPayTypeDomain();
            string strBAPayTypeFromDB = string.Empty;
            long lngRes = objDomain.m_lngGetBAPayType(strIcarePayType, out strBAPayTypeFromDB);
            if (!string.IsNullOrEmpty(strBAPayTypeFromDB) && strBAPayType != strBAPayTypeFromDB)
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