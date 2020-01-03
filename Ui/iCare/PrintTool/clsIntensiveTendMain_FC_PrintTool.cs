using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.Utility.Controls;
using System.Drawing;

namespace iCare
{
	/// <summary>
	/// Σ�ػ��߻����¼��ӡ������(�°�)ժҪ˵����
	/// </summary>
	public class clsIntensiveTendMain_FC_PrintTool: infPrintRecord
	{
		private bool m_blnIsFromDataSource=true;               //�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
		private bool m_blnWantInit=true;	                   //�Ƿ���Ҫ��ʼ��	
		private clsRecordsDomain m_objRecordsDomain;           //��¼��
		private clsPrintInfo_IntensiveTend m_objPrintInfo;     //��ӡ����
		private string strCurrentClass;                        //��ǰ���Ĭ��Ϊ��
		private int intCaseRowCount;                           //��ǰ���̼�¼���������
		private string[] strCurrentCaseTextArr;                //��ǰ���̼�¼��������
		private string[] strCurrentCaseXmlArr;                 //��ǰ���̼�¼�ۼ�����
		private string[] strCurrentCaseCreateDateArr;          //��ǰ���̼�¼����ʱ��
        private string strPerRecordDate = "";                  //��һ����¼�ļ�¼���ڡ����ǰ��������¼������ͬ�����һ����¼����ӡ����

		public clsIntensiveTendMain_FC_PrintTool()
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
		public void m_mthSetPrintInfo(clsPatient p_objPatient,DateTime p_dtmInPatientDate,DateTime p_dtmOpenDate)
		{		
			m_blnIsFromDataSource=true;//�����Ǵ����ݿ��ȡ
			clsPatient m_objPatient=p_objPatient;
			m_objPrintInfo=new clsPrintInfo_IntensiveTend();
			m_objPrintInfo.m_strInPatentID=m_objPatient!=null? m_objPatient.m_StrInPatientID:"";					
			m_objPrintInfo.m_strPatientName=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrFirstName :"";
			m_objPrintInfo.m_strSex=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrSex:"";
			m_objPrintInfo.m_strAge=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrAge : "";
			m_objPrintInfo.m_strBedName=m_objPatient!=null? m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName :"";
			m_objPrintInfo.m_strDeptName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjDept.m_StrDeptName :"";
			m_objPrintInfo.m_strAreaName=m_objPatient!=null? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName:"";			
			m_objPrintInfo.m_dtmInPatientDate=p_dtmInPatientDate;
			m_objPrintInfo.m_dtmOpenDate=p_dtmOpenDate;
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
            m_objRecordsDomain = new clsRecordsDomain(enmRecordsType.IntensiveTend);
				
			long lngRes = m_objRecordsDomain.m_lngGetPrintInfo(m_objPrintInfo.m_strInPatentID,m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"),out m_objPrintInfo.m_objTransDataArr,out m_objPrintInfo.m_dtmFirstPrintDateArr,out m_objPrintInfo.m_blnIsFirstPrintArr);
			if(lngRes <= 0)
				return ;   

			//����¼ʱ��(CreateDate)���� 
//			m_mthSortTransData(ref m_objPrintInfo.m_objTransDataArr);
			//���ñ����ݵ���ӡ��
			m_mthSetPrintContent(m_objPrintInfo.m_objTransDataArr,m_objPrintInfo.m_dtmFirstPrintDateArr);			
			m_objPrintInfo.m_objPrintDataArr=m_objPrintDataArr;
			m_blnWantInit=false;
		}

		/// <summary>
		/// ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
		/// </summary>
		/// <param name="p_objPrintContent">��ӡ����</param>
		public void m_mthSetPrintContent(object p_objPrintContent)
		{
			if(p_objPrintContent.GetType().Name !="clsPrintInfo_IntensiveTend")
			{
				clsPublicFunction.ShowInformationMessageBox("��������");
			}
			m_blnIsFromDataSource=false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ
			m_objPrintInfo=(clsPrintInfo_IntensiveTend)p_objPrintContent;
			m_objPrintDataArr= m_objPrintInfo. m_objPrintDataArr ;		
			
			m_blnWantInit=false;
		}

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
			if(m_objPrintInfo.m_objPrintDataArr == null || m_objPrintInfo.m_objPrintDataArr.Length == 0)
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
			m_fotHeaderFont = new Font("SimSun", 15f);
			m_fotSmallFont = new Font("SimSun",12f);
			m_fotTinyFont=new Font("SimSun",9f);

			m_GridPen = new Pen(Color.Black,1);
			m_slbBrush = new SolidBrush(Color.Black);
		
			m_objPageSetting = new clsPrintPageSettingForRecord();

            m_objPrintContext = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, m_fotSmallFont);			
		
			m_objPrintLenth=new clsPrintLenth_IntensiveTendRecord();
			m_objPrintLenth.m_intPrintLenth_BloodPressure = (int) ((float)(enmRecordRectangleInfo.ColumnsMark6-enmRecordRectangleInfo.ColumnsMark5)/2/8.75)+1;//Ѫѹ���ȷ�һ�룬�����ĸ
			m_objPrintLenth.m_intPrintLenth_Breath = (int) ((float)(enmRecordRectangleInfo.ColumnsMark5-enmRecordRectangleInfo.ColumnsMark4)/8.75)+1;
			m_objPrintLenth.m_intPrintLenth_Echo = (int) ((float)(enmRecordRectangleInfo.ColumnsMark9-enmRecordRectangleInfo.ColumnsMark8)/8.75)+1;		
			m_objPrintLenth.m_intPrintLenth_In = (int) ((float)(enmRecordRectangleInfo.ColumnsMark12-enmRecordRectangleInfo.ColumnsMark11)/8.75)+1;		
			m_objPrintLenth.m_intPrintLenth_Out = (int) ((float)(enmRecordRectangleInfo.ColumnsMark14-enmRecordRectangleInfo.ColumnsMark13)/8.75)+1;
			m_objPrintLenth.m_intPrintLenth_Pulse = (int) ((float)(enmRecordRectangleInfo.ColumnsMark4-enmRecordRectangleInfo.ColumnsMark3)/8.75)+1;
			m_objPrintLenth.m_intPrintLenth_Pupil = (int) ((float)(enmRecordRectangleInfo.ColumnsMark7-enmRecordRectangleInfo.ColumnsMark6)/8.75)+1;
			m_objPrintLenth.m_intPrintLenth_RecordContent = (int) ((float)(enmRecordRectangleInfo.ColumnsMark15-enmRecordRectangleInfo.ColumnsMark14-6)/17.5)+1;//���̼�¼����人��
			m_objPrintLenth.m_intPrintLenth_Temperature = (int) ((float)(enmRecordRectangleInfo.ColumnsMark3-enmRecordRectangleInfo.ColumnsMark2)/8.75)+1;
					
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
		
		public void m_mthPrintPage()
		{
			frmPrintPreviewDialogPF frmPreview = new frmPrintPreviewDialogPF();
			frmPreview.m_evtBeginPrint +=new PrintEventHandler(frmPreview_m_evtBeginPrint);
			frmPreview.m_evtEndPrint +=new PrintEventHandler(frmPreview_m_evtEndPrint);
			frmPreview.m_evtPrintContent +=new PrintPageEventHandler(frmPreview_m_evtPrintContent);
			frmPreview.m_evtPrintFrame +=new PrintPageEventHandler(frmPreview_m_evtPrintFrame);
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
						//���¼�¼��ֻ��ʹ���µ��״δ�ӡʱ����Ϊ��Ч�����������
						//��ż�¼����
						arlRecordType.Add(m_objPrintInfo.m_objTransDataArr[i].m_intFlag);
						//��ż�¼��OpenDate
						arlOpenDate.Add(m_objPrintInfo.m_objTransDataArr[i].m_objRecordContent.m_dtmOpenDate);			
						intUpdateIndex = i;
					}
				}   

