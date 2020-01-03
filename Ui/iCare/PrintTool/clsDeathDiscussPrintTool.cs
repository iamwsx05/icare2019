using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;

namespace iCare
{
    /// <summary>
    /// �����������ۣ�����棩��ӡ��
    /// </summary>
    public class clsDeathDiscussPrintTool : infPrintRecord
    {
        private bool m_blnIsFromDataSource = true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
        private bool m_blnWantInit = true;
        /// <summary>
        /// �Ƿ��ӡ�޸ĺۼ�
        /// </summary>
        public static bool m_blnIsPrintMark = true;
        private clsDiseaseTrackDomain m_objRecordsDomain;
        private clsPrintInfo_DeathCaseDiscussRecord m_objPrintInfo;
        private clsDeadCaseDiscussRecord_VO m_objRecordContent = null;

        public clsDeathDiscussPrintTool()
        { }

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
            m_objPrintInfo = new clsPrintInfo_DeathCaseDiscussRecord();
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
            if (m_objPrintInfo.m_strInPatentID == "" || m_objPrintInfo.m_dtmOpenDate == DateTime.MinValue)
                m_objRecordContent = null;
            else
            {
                //m_objRecordsDomain = new clsDiseaseTrackDomain(new com.digitalwave.DiseaseTrackService.clsDeathCaseDiscussService());
                m_objRecordsDomain = new clsDiseaseTrackDomain(enmDiseaseTrackType.DeathCaseDiscuss);
                clsTrackRecordContent objContent = new clsTrackRecordContent();
                long lngRes = m_objRecordsDomain.m_lngGetRecordContent(m_objPrintInfo.m_strInPatentID, m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"), out objContent);
                if (lngRes <= 0)
                    return;
                m_objRecordContent = (clsDeadCaseDiscussRecord_VO)objContent;
            }
            //���ñ����ݵ���ӡ��			
            m_objPrintInfo.m_objRecordContent = m_objRecordContent;
            m_mthSetPrintValue();//�����з��ӡ����,��ʹ�ڴ�ӡ�հױ�ʱ,����Ҳ����ִ��.			
        }

        /// <summary>
        /// ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
        /// </summary>
        /// <param name="p_objPrintContent">��ӡ����</param>
        public void m_mthSetPrintContent(object p_objPrintContent)
        {


            m_blnWantInit = false;
            //if(p_objPrintContent.GetType().Name !="clsPrintInfo_Base")
            //{
            //    clsPublicFunction.ShowInformationMessageBox("��������");
            //    return;
            //}
            m_blnIsFromDataSource = false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ
            m_objPrintInfo = (clsPrintInfo_DeathCaseDiscussRecord)p_objPrintContent;
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
                m_intYPos += (int)enmRectangleInfo.RowStep - 20;
            }
            
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, (int)enmRectangleInfo.TopY - 30, (int)enmRectangleInfo.RightX, (int)enmRectangleInfo.TopY - 30);


