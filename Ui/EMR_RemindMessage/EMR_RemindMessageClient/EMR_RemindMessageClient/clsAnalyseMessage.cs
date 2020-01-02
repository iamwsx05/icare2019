using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using weCare.Core.Entity;
using com.digitalwave.Emr.StaticObject;

namespace com.digitalwave.iCare.RemindMessage
{
    /// <summary>
    /// ���ƥ���� Ϣ
    /// </summary>
    public class clsAnalyseMessage
    {
        #region �����յ�����Ϣ�Ƿ����ڵ�ǰ�û�
        /// <summary>
        /// �����յ�����Ϣ�Ƿ����ڵ�ǰ�û�
        /// </summary>
        /// <param name="p_strBroadCastingMessage">�㲥��Ϣ</param>
        /// <param name="p_enmItemType">��Ϣ����</param>
        /// <returns></returns>
        public bool m_blnIsMatching(string p_strBroadCastingMessage, out enmMessageItemType p_enmItemType)
        {
            p_enmItemType = enmMessageItemType.None;
            if (clsEMR_StaticObject.s_ObjCurrentEmployee == null || p_strBroadCastingMessage == null)
                return false;

            bool blnIsMatch = false;
            try
            {
                string ID = string.Empty;
                string Item = string.Empty;
                string Remark = string.Empty;
                XmlParserContext m_objXmlParser = new XmlParserContext(null, null, null, XmlSpace.None, Encoding.Unicode);
                XmlTextReader objReader = new XmlTextReader(p_strBroadCastingMessage, XmlNodeType.Element, m_objXmlParser);
                objReader.WhitespaceHandling = WhitespaceHandling.None;

                int intLevel = 0;
                intLevel = m_intCanSeeLevel(objReader);

                while (objReader.Read())
                {
                    switch (objReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (objReader.Name == "ID")
                            {
                                ID = objReader.ReadString();
                            }
                            else if (objReader.Name == "ITEM")
                            {
                                Item = objReader.ReadString();
                                p_enmItemType = m_enmMessageItemType(Item);
                            }
                            else if (objReader.Name == "REMARK")
                            {
                                Remark = objReader.ReadString();
                            }
                            break;
                    }
                }

                if (intLevel == 1 || intLevel == 2)
                {
                    if (p_enmItemType == enmMessageItemType.Consultation && Remark == "1"
                        && ID.Trim() == clsEMR_StaticObject.s_ObjCurrentEmployee.m_strDefaultDeptID.Trim())
                    {
                        blnIsMatch = true;
                    }
                    else
                    {
                        for (int i = 0; i < clsEMR_StaticObject.s_ObjEmpDeptAndAreaArr.Length; i++)
                        {
                            if (ID.Trim() == clsEMR_StaticObject.s_ObjEmpDeptAndAreaArr[i].m_strDEPTID_CHR.Trim())
                            {
                                blnIsMatch = true;
                                break;
                            }
                        }
                    }
                }
                else if (intLevel >= 0)//ָ��������ID�����ָ����ɫ�Ľ������ڷ���������жϲ�����
                {
                    blnIsMatch = true;
                }                
                else
                {
                    blnIsMatch = false;
                }
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;
            }
            return blnIsMatch;
        } 
        #endregion

        #region ���ݿɼ����𷵻���Ӧ���ִ���
        /// <summary>
        /// ���ݿɼ����𷵻���Ӧ���ִ���
        /// </summary>
        /// <param name="objReader">XMLReader</param>
        /// <returns></returns>
        private int m_intCanSeeLevel(XmlTextReader objReader)
        {
            int intLevel = 0;
            if (objReader.IsStartElement("DEPT"))
            {
                intLevel = 1;
            }
            else if (objReader.IsStartElement("AREA"))
            {
                intLevel = 2;
            }
            else if (objReader.IsStartElement("EMPLOYEE"))
            {
                intLevel = 3;
            }
            else if (objReader.IsStartElement("All"))
            {
                intLevel = 0;
            }
            else if (objReader.IsStartElement("ROLE"))
            {
                intLevel = 4;
            }
            return intLevel;
        } 
        #endregion

