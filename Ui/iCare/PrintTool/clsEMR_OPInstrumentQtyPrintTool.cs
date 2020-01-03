using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using weCare.Core.Entity;
using com.digitalwave.controls;

namespace iCare
{
    /// <summary>
    /// ������е�����ϵ������ӡ������
    /// </summary>
    public class clsEMR_OPInstrumentQtyPrintTool : infPrintRecord
    {
        private bool m_blnIsFromDataSource = true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
        private bool m_blnWantInit = true;
        private clsPrintInfo_OPInstrument m_objPrintInfo = null;
        private clsEMR_OPInstrument m_objRecordContent = null;
        private clsEMR_OPInstrument_Dict[] m_objDict = null;

        /// <summary>
        /// ���ô�ӡ��Ϣ(�������ݿ��ȡʱҪ���ȵ���.)
        /// </summary>
        /// <param name="p_objPatient">����</param>
        /// <param name="p_dtmInPatientDate">��Ժ����</param>
        /// <param name="p_dtmOpenDate">OpenDate�������һ�δ�ӡ��μ�¼�������ͣ��粡����¼��������OpenDate</param>
        public void m_mthSetPrintInfo(clsPatient p_objPatient, DateTime p_dtmInPatientDate, DateTime p_dtmOpenDate)
        {
            m_blnIsFromDataSource = true;//�����Ǵ����ݿ��ȡ
            clsPatient m_objPatient = p_objPatient;
            m_objPrintInfo = new clsPrintInfo_OPInstrument();
            m_objPrintInfo.m_strInPatentID = m_objPatient != null ? m_objPatient.m_StrInPatientID : "";
            m_objPrintInfo.m_strPatientName = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrFirstName : "";
            m_objPrintInfo.m_strSex = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrSex : "";
            m_objPrintInfo.m_strAge = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrAge : "";
            m_objPrintInfo.m_strBedName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName : "";
            m_objPrintInfo.m_strDeptName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjDept.m_StrDeptName : "";
            m_objPrintInfo.m_strAreaName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName : "";
            m_objPrintInfo.m_dtmInPatientDate = p_dtmInPatientDate;
            m_objPrintInfo.m_dtmOpenDate = p_dtmOpenDate;
            m_objPrintInfo.m_strHISInPatientID = m_objPatient != null ? m_objPatient.m_StrHISInPatientID : "";
            m_objPrintInfo.m_dtmHISInDate = m_objPatient != null ? m_objPatient.m_DtmSelectedHISInDate : DateTime.MinValue;
        }

