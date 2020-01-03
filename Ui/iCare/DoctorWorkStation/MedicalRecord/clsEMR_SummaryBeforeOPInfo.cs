using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using com.digitalwave.controls;

namespace iCare
{
    /// <summary>
    /// ��ǰС����Ϣ
    /// </summary>
    public class clsEMR_SummaryBeforeOPInfo : clsDiseaseTrackInfo
    {
        /// <summary>
        /// �����¼�����ı��Ļ�ȡ��
        /// </summary>
        /// <returns></returns>
        public override string m_strGetTrackText()
        {
            if (m_objRecordContent == null)
                return "";

            clsEMR_SummaryBeforeOPValue objContent = ((clsEMR_SummaryBeforeOPValue)m_objRecordContent);
            string strText;
            strText = objContent.m_dtmRECORDDATE.ToString("yyyy��MM��dd��") + "   " + "��ǰС��";
            
            strText += "\n����ժҪ:\n" + objContent.m_strDISEASESUMMARY;
            strText += "\n��ǰ���:\n" + objContent.m_strDIAGNOSISBEFOREOP;
            strText += "\n�������:\n" + objContent.m_strDIAGNOSISGIST;
            strText += "\n����ָ��:\n" + objContent.m_strOPINDICATION;
            strText += "\n������ʽ:\n" + objContent.m_strOPMODE;
            strText += "\n����ʽ:\n" + objContent.m_strANAMODE;
            strText += "\nע������(��ǰ�����С�����):\n" + objContent.m_strPROCEEDING;
            strText += "\n��ǰ׼��:\n" + objContent.m_strPREPAREBEFOREOP;

            return strText;
        }

        /// <summary>
        /// �����¼���ݸ�ʽXml�Ļ�ȡ
        /// </summary>
        /// <returns></returns>
        public override string m_strGetTrackXml()
        {
            if (m_objRecordContent == null)
                return "";

            clsEMR_SummaryBeforeOPValue objContent = ((clsEMR_SummaryBeforeOPValue)m_objRecordContent);
            string strText;
            strText = objContent.m_dtmRECORDDATE.ToString("yyyy��MM��dd��") + "   " + "��ǰС��";
            string strCreateUserName = m_strGetSignText();

            string strXML1 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strText, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML2 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n����ժҪ:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White );
            string strXML3 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n��ǰ���:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML4 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML5 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n����ָ��:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML6 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n������ʽ:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML7 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n����ʽ:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML8 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\nע������(��ǰ�����С�����):\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML9 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n��ǰ׼��:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);

            string strXML = ctlRichTextBox.clsXmlTool.s_strCombineXml(new string[] { strXML1, strXML2, objContent.m_strDISEASESUMMARYXML, strXML3, objContent.m_strDIAGNOSISBEFOREOPXML, 
                strXML4, objContent.m_strDIAGNOSISGISTXML, strXML5, objContent.m_strOPINDICATIONXML, strXML6, objContent.m_strOPMODEXML, strXML7,objContent.m_strANAMODEXML,
                strXML8,objContent.m_strPROCEEDINGXML,strXML9,objContent.m_strPREPAREBEFOREOPXML});
            return strXML;
        }

        /// <summary>
        /// �����¼���͵Ļ�ȡ
        /// </summary>
        /// <returns></returns>
        public override enmDiseaseTrackType m_enmGetTrackType()
        {
            return enmDiseaseTrackType.EMR_SummaryBeforeOP;
        }

        /// <summary>
        /// �����¼����ǩ���Ļ�ȡ
        /// </summary>
        /// <returns></returns>
        public override string m_strGetSignText()
        {
            if (m_objRecordContent == null)
                return "";
            string strSigns = "";
            //��ʾǩ����
            if (m_objRecordContent.objSignerArr != null)
            {
                for (int i = 0; i < m_objRecordContent.objSignerArr.Length; i++)
                {
                    if (m_objRecordContent.objSignerArr[i].controlName == "lsvSign")
                    {
                        //����
                        strSigns = strSigns + " " + m_objRecordContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName;
                    }
                }
            }

            return strSigns;

        }

        /// <summary>
        /// �����¼����ǩ���Ļ�ȡ
        /// </summary>
        /// <returns></returns>
        public override string m_strGetSignXml()
        {
            if (m_objRecordContent == null)
                return "";

            return "<Root />";
        }	
    }
}
