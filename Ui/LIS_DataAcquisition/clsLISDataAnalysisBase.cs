using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Xml;
using System.Xml.XPath;
using System.Windows.Forms;
using com.digitalwave.iCare.middletier.LIS;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.LIS
{
    /// <summary>
    /// 检验数据采集基类，为了兼容以前的接口实现
    /// </summary>
    internal class clsLISDataAnalysisBase
    {
        // Fields
        private Hashtable m_hasItemConvert = new Hashtable();
        private Hashtable m_hasPercentItemConvert = new Hashtable();
        private Hashtable m_hasPrivateConvert = new Hashtable();
        private Hashtable m_hasPublicConvert = new Hashtable();
        /// <summary>
        /// 串口通迅控制类
        /// </summary>
        clsSerialPortIO m_objSerialPort;
        /// <summary>
        /// 所在窗体
        /// </summary>
        Form _frmParent;
        /// <summary>
        /// 所在窗体
        /// </summary>
        public Form M_frmParent
        {
            get { return _frmParent; }
            set { _frmParent = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public clsLISDataAnalysisBase()
        {
            try
            {
                string filename = base.GetType().Assembly.Location.Replace("dll", "config");
                ConfigXmlDocument document = new ConfigXmlDocument();
                document.Load(filename);
                try
                {
                    foreach (XmlNode node in document["configuration"]["settings"]["convertSettings"]["public"].ChildNodes)
                    {
                        if ((node.Attributes != null) && (node.Attributes["key"].Value != null))
                        {
                            string key = node.Attributes["key"].Value.ToLower();
                            string str3 = node.Attributes["value"].Value;
                            this.m_hasPublicConvert.Add(key, str3);
                        }
                    }
                }
                catch
                {
                }
                try
                {
                    foreach (XmlNode node2 in document["configuration"]["settings"]["convertSettings"]["item"].ChildNodes)
                    {
                        if ((node2.Attributes != null) && (node2.Attributes["key"].Value != null))
                        {
                            string str4 = node2.Attributes["key"].Value.ToLower();
                            string str5 = node2.Attributes["value"].Value;
                            this.m_hasItemConvert.Add(str4, str5);
                        }
                    }
                }
                catch
                {
                }
                try
                {
                    foreach (XmlNode node3 in document["configuration"]["settings"]["convertSettings"]["private"].ChildNodes)
                    {
                        if (node3.HasChildNodes)
                        {
                            string str6 = node3.LocalName.ToLower();
                            Hashtable hashtable = new Hashtable();
                            foreach (XmlNode node4 in node3.ChildNodes)
                            {
                                if ((node4.Attributes != null) && (node4.Attributes["key"].Value != null))
                                {
                                    string str7 = node4.Attributes["key"].Value.ToLower();
                                    string str8 = node4.Attributes["value"].Value;
                                    hashtable.Add(str7, str8);
                                }
                            }
                            this.m_hasPrivateConvert.Add(str6, hashtable);
                        }
                    }
                }
                catch
                {
                }
                try
                {
                    foreach (XmlNode node5 in document["configuration"]["settings"]["convertSettings"]["percentItem"].ChildNodes)
                    {
                        if ((node5.Attributes != null) && (node5.Attributes["key"].Value != null))
                        {
                            string str9 = node5.Attributes["key"].Value.ToLower();
                            string str10 = " ";
                            this.m_hasPercentItemConvert.Add(str9, str10);
                        }
                    }
                }
                catch
                {
                }
            }
            catch (Exception)
            {
            }
        }


        protected virtual string m_strConvertItem(string p_strItem)
        {
            string key = null;
            if (p_strItem != null)
            {
                key = p_strItem.ToLower();
            }
            if ((key != null) && this.m_hasItemConvert.Contains(key))
            {
                return this.m_hasItemConvert[key].ToString();
            }
            return p_strItem;
        }

        protected virtual string m_strConvertPercentItem(string p_strItem, string p_strItemValue)
        {
            string key = null;
            if (p_strItem != null)
            {
                key = p_strItem.ToLower();
            }
            if ((key != null) && this.m_hasPercentItemConvert.Contains(key))
            {
                float num = 0f;
                try
                {
                    num = float.Parse(p_strItemValue);
                }
                catch
                {
                    return "Convert error";
                }
                num *= 100f;
                return num.ToString("0.0");
            }
            return p_strItemValue;
        }

        protected virtual string m_strConvertValue(string p_strItem, string p_strItemValue)
        {
            string key = null;
            string str2 = null;
            if (p_strItem != null)
            {
                key = p_strItem.ToLower();
            }
            if (p_strItemValue != null)
            {
                str2 = p_strItemValue.ToLower();
            }
            if ((key != null) && this.m_hasPrivateConvert.Contains(key))
            {
                Hashtable hashtable = (Hashtable)this.m_hasPrivateConvert[key];
                if ((str2 != null) && hashtable.Contains(str2))
                {
                    return hashtable[str2].ToString();
                }
            }
            else if ((str2 != null) && this.m_hasPublicConvert.Contains(str2))
            {
                return this.m_hasPublicConvert[str2].ToString();
            }
            return p_strItemValue;
        }

        public string m_strGetConfig(string p_strXPath)
        {
            try
            {
                XPathDocument document = new XPathDocument(base.GetType().Assembly.Location.Replace("dll", "config"));
                XPathNodeIterator iterator = document.CreateNavigator().Select(p_strXPath);
                if (iterator.MoveNext())
                {
                    return iterator.Current.Value;
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        protected virtual string[] m_strGetIntactData(ref string p_strRawData, string p_strStart, string p_strEnd)
        {
            int startIndex = 0;
            string[] strArray = null;
            if (((p_strRawData == null) || (p_strStart == null)) || (p_strEnd == null))
            {
                return null;
            }
            ArrayList list = new ArrayList();
            int index = p_strRawData.IndexOf(p_strStart);
            while (index >= 0)
            {
                int num3 = -1;
                if ((index + p_strStart.Length) < p_strRawData.Length)
                {
                    num3 = p_strRawData.IndexOf(p_strEnd, (int)(index + p_strStart.Length));
                }
                if (num3 >= 0)
                {
                    list.Add(p_strRawData.Substring(index, (num3 - index) + p_strEnd.Length));
                    startIndex = num3 + p_strEnd.Length;
                    if (startIndex < p_strRawData.Length)
                    {
                        index = p_strRawData.IndexOf(p_strStart, startIndex);
                    }
                    else
                    {
                        index = -1;
                    }
                }
                else
                {
                    index = -1;
                }
            }
            if (list.Count != 0)
            {
                strArray = (string[])list.ToArray(typeof(string));
            }
            p_strRawData = p_strRawData.Remove(0, startIndex);
            return strArray;
        }

        protected virtual string[] m_strGetIntactData(string p_strRawData, string p_strStart, string p_strEnd, out int p_intCutIdx)
        {
            p_intCutIdx = 0;
            string[] strArray = null;
            if (((p_strRawData == null) || (p_strStart == null)) || (p_strEnd == null))
            {
                return null;
            }
            ArrayList list = new ArrayList();
            int index = p_strRawData.IndexOf(p_strStart);
            while (index >= 0)
            {
                int num2 = -1;
                if ((index + p_strStart.Length) < p_strRawData.Length)
                {
                    num2 = p_strRawData.IndexOf(p_strEnd, (int)(index + p_strStart.Length));
                }
                if (num2 >= 0)
                {
                    list.Add(p_strRawData.Substring(index, (num2 - index) + p_strEnd.Length));
                    p_intCutIdx = num2 + p_strEnd.Length;
                    if (p_intCutIdx < p_strRawData.Length)
                    {
                        index = p_strRawData.IndexOf(p_strStart, p_intCutIdx);
                    }
                    else
                    {
                        index = -1;
                    }
                }
                else
                {
                    index = -1;
                }
            }
            if (list.Count != 0)
            {
                strArray = (string[])list.ToArray(typeof(string));
            }
            return strArray;
        }

        #region 更新检验编号，同时保存T_OPR_LIS_DEVICE_RELATION表 用于双工自动绑定检验编号 yongchao.li 2012-03-20
        /// <summary>
        /// 更新检验编号，同时保存T_OPR_LIS_DEVICE_RELATION表 用于双工自动绑定检验编号
        /// </summary>
        /// <param name="p_strBarCode"></param>
        /// <param name="p_strDeviceSampleID"></param>
        /// <returns></returns>
        //protected long m_lngUpdateAppCheckSampleNO(string p_strBarCode, string p_strDeviceSampleID)
        //{
        //    long lngRes = 0;
        //    if (string.IsNullOrEmpty(p_strBarCode))
        //        return lngRes;
        //    if (m_objDeviceConfigVO == null || string.IsNullOrEmpty(m_objDeviceConfigVO.strLIS_Instrument_ID))
        //    {
        //        return lngRes;
        //    }
        //    lngRes = m_objDomain.m_lngUpdateAppCheckSampleNO(p_strBarCode, m_objDeviceConfigVO.strLIS_Instrument_ID, m_objDeviceConfigVO.strLIS_Instrument_NO, p_strDeviceSampleID);
        //    return lngRes;
        //}
        #endregion

    }
}
