using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.Utility.Controls;
using System.Drawing;

namespace iCare
{
    /// <summary>
    /// ICU�������Ĵ�ӡ������,Jacky-2003-9-11
    /// </summary>
    public class clsICUBreathPrintTool : infPrintRecord
    {
        private bool m_blnIsFromDataSource = true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
        private bool m_blnWantInit = true;
        private clsRecordsDomain m_objRecordsDomain;
        private clsPrintInfo_ICUBreath m_objPrintInfo;

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
            m_objPrintInfo = new clsPrintInfo_ICUBreath();
            m_objPrintInfo.m_strInPatentID = m_objPatient != null ? m_objPatient.m_StrInPatientID : "";
            m_objPrintInfo.m_strPatientName = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrFirstName : "";
            m_objPrintInfo.m_strSex = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrSex : "";
            m_objPrintInfo.m_strAge = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrAge : "";
            m_objPrintInfo.m_strBedName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName : "";
            m_objPrintInfo.m_strDeptName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjDept.m_StrDeptName : "";
            m_objPrintInfo.m_strAreaName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName : "";
            m_objPrintInfo.m_dtmInPatientDate = p_dtmInPatientDate;
            m_objPrintInfo.m_dtmOpenDate = p_dtmOpenDate;
            m_objPrintInfo.m_strWeight = m_objPatient != null ? "50" : "";//����
            m_objPrintInfo.m_strHISInPatientID = m_objPatient != null ? m_objPatient.m_StrHISInPatientID : "";
            m_objPrintInfo.m_dtmHISInPatientDate = m_objPatient != null ? m_objPatient.m_DtmSelectedHISInDate : DateTime.MinValue;
        }

        /// <summary>
        /// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���(�������ݿ��ȡʱҪ����.)
        /// </summary>
        public void m_mthInitPrintContent()
        {
            if (m_objPrintInfo == null)
            {
                clsPublicFunction.ShowInformationMessageBox("����m_mthInitPrintContent֮ǰ�����ȵ���m_mthSetPrintInfo����");
                return;
            }
            if (m_objPrintInfo.m_strInPatentID == "")
                return;
            m_objRecordsDomain = new clsRecordsDomain(enmRecordsType.ICUBreath);

            long lngRes = m_objRecordsDomain.m_lngGetPrintInfo(m_objPrintInfo.m_strInPatentID, m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), out m_objPrintInfo.m_objTransDataArr, out m_objPrintInfo.m_dtmFirstPrintDateArr, out m_objPrintInfo.m_blnIsFirstPrintArr);
            if (lngRes <= 0)
                return;

            //����¼ʱ��(CreateDate)���� 
            m_mthSortTransData(ref m_objPrintInfo.m_objTransDataArr);
            //���ñ����ݵ���ӡ��
            m_mthSetPrintContent(m_objPrintInfo.m_objTransDataArr, m_objPrintInfo.m_dtmFirstPrintDateArr);
            m_objPrintInfo.m_objPrintDataArr = m_objPrintDataArr;

