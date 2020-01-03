using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;

namespace iCare
{
    /// <summary>
    /// ��ʱ��¼��ӡ������
    /// </summary>
    public class clsBrothRecords_F2PrintTool : infPrintRecord
    {
        private bool m_blnIsFromDataSource = true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
        private bool m_blnWantInit = true;
        private clsPrintInfo_InPatientCaseHistory m_objPrintInfo;
        private clsBaseCaseHistoryDomain m_objRecordsDomain;

        private clsBrothRecords_F2Domain m_objInRoomDomain;//m_objDomain;
      //  private clsNewBabyInRoomRecordDomain m_objInRoomDomain;

        public clsBrothRecords_F2PrintTool()
        {
         //   m_objInRoomDomain = new clsNewBabyInRoomRecordDomain();
            m_objInRoomDomain = new clsBrothRecords_F2Domain();//clsBrothRecords_F2Domain();

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
            m_objPrintInfo.m_strAge = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrAge : "";
            m_objPrintInfo.m_strBedName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName : "";
            m_objPrintInfo.m_strDeptName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjDept.m_StrDeptName : "";
            m_objPrintInfo.m_strAreaName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName : "";
            m_objPrintInfo.m_dtmInPatientDate = p_dtmInPatientDate;
            m_objPrintInfo.m_dtmOpenDate = p_dtmOpenDate;
            m_objPrintInfo.m_strHISInPatientID = m_objPatient != null ? p_objPatient.m_StrHISInPatientID : "";
            m_objPrintInfo.m_dtmHISInPatientDate = m_objPatient != null ? p_objPatient.m_DtmSelectedHISInDate : DateTime.MinValue;
        }

        /// <summary>
        /// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���(�������ݿ��ȡʱҪ����.)
        /// </summary>
        public void m_mthInitPrintContent()
        {
            m_blnWantInit = false;
            clsBrothRecords_F2Rec[] objRecordArr = null;

            if (m_objPrintInfo == null)
            {
                MDIParent.ShowInformationMessageBox("����m_mthInitPrintContent֮ǰ�����ȵ���m_mthSetPrintInfo����");
                return;
            }

            if (m_objPrintInfo.m_strInPatentID != "")
            {
                m_objRecordsDomain = new clsBaseCaseHistoryDomain(enmBaseCaseHistoryTypeInfo.BrothRecords_F2);
                long lngRes = m_objRecordsDomain.m_lngGetPrintInfo(m_objPrintInfo.m_strInPatentID, m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.MinValue, out m_objPrintInfo.m_objContent, out m_objPrintInfo.m_objPicValueArr, out m_objPrintInfo.m_dtmFirstPrintDate, out m_objPrintInfo.m_blnIsFirstPrint);

                if (m_objPrintInfo.m_objContent != null)
                {
                    lngRes = m_objInRoomDomain.m_lngGetAllModifiedCircsRecordContent(m_objPrintInfo.m_strInPatentID, m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"),/* ((clsBrothRecords_F2)(m_objPrintInfo.m_objContent)).m_dtmBIRTHTIME.ToString("yyyy-MM-dd HH:mm:ss")*/ out objRecordArr);
                }
            }
            //���ñ����ݵ���ӡ��,��ʹ�Ǵ�ӡ�հ׵�,����Ҳ����ִ��.(��:�ڱ������ڲ�,����֮�ϲ�׼��return���,���ǳ�������.)
            m_mthSetPrintContent((clsBrothRecords_F2)m_objPrintInfo.m_objContent, objRecordArr, m_objPrintInfo.m_dtmFirstPrintDate);
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

            m_mthSetPrintContent((clsBrothRecords_F2)m_objPrintInfo.m_objContent, null, m_objPrintInfo.m_dtmFirstPrintDate);
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
            TopY = 150,
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
        private void m_mthSetPrintContent(clsBrothRecords_F2 p_objContent, clsBrothRecords_F2Rec[] p_objCircsContentArr, DateTime p_dtmFirstPrintDate)
        {
            m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
                                                                        new clsPrint1(),
                                                                        new  clsPrint2(),
                                                                        new clsPrint3(),
                                                                        new clsPrint4(),
                                                                        new clsPrint5(),
                                                                        new clsPrint6(),
                                                                        new clsPrint7(),
                                                                        new clsPrint8(),
                                                                        new clsPrint9(),                                                                       
                                                                        new clsPrintCircsRecordHeader(),
                                                                        new clsPrintCircsRecordContent()                                                                        
                                                                       });
            m_objPrintLineContext.m_ObjPrintSign = new com.digitalwave.Utility.Controls.clsPrintRecordSign();

            object[] objData = new Object[3];
            objData[0] = m_objChangePrintTextColor(p_objContent);
            objData[1] = m_objPrintInfo;
            objData[2] = p_objCircsContentArr;

            //���ô�ӡ��Ϣ������Set Value��ȥ
            m_objPrintLineContext.m_ObjPrintLineInfo = objData;
            //�����ݿ��ó�����FirstPrintDate����ÿ����ӡ�������m_DtmFirstPrintTime���ڸ���������
            m_objPrintLineContext.m_DtmFirstPrintTime = p_dtmFirstPrintDate;
        }

