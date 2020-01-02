using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ̨ɽҽ�����㴰�ڵ�ժҪ˵��
    /// </summary>
    public partial class frmYB_TS : Form
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public frmYB_TS()
        {
            InitializeComponent();
        }

        private bool m_blnIsRecived = false;

        private clsDcl_Charge objSvc;

        private string strSbmny;

        /// <summary>
        /// ��ʼסԺ��ˮ��(�������ʷ��ˮ���ظ�)
        /// </summary>
        private string _strStartZYLSH0;
        /// <summary>
        /// ��ʼסԺ��ˮ��
        /// </summary>
        public string strStartZYLSH0
        {
            get
            {
                return _strStartZYLSH0;
            }
            set
            {
                _strStartZYLSH0 = value;
            }
        }

        /// <summary>
        /// �Ը����
        /// </summary>
        private decimal sbmny = 0;
        /// <summary>
        /// �Ը����
        /// </summary>
        public decimal SbMny
        {
            get
            {
                return sbmny;
            }
        }

        /// <summary>
        /// ҽ���������ݼ�
        /// </summary>
        private DataTable dtyb = new DataTable();
        /// <summary>
        /// ҽ���������ݼ�
        /// </summary>
        public DataTable dtYB
        {
            get
            {
                return dtyb;
            }
        }

        /// <summary>
        /// Ԥ��Ժ������Ϣ
        /// </summary>
        public  clsBihPatient_VO objPatient_VO;

        private void frmYB_TS_Load(object sender, EventArgs e)
        {
            objSvc = new clsDcl_Charge();
            this.txtYBpay.ReadOnly = true;
            this.txtYBpay.BackColor = Color.White;
            this.txtSbmny.ReadOnly = true;
            this.txtSbmny.BackColor = Color.White;
            if (!m_blnIsRecived)
                btnComplete.Enabled = false;
            this.m_mthGetZYLSH0();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            long lngRes = 0;
            string message = "";            
            string strlsh0 = this.m_strGenerateZYLSH0(this.objPatient_VO.RegisterID);
            lngRes = objSvc.m_lngInsertRegister(strlsh0, this.objPatient_VO.Zyh);
            switch (lngRes)
            {
                case 1:
                    message = "�ϴ�Ԥ��Ժ������Ϣ�ɹ�";
                    break;
                case 0:
                    message = "�Ҳ���Ԥ��Ժ������Ϣ";
                    break;
                default:
                    message = "�ϴ�Ԥ��Ժ������Ϣʧ��";
                    break;
            }
            lngRes = objSvc.m_lngInsertRegisterCharge(strlsh0, this.objPatient_VO.Zyh);
            switch (lngRes)
            {
                case 1:
                    message += "\r\n\r\n�ϴ�������ϸ�ɹ�";
                    break;
                case 0:
                    message = "\r\n\r\n�ϴ� 0 ��������ϸ��¼";
                    break;
                default:
                    message = "\r\n\r\n�ϴ�������ϸʧ��";
                    break;
            }
            this.Cursor = Cursors.Default;
            MessageBox.Show(this, message, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            long lngRes = 0;
            string strMedno = "";
            string strYBpay = "";
            dtyb = new DataTable();
            dtyb.Columns.Add("medno");
            DataRow dr = dtyb.NewRow();            
            try
            {
                lngRes = this.objSvc.m_lngGetYBpay(this.objPatient_VO.RegisterID, out strMedno, out strYBpay);
            }
            catch (Exception objEx)
            {
                MessageBox.Show(this, objEx.Message, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }            
            if (lngRes > 0 && strYBpay != "")
            {
                this.txtYBpay.Text = strYBpay;
                this.txtSbmny.Text = Convert.ToString(Convert.ToDouble(lblTotal.Text) - Convert.ToDouble(txtYBpay.Text));
                m_blnIsRecived = true;
                sbmny = decimal.Parse(this.txtSbmny.Text);
            }
            dr["medno"] = strMedno;
            dtyb.Rows.Add(dr);
            dtyb.AcceptChanges();
            this.Cursor = Cursors.Default;
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            long lngRes = 0;
            string strlsh0 = this.m_strGenerateZYLSH0(this.objPatient_VO.RegisterID);
            lngRes = this.objSvc.m_lngDelYBInfo(strlsh0);
            if (lngRes > 0)
            {
                MessageBox.Show(this, "ɾ���ɹ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Cursor = Cursors.Default;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void txtYBpay_TextChanged(object sender, EventArgs e)
        {
            this.btnComplete.Enabled = true;
        }

        #region �����ʼ��ˮ��
        /// <summary>
        /// �����ʼ��ˮ��
        /// </summary>
        private void m_mthGetZYLSH0()
        {
            int s_intIsCheckedCfg = -1;
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\HISYB.xml"))
                s_intIsCheckedCfg = 1;
            else
                s_intIsCheckedCfg = 0;

            if (s_intIsCheckedCfg == 0)
            {
                _strStartZYLSH0 = "1000000";
            }
            else
            {
                System.Xml.XmlDocument objXml = new System.Xml.XmlDocument();
                objXml.Load(System.Windows.Forms.Application.StartupPath + "\\HISYB.xml");
                _strStartZYLSH0 = objXml.DocumentElement["TAISHAN.ZHONGYIYUAN"]["StartZYLSH0"].Attributes["value"].Value.ToString().Trim();
            }
        }
        #endregion        

        #region ����סԺ��ˮ��
        /// <summary>
        /// ����סԺ��ˮ��
        /// </summary>
        /// <param name="strInputString"></param>
        /// <returns></returns>
        private string m_strGenerateZYLSH0(string strInputString)
        {
            string ZYLSH0 = _strStartZYLSH0;
            if (strInputString.Length > 9)
            {
                strInputString = strInputString.Substring(strInputString.Length - 9, 9);
            }
            ZYLSH0 = Convert.ToString(Convert.ToInt32(_strStartZYLSH0) + Convert.ToInt32(strInputString));
            return ZYLSH0;
        }
        #endregion

        private void frmYB_TS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F2)
            {
                this.btnUpload_Click(sender, (EventArgs)e);
            }
            else if (e.KeyCode == Keys.F3)
            {
                this.btnPrepare_Click(sender, (EventArgs)e);
            }
            else if (e.KeyCode == Keys.F5)
            {
                this.btnReceive_Click(sender, (EventArgs)e);
            }
            else if (e.KeyCode == Keys.F6)
            {
                this.btnComplete_Click(sender, (EventArgs)e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.btnReturn_Click(sender, (EventArgs)e);
            }
            else
            { return; }
        }

        string strAppPath = "";
        private void m_cmdRun_Click(object sender, EventArgs e)
        {
            try
            {
                if (strAppPath == string.Empty)
                {
                    System.Xml.XmlDocument objXml = new System.Xml.XmlDocument();
                    objXml.Load(System.Windows.Forms.Application.StartupPath + "\\HISYB.xml");
                    strAppPath = objXml.DocumentElement["TAISHAN.ZHONGYIYUAN"]["ProcessPath"].Attributes["value"].Value.ToString().Trim();
                }
                string strlsh0 = this.m_strGenerateZYLSH0(this.objPatient_VO.RegisterID);
                System.Diagnostics.Process.Start(strAppPath, strlsh0);
            }
            catch (Exception objEx)
            {
                MessageBox.Show(this, objEx.Message, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPrepare_Click(object sender, EventArgs e)
        {
            try
            {
                if (strAppPath == string.Empty)
                {
                    System.Xml.XmlDocument objXml = new System.Xml.XmlDocument();
                    objXml.Load(System.Windows.Forms.Application.StartupPath + "\\HISYB.xml");
                    strAppPath = objXml.DocumentElement["TAISHAN.ZHONGYIYUAN"]["ProcessPath"].Attributes["value"].Value.ToString().Trim();
                }
                string strlsh0 = this.m_strGenerateZYLSH0(this.objPatient_VO.RegisterID);
                System.Diagnostics.Process.Start(strAppPath, strlsh0);
            }
            catch (Exception objEx)
            {
                MessageBox.Show(this, objEx.Message, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region ֱ���ϴ�������Ϣ
        /// <summary>
        /// ֱ���ϴ�������Ϣ
        /// </summary>
        /// <param name="p_strRegisterID"></param>
        /// <param name="p_strZyh"></param>
        public void m_mthUploadYbInfo(string p_strRegisterID, string p_strZyh)
        {
            long lngRes = 0;
            this.txtYBpay.ReadOnly = true;
            this.txtYBpay.BackColor = Color.White;
            this.txtSbmny.ReadOnly = true;
            this.txtSbmny.BackColor = Color.White;
            if (!m_blnIsRecived)
                btnComplete.Enabled = false;
            this.m_mthGetZYLSH0();
            objSvc = new clsDcl_Charge();
            string strlsh0 = this.m_strGenerateZYLSH0(this.objPatient_VO.RegisterID);
            lngRes = this.objSvc.m_lngDelYBInfo(strlsh0);
            lngRes = objSvc.m_lngInsertRegister(strlsh0, this.objPatient_VO.Zyh);
            lngRes = objSvc.m_lngInsertRegisterCharge(strlsh0, this.objPatient_VO.Zyh);
            this.btnUpload.Enabled = false;
            this.btnDel.Enabled = false;
        }
        #endregion        
    }
}