using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.Windows.Forms;
using System.Collections;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// clsMutiSampleReportPrint ��ժҪ˵����
    /// </summary>
    public class clsMutiSampleReportPrint : com.digitalwave.GUI_Base.clsController_Base, infPrintRecord
    {
        #region ��������
        private long m_lngWidthPage;//��ӡҳ�Ŀ��
        private long m_lngY;//��ǰY��������

        private float m_fltLeftIndentProp;//����������
        private float m_fltRightIndentProp;//����������

        private Font m_fntTitle;
        private Font m_fntSmallBold;
        private Font m_fntSmallNotBold;
        private Font m_fntSmall2NotBold;

        /// �߿򻭱�
        /// </summary>
        private Pen m_GridPen;

        public System.Data.DataTable m_dtbSample = null;//������Ϣ������
        public System.Data.DataTable m_dtbResult;//������
        public string m_strTitle = "";//���浥����

        public bool m_blnFinishPrint = false;//��ӡ������־
        #endregion

        #region ���캯��
        public clsMutiSampleReportPrint()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            m_strTitle = this.m_objComInfo.m_strGetHospitalTitle();
        }
        #endregion

        #region ��ӡ������ʼ����
        /// <summary>
        /// ��ӡ������ʼ����
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        private void m_mthPrintStart(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
        {
            m_lngWidthPage = p_objPrintArg.PageBounds.Width;

            //m_strTitle="��ɽ�еڶ�����ҽԺ���鱨�浥";
            SizeF szTitle = p_objPrintArg.Graphics.MeasureString(m_strTitle, m_fntTitle);
            float fltCurrentX = m_lngWidthPage / 2 - (long)szTitle.Width / 2;//�����ı����Ͻǵ�X������
            m_lngY = 10;//�����ı����Ͻ�Y������
            p_objPrintArg.Graphics.DrawString(m_strTitle, m_fntTitle, Brushes.Black, fltCurrentX, m_lngY);

            //1
            m_lngY = 20 + (int)szTitle.Height;
            SizeF szTmp = p_objPrintArg.Graphics.MeasureString("����:", m_fntSmallBold);
            fltCurrentX = m_lngWidthPage * m_fltLeftIndentProp;
            p_objPrintArg.Graphics.DrawString("����:", m_fntSmallBold, Brushes.Black, fltCurrentX, m_lngY);
            fltCurrentX += szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(m_dtbSample.Rows[0]["patient_name_vchr"].ToString().Trim(), m_fntSmallNotBold, Brushes.Black,
                fltCurrentX, m_lngY);

            szTmp = p_objPrintArg.Graphics.MeasureString("����:", m_fntSmallBold);
            fltCurrentX = m_lngWidthPage * (m_fltLeftIndentProp + (1 - m_fltLeftIndentProp - m_fltRightIndentProp) * 0.8f / 4);
            p_objPrintArg.Graphics.DrawString("����:", m_fntSmallBold, Brushes.Black, fltCurrentX, m_lngY);
            fltCurrentX += szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(m_dtbSample.Rows[0]["deptname_vchr"].ToString().Trim(), m_fntSmallNotBold, Brushes.Black,
                fltCurrentX, m_lngY);


            szTmp = p_objPrintArg.Graphics.MeasureString("סԺ��:", m_fntSmallBold);
            fltCurrentX = m_lngWidthPage * (m_fltLeftIndentProp + (1 - m_fltLeftIndentProp - m_fltRightIndentProp) * 0.82f / 2);
            p_objPrintArg.Graphics.DrawString("סԺ��:", m_fntSmallBold, Brushes.Black, fltCurrentX, m_lngY);
            fltCurrentX += szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(m_dtbSample.Rows[0]["PATIENT_INHOSPITALNO_CHR"].ToString().Trim(), m_fntSmallNotBold, Brushes.Black,
                fltCurrentX, m_lngY);


            szTmp = p_objPrintArg.Graphics.MeasureString("�ͼ�ҽ��:", m_fntSmallBold);
            fltCurrentX = m_lngWidthPage * (m_fltLeftIndentProp + (1 - m_fltLeftIndentProp - m_fltRightIndentProp) * 2.8f / 4);
            p_objPrintArg.Graphics.DrawString("�ͼ�ҽ��:", m_fntSmallBold, Brushes.Black, fltCurrentX, m_lngY);
            fltCurrentX += szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(m_dtbSample.Rows[0]["applyer"].ToString().Trim(), m_fntSmallNotBold, Brushes.Black,
                fltCurrentX, m_lngY);


            //2
            m_lngY = 25 + (int)szTitle.Height + (int)m_fntSmallBold.Height;
            szTmp = p_objPrintArg.Graphics.MeasureString("�Ա�:", m_fntSmallBold);
            fltCurrentX = m_lngWidthPage * m_fltLeftIndentProp;
            p_objPrintArg.Graphics.DrawString("�Ա�:", m_fntSmallBold, Brushes.Black, fltCurrentX, m_lngY);
            fltCurrentX += szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(m_dtbSample.Rows[0]["sex_chr"].ToString().Trim(), m_fntSmallNotBold, Brushes.Black,
                fltCurrentX, m_lngY);


            szTmp = p_objPrintArg.Graphics.MeasureString("����:", m_fntSmallBold);
            fltCurrentX = m_lngWidthPage * (m_fltLeftIndentProp + (1 - m_fltLeftIndentProp - m_fltRightIndentProp) * 0.8f / 4);
            p_objPrintArg.Graphics.DrawString("����:", m_fntSmallBold, Brushes.Black, fltCurrentX, m_lngY);
            fltCurrentX += szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(m_dtbSample.Rows[0]["age_chr"].ToString().Trim(), m_fntSmallNotBold, Brushes.Black,
                fltCurrentX, m_lngY);

            szTmp = p_objPrintArg.Graphics.MeasureString("����:", m_fntSmallBold);
            fltCurrentX = m_lngWidthPage * (m_fltLeftIndentProp + (1 - m_fltLeftIndentProp - m_fltRightIndentProp) * 0.82f / 2);
            p_objPrintArg.Graphics.DrawString("����:", m_fntSmallBold, Brushes.Black, fltCurrentX, m_lngY);
            fltCurrentX += szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(m_dtbSample.Rows[0]["bedno_chr"].ToString().Trim(), m_fntSmallNotBold, Brushes.Black,
                fltCurrentX, m_lngY);

            szTmp = p_objPrintArg.Graphics.MeasureString("�ͼ�ʱ��:", m_fntSmallBold);
            fltCurrentX = m_lngWidthPage * (m_fltLeftIndentProp + (1 - m_fltLeftIndentProp - m_fltRightIndentProp) * 2.8f / 4);
            p_objPrintArg.Graphics.DrawString("�ͼ�ʱ��:", m_fntSmallBold, Brushes.Black, fltCurrentX, m_lngY);
            fltCurrentX += szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(DateTime.Parse(m_dtbSample.Rows[0]["accept_dat"].ToString().Trim()).ToString("yyyy-MM-dd"),
                m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);


            //3
            m_lngY = 30 + (int)szTitle.Height + (int)m_fntSmallBold.Height * 2;
            szTmp = p_objPrintArg.Graphics.MeasureString("�ٴ����:", m_fntSmallBold);
            fltCurrentX = m_lngWidthPage * m_fltLeftIndentProp;
            p_objPrintArg.Graphics.DrawString("�ٴ����:", m_fntSmallBold, Brushes.Black, fltCurrentX, m_lngY);
            fltCurrentX += szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(m_dtbSample.Rows[0]["diagnose_vchr"].ToString().Trim(), m_fntSmallNotBold, Brushes.Black,
                fltCurrentX, m_lngY);

        }

        private void m_mthPrintLine(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
        {
            m_lngY += 2 + m_fntSmallBold.Height;
            long intYStart = m_lngY;

            //������
            p_objPrintArg.Graphics.DrawLine(m_GridPen, m_lngWidthPage * 0.08f, intYStart, m_lngWidthPage * 0.92f, intYStart);

        }
        #endregion

        #region ��ӡ����
        /// <summary>
        /// ��ӡ���浥�м䲿��
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        private void m_mthPrintMiddle(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
        {

            try
            {
                ArrayList arlTempLeft = new ArrayList();//��Ŵ�ӡ��������Group
                ArrayList arlTempRight = new ArrayList();//��Ŵ�ӡ��������Group


                #region ����GROUPID_CHR�ӽ�������еõ�����Group����ʱ���� arlTempLeft ��
                ArrayList arlGroupID = new ArrayList();
                //��ȡ���еı��������
                for (int i = 0; i < m_dtbResult.Rows.Count; i++)
                {
                    bool blnHasSameGroup = false;
                    for (int j = 0; j < arlGroupID.Count; j++)
                    {
                        if (m_dtbResult.Rows[i]["GROUPID_CHR"].ToString().Trim() == arlGroupID[j].ToString().Trim())
                        {
                            blnHasSameGroup = true;
                            break;
                        }
                    }
                    if (!blnHasSameGroup)
                    {
                        string strGroupID = m_dtbResult.Rows[i]["GROUPID_CHR"].ToString().Trim();
                        arlGroupID.Add(strGroupID);
                    }
                }
                //��Ÿ�������
                for (int i = 0; i < arlGroupID.Count; i++)
                {
                    DataView dtvGroupData = new DataView(this.m_dtbResult);

                    dtvGroupData.RowFilter = "GROUPID_CHR = '" + arlGroupID[i].ToString().Trim() + "'";
                    if (dtvGroupData.Count > 0)
                    {
                        clsGroup objGroup = new clsGroup();
                        objGroup.m_dtvGroupData = dtvGroupData;
                        objGroup.m_dtvGroupData.Sort = "REPORT_PRINT_SEQ_INT ASC,SAMPLE_PRINT_SEQ_INT ";
                        objGroup.m_strGroupName = arlGroupID[i].ToString().Trim();
                        arlTempLeft.Add(objGroup);
                    }
                }
                #endregion

                //				this.m_mthDistributeGroup(ref arlTempLeft,ref arlTempRight);

                float fltLeftX = m_lngWidthPage * m_fltLeftIndentProp - 10f;//������ӡ�� X ��ʼ��
                float fltRightX = m_lngWidthPage * 0.43f;//������ӡ�� X ��ʼ��
                float fltLeftY = m_lngY;//������ӡ�� Y ��ʼ��
                float fltRightY = m_lngY;//������ӡ�� Y ��ʼ��
                float fltWidth = m_lngWidthPage * (1 - m_fltLeftIndentProp * 2) / 2 + 10f;// �����Ŀ�� 
                string strGroupName;

                #region ��ӡ�걾��Ϣ
                float fltEndSampleInfoY;
                this.m_mthPrintSampleInfo(p_objPrintArg, fltLeftX, fltLeftY, fltWidth, out fltEndSampleInfoY);
                fltLeftY = fltEndSampleInfoY;
                #endregion

                #region ��ӡGroup
                for (int i = 0; i < arlTempLeft.Count; i++)
                {
                    clsGroup objGroup = (clsGroup)arlTempLeft[i];
                    float fltEndY;
                    strGroupName = objGroup.m_dtvGroupData[0].Row["PRINT_TITLE_VCHR"].ToString().Trim();
                    this.m_mthPrintGroup(p_objPrintArg, objGroup.m_dtvGroupData, strGroupName, fltRightX, fltRightY, fltWidth, out fltEndY);
                    fltRightY = fltEndY;
                }
                #endregion

                float fltBodyEndY = 0;
                if (fltLeftY >= fltRightY)
                {
                    fltBodyEndY = fltLeftY;
                }
                else
                {
                    fltBodyEndY = fltRightY;
                }

                this.m_lngY = (long)fltBodyEndY;
            }
            catch { }
        }

        /// <summary>
        /// ��ӡ�걾��Ϣ
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        /// <param name="p_fltX"></param>
        /// <param name="p_fltY"></param>
        /// <param name="p_fltWidth"></param>
        /// <param name="p_fltEndY"></param>
        private void m_mthPrintSampleInfo(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg, float p_fltX, float p_fltY, float p_fltWidth, out float p_fltEndY)
        {
            p_fltEndY = 0;
            try
            {
                p_fltY += 6;
                float[] fltColumnXArr = new float[3];
                fltColumnXArr[0] = p_fltX + 8f;
                p_objPrintArg.Graphics.DrawString("������", m_fntSmallBold, Brushes.Black, fltColumnXArr[0], p_fltY);

                fltColumnXArr[1] = p_fltX + p_fltWidth * 0.25f + 8f;
                p_objPrintArg.Graphics.DrawString("����������", m_fntSmallBold, Brushes.Black, fltColumnXArr[1], p_fltY);

                fltColumnXArr[2] = p_fltX + p_fltWidth * 0.58f;
                p_objPrintArg.Graphics.DrawString("��������", m_fntSmallBold, Brushes.Black, fltColumnXArr[2], p_fltY);

                //��λY������
                float fltCurrentY = p_fltY + 2;
                for (int i = 0; i < m_dtbSample.Rows.Count; i++)
                {
                    fltCurrentY += 1 + m_fntSmall2NotBold.Height;
                    p_objPrintArg.Graphics.DrawString(m_dtbSample.Rows[i]["sample_id_chr"].ToString().Trim(), m_fntSmall2NotBold, Brushes.Black,
                        fltColumnXArr[0], fltCurrentY);
                    p_objPrintArg.Graphics.DrawString(m_dtbSample.Rows[i]["device_sampleid_chr"].ToString().Trim(), m_fntSmall2NotBold, Brushes.Black,
                        fltColumnXArr[1], fltCurrentY);
                    p_objPrintArg.Graphics.DrawString(m_dtbSample.Rows[i]["SAMPLE_TYPE_DESC_VCHR"].ToString().Trim(), m_fntSmall2NotBold, Brushes.Black,
                        fltColumnXArr[2], fltCurrentY);
                }
                fltCurrentY += 4 + m_fntSmallBold.Height;
                p_fltEndY = fltCurrentY;
            }
            catch { }
        }

        /// <summary>
        /// ��ӡ Group
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        /// <param name="p_dtvGroupData"></param>
        /// <param name="p_strGroupName"></param>
        /// <param name="p_fltX"></param>
        /// <param name="p_fltY"></param>
        /// <param name="p_fltWidth"></param>
        /// <param name="p_fltEndY"></param>
        private void m_mthPrintGroup(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg, System.Data.DataView p_dtvGroupData, string p_strGroupName, float p_fltX, float p_fltY, float p_fltWidth, out float p_fltEndY)
        {
            p_fltEndY = 0;
            try
            {
                p_fltY += 6;
                float[] fltColumnXArr = new float[3];
                fltColumnXArr[0] = p_fltX + 8f;
                p_objPrintArg.Graphics.DrawString(p_strGroupName, m_fntSmallBold, Brushes.Black, fltColumnXArr[0], p_fltY);

                fltColumnXArr[1] = p_fltX + p_fltWidth * 0.32f + 8f;
                p_objPrintArg.Graphics.DrawString(" ���", m_fntSmallBold, Brushes.Black, fltColumnXArr[1], p_fltY);

                fltColumnXArr[2] = p_fltX + p_fltWidth * 0.6f + 15f;
                p_objPrintArg.Graphics.DrawString("�ο���Χ", m_fntSmallBold, Brushes.Black, fltColumnXArr[2], p_fltY);


                //��ӡ�걾��Ŀ���� 2004.06.03
                p_dtvGroupData.Sort = "REPORT_PRINT_SEQ_INT ASC,SAMPLE_PRINT_SEQ_INT ";

                //��λY������
                float fltCurrentY = p_fltY + 2;
                #region ������ӡ��¼
                for (int i = 0; i < p_dtvGroupData.Count; i++)
                {
                    string strResult = p_dtvGroupData[i].Row["result_vchr"].ToString().Trim();
                    string strAbnormal = p_dtvGroupData[i].Row["ABNORMAL_FLAG_CHR"].ToString().Trim();
                    string strUnit = p_dtvGroupData[i].Row["UNIT_VCHR"].ToString().Trim();
                    string strRefRange = p_dtvGroupData[i].Row["refrange_vchr"].ToString() + " " + strUnit;
                    string strMinVal = p_dtvGroupData[i].Row["MIN_VAL_DEC"].ToString().Trim();
                    string strMaxVal = p_dtvGroupData[i].Row["MAX_VAL_DEC"].ToString().Trim();
                    string strCheckItemName = p_dtvGroupData[i].Row["RPTNO_CHR"].ToString().Trim();


                    fltCurrentY += 1 + m_fntSmall2NotBold.Height;
                    p_objPrintArg.Graphics.DrawString(strCheckItemName, m_fntSmall2NotBold, Brushes.Black, fltColumnXArr[0], fltCurrentY);
                    //��ӡ���X���λ��
                    fltColumnXArr[1] = p_fltX + p_fltWidth * 0.32f - 10f;
                    #region ��ӡ ָʾ��ͷ
                    //1.�����쳣��־�ж�,�˴���Ϊ�쳣��־ֻ��"H"(��)��"L"(��)�������
                    if (strAbnormal != null)
                    {
                        System.Drawing.Font objBoldFont = new Font("SimSun", 9, FontStyle.Bold);
                        string strPR;
                        if (strAbnormal == "H")
                        {
                            strPR = strResult + " " + "��";
                            strPR = strPR.PadLeft(10);
                            p_objPrintArg.Graphics.DrawString(strPR, objBoldFont, Brushes.Black, fltColumnXArr[1], fltCurrentY);
                        }
                        else if (strAbnormal == "L")
                        {
                            if (strResult.Contains(">") || strResult.Contains("<"))
                                strPR = strResult + " " + "��";
                            else
                                strPR = strResult + " " + "��";
                            strPR = strPR.PadLeft(10);
                            p_objPrintArg.Graphics.DrawString(strPR, objBoldFont, Brushes.Black, fltColumnXArr[1], fltCurrentY);
                        }
                        else
                        {
                            strPR = strResult + " " + " ";
                            strPR = strPR.PadLeft(10);

                            p_objPrintArg.Graphics.DrawString(strPR, m_fntSmall2NotBold, Brushes.Black, fltColumnXArr[1], fltCurrentY);
                        }
                    }
                    #endregion

                    p_objPrintArg.Graphics.DrawString(strRefRange, m_fntSmallNotBold, Brushes.Black, fltColumnXArr[2], fltCurrentY);

                }
                #endregion
                fltCurrentY += 4 + m_fntSmallBold.Height;
                p_fltEndY = fltCurrentY;
            }
            catch { }
        }
        #endregion

        #region ���ﲿ��
        //���ﲿ��
        private void m_mthPrintSummary(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
        {
            SizeF szPerWord = p_objPrintArg.Graphics.MeasureString("��", m_fntSmallNotBold);//��ȡһ���ַ��Ŀ��;


            p_objPrintArg.Graphics.DrawString("ʵ������ʾ��", m_fntSmallBold, Brushes.Black, m_lngWidthPage * 0.08f, m_lngY + 4);
            m_lngY = m_lngY + m_fntSmallBold.Height + 4;
            long CurrentY = m_lngY;
            if (m_dtbSample.Rows[0]["SUMMARY_VCHR"] != null && m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim() != "")
            {
                float fltLeftX = m_lngWidthPage * m_fltLeftIndentProp + szPerWord.Width + 8;
                float fltRightX = m_lngWidthPage * 0.92f - fltLeftX;
                CurrentY += 4;
                long lngEndY = CurrentY + m_fntSmallNotBold.Height * 2 + 3;
                Rectangle rectSummary = new Rectangle((int)fltLeftX, (int)CurrentY, (int)fltRightX, (int)lngEndY);
                new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, m_fntSmallNotBold).m_mthPrintText(m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim(),
                    m_dtbSample.Rows[0]["XML_SUMMARY_VCHR"].ToString().Trim(), m_fntSmallNotBold, Color.Black, rectSummary, p_objPrintArg.Graphics);
                CurrentY = lngEndY;
            }
            m_lngY += m_fntSmallNotBold.Height * 2 + 4;
        }
        #endregion

        #region ��ӡ���浥��β����
        /// <summary>
        /// ��ӡ���浥��β����
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        private void m_mthPrintEnd(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
        {
            // ����ҽ��
            string strCheckEmp = m_dtbSample.Rows[0]["reportor"].ToString().Trim();
            // ���ҽ�� ����ʱ�Ǽ���ҽ����
            string strConfirmEmp = m_dtbSample.Rows[0]["reportor"].ToString().Trim();

            m_lngY += 10;

            float fltCurrent = m_lngWidthPage * m_fltLeftIndentProp;
            p_objPrintArg.Graphics.DrawString("(�����������ٴ����Ʋο���ֻ�Ըü��ı걾����!)", m_fntSmallNotBold, Brushes.Black, fltCurrent, m_lngY);
            m_lngY += m_fntSmallNotBold.Height + 6;
            //����
            p_objPrintArg.Graphics.DrawLine(m_GridPen, m_lngWidthPage * 0.08f, m_lngY, m_lngWidthPage * 0.92f, m_lngY);

            m_lngY += 6;
            p_objPrintArg.Graphics.DrawString("��������:", m_fntSmallBold, Brushes.Black, fltCurrent, m_lngY);
            string strReportDate = "";
            if (Microsoft.VisualBasic.Information.IsDate(m_dtbSample.Rows[0]["report_dat"]))
            {
                strReportDate = ((DateTime)m_dtbSample.Rows[0]["report_dat"]).ToString("yyyy-MM-dd").Trim();
            }
            SizeF szTmp = p_objPrintArg.Graphics.MeasureString("��������:", m_fntSmallBold);
            fltCurrent += szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(strReportDate, m_fntSmallBold, Brushes.Black, fltCurrent, m_lngY);


            p_objPrintArg.Graphics.DrawString("����ҽ��:", m_fntSmallBold, Brushes.Black, m_lngWidthPage * (m_fltLeftIndentProp + (1 - m_fltLeftIndentProp - m_fltRightIndentProp) / 3), m_lngY);

            fltCurrent = m_lngWidthPage * (m_fltLeftIndentProp + (1 - m_fltLeftIndentProp - m_fltRightIndentProp) / 3) + szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(strCheckEmp, m_fntSmallBold, Brushes.Black, fltCurrent, m_lngY);

            fltCurrent = m_lngWidthPage * (m_fltLeftIndentProp + (1 - m_fltLeftIndentProp - m_fltRightIndentProp) * 2 / 3) + szTmp.Width * 3 / 4 + 4;
            p_objPrintArg.Graphics.DrawString("�����:", m_fntSmallBold, Brushes.Black, fltCurrent, m_lngY);
            szTmp = p_objPrintArg.Graphics.MeasureString("�����:", m_fntSmallBold);
            fltCurrent = fltCurrent + szTmp.Width + 4;
            p_objPrintArg.Graphics.DrawString(strCheckEmp, m_fntSmallBold, Brushes.Black, fltCurrent, m_lngY);


        }
        #endregion

        #region ��ӡ���浥
        private void m_mthPrintPageSub(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
        {
            m_mthPrintStart(p_objPrintArg);
            m_mthPrintLine(p_objPrintArg);
            m_mthPrintMiddle(p_objPrintArg);
            m_mthPrintSummary(p_objPrintArg);
            m_mthPrintEnd(p_objPrintArg);
            m_blnFinishPrint = true;
        }
        #endregion

        #region �̳д�ӡ�ӿ�
        /// <summary>
        /// ��ʼ����ӡ����
        /// </summary>
        /// <param name="p_objArg">�ⲿ��Ҫ��ʼ���ı��������ݲ�ͬ��ʵ��ʹ��</param>
        public void m_mthInitPrintTool(object p_objArg)
        {
            m_fntTitle = new Font("SimSun", 18, FontStyle.Bold);
            m_fntSmallBold = new Font("SimSun", 11, FontStyle.Bold);
            m_fntSmallNotBold = new Font("SimSun", 10f, FontStyle.Regular);
            m_fntSmall2NotBold = new Font("SimSun", 9f, FontStyle.Regular);

            m_GridPen = new Pen(Color.Black, 1);

            m_fltLeftIndentProp = 0.1f;
            m_fltRightIndentProp = 0.1f;

            #region ��ӡ����
            try
            {
                PaperSize ps = null;
                foreach (PaperSize objPs in ((System.Drawing.Printing.PrintDocument)p_objArg).PrinterSettings.PaperSizes)
                {
                    if (objPs.PaperName == "LIS_Report")
                    {
                        ps = objPs;
                        break;
                    }
                }
                if (ps != null)
                {
                    ((System.Drawing.Printing.PrintDocument)p_objArg).DefaultPageSettings.PaperSize = ps;
                }

            }
            catch
            {
                MessageBox.Show("��ӡ�����ϣ�", "iCare", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            #endregion

        }

        /// <summary>
        /// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���
        /// </summary>
        public void m_mthInitPrintContent()
        {

        }

        /// <summary>
        /// �ͷŴ�ӡ����
        /// </summary>
        /// <param name="p_objArg">�ⲿʹ�õ��ı��������ݲ�ͬ��ʵ��ʹ��</param>
        public void m_mthDisposePrintTools(object p_objArg)
        {
        }

        /// <summary>
        /// ��ӡ��ʼ
        /// </summary>
        /// <param name="p_objPrintArg">��ӡ�ĵ�</param>
        public void m_mthBeginPrint(object p_objPrintArg)
        {
            #region ��ʼ����ӡ����
            m_dtbSample = ((clsPrintValuePara)p_objPrintArg).m_dtbBaseInfo;
            m_dtbResult = ((clsPrintValuePara)p_objPrintArg).m_dtbResult;
            m_strTitle = ((clsPrintValuePara)p_objPrintArg).m_strTitle;
            #endregion
        }

        /// <summary>
        /// ��ӡ��
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        public void m_mthPrintPage(object p_objPrintArg)
        {
            m_mthPrintPageSub((PrintPageEventArgs)p_objPrintArg);
        }

        /// <summary>
        /// ��ӡ������һ��ʹ�������������ݿ���Ϣ��
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        public void m_mthEndPrint(object p_objPrintArg)
        {

        }
        #endregion
    }
}
