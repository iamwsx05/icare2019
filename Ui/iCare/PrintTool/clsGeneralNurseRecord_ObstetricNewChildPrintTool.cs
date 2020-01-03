using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.Utility.Controls;
using System.Drawing; 
using System.Windows.Forms;
namespace iCare
{
    /// <summary>
    /// һ�㻼�߻��߻����¼��ɽ�棨��������������ӡ��
    /// </summary>
    public class clsGeneralNurseRecord_ObstetricNewChildPrintTool : infPrintRecord
    {
        private bool m_blnIsFromDataSource = true;               //�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
        private bool m_blnWantInit = true;	                   //�Ƿ���Ҫ��ʼ��	
        private clsRecordsDomain m_objRecordsDomain;           //��¼��
        private string m_strCanModifyTime = "";//�޸��޶�ʱ��

        private clsPrintInfo_GeneralNurseRecordObstetricNewChild m_objPrintInfo;     //��ӡ����
        private string strCurrentClass;                        //��ǰ���Ĭ��Ϊ��
        private int intCaseRowCount;                           //��ǰ���̼�¼���������


        private string[] strCurrentCaseTextArr;                //��ǰ���̼�¼��������
        private string[] strCurrentCaseXmlArr;                 //��ǰ���̼�¼�ۼ�����
        private string[] strCurrentCaseCreateDateArr;          //��ǰ���̼�¼����ʱ��
        private object[][] objDataArr;
        private bool m_bSummaryRow = false;
        public clsGeneralNurseRecord_ObstetricNewChildPrintTool()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            m_strCanModifyTime = clsEMRLogin.StrCanModifyTime;
        }

        #region ��ӡ��ʼ�����¼�
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
            m_objPrintInfo = new clsPrintInfo_GeneralNurseRecordObstetricNewChild();
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
        {	//m_objprintinfoΪ�ձ���δ���ô�ӡ����		
            if (m_objPrintInfo == null)
            {
                clsPublicFunction.ShowInformationMessageBox("����m_mthInitPrintContent֮ǰ�����ȵ���m_mthSetPrintInfo����");
                return;
            }
            //����Ϊ��
            if (m_objPrintInfo.m_strInPatentID == "")
                return;
            //��ȡ��ӡ����
            m_objRecordsDomain = new clsRecordsDomain(enmRecordsType.GeneralNurseRecord_ObstetricNewChildRec);

            long lngRes = m_objRecordsDomain.m_lngGetPrintInfo(m_objPrintInfo.m_strInPatentID, m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), out m_objPrintInfo.m_objTransDataArr, out m_objPrintInfo.m_dtmFirstPrintDateArr, out m_objPrintInfo.m_blnIsFirstPrintArr);
            if (lngRes <= 0)
                return;

