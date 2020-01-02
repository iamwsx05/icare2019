using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.LIS
{
    public partial class frmDefaultHandle : Form
    {
        public string m_strCheckDate;
        public List<string> m_lstDeviceSampleID;

        public frmDefaultHandle()
        {
            InitializeComponent();
        }

        private void frmDefaultHandle_Load(object sender, EventArgs e)
        {
            m_lstDeviceSampleID = new List<string>();
            m_dtpDeviceCheckDate.Value = DateTime.Now;
        }

        private void m_cmdOK_Click(object sender, EventArgs e)
        {
            m_strCheckDate = "";
            m_lstDeviceSampleID.Clear();

            string strSampleID_1 = m_txtDeviceSampleID.Text.Trim();
            string strSampleID_2 = m_txtDeviceSampleID_2.Text.Trim();
            m_strCheckDate = m_dtpDeviceCheckDate.Value.ToString("yyyy-MM-dd");
            if (string.IsNullOrEmpty(strSampleID_1))
            {
                MessageBox.Show("请输入仪器样本号!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(strSampleID_2))
            {
                m_lstDeviceSampleID.Add(strSampleID_1);
                this.DialogResult = DialogResult.OK;
                this.Visible = false;
            }
            else
            {
                int iSampleID_1 = 0;
                int iSampleID_2 = 0;
                bool blnSampleID_1 = int.TryParse(strSampleID_1, out iSampleID_1);
                bool blnSampleID_2 = int.TryParse(strSampleID_2, out iSampleID_2);
                int idx = 0;
                if (blnSampleID_1 && blnSampleID_2)
                {
                    for (idx = iSampleID_1; idx <= iSampleID_2; idx++)
                    {
                        m_lstDeviceSampleID.Add(idx.ToString());
                    }
                    this.DialogResult = DialogResult.OK;
                    this.Visible = false;
                }
                else
                {
                    MessageBox.Show("请输入正确的仪器样本号!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //int iTemp = 0;
                //if (int.TryParse(strSampleID_1, out iTemp))
                //{

                //}
                //string strTemp = "";
                //string strOrder = "";
                
                //int idx = strSampleID_1.Length - 1;
                //for (; idx >= 0; idx--)
                //{
                //    strTemp = strSampleID_1.Substring(idx, 1);
                //    if (int.TryParse(strTemp, out iTemp))
                //    {
                //        strOrder = strTemp + strOrder;
                //    }
                //    else
                //    {
                //        break;
                //    }
                //}
                //strTemp = strSampleID_1.Substring(0, idx + 1);
                //string strOrder2 = strSampleID_2.Replace(strTemp, "");

                //int.TryParse(strTemp, out iTemp);
                //int iOrder2 = 0;
                //if (int.TryParse(strOrder2, out iOrder2))
                //{
                //    if (iTemp > iOrder2)
                //    {
                //        MessageBox.Show("请输入正确的仪器样本号!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        return;
                //    }
                //    else
                //    {
                //        for (int i = iTemp; i <= iOrder2; i++)
                //        {
                //            m_lstDeviceSampleID.Add(strTemp + i.ToString());
                //        }
                //        this.DialogResult = DialogResult.OK;
                //        this.Visible = false;
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("请输入正确的仪器样本号!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
            }
        }

        private void m_cmdCancel_Click(object sender, EventArgs e)
        {
            m_strCheckDate = "";
            m_lstDeviceSampleID.Clear();
            this.DialogResult = DialogResult.Cancel;
            this.Visible = false;
        }
    }
}