        /// <summary>
        /// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���(�������ݿ��ȡʱҪ����.)
        /// </summary>
        public void m_mthInitPrintContent()
        {
            m_blnWantInit = false;//
            if (m_objPrintInfo == null)
            {
                clsPublicFunction.ShowInformationMessageBox("����m_mthInitPrintContent֮ǰ�����ȵ���m_mthSetPrintInfo����");
                return;
            }

            clsEMR_OPInstrumentDomain m_objRecordsDomain = new clsEMR_OPInstrumentDomain();
            long lngRes = m_objRecordsDomain.m_lngGetActiveItemsFromDict(out m_objDict);

            if (m_objPrintInfo.m_strInPatentID == "" || m_objPrintInfo.m_dtmOpenDate == DateTime.MinValue)
                m_objRecordContent = null;
            else
            {
                clsTrackRecordContent objContent = new clsTrackRecordContent();
                lngRes = m_objRecordsDomain.m_lngGetRecordContent(m_objPrintInfo.m_strInPatentID, m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"), out objContent);
                if (lngRes <= 0)
                    return;

                m_objRecordContent = (clsEMR_OPInstrument)objContent;
            }
            m_objRecordsDomain = null;
            //���ñ����ݵ���ӡ��			
            m_objPrintInfo.m_objRecordContent = m_objRecordContent;	
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
            return null;
        }

        /// <summary>
        /// ��ʼ����ӡ����,��������ն��󼴿�.
        /// </summary>
        public void m_mthInitPrintTool(object p_objArg)
        {
            #region �йش�ӡ��ʼ��
            m_fotTitleFont = new Font("SimSun", 15.75f, FontStyle.Bold);//��������
            m_fotHeaderFont = new Font("SimSun", 10.5F);//�������
            m_fotSmallFont = new Font("SimSun", 11);
            m_GridPen = new Pen(Color.Black, 1);
            m_slbBrush = new SolidBrush(Color.Black);
            m_objPageSetting = new clsPrintPageSettingForRecord();
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
        {
        }

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

        private void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
        {
            //ȱʡ�����κζ���
        }

        // ��ӡҳ
        private void m_mthPrintPageSub(PrintPageEventArgs e)
        {
        }
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
        /// ��ӡԪ��
        /// </summary>
        private enum enmItemDefination
        {
            //����Ԫ��
            InPatientID_Title,
            InPatientID,
            Name_Title,
            Name,
            Sex_Title,
            Sex,
            Age_Title,
            Age,
            Dept_Name_Title,
            Dept_Name,
            BedNo_Title,
            BedNo,

            Page_HospitalName,
            Page_Name_Title,
            Page_Title,
            Page_Num,
            Page_Of,
            Page_Count,

            Print_Date_Title,
            Print_Date,
            //�����Ԫ��
            RecordDate,
            RecordTime,
            RecordContent,
            RecordSign1,
            RecordSign2

        }
        #endregion
        /// <summary>
        /// ��ȡ�������
        /// </summary>
        private clsPrintPageSettingForRecord m_objPageSetting;

        #region �����ӡ��Ԫ�ص������
        protected class clsPrintPageSettingForRecord
        {
            public clsPrintPageSettingForRecord() { }

            /// <summary>
            /// ��������
            /// </summary>
            /// <param name="p_intItemName">��Ŀ����</param>
            /// <returns></returns>
            public PointF m_getCoordinatePoint(int p_intItemName)
            {
                float fltOffsetX = 20;//X��ƫ����
                PointF m_fReturnPoint;
                switch (p_intItemName)
                {

                    case (int)enmItemDefination.Page_HospitalName:
                        m_fReturnPoint = new PointF(340f - fltOffsetX, 40f);
                        break;
                    case (int)enmItemDefination.Page_Name_Title:
                        m_fReturnPoint = new PointF(310f - fltOffsetX, 70f);
                        break;
                    case (int)enmItemDefination.Name_Title:
                        m_fReturnPoint = new PointF(55f - fltOffsetX, 105f);
                        break;
                    case (int)enmItemDefination.Name:
                        m_fReturnPoint = new PointF(95f - fltOffsetX, 105f);
                        break;

                    case (int)enmItemDefination.Sex_Title:
                        m_fReturnPoint = new PointF(175f - fltOffsetX, 105f);
                        break;
                    case (int)enmItemDefination.Sex:
                        m_fReturnPoint = new PointF(220f - fltOffsetX, 105f);
                        break;

                    case (int)enmItemDefination.Age_Title:
                        m_fReturnPoint = new PointF(250f - fltOffsetX, 105f);
                        break;
                    case (int)enmItemDefination.Age:
                        m_fReturnPoint = new PointF(295f - fltOffsetX, 105f);
                        break;

                    case (int)enmItemDefination.Dept_Name_Title:
                        m_fReturnPoint = new PointF(365f, 105f);
                        break;
                    case (int)enmItemDefination.Dept_Name:
                        m_fReturnPoint = new PointF(415f, 105f);
                        break;

                    case (int)enmItemDefination.BedNo_Title:
                        m_fReturnPoint = new PointF(565f, 105f);
                        break;
                    case (int)enmItemDefination.BedNo:
                        m_fReturnPoint = new PointF(610f, 105f);
                        break;

                    case (int)enmItemDefination.InPatientID_Title:
                        m_fReturnPoint = new PointF(655f - fltOffsetX, 105f);
                        break;
                    case (int)enmItemDefination.InPatientID:
                        m_fReturnPoint = new PointF(720f - fltOffsetX, 105f);
                        break;

                    default:
                        m_fReturnPoint = new PointF(405f, 400f);
                        break;

                }
                return m_fReturnPoint;
            }
        }

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
            RightX = 820 - 33,
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
            e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, m_fotHeaderFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_HospitalName));

            e.Graphics.DrawString("������е�����ϵ�����", m_fotTitleFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_Name_Title));


