using System;
using iCareData;
using System.Collections;
using System.Text;
using com.digitalwave.controls;
namespace iCare
{
    /// <summary>
    /// ��Ѫ����ͬ����----�½� 
    /// </summary>

    public class clsShuXueZhiLiaoyesInfo_xj : clsDiseaseTrackInfo
    {
        private const string c_strSplitText = "ҽ����";

        private const int c_intWhiteSpaceCount = 14;

        private const string c_strInsertTextWithEnter = "\n������              ";
        private const string c_strInsertText = "������              ";

        /// <summary>
        /// �����¼�����ı��Ļ�ȡ��
        /// </summary>
        /// <returns></returns>
        public override string m_strGetTrackText()
        {
            if (m_objRecordContent == null)
                return "";

            clsShuXueZhiLiaoyesContent_xj objContent = ((clsShuXueZhiLiaoyesContent_xj)m_objRecordContent);
            string strText = m_strGetHeaderText();

            // strText += "\n    ����ʱ��:" + objContent.m_dtmDeadDate.ToString("yyyy��MM��dd��HHʱmm��");
            strText += "\n    ʱ��:" + objContent.m_dtmDoctorDate.ToString("yyyy��MM��dd��HHʱmm��");
            strText += "\n������ѪĿ��:" + objContent.m_strShuXueMuDi;
            strText += "\n����ҽʦǩ��:" + objContent.m_strRecordName;
            //strText += "\n����������:" + m_strGetName(objContent.m_strAttendeeNameArr);
            strText += "\n    ��Ѫ�ɷ�:" + objContent.m_strShuXueChengFen;
            strText += "\n    �ٴ����:" + objContent.m_strZhenDuan;
            //strText += "\n    ��¼��:" + objContent.m_strRecorderName;
            //strText += "\n    ����������ǩ��:" + objContent.m_strCompereName;
            return strText;
        }

        private string m_strGetName(string[] p_strNameArr)
        {
            string strName = "";
            if (p_strNameArr != null)
                for (int i = 0; i < p_strNameArr.Length; i++)
                {
                    if (i == 0)
                        strName += p_strNameArr[i].Trim();
                    else
                        strName += "��" + p_strNameArr[i].Trim();
                }
            return strName;
        }

        /// <summary>
        /// �����¼���ݸ�ʽXml�Ļ�ȡ
        /// </summary>
        public override string m_strGetTrackXml()
        {
            if (m_objRecordContent == null)
                return "";

            clsShuXueZhiLiaoyesContent_xj objContent = ((clsShuXueZhiLiaoyesContent_xj)m_objRecordContent);
            string strText = m_strGetHeaderText();

            string strCreateUserName = "";
            clsEmployee objEmployee = new clsEmployee(m_objRecordContent.m_strCreateUserID);
            if (objEmployee != null)
                strCreateUserName = objEmployee.m_StrLastName;

            string strXML1 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strText, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            // string strXML2 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n    ����ʱ��:" + objContent.m_dtmDeadDate.ToString("yyyy��MM��dd��HHʱmm��"), objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML2 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n    ʱ��:" + objContent.m_dtmDoctorDate.ToString("yyyy��MM��dd��HHʱmm��"), objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML3 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n    ��ѪĿ��:" + objContent.m_strShuXueMuDi, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML4 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n����ҽʦǩ��:" + objContent.m_strRecordName, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
         //   string strXML5 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n����������:" + m_strGetName(objContent.m_strAttendeeNameArr), objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML5 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n������Ѫ�ɷ�:" + objContent.m_strShuXueChengFen, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML6 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n   �ٴ����:" + objContent.m_strZhenDuan, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            //  string strXML14 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n   ����������ǩ��:" + objContent.m_strCompereName, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);

            string strXML = ctlRichTextBox.s_strCombineXml(new string[] { strXML1, strXML2, strXML3, objContent.m_strShuXueMuDiXML, strXML4, strXML5, objContent.m_strShuXueChengFenXML, strXML6, objContent.m_strZhenDuanXML });
            return strXML;
        }

        /// <summary>
        /// ��ȡ��ͷ������Ϣ
        /// </summary>
        /// <returns></returns>
        private string m_strGetHeaderText()
        {
            if (m_objRecordContent == null)
                return "";

            clsShuXueZhiLiaoyesContent_xj objContent = ((clsShuXueZhiLiaoyesContent_xj)m_objRecordContent);
            string strText;
            strText = objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmShuXueZhiLiaoyes_xj")) + "   " + "��Ѫ����ͬ����";
            return strText;
        }

