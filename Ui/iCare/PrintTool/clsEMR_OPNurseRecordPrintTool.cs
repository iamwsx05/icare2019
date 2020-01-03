using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// ���������¼(����)��ӡ������
    /// </summary>
    public class clsEMR_OPNurseRecordPrintTool : infPrintRecord
    {
        private bool m_blnIsFromDataSource = true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
        private bool m_blnWantInit = true;
        private clsPrintInfo_EMR_OPNurseRecord m_objPrintInfo;

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
            m_objPrintInfo = new clsPrintInfo_EMR_OPNurseRecord();
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
            m_objPrintInfo.m_dtmHISInPatientDate = m_objPatient != null ? m_objPatient.m_DtmSelectedHISInDate : DateTime.MinValue;
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

            clsDiseaseTrackDomain m_objRecordsDomain = new clsDiseaseTrackDomain(enmDiseaseTrackType.EMR_OPNurseRecord);
            
            if (m_objPrintInfo.m_strInPatentID != "" && m_objPrintInfo.m_dtmOpenDate != DateTime.MinValue)
            {
                clsTrackRecordContent objContent = new clsTrackRecordContent();
                long lngRes = m_objRecordsDomain.m_lngGetRecordContent(m_objPrintInfo.m_strInPatentID, m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"), out objContent);
                m_objPrintInfo.m_objRecordContent = objContent as clsEMR_OperationRecord_GX;
                if (lngRes <= 0)
                    return;
            }
            m_objRecordsDomain = null;
            //���ñ����ݵ���ӡ��			
            m_mthSetPrintContent((clsEMR_OperationRecord_GX)m_objPrintInfo.m_objRecordContent, m_objPrintInfo.m_dtmFirstPrintDate);
        }

        /// <summary>
        /// ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
        /// </summary>
        /// <param name="p_objPrintContent">��ӡ����</param>
        public void m_mthSetPrintContent(object p_objPrintContent)
        {
            m_blnWantInit = false;
            if (p_objPrintContent.GetType().Name != "clsPrintInfo_EMR_OPNurseRecord")
            {
                MDIParent.ShowInformationMessageBox("��������");
            }
            m_blnIsFromDataSource = false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ
            m_objPrintInfo.m_objRecordContent = (clsEMR_OperationRecord_GX)p_objPrintContent;

            m_mthSetPrintContent(m_objPrintInfo.m_objRecordContent, m_objPrintInfo.m_dtmFirstPrintDate);
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
            if (m_objPrintInfo.m_objRecordContent == null)
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
            m_fotTitleFont = new Font("SimSun", 15.75f, FontStyle.Bold);//��������
            m_fotHeaderFont = new Font("SimSun", 10.5F);//�������
            m_fotItemHead = new Font("", 13, FontStyle.Bold);
            m_fotSmallFont = new Font("SimSun", 11);
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
            if (m_blnIsFromDataSource == false || m_objPrintInfo.m_strInPatentID == "") return;
            //�����ӡ�ɹ�������������Ҫ���µ�ʱ�䣬����У�����ʱ�䡣 
            if (!((PrintEventArgs)p_objPrintArg).Cancel && m_objPrintInfo.m_blnIsFirstPrint)
            {
                clsDiseaseTrackDomain m_objRecordsDomain = new clsDiseaseTrackDomain(enmDiseaseTrackType.EMR_OPNurseRecord);
                m_objRecordsDomain.m_lngUpdateFirstPrintDate(m_objPrintInfo.m_strInPatentID, 
                    m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), 
                    m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"), m_objPrintInfo.m_dtmFirstPrintDate);
            }
        }

        /// <summary>
        /// ���ô�ӡ����
        /// </summary>
        /// <param name="p_objContent"></param>
        /// <param name="p_dtmFirstPrintDate"></param>
        private void m_mthSetPrintContent(clsEMR_OperationRecord_GX p_objContent, DateTime p_dtmFirstPrintDate)
        {
            m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[] { new  clsPrintInstanceBeforeOP(),
                                                                          new  clsPrintNurseInOP(),
                                                                          new  clsPrintNurseAfterOP(),
                                                                          new  clsPrintNurseRecordContent(),
                                                                          new  clsPrintNurseSign()});
            m_objPrintLineContext.m_ObjPrintSign = new com.digitalwave.Utility.Controls.clsPrintRecordSign();

            //���ô�ӡ��Ϣ������Set Value��ȥ
            m_objPrintLineContext.m_ObjPrintLineInfo = m_objPrintInfo;
            //�����ݿ��ó�����FirstPrintDate����ÿ����ӡ�������m_DtmFirstPrintTime���ڸ���������
            m_objPrintLineContext.m_DtmFirstPrintTime = p_dtmFirstPrintDate;
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
        private int m_intYPos = 155;

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
        /// <summary>
		/// ��ȡ�������
		/// </summary>
        private clsPrintPageSettingForRecord m_objPageSetting;
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
        #endregion

        #region ���ӵ���Ϣ
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

            PrintWidth = 690,
            PrintWidth2 = 710,
        } 
        #endregion

        #region ��ӡ��ʼ���ڴ�ӡҳ֮ǰ�Ĳ���
        // ��ӡ��ʼ���ڴ�ӡҳ֮ǰ�Ĳ���
        private void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
        {

        } 
        #endregion

        #region ��ӡҳ
        // ��ӡҳ
        private void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
        {
            m_mthPrintTitleInfo(p_objPrintPageArg);
            m_mthPrintHeader(p_objPrintPageArg);

            Font fntNormal = new Font("", 10);

            while (m_objPrintLineContext.m_BlnHaveMoreLine)
            {
                //�������ݴ�ӡ
                m_objPrintLineContext.m_mthPrintNextLine(ref m_intYPos, p_objPrintPageArg.Graphics, fntNormal);

                if (m_intYPos > p_objPrintPageArg.PageBounds.Height - 85
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
        #endregion

        #region PrintClasses
        #region ��ӡ������
        /// <summary>
        /// ��ӡ������
        /// </summary>
        private abstract class clsPrintInPatientCaseInfo : com.digitalwave.Utility.Controls.clsPrintLineBase
        {
            protected clsEMR_OperationRecord_GX m_objContent;
            /// <summary>
            /// ���־�����ߵı߾�
            /// </summary>
            protected int m_intRecBaseX = clsPrintPosition.c_intLeftX;
            protected int m_intPatientInfoX = clsPrintPosition.c_intLeftX + 45;
            protected clsPrintInfo_EMR_OPNurseRecord m_objPrintInfo;
            /// <summary>
            /// �Ƿ��Ѵ�ӡͼƬ
            /// </summary>
            public static bool m_blnHasPrintPic = false;
            /// <summary>
            /// ��ǰͼƬ
            /// </summary>
            public int m_intCurrentPic = 0;

            public override object m_ObjPrintLineInfo
            {
                get
                {
                    return base.m_blnHaveMoreLine;
                }
                set
                {
                    if (value == null) return;
                    object objData = value;
                    m_objPrintInfo = (clsPrintInfo_EMR_OPNurseRecord)objData;
                }
            }

            public string m_strGetSingArr(string p_strControlName, clsEmrSigns_VO[] p_objSignArr)
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
        } 
        #endregion

        #region ��ǰ���
        /// <summary>
        /// ��ǰ���
        /// </summary>
        private class clsPrintInstanceBeforeOP : clsPrintInPatientCaseInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            private bool m_blnIsFirstPrint = true;
            private int m_intTimes = 0;
            private Font fntCheck = new Font("SimSun", 18);
            private clsPublicControlPaint m_objCPaint = new clsPublicControlPaint();

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    bool blnIsNull = false;
                    if (m_objPrintInfo == null || m_objPrintInfo.m_objRecordContent == null)
                    {
                        blnIsNull = true;
                    }
                    p_intPosY -= 10;

                    #region ����
                    p_objGrp.DrawString("�������ƣ�" + (blnIsNull ? "" : m_objPrintInfo.m_objRecordContent.m_strOPNAME_RIGHT), p_fntNormalText, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    p_objGrp.DrawString("����ҽ����" + (blnIsNull ? "" : m_strGetSingArr("m_lsvOperationer", m_objPrintInfo.m_objRecordContent.objSignerArr)), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 450, p_intPosY); 
                    #endregion

                    p_intPosY += 30;
                    #region ����
                    p_objGrp.DrawString("����ʽ��" + (blnIsNull ? "" : m_objPrintInfo.m_objRecordContent.m_strANANAME_RIGHT), p_fntNormalText, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    p_objGrp.DrawString("����ҽ����" + (blnIsNull ? "" : m_strGetSingArr("m_lsvAnaDocSign", m_objPrintInfo.m_objRecordContent.objSignerArr)), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 450, p_intPosY); 
                    #endregion

                    p_intPosY += 30;
                    #region ������λ
                    p_objGrp.DrawString("������λ��", p_fntNormalText, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 90, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("����λ  ", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 105, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 180, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("����λ  ", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 195, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 270, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("��֫λ  ", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 285, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 360, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("������λ  ", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 375, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 460, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("������" + (blnIsNull ? "" : m_objPrintInfo.m_objRecordContent.m_strOTHERPOSTURE_RIGHT), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 475, p_intPosY);

                    if (!blnIsNull)
                    {
                        if (m_objPrintInfo.m_objRecordContent.m_strPOSTURE[0].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 80, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strPOSTURE[1].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 170, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strPOSTURE[2].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 260, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strPOSTURE[3].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 350, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strPOSTURE[4].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 450, p_intPosY - 8);
                    } 
                    #endregion

                    p_intPosY += 30;
                    p_objGrp.DrawLine(Pens.Black, m_intRecBaseX, p_intPosY, m_intRecBaseX + (int)enmRectangleInfo.RightX - (int)enmRectangleInfo.LeftX + 10, p_intPosY);
                    p_objGrp.DrawString("��ǰ����", p_fntNormalText, Brushes.Black, new RectangleF(m_intRecBaseX + 10, p_intPosY - 90, 20, 120));
                                        
                    m_blnIsFirstPrint = false;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intPatientInfoX, p_intPosY, p_objGrp);

                    p_intPosY += 30;

                    m_intTimes++;
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

                m_blnIsFirstPrint = true;

                m_blnHaveMoreLine = true;

                m_intTimes = 0;
            }
        } 
        #endregion

        #region ���л���
        /// <summary>
        /// ���л���
        /// </summary>
        private class clsPrintNurseInOP : clsPrintInPatientCaseInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            private bool m_blnIsFirstPrint = true;
            private int m_intTimes = 0;
            private Font fntCheck = new Font("SimSun", 18);
            private clsPublicControlPaint m_objCPaint = new clsPublicControlPaint();

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    bool blnIsNull = false;
                    if (m_objPrintInfo == null || m_objPrintInfo.m_objRecordContent == null)
                    {
                        blnIsNull = true;
                    }
                    p_intPosY += 5;
                    #region ֹѪ��
                    p_objGrp.DrawString("ֹѪ����", p_fntNormalText, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 80, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("��Ѫ��Ƥ��  ", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 95, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 195, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("��ѹֹѪ��  ", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 210, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 310, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("�Զ�ֹѪ��  ", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 325, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 425, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("��  ", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 440, p_intPosY);

                    if (!blnIsNull)
                    {
                        if (m_objPrintInfo.m_objRecordContent.m_strSTANCHSTRAP[0].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 70, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strSTANCHSTRAP[1].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 185, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strSTANCHSTRAP[2].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 300, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strSTANCHSTRAP[3].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 415, p_intPosY - 8);
                    }
                    #endregion

                    p_intPosY += 25;
                    #region ֫��䡢������ѹ�����
                    int intGridLines = 7;
                    if (!blnIsNull)
                    {
                        if (m_objPrintInfo.m_objRecordContent.m_objLimbInfoArr != null)
                        {
                            intGridLines = m_objPrintInfo.m_objRecordContent.m_objLimbInfoArr.Length + 2;
                        }
                    }
                    for (int i = 0; i < intGridLines; i++)
                    {
                        p_objGrp.DrawLine(Pens.Black, m_intRecBaseX + 40, p_intPosY + i * 20, m_intRecBaseX + (int)enmRectangleInfo.RightX - (int)enmRectangleInfo.LeftX + 10, p_intPosY + i * 20);
                        if (i == 0)
                        {
                            p_objGrp.DrawString("֫  ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 40 + 80, p_intPosY + 4);
                            p_objGrp.DrawString("����ʱ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 40 + 220, p_intPosY + 4);
                            p_objGrp.DrawString("����ʱ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 40 + 353, p_intPosY + 4);
                            p_objGrp.DrawString("��ʱ��(��)", p_fntNormalText, Brushes.Black, m_intRecBaseX + 40 + 486, p_intPosY + 4);
                            p_objGrp.DrawString("ѹ  ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 40 + 620, p_intPosY + 4);
                        }
                        else if (i < intGridLines - 1)
                        {
                            System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX, p_intPosY + 4 + i * 20), System.Windows.Forms.ButtonState.Flat);
                            p_objGrp.DrawString("��֫", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 15, p_intPosY + 4 + i * 20);
                            System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 60, p_intPosY + 4 + i * 20), System.Windows.Forms.ButtonState.Flat);
                            p_objGrp.DrawString("��֫", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 75, p_intPosY + 4 + i * 20);
                            System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 120, p_intPosY + 4 + i * 20), System.Windows.Forms.ButtonState.Flat);
                            p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 135, p_intPosY + 4 + i * 20);
                            System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 155, p_intPosY + 4 + i * 20), System.Windows.Forms.ButtonState.Flat);
                            p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 170, p_intPosY + 4 + i * 20);
                            p_objGrp.DrawString("mmHg", p_fntNormalText, Brushes.Black, m_intRecBaseX + (int)enmRectangleInfo.RightX - (int)enmRectangleInfo.LeftX - 35, p_intPosY + 2 + i * 20);

                            if (!blnIsNull && m_objPrintInfo.m_objRecordContent.m_objLimbInfoArr != null)
                            {
                                if (m_objPrintInfo.m_objRecordContent.m_objLimbInfoArr[i - 1].m_strCUBITUS == "1")
                                    p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX - 10, p_intPosY + i * 20 - 4);
                                if (m_objPrintInfo.m_objRecordContent.m_objLimbInfoArr[i - 1].m_strLEG == "1")
                                    p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 50, p_intPosY + i * 20 - 4);
                                if (m_objPrintInfo.m_objRecordContent.m_objLimbInfoArr[i - 1].m_strLEFT == "1")
                                    p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 110, p_intPosY + i * 20 - 4);
                                if (m_objPrintInfo.m_objRecordContent.m_objLimbInfoArr[i - 1].m_strRIGHT == "1")
                                    p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 145, p_intPosY + i * 20 - 4);

                                p_objGrp.DrawString(m_objPrintInfo.m_objRecordContent.m_objLimbInfoArr[i - 1].m_strCHARGETIME, p_fntNormalText, Brushes.Black, m_intRecBaseX + 40 + 210, p_intPosY + 4 + i * 20);
                                p_objGrp.DrawString(m_objPrintInfo.m_objRecordContent.m_objLimbInfoArr[i - 1].m_strDEFLATETIME, p_fntNormalText, Brushes.Black, m_intRecBaseX + 40 + 338, p_intPosY + 4 + i * 20);
                                p_objGrp.DrawString(m_objPrintInfo.m_objRecordContent.m_objLimbInfoArr[i - 1].m_strALLTIME, p_fntNormalText, Brushes.Black, m_intRecBaseX + 40 + 466, p_intPosY + 4 + i * 20);
                                p_objGrp.DrawString(m_objPrintInfo.m_objRecordContent.m_objLimbInfoArr[i - 1].m_strPRESS, p_fntNormalText, Brushes.Black, m_intRecBaseX + 40 + 594, p_intPosY + 4 + i * 20);
                            }
                        }
                    }
                    p_objGrp.DrawLine(Pens.Black, m_intRecBaseX + 40 + 200, p_intPosY, m_intRecBaseX + 40 + 200, p_intPosY + (intGridLines - 1) * 20);
                    p_objGrp.DrawLine(Pens.Black, m_intRecBaseX + 40 + 328, p_intPosY, m_intRecBaseX + 40 + 328, p_intPosY + (intGridLines - 1) * 20);
                    p_objGrp.DrawLine(Pens.Black, m_intRecBaseX + 40 + 456, p_intPosY, m_intRecBaseX + 40 + 456, p_intPosY + (intGridLines - 1) * 20);
                    p_objGrp.DrawLine(Pens.Black, m_intRecBaseX + 40 + 584, p_intPosY, m_intRecBaseX + 40 + 584, p_intPosY + (intGridLines - 1) * 20);
                    #endregion

                    p_intPosY += (intGridLines - 1) * 20 + 8;
                    #region ����Foley�����
                    p_objGrp.DrawString("����Foley����ܣ�", p_fntNormalText, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 140, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 155, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 185, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("��������", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 200, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 275, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("������", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 290, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 350, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("˫ǻ", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 365, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 410, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("��ǻ", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 425, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 470, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("������", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 485, p_intPosY);

                    if (!blnIsNull)
                    {
                        if (m_objPrintInfo.m_objRecordContent.m_strFOLEY[0].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 130, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strFOLEY[1].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 175, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strFOLEY[2].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 265, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strFOLEY[3].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 370, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strFOLEY[4].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 400, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strFOLEY[5].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 460, p_intPosY - 8);
                        p_objGrp.DrawString(m_objPrintInfo.m_objRecordContent.m_strOTHERFOLEY_RIGHT, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 530, p_intPosY);
                    }
                    #endregion

                    p_intPosY += 25;
                    #region Ƥ����ճĤ����
                    p_objGrp.DrawString("Ƥ����ճĤ������", p_fntNormalText, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 140, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("2%����", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 155, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 215, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("4%����", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 230, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 290, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("75%�ƾ�", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 305, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 375, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("���ԭҺ", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 390, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 465, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("���ϡ��Һ", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 480, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 570, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("0.1%ϴ��̩Һ", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 585, p_intPosY);

                    if (!blnIsNull)
                    {
                        if (m_objPrintInfo.m_objRecordContent.m_strSKINANTISEPSIS[0].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 130, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strSKINANTISEPSIS[1].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 205, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strSKINANTISEPSIS[2].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 280, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strSKINANTISEPSIS[3].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 365, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strSKINANTISEPSIS[4].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 455, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strSKINANTISEPSIS[5].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 560, p_intPosY - 8);
                    }

                    p_intPosY += 25;
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 140, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("������", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 155, p_intPosY);
                    if (!blnIsNull)
                    {
                        if (m_objPrintInfo.m_objRecordContent.m_strSKINANTISEPSIS[6].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 130, p_intPosY - 8);
                        p_objGrp.DrawString(m_objPrintInfo.m_objRecordContent.m_strOTHERSKINANTISEPSIS_RIGHT, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 200, p_intPosY);
                    } 
                    #endregion

                    p_intPosY += 25;
                    #region Ѫ��Ʒ
                    p_objGrp.DrawString("Ѫ��Ʒ��", p_fntNormalText, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 70, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("ȫѪ " + (blnIsNull ? "" : m_objPrintInfo.m_objRecordContent.m_strWHOLEBLOOD_RIGHT), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 85, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 185, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("ml", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 160, p_intPosY);
                    p_objGrp.DrawString("��ϸ��" + (blnIsNull ? "" : m_objPrintInfo.m_objRecordContent.m_strREDCELL_RIGHT), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 200, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 320, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("��λ", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 280, p_intPosY);
                    p_objGrp.DrawString("Ѫ�� " + (blnIsNull ? "" : m_objPrintInfo.m_objRecordContent.m_strPLASM_RIGHT), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 335, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 435, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("ml", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 410, p_intPosY);
                    p_objGrp.DrawString("������Ѫ " + (blnIsNull ? "" : m_objPrintInfo.m_objRecordContent.m_strSELFBLOOD_RIGHT), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 450, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 575, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("ml", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 555, p_intPosY);
                    p_objGrp.DrawString("ѪС�� " + (blnIsNull ? "" : m_objPrintInfo.m_objRecordContent.m_strPLATELET_RIGHT), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 590, p_intPosY);

                    p_intPosY += 25;
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 70, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("����� " + (blnIsNull ? "" : m_objPrintInfo.m_objRecordContent.m_strCOLDDEPOSIT_RIGHT), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 85, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 210, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("������" + (blnIsNull ? "" : m_objPrintInfo.m_objRecordContent.m_strOTHERBLOOD_RIGHT), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 225, p_intPosY); 
                    #endregion

                    p_intPosY += 25;
                    #region ����Һ��������������
                    p_objGrp.DrawString("����Һ����: " + (blnIsNull ? "      " : m_objPrintInfo.m_objRecordContent.m_strINLIQUID_RIGHT) + "ml", p_fntNormalText, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    p_objGrp.DrawString("����������" + (blnIsNull ? "      " : m_objPrintInfo.m_objRecordContent.m_strPISS_RIGHT) + "ml", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 280, p_intPosY); 
                    #endregion

                    p_intPosY += 25;
                    #region �˿����������
                    p_objGrp.DrawString("�˿������������", p_fntNormalText, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 150, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 165, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 205, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 220, p_intPosY);

                    if (!blnIsNull)
                    {
                        if (m_objPrintInfo.m_objRecordContent.m_strDRAIN[0].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 140, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strDRAIN[1].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 195, p_intPosY - 8);
                    }
                    #endregion

                    p_intPosY += 25;
                    #region ȫ��Ƥ�����
                    p_objGrp.DrawString("ȫ��Ƥ�������", p_fntNormalText, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    p_objGrp.DrawString("����ǰ��", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 140, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 210, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("����", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 225, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 275, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("�쳣", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 290, p_intPosY);
                    p_objGrp.DrawString("Ƥ�����������" + (blnIsNull ? "" : m_objPrintInfo.m_objRecordContent.m_strSKINBEFOREOP_DESC_RIGHT + "    " + m_objPrintInfo.m_objRecordContent.m_strSKINBEFOREOP_DESC2_RIGHT),
                        p_fntNormalText, Brushes.Black, m_intPatientInfoX + 340, p_intPosY);

                    if (!blnIsNull)
                    {
                        if (m_objPrintInfo.m_objRecordContent.m_strSKINBEFOREOP[0].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 200, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strSKINBEFOREOP[1].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 265, p_intPosY - 8);
                    }

                    p_intPosY += 25;
                    p_objGrp.DrawString("������", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 140, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 210, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("����", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 225, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 275, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("�쳣", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 290, p_intPosY);
                    p_objGrp.DrawString("Ƥ�����������", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 340, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 445, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("ͬ��ǰ", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 460, p_intPosY);
                    p_objGrp.DrawString((blnIsNull ? "" : m_objPrintInfo.m_objRecordContent.m_strSKINAFTEROP_DESC_RIGHT), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 525, p_intPosY);

                    if (!blnIsNull)
                    {
                        if (m_objPrintInfo.m_objRecordContent.m_strSKINAFTEROP[0].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 200, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strSKINAFTEROP[1].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 265, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strSKINAFTEROP[2].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 435, p_intPosY - 8);
                    }
                    #endregion

                    p_intPosY += 25;
                    #region �걾
                    p_objGrp.DrawString("�걾��", p_fntNormalText, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 50, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("���没����", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 65, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 175, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("������Ƭ", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 190, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 265, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("ϸ������", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 280, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 355, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 370, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 400, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("����: " + (blnIsNull ? "" : m_objPrintInfo.m_objRecordContent.m_strOTHERSAMPLE_RIGHT), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 415, p_intPosY);

                    if (!blnIsNull)
                    {
                        if (m_objPrintInfo.m_objRecordContent.m_strSAMPLE[0].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 40, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strSAMPLE[1].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 165, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strSAMPLE[2].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 255, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strSAMPLE[3].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 345, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strSAMPLE[4].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 390, p_intPosY - 8);
                    } 
                    #endregion

                    p_intPosY += 25;
                    p_objGrp.DrawString("�޾�����⣺" + (blnIsNull ? "" : m_objPrintInfo.m_objRecordContent.m_strAXENICBAG_RIGHT), p_fntNormalText, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    p_intPosY += 25;
                    p_objGrp.DrawString("ֲ�����ʾ��" + (blnIsNull ? "" : m_objPrintInfo.m_objRecordContent.m_strEMBEDDED_RIGHT), p_fntNormalText, Brushes.Black, m_intPatientInfoX, p_intPosY);

                    p_intPosY += 25;
                    p_objGrp.DrawLine(Pens.Black, m_intRecBaseX, p_intPosY, m_intRecBaseX + (int)enmRectangleInfo.RightX - (int)enmRectangleInfo.LeftX + 10, p_intPosY);
                    p_objGrp.DrawString("��  ��  ��  ��", p_fntNormalText, Brushes.Black, new RectangleF(m_intRecBaseX + 10, p_intPosY - 310, 20, 120));

                    m_blnIsFirstPrint = false;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intPatientInfoX, p_intPosY, p_objGrp);

                    p_intPosY += 30;

                    m_intTimes++;
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

                m_blnIsFirstPrint = true;

                m_blnHaveMoreLine = true;

                m_intTimes = 0;
            }
        } 
        #endregion

        #region ������
        /// <summary>
        /// ������
        /// </summary>
        private class clsPrintNurseAfterOP : clsPrintInPatientCaseInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            private bool m_blnIsFirstPrint = true;
            private int m_intTimes = 0;
            private Font fntCheck = new Font("SimSun", 18);
            private clsPublicControlPaint m_objCPaint = new clsPublicControlPaint();

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    bool blnIsNull = false;
                    if (m_objPrintInfo == null || m_objPrintInfo.m_objRecordContent == null)
                    {
                        blnIsNull = true;
                    }
                    p_intPosY += 5;
                    #region �����ͻ�
                    p_objGrp.DrawString("�����ͻأ�", p_fntNormalText, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 80, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("��������", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 95, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 195, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("ICU", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 210, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 250, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("����", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 265, p_intPosY);

                    if (!blnIsNull)
                    {
                        if (m_objPrintInfo.m_objRecordContent.m_strAFTEROPSEND[0].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 70, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strAFTEROPSEND[1].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 185, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strAFTEROPSEND[2].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 240, p_intPosY - 8);
                    } 
                    #endregion

                    p_intPosY += 25;
                    #region ������ѹ��λ
                    p_objGrp.DrawString("������ѹ��λ��", p_fntNormalText, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    p_intPosY += 25;
                    p_objGrp.DrawString("Ƥ�������ԣ�", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 40, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 150, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 165, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 195, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 215, p_intPosY);
                    p_objGrp.DrawString("�˿���Ѫ��", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 270, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 350, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 365, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 395, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 415, p_intPosY);

                    if (!blnIsNull)
                    {
                        if (m_objPrintInfo.m_objRecordContent.m_strSKINFULL[0].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 140, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strSKINFULL[1].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 185, p_intPosY - 8);

                        if (m_objPrintInfo.m_objRecordContent.m_strSEEPBLOOD[0].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 340, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strSEEPBLOOD[1].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 385, p_intPosY - 8);
                    }  
                    #endregion

                    p_intPosY += 25;
                    #region �������
                    p_objGrp.DrawString("������ӣ�", p_fntNormalText, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    p_intPosY += 25;
                    p_objGrp.DrawString("����������", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 40, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 120, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("ƽ��", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 135, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 175, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("�쳣", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 190, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 230, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("����", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 245, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 285, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("�־壨", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 300, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 350, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 365, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 395, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 410, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 440, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("�أ�", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 455, p_intPosY);

                    if (!blnIsNull)
                    {
                        if (m_objPrintInfo.m_objRecordContent.m_strLIFTSIGNAFTEROP[0].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 110, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strLIFTSIGNAFTEROP[1].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 165, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strLIFTSIGNAFTEROP[2].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 220, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strLIFTSIGNAFTEROP[3].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 275, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strLIFTSIGNAFTEROP[4].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 340, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strLIFTSIGNAFTEROP[5].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 385, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strLIFTSIGNAFTEROP[6].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 430, p_intPosY - 8);
                    } 
                    #endregion

                    p_intPosY += 25;
                    #region ������
                    p_objGrp.DrawString("�����ܣ�", p_fntNormalText, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 70, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 85, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 125, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 140, p_intPosY);

                    if (!blnIsNull)
                    {
                        if (m_objPrintInfo.m_objRecordContent.m_strGUIDING[0].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 60, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strGUIDING[1].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 115, p_intPosY - 8);
                    } 
                    #endregion

                    p_intPosY += 25;
                    #region ��������
                    p_objGrp.DrawString("����������", p_fntNormalText, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 80, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("����", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 95, p_intPosY);
                    System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp, m_objCPaint.m_rtgGetControlPaint(m_intPatientInfoX + 145, p_intPosY), System.Windows.Forms.ButtonState.Flat);
                    p_objGrp.DrawString("δ��", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 160, p_intPosY);

                    if (!blnIsNull)
                    {
                        if (m_objPrintInfo.m_objRecordContent.m_strHEALTHEDU[0].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 70, p_intPosY - 8);
                        if (m_objPrintInfo.m_objRecordContent.m_strHEALTHEDU[1].ToString() == "1")
                            p_objGrp.DrawString("��", fntCheck, Brushes.Black, m_intPatientInfoX + 135, p_intPosY - 8);
                    }  
                    #endregion

                    m_blnIsFirstPrint = false;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intPatientInfoX, p_intPosY, p_objGrp);

                    p_intPosY += 30;

                    m_intTimes++;
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

                m_blnIsFirstPrint = true;

                m_blnHaveMoreLine = true;

                m_intTimes = 0;
            }
        } 
        #endregion

        #region �����¼
        /// <summary>
        /// �����¼
        /// </summary>
        private class clsPrintNurseRecordContent : clsPrintInPatientCaseInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private bool m_blnIsFirstPrint = true;

            private int m_intTimes = 0;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                bool blnIsNull = false;
                if (m_objPrintInfo == null || m_objPrintInfo.m_objRecordContent == null)
                {
                    blnIsNull = true;
                }

                if (m_blnIsFirstPrint)
                {
                    p_intPosY += 25;

                    p_objGrp.DrawString("�����¼��", p_fntNormalText, Brushes.Black, m_intPatientInfoX, p_intPosY);

                    p_intPosY += 25;

                    if (!blnIsNull)
                    {
                        if (string.IsNullOrEmpty(m_objPrintInfo.m_objRecordContent.m_strOPRECORD))
                        {
                            m_blnHaveMoreLine = false;
                            return;
                        }
                    }
                    else
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((blnIsNull ? "" : m_objPrintInfo.m_objRecordContent.m_strOPRECORD), (blnIsNull ? "<root />" : m_objPrintInfo.m_objRecordContent.m_strOPRECORDXML), m_dtmFirstPrintTime, !blnIsNull);
                    m_mthAddSign2("�����¼��", m_objPrintContext.m_ObjModifyUserArr);

                    m_blnIsFirstPrint = false;

                }

                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intPatientInfoX, p_intPosY, p_objGrp);

                    p_intPosY += 20;

                    m_intTimes++;
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

                m_blnIsFirstPrint = true;

                m_blnHaveMoreLine = true;

                m_intTimes = 0;
            }
        } 
        #endregion

        #region ��ʿǩ��
        /// <summary>
        /// ��ʿǩ��
        /// </summary>
        private class clsPrintNurseSign : clsPrintInPatientCaseInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            private bool m_blnIsFirstPrint = true;
            private int m_intTimes = 0;
            private Font fntCheck = new Font("SimSun", 18);
            private clsPublicControlPaint m_objCPaint = new clsPublicControlPaint();

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    bool blnIsNull = false;
                    if (m_objPrintInfo == null || m_objPrintInfo.m_objRecordContent == null)
                    {
                        blnIsNull = true;
                    }

                    p_intPosY = (int)enmRectangleInfo.BottomY + 30;
                    p_objGrp.DrawLine(Pens.Black, m_intRecBaseX, p_intPosY, m_intRecBaseX + (int)enmRectangleInfo.RightX - (int)enmRectangleInfo.LeftX + 10, p_intPosY);
                    p_objGrp.DrawString("��  ��  ��  ��", p_fntNormalText, Brushes.Black, new RectangleF(m_intRecBaseX + 10, p_intPosY - 250, 20, 120));

                    p_intPosY += 5;
                    p_objGrp.DrawString("ϴ�ֻ�ʿǩ��:" + (blnIsNull ? "" : m_strGetSingArr("m_lsvWashNurseSign", m_objPrintInfo.m_objRecordContent.objSignerArr)), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 30, p_intPosY);
                    p_objGrp.DrawString("Ѳ�ػ�ʿǩ��:" + (blnIsNull ? "" : m_strGetSingArr("m_lsvCircuitNurseSign", m_objPrintInfo.m_objRecordContent.objSignerArr)), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 340, p_intPosY);
                    m_blnIsFirstPrint = false;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intPatientInfoX, p_intPosY, p_objGrp);

                    p_intPosY += 30;

                    m_intTimes++;
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

                m_blnIsFirstPrint = true;

                m_blnHaveMoreLine = true;

                m_intTimes = 0;
            }
        } 
        #endregion
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

            e.Graphics.DrawString("��      ҳ", fntHeader, Brushes.Black, 385 - fltOffsetX, e.PageBounds.Height - 70);
            e.Graphics.DrawString(m_intCurrentPage.ToString(), fntHeader, Brushes.Black, 425 - fltOffsetX, e.PageBounds.Height - 70);
        }
        //��ӡ�߿�
        private void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font fntTitle = new Font("SimSun", 18, FontStyle.Bold);
            Font fntHeader = new Font("SimSun", 12);

            e.Graphics.DrawRectangle(Pens.Black, m_intRecBaseX, 135, (int)enmRectangleInfo.RightX - (int)enmRectangleInfo.LeftX + 10, e.PageBounds.Height - 220);
            e.Graphics.DrawLine(Pens.Black, m_intRecBaseX + 40, 135, m_intRecBaseX + 40, 135 + e.PageBounds.Height - 220);
        }
        /// <summary>
        /// �������ֲ���
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, m_fotHeaderFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_HospitalName));

            e.Graphics.DrawString("��  ��  ��  ��  ��  ¼", m_fotTitleFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_Name_Title));

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
    }
}
