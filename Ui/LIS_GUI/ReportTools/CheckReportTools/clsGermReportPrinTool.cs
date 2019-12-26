using System;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// clsLisReportPrint ��ժҪ˵����
    /// </summary>
    public class clsGermReportPrinTool : infPrintRecord
    {
        #region ���浥����
        private float m_fltPaperWidth;        //��ӡֽ�ŵĿ��
        private float m_fltPaperHeight;       //��ӡֽ�ŵĸ߶�
        private float m_fltPrintWidth;        //��ӡ����Ŀ��
        private float m_fltPrintHeight;       //��ӡ����ĸ߶�
        private float m_fltStartX;            //���浥X��ʼλ��
        private float m_fltEndY;              //���浥�ײ���Ϣλ��
        private float m_fltTitleSpace;        //���浥���ӡ������
        private float m_fltItemSpace;         //���浥���ӡ��Ŀ���
        private float m_fltImgSpace;          //ͼ�δ�ӡ���

        //ͼ�����ű���
        private float m_fltXRate = 0.8f;
        private float m_fltYRate = 0.8f;

        private string m_strPatientName = "����:";
        private string m_strSex = "�Ա�:";
        private string m_strAge = "����:";
        private string m_strInPatientNo = "סԺ��:";
        private string m_strDepartment = "����:";
        private string m_strBedNo = "����:";
        private string m_strSampleType = "��������:";
        private string m_strApplyDoc = "�ͼ�ҽ��:";
        private string m_strDiagnose = "�ٴ����:";
        private string m_strSampleID = "������:";
        private string m_strCheckNo = "������:";
        private string m_strCheckDate = "�ͼ�����:";
        private string m_strSummary = "ҩ�����:";
        private string m_strNotice = "ף�����彡��!�˱�����Լ��걾����,�����ҽ���ο�!";
        private string m_strAnnotation = "��ע:";
        private string m_strReportDate = "��������:";
        private string m_strCheckDoc = "����ҽ��:";
        private string m_strConfirmEmp = "�����:";
        private string m_strResult = "���";
        private string m_strReference = "�ο�ֵ";

        //��������
        private Font m_fntTitle;
        private Font m_fntSmallBold;
        private Font m_fntSmallNotBold;
        private Font m_fntSmall2NotBold;
        private Font m_fntHeadNotBold;
        private Font m_fntSmall2Bold;
        private Font m_fntsamll3NotBold;

        //���浥����
        public DataTable m_dtbSample;
        public DataTable m_dtbResult;

        //��ӡ��������
        clsCommonPrintMethod m_printMethodTool;

        //Yλ�ö�λ
        private float m_fltY;

        private bool m_blnDocked = true; //��ӡ�ײ���Ϣ�̶���

        //��ӡ��Ϣ��ҳ
        clsPrintPerPageInfo[] m_objPrintPage;

        //ָʾ��ǰ��ӡҳ��
        private int m_intCurrentPageIdx = 0;
        private int m_intTotalPage = 0;

        //ʵ������ʾ�͸�עΪ��ʱ�Ƿ���ʾ
        bool m_blnSummaryEmptyVisible = false;
        bool m_blnAnnotationEmptyVisible = false;
        /// <summary>
        /// �Ƿ��ӡ���
        /// </summary>
        public static bool blnSurePrintDiagnose = false;
        #endregion

        #region �ײ���ӡ��Ϣ�̶�����
        public bool IsDocked
        {
            get
            {
                return m_blnDocked;
            }
            set
            {
                m_blnDocked = value;
            }
        }
        #endregion


        #region ���캯��

        private Image objImage; //ҽԺͼ��
        public clsGermReportPrinTool()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //	
            //if (p_strParmValue == "1")
            //{
            //    blnSurePrintDiagnose = true;
            //}
            //else
            //{
            //    blnSurePrintDiagnose = false;
            //}
            string m_strStartUp = System.Windows.Forms.Application.StartupPath + "\\Picture\\��ݸ��ɽҽԺͼ��.jpg"; //��ȡ��ӡͼ��

            objImage = Image.FromFile(m_strStartUp, false);
        }
        #endregion

        #region ��ӡ�ӿڳ�ʼ������
        private void m_mthInitalPrintTool(PrintDocument p_printDoc)
        {
            //��ȡֽ�ŵĿ�͸�
            m_fltPaperWidth = p_printDoc.DefaultPageSettings.Bounds.Width;
            m_fltPaperHeight = p_printDoc.DefaultPageSettings.Bounds.Height;

            //���ô�ӡ����Ŀ�͸�
            m_fltPrintWidth = m_fltPaperWidth * 0.8f;
            m_fltPrintHeight = m_fltPaperHeight * 0.9f;
            m_fltStartX = m_fltPaperWidth * 0.1f;
            m_fltEndY = m_fltPaperHeight - 114;

            //���ñ��浥���ӡ���
            m_fltTitleSpace = 5;
            m_fltItemSpace = 2;
            m_fltImgSpace = 10;

            //���ô�ӡ����
            m_fntTitle = new Font("SimSun", 18, FontStyle.Bold);
            m_fntSmallBold = new Font("SimSun", 11, FontStyle.Bold);
            m_fntSmallNotBold = new Font("SimSun", 10f, FontStyle.Regular);
            m_fntSmall2NotBold = new Font("SimSun", 9f, FontStyle.Regular);
            m_fntHeadNotBold = new Font("SimSun", 11f, FontStyle.Regular);
            m_fntSmall2Bold = new Font("SimSun", 10, FontStyle.Bold);
            m_fntsamll3NotBold = new Font("SimSun", 8f, FontStyle.Regular);

        }
        #endregion

        #region ����ͼ��
        private Image m_imgDrawGraphic(byte[] p_bytGraph, string p_strImageFormat)
        {
            Image img = null;
            System.IO.MemoryStream ms = null;
            try
            {
                ms = new System.IO.MemoryStream(p_bytGraph);
                img = Image.FromStream(ms, true);
                string strFormat = (p_strImageFormat == null) ? null : p_strImageFormat.ToLower();
                switch (strFormat)
                {
                    case "lisb":
                        System.Drawing.Bitmap bm = new Bitmap(20, img.Height);
                        Graphics g = Graphics.FromImage(bm);
                        g.DrawImage(img, 0, 0, bm.Width, bm.Height);
                        img.Dispose();
                        img = bm;
                        break;
                    default:
                        break;
                }
            }
            catch
            {
            }
            finally
            {
                if (ms != null)
                    ms.Close();
            }
            return img;
        }
        #endregion

        #region ��ӡ���浥������Ϣ
        private void m_mthPrintBseInfo()
        {
            if (m_dtbSample == null)
                return;

            m_fltY = 5;
            float fltColumn1 = m_fltStartX;
            float fltColumn2 = m_fltPaperWidth * 0.25f;
            float fltColumn3 = m_fltPaperWidth * 0.45f;
            float fltColumn4 = m_fltPaperWidth * 0.62f;


            //ͼ��
            m_printMethodTool.m_mthPrintImage(objImage, fltColumn1, m_fltY);

            //string m_strTitleImg = m_dtbSample.Rows[0]["print_title_vchr"].ToString().Trim().Remove
            //    (m_dtbSample.Rows[0]["print_title_vchr"].ToString().Trim().Length - 5);

            string m_strTitleImg = "�� ݸ �� �� ɽ ҽ Ժ";
            string m_strTitleImgEng = "ChaShan Hospital of DongGuang";

            //ҽԺ����
            m_printMethodTool.m_mthDrawString(m_strTitleImg, m_fntSmallBold, fltColumn1 + objImage.Width, m_fltY + 16);

            //Ӣ��
            m_printMethodTool.m_mthDrawString(m_strTitleImgEng, m_fntsamll3NotBold, fltColumn1 + objImage.Width, m_fltY + 30);

            m_fltY += 5 + objImage.Height;

            string m_strTitle = m_dtbSample.Rows[0]["print_title_vchr"].ToString().Trim().Substring
                (m_dtbSample.Rows[0]["print_title_vchr"].ToString().Trim().Length - 5);
            //if (m_dtbSample.Rows[0]["report_print_chr"] != System.DBNull.Value)
            //{
            //    string strTime = m_dtbSample.Rows[0]["report_print_chr"].ToString().Trim();
            //    int intTime = 0;
            //    try
            //    {
            //        intTime = Convert.ToInt32(strTime);
            //        if (intTime > 0)
            //        {
            //            m_strTitle = m_dtbSample.Rows[0]["print_title_vchr"].ToString() + "(�ش�)";
            //        }
            //    }
            //    catch
            //    { }
            //}


            //DrawTitle
            m_printMethodTool.m_mthPrintTitle(m_strTitle, m_fntTitle, m_fltY, m_fltPaperWidth);

            //Locate Y
            m_fltY += 10 + m_printMethodTool.m_fltGetStringHeight(m_dtbSample.Rows[0]["print_title_vchr"].ToString().Trim(), m_fntTitle);

            m_printMethodTool.m_mthDrawLine(m_fltStartX - 5, m_fltY, m_fltPaperWidth * 0.9f, m_fltY);
            //Locate Y
            m_fltY += 5;

            //����
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntHeadNotBold, m_strPatientName,
                m_dtbSample.Rows[0]["patient_name_vchr"].ToString().Trim(), fltColumn1, m_fltY);


            //�Ա�
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold,
                m_strSex, m_dtbSample.Rows[0]["sex_chr"].ToString().Trim(), fltColumn2, m_fltY);

            //����
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold,
                m_strAge, m_dtbSample.Rows[0]["age_chr"].ToString().Trim(), fltColumn3, m_fltY);

            //סԺ�š����￨�š�����
            string strPatientType = m_dtbSample.Rows[0]["patient_type_chr"].ToString().Trim();
            string strPrintContent = null;
            switch (strPatientType)
            {
                case "2":
                    m_strInPatientNo = "���ƿ���:";
                    strPrintContent = m_dtbSample.Rows[0]["patientcardid_chr"].ToString().Trim();
                    break;

                case "3":
                    m_strInPatientNo = "����:";
                    strPrintContent = m_dtbSample.Rows[0]["patient_inhospitalno_chr"].ToString().Trim();
                    break;

                default:
                    m_strInPatientNo = "סԺ��:";
                    strPrintContent = m_dtbSample.Rows[0]["patient_inhospitalno_chr"].ToString().Trim();
                    break;
            }


            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strInPatientNo,
                strPrintContent, fltColumn4, m_fltY);

            //Locate Y
            m_fltY += 5 + m_printMethodTool.m_fltGetStringHeight(m_strSampleID, m_fntSmallBold);


            //��  ��
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strDepartment,
                m_dtbSample.Rows[0]["deptname_vchr"].ToString().Trim(), fltColumn1, m_fltY);


            //��  ��
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strBedNo,
                m_dtbSample.Rows[0]["bedno_chr"].ToString().Trim(), fltColumn2, m_fltY);

            //��������
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strSampleType,
                m_dtbSample.Rows[0]["sample_type_desc_vchr"].ToString().Trim(), fltColumn3, m_fltY);

            //������
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strCheckNo,
                m_dtbSample.Rows[0]["check_no_chr"].ToString().Trim(), fltColumn4, m_fltY);


            //Locate Y
            m_fltY += 5 + m_printMethodTool.m_fltGetStringHeight(m_strSampleID, m_fntSmallBold);


            m_printMethodTool.m_mthDrawLine(m_fltStartX - 5, m_fltY, m_fltPaperWidth * 0.9f, m_fltY);

            m_fltY += 5;
        }
        #endregion

        #region ��ӡ���浥ʵ������ʾ
        private float m_fltPrintSummary(float p_fltX, float p_fltY, float p_fltPrintWidth)
        {
            if (!m_blnSummaryEmptyVisible && m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim() == "")
                return p_fltY;
            float fltY = p_fltY + 10;
            string strSummary = m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim();
            m_printMethodTool.m_mthDrawString(m_strSummary, m_fntSmallBold, p_fltX, fltY);
            fltY += m_fntSmallBold.Height + m_fltTitleSpace;
            SizeF sf = m_rectGetPrintStringRectangle(m_fntSmallBold, m_fntSmallNotBold, strSummary, p_fltPrintWidth, m_fltTitleSpace,
                m_fltItemSpace);
            Rectangle rectSummary = new Rectangle((int)p_fltX, (int)fltY, (int)sf.Width, (int)sf.Height);
            new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, m_fntSmallNotBold).m_mthPrintText(m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim(),
                m_dtbSample.Rows[0]["XML_SUMMARY_VCHR"].ToString().Trim(), m_fntSmallNotBold, Color.Black, rectSummary, m_printMethodTool.m_printEventArg.Graphics);
            fltY += rectSummary.Height;
            return fltY;
        }
        #endregion

        #region ��ӡ���浥�ײ���Ϣ
        private void m_mthPrintEnd()
        {
            if (m_blnDocked)
            {
                if (m_fltY < m_fltEndY)
                {
                    m_fltY = m_fltEndY;
                }
            }
            m_fltY += 10;
            //Notice
            m_printMethodTool.m_mthDrawString(m_strNotice, m_fntSmallNotBold, m_fltStartX, m_fltY);
            float fltNoticeWidth = m_printMethodTool.m_fltGetStringWidth(m_strNotice, m_fntSmallNotBold);
            //��ע
            bool blnPrintAnnotation = false;
            if (m_dtbSample.Rows[0]["ANNOTATION_VCHR"].ToString().Trim() != "" || m_blnAnnotationEmptyVisible)
            {
                blnPrintAnnotation = true;
            }
            if (blnPrintAnnotation)
            {
                m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallNotBold, m_fntSmallNotBold, m_strAnnotation, m_dtbSample.Rows[0]["ANNOTATION_VCHR"].ToString().Trim(),
                    m_fltStartX + fltNoticeWidth, m_fltY);
            }
            m_fltY += m_printMethodTool.m_fltGetStringHeight(m_strAnnotation, m_fntSmallNotBold) + 3;
            //����
            m_printMethodTool.m_mthDrawLine(m_fltStartX - 5, m_fltY, m_fltPaperWidth * 0.9f, m_fltY);

            m_fltY += 6;

            //column
            float fltColumn1 = m_fltStartX;
            float fltColumn2 = m_fltPaperWidth * 1.4f / 3;
            float fltColumn3 = m_fltPaperWidth * 2.1f / 3;

            //��������
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strReportDate, m_dtbSample.Rows[0]["CONFIRM_DAT"].ToString().ToString(),
                fltColumn1, m_fltY);
            //����ҽ��
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strCheckDoc, m_dtbSample.Rows[0]["reportor"].ToString().Trim(), fltColumn2,
                m_fltY);
            //�����
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strConfirmEmp, m_dtbSample.Rows[0]["confirmer"].ToString().Trim(), fltColumn3,
                m_fltY);
        }

        //��ݸ��ɽʽҳβ
        private void m_mthPrintEnd_DGCS()
        {
            if (m_blnDocked)
            {
                if (m_fltY < m_fltEndY)
                {
                    m_fltY = m_fltEndY;
                }
            }
            m_fltY += 3;

            //����
            //m_printMethodTool.m_mthDrawLine(m_fltStartX - 5, m_fltY, m_fltPaperWidth * 0.9f, m_fltY);

            m_fltY += 6;

            //column
            float fltColumn1 = m_fltStartX;
            float fltColumn2 = m_fltPaperWidth * 1.4f / 3;
            float fltColumn3 = m_fltPaperWidth * 2.1f / 3;

            //��������
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strReportDate, m_dtbSample.Rows[0]["CONFIRM_DAT"].ToString().ToString(),
                fltColumn1, m_fltY);
            //����ҽ��
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strCheckDoc, m_dtbSample.Rows[0]["reportor"].ToString().Trim(), fltColumn2,
                m_fltY);
            //�����
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strConfirmEmp, m_dtbSample.Rows[0]["confirmer"].ToString().Trim(), fltColumn3,
                m_fltY);

            m_fltY += m_printMethodTool.m_fltGetStringHeight(m_strReportDate, m_fntSmallBold) + 6;

            //����
            m_printMethodTool.m_mthDrawLine(m_fltStartX - 5, m_fltY, m_fltPaperWidth * 0.9f, m_fltY);
            m_fltY += 6;

            //Notice
            m_printMethodTool.m_printEventArg.Graphics.DrawString(m_strNotice, new Font("SimSun", 11f, FontStyle.Regular), Brushes.Red, m_fltStartX, m_fltY);
            //m_printMethodTool.m_mthDrawString(m_strNotice, m_fntSmallNotBold, m_fltStartX, m_fltY);
            float fltNoticeWidth = m_printMethodTool.m_fltGetStringWidth(m_strNotice, new Font("SimSun", 11f, FontStyle.Regular));
            //��ע
            bool blnPrintAnnotation = false;
            if (m_dtbSample.Rows[0]["annotation_vchr"].ToString().Trim() != "" || m_blnAnnotationEmptyVisible)
            {
                blnPrintAnnotation = true;
            }
            if (blnPrintAnnotation)
            {
                m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallNotBold, m_fntSmallNotBold, m_strAnnotation, m_dtbSample.Rows[0]["annotation_vchr"].ToString().Trim(),
                    m_fltStartX + fltNoticeWidth, m_fltY);
            }
        }

        #endregion

        #region ��ӡҳ��Ϣ
        private void m_mthPrintDetail()
        {
            string strSummary = m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim();
            SizeF sf = m_rectGetPrintStringRectangle(m_fntSmallBold, m_fntSmallNotBold, strSummary, m_fltPrintWidth, m_fltTitleSpace,
                m_fltItemSpace);
            if (m_objPrintPage == null)
            {
                m_objPrintPage = m_objConstructPrintPageInfo(m_dtbResult, m_fltStartX, m_fltY, m_fltPrintWidth
                    , m_fltPaperHeight - 123 - (m_fltPaperHeight - m_fltEndY) - sf.Height, m_fltPaperHeight - 123 - (m_fltPaperHeight - m_fltEndY));
                m_intTotalPage = m_objPrintPage.Length;
            }
            if (m_intCurrentPageIdx == m_objPrintPage.Length - 1)
            {
                m_printMethodTool.m_printEventArg.HasMorePages = false;
            }
            else
            {
                m_printMethodTool.m_printEventArg.HasMorePages = true;
            }
            if (m_objPrintPage[m_intCurrentPageIdx].m_objSampleArr != null)
            {
                //��ӡ�������
                float fltY = m_fltPrintGroupData(m_objPrintPage[m_intCurrentPageIdx].m_objSampleArr);
                if (fltY != -1)
                    m_fltY = fltY;
            }
            if (m_objPrintPage[m_intCurrentPageIdx].m_imgArr != null)
            {
                //��ӡͼ������
                float fltY = m_fltPrintImageArr(m_objPrintPage[m_intCurrentPageIdx].m_imgArr);
                if (fltY != -1)
                    m_fltY = fltY;
            }
            if (m_printMethodTool.m_printEventArg.HasMorePages == false)
            {
                m_fltY = m_fltPrintSummary(m_fltStartX, m_fltY, m_fltPrintWidth);
            }
        }
        #endregion

        #region ��ӡ������
        //��ӡ������
        private float m_fltPrintGroupData(clsSampleResultInfo[] p_objArr)
        {
            float fltY = 0;
            if (p_objArr == null)
                return -1;
            bool blnHasTwoPart = false;
            if (p_objArr[p_objArr.Length - 1].m_fltX > m_fltStartX)
                blnHasTwoPart = true;
            float[] fltColumnArr = null;
            float fltResultPrintWidth;
            if (blnHasTwoPart)
            {
                fltColumnArr = new float[] { m_fltPrintWidth * 0.21f, m_fltPrintWidth * 0.31f };
                fltResultPrintWidth = (fltColumnArr[1] - fltColumnArr[0]) * 0.9f;
            }
            else
            {
                fltColumnArr = new float[] { m_fltPrintWidth * 0.25f, m_fltPrintWidth * 0.42f };
                fltResultPrintWidth = (fltColumnArr[1] - fltColumnArr[0]) * 0.5f;
            }

            float fltBseY;
            float fltTitleHeight = m_fltGetPrintElementHeight(m_fntSmallBold, m_fltTitleSpace);
            float fltItemHeight = m_fltGetPrintElementHeight(m_fntSmall2NotBold, m_fltTitleSpace);
            for (int i = 0; i < p_objArr.Length; i++)
            {
                fltBseY = p_objArr[i].m_fltY;
                float fltColumn1 = p_objArr[i].m_fltX;
                float fltColumn2 = fltColumn1 + fltColumnArr[0];
                float fltColumn3 = fltColumn1 + fltColumnArr[1];

                //��ӡ����
                m_printMethodTool.m_mthDrawString(p_objArr[i].m_strPrintTitle, m_fntSmallBold, fltColumn1, fltBseY);
                m_printMethodTool.m_mthDrawString(m_strResult, m_fntSmallBold, fltColumn2, fltBseY);
                //				m_printMethodTool.m_mthDrawString(m_strReference,m_fntSmallBold,fltColumn3,fltBseY);
                fltBseY += fltTitleHeight;
                for (int j = 0; j < p_objArr[i].m_intCount; j++)
                {
                    string strResult = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["result_vchr"].ToString().Trim();
                    string strAbnormal = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["ABNORMAL_FLAG_CHR"].ToString().Trim();
                    string strUnit = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["UNIT_VCHR"].ToString().Trim();
                    string strRefRange = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["refrange_vchr"].ToString() + " " + strUnit;
                    string strCheckItemName = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["RPTNO_CHR"].ToString().Trim();

                    //��ӡ��Ŀ
                    m_printMethodTool.m_mthDrawString(strCheckItemName, m_fntSmall2NotBold, fltColumn1, fltBseY);

                    //�쳣��־
                    if (strAbnormal != null)
                    {
                        System.Drawing.Font objBoldFont = new Font("SimSun", 9, FontStyle.Bold);
                        string strPR;

                        strPR = strResult + " " + "��";
                        float fltResultWidth = m_printMethodTool.m_fltGetStringWidth(strPR, objBoldFont);

                        if (strAbnormal == "H")
                        {
                            strPR = strResult + " " + "��";
                            float fltStartPos = fltColumn2 + fltResultPrintWidth - fltResultWidth;
                            m_printMethodTool.m_mthDrawString(strPR, objBoldFont, fltStartPos, fltBseY);
                        }
                        else if (strAbnormal == "L")
                        {
                            if (strResult.Contains(">") || strResult.Contains("<"))
                                strPR = strResult + " " + "��";
                            else
                                strPR = strResult + " " + "��";
                            float fltStartPos = fltColumn2 + fltResultPrintWidth - fltResultWidth;
                            m_printMethodTool.m_mthDrawString(strPR, objBoldFont, fltStartPos, fltBseY);
                        }
                        else
                        {
                            strPR = strResult + " " + " ";
                            float fltStartPos = fltColumn2 + fltResultPrintWidth - fltResultWidth;
                            m_printMethodTool.m_mthDrawString(strPR, m_fntSmall2NotBold, fltStartPos, fltBseY);
                        }
                    }
                    m_printMethodTool.m_mthDrawString(strRefRange, m_fntSmall2NotBold, fltColumn3, fltBseY);

                    //Locate Y 
                    fltBseY += m_fntSmall2NotBold.Height + m_fltItemSpace;
                    if (fltY < fltBseY)
                    {
                        fltY = fltBseY;
                    }
                }
            }
            return fltY;
        }
        #endregion

        #region ��ӡͼ��
        private float m_fltPrintImageArr(clsPrintImage[] p_objArr)
        {
            float fltY = 0;
            if (p_objArr == null)
                return -1;
            for (int i = 0; i < p_objArr.Length; i++)
            {
                m_printMethodTool.m_printEventArg.Graphics.DrawImage(p_objArr[i].m_img, p_objArr[i].m_fltX,
                    p_objArr[i].m_fltY, p_objArr[i].m_fltWidth, p_objArr[i].m_fltHeight);
                if (fltY < p_objArr[i].m_fltY + p_objArr[i].m_fltHeight)
                {
                    fltY = p_objArr[i].m_fltY + p_objArr[i].m_fltHeight;
                }
            }
            return fltY;
        }
        #endregion

        #region �����ַ������С
        //�����ַ������С
        private SizeF m_rectGetPrintStringRectangle(Font p_fntTitle, Font p_fntContent, string p_strContent, float p_fltWidth, float p_fltTitleSpace,
            float p_fltItemSpace)
        {
            if ((p_strContent == "" || p_strContent == null) && !m_blnSummaryEmptyVisible)
            {
                return new SizeF(0, 0);
            }
            float fltTitleHeight = p_fntTitle.Height;
            float fltContentHeight = p_fntContent.Height;
            float fltHeight = 0;
            if (p_strContent != null && p_strContent != "")
            {
                SizeF sfString = m_printMethodTool.m_printEventArg.Graphics.MeasureString(p_strContent, p_fntContent);
                fltHeight = (sfString.Width / p_fltWidth + 1) * fltContentHeight;
            }
            else
            {
                fltHeight = fltTitleHeight + p_fltTitleSpace + fltContentHeight;
            }
            SizeF sf = new SizeF(p_fltWidth, fltHeight);
            return sf;
        }
        #endregion

        #region ��ҳ����
        //�Ⱦ���������ȷ��ӡ��������С���Ѿ����˳�������ݺ�ͼ�����ݣ���ȷ�걾��ĸ����Լ������걾��Ĵ�ӡ�����С��˳��
        //������������DataTable����ӡ��ʼ��XYλ��(fltX,fltY)���Լ���ӡ�Ŀ�Ⱥ͸߶�(fltWidth,fltHeight)
        //          (DataTable p_dtbResult,float p_fltX,float p_fltY,float p_fltWidth,float p_fltHeight)
        //���������clsPrintPerPageInfo[]
        //���̣�
        //0 ���˳�������ݺ�ͼ�����ݣ���������걾���ӡ��Ϣ��clsSampleResultInfo�����Լ���ʼ��ͼ����Ϣ��clsPrintImage��
        //1 ��˳���ӡ�걾��������
        //1.1 �ж�������Ϣ�����ܷ�һҳ����
        //1.1.1 Y GOTO 2
        //1.1.2 N ��ҳ GoTo 1.1
        //2 ������ݴ�ӡ��ɣ��ж��Ƿ���ͼ������
        //2.1 Y �жϵ�ǰҳ���Ƿ�λ�ô�ӡͼ��
        //2.1.1 Y �жϵ�ǰҳ�ܷ��ӡ�����е�ͼ��
        //2.1.1.1 Y ��ӡ GoTo 2.2
        //2.1.1.2 N ��ӡ ��ҳ GoTo 2.1
        //2.1.2 N �жϵ�ǰҳ���Ƿ�λ�ô�ӡͼ��
        //2.1.2.1 Y ��ӡ
        //2.1.2.2 N ��ҳ GoTo 2.1
        //2.2 N ��ӡ���������ز���

        private clsPrintPerPageInfo[] m_objConstructPrintPageInfo(DataTable p_dtbResult, float p_fltX, float p_fltY,
            float p_fltWidth, float p_fltHeight, float p_fltMaxHeight)
        {
            //���˳�������ݺ�ͼ������
            DataView dtvData = m_dtvFilterRows(p_dtbResult, "IS_GRAPH_RESULT_NUM = 0");
            DataView dtvImage = m_dtvFilterRows(p_dtbResult, "IS_GRAPH_RESULT_NUM = 1");

            //����
            dtvData.Sort = "REPORT_PRINT_SEQ_INT ASC,GROUPID_CHR ASC,SAMPLE_PRINT_SEQ_INT ASC";

            // 
            clsSampleResultInfo[] objDataArr = m_objConstructSampleResultArr(dtvData);
            clsPrintImage[] objImgArr = m_objConstructPrintImage(dtvImage);

            #region xing.chen add 2005.9.22
            float fltImgHeight = 0;
            if (objImgArr != null && objImgArr.Length > 0)
            {
                fltImgHeight = objImgArr[0].m_fltHeight + 10;
            }
            #endregion

            int intPage = 0;

            //��ӡ���ҳ
            ArrayList arlPageData = new ArrayList();

            #region ������ݴ�ӡ��ҳ
            float fltLeft = 0;
            float fltRight = 0;
            float fltTitleHeight = m_fltGetPrintElementHeight(m_fntSmallBold, m_fltTitleSpace);
            float fltItemHeight = m_fltGetPrintElementHeight(m_fntSmall2NotBold, m_fltItemSpace);
            //��¼��ҳʣ��ļ�¼����
            int intTotalLeftItemCount = dtvData.Count;
            float fltHeight = 0;
            if (intTotalLeftItemCount * fltItemHeight + objDataArr.Length * fltTitleHeight <= (p_fltHeight - fltImgHeight) * 2)	//xing.chen modify
            {
                fltHeight = p_fltHeight - fltImgHeight;	//xing.chen modify
            }
            else
            {
                fltHeight = p_fltMaxHeight - fltImgHeight;	//xing.chen modify
            }

            ArrayList arlPrintData = new ArrayList();
            //ָʾ��ǰ�Ƿ����ұߴ�ӡ
            bool blnPrintRight = false;
            for (int i = 0; i < objDataArr.Length; i++)
            {
                int intDataCount = objDataArr[i].m_dtvResult.Count;
                objDataArr[i].m_fltHeight = m_fltGetPrintGroupHeight(objDataArr[i], m_fntSmallBold, m_fntSmall2NotBold, m_fltTitleSpace, m_fltItemSpace);
                //��ߴ�ӡ
                if (!blnPrintRight && objDataArr[i].m_fltHeight < fltHeight - fltLeft)
                {
                    objDataArr[i].m_fltX = p_fltX;
                    objDataArr[i].m_fltY = fltLeft + p_fltY;
                    objDataArr[i].m_intStartIdx = 0;
                    objDataArr[i].m_intCount = objDataArr[i].m_dtvResult.Count;
                    objDataArr[i].m_intPageIdx = intPage;
                    fltLeft += objDataArr[i].m_fltHeight + m_fltTitleSpace;
                    arlPrintData.Add(objDataArr[i]);
                    intTotalLeftItemCount -= objDataArr[i].m_intCount;
                }
                else
                {
                    //�ж����µļ�¼�ܷ�����һ�ߴ���,���ҵ�ǰ�Ѿ���ӡ�ļ�¼����������ڻ���ڵ��д�ӡ������1/2
                    if (fltLeft >= fltHeight / 2 && fltItemHeight * intTotalLeftItemCount < fltHeight)
                    {
                        blnPrintRight = true;
                        objDataArr[i].m_fltX = p_fltX + p_fltWidth / 2;
                        objDataArr[i].m_fltY = fltRight + p_fltY;
                        objDataArr[i].m_intStartIdx = 0;
                        objDataArr[i].m_intCount = objDataArr[i].m_dtvResult.Count;
                        objDataArr[i].m_intPageIdx = intPage;
                        fltRight += objDataArr[i].m_fltHeight + m_fltTitleSpace;
                        arlPrintData.Add(objDataArr[i]);
                        intTotalLeftItemCount -= objDataArr[i].m_intCount;
                    }
                    else
                    {
                        while (intDataCount > 0)
                        {
                            if (fltTitleHeight + fltItemHeight < fltHeight - fltLeft)
                            {
                                int intPrintItemCount = 1;
                                while ((intPrintItemCount + 1) * fltItemHeight + fltTitleHeight < fltHeight - fltLeft)
                                {
                                    intPrintItemCount++;
                                }
                                clsSampleResultInfo obj = new clsSampleResultInfo(objDataArr[i].m_dtvResult);
                                obj.m_strPrintTitle = objDataArr[i].m_strPrintTitle;
                                obj.m_fltX = p_fltX;
                                obj.m_fltY = fltLeft + p_fltY;
                                obj.m_intStartIdx = objDataArr[i].m_dtvResult.Count - intDataCount;
                                obj.m_intCount = intPrintItemCount;
                                obj.m_intPageIdx = intPage;
                                fltLeft += intPrintItemCount * fltItemHeight + fltTitleHeight + m_fltTitleSpace;
                                arlPrintData.Add(obj);
                                intDataCount -= intPrintItemCount;
                                intTotalLeftItemCount -= intPrintItemCount;
                            }
                            else
                            {
                                //�ұߴ�ӡ
                                if (fltTitleHeight + fltItemHeight * intDataCount < fltHeight - fltRight)
                                {
                                    clsSampleResultInfo obj = new clsSampleResultInfo(objDataArr[i].m_dtvResult);
                                    obj.m_strPrintTitle = objDataArr[i].m_strPrintTitle;
                                    obj.m_fltX = p_fltX + p_fltWidth / 2;
                                    obj.m_fltY = fltRight + p_fltY;
                                    obj.m_intStartIdx = objDataArr[i].m_dtvResult.Count - intDataCount;
                                    obj.m_intCount = intDataCount;
                                    obj.m_intPageIdx = intPage;
                                    fltRight += intDataCount * fltItemHeight + fltTitleHeight + m_fltTitleSpace;
                                    arlPrintData.Add(obj);
                                    intDataCount -= intDataCount;
                                    intTotalLeftItemCount -= intDataCount;
                                }
                                else
                                {
                                    if (fltTitleHeight + fltItemHeight < fltHeight - fltRight)
                                    {
                                        int intPrintItemCount = 1;
                                        while ((intPrintItemCount + 1) * fltItemHeight + fltTitleHeight < fltHeight - fltRight)
                                        {
                                            intPrintItemCount++;
                                        }
                                        clsSampleResultInfo obj = new clsSampleResultInfo(objDataArr[i].m_dtvResult);
                                        obj.m_strPrintTitle = objDataArr[i].m_strPrintTitle;
                                        obj.m_fltX = p_fltX + p_fltWidth / 2;
                                        obj.m_fltY = fltRight + p_fltY;
                                        obj.m_intStartIdx = objDataArr[i].m_dtvResult.Count - intDataCount;
                                        obj.m_intCount = intPrintItemCount;
                                        obj.m_intPageIdx = intPage;
                                        fltRight += intPrintItemCount * fltItemHeight + fltTitleHeight + m_fltTitleSpace;
                                        arlPrintData.Add(obj);
                                        intDataCount -= intPrintItemCount;
                                        intTotalLeftItemCount -= intPrintItemCount;
                                    }
                                    else
                                    {
                                        fltLeft = 0;
                                        fltRight = 0;
                                        blnPrintRight = false;
                                        intPage++;
                                        arlPageData.Add(arlPrintData);
                                        arlPrintData = new ArrayList();
                                        if (intTotalLeftItemCount * fltItemHeight + objDataArr.Length * fltTitleHeight <= p_fltHeight * 2)
                                        {
                                            fltHeight = p_fltHeight;
                                        }
                                        else
                                        {
                                            fltHeight = p_fltMaxHeight;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (arlPrintData.Count > 0)
            {
                arlPageData.Add(arlPrintData);
            }
            #endregion

            float fltY = Math.Max(fltLeft, fltRight);
            //			fltY += 4*m_fltTitleSpace;
            int intImgStartIdx = intPage;
            ArrayList arlPageImg = null;
            ArrayList arlImg = null;

            #region ͼ�����ݴ�ӡ��ҳ
            if (objImgArr != null && objImgArr.Length > 0)
            {
                arlPageImg = new ArrayList();
                arlImg = new ArrayList();
                float fltX = 0;
                for (int i = 0; i < objImgArr.Length; i++)
                {
                    if (objImgArr[i].m_fltHeight < p_fltMaxHeight && objImgArr[i].m_fltWidth < p_fltWidth)
                    {
                        bool blnDrawed = false;
                        while (!blnDrawed)
                        {
                            if (p_fltMaxHeight - fltY > objImgArr[i].m_fltHeight)
                            {
                                if (p_fltWidth - fltX > objImgArr[i].m_fltWidth)
                                {
                                    objImgArr[i].m_fltX = (fltX == 0 ? fltX + p_fltX : fltX + p_fltX + m_fltImgSpace);
                                    objImgArr[i].m_fltY = fltY + p_fltY;
                                    objImgArr[i].m_intPageIdx = intPage;
                                    arlImg.Add(objImgArr[i]);
                                    fltX += objImgArr[i].m_fltWidth + m_fltImgSpace;
                                    blnDrawed = true;
                                }
                                else
                                {
                                    if (i > 0)
                                    {
                                        fltY += objImgArr[i].m_fltHeight + m_fltImgSpace;
                                        fltX = 0;
                                    }
                                }
                            }
                            else
                            {
                                fltX = 0;
                                fltY = 0;
                                if (arlImg.Count > 0)
                                {
                                    arlPageImg.Add(arlImg);
                                    arlImg = new ArrayList();
                                }
                                intPage++;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (arlImg.Count > 0)
                {
                    arlPageImg.Add(arlImg);
                }
            }
            #endregion

            //ʵ������ʾ
            string strSummary = m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim();
            SizeF sf = m_rectGetPrintStringRectangle(m_fntSmallBold, m_fntSmallNotBold, strSummary, m_fltPrintWidth, m_fltTitleSpace,
                m_fltItemSpace);
            if (sf.Height > 0 && sf.Height > p_fltMaxHeight - fltY)
            {
                intPage++;
            }

            #region ����ҳ���ӡ��Ϣ
            clsPrintPerPageInfo[] objArr = new clsPrintPerPageInfo[intPage + 1];
            int intStartImgIdx = -1;
            if (arlPageImg != null)
            {
                intStartImgIdx = ((clsPrintImage[])((ArrayList)arlPageImg[0]).ToArray(typeof(clsPrintImage)))[0].m_intPageIdx;
            }
            for (int i = 0; i < objArr.Length; i++)
            {
                objArr[i] = new clsPrintPerPageInfo();
                if (i <= arlPageData.Count - 1)
                {
                    objArr[i].m_objSampleArr = (clsSampleResultInfo[])((ArrayList)arlPageData[i]).ToArray(typeof(clsSampleResultInfo));
                }
                if (arlPageImg != null)
                {
                    if (intStartImgIdx <= i && i <= intStartImgIdx + arlPageImg.Count - 1)
                    {
                        objArr[i].m_imgArr = (clsPrintImage[])((ArrayList)arlPageImg[i - intStartImgIdx]).ToArray(typeof(clsPrintImage));
                    }
                }
            }
            #endregion

            return objArr;
        }
        #endregion

        #region FunctionMethod
        /// <summary>
        /// ��ȡ��ӡ��ĸ߶�
        /// </summary>
        /// <param name="p_objData"></param>
        /// <param name="p_fntTitle"></param>
        /// <param name="p_fntItem"></param>
        /// <param name="p_fltTitleSpace"></param>
        /// <param name="p_fltItemSpace"></param>
        /// <returns></returns>
        private float m_fltGetPrintGroupHeight(clsSampleResultInfo p_objData, Font p_fntTitle, Font p_fntItem,
            float p_fltTitleSpace, float p_fltItemSpace)
        {
            float fltHeight = 0;
            fltHeight += (p_fntTitle.Height + p_fltTitleSpace) + (p_objData.m_intCount * (p_fntItem.Height + p_fltItemSpace));
            return fltHeight;
        }

        private float m_fltGetPrintElementHeight(Font p_fnt, float p_fltPrintSpace)
        {
            float fltHeight = 0;
            fltHeight += p_fnt.Height + p_fltPrintSpace;
            return fltHeight;
        }

        /// <summary>
        /// �����ӡ����
        /// </summary>
        /// <param name="p_dtvData"></param>
        /// <returns></returns>
        private clsSampleResultInfo[] m_objConstructSampleResultArr(DataView p_dtvData)
        {
            ArrayList arlGroupID = new ArrayList();
            clsSampleResultInfo[] objArr = null;
            for (int i = 0; i < p_dtvData.Count; i++)
            {
                if (i > 0)
                {
                    if (p_dtvData[i]["groupid_chr"].ToString().Trim() != p_dtvData[i - 1]["groupid_chr"].ToString().Trim())
                    {
                        arlGroupID.Add(p_dtvData[i]["groupid_chr"].ToString().Trim());
                    }
                }
                else
                {
                    arlGroupID.Add(p_dtvData[i]["groupid_chr"].ToString().Trim());
                }
            }
            if (arlGroupID.Count > 0)
            {
                objArr = new clsSampleResultInfo[arlGroupID.Count];
                for (int i = 0; i < arlGroupID.Count; i++)
                {
                    DataView dtv = new DataView(p_dtvData.Table);
                    dtv.RowFilter = "IS_GRAPH_RESULT_NUM = 0 AND groupid_chr = " + arlGroupID[i].ToString().Trim();
                    objArr[i] = new clsSampleResultInfo(dtv);
                    objArr[i].m_dtvResult.Sort = "SAMPLE_PRINT_SEQ_INT ASC";
                    objArr[i].m_strPrintTitle = dtv[0]["print_title_vchr"].ToString().Trim();
                    objArr[i].m_fltHeight = m_fltGetPrintGroupHeight(objArr[i], m_fntSmallBold, m_fntSmall2NotBold, m_fltTitleSpace, m_fltItemSpace);
                    objArr[i].m_intCount = objArr[i].m_dtvResult.Count;
                }
            }
            return objArr;
        }

        /// <summary>
        /// �����ӡͼ��
        /// </summary>
        /// <param name="p_dtvData"></param>
        /// <returns></returns>
        private clsPrintImage[] m_objConstructPrintImage(DataView p_dtvData)		// xing.chen modify 2005.9.22
        {
            int intCount = p_dtvData.Count;
            clsPrintImage[] objImgArr = null;
            ArrayList arl = new ArrayList();
            for (int i = 0; i < intCount; i++)
            {
                if (p_dtvData[i]["GRAPH_IMG"] is System.DBNull)
                {
                    continue;
                }
                Image img = m_imgDrawGraphic((byte[])p_dtvData[i]["GRAPH_IMG"], p_dtvData[i]["GRAPH_FORMAT_NAME_VCHR"].ToString());
                if (img != null)
                {
                    clsPrintImage objImg = new clsPrintImage(img);
                    objImg.m_fltWidth = m_fltXRate * objImg.m_fltWidth;
                    objImg.m_fltHeight = m_fltYRate * objImg.m_fltHeight;
                    arl.Add(objImg);
                }
            }
            objImgArr = (clsPrintImage[])arl.ToArray(typeof(clsPrintImage));
            return objImgArr;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="p_dtbSource"></param>
        /// <param name="p_strFltExp"></param>
        /// <returns></returns>
        private DataView m_dtvFilterRows(DataTable p_dtbSource, string p_strFltExp)
        {
            DataView dtv = new DataView(p_dtbSource);
            dtv.RowFilter = p_strFltExp;
            return dtv;
        }
        #endregion

        #region ��ӡ���ű��浥
        private void m_mthPrint()
        {

            m_mthPrintEnd_DGCS();

            m_mthPrintBseInfo();
            m_mthPrintDetail();
            //m_mthPrintEnd();
            if (m_intTotalPage - 1 > m_intCurrentPageIdx)
            {
                m_intCurrentPageIdx++;
            }
        }
        #endregion

        #region �̳д�ӡ�ӿ�

        public void m_mthInitPrintContent()
        {
        }

        public void m_mthInitPrintTool(object p_objArg)
        {
            m_mthInitalPrintTool((PrintDocument)p_objArg);
        }

        public void m_mthDisposePrintTools(object p_objArg)
        {
        }

        public void m_mthBeginPrint(object p_objPrintArg)
        {
            m_dtbSample = ((clsPrintValuePara)p_objPrintArg).m_dtbBaseInfo;
            m_dtbResult = ((clsPrintValuePara)p_objPrintArg).m_dtbResult;
        }

        public void m_mthPrintPage(object p_objPrintArg)
        {
            m_printMethodTool = new clsCommonPrintMethod((PrintPageEventArgs)p_objPrintArg);
            m_mthPrint();
        }

        public void m_mthEndPrint(object p_objPrintArg)
        {
        }

        #endregion
    }

    /// <summary>
    /// clsLisReportPrint ��ժҪ˵����
    /// </summary>
    public class clsGermReportPrinToolV2 : infPrintRecord
    {
        #region ���浥����
        private float m_fltPaperWidth;        //��ӡֽ�ŵĿ��
        private float m_fltPaperHeight;       //��ӡֽ�ŵĸ߶�
        private float m_fltPrintWidth;        //��ӡ����Ŀ��
        private float m_fltPrintHeight;       //��ӡ����ĸ߶�
        private float m_fltStartX;            //���浥X��ʼλ��
        private float m_fltEndY;              //���浥�ײ���Ϣλ��
        private float m_fltTitleSpace;        //���浥���ӡ������
        private float m_fltItemSpace;         //���浥���ӡ��Ŀ���
        private float m_fltImgSpace;          //ͼ�δ�ӡ���

        //ͼ�����ű���
        private float m_fltXRate = 0.8f;
        private float m_fltYRate = 0.8f;

        private string m_strPatientName = "����:";
        private string m_strSex = "�Ա�:";
        private string m_strAge = "����:";
        private string m_strInPatientNo = "סԺ��:";
        private string m_strDepartment = "����:";
        private string m_strBedNo = "����:";
        private string m_strSampleType = "��������:";
        private string m_strApplyDoc = "�ͼ�ҽ��:";
        private string m_strDiagnose = "�ٴ����:";
        private string m_strSampleID = "������:";
        private string m_strCheckNo = "������:";
        private string m_strCheckDate = "�ͼ�����:";
        private string m_strSummary = "ʵ������ʾ:";
        private string m_strNotice = "ף�����彡��!�˱�����Լ��걾����,�����ҽ���ο�!";
        private string m_strAnnotation = "��ע:";
        private string m_strReportDate = "��������:";
        private string m_strCheckDoc = "����ҽ��:";
        private string m_strConfirmEmp = "�����:";
        private string m_strResult = "���";
        private string m_strReference = "�ο�ֵ";
        private string m_strGermicultureResult = "�������:";

        //��������
        private Font m_fntTitle;
        private Font m_fntSmallBold;
        private Font m_fntSmallNotBold;
        private Font m_fntSmall2NotBold;
        private Font m_fntHeadNotBold;
        private Font m_fntSmall2Bold;
        private Font m_fntsamll3NotBold;

        //���浥����
        public DataTable m_dtbSample;
        public DataTable m_dtbResult;

        //��ӡ��������
        clsCommonPrintMethod m_printMethodTool;

        //Yλ�ö�λ
        private float m_fltY;

        private bool m_blnDocked = true; //��ӡ�ײ���Ϣ�̶���

        //��ӡ��Ϣ��ҳ
        clsPrintPerPageInfo[] m_objPrintPage;

        //ָʾ��ǰ��ӡҳ��
        private int m_intCurrentPageIdx = 0;
        private int m_intTotalPage = 0;

        //ʵ������ʾ�͸�עΪ��ʱ�Ƿ���ʾ
        bool m_blnSummaryEmptyVisible = false;
        bool m_blnAnnotationEmptyVisible = false;
        /// <summary>
        /// �Ƿ��ӡ���
        /// </summary>
        public static bool blnSurePrintDiagnose = false;
        #endregion

        #region �ײ���ӡ��Ϣ�̶�����
        public bool IsDocked
        {
            get
            {
                return m_blnDocked;
            }
            set
            {
                m_blnDocked = value;
            }
        }
        #endregion

        #region ���캯��

        private Image objImage;  //ҽԺͼ��
        public clsGermReportPrinToolV2()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //

            //if (p_strParmValue == "1")
            //{
            //    blnSurePrintDiagnose = true;
            //}
            //else
            //{
            //    blnSurePrintDiagnose = false;
            //}

            string m_strStartUp = System.Windows.Forms.Application.StartupPath + "\\Picture\\��ݸ��ɽҽԺͼ��.jpg"; //��ȡ��ӡͼ��

            objImage = Image.FromFile(m_strStartUp, false);
        }
        #endregion

        #region ��ӡ�ӿڳ�ʼ������
        private void m_mthInitalPrintTool(PrintDocument p_printDoc)
        {
            //��ȡֽ�ŵĿ�͸�
            m_fltPaperWidth = p_printDoc.DefaultPageSettings.Bounds.Width;
            m_fltPaperHeight = p_printDoc.DefaultPageSettings.Bounds.Height;

            //���ô�ӡ����Ŀ�͸�
            m_fltPrintWidth = m_fltPaperWidth * 0.8f;
            m_fltPrintHeight = m_fltPaperHeight * 0.9f;
            m_fltStartX = m_fltPaperWidth * 0.1f;
            m_fltEndY = m_fltPaperHeight - 114;

            //���ñ��浥���ӡ���
            m_fltTitleSpace = 5;
            m_fltItemSpace = 2;
            m_fltImgSpace = 10;

            //���ô�ӡ����
            m_fntTitle = new Font("SimSun", 18, FontStyle.Bold);
            m_fntSmallBold = new Font("SimSun", 11, FontStyle.Bold);
            m_fntSmallNotBold = new Font("SimSun", 10f, FontStyle.Regular);
            m_fntSmall2NotBold = new Font("SimSun", 9f, FontStyle.Regular);
            m_fntHeadNotBold = new Font("SimSun", 11f, FontStyle.Regular);
            m_fntHeadNotBold = new Font("SimSun", 11f, FontStyle.Regular);
            m_fntsamll3NotBold = new Font("SimSun", 8f, FontStyle.Regular);
        }
        #endregion

        #region ����ͼ��
        private Image m_imgDrawGraphic(byte[] p_bytGraph, string p_strImageFormat)
        {
            Image img = null;
            System.IO.MemoryStream ms = null;
            try
            {
                ms = new System.IO.MemoryStream(p_bytGraph);
                img = Image.FromStream(ms, true);
                string strFormat = (p_strImageFormat == null) ? null : p_strImageFormat.ToLower();
                switch (strFormat)
                {
                    case "lisb":
                        System.Drawing.Bitmap bm = new Bitmap(20, img.Height);
                        Graphics g = Graphics.FromImage(bm);
                        g.DrawImage(img, 0, 0, bm.Width, bm.Height);
                        img.Dispose();
                        img = bm;
                        break;
                    default:
                        break;
                }
            }
            catch
            {
            }
            finally
            {
                if (ms != null)
                    ms.Close();
            }
            return img;
        }
        #endregion

        #region ��ӡ���浥������Ϣ
        private void m_mthPrintBseInfo()
        {
            if (m_dtbSample == null)
                return;

            m_fltY = 5;
            float fltColumn1 = m_fltStartX;
            float fltColumn2 = m_fltPaperWidth * 0.25f;
            float fltColumn3 = m_fltPaperWidth * 0.45f;
            float fltColumn4 = m_fltPaperWidth * 0.62f;


            //ͼ��
            m_printMethodTool.m_mthPrintImage(objImage, fltColumn1, m_fltY);

            //string m_strTitleImg = m_dtbSample.Rows[0]["print_title_vchr"].ToString().Trim().Remove
            //    (m_dtbSample.Rows[0]["print_title_vchr"].ToString().Trim().Length - 5);

            string m_strTitleImg = "�� ݸ �� �� ɽ ҽ Ժ";
            string m_strTitleImgEng = "ChaShan Hospital of DongGuang";

            //ҽԺ����
            m_printMethodTool.m_mthDrawString(m_strTitleImg, m_fntSmallBold, fltColumn1 + objImage.Width, m_fltY + 16);

            //Ӣ��
            m_printMethodTool.m_mthDrawString(m_strTitleImgEng, m_fntsamll3NotBold, fltColumn1 + objImage.Width, m_fltY + 30);

            m_fltY += 5 + objImage.Height;

            string m_strTitle = m_dtbSample.Rows[0]["print_title_vchr"].ToString().Trim().Substring
                (m_dtbSample.Rows[0]["print_title_vchr"].ToString().Trim().Length - 5);
            //if (m_dtbSample.Rows[0]["report_print_chr"] != System.DBNull.Value)
            //{
            //    string strTime = m_dtbSample.Rows[0]["report_print_chr"].ToString().Trim();
            //    int intTime = 0;
            //    try
            //    {
            //        intTime = Convert.ToInt32(strTime);
            //        if (intTime > 0)
            //        {
            //            m_strTitle = m_dtbSample.Rows[0]["print_title_vchr"].ToString() + "(�ش�)";
            //        }
            //    }
            //    catch
            //    { }
            //}


            //DrawTitle
            m_printMethodTool.m_mthPrintTitle(m_strTitle, m_fntTitle, m_fltY, m_fltPaperWidth);

            //Locate Y
            m_fltY += 10 + m_printMethodTool.m_fltGetStringHeight(m_dtbSample.Rows[0]["print_title_vchr"].ToString().Trim(), m_fntTitle);

            m_printMethodTool.m_mthDrawLine(m_fltStartX - 5, m_fltY, m_fltPaperWidth * 0.9f, m_fltY);
            //Locate Y
            m_fltY += 5;

            //����
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntHeadNotBold, m_strPatientName,
                m_dtbSample.Rows[0]["patient_name_vchr"].ToString().Trim(), fltColumn1, m_fltY);


            //�Ա�
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold,
                m_strSex, m_dtbSample.Rows[0]["sex_chr"].ToString().Trim(), fltColumn2, m_fltY);

            //����
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold,
                m_strAge, m_dtbSample.Rows[0]["age_chr"].ToString().Trim(), fltColumn3, m_fltY);

            //סԺ�š����￨�š�����
            string strPatientType = m_dtbSample.Rows[0]["patient_type_chr"].ToString().Trim();
            string strPrintContent = null;
            switch (strPatientType)
            {
                case "2":
                    m_strInPatientNo = "���ƿ���:";
                    strPrintContent = m_dtbSample.Rows[0]["patientcardid_chr"].ToString().Trim();
                    break;

                case "3":
                    m_strInPatientNo = "����:";
                    strPrintContent = m_dtbSample.Rows[0]["patient_inhospitalno_chr"].ToString().Trim();
                    break;

                default:
                    m_strInPatientNo = "סԺ��:";
                    strPrintContent = m_dtbSample.Rows[0]["patient_inhospitalno_chr"].ToString().Trim();
                    break;
            }


            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strInPatientNo,
                strPrintContent, fltColumn4, m_fltY);

            //Locate Y
            m_fltY += 5 + m_printMethodTool.m_fltGetStringHeight(m_strSampleID, m_fntSmallBold);


            //��  ��
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strDepartment,
                m_dtbSample.Rows[0]["deptname_vchr"].ToString().Trim(), fltColumn1, m_fltY);


            //��  ��
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strBedNo,
                m_dtbSample.Rows[0]["bedno_chr"].ToString().Trim(), fltColumn2, m_fltY);

            //��������
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strSampleType,
                m_dtbSample.Rows[0]["sample_type_desc_vchr"].ToString().Trim(), fltColumn3, m_fltY);

            //������
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, m_strCheckNo,
                m_dtbSample.Rows[0]["check_no_chr"].ToString().Trim(), fltColumn4, m_fltY);


            //Locate Y
            m_fltY += 5 + m_printMethodTool.m_fltGetStringHeight(m_strSampleID, m_fntSmallBold);


            m_printMethodTool.m_mthDrawLine(m_fltStartX - 5, m_fltY, m_fltPaperWidth * 0.9f, m_fltY);

            m_fltY += 5;
        }
        #endregion

        #region ��ӡ���浥ʵ������ʾ
        private float m_fltPrintSummary(float p_fltX, float p_fltY, float p_fltPrintWidth)
        {
            if (!m_blnSummaryEmptyVisible && m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim() == "")
                return p_fltY;
            float fltY = p_fltY + 10;
            string strSummary = m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim();
            m_printMethodTool.m_mthDrawString(m_strSummary, m_fntSmallBold, p_fltX, fltY);
            fltY += m_fntSmallBold.Height + m_fltTitleSpace;
            SizeF sf = m_rectGetPrintStringRectangle(m_fntSmallBold, m_fntSmallNotBold, strSummary, p_fltPrintWidth, m_fltTitleSpace,
                m_fltItemSpace);
            Rectangle rectSummary = new Rectangle((int)p_fltX, (int)fltY, (int)sf.Width, (int)sf.Height);
            new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, m_fntSmallNotBold).m_mthPrintText(m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim(),
                m_dtbSample.Rows[0]["XML_SUMMARY_VCHR"].ToString().Trim(), m_fntSmallNotBold, Color.Black, rectSummary, m_printMethodTool.m_printEventArg.Graphics);
            fltY += rectSummary.Height;
            return fltY;
        }
        #endregion

        #region ��ӡ���浥�ײ���Ϣ
        private void m_mthPrintEnd()
        {
            if (m_blnDocked)
            {
                if (m_fltY < m_fltEndY)
                {
                    m_fltY = m_fltEndY;
                }
            }
            m_fltY += 10;
            //Notice
            m_printMethodTool.m_mthDrawString(m_strNotice, m_fntSmallNotBold, m_fltStartX, m_fltY);
            float fltNoticeWidth = m_printMethodTool.m_fltGetStringWidth(m_strNotice, m_fntSmallNotBold);
            //��ע
            bool blnPrintAnnotation = false;
            if (m_dtbSample.Rows[0]["ANNOTATION_VCHR"].ToString().Trim() != "" || m_blnAnnotationEmptyVisible)
            {
                blnPrintAnnotation = true;
            }
            if (blnPrintAnnotation)
            {
                m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallNotBold, m_fntSmallNotBold, m_strAnnotation, m_dtbSample.Rows[0]["ANNOTATION_VCHR"].ToString().Trim(),
                    m_fltStartX + fltNoticeWidth, m_fltY);
            }
            m_fltY += m_printMethodTool.m_fltGetStringHeight(m_strAnnotation, m_fntSmallNotBold) + 3;
            //����
            m_printMethodTool.m_mthDrawLine(m_fltStartX - 5, m_fltY, m_fltPaperWidth * 0.9f, m_fltY);

            m_fltY += 6;

            //column
            float fltColumn1 = m_fltStartX;
            float fltColumn2 = m_fltPaperWidth * 1.4f / 3;
            float fltColumn3 = m_fltPaperWidth * 2.1f / 3;

            //��������
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strReportDate, m_dtbSample.Rows[0]["CONFIRM_DAT"].ToString().ToString(),
                fltColumn1, m_fltY);
            //����ҽ��
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strCheckDoc, m_dtbSample.Rows[0]["reportor"].ToString().Trim(), fltColumn2,
                m_fltY);
            //�����
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strConfirmEmp, m_dtbSample.Rows[0]["confirmer"].ToString().Trim(), fltColumn3,
                m_fltY);
        }

        //��ݸ��ɽʽҳβ
        private void m_mthPrintEnd_DGCS()
        {
            if (m_blnDocked)
            {
                if (m_fltY < m_fltEndY)
                {
                    m_fltY = m_fltEndY;
                }
            }
            m_fltY += 3;

            //����
            //m_printMethodTool.m_mthDrawLine(m_fltStartX - 5, m_fltY, m_fltPaperWidth * 0.9f, m_fltY);

            m_fltY += 6;

            //column
            float fltColumn1 = m_fltStartX;
            float fltColumn2 = m_fltPaperWidth * 1.4f / 3;
            float fltColumn3 = m_fltPaperWidth * 2.1f / 3;

            //��������
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strReportDate, m_dtbSample.Rows[0]["CONFIRM_DAT"].ToString().ToString(),
                fltColumn1, m_fltY);
            //����ҽ��
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strCheckDoc, m_dtbSample.Rows[0]["reportor"].ToString().Trim(), fltColumn2,
                m_fltY);
            //�����
            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallBold, m_strConfirmEmp, m_dtbSample.Rows[0]["confirmer"].ToString().Trim(), fltColumn3,
                m_fltY);

            m_fltY += m_printMethodTool.m_fltGetStringHeight(m_strReportDate, m_fntSmallBold) + 6;

            //����
            m_printMethodTool.m_mthDrawLine(m_fltStartX - 5, m_fltY, m_fltPaperWidth * 0.9f, m_fltY);
            m_fltY += 6;

            //Notice
            m_printMethodTool.m_printEventArg.Graphics.DrawString(m_strNotice, new Font("SimSun", 11f, FontStyle.Regular), Brushes.Red, m_fltStartX, m_fltY);
            //m_printMethodTool.m_mthDrawString(m_strNotice, m_fntSmallNotBold, m_fltStartX, m_fltY);
            float fltNoticeWidth = m_printMethodTool.m_fltGetStringWidth(m_strNotice, new Font("SimSun", 11f, FontStyle.Regular));
            //��ע
            bool blnPrintAnnotation = false;
            if (m_dtbSample.Rows[0]["annotation_vchr"].ToString().Trim() != "" || m_blnAnnotationEmptyVisible)
            {
                blnPrintAnnotation = true;
            }
            if (blnPrintAnnotation)
            {
                m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallNotBold, m_fntSmallNotBold, m_strAnnotation, m_dtbSample.Rows[0]["annotation_vchr"].ToString().Trim(),
                    m_fltStartX + fltNoticeWidth, m_fltY);
            }

        }
        #endregion

        #region ��ӡҳ��Ϣ
        private void m_mthPrintGermResult()
        {
            #region old
            //        DataRow[] dtrs = this.m_dtbResult.Select("item_type = 'GermR'", "REPORT_PRINT_SEQ_INT ASC,GROUPID_CHR ASC,SAMPLE_PRINT_SEQ_INT ASC");
            //        float fltX = 0.0f;
            //        float fltX1 = m_fltStartX;
            //        float fltX2 = this.m_fltPrintWidth / 2 + m_fltStartX;
            //        bool blnX1 = true;
            //        m_fltY -= m_fntSmallBold.Height;
            //        foreach (DataRow dtr in dtrs)
            //        {
            //            if (blnX1)
            //                m_fltY += 5 + m_fntSmallBold.Height;
            //            fltX = blnX1 ? fltX1 : fltX2;
            //            m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, "�������:",
            //dtr["result_vchr"].ToString().Trim(), fltX, m_fltY);

            //            blnX1 = !blnX1;

            //        }
            //        m_fltY += 5 + m_fntSmallBold.Height; 
            #endregion

            DataRow dtrN = null;
            DataRow[] dtrn = this.m_dtbResult.Select("item_type = 'Germ'", "REPORT_PRINT_SEQ_INT ASC,GROUPID_CHR ASC,SAMPLE_PRINT_SEQ_INT ASC");
            if (dtrn != null && dtrn.Length > 0)
                dtrN = dtrn[0];
            DataRow[] dtrs = this.m_dtbResult.Select("item_type = 'GermR'", "REPORT_PRINT_SEQ_INT ASC,GROUPID_CHR ASC,SAMPLE_PRINT_SEQ_INT ASC");
            float fltX = 0.0f;
            float fltX1 = m_fltStartX;
            float fltX2 = this.m_fltPrintWidth / 2 + m_fltStartX;
            bool blnX1 = true;
            m_fltY -= m_fntSmallBold.Height;
            foreach (DataRow dtr in dtrs)
            {
                string strResult = "";
                if (dtr["result_vchr"].ToString().Trim() == "-")
                    strResult = "ϸ��";
                else strResult = dtr["result_vchr"].ToString().Trim();
                if (dtrN != null)
                {
                    strResult = dtrN["result_vchr"].ToString().Trim() + strResult;
                }
                strResult = strResult + "����";
                if (blnX1)
                    m_fltY += 5 + m_fntSmallBold.Height;
                fltX = blnX1 ? fltX1 : fltX2;
                m_printMethodTool.m_mthDrawTextAndContent(m_fntSmallBold, m_fntSmallNotBold, "�������:",
    strResult, fltX, m_fltY);

                blnX1 = !blnX1;

            }
            m_fltY += 5 + m_fntSmallBold.Height;

        }
        private void m_mthPrintDetail()
        {
            m_mthPrintGermResult();

            string strFlag = "*";

            float fltY = 0.0f;
            float fltHalfWidth = this.m_fltPrintWidth / 2;
            float fltXFlag = m_fltStartX;
            float fltXSeq = fltXFlag + m_printMethodTool.m_fltGetStringWidth(strFlag, m_fntSmall2NotBold);
            float fltXab = fltXSeq + m_printMethodTool.m_fltGetStringWidth("00", m_fntSmall2NotBold) + 10;
            float fltXItemName = fltXab + m_printMethodTool.m_fltGetStringWidth("XXXX", m_fntSmall2NotBold) + 5;
            float fltXResult = m_fltStartX + fltHalfWidth -
                m_printMethodTool.m_fltGetStringWidth("��ҩ", m_fntSmall2NotBold) - 20;
            DataRow[] dtrs = this.m_dtbResult.Select("item_type is null", "REPORT_PRINT_SEQ_INT ASC,GROUPID_CHR ASC,SAMPLE_PRINT_SEQ_INT ASC");
            if (dtrs.Length != null && dtrs.Length != 0)
            {
                m_printMethodTool.m_mthDrawString("ҩ�����:", m_fntSmallBold, m_fltStartX, m_fltY);
                m_fltY += 25;
                fltY = this.m_fltY;

                int intHalf = (dtrs.Length - 1) / 2;
                float fltOffset = 0;
                for (int i = 0; i < dtrs.Length; i++)
                {
                    string flag = "";
                    if (dtrs[i]["result_vchr"].ToString().Trim() == "����")
                        flag = strFlag;
                    this.m_printMethodTool.m_mthDrawString(flag, this.m_fntSmall2NotBold, fltXFlag + fltOffset, fltY);
                    this.m_printMethodTool.m_mthDrawString((i + 1).ToString(), this.m_fntSmall2NotBold, fltXSeq + fltOffset, fltY);
                    this.m_printMethodTool.m_mthDrawString(dtrs[i]["SHORTNAME_CHR"].ToString().Trim(),
                        this.m_fntSmall2NotBold, fltXab + fltOffset, fltY);
                    this.m_printMethodTool.m_mthDrawString(dtrs[i]["RPTNO_CHR"].ToString().Trim(),
                        this.m_fntSmall2NotBold, fltXItemName + fltOffset, fltY);
                    this.m_printMethodTool.m_mthDrawString(dtrs[i]["result_vchr"].ToString().Trim(),
                        this.m_fntSmall2NotBold, fltXResult + fltOffset, fltY);
                    fltY += m_fntSmall2NotBold.Height + m_fltItemSpace;
                    if (i == intHalf)
                    {
                        fltOffset = fltHalfWidth;
                        float temp = fltY;
                        fltY = m_fltY;
                        m_fltY = temp;
                    }
                }
            }
            string strSummary = m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim();
            SizeF sf = m_rectGetPrintStringRectangle(m_fntSmallBold, m_fntSmallNotBold, strSummary, m_fltPrintWidth, m_fltTitleSpace,
                m_fltItemSpace);
            m_fltY = m_fltPrintSummary(m_fltStartX, m_fltY, m_fltPrintWidth);

            #region old
            //if (m_objPrintPage == null)
            //{
            //    m_objPrintPage = m_objConstructPrintPageInfo(m_dtbResult, m_fltStartX, m_fltY, m_fltPrintWidth
            //        , m_fltPaperHeight - 123 - (m_fltPaperHeight - m_fltEndY) - sf.Height, m_fltPaperHeight - 123 - (m_fltPaperHeight - m_fltEndY));
            //    m_intTotalPage = m_objPrintPage.Length;
            //}
            //if (m_intCurrentPageIdx == m_objPrintPage.Length - 1)
            //{
            //    m_printMethodTool.m_printEventArg.HasMorePages = false;
            //}
            //else
            //{
            //    m_printMethodTool.m_printEventArg.HasMorePages = true;
            //}
            //if (m_objPrintPage[m_intCurrentPageIdx].m_objSampleArr != null)
            //{
            //    //��ӡ�������
            //    float fltY = m_fltPrintGroupData(m_objPrintPage[m_intCurrentPageIdx].m_objSampleArr);
            //    if (fltY != -1)
            //        m_fltY = fltY;
            //}
            //if (m_objPrintPage[m_intCurrentPageIdx].m_imgArr != null)
            //{
            //    //��ӡͼ������
            //    float fltY = m_fltPrintImageArr(m_objPrintPage[m_intCurrentPageIdx].m_imgArr);
            //    if (fltY != -1)
            //        m_fltY = fltY;
            //}
            //if (m_printMethodTool.m_printEventArg.HasMorePages == false)
            //{
            //    m_fltY = m_fltPrintSummary(m_fltStartX, m_fltY, m_fltPrintWidth);
            //} 
            #endregion
        }
        #endregion

        #region ��ӡ������
        //��ӡ������
        private float m_fltPrintGroupData(clsSampleResultInfo[] p_objArr)
        {
            float fltY = 0;
            if (p_objArr == null)
                return -1;
            bool blnHasTwoPart = false;
            if (p_objArr[p_objArr.Length - 1].m_fltX > m_fltStartX)
                blnHasTwoPart = true;
            float[] fltColumnArr = null;
            float fltResultPrintWidth;
            if (blnHasTwoPart)
            {
                fltColumnArr = new float[] { m_fltPrintWidth * 0.21f, m_fltPrintWidth * 0.31f };
                fltResultPrintWidth = (fltColumnArr[1] - fltColumnArr[0]) * 0.9f;
            }
            else
            {
                fltColumnArr = new float[] { m_fltPrintWidth * 0.25f, m_fltPrintWidth * 0.42f };
                fltResultPrintWidth = (fltColumnArr[1] - fltColumnArr[0]) * 0.5f;
            }

            float fltBseY;
            float fltTitleHeight = m_fltGetPrintElementHeight(m_fntSmallBold, m_fltTitleSpace);
            float fltItemHeight = m_fltGetPrintElementHeight(m_fntSmall2NotBold, m_fltTitleSpace);
            for (int i = 0; i < p_objArr.Length; i++)
            {
                fltBseY = p_objArr[i].m_fltY;
                float fltColumn1 = p_objArr[i].m_fltX;
                float fltColumn2 = fltColumn1 + fltColumnArr[0];
                float fltColumn3 = fltColumn1 + fltColumnArr[1];

                //��ӡ����
                m_printMethodTool.m_mthDrawString(p_objArr[i].m_strPrintTitle, m_fntSmallBold, fltColumn1, fltBseY);
                m_printMethodTool.m_mthDrawString(m_strResult, m_fntSmallBold, fltColumn2, fltBseY);
                //				m_printMethodTool.m_mthDrawString(m_strReference,m_fntSmallBold,fltColumn3,fltBseY);
                fltBseY += fltTitleHeight;
                for (int j = 0; j < p_objArr[i].m_intCount; j++)
                {
                    string strResult = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["result_vchr"].ToString().Trim();
                    string strAbnormal = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["ABNORMAL_FLAG_CHR"].ToString().Trim();
                    string strUnit = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["UNIT_VCHR"].ToString().Trim();
                    string strRefRange = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["refrange_vchr"].ToString() + " " + strUnit;
                    string strCheckItemName = p_objArr[i].m_dtvResult[p_objArr[i].m_intStartIdx + j]["RPTNO_CHR"].ToString().Trim();

                    //��ӡ��Ŀ
                    m_printMethodTool.m_mthDrawString(strCheckItemName, m_fntSmall2NotBold, fltColumn1, fltBseY);

                    //�쳣��־
                    if (strAbnormal != null)
                    {
                        System.Drawing.Font objBoldFont = new Font("SimSun", 9, FontStyle.Bold);
                        string strPR;

                        strPR = strResult + " " + "��";
                        float fltResultWidth = m_printMethodTool.m_fltGetStringWidth(strPR, objBoldFont);

                        if (strAbnormal == "H")
                        {
                            strPR = strResult + " " + "��";
                            float fltStartPos = fltColumn2 + fltResultPrintWidth - fltResultWidth;
                            m_printMethodTool.m_mthDrawString(strPR, objBoldFont, fltStartPos, fltBseY);
                        }
                        else if (strAbnormal == "L")
                        {
                            if (strResult.Contains(">") || strResult.Contains("<"))
                                strPR = strResult + " " + "��";
                            else
                                strPR = strResult + " " + "��";
                            float fltStartPos = fltColumn2 + fltResultPrintWidth - fltResultWidth;
                            m_printMethodTool.m_mthDrawString(strPR, objBoldFont, fltStartPos, fltBseY);
                        }
                        else
                        {
                            strPR = strResult + " " + " ";
                            float fltStartPos = fltColumn2 + fltResultPrintWidth - fltResultWidth;
                            m_printMethodTool.m_mthDrawString(strPR, m_fntSmall2NotBold, fltStartPos, fltBseY);
                        }
                    }
                    m_printMethodTool.m_mthDrawString(strRefRange, m_fntSmall2NotBold, fltColumn3, fltBseY);

                    //Locate Y 
                    fltBseY += m_fntSmall2NotBold.Height + m_fltItemSpace;
                    if (fltY < fltBseY)
                    {
                        fltY = fltBseY;
                    }
                }
            }
            return fltY;
        }
        #endregion

        #region ��ӡͼ��
        private float m_fltPrintImageArr(clsPrintImage[] p_objArr)
        {
            float fltY = 0;
            if (p_objArr == null)
                return -1;
            for (int i = 0; i < p_objArr.Length; i++)
            {
                m_printMethodTool.m_printEventArg.Graphics.DrawImage(p_objArr[i].m_img, p_objArr[i].m_fltX,
                    p_objArr[i].m_fltY, p_objArr[i].m_fltWidth, p_objArr[i].m_fltHeight);
                if (fltY < p_objArr[i].m_fltY + p_objArr[i].m_fltHeight)
                {
                    fltY = p_objArr[i].m_fltY + p_objArr[i].m_fltHeight;
                }
            }
            return fltY;
        }
        #endregion

        #region �����ַ������С
        //�����ַ������С
        private SizeF m_rectGetPrintStringRectangle(Font p_fntTitle, Font p_fntContent, string p_strContent, float p_fltWidth, float p_fltTitleSpace,
            float p_fltItemSpace)
        {
            if ((p_strContent == "" || p_strContent == null) && !m_blnSummaryEmptyVisible)
            {
                return new SizeF(0, 0);
            }
            float fltTitleHeight = p_fntTitle.Height;
            float fltContentHeight = p_fntContent.Height;
            float fltHeight = 0;
            if (p_strContent != null && p_strContent != "")
            {
                SizeF sfString = m_printMethodTool.m_printEventArg.Graphics.MeasureString(p_strContent, p_fntContent);
                fltHeight = (sfString.Width / p_fltWidth + 1) * fltContentHeight;
            }
            else
            {
                fltHeight = fltTitleHeight + p_fltTitleSpace + fltContentHeight;
            }
            SizeF sf = new SizeF(p_fltWidth, fltHeight);
            return sf;
        }
        #endregion

        #region ��ҳ����
        //�Ⱦ���������ȷ��ӡ��������С���Ѿ����˳�������ݺ�ͼ�����ݣ���ȷ�걾��ĸ����Լ������걾��Ĵ�ӡ�����С��˳��
        //������������DataTable����ӡ��ʼ��XYλ��(fltX,fltY)���Լ���ӡ�Ŀ�Ⱥ͸߶�(fltWidth,fltHeight)
        //          (DataTable p_dtbResult,float p_fltX,float p_fltY,float p_fltWidth,float p_fltHeight)
        //���������clsPrintPerPageInfo[]
        //���̣�
        //0 ���˳�������ݺ�ͼ�����ݣ���������걾���ӡ��Ϣ��clsSampleResultInfo�����Լ���ʼ��ͼ����Ϣ��clsPrintImage��
        //1 ��˳���ӡ�걾��������
        //1.1 �ж�������Ϣ�����ܷ�һҳ����
        //1.1.1 Y GOTO 2
        //1.1.2 N ��ҳ GoTo 1.1
        //2 ������ݴ�ӡ��ɣ��ж��Ƿ���ͼ������
        //2.1 Y �жϵ�ǰҳ���Ƿ�λ�ô�ӡͼ��
        //2.1.1 Y �жϵ�ǰҳ�ܷ��ӡ�����е�ͼ��
        //2.1.1.1 Y ��ӡ GoTo 2.2
        //2.1.1.2 N ��ӡ ��ҳ GoTo 2.1
        //2.1.2 N �жϵ�ǰҳ���Ƿ�λ�ô�ӡͼ��
        //2.1.2.1 Y ��ӡ
        //2.1.2.2 N ��ҳ GoTo 2.1
        //2.2 N ��ӡ���������ز���

        private clsPrintPerPageInfo[] m_objConstructPrintPageInfo(DataTable p_dtbResult, float p_fltX, float p_fltY,
            float p_fltWidth, float p_fltHeight, float p_fltMaxHeight)
        {
            //���˳�������ݺ�ͼ������
            DataView dtvData = m_dtvFilterRows(p_dtbResult, "IS_GRAPH_RESULT_NUM = 0");
            DataView dtvImage = m_dtvFilterRows(p_dtbResult, "IS_GRAPH_RESULT_NUM = 1");

            //����
            dtvData.Sort = "REPORT_PRINT_SEQ_INT ASC,GROUPID_CHR ASC,SAMPLE_PRINT_SEQ_INT ASC";

            // 
            clsSampleResultInfo[] objDataArr = m_objConstructSampleResultArr(dtvData);
            clsPrintImage[] objImgArr = m_objConstructPrintImage(dtvImage);

            #region xing.chen add 2005.9.22
            float fltImgHeight = 0;
            if (objImgArr != null && objImgArr.Length > 0)
            {
                fltImgHeight = objImgArr[0].m_fltHeight + 10;
            }
            #endregion

            int intPage = 0;

            //��ӡ���ҳ
            ArrayList arlPageData = new ArrayList();

            #region ������ݴ�ӡ��ҳ
            float fltLeft = 0;
            float fltRight = 0;
            float fltTitleHeight = m_fltGetPrintElementHeight(m_fntSmallBold, m_fltTitleSpace);
            float fltItemHeight = m_fltGetPrintElementHeight(m_fntSmall2NotBold, m_fltItemSpace);
            //��¼��ҳʣ��ļ�¼����
            int intTotalLeftItemCount = dtvData.Count;
            float fltHeight = 0;
            if (intTotalLeftItemCount * fltItemHeight + objDataArr.Length * fltTitleHeight <= (p_fltHeight - fltImgHeight) * 2)	//xing.chen modify
            {
                fltHeight = p_fltHeight - fltImgHeight;	//xing.chen modify
            }
            else
            {
                fltHeight = p_fltMaxHeight - fltImgHeight;	//xing.chen modify
            }

            ArrayList arlPrintData = new ArrayList();
            //ָʾ��ǰ�Ƿ����ұߴ�ӡ
            bool blnPrintRight = false;
            for (int i = 0; i < objDataArr.Length; i++)
            {
                int intDataCount = objDataArr[i].m_dtvResult.Count;
                objDataArr[i].m_fltHeight = m_fltGetPrintGroupHeight(objDataArr[i], m_fntSmallBold, m_fntSmall2NotBold, m_fltTitleSpace, m_fltItemSpace);
                //��ߴ�ӡ
                if (!blnPrintRight && objDataArr[i].m_fltHeight < fltHeight - fltLeft)
                {
                    objDataArr[i].m_fltX = p_fltX;
                    objDataArr[i].m_fltY = fltLeft + p_fltY;
                    objDataArr[i].m_intStartIdx = 0;
                    objDataArr[i].m_intCount = objDataArr[i].m_dtvResult.Count;
                    objDataArr[i].m_intPageIdx = intPage;
                    fltLeft += objDataArr[i].m_fltHeight + m_fltTitleSpace;
                    arlPrintData.Add(objDataArr[i]);
                    intTotalLeftItemCount -= objDataArr[i].m_intCount;
                }
                else
                {
                    //�ж����µļ�¼�ܷ�����һ�ߴ���,���ҵ�ǰ�Ѿ���ӡ�ļ�¼����������ڻ���ڵ��д�ӡ������1/2
                    if (fltLeft >= fltHeight / 2 && fltItemHeight * intTotalLeftItemCount < fltHeight)
                    {
                        blnPrintRight = true;
                        objDataArr[i].m_fltX = p_fltX + p_fltWidth / 2;
                        objDataArr[i].m_fltY = fltRight + p_fltY;
                        objDataArr[i].m_intStartIdx = 0;
                        objDataArr[i].m_intCount = objDataArr[i].m_dtvResult.Count;
                        objDataArr[i].m_intPageIdx = intPage;
                        fltRight += objDataArr[i].m_fltHeight + m_fltTitleSpace;
                        arlPrintData.Add(objDataArr[i]);
                        intTotalLeftItemCount -= objDataArr[i].m_intCount;
                    }
                    else
                    {
                        while (intDataCount > 0)
                        {
                            if (fltTitleHeight + fltItemHeight < fltHeight - fltLeft)
                            {
                                int intPrintItemCount = 1;
                                while ((intPrintItemCount + 1) * fltItemHeight + fltTitleHeight < fltHeight - fltLeft)
                                {
                                    intPrintItemCount++;
                                }
                                clsSampleResultInfo obj = new clsSampleResultInfo(objDataArr[i].m_dtvResult);
                                obj.m_strPrintTitle = objDataArr[i].m_strPrintTitle;
                                obj.m_fltX = p_fltX;
                                obj.m_fltY = fltLeft + p_fltY;
                                obj.m_intStartIdx = objDataArr[i].m_dtvResult.Count - intDataCount;
                                obj.m_intCount = intPrintItemCount;
                                obj.m_intPageIdx = intPage;
                                fltLeft += intPrintItemCount * fltItemHeight + fltTitleHeight + m_fltTitleSpace;
                                arlPrintData.Add(obj);
                                intDataCount -= intPrintItemCount;
                                intTotalLeftItemCount -= intPrintItemCount;
                            }
                            else
                            {
                                //�ұߴ�ӡ
                                if (fltTitleHeight + fltItemHeight * intDataCount < fltHeight - fltRight)
                                {
                                    clsSampleResultInfo obj = new clsSampleResultInfo(objDataArr[i].m_dtvResult);
                                    obj.m_strPrintTitle = objDataArr[i].m_strPrintTitle;
                                    obj.m_fltX = p_fltX + p_fltWidth / 2;
                                    obj.m_fltY = fltRight + p_fltY;
                                    obj.m_intStartIdx = objDataArr[i].m_dtvResult.Count - intDataCount;
                                    obj.m_intCount = intDataCount;
                                    obj.m_intPageIdx = intPage;
                                    fltRight += intDataCount * fltItemHeight + fltTitleHeight + m_fltTitleSpace;
                                    arlPrintData.Add(obj);
                                    intDataCount -= intDataCount;
                                    intTotalLeftItemCount -= intDataCount;
                                }
                                else
                                {
                                    if (fltTitleHeight + fltItemHeight < fltHeight - fltRight)
                                    {
                                        int intPrintItemCount = 1;
                                        while ((intPrintItemCount + 1) * fltItemHeight + fltTitleHeight < fltHeight - fltRight)
                                        {
                                            intPrintItemCount++;
                                        }
                                        clsSampleResultInfo obj = new clsSampleResultInfo(objDataArr[i].m_dtvResult);
                                        obj.m_strPrintTitle = objDataArr[i].m_strPrintTitle;
                                        obj.m_fltX = p_fltX + p_fltWidth / 2;
                                        obj.m_fltY = fltRight + p_fltY;
                                        obj.m_intStartIdx = objDataArr[i].m_dtvResult.Count - intDataCount;
                                        obj.m_intCount = intPrintItemCount;
                                        obj.m_intPageIdx = intPage;
                                        fltRight += intPrintItemCount * fltItemHeight + fltTitleHeight + m_fltTitleSpace;
                                        arlPrintData.Add(obj);
                                        intDataCount -= intPrintItemCount;
                                        intTotalLeftItemCount -= intPrintItemCount;
                                    }
                                    else
                                    {
                                        fltLeft = 0;
                                        fltRight = 0;
                                        blnPrintRight = false;
                                        intPage++;
                                        arlPageData.Add(arlPrintData);
                                        arlPrintData = new ArrayList();
                                        if (intTotalLeftItemCount * fltItemHeight + objDataArr.Length * fltTitleHeight <= p_fltHeight * 2)
                                        {
                                            fltHeight = p_fltHeight;
                                        }
                                        else
                                        {
                                            fltHeight = p_fltMaxHeight;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (arlPrintData.Count > 0)
            {
                arlPageData.Add(arlPrintData);
            }
            #endregion

            float fltY = Math.Max(fltLeft, fltRight);
            //			fltY += 4*m_fltTitleSpace;
            int intImgStartIdx = intPage;
            ArrayList arlPageImg = null;
            ArrayList arlImg = null;

            #region ͼ�����ݴ�ӡ��ҳ
            if (objImgArr != null && objImgArr.Length > 0)
            {
                arlPageImg = new ArrayList();
                arlImg = new ArrayList();
                float fltX = 0;
                for (int i = 0; i < objImgArr.Length; i++)
                {
                    if (objImgArr[i].m_fltHeight < p_fltMaxHeight && objImgArr[i].m_fltWidth < p_fltWidth)
                    {
                        bool blnDrawed = false;
                        while (!blnDrawed)
                        {
                            if (p_fltMaxHeight - fltY > objImgArr[i].m_fltHeight)
                            {
                                if (p_fltWidth - fltX > objImgArr[i].m_fltWidth)
                                {
                                    objImgArr[i].m_fltX = (fltX == 0 ? fltX + p_fltX : fltX + p_fltX + m_fltImgSpace);
                                    objImgArr[i].m_fltY = fltY + p_fltY;
                                    objImgArr[i].m_intPageIdx = intPage;
                                    arlImg.Add(objImgArr[i]);
                                    fltX += objImgArr[i].m_fltWidth + m_fltImgSpace;
                                    blnDrawed = true;
                                }
                                else
                                {
                                    if (i > 0)
                                    {
                                        fltY += objImgArr[i].m_fltHeight + m_fltImgSpace;
                                        fltX = 0;
                                    }
                                }
                            }
                            else
                            {
                                fltX = 0;
                                fltY = 0;
                                if (arlImg.Count > 0)
                                {
                                    arlPageImg.Add(arlImg);
                                    arlImg = new ArrayList();
                                }
                                intPage++;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (arlImg.Count > 0)
                {
                    arlPageImg.Add(arlImg);
                }
            }
            #endregion

            //ʵ������ʾ
            string strSummary = m_dtbSample.Rows[0]["SUMMARY_VCHR"].ToString().Trim();
            SizeF sf = m_rectGetPrintStringRectangle(m_fntSmallBold, m_fntSmallNotBold, strSummary, m_fltPrintWidth, m_fltTitleSpace,
                m_fltItemSpace);
            if (sf.Height > 0 && sf.Height > p_fltMaxHeight - fltY)
            {
                intPage++;
            }

            #region ����ҳ���ӡ��Ϣ
            clsPrintPerPageInfo[] objArr = new clsPrintPerPageInfo[intPage + 1];
            int intStartImgIdx = -1;
            if (arlPageImg != null)
            {
                intStartImgIdx = ((clsPrintImage[])((ArrayList)arlPageImg[0]).ToArray(typeof(clsPrintImage)))[0].m_intPageIdx;
            }
            for (int i = 0; i < objArr.Length; i++)
            {
                objArr[i] = new clsPrintPerPageInfo();
                if (i <= arlPageData.Count - 1)
                {
                    objArr[i].m_objSampleArr = (clsSampleResultInfo[])((ArrayList)arlPageData[i]).ToArray(typeof(clsSampleResultInfo));
                }
                if (arlPageImg != null)
                {
                    if (intStartImgIdx <= i && i <= intStartImgIdx + arlPageImg.Count - 1)
                    {
                        objArr[i].m_imgArr = (clsPrintImage[])((ArrayList)arlPageImg[i - intStartImgIdx]).ToArray(typeof(clsPrintImage));
                    }
                }
            }
            #endregion

            return objArr;
        }
        #endregion

        #region FunctionMethod
        /// <summary>
        /// ��ȡ��ӡ��ĸ߶�
        /// </summary>
        /// <param name="p_objData"></param>
        /// <param name="p_fntTitle"></param>
        /// <param name="p_fntItem"></param>
        /// <param name="p_fltTitleSpace"></param>
        /// <param name="p_fltItemSpace"></param>
        /// <returns></returns>
        private float m_fltGetPrintGroupHeight(clsSampleResultInfo p_objData, Font p_fntTitle, Font p_fntItem,
            float p_fltTitleSpace, float p_fltItemSpace)
        {
            float fltHeight = 0;
            fltHeight += (p_fntTitle.Height + p_fltTitleSpace) + (p_objData.m_intCount * (p_fntItem.Height + p_fltItemSpace));
            return fltHeight;
        }

        private float m_fltGetPrintElementHeight(Font p_fnt, float p_fltPrintSpace)
        {
            float fltHeight = 0;
            fltHeight += p_fnt.Height + p_fltPrintSpace;
            return fltHeight;
        }

        /// <summary>
        /// �����ӡ����
        /// </summary>
        /// <param name="p_dtvData"></param>
        /// <returns></returns>
        private clsSampleResultInfo[] m_objConstructSampleResultArr(DataView p_dtvData)
        {
            ArrayList arlGroupID = new ArrayList();
            clsSampleResultInfo[] objArr = null;
            for (int i = 0; i < p_dtvData.Count; i++)
            {
                if (i > 0)
                {
                    if (p_dtvData[i]["groupid_chr"].ToString().Trim() != p_dtvData[i - 1]["groupid_chr"].ToString().Trim())
                    {
                        arlGroupID.Add(p_dtvData[i]["groupid_chr"].ToString().Trim());
                    }
                }
                else
                {
                    arlGroupID.Add(p_dtvData[i]["groupid_chr"].ToString().Trim());
                }
            }
            if (arlGroupID.Count > 0)
            {
                objArr = new clsSampleResultInfo[arlGroupID.Count];
                for (int i = 0; i < arlGroupID.Count; i++)
                {
                    DataView dtv = new DataView(p_dtvData.Table);
                    dtv.RowFilter = "IS_GRAPH_RESULT_NUM = 0 AND groupid_chr = " + arlGroupID[i].ToString().Trim();
                    objArr[i] = new clsSampleResultInfo(dtv);
                    objArr[i].m_dtvResult.Sort = "SAMPLE_PRINT_SEQ_INT ASC";
                    objArr[i].m_strPrintTitle = dtv[0]["print_title_vchr"].ToString().Trim();
                    objArr[i].m_fltHeight = m_fltGetPrintGroupHeight(objArr[i], m_fntSmallBold, m_fntSmall2NotBold, m_fltTitleSpace, m_fltItemSpace);
                    objArr[i].m_intCount = objArr[i].m_dtvResult.Count;
                }
            }
            return objArr;
        }

        /// <summary>
        /// �����ӡͼ��
        /// </summary>
        /// <param name="p_dtvData"></param>
        /// <returns></returns>
        private clsPrintImage[] m_objConstructPrintImage(DataView p_dtvData)		// xing.chen modify 2005.9.22
        {
            int intCount = p_dtvData.Count;
            clsPrintImage[] objImgArr = null;
            ArrayList arl = new ArrayList();
            for (int i = 0; i < intCount; i++)
            {
                if (p_dtvData[i]["GRAPH_IMG"] is System.DBNull)
                {
                    continue;
                }
                Image img = m_imgDrawGraphic((byte[])p_dtvData[i]["GRAPH_IMG"], p_dtvData[i]["GRAPH_FORMAT_NAME_VCHR"].ToString());
                if (img != null)
                {
                    clsPrintImage objImg = new clsPrintImage(img);
                    objImg.m_fltWidth = m_fltXRate * objImg.m_fltWidth;
                    objImg.m_fltHeight = m_fltYRate * objImg.m_fltHeight;
                    arl.Add(objImg);
                }
            }
            objImgArr = (clsPrintImage[])arl.ToArray(typeof(clsPrintImage));
            return objImgArr;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="p_dtbSource"></param>
        /// <param name="p_strFltExp"></param>
        /// <returns></returns>
        private DataView m_dtvFilterRows(DataTable p_dtbSource, string p_strFltExp)
        {
            DataView dtv = new DataView(p_dtbSource);
            dtv.RowFilter = p_strFltExp;
            return dtv;
        }
        #endregion

        #region ��ӡ���ű��浥
        private void m_mthPrint()
        {
            m_mthPrintEnd_DGCS();
            m_mthPrintBseInfo();
            m_mthPrintDetail();
            //m_mthPrintEnd();
            if (m_intTotalPage - 1 > m_intCurrentPageIdx)
            {
                m_intCurrentPageIdx++;
            }
        }
        #endregion

        #region �̳д�ӡ�ӿ�

        public void m_mthInitPrintContent()
        {
        }

        public void m_mthInitPrintTool(object p_objArg)
        {
            m_mthInitalPrintTool((PrintDocument)p_objArg);
        }

        public void m_mthDisposePrintTools(object p_objArg)
        {
        }

        public void m_mthBeginPrint(object p_objPrintArg)
        {
            m_dtbSample = ((clsPrintValuePara)p_objPrintArg).m_dtbBaseInfo;
            m_dtbResult = ((clsPrintValuePara)p_objPrintArg).m_dtbResult;
        }

        public void m_mthPrintPage(object p_objPrintArg)
        {
            m_printMethodTool = new clsCommonPrintMethod((PrintPageEventArgs)p_objPrintArg);
            m_mthPrint();
        }

        public void m_mthEndPrint(object p_objPrintArg)
        {
        }

        #endregion
    }

    #region ��װ��ӡ��صķ���
    //	public class clsCommonPrintMethod
    //	{
    //		//PrintPageEventArgs
    //		public PrintPageEventArgs m_printEventArg;
    //		SizeF m_sf;
    //		float m_fltCurrentX;
    //		float m_fltBseSpace = 5;
    //
    //		#region ˵���ı�������֮��Ļ������
    //		/// <summary>
    //		/// ˵���ı�������֮��Ļ������
    //		/// </summary>
    //		public float m_fltTextBseSpace
    //		{
    //			get
    //			{
    //				return m_fltBseSpace;
    //			}
    //			set
    //			{
    //				m_fltBseSpace = value;
    //			}
    //		}
    //		#endregion
    //
    //		#region ���캯��
    //		//Constructor
    //		public clsCommonPrintMethod(PrintPageEventArgs p_objPrintArg)
    //		{
    //			m_printEventArg = p_objPrintArg;
    //		}
    //		#endregion
    //
    //		#region ��ӡ˵���ı�������
    //		/// <summary>
    //		/// ��ӡ˵���ı�������
    //		/// </summary>
    //		/// <param name="p_fntText">�ı�����</param>
    //		/// <param name="p_strText">˵���ı�</param>
    //		/// <param name="p_strContent">�����ı�</param>
    //		/// <param name="p_fltX">��ӡX�������ʼλ��</param>
    //		/// <param name="p_fltY">��ӡY�������ʼλ��</param>
    //		public void m_mthDrawTextAndContent(Font p_fntText,Font p_fntContent,string p_strText,string p_strContent,float p_fltX,float p_fltY)
    //		{
    //			try
    //			{
    //				m_mthDrawString(p_strText,p_fntText,p_fltX,p_fltY);
    //				m_fltCurrentX = p_fltX + m_fltGetStringWidth(p_strText,p_fntText) + m_fltBseSpace;
    //				m_mthDrawString(p_strContent,p_fntContent,m_fltCurrentX,p_fltY);
    //			}
    //			catch{}
    //		}
    //		#endregion
    //
    //		#region ��ȡ�ַ����Ŀ��
    //		/// <summary>
    //		/// ��ȡ�ַ����Ŀ��
    //		/// </summary>
    //		/// <param name="p_str">�ַ���</param>
    //		/// <param name="m_fnt">����</param>
    //		/// <returns></returns>
    //		public float m_fltGetStringWidth(string p_str,Font m_fnt)
    //		{
    //			try
    //			{
    //				m_sf = m_printEventArg.Graphics.MeasureString(p_str,m_fnt);
    //			}
    //			catch
    //			{
    //				return 0;
    //			}
    //			return m_sf.Width;
    //		}
    //		#endregion
    //
    //		#region ��ȡ�ַ����ĸ߶�
    //		/// <summary>
    //		/// ��ȡ�ַ����ĸ߶�
    //		/// </summary>
    //		/// <param name="p_str">�ַ���</param>
    //		/// <param name="m_fnt">����</param>
    //		/// <returns></returns>
    //		public float m_fltGetStringHeight(string p_str,Font m_fnt)
    //		{
    //			try
    //			{
    //				m_sf = m_printEventArg.Graphics.MeasureString(p_str,m_fnt);
    //			}
    //			catch
    //			{
    //				return 0;
    //			}
    //			return m_sf.Height;
    //		}
    //		#endregion
    //
    //		#region ��ӡ����
    //		/// <summary>
    //		/// ��ӡ����
    //		/// </summary>
    //		/// <param name="p_str">�ַ���</param>
    //		/// <param name="m_fnt">����</param>
    //		/// <param name="m_fltY">��ӡλ�ø߶�</param>
    //		public void m_mthPrintTitle(string p_str,Font m_fnt,float p_fltY,float p_fltWidth)
    //		{
    //			try
    //			{
    //				m_sf = m_printEventArg.Graphics.MeasureString(p_str,m_fnt);
    //				m_printEventArg.Graphics.DrawString(p_str,m_fnt,Brushes.Black,(p_fltWidth-m_sf.Width)/2,p_fltY);
    //			}
    //			catch
    //			{
    //			}
    //		}
    //		#endregion
    //
    //		#region ��ӡ�ַ���
    //		/// <summary>
    //		/// ��ӡ�ַ���
    //		/// </summary>
    //		/// <param name="p_str">��ӡ�ַ���</param>
    //		/// <param name="p_fnt">����</param>
    //		/// <param name="p_fltX">X</param>
    //		/// <param name="p_fltY">Y</param>
    //		public void m_mthDrawString(string p_str,Font p_fnt,float p_fltX,float p_fltY)
    //		{
    //			try
    //			{
    //				m_printEventArg.Graphics.DrawString(p_str,p_fnt,Brushes.Black,p_fltX,p_fltY);
    //			}
    //			catch
    //			{
    //			}
    //		}
    //		#endregion
    //
    //		#region �Ҷ����ı�
    //		public float m_fltFlushRightText(float p_fltStartX,string p_strSourceText,string p_strText,Font p_fnt)
    //		{
    //			try
    //			{
    //				SizeF sf1 = m_printEventArg.Graphics.MeasureString(p_strSourceText,p_fnt);
    //				SizeF sf2 = m_printEventArg.Graphics.MeasureString(p_strText,p_fnt);
    //				p_fltStartX += sf1.Width - sf2.Width;
    //			}
    //			catch
    //			{
    //				return p_fltStartX;
    //			}
    //			return p_fltStartX;
    //		}
    //		#endregion
    //
    //		#region ����
    //		public void m_mthDrawLine(float p_fltStartX,float p_fltStartY,float p_fltEndX,float p_fltEndY)
    //		{
    //			try
    //			{
    //				m_printEventArg.Graphics.DrawLine(Pens.Black,p_fltStartX,p_fltStartY,p_fltEndX,p_fltEndY);
    //			}
    //			catch
    //			{
    //			}
    //		}
    //		#endregion
    //	}
    #endregion

    #region ҳ��ӡ��Ϣ��װ
    /// <summary>
    /// ҳ��ӡ��Ϣ��װ
    /// </summary>
    //	public class clsPrintPerPageInfo
    //	{
    //		/// <summary>
    //		/// ���������Ϣ����
    //		/// </summary>
    //		public clsSampleResultInfo[] m_objSampleArr;
    //		/// <summary>
    //		/// ͼ������
    //		/// </summary>
    //		public clsPrintImage[] m_imgArr;
    //		/// <summary>
    //		/// �Ƿ�ֱߴ�ӡ
    //		/// </summary>
    //		public bool m_blnHasTwoPart;
    //	}
    //
    //	public class clsSampleResultInfo
    //	{
    //		/// <summary>
    //		/// ���캯��
    //		/// </summary>
    //		/// <param name="p_dtv"></param>
    //		public clsSampleResultInfo(DataView p_dtv)
    //		{
    //			m_dtvResult = p_dtv;
    //		}
    //
    //		/// <summary>
    //		/// ��ӡ���
    //		/// </summary>
    //		public DataView m_dtvResult;
    //		/// <summary>
    //		/// ��ӡ����
    //		/// </summary>
    //		public string m_strPrintTitle;
    //		/// <summary>
    //		/// ��ӡ���Ϸ�Xλ��
    //		/// </summary>
    //		public float m_fltX;
    //		/// <summary>
    //		/// ��ӡ���Ϸ�Yλ��
    //		/// </summary>
    //		public float m_fltY;
    //		/// <summary>
    //		/// ��ӡ�Ŀ��
    //		/// </summary>
    //		public float m_fltWidth;
    //		/// <summary>
    //		/// ��ӡ�ĸ߶�
    //		/// </summary>
    //		public float m_fltHeight;
    //		/// <summary>
    //		/// ��ӡ��ʼ����
    //		/// </summary>
    //		public int m_intStartIdx;
    //		/// <summary>
    //		/// ��ӡ����
    //		/// </summary>
    //		public int m_intCount;
    //		/// <summary>
    //		/// ��ӡ��ҳ��
    //		/// </summary>
    //		public int m_intPageIdx;
    //	}
    //
    //	public class clsPrintImage
    //	{
    //		/// <summary>
    //		/// ���캯��
    //		/// </summary>
    //		/// <param name="p_img"></param>
    //		public clsPrintImage(Image p_img)
    //		{
    //			m_img = p_img;
    //			m_fltWidth = p_img.Width;
    //			m_fltHeight = p_img.Height;
    //		}
    //
    //		/// <summary>
    //		/// ͼ��
    //		/// </summary>
    //		public Image m_img;
    //		/// <summary>
    //		/// ��ӡ���Ϸ�Xλ��
    //		/// </summary>
    //		public float m_fltX;
    //		/// <summary>
    //		/// ��ӡ���Ϸ�Yλ��
    //		/// </summary>
    //		public float m_fltY;
    //		/// <summary>
    //		/// ͼ�ο��
    //		/// </summary>
    //		public float m_fltWidth;
    //		/// <summary>
    //		/// ͼ�θ߶�
    //		/// </summary>
    //		public float m_fltHeight;
    //		/// <summary>
    //		/// ��ӡҳ��
    //		/// </summary>
    //		public int m_intPageIdx;
    //	}
    #endregion

}