        /// <summary>
        /// �����¼���͵Ļ�ȡ
        /// </summary>
        /// <returns></returns>
        public override enmDiseaseTrackType m_enmGetTrackType()
        {
            return enmDiseaseTrackType.ShuXueZhiLiaoyes_xj;
        }

        /// <summary>
        /// �����¼����ǩ���Ļ�ȡ
        /// </summary>
        /// <returns></returns>
        //		public override string m_strGetSignText()
        //		{
        //			if(m_objRecordContent==null)
        //				return "";
        //			clsDeadCaseDiscussRecord_VO objContent=((clsDeadCaseDiscussRecord_VO)m_objRecordContent);
        //			return m_strGetName(objContent.m_strAttendeeNameArr);
        //			
        //		}

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

        public void m_mthGetFormatTrackInfo(int p_intCharPerLine, out string p_strText, out string p_strXml)
        {
            m_mthGetFormatTrackInfo(p_intCharPerLine, false, DateTime.Now, out p_strText, out p_strXml);
        }

        public void m_mthGetFormatTrackInfo(int p_intCharPerLine, bool p_blnIsPrintCall, DateTime p_dtmSeperateTime, out string p_strText, out string p_strXml)
        {
            p_strText = "";
            p_strXml = "";

            if (m_objRecordContent == null)
                return;

            string strHeaderText = m_strGetHeaderText();

            clsShuXueZhiLiaoyesContent_xj objContent = ((clsShuXueZhiLiaoyesContent_xj)m_objRecordContent);
            p_strText = strHeaderText;


            //   p_strText += "\n    ����ʱ��:" + objContent.m_dtmDeadDate.ToString("yyyy��MM��dd��HHʱmm��");
            //p_strText += "\n    ����ʱ��:" + objContent.m_dtmDiscussDate.ToString("yyyy��MM��dd��HHʱmm��");
            //p_strText += "\n�������۵ص�:" + objContent.m_strDiscussAddress;
            //p_strText += "\n����������:" + objContent.m_strCompereName;
            //p_strText += "\n�����μ���Ա:" + m_strGetName(objContent.m_strAttendeeNameArr);

            p_strText += "\n    ʱ��:" + objContent.m_dtmDoctorDate.ToString("yyyy��MM��dd��HHʱmm��");
            p_strText += "\n������ѪĿ��:" + objContent.m_strShuXueMuDi;
            p_strText += "\n����ҽʦǩ��:" + objContent.m_strRecordName;
            //p_strText += "\n����������:" + m_strGetName(objContent.m_strAttendeeNameArr);


            string strCreateUserName = "";
            clsEmployee objEmployee = new clsEmployee(m_objRecordContent.m_strCreateUserID);
            if (objEmployee != null)
                strCreateUserName = objEmployee.m_StrLastName;

            //string strXML1 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strHeaderText, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            //string strXML2 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n    ����ʱ��:" + objContent.m_dtmDeadDate.ToString("yyyy��MM��dd��HHʱmm��"), objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            //string strXML3 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n    ����ʱ��:" + objContent.m_dtmDiscussDate.ToString("yyyy��MM��dd��HHʱmm��"), objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            //string strXML4 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n    ���۵ص�:" + objContent.m_strDiscussAddress, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            //string strXML5 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n����������:" + objContent.m_strCompereName, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            //string strXML6 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�����μ���Ա:" + m_strGetName(objContent.m_strAttendeeNameArr), objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            //string strXML7 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n������Ժ���:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            //string strXML8 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�������Լ�¼:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            //string strXML9 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n��������:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            //string strXML10 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�� �������:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            //string strXML11 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�� ����ԭ��:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            //string strXML12 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�� �����ѵ:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            //string strXML13 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n   ��¼��:" + objContent.m_strRecorderName, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            //string strXML14 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n   ����������ǩ��:" + objContent.m_strCompereName, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);

            string strXML1 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strHeaderText, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            // string strXML2 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n    ����ʱ��:" + objContent.m_dtmDeadDate.ToString("yyyy��MM��dd��HHʱmm��"), objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML2 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n    ʱ��:" + objContent.m_dtmDoctorDate.ToString("yyyy��MM��dd��HHʱmm��"), objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML3 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n    ��ѪĿ��:" + objContent.m_strShuXueMuDi, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strXML4 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n����ҽʦǩ��:" + objContent.m_strRecordName, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
           // string strXML5 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n����������:" + m_strGetName(objContent.m_strAttendeeNameArr), objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);

            string strXML5 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n�� ��Ѫ�ɷ�:", objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);

            string strXML6 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n   �ٴ����:" + objContent.m_strZhenDuan, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            //  string strXML14 = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("\n   ����������ǩ��:" + objContent.m_strCompereName, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);

            ArrayList arlXml = new ArrayList();
            arlXml.AddRange(new string[] { strXML1, strXML2, strXML3, objContent.m_strShuXueMuDiXML, strXML4, strXML5, objContent.m_strShuXueChengFenXML, strXML6, objContent.m_strZhenDuanXML });

            //p_strText += "\n������Ժ���:" + objContent.m_strInHospitalDiagnose;
            //p_strText += "\n    ���Լ�¼:" + objContent.m_strSpeakRecord;
            //p_strText += "\n    ����:" + objContent.m_strVerdict;
            //p_strText += "\n    �������:" + objContent.m_strDeadDiagnose;
            //p_strText += "\n    ����ԭ��:" + objContent.m_strDeadReason;
            //p_strText += "\n    �����ѵ:" + objContent.m_strExperience;
            //p_strText += "\n    ��¼��:" + objContent.m_strRecorderName;
            //p_strText += "\n    ����������ǩ��:" + objContent.m_strCompereName;


            //p_strText += "\n������ʷ�㱨:" + objContent.m_strHuiBao;
            //p_strText += "\n    �������:" + objContent.m_strTaoLunYiJian;
            //p_strText += "\n    ����С��:" + objContent.m_strTaoLunXiaoJie;
            //p_strText += "\n    ������:" + objContent.m_strZhuRenName;
            //p_strText += "\n    �ܴ�ҽʦ:" + objContent.m_strGuanChuangName;
            //p_strText += "\n    ��¼��:" + objContent.m_strRecordName;

            //arlXml.AddRange(new string[]{strXML7,objContent.m_strHuiBaoXML,strXML8,objContent.m_strTaoLunYiJianXML,strXML9,objContent.m_strTaoLunXiaoJieXML,
            //                                                        strXML10,strXML11,strXML12 });

            p_strXml = ctlRichTextBox.s_strCombineXml((string[])arlXml.ToArray(typeof(string)));

        }