        private clsBrothRecords_F2 m_objChangePrintTextColor(clsBrothRecords_F2 p_objclsInPatientCase)
        {
            if (p_objclsInPatientCase == null)
                return null;
            //�Ѱ�ɫ��Ϊ��ɫ
            clsXML_DataGrid objclsXML_DataGrid = new clsXML_DataGrid();
            //p_objclsInPatientCase.m_strOTHERCHECKXML = objclsXML_DataGrid.m_strReplaceWhiteToBlack(p_objclsInPatientCase.m_strOTHERCHECKXML);
            //p_objclsInPatientCase.m_strOTHERCHECKXML = objclsXML_DataGrid.m_strReplaceWhiteToBlack(p_objclsInPatientCase.m_strOTHERCHECKXML);

            //p_objclsInPatientCase.m_strOUTHOSPITALADVICEXML = objclsXML_DataGrid.m_strReplaceWhiteToBlack(p_objclsInPatientCase.m_strOUTHOSPITALADVICEXML);
            //p_objclsInPatientCase.m_strDEALWITHXML = objclsXML_DataGrid.m_strReplaceWhiteToBlack(p_objclsInPatientCase.m_strDEALWITHXML);

            return p_objclsInPatientCase;
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
            BabySex_Title,
            BabySex,
            BirthTime_Title,
            Birth,

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
                        m_fReturnPoint = new PointF(340f - fltOffsetX, 80f);
                        break;
                    case (int)enmItemDefination.Page_Name_Title:
                        m_fReturnPoint = new PointF(280f - fltOffsetX, 110f);
                        break;
                    case (int)enmItemDefination.Name_Title:
                        m_fReturnPoint = new PointF(50f - fltOffsetX, 150f);
                        break;
                    case (int)enmItemDefination.Name:
                        m_fReturnPoint = new PointF(130f - fltOffsetX, 150f);
                        break;

                    case (int)enmItemDefination.BabySex_Title:
                        m_fReturnPoint = new PointF(250f - fltOffsetX, 150f);
                        break;
                    case (int)enmItemDefination.BabySex:
                        m_fReturnPoint = new PointF(330f - fltOffsetX, 150f);
                        break;

                    case (int)enmItemDefination.BirthTime_Title:
                        m_fReturnPoint = new PointF(400f - fltOffsetX, 150f);
                        break;
                    case (int)enmItemDefination.Birth:
                        m_fReturnPoint = new PointF(500f - fltOffsetX, 150f);
                        break;
                    case (int)enmItemDefination.InPatientID_Title:
                        m_fReturnPoint = new PointF(610f - fltOffsetX, 115f);
                        break;
                    case (int)enmItemDefination.InPatientID:
                        m_fReturnPoint = new PointF(680f - fltOffsetX, 115f);
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

            PrintWidth = 630,
            PrintWidth2 = 710,

        }

        #endregion

        #region PrintClasses
        private abstract class clsPrintInPatientCaseInfo : com.digitalwave.Utility.Controls.clsPrintLineBase
        {
            protected clsBrothRecords_F2 m_objContent;
            protected Pen m_GridPen = new Pen(Color.Black);
            /// <summary>
            /// ���־�����ߵı߾�
            /// </summary>
            protected int m_intRecBaseX = clsPrintPosition.c_intLeftX;
            protected int m_intPatientInfoX = 70;
            protected clsPrintInfo_InPatientCaseHistory m_objPrintInfo;
            protected clsBrothRecords_F2Rec[] m_objPrintCircsArr;

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
                    m_objContent = (clsBrothRecords_F2)objData[0];
                    m_objPrintInfo = (clsPrintInfo_InPatientCaseHistory)objData[1];
                    m_objPrintCircsArr = (clsBrothRecords_F2Rec[])objData[2];
                }
            }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private class clsPrint1 : clsPrintInPatientCaseInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    p_intPosY += 20;

                    //  p_objGrp.DrawString("һ�������", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

