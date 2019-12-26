using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace com.digitalwave.iCare.gui.MFZ
{

    /// <summary>
    /// 全局配置
    /// </summary>
    public class clsConfig
    {
        private　const string strFile = "LoginFile.xml";
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
                strMsg = "未找到配置文件.";
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
       
        #region 私有成员

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
        /// 开启叫号消息队列模式 0-关闭 1-打开
        /// </summary>
        internal bool m_blnEnableCall = false;

        #endregion

        #region 诊区配置

        /// <summary>
        /// 诊区Id
        /// </summary>
        public int AreaID
        {
            get
            {
                return areaID;
            }
        } 

        #endregion

        #region 患者等候队列样式

        /// <summary>
        /// 获取患者等候队列显示的行数
        /// </summary>
        public int PatientQueueLineNum
        {
            get { return patientQueueLineNum; }
        }

        /// <summary>
        /// 获取患者等候队列显示的列数
        /// </summary>
        public int PatientQueueColumnNum
        {
            get { return patientQueueColumnNum; }
        }

        /// <summary>
        /// 获取患者等候队列的字体名称
        /// </summary>
        public string PatientQueueFont
        {
            get { return patientQueueFont; }
        }

        /// <summary>
        /// 获取患者等候队列的字体大小
        /// </summary>
        public int PatientQueueFontSize
        {
            get { return patientQueueFontSize; }
        } 

        #endregion

        #region 医生排班列表配置项

        public string ArrangeTitle 
        {
            get 
            {
                return this.arrangeTitle;
            }
        }

        /// <summary>
        /// 医生安排显示的列数
        /// </summary>
        public int ArrangeColumnNum
        {
            get { return arrangeColumnNum; }
            set { arrangeColumnNum = value; }
        }

        /// <summary>
        /// 医生安排表的字体
        /// </summary>
        public string ArrageFont
        {
            get
            {
                return arrangeFont;
            }
        }

        /// <summary>
        /// 医生安排表的字体大小
        /// </summary>
        public int ArrangeFontSize
        {
            get
            {
                return arrangeFontSize;
            }
        }

        /// <summary>
        /// 医生安排表的行数
        /// </summary>
        public int ArrangeLineNum
        {
            get
            {
                return arrangeLineNum;
            }
        } 

        #endregion

        #region 叫患者就诊的语音,屏幕显示样式

        /// <summary>
        /// 叫病人的显示样式
        /// </summary>
        public string CallPatientStyle
        {
            get
            {
                return callPatientStyle;
            }
        }

        /// <summary>
        /// 准备病人的显示样式
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
