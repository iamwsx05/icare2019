using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.Utility.Controls;
using System.Drawing;
 
namespace iCare
{
    /// <summary>
    /// �������ǼǼ�¼��ӡ������(�°�)ժҪ˵����
    /// </summary>
    public class clsRegisterQuantity_PrintTool_GX : infPrintRecord
    {
        private bool m_blnIsFromDataSource = true;               //�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
        private bool m_blnWantInit = true;	                   //�Ƿ���Ҫ��ʼ��	
        private clsRecordsDomain m_objRecordsDomain;           //��¼��
        private clsRegisterQuantity_VO m_objPrintInfoOne;     //��ӡ����
        private string strCurrentClass;                        //��ǰ���Ĭ��Ϊ��
        private int intCaseRowCount;                           //��ǰ���̼�¼���������
        private string[] strCurrentCaseTextArr;                //��ǰ���̼�¼��������
        private string[] strCurrentCaseXmlArr;                 //��ǰ���̼�¼�ۼ�����
        private string[] strCurrentCaseCreateDateArr;          //��ǰ���̼�¼����ʱ��
        private object[][] objDataArr;
        private clsRegisterQuantity_VODataInfo[] m_objPrintInfo;
        private string strDiagnose;
        private object[] objtest1;
        private const int m_intWidth = 63;

        private bool m_blnHasMorePage = false;

        private string[] m_strCustomColumn;


        private bool m_bSummaryRow = false;


        public clsRegisterQuantity_PrintTool_GX()
        {

            //
            // TODO: �ڴ˴���ӹ��캯���߼�

            //
        }


        #region ��ӡ��ʼ�����¼�
        /// <summary>
        /// ���ô�ӡ��Ϣ(�������ݿ��ȡʱҪ���ȵ���.)
        /// </summary>
        /// <param name="p_objPatient">����</param>
        /// <param name="p_dtmInPatientDate">��Ժ����</param>
        /// <param name="p_dtmOpenDate">OpenDate�������һ�δ�ӡ��μ�¼�������ͣ��粡����¼��������OpenDate</param>
        /// p_objPatient

        public void m_mthSetPrintInfo(clsPatient p_objPatient, DateTime p_dtmInPatientDate, DateTime p_dtmOpenDate)
        {
            m_blnIsFromDataSource = true;//�����Ǵ����ݿ��ȡ
            clsPatient m_objPatient = p_objPatient;
            m_objPrintInfoOne = new clsRegisterQuantity_VO();
            m_objPrintInfoOne.m_strInPatientID = m_objPatient != null ? m_objPatient.m_StrInPatientID : "";
            m_objPrintInfoOne.m_strInPatientName = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrFirstName : "";
            m_objPrintInfoOne.m_strSex = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrSex : "";
            m_objPrintInfoOne.m_strBedName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName : "";
            m_objPrintInfoOne.m_strDeptName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName: "";
            m_objPrintInfoOne.m_dtmInPatientDate = p_dtmInPatientDate;
            m_objPrintInfoOne.m_dtmHISInDate = m_objPatient.m_DtmSelectedHISInDate;
            m_objPrintInfoOne.m_strHISInPatientID = m_objPatient.m_StrHISInPatientID;
        }

        /// <summary>
        /// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���(�������ݿ��ȡʱҪ����.)
        /// </summary>
        public void m_mthInitPrintContent()
        {	//m_objprintinfoΪ�ձ���δ���ô�ӡ����		
            if (m_objPrintInfoOne == null)
            {

                clsPublicFunction.ShowInformationMessageBox("����m_mthInitPrintContent֮ǰ�����ȵ���m_mthSetPrintInfo����");
                return;
            }
            //����Ϊ��
            if (m_objPrintInfoOne.m_strInPatientID == "")
                return;
            //��ȡ��ӡ����

            //com.digitalwave.clsRecordsService.clsRegisterQuantityService objServ =
            //    (com.digitalwave.clsRecordsService.clsRegisterQuantityService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsRegisterQuantityService));

            long m_lngRs = (new weCare.Proxy.ProxyEmr()).Service.m_lngGetPrintInfo(m_objPrintInfoOne, out m_objPrintInfo);

            //objServ = null;
            if (m_lngRs <= 0)
                return;
            m_blnWantInit = false;
        }
        private DateTime m_dtmPreRecordDate;
        private cltDataGridDSTRichTextBox m_dtcINITEM;
        public cltDataGridDSTRichTextBox m_dtcINFACT;
        public cltDataGridDSTRichTextBox m_dtcOUTPISS;
        public cltDataGridDSTRichTextBox m_dtcOUTSTOOL;
        private cltDataGridDSTRichTextBox m_dtcCHECKT;
        private cltDataGridDSTRichTextBox m_dtcCHECKP;
        private cltDataGridDSTRichTextBox m_dtcCHECKR;
        private cltDataGridDSTRichTextBox m_dtcCHECKBP;
        private string strTempDate = string.Empty;

        private int m_intCurrentPagePrintRow = 0;


        private int m_intMainCurrentPagePrintRow = 0;
        private int m_intCurrentContentRow = 0;



        /// <summary>
        /// ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
        /// </summary>
        /// <param name="p_objPrintContent">��ӡ����</param>


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

            //û�м�¼����ʱ�����ؿ�
            if (m_objPrintInfo == null || m_objPrintInfo.Length == 0)
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
            m_fotHosNameFont = new Font("SimSun", 14f);
            m_fotTinyFont = new Font("SimSun", 8f);

            m_GridPen = new Pen(Color.Black, 1);
            m_GridPenBold = new Pen(Color.Black, 2);

            m_GridRedPen = new Pen(Color.Red, 2);

            m_slbBrush = new SolidBrush(Color.Black);
            m_objPageSetting = new clsPrintPageSettingForRecord();
            m_objPrintContext = new clsPrintRichTextContext(Color.Black, m_fotSmallFont);
            m_intCurrentRecord = 0;
            intNowPage = 1;
            blnBeginPrintNewRecord = true;

        }

