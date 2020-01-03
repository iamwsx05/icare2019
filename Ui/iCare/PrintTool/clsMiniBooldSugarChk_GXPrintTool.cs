using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.Utility.Controls;
using System.Drawing;

namespace iCare
{
    /// <summary>
    /// ����΢��Ѫ�Ǽ���¼��(����)��ӡ����
    /// </summary>
    class clsMiniBooldSugarChk_GXPrintTool : infPrintRecord
    {
        private clsMiniBooldSugarChk_GXDomin m_objDomain;
        private clsPatient m_objPatient;
        private clsMiniBloodSugarChkValue_GX[] m_objValues;

        public void m_mthSetPrintInfo(clsPatient p_objPatient, DateTime p_dtmInPatientDate, DateTime p_dtmOpenDate)
        {
            m_objPatient = p_objPatient;
        }
        public void m_mthSetPrintInfo(clsPatient p_objPatient)
        {
            m_objPatient = p_objPatient;
        }

        /// <summary>
        /// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���(�������ݿ��ȡʱҪ����.)
        /// </summary>
        public void m_mthInitPrintContent()
        {
            if (m_objPatient == null)
            {
                return;
            }
            if (m_objPatient.m_StrInPatientID == "" || m_objPatient.m_DtmSelectedInDate == DateTime.MinValue)
                m_objValues = null;
            else
            {
                m_objDomain = new clsMiniBooldSugarChk_GXDomin();
                long lngRes = m_objDomain.m_lngGetRecoedByInPatient(m_objPatient.m_StrInPatientID, m_objPatient.m_DtmSelectedInDate, out m_objValues);
                if (lngRes <= 0 || m_objValues == null)
                    return;
            }
        }

        /// <summary>
        /// ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
        /// </summary>
        /// <param name="p_objPrintContent">��ӡ����</param>
        public void m_mthSetPrintContent(object p_objPrintContent)
        {
        }

        /// <summary>
        /// ��ȡ��ӡ����,(�������ݿ��ȡʱ,���ñ�����֮ǰ�����ȵ���m_mthSetPrintInfo����)
        /// </summary>
        /// <returns>��ӡ����</returns>
        public object m_objGetPrintInfo()
        {
            return m_objValues;

        }

        /// <summary>
        /// ��ʼ����ӡ����,��������ն��󼴿�.
        /// </summary>
        public void m_mthInitPrintTool(object p_objArg)
        {
            #region �йش�ӡ��ʼ��
            m_fotTitleFont = new Font("SimSun", 16, FontStyle.Bold);
            m_fotHeaderFont = new Font("SimSun", 10.5F);//�������
            m_fotSmallFont = new Font("SimSun", 11);
            m_GridPen = new Pen(Color.Black, 1);
            m_slbBrush = new SolidBrush(Color.Black);

            #endregion
        }

        /// <summary>
        /// �ͷŴ�ӡ����
        /// </summary>
        public void m_mthDisposePrintTools(object p_objArg)
        {
            m_fotTitleFont.Dispose();
            m_fotHeaderFont.Dispose();
            m_fotSmallFont.Dispose();
            m_GridPen.Dispose();
            m_slbBrush.Dispose();
        }

        /// <summary>
        /// ��ӡ��ʼ
        /// </summary>
        /// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintEventArgs���͵Ķ���</param>
        public void m_mthBeginPrint(object p_objPrintArg)
        { }

        /// <summary>
        /// ��ӡ��
        /// </summary>
        /// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintPageEventArgs���͵Ķ���</param>
        public void m_mthPrintPage(object p_objPrintArg)
        {
            frmPrintPreviewDialogPF frmPreview = new frmPrintPreviewDialogPF();
            frmPreview.m_evtBeginPrint += new PrintEventHandler(frmPreview_m_evtBeginPrint);
            frmPreview.m_evtEndPrint += new PrintEventHandler(frmPreview_m_evtEndPrint);
            frmPreview.m_evtPrintContent += new PrintPageEventHandler(frmPreview_m_evtPrintContent);
            frmPreview.m_evtPrintFrame += new PrintPageEventHandler(frmPreview_m_evtPrintFrame);
            frmPreview.ShowDialog();
        }

