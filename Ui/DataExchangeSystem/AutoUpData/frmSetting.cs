using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace AutoUpData
{
    public partial class frmSetting : Form
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

        /// <summary>
        /// Ini�ļ���ַ
        /// </summary>
        private string _IniFileName;

        public frmSetting()
        {
            InitializeComponent();
            _IniFileName = Application.StartupPath + "\\DataExchangeSetting.ini";
        }

        private void frmSetting_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        /// <summary>
        /// ��ʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerToRun_Tick(object sender, EventArgs e)
        {
            TimeSpan t;
            if (dateTimeAutoUpdata.Value.TimeOfDay < DateTime.Now.TimeOfDay)
            {
                t = dateTimeAutoUpdata.Value.TimeOfDay - DateTime.Now.TimeOfDay + TimeSpan.Parse("23:59:59");
            }
            else
            {
                t = dateTimeAutoUpdata.Value.TimeOfDay - DateTime.Now.TimeOfDay;
            }

            tlsNextTime.Text = "�����´�ִ�л���:" + t.Hours.ToString() + "ʱ" + t.Minutes.ToString() + "��" + t.Seconds.ToString() + "��";

            if (t.Hours.ToString() == "0" && t.Minutes.ToString() == "0" && t.Seconds.ToString() == "0")
            {
                tlsLastExcutTime.Text = "�ϴ�ִ��ʱ��:" + DateTime.Now.ToString();
                Process.Start(Application.StartupPath + "\\DataExchangeSystem.exe");
                dateTimeAutoUpdata.Value = dateTimeAutoUpdata.Value.AddDays(1);
            }
        }


        /// <summary>
        /// ���ز�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSetting_Load(object sender, EventArgs e)
        {
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
        }

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

        private void dataGridViewUpdata_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((bool)dataGridViewUpdata.Rows[e.RowIndex].Cells[1].EditedFormattedValue == true)
            {
                dataGridViewUpdata.Rows[e.RowIndex].Cells[1].Value = false;
            }
            else
            {
                dataGridViewUpdata.Rows[e.RowIndex].Cells[1].Value = true;
            }

        }

        private void butStartExe_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewUpdata.Rows.Count; i++)
            {
                if ((bool)dataGridViewUpdata.Rows[i].Cells[1].EditedFormattedValue == true)
                {
                    WritePrivateProfileString("IsUpdateTable", dataGridViewUpdata.Rows[i].Cells[0].Value.ToString(), "1", _IniFileName);
                }
                else
                {
                    WritePrivateProfileString("IsUpdateTable", dataGridViewUpdata.Rows[i].Cells[0].Value.ToString(), "0", _IniFileName);
                }
            }
            MessageBox.Show("��������ɹ�!", "��ʾ");
            this.butStartExe.Enabled = false;
            dateTimeAutoUpdata.Enabled = false;
            this.dataGridViewUpdata.Enabled = false;
            timerToRun.Enabled = true;
            this.btmWate.Enabled = true;

        }

        private void btmWate_Click(object sender, EventArgs e)
        {
            if (btmWate.Text == "�� ͣ")
            {
                btmWate.Text = "�� ��";
                timerToRun.Enabled = false;
                this.butStartExe.Enabled = true;
                dateTimeAutoUpdata.Enabled = true;
                this.dataGridViewUpdata.Enabled = true;
            }
            else
            {
                btmWate.Text = "�� ͣ";
                timerToRun.Enabled = true;
                this.butStartExe.Enabled = false;
                dateTimeAutoUpdata.Enabled = false;
                this.dataGridViewUpdata.Enabled = false;
            }
        }

    }
}