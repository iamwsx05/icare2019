using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.Utility.Controls;
using System.Drawing;
 
namespace iCare
{
	/// <summary>ma
    /// һ �㻼�߻����¼��ӡ������(�°�)ժҪ˵����
	/// </summary>
	public class clsGeneralNurseRecord_GXPrintTool: infPrintRecord
	{
		private bool m_blnIsFromDataSource=true;               //�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
		private bool m_blnWantInit=true;	                   //�Ƿ���Ҫ��ʼ��	
		private clsRecordsDomain m_objRecordsDomain;           //��¼��
		private clsPrintInfo_GeneralNurseGX m_objPrintInfo;     //��ӡ����
		private string strCurrentClass;                        //��ǰ���Ĭ��Ϊ��
		private int intCaseRowCount;                           //��ǰ���̼�¼���������
		private string[] strCurrentCaseTextArr;                //��ǰ���̼�¼��������
		private string[] strCurrentCaseXmlArr;                 //��ǰ���̼�¼�ۼ�����
		private string[] strCurrentCaseCreateDateArr;          //��ǰ���̼�¼����ʱ��
		private object [][] objDataArr;
	
		private string strDiagnose;
		private object[] objtest1;

		private string[] m_strCustomColumn;
	

		private bool m_bSummaryRow=false;
		

		public clsGeneralNurseRecord_GXPrintTool(string[] m_strColumnName)
		{
		   
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			m_strCustomColumn=m_strColumnName;
			//
		}
		/// ��ȡ������
		/// </summary>
		/// <param name="dtStartTime"></param>
		/// 
		private string m_strGetDSTTextXML(string p_strText,string p_strModifyUserID,string p_strModifyUserName)
		{
			return com.digitalwave.controls.ctlRichTextBox.clsXmlTool.s_strMakeDSTXml(p_strText,p_strModifyUserID,p_strModifyUserName,Color.Black,Color.White);
		}

		
		
		#region ��ӡ��ʼ�����¼�
		/// <summary>
		/// ���ô�ӡ��Ϣ(�������ݿ��ȡʱҪ���ȵ���.)
		/// </summary>
		/// <param name="p_objPatient">����</param>
		/// <param name="p_dtmInPatientDate">��Ժ����</param>
		/// <param name="p_dtmOpenDate">OpenDate�������һ�δ�ӡ��μ�¼�������ͣ��粡����¼��������OpenDate</param>
		/// p_objPatient
		
		public void m_mthSetPrintInfo(clsPatient p_objPatient,DateTime p_dtmInPatientDate,DateTime p_dtmOpenDate)
		{		
			m_blnIsFromDataSource=true;//�����Ǵ����ݿ��ȡ
			clsPatient m_objPatient=p_objPatient;
			m_objPrintInfo=new  clsPrintInfo_GeneralNurseGX ();
			m_objPrintInfo.m_strInPatentID=m_objPatient!=null? m_objPatient.m_StrInPatientID:"";					
			m_objPrintInfo.m_strPatientName=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrFirstName :"";
			m_objPrintInfo.m_strSex=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrSex:"";
			m_objPrintInfo.m_strAge=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrAge : "";
			m_objPrintInfo.m_strBedName=m_objPatient!=null? m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName :"";
			m_objPrintInfo.m_strDeptName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjDept.m_StrDeptName :"";
			m_objPrintInfo.m_dtmInPatientDate=p_dtmInPatientDate;
            m_objPrintInfo.m_strHISInPatientID = m_objPatient != null ? m_objPatient.m_StrHISInPatientID : "";
            m_objPrintInfo.m_dtmHISInPatientDate = m_objPatient != null ? m_objPatient.m_DtmSelectedHISInDate : DateTime.MinValue;
		}

