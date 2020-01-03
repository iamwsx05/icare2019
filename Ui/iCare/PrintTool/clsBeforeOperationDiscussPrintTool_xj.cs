using System;
using iCareData;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;

namespace iCare
{
    /// <summary>
    /// ��ǰ���ۼ�¼---�½��Ĵ�ӡ������
    /// </summary>
    public class clsBeforeOperationDiscussPrintTool_xj : infPrintRecord
    {
        private bool m_blnIsFromDataSource = true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
        private bool m_blnWantInit = true;
        /// <summary>
        /// �Ƿ��ӡ�޸ĺۼ�
        /// </summary>
        public static bool m_blnIsPrintMark = true;
        private clsDiseaseTrackDomain m_objRecordsDomain;
        private clsPrintInfo_BeforeOperationDiscuss_xj m_objPrintInfo;
        private clsBeforeOperationDiscussContent_xj m_objRecordContent = null;
        private DateTime m_dtmOutDate = DateTime.MinValue;

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
            m_objPrintInfo = new clsPrintInfo_BeforeOperationDiscuss_xj();
            m_objPrintInfo.m_strInPatentID = m_objPatient != null ? m_objPatient.m_StrInPatientID : "";
            m_objPrintInfo.m_strPatientName = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrFirstName : "";
            m_objPrintInfo.m_strSex = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrSex : "";
            m_objPrintInfo.m_strAge = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrAge : "";
            m_objPrintInfo.m_strBedName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName : "";
            m_objPrintInfo.m_strDeptName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjDept.m_StrDeptName : "";
            m_objPrintInfo.m_strAreaName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName : "";
            m_objPrintInfo.m_dtmInPatientDate = p_dtmInPatientDate;
            m_objPrintInfo.m_dtmOpenDate = p_dtmOpenDate;
            m_objPrintInfo.m_dtmHISInDate = m_objPatient != null ? m_objPatient.m_DtmSelectedHISInDate : DateTime.MinValue;
            m_objPrintInfo.m_strHISInPatientID = m_objPatient != null ? m_objPatient.m_StrHISInPatientID : "";

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
            m_blnWantInit = false;//
            if (m_objPrintInfo == null)
            {
                clsPublicFunction.ShowInformationMessageBox("����m_mthInitPrintContent֮ǰ�����ȵ���m_mthSetPrintInfo����");
                return;
            }
            if (m_objPrintInfo.m_strInPatentID == "" || m_objPrintInfo.m_dtmOpenDate == DateTime.MinValue)
                m_objRecordContent = null;
            else
            {
                m_objRecordsDomain = new clsDiseaseTrackDomain(enmDiseaseTrackType.BeforeOperationDiscuss_xj);
                clsTrackRecordContent objContent = new clsTrackRecordContent();
                long lngRes = m_objRecordsDomain.m_lngGetRecordContent(m_objPrintInfo.m_strInPatentID, m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"), out objContent);
                if (lngRes <= 0)
                    return;
                m_objRecordContent = (clsBeforeOperationDiscussContent_xj)objContent;
            }
            //if (m_objRecordContent != null)
            //    m_objRecordContent.m_dtmOutHospitalDate = m_dtmOutDate;
            //���ñ����ݵ���ӡ��			
            m_objPrintInfo.m_objRecordContent = m_objRecordContent;
            m_mthSetPrintValue();//�����з��ӡ����,��ʹ�ڴ�ӡ�հױ�ʱ,����Ҳ����ִ��.			
        }

        public void m_mthSetOutDateValue(DateTime p_dtmOutDate)
        {
            m_dtmOutDate = p_dtmOutDate;
        }

        /// <summary>
        /// ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
        /// </summary>
        /// <param name="p_objPrintContent">��ӡ����</param>
        public void m_mthSetPrintContent(object p_objPrintContent)
        {


            m_blnWantInit = false;
            if (p_objPrintContent.GetType().Name != "clsPrintInfo_BeforeOperationDiscuss_xj")
            {
                clsPublicFunction.ShowInformationMessageBox("��������");
                return;
            }
            m_blnIsFromDataSource = false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ
            m_objPrintInfo = (clsPrintInfo_BeforeOperationDiscuss_xj)p_objPrintContent;
            m_objRecordContent = m_objPrintInfo.m_objRecordContent;
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
                m_objRecordsDomain.m_lngUpdateFirstPrintDate(m_objPrintInfo.m_strInPatentID, m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"), dtmFirstPrintTime);//�����Ҹ�m_objPrintInfo.m_objRecordContent.m_dtmFirstPrintDate);	
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
                m_intYPos += (int)enmRectangleInfo.RowStep - 20;
            }

            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, (int)enmRectangleInfo.TopY, (int)enmRectangleInfo.RightX, (int)enmRectangleInfo.TopY);

            while (m_objPrintContext.m_BlnHaveMoreLine)
            {
                m_objPrintContext.m_mthPrintNextLine(ref m_intYPos, e.Graphics, fntNormal);

                if (m_intYPos >= (int)enmRectangleInfo.BottomY
                    && m_objPrintContext.m_BlnHaveMoreLine)
                {
                    #region ��ҳ����
                    e.HasMorePages = true;

                    e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, (int)enmRectangleInfo.TopY, (int)enmRectangleInfo.LeftX, m_intYPos);
                    e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.RightX, (int)enmRectangleInfo.TopY, (int)enmRectangleInfo.RightX, m_intYPos);
                    e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, m_intYPos, (int)enmRectangleInfo.RightX, m_intYPos);

