using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.RemindMessage
{
    public partial class frmEMR_SimpleRemindMessage : Form
    {
        /// <summary>
        /// 提醒信息
        /// </summary>
        private string m_strMessate = string.Empty;

        /// <summary>
        /// 简单提醒窗体

        /// </summary>
        public frmEMR_SimpleRemindMessage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取或设置提醒信息

        /// </summary>
        public string m_StrMessage
        {
            get { return m_txtSimpleMessage.Text; }
            set 
            {
                if (string.IsNullOrEmpty(m_txtSimpleMessage.Text))
                {
                    m_txtSimpleMessage.AppendText ( value);
                }
                else
                {
                    m_txtSimpleMessage.AppendText( System.Environment.NewLine + System.Environment.NewLine + value);
                }
                m_txtSimpleMessage.ScrollToCaret();
            }
        }

        private void m_cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEMR_SimpleRemindMessage_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_txtSimpleMessage.Clear();
        }
    }
}