		/// <summary>
		/// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���(�������ݿ��ȡʱҪ����.)
		/// </summary>
		public void m_mthInitPrintContent()
		{	//m_objprintinfoΪ�ձ���δ���ô�ӡ����		
			if(m_objPrintInfo==null)
			{
				clsPublicFunction.ShowInformationMessageBox("����m_mthInitPrintContent֮ǰ�����ȵ���m_mthSetPrintInfo����");
				return;
			}
			//����Ϊ��
			if(m_objPrintInfo.m_strInPatentID=="")
				return;
			//��ȡ��ӡ����
            m_objRecordsDomain = new clsRecordsDomain(enmRecordsType.GeneralNurseRecord_GXRec);
			long lngRes = m_objRecordsDomain.m_lngGetPrintInfo(m_objPrintInfo.m_strInPatentID,m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"),out m_objPrintInfo .m_objTransDataArr,out m_objPrintInfo.m_dtmFirstPrintDateArr,out m_objPrintInfo.m_blnIsFirstPrintArr);
			if(lngRes <= 0)
				return ;   
        	m_blnWantInit=false;
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
		private string strTempDate=string.Empty;

		private int m_intCurrentPagePrintRow=0;
		private int m_intCurrentContentRow=0;

		private int m_intMainCurrentPagePrintRow=0;
		private int m_intMainCurrentContentRow=0;
		

		
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
			if(m_blnIsFromDataSource )
			{
				if(m_objPrintInfo==null)
				{
					clsPublicFunction.ShowInformationMessageBox("�������ݿ��ȡʱ,����m_objGetPrintInfo֮ǰ�����ȵ���m_mthSetPrintInfo����");
					return null;
				}

				if(m_blnWantInit)
					m_mthInitPrintContent();				
			}			

			//û�м�¼����ʱ�����ؿ�
            if (m_objPrintInfo.m_objTransDataArr == null || m_objPrintInfo.m_objTransDataArr.Length == 0)
				return null;
			else
				return m_objPrintInfo;

               
		}		

		/// <summary>
		/// ��ʼ����ӡ����,��������ն��󼴿�.
		/// </summary>
		public void m_mthInitPrintTool(object p_objArg)
		{				
			m_fotTitleFont = new Font("SimSun", 20,FontStyle.Bold );
			m_fotHeaderFont = new Font("SimSun", 12f);
			m_fotSmallFont = new Font("SimSun",10.5f);
			m_fotHosNameFont= new Font("SimSun",14f);
			m_fotTinyFont=new Font("SimSun",9f);

			m_GridPen = new Pen(Color.Black,1);
			m_GridRedPen = new Pen(Color.Red ,2);

			m_slbBrush = new SolidBrush(Color.Black);
			m_objPageSetting = new clsPrintPageSettingForRecord();
            m_objPrintContext = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, m_fotSmallFont);			
			m_intCurrentRecord=0;
			intNowPage=1;
			blnBeginPrintNewRecord=true;

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
			frmPreview.m_evtBeginPrint +=new PrintEventHandler(frmPreview_m_evtBeginPrint);
			frmPreview.m_evtEndPrint +=new PrintEventHandler(frmPreview_m_evtEndPrint);
			frmPreview.m_evtPrintContent +=new PrintPageEventHandler(frmPreview_m_evtPrintContent);
			frmPreview.m_evtPrintFrame +=new PrintPageEventHandler(frmPreview_m_evtPrintFrame);
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
			if(m_blnIsFromDataSource==false || m_objPrintInfo.m_strInPatentID=="") return;
			//�����ӡ�ɹ�������������Ҫ���µ�ʱ�䣬����У�����ʱ�䡣 
			if(!((PrintEventArgs)p_objPrintArg).Cancel && m_objPrintInfo.m_blnIsFirstPrintArr != null)
			{
				ArrayList arlRecordType = new ArrayList();
				ArrayList arlOpenDate = new ArrayList();
				int intUpdateIndex = -1;//��û���κμ�¼
				for(int i=0;i<m_objPrintInfo.m_blnIsFirstPrintArr.Length;i++)
				{
					if(m_objPrintInfo.m_blnIsFirstPrintArr[i])
					{
                        if (m_objPrintInfo.m_objTransDataArr[i] != null)
                        {
                            //���¼�¼��ֻ��ʹ���µ��״δ�ӡʱ����Ϊ��Ч�����������
                            //��ż�¼����
                            arlRecordType.Add(m_objPrintInfo.m_objTransDataArr[i].m_intFlag);
                            //��ż�¼��OpenDate
                            arlOpenDate.Add(m_objPrintInfo.m_objTransDataArr[i].m_objRecordContent.m_dtmOpenDate);
                            intUpdateIndex = i;
                        }
					}
				}   

				if(intUpdateIndex >= 0)
				{
					m_objRecordsDomain.m_lngUpdateFirstPrintDate(m_objPrintInfo.m_strInPatentID,m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"),(int[])arlRecordType.ToArray(typeof(int)),(DateTime[])arlOpenDate.ToArray(typeof(DateTime)),m_objPrintInfo.m_dtmFirstPrintDateArr[intUpdateIndex]);
				}
				#region �����Ԥ������Ӧ��ִ�У�����Ԥ�����ӡ�������Ϊû�����³�ʼ��
//				m_objPrintInfo.m_objTransDataArr = null;
//				m_objPrintInfo.m_blnIsFirstPrintArr = null;
				#endregion
			}                          
			m_mthEndPrintSub((PrintEventArgs)p_objPrintArg);
		}	
	
		public void m_mthSetPrintContent(object p_objPrintContent)
		{
			if(p_objPrintContent.GetType().Name !="clsPrintInfo_GeneralNurseGX")
			{
				clsPublicFunction.ShowInformationMessageBox("��������");
			}
			m_blnIsFromDataSource=false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ
			m_objPrintInfo=(clsPrintInfo_GeneralNurseGX)p_objPrintContent;
			//m_objPrintDataArr= m_objPrintInfo. m_objPrintDataArr ;		
			
			m_blnWantInit=false;
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

		private  void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
		{
			if(m_objPrintInfo.m_objTransDataArr == null)
				m_mthInitPrintContent();
		}
		// ��ӡҳ
		private void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
		{
			try
			{
				p_objPrintPageArg.HasMorePages =false;
				m_mthPrintTitleInfo(p_objPrintPageArg);
				m_mthPrintRectangleInfo(p_objPrintPageArg);	
				m_mthPrintHeaderInfo(p_objPrintPageArg);
				m_mthAddDataToGrid(p_objPrintPageArg);
			}
			catch(Exception err)
			{
				clsPublicFunction.ShowInformationMessageBox(err.Message + "\r\n" + err.StackTrace);

			}
		}

		

		// ��ӡ����ʱ�Ĳ���
		private  void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
		{
			m_intCurrentRecord=0;
			intNowPage=1;
			m_intCurrentRecord=0;
			m_intCurrentRecord=0;
			m_intCurrentContentRow=0;
			m_intMainCurrentContentRow=0;
			m_intCurrentPagePrintRow=0;
			intNowPage=1;
			blnBeginPrintNewRecord=true;
			m_intRowNumberForPrintData = 0;
			m_intPosY = (int)enmRecordRectangleInfo.TopY+130;
		}
		


		#region �йش�ӡ������
		/// <summary>
		/// ���д�ӡ������
		/// </summary>
		private clsGeneralNurseRecordContent_GXDataInfo[] m_objPrintDataArr;

		/// <summary>
		/// ������Σ�ػ����¼�ģ���ӡ�����ĵ���
		/// </summary>		
        private com.digitalwave.controls.clsPrintRichTextContext m_objPrintContext;
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
		int m_intPosY = (int)enmRecordRectangleInfo.TopY+130;
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
		private Pen m_GridRedPen;
		
		/// <summary>
		/// ˢ��
		/// </summary>
		private SolidBrush m_slbBrush;
		/// <summary>
		/// ��¼��ӡ���ڼ�ҳ
		/// </summary>
		private int intNowPage=1;
		/// <summary>
		/// ��ǰ��ӡ�ļ�¼�����
		/// </summary>
		private int m_intCurrentRecord=0;  
		
		/// <summary>
		/// �ɼ�¼����,׼����ӡһ���¼�¼
		/// </summary>
		bool m_blnBeginPrintNewRecord=true;		

		/// <summary>
		/// �ɼ�¼����,׼����ӡһ���¼�¼
		/// </summary>
		bool blnBeginPrintNewRecord=true;	
	
		/// <summary>
		/// ��ǰ��¼�����������޸ĵĴε�����
		/// </summary>
		private int m_intNowRowInOneRecord=0; 	

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
			RowsMark1=30,
			/// <summary>
			/// ��ͷ�ڶ����ָ���
			/// </summary>
			RowsMark2=90,
	

			///<summary>
			/// ���ӵ����
			/// </summary>
			LeftX = 10,
			/// <summary>
			/// ���ӵ��Ҷ�
			/// </summary>
			RightX = 790,
			/// <summary>
			/// ����ÿ�еĲ���
			/// </summary>
			RowStep = 38,
			/// <summary>
			/// ���ӵ�����
			/// </summary>
			RowLinesNum = 23,	
			/// <summary>
			/// �����ڸ�������Ը��Ӷ��˵Ĵ�ֱƫ��
			/// </summary>
			VOffSet = 20,
			/// <summary>
			/// �е���Ŀ
			/// </summary>
			ColumnsNum=10,
			/// <summary>
			/// ��һ�������(X),ʱ�䣨����ߣ�
			/// </summary>			
			ColumnsMark1=80,

			/// <summary>
			/// �ڶ��������(X)��T������ߣ�
			/// </summary>
			ColumnsMark2=130,

			/// <summary>
			/// ��3�������(X)��P������ߣ�
			/// </summary>
			ColumnsMark3=180,

			/// <summary>
			/// R������ߣ�
			/// </summary>
			ColumnsMark4=230,

			/// <summary>
			/// ���ʣ�����ߣ�
			/// </summary>
			ColumnsMark5=280,

			/// <summary>
			/// BP������ߣ�
			/// </summary>
			ColumnsMark6=330,

			/// <summary>
			///�Զ���1������ߣ�
			/// </summary>
			ColumnsMark7=380,

			/// <summary>
			/// �Զ���2������ߣ�
			/// </summary>
			ColumnsMark8=420,

			/// <summary>
			/// ǩ��������ߣ�
			/// </summary>
			ColumnsMark9=460,

			/// <summary>
			/// �����¼
			/// </summary>
			ColumnsMark10=520,
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
			public clsPrintPageSettingForRecord(){}
			
			/// <summary>
			/// ��������
			/// </summary>
			/// <param name="p_intItemName">��Ŀ����</param>
			/// <returns></returns>
			public PointF m_getCoordinatePoint(int p_intItemName)
			{
				PointF m_fReturnPoint;
				switch(p_intItemName)
				{   
                    
					case (int)enmItemDefination.Page_HospitalName:
						m_fReturnPoint = new PointF(320f,30f);
						break;
					case (int)enmItemDefination.Page_Name_Title:
						m_fReturnPoint = new PointF(190f,70f);
						break;
					case (int)enmItemDefination.Name_Title :
						m_fReturnPoint = new PointF(20f,120f);
						break;
					case (int)enmItemDefination.Name:
						m_fReturnPoint = new PointF(70f,120f);
						break;

					case (int)enmItemDefination.Sex_Title :
						m_fReturnPoint = new PointF(160f,120f);
						break;
					case (int)enmItemDefination.Sex :
						m_fReturnPoint = new PointF(210f,120f);
						break;

					case (int)enmItemDefination.Age_Title :
						m_fReturnPoint = new PointF(240f,120f);
						break;
					case (int)enmItemDefination.Age:
						m_fReturnPoint = new PointF(280f,120f);
						break;

					case (int)enmItemDefination.Dept_Name_Title:
						m_fReturnPoint = new PointF(365f,120f);
						break;
					case (int)enmItemDefination.Dept_Name :
						m_fReturnPoint = new PointF(415f,120f);
						break;
					
					case (int)enmItemDefination.BedNo_Title :
						m_fReturnPoint = new PointF(570f,120f);
						break;
					case (int)enmItemDefination.BedNo:
						m_fReturnPoint = new PointF(620f,120f);
						break;

					case (int)enmItemDefination.InPatientID_Title:
						m_fReturnPoint = new PointF(650f,120f);
						break;
					case (int)enmItemDefination.InPatientID :
						m_fReturnPoint = new PointF(710f,120f);
						break;
											
					default:
						m_fReturnPoint = new PointF(400f,400f);
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
			clsEveryRecordPageInfo objEveryRecordPageInfo=new clsEveryRecordPageInfo ();
			//************************************************			
			objEveryRecordPageInfo.strAge =m_objPrintInfo.m_strAge;
			objEveryRecordPageInfo.strPatientName=m_objPrintInfo.m_strPatientName;
			objEveryRecordPageInfo.strBedNo =m_objPrintInfo.m_strBedName;
			objEveryRecordPageInfo.strAreaName=m_objPrintInfo.m_strAreaName;
			objEveryRecordPageInfo.strSex=m_objPrintInfo.m_strSex;
			objEveryRecordPageInfo.strInPatientID=m_objPrintInfo.m_strHISInPatientID;
			objEveryRecordPageInfo.strPrintDate=( m_objPrintInfo.m_strInPatentID!="")? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") :"";		
		
            

            //m_mthDrawMultiString(this.m_fotHosNameFont ,(int)enmRecordRectangleInfo.LeftX ,50,(int)enmRecordRectangleInfo.RightX ,100,1,1,clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,e);
				
            //m_mthDrawMultiString(this.m_fotTitleFont ,(int)enmRecordRectangleInfo.LeftX ,100,(int)enmRecordRectangleInfo.RightX ,150,1,1,"һ  ��  ��  ��  ��  ��  ��  ¼",e);

            e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_HospitalName));

            e.Graphics.DrawString("һ  ��  ��  ��  ��  ��  ��  ¼", m_fotTitleFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_Name_Title));

			e.Graphics.DrawString("���ң�",m_fotSmallFont,m_slbBrush,365,120);
			e.Graphics.DrawString(m_objPrintInfo.m_strDeptName,m_fotSmallFont,m_slbBrush,415,120);

			e.Graphics.DrawString("���ţ�",m_fotSmallFont,m_slbBrush,570,120);
			e.Graphics.DrawString(objEveryRecordPageInfo.strBedNo  ,m_fotSmallFont,m_slbBrush,620,120);	
			
			e.Graphics.DrawString("������",m_fotSmallFont,m_slbBrush,20 ,120);
			e.Graphics.DrawString(objEveryRecordPageInfo.strPatientName ,m_fotSmallFont,m_slbBrush,70,120);

            e.Graphics.DrawString("�Ա�", m_fotSmallFont, m_slbBrush, 160, 120);
            e.Graphics.DrawString(objEveryRecordPageInfo.strSex, m_fotSmallFont, m_slbBrush, 210, 120);

            e.Graphics.DrawString("���䣺", m_fotSmallFont, m_slbBrush, 240, 120);
            e.Graphics.DrawString(m_objPrintInfo.m_strAge, m_fotSmallFont, m_slbBrush, 280, 120);
		
			e.Graphics.DrawString("סԺ�ţ�",m_fotSmallFont,m_slbBrush,650,120);
			e.Graphics.DrawString(objEveryRecordPageInfo.strInPatientID  ,m_fotSmallFont,m_slbBrush,710,120);	
			
		
            //e.Graphics.DrawString("���ڣ�",m_fotSmallFont,m_slbBrush,(float)enmRecordRectangleInfo.RightX -180 ,150);
            //e.Graphics.DrawString(DateTime.Now.ToString("yyyy��MM��dd��"),m_fotSmallFont,m_slbBrush,(float)enmRecordRectangleInfo.RightX -140,150);	
		}
		#endregion
		private void m_mthDrawMultiString(Font fotNormal,Font fotSmall,int iLimitLenth,float x,float y,float x1,float y1,float xOff,float yOff,string strContent,System.Drawing.Printing.PrintPageEventArgs e)
		{
		
			RectangleF drawRect=new RectangleF(x,y+1,x1-x,y1-y);
			RectangleF drawRectNormal=new RectangleF(x,y+yOff,x1-x,y1-y);

			StringFormat strFormat=new StringFormat();
			strFormat.Alignment =System.Drawing.StringAlignment.Center ;
			strFormat.FormatFlags=System.Drawing.StringFormatFlags.LineLimit;   
  
			if(strContent.Length >iLimitLenth)
			{
                if (strContent.Length > iLimitLenth + 1)
                {
                    e.Graphics.DrawString(strContent, fotSmall, m_slbBrush, drawRect, strFormat);
                }
                else//��Ϊ�����С��ֻ���һ��ʱδ����һ�У���ʱ��������
                {
                    e.Graphics.DrawString(strContent, fotSmall, m_slbBrush, drawRectNormal, strFormat);
                }
			}
			else
			{
				e.Graphics.DrawString(strContent,fotNormal,m_slbBrush,drawRectNormal,strFormat);
				
			}
        
		}
		private void m_mthDrawMultiString(Font fotNormal,float x,float y,float x1,float y1,float xOff,float yOff,string strContent,System.Drawing.Printing.PrintPageEventArgs e)
		{
		
			RectangleF drawRect=new RectangleF(x,y+yOff,x1-x,y1-y);
			StringFormat strFormat=new StringFormat();
			strFormat.Alignment =System.Drawing.StringAlignment.Center ;
			strFormat.FormatFlags=System.Drawing.StringFormatFlags.LineLimit;   
  
			e.Graphics.DrawString(strContent, fotNormal,m_slbBrush,drawRect,strFormat);
			
        
		}

