using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace com.digitalwave.iCare.RemindMessage
{
    public class BroadCastObj : MarshalByRefObject, IBroadCast
    {
        /// <summary>
        /// �����ڷ�����ע��������û���Ϣ�������ʽ�μ��ͻ����ļ�
        /// </summary>
        private XmlDocument xmlDoc = null;
        /// <summary>
        /// �����û�ID����Ӧ���¼��б�
        /// </summary>
        private Hashtable delegateMap = new Hashtable();

        /// <summary>
        /// ���캯������ʼ�������û���Ϣ��XML
        /// </summary>
        public BroadCastObj()
        {
            xmlDoc = new XmlDocument();
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", null, null);
            xmlDoc.AppendChild(dec);

            XmlElement elRoot = xmlDoc.CreateElement("CLIENT");
            xmlDoc.AppendChild(elRoot);
        }

        #region IBroadCast ��Ա
        /// <summary>
        /// ����ָ����ע����Ϣע���¼�
        /// </summary>
        /// <param name="eventInfo">ע����Ϣ��XML��ʽ</param>
        /// <param name="handler">�ͻ���ί��</param>
        public void SubscribeEvent(string eventInfo, BroadCastEventHandler handler)
        {
            //ȷ��ͬһʱ��ֻ��һ���߳��޸��¼��б�
            lock (this)
            {
                Delegate delegateChain = null;
                string EMPID = m_strGetEMPID(eventInfo);
                if (string.IsNullOrEmpty(EMPID))
                {
                    return;
                }

                if (delegateMap.Count > 0)
                {
                    delegateChain = delegateMap[EMPID] as Delegate;
                }

                if (delegateChain == null)
                {
                    delegateMap.Add(EMPID, handler);
                    m_mthAddEMPInfoTODoc(eventInfo);
                }
                else
                {
                    delegateChain = Delegate.Combine(delegateChain, handler);
                    delegateMap[EMPID] = delegateChain;
                }
            }
        }

        /// <summary>
        /// ע���ͻ��ˣ������¼��б�ȥ���ÿͻ��˵��õ��¼�
        /// </summary>
        /// <param name="eventInfo">ע����Ϣ</param>
        /// <param name="handler"></param>
        public void UnSubscribeEvent(string eventInfo, BroadCastEventHandler handler)
        {
            //ȷ��ͬһʱ��ֻ��һ���߳��޸��¼��б�
            lock (this)
            {
                string EMPID = m_strGetEMPID(eventInfo);
                if (string.IsNullOrEmpty(EMPID))
                {
                    return;
                }

                Delegate delegateChain = delegateMap[EMPID] as Delegate;

                if (delegateChain != null)
                {
                    delegateChain = Delegate.Remove(delegateChain, handler);
                    if (delegateChain == null || delegateChain.GetInvocationList() == null || delegateChain.GetInvocationList().Length <= 0)
                    {
                        delegateMap.Remove(EMPID);
                        m_mthRemoveEMPInfoFromDoc(EMPID);
                    }
                    else
                    {
                        delegateMap[EMPID] = delegateChain;
                    }
                }
            }
        }

        /// <summary>
        /// �����¼��б��е�ָ���¼�
        /// </summary>
        /// <param name="eventName">������Ϣ��XML��ʽ�������ɼ���Χ�������ʽ�μ��ͻ����ļ�</param>
        public void RaiseEvent(string eventName)
        {
            string[] strEMPID = m_strGetRightEMPID(eventName);
            if (strEMPID == null)
            {
                return;
            }
            for (int i = 0; i < strEMPID.Length; i++)
            {
                Delegate delegateChain = delegateMap[strEMPID[i]] as Delegate;
                IEnumerator invocationEnumerator = delegateChain.GetInvocationList().GetEnumerator();

                while (invocationEnumerator.MoveNext())
                {
                    Delegate handler = (Delegate)invocationEnumerator.Current;
                    try
                    {
                        handler.DynamicInvoke(new object[] { eventName });
                    }
                    catch (Exception ex)
                    {
                        delegateChain = Delegate.Remove(delegateChain, handler);
                        if (delegateChain == null || delegateChain.GetInvocationList() == null || delegateChain.GetInvocationList().Length <= 0)
                        {
                            delegateMap.Remove(strEMPID[i]);
                            m_mthRemoveEMPInfoFromDoc(strEMPID[i]);
                        }
                        else
                        {
                            delegateMap[strEMPID[i]] = delegateChain;
                        }
                    }
                }
            }        
        }
        #endregion

        public override object InitializeLifetimeService()
        {
            return null;
        }

        #region ��ע����Ϣ�л�ȡ�û�ID��ΪHashTable��
        /// <summary>
        /// ��ע����Ϣ�л�ȡ�û�ID��ΪHashTable��
        /// </summary>
        /// <param name="eventInfo">ע����Ϣ��XML��ʽ</param>
        /// <returns></returns>
        private string m_strGetEMPID(string eventInfo)
        {
            string strEMPID = string.Empty;
            try
            {
                XmlDocument docInfo = new XmlDocument();
                docInfo.LoadXml(eventInfo);

                if (docInfo != null && docInfo.FirstChild != null)
                {
                    strEMPID = docInfo.FirstChild.Attributes[0].Value;
                }
            }
            catch (Exception Ex)
            {
                string strEx = Ex.Message;
            }
            return strEMPID;         
        } 
        #endregion

        #region ��ע����Ϣ������ͻ����û��б�
        /// <summary>
        /// ��ע����Ϣ������ͻ����û��б�
        /// </summary>
        /// <param name="eventInfo">ע����Ϣ</param>
        private void m_mthAddEMPInfoTODoc(string eventInfo)
        {
            try
            {
                XmlDocument docTemp = new XmlDocument();
                docTemp.LoadXml(eventInfo);
                if (docTemp == null)
                {
                    return;
                }

                xmlDoc.ChildNodes[1].AppendChild(xmlDoc.ImportNode(docTemp.ChildNodes[0], true));
            }
            catch (Exception Ex)
            {
                string strEx = Ex.Message;
            }            
        } 
        #endregion

        #region �ӿͻ����û��б���ȥ���û�ע����Ϣ
        /// <summary>
        /// �ӿͻ����û��б���ȥ���û�ע����Ϣ
        /// </summary>
        /// <param name="strEMPID"></param>
        private void m_mthRemoveEMPInfoFromDoc(string strEMPID)
        {
            try
            {                
                XmlNodeList nodeL = xmlDoc.SelectNodes(@"//EMPLOYEE[@ID='" + strEMPID + "']");
                foreach (XmlNode nodeC in nodeL)
                {
                    xmlDoc.ChildNodes[1].RemoveChild(nodeC);
                }
            }
            catch (Exception Ex)
            {
                string strEx = Ex.Message; 
            }
        } 
        #endregion

        #region ��ȡ��Ҫ�����¼����û�ID�б�
        /// <summary>
        /// ��ȡ��Ҫ�����¼����û�ID�б�
        /// </summary>
        /// <param name="p_strEventName">������Ϣ</param>
        /// <returns></returns>
        private string[] m_strGetRightEMPID(string p_strEventName)
        {
            string[] strEMPIDArr = null;
            try
            {
                string ID = string.Empty;
                string Item = string.Empty;
                XmlParserContext m_objXmlParser = new XmlParserContext(null, null, null, XmlSpace.None, Encoding.Unicode);
                XmlTextReader objReader = new XmlTextReader(p_strEventName, XmlNodeType.Element, m_objXmlParser);
                objReader.WhitespaceHandling = WhitespaceHandling.None;

                bool blnFirstElement = true;
                while (objReader.Read())
                {
                    switch (objReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (blnFirstElement)
                            {
                                Item = objReader.Name;
                                blnFirstElement = false;
                            }
                            else
                            {
                                if (objReader.Name == "ID")
                                {
                                    ID = objReader.ReadString();
                                }
                            }
                            break;
                    }
                }

                XmlNodeList nodeL = null;
                if (Item.ToUpper() == "ALL")
                {
                    nodeL = xmlDoc.SelectNodes("//EMPLOYEE");

                    List<string> strID = new List<string>();
                    string strText = string.Empty;
                    foreach (XmlNode Xnode in nodeL)
                    {
                        strID.Add(Xnode.Attributes[0].Value);
                    }
                    strEMPIDArr = strID.ToArray();
                }
                else
                {
                    nodeL = xmlDoc.SelectNodes("//" + Item.ToUpper() + "[@ID='" + ID + "']");

                    List<string> strID = new List<string>();
                    string strText = string.Empty;
                    if (Item.ToUpper() == "EMPLOYEE")
                    {
                        foreach (XmlNode Xnode in nodeL)
                        {
                            strID.Add(Xnode.Attributes[0].Value);
                        }
                    }
                    else
                    {
                        foreach (XmlNode Xnode in nodeL)
                        {
                            strID.Add(Xnode.ParentNode.Attributes[0].Value);
                        }
                    }
                    strEMPIDArr = strID.ToArray();
                }
            }
            catch (Exception Ex)
            {
                string strEx = Ex.Message;
            }
            return strEMPIDArr;
        } 
        #endregion
    }
}