            while (m_objPrintContext.m_BlnHaveMoreLine)
            {
                m_objPrintContext.m_mthPrintNextLine(ref m_intYPos, e.Graphics, fntNormal);

                if (m_intYPos >= (int)enmRectangleInfo.BottomY
                    && m_objPrintContext.m_BlnHaveMoreLine)
                {


                    #region ��ҳ����
                    e.HasMorePages = true;

                    e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, (int)enmRectangleInfo.TopY - 30, (int)enmRectangleInfo.LeftX, m_intYPos);
                    e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.RightX, (int)enmRectangleInfo.TopY - 30, (int)enmRectangleInfo.RightX, m_intYPos);
                    e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, m_intYPos, (int)enmRectangleInfo.RightX, m_intYPos);
                    //					e.Graphics.DrawString("����"+m_intPages.ToString()+"ҳ��",m_fotSmallFont ,m_slbBrush,(int)enmRectangleInfo.LeftX+500 ,m_intYPos+20);
                    m_intPages++;
                    m_intYPos = (int)enmRectangleInfo.TopY-20;
                    clsPrintLine2.m_blnSinglePage = false;
                    return;

                    #endregion ��ҳ����
                }

            }

            #region ���һҳ����
            m_intYPos += 30;
            e.Graphics.DrawString("��¼��:", new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 498, m_intYPos);
            if (m_objRecordContent != null)
            {
                //    e.Graphics.DrawString(m_objRecordContent.m_strRecorderName, new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 560 + (int)(5f * 17.5f), m_intYPos);

                com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
                clsEmrEmployeeBase_VO objEmpVO = null;
                objEmployeeSign.m_lngGetEmpByNO(m_objRecordContent.m_strRecorderID, out objEmpVO);
                if (objEmpVO != null)
                {
                    Image imgEmpSig = ImageSignature.GetEmpSigImage(objEmpVO.m_strLASTNAME_VCHR);
                    if (imgEmpSig != null)
                    {
                            //imgEmpSig = ImageSignature.pictureProcess(imgEmpSig, 579, 268);
                            e.Graphics.DrawString(objEmpVO.m_strTechnicalRank, new Font("SimSun", 12, FontStyle.Bold), Brushes.Black, (int)enmRectangleInfo.LeftX + 560, m_intYPos);
                            e.Graphics.DrawImage(imgEmpSig, (int)enmRectangleInfo.LeftX + 650, m_intYPos - 5, 70, 30);
                    }
                    else
                    {
                        if (!(string.IsNullOrEmpty(objEmpVO.m_strGetTechnicalRankAndName)))
                              e.Graphics.DrawString(objEmpVO.m_strGetTechnicalRankAndName, new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 498 + (int)(5f * 17.5f), m_intYPos);
                    }
                }
            }

            m_intYPos += 30;

            e.Graphics.DrawString("����������ǩ��:", new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 430, m_intYPos);
            if (m_objRecordContent != null)
            {
                //e.Graphics.DrawString(m_objRecordContent.m_strCompereSignName, new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 560 + (int)(5f * 17.5f), m_intYPos);
                com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
                clsEmrEmployeeBase_VO objEmpVO = null;
                objEmployeeSign.m_lngGetEmpByNO(m_objRecordContent.m_strCompereSignID, out objEmpVO);
                if (objEmpVO != null)
                {
                    Image imgEmpSig = ImageSignature.GetEmpSigImage(objEmpVO.m_strLASTNAME_VCHR);
                    if (imgEmpSig != null)
                    {
                        //imgEmpSig = ImageSignature.pictureProcess(imgEmpSig, 579, 268);
                        e.Graphics.DrawString(objEmpVO.m_strTechnicalRank, new Font("SimSun", 12, FontStyle.Bold), Brushes.Black, (int)enmRectangleInfo.LeftX + 560, m_intYPos);
                        e.Graphics.DrawImage(imgEmpSig, (int)enmRectangleInfo.LeftX + 650, m_intYPos - 5, 70, 30);
                    }
                    else
                    {
                        if (!(string.IsNullOrEmpty(objEmpVO.m_strGetTechnicalRankAndName)))
                            e.Graphics.DrawString(objEmpVO.m_strGetTechnicalRankAndName, new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 500 + (int)(5f * 15.5f), m_intYPos);
                    }
                }
            }

            m_intYPos += 25;
            if (m_intYPos < (int)enmRectangleInfo.BottomY)
                m_intYPos = (int)enmRectangleInfo.BottomY;
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, (int)enmRectangleInfo.TopY - 30, (int)enmRectangleInfo.LeftX, m_intYPos);
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.RightX, (int)enmRectangleInfo.TopY - 30, (int)enmRectangleInfo.RightX, m_intYPos);
            e.Graphics.DrawLine(m_GridPen, (int)enmRectangleInfo.LeftX, m_intYPos, (int)enmRectangleInfo.RightX, m_intYPos);

            //			e.Graphics.DrawString("����"+m_intPages.ToString()+"ҳ��",m_fotSmallFont ,m_slbBrush,(int)enmRectangleInfo.LeftX+350 ,m_intYPos+20);
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
            m_intYPos = (int)enmRectangleInfo.TopY ;
        }

        // ��ӡ����ʱ�Ĳ���
        private void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
        {
           clsPrintLine2.m_blnSinglePage = true;
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
        private int m_intYPos = (int)enmRectangleInfo.TopY + 27;
        private int m_intPages = 1;

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

        // <summary>
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
            Area_Title,
            Area,
            Bed_Title,
            Bed,

            InpatientDate_Title,
            InpatientDate,
            DeathTime_Title,
            DeathTime,
            DiscussTime_Title,
            DiscussTime,
            DiscussAddress_Title,
            DiscussAddress,
            Comperetor_Title,
            Comperetor,

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
                        m_fReturnPoint = new PointF(273f, 60f);
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
                    case (int)enmItemDefination.Area_Title:
                        m_fReturnPoint = new PointF(380f, 100f);
                        break;
                    case (int)enmItemDefination.Area:
                        m_fReturnPoint = new PointF(425f, 100f);
                        break;
                    case (int)enmItemDefination.Bed_Title:
                        m_fReturnPoint = new PointF(565f, 100f);
                        break;
                    case (int)enmItemDefination.Bed:
                        m_fReturnPoint = new PointF(605f, 97f);
                        break;

                    case (int)enmItemDefination.Age_Title:
                        m_fReturnPoint = new PointF(275f, 100f);
                        break;
                    case (int)enmItemDefination.Age:
                        m_fReturnPoint = new PointF(320f, 100f);
                        break;
                    case (int)enmItemDefination.InpatientDate_Title:
                        m_fReturnPoint = new PointF(450f, 145f);
                        break;
                    case (int)enmItemDefination.InpatientDate:
                        m_fReturnPoint = new PointF(530f, 145f);
                        break;
                    case (int)enmItemDefination.DeathTime_Title:
                        m_fReturnPoint = new PointF(450f, 165f);
                        break;
                    case (int)enmItemDefination.DeathTime:
                        m_fReturnPoint = new PointF(530f, 165f);
                        break;
                    case (int)enmItemDefination.DiscussTime_Title:
                        m_fReturnPoint = new PointF(450f, 185f);
                        break;
                    case (int)enmItemDefination.DiscussTime:
                        m_fReturnPoint = new PointF(530f, 185f);
                        break;
                    case (int)enmItemDefination.DiscussAddress_Title:
                        m_fReturnPoint = new PointF(40f, 220f);
                        break;
                    case (int)enmItemDefination.DiscussAddress:
                        m_fReturnPoint = new PointF(120f, 220f);
                        break;
                    case (int)enmItemDefination.Comperetor_Title:
                        m_fReturnPoint = new PointF(40f, 245f);
                        break;
                    case (int)enmItemDefination.Comperetor:
                        m_fReturnPoint = new PointF(100f, 245f);
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
            m_objLine1Arr = new clsPrintLine1[6];
            m_objLine2Arr = new clsPrintLine2[6];
            for (int i = 0; i < m_objLine1Arr.Length; i++)
                m_objLine1Arr[i] = new clsPrintLine1();


            m_objLine2Arr[0] = new clsPrintLine2(270);
            m_objLine2Arr[1] = new clsPrintLine2(340);
            m_objLine2Arr[2] = new clsPrintLine2(500);
            m_objLine2Arr[3] = new clsPrintLine2(750);
            m_objLine2Arr[4] = new clsPrintLine2(860);
            m_objLine2Arr[5] = new clsPrintLine2(900);

            m_objPrintContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
										  m_objLine1Arr[0],m_objLine1Arr[1],m_objLine1Arr[2],m_objLine1Arr[3],m_objLine1Arr[4],
                                          m_objLine1Arr[5],//,m_objLine1Arr[6]
                                          m_objLine2Arr[0],m_objLine2Arr[1],m_objLine2Arr[2],m_objLine2Arr[3],m_objLine2Arr[4],
                                          m_objLine2Arr[5]
									  });
            m_objPrintContext.m_ObjPrintSign = new com.digitalwave.Utility.Controls.clsPrintRecordSign();
            #endregion

            #region ��ÿһ�е�Ԫ�ظ�ֵ
            string strBlanks = "���������������� ���� ��������������";
            if (m_objRecordContent != null)
            {
                ///////////////1��/////////////////
                Object[] objData1 = new object[4];
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
                m_objLine2Arr[0].m_ObjPrintLineInfo = objData1;


                ///////////////3��/////////////////
                objData1[0] = m_objRecordContent.m_strInHospitalDiagnose;
                objData1[1] = m_objRecordContent.m_strInHospitalDiagnoseXML;
                objData1[3] = "��Ժ��ϣ�";
                m_objLine2Arr[1].m_ObjPrintLineInfo = objData1;

                ///////////////4��/////////////////
                objData1[0] = m_objRecordContent.m_strSpeakRecord;
                objData1[1] = m_objRecordContent.m_strSpeakRecordXML;
                objData1[2] = dtmFirstPrintTime;
                objData1[3] = "���Լ�¼";
                m_objLine2Arr[2].m_ObjPrintLineInfo = objData1;

                ///////////////5��/////////////////
                objData1[0] = m_objRecordContent.m_strVerdict;
                objData1[1] = m_objRecordContent.m_strVerdictXML;
                objData1[3] = "���ۣ�";
                m_objLine2Arr[3].m_ObjPrintLineInfo = objData1;

                ///////////////6��/////////////////
                objData1[0] = m_objRecordContent.m_strDeadDiagnose;
                objData1[1] = m_objRecordContent.m_strDeadDiagnoseXML;
                objData1[3] = "������ϣ�";
                m_objLine2Arr[4].m_ObjPrintLineInfo = objData1;

                ///////////////7��/////////////////
                objData1[0] = m_objRecordContent.m_strDeadReason;
                objData1[1] = m_objRecordContent.m_strDeadDiagnoseXML;
                objData1[3] = "����ԭ��";
                m_objLine2Arr[5].m_ObjPrintLineInfo = objData1;

                ///////////////2��/////////////////
                //objData1[0] = m_objRecordContent.m_strExperience;
                //objData1[1] = m_objRecordContent.m_strExperienceXML;
                //objData1[2] = dtmFirstPrintTime;
                //objData1[3] = "�����ѵ��";
                //m_objLine1Arr[6].m_ObjPrintLineInfo = objData1;

            }
            else
            {
                ///////////////1��/////////////////
                Object[] objData1 = new object[4];
                objData1[0] = "";
                objData1[1] = "";
                objData1[2] = dtmFirstPrintTime;
                objData1[3] = "�μ���Ա��";
                m_objLine2Arr[0].m_ObjPrintLineInfo = objData1;

                ///////////////2��/////////////////				
                objData1[0] = "";
                objData1[1] = "";
                objData1[2] = dtmFirstPrintTime;
                objData1[3] = "��Ժ��ϣ�";
                m_objLine2Arr[1].m_ObjPrintLineInfo = objData1;

                ///////////////3��/////////////////
                objData1[0] = "";
                objData1[1] = "";
                objData1[3] = "���Լ�¼";
                m_objLine2Arr[2].m_ObjPrintLineInfo = objData1;
                ///////////////4��/////////////////	
                objData1[0] = "";
                objData1[1] = "";
                objData1[2] = dtmFirstPrintTime;
                objData1[3] = "���ۣ�";
                m_objLine2Arr[3].m_ObjPrintLineInfo = objData1;
                ///////////////5��/////////////////
                objData1[0] = "";
                objData1[1] = "";
                objData1[3] = "������ϣ�";
                m_objLine2Arr[4].m_ObjPrintLineInfo = objData1;
                ///////////////6��/////////////////
                objData1[0] = "";
                objData1[1] = "";
                objData1[3] = "����ԭ��";
                m_objLine2Arr[5].m_ObjPrintLineInfo = objData1;
                ///////////////7��/////////////////
                //objData1[0] = "";
                //objData1[1] = "";
                //objData1[3] = "�����ѵ��";
                //m_objLine1Arr[6].m_ObjPrintLineInfo = objData1;
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

            e.Graphics.DrawString("�� �� �� �� �� �� �� ¼", m_fotTitleFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_Name_Title));

            e.Graphics.DrawString("����:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name_Title));

            e.Graphics.DrawString(m_objPrintInfo.m_strPatientName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name));

            e.Graphics.DrawString("�Ա�:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex_Title));

            e.Graphics.DrawString(m_objPrintInfo.m_strSex, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex));

            e.Graphics.DrawString("����:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age_Title));

            e.Graphics.DrawString(m_objPrintInfo.m_strAge, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age));

            e.Graphics.DrawString("������", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Area_Title));

            e.Graphics.DrawString(m_objPrintInfo.m_strAreaName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Area));

            e.Graphics.DrawString("���ţ�", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Bed_Title));

            e.Graphics.DrawString(m_objPrintInfo.m_strBedName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Bed));

            e.Graphics.DrawString("סԺ�ţ�", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID_Title));

            e.Graphics.DrawString(m_objPrintInfo.m_strHISInPatientID, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID));

            if (m_intPages == 1)
            {
                e.Graphics.DrawString("����:", m_fotSmallFont, m_slbBrush, 120, 145);

                e.Graphics.DrawString(m_objPrintInfo.m_strPatientName, m_fotSmallFont, m_slbBrush, 170, 145);

                e.Graphics.DrawString("�Ա�:", m_fotSmallFont, m_slbBrush, 120, 165);

                e.Graphics.DrawString(m_objPrintInfo.m_strSex, m_fotSmallFont, m_slbBrush, 170, 165);

                e.Graphics.DrawString("����:", m_fotSmallFont, m_slbBrush, 120, 185);

                e.Graphics.DrawString(m_objPrintInfo.m_strAge, m_fotSmallFont, m_slbBrush, 170, 185);

                e.Graphics.DrawString("��Ժʱ��:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InpatientDate_Title));

                e.Graphics.DrawString((m_objPrintInfo.m_strHISInPatientID == "" ? "" : m_objPrintInfo.m_dtmHISInDate.ToString("yyyy��MM��dd�� HH:mm")), m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InpatientDate));

                e.Graphics.DrawString("����ʱ��:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.DeathTime_Title));
                if (m_objRecordContent != null)
                    e.Graphics.DrawString(m_objRecordContent.m_dtmDeadDate.ToString("yyyy��MM��dd�� HHʱmm��ss��"), m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.DeathTime));

                e.Graphics.DrawString("����ʱ��:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.DiscussTime_Title));
                if (m_objRecordContent != null)
                    e.Graphics.DrawString(m_objRecordContent.m_dtmDiscussDate.ToString("yyyy��MM��dd�� HH:mm"), m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.DiscussTime));


                e.Graphics.DrawString("���۵ص�:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.DiscussAddress_Title));
                if (m_objRecordContent != null)
                    e.Graphics.DrawString(m_objRecordContent.m_strDiscussAddress, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.DiscussAddress));

                e.Graphics.DrawString("������:", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Comperetor_Title));
                if (m_objRecordContent != null)
                {
                    // e.Graphics.DrawString(m_objRecordContent.m_strCompereName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Comperetor));
                    com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
                    clsEmrEmployeeBase_VO objEmpVO = null;
                    objEmployeeSign.m_lngGetEmpByNO(m_objRecordContent.m_strCompereID, out objEmpVO);
                    if (objEmpVO != null)
                        if (!(string.IsNullOrEmpty(objEmpVO.m_strGetTechnicalRankAndName)))
                            // e.Graphics.DrawString(objEmpVO.m_strGetTechnicalRankAndName, new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 500 + (int)(5f * 15.5f), m_intYPos);
                            e.Graphics.DrawString(objEmpVO.m_strGetTechnicalRankAndName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Comperetor));
                }
                m_intYPos += 60;
            }
        }
        #endregion


        #region print class

        private class clsPrintLine1 : com.digitalwave.Utility.Controls.clsPrintLineBase
        {
            private clsPrintRichTextContext m_objDiagnose;
            private DateTime dtmFirstPrint;
            private string m_strTitle;

            /// <summary>
            /// �μ���Ա
            /// </summary>
            private const int c_intTop1 = 270;
            /// <summary>
            /// ��Ժ���
            /// </summary>
            private const int c_intTop2 = 380;
            /// <summary>
            /// ���Լ�¼
            /// </summary>
            private const int c_intTop3 = 680;
            /// <summary>
            /// ����
            /// </summary>
            private const int c_intTop4 = 750;
            /// <summary>
            /// �������
            /// </summary>
            private const int c_intTop5 = 860;
            /// <summary>
            /// ����ԭ��
            /// </summary>
            private const int c_intTop6 = 900;
            /// <summary>
            /// �����ѵ
            /// </summary>
            private const int c_intTop7 = 1000;
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
            private static int s_intHeight2Margin = 0;
            /// <summary>
            /// �ݴ��ƫ����
            /// </summary>
            private static int s_intMarginTemp = 0;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                Font fntTitle = new Font("SimSun", 11);
                Pen m_Line1Pen = new Pen(Color.Gray, 0.5f);

                int intPosYy = p_intPosY + 30;

                Rectangle rtgDianose = new Rectangle(
                    (int)enmRectangleInfo.LeftX,
                    p_intPosY,
                    0,
                    0);
                bool blnMiddle = true;

                int intSwitchNumber = 0;
                // ԭ���̶��ĸ߶�
                int intPegPosY = 0;

                switch (m_strTitle)
                {
                    case "�μ���Ա��":

                        rtgDianose.X += (int)enmRectangleInfo.LeftX;
                        rtgDianose.Y += 30;
                        rtgDianose.Width = (int)enmRectangleInfo.RightX - rtgDianose.X;
                        blnMiddle = false;
                        rtgDianose.Height = c_intOneRowHeight;
                        intPegPosY = c_intTop1;
                        s_intHeight2Margin = 0;
                        intSwitchNumber = 1;
                        break;
                    case "��Ժ��ϣ�":
                        rtgDianose.X += (int)enmRectangleInfo.LeftX;
                        rtgDianose.Y += 52;
                        rtgDianose.Width = (int)enmRectangleInfo.RightX - rtgDianose.X;
                        blnMiddle = false;
                        if ((rtgDianose.Height = ((c_intTop2 - c_intTop1 - 40) - s_intHeight2Margin)) < c_intOneRowHeight)
                        {
                            if (rtgDianose.Height < 0)
                                s_intHeight2Margin = Math.Abs(rtgDianose.Height);
                            rtgDianose.Height = c_intOneRowHeight;
                        }
                        intPegPosY = c_intTop2;
                        break;
                    case "���Լ�¼":
                        rtgDianose.X += (int)enmRectangleInfo.LeftX;
                        rtgDianose.Y += 48;
                        rtgDianose.Width = (int)enmRectangleInfo.RightX - rtgDianose.X;
                        blnMiddle = false;
                        if ((rtgDianose.Height = ((c_intTop3 - c_intTop2 - 40) - s_intHeight2Margin)) < c_intOneRowHeight)
                        {
                            if (rtgDianose.Height < 0)
                                s_intHeight2Margin = Math.Abs(rtgDianose.Height);
                            rtgDianose.Height = c_intOneRowHeight;
                        }
                        intPegPosY = c_intTop3;
                        break;
                    case "���ۣ�":
                        rtgDianose.X += (int)enmRectangleInfo.LeftX;
                        rtgDianose.Y += 30;
                        rtgDianose.Width = (int)enmRectangleInfo.RightX - rtgDianose.X;
                        blnMiddle = false;
                        if ((rtgDianose.Height = ((c_intTop4 - c_intTop3 - 40) - s_intHeight2Margin)) < c_intOneRowHeight)
                        {
                            if (rtgDianose.Height < 0)
                                s_intHeight2Margin = Math.Abs(rtgDianose.Height);
                            rtgDianose.Height = c_intOneRowHeight;
                        }
                        //						intPegPosY = c_intTop4;
                        intPegPosY = rtgDianose.Y;
                        intSwitchNumber = 4;
                        break;
                    case "������ϣ�":
                        rtgDianose.X += (int)enmRectangleInfo.LeftX;
                        rtgDianose.Y += 48;
                        rtgDianose.Width = (int)enmRectangleInfo.RightX - rtgDianose.X;
                        blnMiddle = false;
                        if ((rtgDianose.Height = ((c_intTop5 - c_intTop4 - 40) - s_intHeight2Margin)) < c_intOneRowHeight)
                        {
                            if (rtgDianose.Height < 0)
                                s_intHeight2Margin = Math.Abs(rtgDianose.Height);
                            rtgDianose.Height = c_intOneRowHeight;
                        }
                        //						intPegPosY = c_intTop5;
                        intPegPosY = rtgDianose.Y;
                        break;
                    case "����ԭ��":
                        rtgDianose.X += (int)enmRectangleInfo.LeftX;
                        rtgDianose.Y += 48;
                        rtgDianose.Width = (int)enmRectangleInfo.RightX - rtgDianose.X;
                        blnMiddle = false;
                        if ((rtgDianose.Height = ((c_intTop6 - c_intTop5 - 40) - s_intHeight2Margin)) < c_intOneRowHeight)
                        {
                            if (rtgDianose.Height < 0)
                                s_intHeight2Margin = Math.Abs(rtgDianose.Height);
                            rtgDianose.Height = c_intOneRowHeight;
                        }
                        //						intPegPosY = c_intTop6;
                        intPegPosY = rtgDianose.Y;
                        break;
                    //case "�����ѵ��":
                    //    rtgDianose.X += (int)enmRectangleInfo.LeftX;
                    //    rtgDianose.Y += 48;
                    //    rtgDianose.Width = (int)enmRectangleInfo.RightX - rtgDianose.X;
                    //    blnMiddle = false;
                    //    if ((rtgDianose.Height = ((c_intTop7 - c_intTop6 - 40) - s_intHeight2Margin)) < c_intOneRowHeight)
                    //    {
                    //        if (rtgDianose.Height < 0)
                    //            s_intHeight2Margin = Math.Abs(rtgDianose.Height);
                    //        rtgDianose.Height = c_intOneRowHeight;
                    //    }
                    //    //						intPegPosY = c_intTop7;
                    //    intPegPosY = rtgDianose.Y;
                    //    break;
                    default:
                        s_intHeight2Margin = 0;
                        break;
                }
                int intRealHeight;
                m_objDiagnose.m_blnPrintAllBySimSun(11, rtgDianose, p_objGrp, out intRealHeight, blnMiddle);

                if (intRealHeight > rtgDianose.Height)
                {
                    if (intSwitchNumber == 1 || intSwitchNumber == 4)
                        p_intPosY += intRealHeight;
                    else
                        p_intPosY += intRealHeight + 25;
                }
                else
                {
                    p_intPosY += rtgDianose.Height;
                }

                if (p_intPosY > intPegPosY)
                    s_intHeight2Margin += p_intPosY - intPegPosY;
                else
                {
                    p_intPosY = intPegPosY;
                    s_intHeight2Margin = 0;
                }

                if (m_strTitle != "���Լ�¼")
                    p_objGrp.DrawString(m_strTitle, fntTitle, Brushes.Black, (int)enmRectangleInfo.LeftX, intPosYy);
                else
                    p_objGrp.DrawString(m_strTitle, fntTitle, Brushes.Black, (int)enmRectangleInfo.LeftX + 350, intPosYy);

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
                   // p_objGrp.DrawString(m_strTitle, p_fntNormalText, Brushes.Black, (int)enmRectangleInfo.LeftX, p_intPosY);
                    if (m_strTitle != "���Լ�¼")
                        p_objGrp.DrawString(m_strTitle, p_fntNormalText, Brushes.Black, (int)enmRectangleInfo.LeftX, p_intPosY);
                    else
                        p_objGrp.DrawString(m_strTitle, p_fntNormalText, Brushes.Black, (int)enmRectangleInfo.LeftX + 350, p_intPosY);

                    p_intPosY += 20;
                    m_blnIsFirstPrint = false;
                }

                if (m_objDiagnose.m_BlnHaveNextLine())
                {
                    m_objDiagnose.m_mthPrintLine((int)enmRectangleInfo.PrintWidth2 - 5, (int)enmRectangleInfo.LeftX + 40, p_intPosY, p_objGrp);
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
                        if (clsDeathDiscussPrintTool.m_blnIsPrintMark)
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
    }
}