        /// <summary>
        /// �ͷŴ�ӡ����
        /// </summary>
        public void m_mthDisposePrintTools(object p_objArg)
        {
            m_fotTitleFont.Dispose();
            m_fotHeaderFont.Dispose();
            m_fotSmallFont.Dispose();
            m_fotTinyFont.Dispose();
            m_GridPen.Dispose();
            m_GridRedPen.Dispose();
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

        public void m_mthPrintPage(PageSettings p_pstDefault)
        {
            frmPrintPreviewDialogPF frmPreview = new frmPrintPreviewDialogPF();
            frmPreview.m_evtBeginPrint += new PrintEventHandler(frmPreview_m_evtBeginPrint);
            frmPreview.m_evtEndPrint += new PrintEventHandler(frmPreview_m_evtEndPrint);
            frmPreview.m_evtPrintContent += new PrintPageEventHandler(frmPreview_m_evtPrintContent);
            frmPreview.m_evtPrintFrame += new PrintPageEventHandler(frmPreview_m_evtPrintFrame);
            frmPreview.m_pstDefaultPageSettings = p_pstDefault;
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
            m_mthPrintHeaderInfo(e);
        }
        #endregion

        /// <summary>
        /// ��ӡ������һ��ʹ�������������ݿ���Ϣ��
        /// </summary>
        /// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintEventArgs���͵Ķ���</param>
        public void m_mthEndPrint(object p_objPrintArg)
        {
            if (m_blnIsFromDataSource == false || m_objPrintInfoOne.m_strInPatientID == "") return;
            //�����ӡ�ɹ�������������Ҫ���µ�ʱ�䣬����У�����ʱ�䡣 
            if (!((PrintEventArgs)p_objPrintArg).Cancel)
            {
                //com.digitalwave.clsRecordsService.clsRegisterQuantityService objServ =
                //    (com.digitalwave.clsRecordsService.clsRegisterQuantityService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.clsRecordsService.clsRegisterQuantityService));

                long m_lngRs = (new weCare.Proxy.ProxyEmr()).Service.m_lngUptFirstPrintDate(m_objPrintInfoOne.m_strInPatientID, m_objPrintInfoOne.m_dtmInPatientDate.ToString());

             }

            //}                          
            m_mthEndPrintSub((PrintEventArgs)p_objPrintArg);
        }

        public void m_mthSetPrintContent(object p_objPrintContent)
        {
            if (p_objPrintContent.GetType().Name != "clsPrintInfo_GeneralNurseGX")
            {
                //clsPublicFunction.ShowInformationMessageBox("��������");
            }
            m_blnIsFromDataSource = false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ

            //m_objPrintDataArr= m_objPrintInfo. m_objPrintDataArr ;		

            m_blnWantInit = false;
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

        private void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
        {
            if (m_objPrintInfo == null)
                m_mthInitPrintContent();
        }
        // ��ӡҳ
        private void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
        {
            try
            {

                m_mthPrintTitleInfo(p_objPrintPageArg);
                m_mthPrintRectangleInfo(p_objPrintPageArg);
                m_mthPrintHeaderInfo(p_objPrintPageArg);
                m_mthAddDataToGrid(p_objPrintPageArg);
            }
            catch (Exception err)
            {
                clsPublicFunction.ShowInformationMessageBox(err.Message + "\r\n" + err.StackTrace);

            }
        }



        // ��ӡ����ʱ�Ĳ���
        private void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
        {
            m_intCurrentRecord = 0;
            intNowPage = 1;
            m_intCurrentRecord = 0;
            m_intCurrentRecord = 0;
            m_intCurrentContentRow = 0;
            m_intCurrentContentRow = 0;
            m_intCurrentPagePrintRow = 1;
            intNowPage = 1;
            blnBeginPrintNewRecord = true;
            m_intRowNumberForPrintData = 0;
            m_intPosY = (int)enmRecordRectangleInfo.TopY + 130;
        }



        #region �йش�ӡ������
        /// <summary>
        /// ���д�ӡ������
        /// </summary>
        private clsGeneralNurseRecordContent_GXDataInfo[] m_objPrintDataArr;

        /// <summary>
        /// ������Σ�ػ����¼�ģ���ӡ�����ĵ���
        /// </summary>		
        private clsPrintRichTextContext m_objPrintContext;
        /// <summary>
        /// ÿ����ʾ�ĺ��֣����̼�¼������ĸ������������Ŀ
        /// </summary>
        private class clsPrintLenth_IntensiveTendRecord
        {
            public int m_intPrintLenth_RecordContent;		//���̼�¼
            public int m_intPrintLenth_Temperature;			//����
            public int m_intPrintLenth_Breath;				//����
            public int m_intPrintLenth_Mind;				//��־
            public int m_intPrintLenth_Pulse;				//����
            public int m_intPrintLenth_BloodPressure;		//Ѫѹ	
            public int m_intPrintLenth_Pupil;				//ͫ�ף���С��		
            public int m_intPrintLenth_Echo;				//����		
            public int m_intPrintLenth_In;					//����
            public int m_intPrintLenth_Out;					//�ų�		
        }

        /// <summary>
        /// ��ǰ�е�Y����
        /// </summary>
        int m_intPosY = (int)enmRecordRectangleInfo.TopY + 130;
        /// <summary>
        /// ÿ�������еĸ߶�
        /// </summary>
        int intTempDeltaY = 40;

        private clsPrintLenth_IntensiveTendRecord m_objPrintLenth;
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
        /// ҽԺ������
        /// </summary>
        private Font m_fotHosNameFont;
        /// <summary>
        /// ��С������
        /// </summary>
        private Font m_fotTinyFont;
        /// <summary>
        /// �߿򻭱�
        /// </summary>
        private Pen m_GridPen;
        private Pen m_GridPenBold;

        private Pen m_GridRedPen;

        /// <summary>
        /// ˢ��
        /// </summary>
        private SolidBrush m_slbBrush;
        /// <summary>
        /// ��¼��ӡ���ڼ�ҳ
        /// </summary>
        private int intNowPage = 1;
        /// <summary>
        /// ��ǰ��ӡ�ļ�¼�����
        /// </summary>
        private int m_intCurrentRecord = 0;

        /// <summary>
        /// �ɼ�¼����,׼����ӡһ���¼�¼
        /// </summary>
        bool m_blnBeginPrintNewRecord = true;

        /// <summary>
        /// �ɼ�¼����,׼����ӡһ���¼�¼
        /// </summary>
        bool blnBeginPrintNewRecord = true;

        /// <summary>
        /// ��ǰ��¼�����������޸ĵĴε�����
        /// </summary>
        private int m_intNowRowInOneRecord = 0;

        /// <summary>
        /// ����Ҫ������ʷ�ۼ�����ǰ��¼����
        /// </summary>
        private string[][] m_strValueArr;

        /// <summary>
        /// Ҫ��ӡ�����еĻ����¼
        /// </summary>
        //private clsIntensiveTendRecord [] objGeneralTendRecordForPrint=null;
        /// <summary>
        /// ��ȡ�������
        /// </summary>
        private clsPrintPageSettingForRecord m_objPageSetting;
        /// <summary>
        /// ��ӡ�Ĳ��˻�����Ϣ��
        /// </summary>
        private class clsEveryRecordPageInfo
        {
            public string strPatientName;
            public string strSex;
            public string strAge;
            public string strBedNo;
            public string strAreaName;
            public string strInPatientID;
            //public int intCurrentPate;
            //public int intTotalPages;
            public string strPrintDate;
        }

        /// <summary>
        /// ���ӵ���Ϣ
        /// </summary>
        private enum enmRecordRectangleInfo
        {
            /// <summary>
            /// ���ӵĶ���
            /// </summary>
            TopY = 150,

            /// <summary>
            /// ��ͷ��һ���ָ���
            /// </summary>
            RowsMark1 = 50,
            /// <summary>
            /// ��ͷ�ڶ����ָ���
            /// </summary>
            RowsMark2 = 100,


            ///<summary>
            /// ���ӵ����
            /// </summary>
            LeftX = 20,
            /// <summary>
            /// ���ӵ��Ҷ�
            /// </summary>
            RightX = 1135,
            /// <summary>
            /// ����ÿ�еĲ���
            /// </summary>
            RowStep = 50,
            /// <summary>
            /// ���ӵ�����
            /// </summary>
            RowLinesNum = 13,
            /// <summary>
            /// �����ڸ�������Ը��Ӷ��˵Ĵ�ֱƫ��
            /// </summary>
            VOffSet = 20,
            /// <summary>
            /// �е���Ŀ
            /// </summary>
            ColumnsNum = 18,
            /// <summary>
            /// ��һ�������(X),ʱ��Σ�����ߣ�
            /// </summary>			
            ColumnsMark1 = 40,
            /// <summary>
            /// �ڶ��������(X)����㣨����ߣ�
            /// </summary>
            ColumnsMark2 = 105,
            //ColumnsMark2=150,

            /// <summary>
            /// ��3�������(X)��С�㣨����ߣ�
            /// </summary>
            ColumnsMark3 = ColumnsMark2 + m_intWidth,
            /// <summary>
            /// ��4�������(X)��θҺ������ߣ�
            /// </summary>
            ColumnsMark4 = ColumnsMark3 + m_intWidth,

            /// <summary>
            /// ��5�������(X)����֭������ߣ�
            /// </summary>
            ColumnsMark5 = ColumnsMark4 + m_intWidth,

            /// <summary>
            /// ��6�����(X)����Һ������ߣ�
            /// </summary>
            ColumnsMark6 = ColumnsMark5 + m_intWidth,

            /// <summary>
            /// ��7�����(X)����Һ������ߣ�
            /// </summary>
            ColumnsMark7 = ColumnsMark6 + m_intWidth,

            /// <summary>
            /// ��8�����(X)����������������ߣ�
            /// </summary>
            ColumnsMark8 = ColumnsMark7 + m_intWidth,

            /// <summary>
            /// ��9�����(X)�������Զ��壨����ߣ�
            /// </summary>
            ColumnsMark9 = ColumnsMark8 + m_intWidth,

            /// <summary>
            /// ��10�����(X)����ˮ������ߣ�
            /// </summary>
            ColumnsMark10 = ColumnsMark9 + m_intWidth,

            /// <summary>
            /// ��11�����(X)��ʳ�����ߣ�
            /// </summary>
            ColumnsMark11 = ColumnsMark10 + m_intWidth,

            /// <summary>
            /// ��12�����(X)��Ѫ������ߣ�
            /// </summary>
            ColumnsMark12 = ColumnsMark11 + m_intWidth,

            /// <summary>
            /// ��13�����(X)����������ߣ�
            /// </summary>
            ColumnsMark13 = ColumnsMark12 + m_intWidth,

            /// <summary>
            /// ��14�����(X)����ˮ������ߣ�
            /// </summary>
            ColumnsMark14 = ColumnsMark13 + m_intWidth,

            /// <summary>
            /// ��15�����(X)����ˮ������ߣ�
            /// </summary>
            ColumnsMark15 = ColumnsMark14 + m_intWidth,
            /// <summary>
            /// ��16�����(X),��������������ߣ�
            /// </summary>
            ColumnsMark16 = ColumnsMark15 + m_intWidth,
            /// <summary>
            /// ��17�����(X),�����Զ��壨����ߣ�
            /// </summary>
            ColumnsMark17 = ColumnsMark16 + m_intWidth,
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
            RecordSign2,
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
                        m_fReturnPoint = new PointF(320f, 60f);
                        break;
                    case (int)enmItemDefination.Page_Name_Title:
                        m_fReturnPoint = new PointF(225f, 100f);
                        break;
                    case (int)enmItemDefination.Name_Title:
                        m_fReturnPoint = new PointF(20f, 150f);
                        break;
                    case (int)enmItemDefination.Name:
                        m_fReturnPoint = new PointF(70f, 150f);
                        break;

                    case (int)enmItemDefination.Sex_Title:
                        m_fReturnPoint = new PointF(150f, 150f);
                        break;
                    case (int)enmItemDefination.Sex:
                        m_fReturnPoint = new PointF(200f, 150f);
                        break;

                    case (int)enmItemDefination.Age_Title:
                        m_fReturnPoint = new PointF(250f, 150f);
                        break;
                    case (int)enmItemDefination.Age:
                        m_fReturnPoint = new PointF(300f, 150f);
                        break;

                    case (int)enmItemDefination.Dept_Name_Title:
                        m_fReturnPoint = new PointF(350f, 150f);
                        break;
                    case (int)enmItemDefination.Dept_Name:
                        m_fReturnPoint = new PointF(400f, 150f);
                        break;

                    case (int)enmItemDefination.BedNo_Title:
                        m_fReturnPoint = new PointF(550f, 150f);
                        break;
                    case (int)enmItemDefination.BedNo:
                        m_fReturnPoint = new PointF(600f, 150f);
                        break;

                    case (int)enmItemDefination.InPatientID_Title:
                        m_fReturnPoint = new PointF(650f, 150f);
                        break;
                    case (int)enmItemDefination.InPatientID:
                        m_fReturnPoint = new PointF(720f, 150f);
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

        #region �������ֲ���
        /// <summary>
        /// �������ֲ���
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {
            clsEveryRecordPageInfo objEveryRecordPageInfo = new clsEveryRecordPageInfo();
            //************************************************			

            objEveryRecordPageInfo.strPatientName = m_objPrintInfoOne.m_strInPatientName;
            objEveryRecordPageInfo.strBedNo = m_objPrintInfoOne.m_strBedName;
            objEveryRecordPageInfo.strSex = m_objPrintInfoOne.m_strSex;
            objEveryRecordPageInfo.strInPatientID = m_objPrintInfoOne.m_strHISInPatientID;
            //objEveryRecordPageInfo.strPrintDate=( m_objPrintInfoOne.m_strInPatentID!="")? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") :"";		



            m_mthDrawMultiString(m_fotSmallFont, (float)enmRecordRectangleInfo.LeftX, 40, (float)enmRecordRectangleInfo.RightX, 70, 0, 0, clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, e);

            m_mthDrawMultiString(m_fotTitleFont, (float)enmRecordRectangleInfo.LeftX, 70, (float)enmRecordRectangleInfo.RightX, 170, 0, 0, "��  ��  ��  ��  ��  ��", e);


            e.Graphics.DrawString("���ң�", m_fotSmallFont, m_slbBrush, (float)enmRecordRectangleInfo.LeftX, 120);
            e.Graphics.DrawString(m_objPrintInfoOne.m_strDeptName, m_fotSmallFont, m_slbBrush, (float)enmRecordRectangleInfo.LeftX + 40, 120);

            e.Graphics.DrawString("���ţ�", m_fotSmallFont, m_slbBrush, (float)enmRecordRectangleInfo.ColumnsMark3 + 150, 120);
            e.Graphics.DrawString(objEveryRecordPageInfo.strBedNo, m_fotSmallFont, m_slbBrush, (float)enmRecordRectangleInfo.ColumnsMark3 + 190, 120);

            e.Graphics.DrawString("������", m_fotSmallFont, m_slbBrush, (float)enmRecordRectangleInfo.ColumnsMark5 + 160, 120);
            e.Graphics.DrawString(objEveryRecordPageInfo.strPatientName, m_fotSmallFont, m_slbBrush, (float)enmRecordRectangleInfo.ColumnsMark6 + 140, 120);

            e.Graphics.DrawString("סԺ�ţ�", m_fotSmallFont, m_slbBrush, (float)enmRecordRectangleInfo.ColumnsMark8 + 80, 120);
            e.Graphics.DrawString(objEveryRecordPageInfo.strInPatientID, m_fotSmallFont, m_slbBrush, (float)enmRecordRectangleInfo.ColumnsMark8 - 20 + 160, 120);


            e.Graphics.DrawString("��ӡ���ڣ�", m_fotSmallFont, m_slbBrush, 960, 120);
            e.Graphics.DrawString(DateTime.Now.ToString("yyyy��MM��dd��"), m_fotSmallFont, m_slbBrush, 1030, 120);
        }
        #endregion
        #region �Զ����DrawString
        private void m_mthDrawMultiString(Font fotNormal, Font fotSmall, int iLimitLenth, float x, float y, float x1, float y1, float xOff, float yOff, string strContent, System.Drawing.Printing.PrintPageEventArgs e)
        {

            RectangleF drawRect = new RectangleF(x, y + 1, x1 - x, y1 - y);
            RectangleF drawRectNormal = new RectangleF(x, y + yOff, x1 - x, (float)y1 - (float)y);

            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = System.Drawing.StringAlignment.Center;
            strFormat.FormatFlags = System.Drawing.StringFormatFlags.LineLimit;

            if (strContent.Length > iLimitLenth)
            {
                e.Graphics.DrawString(strContent, fotSmall, m_slbBrush, drawRect, strFormat);
            }
            else
            {
                e.Graphics.DrawString(strContent, fotNormal, m_slbBrush, drawRectNormal, strFormat);

            }

        }

        private void m_mthDrawMultiString(float p_flaX, float p_flaY, float p_flaX1, float p_flaY1, DateTime p_dtmDate, System.Drawing.Printing.PrintPageEventArgs e, bool p_blnFillContent)
        {
            float m_flaTemp = (p_flaY1 - p_flaY - 30) / 6;
            p_flaY = p_flaY + 30;
            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = System.Drawing.StringAlignment.Center;
            RectangleF drawRectNormal;
            //���ֵ
            if (p_blnFillContent == true)
            {
                drawRectNormal = new RectangleF(p_flaX, p_flaY, p_flaX1 - p_flaX, 60);
                e.Graphics.DrawString(p_dtmDate.Year.ToString(), m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            }

            //��
            if (p_blnFillContent == false)
            {
                drawRectNormal = new RectangleF(p_flaX, p_flaY + m_flaTemp * 1, p_flaX1 - p_flaX, 60);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            }

            //��ֵ
            if (p_blnFillContent == true)
            {
                drawRectNormal = new RectangleF(p_flaX, p_flaY + m_flaTemp * 2, p_flaX1 - p_flaX, 60);
                e.Graphics.DrawString(p_dtmDate.Month.ToString(), m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            }


            //�·� 
            if (p_blnFillContent == false)
            {
                drawRectNormal = new RectangleF(p_flaX, p_flaY + m_flaTemp * 3, p_flaX1 - p_flaX, 60);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            }

            //����ֵ
            if (p_blnFillContent == true)
            {
                drawRectNormal = new RectangleF(p_flaX, p_flaY + m_flaTemp * 4, p_flaX1 - p_flaX, 60);
                e.Graphics.DrawString(p_dtmDate.Day.ToString(), m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            }

            //����
            if (p_blnFillContent == false)
            {
                drawRectNormal = new RectangleF(p_flaX, p_flaY + m_flaTemp * 5, p_flaX1 - p_flaX, 60);
                e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            }


            strFormat = null;
        }
        private void m_mthDrawSummaryString(float p_flaX, float p_flaY, float p_flaX1, float p_flaY1, string p_strValue, System.Drawing.Printing.PrintPageEventArgs e, bool p_blnFillContent, int p_intSummaryType)
        {


            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = System.Drawing.StringAlignment.Center;
            RectangleF drawRectNormal;
            //������--����
            if (p_blnFillContent == false)
            {
                p_flaX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark5;
                p_flaX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10;
                drawRectNormal = new RectangleF(p_flaX + 5, p_flaY + 10, p_flaX1 - p_flaX, 60);
                e.Graphics.DrawString("����������:                  ����", m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            }

            //������--ֵ
            if (p_blnFillContent == true && p_intSummaryType == 1)
            {
                p_flaX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7;
                p_flaX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9;
                drawRectNormal = new RectangleF(p_flaX, p_flaY + 10, p_flaX1 - p_flaX, 60);
                e.Graphics.DrawString(p_strValue, m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            }

            //������&����--����
            if (p_blnFillContent == false)
            {
                p_flaX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2;
                p_flaX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7;
                drawRectNormal = new RectangleF(p_flaX, p_flaY + 30, p_flaX1 - p_flaX, 60);

                e.Graphics.DrawString("�ܳ���:                      ����  ����: ", m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            }


            //������--ֵ
            if (p_blnFillContent == true && p_intSummaryType == 2)
            {
                p_flaX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark3;
                p_flaX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark5;
                drawRectNormal = new RectangleF(p_flaX, p_flaY + 30, p_flaX1 - p_flaX, 60);

                e.Graphics.DrawString(p_strValue, m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            }
            //����--ֵ
            if (p_blnFillContent == true && p_intSummaryType == 3)
            {
                p_flaX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7;
                p_flaX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10;
                drawRectNormal = new RectangleF(p_flaX, p_flaY + 30, p_flaX1 - p_flaX, 60);

                e.Graphics.DrawString(p_strValue, m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            }

            //������--����
            if (p_blnFillContent == false)
            {
                p_flaX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10;
                p_flaX1 = (int)enmRecordRectangleInfo.RightX;
                drawRectNormal = new RectangleF(p_flaX, p_flaY + 15, p_flaX1 - p_flaX, 60);

                e.Graphics.DrawString("������:                   ����", m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            }
            //������--ֵ
            if (p_blnFillContent == true && p_intSummaryType == 4)
            {
                p_flaX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13;
                p_flaX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15;
                drawRectNormal = new RectangleF(p_flaX, p_flaY + 15, p_flaX1 - p_flaX, 60);

                e.Graphics.DrawString(p_strValue, m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            }

            //ǩ��--����
            if (p_blnFillContent == false)
            {
                p_flaX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark14 - 15;
                p_flaX1 = (int)enmRecordRectangleInfo.RightX;
                drawRectNormal = new RectangleF(p_flaX, p_flaY + 35, p_flaX1 - p_flaX, 60);

                e.Graphics.DrawString("ǩ��:", m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            }
            //ǩ��--ֵ
            if (p_blnFillContent == true && p_intSummaryType == 5)
            {
                p_flaX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16;
                p_flaX1 = (int)enmRecordRectangleInfo.RightX;
                drawRectNormal = new RectangleF(p_flaX, p_flaY + 35, p_flaX1 - p_flaX, 60);
                e.Graphics.DrawString(p_strValue, m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            }

            strFormat = null;
        }
        private void m_mthDrawMultiString(float p_flaX, float p_flaY, float p_flaX1, float p_flaY1, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float m_flaTemp = (p_flaY1 - p_flaY) / 5;

            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = System.Drawing.StringAlignment.Center;
            //��һʱ���
            RectangleF drawRectNormal = new RectangleF(p_flaX, p_flaY + m_flaTemp * 0, p_flaX1 - p_flaX, p_flaY + m_flaTemp * 1 / 3 * 1);
            e.Graphics.DrawString("������ʱ", m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            drawRectNormal = new RectangleF(p_flaX, p_flaY + m_flaTemp * 1 / 3 * 1, p_flaX1 - p_flaX, p_flaY + m_flaTemp * 1 / 3 * 2);
            e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            drawRectNormal = new RectangleF(p_flaX, p_flaY + m_flaTemp * 1 / 3 * 2, p_flaX1 - p_flaX, p_flaY + m_flaTemp * 1 / 3 * 3);
            e.Graphics.DrawString("������ʱ", m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            //�ڶ�ʱ���
            drawRectNormal = new RectangleF(p_flaX, p_flaY + m_flaTemp * 1, p_flaX1 - p_flaX, p_flaY + m_flaTemp * 2 / 3 * 1);
            e.Graphics.DrawString("������ʱ", m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            drawRectNormal = new RectangleF(p_flaX, p_flaY + m_flaTemp * 1 + m_flaTemp / 3 * 1, p_flaX1 - p_flaX, p_flaY + m_flaTemp * 2 / 3 * 2);
            e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            drawRectNormal = new RectangleF(p_flaX, p_flaY + m_flaTemp * 1 + m_flaTemp / 3 * 2, p_flaX1 - p_flaX, p_flaY + m_flaTemp * 2 / 3 * 3);
            e.Graphics.DrawString("������ʱ", m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            //����ʱ���
            drawRectNormal = new RectangleF(p_flaX, p_flaY + m_flaTemp * 2, p_flaX1 - p_flaX, p_flaY + m_flaTemp * 3 / 3 * 1);
            e.Graphics.DrawString("������ʱ", m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            drawRectNormal = new RectangleF(p_flaX, p_flaY + m_flaTemp * 2 + m_flaTemp / 3 * 1, p_flaX1 - p_flaX, p_flaY + m_flaTemp * 3 / 3 * 2);
            e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            drawRectNormal = new RectangleF(p_flaX, p_flaY + m_flaTemp * 2 + m_flaTemp / 3 * 2, p_flaX1 - p_flaX, p_flaY + m_flaTemp * 3 / 3 * 3);
            e.Graphics.DrawString("�賿��ʱ", m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            //����ʱ���
            drawRectNormal = new RectangleF(p_flaX, p_flaY + m_flaTemp * 3, p_flaX1 - p_flaX, p_flaY + m_flaTemp * 4 / 3 * 1);
            e.Graphics.DrawString("�賿��ʱ", m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            drawRectNormal = new RectangleF(p_flaX, p_flaY + m_flaTemp * 3 + m_flaTemp / 3 * 1, p_flaX1 - p_flaX, p_flaY + m_flaTemp * 4 / 3 * 2);
            e.Graphics.DrawString("��", m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);
            drawRectNormal = new RectangleF(p_flaX, p_flaY + m_flaTemp * 3 + m_flaTemp / 3 * 2, p_flaX1 - p_flaX, p_flaY + m_flaTemp * 4 / 3 * 3);
            e.Graphics.DrawString("������ʱ", m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);

            //�ܼ�
            drawRectNormal = new RectangleF(p_flaX, p_flaY + m_flaTemp * 4 + 15, p_flaX1 - p_flaX, p_flaY1);
            e.Graphics.DrawString("��   ��", m_fotSmallFont, m_slbBrush, drawRectNormal, strFormat);


            strFormat = null;
        }

        private void m_mthDrawMultiString(Font fotNormal, float x, float y, float x1, float y1, float xOff, float yOff, string strContent, System.Drawing.Printing.PrintPageEventArgs e)
        {

            RectangleF drawRect = new RectangleF(x, y + yOff, x1 - x, y1 - y);
            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = System.Drawing.StringAlignment.Center;
            strFormat.FormatFlags = System.Drawing.StringFormatFlags.LineLimit;

            e.Graphics.DrawString(strContent, fotNormal, m_slbBrush, drawRect, strFormat);


        }

        #endregion

        #region ���������Ŀ
        /// <summary>
        /// ���������Ŀ
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintHeaderInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {

            m_mthDrawMultiString((float)enmRecordRectangleInfo.LeftX,
                (float)enmRecordRectangleInfo.TopY + (float)enmRecordRectangleInfo.RowStep + 40,
                (float)enmRecordRectangleInfo.LeftX + (float)enmRecordRectangleInfo.ColumnsMark1,
                (float)enmRecordRectangleInfo.TopY + (float)enmRecordRectangleInfo.RowStep * 6 + 40, DateTime.Now,
                e, false);
            m_mthDrawMultiString((float)enmRecordRectangleInfo.LeftX,
                (float)enmRecordRectangleInfo.TopY + (float)enmRecordRectangleInfo.RowStep * 7 + 40,
                (float)enmRecordRectangleInfo.LeftX + (float)enmRecordRectangleInfo.ColumnsMark1,
                (float)enmRecordRectangleInfo.TopY + (float)enmRecordRectangleInfo.RowStep * 12 + 40, DateTime.Now,
                e, false);


            //������������
            m_mthDrawMultiString((float)enmRecordRectangleInfo.LeftX + (float)enmRecordRectangleInfo.ColumnsMark1,
                (float)enmRecordRectangleInfo.TopY + (float)enmRecordRectangleInfo.RowStep + 40,
                (float)enmRecordRectangleInfo.LeftX + (float)enmRecordRectangleInfo.ColumnsMark2,
                (float)enmRecordRectangleInfo.TopY + (float)enmRecordRectangleInfo.RowStep * 6 + 40, e);
            //������������
            m_mthDrawMultiString((float)enmRecordRectangleInfo.LeftX + (float)enmRecordRectangleInfo.ColumnsMark1,
                (float)enmRecordRectangleInfo.TopY + (float)enmRecordRectangleInfo.RowStep * 7 + 40,
                (float)enmRecordRectangleInfo.LeftX + (float)enmRecordRectangleInfo.ColumnsMark2,
                (float)enmRecordRectangleInfo.TopY + (float)enmRecordRectangleInfo.RowStep * 12 + 40, e);
            //���ϰ벿�ֵ��ܽ����
            m_mthDrawSummaryString((float)enmRecordRectangleInfo.LeftX + (float)enmRecordRectangleInfo.ColumnsMark1,
                (float)enmRecordRectangleInfo.TopY + (float)enmRecordRectangleInfo.RowStep * 5 + 40,
                (float)enmRecordRectangleInfo.LeftX + (float)enmRecordRectangleInfo.ColumnsMark2,
                (float)enmRecordRectangleInfo.TopY + (float)enmRecordRectangleInfo.RowStep * 6 + 40, "", e, false, 0);

            //���°벿�ֵ��ܽ����
            m_mthDrawSummaryString((float)enmRecordRectangleInfo.LeftX + (float)enmRecordRectangleInfo.ColumnsMark1,
                (float)enmRecordRectangleInfo.TopY + (float)enmRecordRectangleInfo.RowStep * 11 + 40,
                (float)enmRecordRectangleInfo.LeftX + (float)enmRecordRectangleInfo.ColumnsMark2,
                (float)enmRecordRectangleInfo.TopY + (float)enmRecordRectangleInfo.RowStep * 12 + 40, "", e, false, 0);

            //����
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2,
                (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10,
                (int)enmRecordRectangleInfo.TopY + 40, 0, 10, "��      ��", e);
            //����
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10,
                (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.RightX,
                (int)enmRecordRectangleInfo.TopY + 40, 0, 10, "��      ��", e);
            //���
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2,
                (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark3,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 1 + 40, 2, 15, "���", e);

            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 6 + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark3,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 7 + 40, 2, 15, "���", e);

            //С��
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark3,
                (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark4,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 1 + 40, 2, 15, "С��", e);
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark3,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 6 + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark4,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 7 + 40, 2, 15, "С��", e);
            //θҺ
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark4,
                (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark5,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 1 + 40, 2, 15, "θҺ", e);
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark4,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 6 + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark5,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 7 + 40, 2, 15, "θҺ", e);
            //��֭
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark5,
                (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark6,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 1 + 40, 2, 15, "��֭", e);
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark5,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 6 + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark6,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 7 + 40, 2, 15, "��֭", e);
            //��Һ
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark6,
                (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 1 + 40, 2, 15, "��Һ", e);
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark6,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 6 + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 7 + 40, 2, 15, "��Һ", e);
            //��Һ
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7,
                (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark8,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 1 + 40, 2, 15, "��Һ", e);
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 6 + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark8,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 7 + 40, 2, 15, "��Һ", e);
            //����
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark8,
                (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 1 + 40, 2, 15, "����", e);
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark8,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 6 + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 7 + 40, 2, 15, "����", e);
            //�Զ������ ��ӡʱ��ֵ

            //��ˮ
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10,
                (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 1 + 40, 2, 15, "��ˮ", e);
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 6 + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 7 + 40, 2, 15, "��ˮ", e);
            //ʳ��
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11,
                (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 1 + 40, 2, 15, "ʳ��", e);
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 6 + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 7 + 40, 2, 15, "ʳ��", e);
            //Ѫ
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12,
                (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 1 + 40, 2, 15, "Ѫ", e);
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 6 + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 7 + 40, 2, 15, "Ѫ", e);
            //��
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13,
                (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark14,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 1 + 40, 2, 15, "��", e);
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 6 + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark14,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 7 + 40, 2, 15, "��", e);
            //��ˮ
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark14,
                (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 1 + 40, 2, 15, "��ˮ", e);
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark14,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 6 + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 7 + 40, 2, 15, "��ˮ", e);
            //��ˮ
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15,
                (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 1 + 40, 2, 15, "��ˮ", e);
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 6 + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 7 + 40, 2, 15, "��ˮ", e);
            //����
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16,
                (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark17,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 1 + 40, 2, 15, "����", e);
            m_mthDrawMultiString(this.m_fotHeaderFont, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 6 + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark17,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 7 + 40, 2, 15, "����", e);


            e.Graphics.DrawString("��  ��", this.m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + 5, (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep);
            e.Graphics.DrawString("��  ��", this.m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark1, (int)enmRecordRectangleInfo.TopY + 15);
            e.Graphics.DrawString("��  ��", this.m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + 5, (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 7 + 20);
            e.Graphics.DrawString("��  ��", this.m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark1, (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 6 + 40 + 5);




        }
        #endregion

        #region ������
        /// <summary>
        ///  ������
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintRectangleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {



            #region//�����Ӻ���
            //�����Ӻ���
            for (int i1 = 0; i1 <= (int)enmRecordRectangleInfo.RowLinesNum; i1++)
            {
                if (i1 == 0)
                    e.Graphics.DrawLine(m_GridPenBold, (int)enmRecordRectangleInfo.LeftX,
                        (int)enmRecordRectangleInfo.TopY,
                        (int)enmRecordRectangleInfo.RightX,
                        (int)enmRecordRectangleInfo.TopY);
                else if (i1 == 1)
                    e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2,
                        (int)enmRecordRectangleInfo.TopY + 40,
                        (int)enmRecordRectangleInfo.RightX,
                        (int)enmRecordRectangleInfo.TopY + 40);
                else if (i1 == 7)
                    e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX,
                        (int)enmRecordRectangleInfo.TopY + ((int)enmRecordRectangleInfo.RowStep * (i1 - 1)) + 40,
                        (int)enmRecordRectangleInfo.RightX,
                        (int)enmRecordRectangleInfo.TopY + ((int)enmRecordRectangleInfo.RowStep * (i1 - 1)) + 40);
                else if (i1 == 8)
                    e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX,
                        (int)enmRecordRectangleInfo.TopY + ((int)enmRecordRectangleInfo.RowStep * (i1 - 1)) + 40,
                        (int)enmRecordRectangleInfo.RightX,
                        (int)enmRecordRectangleInfo.TopY + ((int)enmRecordRectangleInfo.RowStep * (i1 - 1)) + 40);
                else if (i1 == 2)
                    e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX,
                        (int)enmRecordRectangleInfo.TopY + ((int)enmRecordRectangleInfo.RowStep * (i1 - 1)) + 40,
                        (int)enmRecordRectangleInfo.RightX,
                        (int)enmRecordRectangleInfo.TopY + ((int)enmRecordRectangleInfo.RowStep * (i1 - 1)) + 40);
                else if (i1 == 13)
                    e.Graphics.DrawLine(m_GridPenBold, (int)enmRecordRectangleInfo.LeftX,
                        (int)enmRecordRectangleInfo.TopY + ((int)enmRecordRectangleInfo.RowStep * (i1 - 1)) + 40,
                        (int)enmRecordRectangleInfo.RightX,
                        (int)enmRecordRectangleInfo.TopY + ((int)enmRecordRectangleInfo.RowStep * (i1 - 1)) + 40);
                else //if(i1==2)
                    e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark1,
                        (int)enmRecordRectangleInfo.TopY + ((int)enmRecordRectangleInfo.RowStep * (i1 - 1)) + 40,
                        (int)enmRecordRectangleInfo.RightX,
                        (int)enmRecordRectangleInfo.TopY + ((int)enmRecordRectangleInfo.RowStep * (i1 - 1)) + 40);
            }

            #endregion
            #region ����������
            int intHeight = (int)enmRecordRectangleInfo.TopY + 40 + (int)enmRecordRectangleInfo.RowStep * ((int)enmRecordRectangleInfo.RowLinesNum - 1);


            //��������� ����
            e.Graphics.DrawLine(m_GridPenBold, (int)enmRecordRectangleInfo.LeftX, (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.LeftX, intHeight);
            //��ʱ�������
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark1, (int)enmRecordRectangleInfo.TopY + 40 + (int)enmRecordRectangleInfo.RowStep,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark1, (int)enmRecordRectangleInfo.TopY + 40 + (int)enmRecordRectangleInfo.RowStep * 6);

            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark1, (int)enmRecordRectangleInfo.TopY + 40 + (int)enmRecordRectangleInfo.RowStep * 7,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark1, intHeight);
            //���������� ����
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2, (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2, intHeight);

            //���¶����
            intHeight = (int)enmRecordRectangleInfo.TopY + 40 + (int)enmRecordRectangleInfo.RowStep * 5;
            int intHeight1 = (int)enmRecordRectangleInfo.TopY + 40 + (int)enmRecordRectangleInfo.RowStep * ((int)enmRecordRectangleInfo.RowLinesNum - 2);

            //��С��������
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark3, (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark3, intHeight);

            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark3, (int)enmRecordRectangleInfo.RowStep + intHeight,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark3, intHeight1);

            //��θҺ�������
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark4, (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark4, intHeight);

            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark4, (int)enmRecordRectangleInfo.RowStep + intHeight,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark4, intHeight1);

            //����֭�������
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark5, (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark5, intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark5, (int)enmRecordRectangleInfo.RowStep + intHeight,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark5, intHeight1);

            //����Һ�������
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark6, (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark6, intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark6, (int)enmRecordRectangleInfo.RowStep + intHeight,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark6, intHeight1);

            //����Һ�������
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7, (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7, intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7, (int)enmRecordRectangleInfo.RowStep + intHeight,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7, intHeight1);

            //�����������������
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark8, (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark8, intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark8, (int)enmRecordRectangleInfo.RowStep + intHeight,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark8, intHeight1);

            //�������Զ���������
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9, (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9, intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9, (int)enmRecordRectangleInfo.RowStep + intHeight,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9, intHeight1);


            //���¶����,�����м���
            intHeight = (int)enmRecordRectangleInfo.TopY + 40 + (int)enmRecordRectangleInfo.RowStep * ((int)enmRecordRectangleInfo.RowLinesNum - 1);
            //���м���
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10, (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10, intHeight);

            //���¶����
            intHeight = (int)enmRecordRectangleInfo.TopY + 40 + (int)enmRecordRectangleInfo.RowStep * 5;


            //������-ʳ�������
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11, (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11, intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11, (int)enmRecordRectangleInfo.RowStep + intHeight,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11, intHeight1);


            //������-Ѫ�����
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12, (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12, intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12, (int)enmRecordRectangleInfo.RowStep + intHeight,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12, intHeight1);

            //������-�������
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13, (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13, intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13, (int)enmRecordRectangleInfo.RowStep + intHeight,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13, intHeight1);

            //������-��ˮ�����
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark14, (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark14, intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark14, (int)enmRecordRectangleInfo.RowStep + intHeight,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark14, intHeight1);

            //������-��ˮ�����
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15, (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15, intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15, (int)enmRecordRectangleInfo.RowStep + intHeight,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15, intHeight1);
            //������-���������
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16, (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16, intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16, (int)enmRecordRectangleInfo.RowStep + intHeight,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16, intHeight1);
            //������-�Զ��������
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark17, (int)enmRecordRectangleInfo.TopY + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark17, intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark17, (int)enmRecordRectangleInfo.RowStep + intHeight,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark17, intHeight1);


            //���ұ����� ����
            e.Graphics.DrawLine(m_GridPenBold, (int)enmRecordRectangleInfo.RightX, (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.RightX, intHeight1 + (int)enmRecordRectangleInfo.RowStep);

            //��б��

            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX, (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2, (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep + 40);

            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX, (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 6 + 40,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2, (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 7 + 40);


            #endregion

        }


        #endregion

        #region ��ӡ����ʵ��


        /*��¼��ҳ��ǰ�Ĵ�ӡ����*/
        int intNowRow = 1;

        /*��¼��ǰ��ӡ������¼��m_objPrintDataArr�е���ţ����ڻ�ҳ����Ŵ�ӡ*/
        private int m_intRowNumberForPrintData = 0;

        #region ������ݵ����
        /// <summary>
        /// ������ݵ����
        /// </summary>
        /// <param name="e"></param>
        private void m_mthAddDataToGrid(System.Drawing.Printing.PrintPageEventArgs e)
        {

            DateTime dtmFlagTime;
            /*��¼��ҳ��ǰ�Ĵ�ӡ����*/
            intNowRow = 1;

            //�����ǰΪ�����˳�

            if (m_objPrintInfo.Length == 0) return;

            m_intCurrentRecord = 0;

            m_intCurrentPagePrintRow = 1;
            e.Graphics.DrawString("��" + intNowPage.ToString() + "ҳ", m_fotSmallFont, m_slbBrush, (float)enmRecordRectangleInfo.ColumnsMark8 - 20 + 340, 120);

            //��ӡ��ѭ��
            for (int i1 = m_intCurrentContentRow; i1 < m_objPrintInfo.Length; i1++)
            {

                //m_intCurrentContentRow=i1;

                enmReturnValue m_enmRe = m_blnPrintOneValueRegister(e, m_intPosY);

                //���ݷ���ֵ����ҳ���
                if (m_enmRe == enmReturnValue.enmFaild)
                {
                    e.HasMorePages = false;
                    m_blnHasMorePage = false;
                }
                if (m_enmRe == enmReturnValue.enmNeedNextPage)
                {

                    m_blnHasMorePage = true;
                    e.HasMorePages = true;
                    return;
                }

                if (m_enmRe == enmReturnValue.enmSuccessed)
                {
                    m_blnHasMorePage = false;
                    e.HasMorePages = false;
                    blnBeginPrintNewRecord = true;
                }


            }




        }
        #endregion



        #region ֻ��ӡһ��
        /// <summary>
        /// ֻ��ӡһ��
        /// </summary>
        /// <param name="e"></param>
        /// <param name="p_intBottomY"></param>
        /// <returns></returns>
        private enmReturnValue m_blnPrintOneValueRegister(System.Drawing.Printing.PrintPageEventArgs e, int p_intBottomY)
        {
            //m_intPosY +=(int)enmRecordRectangleInfo.VOffSet;
            enmReturnValue enmIsRecordFinish = m_blnPrintOneMainRowValue(e);
            return enmIsRecordFinish;
        }

        #endregion

        private bool m_blnCheckPageChange(System.Drawing.Printing.PrintPageEventArgs e)
        {
            //����ǰ�г������һ�У��� >ҳ��������ʱ��ҳ
            if (m_intCurrentPagePrintRow > 2)/*��ȥ��ͷ3��������Ч����*/
            {
                m_intCurrentPagePrintRow = 1;
                intNowPage++;

                return true;
            }
            else return false;
        }

        #endregion
        private enmReturnValue m_blnPrintDetailValue(int p_intPosY, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //
            string m_strTemp = string.Empty;
            int m_intPosX = 0;
            int m_intPosX1 = 0;
            int m_intPosY = p_intPosY;//��ǰ��Y���� 
            int m_intPosY1 = 0;
            int m_intYOff = 15;
            com.digitalwave.Utility.Controls.ctlRichTextBox m_txtTemp = new ctlRichTextBox();

            for (int i1 = 0; i1 < 4; i1++)
            {
                m_intPosY = p_intPosY + (int)enmRecordRectangleInfo.RowStep * i1;//��ǰ��Y���� 
                m_intPosY1 = m_intPosY + (int)enmRecordRectangleInfo.RowStep * (i1 + 1);//��ǰ��Y1���� 
                //С��

                m_txtTemp.m_mthSetNewText(m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strOutItem1.Trim(), m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strOutItem1XML.Trim());
                m_strTemp = m_txtTemp.m_strGetRightText();


                if (m_strTemp.Trim().Length != 0)
                {
                    m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 0;//��ǰ��X����
                    m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 1;//��ǰ��X1����
                    m_mthDrawMultiString(this.m_fotSmallFont, this.m_fotSmallFont, 3, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, 0, m_intYOff, m_strTemp, e);
                }

                //С��


                m_txtTemp.m_mthSetNewText(m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strOutItem2.Trim(), m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strOutItem2XML.Trim());
                m_strTemp = m_txtTemp.m_strGetRightText();

                if (m_strTemp.Trim().Length != 0)
                {
                    m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 1;//��ǰ��X����
                    m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 2;//��ǰ��X1����
                    m_mthDrawMultiString(this.m_fotSmallFont, this.m_fotSmallFont, 3, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, 0, m_intYOff, m_strTemp, e);
                }
                //θҺ
                m_txtTemp.m_mthSetNewText(m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strOutItem3.Trim(), m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strOutItem3XML.Trim());
                m_strTemp = m_txtTemp.m_strGetRightText();

                if (m_strTemp.Trim().Length != 0)
                {
                    m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 2;//��ǰ��X����
                    m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 3;//��ǰ��X1����
                    m_mthDrawMultiString(this.m_fotSmallFont, this.m_fotSmallFont, 3, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, 0, m_intYOff, m_strTemp, e);
                }
                //��֭
                m_txtTemp.m_mthSetNewText(m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strOutItem4.Trim(), m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strOutItem4XML.Trim());
                m_strTemp = m_txtTemp.m_strGetRightText();

                if (m_strTemp.Trim().Length != 0)
                {
                    m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 3;//��ǰ��X����
                    m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 4;//��ǰ��X1����
                    m_mthDrawMultiString(this.m_fotSmallFont, this.m_fotSmallFont, 3, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, 0, m_intYOff, m_strTemp, e);
                }
                //��Һ
                m_txtTemp.m_mthSetNewText(m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strOutItem5.Trim(), m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strOutItem5XML.Trim());
                m_strTemp = m_txtTemp.m_strGetRightText();

                if (m_strTemp.Trim().Length != 0)
                {
                    m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 4;//��ǰ��X����
                    m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 5;//��ǰ��X1����
                    m_mthDrawMultiString(this.m_fotSmallFont, this.m_fotSmallFont, 3, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, 0, m_intYOff, m_strTemp, e);
                }
                //��Һ
                m_txtTemp.m_mthSetNewText(m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strOutItem6.Trim(), m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strOutItem6XML.Trim());
                m_strTemp = m_txtTemp.m_strGetRightText();

                if (m_strTemp.Trim().Length != 0)
                {
                    m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 5;//��ǰ��X����
                    m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 6;//��ǰ��X1����
                    m_mthDrawMultiString(this.m_fotSmallFont, this.m_fotSmallFont, 3, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, 0, m_intYOff, m_strTemp, e);
                }
                //����
                m_txtTemp.m_mthSetNewText(m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strOutItem7.Trim(), m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strOutItem7XML.Trim());
                m_strTemp = m_txtTemp.m_strGetRightText();

                if (m_strTemp.Trim().Length != 0)
                {
                    m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 6;//��ǰ��X����
                    m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 7;//��ǰ��X1����
                    m_mthDrawMultiString(this.m_fotSmallFont, this.m_fotTinyFont, 3, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, 0, m_intYOff, m_strTemp, e);
                }
                //�Զ���

                m_txtTemp.m_mthSetNewText(m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strOutItem8.Trim(), m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strOutItem7XML.Trim());
                m_strTemp = m_txtTemp.m_strGetRightText();

                if (m_strTemp.Trim().Length != 0)
                {
                    m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 7;//��ǰ��X����
                    m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 8;//��ǰ��X1����
                    m_mthDrawMultiString(this.m_fotSmallFont, this.m_fotSmallFont, 3, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, 0, m_intYOff, m_strTemp, e);
                }

                //��ˮ
                m_strTemp = m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strInItem1.Trim();

                if (m_strTemp.Trim().Length != 0)
                {
                    m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 8;//��ǰ��X����
                    m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 9;//��ǰ��X1����
                    m_mthDrawMultiString(this.m_fotSmallFont, this.m_fotSmallFont, 3, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, 0, m_intYOff, m_strTemp, e);
                }
                //ʳ��
                m_strTemp = m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strInItem2.Trim();
                if (m_strTemp.Trim().Length != 0)
                {
                    m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 9;//��ǰ��X����
                    m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 10;//��ǰ��X1����
                    m_mthDrawMultiString(this.m_fotSmallFont, this.m_fotSmallFont, 3, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, 0, m_intYOff, m_strTemp, e);
                }
                //Ѫ
                m_txtTemp.m_mthSetNewText(m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strInItem3.Trim(), m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strInItem3XML.Trim());
                m_strTemp = m_txtTemp.m_strGetRightText();

                if (m_strTemp.Trim().Length != 0)
                {
                    m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 10;//��ǰ��X����
                    m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 11;//��ǰ��X1����
                    m_mthDrawMultiString(this.m_fotSmallFont, this.m_fotSmallFont, 3, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, 0, m_intYOff, m_strTemp, e);
                }
                //��
                m_txtTemp.m_mthSetNewText(m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strInItem4.Trim(), m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strInItem4XML.Trim());
                m_strTemp = m_txtTemp.m_strGetRightText();

                if (m_strTemp.Trim().Length != 0)
                {
                    m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 11;//��ǰ��X����
                    m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 12;//��ǰ��X1����
                    m_mthDrawMultiString(this.m_fotSmallFont, this.m_fotSmallFont, 3, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, 0, m_intYOff, m_strTemp, e);
                }
                //��ˮ
                m_txtTemp.m_mthSetNewText(m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strInItem5.Trim(), m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strInItem5XML.Trim());
                m_strTemp = m_txtTemp.m_strGetRightText();

                if (m_strTemp.Trim().Length != 0)
                {
                    m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 12;//��ǰ��X����
                    m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 13;//��ǰ��X1����
                    m_mthDrawMultiString(this.m_fotSmallFont, this.m_fotSmallFont, 3, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, 0, m_intYOff, m_strTemp, e);
                }
                //��ˮ
                m_txtTemp.m_mthSetNewText(m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strInItem6.Trim(), m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strInItem6XML.Trim());
                m_strTemp = m_txtTemp.m_strGetRightText();

                if (m_strTemp.Trim().Length != 0)
                {
                    m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 13;//��ǰ��X����
                    m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 14;//��ǰ��X1����
                    m_mthDrawMultiString(this.m_fotSmallFont, this.m_fotSmallFont, 3, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, 0, m_intYOff, m_strTemp, e);
                }
                //����
                m_strTemp = m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strInItem7.ToString().Trim();
                if (m_strTemp.Trim().Length != 0)
                {
                    m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 14;//��ǰ��X����
                    m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 15;//��ǰ��X1����
                    m_mthDrawMultiString(this.m_fotSmallFont, this.m_fotTinyFont, 3, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, 0, m_intYOff, m_strTemp, e);
                }
                //�Զ���
                m_txtTemp.m_mthSetNewText(m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strInItem8.Trim(), m_objPrintInfo[m_intCurrentContentRow].m_objRecordArr[i1].m_strInItem8XML.Trim());
                m_strTemp = m_txtTemp.m_strGetRightText();

                if (m_strTemp.Trim().Length != 0)
                {
                    m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 15;//��ǰ��X����
                    m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2 + m_intWidth * 16;//��ǰ��X1����
                    m_mthDrawMultiString(this.m_fotSmallFont, this.m_fotSmallFont, 3, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, 0, m_intYOff, m_strTemp, e);
                }


            }
            return 0;
        }



        //������������������m_intRowNumberForPrintData�������������ϵͳ�ڻ�ҳ�������һҳ�Ĵ�ӡ
        /// <summary>
        /// ��¼���µ�һҳ��Ҫ��ӡ�ĵ�һ����¼�ڴ�ӡ����strValueArr�е����
        /// </summary>
        private int m_intRowNumberInValueArr = 0;

        /// <summary>
        /// ��¼���µ�һҳ��Ҫ��ӡ�ĵ�һ����¼��TempArr����������
        /// </summary>
        private int m_intRowNumberInTempArr = 0;



        private enmReturnValue m_blnPrintOneMainRowValue(System.Drawing.Printing.PrintPageEventArgs e)
        {
            string m_strTemp = string.Empty;
            string m_strTempValue = string.Empty;
            int m_intPosX = 0;
            int m_intPosX1 = 0;
            int m_intPosY = 0;
            int m_intPosY1 = 0;

            com.digitalwave.Utility.Controls.ctlRichTextBox m_txtTemp = new ctlRichTextBox();
            CharacterRange[] rgnDSTArr = new CharacterRange[1];
            rgnDSTArr[0] = new CharacterRange(0, 0);

            RectangleF rtfText = new RectangleF(0, 0, 10000, 100);

            StringFormat stfMeasure = new StringFormat(StringFormatFlags.LineLimit);



            if (m_blnCheckPageChange(e) == true) //ÿ��ӡһ��֮ǰ��Ҫ����Ƿ�ҳ
            {
                return enmReturnValue.enmNeedNextPage;
                //��ҳ

            }




            //����


            //��ӡ�����¼������ÿҳ���У������Ƿ�Ϊ2�ı����ж�
            if ((float)m_intCurrentContentRow / 2 == (int)m_intCurrentContentRow / 2)
            {
                //�����Զ���


                m_strTemp = m_objPrintInfo[m_intCurrentContentRow].m_objMainRecord.m_strCustomOutComumnName.Trim();
                if (m_strTemp.Length != 0)
                {
                    m_mthDrawMultiString(this.m_fotHeaderFont, this.m_fotSmallFont, 3, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9,
                        (int)enmRecordRectangleInfo.TopY + 40,
                        (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10,
                        (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 1 + 40, 2, 15, m_strTemp, e);
                }
                //�����Զ���
                m_strTemp = m_objPrintInfo[m_intCurrentContentRow].m_objMainRecord.m_strCustomInComumnName.Trim();
                if (m_strTemp.Length != 0)
                {
                    m_mthDrawMultiString(this.m_fotHeaderFont, this.m_fotSmallFont, 3, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark17,
                        (int)enmRecordRectangleInfo.TopY + 40,
                        (int)enmRecordRectangleInfo.RightX,
                        (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 1 + 40, 2, 15, m_strTemp, e);
                }

                //����
                m_intPosX = (int)enmRecordRectangleInfo.LeftX;//��ǰ��X����
                m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark1;//��ǰ��X����
                m_intPosY = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark2;
                m_intPosY1 = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowsMark2 + (int)enmRecordRectangleInfo.RowStep * 5;
                m_mthDrawMultiString(m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, m_objPrintInfo[m_intCurrentContentRow].m_objMainRecord.m_dtmRegDate, e, true);
                #region �ܼƵĸ�ֵ
                //����������
                m_txtTemp.m_mthSetNewText(m_objPrintInfo[m_intCurrentContentRow].m_objMainRecord.m_strOutUrineSummary.Trim(), m_objPrintInfo[m_intCurrentContentRow].m_objMainRecord.m_strOutUrineSummaryXML.Trim());
                m_strTemp = m_txtTemp.m_strGetRightText();
                m_intPosY = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 5 + 40;
                if (m_strTemp.Length != 0)
                {
                    this.m_mthDrawSummaryString(0, m_intPosY, 0, 0, m_strTemp, e, true, 1);
                }
                //���������ܼ�
                //m_strTempValue=
                m_txtTemp.m_mthSetNewText(m_objPrintInfo[m_intCurrentContentRow].m_objMainRecord.m_strOutSummary.Trim(), m_objPrintInfo[m_intCurrentContentRow].m_objMainRecord.m_strOutSummaryXML.Trim());
                m_strTemp = m_txtTemp.m_strGetRightText();

                if (m_strTemp.Length != 0)
                {
                    this.m_mthDrawSummaryString(0, m_intPosY, 0, 0, m_strTemp, e, true, 2);
                }
                //������������
                m_txtTemp.m_mthSetNewText(m_objPrintInfo[m_intCurrentContentRow].m_objMainRecord.m_strOutSummaryRate.Trim(), m_objPrintInfo[m_intCurrentContentRow].m_objMainRecord.m_strOutSummaryRateXML.Trim());
                m_strTemp = m_txtTemp.m_strGetRightText();
                if (m_strTemp.Length != 0)
                {
                    this.m_mthDrawSummaryString(0, m_intPosY, 0, 0, m_strTemp, e, true, 3);
                }


                //���������ܼ� 

                m_txtTemp.m_mthSetNewText(m_objPrintInfo[m_intCurrentContentRow].m_objMainRecord.m_strInSummary.Trim(), m_objPrintInfo[m_intCurrentContentRow].m_objMainRecord.m_strInSummaryXML.Trim());
                m_strTemp = m_txtTemp.m_strGetRightText();
                if (m_strTemp.Length != 0)
                {
                    this.m_mthDrawSummaryString(0, m_intPosY, 0, 0, m_strTemp, e, true, 4);
                }
                //ǩ��
                m_strTemp = m_objPrintInfo[m_intCurrentContentRow].m_objMainRecord.m_strRecordersignName.Trim();
                if (m_strTemp.Length != 0)
                {
                    this.m_mthDrawSummaryString(0, m_intPosY, 0, 0, m_strTemp, e, true, 5);
                }
                #endregion �ܼƵĸ�ֵ
                //��ӡ��ϸֵ
                m_blnPrintDetailValue((int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 1 + 40, e);

            }
            else
            {
                //�����Զ���
                m_strTemp = m_objPrintInfo[m_intCurrentContentRow].m_objMainRecord.m_strCustomOutComumnName.Trim();
                if (m_strTemp.Length != 0)
                {
                    m_mthDrawMultiString(this.m_fotHeaderFont, this.m_fotSmallFont, 3, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9,
                        (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 6 + 40,
                        (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10,
                        (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 7 + 40, 2, 15, m_strTemp, e);
                }
                //�����Զ���
                m_strTemp = m_objPrintInfo[m_intCurrentContentRow].m_objMainRecord.m_strCustomInComumnName.Trim();
                if (m_strTemp.Length != 0)
                {
                    m_mthDrawMultiString(this.m_fotHeaderFont, this.m_fotSmallFont, 3, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark17,
                        (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 6 + 40,
                        (int)enmRecordRectangleInfo.RightX,
                        (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 7 + 40, 2, 15, m_strTemp, e);
                }

                //����
                m_intPosX = (int)enmRecordRectangleInfo.LeftX;//��ǰ��X����
                m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark1;//��ǰ��X����
                m_intPosY = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 7 + 40;
                m_intPosY1 = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 12 + 40;
                m_mthDrawMultiString(m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, m_objPrintInfo[m_intCurrentContentRow].m_objMainRecord.m_dtmRegDate, e, true);
                #region �ܼƵĸ�ֵ
                //����������

                m_txtTemp.m_mthSetNewText(m_objPrintInfo[m_intCurrentContentRow].m_objMainRecord.m_strOutUrineSummary.Trim(), m_objPrintInfo[m_intCurrentContentRow].m_objMainRecord.m_strOutUrineSummaryXML.Trim());
                m_strTemp = m_txtTemp.m_strGetRightText();

                m_intPosY = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 11 + 40;
                if (m_strTemp.Length != 0)
                {
                    this.m_mthDrawSummaryString(0, m_intPosY, 0, 0, m_strTemp, e, true, 1);
                }
                //���������ܼ�
                //m_strTempValue=

                m_txtTemp.m_mthSetNewText(m_objPrintInfo[m_intCurrentContentRow].m_objMainRecord.m_strOutSummary.Trim(), m_objPrintInfo[m_intCurrentContentRow].m_objMainRecord.m_strOutSummaryXML.Trim());
                m_strTemp = m_txtTemp.m_strGetRightText();

                if (m_strTemp.Length != 0)
                {
                    this.m_mthDrawSummaryString(0, m_intPosY, 0, 0, m_strTemp, e, true, 2);
                }
                //������������

                m_txtTemp.m_mthSetNewText(m_objPrintInfo[m_intCurrentContentRow].m_objMainRecord.m_strOutSummaryRate.Trim(), m_objPrintInfo[m_intCurrentContentRow].m_objMainRecord.m_strOutSummaryRateXML.Trim());
                m_strTemp = m_txtTemp.m_strGetRightText();

                if (m_strTemp.Length != 0)
                {
                    this.m_mthDrawSummaryString(0, m_intPosY, 0, 0, m_strTemp, e, true, 3);
                }


                //���������ܼ� 

                m_txtTemp.m_mthSetNewText(m_objPrintInfo[m_intCurrentContentRow].m_objMainRecord.m_strInSummary.Trim(), m_objPrintInfo[m_intCurrentContentRow].m_objMainRecord.m_strInSummaryXML.Trim());
                m_strTemp = m_txtTemp.m_strGetRightText();

                if (m_strTemp.Length != 0)
                {
                    this.m_mthDrawSummaryString(0, m_intPosY, 0, 0, m_strTemp, e, true, 4);
                }
                //��������ǩ��
                m_strTemp = m_objPrintInfo[m_intCurrentContentRow].m_objMainRecord.m_strRecordersignName.Trim();
                if (m_strTemp.Length != 0)
                {
                    this.m_mthDrawSummaryString(0, m_intPosY, 0, 0, m_strTemp, e, true, 5);
                }
                #endregion
                //��ӡ��ϸֵ
                m_blnPrintDetailValue((int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * 7 + 40, e);

            }
            m_intCurrentContentRow++;
            m_intCurrentPagePrintRow++;
            return enmReturnValue.enmSuccessed;
        }


    }




}
        #endregion