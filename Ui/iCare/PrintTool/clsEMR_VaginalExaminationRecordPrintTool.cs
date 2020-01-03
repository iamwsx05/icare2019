using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;

namespace iCare
{
    /// <summary>
    /// ��������¼���ӡ��
    /// </summary>
    public class clsEMR_VaginalExaminationRecordPrintTool : infPrintRecord, IDisposable
    {
        #region ����
        /// <summary>
        /// �Ƿ��ӡ�޸ĺۼ�
        /// </summary>
        public static bool m_blnIsPrintMark = true;
        /// <summary>
        /// �����˴δ�ӡ���ⲿ��ֵ(false)�������ڲ��Զ������ݿ���ȡ����(true)
        /// </summary>
        private bool m_blnIsFromDataSource = true;
        /// <summary>
        /// ��ʶ��ӡ�����ڰ��ӡ����֮ǰ�Ƿ��ʼ��
        /// </summary>
        private bool m_blnWantInit = true;
        /// <summary>
        /// ��ӡҳ��ʱ������ʶ��ǰ�ڼ�ҳ
        /// </summary>
        private int m_intCurrentPage = 1;
        /// <summary>
        /// ��ӡ�����ݶ���
        /// </summary>
        private clsPrintInfo_EMR_VaginalExaminationRecord m_objPrintInfo = null;
        /// <summary>
        /// ��ӡ������
        /// </summary>
        private com.digitalwave.Utility.Controls.clsPrintContext m_objPrintLineContext = null;
        /// <summary>
        /// ��ǰ��ӡλ�ã�Y��
        /// </summary>
        private int m_intYPos = 130;//155;

        /// <summary>
        /// ҽԺ��������
        /// </summary>
        private static Font m_fotHospitalTitle = null;
        /// <summary>
        /// ����������
        /// </summary>
        public static Font m_fotHeader = null;
        /// <summary>
        /// ҳ������
        /// </summary>
        public static Font m_fotFooter = null;
        /// <summary>
        /// ��������
        /// </summary>
        public static Font m_fotContent = null;
        /// <summary>
        /// ǩ������
        /// </summary>
        public static Font m_fotSign = null;
        ///// <summary>
        ///// ��ȡ�������
        ///// </summary>
        //private clsPrintPageSettingForRecord m_objPageSetting;
        #endregion
        /// <summary>
        /// ��̬���캯��
        /// </summary>
        static clsEMR_VaginalExaminationRecordPrintTool()
        {
            clsEMR_VaginalExaminationRecordPrintTool.m_fotHospitalTitle = new Font("Simsun", 15);
            clsEMR_VaginalExaminationRecordPrintTool.m_fotHeader = new Font("Simsun", 18);
            clsEMR_VaginalExaminationRecordPrintTool.m_fotFooter = new Font("Simsun", 12);
            clsEMR_VaginalExaminationRecordPrintTool.m_fotContent = new Font("Simsun", 12);
            clsEMR_VaginalExaminationRecordPrintTool.m_fotSign = new Font("Simsun", 10);
        }

        /// <summary>
        /// ��ȡ��ӡ�޸ĺۼ�����
        /// </summary>
        private void m_mthGetPrintMarkConfig()
        {
            int intConfig = com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_intGetEmrSettingValue("3012");
            if (intConfig == 0)
            {
                m_blnIsPrintMark = false;
            }
            else
            {
                m_blnIsPrintMark = true;
            }
        }

        #region ���ӵ���Ϣ
        /// <summary>
        /// ���ӵ���Ϣ
        /// </summary>
        private enum enmRectangleInfoEMR_VaginalExaminationRecord
        {
            /// <summary>
            /// ���ӵĶ���
            /// </summary>
            TopY = 140,
            ///<summary>
            /// ���ӵ����
            /// </summary>
            LeftX = 60,
            /// <summary>
            /// ���ӵ��Ҷ�
            /// </summary>
            RightX = 180,
            /// <summary>
            /// ����ÿ�еĲ���
            /// </summary>
            RowStep = 7,
            SmallRowStep = 20,
            /// <summary>
            /// ���ӵ�����
            /// </summary>
            RowLinesNum = 32,

            ColumnsMark1 = 35,
            /// <summary>
            /// CheckBoxƫ���ұ��ı��ľ���
            /// </summary>
            CheckShift = 15,
            /// <summary>
            /// �׻���ƫ���ı�����ľ���
            /// </summary>
            BottomLineShift = 15,

            BottomY = 1024,

            PrintWidth = 690,
            PrintWidth2 = 600,//710,
            PrintHeight = 920
        }
        #endregion

        #region ��ӡ������
        /// <summary>
        /// ��ӡ����֮����
        /// </summary>
        private abstract class clsEMR_VaginalExaminationRecordInfo : com.digitalwave.Utility.Controls.clsPrintLineBase
        {
            /// <summary>
            /// ���־�����ߵı߾� 
            /// </summary>
            protected int m_intPatientInfoX = 70;
            /// <summary>
            /// ������Ϣ
            /// </summary>
            protected clsPrintInfo_EMR_VaginalExaminationRecord m_objPrintInfo;

