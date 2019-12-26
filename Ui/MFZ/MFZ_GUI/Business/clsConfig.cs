using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace com.digitalwave.iCare.gui.MFZ
{

    /// <summary>
    /// ȫ������
    /// </summary>
    public class clsConfig
    {
        private��const string strFile = "LoginFile.xml";
        private static clsConfig currentConfig = new clsConfig();
        public static clsConfig CurrentConfig
        {
            get
            {
                return currentConfig;
            }
        }
        public string strVoiceLibPTH = "";
        public string strVoiceLibYy = "";
 
        public bool Load(out string strMsg)
        {
            strMsg = string.Empty;

            areaID = int.MinValue;
            arrangeFont = string.Empty;
            arrangeFontSize = int.MinValue;
            arrangeLineNum = int.MinValue;
            callPatientStyle = string.Empty;
            preparePatientStyle = string.Empty;

            if (!System.IO.File.Exists(strFile))
            {
                strMsg = "δ�ҵ������ļ�.";
                return false;
            }
            System.Xml.XmlDocument xmlConfig = new System.Xml.XmlDocument();
            try
            {
                xmlConfig.Load(strFile);

                areaID = int.Parse(xmlConfig["Main"]["MFZ_GUI"]["Area"].Attributes["ID"].Value);

                arrangeTitle = xmlConfig["Main"]["MFZ_GUI"]["DoctArrange"].Attributes["Tilte"].Value;
                arrangeFont = xmlConfig["Main"]["MFZ_GUI"]["DoctArrange"].Attributes["Font"].Value;
                arrangeFontSize = int.Parse(xmlConfig["Main"]["MFZ_GUI"]["DoctArrange"].Attributes["FontSize"].Value);
                arrangeLineNum = int.Parse(xmlConfig["Main"]["MFZ_GUI"]["DoctArrange"].Attributes["LineNum"].Value);
                arrangeColumnNum = int.Parse(xmlConfig["Main"]["MFZ_GUI"]["DoctArrange"].Attributes["ColumnNum"].Value);

                patientQueueFont = xmlConfig["Main"]["MFZ_GUI"]["PatientQueue"].Attributes["Font"].Value;
                patientQueueFontSize = int.Parse(xmlConfig["Main"]["MFZ_GUI"]["PatientQueue"].Attributes["FontSize"].Value);
                patientQueueLineNum = int.Parse(xmlConfig["Main"]["MFZ_GUI"]["PatientQueue"].Attributes["LineNum"].Value);
                patientQueueColumnNum = int.Parse(xmlConfig["Main"]["MFZ_GUI"]["PatientQueue"].Attributes["ColumnNum"].Value);

                callPatientStyle = xmlConfig["Main"]["MFZ_GUI"]["LedContentStyle"].Attributes["CallPatient"].Value;
                preparePatientStyle = xmlConfig["Main"]["MFZ_GUI"]["LedContentStyle"].Attributes["PreparePatient"].Value;
                m_blnEnableCall = (xmlConfig["Main"]["register"]["EnableCallMSMQ"].Attributes["value"].Value == "1");
            }
            catch (Exception ex)
            {
                strMsg = ex.Message;
                return false;
            }
            return true;
        }

        public void m_mthLoadVoiceLib()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(strFile);
            XmlNode node = xmlDoc.SelectSingleNode("root/Main/MFZ_GUI/VoiceLibPTH");
            if (node == null)
            {
                XmlElement xe = xmlDoc["Main"]["MFZ_GUI"];
                XmlElement xePTH = xmlDoc.CreateElement("VoiceLibPTH");
                XmlElement xeYy = xmlDoc.CreateElement("VoiceLibYy");
                XmlAttribute xaValue1 = xmlDoc.CreateAttribute("value");
                XmlAttribute xaValue2 = xmlDoc.CreateAttribute("value");
                xaValue1.Value = "";
                xaValue2.Value = "";
                xePTH.Attributes.Append(xaValue1);
                xeYy.Attributes.Append(xaValue2);
                xe.AppendChild(xePTH);
                xe.AppendChild(xeYy);
                xmlDoc.Save(strFile);
            }
            xmlDoc.Load(strFile);
            strVoiceLibPTH = xmlDoc["Main"]["MFZ_GUI"]["VoiceLibPTH"].Attributes["value"].Value;
            strVoiceLibYy = xmlDoc["Main"]["MFZ_GUI"]["VoiceLibYy"].Attributes["value"].Value;
        }
       
        #region ˽�г�Ա

        private int areaID;

        private string arrangeTitle;
        private string arrangeFont;
        private int arrangeFontSize;
        private int arrangeLineNum;
        private int arrangeColumnNum;

        private string callPatientStyle;
        private string preparePatientStyle;
        private int patientQueueLineNum;
        private int patientQueueColumnNum;
        private string patientQueueFont;
        private int patientQueueFontSize;
        /// <summary>
        /// �����к���Ϣ����ģʽ 0-�ر� 1-��
        /// </summary>
        internal bool m_blnEnableCall = false;

        #endregion

        #region ��������

        /// <summary>
        /// ����Id
        /// </summary>
        public int AreaID
        {
            get
            {
                return areaID;
            }
        } 

        #endregion

        #region ���ߵȺ������ʽ

        /// <summary>
        /// ��ȡ���ߵȺ������ʾ������
        /// </summary>
        public int PatientQueueLineNum
        {
            get { return patientQueueLineNum; }
        }

        /// <summary>
        /// ��ȡ���ߵȺ������ʾ������
        /// </summary>
        public int PatientQueueColumnNum
        {
            get { return patientQueueColumnNum; }
        }

        /// <summary>
        /// ��ȡ���ߵȺ���е���������
        /// </summary>
        public string PatientQueueFont
        {
            get { return patientQueueFont; }
        }

        /// <summary>
        /// ��ȡ���ߵȺ���е������С
        /// </summary>
        public int PatientQueueFontSize
        {
            get { return patientQueueFontSize; }
        } 

        #endregion

        #region ҽ���Ű��б�������

        public string ArrangeTitle 
        {
            get 
            {
                return this.arrangeTitle;
            }
        }

        /// <summary>
        /// ҽ��������ʾ������
        /// </summary>
        public int ArrangeColumnNum
        {
            get { return arrangeColumnNum; }
            set { arrangeColumnNum = value; }
        }

        /// <summary>
        /// ҽ�����ű������
        /// </summary>
        public string ArrageFont
        {
            get
            {
                return arrangeFont;
            }
        }

        /// <summary>
        /// ҽ�����ű�������С
        /// </summary>
        public int ArrangeFontSize
        {
            get
            {
                return arrangeFontSize;
            }
        }

        /// <summary>
        /// ҽ�����ű������
        /// </summary>
        public int ArrangeLineNum
        {
            get
            {
                return arrangeLineNum;
            }
        } 

        #endregion

        #region �л��߾��������,��Ļ��ʾ��ʽ

        /// <summary>
        /// �в��˵���ʾ��ʽ
        /// </summary>
        public string CallPatientStyle
        {
            get
            {
                return callPatientStyle;
            }
        }

        /// <summary>
        /// ׼�����˵���ʾ��ʽ
        /// </summary>
        public string PreparePatientStyle
        {
            get
            {
                return preparePatientStyle;
            }
        }
        
        #endregion
    }
}
