using System;
using iCareData;
using com.digitalwave.controls;

namespace iCare
{
    /// <summary>
    /// �״β��̼�¼��Ϣ---�½�
    /// </summary>
    public class clsFirstIllnessNoteInfo_XJ : clsDiseaseTrackInfo
    {
        /// <summary>
        /// �����¼�����ı��Ļ�ȡ��
        /// </summary>
        /// <returns></returns>
        public override string m_strGetTrackText()
        {
            if (m_objRecordContent == null)
                return "";

            clsFirstIllnessNoteRecordContent_XJ objContent = ((clsFirstIllnessNoteRecordContent_XJ)m_objRecordContent);
            string strText;
            strText = objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmFirstIllnessNote_XJ")) + "   " + "�״β��̼�¼";
            clsPatient objPatient = new clsPatient(objContent.m_strInPatientID);

            //strText += "\n" + objContent.m_strMostlyContent;
            strText += "\n(һ)�����ص�:\n" + objContent.m_strBingLiTeDian;
            //if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO != "440605001")
                strText += "\n(��)��ҽ�粡��֤����:\n" + objContent.m_strZhongYiBianBing;
           // else
                strText += "\n(��)��ҽ�������:\n" + objContent.m_strXiYiZhenDuanYiJu;
            strText += "\n(��)��ҽ�������:\n" + objContent.m_strZhongYiJianBie;
            strText += "\n(��)��ҽ�������:\n" + objContent.m_strXiYiJianBie;
            strText += "\n(��)��ҽ�������:\n" + objContent.m_strZhongYiChuBu;
            strText += "\n(��)��ҽ�������:\n" + objContent.m_strXiYiChuBu;
            strText += "\n(��)���Ƽƻ�:\n" + objContent.m_strZhenLiaoJiHua;

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

            clsFirstIllnessNoteRecordContent_XJ objContent = ((clsFirstIllnessNoteRecordContent_XJ)m_objRecordContent);
            string strText;
            strText = objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmFirstIllnessNote")) + "   " + "�״β��̼�¼";
            clsPatient objPatient = new clsPatient(objContent.m_strInPatientID);

            string strCreateUserName = m_strGetSignText();

            string strXML1 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strText, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
           // string strXML2 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML3 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n(һ)�����ص�:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            //string strXML4 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n(��)�����������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
           // string strXML5 = "";
           // if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO != "440605001")
            string strXML4 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n(��)��ҽ�粡��֤����:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
          //  else 
            string strXML5 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n(��)��ҽ�������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML6 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n(��)��ҽ�������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);

            string strXML7 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n(��)��ҽ�������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML8 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n(��)��ҽ�������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML9 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n(��)��ҽ�������:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML10 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n(��)���Ƽƻ�:\n", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);

            string strXML = ctlRichTextBox.clsXmlTool.s_strCombineXml(new string[] { strXML1, strXML3, objContent.m_strBingLiTeDianXML, strXML4, objContent.m_strZhongYiBianBingXML, strXML5, objContent.m_strXiYiZhenDuanYiJuXML, strXML6, objContent.m_strZhongYiJianBieXML, strXML7, objContent.m_strXiYiJianBieXML, strXML8, objContent.m_strZhongYiChuBuXML, strXML9, objContent.m_strXiYiChuBuXML, strXML10, objContent.m_strZhenLiaoJiHuaXML });
            return strXML;
        }

        /// <summary>
        /// �����¼���͵Ļ�ȡ
        /// </summary>
        /// <returns></returns>
        public override enmDiseaseTrackType m_enmGetTrackType()
        {
            return enmDiseaseTrackType.FirstIllnessNote_XJ;
        }

        /// <summary>
        /// �����¼����ǩ���Ļ�ȡ
        /// </summary>
        /// <returns></returns>
        public override string m_strGetSignText()
        {
            if (m_objRecordContent == null)
                return "";
            //��ȡǩ��
            string strSigns = "";
            bool blnFirst = false;
            if (m_objRecordContent.objSignerArr != null)
            {
                for (int i = 0; i < m_objRecordContent.objSignerArr.Length; i++)
                {
                    if (m_objRecordContent.objSignerArr[i].controlName == "lsvSign")
                    {
                        //����
                        if (!blnFirst)
                        {
                            strSigns = m_objRecordContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName;
                            blnFirst = true;
                        }
                        else
                            strSigns = strSigns + "��" + m_objRecordContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName;
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
