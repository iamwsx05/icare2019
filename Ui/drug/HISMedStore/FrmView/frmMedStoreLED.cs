using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Printing;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HI;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace com.digitalwave.iCare.gui.HIS
{����
    
    /// <summary>
    /// ҩ��ͬ����
    /// </summary>
    public partial class frmMedStoreLED : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {��
        /// <summary>
        /// ���캯��
        /// </summary>
        public frmMedStoreLED()
        {
            InitializeComponent();
            m_objConfigVo = new clsLedConfigVo();
        }
        private string XMLFile = System.Windows.Forms.Application.StartupPath + "\\" + "LoginFile.xml";
        /// <summary>
        /// ҩ��id
        /// </summary>
        public string MedStoreID = string.Empty;
        /// <summary>
        /// ��ҩ����id
        /// </summary>
        public List<clsWindowsInfo> m_objWindowIDList = new List<clsWindowsInfo>();
        /// <summary>
        /// ��ҩ����id
        /// </summary>
        public List<clsWindowsInfo> m_objSendWinIDList = new List<clsWindowsInfo>();
        /// <summary>
        /// �Զ�����ʾ����
        /// </summary>
        /// <param name="m_strMedStoreID"></param>
        /// <param name="m_strWindowID"></param>
        /// <param name="m_strSendWinID"></param>
        public void m_mthShow(string m_strMedStoreID, string m_strWindowID,string m_strSendWinID)
        {
            MedStoreID = m_strMedStoreID.Trim();
            clsWindowsInfo m_objWindowsInfo;
            string[] m_strWindowIDArr = m_strWindowID.Split('*');
            string[] m_strSendWindowIDArr = m_strSendWinID.Split('*');
            for (int i = 0; i < m_strWindowIDArr.Length; i++)
            {
                m_objWindowsInfo = new clsWindowsInfo();
                m_objWindowsInfo.m_strWindowID = m_strWindowIDArr[i].Trim();
                m_objWindowIDList.Add(m_objWindowsInfo);
            }
            for (int i = 0; i < m_strSendWindowIDArr.Length; i++)
            {
                m_objWindowsInfo = new clsWindowsInfo();
                m_objWindowsInfo.m_strWindowID = m_strSendWindowIDArr[i].Trim();
                m_objSendWinIDList.Add(m_objWindowsInfo);
            }
            this.Show();
        }
        /// <summary>
        /// ���ô������
        /// </summary>
        public override void CreateController()
        {
            this.objController = new clscontrollMedStoreLED();
            this.objController.Set_GUI_Apperance(this);
        }

        private void frmMedStoreLED_Load(object sender, EventArgs e)
        {
            this.m_mthReadXml();
            ((clscontrollMedStoreLED)this.objController).m_mthGetWindowInfoTable();
            this.m_mthInitial();
        }
        private void m_mthInitial()
        {
            this.WindowState = FormWindowState.Normal;
            this.Width = this.m_objConfigVo.m_intWidth;
            this.Height = this.m_objConfigVo.m_intHeight;
            this.SetDesktopLocation(this.m_objConfigVo.m_intPosX, this.m_objConfigVo.m_intPosY);
            ((clscontrollMedStoreLED)this.objController).m_mthSetDataSource();
        }
        /// <summary>
        /// ����������
        /// </summary>
        public clsLedConfigVo m_objConfigVo;
        /// <summary>
        /// ��ȡ��������������
        /// </summary>
        public void m_mthReadXml()
        {
            try
            {
                if (File.Exists(XMLFile))
                {
                    XmlDocument m_objXmlDoc = new XmlDocument();
                    m_objXmlDoc.Load(XMLFile);
                    XmlNode m_objXmlParentNode = m_objXmlDoc.DocumentElement.SelectNodes("DGCS_MedicineStore")[0];
                    XmlNode LEDRefreshTime = m_objXmlParentNode.SelectSingleNode("LEDRefreshTime");
                    XmlNode LEDPosX = m_objXmlParentNode.SelectSingleNode("LEDPosX");
                    XmlNode LEDPosY = m_objXmlParentNode.SelectSingleNode("LEDPosY");
                    XmlNode LEDWidth = m_objXmlParentNode.SelectSingleNode("LEDWidth");
                    XmlNode LEDHeight = m_objXmlParentNode.SelectSingleNode("LEDHeight");
    

                    if (m_objXmlParentNode != null)
                    {
                        try
                        {
                            m_objConfigVo.m_intRefreshTime = int.Parse(LEDRefreshTime.Attributes["value"].Value.ToString().Trim());
                            m_objConfigVo.m_intPosX = int.Parse(LEDPosX.Attributes["value"].Value.ToString().Trim());
                            m_objConfigVo.m_intPosY = int.Parse(LEDPosY.Attributes["value"].Value.ToString().Trim());
                            m_objConfigVo.m_intHeight = int.Parse(LEDHeight.Attributes["value"].Value.ToString().Trim());
                            m_objConfigVo.m_intWidth = int.Parse(LEDWidth.Attributes["value"].Value.ToString().Trim());
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        /// <summary>
        /// �����Զ�������
        /// </summary>
        public void m_mthWriteXml()
        {
            try
            {
                if (File.Exists(XMLFile))
                {
                    XmlDocument m_objXmlDoc = new XmlDocument();
                    m_objXmlDoc.Load(XMLFile);
                    XmlNode m_objXmlParentNode = m_objXmlDoc.DocumentElement.SelectNodes("DGCS_MedicineStore")[0];
                    XmlNode LEDPosX = m_objXmlParentNode.SelectSingleNode("LEDPosX");
                    XmlNode LEDPosY = m_objXmlParentNode.SelectSingleNode("LEDPosY");
                    XmlNode LEDWidth = m_objXmlParentNode.SelectSingleNode("LEDWidth");
                    XmlNode LEDHeight = m_objXmlParentNode.SelectSingleNode("LEDHeight");


                    if (m_objXmlParentNode != null)
                    {
                        try
                        {
                            LEDPosX.Attributes["value"].Value = this.DesktopLocation.X.ToString();
                            LEDPosY.Attributes["value"].Value = this.DesktopLocation.Y.ToString();
                            LEDHeight.Attributes["value"].Value = this.Height.ToString();
                            LEDWidth.Attributes["value"].Value = this.Width.ToString();
                            m_objXmlDoc.Save(XMLFile);
                        }

                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            ((clscontrollMedStoreLED)this.objController).m_mthSetDataSource();
        }

        private void frmMedStoreLED_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult m_objDR = MessageBox.Show(this, "�Ƿ񱣴浱ǰ�������Ĵ�С�͸߶����ã�", "ϵͳ��ʾ��Ϣ��", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            if (m_objDR == DialogResult.OK)
            {
                try
                {
                    this.m_mthWriteXml();
                }
                catch
                {
                    MessageBox.Show(this, "�鿴loginfile.xml�ļ��Ƿ�Ϊֻ����", "ϵͳ��ʾ��Ϣ��", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.dataGridView1.Rows[5].Selected = true;
          
        }

    }
    /// <summary>
    /// ҩ������������������
    /// </summary>
    public class clsLedConfigVo
    {   
        /// <summary>
        ///������ˢ��ʱ��
        /// </summary>
        public int m_intRefreshTime = 4;
        /// <summary>
        /// ������ˮƽλ��
        /// </summary>
        public int m_intPosY = 0;
        /// <summary>
        /// ��������ֱλ��
        /// </summary>
        public int m_intPosX = 0;
        /// <summary>
        /// �������߶�
        /// </summary>
        public int m_intHeight = 0;
        /// <summary>
        /// ���������
        /// </summary>
        public int m_intWidth = 0;
    }
}