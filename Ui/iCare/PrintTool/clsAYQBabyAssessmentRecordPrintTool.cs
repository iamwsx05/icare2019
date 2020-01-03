using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;

namespace iCare
{
    /// <summary>
    /// ��Ӥ��Ӥ���������ӡ������
    /// </summary>
    public class clsAYQBabyAssessmentRecordPrintTool : infPrintRecord
    {
        private bool m_blnIsFromDataSource = true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
        private bool m_blnWantInit = true;
        private clsPrintInfo_InPatientCaseHistory m_objPrintInfo;
        private clsBaseCaseHistoryDomain m_objRecordsDomain;
        private clsAYQBabyAssessmentContentDomain m_objInRoomDomain;
        private clsAYQBabyAssessmentContent[] objRecordArr = null;
        /// <summary>
        /// �Ƿ��ӡ�޸ĺۼ�
        /// </summary>
        public static bool m_blnIsPrintMark = true;

        public clsAYQBabyAssessmentRecordPrintTool()
        {
            m_objInRoomDomain = new clsAYQBabyAssessmentContentDomain();
        }

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
            m_objPrintInfo = new clsPrintInfo_InPatientCaseHistory();
            m_objPrintInfo.m_strInPatentID = m_objPatient != null ? m_objPatient.m_StrInPatientID : "";
            m_objPrintInfo.m_strPatientName = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrLastName : "";
            m_objPrintInfo.m_strSex = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrSex : "";
            //������̼�¼����Ӥ��Ӥ��������Ҫ�󣺵���������С��һ����ʱ���á��¡���ʾ
            if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO == "440605001")
            {
                string strAge = "";
                if (m_objPatient != null)
                {
                    strAge = m_objPatient.m_ObjPeopleInfo.m_StrAge;
                    if (strAge.IndexOf("��") == -1 && strAge.IndexOf("��") == -1)
                        strAge = "��";
                }
                m_objPrintInfo.m_strAge = strAge;
            }
            else
                m_objPrintInfo.m_strAge = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrAge : "";
            m_objPrintInfo.m_strBedName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName : "";
            m_objPrintInfo.m_strDeptName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjDept.m_StrDeptName : "";
            m_objPrintInfo.m_strAreaName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName : "";
            m_objPrintInfo.m_dtmInPatientDate = p_dtmInPatientDate;
            m_objPrintInfo.m_dtmOpenDate = p_dtmOpenDate;
            m_objPrintInfo.m_strHISInPatientID = m_objPatient != null ? p_objPatient.m_StrHISInPatientID : "";
            m_objPrintInfo.m_dtmHISInPatientDate = m_objPatient != null ? p_objPatient.m_DtmSelectedHISInDate : DateTime.MinValue;
            m_mthGetPrintMarkConfig();
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
        /// <summary>
        /// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���(�������ݿ��ȡʱҪ����.)
        /// </summary>
        public void m_mthInitPrintContent()
        {
            m_blnWantInit = false;
            
            if (m_objPrintInfo == null)
            {
                MDIParent.ShowInformationMessageBox("����m_mthInitPrintContent֮ǰ�����ȵ���m_mthSetPrintInfo����");
                return;
            }

            if (m_objPrintInfo.m_strInPatentID != "")
            {
                m_objRecordsDomain = new clsBaseCaseHistoryDomain(enmBaseCaseHistoryTypeInfo.AYQBabyAssessmentRecord);
                long lngRes = m_objRecordsDomain.m_lngGetPrintInfo(m_objPrintInfo.m_strInPatentID, m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.MinValue, out m_objPrintInfo.m_objContent, out m_objPrintInfo.m_objPicValueArr, out m_objPrintInfo.m_dtmFirstPrintDate, out m_objPrintInfo.m_blnIsFirstPrint);

                //if (m_objPrintInfo.m_objContent != null)
                //{
                    lngRes = m_objInRoomDomain.m_lngGetAllModifiedCircsRecordContent(m_objPrintInfo.m_strInPatentID, m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"),  out objRecordArr);
                //}
            }
            //���ñ����ݵ���ӡ��,��ʹ�Ǵ�ӡ�հ׵�,����Ҳ����ִ��.(��:�ڱ������ڲ�,����֮�ϲ�׼��return���,���ǳ�������.)
            m_mthSetPrintContent((clsAYQBabyAssessmentContent_EspRecord)m_objPrintInfo.m_objContent, objRecordArr, m_objPrintInfo.m_dtmFirstPrintDate);
        }

        /// <summary>
        /// ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
        /// </summary>
        /// <param name="p_objPrintContent">��ӡ����</param>
        public void m_mthSetPrintContent(object p_objPrintContent)
        {
            m_blnWantInit = false;
            if (p_objPrintContent.GetType().Name != "clsPrintInfo_InPatientCaseHistory")
            {
                MDIParent.ShowInformationMessageBox("��������");
            }
            m_blnIsFromDataSource = false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ
            m_objPrintInfo = (clsPrintInfo_InPatientCaseHistory)p_objPrintContent;

            m_mthSetPrintContent((clsAYQBabyAssessmentContent_EspRecord)m_objPrintInfo.m_objContent, objRecordArr, m_objPrintInfo.m_dtmFirstPrintDate);
        }

        /// <summary>
        /// ��ȡ��ӡ����,(�������ݿ��ȡʱ,���ñ�����֮ǰ�����ȵ���m_mthSetPrintInfo����)
        /// </summary>
        /// <returns>��ӡ����</returns>
        public object m_objGetPrintInfo()
        {
            if (m_blnIsFromDataSource)
            {
                if (m_objPrintInfo == null)
                {
                    MDIParent.ShowInformationMessageBox("�������ݿ��ȡʱ,����m_objGetPrintInfo֮ǰ�����ȵ���m_mthSetPrintInfo����");
                    return null;
                }

                if (m_blnWantInit)
                    m_mthInitPrintContent();
            }

            //û�м�¼����ʱ�����ؿ�
            if (m_objPrintInfo.m_objContent == null)
                return null;
            else
                return m_objPrintInfo;
        }

        /// <summary>
        /// ��ʼ����ӡ����,��������ն��󼴿�.
        /// </summary>
        public void m_mthInitPrintTool(object p_objArg)
        {
            #region �йش�ӡ��ʼ��

            m_fotTitleFont = new Font("SimSun", 16, FontStyle.Bold);
            m_fotHeaderFont = new Font("SimSun", 18, FontStyle.Bold);
            m_fotItemHead = new Font("", 13, FontStyle.Bold);
            m_fotSmallFont = new Font("SimSun", 12);
            m_GridPen = new Pen(Color.Black, 2);
            m_slbBrush = new SolidBrush(Color.Black);
            m_objPageSetting = new clsPrintPageSettingForRecord();


            #endregion �йش�ӡ��ʼ��
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


        #region ��ӡ

        /// <summary>
        /// ��ӡ��ʼ
        /// </summary>
        /// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintEventArgs���͵Ķ���</param>
        public void m_mthBeginPrint(object p_objPrintArg)
        {
            m_mthBeginPrintSub((PrintEventArgs)p_objPrintArg);
        }

        /// <summary>
        /// ��ӡ��
        /// </summary>
        /// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintPageEventArgs���͵Ķ���</param>
        public void m_mthPrintPage(object p_objPrintArg)
        {
            m_mthPrintPageSub((PrintPageEventArgs)p_objPrintArg);
        }

        /// <summary>
        /// ��ӡ������һ��ʹ�������������ݿ���Ϣ��
        /// </summary>
        /// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintEventArgs���͵Ķ���</param>
        public void m_mthEndPrint(object p_objPrintArg)
        {
            m_mthEndPrintSub((PrintEventArgs)p_objPrintArg);
            if (m_blnIsFromDataSource == false || m_objPrintInfo.m_strInPatentID == "")
                return;
            //�����ӡ�ɹ�������������Ҫ���µ�ʱ�䣬����У�����ʱ�䡣 
            if (!((PrintEventArgs)p_objPrintArg).Cancel && m_objPrintInfo.m_blnIsFirstPrint)
            {
                m_objRecordsDomain.m_lngUpdateFirstPrintDate(m_objPrintInfo.m_strInPatentID, m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"), m_objPrintInfo.m_dtmFirstPrintDate);
            }
        }

        #region �йش�ӡ������

        /// <summary>
        /// ��ӡһ�е�����
        /// </summary>
        private com.digitalwave.Utility.Controls.clsPrintContext m_objPrintLineContext;

        /// <summary>
        /// ��ӡ�߿����߾�
        /// </summary>
        private const int m_intRecBaseX = clsPrintPosition.c_intLeftX;
        private int m_intCurrentPage = 1;
        /// <summary>
        /// ���������(20 bold)
        /// </summary>
        private Font m_fotTitleFont;
        /// <summary>
        /// ��ͷ������(14 )
        /// </summary>
        private Font m_fotHeaderFont;
        /// <summary>
        /// ����Ŀ�ı��⣬�������
        /// </summary>
        public static Font m_fotItemHead;
        /// <summary>
        /// �����ݵ�����(11)
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
        /// <summary>
        /// ��ǰ��ӡλ�ã�Y��
        /// </summary>
        private int m_intYPos = 155;//= (int)enmRectangleInfo.TopY+5;

        /// <summary>
        /// ���ӵ���Ϣ
        /// </summary>
        public enum enmRectangleInfo
        {

            /// <summary>
            /// ���ӵĶ���
            /// </summary>
            TopY = 140,
            ///<summary>
            /// ���ӵ����
            /// </summary>
            LeftX = clsPrintPosition.c_intLeftX,
            /// <summary>
            /// ���ӵ��Ҷ�
            /// </summary>
            RightX = clsPrintPosition.c_intRightX,
            /// <summary>
            /// ����ÿ�еĲ���
            /// </summary>
            RowStep = 25,
            SmallRowStep = 25,
            /// <summary>
            /// ���ӵ�����
            /// </summary>
            RowLinesNum = 34,

            ColumnsMark1 = 35,

            /// <summary>
            /// CheckBoxƫ���ұ��ı��ľ���
            /// </summary>
            CheckShift = 15,

            /// <summary>
            /// �׻���ƫ���ı�����ľ���
            /// </summary>
            BottomLineShift = 15,

            BottomY = 1025

        }

        #endregion
        // ��ӡ��ʼ���ڴ�ӡҳ֮ǰ�Ĳ���
        private void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
        {

        }

        // ��ӡҳ
        private void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
        {
            m_mthPrintTitleInfo(p_objPrintPageArg);

            Font fntNormal = new Font("", 10);

            while (m_objPrintLineContext.m_BlnHaveMoreLine)
            {
                //�������ݴ�ӡ
                m_objPrintLineContext.m_mthPrintNextLine(ref m_intYPos, p_objPrintPageArg.Graphics, fntNormal);

                if (m_intYPos > p_objPrintPageArg.PageBounds.Height - 270
                    && m_objPrintLineContext.m_BlnHaveMoreLine)
                {
                    //�������ݴ�ӡ������Ҫ��ҳ

                    m_mthPrintFoot(p_objPrintPageArg);

                    p_objPrintPageArg.HasMorePages = true;

                    m_intYPos = 155;

                    m_intCurrentPage++;

                    return;
                }
            }

            m_intYPos += 30;
            Font fntSign = new Font("", 6);
            while (m_objPrintLineContext.m_BlnHaveMoreSign)
            {
                m_objPrintLineContext.m_mthPrintNextSign(30 + 10, m_intYPos, p_objPrintPageArg.Graphics, fntSign);

                m_intYPos += (int)enmRectangleInfo.RowStep - 10;
            }

            //ȫ������			

            m_mthPrintFoot(p_objPrintPageArg);
        }

        // ���ô�ӡ���ݡ�
        private void m_mthSetPrintContent(clsAYQBabyAssessmentContent_EspRecord p_objContent, clsAYQBabyAssessmentContent[] p_objCircsContentArr, DateTime p_dtmFirstPrintDate)
        {
            m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
																		new clsPrintRecordHeader(),
																		new clsPrintCircsRecordContent(),
                                                                        new clsPrintEspRecordContent()
																	   });
            m_objPrintLineContext.m_ObjPrintSign = new com.digitalwave.Utility.Controls.clsPrintRecordSign();

            object[] objData = new Object[3];
            objData[0] = p_objContent;
            objData[1] = m_objPrintInfo;
            objData[2] = p_objCircsContentArr;

            //���ô�ӡ��Ϣ������Set Value��ȥ
            m_objPrintLineContext.m_ObjPrintLineInfo = objData;
            //�����ݿ��ó�����FirstPrintDate����ÿ����ӡ�������m_DtmFirstPrintTime���ڸ���������
            m_objPrintLineContext.m_DtmFirstPrintTime = p_dtmFirstPrintDate;
        }

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
            BedNO_Title,
            BedNO,
            Dept_Name_Title,
            Dept_Name,
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
                        m_fReturnPoint = new PointF(320f, 40f);
                        break;
                    case (int)enmItemDefination.Page_Name_Title:
                        m_fReturnPoint = new PointF(303f, 70f);
                        break;
                    case (int)enmItemDefination.Name_Title:
                        m_fReturnPoint = new PointF(45f, 110f);
                        break;
                    case (int)enmItemDefination.Name:
                        m_fReturnPoint = new PointF(95f, 110f);
                        break;

                    case (int)enmItemDefination.Sex_Title:
                        m_fReturnPoint = new PointF(165f, 110f);
                        break;
                    case (int)enmItemDefination.Sex:
                        m_fReturnPoint = new PointF(210f, 110f);
                        break;

                    case (int)enmItemDefination.Age_Title:
                        m_fReturnPoint = new PointF(260f, 110f);
                        break;
                    case (int)enmItemDefination.Age:
                        m_fReturnPoint = new PointF(305f, 110f);
                        break;

                    case (int)enmItemDefination.Dept_Name_Title:
                        m_fReturnPoint = new PointF(390f, 110f);
                        break;
                    case (int)enmItemDefination.Dept_Name:
                        m_fReturnPoint = new PointF(440f, 110f);
                        break;

                    case (int)enmItemDefination.BedNO_Title:
                        m_fReturnPoint = new PointF(555f, 110f);
                        break;
                    case (int)enmItemDefination.BedNO:
                        m_fReturnPoint = new PointF(605f, 110f);
                        break;
                    case (int)enmItemDefination.InPatientID_Title:
                        m_fReturnPoint = new PointF(647f, 110f);
                        break;
                    case (int)enmItemDefination.InPatientID:
                        m_fReturnPoint = new PointF(707f, 110f);
                        break;

                    default:
                        m_fReturnPoint = new PointF(400f, 400f);
                        break;

                }
                return m_fReturnPoint;
            }
        }

        /// <summary>
        /// ���ӵ���Ϣ
        /// </summary>
        private enum enmRectangleInfoInPatientCaseInfo
        {
            /// <summary>
            /// ���ӵĶ���
            /// </summary>
            TopY = 140,

            ///<summary>
            /// ���ӵ����
            /// </summary>
            LeftX = 16,

            /// <summary>
            /// ���ӵ��Ҷ�
            /// </summary>
            RightX = 180 + 17,
            /// <summary>
            /// ����ÿ�еĲ���
            /// </summary>
            RowStep = 7,
            SmallRowStep = 25,
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

            PrintWidth = 630,
            PrintWidth2 = 710,

        }

        #endregion

        #region PrintClasses
        private abstract class clsPrintInPatientCaseInfo : com.digitalwave.Utility.Controls.clsPrintLineBase
        {
            protected clsAYQBabyAssessmentContent_EspRecord m_objContent;
            protected Pen m_GridPen = new Pen(Color.Black);
            /// <summary>
            /// ���־�����ߵı߾�
            /// </summary>
            protected int m_intRecBaseX = clsPrintPosition.c_intLeftX;
            protected int m_intPatientInfoX = 70;
            protected clsPrintInfo_InPatientCaseHistory m_objPrintInfo;
            protected clsAYQBabyAssessmentContent[] m_objPrintCircsArr;

            public override object m_ObjPrintLineInfo
            {
                get
                {
                    return base.m_blnHaveMoreLine;
                }
                set
                {
                    if (value == null) return;
                    object[] objData = (object[])value;
                    m_objContent = (clsAYQBabyAssessmentContent_EspRecord)objData[0];
                    m_objPrintInfo = (clsPrintInfo_InPatientCaseHistory)objData[1];
                    m_objPrintCircsArr = (clsAYQBabyAssessmentContent[])objData[2];
                }
            }
        }
        /// <summary>
        /// ����񼰱���
        /// </summary>
        private class clsPrintRecordHeader : clsPrintInPatientCaseInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 8));
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private Font m_fotTitleFont = new Font("SimSun", 16, FontStyle.Bold);
            private Font m_fotContentFont = new Font("SimSun", 10);
            private PointF m_fReturnPoint;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objPrintCircsArr == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    int m_intY = (int)enmRectangleInfo.TopY + (int)enmRectangleInfoInPatientCaseInfo.SmallRowStep;
                    //������
                    //int m_intWidth = (int)enmRectangleInfo.RightX - (int)enmRectangleInfo.LeftX;
                    for (int i = 0; i < (int)enmRectangleInfo.RowLinesNum; i++)
                    {
                        if (i != 0)
                        {
                            p_objGrp.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, m_intY, (int)enmRectangleInfo.RightX, m_intY);
                            m_intY += (int)enmRectangleInfoInPatientCaseInfo.SmallRowStep;
                        }
                        else
                        {
                            p_objGrp.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX + 110, m_intY, (int)enmRectangleInfo.RightX, m_intY);
                            m_intY += (int)enmRectangleInfoInPatientCaseInfo.SmallRowStep;
                        }
                    }
                    //������
                    int m_intX = (int)enmRectangleInfo.LeftX;
                    for (int j = 0; j < 8; j++)
                    {
                        p_objGrp.DrawLine(m_GridPen, m_intX + 110, (int)enmRectangleInfo.TopY, m_intX + 110, (int)enmRectangleInfo.TopY + 14 * (int)enmRectangleInfoInPatientCaseInfo.SmallRowStep);
                        m_intX += 80;
                    }
                    //����������������֮���б��
                    p_intPosY = (int)enmRectangleInfo.TopY + 16;
                    p_objGrp.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, (int)enmRectangleInfo.TopY, (int)enmRectangleInfo.LeftX + 110, (int)enmRectangleInfo.TopY + 2 * (int)enmRectangleInfoInPatientCaseInfo.SmallRowStep);
                    #region ������
                    p_objGrp.DrawString("����", m_fotContentFont, Brushes.Black, (int)enmRectangleInfo.LeftX + 67, p_intPosY);
                    p_intPosY += 10;
                    p_objGrp.DrawString("��������", m_fotContentFont, Brushes.Black, (int)enmRectangleInfo.LeftX + 5, p_intPosY);
                    p_intPosY += 32;
                    p_objGrp.DrawString("��  ɫ", m_fotContentFont, Brushes.Black, (int)enmRectangleInfo.LeftX + 30,p_intPosY);
                    p_intPosY += 25;
                    p_objGrp.DrawString("��  ��", m_fotContentFont, Brushes.Black, (int)enmRectangleInfo.LeftX + 30, p_intPosY);
                    p_intPosY += 25;
                    p_objGrp.DrawString("��  Ӧ", m_fotContentFont, Brushes.Black, (int)enmRectangleInfo.LeftX + 30,p_intPosY);
                    p_intPosY += 25;
                    p_objGrp.DrawString("��  ʳ", m_fotContentFont, Brushes.Black, (int)enmRectangleInfo.LeftX + 30, p_intPosY);
                    p_intPosY += 25;
                    p_objGrp.DrawString("Ҹ  ��", m_fotContentFont, Brushes.Black, (int)enmRectangleInfo.LeftX + 30, p_intPosY);
                    p_intPosY += 25;
                    p_objGrp.DrawString("Ƥ  ��", m_fotContentFont, Brushes.Black, (int)enmRectangleInfo.LeftX + 30, p_intPosY);
                    p_intPosY += 25;
                    p_objGrp.DrawString("��  ��", m_fotContentFont, Brushes.Black, (int)enmRectangleInfo.LeftX + 30, p_intPosY);
                    p_intPosY += 25;
                    p_objGrp.DrawString("��  ��", m_fotContentFont, Brushes.Black, (int)enmRectangleInfo.LeftX + 30, p_intPosY);
                    p_intPosY += 25;
                    p_objGrp.DrawString("��֫�", m_fotContentFont, Brushes.Black, (int)enmRectangleInfo.LeftX + 25, p_intPosY);
                    p_intPosY += 25;
                    p_objGrp.DrawString("��  ��", m_fotContentFont, Brushes.Black, (int)enmRectangleInfo.LeftX + 30, p_intPosY);
                    p_intPosY += 25;
                    p_objGrp.DrawString("С  ��", m_fotContentFont, Brushes.Black, (int)enmRectangleInfo.LeftX + 30, p_intPosY);
                    p_intPosY += 25;
                    p_objGrp.DrawString("ǩ  ��", m_fotContentFont, Brushes.Black, (int)enmRectangleInfo.LeftX + 30, p_intPosY);
                    p_intPosY += 25;
                    p_objGrp.DrawString("�����¼��", m_fotContentFont, Brushes.Black, (int)enmRectangleInfo.LeftX + 5, p_intPosY);
                    
                    #endregion

                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 60, p_intPosY, p_objGrp);

                    p_intPosY += 20;

                    intLine++;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
                    m_blnHaveMoreLine = true;
                else
                {
                    m_blnHaveMoreLine = false;
                }
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();

                m_blnHaveMoreLine = true;

                m_blnIsFirstPrint = true;
            }
        }

        /// <summary>
        /// ������Ŀ����
        /// </summary>
        private class clsPrintCircsRecordContent : clsPrintInPatientCaseInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private Font m_fotContentFont = new Font("SimSun", 9);
            private Font m_fotTimetFont = new Font("SimSun", 8);

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objPrintCircsArr == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                p_intPosY = (int)enmRectangleInfo.TopY + 10;
                if (m_blnIsFirstPrint)
                {
                    int intTempX = (int)enmRectangleInfo.LeftX + 115;
                    for (int i = 0; i < m_objPrintCircsArr.Length; i++)
                    {
                        #region ��ӡ�������
                        p_objGrp.DrawString(m_objPrintCircsArr[i].m_dtmRecordDate.ToString("yyyy.MM.dd"), m_fotContentFont, Brushes.Black, intTempX, p_intPosY);
                        p_intPosY += 25;
                        p_objGrp.DrawString(m_objPrintCircsArr[i].m_dtmRecordDate.ToString("HH:mm"), m_fotContentFont, Brushes.Black, intTempX + 5, p_intPosY);
                        p_intPosY += 25;
                        p_objGrp.DrawString(m_objPrintCircsArr[i].m_strFacecolor, m_fotContentFont, Brushes.Black, intTempX , p_intPosY);
                        p_intPosY += 25;
                        p_objGrp.DrawString(m_objPrintCircsArr[i].m_strRespiration, m_fotContentFont, Brushes.Black, intTempX, p_intPosY);
                        p_intPosY += 25;
                        p_objGrp.DrawString(m_objPrintCircsArr[i].m_strReaction, m_fotContentFont, Brushes.Black, intTempX, p_intPosY);
                        p_intPosY += 25;
                        p_objGrp.DrawString(m_objPrintCircsArr[i].m_strTakeFood, m_fotContentFont, Brushes.Black, intTempX, p_intPosY);
                        p_intPosY += 25;
                        p_objGrp.DrawString(m_objPrintCircsArr[i].m_strArmpitWet, m_fotContentFont, Brushes.Black, intTempX, p_intPosY);
                        p_intPosY += 25;
                        p_objGrp.DrawString(m_objPrintCircsArr[i].m_strDerm, m_fotContentFont, Brushes.Black, intTempX, p_intPosY);
                        p_intPosY += 25;
                        p_objGrp.DrawString(m_objPrintCircsArr[i].m_strAurigo, m_fotContentFont, Brushes.Black, intTempX, p_intPosY);
                        p_intPosY += 25;
                        p_objGrp.DrawString(m_objPrintCircsArr[i].m_strUmbilicalRegion, m_fotContentFont, Brushes.Black, intTempX, p_intPosY);
                        p_intPosY += 25;
                        p_objGrp.DrawString(m_objPrintCircsArr[i].m_strLimbActivity, m_fotContentFont, Brushes.Black, intTempX, p_intPosY);
                        p_intPosY += 25;
                        p_objGrp.DrawString(m_objPrintCircsArr[i].m_strStool, m_fotContentFont, Brushes.Black, intTempX, p_intPosY);
                        p_intPosY += 25;
                        p_objGrp.DrawString(m_objPrintCircsArr[i].m_strUrine, m_fotContentFont, Brushes.Black, intTempX, p_intPosY);
                        p_intPosY += 25;
                        p_objGrp.DrawString(m_objPrintCircsArr[i].m_strSignUserName, m_fotContentFont, Brushes.Black, intTempX, p_intPosY);
                        intTempX += 80;
                        p_intPosY = (int)enmRectangleInfo.TopY + 10;
                        #endregion
                    }
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 60, p_intPosY, p_objGrp);

                    p_intPosY += 20;

                    intLine++;
                }


                if (m_objPrintContext.m_BlnHaveNextLine())
                    m_blnHaveMoreLine = true;
                else
                {
                    m_blnHaveMoreLine = false;
                }
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();

                m_blnHaveMoreLine = true;

                m_blnIsFirstPrint = true;
            }
        }

        /// <summary>
        /// �����¼
        /// </summary>
        private class clsPrintEspRecordContent : clsPrintInPatientCaseInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private Font m_fotContentFont = new Font("SimSun", 9);
            int intLine = 0;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                
                if (m_blnIsFirstPrint)
                {
                    p_intPosY = (int)enmRectangleInfo.TopY + (int)enmRectangleInfo.SmallRowStep * 14 + 6;
                    string strTemp = "";
                    if (m_objContent.m_strEspRecord.Length == 0 && m_objContent.m_strEspRecord2.Length == 0 && m_objContent.m_strEspRecord3.Length == 0 && m_objContent.m_strEspRecord4.Length == 0)
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    if (m_objContent.m_strEspRecord.Length != 0)
                    {
                        strTemp += m_objContent.m_strEspRecord + '\n' + "                                                                                                                                --" + m_objContent.m_strRecordSign + "   " + m_objContent.m_dtmRecordDate + '\n';
                    }
                    if (m_objContent.m_strEspRecord2.Length != 0)
                    {
                        strTemp += m_objContent.m_strEspRecord2 + '\n' + "                                                                                                                                --" + m_objContent.m_strRecordSign2 + "   " + m_objContent.m_dtmRecordDate2 + '\n';
                    }
                    if (m_objContent.m_strEspRecord3.Length != 0)
                    {
                        strTemp += m_objContent.m_strEspRecord3 + '\n' + "                                                                                                                                --" + m_objContent.m_strRecordSign3 + "   " + m_objContent.m_dtmRecordDate3 + '\n';
                    }
                    if (m_objContent.m_strEspRecord4.Length != 0)
                    {
                        strTemp += m_objContent.m_strEspRecord4 + '\n' + "                                                                                                                                --" + m_objContent.m_strRecordSign4 + "   " + m_objContent.m_dtmRecordDate4 + '\n';
                    }
                    //if (clsAYQBabyAssessmentRecordPrintTool.m_blnIsPrintMark)
                    //{
                    //    m_objPrintContext.m_mthSetContextWithCorrectBefore(m_objContent.m_strEspRecord, m_objContent.m_strEspRecordXML, m_dtmFirstPrintTime, true );
                    //}
                    //else
                    //{
                        m_objPrintContext.m_mthSetContextWithAllCorrect(strTemp, "<root />");
                    //}
                    m_blnIsFirstPrint = false;
                }

                
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    if( intLine == 0)
                        m_objPrintContext.m_mthPrintLine((int)enmRectangleInfo.RightX - (int)enmRectangleInfo.LeftX - 70, (int)enmRectangleInfo.LeftX + 75, p_intPosY, p_objGrp);
                    else
                        m_objPrintContext.m_mthPrintLine((int)enmRectangleInfo.RightX - (int)enmRectangleInfo.LeftX , (int)enmRectangleInfo.LeftX + 5, p_intPosY, p_objGrp);
                    p_intPosY += 25;

                    intLine++;
                }


                if (m_objPrintContext.m_BlnHaveNextLine())
                    m_blnHaveMoreLine = true;
                else
                {
                    m_blnHaveMoreLine = false;
                }
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();

                m_blnHaveMoreLine = true;

                m_blnIsFirstPrint = true;
            }
        }
        #endregion


        #region �������ֲ���
        /// <summary>
        /// ��ӡҳ��
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintFoot(System.Drawing.Printing.PrintPageEventArgs e)
        {
            float fltOffsetX = 20;//X��ƫ����
            Font fntHeader = new Font("SimSun", 12);

            e.Graphics.DrawString("��      ҳ", fntHeader, Brushes.Black, 385 - fltOffsetX, e.PageBounds.Height - 175);
            e.Graphics.DrawString(m_intCurrentPage.ToString(), fntHeader, Brushes.Black, 425 - fltOffsetX, e.PageBounds.Height - 175);
        }
        /// <summary>
        /// �������ֲ���
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_HospitalName));

            e.Graphics.DrawString("��Ӥ��Ӥ��������", m_fotTitleFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_Name_Title));
            e.Graphics.DrawString("������", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name_Title));
            e.Graphics.DrawString(m_objPrintInfo.m_strPatientName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name));
            e.Graphics.DrawString("�Ա�", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex_Title));
            e.Graphics.DrawString(m_objPrintInfo.m_strSex, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex));
            e.Graphics.DrawString("���䣺", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age_Title));
            e.Graphics.DrawString(m_objPrintInfo.m_strAge, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age));
            e.Graphics.DrawString("������", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name_Title));
            e.Graphics.DrawString(m_objPrintInfo.m_strAreaName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name));
            e.Graphics.DrawString("���ţ�", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNO_Title));
            e.Graphics.DrawString(m_objPrintInfo.m_strBedName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNO));
            e.Graphics.DrawString("סԺ�ţ�", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID_Title));
            e.Graphics.DrawString(m_objPrintInfo.m_strHISInPatientID, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID));
            e.Graphics.DrawRectangle(Pens.Black, (int)enmRectangleInfo.LeftX, (int)enmRectangleInfo.TopY, (int)enmRectangleInfo.RightX - (int)enmRectangleInfo.LeftX, (int)enmRectangleInfo.RowLinesNum * (int)enmRectangleInfo.SmallRowStep);
            e.Graphics.DrawString("ע��������\"��\"", m_fotSmallFont, m_slbBrush, (int)enmRectangleInfo.LeftX + 20, (int)enmRectangleInfo.RowLinesNum * (int)enmRectangleInfo.SmallRowStep + 5 + (int)enmRectangleInfo.TopY);
        }
        #endregion

        // ��ӡ����ʱ�Ĳ���
        private void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
        {
            m_mthResetWhenEndPrint();
        }

        /// <summary>
        /// ÿ�δ�ӡ����֮��ĸ�λ,�����Ǵ�ӡ��ǰҳ���ߴ�ӡȫ��.
        /// </summary>
        private void m_mthResetWhenEndPrint()
        {
            m_objPrintLineContext.m_mthReset();

            m_intYPos = 155;

            m_intCurrentPage = 1;
        }

        #endregion
    }
}
