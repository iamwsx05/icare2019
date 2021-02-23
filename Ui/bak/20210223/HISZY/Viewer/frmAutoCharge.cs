using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ����UI��
    /// </summary>
    public partial class frmAutoCharge : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        /// <summary>
        /// ����ʱ��
        /// </summary>
        private string CreateTime = "";
        /// <summary>
        /// ��ʱ����ִ��ʱ��
        /// </summary>
        private string StartTime = "";
        /// <summary>
        /// ����
        /// </summary>
        public frmAutoCharge()
        {
            InitializeComponent();
        }

        #region ˢ��
        /// <summary>
        /// ˢ��
        /// </summary>
        public void m_mthRefresh()
        {
            DataTable dt;

            clsDcl_Charge objCharge = new clsDcl_Charge();

            long l = objCharge.GetDayAccountsInfo(out dt);
            if (l > 0)
            {
                this.lvHistory.Items.Clear();
                string middate = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string feedate = dt.Rows[i]["feedate"].ToString();
                    string createdate = dt.Rows[i]["createdate"].ToString();
                    ListViewItem lv = new ListViewItem(Convert.ToString(i + 1));

                    lv.SubItems.Add("�ɹ�");
                    lv.SubItems.Add(createdate);
                    lv.SubItems.Add(feedate.Substring(0, 10) + " 00:00:00��" + feedate.Substring(0, 10) + " 23:59:59");
                    lv.SubItems.Add(feedate.Substring(0, 4) + feedate.Substring(5, 2) + feedate.Substring(8, 2));

                    if (i != 0)
                    {
                        DateTime dte1 = Convert.ToDateTime(middate);
                        DateTime dte2 = Convert.ToDateTime(feedate.Substring(0, 10));
                        TimeSpan ts = dte2.Subtract(dte1);
                        TimeSpan ts1 = new TimeSpan(1, 0, 0, 0);
                        if (ts.Days > 1)
                        {
                            for (int j = 1; j < ts.Days; j++)
                            {
                                dte1 = dte1.Add(ts1);
                                ListViewItem lv1 = new ListViewItem("*");
                                lv1.SubItems.Add("ʧ��");
                                lv1.SubItems.Add(dte1.ToString("yyyy-MM-dd"));
                                lv1.SubItems.Add("*");
                                lv1.SubItems.Add("*");
                                lv1.ForeColor = Color.Red;
                                lv1.ImageIndex = 1;
                                lv1.Tag = "failure";
                                this.lvHistory.Items.Add(lv1);
                            }
                        }
                    }
                    lv.ImageIndex = 0;
                    lv.Tag = "success";
                    this.lvHistory.Items.Add(lv);

                    middate = feedate.Substring(0, 10);
                }
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// �Զ�����
        /// </summary>
        /// <param name="Flag">1 ��ʱ���� 2 �ֹ��Զ�����</param>
        public void m_mthAutoBuild(int Flag)
        {
            if (Flag != 1)
            {
                if (MessageBox.Show("��ȷ���Ƿ�ʼ�ֹ����ѣ�", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            try
            {
                long l = 0;
                string status = "";
                string date = "";
                TimeSpan ts = new TimeSpan(1, 0, 0, 0);
                bool bstatus = false;

                if (this.lvHistory.Items.Count == 0)
                {
                    MessageBox.Show("���ֹ����ɳ�ʼ��һ��ķ��á�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                clsDcl_Charge objCharge = new clsDcl_Charge();

                this.Cursor = Cursors.WaitCursor;
                clsPublic.PlayAvi("findFILE.avi", "�������ɷ�����Ϣ�����Ժ�...");

                CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                if (this.lvHistory.Items.Count > 0)
                {
                    for (int i = 0; i < this.lvHistory.Items.Count; i++)
                    {
                        status = this.lvHistory.Items[i].Tag.ToString();
                        date = this.lvHistory.Items[i].SubItems[4].Text.Trim();
                        date = date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2);

                        if (status.ToLower() == "success")
                        {
                            continue;
                        }

                        l = objCharge.AutoCharge(CreateTime, date + " 23:59:59", this.LoginInfo.m_strEmpID, null, 1);
                        if (l <= 0)
                        {
                            clsPublic.CloseAvi();
                            this.Cursor = Cursors.Default;
                            this.m_mthRefresh();
                            MessageBox.Show("����ʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        bstatus = true;
                    }
                }

                DateTime dte = DateTime.Now.Subtract(ts);

                if (Convert.ToDateTime(dte.ToString("yyyy-MM-dd")) > Convert.ToDateTime(date))
                {
                    DateTime dte1 = Convert.ToDateTime(date);
                    TimeSpan ts1 = dte.Subtract(dte1);
                    for (int j = 0; j < ts1.Days; j++)
                    {
                        dte1 = dte1.Add(ts);

                        l = objCharge.AutoCharge(CreateTime, dte1.ToString("yyyy-MM-dd") + " 23:59:59", this.LoginInfo.m_strEmpID, null, 1);
                        if (l <= 0)
                        {
                            clsPublic.CloseAvi();
                            this.Cursor = Cursors.Default;
                            this.m_mthRefresh();
                            MessageBox.Show("����ʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        bstatus = true;
                    }
                }

                clsPublic.CloseAvi();
                this.Cursor = Cursors.Default;
                if (bstatus)
                {
                    this.m_mthRefresh();
                    if (Flag != 1)
                    {
                        MessageBox.Show("���ѳɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    if (Flag != 1)
                    {
                        MessageBox.Show("û���·������ݡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch
            {
                clsPublic.CloseAvi();
                this.Cursor = Cursors.Default;
                this.m_mthRefresh();
                MessageBox.Show("����ʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        /// <summary>
        /// �ֹ�����
        /// </summary>
        public void m_mthHandBuild()
        {
            frmAutoChargeDate f = new frmAutoChargeDate();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string date = f.FeeDate;

                if (MessageBox.Show("�Ƿ��ֹ���������Ϊ��" + date + "�ķ��ã�", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                try
                {
                    clsDcl_Charge objCharge = new clsDcl_Charge();

                    this.Cursor = Cursors.WaitCursor;

                    clsPublic.PlayAvi("findFILE.avi", "�������ɷ�����Ϣ�����Ժ�...");

                    CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    long l = objCharge.AutoCharge(CreateTime, date + " 23:59:59", this.LoginInfo.m_strEmpID, null, 1); 
                    clsPublic.CloseAvi();

                    if (l > 0)
                    {
                        this.m_mthRefresh();
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("���ѳɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("����ʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                catch
                {
                    clsPublic.CloseAvi();
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("����ʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }
        #endregion

        private void frmAutoCharge_Load(object sender, EventArgs e)
        {
            StartTime = clsPublic.m_strGetSysparm("0004");

            this.m_mthRefresh();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.m_mthAutoBuild(2);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.m_mthHandBuild();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.m_mthRefresh();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (StartTime == "")
            {
                if (CreateTime.Substring(11, 8) == "03:00:00")
                {
                    this.m_mthAutoBuild(1);
                }
            }
            else
            {
                if (CreateTime.Substring(11, 8) == StartTime)
                {
                    this.m_mthAutoBuild(1);
                }
            }
        }
    }
}