        private string m_strFormatText(string p_strText, string p_strOldXml, int p_intCharPerLine, ArrayList p_arlXml)
        {
            string strTemp = p_strText.Replace("ҽ��:", "ҽ����");

            int intStarIndex = 0;

            int intPreDocIndex = strTemp.IndexOf(c_strSplitText, intStarIndex);

            if (intPreDocIndex < 0)
            {
                p_arlXml.Add(p_strOldXml);
                return p_strText;
            }

            int intPreRetIndex = strTemp.LastIndexOf('\n', intPreDocIndex);

            intStarIndex = intPreDocIndex + 3;

            ArrayList arlInsertIndex = new ArrayList();

            while (true)
            {
                int intDocIndex = strTemp.IndexOf(c_strSplitText, intStarIndex);

                if (intDocIndex >= 0)
                {
                    int intRetIndex = strTemp.LastIndexOf('\n', intDocIndex);

                    if (intRetIndex == -1)
                        intRetIndex = 0;

                    m_mthGetFormat(arlInsertIndex, p_strText, c_intWhiteSpaceCount, p_intCharPerLine, intStarIndex, intPreDocIndex, intPreRetIndex, intRetIndex);

                    intPreDocIndex = intDocIndex;
                    intPreRetIndex = intRetIndex;

                    intStarIndex = intPreDocIndex + 3;
                }
                else
                {
                    m_mthGetFormat(arlInsertIndex, p_strText, c_intWhiteSpaceCount, p_intCharPerLine, intStarIndex, intPreDocIndex, intPreRetIndex, -1);

                    break;
                }
            }

            return m_strFormat(arlInsertIndex, p_strText, p_strOldXml, c_intWhiteSpaceCount, p_arlXml);
        }