            /// <summary>
            /// 
            /// </summary>
            public override object m_ObjPrintLineInfo
            {
                get
                {
                    return base.m_blnHaveMoreLine;
                }
                set
                {
                    if (value != null)
                    {
                        this.m_objPrintInfo = (clsPrintInfo_EMR_VaginalExaminationRecord)value;
                    }
                }
            }

            /// <summary>
            /// ���ɴ�ӡ�����ı��飨p_strName���ȱ����p_strKey����һ�£�
            /// </summary>
            /// <param name="p_strName">
            /// ��ӡ�ֶ��������飬���ȱ����p_strKey����һ�¡�
            /// 1�����ӵ��ֶ���#��ͷ�����Լ����жϣ�������ӡ�������ݣ�
            /// 2��ֻ��ӡ���ݲ���ӡ�ֶε��ÿգ���""
            /// </param>
            /// <param name="p_strKey">��ӡ����</param>
            /// <param name="p_strKeyXml">��ӡ���ݶ�Ӧ��XML</param>
            /// <param name="p_strTextAll">�����ı�</param>
            /// <param name="p_strTextXML">XML�ı�</param>
            protected void m_mthMakeText(string[] p_strName, string[] p_strKey, string[] p_strKeyXml, ref string p_strTextAll, ref string p_strTextXML)
            {
                if (p_strName == null || p_strKey == null || p_strKeyXml == null || p_strKeyXml.Length != p_strKey.Length || p_strKeyXml.Length != p_strName.Length || p_strKey.Length != p_strName.Length)
                    return;

                string m_strCreadUserID = m_objPrintInfo.m_objRecordContent.m_strCreateUserID;
                string strFirstName = new clsEmployee(m_strCreadUserID).m_StrFirstName;
                string strSemicolonXML = "<root/>";
                bool blnIsFirst = true;
                string strXML = "";

                string m_strDateType = "";
                //ר�Ʋ����е�ctlRichTextBox�ڸñ������¼һ��ʱ����ӡҲ�������ɫ�޸ı�ǣ�
                //����ͨ����m_strMakedate���¸�ֵ���

                string m_strMakedate = m_objPrintInfo.m_objRecordContent.m_dtmFirstPrintDate.ToString("yyyy-MM-dd HH:mm:ss");
                for (int i = 0; i < p_strName.Length; i++)
                {
                    string strSam = "��";
                    if (p_strName[i].EndsWith("$$"))
                    {
                        strSam = "";
                        p_strName[i] = p_strName[i].Substring(0, p_strName[i].Length - 2);
                    }
                    strSemicolonXML = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strSam, m_strCreadUserID, strFirstName, Color.Black, m_strMakedate);
                    if (p_strKey[i] != "")
                    {

                        string m_strKey = p_strKey[i];
                        string m_strKeyXml = p_strKeyXml[i];
                        if (p_strName[i].StartsWith("#") == true)
                        {
                            p_strTextAll += p_strName[i].Substring(1);
                            strXML = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(p_strName[i].Substring(1), m_strCreadUserID, strFirstName, Color.Black, m_strMakedate);
                            if (p_strTextXML != "")
                                p_strTextXML = ctlRichTextBox.s_strCombineXml(new string[] { p_strTextXML, strXML });
                            else
                                p_strTextXML = strXML;
                            blnIsFirst = false;
                        }
                        else
                        {
                            if (m_strKeyXml == null || m_strKeyXml == "")
                            {
                                string strContent = "";
                                if (m_strDateType == "")
                                    strContent = m_strCheckDateType(m_strKey);
                                else
                                {
                                    strContent = m_strCheckDateType(m_strKey, m_strDateType);
                                    m_strDateType = "";
                                }
                                p_strTextAll += (blnIsFirst ? "" : strSam) + p_strName[i] + strContent;
                                strXML = blnIsFirst ? ctlRichTextBox.clsXmlTool.s_strMakeTextXml((p_strName[i] + strContent), m_strCreadUserID, strFirstName, Color.Black, m_strMakedate) :
                                    ctlRichTextBox.s_strCombineXml(new string[] { strSemicolonXML, ctlRichTextBox.clsXmlTool.s_strMakeTextXml((p_strName[i] + strContent), m_strCreadUserID, strFirstName, Color.Black, m_strMakedate) });
                                if (p_strTextXML != "")
                                    p_strTextXML = ctlRichTextBox.s_strCombineXml(new string[] { p_strTextXML, strXML });
                                else
                                    p_strTextXML = strXML;
                            }
                            else
                            {
                                p_strTextAll += ((blnIsFirst || p_strName[i] == "") ? "" : strSam) + p_strName[i] + m_strKey;
                                strXML = (blnIsFirst || p_strName[i] == "") ? ctlRichTextBox.s_strCombineXml(new string[] { ctlRichTextBox.clsXmlTool.s_strMakeTextXml(p_strName[i], m_strCreadUserID, strFirstName, Color.Black, m_strMakedate), m_strKeyXml }) :
                                    ctlRichTextBox.s_strCombineXml(new string[] { strSemicolonXML, ctlRichTextBox.clsXmlTool.s_strMakeTextXml(p_strName[i], m_strCreadUserID, strFirstName, Color.Black, m_strMakedate), m_strKeyXml });
                                if (p_strTextXML != "")
                                    p_strTextXML = ctlRichTextBox.s_strCombineXml(new string[] { p_strTextXML, strXML });
                                else
                                    p_strTextXML = strXML;
                            }
                            blnIsFirst = false;
                        }


                    }
                    else
                    {
                        p_strTextAll += (blnIsFirst ? "" : strSam) + p_strName[i];
                        strXML = blnIsFirst ? ctlRichTextBox.clsXmlTool.s_strMakeTextXml(p_strName[i], m_strCreadUserID, strFirstName, Color.Black, m_strMakedate) :
                            ctlRichTextBox.s_strCombineXml(new string[] { strSemicolonXML, ctlRichTextBox.clsXmlTool.s_strMakeTextXml(p_strName[i], m_strCreadUserID, strFirstName, Color.Black, m_strMakedate) });
                        if (p_strTextXML != "")
                            p_strTextXML = ctlRichTextBox.s_strCombineXml(new string[] { p_strTextXML, strXML });
                        else
                            p_strTextXML = strXML;
                        blnIsFirst = true;
                    }
                }
            }

