using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmRegisterRepeatPrt : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        private string OldNo = "";
        private string newno = "";
        /// <summary>
        /// �º�
        /// </summary>
        public string NewNo
        {
            get
            {
                return newno;
            }
        }
        private string prntype = "";
        /// <summary>
        /// �ش����� 1 ԭ�� 2 �º�
        /// </summary>
        public string PrnType
        {
            get
            {
                return prntype;
            }
        }

        public frmRegisterRepeatPrt(string oldno)
        {
            InitializeComponent();
            OldNo = oldno;
        }

        private void frmRegisterRepeatPrt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRegisterRepeatPrt_Load(object sender, EventArgs e)
        {
            this.lblOldNo.Text = OldNo;

            this.rdo1.Checked = true;
            this.rdo2.Checked = false;
            this.txtNewNo.Text = "";
            this.txtNewNo.Enabled = false;
        }

        private void rdo1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdo1.Checked)
            {
                this.txtNewNo.Text = "";
                this.txtNewNo.Enabled = false;
            }
        }

        private void rdo2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdo2.Checked)
            {
                this.txtNewNo.Enabled = true;
                this.txtNewNo.Focus();
            }
        }

        #region ���Һŷ�Ʊ���ʽ true ͨ�� false δͨ��
        /// <summary>
        /// ���Һŷ�Ʊ���ʽ true ͨ�� false δͨ��
        /// </summary>
        /// <param name="InvoNo"></param>
        /// <returns></returns>
        public bool m_blnCheckInvoNoExpression(string InvoNo)
        {
            string Exp = "";
            bool ret = false;
                        
            try
            {
                if (File.Exists(Application.StartupPath + "\\LoginFile.xml"))
                {
                    //��ȡ�������ñ��ʽ
                    XmlDocument doc = new XmlDocument();
                    doc.Load(Application.StartupPath + "\\LoginFile.xml");
                    XmlNode xn = doc.DocumentElement.SelectSingleNode("InvoiceExpression");
                    Exp = xn.InnerText;

                    //������ʽ�Ƚ�
                    Regex r = new Regex(Exp);
                    Match m = r.Match(InvoNo);
                    if (m.Success)
                    {
                        ret = true;
                    }                    
                }
            }
            catch
            {
                return false;
            }

            return ret;
        }
        #endregion

        #region ���Һŷ�Ʊ�Ƿ�ʹ�� true ��ʹ�� false δ��ʹ��
        /// <summary>
        /// ���Һŷ�Ʊ�Ƿ�ʹ�� true ��ʹ�� false δ��ʹ��
        /// </summary>
        /// <param name="InvoNo"></param>
        /// <returns></returns>
        public bool m_blnCheckInvoNoIsUse(string InvoNo)
        {           
            clsDomainControl_Register domain = new clsDomainControl_Register();

            if (!domain.m_mthIsCanDo("0008"))
            {
                DataTable dt = null;
                long l = domain.m_lngCheckNO(InvoNo.Trim(), out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    MessageBox.Show("�÷�Ʊ��" + InvoNo + "����" + DateTime.Parse(dt.Rows[dt.Rows.Count - 1]["REGISTERDATE_DAT"].ToString()).ToString("yyyy-MM-dd") + "��" + dt.Rows[dt.Rows.Count - 1]["EMPNO_CHR"].ToString().Trim() + "ʹ��\r\n\r\n����������(�뵱ǰ��ӡƱ�ݺ���ͬ)��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtNewNo.Focus();                    
                    return true;
                }
            }

            return false;
        }
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            newno = this.txtNewNo.Text.Trim();

            if (this.rdo1.Checked)
            {
                prntype = "1";
            }
            else if (this.rdo2.Checked)
            {
                if (newno == "")
                {
                    MessageBox.Show("�������º���!", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!this.m_blnCheckInvoNoExpression(newno))
                {
                    MessageBox.Show("��ǰ�Һŷ�Ʊ�ı�Ų����ϱ����������������(�뵱ǰ��ӡƱ�ݺ���ͬ)��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtNewNo.Focus();
                    this.txtNewNo.SelectAll();
                    return;
                }

                if (this.m_blnCheckInvoNoIsUse(newno))
                {                    
                    this.txtNewNo.Focus();
                    this.txtNewNo.SelectAll();
                    return;
                }

                prntype = "2";
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}