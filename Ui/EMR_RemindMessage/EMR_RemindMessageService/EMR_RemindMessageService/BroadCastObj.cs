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
        /// 保存在服务器注册的所有用户信息，具体格式参见客户端文件
        /// </summary>
        private XmlDocument xmlDoc = null;
        /// <summary>
        /// 保存用户ID及相应的事件列表
        /// </summary>
        private Hashtable delegateMap = new Hashtable();

        /// <summary>
        /// 构造函数，初始化保存用户信息的XML
        /// </summary>
        public BroadCastObj()
        {
            xmlDoc = new XmlDocument();
            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", null, null);
            xmlDoc.AppendChild(dec);

            XmlElement elRoot = xmlDoc.CreateElement("CLIENT");
            xmlDoc.AppendChild(elRoot);
        }

        #region IBroadCast 成员
        /// <summary>
        /// 根据指定的注册信息注册事件
        /// </summary>
        /// <param name="eventInfo">注册信息，XML格式</param>
        /// <param name="handler">客户端委托</param>
        public void SubscribeEvent(string eventInfo, BroadCastEventHandler handler)
        {
            //确保同一时间只有一个线程修改事件列表
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
        /// 注销客户端，并从事件列表去除该客户端调用的事件
        /// </summary>
        /// <param name="eventInfo">注册信息</param>
        /// <param name="handler"></param>
        public void UnSubscribeEvent(string eventInfo, BroadCastEventHandler handler)
        {
            //确保同一时间只有一个线程修改事件列表
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
        /// 调用事件列表中的指定事件
        /// </summary>
        /// <param name="eventName">发送信息，XML格式，包含可见范围，具体格式参见客户端文件</param>
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

        #region 从注册信息中获取用户ID作为HashTable键
        /// <summary>
        /// 从注册信息中获取用户ID作为HashTable键
        /// </summary>
        /// <param name="eventInfo">注册信息，XML格式</param>
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

        #region 将注册信息添加至客户端用户列表
        /// <summary>
        /// 将注册信息添加至客户端用户列表
        /// </summary>
        /// <param name="eventInfo">注册信息</param>
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

        #region 从客户端用户列表中去除用户注册信息
        /// <summary>
        /// 从客户端用户列表中去除用户注册信息
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

        #region 获取需要激发事件的用户ID列表
        /// <summary>
        /// 获取需要激发事件的用户ID列表
        /// </summary>
        /// <param name="p_strEventName">发送信息</param>
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