        #region  �����¼�
        private void frmPreview_m_evtBeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            m_mthBeginPrint(e);
        }
        private void frmPreview_m_evtEndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            m_mthEndPrint(e);
        }
        private void frmPreview_m_evtPrintContent(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            m_mthAddDataToGrid(e);
        }
        private void frmPreview_m_evtPrintFrame(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            m_mthPrintTitleInfo(e);
            m_mthPrintRectangleInfo(e);
        }
        #endregion

        /// <summary>
        /// ��ӡ������һ��ʹ�������������ݿ���Ϣ��
        /// </summary>
        /// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintEventArgs���͵Ķ���</param>
        public void m_mthEndPrint(object p_objPrintArg)
        {
            m_intLines = 0;
            m_intPages = 1;
        }

        // ��ӡҳ
        private void m_mthPrintPageSub(PrintPageEventArgs e)
        { }

        #region ��ӡ

        #region �йش�ӡ������

        /// <summary>
        /// ���������
        /// </summary>
        private Font m_fotTitleFont;
        /// <summary>
        /// ��ͷ������
        /// </summary>
        private Font m_fotHeaderFont;
        /// <summary>
        /// �����ݵ�����
        /// </summary>
        private Font m_fotSmallFont;
        /// <summary>
        /// �߿򻭱�
        /// </summary>
        private Pen m_GridPen;
        /// <summary>
        /// ˢ��
        /// </summary>
        private SolidBrush m_slbBrush;

        private int m_intPages = 1;
        private int m_intLines = 0;
        private int m_intLineStep = 135;
        private string m_strDateFormat = "yyyy-MM-dd HH:mm";

        /// <summary>
        /// ���ӵ���Ϣ
        /// </summary>
        private enum enmRectangleInfo
        {
            /// <summary>
            /// ���ӵĶ���
            /// </summary>
            TopY = 130,
            ///<summary>
            /// ���ӵ����
            /// </summary>
            LeftX = 40,
            /// <summary>
            /// ���ӵ��Ҷ�
            /// </summary>
            RightX = 820 - 28,
            /// <summary>
            /// ����ÿ�еĲ���
            /// </summary>
            RowStep = 25,

            BottomY = 1034
        }


        #endregion

        #region �������ֲ���
        /// <summary>
        /// �������ֲ���
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, m_fotHeaderFont, m_slbBrush, 320, 40);

            e.Graphics.DrawString("����΢��Ѫ�Ǽ���¼��", m_fotTitleFont, m_slbBrush, 270, 60);

            e.Graphics.DrawString("������", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX, 100);
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + 50, 120, (int)enmRectangleInfo.LeftX + 120, 120);
            e.Graphics.DrawString((m_objPatient == null ? "" : m_objPatient.m_StrName), m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 50, 100);

            e.Graphics.DrawString("�Ա�", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 130, 100);
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + 175, 120, (int)enmRectangleInfo.LeftX + 195, 120);
            e.Graphics.DrawString((m_objPatient == null ? "" : m_objPatient.m_StrSex), m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 175, 100);

            e.Graphics.DrawString("���䣺", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 205, 100);
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + 250, 120, (int)enmRectangleInfo.LeftX + 330, 120);
            e.Graphics.DrawString((m_objPatient == null ? "" : m_objPatient.m_ObjPeopleInfo.m_StrAge), m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 250, 100);

            e.Graphics.DrawString("���ң�", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 350, 100);
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + 395, 120, (int)enmRectangleInfo.LeftX + 470, 120);
            e.Graphics.DrawString((m_objPatient == null ? "" : m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(m_objPatient.m_DtmSelectedInDate).m_ObjInBedInfo.m_ObjLastDeptInfo.m_ObjDept.m_StrDeptName), m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 395, 100);

            e.Graphics.DrawString("סԺ�ţ�", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 490, 100);
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + 550, 120, (int)enmRectangleInfo.LeftX + 630, 120);
            e.Graphics.DrawString((m_objPatient == null ? "" : m_objPatient.m_StrHISInPatientID.Trim()), m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 550, 100);

            e.Graphics.DrawString("���ţ�", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 640, 100);
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + 695, 120, (int)enmRectangleInfo.LeftX + 740, 120);
            string strTemp;
            try
            {
                strTemp = m_objPatient == null ? "" : m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName.ToString().Trim();
            }
            catch
            {
                strTemp = "";
            }
            e.Graphics.DrawString(strTemp, m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 695, 100);
        }


        #endregion

        private void m_mthPrintRectangleInfo(PrintPageEventArgs e)
        {
            int intYPos = 125;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, intYPos, (int)enmRectangleInfo.LeftX, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.RightX, intYPos, (int)enmRectangleInfo.RightX, (int)enmRectangleInfo.BottomY);

            m_fotSmallFont = new Font("SimSun", 9);
            int intXPos = 120;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("     ��    ��", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX, 133);
            e.Graphics.DrawString("  �ո�\r\n(mmol/L)", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 128);
            intXPos += 64;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("���2H\r\n(mmol/L)", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 128);
            intXPos += 64;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("�в�ǰ11\r\nam(mmol/L)", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos, 128);
            intXPos += 64;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("�вͺ�2H\r\n(mmol/L)", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 128);
            intXPos += 64;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("���ǰ5pm\r\n(mmol/L)", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 128);
            intXPos += 64;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("�賿0am\r\n(mmol/L)", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 128);
            intXPos += 64;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("�賿3am\r\n(mmol/L)", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 128);
            intXPos += 64;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("���Ѫ��\r\n(mmol/L)", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 128);
            intXPos += 64;

            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            StringFormat m_sfmPrint = new StringFormat(StringFormatFlags.FitBlackBox);
            m_sfmPrint.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(m_objValues == null ? "" : m_objValues[0].m_strCustom1Name, m_fotSmallFont, m_slbBrush,
                new RectangleF((int)enmRectangleInfo.LeftX + intXPos + 1, 128, 60, 40), m_sfmPrint);
            intXPos += 60;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString(m_objValues == null ? "" : m_objValues[0].m_strCustom2Name, m_fotSmallFont, m_slbBrush,
                new RectangleF((int)enmRectangleInfo.LeftX + intXPos + 1, 128, 60, 40), m_sfmPrint);

            m_fotSmallFont = new Font("SimSun", 11);
            for (int i = 0; i < 37; i++)
            {
                e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, intYPos, (int)enmRectangleInfo.RightX, intYPos);
                if (i == 0)
                {
                    intYPos += (int)enmRectangleInfo.RowStep + 10;
                }
                else
                    intYPos += (int)enmRectangleInfo.RowStep;
            }
            e.Graphics.DrawString("�� " + m_intPages.ToString() + " ҳ", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 600, intYPos + 30);
        }

        private void m_mthAddDataToGrid(PrintPageEventArgs e)
        {
            if (m_objValues == null)
            {
                e.HasMorePages = false;
                return;
            }
            int intYPos = 162;
            for (; m_intLines < m_objValues.Length; )
            {
                e.Graphics.DrawString(m_objValues[m_intLines].m_dtmCreateDate.ToString(m_strDateFormat), new Font("SimSun", 10), m_slbBrush, (int)enmRectangleInfo.LeftX, intYPos);
                e.Graphics.DrawString(m_objValues[m_intLines].m_strCONTENT_LIMOSIS, m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 121, intYPos);
                e.Graphics.DrawString(m_objValues[m_intLines].m_strCONTENT_BREAKFAST2H, m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 185, intYPos);
                e.Graphics.DrawString(m_objValues[m_intLines].m_strCONTENT_BEFORELUNCH11AM, m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 249, intYPos);
                e.Graphics.DrawString(m_objValues[m_intLines].m_strCONTENT_AFTERLUNCH2H, m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 313, intYPos);
                e.Graphics.DrawString(m_objValues[m_intLines].m_strCONTENT_BEFORESUPPER5PM, m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 377, intYPos);
                e.Graphics.DrawString(m_objValues[m_intLines].m_strCONTENT_WEEHOURS0AM, m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 441, intYPos);
                e.Graphics.DrawString(m_objValues[m_intLines].m_strCONTENT_WEEHOURS3AM, m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 505, intYPos);
                e.Graphics.DrawString(m_objValues[m_intLines].m_strCONTENT_RANDOM, m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 569, intYPos);
                e.Graphics.DrawString(m_objValues[m_intLines].m_strCustom1Content, m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 633, intYPos);
                e.Graphics.DrawString(m_objValues[m_intLines].m_strCustom2Content, m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 693, intYPos);
                intYPos += (int)enmRectangleInfo.RowStep;
                m_intLines++;
                if (m_intLines % 35 == 0)
                {
                    m_intPages++;
                    e.HasMorePages = true;
                    return;
                }
            }
            e.HasMorePages = false;
        }

        #endregion
    }
}