        private void m_mthGetFormat(ArrayList p_arlFormatInfo, string p_strText, int p_intWhiteSpaceCount, int p_intCharPerLine, int p_intStartIndex, int p_intPreDocIndex, int p_intPreRetIndex, int p_intRetIndex)
        {
            int intDocDescLength = p_intPreDocIndex + 3 - (p_intPreRetIndex + 1);
            int intTempWhiteSpace = p_intWhiteSpaceCount;

            for (int i = 0; i < intDocDescLength; i++)
            {
                if ((int)p_strText[i + p_intPreRetIndex + 1] < 255)
                    intTempWhiteSpace--;
                else
                    intTempWhiteSpace -= 2;
            }

            p_arlFormatInfo.Add(p_intPreRetIndex + 1 + 20000000 + intTempWhiteSpace * 10000);

            int intTextLength = 0;
            if (p_intRetIndex >= 0)
            {
                intTextLength = p_intRetIndex - p_intStartIndex + 1;
            }
            else
            {
                intTextLength = p_strText.Length - p_intStartIndex;
            }

            int intCountTemp = 0;

            for (int i = 0; i < intTextLength; i++)
            {
                if (p_strText[i + p_intStartIndex] != '\n')
                {
                    intCountTemp++;

                    if (intCountTemp == p_intCharPerLine)
                    {
                        p_arlFormatInfo.Add(p_intStartIndex + i + 1);
                        intCountTemp = 0;
                    }
                }
                else if (i != intTextLength - 1)
                {
                    p_arlFormatInfo.Add(p_intStartIndex + i + 1 + 10000);
                    intCountTemp = 0;
                }
            }
        }

        private string m_strFormat(ArrayList p_arlFormatInfo, string p_strText, string p_strOldXml, int p_intWhiteSpaceCount, ArrayList p_arlXml)
        {
            clsShuXueZhiLiaoyesContent_xj objContent = ((clsShuXueZhiLiaoyesContent_xj)m_objRecordContent);
            string strCreateUserName = "";
            clsEmployee objEmployee = new clsEmployee(m_objRecordContent.m_strCreateUserID);
            if (objEmployee != null)
                strCreateUserName = objEmployee.m_StrLastName;

            string strInsertTextWithEnterXml = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(c_strInsertTextWithEnter, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);
            string strInsertTextXml = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(c_strInsertText, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);

            ctlRichTextBox.clsUserXMLInfo objUserXmlInfo = ctlRichTextBox.s_objGetUserXMLInfo(p_strOldXml);

            StringBuilder sbValue = new StringBuilder();
            int intPreIndex = 0;

            for (int i = 0; i < p_arlFormatInfo.Count; i++)
            {
                int intIndex = (int)p_arlFormatInfo[i];

                if (intIndex < 10000)
                {
                    sbValue.Append(p_strText.Substring(intPreIndex, intIndex - intPreIndex));
                    sbValue.Append(c_strInsertTextWithEnter);

                    p_arlXml.Add(ctlRichTextBox.s_strGetSubXml(objUserXmlInfo, intPreIndex, intIndex - 1));
                    p_arlXml.Add(strInsertTextWithEnterXml);

                    intPreIndex = intIndex;
                }
                else if (intIndex < 20000000)
                {
                    intIndex %= 10000;

                    sbValue.Append(p_strText.Substring(intPreIndex, intIndex - intPreIndex));
                    sbValue.Append(c_strInsertText);

                    p_arlXml.Add(ctlRichTextBox.s_strGetSubXml(objUserXmlInfo, intPreIndex, intIndex - 1));
                    p_arlXml.Add(strInsertTextXml);

                    intPreIndex = intIndex;
                }
                else
                {
                    intIndex %= 20000000;
                    int intWhiteCount = intIndex / 10000;
                    intIndex %= 10000;

                    string strWhiteSpace = "������";
                    for (int j2 = 0; j2 < intWhiteCount; j2++)
                    {
                        strWhiteSpace += " ";
                    }
                    string strWhiteSpaceXml = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strWhiteSpace, objContent.m_strCreateUserID, strCreateUserName, System.Drawing.Color.White);

                    if (intIndex > 0)
                    {
                        sbValue.Append(p_strText.Substring(intPreIndex, intIndex - intPreIndex));
                        sbValue.Append(strWhiteSpace);

                        p_arlXml.Add(ctlRichTextBox.s_strGetSubXml(objUserXmlInfo, intPreIndex, intIndex - 1));
                        p_arlXml.Add(strWhiteSpaceXml);

                        intPreIndex = intIndex;
                    }
                    else if (intIndex == 0)
                    {
                        sbValue.Append(strWhiteSpace);
                        p_arlXml.Add(strWhiteSpaceXml);

                    }
                }
            }

            sbValue.Append(p_strText.Substring(intPreIndex));

            p_arlXml.Add(ctlRichTextBox.s_strGetSubXml(objUserXmlInfo, intPreIndex, p_strText.Length - 1));

            return sbValue.ToString();
        }

    }

}