                    p_objGrp.DrawString("���������� " + (m_objContent.M_TXTAIRENNAME == null ? "" : m_objContent.M_TXTAIRENNAME) + "�����䣺 " + (m_objContent.M_TXTAGE == null ? "" : m_objContent.M_TXTAGE) + "�����᣺ " +
                        (m_objContent.M_TXTJIGUAN == null ? "" : m_objContent.M_TXTJIGUAN) + "��ְҵ�� " + (m_objContent.M_TXTZHIYE == null ? "" : m_objContent.M_TXTZHIYE) + "����ְ������ " +
                        (m_objContent.M_TXTRENZHI == null ? "" : m_objContent.M_TXTRENZHI) + "��סַ�� " + (m_objContent.M_TXTZHUZHI == null ? "" : m_objContent.M_TXTZHUZHI) + "��Ӥ��סԺ�ţ� " + (m_objContent.M_TXTBABYID == null ? "" : m_objContent.M_TXTBABYID), p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

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
        /// ������Ĥʱ�伱��ʽ
        /// </summary>
        private class clsPrint2 : clsPrintInPatientCaseInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    p_intPosY += 20;

                    p_objGrp.DrawString("����ʼ�ڣ� " + (m_objContent.M_DTPGONGSUOTIME.ToString() == null ? "" : m_objContent.M_DTPGONGSUOTIME.ToString()) + "��̥Ĥ���ڣ� " + (m_objContent.M_DTPPOMOTIME.ToString() == null ? "" : m_objContent.M_DTPPOMOTIME.ToString()), p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                  //  p_intPosY += 20;
                    p_objGrp.DrawString("Ĥ�Ʒ�ʽ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 450, p_intPosY);

                    string strChildBearing0 = m_objContent.M_RDBMOPO[0].ToString();//����
                    string strChildBearing1 = m_objContent.M_RDBMOPO[1].ToString();//������                           

                    string strPrint = (strChildBearing0 == "0" ? "" : "����") + (strChildBearing1 == "0" ? "" : "������");
                    p_objGrp.DrawString(strPrint, p_fntNormalText, Brushes.Black, m_intRecBaseX + 540, p_intPosY);
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
        /// ���ڿ�ȫ,̥λ.....
        /// </summary>
        private class clsPrint3 : clsPrintInPatientCaseInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary> 
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    p_intPosY += 20;

                    //  p_objGrp.DrawString("һ�������", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

                    p_objGrp.DrawString("���ڿ�ȫ�� " + (m_objContent.M_DTPGONGKOUKAITIME.ToString() == null ? "" : m_objContent.M_DTPGONGKOUKAITIME.ToString()) + "��̥�Σ� " + (m_objContent.M_CBOTAICI == null ? "" : m_objContent.M_CBOTAICI) + "�����ڣ� " +
                        (m_objContent.M_CBOYUNQI == null ? "" : m_objContent.M_CBOYUNQI) + "��̥λ�� " + (m_objContent.M_CBOTAIWEI == null ? "" : m_objContent.M_CBOTAIWEI), p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

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
        /// ����
        /// </summary>
        private class clsPrint4 : clsPrintInPatientCaseInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    p_intPosY += 20;

                    p_objGrp.DrawString("������ " + (m_objContent.M_DTPBROTHTIME == null ? "" : m_objContent.M_DTPBROTHTIME.ToString()), p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //  p_intPosY += 20;
                    p_objGrp.DrawString("��ʽ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 200, p_intPosY);

                    string strChildBearing0 = m_objContent.M_DTPBROTHTYPE[0].ToString();//��Ȼ��
                    string strChildBearing1 = m_objContent.M_DTPBROTHTYPE[1].ToString();//������                           

                    string strPrint = (strChildBearing0 == "0" ? "" : "��Ȼ��") + (strChildBearing1 == "0" ? "" : "������");

                    p_objGrp.DrawString(strPrint, p_fntNormalText, Brushes.Black, m_intRecBaseX + 250, p_intPosY);

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
        /// ̥�����
        /// </summary>
        private class clsPrint5 : clsPrintInPatientCaseInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    p_intPosY += 20;

                    p_objGrp.DrawString("̥������� " + (m_objContent.M_DTPTAIPANTIME == null ? "" : m_objContent.M_DTPTAIPANTIME.ToString()), p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //  p_intPosY += 20;
                    p_objGrp.DrawString("��ʽ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 230, p_intPosY);

                    string strChildBearing0 = m_objContent.M_RDBTAIPANTYPE[0].ToString();//��Ȼ
                    string strChildBearing1 = m_objContent.M_RDBTAIPANTYPE[1].ToString();//�ȳ�    
                    string strChildBearing2 = m_objContent.M_RDBTAIPANTYPE[2].ToString();//����
                    string strChildBearing3 = m_objContent.M_RDBTAIPANTYPE[3].ToString();//��ʽ                           
                    string strChildBearing4 = m_objContent.M_RDBTAIPANTYPE[4].ToString();//����   

                    string strPrint = (strChildBearing0 == "0" ? "" : "��Ȼ") + (strChildBearing1 == "0" ? "" : "�ȳ�") + (strChildBearing2 == "0" ? "" : "����") + (strChildBearing3 == "0" ? "" : "��ʽ") + (strChildBearing4 == "0" ? "" : "����");

                    p_objGrp.DrawString(strPrint, p_fntNormalText, Brushes.Black, m_intRecBaseX + 280, p_intPosY);
                    
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
        /// ̥��
        /// </summary>
        private class clsPrint6 : clsPrintInPatientCaseInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    p_intPosY += 20;

                    p_objGrp.DrawString("̥�̣�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

                    p_objGrp.DrawString("��״�� " + (m_objContent.M_CBOXINGZHUANG == null ? "" : m_objContent.M_CBOXINGZHUANG) + "�������� " + (m_objContent.M_CBOZHONGLIANG == null ? "" : m_objContent.M_CBOZHONGLIANG) + "����С�� " +
                        (m_objContent.M_CBODAXIAO == null ? "" : m_objContent.M_CBODAXIAO) + "�������� " + (m_objContent.M_CBOWANZHENG == null ? "" : m_objContent.M_CBOWANZHENG), p_fntNormalText, Brushes.Black, m_intRecBaseX + 50, p_intPosY);
                    p_intPosY += 20;
                    p_objGrp.DrawString("������Ŵ��� " + (m_objContent.M_CBOQIDAI == null ? "" : m_objContent.M_CBOQIDAI) + "���곤�� " + (m_objContent.M_CBOQICHANG == null ? "" : m_objContent.M_CBOQICHANG) + "�������� " +
                       (m_objContent.M_CBOZZHILIU == null ? "" : m_objContent.M_CBOZZHILIU), p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

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
        /// ����
        /// </summary>
        private class clsPrint7 : clsPrintInPatientCaseInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    p_intPosY += 20;

                    //  p_objGrp.DrawString("̥������� " + (m_objContent.M_DTPTAIPANTIME == null ? "" : m_objContent.M_DTPTAIPANTIME), p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //  p_intPosY += 20;
                    p_objGrp.DrawString("������", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

                    string strChildBearing0 = m_objContent.M_RDBHUIYINTYPE[0].ToString();//δ��
                    string strChildBearing1 = m_objContent.M_RDBHUIYINTYPE[1].ToString();//����    
                    string strChildBearing2 = m_objContent.M_RDBHUIYINTYPE[2].ToString();//����
                    string strChildBearing3 = m_objContent.M_RDBHUIYINTYPE[3].ToString();//1                           
                    string strChildBearing4 = m_objContent.M_RDBHUIYINTYPE[4].ToString();//2   
                    string strChildBearing5 = m_objContent.M_RDBHUIYINTYPE[5].ToString();//3   

                    string strPrint = (strChildBearing0 == "0" ? "" : "δ��") + (strChildBearing1 == "0" ? "" : "����") + (strChildBearing2 == "0" ? "" : "����") + (strChildBearing3 == "0" ? "" : "1��") + (strChildBearing4 == "0" ? "" : "2��") + (strChildBearing5 == "0" ? "" : "3��");

                    p_objGrp.DrawString(strPrint, p_fntNormalText, Brushes.Black, m_intRecBaseX + 60, p_intPosY);
                    p_objGrp.DrawString("�п�����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 100, p_intPosY);
                    string strChildBearin0 = m_objContent.M_RDBHUIYINQIEKAITYPE[0].ToString();//��
                    string strChildBearin1 = m_objContent.M_RDBHUIYINQIEKAITYPE[1].ToString();//��    
                    string strChildBearin2 = m_objContent.M_RDBHUIYINQIEKAITYPE[2].ToString();//����
                    string strPrint1 = (strChildBearin0 == "0" ? "" : "��") + (strChildBearin1 == "0" ? "" : "��") + (strChildBearin2 == "0" ? "" : "����");

                    p_objGrp.DrawString(strPrint1, p_fntNormalText, Brushes.Black, m_intRecBaseX + 180, p_intPosY);

                    p_objGrp.DrawString("�ڷ죺 " + (m_objContent.M_CBONEIFENG == null ? "" : m_objContent.M_CBONEIFENG) + "�룻��죺 " + (m_objContent.M_CBOWAIFENG == null ? "" : m_objContent.M_CBOWAIFENG) + "�룻ʧѪ�� " +
                      (m_objContent.M_CBOSHIXUE == null ? "" : m_objContent.M_CBOSHIXUE) + "ml��������������׸� ��" + (m_objContent.M_CBOGONGDIGAO == null ? "" : m_objContent.M_CBOGONGDIGAO), p_fntNormalText, Brushes.Black, m_intRecBaseX + 250, p_intPosY);
                    p_intPosY += 20;
                    p_objGrp.DrawString("��������� " + (m_objContent.M_CBOGONGJINGQINGKUANG == null ? "" : m_objContent.M_CBOGONGJINGQINGKUANG) + "������Ѫѹ�� " + (m_objContent.M_CBOXUEYA1 == null ? "" : m_objContent.M_CBOXUEYA1) + "/" +
                       (m_objContent.M_CBOXUEYA2 == null ? "" : m_objContent.M_CBOXUEYA2) + "kpa�������� " + (m_objContent.M_CBOHUXI == null ? "" : m_objContent.M_CBOHUXI) + "��/�֣������� " + (m_objContent.M_CBOMAIBO == null ? "" : m_objContent.M_CBOMAIBO) + "��/��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);


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
        /// Ӥ�� 
        /// </summary>
        private class clsPrint8 : clsPrintInPatientCaseInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    p_intPosY += 20;

                    //  p_objGrp.DrawString("̥������� " + (m_objContent.M_DTPTAIPANTIME == null ? "" : m_objContent.M_DTPTAIPANTIME), p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //  p_intPosY += 20;
                    p_objGrp.DrawString("Ӥ�����Ա�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

                    string strChildBearing0 = m_objContent.M_RDBYINGER[0].ToString();//��
                    string strChildBearing1 = m_objContent.M_RDBYINGER[1].ToString();//Ů    
                    string strChildBearing2 = m_objContent.M_RDBYINGER[2].ToString();//��Ӥ
                    string strChildBearing3 = m_objContent.M_RDBYINGER[3].ToString();//̥ 

                    string strPrint = (strChildBearing0 == "0" ? "" : "��") + (strChildBearing1 == "0" ? "" : "Ů") + (strChildBearing2 == "0" ? "" : "��Ӥ") + (strChildBearing3 == "0" ? "" : "̥");

                    p_objGrp.DrawString(strPrint, p_fntNormalText, Brushes.Black, m_intRecBaseX + 90, p_intPosY);

                    p_objGrp.DrawString("����ԭ�� " + (m_objContent.M_CBOSIWANGYUANYIN == null ? "" : m_objContent.M_CBOSIWANGYUANYIN) , p_fntNormalText, Brushes.Black, m_intRecBaseX + 135, p_intPosY);
                   
                    string strChildBearin0 = m_objContent.M_RDBHUXITYPE[0].ToString();//��Ȼ����
                    string strChildBearin1 = m_objContent.M_RDBHUXITYPE[1].ToString();//�����Ϣ    
                    string strChildBearin2 = m_objContent.M_RDBHUXITYPE[2].ToString();//������Ϣ
                    string strChildBearin3 = m_objContent.M_RDBHUXITYPE[3].ToString();//�԰���Ϣ
                    string strPrint1 = (strChildBearin0 == "0" ? "" : "��Ȼ����") + (strChildBearin1 == "0" ? "" : "�����Ϣ") + (strChildBearin2 == "0" ? "" : "������Ϣ") + (strChildBearin3 == "0" ? "" : "�԰���Ϣ");

                    p_objGrp.DrawString(strPrint1, p_fntNormalText, Brushes.Black, m_intRecBaseX + 255, p_intPosY);

                    p_objGrp.DrawString("���أ� " + (m_objContent.M_CBOTIZHONG == null ? "" : m_objContent.M_CBOTIZHONG) + "���֣����� " + (m_objContent.M_CBOSHENCHANG == null ? "" : m_objContent.M_CBOSHENCHANG) + "���� ", p_fntNormalText, Brushes.Black, m_intRecBaseX + 360, p_intPosY);
                    p_intPosY += 20;
                    p_objGrp.DrawString("˫������ " + (m_objContent.M_CBOSHUANGDINGJING == null ? "" : m_objContent.M_CBOSHUANGDINGJING) + "���֣����� " + (m_objContent.M_CBOZHENJING == null ? "" : m_objContent.M_CBOZHENJING) + "���֣��ģ� " + (m_objContent.M_CBOXIN == null ? "" : m_objContent.M_CBOXIN) + "���Σ�" + (m_objContent.M_CBOFEI == null ? "" : m_objContent.M_CBOFEI) + "�����Σ�" + (m_objContent.M_CBOJIXING == null ? "" : m_objContent.M_CBOJIXING), p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);


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
        /// ���� ��ǩ��
        /// </summary>
        private class clsPrint9 : clsPrintInPatientCaseInfo 
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }

                if (m_blnIsFirstPrint)
                {                   
                  
                    p_intPosY += 20;
                    p_objGrp.DrawString("��һ���̣� " + (m_objContent.M_CBOYICHANCHENG == null ? "" : m_objContent.M_CBOYICHANCHENG) + "���ڶ����̣� " + (m_objContent.M_CBOERCHANCHENG == null ? "" : m_objContent.M_CBOERCHANCHENG)  , p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    p_objGrp.DrawString("�������̣� " + (m_objContent.M_CBOSANCHANCHENG == null ? "" : m_objContent.M_CBOSANCHANCHENG) + "��ȫ�̣�" + (m_objContent.M_CBOQUANCHENG == null ? "" : m_objContent.M_CBOQUANCHENG), p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    p_objGrp.DrawString("�����ˣ� " + (m_objContent.M_TXTJIESHEN == null ? "" : m_objContent.M_TXTJIESHEN) + "�������ˣ� " + (m_objContent.M_TXTZHULI == null ? "" : m_objContent.M_TXTZHULI) + "�������ˣ� " + (m_objContent.M_TXTHULI == null ? "" : m_objContent.M_TXTHULI) + "��ָ���ˣ�" + (m_objContent.M_TXTZHIDAO == null ? "" : m_objContent.M_TXTZHIDAO), p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                  
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
        /// �������������¼���⼰����ͷ
        /// </summary>
        private class clsPrintCircsRecordHeader : clsPrintInPatientCaseInfo
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
                    p_intPosY += 30;
                    m_fReturnPoint = new PointF(300f, p_intPosY);
                    p_objGrp.DrawString(" �� ʱ �� ¼", m_fotTitleFont, Brushes.Black, m_fReturnPoint);
                    p_intPosY += 25;

                    p_objGrp.DrawLine(m_GridPen, m_intRecBaseX - 10, p_intPosY, m_intRecBaseX + 744, p_intPosY);

                    #region ������ͷ
                    int intPosX = m_intRecBaseX - 10;
                    p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY, intPosX, p_intPosY + 80);//0 
                    p_objGrp.DrawString("����", m_fotContentFont, Brushes.Black, new RectangleF(intPosX + 20f, p_intPosY + 20, 50, 80));
                    intPosX += 110;
                    p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY, intPosX, p_intPosY + 80);//1					
                    p_objGrp.DrawString("Ѫѹ", m_fotContentFont, Brushes.Black, new RectangleF(intPosX + 3f, p_intPosY + 20, 20, 80));
                    intPosX += 50;
                    p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY, intPosX, p_intPosY + 80);//2				
                    p_objGrp.DrawString("������Ъ", m_fotContentFont, Brushes.Black, new RectangleF(intPosX + 3f, p_intPosY + 10, 20, 80));
                    intPosX += 50;
                    p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY , intPosX, p_intPosY + 80);//3		
                    p_objGrp.DrawString("������ʱ", m_fotContentFont, Brushes.Black, new RectangleF(intPosX + 3f, p_intPosY + 10, 20, 80));
                  //  p_objGrp.DrawString("ͷ", m_fotContentFont, Brushes.Black, new RectangleF(intPosX + 10f, p_intPosY + 2, 20, 80));
                    intPosX += 50;
                    p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY , intPosX, p_intPosY + 80);//4		
                    p_objGrp.DrawString("̥�� ��/��", m_fotContentFont, Brushes.Black, new RectangleF(intPosX + 3f, p_intPosY + 2, 20, 80));
                    intPosX += 50;
                    p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY, intPosX, p_intPosY + 80);//5		
                    p_objGrp.DrawString("���ڿ���", m_fotContentFont, Brushes.Black, new RectangleF(intPosX + 10f, p_intPosY + 10, 20, 80));
                    intPosX += 70;
                    //p_objGrp.DrawString("��", m_fotContentFont, Brushes.Black, new RectangleF(intPosX - 5f, p_intPosY + 2, 20, 80));
                    p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY , intPosX, p_intPosY + 80);//6		
                    p_objGrp.DrawString("̥Ĥ���", m_fotContentFont, Brushes.Black, new RectangleF(intPosX + 3f, p_intPosY + 8, 20, 80));
                    intPosX += 70;
                    p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY, intPosX, p_intPosY + 80);//7		
                    p_objGrp.DrawString("��¶�ߵ�", m_fotContentFont, Brushes.Black, new RectangleF(intPosX + 3f, p_intPosY + 8, 20, 80));
                    intPosX += 70;
                  //  p_objGrp.DrawString("��", m_fotContentFont, Brushes.Black, new RectangleF(intPosX - 5f, p_intPosY + 2, 20, 80));
                    p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY , intPosX, p_intPosY + 80);//8		
                    p_objGrp.DrawString("��鷨", m_fotContentFont, Brushes.Black, new RectangleF(intPosX + 10f, p_intPosY + 17, 20, 80));
                    intPosX += 70;
                    p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY, intPosX, p_intPosY + 80);//9							
                    p_objGrp.DrawString("���׼�����", m_fotContentFont, Brushes.Black, new RectangleF(intPosX + 3f, p_intPosY + 2, 20, 80));
                  //  p_objGrp.DrawString("Ƥ��", m_fotContentFont, Brushes.Black, new RectangleF(intPosX + 20f, p_intPosY + 2, 80, 80));
                    intPosX += 53;
                    p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY , intPosX, p_intPosY + 80);//10		
                    p_objGrp.DrawString("����������", m_fotContentFont, Brushes.Black, new RectangleF(intPosX + 3f, p_intPosY + 2, 20, 80));
                    intPosX += 50;                   
                    p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY, intPosX, p_intPosY + 80);//18
                    p_objGrp.DrawString("ǩ��", m_fotContentFont, Brushes.Black, new RectangleF(intPosX + 10f, p_intPosY + 20, 60f, 80f));
                    intPosX += 60;
                    p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY, intPosX, p_intPosY + 80);//19

                  //  p_objGrp.DrawLine(m_GridPen, m_intRecBaseX + 70, p_intPosY + 17, intPosX - 181, p_intPosY + 17);
                    p_objGrp.DrawLine(m_GridPen, m_intRecBaseX - 10, p_intPosY + 80, m_intRecBaseX + 744, p_intPosY + 80);
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
        /// ��ʱ��¼�������
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

                p_intPosY += 80;

                if (m_blnIsFirstPrint)
                {
                    for (int i = 0; i < m_objPrintCircsArr.Length; i++)
                    {
                        int intThisLows = 0;
                        string[] strArrTemp;
                        string[] strXMLArrTemp;
                        p_intPosY += 2;
                        #region ��ӡ�������
                        int intTempX = m_intRecBaseX - 9;
                        p_objGrp.DrawString(m_objPrintCircsArr[i].m_dtmRecordDate.ToString("yyyy-MM-dd hh:mm:ss"), m_fotTimetFont, Brushes.Black, intTempX, p_intPosY);
                        intTempX += 110;
                        //p_objGrp.DrawString(m_objPrintCircsArr[i].m_strBIRTHDAYS, m_fotContentFont, Brushes.Black, intTempX, p_intPosY);
                        //intTempX += 20;
                        ctlRichTextBox.m_mthSplitXml(m_objPrintCircsArr[i].str_XUEYA, m_objPrintCircsArr[i].str_XUEYAXML, 3, out strArrTemp, out strXMLArrTemp);
                        if (intThisLows < strArrTemp.Length)
                            intThisLows = strArrTemp.Length;
                        p_objGrp.DrawString(m_objPrintCircsArr[i].str_XUEYA, m_fotContentFont, Brushes.Black, new RectangleF(intTempX, p_intPosY, 38, 100));
                        intTempX += 50;
                        strArrTemp = null;
                        strXMLArrTemp = null;
                        ctlRichTextBox.m_mthSplitXml(m_objPrintCircsArr[i].str_GONGSUOJIANXUE, m_objPrintCircsArr[i].str_GONGSUOJIANXUEXML, 3, out strArrTemp, out strXMLArrTemp);
                        if (intThisLows < strArrTemp.Length)
                            intThisLows = strArrTemp.Length;
                        p_objGrp.DrawString(m_objPrintCircsArr[i].str_GONGSUOJIANXUE, m_fotContentFont, Brushes.Black, new RectangleF(intTempX, p_intPosY, 38, 100));
                        intTempX += 50;
                        strArrTemp = null;
                        strXMLArrTemp = null;
                        ctlRichTextBox.m_mthSplitXml(m_objPrintCircsArr[i].str_GONGSUOTIME, m_objPrintCircsArr[i].str_GONGSUOTIMEXML, 3, out strArrTemp, out strXMLArrTemp);
                        if (intThisLows < strArrTemp.Length)
                            intThisLows = strArrTemp.Length;
                        p_objGrp.DrawString(m_objPrintCircsArr[i].str_GONGSUOTIME, m_fotContentFont, Brushes.Black, new RectangleF(intTempX, p_intPosY, 38, 100));
                        intTempX += 50;
                        strArrTemp = null;
                        strXMLArrTemp = null;
                        ctlRichTextBox.m_mthSplitXml(m_objPrintCircsArr[i].str_TAIXIN, m_objPrintCircsArr[i].str_TAIXINXML, 3, out strArrTemp, out strXMLArrTemp);
                        if (intThisLows < strArrTemp.Length)
                            intThisLows = strArrTemp.Length;
                        p_objGrp.DrawString(m_objPrintCircsArr[i].str_TAIXIN, m_fotContentFont, Brushes.Black, new RectangleF(intTempX, p_intPosY, 38, 100));
                        intTempX += 50;
                        strArrTemp = null;
                        strXMLArrTemp = null;
                        ctlRichTextBox.m_mthSplitXml(m_objPrintCircsArr[i].str_GONGKOU, m_objPrintCircsArr[i].str_GONGKOUXML, 3, out strArrTemp, out strXMLArrTemp);
                        if (intThisLows < strArrTemp.Length)
                            intThisLows = strArrTemp.Length;
                        p_objGrp.DrawString(m_objPrintCircsArr[i].str_GONGKOU, m_fotContentFont, Brushes.Black, new RectangleF(intTempX, p_intPosY, 38, 100));
                        intTempX += 70;
                        strArrTemp = null;
                        strXMLArrTemp = null;
                        ctlRichTextBox.m_mthSplitXml(m_objPrintCircsArr[i].str_TAIMO, m_objPrintCircsArr[i].str_TAIMOXML, 3, out strArrTemp, out strXMLArrTemp);
                        if (intThisLows < strArrTemp.Length)
                            intThisLows = strArrTemp.Length;
                        p_objGrp.DrawString(m_objPrintCircsArr[i].str_TAIMO, m_fotContentFont, Brushes.Black, new RectangleF(intTempX, p_intPosY, 38, 100));
                        intTempX += 70;
                        strArrTemp = null;
                        strXMLArrTemp = null;
                        ctlRichTextBox.m_mthSplitXml(m_objPrintCircsArr[i].str_XIANLU, m_objPrintCircsArr[i].str_XIANLUXML, 3, out strArrTemp, out strXMLArrTemp);
                        if (intThisLows < strArrTemp.Length)
                            intThisLows = strArrTemp.Length;
                        p_objGrp.DrawString(m_objPrintCircsArr[i].str_XIANLU, m_fotContentFont, Brushes.Black, new RectangleF(intTempX, p_intPosY, 38, 100));
                        intTempX += 70;
                        strArrTemp = null;
                        strXMLArrTemp = null;
                        ctlRichTextBox.m_mthSplitXml(m_objPrintCircsArr[i].str_JIANCHAFA, m_objPrintCircsArr[i].str_JIANCHAFAXML, 3, out strArrTemp, out strXMLArrTemp);
                        if (intThisLows < strArrTemp.Length)
                            intThisLows = strArrTemp.Length;
                        p_objGrp.DrawString(m_objPrintCircsArr[i].str_JIANCHAFA, m_fotContentFont, Brushes.Black, new RectangleF(intTempX, p_intPosY, 38, 100));
                        intTempX += 70;
                        strArrTemp = null;
                        strXMLArrTemp = null;
                        ctlRichTextBox.m_mthSplitXml(m_objPrintCircsArr[i].str_GONGDIJIZHOU, m_objPrintCircsArr[i].str_GONGDIJIZHOUXML, 3, out strArrTemp, out strXMLArrTemp);
                        if (intThisLows < strArrTemp.Length)
                            intThisLows = strArrTemp.Length;
                        p_objGrp.DrawString(m_objPrintCircsArr[i].str_GONGDIJIZHOU, m_fotContentFont, Brushes.Black, new RectangleF(intTempX, p_intPosY, 38, 100));
                        intTempX += 53;
                        strArrTemp = null;
                        strXMLArrTemp = null;
                        ctlRichTextBox.m_mthSplitXml(m_objPrintCircsArr[i].str_FENMIWU, m_objPrintCircsArr[i].str_FENMIWUXML, 3, out strArrTemp, out strXMLArrTemp);
                        if (intThisLows < strArrTemp.Length)
                            intThisLows = strArrTemp.Length;
                        p_objGrp.DrawString(m_objPrintCircsArr[i].str_FENMIWU, m_fotContentFont, Brushes.Black, new RectangleF(intTempX, p_intPosY, 38, 100));
                        intTempX += 50;
                    
                        p_objGrp.DrawString(m_objPrintCircsArr[i].m_strSignUserName, m_fotContentFont, Brushes.Black, intTempX, p_intPosY);
                        #endregion

                        #region ��ӡ���б����
                        int intPosX = m_intRecBaseX - 10;
                        int intThisLowHeight = intThisLows * 17;
                        p_intPosY -= 2;
                        p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY, intPosX, p_intPosY + intThisLowHeight);
                        intPosX += 110;
                        p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY, intPosX, p_intPosY + intThisLowHeight);
                        intPosX += 50;
                        p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY, intPosX, p_intPosY + intThisLowHeight);
                        intPosX += 50;
                        p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY, intPosX, p_intPosY + intThisLowHeight);
                        intPosX += 50;
                        p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY, intPosX, p_intPosY + intThisLowHeight);
                        intPosX += 50;
                        p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY, intPosX, p_intPosY + intThisLowHeight);
                        intPosX += 70;
                        p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY, intPosX, p_intPosY + intThisLowHeight);
                        intPosX += 70;
                        p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY, intPosX, p_intPosY + intThisLowHeight);
                        intPosX += 70;
                        p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY, intPosX, p_intPosY + intThisLowHeight);
                        intPosX += 70;
                        p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY, intPosX, p_intPosY + intThisLowHeight);
                        intPosX += 53;
                        p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY, intPosX, p_intPosY + intThisLowHeight);
                        intPosX += 50;                       
                        p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY, intPosX, p_intPosY + intThisLowHeight);
                        intPosX += 60;
                        p_objGrp.DrawLine(m_GridPen, intPosX, p_intPosY, intPosX, p_intPosY + intThisLowHeight);

                        p_objGrp.DrawLine(m_GridPen, m_intRecBaseX - 10, p_intPosY + intThisLowHeight, m_intRecBaseX + 744, p_intPosY + intThisLowHeight);

                        p_intPosY += intThisLowHeight;
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

            e.Graphics.DrawString("      �� ʱ �� ¼", m_fotTitleFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_Name_Title));

            e.Graphics.DrawString("סԺ�ţ�", m_fotItemHead, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID_Title));
            e.Graphics.DrawString(m_objPrintInfo.m_strHISInPatientID, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID));
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

            m_intYPos = 55;

            m_intCurrentPage = 1;
        }

        #endregion
    }
}