            //����¼ʱ��(CreateDate)���� 
            //			m_mthSortTransData(ref m_objPrintInfo.m_objTransDataArr);
            //���ñ����ݵ���ӡ��
            m_mthSetPrintContent(m_objPrintInfo.m_objTransDataArr, m_objPrintInfo.m_dtmFirstPrintDateArr);
            m_objPrintInfo.m_objPrintDataArr = m_objPrintDataArr;
            m_blnWantInit = false;
        }

        private DateTime m_dtmPreRecordDate;
        private string strTempDate = string.Empty;
        private int m_intCurrentPagePrintRow = 0;
        private int m_intCurrentContentRow = 0;
        private int m_intMainCurrentPagePrintRow = 0;
        private int m_intMainCurrentContentRow = 0;

        /// <summary>
        /// ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
        /// </summary>
        /// <param name="p_objPrintContent">��ӡ����</param>
        public void m_mthSetPrintContent(object p_objPrintContent)
        {
            if (p_objPrintContent.GetType().Name != "clsPrintInfo_IntensiveTend")
            {
                clsPublicFunction.ShowInformationMessageBox("��������");
            }
            m_blnIsFromDataSource = false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ
            m_objPrintInfo = (clsPrintInfo_GeneralNurseRecordObstetricNewChild)p_objPrintContent;
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

            //û�м�¼����ʱ�����ؿ�


            if (m_objPrintInfo.m_objPrintDataArr == null || m_objPrintInfo.m_objPrintDataArr.Length == 0)
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
            m_fotHeaderFont = new Font("SimSun", 10.5f);
            m_fotSmallFont = new Font("SimSun", 12f);
            m_fotTinyFont = new Font("SimSun", 9f);

            m_GridPen = new Pen(Color.Black, 1);
            m_slbBrush = new SolidBrush(Color.Black);

            m_objPageSetting = new clsPrintPageSettingForRecord();

            m_objPrintContext = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, m_fotSmallFont);

            //m_objPrintLenth = new clsPrintLenth_GeneralNurseRecordCS();
            //m_objPrintLenth.m_intPrintLenth_BloodPress = (int)((float)(enmRecordRectangleInfo.ColumnsMark6 - enmRecordRectangleInfo.ColumnsMark5) / 2 / 8.75) + 1;//Ѫѹ���ȷ�һ�룬�����ĸ
            //m_objPrintLenth.m_intPrintLenth_Breath = (int)((float)(enmRecordRectangleInfo.ColumnsMark5 - enmRecordRectangleInfo.ColumnsMark4) / 8.75) + 1;
            //m_objPrintLenth.m_intPrintLenth_Echo = (int)((float)(enmRecordRectangleInfo.ColumnsMark13 - enmRecordRectangleInfo.ColumnsMark12) / 8.75) + 1;
            //m_objPrintLenth.m_intPrintLenth_In = (int)((float)(enmRecordRectangleInfo.ColumnsMark15 - enmRecordRectangleInfo.ColumnsMark14) / 8.75) + 1;
            //m_objPrintLenth.m_intPrintLenth_Out = (int)((float)(enmRecordRectangleInfo.ColumnsMark17 - enmRecordRectangleInfo.ColumnsMark16) / 8.75) + 1;
            //m_objPrintLenth.m_intPrintLenth_Pupil = (int)((float)(enmRecordRectangleInfo.ColumnsMark10 - enmRecordRectangleInfo.ColumnsMark9) / 8.75) + 1;
            //m_objPrintLenth.m_intPrintLenth_RecordContent = (int)((float)(enmRecordRectangleInfo.ColumnsMark20 - enmRecordRectangleInfo.ColumnsMark19 - 6) / 17.5) + 1;//���̼�¼����人��


            //m_objPrintLenth.m_intPrintLenth_Temperature = (int)((float)(enmRecordRectangleInfo.ColumnsMark3 - enmRecordRectangleInfo.ColumnsMark2) / 8.75) + 1;
            //m_objPrintLenth.m_intPrintLenth_SpO2 = (int)((float)(enmRecordRectangleInfo.ColumnsMark7 - enmRecordRectangleInfo.ColumnsMark6) / 8.75) + 1;
            //m_objPrintLenth.m_intPrintLenth_CVP = (int)((float)(enmRecordRectangleInfo.ColumnsMark8 - enmRecordRectangleInfo.ColumnsMark7) / 8.75) + 1;
            //m_objPrintLenth.m_intPrintLenth_Mind = (int)((float)(enmRecordRectangleInfo.ColumnsMark9 - enmRecordRectangleInfo.ColumnsMark8) / 8.75) + 1;
            //m_objPrintLenth.m_intPrintLenth_Custom = (int)((float)(enmRecordRectangleInfo.ColumnsMark18 - enmRecordRectangleInfo.ColumnsMark17) / 8.75) + 1;
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
                        arlRecordType.Add(m_objPrintInfo.m_objTransDataArr[i].m_intFlag);
                        //��ż�¼��OpenDate
                        arlOpenDate.Add(m_objPrintInfo.m_objTransDataArr[i].m_objRecordContent.m_dtmOpenDate);
                        intUpdateIndex = i;
                    }
                }

                if (intUpdateIndex >= 0)
                {
                    m_objRecordsDomain.m_lngUpdateFirstPrintDate(m_objPrintInfo.m_strInPatentID, m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), (int[])arlRecordType.ToArray(typeof(int)), (DateTime[])arlOpenDate.ToArray(typeof(DateTime)), m_objPrintInfo.m_dtmFirstPrintDateArr[intUpdateIndex]);
                }
            }
            m_mthEndPrintSub((PrintEventArgs)p_objPrintArg);
        }

        private void m_mthSetPrintContent(clsTransDataInfo[] p_objTransDataArr,
            DateTime[] p_dtmFirstPrintDate)
        {
            try
            {
                if (p_objTransDataArr == null || p_dtmFirstPrintDate == null || p_objTransDataArr.Length != p_dtmFirstPrintDate.Length)
                {
                    clsPublicFunction.ShowInformationMessageBox("��ӡ��������!");
                    return;
                }

                //���ݲ�ͬ�ı����ͣ���ȡ��Ӧ��clsDiseaseTrackInfo
                clsDiseaseTrackInfo objTrackInfo = null;
                m_objPrintDataArr = new clsGeneralNurseRecordContent_ObstetricNewChildDataInfo[p_objTransDataArr.Length];
                //				m_objPrintDataArr=(clsIntensiveTendDataInfo[])(p_objTransDataArr.Clone());
                ArrayList arlTemp = new ArrayList();
                arlTemp.AddRange(p_objTransDataArr);
                m_objPrintDataArr = (clsGeneralNurseRecordContent_ObstetricNewChildDataInfo[])arlTemp.ToArray(typeof(clsGeneralNurseRecordContent_ObstetricNewChildDataInfo));

                //System.Data.DataTable dtbBlankRecord = null;
                //new clsDiseaseTrackAddBlankDomain().m_lngGetBlankRecordContent(m_objPrintInfo.m_strInPatentID,m_objPrintInfo.m_dtmInPatientDate,out dtbBlankRecord);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
            if (m_objPrintInfo.m_objTransDataArr == null)
                m_mthInitPrintContent();
        }
        // ��ӡҳ
        private void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
        {
            try
            {
                p_objPrintPageArg.HasMorePages = false;
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
            blnBeginPrintNewRecord = true;
            m_intRowNumberForPrintData = 0;
            m_intCurrentContentRow = 0;
            m_intMainCurrentContentRow = 0;
            m_intCurrentPagePrintRow = 0;
            m_intPosY = (int)enmRecordRectangleInfo.TopY + 220;
        }
        #endregion

        #region �йش�ӡ������


        /// <summary>
        /// ���д�ӡ������
        /// </summary>
        private clsGeneralNurseRecordContent_ObstetricNewChildDataInfo[] m_objPrintDataArr;

        /// <summary>
        /// ������Σ�ػ����¼�ģ���ӡ�����ĵ���
        /// </summary>		
        private com.digitalwave.controls.clsPrintRichTextContext m_objPrintContext;
        /// <summary>
        /// ÿ����ʾ�ĺ��֣����̼�¼������ĸ������������Ŀ
        /// </summary>
        private class clsPrintLenth_GeneralNurseRecordObstetricNewChild
        {
            public int m_intPrintLenth_RecordContent;		//���̹۲�
            public int m_intPrintLenth_Temperature;			//����
            public int m_intPrintLenth_HeartRate;			//����
            public int m_intPrintLenth_Breath;				//����
            public int m_intPrintLenth_BloodPress;		    //Ѫѹ


            public int m_intPrintLenth_SpO2;				//spo2
            public int m_intPrintLenth_CVP;				    //CVP
            public int m_intPrintLenth_Mind;				//��־
            public int m_intPrintLenth_Pupil;				//ͫ�״�С
            public int m_intPrintLenth_Echo;				//ͫ�׷���		
            public int m_intPrintLenth_In;					//����
            public int m_intPrintLenth_Out;					//�ų�
            public int m_intPrintLenth_Custom;				//�Զ�����
        }

        /// <summary>
        /// ��ǰ�е�Y����
        /// </summary>
        int m_intPosY = (int)enmRecordRectangleInfo.TopY + 130;
        /// <summary>
        /// ÿ�������еĸ߶�
        /// </summary>
        int intTempDeltaY = 40;


        private clsPrintLenth_GeneralNurseRecordObstetricNewChild m_objPrintLenth;
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
            ///<summary>
            /// ���ӵ����
            /// </summary>
            LeftX = 110,//��������3cmװ��
            /// <summary>
            /// ���ӵ��Ҷ�
            /// </summary>
            RightX = 1130,
            /// <summary>
            /// ����ÿ�еĲ���
            /// </summary>
            RowStep = 30,
            /// <summary>
            /// ���ӵ�����
            /// </summary>
            RowLinesNum = 21,
            /// <summary>
            /// �����ڸ�������Ը��Ӷ��˵Ĵ�ֱƫ��
            /// </summary>
            VOffSet = 15,
            /// <summary>
            /// �е���Ŀ
            /// </summary>
            ColumnsNum = 20,
            ColumnsMark1 = 70,//ʱ��
            ColumnsMark2 = 110,//����
            ColumnsMark3 = 150,//����
            ColumnsMark4 = 190,//����
            ColumnsMark5 = 230,//ض��
            ColumnsMark6 = 270,//����
            ColumnsMark7 = 310,//Ѫ��
            ColumnsMark8 = 350,//��ɫ
            ColumnsMark9 = 390,//����
            ColumnsMark10 = 430,//��˱��
            ColumnsMark11 = 470,//�겿
            ColumnsMark12 = 510,//���
            ColumnsMark13 = 550,//С��
            ColumnsMark14 = 590,//ִ��ǩ��
            ColumnsMark15 = 658,//����۲�
            ColumnsMark16 = 950//��¼ǩ��
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
                        m_fReturnPoint = new PointF(550f, 40f);
                        break;
                    case (int)enmItemDefination.Page_Name_Title:
                        m_fReturnPoint = new PointF(548f, 80f);
                        break;
                    case (int)enmItemDefination.Name_Title:
                        m_fReturnPoint = new PointF(180f, 120f);
                        break;
                    case (int)enmItemDefination.Name:
                        m_fReturnPoint = new PointF(230f, 120f);
                        break;

                    case (int)enmItemDefination.Sex_Title:
                        m_fReturnPoint = new PointF(340f, 120f);
                        break;
                    case (int)enmItemDefination.Sex:
                        m_fReturnPoint = new PointF(390f, 120f);
                        break;

                    case (int)enmItemDefination.Age_Title:
                        m_fReturnPoint = new PointF(450f, 120f);
                        break;
                    case (int)enmItemDefination.Age:
                        m_fReturnPoint = new PointF(500f, 120f);
                        break;

                    case (int)enmItemDefination.Dept_Name_Title:
                        m_fReturnPoint = new PointF(620f, 120f);
                        break;
                    case (int)enmItemDefination.Dept_Name:
                        m_fReturnPoint = new PointF(670f, 120f);
                        break;

                    case (int)enmItemDefination.BedNo_Title:
                        m_fReturnPoint = new PointF(810f, 120f);
                        break;
                    case (int)enmItemDefination.BedNo:
                        m_fReturnPoint = new PointF(860f, 120f);
                        break;

                    case (int)enmItemDefination.InPatientID_Title:
                        m_fReturnPoint = new PointF(915f, 120f);
                        break;
                    case (int)enmItemDefination.InPatientID:
                        m_fReturnPoint = new PointF(980f, 120f);
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
            objEveryRecordPageInfo.strAge = m_objPrintInfo.m_strAge;
            objEveryRecordPageInfo.strPatientName = m_objPrintInfo.m_strPatientName;
            objEveryRecordPageInfo.strBedNo = m_objPrintInfo.m_strBedName;
            objEveryRecordPageInfo.strAreaName = m_objPrintInfo.m_strAreaName;
            objEveryRecordPageInfo.strSex = m_objPrintInfo.m_strSex;
            objEveryRecordPageInfo.strInPatientID = m_objPrintInfo.m_strHISInPatientID;
            objEveryRecordPageInfo.strPrintDate = (m_objPrintInfo.m_strInPatentID != "") ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : "";

            SizeF m_szfHospitalTitle = e.Graphics.MeasureString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, new Font("����", 15, FontStyle.Bold));
            SizeF m_szfChildTitle = e.Graphics.MeasureString("���������������¼��", new Font("����", 18));
            int m_intChildTitleNameOffSetX = (int)(m_szfHospitalTitle.Width / 2 + m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_HospitalName).X - m_szfChildTitle.Width / 2 + 4);
            e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, new Font("����", 15, FontStyle.Bold), m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_HospitalName));

            e.Graphics.DrawString("���������������¼��", new Font("����", 18), m_slbBrush, m_intChildTitleNameOffSetX, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_Name_Title).Y);

            //e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, new Font("����",15,FontStyle.Bold), m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_HospitalName));

            //e.Graphics.DrawString("Σ�ػ��߻����¼", new Font("����",18), m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_Name_Title));

            e.Graphics.DrawString("������", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name_Title));
            e.Graphics.DrawString(objEveryRecordPageInfo.strPatientName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name));

            e.Graphics.DrawString("�Ա�", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex_Title));
            e.Graphics.DrawString(objEveryRecordPageInfo.strSex, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex));

            e.Graphics.DrawString("���䣺", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age_Title));
            e.Graphics.DrawString(objEveryRecordPageInfo.strAge, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age));

            e.Graphics.DrawString("������", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name_Title));
            e.Graphics.DrawString(objEveryRecordPageInfo.strAreaName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name));

            e.Graphics.DrawString("���ţ�", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo_Title));
            e.Graphics.DrawString(objEveryRecordPageInfo.strBedNo, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo));

            e.Graphics.DrawString("סԺ�ţ�", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID_Title));
            e.Graphics.DrawString(objEveryRecordPageInfo.strInPatientID, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID));



            //�ܴ���ʿǩ��
            //e.Graphics.DrawString("�ܴ���ʿ��____________", m_fotTinyFont, m_slbBrush, (int)enmRecordRectangleInfo.RightX - 160,
            //    (int)enmRecordRectangleInfo.TopY + ((int)enmRecordRectangleInfo.RowStep) * ((int)enmRecordRectangleInfo.RowLinesNum + 1) - 26);//20);

            //e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.RightX-100,
            //           ((int)enmRecordRectangleInfo.TopY + ((int)enmRecordRectangleInfo.RowStep) * ((int)enmRecordRectangleInfo.RowLinesNum + 1) ),
            //           (int)enmRecordRectangleInfo.RightX,
            //           (int)enmRecordRectangleInfo.TopY + ((int)enmRecordRectangleInfo.RowStep) * ((int)enmRecordRectangleInfo.RowLinesNum + 1) );
            //---------------

            //�����һ�������ӡ˵������
            e.Graphics.DrawString("����" + intNowPage.ToString() + "ҳ��", new Font("SimSun", 9), m_slbBrush,
                (int)enmRecordRectangleInfo.LeftX + 20,
                (int)enmRecordRectangleInfo.TopY + ((int)enmRecordRectangleInfo.RowStep) * ((int)enmRecordRectangleInfo.RowLinesNum + 1) - 26);//20);

        }
        #endregion
        private void m_mthDrawMultiString(Font fotNormal, Font fotSmall, int iLimitLenth, float x, float y, float x1, float y1, float xOff, float yOff, string strContent, System.Drawing.Printing.PrintPageEventArgs e)
        {

            RectangleF drawRect = new RectangleF(x, y + 1, x1 - x, y1 - y);
            RectangleF drawRectNormal = new RectangleF(x, y + yOff, x1 - x, y1 - y);

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
        private void m_mthDrawMultiString(Font fotNormal, float x, float y, float x1, float y1, float xOff, float yOff, string strContent, System.Drawing.Printing.PrintPageEventArgs e)
        {

            RectangleF drawRect = new RectangleF(x, y + yOff, x1 - x, y1 - y);
            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = System.Drawing.StringAlignment.Center;
            strFormat.FormatFlags = System.Drawing.StringFormatFlags.LineLimit;

            e.Graphics.DrawString(strContent, fotNormal, m_slbBrush, drawRect, strFormat);


        }
        private string m_strGetDSTTextXML(string p_strText, string p_strModifyUserID, string p_strModifyUserName)
        {
            return com.digitalwave.controls.ctlRichTextBox.clsXmlTool.s_strMakeDSTXml(p_strText, p_strModifyUserID, p_strModifyUserName, Color.Black, Color.White);
        }
        #region ���������Ŀ
        /// <summary>
        /// ���������Ŀ
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintHeaderInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {
            //����
            e.Graphics.DrawString("�� ��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + 15,
                (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep + 5);

            //ʱ��
            e.Graphics.DrawString("ʱ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark1 + 12, (int)enmRecordRectangleInfo.TopY + 2);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark1 + 12, (int)enmRecordRectangleInfo.TopY + 2 * (int)enmRecordRectangleInfo.RowStep + 5);

            //����	
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark2 + 11, (int)enmRecordRectangleInfo.TopY + 2);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark2 + 11, (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark2 + 11, (int)enmRecordRectangleInfo.TopY - 10 + 2 * (int)enmRecordRectangleInfo.RowStep);
            e.Graphics.DrawString("C", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark2 + 16, (int)enmRecordRectangleInfo.TopY + 2 * (int)enmRecordRectangleInfo.RowStep + 1);

            //����(��/��)
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark3 + 11, (int)enmRecordRectangleInfo.TopY + 2);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark3 + 11, (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep - 13);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark3 + 11, (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep + 10);
            e.Graphics.DrawString("/", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark3 + 13, (int)enmRecordRectangleInfo.TopY + 2 * (int)enmRecordRectangleInfo.RowStep - 7);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark3 + 11, (int)enmRecordRectangleInfo.TopY + 2 * (int)enmRecordRectangleInfo.RowStep + 11);

            //����(��/��)
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark4 + 11, (int)enmRecordRectangleInfo.TopY + 2);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark4 + 11, (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep - 13);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark4 + 11, (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep + 10);
            e.Graphics.DrawString("/", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark4 + 16, (int)enmRecordRectangleInfo.TopY + 2 * (int)enmRecordRectangleInfo.RowStep - 7);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark4 + 11, (int)enmRecordRectangleInfo.TopY + 2 * (int)enmRecordRectangleInfo.RowStep + 11);

            //ض��
            e.Graphics.DrawString("ض", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark5 + 11, (int)enmRecordRectangleInfo.TopY + 2);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark5 + 11, (int)enmRecordRectangleInfo.TopY + 2 * (int)enmRecordRectangleInfo.RowStep + 2);


            //����
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark6 + 11, (int)enmRecordRectangleInfo.TopY + 2);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark6 + 11, (int)enmRecordRectangleInfo.TopY + 2 * (int)enmRecordRectangleInfo.RowStep + 5);


            //Ѫ��
            e.Graphics.DrawString("Ѫ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark7 + 11, (int)enmRecordRectangleInfo.TopY + 2);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark7 + 11, (int)enmRecordRectangleInfo.TopY + 2 * (int)enmRecordRectangleInfo.RowStep + 5);

            //��ɫ
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark8 + 11, (int)enmRecordRectangleInfo.TopY + 2);
            e.Graphics.DrawString("ɫ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark8 + 11, (int)enmRecordRectangleInfo.TopY + 2 * (int)enmRecordRectangleInfo.RowStep + 5);

            //����
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark9 + 11, (int)enmRecordRectangleInfo.TopY + 2);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark9 + 11, (int)enmRecordRectangleInfo.TopY + 2 * (int)enmRecordRectangleInfo.RowStep + 7);

            //��˱��
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark10 + 11, (int)enmRecordRectangleInfo.TopY + 2);
            e.Graphics.DrawString("˱", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark10 + 11, (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep + 5);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark10 + 11, (int)enmRecordRectangleInfo.TopY + 2 * (int)enmRecordRectangleInfo.RowStep + 5);

            //�겿
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark11 + 11, (int)enmRecordRectangleInfo.TopY + 2);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark11 + 11, (int)enmRecordRectangleInfo.TopY + 2 * (int)enmRecordRectangleInfo.RowStep + 5);

            //���
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark12 + 11, (int)enmRecordRectangleInfo.TopY + 2);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark12 + 11, (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep + 5);
            e.Graphics.DrawString("/��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark12 + 6, (int)enmRecordRectangleInfo.TopY + 2 * (int)enmRecordRectangleInfo.RowStep + 5);

            //С��
            e.Graphics.DrawString("С", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark13 + 11, (int)enmRecordRectangleInfo.TopY + 2);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark13 + 11, (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep + 5);
            e.Graphics.DrawString("/��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark13 + 6, (int)enmRecordRectangleInfo.TopY + 2 * (int)enmRecordRectangleInfo.RowStep + 5);


            //ִ��ǩ��
            e.Graphics.DrawString("ִ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark14 + 23, (int)enmRecordRectangleInfo.TopY + 5);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark14 + 23, (int)enmRecordRectangleInfo.TopY + 27);
            e.Graphics.DrawString("ǩ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark14 + 23, (int)enmRecordRectangleInfo.TopY + 49);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark14 + 23, (int)enmRecordRectangleInfo.TopY + 71);

            //���顢�����ʩ��Ч��
            e.Graphics.DrawString("���顢�����ʩ��Ч��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark15 + 80, (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep + 5);

            //��¼ǩ��
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark16 + 30, (int)enmRecordRectangleInfo.TopY + 5);
            e.Graphics.DrawString("¼", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark16 + 30, (int)enmRecordRectangleInfo.TopY + 27);
            e.Graphics.DrawString("ǩ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark16 + 30, (int)enmRecordRectangleInfo.TopY + 49);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark16 + 30, (int)enmRecordRectangleInfo.TopY + 71);
        }
        #endregion

        #region ������
        /// <summary>
        ///  ������
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintRectangleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {
            //�����Ӻ���
            for (int i1 = 0; i1 <= (int)enmRecordRectangleInfo.RowLinesNum; i1++)
            {
                if (i1 != 1 && i1 != 2)
                    e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX,
                        (int)enmRecordRectangleInfo.TopY + ((int)enmRecordRectangleInfo.RowStep) * i1,
                        (int)enmRecordRectangleInfo.RightX,
                        (int)enmRecordRectangleInfo.TopY + ((int)enmRecordRectangleInfo.RowStep) * i1);
            }

            #region ����������


            int intHeight = ((int)enmRecordRectangleInfo.RowLinesNum) * (int)enmRecordRectangleInfo.RowStep;
            //���������


            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX, (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.LeftX, (int)enmRecordRectangleInfo.TopY + intHeight);

            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark1, (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark1, (int)enmRecordRectangleInfo.TopY + intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2, (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2, (int)enmRecordRectangleInfo.TopY + intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark3, (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark3, (int)enmRecordRectangleInfo.TopY + intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark4, (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark4, (int)enmRecordRectangleInfo.TopY + intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark5, (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark5, (int)enmRecordRectangleInfo.TopY + intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark6, (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark6, (int)enmRecordRectangleInfo.TopY + intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7, (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7, (int)enmRecordRectangleInfo.TopY + intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark8, (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark8, (int)enmRecordRectangleInfo.TopY + intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9, (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9, (int)enmRecordRectangleInfo.TopY + intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10, (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10, (int)enmRecordRectangleInfo.TopY + intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11, (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11, (int)enmRecordRectangleInfo.TopY + intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12, (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12, (int)enmRecordRectangleInfo.TopY + intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13, (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13, (int)enmRecordRectangleInfo.TopY + intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark14, (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark14, (int)enmRecordRectangleInfo.TopY + intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15, (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15, (int)enmRecordRectangleInfo.TopY + intHeight);
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16, (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16, (int)enmRecordRectangleInfo.TopY + intHeight);

            //ͫ�״�С���ҷֽ���



            //�ұ���
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.RightX, (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.RightX, (int)enmRecordRectangleInfo.TopY + intHeight);
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
            string strRecord = "";
            string strRecordXML = "";
            DateTime dtmFlagTime;
            intNowRow = 1;
            int iTemp = (int)enmRecordRectangleInfo.TopY + ((int)enmRecordRectangleInfo.RowStep * ((int)enmRecordRectangleInfo.RowLinesNum)) + 10;
            //m_mthDrawMultiString(new Font("SimSun", 9), (int)enmRecordRectangleInfo.LeftX + 450, iTemp, (int)enmRecordRectangleInfo.RightX + 450, iTemp + 50, 1, 1, "����" + intNowPage.ToString() + "ҳ��", e);
            if (m_objPrintInfo.m_strInPatentID == "") return;
            m_intCurrentRecord = 0;
            //��ӡ��ѭ��
            for (int i1 = m_intMainCurrentContentRow; i1 < m_objPrintInfo.m_objTransDataArr.Length; i1++)
            {
                clsGeneralNurseRecordContent_ObstetricNewChildDataInfo clsGereralData = new clsGeneralNurseRecordContent_ObstetricNewChildDataInfo();
                clsGereralData = (clsGeneralNurseRecordContent_ObstetricNewChildDataInfo)m_objPrintInfo.m_objTransDataArr[i1];
                objDataArr = m_objGetRecordsValueArr(clsGereralData);
                if (objDataArr == null)
                    continue;
                for (m_intCurrentRecord = m_intCurrentContentRow; m_intCurrentRecord < objDataArr.Length; m_intCurrentRecord++)
                {
                    enmReturnValue m_enmRe = m_blnPrintOneValueCS(e, m_intPosY);
                    //���ݷ���ֵ����ҳ���
                    if (m_enmRe == enmReturnValue.enmFaild)
                        e.HasMorePages = false;
                    if (m_enmRe == enmReturnValue.enmNeedNextPage)
                    {
                        m_intRowNumberForPrintData = m_intCurrentRecord;
                        m_intPosY = (int)enmRecordRectangleInfo.TopY + 130;
                        e.HasMorePages = true;
                        return;
                    }
                    if (m_enmRe == enmReturnValue.enmSuccessed)
                    {
                        e.HasMorePages = false;
                        blnBeginPrintNewRecord = true;
                    }
                }
                m_intMainCurrentContentRow++;
            }
        }

        #region ֻ��ӡһ��


        /// <summary>
        /// ֻ��ӡһ��


        /// </summary>
        /// <param name="e"></param>
        /// <param name="p_intBottomY"></param>
        /// <returns></returns>
        private enmReturnValue m_blnPrintOneValueCS(System.Drawing.Printing.PrintPageEventArgs e, int p_intBottomY)
        {
            enmReturnValue enmIsRecordFinish = m_blnPrintOneRowValueCS(e);
            if (enmIsRecordFinish != enmReturnValue.enmNeedNextPage)
            {
                m_intRowNumberInValueArr = 0;
                m_intRowNumberInTempArr = 0;
            }
            return enmIsRecordFinish;
        }
        #endregion

        /// <summary>
        /// ����Ƿ�ҳ,true:��ҳ��false:����ҳ
        /// </summary>
        /// <param name="p_intNowRow">��ǰ��ӡ�У���p_intNowRow��</param>
        /// <param name="e"></param>
        /// <returns></returns>
        private string m_strConvertObjectValue(object obj)
        {
            clsDSTRichTextBoxValue objclsDSTRichTextBoxValue;
            string strTemp = "";
            if (obj == null)
            {
                strTemp = "";
            }
            else
            {
                if (obj.GetType().Name == "clsDSTRichTextBoxValue")
                {
                    objclsDSTRichTextBoxValue = (clsDSTRichTextBoxValue)obj;
                    if (objclsDSTRichTextBoxValue.m_blnUnderDST == true)
                    {
                        m_bSummaryRow = true;
                    }
                    strTemp = objclsDSTRichTextBoxValue.m_strText != null ? objclsDSTRichTextBoxValue.m_strText : "";
                }
                else
                {
                    strTemp = obj.ToString();
                }
            }
            return strTemp;
        }
        private bool m_blnCheckPageChange(System.Drawing.Printing.PrintPageEventArgs e)
        {
            //����ǰ�г������һ�У��� >ҳ��������ʱ��ҳ
            if (m_intCurrentPagePrintRow > ((int)enmRecordRectangleInfo.RowLinesNum - 4)/*��ȥ��ͷ2��������Ч����*/)
            {
                m_intCurrentPagePrintRow = 0;
                intNowPage++;
                return true;
            }
            else return false;
        }
        #endregion

        //������������������m_intRowNumberForPrintData�������������ϵͳ�ڻ�ҳ�������һҳ�Ĵ�ӡ
        /// <summary>
        /// ��¼���µ�һҳ��Ҫ��ӡ�ĵ�һ����¼�ڴ�ӡ����strValueArr�е����
        /// </summary>
        private int m_intRowNumberInValueArr = 0;
        /// <summary>
        /// ��¼���µ�һҳ��Ҫ��ӡ�ĵ�һ����¼��TempArr����������
        /// </summary>
        private int m_intRowNumberInTempArr = 0;
        private enmReturnValue m_blnPrintOneRowValueCS(System.Drawing.Printing.PrintPageEventArgs e)
        {
            string strTemp;
            CharacterRange[] rgnDSTArr = new CharacterRange[1];
            rgnDSTArr[0] = new CharacterRange(0, 0);
            RectangleF rtfText = new RectangleF(0, 0, 10000, 100);
            StringFormat stfMeasure = new StringFormat(StringFormatFlags.LineLimit);
            RectangleF rtfBounds;
            Region[] rgnDST;
            clsDSTRichTextBoxValue objclsDSTRichTextBoxValue;
            if (m_blnCheckPageChange(e) == true) //ÿ��ӡһ��֮ǰ��Ҫ����Ƿ�ҳ
            {
                return enmReturnValue.enmNeedNextPage;
                //��ҳ
            }
            //����
            int m_intPosX = (int)enmRecordRectangleInfo.LeftX;//��ǰ��X����
            int m_intPosX1 = 0;
            int m_intPosY = (int)enmRecordRectangleInfo.TopY + ((m_intCurrentPagePrintRow + 3) * (int)enmRecordRectangleInfo.RowStep);
            int m_intPosY1 = (int)enmRecordRectangleInfo.TopY + ((m_intCurrentPagePrintRow + 4) * (int)enmRecordRectangleInfo.RowStep);
            int m_intXOff = 1;
            int m_intYOff = 15;
            int intTempColumn = 4;
            strTemp = m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
            if (strTemp.Trim().Length != 0)
            {
                e.Graphics.DrawString(strTemp, m_fotTinyFont, Brushes.Black, m_intPosX + 2, m_intPosY + 15);
                strTempDate = strTemp;
            }
            //����ʱ��һ�е����ڲ���ʡȥ,ǰ��ֵ��Ϊ�գ�����ֵΪ�գ��Ҵ��ڵ�һ��
            if (strTempDate.Trim().Length != 0 && m_intCurrentPagePrintRow == 0 && strTemp.Trim().Length == 0)
            {
                e.Graphics.DrawString(strTempDate, m_fotTinyFont, Brushes.Black, m_intPosX + 5, m_intPosY + 15);
            }
            //ʱ��
            m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark1;//��ǰ��X����
            intTempColumn = 5;
            strTemp = m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
            if (strTemp.Trim().Length != 0)
            {
                e.Graphics.DrawString(strTemp, m_fotTinyFont, Brushes.Black, m_intPosX + 3, m_intPosY + 15);
            }
            //����
            m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2;//��ǰ��X����
            m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark3;//��ǰ��X����
            intTempColumn = 6;
            strTemp = m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
            if (strTemp.Trim().Length != 0)
            {
                m_mthDrawMultiString(this.m_fotTinyFont, this.m_fotTinyFont, 5, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, m_intXOff, m_intYOff, strTemp, e);
            }
            //����
            m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark3;//��ǰ��X����
            m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark4;//��ǰ��X����
            intTempColumn = 7;
            strTemp = m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
            if (strTemp.Trim().Length != 0)
            {
                m_mthDrawMultiString(this.m_fotTinyFont, this.m_fotTinyFont, 5, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, m_intXOff, m_intYOff, strTemp, e);
            }
            m_bSummaryRow = false;
            //����
            m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark4;//��ǰ��X����
            m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark5;//��ǰ��X����
            intTempColumn = 8;
            strTemp = m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
            if (strTemp.Trim().Length != 0)
            {
                m_mthDrawMultiString(this.m_fotTinyFont, this.m_fotTinyFont, 5, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, m_intXOff, m_intYOff, strTemp, e);
            }

            //ض��
            m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark5;//��ǰ��X����
            m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark6;//��ǰ��X����
            m_intXOff = 10;
            intTempColumn = 9;
            strTemp = m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
            if (strTemp.Trim().Length != 0)
            {
                m_mthDrawMultiString(this.m_fotTinyFont, this.m_fotTinyFont, 2, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, m_intXOff, m_intYOff, strTemp, e);
            }
            //����
            m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark6;//��ǰ��X����
            m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7;//��ǰ��X����
            m_intXOff = 10;
            intTempColumn = 10;
            strTemp = m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
            if (strTemp.Trim().Length != 0)
            {
                m_mthDrawMultiString(this.m_fotTinyFont, this.m_fotTinyFont, 2, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, m_intXOff, m_intYOff, strTemp, e);
            }
            //Ѫ��
            m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7;//��ǰ��X����
            m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark8;//��ǰ��X����
            m_intXOff = 10;
            intTempColumn = 11;
            strTemp = m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
            if (strTemp.Trim().Length != 0)
            {
                m_mthDrawMultiString(this.m_fotTinyFont, this.m_fotTinyFont, 2, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, m_intXOff, m_intYOff, strTemp, e);
            }
            //��ɫ
            m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark8;//��ǰ��X����
            m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9;//��ǰ��X����
            m_intXOff = 12;
            intTempColumn = 12;
            strTemp = m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
            if (strTemp.Trim().Length != 0)
            {
                m_mthDrawMultiString(this.m_fotTinyFont, this.m_fotTinyFont, 2, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, m_intXOff, m_intYOff, strTemp, e);
            }
            //����
            m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9;//��ǰ��X����
            m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10;//��ǰ��X����
            m_intXOff = 10;
            intTempColumn = 13;
            strTemp = m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
            if (strTemp.Trim().Length != 0)
            {
                m_mthDrawMultiString(this.m_fotTinyFont, this.m_fotTinyFont, 2, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, m_intXOff, m_intYOff, strTemp, e);
            }
            //��˱��
            m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10;//��ǰ��X����
            m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11;//��ǰ��X����
            m_intXOff = 10;
            intTempColumn = 14;
            strTemp = m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
            if (strTemp.Trim().Length != 0)
            {
                m_mthDrawMultiString(this.m_fotTinyFont, this.m_fotTinyFont, 2, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, m_intXOff, m_intYOff, strTemp, e);
            }
            //�겿
            m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11;//��ǰ��X����
            m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12;//��ǰ��X����
            m_intXOff = 10;
            intTempColumn = 15;
            strTemp = m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
            if (strTemp.Trim().Length != 0)
            {
                m_mthDrawMultiString(this.m_fotTinyFont, this.m_fotTinyFont, 2, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, m_intXOff, m_intYOff, strTemp, e);
            }
            //���
            m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12;//��ǰ��X����
            m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13;//��ǰ��X����
            m_intXOff = 10;
            intTempColumn = 16;
            strTemp = m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
            if (strTemp.Trim().Length != 0)
            {
                m_mthDrawMultiString(this.m_fotTinyFont, this.m_fotTinyFont, 5, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, m_intXOff, m_intYOff, strTemp, e);
            }
            //С��
            m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13;//��ǰ��X����
            m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark14;//��ǰ��X����
            m_intXOff = 10;
            intTempColumn = 17;
            strTemp = m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
            if (strTemp.Trim().Length != 0)
            {
                m_mthDrawMultiString(this.m_fotTinyFont, this.m_fotTinyFont, 5, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, m_intXOff, m_intYOff, strTemp, e);
            }
            //ִ��ǩ��
            m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark14;//��ǰ��X����
            m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15;//��ǰ��X����
            intTempColumn = 18;
            strTemp = m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
            if (strTemp.Trim().Length != 0)
            {
                //if (strTemp.Length > 5)
                //{
                //    e.Graphics.DrawString(strTemp.Substring(0, 5), m_fotTinyFont, m_slbBrush, m_intPosX, m_intPosY + 5);
                //    e.Graphics.DrawString(strTemp.Substring(5), m_fotTinyFont, m_slbBrush, m_intPosX, m_intPosY + 25);
                //}
                //else
                //    e.Graphics.DrawString(strTemp, m_fotTinyFont, m_slbBrush, m_intPosX, m_intPosY + m_intYOff);

                Image imgEmpSig = ImageSignature.GetEmpSigImage(strTemp);
                if (imgEmpSig != null)
                {
                    //imgEmpSig = ImageSignature.pictureProcess(imgEmpSig, 579, 238);
                    e.Graphics.DrawImage(imgEmpSig, m_intPosX + 2, m_intPosY + 1, 60, 26);
                }
                else
                {
                    if (strTemp.Length > 5)
                    {
                        e.Graphics.DrawString(strTemp.Substring(0, 5), m_fotTinyFont, m_slbBrush, m_intPosX, m_intPosY + 5);
                        e.Graphics.DrawString(strTemp.Substring(5), m_fotTinyFont, m_slbBrush, m_intPosX, m_intPosY + 25);
                    }
                    else
                        e.Graphics.DrawString(strTemp, m_fotTinyFont, m_slbBrush, m_intPosX, m_intPosY + m_intYOff);
                }
            }
            m_bSummaryRow = false;
            //����۲�
            m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15;//��ǰ��X����
            m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16;//��ǰ��X����
            intTempColumn = 19;
            strTemp = m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
            if (strTemp.Trim().Length != 0)
            {
                e.Graphics.DrawString(strTemp, m_fotTinyFont, m_slbBrush, m_intPosX, m_intPosY + m_intYOff);
            }
            //��¼ǩ��
            m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16;//��ǰ��X����
            m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.RightX;//��ǰ��X����
            intTempColumn = 21;
            strTemp = m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
            if (strTemp.Trim().Length != 0)
            {
                Image imgEmpSig = ImageSignature.GetEmpSigImage(strTemp);
                if (imgEmpSig != null)
                {
                    //imgEmpSig = ImageSignature.pictureProcess(imgEmpSig, 579, 238);
                    e.Graphics.DrawImage(imgEmpSig, m_intPosX+3 , m_intPosY+2, 60, 26);
                }
                else
                {
                    if (strTemp.Length > 5)
                    {
                        e.Graphics.DrawString(strTemp.Substring(0, 5), m_fotTinyFont, m_slbBrush, m_intPosX, m_intPosY + 5);
                        e.Graphics.DrawString(strTemp.Substring(5), m_fotTinyFont, m_slbBrush, m_intPosX, m_intPosY + 25);
                    }
                    else
                        e.Graphics.DrawString(strTemp, m_fotTinyFont, m_slbBrush, m_intPosX, m_intPosY + m_intYOff);
                }
            }
            m_intCurrentContentRow++;
            m_intCurrentPagePrintRow++;
        #endregion
            return enmReturnValue.enmSuccessed;
        }

        private void m_mthSetOtherDetail(object[] objDetail, int intCurrentDetail, int intRowOfCurrentDetail, clsGeneralNurseRecordContent_ObstetricNewChild objCurrent, out object[] objOtherDetail)
        {
            objOtherDetail = new object[23];
            string strText = ((string[])(objDetail[0]))[intRowOfCurrentDetail];
            string strXml = ((string[])(objDetail[1]))[intRowOfCurrentDetail];
            clsDSTRichTextBoxValue objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
            objclsDSTRichTextBoxValue.m_strText = strText;
            objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
            objOtherDetail[19] = objclsDSTRichTextBoxValue;
            objOtherDetail[20] = (DateTime)objDetail[3];
        }

        private object[][] m_objGetRecordsValueArr(clsTransDataInfo p_objTransDataInfo)
        {
            #region ��ʾ��¼��DataGrid
            try
            {
                object[] objData;
                ArrayList objReturnData = new ArrayList();
                ArrayList arlDetail = new ArrayList();//��Ų����¼
                int intCurrentDetail = 0;//��ǰ�����¼��ArrayList�е�����
                int intRecordCount = 0;
                bool blnPreIsHide = false;//�ж���һ����¼�Ƿ�����
                int intCurrentSignIndex = 0;//��¼ǩ������
                bool blnMark = false;
                clsGeneralNurseRecordContent_ObstetricNewChildDataInfo objGNRCInfo = new clsGeneralNurseRecordContent_ObstetricNewChildDataInfo();
                clsDSTRichTextBoxValue objclsDSTRichTextBoxValue;
                string strText, strXml;

                objGNRCInfo = (clsGeneralNurseRecordContent_ObstetricNewChildDataInfo)p_objTransDataInfo;

                if (objGNRCInfo.m_objRecordArr == null && objGNRCInfo.m_objDetailArr == null)
                    return null;

                #region �Բ���۲졢�����ʩ��Ч�����д���
                if (objGNRCInfo.m_objDetailArr != null)
                {
                    string strDetail = "";
                    string strDetailXML = "";
                    for (int n = 0; n < objGNRCInfo.m_objDetailArr.Length; n++)
                    {
                        clsGeneralNurseRecordContent_ObstetricNewChildDetail objDetail = objGNRCInfo.m_objDetailArr[n];
                        object[] objTemp = new object[7];
                        strDetail = objDetail.m_strRECORDCONTENTAll;
                        strDetailXML = objDetail.m_strRECORDCONTENTXML;
                        string[] strDetailArr;
                        string[] strDetailXMLArr;
                        //�������¼��Ϊ�С�
                        com.digitalwave.controls.ctlRichTextBox.m_mthSplitXmlByBytes(strDetail, strDetailXML, 42, out strDetailArr, out strDetailXMLArr);

                        if (strDetail != string.Empty)
                        {
                            objTemp[0] = strDetailArr;
                            objTemp[1] = strDetailXMLArr;
                            objTemp[2] = strDetailArr.Length;
                            objTemp[3] = objDetail.m_dtmRECORDDATE;
                            objTemp[4] = objDetail.objSignerArr;
                            objTemp[5] = objDetail.m_strDetailCreateUserName;
                            objTemp[6] = objDetail.m_strCREATERECORDUSERID;
                            arlDetail.Add(objTemp);
                        }
                    }
                }
                #endregion

                if (objGNRCInfo.m_objRecordArr != null)
                    intRecordCount = objGNRCInfo.m_objRecordArr.Length;
                int intRowOfCurrentDetail = 0;

                #region ��ȡ�޸��޶�ʱ��
                int intCanModifyTime = 0;
                try
                {
                    intCanModifyTime = int.Parse(m_strCanModifyTime);
                }
                catch
                {
                    intCanModifyTime = 6;
                }
                #endregion

                clsGeneralNurseRecordContent_ObstetricNewChild objCurrent;
                clsGeneralNurseRecordContent_ObstetricNewChild objNext;
                for (int i = 0; i < intRecordCount; i++)
                {
                    blnMark = false;
                    objData = new object[23];
                    objCurrent = objGNRCInfo.m_objRecordArr[i];
                    objNext = new clsGeneralNurseRecordContent_ObstetricNewChild();//��һ�������¼
                    if (i < intRecordCount - 1)
                        objNext = objGNRCInfo.m_objRecordArr[i + 1];
                    clsGeneralNurseRecordContent_ObstetricNewChild objLast = null;
                    if (i > 0)
                        objLast = objGNRCInfo.m_objRecordArr[i - 1];
                    //����û����¼���޸�ǰ�ļ�¼������ָ��ʱ�����޸ĵģ��޸����봴����Ϊͬһ�ˣ�����ʾ
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        //  && objNext.m_strModifyUserID.Trim() == objCurrent.m_strCreateUserID.Trim()
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate)
                    {
                        TimeSpan tsModify = objNext.m_dtmModifyDate - objCurrent.m_dtmModifyDate;
                        if ((int)tsModify.TotalHours < intCanModifyTime)
                        {
                            blnPreIsHide = true;
                            continue;
                        }
                    }
                    #region ��Źؼ��ֶ�
                    if (objCurrent.m_dtmCreateDate != DateTime.MinValue)
                    {
                        objData[0] = objCurrent.m_dtmRECORDDATE;//��ż�¼ʱ����ַ���
                        objData[1] = (int)enmRecordsType.GeneralNurseRecord_ObstetricNewChildRec;//��ż�¼���͵�intֵ
                        objData[2] = objCurrent.m_dtmOpenDate;//��ż�¼��OpenDate�ַ���
                        objData[3] = objCurrent.m_dtmModifyDate;//��ż�¼��ModifyDate�ַ���   
                        objData[22] = objCurrent.m_strCreateUserID;//��ż�¼��createUserid�ַ���   
                        //ͬһ����ֻ�ڵ�һ����ʾ����
                        if (objCurrent.m_dtmRECORDDATE.Date.ToString() != m_dtmPreRecordDate.Date.ToString())
                        {
                            objData[4] = objCurrent.m_dtmRECORDDATE.Date.ToString("yyyy-MM-dd");//�����ַ���
                        }
                        //�޸ĺ���кۼ��ļ�¼������ʾʱ��
                        if (m_dtmPreRecordDate != objCurrent.m_dtmRECORDDATE)
                            objData[5] = objCurrent.m_dtmRECORDDATE.ToString("HH:mm");//ʱ���ַ���
                    }
                    #endregion ;
                    #region ��ŵ�����Ϣ
                    bool blnIsRed = false;
                    //����
                    strText = objCurrent.m_strTEMPERATURE_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strTEMPERATURE_RIGHT != objCurrent.m_strTEMPERATURE_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strTEMPERATURE_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    if (objLast != null && objLast.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objLast.m_dtmOpenDate == objLast.m_dtmOpenDate
                        && string.IsNullOrEmpty(objLast.m_strTEMPERATURE_RIGHT) && !blnPreIsHide)
                    {
                        blnIsRed = true;
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    if (blnIsRed)
                    {
                        objclsDSTRichTextBoxValue.m_clrTextColor = Color.Red;
                        blnIsRed = false;
                    }
                    objData[6] = objclsDSTRichTextBoxValue;//T

                    //����
                    strText = objCurrent.m_strHEARTRATE_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strHEARTRATE_RIGHT != objCurrent.m_strHEARTRATE_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strHEARTRATE_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    if (objLast != null && objLast.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objLast.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && string.IsNullOrEmpty(objLast.m_strHEARTRATE_RIGHT) && !blnPreIsHide)
                    {
                        blnIsRed = true;
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    if (blnIsRed)
                    {
                        objclsDSTRichTextBoxValue.m_clrTextColor = Color.Red;
                        blnIsRed = false;
                    }
                    objData[7] = objclsDSTRichTextBoxValue;//HR
                    //����
                    strText = objCurrent.m_strRESPIRATION_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strRESPIRATION_RIGHT != objCurrent.m_strRESPIRATION_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strRESPIRATION_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    if (objLast != null && objLast.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objLast.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && string.IsNullOrEmpty(objLast.m_strRESPIRATION_RIGHT) && !blnPreIsHide)
                    {
                        blnIsRed = true;
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    if (blnIsRed)
                    {
                        objclsDSTRichTextBoxValue.m_clrTextColor = Color.Red;
                        blnIsRed = false;
                    }
                    objData[8] = objclsDSTRichTextBoxValue;//P

                    //ض��
                    strText = objCurrent.m_strFONTANEL;
                    strXml = "<root />";
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[9] = objclsDSTRichTextBoxValue;
                    //����
                    strText = objCurrent.m_strCAPUTSUCCEDANEUM;
                    strXml = "<root />";
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[10] = objclsDSTRichTextBoxValue;
                    //Ѫ��
                    strText = objCurrent.m_strBLOODEDEMA;
                    strXml = "<root />";
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[11] = objclsDSTRichTextBoxValue;
                    //��ɫ
                    strText = objCurrent.m_strFACECOLOR;
                    strXml = "<root />";
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[12] = objclsDSTRichTextBoxValue;
                    //����
                    strText = objCurrent.m_strCRY;
                    strXml = "<root />";
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[13] = objclsDSTRichTextBoxValue;
                    //��˱��
                    strText = objCurrent.m_strSUCKPOWER;
                    strXml = "<root />";
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[14] = objclsDSTRichTextBoxValue;
                    //�겿
                    strText = objCurrent.m_strUMBILICALREGION;
                    strXml = "<root />";
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[15] = objclsDSTRichTextBoxValue;
                    //���
                    strText = objCurrent.m_strSTOOL_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strSTOOL_RIGHT != objCurrent.m_strSTOOL_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strSTOOL_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    if (objLast != null && objLast.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objLast.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && string.IsNullOrEmpty(objLast.m_strSTOOL_RIGHT) && !blnPreIsHide)
                    {
                        blnIsRed = true;
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    if (blnIsRed)
                    {
                        objclsDSTRichTextBoxValue.m_clrTextColor = Color.Red;
                        blnIsRed = false;
                    }
                    objData[16] = objclsDSTRichTextBoxValue;//
                    //С��
                    strText = objCurrent.m_strURINE_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && objNext.m_strURINE_RIGHT != objCurrent.m_strURINE_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strURINE_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    if (objLast != null && objLast.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objLast.m_dtmOpenDate == objCurrent.m_dtmOpenDate
                        && string.IsNullOrEmpty(objLast.m_strURINE_RIGHT) && !blnPreIsHide)
                    {
                        blnIsRed = true;
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    if (blnIsRed)
                    {
                        objclsDSTRichTextBoxValue.m_clrTextColor = Color.Red;
                        blnIsRed = false;
                    }
                    objData[17] = objclsDSTRichTextBoxValue;//

                    #endregion
                    #region ����۲졢�����ʩ��Ч��
                    for (; intCurrentDetail < arlDetail.Count; intCurrentDetail++)
                    {//ѭ��������в����¼
                        if ((DateTime)((object[])arlDetail[intCurrentDetail])[3] == objCurrent.m_dtmRECORDDATE)
                        {//����ǰ��¼�����벡��۲��¼������ͬ���������ǰ��¼�����������۲��¼
                            #region ִ��ǩ��
                            int m_intMax = Math.Max(objCurrent.objSignerArr == null ? 0 : objCurrent.objSignerArr.Length, ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length + ((clsEmrSigns_VO[])(((object[])arlDetail[intCurrentDetail])[4])).Length - 1);
                            for (int m = 0; m < m_intMax; m++)
                            {
                                if (objCurrent.objSignerArr != null && m < objCurrent.objSignerArr.Length)
                                {
                                    strText = objCurrent.objSignerArr[m].objEmployee.m_strLASTNAME_VCHR;
                                    objData[18] = strText;
                                }
                                if (m < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length)
                                {
                                    strText = ((string[])(((object[])arlDetail[intCurrentDetail])[0]))[m];
                                    strXml = ((string[])(((object[])arlDetail[intCurrentDetail])[1]))[m];
                                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                                    objclsDSTRichTextBoxValue.m_strText = strText;
                                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                                    objData[19] = objclsDSTRichTextBoxValue;
                                    objData[20] = (DateTime)((object[])arlDetail[intCurrentDetail])[3];
                                }
                                if (m >= ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length - 1 && m < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length)
                                {
                                    objData[21] = ((clsEmrSigns_VO[])(((object[])arlDetail[intCurrentDetail])[4]))[intCurrentSignIndex++].objEmployee.m_strLASTNAME_VCHR;
                                }
                                else
                                {
                                    objData[21] = "";
                                }
                                objReturnData.Add(objData);
                                objData = new object[23];
                            }

                            m_dtmPreRecordDate = objCurrent.m_dtmRECORDDATE;
                            intCurrentSignIndex = 0;
                            intCurrentDetail++;
                            blnMark = true;
                            break;
                            #endregion
                        }
                        else if ((DateTime)(((object[])arlDetail[intCurrentDetail])[3]) < objCurrent.m_dtmRECORDDATE)
                        {//����ǰ��¼���ڴ��ڲ���۲��¼���ڣ����������۲��¼��ѭ����һ������۲��¼
                            for (int j = intRowOfCurrentDetail; j < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length; j++)
                            {
                                object[] objOtherDetail = new object[23];
                                m_mthSetOtherDetail(((object[])arlDetail[intCurrentDetail]), intCurrentDetail, j, objCurrent, out objOtherDetail);
                                if (j == 0)
                                {
                                    //ͬһ����ֻ�ڵ�һ����ʾ����
                                    if (((DateTime)((object[])arlDetail[intCurrentDetail])[3]).ToString("yyyyMMdd") != m_dtmPreRecordDate.Date.ToString("yyyyMMdd") || m_dtmPreRecordDate == DateTime.MinValue)
                                    {
                                        objOtherDetail[4] = ((DateTime)((object[])arlDetail[intCurrentDetail])[3]).ToString("yyyy-MM-dd");
                                    }
                                    objOtherDetail[5] = ((DateTime)((object[])arlDetail[intCurrentDetail])[3]).ToString("HH:mm");
                                }
                                if ((clsEmrSigns_VO[])((object[])arlDetail[intCurrentDetail])[4] != null && j == ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length - 1)
                                {
                                    if (((clsEmrSigns_VO[])(((object[])arlDetail[intCurrentDetail])[4])) != null)
                                    {
                                        for (int h = 0; h < ((clsEmrSigns_VO[])(((object[])arlDetail[intCurrentDetail])[4])).Length; h++)
                                        {
                                            objOtherDetail[21] = ((clsEmrSigns_VO[])(((object[])arlDetail[intCurrentDetail])[4]))[h].objEmployee.m_strLASTNAME_VCHR;
                                            objReturnData.Add(objOtherDetail);
                                            objOtherDetail = new object[23];
                                        }
                                    }
                                    else
                                    {
                                        objOtherDetail[21] = "";
                                        objReturnData.Add(objOtherDetail);
                                        objOtherDetail = new object[23];
                                    }
                                    
                                }
                                else
                                {
                                    objOtherDetail[21] = "";
                                    objReturnData.Add(objOtherDetail);
                                }

                            }
                            m_dtmPreRecordDate = (DateTime)(((object[])arlDetail[intCurrentDetail])[3]);
                            intRowOfCurrentDetail = 0;

                        }
                        else
                        {//����ǰ��¼����С�ڲ���۲��¼���ڣ��������ǰ��¼��ѭ����һ����ǰ��¼
                            #region ִ��ǩ������¼ǩ��
                            //ͬһ����ֻ�ڵ�һ����ʾ����
                            if (objCurrent.m_dtmRECORDDATE.Date.ToString("yyyyMMdd") != m_dtmPreRecordDate.Date.ToString("yyyyMMdd"))
                            {
                                objData[4] = objCurrent.m_dtmRECORDDATE.Date.ToString("yyyy-MM-dd");//�����ַ���
                            }
                            if (objCurrent.objSignerArr != null)
                            {
                                for (int m = 0; m < objCurrent.objSignerArr.Length; m++)
                                {
                                    objData[18] = objCurrent.objSignerArr[m].objEmployee.m_strLASTNAME_VCHR;
                                    objData[21] = "";
                                    objReturnData.Add(objData);
                                    objData = new object[23];
                                }
                            }
                            m_dtmPreRecordDate = objCurrent.m_dtmRECORDDATE;
                            #endregion
                            break;
                        }
                    }
                    #endregion
                    if (!blnMark && intCurrentDetail == arlDetail.Count)
                    {
                        #region ִ��ǩ������¼ǩ��
                        //ͬһ����ֻ�ڵ�һ����ʾ����
                        if (objCurrent.m_dtmRECORDDATE.Date.ToString("yyyyMMdd") != m_dtmPreRecordDate.Date.ToString("yyyyMMdd"))
                        {
                            objData[4] = objCurrent.m_dtmRECORDDATE.Date.ToString("yyyy-MM-dd");//�����ַ���
                        }
                        if (objCurrent.objSignerArr != null)
                        {
                            for (int m = 0; m < objCurrent.objSignerArr.Length; m++)
                            {
                                objData[18] = objCurrent.objSignerArr[m].objEmployee.m_strLASTNAME_VCHR;
                                objData[21] = "";
                                objReturnData.Add(objData);
                                objData = new object[23];
                            }
                        }
                        m_dtmPreRecordDate = objCurrent.m_dtmRECORDDATE;
                        #endregion
                    }
                }

                #region �������۲졢�����ʩ��Ч��δ��ʾ������������¼��ȫ����ʾ�꣬��������ʣ�ಿ��
                while (arlDetail != null && intCurrentDetail < arlDetail.Count && arlDetail.Count > 0)
                {
                    if (intRowOfCurrentDetail < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length)
                    {
                        for (int m = intRowOfCurrentDetail; m < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length; m++)
                        {
                            object[] objOtherDetail = new object[23];
                            m_mthSetOtherDetail(((object[])arlDetail[intCurrentDetail]), intCurrentDetail, m, null, out objOtherDetail);
                            if (m == 0)
                            {
                                //ͬһ����ֻ�ڵ�һ����ʾ����
                                if (((DateTime)((object[])arlDetail[intCurrentDetail])[3]).ToString("yyyyMMdd") != m_dtmPreRecordDate.Date.ToString("yyyyMMdd"))
                                {
                                    objOtherDetail[4] = ((DateTime)((object[])arlDetail[intCurrentDetail])[3]).ToString("yyyy-MM-dd");
                                }
                                objOtherDetail[5] = ((DateTime)((object[])arlDetail[intCurrentDetail])[3]).ToString("HH:mm");
                            }
                            if ((clsEmrSigns_VO[])((object[])arlDetail[intCurrentDetail])[4] != null && m == ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length - 1)
                            {
                                if (((clsEmrSigns_VO[])(((object[])arlDetail[intCurrentDetail])[4])) != null)
                                {
                                    for (int h = 0; h < ((clsEmrSigns_VO[])(((object[])arlDetail[intCurrentDetail])[4])).Length; h++)
                                    {
                                        objOtherDetail[21] = ((clsEmrSigns_VO[])(((object[])arlDetail[intCurrentDetail])[4]))[h].objEmployee.m_strLASTNAME_VCHR;
                                        objReturnData.Add(objOtherDetail);
                                        objOtherDetail = new object[23];
                                    }
                                }
                                else
                                {
                                    objOtherDetail[21] = "";
                                    objReturnData.Add(objOtherDetail);
                                    objOtherDetail = new object[23];
                                }
                                
                            }
                            else
                            {
                                objOtherDetail[21] = "";
                                objReturnData.Add(objOtherDetail);
                            }
                        }
                        m_dtmPreRecordDate = (DateTime)((object[])arlDetail[intCurrentDetail])[3];
                        intCurrentDetail++;
                        intRowOfCurrentDetail = 0;
                    }
                }
                object[][] m_objRe = new object[objReturnData.Count][];

                for (int m = 0; m < objReturnData.Count; m++)
                    m_objRe[m] = (object[])objReturnData[m];
                return m_objRe;
            }
                #endregion
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            #endregion
        }

    }
}
