using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;

namespace iCare
{
    /// <summary>
    /// clsEMR_OutHospitalIn24HoursPrintTool 
    /// Description:��Ժ24Сʱ��Ժ
    /// Author:Jock
    /// Date:05-12-27
    /// </summary>
    public class clsEMR_OutHospitalIn24HoursPrintTool : infPrintRecord
    {

        private bool m_blnIsFromDataSource = true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
        private bool m_blnWantInit = true;

        private clsOutHospitalIn24HoursDomain m_objRecordsDomain;

        private clsPrintInfo_OutHospitalIn24Hours m_objPrintInfo;

        private clsEMR_OutHospitalIn24HoursValue m_objRecordContentOutIn24 = null;

        public clsEMR_OutHospitalIn24HoursPrintTool()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
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
            m_objPrintInfo = new clsPrintInfo_OutHospitalIn24Hours();
            m_objPrintInfo.m_strInPatentID = m_objPatient != null ? m_objPatient.m_StrInPatientID : "";
            m_objPrintInfo.m_strPatientName = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrFirstName : "";
            m_objPrintInfo.m_strSex = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrSex : "";
            m_objPrintInfo.m_strAge = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrAge : "";
            m_objPrintInfo.m_strBedName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName : "";
            m_objPrintInfo.m_strDeptName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjDept.m_StrDeptName : "";
            m_objPrintInfo.m_strAreaName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName : "";
            m_objPrintInfo.m_dtmInPatientDate = p_dtmInPatientDate;
            m_objPrintInfo.m_dtmOpenDate = p_dtmOpenDate;

            m_objPrintInfo.m_strNative = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrNativePlace : "";//����
            m_objPrintInfo.m_strOccupation = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrOccupation : "";//ְҵ
            m_objPrintInfo.m_strIsMarried = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrMarried : "";//���
            m_objPrintInfo.m_strFolk = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrNation : "";//����

            m_objPrintInfo.m_strCompany = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrOffice_name : "";//��λ
            m_objPrintInfo.m_strCompanyPhone = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrOfficePhone : "";//��λ�绰
            m_objPrintInfo.m_strAddress = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrHomeAddress : "";//��ַ
            m_objPrintInfo.m_strAddressPhone = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrHomePhone : "";//��ͥ�绰
            m_objPrintInfo.m_strIDCard = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrIDCard : "";//ID Card

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
            if (m_objPrintInfo.m_strInPatentID == "" || m_objPrintInfo.m_dtmOpenDate == DateTime.MinValue)
                m_objRecordContentOutIn24 = null;
            else
            {
                m_objRecordsDomain = new clsOutHospitalIn24HoursDomain();
                clsTrackRecordContent objContent = new clsTrackRecordContent();
                long lngRes = m_objRecordsDomain.m_lngGetRecordContent(m_objPrintInfo.m_strInPatentID, m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"), out objContent);
                if (lngRes <= 0)
                    return;
                m_objRecordContentOutIn24 = (clsEMR_OutHospitalIn24HoursValue)objContent;
            }
            //���ñ����ݵ���ӡ��			
            m_objPrintInfo.m_objRecordContent = m_objRecordContentOutIn24;
            m_mthSetPrintValue();//�����з��ӡ����,��ʹ�ڴ�ӡ�հױ�ʱ,����Ҳ����ִ��.			
        }

        /// <summary>
        /// ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
        /// </summary>
        /// <param name="p_objPrintContent">��ӡ����</param>
        public void m_mthSetPrintContent(object p_objPrintContent)
        {


            m_blnWantInit = false;
            //if (p_objPrintContent.GetType().Name != "clsPrintInfo_Base")
            //{
            //    clsPublicFunction.ShowInformationMessageBox("��������");
            //    return;
            //}
            m_blnIsFromDataSource = false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ
            m_objPrintInfo = (clsPrintInfo_OutHospitalIn24Hours)p_objPrintContent;
            m_objRecordContentOutIn24 = m_objPrintInfo.m_objRecordContent;
            m_mthSetPrintValue();
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
                    clsPublicFunction.ShowInformationMessageBox("�������ݿ��ȡʱ,����m_objGetPrintInfo֮ǰ�����ȵ���m_mthSetPrintInfo����");
                    return null;
                }

                if (m_blnWantInit)
                    m_mthInitPrintContent();
            }

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
            m_fotSmallFont = new Font("SimSun", 11);
            m_GridPen = new Pen(Color.Black, 1);
            m_LinePen = new Pen(Color.Gray, 0.5f);
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
            if (m_blnIsFromDataSource == false || m_objPrintInfo.m_strInPatentID == "" || m_objPrintInfo.m_objRecordContent == null) return;
            //�����ӡ�ɹ�������������Ҫ���µ�ʱ�䣬����У�����ʱ�䡣 
            if (!((PrintEventArgs)p_objPrintArg).Cancel && m_objPrintInfo.m_objRecordContent.m_dtmFirstPrintDate == DateTime.MinValue)
            {
                m_objRecordsDomain.m_lngUpdateFirstPrintDate(m_objPrintInfo.m_strInPatentID, m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"), dtmFirstPrintTime);
            }
            m_mthEndPrintSub((PrintEventArgs)p_objPrintArg);
        }

        private void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
        {
            //ȱʡ�����κζ���
        }