        #region ����ITEM����ַ���������Ϣ����
        /// <summary>
        /// ����ITEM����ַ���������Ϣ����
        /// </summary>
        /// <param name="p_strMessageInfo">XML�е�ITEM����ַ���</param>
        /// <returns></returns>
        private enmMessageItemType m_enmMessageItemType(string p_strMessageInfo)
        {
            enmMessageItemType ItemType = enmMessageItemType.None;
            switch (p_strMessageInfo)
            {
                case "CONSULTATION":
                    ItemType = enmMessageItemType.Consultation;
                    break;
                case "CASEHISTORYARCHIVINGREQUEST":
                    ItemType = enmMessageItemType.CaseHistoryArchivingRequest;
                    break;
                case "CASEHISTORYARCHIVINGAPPROVE":
                    ItemType = enmMessageItemType.CaseHistoryArchivingApprove;
                    break;
                case "CANCELARCHIVINGAPPROVE":
                    ItemType = enmMessageItemType.CancelArchivingApprove;
                    break;
                case "SUBMITORDERS":
                    ItemType = enmMessageItemType.SubmitOrders;
                    break;
                case "FOLLOWUPSURVEY":
                    ItemType = enmMessageItemType.FollowUpSurvey;
                    break;
                case "AUDITINGINFECTION":
                    ItemType = enmMessageItemType.AuditingInfection;
                    break;
                default:
                    break;
            }
            return ItemType;
        } 
        #endregion

        #region ���ɹ㲥��Ϣ
        /// <summary>
        /// ���ɹ㲥��Ϣ
        /// </summary>
        /// <param name="p_Level">�ɼ�����</param>
        /// <param name="p_Item">��Ŀ����</param>
        /// <param name="p_strID">ID</param>
        /// <param name="p_strRemark">��ע</param>
        /// <returns></returns>
        public string m_strSetBroadCastingMessage(enmVisibleLevel p_Level, enmMessageItemType p_Item, string p_strID, string p_strRemark)
        {
            if (string.IsNullOrEmpty(p_strID))
                return string.Empty;

            if (p_strRemark == null)
                p_strRemark = "";

            string strXML = @"<Level><ID>GetID</ID><ITEM>GetItem</ITEM><REMARK>GetRemark</REMARK></Level>";
            switch (p_Level)
            {
                case enmVisibleLevel.DEPT:
                    strXML = strXML.Replace("Level", "DEPT");
                    break;
                case enmVisibleLevel.AREA:
                    strXML = strXML.Replace("Level", "AREA");
                    break;
                case enmVisibleLevel.EMP:
                    strXML = strXML.Replace("Level", "EMPLOYEE");
                    break;
                case enmVisibleLevel.ROLE:
                    strXML = strXML.Replace("Level", "ROLE");
                    break;
                case enmVisibleLevel.ALL:
                    strXML = strXML.Replace("Level", "ALL");
                    break;
            }

            switch (p_Item)
            {
                case enmMessageItemType.Consultation:
                    strXML = strXML.Replace("GetItem", "CONSULTATION");
                    break;
                case enmMessageItemType.CaseHistoryArchivingRequest:
                    strXML = strXML.Replace("GetItem", "CASEHISTORYARCHIVINGREQUEST");
                    break;
                case enmMessageItemType.CaseHistoryArchivingApprove:
                    strXML = strXML.Replace("GetItem", "CASEHISTORYARCHIVINGAPPROVE");
                    break;
                case enmMessageItemType.CancelArchivingApprove:
                    strXML = strXML.Replace("GetItem", "CANCELARCHIVINGAPPROVE");
                    break;
                case enmMessageItemType.SubmitOrders:
                    strXML = strXML.Replace("GetItem", "SUBMITORDERS");
                    break;
                case enmMessageItemType.FollowUpSurvey:
                    strXML = strXML.Replace("GetItem", "FOLLOWUPSURVEY");
                    break;
                case enmMessageItemType.AuditingInfection:
                    strXML = strXML.Replace("GetItem", "AUDITINGINFECTION");
                    break;
            }
            strXML = strXML.Replace("GetID", p_strID).Replace("GetRemark", p_strRemark);
            return strXML;
        } 
        #endregion
    }
}