            /// <summary>
            /// ����CheckBox�Ĵ�ӡ�ı���p_strName���ȱ����p_blnChecked���ȴ�һ��
            /// </summary>
            /// <param name="p_strName">CheckBox�������飬�����һ��Ϊ��ʶ���ӵڶ��ʼ���Ǽ�</param>
            /// <param name="p_blnChecked"></param>
            /// <param name="p_strTextAll">�����ı�</param>
            /// <param name="p_strTextXML">XML�ı�</param>
            protected void m_mthMakeCheckText(string[] p_strName, bool[] p_blnChecked, ref string p_strTextAll, ref string p_strTextXML)
            {
                if (p_strName == null || p_blnChecked == null || p_strName.Length <= 2 || p_blnChecked.Length <= 1)
                    return;
                bool blnPrintFirst = false;
                string m_strMakedate = m_objPrintInfo.m_objRecordContent.m_dtmFirstPrintDate.ToString("yyyy-MM-dd HH:mm:ss");
                string m_strCreadUserID = m_objPrintInfo.m_objRecordContent.m_strCreateUserID;
                string strFirstName = new clsEmployee(m_strCreadUserID).m_StrFirstName;
                string strDH_XML = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("��", m_strCreadUserID, strFirstName, Color.Black, m_strMakedate);
                p_strTextAll += p_strName[0];
                string strXML = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(p_strName[0], m_strCreadUserID, strFirstName, Color.Black, m_strMakedate);
                if (p_strTextXML != "")
                    p_strTextXML = ctlRichTextBox.s_strCombineXml(new string[] { p_strTextXML, strXML });
                else
                    p_strTextXML = strXML;
                for (int i = 1; i < p_strName.Length; i++)
                {
                    if (p_blnChecked[i - 1] == true)
                    {
                        string strText = p_strName[i];

                        p_strTextAll += (blnPrintFirst == true ? "��" : "") + strText;
                        p_strTextXML = ctlRichTextBox.s_strCombineXml(new string[] { p_strTextXML, (blnPrintFirst == true ? strDH_XML : "<root />"), ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strText, m_strCreadUserID, strFirstName, Color.Black, m_strMakedate) });
                        blnPrintFirst = true;
                    }
                }
                p_strTextAll += "��";
                p_strTextXML = ctlRichTextBox.s_strCombineXml(new string[] { p_strTextXML, ctlRichTextBox.clsXmlTool.s_strMakeTextXml("��", m_strCreadUserID, strFirstName, Color.Black, m_strMakedate) });
            }

            /// <summary>
            /// �������ڸ�ʽ
            /// </summary>
            /// <param name="p_strValue"></param>
            /// <returns></returns>
            private string m_strCheckDateType(string p_strValue)
            {
                if (p_strValue.Length < 8)
                    return p_strValue;
                string str = "";
                try
                {
                    str = DateTime.Parse(p_strValue).ToString("yyyy��MM��dd��");
                }
                catch { return p_strValue; }
                return str;
            }