            m_blnWantInit = false;
        }

        /// <summary>
        /// ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
        /// </summary>
        /// <param name="p_objPrintContent">��ӡ����</param>
        public void m_mthSetPrintContent(object p_objPrintContent)
        {
            if (p_objPrintContent.GetType().Name != "clsPrintInfo_ICUBreath")
            {
                //clsPublicFunction.ShowInformationMessageBox("��������");
            }
            m_blnIsFromDataSource = false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ
            m_objPrintInfo = (clsPrintInfo_ICUBreath)p_objPrintContent;
            m_objPrintDataArr = m_objPrintInfo.m_objPrintDataArr;

            m_blnWantInit = false;
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
            if (m_objPrintInfo.m_objPrintDataArr.Length == 0 || m_objPrintInfo.m_objTransDataArr.Length == 0)
                return null;
            else
                return m_objPrintInfo;
        }

        /// <summary>
        /// ��ʼ����ӡ����,��������ն��󼴿�.
        /// </summary>
        public void m_mthInitPrintTool(object p_objArg)
        {
            m_fotTitleFont = new Font("SimSun", 20, FontStyle.Bold);
            m_fotHeaderFont = new Font("SimSun", 12f);
            m_fotSmallFont = new Font("SimSun", 10.5f);
            m_fotTinyFont = new Font("SimSun", 8f);
            m_GridPen = new Pen(Color.Black, 1);
            m_slbBrush = new SolidBrush(Color.Black);

            m_objPageSetting = new clsPrintPageSettingForRecord();

        }

        /// <summary>
        /// �ͷŴ�ӡ����
        /// </summary>
        public void m_mthDisposePrintTools(object p_objArg)
        {
            m_objPageSetting = null;
            m_fotTitleFont.Dispose();
            m_fotHeaderFont.Dispose();
            m_fotSmallFont.Dispose();
            m_fotTinyFont.Dispose();
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
            if (m_blnIsFromDataSource == false || m_objPrintInfo.m_strInPatentID == "") return;
            //�����ӡ�ɹ�������������Ҫ���µ�ʱ�䣬����У�����ʱ�䡣 
            if (!((PrintEventArgs)p_objPrintArg).Cancel && m_objPrintInfo.m_blnIsFirstPrintArr != null)
            {
                ArrayList arlRecordType = new ArrayList();
                ArrayList arlOpenDate = new ArrayList();
                int intUpdateIndex = -1;//��û���κμ�¼
                for (int i = 0; i < m_objPrintInfo.m_blnIsFirstPrintArr.Length; i++)
                {
                    if (m_objPrintInfo.m_blnIsFirstPrintArr[i])
                    {
                        //���¼�¼��ֻ��ʹ���µ��״δ�ӡʱ����Ϊ��Ч�����������
                        //��ż�¼����
                        if (m_objPrintInfo.m_objTransDataArr[i] != null)
                        {
                            arlRecordType.Add(m_objPrintInfo.m_objTransDataArr[i].m_intFlag);
                            //��ż�¼��OpenDate
                            arlOpenDate.Add(m_objPrintInfo.m_objTransDataArr[i].m_objRecordContent.m_dtmOpenDate);
                            intUpdateIndex = i;
                        }
                    }
                }

                if (intUpdateIndex >= 0)
                {
                    m_objRecordsDomain.m_lngUpdateFirstPrintDate(m_objPrintInfo.m_strInPatentID, m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), (int[])arlRecordType.ToArray(typeof(int)), (DateTime[])arlOpenDate.ToArray(typeof(DateTime)), m_objPrintInfo.m_dtmFirstPrintDateArr[intUpdateIndex]);
                }
                m_objPrintInfo.m_objTransDataArr = null;
                m_objPrintInfo.m_blnIsFirstPrintArr = null;
            }
            m_mthEndPrintSub((PrintEventArgs)p_objPrintArg);
        }


        /// <summary>
        /// ����¼˳��(CreateDate)�������p_objTansDataInfoArr����
        /// </summary>
        /// <param name="p_objTansDataInfoArr"></param>
        private void m_mthSortTransData(ref clsTransDataInfo[] p_objTansDataInfoArr)
        {
            ArrayList m_arlSort = new ArrayList(p_objTansDataInfoArr);
            m_arlSort.Sort();
            p_objTansDataInfoArr = (clsTransDataInfo[])m_arlSort.ToArray(typeof(clsTransDataInfo));
        }

        #region �йش�ӡ������
        /// <summary>
        /// ��ǰ�е�Y����
        /// </summary>
        private int m_intPosY = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark3;
        /// <summary>
        /// ÿ�������еĸ߶�
        /// </summary>
        int intTempDeltaY = 38;
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
        /// ��С������
        /// </summary>
        private Font m_fotTinyFont;
        /// <summary>
        /// �߿򻭱�
        /// </summary>
        private Pen m_GridPen;
        /// <summary>
        /// ˢ��
        /// </summary>
        private SolidBrush m_slbBrush;
        /// <summary>
        /// ��¼��ӡ���ڼ�ҳ
        /// </summary>
        private int m_intNowPage = 1;
        /// <summary>
        /// ��ǰ��ӡ�ļ�¼�����
        /// </summary>
        private int m_intCurrentRecord = 0;
        /// <summary>
        /// �ɼ�¼����,׼����ӡһ���¼�¼
        /// </summary>
        bool m_blnBeginPrintNewRecord = true;

        /// <summary>
        /// ����Ҫ������ʷ�ۼ�����ǰ��¼����
        /// </summary>
        private string[][] m_strValueArr;

        /// <summary>
        /// ��ǰ��¼�����������޸ĵĴε�����
        /// </summary>
        private int m_intNowRowInOneRecord = 0;

        /// <summary>
        /// ��ȡ�������
        /// </summary>
        private clsPrintPageSettingForRecord m_objPageSetting;

        /// <summary>
        /// ���ӵ���Ϣ
        /// </summary>
        private enum enmRecordRectangleInfo
        {//A3ֽ�ţ����� ��1620*1024 Size
            /// <summary>
            /// ��ѿ��
            /// </summary>
            PerformWith = 45,
            /// <summary>
            /// ���ӵĶ���
            /// </summary>
            TopY = 200,
            /// <summary>
            /// ��ͷ��һ���ָ���
            /// </summary>
            RowsMark1 = 22,
            /// <summary>
            /// ��ͷ�ڶ����ָ���
            /// </summary>
            RowsMark2 = 42,
            /// <summary>
            /// ��ͷ�������ָ��ߣ������û����ݵ�����ߣ�
            /// </summary>
            RowsMark3 = 180,
            /// <summary>
            /// ��ͷ�������ָ���
            /// </summary>
            RowsMarkBreathSound = 90,
            ///<summary>
            /// ���ӵ����
            /// </summary>
            LeftX = 30,
            /// <summary>
            /// ���ӵ��Ҷ�
            /// </summary>
            RightX = 1620 - 35,
            /// <summary>
            /// ����ÿ�еĲ���
            /// </summary>
            RowStep = 38,
            /// <summary>
            /// ���ӵ�����
            /// </summary>
            RowLinesNum = 17,
            /// <summary>
            /// �����ڸ�������Ը��Ӷ��˵Ĵ�ֱƫ��
            /// </summary>
            VOffSet = 20,
            /// <summary>
            /// �е���Ŀ
            /// </summary>
            ColumnsNum = 19,
            /// <summary>
            /// ��һ�������(X),ʱ�䣨����ߣ�
            /// </summary>			
            ColumnsMark1 = 75,

            /// <summary>
            /// �ڶ��������(X)��ͨ����ʽ������ߣ�
            /// </summary>
            ColumnsMark2 = ColumnsMark1 + PerformWith,//120

            /// <summary>
            /// ��3�������(X)�������� ������ߣ�
            /// </summary>
            ColumnsMark3 = ColumnsMark2 + 2 * (int)PerformWith - 20,//210

            /// <summary>
            /// ������ �ң�����ߣ�
            /// </summary>
            ColumnsMark4 = ColumnsMark3 + PerformWith,//250

            /// <summary>
            /// �����ȣ�����ߣ�
            /// </summary>
            ColumnsMark5 = ColumnsMark4 + PerformWith,//295,

            /// <summary>
            /// ����ѹ��������ߣ�
            /// </summary>
            ColumnsMark6 = ColumnsMark5 + PerformWith - 15,//340,

            /// <summary>
            /// TIDAL_VOLUME������ߣ�
            /// </summary>
            ColumnsMark7 = ColumnsMark6 + PerformWith - 15,//385,

            /// <summary>
            /// RATE������ߣ�
            /// </summary>
            ColumnsMark8 = ColumnsMark7 + PerformWith - 5,//425,

            /// <summary>
            /// PEAK_FLOW������ߣ�
            /// </summary>
            ColumnsMark9 = ColumnsMark8 + PerformWith - 5,//470,

            /// <summary>
            /// O2������ߣ�
            /// </summary>
            ColumnsMark10 = ColumnsMark9 + PerformWith - 5,//515,

            /// <summary>
            /// PS������ߣ�
            /// </summary>
            ColumnsMark11 = ColumnsMark10 + PerformWith,//560,

            /// <summary>
            /// ASSIST_SENSITIVITY������ߣ�
            /// </summary>
            ColumnsMark12 = ColumnsMark11 + PerformWith,//605,

            /// <summary>
            /// INSPIRATORY_PAUSE������ߣ�
            /// </summary>
            ColumnsMark13 = ColumnsMark12 + PerformWith,//490,

            /// <summary>
            /// MMV_LEVEL������ߣ�
            /// </summary>
            ColumnsMark14 = ColumnsMark13 + PerformWith,//520,

            /// <summary>
            /// COMPLIANCE_COMP������ߣ�
            /// </summary>
            ColumnsMark15 = ColumnsMark14 + PerformWith,//550,

            /// <summary>
            /// INSPIRATORY_TIME������ߣ�
            /// </summary>
            ColumnsMark16 = ColumnsMark15 + PerformWith,//580,

            /// <summary>
            /// INSPIRATORY_PRESSURE������ߣ�
            /// </summary>
            ColumnsMark17 = ColumnsMark16 + PerformWith,//610,

            /// <summary>
            /// BASE_FLOW������ߣ�
            /// </summary>
            ColumnsMark18 = ColumnsMark17 + PerformWith,//640,

            /// <summary>
            /// FLOW_TRIGGER������ߣ�
            /// </summary>
            ColumnsMark19 = ColumnsMark18 + PerformWith,//667,

            /// <summary>
            /// PRESSURE_SLOPE������ߣ�
            /// </summary>
            ColumnsMark20 = ColumnsMark19 + PerformWith,//670,

            /// <summary>
            /// PEEP������ߣ�
            /// </summary>
            ColumnsMark21 = ColumnsMark20 + PerformWith,//700,
            /// <summary>
            /// TIDAL_VOL������ߣ�
            /// </summary>
            ColumnsMark22 = ColumnsMark21 + PerformWith,//730,
            /// <summary>
            /// TOTAL_MV������ߣ�
            /// </summary>
            ColumnsMark23 = ColumnsMark22 + PerformWith,//775,
            /// <summary>
            /// SPONT_MV������ߣ�
            /// </summary>
            ColumnsMark24 = ColumnsMark23 + PerformWith,//820,
            /// <summary>
            /// TOTAL������ߣ�
            /// </summary>
            ColumnsMark25 = ColumnsMark24 + PerformWith,//865,
            /// <summary>
            /// SPONT������ߣ�
            /// </summary>
            ColumnsMark26 = ColumnsMark25 + PerformWith,//910,
            /// <summary>
            /// I_E_RATIO������ߣ�
            /// </summary>
            ColumnsMark27 = ColumnsMark26 + PerformWith,//955,
            /// <summary>
            /// Ti������ߣ�
            /// </summary>
            ColumnsMark28 = ColumnsMark27 + PerformWith,//1000,
            /// <summary>
            /// MMV������ߣ�
            /// </summary>
            ColumnsMark29 = ColumnsMark28 + PerformWith,//1045,
            /// <summary>
            /// PEAR������ߣ�
            /// </summary>
            ColumnsMark30 = ColumnsMark29 + PerformWith + 5,//1090,
            /// <summary>
            /// MEAN������ߣ�
            /// </summary>
            ColumnsMark31 = ColumnsMark30 + PerformWith,//1135,
            /// <summary>
            /// PLATEAU������ߣ�
            /// </summary>
            ColumnsMark32 = ColumnsMark31 + PerformWith,//1180,	
            /// <summary>
            /// ǩ��������ߣ�
            /// </summary>
            ColumnsMark33 = ColumnsMark32 + PerformWith,//1230	
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
            Dept_Name_Title,
            Dept_Name,
            BedNo_Title,
            BedNo,
            Weight_Title,
            Weight,

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
                        m_fReturnPoint = new PointF(720f, 60f);
                        break;
                    case (int)enmItemDefination.Page_Name_Title:
                        m_fReturnPoint = new PointF(543f, 100f);
                        break;
                    case (int)enmItemDefination.Name_Title:
                        m_fReturnPoint = new PointF(213f, 150f);
                        break;
                    case (int)enmItemDefination.Name:
                        m_fReturnPoint = new PointF(263f, 150f);
                        break;

                    case (int)enmItemDefination.Sex_Title:
                        m_fReturnPoint = new PointF(550f, 150f);
                        break;
                    case (int)enmItemDefination.Sex:
                        m_fReturnPoint = new PointF(600f, 150f);
                        break;

                    case (int)enmItemDefination.Age_Title:
                        m_fReturnPoint = new PointF(380f, 150f);
                        break;
                    case (int)enmItemDefination.Age:
                        m_fReturnPoint = new PointF(430f, 150f);
                        break;

                    case (int)enmItemDefination.Dept_Name_Title:
                        m_fReturnPoint = new PointF(480f, 150f);
                        break;
                    case (int)enmItemDefination.Dept_Name:
                        m_fReturnPoint = new PointF(530f, 150f);
                        break;

                    case (int)enmItemDefination.BedNo_Title:
                        m_fReturnPoint = new PointF(700f, 150f);
                        break;
                    case (int)enmItemDefination.BedNo:
                        m_fReturnPoint = new PointF(750f, 150f);
                        break;

                    case (int)enmItemDefination.Weight_Title:
                        m_fReturnPoint = new PointF(850f, 150f);
                        break;
                    case (int)enmItemDefination.Weight:
                        m_fReturnPoint = new PointF(900f, 150f);
                        break;

                    case (int)enmItemDefination.InPatientID_Title:
                        m_fReturnPoint = new PointF(1050f, 150f);
                        break;
                    case (int)enmItemDefination.InPatientID:
                        m_fReturnPoint = new PointF(1120f, 150f);
                        break;
                    case (int)enmItemDefination.Print_Date_Title:
                        m_fReturnPoint = new PointF(1230f, 150f);
                        break;
                    case (int)enmItemDefination.Print_Date:
                        m_fReturnPoint = new PointF(1310f, 150f);
                        break;

                    default:
                        m_fReturnPoint = new PointF(400f, 400f);
                        break;

                }
                return m_fReturnPoint;
            }
        }

        #endregion

        #endregion

        #region ��ӡ
        private clsICUBreath[] m_objPrintDataArr;
        /// <summary>
        /// ���ô�ӡ���ݡ�
        /// </summary>
        /// <param name="p_objTransDataArr"></param>
        /// <param name="p_dtmFirstPrintDate"></param>
        private void m_mthSetPrintContent(clsTransDataInfo[] p_objTransDataArr,
            DateTime[] p_dtmFirstPrintDate)
        {
            if (p_objTransDataArr == null || p_dtmFirstPrintDate == null || p_objTransDataArr.Length != p_dtmFirstPrintDate.Length)
            {
                clsPublicFunction.ShowInformationMessageBox("��ӡ��������!");
                return;
            }
            ArrayList m_arlTemp = new ArrayList();
            for (int i1 = 0; i1 < p_objTransDataArr.Length; i1++)
            {
                //if(p_objTransDataArr[i1].m_intFlag==(int)enmRecordsType.ICUBreath)
                m_arlTemp.Add(p_objTransDataArr[i1]);

            }
            m_objPrintDataArr = (clsICUBreath[])m_arlTemp.ToArray(typeof(clsICUBreath));


        }

        // ��ӡ��ʼ���ڴ�ӡҳ֮ǰ�Ĳ���
        private void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
        {

        }

        // ��ӡҳ��ÿ��ӡһҳ������һ�Σ��Ǵ�ӡ�������õĺ�����
        private void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
        {
            try
            {
                p_objPrintPageArg.HasMorePages = false;
                m_mthPrintTitleInfo(p_objPrintPageArg);
                m_mthPrintRectangleInfo(p_objPrintPageArg);
                m_mthPrintHeaderInfo(p_objPrintPageArg);

                if (m_objPrintInfo.m_strInPatentID == "" || m_objPrintDataArr == null || m_objPrintDataArr.Length == 0)
                    return;
                while (m_intCurrentRecord < m_objPrintDataArr.Length)
                {
                    if (m_intCurrentRecord == 0)
                        m_intSetPrintOneValueRows(p_objPrintPageArg, ref m_intPosY);

                    if (m_blnCheckPageChange(m_intPosY + intTempDeltaY, p_objPrintPageArg) == true)
                        return;
                    m_blnBeginPrintNewRecord = m_blnPrintOneValue(p_objPrintPageArg, m_intPosY);

                    if (m_blnBeginPrintNewRecord)
                    {
                        m_intCurrentRecord++;

                        m_mthPrintOneHorizontalLine(p_objPrintPageArg, m_intPosY);

                        int intMaxRows = m_intSetPrintOneValueRows(p_objPrintPageArg, ref m_intPosY);
                        if (m_blnCheckPageChange(m_intPosY + intMaxRows * intTempDeltaY, p_objPrintPageArg) == true)
                            return;
                    }

                }
                m_mthPrintVLines(p_objPrintPageArg, m_intPosY);
                //ҳ��//////////////////////////////////////////////////////////////
                p_objPrintPageArg.Graphics.DrawString("����" + m_intNowPage.ToString() + "ҳ��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark32,
                    m_intPosY + (int)enmRecordRectangleInfo.VOffSet);

                #region ��ӡ��ϣ�ReSet(��λ)����
                if (m_intCurrentRecord == m_objPrintDataArr.Length)
                {
                    m_intPosY = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark3;
                    m_intCurrentRecord = 0;//��ǰ��¼����Ÿ�λ���Ա���һ�δ�ӡ����
                    m_blnBeginPrintNewRecord = true;//��λ
                    m_intNowPage = 1;//��λ						
                }
                #endregion
            }
            catch (Exception err)
            {
                clsPublicFunction.ShowInformationMessageBox(err.Message + "\r\n" + err.StackTrace);

            }
        }

        /// <summary>
        /// ����Ƿ�ҳ,true:��ҳ��false:����ҳ
        /// </summary>
        /// <param name="p_intYBottom">Ҫ���ĵ���Y����</param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool m_blnCheckPageChange(int p_intYBottom, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (p_intYBottom >= 1050)
            {
                e.HasMorePages = true;

                //Print VLine
                m_mthPrintVLines(e, m_intPosY);
                m_mthPrintOneHorizontalLine(e, m_intPosY);

                //ҳ��//////////////////////////////////////////////////////////////
                e.Graphics.DrawString("����" + m_intNowPage.ToString() + "ҳ��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark32,
                    m_intPosY + (int)enmRecordRectangleInfo.VOffSet);


                m_intPosY = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark3;
                m_intNowPage++;
                return true;

            }
            else return false;
        }

        // ��ӡ����ʱ�Ĳ���
        private void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
        {
        }

        #region �������ֲ���
        /// <summary>
        /// �������ֲ���
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_HospitalName));

            e.Graphics.DrawString("�� �� ICU �� �� �� �� �� �� �� �� ¼ ��", m_fotTitleFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_Name_Title));


            e.Graphics.DrawString("������", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name_Title));
            e.Graphics.DrawString(m_objPrintInfo.m_strPatientName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name));

            e.Graphics.DrawString("�Ա�", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex_Title));
            e.Graphics.DrawString(m_objPrintInfo.m_strSex, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex));

            e.Graphics.DrawString("���䣺", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age_Title));
            e.Graphics.DrawString(m_objPrintInfo.m_strAge, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age));

            //			e.Graphics.DrawString("���ң�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name_Title ));
            //			e.Graphics.DrawString(m_objPrintInfo.m_strDeptName ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name ));
            //
            //			e.Graphics.DrawString("������",m_fotSmallFont,m_slbBrush,new PointF(430f,150f));
            //			e.Graphics.DrawString(m_objPrintInfo.m_strAreaName ,m_fotSmallFont,m_slbBrush,new PointF(480f,150f));

            e.Graphics.DrawString("���ţ�", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo_Title));
            e.Graphics.DrawString(m_objPrintInfo.m_strBedName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo));

            e.Graphics.DrawString("���أ�", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Weight_Title));
            e.Graphics.DrawString((m_objPrintInfo.m_strWeight == null || m_objPrintInfo.m_strWeight == "") ? "     kg" : m_objPrintInfo.m_strWeight + "   kg", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Weight));

            e.Graphics.DrawString("סԺ�ţ�", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID_Title));
            e.Graphics.DrawString(m_objPrintInfo.m_strHISInPatientID, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID));

            e.Graphics.DrawString("��ӡ���ڣ�", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Print_Date_Title));
            e.Graphics.DrawString((m_objPrintInfo.m_strInPatentID != "") ? DateTime.Now.ToString("yyyy��MM��dd��") : "    ��  ��  ��", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Print_Date));

        }
        #endregion

        #region ����ͷ����
        /// <summary>
        ///  ����ͷ����
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintRectangleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {

            #region �����Ӻ���
            for (int i1 = 0; i1 < 4 /*(int)enmRecordRectangleInfo.RowLinesNum*/ ; i1++)
            {
                if (i1 == 0)
                    e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX,
                        (int)enmRecordRectangleInfo.TopY,
                        (int)enmRecordRectangleInfo.RightX,
                        (int)enmRecordRectangleInfo.TopY);
                else if (i1 == 3)
                    e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX,
                        (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark3,
                        (int)enmRecordRectangleInfo.RightX,
                        (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark3);
                else if (i1 == 1)
                {
                    e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7,
                        (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark1,
                        (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark33,
                        (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark1);
                }
                else //if(i1==2)
                {
                    e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark22,
                        (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark2,
                        (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark33,
                        (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark2);
                }
            }

            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark3,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMarkBreathSound,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark5,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMarkBreathSound);
            #endregion �����Ӻ���

            #region ����������
            int intXPos = (int)enmRecordRectangleInfo.LeftX;
            int intYTop = (int)enmRecordRectangleInfo.TopY;
            int intYBottom = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark3;

            //���������
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark1;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);


            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark3;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark4;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop + (int)enmRecordRectangleInfo.RowsMarkBreathSound, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark5;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark6;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark8;
            intYTop = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark1;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark14;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark17;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark18;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark19;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark20;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark21;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark22;
            intYTop = (int)enmRecordRectangleInfo.TopY;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark23;
            intYTop = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark2;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark24;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark25;
            intYTop = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark1;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark26;
            intYTop = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark2;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark27;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark28;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark29;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark30;
            intYTop = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark1;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark31;
            intYTop = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark2;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark32;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark33;
            intYTop = (int)enmRecordRectangleInfo.TopY;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.RightX;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);


            #endregion

        }


        #endregion

        #region ���������Ŀ
        /// <summary>
        /// ���������Ŀ
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintHeaderInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {
            ///������Ŀ�ĸ����ֵ�Y���꣨����Ϊ9�У�
            int[] intYPosFontArr = new int[9]{ (int)enmRecordRectangleInfo.TopY + 3,//���� :0
											   (int)enmRecordRectangleInfo.TopY + 25,//�գ��ڣ�:1
											   (int)enmRecordRectangleInfo.TopY + 45,//ml :2
											   (int)enmRecordRectangleInfo.TopY + 65,//ͫ�� ��3
											   (int)enmRecordRectangleInfo.TopY + 85,//T :4
											   (int)enmRecordRectangleInfo.TopY + 105,//���գ��ڣ���С ��5
											   (int)enmRecordRectangleInfo.TopY + 125,//ͨ������ ��6
											   (int)enmRecordRectangleInfo.TopY + 145,//���ң�7
											   (int)enmRecordRectangleInfo.TopY + 165//��ͨ���� ��8				   
										   };
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + 25, intYPosFontArr[1]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + 25, intYPosFontArr[5]);

            e.Graphics.DrawString("ʱ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark1 + 15, intYPosFontArr[1]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark1 + 15, intYPosFontArr[5]);

            e.Graphics.DrawString("ͨ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + 5, intYPosFontArr[1]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + 5, intYPosFontArr[5]);
            e.Graphics.DrawString("ʽ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + 5, intYPosFontArr[7]);

            e.Graphics.DrawString("������", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark3 + 15, intYPosFontArr[2]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark3 + 5, intYPosFontArr[5]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark4 + 5, intYPosFontArr[5]);

            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark5 + 5, intYPosFontArr[1]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark5 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark5 + 5, intYPosFontArr[5]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark5 + 5, intYPosFontArr[7]);

            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark6 + 5, intYPosFontArr[1]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark6 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("ѹ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark6 + 5, intYPosFontArr[5]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark6 + 5, intYPosFontArr[7]);

            //�趨����
            e.Graphics.DrawString("�趨����", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13, intYPosFontArr[0]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7 + 5, intYPosFontArr[4]);
            e.Graphics.DrawString("(TV)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7 + 1, intYPosFontArr[5]);
            e.Graphics.DrawString("L", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7 + 5, intYPosFontArr[8]);

            e.Graphics.DrawString("Ƶ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark8 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark8 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("(R)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark8 + 1, intYPosFontArr[4]);
            e.Graphics.DrawString("BPM", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark8 + 5, intYPosFontArr[8]);

            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("ֵ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9 + 5, intYPosFontArr[4]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9 + 5, intYPosFontArr[5]);
            e.Graphics.DrawString("(PF)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9 + 1, intYPosFontArr[6]);
            e.Graphics.DrawString("LPM", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9 + 5, intYPosFontArr[8]);

            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("Ũ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10 + 5, intYPosFontArr[4]);
            e.Graphics.DrawString("(O2%)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10 + 1, intYPosFontArr[5]);
            e.Graphics.DrawString("%", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10 + 5, intYPosFontArr[8]);

            e.Graphics.DrawString("ѹ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("֧", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11 + 5, intYPosFontArr[4]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11 + 5, intYPosFontArr[5]);
            e.Graphics.DrawString("(PS)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11 + 1, intYPosFontArr[6]);
            e.Graphics.DrawString("cmH2O", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11 + 1, intYPosFontArr[8]);

            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12 + 5, intYPosFontArr[4]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12 + 5, intYPosFontArr[5]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12 + 5, intYPosFontArr[6]);
            e.Graphics.DrawString("(AS)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12 + 1, intYPosFontArr[7]);
            e.Graphics.DrawString("cmH2O", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12 + 1, intYPosFontArr[8]);

            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13 + 5, intYPosFontArr[4]);
            e.Graphics.DrawString("ͣ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13 + 5, intYPosFontArr[5]);
            e.Graphics.DrawString("(IPu)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13 + 1, intYPosFontArr[6]);
            e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13 + 5, intYPosFontArr[8]);

            e.Graphics.DrawString("ˮ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark14 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("ƽ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark14 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark14 + 5, intYPosFontArr[4]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark14 + 5, intYPosFontArr[5]);
            e.Graphics.DrawString("(ML)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark14 + 1, intYPosFontArr[6]);
            e.Graphics.DrawString("LPM", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark14 + 5, intYPosFontArr[8]);

            e.Graphics.DrawString("˳", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("Ӧ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15 + 5, intYPosFontArr[4]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15 + 5, intYPosFontArr[5]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15 + 5, intYPosFontArr[6]);
            e.Graphics.DrawString("(CC)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15 + 1, intYPosFontArr[7]);
            e.Graphics.DrawString("cmH2O", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15 + 1, intYPosFontArr[8]);

            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("ʱ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16 + 5, intYPosFontArr[4]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16 + 5, intYPosFontArr[5]);
            e.Graphics.DrawString("(IT)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16 + 1, intYPosFontArr[6]);
            e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16 + 5, intYPosFontArr[8]);

            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark17 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark17 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("ѹ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark17 + 5, intYPosFontArr[4]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark17 + 5, intYPosFontArr[5]);
            e.Graphics.DrawString("(IPr)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark17 + 1, intYPosFontArr[6]);
            e.Graphics.DrawString("cmH2O", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark17 + 1, intYPosFontArr[8]);

            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark18 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark18 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark18 + 5, intYPosFontArr[4]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark18 + 5, intYPosFontArr[5]);
            e.Graphics.DrawString("(BF)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark18 + 1, intYPosFontArr[6]);
            e.Graphics.DrawString("LPM", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark18 + 5, intYPosFontArr[8]);

            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark19 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark19 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark19 + 5, intYPosFontArr[4]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark19 + 5, intYPosFontArr[5]);
            e.Graphics.DrawString("(FT)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark19 + 1, intYPosFontArr[6]);
            e.Graphics.DrawString("LPM", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark19 + 5, intYPosFontArr[8]);

            e.Graphics.DrawString("ѹ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark20 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark20 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("б", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark20 + 5, intYPosFontArr[4]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark20 + 5, intYPosFontArr[5]);
            e.Graphics.DrawString("(PSe)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark20 + 1, intYPosFontArr[6]);

            e.Graphics.DrawString("PEEP", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark21 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("cmH2O", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark21 + 1, intYPosFontArr[8]);


            //�����ֵ
            e.Graphics.DrawString("�����ֵ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark26 + 22, intYPosFontArr[0]);
            e.Graphics.DrawString("�������������", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark22 + 5, intYPosFontArr[1]);
            e.Graphics.DrawString("����Ƶ�ʼ��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark26 + 5, intYPosFontArr[1]);
            e.Graphics.DrawString("ѹ�����", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark30 + 20, intYPosFontArr[1]);

            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark22 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark22 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark22 + 5, intYPosFontArr[4]);
            e.Graphics.DrawString("(TV)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark22 + 1, intYPosFontArr[5]);
            e.Graphics.DrawString("L", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark22 + 5, intYPosFontArr[8]);

            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark23 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("����", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark23 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("ͨ��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark23 + 5, intYPosFontArr[4]);
            e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark23 + 5, intYPosFontArr[5]);
            e.Graphics.DrawString("(TM)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark23 + 5, intYPosFontArr[6]);
            e.Graphics.DrawString("LPM", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark23 + 5, intYPosFontArr[8]);

            e.Graphics.DrawString("����", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark24 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("����", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark24 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("����", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark24 + 5, intYPosFontArr[4]);
            e.Graphics.DrawString("ͨ��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark24 + 5, intYPosFontArr[5]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark24 + 5, intYPosFontArr[6]);
            e.Graphics.DrawString("(SM)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark24 + 5, intYPosFontArr[7]);
            e.Graphics.DrawString("LPM", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark24 + 5, intYPosFontArr[8]);

            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark25 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("Ƶ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark25 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark25 + 5, intYPosFontArr[4]);
            e.Graphics.DrawString("(To)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark25 + 1, intYPosFontArr[5]);
            e.Graphics.DrawString("BPM", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark25 + 5, intYPosFontArr[8]);

            e.Graphics.DrawString("����", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark26 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("����", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark26 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("Ƶ��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark26 + 5, intYPosFontArr[4]);
            e.Graphics.DrawString("(S)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark26 + 5, intYPosFontArr[5]);
            e.Graphics.DrawString("BPM", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark26 + 5, intYPosFontArr[8]);

            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark27 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark27 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark27 + 5, intYPosFontArr[4]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark27 + 5, intYPosFontArr[5]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark27 + 5, intYPosFontArr[6]);
            e.Graphics.DrawString("(IER)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark27 + 1, intYPosFontArr[7]);

            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark28 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark28 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("ʱ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark28 + 5, intYPosFontArr[4]);
            e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark28 + 5, intYPosFontArr[5]);
            e.Graphics.DrawString("(Ti)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark28 + 1, intYPosFontArr[6]);
            e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark28 + 5, intYPosFontArr[8]);

            e.Graphics.DrawString("��С", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark29 + 6, intYPosFontArr[2]);
            e.Graphics.DrawString("����", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark29 + 6, intYPosFontArr[3]);
            e.Graphics.DrawString("ͨ��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark29 + 6, intYPosFontArr[4]);
            e.Graphics.DrawString("��ͨ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark29 + 6, intYPosFontArr[5]);
            e.Graphics.DrawString("����", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark29 + 6, intYPosFontArr[6]);
            e.Graphics.DrawString("�ֱ�", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark29 + 6, intYPosFontArr[7]);
            e.Graphics.DrawString("(MMV)%", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark29 + 2, intYPosFontArr[8]);

            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark30 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("ѹ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark30 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("(Pe)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark30 + 1, intYPosFontArr[5]);
            e.Graphics.DrawString("cmH2O", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark30 + 1, intYPosFontArr[8]);

            e.Graphics.DrawString("ƽ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark31 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark31 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("ѹ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark31 + 5, intYPosFontArr[4]);
            e.Graphics.DrawString("(Me)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark31 + 1, intYPosFontArr[5]);
            e.Graphics.DrawString("cmH2O", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark31 + 1, intYPosFontArr[8]);

            e.Graphics.DrawString("ƽ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark32 + 5, intYPosFontArr[2]);
            e.Graphics.DrawString("̨", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark32 + 5, intYPosFontArr[3]);
            e.Graphics.DrawString("ѹ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark32 + 5, intYPosFontArr[4]);
            e.Graphics.DrawString("(Pu)", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark32 + 1, intYPosFontArr[5]);
            e.Graphics.DrawString("cmH2O", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark32 + 1, intYPosFontArr[8]);

            e.Graphics.DrawString("ǩ��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark33 + 5, intYPosFontArr[4]);

        }
        #endregion

        #region ��ӡ���еĴ�ֱ��
        /// <summary>
        /// ��ӡ���еĴ�ֱ��
        /// </summary>
        /// <param name="e"></param>
        /// <param name="p_intPageBottomY"></param>
        private void m_mthPrintVLines(PrintPageEventArgs e, int p_intPageBottomY)
        {
            #region ����������
            int intXPos = (int)enmRecordRectangleInfo.LeftX;
            int intYTop = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark3;
            int intYBottom = p_intPageBottomY;

            //���������
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark1;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark3;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark4;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark5;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark6;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark8;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark14;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark17;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark18;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark19;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark20;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark21;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark22;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark23;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark24;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark25;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark26;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark27;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark28;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark29;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark30;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark31;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark32;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark33;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            intXPos = (int)enmRecordRectangleInfo.RightX;
            e.Graphics.DrawLine(m_GridPen, intXPos, intYTop, intXPos, intYBottom);

            #endregion
        }
        #endregion

        #region ��ӡһ��ˮƽ��
        /// <summary>
        /// ��ӡһ��ˮƽ��
        /// </summary>
        /// <param name="e"></param>
        /// <param name="p_intBottomY"></param>
        private void m_mthPrintOneHorizontalLine(System.Drawing.Printing.PrintPageEventArgs e, int p_intBottomY)
        {
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX,
                p_intBottomY,
                (int)enmRecordRectangleInfo.RightX,
                p_intBottomY);
        }
        #endregion

        #region ��ӡһ����ֵ,���жϵ�ǰ��¼�Ƿ��ӡ��
        /// <summary>
        /// ��ӡһ����ֵ,���жϵ�ǰ��¼�Ƿ��ӡ��
        /// </summary>
        /// <param name="e"></param>
        /// <param name="p_intBottomY"></param>
        /// <returns></returns>
        private bool m_blnPrintOneValue(System.Drawing.Printing.PrintPageEventArgs e, int p_intBottomY)
        {
            p_intBottomY += (int)enmRecordRectangleInfo.VOffSet;
            #region ������¼�¼����ӡ����
            if (m_blnBeginPrintNewRecord == true)
            {
                m_intNowRowInOneRecord = 0;

                //��������
                string strCreateDate;
                string strCreateTime;
                string strCreateDateTime;

                if (m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr == null)
                {
                    strCreateDate = "";
                    strCreateTime = "";
                    strCreateDateTime = "";
                }
                else
                {
                    strCreateDateTime = m_objPrintDataArr[m_intCurrentRecord].m_objRecordContent.m_dtmCreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                    try
                    {
                        strCreateDate = DateTime.Parse(strCreateDateTime).ToString("yyyy-M-d");
                        strCreateTime = DateTime.Parse(strCreateDateTime).ToString("HH:mm");
                    }
                    catch
                    { strCreateDate = "����"; strCreateTime = "����"; }
                }
                //��ʼ��ӡһ���¼�¼/////////////////////////////////////////////////////////////////////
                e.Graphics.DrawString(strCreateDate, m_fotSmallFont, m_slbBrush,
                    (int)enmRecordRectangleInfo.LeftX,
                    p_intBottomY);
                e.Graphics.DrawString(strCreateTime, m_fotSmallFont, m_slbBrush,
                    (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark1 + 1,
                    p_intBottomY);
            }
            #endregion



            #region ���޸�˳���ӡ��ǰ��¼��ĳһ��
            bool blnIsRecordFinish = m_blnPrintOneRowValue(m_strValueArr, m_intNowRowInOneRecord, e, p_intBottomY);


            #region ǩ���������޸ĵ���ǩ����
            string strSign;
            if (m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr == null
                || m_intNowRowInOneRecord > m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr.Length - 1)
                strSign = "";
            else
                strSign = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[m_intNowRowInOneRecord].m_strModifyUserName;
            //			clsEmployee objclsEmployee=new clsEmployee(m_objclsICUBreathRecordContent_AllArr[m_intCurrentRecord].m_objclsICUBreathRecordContentArr[m_intNowRowInOneRecord].m_strModifyUserID);
            //			if(objclsEmployee!=null)
            //				strSign=objclsEmployee.m_StrFirstName;			
            e.Graphics.DrawString(strSign, m_fotSmallFont, m_slbBrush,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark33 + 1,
                p_intBottomY);
            #endregion

            m_blnBeginPrintNewRecord = blnIsRecordFinish;//��ǰ��¼�Ƿ����					
            m_intNowRowInOneRecord++;
            #endregion

            m_intPosY += intTempDeltaY;
            return blnIsRecordFinish;
        }


        #endregion ��ӡһ����ֵ,���жϵ�ǰ��¼�Ƿ��ӡ��

        #region ��ӡһ����ֵ
        /// <summary>
        /// ��ӡһ����ֵ
        /// </summary>
        /// <param name="p_strValueArr">��ֵ(�ӡ�ͨ����ʽ������ƽ̨ѹ������31��)</param>
        /// <param name="p_intNowRowInOneRecord">�ڼ��εĽ��:�ȼ���NowRowInOneRecord</param>
        /// <param name="e">��ӡ����</param>
        /// <param name="p_intPosY">Y����</param>
        private bool m_blnPrintOneRowValue(string[][] p_strValueArr, int p_intNowRowInOneRecord, System.Drawing.Printing.PrintPageEventArgs e, int p_intPosY)
        {
            #region ��ӡÿ���޸ĵĵ��м�¼
            if (p_intNowRowInOneRecord < p_strValueArr.Length)
            {
                string[] strValueArr = p_strValueArr[p_intNowRowInOneRecord];

                CharacterRange[] rgnDSTArr = new CharacterRange[1];
                rgnDSTArr[0] = new CharacterRange(0, 0);

                RectangleF rtfText = new RectangleF(0, 0, 10000, 100);

                StringFormat stfMeasure = new StringFormat(StringFormatFlags.LineLimit);

                RectangleF rtfBounds;

                Region[] rgnDST;

                int intTempColumn = 0;//��ǰ����������ԣ���ָ��ȥ���ں�ʱ������֮����������ţ�
                int intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2;//��ǰ��X����
                //ͨ����ʽ
                #region ��ӡһ�񣬣�������ȫ��ͬ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark3;//��ǰ��X����
                //��������
                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark4;//��ǰ��X����
                //��������
                #region ��ӡһ��

                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark5;//��ǰ��X����

                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark6;//��ǰ��X����

                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7;//��ǰ��X����

                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark8;//��ǰ��X����

                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9;//��ǰ��X����

                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10;//��ǰ��X����

                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11;//��ǰ��X����

                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12;//��ǰ��X����

                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13;//��ǰ��X����

                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark14;//��ǰ��X����

                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15;//��ǰ��X����

                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16;//��ǰ��X����

                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark17;//��ǰ��X����

                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark18;//��ǰ��X����

                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark19;//��ǰ��X����

                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark20;//��ǰ��X����

                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark21;//��ǰ��X����

                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark22;//��ǰ��X����

                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark23;//��ǰ��X����

                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark24;//��ǰ��X����

                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark25;//��ǰ��X����
                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark26;//��ǰ��X����
                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark27;//��ǰ��X����
                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark28;//��ǰ��X����
                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark29;//��ǰ��X����
                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark30;//��ǰ��X����
                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark31;//��ǰ��X����
                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark32;//��ǰ��X����				
                #region ��ӡһ��
                if (strValueArr[intTempColumn] != null && strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, p_intPosY);
                    if (p_intNowRowInOneRecord + 1 < p_strValueArr.Length)
                    {
                        if (strValueArr[intTempColumn] != p_strValueArr[p_intNowRowInOneRecord + 1][intTempColumn])
                        {
                            rtfText.X = intPosX;
                            rtfText.Y = p_intPosY;

                            rgnDSTArr[0].First = 0;
                            rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);

                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn], m_fotSmallFont, rtfText, stfMeasure);

                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2);
                            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 3, rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 3);
                        }
                    }
                }
                #endregion	��ӡһ��

            }
            #endregion ��ӡÿ���޸ĵĵ��м�¼

            if (p_intNowRowInOneRecord >= p_strValueArr.Length - 1)
                return true;
            else return false;
        }

        #endregion ��ӡһ����ֵ


        #region ���õ�ǰҪ��ӡ��һ����¼����
        /// <summary>
        /// ���õ�ǰҪ��ӡ��һ����¼����,���أ�������¼������ӡ����(�����д�ӡ��5����)
        /// </summary>
        /// <param name="e"></param>
        /// <param name="p_intBottomY"></param>
        /// <returns></returns>
        private int m_intSetPrintOneValueRows(PrintPageEventArgs e, ref int p_intBottomY)
        {
            if (m_objPrintDataArr == null || m_intCurrentRecord >= m_objPrintDataArr.Length)
                return 0;

            if (m_objPrintDataArr[m_intCurrentRecord].m_intFlag == (int)enmRecordsType.ICUBreath && (m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr == null || m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr.Length == 0))
                return 0;

            int intRowsOfOneRecord;
            string strModifyDate;

            try
            {
                #region ������¼�¼���ж��Ƿ����ۼ�
                int intLenth;
                if (m_blnBeginPrintNewRecord == true)
                {
                    #region ��ǰ��¼���鸳ֵ

                    intRowsOfOneRecord = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr.Length;
                    strModifyDate = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[intRowsOfOneRecord - 1].m_dtmModifyDate.ToString("yyyy-MM-dd HH:mm:ss");
                    intLenth = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr.Length;
                    m_strValueArr = new string[intLenth][];
                    for (int k1 = 0; k1 < intLenth; k1++)
                    {
                        m_strValueArr[k1] = new string[31];
                        m_strValueArr[k1][0] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strMachineMode_Last;
                        m_strValueArr[k1][1] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strBreathSoundLeft_Last;
                        m_strValueArr[k1][2] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strBreathSoundRight_Last;
                        m_strValueArr[k1][3] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strInLength_Last;
                        m_strValueArr[k1][4] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strGasbagPress_Last;
                        m_strValueArr[k1][5] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strTIDAL_VOLUME_Last;
                        m_strValueArr[k1][6] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strRATE_Last;
                        m_strValueArr[k1][7] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strPEAK_FLOW_Last;
                        m_strValueArr[k1][8] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strO2_Last;
                        m_strValueArr[k1][9] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strPS_Last;
                        m_strValueArr[k1][10] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strASSIST_SENSITIVITY_Last;
                        m_strValueArr[k1][11] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strINSPIRATORY_PAUSE_Last;
                        m_strValueArr[k1][12] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strMMV_LEVEL_Last;
                        m_strValueArr[k1][13] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strCOMPLIANCE_COMP_Last;
                        m_strValueArr[k1][14] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strINSPIRATORY_TIME_Last;
                        m_strValueArr[k1][15] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strINSPIRATORY_PRESSURE_Last;
                        m_strValueArr[k1][16] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strBASE_FLOW_Last;
                        m_strValueArr[k1][17] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strFLOW_TRIGGER_Last;
                        m_strValueArr[k1][18] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strPRESSURE_SLOPE_Last;
                        m_strValueArr[k1][19] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strPEEP_Last;
                        m_strValueArr[k1][20] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strTIDAL_VOL_Last;
                        m_strValueArr[k1][21] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strTOTAL_MV_Last;
                        m_strValueArr[k1][22] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strSPONT_MV_Last;
                        m_strValueArr[k1][23] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strTOTAL_Last;
                        m_strValueArr[k1][24] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strSPONT_Last;
                        m_strValueArr[k1][25] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strI_E_RATIO_Last;
                        m_strValueArr[k1][26] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strTi_Last;
                        m_strValueArr[k1][27] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strMMV_Last;
                        m_strValueArr[k1][28] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strPEAR_Last;
                        m_strValueArr[k1][29] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strMEAN_Last;
                        m_strValueArr[k1][30] = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strPLATEAU_Last;
                    }

                    return intLenth;

                    #endregion

                }
                else
                    return 0;
                #endregion
            }
            catch (Exception ex)
            {
                clsPublicFunction.ShowInformationMessageBox(ex.Message);
                return 1;
            }
        }
        #endregion
        #endregion

        /// <summary>
        /// ��ӡ��Ϣ.
        /// </summary>
        [Serializable]
        private class clsPrintInfo_ICUBreath
        {
            public string m_strInPatentID;
            public DateTime m_dtmInPatientDate;
            public string m_strWeight;
            public string m_strPatientName;
            public string m_strSex;
            public string m_strAge;
            public string m_strBedName;
            public string m_strDeptName;
            public string m_strAreaName;
            public DateTime m_dtmOpenDate;
            public string m_strHISInPatientID;
            public DateTime m_dtmHISInPatientDate;

            public clsTransDataInfo[] m_objTransDataArr;
            public DateTime[] m_dtmFirstPrintDateArr;//Length��m_dtmFirstPrintDateArr.Length��ͬ.
            public bool[] m_blnIsFirstPrintArr;//Length��m_dtmFirstPrintDateArr.Length��ͬ.

            public clsICUBreath[] m_objPrintDataArr;
        }

        #region ���ⲿ���Ա���ӡ����ʾʵ��.
        //		using System.IO;
        //		using System.Runtime.Serialization;
        //		System.Drawing.Printing.PrintDocument m_pdcPrintDocument;
        //		private void m_mthfrmLoad()
        //		{	
        //			this.m_pdcPrintDocument = new System.Drawing.Printing.PrintDocument();
        //			this.m_pdcPrintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_BeginPrint);
        //			this.m_pdcPrintDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_EndPrint);
        //			this.m_pdcPrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.m_pdcPrintDocument_PrintPage);		
        //		}
        //		private void m_pdcPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        //		{			
        //			objPrintTool.m_mthPrintPage(e);
        //		}
        //
        //		private void m_pdcPrintDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //		{
        //			objPrintTool.m_mthBeginPrint(e);				
        //		}
        //
        //		private void m_pdcPrintDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //		{
        //			objPrintTool.m_mthEndPrint(e);
        //		}
        //
        //		clsICUBreathPrintTool objPrintTool;
        //		private void m_mthDemoPrint_FromDataSource()
        //		{	
        //			objPrintTool=new clsICUBreathPrintTool();
        //			objPrintTool.m_mthInitPrintTool(null);	
        //			if(m_objBaseCurrentPatient==null)
        //				objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,DateTime.MinValue,DateTime.MinValue);
        //			else 
        //				objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,m_objBaseCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate,DateTime.MinValue);
        //						
        //			objPrintTool.m_mthInitPrintContent();	

        //			//���浽�ļ�
        //			object objtemp=objPrintTool.m_objGetPrintInfo();
        //			IFormatter objForm = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        //
        //			Stream objStream = new System.IO.FileStream("d:\\code\\Tese.bin",FileMode.Create);
        //
        //			objForm.Serialize(objStream,objtemp);
        //
        //			objStream.Flush();
        //			objStream.Close();
        //		
        //			m_mthStartPrint();
        //		}
        //		private void m_mthDemoPrint_FromFile()
        //		{	
        //			objPrintTool=new clsICUBreathPrintTool();
        //			objPrintTool.m_mthInitPrintTool(null);	
        //
        //			IFormatter objForm = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        //			Stream objStream = new System.IO.FileStream("d:\\code\\Tese.bin",FileMode.Open);
        //			object objtemp = objForm.Deserialize(objStream);//
        //			objStream.Close();
        //		
        //			objPrintTool.m_mthSetPrintContent(objtemp);		
        //
        //			m_mthStartPrint();
        //		}
        //		private void m_mthStartPrint()
        //		{			
        //			PrintPreviewDialog ppdPrintPreview = new PrintPreviewDialog();
        //			ppdPrintPreview.Document = m_pdcPrintDocument;
        //			ppdPrintPreview.ShowDialog();
        //		}
        #endregion ���ⲿ���Ա���ӡ����ʾʵ��.
    }


}