            e.Graphics.DrawString("������", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name_Title));
            e.Graphics.DrawString(m_objPrintInfo.m_strPatientName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name));

            e.Graphics.DrawString("�Ա�", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex_Title));
            e.Graphics.DrawString(m_objPrintInfo.m_strSex, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex));

            e.Graphics.DrawString("���䣺", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age_Title));
            e.Graphics.DrawString(m_objPrintInfo.m_strAge, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age));

            e.Graphics.DrawString("���ң�", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name_Title));
            e.Graphics.DrawString(m_objPrintInfo.m_strDeptName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name));

            e.Graphics.DrawString("���ţ�", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo_Title));
            e.Graphics.DrawString(m_objPrintInfo.m_strBedName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo));	

            e.Graphics.DrawString("סԺ�ţ�", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID_Title));
            e.Graphics.DrawString(m_objPrintInfo.m_strHISInPatientID, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID));
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
            e.Graphics.DrawString("��  ��", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 1, 138);
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, intYPos, (int)enmRectangleInfo.LeftX + intXPos, intYPos + (int)enmRectangleInfo.RowStep + 10);
            e.Graphics.DrawString("��  ��", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 70, 133);
            e.Graphics.DrawString("��ǰ\r\n���", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 128);
            intXPos += 43;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("��ǰ\r\n�˶�", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 128);
            intXPos += 43;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("�غ�\r\n�˶�", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 128);
            intXPos += 43;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("��  ��", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 138);
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos + 120, intYPos + (int)enmRectangleInfo.RowStep + 10);
            e.Graphics.DrawString("��  ��", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 70, 133);
            intXPos += 120;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("��ǰ\r\n���", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 128);
            intXPos += 43;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("��ǰ\r\n�˶�", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 128);
            intXPos += 43;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("�غ�\r\n�˶�", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 128);
            intXPos += 43;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("��  ��", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 138);
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos + 120, intYPos + (int)enmRectangleInfo.RowStep + 10);
            e.Graphics.DrawString("��  ��", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 70, 133);
            intXPos += 120;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("��ǰ\r\n���", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 128);
            intXPos += 43;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("��ǰ\r\n�˶�", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 128);
            intXPos += 43;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + intXPos, intYPos, (int)enmRectangleInfo.LeftX + intXPos, (int)enmRectangleInfo.BottomY);
            e.Graphics.DrawString("�غ�\r\n�˶�", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + intXPos + 1, 128);

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
            e.Graphics.DrawString("�� " + m_intPages.ToString() + " ҳ", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 350, intYPos + 30);
        }

        private void m_mthAddDataToGrid(PrintPageEventArgs e)
        {
            int intYPos = 165;
            if (m_objRecordContent == null 
                || m_objRecordContent.m_objOPInstrument == null || m_objRecordContent.m_objOPInstrument.Length <= 0)
            {
                if (m_objDict != null && m_objDict.Length > 0)
                {
                    m_mthShowItems(m_objDict, ref intYPos, e);
                }
                e.Graphics.DrawString("������ǩ��:", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX , intYPos + 5);
                e.Graphics.DrawString("��е��ʿǩ��:", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 249, intYPos + 5);
                e.Graphics.DrawString("�˶Ի�ʿ(��ǰ)ǩ��:", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 498, intYPos + 5);
                e.Graphics.DrawString("����ǩ��:", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX, intYPos + 25);
                e.Graphics.DrawString("Ѳ�ػ�ʿǩ��:", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 249, intYPos + 25);
                e.Graphics.DrawString("�˶Ի�ʿ(�غ�)ǩ��:", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 498, intYPos + 25);
                e.HasMorePages = false;
                return;
            }

            m_mthShowItems(m_objRecordContent.m_objOPInstrument, ref intYPos, e);

            e.Graphics.DrawString("������ǩ��:" + m_strGetSingArr("m_lsvOperationer", m_objRecordContent.objSignerArr), m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX, intYPos + 5);
            e.Graphics.DrawString("��е��ʿǩ��:" + m_strGetSingArr("m_lsvInstrumentNurse", m_objRecordContent.objSignerArr), m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 249, intYPos + 5);
            e.Graphics.DrawString("�˶Ի�ʿ(��ǰ)ǩ��:" + m_strGetSingArr("m_lsvCheckNurse", m_objRecordContent.objSignerArr), m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 498, intYPos + 5);
            e.Graphics.DrawString("����ǩ��:" + m_strGetSingArr("m_lsvAssitantor", m_objRecordContent.objSignerArr), m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX, intYPos + 25);
            e.Graphics.DrawString("Ѳ�ػ�ʿǩ��:" + m_strGetSingArr("m_lsvItinerationNurse", m_objRecordContent.objSignerArr), m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 249, intYPos + 25);
            e.Graphics.DrawString("�˶Ի�ʿ(�غ�)ǩ��:" + m_strGetSingArr("m_lsvCheckNurseName", m_objRecordContent.objSignerArr), m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 498, intYPos + 25);
            e.Graphics.DrawString("��¼����:" + m_objRecordContent.m_dtmRecordDate.ToString("yyyy��MM��dd�� HHʱ"), m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 498, intYPos + 45);
            e.HasMorePages = false;
        }

        private string m_strGetSingArr(string p_strControlName,clsEmrSigns_VO[] p_objSignArr)
        {
            if (p_objSignArr == null || string.IsNullOrEmpty(p_strControlName))
                return string.Empty;
            string strSigns = "";
            for (int i = 0; i < p_objSignArr.Length; i++)
            {
                if (p_objSignArr[i].controlName == p_strControlName.Trim())
                {
                    strSigns += p_objSignArr[i].objEmployee.m_strGetTechnicalRankAndName + "  ";
                }
            }
            return strSigns;
        }

        private int intIdx = 0;
        private void m_mthShowItems(object[] p_objItems,ref int p_intStartY, PrintPageEventArgs e)
        {
            if (p_objItems == null)
                return;

            if (p_objItems is clsEMR_OPInstrument_Dict[])
            {
                clsEMR_OPInstrument_Dict[] objDictArr = p_objItems as clsEMR_OPInstrument_Dict[];
                if (objDictArr == null)
                    return;

                int intPages = objDictArr.Length / 105;
                for (m_intLines = intIdx * 105; m_intLines < intIdx * 105 + 35; m_intLines++)
                {
                    if (m_intLines >= objDictArr.Length)
                        break;
                    string[] itemArr = new string[] { objDictArr[m_intLines].m_strOPInstrumentName, "", "", "", "", "", "", "", "", "", "", "" };
                    if (m_intLines + 35 < objDictArr.Length)
                    {
                        itemArr[4] = objDictArr[m_intLines + 35].m_strOPInstrumentName;
                    }
                    if (m_intLines + 70 < objDictArr.Length) 
                    {
                        itemArr[8] = objDictArr[m_intLines + 70].m_strOPInstrumentName;
                    }
                    m_mthDrawString(itemArr, p_intStartY, e);
                    p_intStartY += (int)enmRectangleInfo.RowStep;

                    if (m_intLines != 0 && m_intLines % 35 == 0)
                    {
                        intIdx++;
                        m_intPages++;
                        e.HasMorePages = true;
                        return;
                    }
                }
            }

            if (p_objItems is clsEMR_OPInstrumentItem[])
            {
                clsEMR_OPInstrumentItem[] objDictArr = p_objItems as clsEMR_OPInstrumentItem[];
                if (objDictArr == null)
                    return;

                int intPages = objDictArr.Length / 105;
                for (m_intLines = intIdx * 105; m_intLines < intIdx * 105 + 35; m_intLines++)
                {
                    if (m_intLines >= objDictArr.Length)
                        break;
                    string[] itemArr = new string[] { objDictArr[m_intLines].m_objOPInstrumentInfo.m_strOPInstrumentName, 
                        objDictArr[m_intLines].m_strBeforeOP, objDictArr[m_intLines].m_strBeforeClose, 
                        objDictArr[m_intLines].m_strAfterClose, "", "", "", "", "", "", "", "" };
                    if (m_intLines + 35 < objDictArr.Length)
                    {
                        itemArr[4] = objDictArr[m_intLines + 35].m_objOPInstrumentInfo.m_strOPInstrumentName;
                        itemArr[5] = objDictArr[m_intLines + 35].m_strBeforeOP;
                        itemArr[6] = objDictArr[m_intLines + 35].m_strBeforeClose;
                        itemArr[7] = objDictArr[m_intLines + 35].m_strAfterClose;
                    }
                    if (m_intLines + 70 < objDictArr.Length)
                    {
                        itemArr[8] = objDictArr[m_intLines + 70].m_objOPInstrumentInfo.m_strOPInstrumentName; 
                        itemArr[9] = objDictArr[m_intLines + 70].m_strBeforeOP;
                        itemArr[10] = objDictArr[m_intLines + 70].m_strBeforeClose;
                        itemArr[11] = objDictArr[m_intLines + 70].m_strAfterClose;
                    }
                    m_mthDrawString(itemArr, p_intStartY, e);
                    p_intStartY += (int)enmRectangleInfo.RowStep;

                    if (m_intLines != 0 && m_intLines % 35 == 0)
                    {
                        intIdx++;
                        m_intPages++;
                        e.HasMorePages = true;
                        return;
                    }
                }
            }
        }

        private void m_mthDrawString(string[] p_strItems,float y,PrintPageEventArgs e)
        {
            if (p_strItems == null || p_strItems.Length != 12)
                return;

            e.Graphics.DrawString(p_strItems[0], m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 1, y);
            e.Graphics.DrawString(p_strItems[1], m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 121, y);
            e.Graphics.DrawString(p_strItems[2], m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 164, y);
            e.Graphics.DrawString(p_strItems[3], m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 207, y);
            e.Graphics.DrawString(p_strItems[4], m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 250, y);
            e.Graphics.DrawString(p_strItems[5], m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 370, y);
            e.Graphics.DrawString(p_strItems[6], m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 413, y);
            e.Graphics.DrawString(p_strItems[7], m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 456, y);
            e.Graphics.DrawString(p_strItems[8], m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 499, y);
            e.Graphics.DrawString(p_strItems[9], m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 619, y);
            e.Graphics.DrawString(p_strItems[10], m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 662, y);
            e.Graphics.DrawString(p_strItems[11], m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 705, y);
        }
        #endregion
    }
}