            /// <summary>
            /// ����Ҫ�󷵻��������ڸ�ʽ
            /// </summary>
            /// <param name="p_strValue"></param>
            /// <param name="p_strDateType"></param>
            /// <returns></returns>
            private string m_strCheckDateType(string p_strValue, string p_strDateType)
            {
                if (p_strValue.Length < 8)
                    return p_strValue;
                string str = "";
                try
                {
                    str = DateTime.Parse(p_strValue).ToString(p_strDateType);
                }
                catch { return p_strValue; }
                return str;
            }
        }
        /// <summary>
        /// ���⼰���˻�����Ϣ
        /// </summary>
        private class clsEMR_VaginalExaminationRecordFixInfo : clsEMR_VaginalExaminationRecordInfo
        {
            /// <summary>
            /// �Ƿ��һ�δ�ӡ�����е�����
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            /// <summary>
            /// ����
            /// </summary>
            /// <param name="p_intPosY"></param>
            /// <param name="p_objGrp"></param>
            /// <param name="p_fntNormalText"></param>
            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
            {
                if (base.m_objPrintInfo != null && this.m_blnIsFirstPrint)
                {
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.LineAlignment = StringAlignment.Center;
                    // ҽԺ����

                    p_objGrp.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, m_fotHospitalTitle, Brushes.Black, new PointF(330f, 100f), stringFormat);
                    // ������
                    p_objGrp.DrawString("��������¼��", m_fotHeader, Brushes.Black, base.m_intPatientInfoX + 270, p_intPosY, stringFormat);
                    p_intPosY += 30;
                    stringFormat.Dispose();

                    p_objGrp.DrawString("������" + base.m_objPrintInfo.m_strPatientName, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX, p_intPosY);//115
                    p_objGrp.DrawString("�Ա�" + base.m_objPrintInfo.m_strSex, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 115, p_intPosY); // 90
                    p_objGrp.DrawString("���䣺" + (base.m_objPrintInfo.m_strAge == null ? "   ��" : base.m_objPrintInfo.m_strAge), p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 205, p_intPosY);//90

                    p_objGrp.DrawString("������" + base.m_objPrintInfo.m_strAreaName, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 295, p_intPosY); // 150
                    p_objGrp.DrawString("���ţ�" + base.m_objPrintInfo.m_strBedName, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 445, p_intPosY); // 85
                    p_objGrp.DrawString("סԺ�ţ�" + (base.m_objPrintInfo.m_strInPatentID == null ? "" : base.m_objPrintInfo.m_strInPatentID), p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 530, p_intPosY);//700
                    p_intPosY += 25;
                    // ��ӡ�߿�
                    p_objGrp.DrawRectangle(Pens.Black, (int)enmRectangleInfoEMR_VaginalExaminationRecord.LeftX, p_intPosY, (int)enmRectangleInfoEMR_VaginalExaminationRecord.PrintWidth, (int)enmRectangleInfoEMR_VaginalExaminationRecord.PrintHeight);
                    
                    this.m_blnIsFirstPrint = false;
                }
                base.m_blnHaveMoreLine = false;
            }
            /// <summary>
            /// ���ò���
            /// </summary>
            public override void m_mthReset()
            {
                this.m_blnIsFirstPrint = true;
                base.m_blnHaveMoreLine = true;
            }
        }
        /// <summary>
        /// ��ӡ��Ҫ��Ϣ
        /// </summary>
        private class clsEMR_VaginalExaminationRecordMain : clsEMR_VaginalExaminationRecordInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, clsEMR_CesareanRecordPrintTool.m_fotContent);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            /// <summary>
            /// ����
            /// </summary>
            /// <param name="p_intPosY"></param>
            /// <param name="p_objGrp"></param>
            /// <param name="p_fntNormalText"></param>
            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
            {
                string m_strAllText = "";
                string m_strAllXml = "";
                if (this.m_blnIsFirstPrint)
                {
                    clsEMR_VaginalExaminationValue m_objValue = m_objPrintInfo.m_objRecordContent;

                    m_mthMakeText(new string[] { "\n����ʱ�䣺", " ����ԭ��", "\n1.�������ţ�", "cm��$$", "  ��¶��", "  ��¶�ߵͣ�", "  ��λ��", " \n  ��", "��­���ص���$$", "������" },
                        new string[] { m_objValue.m_dtmEXAMINATIONTIME_DAT.ToString("yyyy��MM��dd�� hh:mm"), // ����ʱ��
                                       m_objValue.m_strEXAMINATIONREASON_VCHR, // ����ԭ��
                                       m_objValue.m_strMETREURYSIS_VCHR, // ��������
                                       "",      // cm
                                       m_objValue.m_strPRESENTATION_VCHR, // ��¶
                                       m_objValue.m_strPRESENTATIONHEIGHT_VCHR, // ��¶�ߵ�
                                       m_objValue.m_strPRESENTATIONORIENTATION_VCHR, // ��λ
                                       m_objValue.m_strSKULL_VCHR,          // ­���ص�
                                       m_objValue.m_strOVERLAPPING_VCHR, // ­���ص����
                                       m_objValue.m_strCAPUTSUCCEDANEUM_VCHR // ����
                        },
                        new string[]{ "", m_objValue.m_strEXAMINATIONREASON_XML, // ����ԭ��Xml
                                          m_objValue.m_strMETREURYSIS_XML, // ��������Xml
                                          "",                   // cm
                                          m_objValue.m_strPRESENTATION_XML, // ��¶Xml
                                          m_objValue.m_strPRESENTATIONHEIGHT_XML, // ��¶�ߵ�Xml
                                          m_objValue.m_strPRESENTATIONORIENTATION_XML, // ��¶��λXml
                                          m_objValue.m_strSKULL_XML, // ­���ص�XML
                                          m_objValue.m_strOVERLAPPING_XML, //  ­���ص����XML
                                          m_objValue.m_strCAPUTSUCCEDANEUM_XML  // ����XML
                                      
                        }, ref m_strAllText, ref m_strAllXml);

                    string m_strTemp;
                    string m_strTemp2;
                    string m_strTemp3;
                    if (m_objValue.m_strRUPTUREDFETALMEMBRANES_CHR == "10")
                    {
                        m_strTemp = " ����";
                        if (m_objValue.m_strRUPTUREDMODE_CHR == "10")
                            m_strTemp2 = " ��Ȼ";
                        else if (m_objValue.m_strRUPTUREDMODE_CHR == "01")
                            m_strTemp2 = " �˹�";
                        else m_strTemp2 = "";

                        m_strTemp3 = m_objValue.m_dtmRUPTURETIME_DAT.ToString("yyyy��MM��dd�� hh:mm");
                    }
                    else if (m_objValue.m_strRUPTUREDFETALMEMBRANES_CHR == "01")
                    {
                        m_strTemp = " δ��";
                        m_strTemp2 = "";
                        m_strTemp3 = "";
                    }
                    else
                    {
                        m_strTemp = "";
                        m_strTemp2 = "";
                        m_strTemp3 = "";
                    }
                    m_mthMakeText(new string[] { "\n2.̥Ĥ��", "��Ĥ��", "��Ĥʱ�䣺" }, new string[] { m_strTemp, m_strTemp2, m_strTemp3 }, new string[] { "", "", "" }, ref m_strAllText, ref m_strAllXml);
                    m_mthMakeText(new string[] { "\n  ��ˮ����$$", "ml�� $$", "��״��", " FHR��", "��/��$$" },
                                  new string[] { m_objValue.m_strAMNIOTICFLUID_VCHR, "", m_objValue.m_strAMNIOTICFLUIDCHARACTER_VCHR, m_objValue.m_strFHR_VCHR, "" },
                                  new string[] { m_objValue.m_strAMNIOTICFLUID_XML, "", m_objValue.m_strAMNIOTICFLUIDCHARACTER_XML, m_objValue.m_strFHR_XML, "" },
                                  ref m_strAllText, ref m_strAllXml);

                    m_strTemp = "";
                    m_strTemp2 = "";
                    m_strTemp3 = "";

                    if (m_objValue.m_strCOCCYX_CHR == "100")
                        m_strTemp = " ��";
                    else if (m_objValue.m_strCOCCYX_CHR == "010")
                        m_strTemp = " ��";
                    else if (m_objValue.m_strCOCCYX_CHR == "001")
                        m_strTemp = " ��";

                    if (m_objValue.m_strURETHRALCATHETERIZATION_CHR == "10")
                        m_strTemp2 = " ��";
                    else if (m_objValue.m_strURETHRALCATHETERIZATION_CHR == "01")
                        m_strTemp2 = " ��";
                    m_mthMakeText(new string[] { "\n3.����:���Ǽ���", "β�ǻ��ȣ�", "���ǻ�ȣ�", " �ܹǹ���", "��$$", "\n  DC����", "cm��$$", "�����м���", " ����", "��$$", "   ml ��״��$$", "��" },
                                  new string[] { m_objValue.m_strISCHIALSPINE_VCHR, m_strTemp, m_objValue.m_strSACRALBONE_VCHR, m_objValue.m_strPUBICARCH_VCHR, "", m_objValue.m_strDC_VCHR, "", m_objValue.m_strISCHIUMNOTCH_VCHR, m_strTemp2, m_objValue.m_strPISS_VCHR, m_objValue.m_strUCCHARACTER_VCHR, "" },
                                  new string[] { m_objValue.m_strISCHIALSPINE_XML, "", m_objValue.m_strSACRALBONE_XML, m_objValue.m_strPUBICARCH_XML, "", m_objValue.m_strDC_XML, "", m_objValue.m_strISCHIUMNOTCH_XML, "", m_objValue.m_strPISS_XML, m_objValue.m_strUCCHARACTER_XML, "" },
                                  ref m_strAllText, ref m_strAllXml);

                    m_mthMakeText(new string[] { "\n����ƻ���" }, new string[] { m_objValue.m_strPROJECT_VCHR }, new string[] { m_objValue.m_strPROJECT_XML }, ref m_strAllText, ref m_strAllXml);
                                        
                    this.m_blnIsFirstPrint = false;
                }
                else
                {
                    base.m_blnHaveMoreLine = false;
                    return;
                }

                m_objPrintContext.m_mthSetContextWithCorrectBefore(m_strAllText, m_strAllXml, m_objPrintInfo.m_dtmFirstPrintDate);
                //m_mthAddSign2("����ߣ�", m_objPrintContext.m_ObjModifyUserArr);

                if (this.m_objPrintContext.m_BlnHaveNextLine())
                {
                    int m_intRealHeight;
                    Rectangle m_printRtg = new Rectangle(base.m_intPatientInfoX + 30, p_intPosY, (int)enmRectangleInfoEMR_VaginalExaminationRecord.PrintWidth2, 30);
                    this.m_objPrintContext.m_blnPrintAllBySimSun(12, m_printRtg, p_objGrp, out m_intRealHeight,false);  //  .m_mthPrintLine((int)enmRectangleInfoEMR_VaginalExaminationRecord.PrintWidth2, base.m_intPatientInfoX + 70, p_intPosY, p_objGrp);
                    if (m_intRealHeight > 30)
                        p_intPosY += m_intRealHeight;
                    else
                        p_intPosY += 30;
                }
                else
                    base.m_blnHaveMoreLine = false;
            }
            /// <summary>
            /// ���ò���
            /// </summary>
            public override void m_mthReset()
            {
                this.m_objPrintContext.m_mthRestartPrint();
                base.m_blnHaveMoreLine = true;
                this.m_blnIsFirstPrint = true;
            }
        }

        private class clsEMR_VaginalExaminationRecordSign : clsEMR_VaginalExaminationRecordInfo
        {
            /// <summary>
            /// �Ƿ��һ�δ�ӡ�����е�����
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            /// <summary>
            /// ����
            /// </summary>
            /// <param name="p_intPosY"></param>
            /// <param name="p_objGrp"></param>
            /// <param name="p_fntNormalText"></param>
            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    string m_strSign = "����ߣ�";
                    p_objGrp.DrawString(m_strSign, m_fotSign, Brushes.Black, (float)(enmRectangleInfoEMR_VaginalExaminationRecord.LeftX + 400), (float)p_intPosY);
                    if(m_objPrintInfo.m_objRecordContent.objSignerArr != null && m_objPrintInfo.m_objRecordContent.objSignerArr.Length > 0)
                    {
                        clsEmrSigns_VO[] m_objSignArr = m_objPrintInfo.m_objRecordContent.objSignerArr;
                        m_strSign = "";
                        for (int i = 0; i < m_objSignArr.Length; i++)
                        {
                            if (i < m_objSignArr.Length - 1)
                            {
                                m_strSign += m_objSignArr[i].objEmployee.ToString() + "��\n";
                            }
                            else
                            {
                                m_strSign += m_objSignArr[i].objEmployee.ToString();
                            }
                        }
                        p_objGrp.DrawString(m_strSign, m_fotSign, Brushes.Black, (float)(enmRectangleInfoEMR_VaginalExaminationRecord.LeftX + 450), (float)p_intPosY);
                    } 
                    m_blnIsFirstPrint = false;
                }
                base.m_blnHaveMoreLine = false;
            }
            /// <summary>
            /// ���ò���
            /// </summary>
            public override void m_mthReset()
            {
                this.m_blnIsFirstPrint = true;
                base.m_blnHaveMoreLine = true;
            }
        }
        #endregion PrintClasses

        #region infPrintRecord ��Ա

        /// <summary>
        /// ��ʼ����ӡ���õ��Ĳ�����Ϣ
        /// </summary>
        /// <param name="p_objPatient"></param>
        /// <param name="p_dtmInPatientDate"></param>
        /// <param name="p_dtmOpenDate"></param>
        public void m_mthSetPrintInfo(clsPatient p_objPatient, DateTime p_dtmInPatientDate, DateTime p_dtmOpenDate)
        {
            this.m_blnIsFromDataSource = true;//�����Ǵ����ݿ��ȡ
            clsPatient m_objPatient = p_objPatient;
            this.m_objPrintInfo = new clsPrintInfo_EMR_VaginalExaminationRecord();
            this.m_objPrintInfo.m_strInPatentID = m_objPatient != null ? m_objPatient.m_StrInPatientID : "";
            this.m_objPrintInfo.m_strPatientName = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrFirstName : "";
            this.m_objPrintInfo.m_strSex = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrSex : "";
            this.m_objPrintInfo.m_strAge = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrAge : "";
            this.m_objPrintInfo.m_strBedName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName : "";
            this.m_objPrintInfo.m_strDeptName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjDept.m_StrDeptName : "";
            this.m_objPrintInfo.m_strAreaName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName : "";
            this.m_objPrintInfo.m_dtmInPatientDate = p_dtmInPatientDate;
            this.m_objPrintInfo.m_dtmOpenDate = p_dtmOpenDate;
            this.m_objPrintInfo.m_strHISInPatientID = m_objPatient != null ? m_objPatient.m_StrHISInPatientID : "";
            this.m_objPrintInfo.m_dtmHISInPatientDate = m_objPatient != null ? m_objPatient.m_DtmSelectedHISInDate : DateTime.MinValue;
            this.m_objPrintInfo.m_strRegisterId = p_objPatient.m_StrRegisterId;
        }
        /// <summary>
        /// ��ӡ 1 ��ʼ����ӡ����
        /// ��ӡ�����ݿ��ȡ������
        /// </summary>
        public void m_mthInitPrintContent()
        {
            this.m_blnWantInit = false;
            if (this.m_objPrintInfo == null)
            {
                clsPublicFunction.ShowInformationMessageBox("��ִ��m_mthInitPrintContent֮ǰ����ִ��m_mthSetPrintInfo����");
                return;
            }

            clsDiseaseTrackDomain m_objRecordsDomain = new clsDiseaseTrackDomain(enmDiseaseTrackType.EMR_VaginalExamination);

            //if (!string.IsNullOrEmpty(this.m_objPrintInfo.m_strInPatentID) && this.m_objPrintInfo.m_dtmOpenDate != DateTime.MinValue)
            if (!string.IsNullOrEmpty(this.m_objPrintInfo.m_strInPatentID))
            {
                clsTrackRecordContent objContent = new clsTrackRecordContent();
                //�����ݿ��ȡ��ӡ��������
                long lngRes = m_objRecordsDomain.m_lngGetRecordContent(this.m_objPrintInfo.m_strInPatentID, this.m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), this.m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"), out objContent);

                if (lngRes <= 0)
                    return;
                this.m_objPrintInfo.m_objRecordContent = objContent as clsEMR_VaginalExaminationValue;
            }
            //m_objRecordsDomain = null;
            //���ñ����ݵ���ӡ��			
            this.m_mthSetPrintContent(this.m_objPrintInfo.m_objRecordContent, this.m_objPrintInfo.m_dtmFirstPrintDate);
        }
        /// <summary>
        /// ���ô�ӡ����
        /// </summary>
        /// <param name="p_objContent"></param>
        /// <param name="p_dtmFirstPrintDate"></param>
        private void m_mthSetPrintContent(clsEMR_VaginalExaminationValue p_objContent, DateTime p_dtmFirstPrintDate)
        {
            this.m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext
                (
                    new com.digitalwave.Utility.Controls.clsPrintLineBase[]
                    {
                       new clsEMR_VaginalExaminationRecordFixInfo(),
                       new clsEMR_VaginalExaminationRecordMain(),
                       new clsEMR_VaginalExaminationRecordSign()
                    }
                );
            this.m_objPrintLineContext.m_ObjPrintSign = new com.digitalwave.Utility.Controls.clsPrintRecordSign();
            //���ô�ӡ��Ϣ������Set Value��ȥ

            this.m_objPrintLineContext.m_ObjPrintLineInfo = this.m_objPrintInfo;
            //�����ݿ��ó�����FirstPrintDate����ÿ����ӡ�������m_DtmFirstPrintTime���ڸ���������
            this.m_objPrintLineContext.m_DtmFirstPrintTime = p_dtmFirstPrintDate;
        }
        /// <summary>
        /// ��ӡ 2 ��ʼ����ӡ����
        /// ��ӡ����Ҫ�����ݿ��ȡ���Ѵ��ڵ����ݣ��롰��ӡ 1�� �Ĺ��̲�ͬʱִ��
        /// </summary>
        /// <param name="p_objPrintContent"></param>
        public void m_mthSetPrintContent(object p_objPrintContent)
        {
            //this.m_blnWantInit = false;
            //if (p_objPrintContent.GetType().Name != "clsPrintInfo_EMR_CesareanRecord")
            //{
            //    MDIParent.ShowInformationMessageBox("��������");
            //}
            //m_blnIsFromDataSource = false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ
            //this.m_objPrintInfo.m_objRecordContent = (clsPrintInfo_EMR_CesareanRecord)p_objPrintContent;

            //m_mthSetPrintContent(m_objPrintInfo.m_objRecordContent, m_objPrintInfo.m_dtmFirstPrintDate);
        }
        /// <summary>
        /// ��ȡ�ô�ӡ����ʵ���Ĵ�ӡ����
        /// </summary>
        /// <returns></returns>
        public object m_objGetPrintInfo()
        {
            if (this.m_blnIsFromDataSource)
            {
                if (this.m_objPrintInfo == null)
                {
                    MDIParent.ShowInformationMessageBox("��ӡ��δ��ʼ��������ִ��m_mthSetPrintInfo������");
                    return null;
                }

                if (this.m_blnWantInit)
                    this.m_mthInitPrintContent();
            }

            //û�м�¼����ʱ�����ؿ�
            if (this.m_objPrintInfo.m_objRecordContent == null)
                return null;
            else
                return this.m_objPrintInfo;
        }
        /// <summary>
        /// ��ʼ����ӡ�������һЩ���ԣ��磺��ӡ��������ʹ�õ����塢��ˢ��
        /// </summary>
        /// <param name="p_objArg"></param>
        public void m_mthInitPrintTool(object p_objArg)
        {
            //this.m_objPageSetting = new clsPrintPageSettingForRecord();
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="p_objArg"></param>
        public void m_mthDisposePrintTools(object p_objArg)
        {
        }
        /// <summary>
        /// ��ʼ��ӡ
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        public void m_mthBeginPrint(object p_objPrintArg)
        {
            //m_mthBeginPrintSub((PrintEventArgs)p_objPrintArg);
        }
        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        public void m_mthPrintPage(object p_objPrintArg)
        {
            this.m_mthPrintPageSub((PrintPageEventArgs)p_objPrintArg);
        }
        /// <summary>
        /// ������ӡ
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        public void m_mthEndPrint(object p_objPrintArg)
        {
            this.m_mthEndPrintSub((PrintEventArgs)p_objPrintArg);
            if (!this.m_blnIsFromDataSource || string.IsNullOrEmpty(this.m_objPrintInfo.m_strInPatentID))
                return;
            //�����ӡ�ɹ�������������Ҫ���µ�ʱ�䣬����У�����ʱ�䡣 
            if (!((PrintEventArgs)p_objPrintArg).Cancel && this.m_objPrintInfo.m_blnIsFirstPrint)
            {
                clsDiseaseTrackDomain m_objRecordsDomain = new clsDiseaseTrackDomain(enmDiseaseTrackType.EMR_VaginalExamination);
                m_objRecordsDomain.m_lngUpdateFirstPrintDate(this.m_objPrintInfo.m_strInPatentID,
                                                                    this.m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                                                    this.m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                                                    this.m_objPrintInfo.m_dtmFirstPrintDate);
            }
        }

        #endregion

        #region ��ӡ����
        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="p_objPrintPageArg"></param>
        private void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
        {
            //���ڴ���Ӵ�ӡ��ͷҳβ�Ĵ���
            //���д�ӡ
            while (this.m_objPrintLineContext.m_BlnHaveMoreLine)
            {
                //��ҳ��ӡ�����������д�ӡ�У�����ҳ�Ѵ���
                // p_objPrintPageArg.MarginBounds.Height PageBounds
                if ((this.m_intYPos > (p_objPrintPageArg.PageBounds.Height - 150)) && this.m_objPrintLineContext.m_BlnHaveMoreLine)
                {
                    this.m_mthPrintFoot(p_objPrintPageArg);
                    p_objPrintPageArg.HasMorePages = true;
                    this.m_intYPos = 130;//155;
                    this.m_intCurrentPage++;
                    return;
                }
                //��ӡ��
                Pen newPen = new Pen(Brushes.Black);
                newPen.Width = 2;
                //��ӡ�߿�
                //p_objPrintPageArg.Graphics.DrawRectangle(newPen, 50, 129, p_objPrintPageArg.PageBounds.Right - 50, p_objPrintPageArg.PageBounds.Bottom - 100);
                this.m_objPrintLineContext.m_mthPrintNextLine(ref this.m_intYPos, p_objPrintPageArg.Graphics, clsEMR_VaginalExaminationRecordPrintTool.m_fotContent);
            }
            //��ӡǩ��
            this.m_intYPos += 20;
            while (this.m_objPrintLineContext.m_BlnHaveMoreSign)
            {
                this.m_objPrintLineContext.m_mthPrintNextSign(70 + 10, this.m_intYPos, p_objPrintPageArg.Graphics, clsEMR_VaginalExaminationRecordPrintTool.m_fotSign);
                m_intYPos += 20;
            }

            //��ӡҳ�ţ����˸��ĵ�ȫ������	
            this.m_mthPrintFoot(p_objPrintPageArg);
        }
        /// <summary>
        /// ��ӡҳ��
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintFoot(PrintPageEventArgs e)
        {
            float fltOffsetX = 20;//X��ƫ����
            e.Graphics.DrawString("��      ҳ", clsEMR_VaginalExaminationRecordPrintTool.m_fotFooter, Brushes.Black, 385 - fltOffsetX, e.PageBounds.Height - 70);
            e.Graphics.DrawString(this.m_intCurrentPage.ToString(), clsEMR_VaginalExaminationRecordPrintTool.m_fotFooter, Brushes.Black, 425 - fltOffsetX, e.PageBounds.Height - 70);
        }
        /// <summary>
        /// ���ò���
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        private void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
        {
            this.m_objPrintLineContext.m_mthReset();
            this.m_intYPos = 155;
            this.m_intCurrentPage = 1;
        }
        #endregion

        #region IDisposable ��Ա

        public void Dispose()
        {
            GC.Collect();//ǿ��ִ����������
        }

        #endregion
    }
}
