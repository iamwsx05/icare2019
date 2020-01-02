using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS.Reports
{
    /// <summary>
    /// ������Ƶ����UI��
    /// </summary>
    public partial class frmAnimation : Form
    {
        /// <summary>
        /// ��ƵAVI�ļ���
        /// </summary>
        private string AviFile = "";
        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        private string MsgInfo = "";
        /// <summary>
        /// ����
        /// </summary>
        public frmAnimation(string AviFileName, string MessageInfo)
        {
            InitializeComponent();
            AviFile = AviFileName;
            MsgInfo = MessageInfo;            
        }

        private void frmAnimation_Load(object sender, EventArgs e)
        {
            this.m_mthStart();
        }

        /// <summary>
        /// start
        /// </summary>
        public void m_mthStart()
        {
            if (AviFile.Trim() != "")
            {
                this.axAnimation.Open(Application.StartupPath + "\\" + AviFile);
            }
            else
            {
                this.axAnimation.Open(Application.StartupPath + "\\findFILE.avi");
            }

            this.axAnimation.Play();

            if (MsgInfo.Trim() != "")
            {
                this.lblInfo.Text = MsgInfo;
            }
            else
            {
                this.lblInfo.Text = "ϵͳ�����У����Ժ�...";
            }
        }
    }
}