                    m_intPages++;
                    m_intYPos = (int)enmRectangleInfo.TopY + 10;

                    clsPrintLine2.m_blnSinglePage = false;
                    return;

                    #endregion ��ҳ����
                }

            }

            #region ���һҳ����
            m_intYPos += 30;

            //string strRecordName = "                 ";
            //string strGuanChangName = "";
            //string strZhuRenName = "";
            //if (m_objRecordContent != null)
            //{
            //    com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
            //    clsEmrEmployeeBase_VO objEmpVO = null;
            //    objEmployeeSign.m_lngGetEmpByNO(m_objRecordContent.m_strRecordID, out objEmpVO);
            //    if (objEmpVO != null)
            //        strRecordName = objEmpVO.ToString();
            //    objEmployeeSign.m_lngGetEmpByNO(m_objRecordContent.m_strGuanChuangID, out objEmpVO);
            //    if (objEmpVO != null)
            //        strGuanChangName = objEmpVO.ToString();
            //    objEmployeeSign.m_lngGetEmpByNO(m_objRecordContent.m_strZhuRenID, out objEmpVO);
            //    if (objEmpVO != null)
            //        strZhuRenName = objEmpVO.ToString();
            //}
            //e.Graphics.DrawString("��¼��:" + strRecordName + " �ܴ�ҽʦ:" + strGuanChangName + " ������:" + strZhuRenName + " " + m_objRecordContent.m_dtmDiscussDate.ToString("yyyy-MM-dd"), new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX, m_intYPos);
            e.Graphics.DrawString("��¼����:" + m_objRecordContent.m_dtmDiscussDate.ToString("yyyy-MM-dd"), new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX, m_intYPos);
            //			m_intYPos+=25;
            //			e.Graphics.DrawString("��������:",new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX+560,m_intYPos);
            //			if(m_objRecordContent!=null)
            //				e.Graphics.DrawString(m_objRecordContent.m_strDoctorID,new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX+560+(int)(5f*17.5f),m_intYPos);
            /////////////////////////////////*******************************************
            m_intYPos += 50;
            if (m_intYPos < (int)enmRectangleInfo.BottomY)
                m_intYPos = (int)enmRectangleInfo.BottomY;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, (int)enmRectangleInfo.TopY, (int)enmRectangleInfo.LeftX, m_intYPos);
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.RightX, (int)enmRectangleInfo.TopY, (int)enmRectangleInfo.RightX, m_intYPos);
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
            clsPrintLine2.m_blnSinglePage = true;
        }


        #region ��ӡ
        #region �йش�ӡ������

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
        private int m_intYPos = (int)enmRectangleInfo.TopY + 5;
        private int m_intPages = 1;

        /// <summary>
        /// ���ӵ���Ϣ
        /// </summary>
        public enum enmRectangleInfo
        {
            /// <summary>
            /// ���ӵĶ���TopY = 120,
            /// </summary>
            TopY = 160,
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

            BottomY = 1024,

            PrintWidth = 670,
            PrintWidth2 = 710
        }

        /// <summary>
        /// ��ӡԪ��
        /// </summary>
        private enum enmItemDefination
        {
            //����Ԫ��
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
            YiBao_title,
            YiBao,
            //HeartID_Title,
            //HeartID,
            //XRayID_Title,
            //XRayID,

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
                    case (int)enmItemDefination.Area:
                        m_fReturnPoint = new PointF(370f, 140f);
                        break;
                    case (int)enmItemDefination.Area_Title:
                        m_fReturnPoint = new PointF(330f, 140f);
                        break;
                    case (int)enmItemDefination.Bed:
                        m_fReturnPoint = new PointF(510f, 137f);
                        break;
                    case (int)enmItemDefination.Bed_Title:
                        m_fReturnPoint = new PointF(470f, 140f);
                        break;
                    case (int)enmItemDefination.Page_HospitalName:
                        m_fReturnPoint = new PointF(320f, 70f);
                        break;
                    case (int)enmItemDefination.Page_Name_Title:
                        m_fReturnPoint = new PointF(290f, 100f);
                        break;
                    case (int)enmItemDefination.Name_Title:
                        m_fReturnPoint = new PointF(30f, 140f);
                        break;
                    case (int)enmItemDefination.Name:
                        m_fReturnPoint = new PointF(75f, 140f);
                        break;

                    case (int)enmItemDefination.Sex_Title:
                        m_fReturnPoint = new PointF(155f, 140f);
                        break;
                    case (int)enmItemDefination.Sex:
                        m_fReturnPoint = new PointF(200f, 140f);
                        break;

                    case (int)enmItemDefination.Age_Title:
                        m_fReturnPoint = new PointF(225f, 140f);
                        break;
                    case (int)enmItemDefination.Age:
                        m_fReturnPoint = new PointF(270f, 140f);
                        break;

                    case (int)enmItemDefination.InPatientID_Title:
                        m_fReturnPoint = new PointF(540f, 140f);
                        break;
                    case (int)enmItemDefination.InPatientID:
                        m_fReturnPoint = new PointF(600f, 140f);
                        break;

                    case (int)enmItemDefination.YiBao_title:
                        m_fReturnPoint = new PointF(645f, 140f);
                        break;
                    case (int)enmItemDefination.YiBao:
                        m_fReturnPoint = new PointF(700f, 140f);
                        break;

                    //case (int)enmItemDefination.HeartID_Title:
                    //    m_fReturnPoint = new PointF(60f, 155f);
                    //    break;
                    //case (int)enmItemDefination.HeartID:
                    //    m_fReturnPoint = new PointF(145f, 153f);
                    //    break;

                    //case (int)enmItemDefination.XRayID_Title:
                    //    m_fReturnPoint = new PointF(220f, 155f);
                    //    break;
                    //case (int)enmItemDefination.XRayID:
                    //    m_fReturnPoint = new PointF(280f, 153f);
                    //    break;

                    default:
                        m_fReturnPoint = new PointF(400f, 440f);
                        break;

                }

                return m_fReturnPoint;
            }
        }

        #endregion
        #endregion

        #region ��ӡ�ж���
        private clsPrintLine1[] m_objLine1Arr;
        private clsPrintLine2[] m_objLine2Arr;
        #endregion

        private DateTime dtmFirstPrintTime;
        /// <summary>
        /// ��ÿһ��ӡ�е�Ԫ�ظ�ֵ
        /// </summary>
        private void m_mthSetPrintValue()
        {
            #region  ��һ�δ�ӡʱ�丳ֵ
            dtmFirstPrintTime = DateTime.Now;
            if (m_objRecordContent != null && m_objRecordContent.m_dtmFirstPrintDate != DateTime.MinValue)
                dtmFirstPrintTime = m_objRecordContent.m_dtmFirstPrintDate;
            #endregion  ��һ�δ�ӡʱ�丳ֵ

            #region ��ӡ�г�ʼ��
            m_objLine1Arr = new clsPrintLine1[5];
            m_objLine2Arr = new clsPrintLine2[3];
            for (int i = 0; i < m_objLine1Arr.Length; i++)
                m_objLine1Arr[i] = new clsPrintLine1();


            m_objLine2Arr[0] = new clsPrintLine2(20);
            m_objLine2Arr[1] = new clsPrintLine2(360);
            m_objLine2Arr[2] = new clsPrintLine2(420);
            //     m_objLine2Arr[3] = new clsPrintLine2(790);
            m_objPrintContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
										 m_objLine1Arr[2],m_objLine1Arr[3], m_objLine1Arr[4],m_objLine1Arr[1],
                                         m_objLine2Arr[0], m_objLine2Arr[1],m_objLine2Arr[2]
									  });
            m_objPrintContext.m_ObjPrintSign = new com.digitalwave.Utility.Controls.clsPrintRecordSign();
            #endregion

            #region ��ÿһ�е�Ԫ�ظ�ֵ
            string strBlanks = "������������������������";
            if (m_objRecordContent != null)
            {
                ///////////////1��/////////////////
                string strOutDate = "";
                //if (m_objRecordContent.m_dtmOutHospitalDate != DateTime.MinValue
                //    && m_objRecordContent.m_dtmOutHospitalDate != new DateTime(1900, 1, 1))
                //    strOutDate = m_objRecordContent.m_dtmOutHospitalDate.ToString("yyyy��MM��dd��");
                Object[] objData1 = new object[5];
                objData1[0] = m_objRecordContent.m_strNiZheng;
                objData1[1] = m_objRecordContent.m_strNiZhengXML;
                objData1[2] = dtmFirstPrintTime;
                objData1[3] = "��ǰ׼�����: "; //+ m_objPrintInfo.m_dtmHISInDate.ToString("yyyy��MM��dd��") + "����������������������" + "��Ժ����: " + strOutDate;
                m_objLine1Arr[1].m_ObjPrintLineInfo = objData1;

                ///////////////2��/////////////////
                objData1[0] = "    ";
                foreach (string str in m_objRecordContent.m_strAttendeeIDArr)
                {
                    com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
                    clsEmrEmployeeBase_VO objEmpVO = null;
                    objEmployeeSign.m_lngGetEmpByNO(str, out objEmpVO);
                    //if (objEmpVO != null)
                    //    if (!(string.IsNullOrEmpty(objEmpVO.m_strGetTechnicalRankAndName)))
                    //        e.Graphics.DrawString(objEmpVO.m_strGetTechnicalRankAndName, new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 500 + (int)(5f * 15.5f), m_intYPos);

                    objData1[0] += objEmpVO.m_strGetTechnicalRankAndName + "��";
                }
                objData1[1] = "";
                objData1[2] = dtmFirstPrintTime;
                objData1[3] = "�μ���Ա��";
                m_objLine1Arr[2].m_ObjPrintLineInfo = objData1;
                //////////////////////////////
                objData1[0] = "    ";
                com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign1 = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
                clsEmrEmployeeBase_VO objEmpVO1 = null;
                objEmployeeSign1.m_lngGetEmpByNO(m_objRecordContent.m_strCompereID, out objEmpVO1);
                //if (objEmpVO != null)
                //    if (!(string.IsNullOrEmpty(objEmpVO.m_strGetTechnicalRankAndName)))
                //        e.Graphics.DrawString(objEmpVO.m_strGetTechnicalRankAndName, new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 500 + (int)(5f * 15.5f), m_intYPos);

                objData1[0] += objEmpVO1.m_strGetTechnicalRankAndName + "��";
                objData1[1] = "";
                objData1[2] = dtmFirstPrintTime;
                objData1[3] = "�����ˣ�";
                m_objLine1Arr[3].m_ObjPrintLineInfo = objData1;
                //////////////////////////////
                objData1[0] = "    ";
                com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign2 = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
                clsEmrEmployeeBase_VO objEmpVO2 = null;
                objEmployeeSign2.m_lngGetEmpByNO(m_objRecordContent.m_strHuiBaoID, out objEmpVO2);
                //if (objEmpVO != null)
                //    if (!(string.IsNullOrEmpty(objEmpVO.m_strGetTechnicalRankAndName)))
                //        e.Graphics.DrawString(objEmpVO.m_strGetTechnicalRankAndName, new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 500 + (int)(5f * 15.5f), m_intYPos);

                objData1[0] += objEmpVO2.m_strGetTechnicalRankAndName + "��";
                objData1[1] = "";
                objData1[2] = dtmFirstPrintTime;
                objData1[3] = "��ʷ�㱨�ߣ�";
                m_objLine1Arr[4].m_ObjPrintLineInfo = objData1;


                /////////////////3��/////////////////
                //objData1[0] = m_objRecordContent.m_strOutHospitalDiagnose;
                //objData1[1] = m_objRecordContent.m_strOutHospitalDiagnoseXML;
                //objData1[3] = "��ҽ��Ժ���:";
                //m_objLine1Arr[4].m_ObjPrintLineInfo = objData1;
                ///////////////////////////////////////
                //objData1[0] = m_objRecordContent.m_strOutHospitalDiagnoseXi;
                //objData1[1] = m_objRecordContent.m_strOutHospitalDiagnoseXiXML;
                //objData1[3] = "��ҽ��Ժ���:";
                //m_objLine1Arr[5].m_ObjPrintLineInfo = objData1;
                /////////////////4��/////////////////
                objData1[0] = "";
                objData1[1] = "";
                objData1[2] = dtmFirstPrintTime;
                m_objLine1Arr[0].m_ObjPrintLineInfo = objData1;

                ///////////////5��/////////////////
                objData1[0] = m_objRecordContent.m_strTaoLunYiJian;
                objData1[1] = m_objRecordContent.m_strTaoLunYiJianXML;
                objData1[3] = "����ָ��������֢:";
                m_objLine2Arr[0].m_ObjPrintLineInfo = objData1;

                objData1[0] = m_objRecordContent.m_strTaoLunXiaoJie;
                objData1[1] = m_objRecordContent.m_strTaoLunXiaoJieXML;
                objData1[3] = "��������:";
                m_objLine2Arr[1].m_ObjPrintLineInfo = objData1;
                ///////////////8��/////////////////			
                objData1[0] = m_objRecordContent.m_strHuiBao;
                objData1[1] = m_objRecordContent.m_strHuiBaoXML;
                objData1[3] = "���ܳ��ֵ����⼰������ʩ:";
                m_objLine2Arr[2].m_ObjPrintLineInfo = objData1;

            }
            else
            {
                ///////////////1��/////////////////
                Object[] objData1 = new object[5];
                //objData1[0] = "";
                //objData1[1] = "";
                //objData1[2] = dtmFirstPrintTime;
                //if (m_objPrintInfo.m_dtmHISInDate != DateTime.MinValue)
                //{
                //    objData1[3] = "��Ժ����:" + m_objPrintInfo.m_dtmHISInDate.ToString("yyyy��MM��dd��") + "����������������������" + "��Ժ����:" + "    ��  ��  ��";
                //}
                //else
                //{
                //    objData1[3] = "��Ժ����:" + "    ��  ��  ��" + "����������������������" + "��Ժ����:" + "    ��  ��  ��";
                //}
                //m_objLine1Arr[1].m_ObjPrintLineInfo = objData1;

                ///////////////2��/////////////////				
                objData1[0] = "";
                objData1[1] = "";
                objData1[2] = dtmFirstPrintTime;
                objData1[3] = "��ǰ׼�����:";
                m_objLine1Arr[1].m_ObjPrintLineInfo = objData1;
                ///////////////////////////
                objData1[0] = "";
                objData1[1] = "";
                objData1[2] = dtmFirstPrintTime;
                objData1[3] = "�μ���Ա:";
                m_objLine1Arr[2].m_ObjPrintLineInfo = objData1;

                ///////////////3��/////////////////
                objData1[0] = "";
                objData1[1] = "";
                objData1[3] = "������:";
                m_objLine1Arr[3].m_ObjPrintLineInfo = objData1;
                //////////////////////////////////////
                objData1[0] = "";
                objData1[1] = "";
                objData1[3] = "��ʷ�㱨��:";
                m_objLine1Arr[4].m_ObjPrintLineInfo = objData1;
                ///////////////4��/////////////////	
                objData1[0] = "";
                objData1[1] = "";
                objData1[2] = dtmFirstPrintTime;
                //   objData1[3] = "  �ĵ�ͼ��:" + strBlanks + "X���:" + strBlanks;// +"����ҽʦ:";
                m_objLine1Arr[0].m_ObjPrintLineInfo = objData1;
                ///////////////5��/////////////////
                objData1[0] = "";
                objData1[1] = "";
                objData1[3] = "����ָ��������֢:";
                m_objLine2Arr[0].m_ObjPrintLineInfo = objData1;
                ///////////////6��/////////////////
                objData1[0] = "";
                objData1[1] = "";
                //if (clsEMRLogin.m_StrCurrentHospitalNO == "450101001")//����
                //{
                //    objData1[3] = "�����ƾ���:";
                //}
                //else
                //{
                //    objData1[3] = "�����ƾ���:(�ص��¼�����ݱ���Ҫ��ҩ�����������Ҫ����)";
                //}
                //m_objLine2Arr[1].m_ObjPrintLineInfo = objData1;
                ///////////////7��/////////////////
                objData1[0] = "";
                objData1[1] = "";
                objData1[3] = "��������:";
                m_objLine2Arr[1].m_ObjPrintLineInfo = objData1;
                ///////////////8��/////////////////			
                objData1[0] = "";
                objData1[1] = "";
                objData1[3] = "���ܳ��ֵ����⼰������ʩ:";
                m_objLine2Arr[2].m_ObjPrintLineInfo = objData1;

            }

            #endregion
        }


        #region �������ֲ���
        /// <summary>
        /// �������ֲ���
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_HospitalName));

            e.Graphics.DrawString("��ǰ���ۼ�¼", m_fotTitleFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_Name_Title));

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

            e.Graphics.DrawString(m_objPrintInfo.m_strHISInPatientID, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID));

            e.Graphics.DrawString("ҽ����:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.YiBao_title));
            e.Graphics.DrawString(m_objPrintInfo.m_objRecordContent.m_strYiBao, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.YiBao));

            //e.Graphics.DrawString("�ĵ�ͼ�ţ�", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.HeartID_Title));
            //if (m_objPrintInfo.m_objRecordContent != null)
            //    e.Graphics.DrawString(m_objPrintInfo.m_objRecordContent.m_strHeartID_Right, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.HeartID));

            //e.Graphics.DrawString("X��ţ�", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.XRayID_Title));
            //if (m_objPrintInfo.m_objRecordContent != null)
            //e.Graphics.DrawString(m_objPrintInfo.m_objRecordContent.m_strXRayID_Right, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.XRayID));	
        }


        #endregion

        #region print class

        #region
        //private class clsPrintLine1 : com.digitalwave.Utility.Controls.clsPrintLineBase
        //{
        //    private clsPrintRichTextContext m_objDiagnose;
        //    private DateTime dtmFirstPrint;
        //    //			private bool m_blnFirstPrint = true;
        //    private string m_strTitle;

        //    #region
        //    /// <summary>
        //    /// ����
        //    /// </summary>
        //    private const int c_intTop1 = 195;
        //    /// <summary>
        //    /// �����
        //    /// </summary>
        //    private const int c_intTop2 = 245;
        //    /// <summary>
        //    /// �����
        //    /// </summary>
        //    private const int c_intTop3 = 295;
        //    /// <summary>
        //    /// ��Ժ���
        //    /// </summary>
        //    private const int c_intTop4 = 345;
        //    /// <summary>
        //    /// ���ƾ���
        //    /// </summary>
        //    private const int c_intTop5 = 540;
        //    /// <summary>
        //    /// ��Ժ���
        //    /// </summary>
        //    private const int c_intTop6 = 700;
        //    /// <summary>
        //    /// ��Ժҽ��
        //    /// </summary>
        //    private const int c_intTop7 = 830;
        //    /// <summary>
        //    /// һ�еĸ߶�
        //    /// </summary>
        //    private const int c_intOneRowHeight = 30;

        //    #endregion

        //    public clsPrintLine1()
        //    {
        //        m_objDiagnose = new clsPrintRichTextContext(Color.Black, new Font("SimSun", 12));
        //    }

        //    //			private int m_intTimes = 0;

        //    /// <summary>
        //    /// ���ӳ�������ƫ����
        //    /// </summary>
        //    private static int s_intHeightMargin;
        //    /// <summary>
        //    /// �ݴ��ƫ����
        //    /// </summary>
        //    private static int s_intMarginTemp = 0;

        //    public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
        //    {
        //        Font fntTitle = new Font("SimSun", 11);

        //        //if (m_strTitle != "����Ժ���:")
        //        //{
        //            p_objGrp.DrawString(m_strTitle, fntTitle, Brushes.Black, (int)enmRectangleInfo.LeftX, p_intPosY+ 5);
        //        //}
        //        //else
        //        //{
        //        //    p_objGrp.DrawString(m_strTitle, fntTitle, Brushes.Black, (int)enmRectangleInfo.LeftX + 363, p_intPosY -30);//
        //        //}
        //        p_intPosY += 10;
        //        Rectangle rtgDianose = new Rectangle(
        //            (int)enmRectangleInfo.LeftX,
        //            p_intPosY,
        //            0,
        //            0);
        //        bool blnMiddle = true;

        //        int intSwitchNumber = 0;
        //        // ԭ���̶��ĸ߶�
        //        int intPegPosY = 0;


        //        #region
        //        switch (m_strTitle)
        //        {
        //            case "����Ժ���:":
        //                intSwitchNumber = 2;
        //                rtgDianose.X += 95;
        //                rtgDianose.Height = 80;
        //                rtgDianose.Width = (int)enmRectangleInfo.LeftX + 370 - rtgDianose.X;
        //                intPegPosY = 285;
        //                //						rtgDianose.X += 95;
        //                //						rtgDianose.Y -= 30
        //                //						rtgDianose.Height = 80;
        //                //						rtgDianose.Width = (int)enmRectangleInfo.LeftX+370-rtgDianose.X;
        //                //						p_intPosY = c_intTop2;
        //                break;
        //            case "����Ժ���:":
        //                intSwitchNumber = 3;
        //                rtgDianose.X += 452;
        //                rtgDianose.Height = 80;
        //                rtgDianose.Width = (int)enmRectangleInfo.RightX - rtgDianose.X;
        //                intPegPosY = 285;
        //                //						rtgDianose.X += 452;
        //                //						rtgDianose.Y -= 30
        //                //						rtgDianose.Height = 80;
        //                //						rtgDianose.Width = (int)enmRectangleInfo.RightX-rtgDianose.X;
        //                //						p_intPosY = c_intTop3;
        //                break;
        //            //                    case "����Ժ���:":
        //            //                        rtgDianose.X += (int)enmRectangleInfo.LeftX;
        //            //                        rtgDianose.Y += 30;
        //            //                        rtgDianose.Width = (int)enmRectangleInfo.RightX-rtgDianose.X;
        //            //                        blnMiddle = false;
        //            //                        if((rtgDianose.Height =((c_intTop5-c_intTop4-40) - s_intHeightMargin)) < c_intOneRowHeight)
        //            //                        {
        //            //                            if (rtgDianose.Height < 0)
        //            //                                s_intHeightMargin = Math.Abs(rtgDianose.Height);
        //            //                            rtgDianose.Height = c_intOneRowHeight;
        //            //                        }
        //            //                        intPegPosY = c_intTop5;
        //            ////						rtgDianose.X += (int)enmRectangleInfo.LeftX;
        //            ////						rtgDianose.Y += 30;
        //            ////						rtgDianose.Height = c_intTop5-c_intTop4-40;
        //            ////						rtgDianose.Width = (int)enmRectangleInfo.RightX-rtgDianose.X;
        //            ////						blnMiddle = false;
        //            ////						p_intPosY = c_intTop5;
        //            //                        break;
        //            //                    case "�����ƾ���:(�ص��¼�����ݱ���Ҫ��ҩ�����������Ҫ����)":
        //            //                    case "�����ƾ���:":
        //            //                        rtgDianose.X += (int)enmRectangleInfo.LeftX;
        //            //                        rtgDianose.Y += 30;
        //            //                        rtgDianose.Width = (int)enmRectangleInfo.RightX-rtgDianose.X;
        //            //                        blnMiddle = false;
        //            //                        if((rtgDianose.Height =((c_intTop6-c_intTop5-40) - s_intHeightMargin)) < c_intOneRowHeight)
        //            //                        {
        //            //                            if (rtgDianose.Height < 0)
        //            //                                s_intHeightMargin = Math.Abs(rtgDianose.Height);
        //            //                            rtgDianose.Height = c_intOneRowHeight;
        //            //                        }
        //            //                        intPegPosY = c_intTop6;
        //            ////						rtgDianose.X += (int)enmRectangleInfo.LeftX;
        //            ////						rtgDianose.Y += 30;
        //            ////						rtgDianose.Height = c_intTop6-c_intTop5-40;
        //            ////						rtgDianose.Width = (int)enmRectangleInfo.RightX-rtgDianose.X;
        //            ////						blnMiddle = false;
        //            ////						p_intPosY = c_intTop6;
        //            //                        break;
        //            //                    case "����Ժ���:":
        //            //                        rtgDianose.X += (int)enmRectangleInfo.LeftX;
        //            //                        rtgDianose.Y += 30;
        //            //                        rtgDianose.Width = (int)enmRectangleInfo.RightX-rtgDianose.X;
        //            //                        blnMiddle = false;
        //            //                        if((rtgDianose.Height =((c_intTop7-c_intTop6-40) - s_intHeightMargin)) < c_intOneRowHeight)
        //            //                        {
        //            //                            if (rtgDianose.Height < 0)
        //            //                                s_intHeightMargin = Math.Abs(rtgDianose.Height);
        //            //                            rtgDianose.Height = c_intOneRowHeight;
        //            //                        }
        //            //                        intPegPosY = c_intTop7;
        //            ////						rtgDianose.X += (int)enmRectangleInfo.LeftX;
        //            ////						rtgDianose.Y += 30;
        //            ////						rtgDianose.Height = c_intTop7-c_intTop6-40;
        //            ////						rtgDianose.Width = (int)enmRectangleInfo.RightX-rtgDianose.X;
        //            ////						blnMiddle = false;
        //            ////						p_intPosY = c_intTop7;
        //            //                        break;
        //            //                    case "����Ժҽ��:":
        //            //                        rtgDianose.X += (int)enmRectangleInfo.LeftX;
        //            //                        rtgDianose.Y += 30;
        //            //                        rtgDianose.Width = (int)enmRectangleInfo.RightX-rtgDianose.X;
        //            //                        blnMiddle = false;
        //            //                        if((rtgDianose.Height =((5*(int)enmRectangleInfo.RowStep-40) - s_intHeightMargin)) < c_intOneRowHeight)
        //            //                        {
        //            //                            if (rtgDianose.Height < 0)
        //            //                                s_intHeightMargin = Math.Abs(rtgDianose.Height);
        //            //                            rtgDianose.Height = c_intOneRowHeight;
        //            //                        }
        //            //                        intPegPosY = c_intTop7 + 5*(int)enmRectangleInfo.RowStep;
        //            ////						rtgDianose.X += (int)enmRectangleInfo.LeftX;
        //            ////						rtgDianose.Y += 30;
        //            ////						rtgDianose.Height = 5*(int)enmRectangleInfo.RowStep-40;
        //            ////						rtgDianose.Width = (int)enmRectangleInfo.RightX-rtgDianose.X;
        //            ////						blnMiddle = false;
        //            ////						p_intPosY +=5*(int)enmRectangleInfo.RowStep;
        //            //                        break;
        //            default:
        //                if (m_strTitle.StartsWith("����Ժ����:"))
        //                {
        //                    p_intPosY = c_intTop2;
        //                    intSwitchNumber = 1;
        //                    intPegPosY = c_intTop2;
        //                }
        //                else
        //                {
        //                    //							p_intPosY = c_intTop4;
        //                    intPegPosY = c_intTop4;
        //                }
        //                break;
        //        }

        //        #endregion

        //        int intRealHeight;
        //        m_objDiagnose.m_blnPrintAllBySimSun(11, rtgDianose, p_objGrp, out intRealHeight, blnMiddle);

        //        if (intRealHeight > rtgDianose.Height)
        //        {
        //            p_intPosY += intRealHeight + 5;
        //        }
        //        else
        //        {
        //            p_intPosY += rtgDianose.Height;
        //        }
        //        if (intSwitchNumber == 0) p_intPosY += 40;

        //        if (p_intPosY > intPegPosY)
        //            s_intHeightMargin += p_intPosY - intPegPosY;
        //        else
        //            s_intHeightMargin = 0;

        //        if (intSwitchNumber == 2)
        //        {
        //            s_intMarginTemp = s_intHeightMargin;
        //            s_intHeightMargin = 0;
        //            if (intRealHeight > rtgDianose.Height)
        //                p_intPosY -= (intRealHeight + 5);
        //            else
        //                p_intPosY -= rtgDianose.Height;
        //        }
        //        if (intSwitchNumber == 3)
        //        {
        //            if (s_intHeightMargin < s_intMarginTemp)
        //            {
        //                p_intPosY += s_intMarginTemp - s_intHeightMargin;
        //                s_intHeightMargin = s_intMarginTemp;
        //            }
        //        }


        //        //if (m_objDiagnose.m_BlnHaveNextLine())
        //        //{
        //        //    m_objDiagnose.m_mthPrintLine((int)enmRectangleInfo.PrintWidth2 - 5, (int)enmRectangleInfo.LeftX + 40, p_intPosY, p_objGrp);
        //        //    p_intPosY += 20;
        //        //}

        //        //if (m_objDiagnose.m_BlnHaveNextLine())
        //        //    m_blnHaveMoreLine = true;
        //        //else
        //        //{
        //        //    m_blnHaveMoreLine = false;
        //        //}



        //        fntTitle.Dispose();

        //        m_blnHaveMoreLine = false;
        //    }

        //    public override void m_mthReset()
        //    {
        //        //				m_intTimes = 0;
        //        m_blnHaveMoreLine = true;
        //        //				m_blnFirstPrint = true;
        //        m_objDiagnose.m_mthRestartPrint();
        //    }

        //    public override object m_ObjPrintLineInfo
        //    {
        //        get
        //        {
        //            return m_objPrintLineInfo;
        //        }
        //        set
        //        {
        //            if (value != null)
        //            {
        //                Object[] objData = (object[])value;
        //                m_strTitle = objData[3].ToString();
        //                dtmFirstPrint = (DateTime)objData[2];
        //                if (clsOutHospital_XJPrintTool.m_blnIsPrintMark)
        //                {
        //                    if (objData[1].ToString() == "")
        //                    {
        //                        m_objDiagnose.m_mthSetContextWithCorrectBefore(objData[0].ToString(), objData[1].ToString(), dtmFirstPrint);
        //                    }
        //                    else
        //                    {
        //                        m_objDiagnose.m_mthSetContextWithCorrectBefore(objData[0].ToString(), objData[1].ToString(), dtmFirstPrint, true);
        //                        m_mthAddSign2(m_strTitle.Trim(), m_objDiagnose.m_ObjModifyUserArr);
        //                    }
        //                }
        //                else
        //                {
        //                    m_objDiagnose.m_mthSetContextWithAllCorrect(objData[0].ToString(), objData[1].ToString());
        //                }
        //                if (m_objDiagnose.m_ObjModifyUserArr != null)
        //                    for (int i = 0; i < m_objDiagnose.m_ObjModifyUserArr.Length; i++)
        //                    {
        //                        if (m_objDiagnose.m_ObjModifyUserArr[i].m_clrText.ToArgb() == Color.White.ToArgb())
        //                            m_objDiagnose.m_ObjModifyUserArr[i].m_clrText = Color.Black;
        //                    }
        //            }
        //        }
        //    }
        //}
        #endregion

        private class clsPrintLine1 : com.digitalwave.Utility.Controls.clsPrintLineBase
        {
            private clsPrintRichTextContext m_objDiagnose;
            private DateTime dtmFirstPrint;
            private bool m_blnIsFirstPrint = true;
            private string m_strTitle;

            /// <summary>
            /// Ĭ��ֵ
            /// </summary>
            private readonly int c_intTop4 = 20;

            internal static bool m_blnSinglePage = true;

            public clsPrintLine1()
            {
                // c_intTop4 = intTop4;
                m_objDiagnose = new clsPrintRichTextContext(Color.Black, new Font("SimSun", 12));
            }

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    if (p_intPosY < c_intTop4 && m_blnSinglePage)
                        p_intPosY = c_intTop4;
                    p_objGrp.DrawString(m_strTitle, p_fntNormalText, Brushes.Black, (int)enmRectangleInfo.LeftX+5, p_intPosY);
                    p_intPosY += 20;
                    m_blnIsFirstPrint = false;
                }

                if (m_objDiagnose.m_BlnHaveNextLine())
                {
                    m_objDiagnose.m_mthPrintLine((int)enmRectangleInfo.PrintWidth2 - 5, (int)enmRectangleInfo.LeftX + 45, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                }

                if (m_objDiagnose.m_BlnHaveNextLine())
                    m_blnHaveMoreLine = true;
                else
                {
                    m_blnHaveMoreLine = false;
                }
            }

            public override void m_mthReset()
            {
                m_blnHaveMoreLine = true;
                m_blnIsFirstPrint = true;
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
                        if (clsDifficultCaseDiscuss_XJPrintTool.m_blnIsPrintMark)
                        {
                            if (objData[1].ToString() == "")
                            {
                                m_objDiagnose.m_mthSetContextWithCorrectBefore(objData[0].ToString(), objData[1].ToString(), dtmFirstPrint);
                            }
                            else
                            {
                                m_objDiagnose.m_mthSetContextWithCorrectBefore(objData[0].ToString(), objData[1].ToString(), dtmFirstPrint, true);
                                m_mthAddSign2(m_strTitle.Trim(), m_objDiagnose.m_ObjModifyUserArr);
                            }
                        }
                        else
                        {
                            m_objDiagnose.m_mthSetContextWithAllCorrect(objData[0].ToString(), objData[1].ToString());
                        }
                        if (m_objDiagnose.m_ObjModifyUserArr != null)
                        {
                            for (int i = 0; i < m_objDiagnose.m_ObjModifyUserArr.Length; i++)
                            {
                                if (m_objDiagnose.m_ObjModifyUserArr[i].m_clrText.ToArgb() == Color.White.ToArgb())
                                    m_objDiagnose.m_ObjModifyUserArr[i].m_clrText = Color.Black;
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region 2
        private class clsPrintLine2 : com.digitalwave.Utility.Controls.clsPrintLineBase
        {
            private clsPrintRichTextContext m_objDiagnose;
            private DateTime dtmFirstPrint;
            private bool m_blnIsFirstPrint = true;
            private string m_strTitle;

            /// <summary>
            /// Ĭ��ֵ
            /// </summary>
            private readonly int c_intTop4 = 0;

            internal static bool m_blnSinglePage = true;

            public clsPrintLine2(int intTop4)
            {
                c_intTop4 = intTop4;
                m_objDiagnose = new clsPrintRichTextContext(Color.Black, new Font("SimSun", 12));
            }

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    if (p_intPosY < c_intTop4 && m_blnSinglePage)
                        p_intPosY = c_intTop4;
                    p_objGrp.DrawString(m_strTitle, p_fntNormalText, Brushes.Black, (int)enmRectangleInfo.LeftX+5, p_intPosY);
                    p_intPosY += 20;
                    m_blnIsFirstPrint = false;
                }

                if (m_objDiagnose.m_BlnHaveNextLine())
                {
                    m_objDiagnose.m_mthPrintLine((int)enmRectangleInfo.PrintWidth2 - 5, (int)enmRectangleInfo.LeftX + 45, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                }

                if (m_objDiagnose.m_BlnHaveNextLine())
                    m_blnHaveMoreLine = true;
                else
                {
                    m_blnHaveMoreLine = false;
                }
            }

            public override void m_mthReset()
            {
                m_blnHaveMoreLine = true;
                m_blnIsFirstPrint = true;
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
                        if (clsDifficultCaseDiscuss_XJPrintTool.m_blnIsPrintMark)
                        {
                            if (objData[1].ToString() == "")
                            {
                                m_objDiagnose.m_mthSetContextWithCorrectBefore(objData[0].ToString(), objData[1].ToString(), dtmFirstPrint);
                            }
                            else
                            {
                                m_objDiagnose.m_mthSetContextWithCorrectBefore(objData[0].ToString(), objData[1].ToString(), dtmFirstPrint, true);
                                m_mthAddSign2(m_strTitle.Trim(), m_objDiagnose.m_ObjModifyUserArr);
                            }
                        }
                        else
                        {
                            m_objDiagnose.m_mthSetContextWithAllCorrect(objData[0].ToString(), objData[1].ToString());
                        }
                        if (m_objDiagnose.m_ObjModifyUserArr != null)
                        {
                            for (int i = 0; i < m_objDiagnose.m_ObjModifyUserArr.Length; i++)
                            {
                                if (m_objDiagnose.m_ObjModifyUserArr[i].m_clrText.ToArgb() == Color.White.ToArgb())
                                    m_objDiagnose.m_ObjModifyUserArr[i].m_clrText = Color.Black;
                            }
                        }
                    }
                }
            }
        }
        #endregion 2
        #endregion
    }
}