        // ��ӡҳ
        private void m_mthPrintPageSub(PrintPageEventArgs e)
        {
            e.HasMorePages = false;
            m_mthPrintTitleInfo(e);
            Font fntNormal = new Font("SimSun", 12);

            if (m_intPages == 1)
            {
                m_intYPos = 240;
                // e.Graphics.DrawLine(m_LinePen, (int)enmRectangleInfo.LeftX, 206, (int)enmRectangleInfo.RightX, 206);
                //  e.Graphics.DrawLine(m_LinePen, (int)enmRectangleInfo.LeftX, 235, (int)enmRectangleInfo.RightX, 235);
                m_mthPrintBasicInfo(e);
            }



            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, 115, (int)enmRectangleInfo.RightX, 115);


            while (m_objPrintContext.m_BlnHaveMoreLine)
            {
                m_objPrintContext.m_mthPrintNextLine(ref m_intYPos, e.Graphics, fntNormal);

                if (m_intYPos >= (int)enmRectangleInfo.BottomY
                    && m_objPrintContext.m_BlnHaveMoreLine)
                {


                    #region ��ҳ����
                    e.HasMorePages = true;

                    e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, 115, (int)enmRectangleInfo.LeftX, m_intYPos);
                    e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.RightX, 115, (int)enmRectangleInfo.RightX, m_intYPos);
                    e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, m_intYPos, (int)enmRectangleInfo.RightX, m_intYPos);

                    m_intPages++;
                    m_intYPos = (int)enmRectangleInfo.TopY + 20;
                    return;

                    #endregion ��ҳ����
                }

            }

            #region ���һҳ����
            m_intYPos += 30;
            e.Graphics.DrawString("ҽʦǩ��:", new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 440, m_intYPos);

            if (m_objRecordContentOutIn24 != null)
            {
                com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
                clsEmrEmployeeBase_VO objEmpVO = null;
                objEmployeeSign.m_lngGetEmpByNO(m_objRecordContentOutIn24.m_strDOCTORSIGN, out objEmpVO);
                if (objEmpVO != null)
                    if (!(string.IsNullOrEmpty(objEmpVO.m_strGetTechnicalRankAndName)))
                        e.Graphics.DrawString(objEmpVO.m_strGetTechnicalRankAndName, new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 440 + (int)(5f * 17.5f), m_intYPos);
            }

            m_intYPos += 30;
            e.Graphics.DrawString("��¼����:", new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 440, m_intYPos);
            if (m_objRecordContentOutIn24 != null)
                e.Graphics.DrawString(m_objRecordContentOutIn24.m_dtmRECORDDATE.ToString("yyyy��MM��dd��HHʱmm��"), new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 440 + (int)(5f * 17.5f), m_intYPos);

            m_intYPos += 25;
            if (m_intYPos < (int)enmRectangleInfo.BottomY)
                m_intYPos = (int)enmRectangleInfo.BottomY;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, 115, (int)enmRectangleInfo.LeftX, m_intYPos);
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.RightX, 115, (int)enmRectangleInfo.RightX, m_intYPos);
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, m_intYPos, (int)enmRectangleInfo.RightX, m_intYPos);

            #endregion ���һҳ����

            m_intYPos += (int)enmRectangleInfo.RowStep + 15;
            Font fntSign = new Font("", 6);
            while (m_objPrintContext.m_BlnHaveMoreSign)
            {
                m_objPrintContext.m_mthPrintNextSign((int)enmRectangleInfo.LeftX, m_intYPos, e.Graphics, fntSign);

                m_intYPos += (int)enmRectangleInfo.RowStep - 10;
            }

            //ȫ������
            m_objPrintContext.m_mthReset();
            m_intPages = 1;
            m_intYPos = (int)enmRectangleInfo.TopY;
        }

        // ��ӡ����ʱ�Ĳ���
        private void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
        {

        }

        private com.digitalwave.Utility.Controls.clsPrintContext m_objPrintContext;
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
        /// �±��߻���
        /// </summary>
        private Pen m_LinePen;
        /// <summary>
        /// ˢ��
        /// </summary>
        private SolidBrush m_slbBrush;
        /// <summary>
        /// ��ȡ�������
        /// </summary>
        private clsPrintPageSettingForRecord m_objPageSetting;
        /// <summary>
        /// ��ӡ�Ĳ��˻�����Ϣ��
        /// </summary>
        /// 
        private int m_intYPos = (int)enmRectangleInfo.TopY + 7;
        private int m_intPages = 1;

        /// <summary>
        /// ���ӵ���Ϣ
        /// </summary>
        public enum enmRectangleInfo
        {
            /// <summary>
            /// ���ӵĶ���
            /// </summary>
            TopY = 115,
            ///<summary>
            /// ���ӵ����
            /// </summary>
            LeftX = 40,
            /// <summary>
            /// ���ӵ��Ҷ�
            /// </summary>
            RightX = 820 - 40,
            /// <summary>
            /// ����ÿ�еĲ���
            /// </summary>
            RowStep = 20,
            SmallRowStep = 20,

            ColumnsMark1 = 110,

            /// <summary>
            /// �׻���ƫ���ı�����ľ���
            /// </summary>
            BottomLineShift = 15,

            BottomY = 1024
        }

        // <summary>
        /// ��ӡԪ��
        /// </summary>
        private enum enmItemDefination
        {
            //����Ԫ��
            Department_Title,
            Department,
            Area_Title,
            Area,
            Bed_Title,
            Bed,
            InPatientID_Title,
            InPatientID,
            Name_Title,
            Name,
            Sex_Title,
            Sex,
            Age_Title,
            Age,
            Occupation_Title,
            Occupation,
            Native_Title,
            Native,
            Folk_Title,
            Folk,
            IsMarried_Title,
            IsMarried,

            Company_Title,
            Company,
            Company_Phone_Title,
            Company_Phone,

            Address_Title,
            Address,
            Address_Phone_Title,
            Address_Phone,

            SickTeller_Title,
            SickTeller,
            ID_Card_Title,
            ID_Card,


            CardiogramID_Title,
            CardiogramID,

            XRayID_Title,
            XRayID,
            UltrasonicID_Title,
            UltrasonicID,
            MRIID_Title,
            MRIID,
            BrainID_Title,
            BrainID,

            Page_HospitalName,
            Page_Name_Title,
            Page_Title,
            Page_Num,
            Page_Of,
            Page_Count,

            Print_Date_Title,
            Print_Date,

        }

        #region �����ӡ��Ԫ�ص������
        private class clsPrintPageSettingForRecord
        {
            public clsPrintPageSettingForRecord() { }

            /// <summary>
            /// ��������
            /// </summary>
            /// <param name="p_intItemName">��Ŀ����</param>
            /// <returns></returns>
            public PointF m_getCoordinatePoint(int p_intItemName)
            {
                PointF m_fReturnPoint;
                switch (p_intItemName)
                {

                    case (int)enmItemDefination.Page_HospitalName:
                        m_fReturnPoint = new PointF(330f, 30f);
                        break;
                    case (int)enmItemDefination.Page_Name_Title:
                        m_fReturnPoint = new PointF(280f, 60f);
                        break;
                    case (int)enmItemDefination.Department:
                        m_fReturnPoint = new PointF(40f, 97f);
                        break;
                    case (int)enmItemDefination.Department_Title:
                        m_fReturnPoint = new PointF(120f, 100f);
                        break;
                    case (int)enmItemDefination.Area:
                        m_fReturnPoint = new PointF(425f, 100f);
                        break;
                    case (int)enmItemDefination.Area_Title:
                        m_fReturnPoint = new PointF(380f, 100f);
                        break;
                    case (int)enmItemDefination.Bed:
                        m_fReturnPoint = new PointF(605f, 97f);
                        break;
                    case (int)enmItemDefination.Bed_Title:
                        m_fReturnPoint = new PointF(565f, 100f);
                        break;
                    case (int)enmItemDefination.InPatientID_Title:
                        m_fReturnPoint = new PointF(635f, 100f);
                        break;
                    case (int)enmItemDefination.InPatientID:
                        m_fReturnPoint = new PointF(690f, 97f);
                        break;

                    case (int)enmItemDefination.Name_Title:
                        m_fReturnPoint = new PointF(60f, 100f);
                        break;
                    case (int)enmItemDefination.Name:
                        m_fReturnPoint = new PointF(105f, 100f);
                        break;
                    case (int)enmItemDefination.Sex_Title:
                        m_fReturnPoint = new PointF(195f, 100f);
                        break;
                    case (int)enmItemDefination.Sex:
                        m_fReturnPoint = new PointF(240f, 100f);
                        break;

                    case (int)enmItemDefination.Age_Title:
                        m_fReturnPoint = new PointF(275f, 100f);
                        break;
                    case (int)enmItemDefination.Age:
                        m_fReturnPoint = new PointF(320f, 100f);
                        break;
                    case (int)enmItemDefination.Occupation:
                        m_fReturnPoint = new PointF(380f, 100f);
                        break;
                    case (int)enmItemDefination.Native_Title:
                        m_fReturnPoint = new PointF(405f, 100f);
                        break;
                    case (int)enmItemDefination.Native:
                        m_fReturnPoint = new PointF(480f, 100f);
                        break;
                    case (int)enmItemDefination.Folk_Title:
                        m_fReturnPoint = new PointF(560f, 100);
                        break;
                    case (int)enmItemDefination.Folk:
                        m_fReturnPoint = new PointF(610f, 100);
                        break;
                    case (int)enmItemDefination.IsMarried_Title:
                        m_fReturnPoint = new PointF(690f, 100f);
                        break;
                    case (int)enmItemDefination.IsMarried:
                        m_fReturnPoint = new PointF(730f, 100f);
                        break;

                    default:
                        m_fReturnPoint = new PointF(400f, 400f);
                        break;

                }
                return m_fReturnPoint;
            }
        }

        #endregion

        #region ��ӡ�ж���
        private clsPrintLine1[] m_objLine1Arr;
        #endregion


        private DateTime dtmFirstPrintTime;
        /// <summary>
        /// ��ÿһ��ӡ�е�Ԫ�ظ�ֵ
        /// </summary>
        private void m_mthSetPrintValue()
        {

            //�����Ƿ����´�ӡ


            #region  ��һ�δ�ӡʱ�丳ֵ
            dtmFirstPrintTime = DateTime.Now;
            if (m_objRecordContentOutIn24 != null && m_objRecordContentOutIn24.m_dtmFirstPrintDate != DateTime.MinValue)
                dtmFirstPrintTime = m_objRecordContentOutIn24.m_dtmFirstPrintDate;
            #endregion  ��һ�δ�ӡʱ�丳ֵ

            #region ��ӡ�г�ʼ��
            m_objLine1Arr = new clsPrintLine1[7];
            for (int i = 0; i < m_objLine1Arr.Length; i++)
                m_objLine1Arr[i] = new clsPrintLine1();

            m_objPrintContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
                                                                           m_objLine1Arr[0],m_objLine1Arr[1],m_objLine1Arr[2],m_objLine1Arr[3],m_objLine1Arr[4],
                                                                           m_objLine1Arr[5],m_objLine1Arr[6]
                                                                       });
            m_objPrintContext.m_ObjPrintSign = new com.digitalwave.Utility.Controls.clsPrintRecordSign();
            #endregion

            #region ��ÿһ�е�Ԫ�ظ�ֵ
            string strBlanks = "���������������� ���� ��������������";
            if (m_objRecordContentOutIn24 != null)
            {
                Object[] objData1 = new object[4];
                ///////////////1��/////////////////				
                objData1[0] = m_objRecordContentOutIn24.m_strMAINDESCRIPTION;
                objData1[1] = m_objRecordContentOutIn24.m_strMAINDESCRIPTIONXML;
                objData1[2] = dtmFirstPrintTime;
                objData1[3] = "������:";
                m_objLine1Arr[0].m_ObjPrintLineInfo = objData1;

                ///////////////2��/////////////////				
                objData1[0] = m_objRecordContentOutIn24.m_strINHOSPITALINSTANCE;
                objData1[1] = m_objRecordContentOutIn24.m_strINHOSPITALINSTANCEXML;
                objData1[2] = dtmFirstPrintTime;
                objData1[3] = "����Ժ���:";
                m_objLine1Arr[1].m_ObjPrintLineInfo = objData1;
                ///////////////3��/////////////////
                objData1[0] = m_objRecordContentOutIn24.m_strINHOSPITALDIAGNOSE1;
                objData1[1] = m_objRecordContentOutIn24.m_strINHOSPITALDIAGNOSE1XML;
                objData1[3] = "����Ժ���:";
                m_objLine1Arr[2].m_ObjPrintLineInfo = objData1;
                ///////////////4��/////////////////
                //objData1[0] = m_objRecordContentOutIn24.m_strINHOSPITALDIAGNOSE2;
                //objData1[1] = m_objRecordContentOutIn24.m_strINHOSPITALDIAGNOSE2XML;
                //objData1[3] = "  ��Ժ���2:";
                //m_objLine1Arr[3].m_ObjPrintLineInfo = objData1;
                ///////////////4��/////////////////
                objData1[0] = m_objRecordContentOutIn24.m_strDIAGNOSECORUSE;
                objData1[1] = m_objRecordContentOutIn24.m_strDIAGNOSECORUSEXML;
                objData1[3] = "�����ƾ���:";
                m_objLine1Arr[3].m_ObjPrintLineInfo = objData1;
                ///////////////5��/////////////////
                objData1[0] = m_objRecordContentOutIn24.m_strOUTHOSPITALINSTANCE;
                objData1[1] = m_objRecordContentOutIn24.m_strOUTHOSPITALINSTANCEXML;
                objData1[3] = "����Ժ���:";
                m_objLine1Arr[4].m_ObjPrintLineInfo = objData1;
                ///////////////6��/////////////////
                objData1[0] = m_objRecordContentOutIn24.m_strOUTHOSPITALDIAGNOSE1;
                objData1[1] = m_objRecordContentOutIn24.m_strOUTHOSPITALDIAGNOSE1XML;
                objData1[3] = "����Ժ���:";
                m_objLine1Arr[5].m_ObjPrintLineInfo = objData1;
                ///////////////8��/////////////////			
                //objData1[0] = m_objRecordContentOutIn24.m_strOUTHOSPITALDIAGNOSE2;
                //objData1[1] = m_objRecordContentOutIn24.m_strOUTHOSPITALDIAGNOSE2XML;
                //objData1[3] = "����Ժ���2:";
                //m_objLine1Arr[7].m_ObjPrintLineInfo = objData1;
                ///////////////7��/////////////////			
                objData1[0] = m_objRecordContentOutIn24.m_strOUTHOSPITALADVICE1;
                objData1[1] = m_objRecordContentOutIn24.m_strOUTHOSPITALADVICE1XML;
                objData1[3] = "����Ժҽ��:";
                m_objLine1Arr[6].m_ObjPrintLineInfo = objData1;
                ///////////////10��/////////////////			
                //objData1[0] = m_objRecordContentOutIn24.m_strOUTHOSPITALADVICE2;
                //objData1[1] = m_objRecordContentOutIn24.m_strOUTHOSPITALADVICE2XML;
                //objData1[3] = "  ��Ժҽ��2:";
                //m_objLine1Arr[9].m_ObjPrintLineInfo = objData1;

            }
            else
            {
                Object[] objData1 = new object[4];
                ///////////////1��/////////////////				
                objData1[0] = "";
                objData1[1] = "";
                objData1[2] = dtmFirstPrintTime;
                objData1[3] = "������:";
                m_objLine1Arr[0].m_ObjPrintLineInfo = objData1;

                ///////////////2��/////////////////				
                objData1[0] = "";
                objData1[1] = "";
                objData1[2] = dtmFirstPrintTime;
                objData1[3] = "����Ժ���:";
                m_objLine1Arr[1].m_ObjPrintLineInfo = objData1;
                ///////////////3��/////////////////
                objData1[0] = "";
                objData1[1] = "";
                objData1[3] = "����Ժ���:";
                m_objLine1Arr[2].m_ObjPrintLineInfo = objData1;
                ///////////////4��/////////////////
                //objData1[0] = "";
                //objData1[1] = "";
                //objData1[3] = "  ��Ժ���2:";
                //m_objLine1Arr[3].m_ObjPrintLineInfo = objData1;
                ///////////////4��/////////////////
                objData1[0] = "";
                objData1[1] = "";
                objData1[3] = "�����ƾ���:";
                m_objLine1Arr[3].m_ObjPrintLineInfo = objData1;
                ///////////////5��/////////////////
                objData1[0] = "";
                objData1[1] = "";
                objData1[3] = "����Ժ���:";
                m_objLine1Arr[4].m_ObjPrintLineInfo = objData1;
                ///////////////6��/////////////////
                objData1[0] = "";
                objData1[1] = "";
                objData1[3] = "����Ժ���:";
                m_objLine1Arr[5].m_ObjPrintLineInfo = objData1;
                ///////////////8��/////////////////			
                //objData1[0] = "";
                //objData1[1] = "";
                //objData1[3] = "����Ժ���2:";
                //m_objLine1Arr[7].m_ObjPrintLineInfo = objData1;
                ///////////////7��/////////////////			
                objData1[0] = "";
                objData1[1] = "";
                objData1[3] = "����Ժҽ��:";
                m_objLine1Arr[6].m_ObjPrintLineInfo = objData1;
                ///////////////10��/////////////////			
                //objData1[0] = "";
                //objData1[1] = "";
                //objData1[3] = "  ��Ժҽ��2:";
                //m_objLine1Arr[9].m_ObjPrintLineInfo = objData1;
            }

            #endregion
        }
        /// <summary>
        /// First page fixed info.
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintBasicInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {
            string m_strTemp;

            e.Graphics.DrawString("����:" + m_objPrintInfo.m_strNative, m_fotSmallFont, m_slbBrush, 60, 130);
            e.Graphics.DrawString("����:" + m_objPrintInfo.m_strFolk, m_fotSmallFont, m_slbBrush, 400, 130);
            e.Graphics.DrawString("���:" + m_objPrintInfo.m_strIsMarried, m_fotSmallFont, m_slbBrush, 600, 130);

            //2��
            e.Graphics.DrawString("ְҵ:" + m_objPrintInfo.m_strOccupation, m_fotSmallFont, m_slbBrush, 60, 150);
            e.Graphics.DrawString("��λ:" + m_objPrintInfo.m_strCompany, m_fotSmallFont, m_slbBrush, 240, 150);
            e.Graphics.DrawString("��λ�绰:" + m_objPrintInfo.m_strCompanyPhone, m_fotSmallFont, m_slbBrush, 600, 150);

            //3��
            e.Graphics.DrawString("סַ:" + m_objPrintInfo.m_strAddress, m_fotSmallFont, m_slbBrush, 60, 170);
            e.Graphics.DrawString("��ͥ�绰:" + m_objPrintInfo.m_strAddressPhone, m_fotSmallFont, m_slbBrush, 600, 170);

            //4��
            if (m_objRecordContentOutIn24 != null)
            {
                e.Graphics.DrawString("��ʷ������:" + m_objRecordContentOutIn24.m_strREPRESENTOR, m_fotSmallFont, m_slbBrush, 60, 190);
            }
            else
            {
                e.Graphics.DrawString("��ʷ������:", m_fotSmallFont, m_slbBrush, 60, 190);
            }
            e.Graphics.DrawString("���֤����:" + m_objPrintInfo.m_strIDCard, m_fotSmallFont, m_slbBrush, 440, 190);


            if (m_objPrintInfo.m_dtmInPatientDate != DateTime.MinValue)
            {
                m_strTemp = "��Ժ����: " + m_objPrintInfo.m_dtmHISInDate.ToString("yyyy��MM��dd��HHʱmm��");
            }
            else
            {
                m_strTemp = "��Ժ����:     ��  ��  ��  ʱ  ��"; ;
            }
            e.Graphics.DrawString(m_strTemp, m_fotSmallFont, m_slbBrush, 60, 210);
            DateTime m_dtmOutHospitalDate = new DateTime(1900, 1, 1);
            if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO == "450101001")
                m_mthGetSetlectedOutDate(out m_dtmOutHospitalDate);
            else
                if (m_objRecordContentOutIn24 != null && m_objRecordContentOutIn24.m_dtmOutHospital24Hours != null)
                m_dtmOutHospitalDate = m_objRecordContentOutIn24.m_dtmOutHospital24Hours;
            if (m_dtmOutHospitalDate != DateTime.MinValue && m_dtmOutHospitalDate != new DateTime(1900, 1, 1))
            {
                m_strTemp = "��Ժ����: " + m_dtmOutHospitalDate.ToString("yyyy��MM��dd��HHʱmm��");
            }
            else
            {
                m_strTemp = "��Ժ����:     ��  ��  ��  ʱ  ��";
            }
            e.Graphics.DrawString(m_strTemp, m_fotSmallFont, m_slbBrush, 350, 210);

        }
        #region �������ֲ���
        /// <summary>
        /// �������ֲ���
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_HospitalName));

            e.Graphics.DrawString("��Ժ����Сʱ�ڳ�Ժ��¼", m_fotTitleFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_Name_Title));


            // e.Graphics.DrawString(m_objPrintInfo.m_strDeptName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Department));
            //
            e.Graphics.DrawString("����:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name_Title));

            e.Graphics.DrawString(m_objPrintInfo.m_strPatientName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name));

            e.Graphics.DrawString("�Ա�:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex_Title));

            e.Graphics.DrawString(m_objPrintInfo.m_strSex, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex));

            e.Graphics.DrawString("����:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age_Title));

            e.Graphics.DrawString(m_objPrintInfo.m_strAge, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age));

            e.Graphics.DrawString("����:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Area_Title));

            e.Graphics.DrawString(m_objPrintInfo.m_strAreaName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Area));

            e.Graphics.DrawString("����:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Bed_Title));

            e.Graphics.DrawString(m_objPrintInfo.m_strBedName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Bed));

            e.Graphics.DrawString("סԺ��:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID_Title));

            e.Graphics.DrawString(m_objPrintInfo.m_strInPatentID, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID));

            // e.Graphics.DrawString(m_objPrintInfo.m_strOccupation, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Occupation));

            //e.Graphics.DrawString("����:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Native_Title));

            //e.Graphics.DrawString(m_objPrintInfo.m_strNative, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Native));

            //e.Graphics.DrawString("����:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Folk_Title));

            //e.Graphics.DrawString(m_objPrintInfo.m_strFolk, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Folk));

            //e.Graphics.DrawString("���:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.IsMarried_Title));

            //e.Graphics.DrawString(m_objPrintInfo.m_strIsMarried, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.IsMarried));

            //e.Graphics.DrawLine(m_LinePen, (int)enmRectangleInfo.LeftX, 177, (int)enmRectangleInfo.RightX, 177);

            //e.Graphics.DrawString("ְҵ:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Occupation_Title));


            //e.Graphics.DrawLine(m_LinePen, 40, 115, 120, 115);
        }
        #endregion
        #region  ��ȡ���˳�Ժʱ�䣬��ʱ���ڸ��������ѯ
        /// <summary>
        /// ��ȡ���˳�Ժʱ�䣬��ʱ���ڸ��������ѯ
        /// </summary>
        /// <returns></returns>
        private long m_mthGetSetlectedOutDate(out DateTime m_dtmOutHospitalDate)
        {
            m_dtmOutHospitalDate = new DateTime(1900, 1, 1);
            string strRegisterID = "";
            long lngRes = 0;
            //clsPatientManagerService objServ =
            //    (clsPatientManagerService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPatientManagerService));


            lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetRegisterIDByPatient(m_objPrintInfo.m_strInPatentID, m_objPrintInfo.m_dtmHISInDate.ToString("yyyy-MM-dd HH:mm:ss"), out strRegisterID);

            lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetOutHospitalDate(strRegisterID, out m_dtmOutHospitalDate);
            //objServ = null;
            return lngRes;
        }
        #endregion
        #region print class

        private class clsPrintLine1 : com.digitalwave.Utility.Controls.clsPrintLineBase
        {
            private clsPrintRichTextContext m_objDiagnose;
            private DateTime dtmFirstPrint;
            private string m_strTitle;

            /// <summary>
            /// ����
            /// </summary>
            private const int c_intTop1 = 270;
            /// <summary>
            /// ��Ժ���
            /// </summary>
            private const int c_intTop2 = 340;
            /// <summary>
            /// ��Ժ���1
            /// </summary>
            private const int c_intTop3 = 400;
            /// <summary>
            /// ��Ժ���2
            /// </summary>
            private const int c_intTop4 = 450;
            /// <summary>
            /// ���ƾ���
            /// </summary>
            private const int c_intTop5 = 520;
            /// <summary>
            /// ��Ժ���
            /// </summary>
            private const int c_intTop6 = 590;
            /// <summary>
            /// ��Ժ���1
            /// </summary>
            private const int c_intTop7 = 670;
            /// <summary>
            /// ��Ժ���2
            /// </summary>
            private const int c_intTop8 = 670;
            /// <summary>
            /// ��Ժҽ��1
            /// </summary>
            private const int c_intTop9 = 750;
            /// <summary>
            /// ��Ժҽ��2
            /// </summary>
            private const int c_intTop10 = 800;
            /// <summary>
            /// һ�еĸ߶�
            /// </summary>
            private const int c_intOneRowHeight = 25;

            public clsPrintLine1()
            {
                m_objDiagnose = new clsPrintRichTextContext(Color.Black, new Font("SimSun", 12));
            }

            /// <summary>
            /// ���ӳ�������ƫ����
            /// </summary>
            private static int s_intHeightMargin;
            /// <summary>
            /// �ݴ��ƫ����
            /// </summary>
            private static int s_intMarginTemp = 0;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                Font fntTitle = new Font("SimSun", 11);
                Pen m_Line1Pen = new Pen(Color.Gray, 0.5f);

                //p_objGrp.DrawString(m_strTitle, fntTitle, Brushes.Black, (int)enmRectangleInfo.LeftX, p_intPosY + 5);
                p_objGrp.DrawString(m_strTitle, fntTitle, Brushes.Black, (int)enmRectangleInfo.LeftX, p_intPosY + 5);
                Rectangle rtgDianose = new Rectangle(
                    (int)enmRectangleInfo.LeftX,
                    p_intPosY,
                    0,
                    0);
                bool blnMiddle = true;

                int intSwitchNumber = 0;
                // ԭ���̶��ĸ߶�
                int intPegPosY = 0;

                switch (m_strTitle.Trim())
                {
                    case "����:":
                        rtgDianose.X += (int)enmRectangleInfo.LeftX;
                        rtgDianose.X += 23;
                        rtgDianose.Y += 5;
                        rtgDianose.Width = (int)enmRectangleInfo.RightX - rtgDianose.X - 18;
                        blnMiddle = false;
                        if ((rtgDianose.Height = ((c_intTop2 - c_intTop1 - 40) - s_intHeightMargin)) < c_intOneRowHeight)
                        {
                            if (rtgDianose.Height < 0)
                                s_intHeightMargin = Math.Abs(rtgDianose.Height);
                            rtgDianose.Height = c_intOneRowHeight;
                        }
                        intPegPosY = c_intTop2;
                        break;
                    case "��Ժ���:":
                        rtgDianose.X += (int)enmRectangleInfo.LeftX;
                        rtgDianose.X += 50;
                        rtgDianose.Y += 5;
                        rtgDianose.Width = (int)enmRectangleInfo.RightX - rtgDianose.X - 45;
                        blnMiddle = false;
                        if ((rtgDianose.Height = ((c_intTop3 - c_intTop2 - 40) - s_intHeightMargin)) < c_intOneRowHeight)
                        {
                            if (rtgDianose.Height < 0)
                                s_intHeightMargin = Math.Abs(rtgDianose.Height);
                            rtgDianose.Height = c_intOneRowHeight;
                        }
                        intPegPosY = c_intTop3;
                        break;
                    case "��Ժ���:":
                        rtgDianose.X += (int)enmRectangleInfo.LeftX;
                        rtgDianose.X += 55;
                        rtgDianose.Y += 5;
                        rtgDianose.Width = (int)enmRectangleInfo.RightX - rtgDianose.X - 50;
                        blnMiddle = false;
                        if ((rtgDianose.Height = ((c_intTop5 - c_intTop3 - 40) - s_intHeightMargin)) < c_intOneRowHeight)
                        {
                            if (rtgDianose.Height < 0)
                                s_intHeightMargin = Math.Abs(rtgDianose.Height);
                            rtgDianose.Height = c_intOneRowHeight;
                        }
                        intPegPosY = c_intTop5;
                        break;
                    //case "��Ժ���2:":
                    //    rtgDianose.X += (int)enmRectangleInfo.LeftX;
                    //    rtgDianose.X += 55;
                    //    rtgDianose.Y += 5;
                    //    rtgDianose.Width = (int)enmRectangleInfo.RightX - rtgDianose.X - 50;
                    //    blnMiddle = false;
                    //    if ((rtgDianose.Height = ((c_intTop5 - c_intTop4 - 40) - s_intHeightMargin)) < c_intOneRowHeight)
                    //    {
                    //        if (rtgDianose.Height < 0)
                    //            s_intHeightMargin = Math.Abs(rtgDianose.Height);
                    //        rtgDianose.Height = c_intOneRowHeight;
                    //    }
                    //    intPegPosY = c_intTop5;
                    //    break;
                    case "���ƾ���:":
                        rtgDianose.X += (int)enmRectangleInfo.LeftX;
                        rtgDianose.X += 50;
                        rtgDianose.Y += 5;
                        rtgDianose.Width = (int)enmRectangleInfo.RightX - rtgDianose.X - 45;
                        blnMiddle = false;
                        if ((rtgDianose.Height = ((c_intTop6 - c_intTop5 - 40) - s_intHeightMargin)) < c_intOneRowHeight)
                        {
                            if (rtgDianose.Height < 0)
                                s_intHeightMargin = Math.Abs(rtgDianose.Height);
                            rtgDianose.Height = c_intOneRowHeight;
                        }
                        intPegPosY = c_intTop6;
                        break;
                    case "��Ժ���:":
                        rtgDianose.X += (int)enmRectangleInfo.LeftX;
                        rtgDianose.X += 50;
                        rtgDianose.Y += 5;
                        rtgDianose.Width = (int)enmRectangleInfo.RightX - rtgDianose.X - 45;
                        blnMiddle = false;
                        if ((rtgDianose.Height = ((c_intTop7 - c_intTop6 - 40) - s_intHeightMargin)) < c_intOneRowHeight)
                        {
                            if (rtgDianose.Height < 0)
                                s_intHeightMargin = Math.Abs(rtgDianose.Height);
                            rtgDianose.Height = c_intOneRowHeight;
                        }
                        intPegPosY = c_intTop7;
                        break;
                    case "��Ժ���:":
                        rtgDianose.X += (int)enmRectangleInfo.LeftX;
                        rtgDianose.X += 55;
                        rtgDianose.Y += 5;
                        rtgDianose.Width = (int)enmRectangleInfo.RightX - rtgDianose.X - 50;
                        blnMiddle = false;
                        if ((rtgDianose.Height = ((c_intTop9 - c_intTop7 - 40) - s_intHeightMargin)) < c_intOneRowHeight)
                        {
                            if (rtgDianose.Height < 0)
                                s_intHeightMargin = Math.Abs(rtgDianose.Height);
                            rtgDianose.Height = c_intOneRowHeight;
                        }
                        intPegPosY = c_intTop9;
                        break;
                    //case "��Ժ���2:":
                    //    rtgDianose.X += (int)enmRectangleInfo.LeftX;
                    //    rtgDianose.X += 55;
                    //    rtgDianose.Y += 5;
                    //    rtgDianose.Width = (int)enmRectangleInfo.RightX - rtgDianose.X - 50;
                    //    blnMiddle = false;
                    //    if ((rtgDianose.Height = ((c_intTop8 - c_intTop7 - 40) - s_intHeightMargin)) < c_intOneRowHeight)
                    //    {
                    //        if (rtgDianose.Height < 0)
                    //            s_intHeightMargin = Math.Abs(rtgDianose.Height);
                    //        rtgDianose.Height = c_intOneRowHeight;
                    //    }
                    //    intPegPosY = c_intTop9;
                    //    break;

                    case "��Ժҽ��:":
                        rtgDianose.X += (int)enmRectangleInfo.LeftX;
                        rtgDianose.X += 55;
                        rtgDianose.Y += 5;
                        rtgDianose.Width = (int)enmRectangleInfo.RightX - rtgDianose.X - 50;
                        blnMiddle = false;
                        if ((rtgDianose.Height = ((c_intTop10 - c_intTop9 - 40) - s_intHeightMargin)) < c_intOneRowHeight)
                        {
                            if (rtgDianose.Height < 0)
                                s_intHeightMargin = Math.Abs(rtgDianose.Height);
                            rtgDianose.Height = c_intOneRowHeight;
                        }
                        intPegPosY = c_intTop10;
                        break;
                    //case "��Ժҽ��2:":
                    //    rtgDianose.X += (int)enmRectangleInfo.LeftX;
                    //    rtgDianose.X += 55;
                    //    rtgDianose.Y += 5;
                    //    rtgDianose.Width = (int)enmRectangleInfo.RightX - rtgDianose.X - 50;
                    //    blnMiddle = false;
                    //    if ((rtgDianose.Height = ((5 * (int)enmRectangleInfo.RowStep - 40) - s_intHeightMargin)) < c_intOneRowHeight)
                    //    {
                    //        if (rtgDianose.Height < 0)
                    //            s_intHeightMargin = Math.Abs(rtgDianose.Height);
                    //        rtgDianose.Height = c_intOneRowHeight;
                    //    }
                    //    intPegPosY = c_intTop10 + 6 * (int)enmRectangleInfo.RowStep;
                    //    break;
                    default:
                        intSwitchNumber = 3;
                        rtgDianose.X += (int)enmRectangleInfo.LeftX + 50;
                        rtgDianose.Y += 3;
                        rtgDianose.Width = (int)enmRectangleInfo.RightX - rtgDianose.X;
                        blnMiddle = false;
                        rtgDianose.Height = c_intOneRowHeight;
                        intPegPosY = c_intTop1;

                        break;
                }
                int intRealHeight;
                m_objDiagnose.m_blnPrintAllBySimSun(11, rtgDianose, p_objGrp, out intRealHeight, blnMiddle);

                if (intRealHeight > rtgDianose.Height)
                {
                    p_intPosY += intRealHeight + 5;
                }
                else
                {
                    p_intPosY += rtgDianose.Height;
                }
                if (intSwitchNumber == 0) p_intPosY += 27;

                if (p_intPosY > intPegPosY)
                    s_intHeightMargin += p_intPosY - intPegPosY;
                else
                    s_intHeightMargin = 0;

                if (intSwitchNumber == 3)
                {
                    if (s_intHeightMargin < s_intMarginTemp)
                    {
                        p_intPosY += s_intMarginTemp - s_intHeightMargin;
                        s_intHeightMargin = s_intMarginTemp;
                    }
                }

                fntTitle.Dispose();

                m_blnHaveMoreLine = false;
            }

            public override void m_mthReset()
            {
                m_blnHaveMoreLine = true;
                m_objDiagnose.m_mthRestartPrint();
            }

            public override object m_ObjPrintLineInfo
            {
                get
                {
                    return m_objPrintLineInfo;
                }
                set
                {
                    if (value != null)
                    {
                        Object[] objData = (object[])value;
                        m_strTitle = objData[3].ToString();
                        dtmFirstPrint = (DateTime)objData[2];
                        if (objData[1].ToString() == "")
                        {
                            m_objDiagnose.m_mthSetContextWithCorrectBefore(objData[0].ToString(), objData[1].ToString(), dtmFirstPrint);
                        }
                        else
                        {
                            m_objDiagnose.m_mthSetContextWithCorrectBefore(objData[0].ToString(), objData[1].ToString(), dtmFirstPrint, true);
                            m_mthAddSign2(m_strTitle.Trim(), m_objDiagnose.m_ObjModifyUserArr);
                        }
                        if (m_objDiagnose.m_ObjModifyUserArr != null)
                            for (int i = 0; i < m_objDiagnose.m_ObjModifyUserArr.Length; i++)
                            {
                                if (m_objDiagnose.m_ObjModifyUserArr[i].m_clrText.ToArgb() == Color.White.ToArgb())
                                    m_objDiagnose.m_ObjModifyUserArr[i].m_clrText = Color.Black;
                            }
                    }
                }
            }
        }
        #endregion

    }
}