		#region ���������Ŀ
		/// <summary>
		/// ���������Ŀ
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintHeaderInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{
			
            e.Graphics.DrawString("����",m_fotHeaderFont ,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+20,
				(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep-2);
			e.Graphics.DrawString("ʱ��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark1+5, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep-2);
			
			//T.
			e.Graphics.DrawString("T: C��P:��/�֡�R: ��/�֡�BP:mmHg",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark2 +20,(int)enmRecordRectangleInfo.TopY+10);
			e.Graphics.DrawString("T",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark2 +20,(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep  +10);
			e.Graphics.DrawString("P",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark3 +20,(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep +10);
			e.Graphics.DrawString("R",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark4 +20,(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep +10);
			e.Graphics.DrawString("����",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark5 +5,(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep +10);
			e.Graphics.DrawString("BP",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark6 +10,(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep +10);
			//T.�Զ���
			m_mthDrawMultiString(m_fotHeaderFont,m_fotHeaderFont,3,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7,(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8,(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowStep*2),10,10,"",e);
			
            //e.Graphics.DrawString(m_strCustomColumn[1].ToString() ,m_fotHeaderFont ,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8 +10,
            //    (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+5);

            StringFormat m_sfmPrint = new StringFormat(StringFormatFlags.FitBlackBox);
            m_sfmPrint.Alignment = StringAlignment.Near;
            string strCustomColumn0 = m_strCustomColumn[0].ToString().Replace("\r\n", "");
            if (strCustomColumn0.Length > 4)
            {
                e.Graphics.DrawString(strCustomColumn0, m_fotTinyFont, m_slbBrush,
                    new RectangleF((int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7 + 1, (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep, 60, 40), m_sfmPrint);
            }
            else
            {
                e.Graphics.DrawString(strCustomColumn0, m_fotHeaderFont, m_slbBrush,
                    new RectangleF((int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7 + 1, (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep, 40, 40), m_sfmPrint);
            }

            string strCustomColumn1 = m_strCustomColumn[1].ToString().Replace("\r\n", "");
            if (strCustomColumn1.Length > 4)
            {
                e.Graphics.DrawString(strCustomColumn1, m_fotTinyFont, m_slbBrush,
                new RectangleF((int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark8 + 1, (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep, 60, 40), m_sfmPrint);
            }
            else
            {
                e.Graphics.DrawString(strCustomColumn1, m_fotHeaderFont, m_slbBrush,
                    new RectangleF((int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark8 + 1, (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep, 40, 40), m_sfmPrint);
            }
			//ǩ��
			e.Graphics.DrawString("ǩ��",m_fotHeaderFont ,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark9 +10,
				(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep-2);
			//�����¼
			e.Graphics.DrawString("�����¼",m_fotHeaderFont ,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark10 +70,
				(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep-2);
		
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
			for(int i1=0;i1<=(int)enmRecordRectangleInfo.RowLinesNum  ;i1++)
			{
				if(i1==0)
					e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX ,
						(int)enmRecordRectangleInfo.TopY,
						(int)enmRecordRectangleInfo.RightX,
						(int)enmRecordRectangleInfo.TopY);
				else if(i1==1)
					e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+ (int)enmRecordRectangleInfo.ColumnsMark2,
						(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep,
						(int)enmRecordRectangleInfo.LeftX+ (int)enmRecordRectangleInfo.ColumnsMark9,
						(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep) ;
				else //if(i1==2)
					e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX,
						(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowStep * i1),
						(int)enmRecordRectangleInfo.RightX,
						(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowStep * i1));
			}
			
			#endregion �����Ӻ���
			#region ����������
			int intHeight=(int)enmRecordRectangleInfo.RowStep *23;
			//��������� ����
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX,(int)enmRecordRectangleInfo.TopY+intHeight);
			//��ʱ������� ����
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark1,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark1,(int)enmRecordRectangleInfo.TopY+intHeight);
			//��T����� ����
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark2,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark2,(int)enmRecordRectangleInfo.TopY+intHeight);
			//��p�����
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark3,(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark3,(int)enmRecordRectangleInfo.TopY+intHeight);
			//��R����� 
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark4,(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark4,(int)enmRecordRectangleInfo.TopY+intHeight);
			//�����������
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark5,(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark5,(int)enmRecordRectangleInfo.TopY+intHeight);
			//��BP�����
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark6,(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark6,(int)enmRecordRectangleInfo.TopY+intHeight);
			//���Զ���1�����
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7,(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7,(int)enmRecordRectangleInfo.TopY+intHeight);
			//���Զ���2�����
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8,(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8,(int)enmRecordRectangleInfo.TopY+intHeight);
			//��ǩ�� ����
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark9,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark9,(int)enmRecordRectangleInfo.TopY+intHeight);
			//�������¼����� ����
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark10,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark10,(int)enmRecordRectangleInfo.TopY+intHeight);
			
			//���ұ����� ����
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.RightX,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.RightX ,(int)enmRecordRectangleInfo.TopY+intHeight);
			#endregion 		
			
		}

						
		#endregion

		#region ��ӡ����ʵ��
		
	
		/*��¼��ҳ��ǰ�Ĵ�ӡ����*/
		int intNowRow=1; 

		/*��¼��ǰ��ӡ������¼��m_objPrintDataArr�е���ţ����ڻ�ҳ����Ŵ�ӡ*/
		private int m_intRowNumberForPrintData=0;
		
		#region ������ݵ����		
		/// <summary>
		/// ������ݵ����
		/// </summary>
		/// <param name="e"></param>
		private void m_mthAddDataToGrid(System.Drawing.Printing.PrintPageEventArgs e)
		{  
			string strRecord="";			
			string strRecordXML="";			
			DateTime dtmFlagTime;
			/*��¼��ҳ��ǰ�Ĵ�ӡ����*/
			intNowRow=1; 			
			
			int iTemp=(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowStep*((int)enmRecordRectangleInfo.RowLinesNum))+20;
            m_mthDrawMultiString(this.m_fotSmallFont ,(int)enmRecordRectangleInfo.LeftX ,iTemp,(int)enmRecordRectangleInfo.RightX ,iTemp+50,1,1,"����"+intNowPage.ToString()+"ҳ��",e);
			//	if(m_objPrintInfo.m_strInPatentID ==""|| m_objPrintDataArr==null)return;
			if(m_objPrintInfo.m_strInPatentID =="")return;

			m_intCurrentRecord=0;

			//��ӡ��ѭ��


			for(int i1=m_intMainCurrentContentRow;i1<m_objPrintInfo.m_objTransDataArr.Length ;i1++)
			{ 
	
			   
              
				clsGeneralNurseRecordContent_GXDataInfo clsGereralData=new clsGeneralNurseRecordContent_GXDataInfo ();
				clsGereralData =(clsGeneralNurseRecordContent_GXDataInfo) m_objPrintInfo.m_objTransDataArr [i1];

				objDataArr = m_objGetRecordsValueArr(clsGereralData);
			   
				if(objDataArr==null)
					continue;
				for(m_intCurrentRecord=m_intCurrentContentRow;m_intCurrentRecord<objDataArr.Length ;m_intCurrentRecord++)
				{
					enmReturnValue m_enmRe= m_blnPrintOneValueGX(e, m_intPosY);	
					//���ݷ���ֵ����ҳ���
					if (m_enmRe == enmReturnValue.enmFaild)
						e.HasMorePages=false;
					if(m_enmRe==enmReturnValue.enmNeedNextPage)
					{
						m_intRowNumberForPrintData=m_intCurrentRecord;
						m_intPosY = (int)enmRecordRectangleInfo.TopY+130;
						e.HasMorePages=true;
						return;
					}

					if(m_enmRe==enmReturnValue.enmSuccessed)
					{
						e.HasMorePages=false;
						blnBeginPrintNewRecord=true;
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
		private enmReturnValue m_blnPrintOneValueGX(System.Drawing.Printing.PrintPageEventArgs e,int p_intBottomY)
		{			
			//m_intPosY +=(int)enmRecordRectangleInfo.VOffSet;
			enmReturnValue enmIsRecordFinish=m_blnPrintOneRowValueGX(e);
			if(enmIsRecordFinish!=enmReturnValue.enmNeedNextPage)
			{
			

				m_intRowNumberInValueArr=0;
				m_intRowNumberInTempArr=0;
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
          
			string strTemp="";
			if(obj==null)
			{
				strTemp="";
			}
			else
			{
				if(obj.GetType().Name =="clsDSTRichTextBoxValue")
				{
					objclsDSTRichTextBoxValue=(clsDSTRichTextBoxValue)obj;
					if(objclsDSTRichTextBoxValue.m_blnUnderDST==true)
					{
						m_bSummaryRow=true;
					}
                    
					strTemp= objclsDSTRichTextBoxValue.m_strText!=null? objclsDSTRichTextBoxValue.m_strText:"";				
					
				}
				else
				{
					strTemp=obj.ToString();
				}
			}
			return strTemp;
		}
		private bool m_blnCheckPageChange(System.Drawing.Printing.PrintPageEventArgs e)
		{
			//����ǰ�г������һ�У��� >ҳ��������ʱ��ҳ
			if(m_intCurrentPagePrintRow>((int)enmRecordRectangleInfo.RowLinesNum-3)/*��ȥ��ͷ2��������Ч����*/) 
			{
				m_intCurrentPagePrintRow=0;
				intNowPage ++;

				return true;
			}
			else return false;
		}
 
		#endregion 						

	


		//������������������m_intRowNumberForPrintData�������������ϵͳ�ڻ�ҳ�������һҳ�Ĵ�ӡ
		/// <summary>
		/// ��¼���µ�һҳ��Ҫ��ӡ�ĵ�һ����¼�ڴ�ӡ����strValueArr�е����
		/// </summary>
		private int m_intRowNumberInValueArr=0;

		/// <summary>
		/// ��¼���µ�һҳ��Ҫ��ӡ�ĵ�һ����¼��TempArr����������
		/// </summary>
		private int m_intRowNumberInTempArr=0;
		
		
	
		private enmReturnValue m_blnPrintOneRowValueGX(System.Drawing.Printing.PrintPageEventArgs e)
		{			
			//string [] strValueArr = p_strValueArr[p_intIndex];
			
			string strTemp;
			CharacterRange []rgnDSTArr = new CharacterRange[1];
			rgnDSTArr[0] = new CharacterRange(0,0);			
				
			RectangleF rtfText = new RectangleF(0,0,10000,100);

			StringFormat stfMeasure = new StringFormat(StringFormatFlags.LineLimit);			

			RectangleF rtfBounds;

			Region [] rgnDST;
			clsDSTRichTextBoxValue objclsDSTRichTextBoxValue;
								
			if(m_blnCheckPageChange(e)==true) //ÿ��ӡһ��֮ǰ��Ҫ����Ƿ�ҳ
			{
				return enmReturnValue.enmNeedNextPage;
				//��ҳ
					
			}

			     
			    
			 
			//����
			int m_intPosX=(int)enmRecordRectangleInfo.LeftX;//��ǰ��X����
			int m_intPosX1=0;
			int m_intPosY=(int)enmRecordRectangleInfo.TopY+((m_intCurrentPagePrintRow+2)*(int)enmRecordRectangleInfo.RowStep);
			int m_intPosY1=(int)enmRecordRectangleInfo.TopY+((m_intCurrentPagePrintRow+3)*(int)enmRecordRectangleInfo.RowStep);;
			int m_intXOff=1;
			int m_intYOff=15;

			int intTempColumn=4;
			strTemp=m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
			
			if(strTemp.Trim().Length != 0)
			{
				e.Graphics.DrawString(strTemp,m_fotSmallFont,Brushes.Black,m_intPosX, m_intPosY+15);
				strTempDate=strTemp;
			}
			//����ʱ��һ�е����ڲ���ʡȥ,ǰ��ֵ��Ϊ�գ�����ֵΪ�գ��Ҵ��ڵ�һ��
			if(strTempDate.Trim().Length != 0&&m_intCurrentPagePrintRow==0&&strTemp.Trim().Length == 0)
			{
				e.Graphics.DrawString(strTempDate,m_fotSmallFont,Brushes.Black,m_intPosX+1, m_intPosY+15);
				strTempDate=strTemp;
			}
			//ʱ��
			m_intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark1;//��ǰ��X����
			  
			intTempColumn=5;
			strTemp=m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
			if(strTemp.Trim().Length != 0)
			{
				e.Graphics.DrawString(strTemp,m_fotSmallFont,Brushes.Black,m_intPosX,m_intPosY+15);
			}
			//T
			m_intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark2;//��ǰ��X����
			m_intPosX1=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark3;//��ǰ��X����

			intTempColumn=6;
			strTemp=m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
			
			if(strTemp.Trim().Length != 0)
			{
				//m_mthDrawMultiString(m_fotSmallFont,m_intPosX,m_intPosY,m_intPosX1,m_intPosY1,m_intXOff,m_intYOff,strTemp,e);
				m_mthDrawMultiString(this.m_fotSmallFont,this.m_fotSmallFont ,6, m_intPosX,m_intPosY,m_intPosX1,m_intPosY1,m_intXOff,m_intYOff,strTemp,e) ;
			}
			
			//P
			m_intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark3;//��ǰ��X����
			m_intPosX1=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark4;//��ǰ��X����
			intTempColumn=7;
			strTemp=m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
			if(strTemp.Trim().Length != 0)
			{
				//m_mthDrawMultiString(m_fotSmallFont,m_intPosX,m_intPosY,m_intPosX1,m_intPosY1,m_intXOff,m_intYOff,strTemp,e);
				m_mthDrawMultiString(this.m_fotSmallFont,this.m_fotSmallFont ,6, m_intPosX,m_intPosY,m_intPosX1,m_intPosY1,m_intXOff,m_intYOff,strTemp,e) ;
			}
			
			m_bSummaryRow=false;

			//R
			m_intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark4;//��ǰ��X����
			m_intPosX1=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark5;//��ǰ��X����
			intTempColumn=8;
			strTemp=m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
			if(strTemp.Trim().Length != 0)
			{
				//m_mthDrawMultiString(m_fotSmallFont,m_intPosX,m_intPosY,m_intPosX1,m_intPosY1,m_intXOff,m_intYOff,strTemp,e);
				m_mthDrawMultiString(this.m_fotSmallFont,this.m_fotSmallFont ,6, m_intPosX,m_intPosY,m_intPosX1,m_intPosY1,m_intXOff,m_intYOff,strTemp,e) ;
				
			}

			//����
			m_intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark5;//��ǰ��X����
			m_intPosX1=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark6;//��ǰ��X����
			intTempColumn=9;
			strTemp=m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
			if(strTemp.Trim().Length != 0)
			{
				//m_mthDrawMultiString(m_fotSmallFont,m_intPosX,m_intPosY,m_intPosX1,m_intPosY1,m_intXOff,m_intYOff,strTemp,e);
				m_mthDrawMultiString(this.m_fotSmallFont,this.m_fotSmallFont ,6, m_intPosX,m_intPosY,m_intPosX1,m_intPosY1,m_intXOff,m_intYOff,strTemp,e) ;
				
			}
			//BP 1
			
			m_intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark6;//��ǰ��X����
			m_intPosX1=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7;//��ǰ��X����
			m_intXOff=10;
			intTempColumn=10;
			strTemp=m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
			strTemp=strTemp+"/";
			//BP 2
			
			intTempColumn=11;
			strTemp=strTemp+m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
            if(strTemp.Trim ()!="/")
            {
				//m_mthDrawMultiString(m_fotSmallFont,m_intPosX,m_intPosY,m_intPosX1,m_intPosY1,m_intXOff,m_intYOff,strTemp,e);
                m_mthDrawMultiString(this.m_fotSmallFont, this.m_fotTinyFont, 6, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, m_intXOff, m_intYOff, strTemp, e);
			}

            //�Զ���1
            m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7;//��ǰ��X����
            m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark8;//��ǰ��X����
            intTempColumn = 12;
            strTemp = m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
            if (strTemp.Trim().Length != 0)
            {
                //m_mthDrawMultiString(m_fotSmallFont,m_intPosX,m_intPosY,m_intPosX1,m_intPosY1,m_intXOff,m_intYOff,strTemp,e);
                m_mthDrawMultiString(this.m_fotSmallFont, this.m_fotSmallFont, 6, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, m_intXOff, m_intYOff, strTemp, e);
            }

            m_bSummaryRow = false;

            //�Զ���2
            m_intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark8;//��ǰ��X����
            m_intPosX1 = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9;//��ǰ��X����
            intTempColumn = 13;
            strTemp = m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
            if (strTemp.Trim().Length != 0)
            {
                //m_mthDrawMultiString(m_fotSmallFont,m_intPosX,m_intPosY,m_intPosX1,m_intPosY1,m_intXOff,m_intYOff,strTemp,e);
                m_mthDrawMultiString(this.m_fotSmallFont, this.m_fotSmallFont, 6, m_intPosX, m_intPosY, m_intPosX1, m_intPosY1, m_intXOff, m_intYOff, strTemp, e);
            }

            m_bSummaryRow = false;

			//ǩ��
			m_intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark9;//��ǰ��X����
			m_intPosX1=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark10;//��ǰ��X����
			intTempColumn=14;
			strTemp=m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
			if(strTemp.Trim().Length != 0)
			{
			   m_mthDrawMultiString(m_fotSmallFont,m_fotSmallFont,3,m_intPosX,m_intPosY,m_intPosX1,m_intPosY1,0,m_intYOff,strTemp,e);
			}
			//�����¼
			m_intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark10;//��ǰ��X����
			m_intPosX1=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.RightX ;//��ǰ��X����
			intTempColumn=15;
			strTemp=m_strConvertObjectValue(objDataArr[m_intCurrentContentRow][intTempColumn]);
			if(strTemp.Trim().Length != 0)
			{
			  e.Graphics.DrawString(strTemp,m_fotSmallFont,m_slbBrush,m_intPosX,m_intPosY+m_intYOff);
			}
			
	
			m_intCurrentContentRow++;
			m_intCurrentPagePrintRow++;
			
			
			#endregion

			return enmReturnValue.enmSuccessed;
		}
	
		
	
	
		
		private int m_intGetClass(DateTime dtmRecordDate)
		{
			string strDate = dtmRecordDate.Year.ToString()+dtmRecordDate.Month.ToString()+dtmRecordDate.Day.ToString();
			string strYesterday = dtmRecordDate.Year.ToString()+dtmRecordDate.Month.ToString()+dtmRecordDate.AddDays(-1).Day.ToString();
			DateTime dtClass= DateTime.Parse(dtmRecordDate.ToString("yyyy-MM-dd HH:mm:00"));
			DateTime dtDt0 = dtmRecordDate.Date;
			DateTime dt1=dtDt0.AddHours(2).AddMinutes(1);
			DateTime dt2=dtDt0.AddHours(8);
			DateTime dt3=dtDt0.AddHours(14).AddMinutes(31);
			DateTime dt4=dtDt0.AddHours(18).AddMinutes(1);
			DateTime dt5=dtDt0.AddHours(26).AddMinutes(1);

			if(dtmRecordDate >= dt1 && dtmRecordDate < dt2)
				return Convert.ToInt32(strDate + "0");
			else if(dtmRecordDate >= dt2 && dtmRecordDate < dt3)
				return Convert.ToInt32(strDate + "1");
			else if(dtmRecordDate >= dt3 && dtmRecordDate < dt4)
				return Convert.ToInt32(strDate + "2");
			else if(dtmRecordDate >= dt4 && dtmRecordDate < dt5)
				return Convert.ToInt32(strDate + "3");
			else if(dtmRecordDate < dt1)
				return Convert.ToInt32(strYesterday + "3");
			return 0;
		}
		
		private void m_mthSetOtherDetail(object[] objDetail, int intCurrentDetail,int intRowOfCurrentDetail,clsGeneralNurseRecordContent_GX objCurrent, out object[] objOtherDetail)
		{
			objOtherDetail = new object[18];
			string strText = ((string[])(objDetail[0]))[intRowOfCurrentDetail];
			//string strXml = ((string[])(objDetail[1]))[intRowOfCurrentDetail];;
			//clsDSTRichTextBoxValue objclsDSTRichTextBoxValue=new clsDSTRichTextBoxValue();
			//objclsDSTRichTextBoxValue.m_strText=strText;						
			//objclsDSTRichTextBoxValue.m_strDSTXml=strXml;
            objOtherDetail[15] = strText;
			objOtherDetail[16] = (DateTime)objDetail[3];
			if(objCurrent != null)
				objOtherDetail[17] = objCurrent.m_strCreateUserID+"��"+objDetail[6].ToString();
			else
				objOtherDetail[17] = " ��"+objDetail[6].ToString();
		}
		private bool m_blnIsSameClass(DateTime p_dtmMainRecord, DateTime p_dtmDetail)
		{
			if(m_intGetClass(p_dtmMainRecord) == m_intGetClass(p_dtmDetail))
				return true;
			else
				return false;
		}
        private object[][] m_objGetRecordsValueArr(clsTransDataInfo p_objTransDataInfo)
        {
            com.digitalwave.controls.ctlRichTextBox m_txtTemp = new com.digitalwave.controls.ctlRichTextBox();
            #region ��ʾ��¼��DataGrid
            try
            {
                object[] objData;
                ArrayList objReturnData = new ArrayList();
                ArrayList arlDetail = new ArrayList();//��Ų����¼
                int intCurrentDetail = 0;//��ǰ�����¼��ArrayList�е�����
                int intRecordCount = 0;
                bool blnIsSameClass = false;//�ж��Ƿ�Ϊͬһ���

                clsGeneralNurseRecordContent_GXDataInfo objGNRCInfo = new clsGeneralNurseRecordContent_GXDataInfo();
                clsDSTRichTextBoxValue objclsDSTRichTextBoxValue;
                string strText, strXml;

                objGNRCInfo = (clsGeneralNurseRecordContent_GXDataInfo)p_objTransDataInfo;

                if (objGNRCInfo.m_objRecordArr == null && objGNRCInfo.m_objDetailArr == null)
                    return null;

                #region �Բ����¼���д���
                if (objGNRCInfo.m_objDetailArr != null)
                {
                    string strDetail = "";
                    string strDetailXML = "";
                    for (int n = 0; n < objGNRCInfo.m_objDetailArr.Length; n++)
                    {
                        clsGeneralNurseRecordContent_GXDetail objDetail = objGNRCInfo.m_objDetailArr[n];
                        object[] objTemp = new object[7];
                        strDetail = objDetail.m_strRECORDCONTENT_RIGHT;
                        strDetailXML = objDetail.m_strRECORDCONTENTXML;
                        string[] strDetailArrTemp;
                        string[] strDetailXMLArrTemp;
                        //�������¼��Ϊ�С�
                        com.digitalwave.controls.ctlRichTextBox.m_mthSplitXmlByBytes(strDetail, strDetailXML, 31, out strDetailArrTemp, out strDetailXMLArrTemp);
                        string[] strDetailArr, strDetailXMLArr;
                        if (strDetail != string.Empty)
                        {
                            strDetailArr = new string[strDetailArrTemp.Length + 2];//���������ں�ǩ�������¼
                            strDetailXMLArr = new string[strDetailXMLArrTemp.Length + 2];//���������ں�ǩ�������¼XML

                            //�����ں�ǩ����ӽ������¼
                            strDetailArr[0] = objDetail.m_dtmRECORDDATE.ToString("yyyy-MM-dd HH:mm");
                            strDetailArr[1] = strDetailArrTemp[0];
                            for (int i = 2; i < strDetailArr.Length - 1; i++)
                            {
                                strDetailArr[i] = strDetailArrTemp[i - 1];
                            }
                            strDetailArr[strDetailArr.Length - 1] = "                   " + objDetail.m_strDetailCreateUserName;

                            strDetailXMLArr[0] = strDetailXMLArr[strDetailXMLArr.Length - 1] = "";
                            for (int i = 1; i < strDetailXMLArr.Length - 1; i++)
                            {
                                strDetailXMLArr[i] = strDetailXMLArrTemp[i - 1];
                            }

                            objTemp[0] = strDetailArr;
                            objTemp[1] = strDetailXMLArr;
                            objTemp[2] = strDetailArr.Length;
                            objTemp[3] = objDetail.m_dtmRECORDDATE;
                            objTemp[4] = objDetail.m_intClass;
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
                
                #endregion

                for (int i = 0; i < intRecordCount; i++)
                {
                    objData = new object[18];
                    clsGeneralNurseRecordContent_GX objCurrent = objGNRCInfo.m_objRecordArr[i];
                    clsGeneralNurseRecordContent_GX objNext = new clsGeneralNurseRecordContent_GX();//��һ�������¼
                    if (i < intRecordCount - 1)
                        objNext = objGNRCInfo.m_objRecordArr[i + 1];

                    //����û����¼���޸�ǰ�ļ�¼������ָ��ʱ�����޸ĵģ��޸����봴����Ϊͬһ�ˣ�����ʾ
                   // if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate && objNext.m_strModifyUserID.Trim() == objCurrent.m_strCreateUserID.Trim())
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate)
                    {
                     //   TimeSpan tsModify = objNext.m_dtmModifyDate - objCurrent.m_dtmModifyDate;
                     //   if ((int)tsModify.TotalHours < intCanModifyTime)
                            continue;
                    }

                    #region ��Źؼ��ֶ�
                    if (objCurrent.m_dtmCreateDate != DateTime.MinValue)
                    {
                        objData[0] = objCurrent.m_dtmRECORDDATE;//��ż�¼ʱ����ַ���
                        objData[1] = (int)enmRecordsType.GeneralNurseRecord_GXRec;//��ż�¼���͵�intֵ
                        objData[2] = objCurrent.m_dtmOpenDate;//��ż�¼��OpenDate�ַ���
                        objData[3] = objCurrent.m_dtmModifyDate;//��ż�¼��ModifyDate�ַ���   

                        //ͬһ����ֻ�ڵ�һ����ʾ����
                        if (objCurrent.m_dtmRECORDDATE.Date.ToString() != m_dtmPreRecordDate.Date.ToString())
                        {
                            objData[4] = objCurrent.m_dtmRECORDDATE.Date.ToString("yyyy-MM-dd");//�����ַ���
                        }
                        //�޸ĺ���кۼ��ļ�¼������ʾʱ��
                        if (m_dtmPreRecordDate != objCurrent.m_dtmRECORDDATE)
                            objData[5] = objCurrent.m_dtmRECORDDATE.ToString("HH:mm");//ʱ���ַ���

                    }
                    m_dtmPreRecordDate = objCurrent.m_dtmRECORDDATE;
                    #endregion ;

                    #region ��ŵ�����Ϣ
                    //����
                    objData[6] = objCurrent.m_strTEMPERATURE_RIGHT;//T
                    //����
                    objData[7] = objCurrent.m_strPULSE_RIGHT;//HR

                    //����
                    objData[8] = objCurrent.m_strRESPIRATION_RIGHT;//P

                    //����
                    objData[9] = objCurrent.m_strHEARTRATE_RIGHT;//HR


                    //ѪѹA
                    objData[10] = objCurrent.m_strBLOODPRESSURES_RIGHT;//��������ݿ�ʱ�ѽ�A��S�Ե�

                    //ѪѹS
                    objData[11] = objCurrent.m_strBLOODPRESSUREA_RIGHT;//

                    //�Զ�����1
                    objData[12] = objCurrent.m_strCUSTOM1_RIGHT;//

                    //�Զ�����2
                    objData[13] = objCurrent.m_strCUSTOM2_RIGHT;//

                    //ǩ��
                    objData[14] = objCurrent.m_strContentCreateUserName;

                    //�����¼
                    if (arlDetail != null && intCurrentDetail < arlDetail.Count && arlDetail.Count > 0)
                    {
                        int intClass = (int)(((object[])(arlDetail[intCurrentDetail]))[4]);
                        DateTime dtDetailRecordTime = (DateTime)(((object[])(arlDetail[intCurrentDetail]))[3]);
                        //��Ϊ�ɼ�¼δ�б�������Ϣ�����½����ж�
                        if (objCurrent.m_intClass == 0 || objCurrent.m_intClass == 1 || intClass == 0)
                            blnIsSameClass = m_blnIsSameClass(objCurrent.m_dtmRECORDDATE, dtDetailRecordTime);
                        else
                            blnIsSameClass = objCurrent.m_intClass == intClass ? true : false;
                        //�����һ��Σ�ֱ�����
                        if (blnIsSameClass)
                        {
                            strText = ((string[])(((object[])arlDetail[intCurrentDetail])[0]))[intRowOfCurrentDetail];
                            //strXml = ((string[])(((object[])arlDetail[intCurrentDetail])[1]))[intRowOfCurrentDetail]; ;
                           // objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                            //objclsDSTRichTextBoxValue.m_strText = strText;
                           // objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                            objData[15] = strText;
                            objData[16] = (DateTime)(((object[])arlDetail[intCurrentDetail])[3]);
                            objData[17] = objCurrent.m_strCreateUserID + "��" + (((object[])arlDetail[intCurrentDetail])[3]).ToString();

                            if (intRowOfCurrentDetail == ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length - 1)
                            {
                                intRowOfCurrentDetail = 0;
                                intCurrentDetail++;
                            }
                            else
                                intRowOfCurrentDetail++;
                            objReturnData.Add(objData);
                        }
                        else if (objNext != null)
                        {
                            while (arlDetail != null &&
                                intCurrentDetail < arlDetail.Count &&
                                intRowOfCurrentDetail < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length &&
                                (DateTime)(((object[])arlDetail[intCurrentDetail])[3]) <= objCurrent.m_dtmRECORDDATE)
                            {
                                //��Ϊ�ɼ�¼δ�б�������Ϣ�����½����ж�
                                if (objNext.m_intClass == 0 || objNext.m_intClass == 1 || (int)(((object[])arlDetail[intCurrentDetail])[4]) == 0)
                                    blnIsSameClass = m_blnIsSameClass(objCurrent.m_dtmRECORDDATE, (DateTime)(((object[])arlDetail[intCurrentDetail])[3]));
                                else
                                    blnIsSameClass = objNext.m_intClass == (int)(((object[])arlDetail[intCurrentDetail])[4]) ? true : false;

                                if (!blnIsSameClass)
                                {
                                    for (int m = intRowOfCurrentDetail; m < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length; m++)
                                    {
                                        object[] objOtherDetail = new object[18];
                                        m_mthSetOtherDetail(((object[])arlDetail[intCurrentDetail]), intCurrentDetail, m, objCurrent, out objOtherDetail);
                                        objReturnData.Add(objOtherDetail);
                                    }

                                    intCurrentDetail++;
                                    intRowOfCurrentDetail = 0;
                                }
                                else
                                    break;
                            }
                        }
                    }
                    if (objData != null && objData[15] == null)
                        objReturnData.Add(objData);
                    #endregion
                }

                //��������¼δ��ʾ������������¼��ȫ����ʾ�꣬��������ʣ�ಿ��
                while (arlDetail != null && intCurrentDetail < arlDetail.Count && arlDetail.Count > 0)
                {
                    if (intRowOfCurrentDetail < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length)
                    {
                        for (int m = intRowOfCurrentDetail; m < ((string[])(((object[])arlDetail[intCurrentDetail])[0])).Length; m++)
                        {
                            object[] objOtherDetail = new object[18];
                            m_mthSetOtherDetail(((object[])arlDetail[intCurrentDetail]), intCurrentDetail, m, null, out objOtherDetail);
                            objReturnData.Add(objOtherDetail);
                        }

                        intCurrentDetail++;
                        intRowOfCurrentDetail = 0;
                    }
                }
                object[][] m_objRe = new object[objReturnData.Count][];

                for (int m = 0; m < objReturnData.Count; m++)
                    m_objRe[m] = (object[])objReturnData[m];
                return m_objRe;
            }
            catch (Exception ex)
            {
              
                return null;
            }
            #endregion
        }

	}

		 


}
#endregion