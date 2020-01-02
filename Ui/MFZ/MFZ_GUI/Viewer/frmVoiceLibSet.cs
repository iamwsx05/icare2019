using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SpeechLib;

namespace com.digitalwave.iCare.gui.MFZ
{
    public partial class frmVoiceLibSet : Form
    {
        public frmVoiceLibSet()
        {
            InitializeComponent();
        }

        SpVoice voice = new SpVoice();
        System.Xml.XmlDocument xmlConfig = new System.Xml.XmlDocument();

        private void frmVoiceLibSet_Load(object sender, EventArgs e)
        {
            ISpeechObjectTokens obj = voice.GetVoices("", "");
            for (int i = 0; i < obj.Count; i++)
            {
                string desc = obj.Item(i).GetDescription(0);
                this.comboBox1.Items.Add(desc);
                this.comboBox2.Items.Add(desc);
            }

            if (!System.IO.File.Exists("LoginFile.xml"))
            {
                MessageBox.Show("未找到配置文件。", "提示");
            }
            string strLibPTH = string.Empty;
            string strLibYy = string.Empty;
            xmlConfig.Load("LoginFile.xml");
            try
            {
                strLibPTH = xmlConfig["Main"]["MFZ_GUI"]["VoiceLibPTH"].Attributes["value"].Value;
                strLibYy = xmlConfig["Main"]["MFZ_GUI"]["VoiceLibYy"].Attributes["value"].Value;
            }
            catch { }
            if (this.comboBox1.Items.Contains(strLibPTH))
            {
                this.comboBox1.SelectedItem = strLibPTH;
            }
            else
            {
                this.comboBox1.SelectedIndex = 0;
            }

            if (this.comboBox2.Items.Contains(strLibYy))
            {
                this.comboBox2.SelectedItem = strLibYy;
            }
            else
            {
                this.comboBox2.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            xmlConfig.Load("LoginFile.xml");
            xmlConfig["Main"]["MFZ_GUI"]["VoiceLibPTH"].Attributes["value"].Value = this.comboBox1.SelectedItem.ToString();
            xmlConfig["Main"]["MFZ_GUI"]["VoiceLibYy"].Attributes["value"].Value = this.comboBox2.SelectedItem.ToString();
            xmlConfig.Save("LoginFile.xml");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}