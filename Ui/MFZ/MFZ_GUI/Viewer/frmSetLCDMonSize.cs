using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace com.digitalwave.iCare.gui.MFZ
{
    /// <summary>
    /// 自定义发送显示屏的尺寸
    /// kenny in 2008.08.12
    /// </summary>
    public partial class frmSetLCDMonSize : Form
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmSetLCDMonSize()
        {
            InitializeComponent();
        }
        #endregion

        #region 变量

        /// <summary>
        /// XML路径
        /// </summary>
        private string m_strXMLFile = "LoginFile.xml";
        /// <summary>
        /// 已定义方案数组
        /// </summary>
        List<string[]> lstNodes;
        /// <summary>
        /// 当前最大可用key值
        /// </summary>
        int n = 1;
        /// <summary>
        /// 默认方案在数组中的索引值
        /// </summary>
        int m_intFirst = -1;
        /// <summary>
        /// 是否新增标识
        /// </summary>
        bool m_blnAdd = false;
        /// <summary>
        /// 发送事件
        /// </summary>
        public SendNewSizeEvtHandle sendNewSize = null;
        /// <summary>
        /// 发送宽度值
        /// </summary>
        public int m_intSendWidthValue = 268;
        /// <summary>
        /// 发送高度值
        /// </summary>
        public int m_intSendHeightValue = 273;

        #endregion

        #region 事件

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Control objCtl in this.grouper1.Controls)
            {
                if (objCtl.AccessibleDescription == "13")
                {
                    objCtl.KeyPress += new KeyPressEventHandler(EditControls_KeyPress);
                }
            }
            this.m_mthReWriteXML();
            this.m_mthReadXML();
            this.m_mthInitComboBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(m_strXMLFile);
            int selectindex = 0;
            object objLock = new object();
            lock (objLock)
            {
                if (!m_blnCheckInput())
                    return;

                if (this.btnEdit.Text == "保存")
                {
                    XmlNode xn = xmlDoc["Main"].SelectSingleNode("MFZ_GUI");

                    XmlElement xe = xmlDoc.CreateElement("LCDRoomWidth");
                    xe.SetAttribute("key", n.ToString());
                    xe.SetAttribute("name", this.textBox1.Text.Trim());
                    xe.SetAttribute("value", textBox2.Text.Trim());
                    xn.AppendChild(xe);

                    XmlElement xe2 = xmlDoc.CreateElement("LCDRoomHeight");
                    xe2.SetAttribute("key", n.ToString());
                    xe2.SetAttribute("name", this.textBox1.Text.Trim());
                    xe2.SetAttribute("value", textBox3.Text.Trim());
                    xn.AppendChild(xe2);
                    n++;

                    this.comboBox1.Items.Add(this.textBox1.Text.Trim());
                    selectindex = this.comboBox1.Items.Count - 1;
                }
                else
                {
                    for (int i = 0; i < lstNodes.Count; i++)
                    {
                        if (lstNodes[i][1].Trim() == this.comboBox1.Text.Trim())
                        {
                            XmlNode xe = xmlDoc["Main"]["MFZ_GUI"].SelectSingleNode("LCDRoomWidth[@key=\"" + lstNodes[i][0] + "\"][@name=\"" + lstNodes[i][1] + "\"]");
                            xe.Attributes["value"].Value = this.textBox2.Text.Trim();
                            xe.Attributes["name"].Value = this.textBox1.Text.Trim();
                            XmlNode xe2 = xmlDoc["Main"]["MFZ_GUI"].SelectSingleNode("LCDRoomHeight[@key=\"" + lstNodes[i][0] + "\"][@name=\"" + lstNodes[i][1] + "\"]");
                            xe2.Attributes["value"].Value = this.textBox3.Text.Trim();
                            xe2.Attributes["name"].Value = this.textBox1.Text.Trim();
                            break;
                        }
                    }
                    selectindex = this.comboBox1.SelectedIndex;
                }

                xmlDoc.Save(m_strXMLFile);
                MessageBox.Show("保存成功!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_mthReadXML(); //刷新数组
                m_mthInitComboBox();
                this.comboBox1.SelectedIndex = selectindex;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (m_blnAdd)
            {
                this.comboBox1.Text = this.textBox1.Text;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            m_mthClear();
            this.textBox1.Focus();
            m_blnAdd = true;
            this.btnEdit.Text = "保存";
        }

        private void EditControls_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex == -1 || this.comboBox1.Text == string.Empty)
            {
                MessageBox.Show("请先保存方案!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.btnEdit.Focus();
                return;
            }

            this.m_intSendHeightValue = int.Parse(this.textBox3.Text);
            this.m_intSendWidthValue = int.Parse(this.textBox2.Text);            
            if (sendNewSize != null)
            {
                sendNewSize(sender, e);
            }

            this.m_mthSetDefault(); //把当前发送项目设为默认
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                this.textBox1.Text = lstNodes[this.comboBox1.SelectedIndex][1];
                this.textBox2.Text = lstNodes[this.comboBox1.SelectedIndex][2];
                this.textBox3.Text = lstNodes[this.comboBox1.SelectedIndex][3];
                m_blnAdd = false;
                this.btnEdit.Text = "修改";
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(m_strXMLFile);
            object objLock = new object();
            lock (objLock)
            {
                XmlNode xn = xmlDoc["Main"].SelectSingleNode("MFZ_GUI");
                for (int i = 0; i < lstNodes.Count; i++)
                {
                    if (lstNodes[i][1].Trim() == this.comboBox1.Text.Trim())
                    {
                        XmlNode xe = xmlDoc["Main"]["MFZ_GUI"].SelectSingleNode("LCDRoomWidth[@key=\"" + lstNodes[i][0] + "\"][@name=\"" + lstNodes[i][1] + "\"]");
                        XmlNode xe2 = xmlDoc["Main"]["MFZ_GUI"].SelectSingleNode("LCDRoomHeight[@key=\"" + lstNodes[i][0] + "\"][@name=\"" + lstNodes[i][1] + "\"]");
                        xn.RemoveChild(xe);
                        xn.RemoveChild(xe2);
                        break;
                    }
                }

                xmlDoc.Save(m_strXMLFile);
                m_mthReadXML(); //刷新数组
                m_mthInitComboBox();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 13)
            {
                this.m_mthShowWarning((TextBox)sender, "输入的只能是数字!");
                e.Handled = true;
            }
        }

        #endregion

        #region 方法

        #region 重新读取XML设定数组
        /// <summary>
        /// 重新读取XML设定数组
        /// </summary>
        private void m_mthReadXML()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(m_strXMLFile);

            lstNodes = new List<string[]>();

            string[] sarr = null;
            string strKey = string.Empty;
            string strName = string.Empty;
            string strWidthValue = string.Empty;
            string strHeightValue = string.Empty;

            try
            {
                XmlNode xn = xmlDoc["Main"].SelectSingleNode("MFZ_GUI");
                foreach (XmlNode xe in xn.ChildNodes)
                {
                    sarr = new string[4];
                    if (xe.NodeType == XmlNodeType.Element && xe.Name == "LCDRoomWidth")
                    {
                        strKey = xe.Attributes["key"].Value;
                        strName = xe.Attributes["name"].Value;
                        strWidthValue = xn.SelectSingleNode("LCDRoomWidth[@key=\"" + strKey + "\"][@name=\"" + strName + "\"]").Attributes["value"].Value;
                        strHeightValue = xn.SelectSingleNode("LCDRoomHeight[@key=\"" + strKey + "\"][@name=\"" + strName + "\"]").Attributes["value"].Value;
                        sarr[0] = strKey;
                        sarr[1] = strName;
                        sarr[2] = strWidthValue;
                        sarr[3] = strHeightValue;
                        lstNodes.Add(sarr);

                        if (strKey == "default")
                        {
                            m_intFirst = lstNodes.Count - 1; // 默认方案索引号
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                MessageBox.Show(objEx.Message, "系统提示");
            }
        }
        #endregion

        /// <summary>
        /// 重写XML文件
        /// 功能:把节点按顺序赋key值
        /// </summary>
        private void m_mthReWriteXML()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(m_strXMLFile);

            try
            {
                XmlNode xn = xmlDoc["Main"].SelectSingleNode("MFZ_GUI");
                XmlNode cn = null;
                XmlNode cnNext = null;

                n = 1;
                for (int i1 = 0; i1 < xn.ChildNodes.Count; i1++)
                {
                    cn = xn.ChildNodes[i1];
                    if (cn.NodeType == XmlNodeType.Element && cn.Name == "LCDRoomWidth")
                    {
                        if (cn.Attributes["key"].Value != "default")
                        {
                            cn.Attributes["key"].Value = n.ToString();
                            cnNext = xn.ChildNodes[i1 + 1];
                            cnNext.Attributes["key"].Value = n.ToString();
                            n++;
                        }
                    }
                }
                xmlDoc.Save(m_strXMLFile);
            }
            catch (Exception objEx)
            {
                MessageBox.Show(objEx.Message, "系统提示");
            }
        }

        private void m_mthSetDefault()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(m_strXMLFile);
            XmlNode xnold_h = null;
            XmlNode xnold_w = null;
            XmlNode xnnew_h = null;
            XmlNode xnnew_w = null;

            for (int i1 = 0; i1 < lstNodes.Count; i1++)
            {
                if (lstNodes[i1][1].Trim() == this.comboBox1.Text.Trim())
                {
                    // 获取当前发送节点(将设为默认)
                    xnnew_h = xmlDoc["Main"]["MFZ_GUI"].SelectSingleNode("LCDRoomHeight[@key=\"" + lstNodes[i1][0] + "\"][@name=\"" + lstNodes[i1][1] + "\"]");
                    xnnew_w = xmlDoc["Main"]["MFZ_GUI"].SelectSingleNode("LCDRoomWidth[@key=\"" + lstNodes[i1][0] + "\"][@name=\"" + lstNodes[i1][1] + "\"]");
                }

                if (lstNodes[i1][0].Trim() == "default")
                {
                    // 获取原默认节点(将取消默认)
                    xnold_h = xmlDoc["Main"]["MFZ_GUI"].SelectSingleNode("LCDRoomHeight[@key=\"" + lstNodes[i1][0] + "\"][@name=\"" + lstNodes[i1][1] + "\"]");
                    xnold_w = xmlDoc["Main"]["MFZ_GUI"].SelectSingleNode("LCDRoomWidth[@key=\"" + lstNodes[i1][0] + "\"][@name=\"" + lstNodes[i1][1] + "\"]");
                }
            }

            if (xnnew_h.Equals(xnold_h))
            {
                return;
            }
            else
            {
                if (xnold_h != null && xnold_w != null)
                {
                    xnold_h.Attributes["key"].Value = xnnew_h.Attributes["key"].Value;
                    xnold_w.Attributes["key"].Value = xnnew_w.Attributes["key"].Value;
                }
                xnnew_h.Attributes["key"].Value = "default";
                xnnew_w.Attributes["key"].Value = "default";
                xmlDoc.Save(m_strXMLFile);
                m_mthReadXML(); //刷新数组
                m_mthInitComboBox();
            }
        }

        /// <summary>
        /// 初始化方案下拉框
        /// </summary>
        private void m_mthInitComboBox()
        {
            this.comboBox1.Items.Clear();
            for (int i2 = 0; i2 < lstNodes.Count; i2++)
            {
                this.comboBox1.Items.Add(lstNodes[i2][1]);
            }
            this.comboBox1.SelectedIndex = (m_intFirst < lstNodes.Count ? m_intFirst : -1);
        }

        /// <summary>
        /// 清空
        /// </summary>
        private void m_mthClear()
        {
            this.comboBox1.SelectedIndex = -1;
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox3.Text = "";
        }

        public void m_mthShowWarning(TextBox txtBox, string strWaring)
        {
            com.digitalwave.controls.Control.frmShowWarning ShowWarning = new com.digitalwave.controls.Control.frmShowWarning();
            ShowWarning.TopMost = false;
            ShowWarning.m_GetWaring = strWaring;
            Point p = txtBox.Parent.PointToScreen(txtBox.Location);
            p.Offset(-80, -ShowWarning.Height);
            ShowWarning.Location = p;
            m_mthShowForm(ShowWarning);
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public static void m_mthShowForm(Form form)
        {
            SetWindowPos(form.Handle, new IntPtr(-1), 0, 0, 0, 0, 0x0001 | 0x0002 | 0x0040 | 0x0010);
        }

        /// <summary>
        /// 校验输入
        /// </summary>
        /// <returns></returns>
        private bool m_blnCheckInput()
        {
            bool blnPass = true;
            if (this.textBox1.Text.Trim() == "")
            {
                m_mthShowWarning(this.textBox1, "方案名称不能为空");
                this.textBox1.Focus();
                return false;
            }

            if (this.textBox2.Text.Trim() == "")
            {
                m_mthShowWarning(this.textBox2, "宽度值不能为空");
                this.textBox2.Focus();
                return false;
            }

            if (this.textBox3.Text.Trim() == "")
            {
                m_mthShowWarning(this.textBox3, "高度值不能为空");
                this.textBox3.Focus();
                return false;
            }
            return blnPass;
        }

        #endregion

        public delegate void SendNewSizeEvtHandle(object sender, EventArgs e);
    }
}