using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;

namespace com.digitalwave.iCare.gui.DataExchangeSystem
{
    public partial class frmDataExchangeMain : Form
    {
        #region ����DLL����
        [DllImport("kernel32.dll")]
        public extern static int GetPrivateProfileString(string segName, string keyName, string sDefault, StringBuilder buffer, int nSize, string fileName);

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileStringA")]
        public extern static int GetPrivateProfileStringA(string segName, string keyName, string sDefault, byte[] buffer, int iLen, string fileName); // ANSI�汾


        [DllImport("kernel32.dll")]
        public extern static int GetPrivateProfileSection(string segName, StringBuilder buffer, int nSize, string fileName);

        [DllImport("kernel32.dll")]
        public extern static int WritePrivateProfileSection(string segName, string sValue, string fileName);

        [DllImport("kernel32.dll")]
        public extern static int WritePrivateProfileString(string segName, string keyName, string sValue, string fileName);

        [DllImport("kernel32.dll")]
        public extern static int GetPrivateProfileSectionNamesA(byte[] buffer, int iLen, string fileName);

        #endregion

        #region ����
        /// <summary>
        /// ini�ļ���
        /// </summary>
        public string _IniFileName;

        /// <summary>
        ///������ 
        /// </summary>
        private clsCtl_DataExchangeMain objController;
        #endregion


        public frmDataExchangeMain()
        {
            InitializeComponent();
            _IniFileName = Application.StartupPath + "\\DataExchangeSetting.ini";

        }

        private void butUpload_Click(object sender, EventArgs e)//�����ϴ�����
        {
            ((clsCtl_DataExchangeMain)objController).m_mthUploadExchangeData();
        }

        #region �������ݽ�dataGridViewUpdata
        /// ���ظ������ļ�������Section���Ƶļ���

        public ArrayList ReadSections()
        {
            byte[] buffer = new byte[65535];
            int rel = GetPrivateProfileSectionNamesA(buffer, buffer.GetUpperBound(0), _IniFileName);
            int iCnt, iPos;
            ArrayList arrayList = new ArrayList();
            string tmp;
            if (rel > 0)
            {
                iCnt = 0; iPos = 0;
                for (iCnt = 0; iCnt < rel; iCnt++)
                {
                    if (buffer[iCnt] == 0x00)
                    {
                        tmp = System.Text.ASCIIEncoding.Default.GetString(buffer, iPos, iCnt - iPos).Trim();
                        iPos = iCnt + 1;
                        if (tmp != "")
                            arrayList.Add(tmp);
                    }
                }
            }
            return arrayList;
        }

        /// <summary>
        /// ��ȡ�����ļ�ĳsection�µ�����key����
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public ArrayList ReadKeys(string sectionName)
        {

            byte[] buffer = new byte[5120];
            int rel = GetPrivateProfileStringA(sectionName, null, "", buffer, buffer.GetUpperBound(0), _IniFileName);

            int iCnt, iPos;
            ArrayList arrayList = new ArrayList();
            string tmp;
            if (rel > 0)
            {
                iCnt = 0; iPos = 0;
                for (iCnt = 0; iCnt < rel; iCnt++)
                {
                    if (buffer[iCnt] == 0x00)
                    {
                        tmp = System.Text.ASCIIEncoding.Default.GetString(buffer, iPos, iCnt - iPos).Trim();
                        iPos = iCnt + 1;
                        if (tmp != "")
                            arrayList.Add(tmp);
                    }
                }
            }
            return arrayList;
        }

        #region ��ȡini�ļ���ĳ��key��ֵ
        /// <summary>
        /// ��ȡini�ļ���ĳ��key��ֵ
        /// </summary>
        /// <param name="sectionName">�ڵ���</param>
        /// <param name="keyName">key����</param>
        /// <returns></returns>
        public string GetKeyValue(string sectionName, string keyName)
        {
            StringBuilder KeyValue = new StringBuilder(128);
            GetPrivateProfileString(sectionName, keyName, "", KeyValue, 128, _IniFileName);
            string strKeyValue = KeyValue.ToString();

            return strKeyValue;
        }
        #endregion

        private void frmDataExchangeMain_Load(object sender, EventArgs e)
        {
            #region ��ȡ��Ҫ�ϴ��ı���б�
            ArrayList lisTable = ReadKeys("IsUpdateTable");
            if (lisTable.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("UpdataTableName");
                dt.Columns.Add("isAutoUpdata");


                for (int i = 0; i < lisTable.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["UpdataTableName"] = lisTable[i].ToString();
                    if (GetKeyValue("IsUpdateTable", lisTable[i].ToString()) == "1")
                    {
                        dr["isAutoUpdata"] = true;
                    }
                    else
                    {
                        dr["isAutoUpdata"] = false;
                    }
                    dt.Rows.Add(dr);
                }
                dataGridViewUpdata.DataSource = dt;
            }
            #endregion

            objController = new clsCtl_DataExchangeMain();
            objController.m_objViewer = this;
           
             StringBuilder isAutorun = new StringBuilder(128);//�����Ƿ��Զ�ִ�еĲ���
            GetPrivateProfileString("doInitialization", "isInitialization", "", isAutorun, 128, _IniFileName);
            if (isAutorun.ToString() == "2")
            { 
                frmAsk frmask=new frmAsk();
               
                if (frmask.ShowDialog() == DialogResult.OK)
                {
                    this.dtmBegin.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    this.dtmEnd.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    this.Show();
                    butUpload_Click(sender, e);
                    if (!string.IsNullOrEmpty(rtb_showLog.Text))
                    {
                        string strFile = DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                        this.rtb_showLog.SaveFile(strFile, RichTextBoxStreamType.UnicodePlainText);
                    }
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    this.dtmBegin.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    this.dtmEnd.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    this.Show();
                }
            }
            else
            {
                this.dtmBegin.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                this.dtmEnd.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                this.Show();
            }

        }

        #endregion

        private void dataGridViewUpdata_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if ((bool)dataGridViewUpdata.Rows[e.RowIndex].Cells[1].EditedFormattedValue == true)
            {
                dataGridViewUpdata.Rows[e.RowIndex].Cells[1].Value = false;
            }
            else
            {
                dataGridViewUpdata.Rows[e.RowIndex].Cells[1].Value = true;
            }
        }

        private void tmiCleanLog_Click(object sender, EventArgs e)//���log
        {
            this.rtb_showLog.Clear();
        }

        private void tmiSaveLog_Click(object sender, EventArgs e)//���Ϊ
        {
            
            sfdForSaveLog.DefaultExt = ".txt";
            sfdForSaveLog.AddExtension = true;
            sfdForSaveLog.Filter = "�ı��ļ�(*.txt)|*.txt";
            sfdForSaveLog.FileName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt";
            try
            {
                if (sfdForSaveLog.ShowDialog() == DialogResult.OK)
                {
                    this.rtb_showLog.SaveFile(sfdForSaveLog.FileName, RichTextBoxStreamType.UnicodePlainText);
                    MessageBox.Show("����ɹ�!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rtb_showLog_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.rtb_showLog.Text.Trim()))
            {
                tmiSaveLog.Enabled = false;
            }
            else
            {
                tmiSaveLog.Enabled = true;
            }
        }

      
    }
}