				if(intUpdateIndex >= 0)
				{
					m_objRecordsDomain.m_lngUpdateFirstPrintDate(m_objPrintInfo.m_strInPatentID,m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"),(int[])arlRecordType.ToArray(typeof(int)),(DateTime[])arlOpenDate.ToArray(typeof(DateTime)),m_objPrintInfo.m_dtmFirstPrintDateArr[intUpdateIndex]);
				}
				#region �����Ԥ������Ӧ��ִ�У�����Ԥ�����ӡ�������Ϊû�����³�ʼ��
				m_objPrintInfo.m_objTransDataArr = null;
				m_objPrintInfo.m_blnIsFirstPrintArr = null;
				#endregion
			}                          
			m_mthEndPrintSub((PrintEventArgs)p_objPrintArg);
		}	
	
		private void m_mthSetPrintContent(clsTransDataInfo[] p_objTransDataArr,
			DateTime[] p_dtmFirstPrintDate)
		{
			try
			{
				if(p_objTransDataArr==null || p_dtmFirstPrintDate==null ||p_objTransDataArr.Length !=p_dtmFirstPrintDate.Length)
				{
					clsPublicFunction.ShowInformationMessageBox("��ӡ��������!");
					return;
				}			

				//���ݲ�ͬ�ı����ͣ���ȡ��Ӧ��clsDiseaseTrackInfo
				clsDiseaseTrackInfo objTrackInfo=null;
				m_objPrintDataArr = new clsIntensiveTendDataInfo[p_objTransDataArr.Length];		
				//				m_objPrintDataArr=(clsIntensiveTendDataInfo[])(p_objTransDataArr.Clone());
				ArrayList arlTemp = new ArrayList();
				arlTemp.AddRange(p_objTransDataArr);
				m_objPrintDataArr = (clsIntensiveTendDataInfo[])arlTemp.ToArray(typeof(clsIntensiveTendDataInfo));
				
				//System.Data.DataTable dtbBlankRecord = null;
				//new clsDiseaseTrackAddBlankDomain().m_lngGetBlankRecordContent(m_objPrintInfo.m_strInPatentID,m_objPrintInfo.m_dtmInPatientDate,out dtbBlankRecord);

			}
			catch(Exception ex)
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
			blnBeginPrintNewRecord=true;
			m_intRowNumberForPrintData = 0;
			m_intPosY = (int)enmRecordRectangleInfo.TopY+130;
		}
		
		#endregion ;

		#region �йش�ӡ������
		/// <summary>
		/// ���д�ӡ������
		/// </summary>
		private clsIntensiveTendDataInfo[] m_objPrintDataArr;

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
		private int intNowPage=1;
		/// <summary>
		/// ��ǰ��ӡ�ļ�¼����ţ��ڼ�����¼��
		/// </summary>
		private int m_intCurrentRecord=0;
        /// <summary>
        /// ��ǰ��ӡ��¼���кţ��ڼ�����¼�ĵڼ��У�
        /// </summary>
        private int m_intCurrentRecordRowIndex = 0;
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
			TopY = 170,
			///<summary>
			/// ���ӵ����
			/// </summary>
			LeftX = 40,
			/// <summary>
			/// ���ӵ��Ҷ�
			/// </summary>
			RightX = 820-32,
			/// <summary>
			/// ����ÿ�еĲ���
			/// </summary>
			RowStep = 40,
			/// <summary>
			/// ���ӵ�����
			/// </summary>
			RowLinesNum = 21,			
			/// <summary>
			/// �����ڸ�������Ը��Ӷ��˵Ĵ�ֱƫ��
			/// </summary>
			VOffSet = 22,
			/// <summary>
			/// �е���Ŀ
			/// </summary>
			ColumnsNum=17,
			/// <summary>
			/// ��һ�������(X)
			/// </summary>
			ColumnsMark1=70,
			/// <summary>
			/// �ڶ��������(X)
			/// </summary>
			ColumnsMark2=105,
			ColumnsMark3=145,
			ColumnsMark4=170,
			ColumnsMark5=200,
			ColumnsMark6=230,
			ColumnsMark7=290,
			ColumnsMark8=315,
			ColumnsMark9=340,
			ColumnsMark10=365,
			ColumnsMark11=390,
			ColumnsMark12=420,
			ColumnsMark13=445,
			ColumnsMark14=470,
			ColumnsMark15=495,
            ColumnsMark16 = 520,
			ColumnsMark17=755				
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
						m_fReturnPoint = new PointF(320f,60f);
						break;
					case (int)enmItemDefination.Page_Name_Title:
						m_fReturnPoint = new PointF(225f,100f);
						break;
					case (int)enmItemDefination.Name_Title :
						m_fReturnPoint = new PointF(20f,150f);
						break;
					case (int)enmItemDefination.Name:
						m_fReturnPoint = new PointF(70f,150f);
						break;

					case (int)enmItemDefination.Sex_Title :
						m_fReturnPoint = new PointF(150f,150f);
						break;
					case (int)enmItemDefination.Sex :
						m_fReturnPoint = new PointF(200f,150f);
						break;

					case (int)enmItemDefination.Age_Title :
						m_fReturnPoint = new PointF(250f,150f);
						break;
					case (int)enmItemDefination.Age:
						m_fReturnPoint = new PointF(300f,150f);
						break;

					case (int)enmItemDefination.Dept_Name_Title:
						m_fReturnPoint = new PointF(350f,150f);
						break;
					case (int)enmItemDefination.Dept_Name :
						m_fReturnPoint = new PointF(400f,150f);
						break;
					
					case (int)enmItemDefination.BedNo_Title :
						m_fReturnPoint = new PointF(550f,150f);
						break;
					case (int)enmItemDefination.BedNo:
						m_fReturnPoint = new PointF(600f,150f);
						break;

					case (int)enmItemDefination.InPatientID_Title:
						m_fReturnPoint = new PointF(650f,150f);
						break;
					case (int)enmItemDefination.InPatientID :
						m_fReturnPoint = new PointF(720f,150f);
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
			
            
			e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,m_fotSmallFont ,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_HospitalName  ));
		
			e.Graphics.DrawString("Σ �� �� �� �� �� �� ¼",m_fotTitleFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_Name_Title ));
			

			e.Graphics.DrawString("������",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name_Title  ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strPatientName  ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name ));
		
			e.Graphics.DrawString("�Ա�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strSex ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex  ));

			e.Graphics.DrawString("���䣺",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strAge ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age ));

			e.Graphics.DrawString("������",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strAreaName ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name ));

			e.Graphics.DrawString("���ţ�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strBedNo ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo ));	
			
			e.Graphics.DrawString("סԺ�ţ�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strInPatientID ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID ));	
			
			//�����һ�������ӡ˵������
			e.Graphics.DrawString("����������:U--�� S--��� V--Ż���� E--����Һ D--��ʳ I--��Һ",m_fotSmallFont,m_slbBrush,
				(int)enmRecordRectangleInfo.LeftX +(int)enmRecordRectangleInfo.ColumnsMark2,
				(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowStep)*((int)enmRecordRectangleInfo.RowLinesNum+1)-20);

		}
		#endregion
		
		#region ���������Ŀ
		/// <summary>
		/// ���������Ŀ
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintHeaderInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{
			
			e.Graphics.DrawString("����",m_fotHeaderFont ,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+15,
				(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+5);
		     
			e.Graphics.DrawString("ʱ",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark1+4, (int)enmRecordRectangleInfo.TopY + 2);
            e.Graphics.DrawString("��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark1+4, (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep );

			//���� C			
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark2+1, (int)enmRecordRectangleInfo.TopY+2);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark2+1, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark2+1, (int)enmRecordRectangleInfo.TopY-10+2*(int)enmRecordRectangleInfo.RowStep);
			e.Graphics.DrawString("C",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark2+9, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep+1);

			//����(��/��)
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark3+1, (int)enmRecordRectangleInfo.TopY+2);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark3+1, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep-13);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark3+1, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+10);
			e.Graphics.DrawString("/",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark3+5, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep-9);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark3+1, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep+11);

			//����(��/��)
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark4+1, (int)enmRecordRectangleInfo.TopY+2);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark4+1, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep-13);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark4+1, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+10);
			e.Graphics.DrawString("/",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark4+5, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep-9);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark4+1, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep+11);

			//��־
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark5+1, (int)enmRecordRectangleInfo.TopY+2);
			e.Graphics.DrawString("־",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark5+1, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep);

			//Ѫѹ(mmHg)
			e.Graphics.DrawString("Ѫ",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark6+12, (int)enmRecordRectangleInfo.TopY+2);
            e.Graphics.DrawString("ѹ", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark6+12, (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep);
			e.Graphics.DrawString("mmHg",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark6+8, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep);

			e.Graphics.DrawString(" ͫ ��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark7+15, (int)enmRecordRectangleInfo.TopY+5);
			e.Graphics.DrawString("��С",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark7+5, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+5-10);
			e.Graphics.DrawString("(mm)",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark7+5, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+5+10);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark7, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep+5);

			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark8, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep+5);


			e.Graphics.DrawString("����",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark9+3, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+5);

			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark9, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep+5);

			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark10, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep+5);
            Font m_fotTemp = new Font("Simsun", 10f);
            e.Graphics.DrawString(" Ѫ", m_fotTemp, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark11, (int)enmRecordRectangleInfo.TopY + 2);
            e.Graphics.DrawString(" ��", m_fotTemp, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark11, (int)enmRecordRectangleInfo.TopY + 20);
            e.Graphics.DrawString(" ��", m_fotTemp, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark11, (int)enmRecordRectangleInfo.TopY + 40);
            e.Graphics.DrawString(" ��", m_fotTemp, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark11, (int)enmRecordRectangleInfo.TopY + 60);
            e.Graphics.DrawString(" ��", m_fotTemp, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark11, (int)enmRecordRectangleInfo.TopY + 80);
            e.Graphics.DrawString(" %", m_fotTemp, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
                (int)enmRecordRectangleInfo.ColumnsMark11, (int)enmRecordRectangleInfo.TopY + 100);



			e.Graphics.DrawString("����",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark12+3, (int)enmRecordRectangleInfo.TopY+5);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark12+1, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+5);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark12+1, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep+5);

			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark13+1, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+5);
			e.Graphics.DrawString("ml",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark13+1, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep+5);


			e.Graphics.DrawString("�ų�",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark14+1, (int)enmRecordRectangleInfo.TopY+5);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark14+1, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+5);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark14+1, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep+5);

			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark15+1, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+5);
			e.Graphics.DrawString("ml",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark15+1, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep+5);

            //e.Graphics.DrawString("ǩ��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
            //    (int)enmRecordRectangleInfo.ColumnsMark16 + 1, (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep + 5);


			e.Graphics.DrawString(" ���顢�����ʩ��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark16+20, (int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+5);
            e.Graphics.DrawString("  Ч����ǩ��", m_fotHeaderFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX +
				(int)enmRecordRectangleInfo.ColumnsMark16+30, (int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep+5);

//			e.Graphics.DrawString("ǩ��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
//				(int)enmRecordRectangleInfo.ColumnsMark16+1,(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep+5);
		
		
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
			for(int i1=0;i1<=(int)enmRecordRectangleInfo.RowLinesNum ;i1++)
			{
				if(i1 !=1 && i1 !=2)
					e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX ,
						(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowStep)*i1,
						(int)enmRecordRectangleInfo.RightX,
						(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowStep)*i1);
                else if (i1 == 1)
                {
                    e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7,
                        (int)enmRecordRectangleInfo.TopY + ((int)enmRecordRectangleInfo.RowStep) * i1 - 8,
                        (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11,
                        (int)enmRecordRectangleInfo.TopY + ((int)enmRecordRectangleInfo.RowStep) * i1 - 8);

                    e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12,
                        (int)enmRecordRectangleInfo.TopY + ((int)enmRecordRectangleInfo.RowStep) * i1 - 8,
                        (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16,
                        (int)enmRecordRectangleInfo.TopY + ((int)enmRecordRectangleInfo.RowStep) * i1 - 8);
                }
                else //if(i1==2)
                    e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7,
                        (int)enmRecordRectangleInfo.TopY + ((int)enmRecordRectangleInfo.RowStep) * i1,
                        (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11,
                        (int)enmRecordRectangleInfo.TopY + ((int)enmRecordRectangleInfo.RowStep) * i1);
			}
			
			#region ����������
			int intHeight=((int)enmRecordRectangleInfo.RowLinesNum)*(int)enmRecordRectangleInfo.RowStep;
			//���������
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX,(int)enmRecordRectangleInfo.TopY+intHeight);
			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark1,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark1,(int)enmRecordRectangleInfo.TopY+intHeight);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark2,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark2,(int)enmRecordRectangleInfo.TopY+intHeight);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark3,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark3,(int)enmRecordRectangleInfo.TopY+intHeight);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark4,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark4,(int)enmRecordRectangleInfo.TopY+intHeight);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark5,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark5,(int)enmRecordRectangleInfo.TopY+intHeight);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark6,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark6,(int)enmRecordRectangleInfo.TopY+intHeight);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7,(int)enmRecordRectangleInfo.TopY+intHeight);

			//ͫ�״�С���ҷֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8,(int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8,(int)enmRecordRectangleInfo.TopY+intHeight);
			//ͫ�״�С�뷴��ֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark9,(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep-8,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark9,(int)enmRecordRectangleInfo.TopY+intHeight);
			//ͫ�׷������ҷֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark10,(int)enmRecordRectangleInfo.TopY+2*(int)enmRecordRectangleInfo.RowStep,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark10,(int)enmRecordRectangleInfo.TopY+intHeight);			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark11,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark11,(int)enmRecordRectangleInfo.TopY+intHeight);
            //Ѫ�����Ͷ�
            e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12, (int)enmRecordRectangleInfo.TopY,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12, (int)enmRecordRectangleInfo.TopY + intHeight);
			//�����м�ֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark13,(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep-8,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark13,(int)enmRecordRectangleInfo.TopY+intHeight);			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark14,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark14,(int)enmRecordRectangleInfo.TopY+intHeight);
			//�ų��м�ֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15,(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep-8,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15,(int)enmRecordRectangleInfo.TopY+intHeight);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark16,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark16,(int)enmRecordRectangleInfo.TopY+intHeight);
            //ǩ��
            //e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark17, (int)enmRecordRectangleInfo.TopY,
            //    (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark17, (int)enmRecordRectangleInfo.TopY + intHeight);						
			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.RightX ,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.RightX,(int)enmRecordRectangleInfo.TopY+intHeight);
			#endregion

			//���Խ��ߣ�Ѫѹ��
			for(int i1=3;i1<(int)enmRecordRectangleInfo.RowLinesNum ;i1++)//б��ֻ��Ҫ�ӵ����п�ʼ�������ڶ���
			{
				e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX +(int)enmRecordRectangleInfo.ColumnsMark7,
					(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowStep)*i1,
					(int)enmRecordRectangleInfo.LeftX +(int)enmRecordRectangleInfo.ColumnsMark6,
					(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowStep)*(i1+1));					
			}	
			
		}

						
		#endregion

		#region ��ӡ����ʵ��
				#region ���õ�ǰҪ��ӡ��һ����¼����
		/// <summary>
		/// ���õ�ǰҪ��ӡ��һ����¼����
		/// </summary>
		/// <param name="e"></param>
		/// <param name="p_intBottomY"></param>
		/// <returns></returns>
		private int m_intSetPrintOneValueRows(PrintPageEventArgs e,ref int p_intBottomY)
		{			
			if(m_objPrintDataArr==null || m_intCurrentRecord>= m_objPrintDataArr.Length)
				return 0;
	
			int intRowsOfOneRecord;
			string strModifyDate;
		
			try
			{
				#region ������¼�¼���ж��Ƿ����ۼ�
				int intLenth;
				if(m_blnBeginPrintNewRecord==true) 
				{									
					#region ��ǰ��¼���鸳ֵ
					if(m_objPrintDataArr[m_intCurrentRecord].m_intFlag == 0)
					{
						#region "ͳ����Ϣ"
						m_strValueArr=new string[1][];
						m_strValueArr[0]=new string[18];
						m_strValueArr[0][0]="";
						m_strValueArr[0][1]="";
						m_strValueArr[0][2]="";
						m_strValueArr[0][3]="";
						m_strValueArr[0][4]="";
						m_strValueArr[0][5]="";
                        m_strValueArr[0][6] = "";
                        m_strValueArr[0][7] = "  24";
                        m_strValueArr[0][8] = "Сʱ";
                        m_strValueArr[0][9] = "�ܼ�:";
                        m_strValueArr[0][10] = "����";
                        m_strValueArr[0][11] = (m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intTotal_In == 0 ? "" : m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intTotal_In.ToString());
                        m_strValueArr[0][12] = "�ų�";
                        m_strValueArr[0][13] = (m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intTotal_Out == 0 ? "" : m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intTotal_Out.ToString());
                        m_strValueArr[0][14] = "";
                        m_strValueArr[0][15] = "";
                        m_strValueArr[0][16] = "";
                        m_strValueArr[0][17] = "";
						return 1;
						#endregion
					}
					else
					{
						#region "������¼"
							intRowsOfOneRecord=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr.Length;

							strModifyDate=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[intRowsOfOneRecord-1].
								m_dtmModifyDate.ToString("yyyy-MM-dd HH:mm:ss");
							intLenth=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr.Length;

							//��ʱ�洢����
							ArrayList m_RecordInfo=new ArrayList();
							int m_intAllRecords=0;
                            for (int k1 = intLenth-1; k1 < intLenth; k1++)
							{
								clsIntensiveTendRecordContent1 m_objCurrent=
									m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1];

								//�������������ֱ���������룬�ų������̼�¼�ļ�¼��
								int intIn_Count=0;
								int intOut_Count=0;
								int intContent_Count=0;
								int intMaxCount=0;
						
								ArrayList objIn=new ArrayList();
								ArrayList objOut=new ArrayList();

								#region ��õ�ǰ��¼���������
								//��ʳ
								string[] strInfo0=new String[3];
								if(m_objCurrent.m_intInD!=0)
								{
									strInfo0[0]="D";
									strInfo0[1]=(m_objCurrent.m_intInD == 0?"":m_objCurrent.m_intInD.ToString()) ;
									strInfo0[2]=m_objCurrent.m_strInDXML;

									objIn.Add(strInfo0);

									intIn_Count++;
								}
					 
								//��Һ
								string[] strInfo1=new String[3];
								if(m_objCurrent.m_intInI!=0)
								{
									strInfo1[0]="I";
									strInfo1[1]=(m_objCurrent.m_intInI == 0?"":m_objCurrent.m_intInI.ToString());
									strInfo1[2]=m_objCurrent.m_strInIXML;

									objIn.Add(strInfo1);

									intIn_Count++;
								}

								//����Һ
								string[] strInfo2=new String[3];
								if(m_objCurrent.m_intOutE!=0)
								{
									strInfo2[0]="E";
									strInfo2[1]=(m_objCurrent.m_intOutE == 0?"":m_objCurrent.m_intOutE.ToString()) ;
									strInfo2[2]=m_objCurrent.m_strOutEXML;

									objOut.Add(strInfo2);

									intOut_Count++;
								}
				
								//���
								string[] strInfo3=new String[3];
								if(m_objCurrent.m_intOutS!=0)
								{
									strInfo3[0]="S";
									strInfo3[1]=(m_objCurrent.m_intOutS == 0?"":m_objCurrent.m_intOutS.ToString()) ;
									strInfo3[2]=m_objCurrent.m_strOutSXML;

									objOut.Add(strInfo3);

									intOut_Count++;
								}

								//��
								string[] strInfo4=new String[3];
								if(m_objCurrent.m_intOutU!=0)
								{
									strInfo4[0]="U";
									strInfo4[1]=(m_objCurrent.m_intOutU == 0?"":m_objCurrent.m_intOutU.ToString()) ;
									strInfo4[2]=m_objCurrent.m_strOutUXML;

									objOut.Add(strInfo4);

									intOut_Count++;
								}

								//Ż����
								string[] strInfo5=new String[3];
								if(m_objCurrent.m_intOutV!=0)
								{
									strInfo5[0]="V";
									strInfo5[1]=(m_objCurrent.m_intOutV == 0?"":m_objCurrent.m_intOutV.ToString());
									strInfo5[2]=m_objCurrent.m_strOutVXML;

									objOut.Add(strInfo5);

									intOut_Count++;
                                }
                                #region ���̼�¼���ݴ���--wf20080117
                                //���̼�¼���ݴ���
                                ArrayList arlRecordContent = new ArrayList();
                                ArrayList arlRecordContentXML = new ArrayList();
                                for (int k = 0; k < m_objPrintDataArr[m_intCurrentRecord].m_objCourseDiseasesRecordArr.Length; k++)
                                {
                                    if (m_objPrintDataArr[m_intCurrentRecord].m_objCourseDiseasesRecordArr[k].m_strDiseasesRecordContent.Trim().Length != 0)
                                    {
                                        arlRecordContent.Add(m_objPrintDataArr[m_intCurrentRecord].m_objCourseDiseasesRecordArr[k].m_dtmCreateDate.ToString("yyyy-MM-dd HH:mm:ss"));
                                        arlRecordContentXML.Add("<root />");
                                        string strCase = m_objPrintDataArr[m_intCurrentRecord].m_objCourseDiseasesRecordArr[k].m_strDiseasesRecordContent;
                                        string strCaseXML = m_objPrintDataArr[m_intCurrentRecord].m_objCourseDiseasesRecordArr[k].m_strDiseasesRecordContentXml;
                                        string[] strCaseTextArr, strCaseXmlArr;
                                        com.digitalwave.controls.ctlRichTextBox.m_mthSplitXmlByBytes(strCase, strCaseXML, 36, out strCaseTextArr, out strCaseXmlArr);
                                        for (int k2 = 0; k2 < strCaseTextArr.Length; k2++)
                                        {
                                            arlRecordContent.Add(strCaseTextArr[k2]);
                                            arlRecordContentXML.Add(strCaseXmlArr[k2]);
                                        }
                                        arlRecordContent.Add("                        --" + m_objPrintDataArr[m_intCurrentRecord].m_objCourseDiseasesRecordArr[k].m_strModifyUserName);
                                        arlRecordContentXML.Add("<root />");
                                    }
                                }
                                intContent_Count = arlRecordContent.Count + 1;
                                #endregion
                                #region ����--wf20080117
                                /*
								//���̼�¼ֻ��ʾһ��
								if (m_objCurrent.m_strRecordContent.Trim().Length!=0)
								{
									string[] strCurrentCaseTextArrTemp=null;                //��ǰ���̼�¼��������(��ʱ)
									string[] strCurrentCaseXmlArrTemp=null;                 //��ǰ���̼�¼�ۼ�����(��ʱ)
									string[] strCurrentCaseCreateDateArrTemp=null;          //��ǰ���̼�¼����ʱ������(��ʱ)
                        

									if (intCaseRowCount==0)
									{
										strCurrentCaseTextArr=null;
										strCurrentCaseXmlArr=null;
										strCurrentCaseCreateDateArr=null;
										intCaseRowCount=0;
									}
									else
									{
										strCurrentCaseTextArrTemp=strCurrentCaseTextArr;                 
										strCurrentCaseXmlArrTemp=strCurrentCaseXmlArr;                 
										strCurrentCaseCreateDateArrTemp=strCurrentCaseCreateDateArr;          
										strCurrentCaseTextArr=null;
										strCurrentCaseXmlArr=null;
										strCurrentCaseCreateDateArr=null;
									}
//									strCurrentCaseTextArr=null;
//									strCurrentCaseXmlArr=null;
//									intCaseRowCount=0;
									string strCase="";
									strCase = m_objCurrent.m_strRecordContent;

									string strCaseXML =m_objCurrent.m_strRecordContentXml ;
									string[] strCaseTextArr,strCaseXmlArr;
									com.digitalwave.controls.ctlRichTextBox.m_mthSplitXml(strCase,strCaseXML,14,out strCaseTextArr,out strCaseXmlArr);
									int intCaseCount = strCaseTextArr.Length;
									if (intCaseRowCount>0)//׷�ӵ�����
									{
										int intOldCaseCount=strCurrentCaseTextArrTemp.Length;
										strCurrentCaseTextArr=new string[intCaseCount+intOldCaseCount+1];
										strCurrentCaseXmlArr=new string[intCaseCount+intOldCaseCount+1];
										strCurrentCaseCreateDateArr=new string[intCaseCount+intOldCaseCount+1];
										for (int i = 0; i <intOldCaseCount; i++)
										{
											strCurrentCaseTextArr[i]=strCurrentCaseTextArrTemp[i];
											strCurrentCaseXmlArr[i]=strCurrentCaseXmlArrTemp[i];
											strCurrentCaseCreateDateArr[i]=strCurrentCaseCreateDateArrTemp[i];
										}
										for (int j = 0; j <intCaseCount; j++)
										{
											strCurrentCaseTextArr[j+intOldCaseCount]=strCaseTextArr[j];
											strCurrentCaseXmlArr[j+intOldCaseCount]=strCaseXmlArr[j];
											strCurrentCaseCreateDateArr[j+intOldCaseCount]=m_objCurrent.m_dtContentCreateDate.ToString();
										}
										//���ǩ��
										strCurrentCaseTextArr[intCaseCount+intOldCaseCount]="            --"+ m_objCurrent.m_strContentModifyUserName;
										strCurrentCaseXmlArr[intCaseCount+intOldCaseCount]=strCaseXmlArr[intCaseCount-1];
										strCurrentCaseCreateDateArr[intCaseCount+intOldCaseCount]=m_objCurrent.m_dtContentCreateDate.ToString();

										//���̼�¼�����
										intCaseRowCount=intCaseRowCount+intCaseCount+1;
										intContent_Count=intCaseRowCount;
									}
									else
									{
										strCurrentCaseTextArr=new string[intCaseCount+1];
										strCurrentCaseXmlArr=new string[intCaseCount+1];
										strCurrentCaseCreateDateArr=new string[intCaseCount+1];
										for (int i = 0; i <intCaseCount; i++)
										{
											strCurrentCaseTextArr[i]=strCaseTextArr[i];
											strCurrentCaseXmlArr[i]=strCaseXmlArr[i];
											strCurrentCaseCreateDateArr[i]=m_objCurrent.m_dtContentCreateDate.ToString();
										}
										//���ǩ��
										strCurrentCaseTextArr[intCaseCount]="            --"+ m_objCurrent.m_strContentModifyUserName;
										strCurrentCaseXmlArr[intCaseCount]=strCaseXmlArr[intCaseCount-1];
										strCurrentCaseCreateDateArr[intCaseCount]=m_objCurrent.m_dtContentCreateDate.ToString();
										//���̼�¼�����
										intCaseRowCount=intCaseCount+1;
										intContent_Count=intCaseCount+1;
									}
						
									strCurrentClass=m_objCurrent.m_strClass;
//									//���̼�¼�����
//									intCaseRowCount=intCaseCount;
//									intContent_Count=intCaseCount;
//									//����
//									strCurrentCaseTextArr=strCaseTextArr;
//									strCurrentCaseXmlArr=strCaseXmlArr;
//									strCurrentClass=m_objCurrent.m_strClass;
								}
                                */
                                #endregion
                                if (intMaxCount<intIn_Count)
									intMaxCount=intIn_Count;
								if(intMaxCount<intOut_Count)
									intMaxCount=intOut_Count;
                                if (intMaxCount < intContent_Count)
                                    intMaxCount = intContent_Count;
				
								if(intMaxCount == 0)
									intMaxCount = 1;

								///�ۼ�����
								m_intAllRecords+=intMaxCount;

								#endregion

								for(int k2=0;k2<intMaxCount;k2++)
								{
									//m_strValue[14]������¼ǩ������ȡ��������������
									//ֻ����ʾ��ǰ��ε����ǩ��
									//m_strValue[16]��־һ����¼
									string[] m_strValue=new String[18];
									#region ��ʼ��ȫΪ��
									for (int i = 0; i <m_strValue.Length; i++)
									{
										m_strValue[i]="";
									}
									#endregion
									if(k2<=0)
									{
										m_strValue[0]=(m_objCurrent.m_strTemperature==null? "": m_objCurrent.m_strTemperature);
										m_strValue[1]=(m_objCurrent.m_strPulse==null? "" :m_objCurrent.m_strPulse);
										m_strValue[2]=(m_objCurrent.m_strBreath==null? "" :m_objCurrent.m_strBreath);
										m_strValue[3]=(m_objCurrent.m_strMind==null? "" :m_objCurrent.m_strMind);
										m_strValue[4]=(m_objCurrent.m_strBloodPressureS==null? "" :m_objCurrent.m_strBloodPressureS);
										m_strValue[5]=(m_objCurrent.m_strBloodPressureA==null? "" :m_objCurrent.m_strBloodPressureA);
										m_strValue[6]=(m_objCurrent.m_strPupilLeft==null? "" :m_objCurrent.m_strPupilLeft);
										m_strValue[7]=(m_objCurrent.m_strPupilRight==null? "" :m_objCurrent.m_strPupilRight);
										m_strValue[8]=(m_objCurrent.m_strEchoLeft==null? "" :m_objCurrent.m_strEchoLeft);
										m_strValue[9]=(m_objCurrent.m_strEchoRight==null? "" :m_objCurrent.m_strEchoRight);
                                        m_strValue[10] = (m_objCurrent.m_strBloodOxygenSaturation == null ? "" : m_objCurrent.m_strBloodOxygenSaturation);
									}

									if(k2<intIn_Count)
									{
										m_strValue[11]=((string[])objIn[k2])[0];
										m_strValue[12]=((string[])objIn[k2])[1];
									}
									else
									{
										m_strValue[11]="";
										m_strValue[12]="";
									
									}
									if(k2<intOut_Count)
									{
										m_strValue[13]=((string[])objOut[k2])[0];
										m_strValue[14]=((string[])objOut[k2])[1];
									}
									else
									{
										m_strValue[13]="";
										m_strValue[14]="";

									}
                                    if (arlRecordContent.Count > 0 && k2 != 0)
                                    {
                                        m_strValue[15] = arlRecordContent[k2 - 1].ToString();
                                    }
                                    else if (k2 == 0)
                                    {
                                        m_strValue[15] = m_objCurrent.m_strModifyUserName;//�����¼��ǩ����ʾ�ڲ��̼�¼��;
                                    }
                                    m_strValue[16] = "";
									if (k2==0)
										//һ����¼
										m_strValue[17]="1";
									else
										m_strValue[17]="0";
									m_RecordInfo.Add(m_strValue);

								}
							}
							m_strValueArr=new string[m_intAllRecords][];
							for(int m=0;m<m_intAllRecords ;m++)
								m_strValueArr[m]=(string[])m_RecordInfo[m];
							
							return intLenth;
						#endregion

					}
					#endregion
				}
				else 
					return 0;
				#endregion
			}
			catch(Exception ex)
			{
				clsPublicFunction.ShowInformationMessageBox(ex.Message);
				return 1;
			}			
		}
		#endregion
	
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

			
			//ҳ��//////////////////////////////////////////////////////////////
			//			e.Graphics.DrawString("����"+intNowPage.ToString()+"ҳ��",m_fotSmallFont ,m_slbBrush,
			//				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15-30 ,
			//				(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep*((
			//				int)enmRecordRectangleInfo.RowLinesNum+1)+(int)enmRecordRectangleInfo.VOffSet );
			e.Graphics.DrawString("����"+intNowPage.ToString()+"ҳ��",m_fotSmallFont ,m_slbBrush,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark17-70 ,
				(int)enmRecordRectangleInfo.TopY+((int)enmRecordRectangleInfo.RowStep)*((int)enmRecordRectangleInfo.RowLinesNum+1)-20);
           		
			if(m_objPrintInfo.m_strInPatentID ==""|| m_objPrintDataArr==null)return;

			m_intCurrentRecord=0;

			//��ӡ��ѭ��
			for(m_intCurrentRecord=m_intRowNumberForPrintData;m_intCurrentRecord<m_objPrintDataArr.Length ;m_intCurrentRecord++)
			{
			
				if(blnBeginPrintNewRecord)
					m_intSetPrintOneValueRows(e,ref m_intPosY);
				enmReturnValue m_enmRe= m_blnPrintOneValue(e, m_intPosY);	

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

				try
				{
					#region ������¼�¼����ӡ���ڣ����ô�ӡ����ֵ
//					if(blnBeginPrintNewRecord)
//					{
//						
//						//��ӡһ����¼/////////////////////////////////////////////////////////////////////
//						/*�޸Ĵ�ӡ���ݷ�ʽ���Ե�һ�δ�ӡʱ��Ϊ�ָ��ʱ���������޸ĵĺۼ���Ҫ������
//						 * ���δ��ӡ������ʾ��ȷ�ļ�¼��*/				
//						if(m_objPrintDataArr[m_intCurrentRecord].m_objRecordContent.m_dtmFirstPrintDate==
//							DateTime.MinValue)
//							dtmFlagTime=DateTime.Parse(new clsPublicDomain().m_strGetServerTime());					
//						else 
//							dtmFlagTime=m_objPrintDataArr[m_intCurrentRecord].m_objRecordContent.m_dtmFirstPrintDate;
//						
//						m_objPrintContext.m_mthSetContextWithCorrectBefore(strRecord,strRecordXML,dtmFlagTime);					
//					
//						com.digitalwave.Utility.Controls.ctlRichTextBox.clsModifyUserInfo[] m_objModifyUserArr =
//							m_objPrintContext.m_ObjModifyUserArr;
//
//						for(int i=0;i< m_objModifyUserArr.Length;i++)
//						{
//							if(m_objModifyUserArr[i].m_clrText.ToArgb() == Color.White.ToArgb())
//							{
//								m_objModifyUserArr[i].m_clrText = Color.Black;
//							}
//						}
//
//					}
					#endregion
				}
				catch(Exception ex)
				{
					clsPublicFunction.ShowInformationMessageBox(ex.Message);
				}					
			
			}
            strPerRecordDate = "";//���м�¼��ӡ�����ô˱����Ա�����Ԥ�����ӡ���ڲ���ʾ
		}

		
		#region ֻ��ӡһ��
		/// <summary>
		/// ֻ��ӡһ��
		/// </summary>
		/// <param name="e"></param>
		/// <param name="p_intBottomY"></param>
		/// <returns></returns>
		private enmReturnValue m_blnPrintOneValue(System.Drawing.Printing.PrintPageEventArgs e,int p_intBottomY)
		{			
		    //��������
			string strCreateDate;
			string strCreateTime;
			string strCreateDateTime;
			
			if(m_objPrintDataArr[m_intCurrentRecord].m_intFlag == 0)
			{
				strCreateDate = "";
				strCreateTime = "";
				strCreateDateTime = "";
			}
			else
			{
				if(m_objPrintDataArr[m_intCurrentRecord].m_objRecordContent.m_dtmCreateDate!=DateTime.MinValue)
				{
                    strCreateDateTime = m_objPrintDataArr[m_intCurrentRecord].m_objRecordContent.m_dtmCreateDate.ToString("yyyy-MM-dd HH:mm:ss");	
					try
					{
						strCreateDate=DateTime.Parse(strCreateDateTime).ToString("yyyy-M-d");
						strCreateTime=DateTime.Parse(strCreateDateTime).ToString("HH:mm");
                        if (strCreateDate == strPerRecordDate)
                        {
                            strCreateDate = "";
                        }
                        else
                            strPerRecordDate = strCreateDate;
					}
					catch
					{
                        strCreateDate="����";
                        strCreateTime="����";
                    }	
				}
				else
				{
					strCreateDate = "";
					strCreateTime = "";
					strCreateDateTime = "";
				}
            }
			//��ʼ��ӡһ���¼�¼/////////////////////////////////////////////////////////////////////
			e.Graphics.DrawString(strCreateDate,m_fotTinyFont ,m_slbBrush,
				(int)enmRecordRectangleInfo.LeftX, 
				m_intPosY);
            e.Graphics.DrawString(strCreateTime, m_fotTinyFont, m_slbBrush,
                (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark1 + 1,
                m_intPosY);	
			
			enmReturnValue enmIsRecordFinish=m_blnPrintOneRowValue(m_strValueArr,m_intNowRowInOneRecord,e);
			
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
		private bool m_blnCheckPageChange(int p_intNowRow,System.Drawing.Printing.PrintPageEventArgs e)
		{
			//����ǰ�г������һ�У��� >ҳ��������ʱ��ҳ
			if(p_intNowRow>(int)enmRecordRectangleInfo.RowLinesNum-3/*��ȥ��ͷ3��������Ч����*/) 
			{
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
		
		/// <summary>
		/// ��ӡһ��ʱ���¼��һ����ֵ�������Ѫѹб�ߵĴ�ӡ��
		/// </summary>
		/// <param name="p_strValueArr">��ֵ(�ӡ����¡�����Ż�������19��)</param>
		/// <param name="p_intIndex">�ڼ��εĽ��</param>
		/// <param name="e">��ӡ����</param>
		/// <param name="p_intPosY">Y����</param>
		private enmReturnValue m_blnPrintOneRowValue(string [][] p_strValueArr,int p_intIndex,
			System.Drawing.Printing.PrintPageEventArgs e)
		{			
			string[][] strValueArr = p_strValueArr;

			#region  �ж��Ƿ�ͳ�Ƽ�¼
			bool blnIsSummary=false;                
			for( int i=0;i<p_strValueArr.Length;i++)
			{
				if(p_strValueArr[i][9] == "�ܼ�:")
				{
					blnIsSummary=true;
					break;
				}
			}
			//ͳ�Ƽ�¼��ӡ
			if(blnIsSummary)
			{
                if (p_strValueArr[0][11].ToString() == "" && p_strValueArr[0][13].ToString() == "")
                    return enmReturnValue.enmSuccessed;
                else
				    return m_blnPrintOneRowValueOfSummary(p_strValueArr,e,ref m_intPosY);
			}
			#endregion

			CharacterRange []rgnDSTArr = new CharacterRange[1];
			rgnDSTArr[0] = new CharacterRange(0,0);			
				
			RectangleF rtfText = new RectangleF(0,0,10000,100);

			StringFormat stfMeasure = new StringFormat(StringFormatFlags.LineLimit);			

			RectangleF rtfBounds;

			Region [] rgnDST;

            for (int i = m_intCurrentRecordRowIndex; i < strValueArr.Length; i++)
            {
                //����
                int intTempColumn = 0;//��ǰ����������ԣ�
                int intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2;//��ǰ��X����
                if (strValueArr[i][intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[i][intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, m_intPosY);
                }
                //����
                intTempColumn++;//��ǰ����������ԣ�
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark3;//��ǰ��X����
                if (strValueArr[i][intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[i][intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, m_intPosY);
                }
                //����
                intTempColumn++;//��ǰ����������ԣ�
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark4;//��ǰ��X����
                if (strValueArr[i][intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[i][intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, m_intPosY);
                }
                //��־
                intTempColumn++;//��ǰ����������ԣ�
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark5;//��ǰ��X����
                if (strValueArr[i][intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[i][intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, m_intPosY);
                }
                //Ѫѹ(����ѹ)
                intTempColumn++;//��ǰ����������ԣ�
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark6;//��ǰ��X����
                if (strValueArr[i][intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[i][intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, m_intPosY - 7);
                }
                //Ѫѹ(����ѹ)
                intTempColumn++;//��ǰ����������ԣ�
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark6 + 30;//��ǰ��X����
                if (strValueArr[i][intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[i][intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, m_intPosY + 5);
                }
                //ͫ�״�С����
                intTempColumn++;//��ǰ����������ԣ�
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark7;//��ǰ��X����
                if (strValueArr[i][intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[i][intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, m_intPosY);
                }
                //ͫ�״�С���ң�
                intTempColumn++;//��ǰ����������ԣ�
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark8;//��ǰ��X����
                if (strValueArr[i][intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[i][intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, m_intPosY);
                }
                //ͫ�׷��䣨��)
                intTempColumn++;//��ǰ����������ԣ�
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9;//��ǰ��X����
                if (strValueArr[i][intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[i][intTempColumn], m_fotTinyFont, Brushes.Black, intPosX, m_intPosY);
                }
                //ͫ�׷��䣨�ң�
                intTempColumn++;//��ǰ����������ԣ�
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10;//��ǰ��X����
                if (strValueArr[i][intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[i][intTempColumn], m_fotTinyFont, Brushes.Black, intPosX, m_intPosY);
                }
                //Ѫ�����Ͷ�
                intTempColumn ++;//��ǰ����������ԣ�
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11;//��ǰ��X����
                if (strValueArr[i][intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[i][intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, m_intPosY);
                }
                //��������
                intTempColumn++;//��ǰ����������ԣ�
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12;//��ǰ��X����
                if (strValueArr[i][intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[i][intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, m_intPosY);
                }
                //��������
                intTempColumn++;//��ǰ����������ԣ�
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13;//��ǰ��X����
                if (strValueArr[i][intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[i][intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, m_intPosY);
                }
                //�ų�����
                intTempColumn++;//��ǰ����������ԣ�
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark14;//��ǰ��X����
                if (strValueArr[i][intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[i][intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, m_intPosY);
                }
                //�ų�����
                intTempColumn++;//��ǰ����������ԣ�
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15;//��ǰ��X����
                if (strValueArr[i][intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[i][intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, m_intPosY);
                }
                //���̼�¼���߻����¼ǩ��
                intTempColumn++;//��ǰ����������ԣ�
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16;//��ǰ��X����
                if (strValueArr[i][intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[i][intTempColumn], m_fotTinyFont, Brushes.Black, intPosX, m_intPosY);
                }
                if (m_blnCheckPageChange(++intNowRow, e) == true) //ÿ��ӡ��һ�м����һ���Ƿ���Ҫ��ҳ
                {
                    m_intCurrentRecordRowIndex++;
                    //��ҳ
                    return enmReturnValue.enmNeedNextPage;
                }
                else
                {
                    m_intPosY += intTempDeltaY;
                }
            }
            #region ��ӡһ����¼ռ�ü��е���� ����--wf20080121
            
//            clsIntensiveTendDataInfo m_objTemp=(clsIntensiveTendDataInfo)m_objPrintInfo.m_objTransDataArr[m_intCurrentRecord];

//            //n����ά���뱨���еĶ�ά������Ӧ���ڴ������С�
//            for(int n=m_intRowNumberInValueArr,m=m_intRowNumberInTempArr;n<strValueArr.GetLength(0) && m<m_objTemp.m_objTransDataArr.Length;n++)
//            {					
//                if(m_blnCheckPageChange(intNowRow,e)==true) //ÿ��ӡһ��֮ǰ��Ҫ����Ƿ�ҳ
//                {
//                    m_intRowNumberInValueArr=n;
//                    m_intRowNumberInTempArr=m;
//                    //��ҳ
//                    return enmReturnValue.enmNeedNextPage;
//                }
				
//                //����Ƿ��������¼�¼���������1
//                if(n>0 && strValueArr[n][16]=="1")
//                {
//                    m++;
//                }

//                int intTempColumn=0;//��ǰ����������ԣ�
//                int intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark2;//��ǰ��X����
//                //����
//                #region ��ӡһ�񣬣�������ȫ��ͬ��
//                if(strValueArr[n][intTempColumn].Trim().Length != 0)
//                {
//                    e.Graphics.DrawString(strValueArr[n][intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,m_intPosY);
//                    //���´��������������޸ĺۼ�
//                    /*��Ҫ�ۼ�
//                    if(m+1< m_objTemp.m_objTransDataArr.Length)
//                    {
//                        if(strValueArr[n][intTempColumn] != m_objTemp.m_objTransDataArr[m+1].m_strTemperature)
//                        {
//                            rtfText.X = intPosX;
//                            rtfText.Y = m_intPosY;

//                            rgnDSTArr[0].First = 0;
//                            rgnDSTArr[0].Length = strValueArr[n][intTempColumn].Length;

//                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

//                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[n][intTempColumn],m_fotSmallFont,
//                                rtfText,stfMeasure);

//                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

//                            e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,
//                                rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
//                            e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,
//                                rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
//                        }
//                    }*/
//                }
//                #endregion	��ӡһ��

//                intTempColumn=1;
//                intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark3;//��ǰ��X����
//                //����
//                #region ��ӡһ��
//                if(strValueArr[n][intTempColumn].Trim().Length != 0)
//                {
//                    e.Graphics.DrawString(strValueArr[n][intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,m_intPosY);
//                    /*��Ҫ�ۼ�
//                    if(m+1 < m_objTemp.m_objTransDataArr.Length)
//                    {
//                        if(strValueArr[n][intTempColumn] != m_objTemp.m_objTransDataArr[m+1].m_strPulse)
//                        {
//                            rtfText.X = intPosX;
//                            rtfText.Y = m_intPosY;

//                            rgnDSTArr[0].First = 0;
//                            rgnDSTArr[0].Length = strValueArr[n][intTempColumn].Length;

//                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

//                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[n][intTempColumn],m_fotSmallFont,
//                                rtfText,stfMeasure);

//                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

//                            e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,
//                                rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
//                            e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,
//                                rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
//                        }
//                    }*/
//                }
//                #endregion	��ӡһ��

//                intTempColumn=2;
//                intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark4;//��ǰ��X����

//                //����
//                #region ��ӡһ��
//                if(strValueArr[n][intTempColumn].Trim().Length != 0)
//                {
//                    e.Graphics.DrawString(strValueArr[n][intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,m_intPosY);
//                    /*��Ҫ�ۼ�
//                    if(m+1 < m_objTemp.m_objTransDataArr.Length)
//                    {
//                        if(strValueArr[n][intTempColumn] != m_objTemp.m_objTransDataArr[m+1].m_strBreath)
//                        {
//                            rtfText.X = intPosX;
//                            rtfText.Y = m_intPosY;

//                            rgnDSTArr[0].First = 0;
//                            rgnDSTArr[0].Length = strValueArr[n][intTempColumn].Length;

//                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

//                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[n][intTempColumn],m_fotSmallFont,
//                                rtfText,stfMeasure);

//                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

//                            e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,
//                                rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
//                            e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,
//                                rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
//                        }
//                    }*/
//                }
//                #endregion	��ӡһ��

//                intTempColumn=3;
//                intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark5;//��ǰ��X����
//                //��־
//                #region ��ӡһ��
//                if(strValueArr[n][intTempColumn].Trim().Length != 0)
//                {
//                    if (strValueArr[n][intTempColumn].ToString().Length>3)
//                        e.Graphics.DrawString(strValueArr[n][intTempColumn],m_fotTinyFont,Brushes.Black,intPosX,m_intPosY);
//                    else
//                        e.Graphics.DrawString(strValueArr[n][intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,m_intPosY);
//                    /*��Ҫ�ۼ�
//                    if(m+1 < m_objTemp.m_objTransDataArr.Length)
//                    {
//                        if(strValueArr[n][intTempColumn] != m_objTemp.m_objTransDataArr[m+1].m_strMind)
//                        {
//                            rtfText.X = intPosX;
//                            rtfText.Y = m_intPosY;

//                            rgnDSTArr[0].First = 0;
//                            rgnDSTArr[0].Length = strValueArr[n][intTempColumn].Length;

//                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

//                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[n][intTempColumn],m_fotSmallFont,
//                                rtfText,stfMeasure);

//                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

//                            e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,
//                                rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
//                            e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,
//                                rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
//                        }
//                    }*/
//                }
//                #endregion	��ӡһ��

//                intTempColumn=4;
//                intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark6;//��ǰ��X����

						
//                bool blnIsLastModify=false;
//                if( m == m_objTemp.m_objTransDataArr.Length-1 || (strValueArr[n][4] == m_objTemp.m_objTransDataArr[m+1].
//                    m_strBloodPressureA && 
//                    strValueArr[n][5] == m_objTemp.m_objTransDataArr[m+1].m_strBloodPressureA && 
//                    strValueArr[n][4] == m_objTemp.m_objTransDataArr[m+1].m_strBloodPressureS && 
//                    strValueArr[n][5] ==m_objTemp.m_objTransDataArr[m+1].m_strBloodPressureA ))

//                {// ��������һ�У����ҵ�ǰԪ�� != ��һ�д�Ԫ��				
//                    blnIsLastModify=true;					
//                }
//                //Ѫѹ(����ѹ)
//                #region ��ӡһ��
//                if(strValueArr[n][4].Trim().Length != 0)
//                {
//                    e.Graphics.DrawString(strValueArr[n][intTempColumn],m_fotSmallFont,Brushes.Black,
//                        (int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark6,m_intPosY-7);
//                    /*��Ҫ�ۼ�
//                    if( ! blnIsLastModify)
//                    {					
//                        rtfText.X = (int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark6;
//                        rtfText.Y = m_intPosY-15;

//                        rgnDSTArr[0].First = 0;
//                        rgnDSTArr[0].Length = strValueArr[n][intTempColumn].Length;

//                        stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

//                        rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[n][intTempColumn],m_fotSmallFont,
//                            rtfText,stfMeasure);

//                        rtfBounds = rgnDST[0].GetBounds(e.Graphics);
	
//                        e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+6,
//                            rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+6);
//                        e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3+6,
//                            rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3+6);
					
//                    }
//                     * */
//                }	
				
//                #endregion ;
			
//                //Ѫѹ(����ѹ)
//                #region ��ӡһ��
//                if(strValueArr[n][5].Trim().Length != 0)
//                {
//                    e.Graphics.DrawString(strValueArr[n][intTempColumn+1],m_fotSmallFont,Brushes.Black,
//                        (int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark6+30,m_intPosY+5);
//                    /*��Ҫ�ۼ�
//                    if( ! blnIsLastModify)
//                    {					
//                        rtfText.X = (int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark6+30;
//                        rtfText.Y = m_intPosY;

//                        rgnDSTArr[0].First = 0;
//                        rgnDSTArr[0].Length = strValueArr[n][intTempColumn+1].Length;

//                        stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

//                        rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[n][intTempColumn+1],m_fotSmallFont,
//                            rtfText,stfMeasure);

//                        rtfBounds = rgnDST[0].GetBounds(e.Graphics);

//                        e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+4,
//                            rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+4);
//                        e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3+4,
//                            rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3+4);
					
//                    }*/
//                }

				
//                #endregion ;
//                intTempColumn=6;
//                intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7;//��ǰ��X����
//                //ͫ�״�С����
//                #region ��ӡһ��
//                if(strValueArr[n][intTempColumn].Trim().Length != 0)
//                {
//                    e.Graphics.DrawString(strValueArr[n][intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,m_intPosY);
//                    /*��Ҫ�ۼ�
//                    if(m+1 < m_objTemp.m_objTransDataArr.Length)
//                    {
//                        if(strValueArr[n][intTempColumn] != m_objTemp.m_objTransDataArr[m+1].m_strPupilLeft)
//                        {
//                            rtfText.X = intPosX;
//                            rtfText.Y = m_intPosY;

//                            rgnDSTArr[0].First = 0;
//                            rgnDSTArr[0].Length = strValueArr[n][intTempColumn].Length;

//                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

//                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[n][intTempColumn],m_fotSmallFont,
//                                rtfText,stfMeasure);

//                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

//                            e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,
//                                rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
//                            e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,
//                                rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
//                        }
//                    }*/
//                }
//                #endregion	��ӡһ��

//                intTempColumn++;
//                intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8;//��ǰ��X����
//                //ͫ�״�С���ң�
//                #region ��ӡһ��
//                if(strValueArr[n][intTempColumn].Trim().Length != 0)
//                {
//                    e.Graphics.DrawString(strValueArr[n][intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,m_intPosY);
//                    /*��Ҫ�ۼ�
//                    if(m+1 < m_objTemp.m_objTransDataArr.Length)
//                    {
//                        if(strValueArr[n][intTempColumn] != m_objTemp.m_objTransDataArr[m+1].m_strPupilRight)
//                        {
//                            rtfText.X = intPosX;
//                            rtfText.Y = m_intPosY;

//                            rgnDSTArr[0].First = 0;
//                            rgnDSTArr[0].Length = strValueArr[n][intTempColumn].Length;

//                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

//                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[n][intTempColumn],m_fotSmallFont,
//                                rtfText,stfMeasure);

//                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

//                            e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,
//                                rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
//                            e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,
//                                rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
//                        }
//                    }*/
//                }
//                #endregion	��ӡһ��

//                intTempColumn++;
//                intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark9;//��ǰ��X����
//                //ͫ�׷��䣨��
//                #region ��ӡһ��
//                if(strValueArr[n][intTempColumn].Trim().Length != 0)
//                {
//                    if(strValueArr[n][intTempColumn].Length > 1)
//                        e.Graphics.DrawString(strValueArr[n][intTempColumn],m_fotTinyFont,Brushes.Black,intPosX,m_intPosY);
//                    else
//                        e.Graphics.DrawString(strValueArr[n][intTempColumn],m_fotSmallFont, Brushes.Black, intPosX, m_intPosY);
//                    /*��Ҫ�ۼ�
//                    if(m+1 < m_objTemp.m_objTransDataArr.Length)
//                    {
//                        if(strValueArr[n][intTempColumn] != m_objTemp.m_objTransDataArr[m+1].m_strEchoLeft)
//                        {
//                            rtfText.X = intPosX;
//                            rtfText.Y = m_intPosY;

//                            rgnDSTArr[0].First = 0;
//                            rgnDSTArr[0].Length = strValueArr[n][intTempColumn].Length;

//                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

//                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[n][intTempColumn],m_fotSmallFont,
//                                rtfText,stfMeasure);

//                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

//                            e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,
//                                rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
//                            e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,
//                                rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
//                        }
//                    }*/
//                }
//                #endregion	��ӡһ��		

//                intTempColumn++;
//                intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark10;//��ǰ��X����
//                //ͫ�׷��䣨�ң�
//                #region ��ӡһ��
//                if(strValueArr[n][intTempColumn].Trim().Length != 0)
//                {
//                    if (strValueArr[n][intTempColumn].Length > 1)
//                        e.Graphics.DrawString(strValueArr[n][intTempColumn], m_fotTinyFont, Brushes.Black, intPosX, m_intPosY);
//                    else
//                        e.Graphics.DrawString(strValueArr[n][intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, m_intPosY);
//                    /*��Ҫ�ۼ�
//                    if(m+1 < m_objTemp.m_objTransDataArr.Length)
//                    {
//                        if(strValueArr[n][intTempColumn] != m_objTemp.m_objTransDataArr[m+1].m_strEchoRight)
//                        {
//                            rtfText.X = intPosX;
//                            rtfText.Y = m_intPosY;

//                            rgnDSTArr[0].First = 0;
//                            rgnDSTArr[0].Length = strValueArr[n][intTempColumn].Length;

//                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

//                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[n][intTempColumn],m_fotSmallFont,
//                                rtfText,stfMeasure);

//                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

//                            e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,
//                                rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
//                            e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,
//                                rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
//                        }
//                    }*/
//                }
//                #endregion	��ӡһ��		
			
//                intTempColumn++;
//                intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark11;//��ǰ��X����
//                //Ѫ�����Ͷ�
//                #region ��ӡһ��
//                if (strValueArr[n][intTempColumn].Trim().Length != 0)
//                {
//                    e.Graphics.DrawString(strValueArr[n][intTempColumn], m_fotSmallFont, Brushes.Black, intPosX, m_intPosY);

//                }
//                #endregion	��ӡһ��

//                intTempColumn++;
//                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12;//��ǰ��X����
//                //��������
//                #region ��ӡһ��
//                if(strValueArr[n][intTempColumn].Trim().Length != 0)
//                {
//                    e.Graphics.DrawString(strValueArr[n][intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,m_intPosY);
					
//                }
//                #endregion	��ӡһ��					

//                intTempColumn++;
//                intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark13;//��ǰ��X����
//                //��������
//                #region ��ӡһ��
//                if(strValueArr[n][intTempColumn].Trim().Length != 0)
//                {
//                    e.Graphics.DrawString(strValueArr[n][intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,m_intPosY);
//                    /*��Ҫ�ۼ�
//                    if(m+1 < m_objTemp.m_objTransDataArr.Length)
//                    {
//                        if((strValueArr[n][intTempColumn-1]=="D" && 
//                            strValueArr[n][intTempColumn]!=m_objTemp.m_objTransDataArr[m+1].m_intInD.ToString()) || 
//                            (strValueArr[n][intTempColumn-1]=="I" && 
//                            strValueArr[n][intTempColumn]!=m_objTemp.m_objTransDataArr[m+1].m_intInI.ToString()))
//                        {
//                            rtfText.X = intPosX;
//                            rtfText.Y = m_intPosY;

//                            rgnDSTArr[0].First = 0;
//                            rgnDSTArr[0].Length = strValueArr[n][intTempColumn].Length;

//                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

//                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[n][intTempColumn],m_fotSmallFont,
//                                rtfText,stfMeasure);

//                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

//                            e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,
//                                rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
//                            e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,
//                                rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
//                        }
//                    }*/
//                }
//                #endregion	��ӡһ��								

//                intTempColumn++;
//                intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark14;//��ǰ��X����
//                //�ų�����
//                #region ��ӡһ��
//                if(strValueArr[n][intTempColumn].Trim().Length != 0)
//                {
//                    e.Graphics.DrawString(strValueArr[n][intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,m_intPosY);
					
//                }
//                #endregion	��ӡһ��								

//                intTempColumn++;
//                intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15;//��ǰ��X����
//                //�ų�����
//                #region ��ӡһ��
//                if(strValueArr[n][intTempColumn].Trim().Length != 0)
//                {
//                    e.Graphics.DrawString(strValueArr[n][intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,m_intPosY);
//                    /*��Ҫ�ۼ�
//                    if(m+1 < m_objTemp.m_objTransDataArr.Length)
//                    {
//                        if((strValueArr[n][intTempColumn-1]=="U" && 
//                            strValueArr[n][intTempColumn]!=m_objTemp.m_objTransDataArr[m+1].m_intOutU.ToString()) || 
//                            (strValueArr[n][intTempColumn-1]=="V" && 
//                            strValueArr[n][intTempColumn]!=m_objTemp.m_objTransDataArr[m+1].m_intOutV.ToString()) ||
//                            (strValueArr[n][intTempColumn-1]=="S" && 
//                            strValueArr[n][intTempColumn]!=m_objTemp.m_objTransDataArr[m+1].m_intOutS.ToString()) ||
//                            (strValueArr[n][intTempColumn-1]=="E" && 
//                            strValueArr[n][intTempColumn]!=m_objTemp.m_objTransDataArr[m+1].m_intOutE.ToString()))
//                        {
								
						
//                            rtfText.X = intPosX;
//                            rtfText.Y = m_intPosY;

//                            rgnDSTArr[0].First = 0;
//                            rgnDSTArr[0].Length = strValueArr[n][intTempColumn].Length;

//                            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

//                            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[n][intTempColumn],m_fotSmallFont,
//                                rtfText,stfMeasure);

//                            rtfBounds = rgnDST[0].GetBounds(e.Graphics);

//                            e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,
//                                rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
//                            e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,
//                                rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
//                        }
//                    }*/
//                }
//                #endregion	��ӡһ��								

//                intTempColumn++;
//                intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark16 + 1;//��ǰ��X����
//                //���̼�¼
//                #region ��ӡһ��
//                if(strValueArr[n][intTempColumn].Trim().Length != 0)
//                {
//                    e.Graphics.DrawString(strValueArr[n][intTempColumn],m_fotTinyFont,Brushes.Black,intPosX,m_intPosY);
////					if(m+1 < m_objTemp.m_objTransDataArr.Length)
////					{
////						if(strValueArr[n][intTempColumn] !=m_objTemp.m_objTransDataArr[m+1].m_strRecordContent)
////						{
////							rtfText.X = intPosX;
////							rtfText.Y = m_intPosY;
////
////							rgnDSTArr[0].First = 0;
////							rgnDSTArr[0].Length = strValueArr[n][intTempColumn].Length;
////
////							stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					
////
////							rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[n][intTempColumn],m_fotSmallFont,
////								rtfText,stfMeasure);
////
////							rtfBounds = rgnDST[0].GetBounds(e.Graphics);
////
////							e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,
////								rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
////							e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,
////								rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
////						}
////					}
//                }
//                #endregion

//                intTempColumn++;
//                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark16 + 1;//��ǰ��X����
//                #region ǩ���������޸ĵ���ǩ����
//                if (strValueArr[n][intTempColumn].Trim().Length != 0)
//                {
//                    e.Graphics.DrawString(strValueArr[n][intTempColumn], m_fotTinyFont, Brushes.Black, intPosX, m_intPosY);
//                }
//                #endregion	

//                if(n<strValueArr.GetLength(0))
//                    m_intPosY +=intTempDeltaY;

//                //��ӡ�����ӣ������������ʵ���ʱ��ҳ
//                intNowRow ++;
				
//            }
//*/
            #endregion
            m_intCurrentRecordRowIndex = 0;//��һ����¼�����д�ӡ�꣬���ô˱���
			return enmReturnValue.enmSuccessed;
		}
	
		/// <summary>
		/// ��ӡһ��ʱ���¼��һ����ֵ�������Ѫѹб�ߵĴ�ӡ��
		/// </summary>
		/// <param name="p_strValueArr">��ֵ(�ӡ����¡�����Ż�������19��)</param>
		/// <param name="p_intIndex">�ڼ��εĽ��</param>
		/// <param name="e">��ӡ����</param>
		/// <param name="p_intPosY">Y����</param>
		private enmReturnValue m_blnPrintOneRowValueOfSummary(string [][] p_strValueArr,System.Drawing.Printing.PrintPageEventArgs e,ref int p_intPosY)
        {
            #region ����--wf20080121
            /*
            for (int i=0;i<p_strValueArr.Length-1;i++)
			{
				if(m_blnCheckPageChange(intNowRow,e)==true) //ÿ��ӡһ��֮ǰ��Ҫ����Ƿ�ҳ
				{
					//����δ�򲡳̼�¼����ԭ
					if(strCurrentCaseTextArr != null)
					    intCaseRowCount+=strCurrentCaseTextArr.Length-2-i;

					//��ҳ
					return enmReturnValue.enmNeedNextPage;
				
				}
				string [] strValueArr = p_strValueArr[i];

				CharacterRange []rgnDSTArr = new CharacterRange[1];
				rgnDSTArr[0] = new CharacterRange(0,0);			
					
				RectangleF rtfText = new RectangleF(0,0,10000,100);

				StringFormat stfMeasure = new StringFormat(StringFormatFlags.LineLimit);			

				RectangleF rtfBounds;

				Region [] rgnDST;

				int intTempColumn=0;//��ǰ����������ԣ�
				int intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark2;//��ǰ��X����
				//����

				intTempColumn++;
				intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark3;//��ǰ��X����
				//����

				intTempColumn++;
				intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark4;//��ǰ��X����
				//����

				intTempColumn+=3;
				intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark6;//��ǰ��X����
				//ͫ�״�С����

				intTempColumn++;
				intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8-1;//��ǰ��X����
				//ͫ�״�С���ң�
				if(strValueArr[intTempColumn].Trim().Length != 0)
				{
					e.Graphics.DrawString(strValueArr[intTempColumn],m_fotTinyFont,Brushes.Black,intPosX,m_intPosY);
				}

				intTempColumn++;
				intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark9;//��ǰ��X����
				//ͫ�׷��䣨��
				if(strValueArr[intTempColumn].Trim().Length != 0)
				{
					e.Graphics.DrawString(strValueArr[intTempColumn],m_fotTinyFont,Brushes.Black,intPosX,m_intPosY);
				}

				intTempColumn++;
				intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark10;//��ǰ��X����
				//ͫ�׷��䣨�ң�
				#region ��ӡһ��
				if(strValueArr[intTempColumn].Trim().Length != 0)
				{
					e.Graphics.DrawString(strValueArr[intTempColumn],m_fotTinyFont,Brushes.Black,intPosX,m_intPosY);
				}
				#endregion	��ӡһ��		
				
                intTempColumn++;
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11;//��ǰ��X����
                //Ѫ�����Ͷ�
                #region ��ӡһ��
                if (strValueArr[intTempColumn].Trim().Length != 0)
                {
                    e.Graphics.DrawString(strValueArr[intTempColumn], m_fotTinyFont, Brushes.Black, intPosX, m_intPosY);

                }
                #endregion	��ӡһ��

				intTempColumn++;
				intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark12;//��ǰ��X����
				//��������
				#region ��ӡһ��
				if(strValueArr[intTempColumn].Trim().Length != 0)
				{
					e.Graphics.DrawString(strValueArr[intTempColumn],m_fotTinyFont,Brushes.Black,intPosX,m_intPosY);
					
				}
				#endregion	��ӡһ��					

				intTempColumn++;
				intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark13;//��ǰ��X����
				//��������
				#region ��ӡһ��
				if(strValueArr[intTempColumn].Trim().Length != 0)
				{
					e.Graphics.DrawString(strValueArr[intTempColumn],m_fotTinyFont,Brushes.Black,intPosX+5,m_intPosY);

					rtfText.X = intPosX;
					rtfText.Y = m_intPosY;

					rgnDSTArr[0].First = 0;
					rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

					stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

					rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,
						rtfText,stfMeasure);

					rtfBounds = rgnDST[0].GetBounds(e.Graphics);
					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+6,
						rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+6);
					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+9,
						rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+9);

				}
				#endregion	��ӡһ��								

				intTempColumn++;
				intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark14;//��ǰ��X����
				//�ų�����
				#region ��ӡһ��
				if(strValueArr[intTempColumn].Trim().Length != 0)
				{
					e.Graphics.DrawString(strValueArr[intTempColumn],m_fotTinyFont,Brushes.Black,intPosX,m_intPosY);

				}
				#endregion	��ӡһ��								

				intTempColumn++;
				intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15;//��ǰ��X����
				//�ų�����
				#region ��ӡһ��
				if(strValueArr[intTempColumn].Trim().Length != 0)
				{
					e.Graphics.DrawString(strValueArr[intTempColumn],m_fotTinyFont,Brushes.Black,intPosX+5,m_intPosY);

					rtfText.X = intPosX;
					rtfText.Y = m_intPosY;

					rgnDSTArr[0].First = 0;
					rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

					stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

					rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

					rtfBounds = rgnDST[0].GetBounds(e.Graphics);

					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+6,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+6);
					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+9,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+9);

				}
				#endregion	��ӡһ��								

				intTempColumn++;
				intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark17;//��ǰ��X����
				
				//���̼�¼������У�
				#region ��ӡһ��
				if(strValueArr[intTempColumn].Trim().Length != 0)
				{
					e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX+5,m_intPosY);
					rtfText.X = intPosX;
					rtfText.Y = m_intPosY;

//					rgnDSTArr[0].First = 0;
//					rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;
//
//					stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					
//
//					rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);
//
//					rtfBounds = rgnDST[0].GetBounds(e.Graphics);
//
//
//					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,
//						rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
//					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,
//						rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
 				}
				#endregion


				//������һ�еĴ�ӡ����
				m_intPosY+=intTempDeltaY;

				//��¼��ǰ�Ѵ�ӡ������
				intNowRow ++;
            }
            */
            #endregion
            string[] strValueArr = p_strValueArr[0];
            CharacterRange[] rgnDSTArr = new CharacterRange[1];
            rgnDSTArr[0] = new CharacterRange(0, 0);	
            RectangleF rtfText = new RectangleF(0, 0, 10000, 100);
            StringFormat stfMeasure = new StringFormat(StringFormatFlags.LineLimit);
            RectangleF rtfBounds;
            Region[] rgnDST;
            //24Сʱ
            int intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark9 + 1;
            e.Graphics.DrawString(strValueArr[7], m_fotTinyFont, Brushes.Black, intPosX, p_intPosY);
            //����
            intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark10 + 1;
            e.Graphics.DrawString(strValueArr[8], m_fotTinyFont, Brushes.Black, intPosX, p_intPosY);
            //�ܼ�
            intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark11 + 1;
            e.Graphics.DrawString(strValueArr[9], m_fotTinyFont, Brushes.Black, intPosX, p_intPosY);
            //����
            intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark12 + 1;
            e.Graphics.DrawString(strValueArr[10], m_fotTinyFont, Brushes.Black, intPosX, p_intPosY);
            //��ɫ˫�»���
            rtfText.X = intPosX;
            rtfText.Y = m_intPosY;
            rgnDSTArr[0].First = 0;
            rgnDSTArr[0].Length = strValueArr[10].Length;
            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);
            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[10], m_fotSmallFont,
                rtfText, stfMeasure);
            rtfBounds = rgnDST[0].GetBounds(e.Graphics);
            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 6,
                rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 6);
            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 9,
                rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 9);
            //������
            if (strValueArr[11] != "")
            {
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark13 + 1;
                e.Graphics.DrawString(strValueArr[11], m_fotTinyFont, Brushes.Black, intPosX, p_intPosY);
                //��ɫ˫�»���
                rtfText.X = intPosX;
                rtfText.Y = m_intPosY;
                rgnDSTArr[0].First = 0;
                rgnDSTArr[0].Length = strValueArr[11].Length;
                stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);
                rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[11], m_fotSmallFont,
                    rtfText, stfMeasure);
                rtfBounds = rgnDST[0].GetBounds(e.Graphics);
                e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 6,
                    rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 6);
                e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 9,
                    rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 9);
            }
            //�ų�
            intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark14 + 1;
            e.Graphics.DrawString(strValueArr[12], m_fotTinyFont, Brushes.Black, intPosX, p_intPosY);
            //��ɫ˫�»���
            rtfText.X = intPosX;
            rtfText.Y = m_intPosY;
            rgnDSTArr[0].First = 0;
            rgnDSTArr[0].Length = strValueArr[12].Length;
            stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);
            rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[12], m_fotSmallFont,
                rtfText, stfMeasure);
            rtfBounds = rgnDST[0].GetBounds(e.Graphics);
            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 6,
                rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 6);
            e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 9,
                rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 9);
            //�ų���
            if (strValueArr[13] != "")
            {
                intPosX = (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark15 + 1;
                e.Graphics.DrawString(strValueArr[13], m_fotTinyFont, Brushes.Black, intPosX, p_intPosY);
                //��ɫ˫�»���
                rtfText.X = intPosX;
                rtfText.Y = m_intPosY;
                rgnDSTArr[0].First = 0;
                rgnDSTArr[0].Length = strValueArr[13].Length;
                stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);
                rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[13], m_fotSmallFont,
                    rtfText, stfMeasure);
                rtfBounds = rgnDST[0].GetBounds(e.Graphics);
                e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 6,
                    rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 6);
                e.Graphics.DrawLine(Pens.Red, rtfBounds.X, rtfBounds.Y + rtfBounds.Height / 2 + 9,
                    rtfBounds.X + rtfBounds.Width, rtfBounds.Y + rtfBounds.Height / 2 + 9);
            }
            intNowRow++;
            p_intPosY += intTempDeltaY; 
            return enmReturnValue.enmSuccessed;
		}
	


	}
//	public enum enmReturnValue
//	{
//		enmSuccessed=1,
//		enmFaild=-1,
//		enmNeedNextPage=2,
//		
//	}
		#endregion ;
		


}
