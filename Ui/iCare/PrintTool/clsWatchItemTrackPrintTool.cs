using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.Utility.Controls;
using System.Drawing;

namespace iCare
{
	/// <summary>
	/// �۲���Ŀ�Ĵ�ӡ������,Jacky-2003-6-5
	/// </summary>
	public class clsWatchItemTrackPrintTool: infPrintRecord
	{	
		private bool m_blnIsFromDataSource=true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
		private bool m_blnWantInit=true;		
		private clsRecordsDomain m_objRecordsDomain;
		private clsPrintInfo_WatchItem m_objPrintInfo;
		
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
			m_objPrintInfo=new clsPrintInfo_WatchItem();
			m_objPrintInfo.m_strInPatentID=m_objPatient!=null? m_objPatient.m_StrInPatientID:"";					
			m_objPrintInfo.m_strPatientName=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrFirstName :"";
			m_objPrintInfo. m_strSex=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrSex:"";
			m_objPrintInfo. m_strAge=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrAge : "";
			m_objPrintInfo. m_strBedName=m_objPatient!=null? m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName :"";
			m_objPrintInfo. m_strDeptName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjDept.m_StrDeptName :"";
			m_objPrintInfo. m_strAreaName=m_objPatient!=null? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName:"";			
			m_objPrintInfo.m_dtmInPatientDate=p_dtmInPatientDate;
			m_objPrintInfo.m_dtmOpenDate=p_dtmOpenDate;

            m_objPrintInfo.m_dtmHISInDate = m_objPatient != null ? m_objPatient.m_DtmSelectedHISInDate : DateTime.MinValue;
            m_objPrintInfo.m_strHISInPatientID = m_objPatient != null ? m_objPatient.m_StrHISInPatientID : "";
		}

		/// <summary>
		/// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���(�������ݿ��ȡʱҪ����.)
		/// </summary>
		public void m_mthInitPrintContent()
		{			
			if(m_objPrintInfo==null)
			{
				MDIParent.ShowInformationMessageBox("����m_mthInitPrintContent֮ǰ�����ȵ���m_mthSetPrintInfo����");
				return;
			}
			if(m_objPrintInfo.m_strInPatentID=="")
				return;
            m_objRecordsDomain = new clsRecordsDomain(enmRecordsType.WatchItem);
				
			long lngRes = m_objRecordsDomain.m_lngGetPrintInfo(m_objPrintInfo.m_strInPatentID,m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"),out m_objPrintInfo.m_objTransDataArr,out m_objPrintInfo.m_dtmFirstPrintDateArr,out m_objPrintInfo.m_blnIsFirstPrintArr);
			if(lngRes <= 0)
				return ;   

			//����¼ʱ��(CreateDate)���� 
			m_mthSortTransData(ref m_objPrintInfo.m_objTransDataArr);
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
			if(p_objPrintContent.GetType().Name !="clsPrintInfo_WatchItem")
			{
				MDIParent.ShowInformationMessageBox("��������");
			}
			m_blnIsFromDataSource=false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ
			m_objPrintInfo=(clsPrintInfo_WatchItem)p_objPrintContent;
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
					MDIParent.ShowInformationMessageBox("�������ݿ��ȡʱ,����m_objGetPrintInfo֮ǰ�����ȵ���m_mthSetPrintInfo����");
					return null;
				}

				if(m_blnWantInit)
					m_mthInitPrintContent();				
			}			
			
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
			m_fotTinyFont=new Font("SimSun",8f);		
			m_GridPen = new Pen(Color.Black,1);
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
			m_mthPrintPageSub(e);
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
		private int m_intPosY = (int)enmRecordRectangleInfo.TopY+150;
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
		private int m_intNowPage=1;
		/// <summary>
		/// ��ǰ��ӡ�ļ�¼�����
		/// </summary>
		private int m_intCurrentRecord=0;  
		/// <summary>
		/// �ɼ�¼����,׼����ӡһ���¼�¼
		/// </summary>
		bool m_blnBeginPrintNewRecord=true;		

		/// <summary>
		/// ����Ҫ������ʷ�ۼ�����ǰ��¼����
		/// </summary>
		private string[][] m_strValueArr;

		/// <summary>
		/// ��ǰ��¼�����������޸ĵĴε�����
		/// </summary>
		private int m_intNowRowInOneRecord=0; 	

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
			public string strDeptName;
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
			TopY = 200,
			///<summary>
			/// ���ӵ����
			/// </summary>
			LeftX = 5,
			/// <summary>
			/// ���ӵ��Ҷ�
			/// </summary>
			RightX = 820-35,
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
			ColumnsNum=19,
			/// <summary>
			/// ��һ�������(X),ʱ�䣨����ߣ�
			/// </summary>			
			ColumnsMark1=75,

			/// <summary>
			/// �ڶ��������(X)�����£�����ߣ�
			/// </summary>
			ColumnsMark2=120,

			/// <summary>
			/// ��3�������(X)�����ɣ�����ߣ�
			/// </summary>
			ColumnsMark3=154,

			/// <summary>
			/// ���� ��/�֣�����ߣ�
			/// </summary>
			ColumnsMark4=194,

			/// <summary>
			/// ����������ߣ�
			/// </summary>
			ColumnsMark5=224,

			/// <summary>
			/// ����������ߣ�
			/// </summary>
			ColumnsMark6=254,

			/// <summary>
			/// Ѫѹ������ߣ�
			/// </summary>
			ColumnsMark7=284,

			/// <summary>
			/// ͫ�״�С ������ߣ�
			/// </summary>
			ColumnsMark8=340,

			/// <summary>
			/// ͫ�״�С �ң�����ߣ�
			/// </summary>
			ColumnsMark9=370,

			/// <summary>
			/// ���� ������ߣ�
			/// </summary>
			ColumnsMark10=400,

			/// <summary>
			/// ���� �ң�����ߣ�
			/// </summary>
			ColumnsMark11=440,

			/// <summary>
			/// Ѫ�����Ͷȣ�����ߣ�
			/// </summary>
			ColumnsMark12=480,

			/// <summary>
			/// ����Ѫ�ǣ�����ߣ�
			/// </summary>
			ColumnsMark13=510,

			/// <summary>
			/// ��Һ��������ߣ�
			/// </summary>
			ColumnsMark14=550,

			/// <summary>
			/// ��ʳ��������ߣ�
			/// </summary>
			ColumnsMark15=580,

			/// <summary>
			/// ������������ߣ�
			/// </summary>
			ColumnsMark16=610,

			/// <summary>
			/// �� ��������ߣ�
			/// </summary>
			ColumnsMark17=640,

			ColumnsMark18=670,

			ColumnsMark19=700,

			/// <summary>
			/// ǩ��������ߣ�
			/// </summary>
			ColumnsMark20=730	
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
						m_fReturnPoint = new PointF(60f,150f);
						break;

					case (int)enmItemDefination.Sex_Title :
						m_fReturnPoint = new PointF(130f,150f);
						break;
					case (int)enmItemDefination.Sex :
						m_fReturnPoint = new PointF(170f,150f);
						break;

					case (int)enmItemDefination.Age_Title :
						m_fReturnPoint = new PointF(200f,150f);
						break;
					case (int)enmItemDefination.Age:
						m_fReturnPoint = new PointF(240f,150f);
						break;

					case (int)enmItemDefination.Dept_Name_Title:
						m_fReturnPoint = new PointF(280f,150f);
						break;
					case (int)enmItemDefination.Dept_Name :
						m_fReturnPoint = new PointF(320f,150f);
						break;
					
					case (int)enmItemDefination.BedNo_Title :
						m_fReturnPoint = new PointF(600f,150f);
						break;
					case (int)enmItemDefination.BedNo:
						m_fReturnPoint = new PointF(640f,150f);
						break;

					case (int)enmItemDefination.InPatientID_Title:
						m_fReturnPoint = new PointF(680f,150f);
						break;
					case (int)enmItemDefination.InPatientID :
						m_fReturnPoint = new PointF(740f,150f);
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

		#region ��ӡ
		private clsWatchItemDataInfo[] m_objPrintDataArr;
		/// <summary>
		/// ���ô�ӡ���ݡ�
		/// </summary>
		/// <param name="p_objTransDataArr"></param>
		/// <param name="p_dtmFirstPrintDate"></param>
		private void m_mthSetPrintContent(clsTransDataInfo[] p_objTransDataArr,
			DateTime[] p_dtmFirstPrintDate)
		{
			if(p_objTransDataArr==null || p_dtmFirstPrintDate==null ||p_objTransDataArr.Length !=p_dtmFirstPrintDate.Length)
			{
				MDIParent.ShowInformationMessageBox("��ӡ��������!");
				return;
			}
			ArrayList m_arlTemp = new ArrayList();
			for(int i1=0;i1<p_objTransDataArr.Length;i1++)
			{
				m_arlTemp.Add(p_objTransDataArr[i1]);
			}
			m_objPrintDataArr = (clsWatchItemDataInfo[])m_arlTemp.ToArray(typeof(clsWatchItemDataInfo));
		}

		// ��ӡ��ʼ���ڴ�ӡҳ֮ǰ�Ĳ���
		private void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
		{
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

				while(m_intCurrentRecord < m_objPrintDataArr.Length)
				{				
					if(m_intCurrentRecord==0)
						m_intSetPrintOneValueRows(p_objPrintPageArg,ref m_intPosY);
					m_blnBeginPrintNewRecord = m_blnPrintOneValue(p_objPrintPageArg, m_intPosY);	
				
					//б��
					p_objPrintPageArg.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8 ,
						m_intPosY-intTempDeltaY,
						(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7,
						m_intPosY);			

					if(m_blnBeginPrintNewRecord)
					{
						m_intCurrentRecord++;
					
						m_mthPrintOneHorizontalLine(p_objPrintPageArg,m_intPosY);

						int intMaxRows=m_intSetPrintOneValueRows(p_objPrintPageArg,ref m_intPosY);
						if(m_intPosY + intMaxRows*intTempDeltaY >= 1100	&& m_intCurrentRecord < m_objPrintDataArr.Length)
						{
							p_objPrintPageArg.HasMorePages = true;				

							//Print VLine
							m_mthPrintVLines(p_objPrintPageArg,m_intPosY);
							m_mthPrintOneHorizontalLine(p_objPrintPageArg,m_intPosY);

							//ҳ��//////////////////////////////////////////////////////////////
							p_objPrintPageArg.Graphics.DrawString("����"+m_intNowPage.ToString()+"ҳ��",m_fotSmallFont ,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15-30 ,
								1092/*m_intPosY+(int)enmRecordRectangleInfo.VOffSet*/ );
           

							m_intPosY = (int)enmRecordRectangleInfo.TopY+150;
							m_intNowPage++;
							return;
					
						}
					}					
				
				}
				m_mthPrintVLines(p_objPrintPageArg,m_intPosY);
				//ҳ��//////////////////////////////////////////////////////////////
				p_objPrintPageArg.Graphics.DrawString("����"+m_intNowPage.ToString()+"ҳ��",m_fotSmallFont ,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15-30 ,
					1092/*m_intPosY+(int)enmRecordRectangleInfo.VOffSet*/ );
			
				#region ��ӡ��ϣ�ReSet(��λ)����
				if(m_intCurrentRecord==m_objPrintDataArr.Length)
				{	
					m_intPosY = (int)enmRecordRectangleInfo.TopY+3*m_intHeaderRowStep;
					m_intCurrentRecord=0;//��ǰ��¼����Ÿ�λ���Ա���һ�δ�ӡ����
					m_blnBeginPrintNewRecord=true;//��λ
					m_intNowPage=1;//��λ						
				}
				#endregion				
			}
			catch(Exception err)
			{
				MDIParent.ShowInformationMessageBox(err.Message + "\r\n" + err.StackTrace);

			}
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
			clsEveryRecordPageInfo objEveryRecordPageInfo=new clsEveryRecordPageInfo();
			//************************************************			
			objEveryRecordPageInfo.strAge =m_objPrintInfo.m_strAge;
			objEveryRecordPageInfo.strPatientName=m_objPrintInfo.m_strPatientName;
			objEveryRecordPageInfo.strDeptName=m_objPrintInfo.m_strDeptName;
			objEveryRecordPageInfo.strBedNo =m_objPrintInfo.m_strBedName;
			objEveryRecordPageInfo.strAreaName=m_objPrintInfo.m_strAreaName;
			objEveryRecordPageInfo.strSex=m_objPrintInfo.m_strSex;
			objEveryRecordPageInfo.strInPatientID=m_objPrintInfo.m_strHISInPatientID;
			objEveryRecordPageInfo.strPrintDate=( m_objPrintInfo.m_strInPatentID!="")? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") :"";		
			

			e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,m_fotSmallFont ,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_HospitalName  ));
            
			e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,m_fotSmallFont ,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_HospitalName  ));
		
			e.Graphics.DrawString("�� �� �� Ŀ �� ¼ ��",m_fotTitleFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_Name_Title ));
			

			e.Graphics.DrawString("������",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name_Title  ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strPatientName  ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name ));
		
			e.Graphics.DrawString("�Ա�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strSex ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex  ));

			e.Graphics.DrawString("���䣺",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strAge ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age ));

			e.Graphics.DrawString("���ң�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strDeptName ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name ));

			e.Graphics.DrawString("������",m_fotSmallFont,m_slbBrush,new PointF(440f,150f));
			e.Graphics.DrawString(objEveryRecordPageInfo.strAreaName ,m_fotSmallFont,m_slbBrush,new PointF(480f,150f));

			e.Graphics.DrawString("���ţ�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strBedNo ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo ));	
			
			e.Graphics.DrawString("סԺ�ţ�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strInPatientID ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID ));	
						
		}
		#endregion

		#region ����ͷ����
		/// <summary>
		///  ����ͷ����
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintRectangleInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{
			int m_intHeaderRowStep=50;
			
			//�����Ӻ���
			for(int i1=0;i1<4 /*(int)enmRecordRectangleInfo.RowLinesNum*/ ;i1++)
			{
				if(i1 !=1 && i1 !=2)
					e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX ,
						(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep*i1,
						(int)enmRecordRectangleInfo.RightX,
						(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep*i1);
				else if(i1==1)
				{
					e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+ (int)enmRecordRectangleInfo.ColumnsMark8,
						(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep*i1-5,
						(int)enmRecordRectangleInfo.LeftX+ (int)enmRecordRectangleInfo.ColumnsMark12,
						(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep*i1-5);
					e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+ (int)enmRecordRectangleInfo.ColumnsMark14,
						(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep*i1-5,
						(int)enmRecordRectangleInfo.LeftX+ (int)enmRecordRectangleInfo.ColumnsMark20,
						(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep*i1-5);
				}
				else //if(i1==2)
					e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+ (int)enmRecordRectangleInfo.ColumnsMark8,
						(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep*i1,
						(int)enmRecordRectangleInfo.LeftX+ (int)enmRecordRectangleInfo.ColumnsMark12,
						(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep*i1);
			}
			
			#region ����������
			int intHeight=3*m_intHeaderRowStep;
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
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8,(int)enmRecordRectangleInfo.TopY+intHeight);
			//ͫ�״�С���ҷֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark9,(int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark9,(int)enmRecordRectangleInfo.TopY+intHeight);
			//ͫ�״�С�뷴��ֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark10,(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep-5,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark10,(int)enmRecordRectangleInfo.TopY+intHeight);
			//ͫ�׷������ҷֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark11,(int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark11,(int)enmRecordRectangleInfo.TopY+intHeight);			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark12,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark12,(int)enmRecordRectangleInfo.TopY+intHeight);
			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark13,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark13,(int)enmRecordRectangleInfo.TopY+intHeight);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark14,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark14,(int)enmRecordRectangleInfo.TopY+intHeight);
			
			//�����м�ֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15,(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep-5,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15,(int)enmRecordRectangleInfo.TopY+intHeight);			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark16,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark16,(int)enmRecordRectangleInfo.TopY+intHeight);
			//�ų��м�ֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark17,(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep-5,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark17,(int)enmRecordRectangleInfo.TopY+intHeight);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark18,(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep-5,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark18,(int)enmRecordRectangleInfo.TopY+intHeight);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark19,(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep-5,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark19,(int)enmRecordRectangleInfo.TopY+intHeight);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark20,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark20,(int)enmRecordRectangleInfo.TopY+intHeight);
			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.RightX ,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.RightX,(int)enmRecordRectangleInfo.TopY+intHeight);
			#endregion				

			#region ��ӡ�ձ���
			if(m_objPrintInfo.m_strInPatentID =="" || m_objPrintDataArr==null || m_objPrintDataArr.Length==0)
			{					
				while(m_intPosY < 1060)
				{
					m_intPosY += (int)enmRecordRectangleInfo.RowStep;

					//б��
					e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8 ,
						m_intPosY-intTempDeltaY,
						(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7,
						m_intPosY);		

					//ˮƽ��
					m_mthPrintOneHorizontalLine(e,m_intPosY);
				}
	
				//Print VLine
				m_mthPrintVLines(e,m_intPosY);
				m_mthPrintOneHorizontalLine(e,m_intPosY);

				//ҳ��//////////////////////////////////////////////////////////////
				e.Graphics.DrawString("���� 1 ҳ��",m_fotSmallFont ,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15-30 ,
					m_intPosY+(int)enmRecordRectangleInfo.VOffSet );
	
				//��λ
				m_intPosY = (int)enmRecordRectangleInfo.TopY+3*m_intHeaderRowStep;
						
				return;
			}
			#endregion
			
		}

						
		#endregion		

		#region ���������Ŀ
		private int m_intHeaderRowStep=50;
		/// <summary>
		/// ���������Ŀ
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintHeaderInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{			
		
			e.Graphics.DrawString("����",m_fotHeaderFont ,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+15,
				(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5);
		     
			e.Graphics.DrawString("ʱ��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark1+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5);

			//���� C			
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark2+1, (int)enmRecordRectangleInfo.TopY+7);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark2+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark2+1, (int)enmRecordRectangleInfo.TopY-10+2*m_intHeaderRowStep+5);
			e.Graphics.DrawString("C",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark2+9, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep+1+5);

			// ����			
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark3+5, (int)enmRecordRectangleInfo.TopY+7);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark3+5, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5);
			
			//����(��/��)
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark4+1, (int)enmRecordRectangleInfo.TopY+7);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark4+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep-13);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark4+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+15);
			e.Graphics.DrawString("/",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark4+5, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep-7);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark4+1, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep+13);

			//����(��/��)
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark5+1, (int)enmRecordRectangleInfo.TopY+7);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark5+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep-13);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark5+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+15);
			e.Graphics.DrawString("/",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark5+5, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep-7);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark5+1, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep+13);

			//����(��/��)
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark6+1, (int)enmRecordRectangleInfo.TopY+7);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark6+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep-13);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark6+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+15);
			e.Graphics.DrawString("/",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark6+5, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep-7);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark6+1, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep+13);

			//Ѫѹ(mmHg)
			e.Graphics.DrawString("Ѫѹ",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark7+8, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep-15);
			e.Graphics.DrawString("mmHg",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark7+8, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep-10);
	
			e.Graphics.DrawString(" ͫ ��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark8+31, (int)enmRecordRectangleInfo.TopY+7);
			e.Graphics.DrawString("��С",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark8+10, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5-5);
			e.Graphics.DrawString("(mm)",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark8+10, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5+15);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark8+1, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep+5);

			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark9+1, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep+5);


			e.Graphics.DrawString("����",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark10+18, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5);

			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark10+1, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep+5);

			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark11+1, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep+5);

			//Ѫ�����Ͷ�(%)
			e.Graphics.DrawString("Ѫ",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark12+2, (int)enmRecordRectangleInfo.TopY+4);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark12+2, (int)enmRecordRectangleInfo.TopY+3*m_intHeaderRowStep*1/6);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark12+2, (int)enmRecordRectangleInfo.TopY+3*m_intHeaderRowStep*2/6);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark12+2, (int)enmRecordRectangleInfo.TopY+3*m_intHeaderRowStep*3/6);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark12+2, (int)enmRecordRectangleInfo.TopY+3*m_intHeaderRowStep*4/6);
			e.Graphics.DrawString("(%)",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark12, (int)enmRecordRectangleInfo.TopY+3*m_intHeaderRowStep*5/6);

			//����Ѫ��(mmol/L)
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark13+6, (int)enmRecordRectangleInfo.TopY+4);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark13+6, (int)enmRecordRectangleInfo.TopY+3*m_intHeaderRowStep*1/6);
			e.Graphics.DrawString("Ѫ",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark13+6, (int)enmRecordRectangleInfo.TopY+3*m_intHeaderRowStep*2/6);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark13+6, (int)enmRecordRectangleInfo.TopY+3*m_intHeaderRowStep*3/6);
			e.Graphics.DrawString("mmol",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark13+2, (int)enmRecordRectangleInfo.TopY+3*m_intHeaderRowStep*4/6);
			e.Graphics.DrawString("/L",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark13+6, (int)enmRecordRectangleInfo.TopY+3*m_intHeaderRowStep*5/6);


			e.Graphics.DrawString("����",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark15-20, (int)enmRecordRectangleInfo.TopY+4);
			e.Graphics.DrawString("(ml)",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark15-20, (int)enmRecordRectangleInfo.TopY+25);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark14+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5);
			e.Graphics.DrawString("Һ",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark14+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+38);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark14+1, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep+20);

			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark15+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5);
			e.Graphics.DrawString("ʳ",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark15+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+38);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark15+1, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep+20);


			e.Graphics.DrawString("�ų�",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark18-20, (int)enmRecordRectangleInfo.TopY+4);
			e.Graphics.DrawString("(ml)",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark18-20, (int)enmRecordRectangleInfo.TopY+25);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark16+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark16+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+38);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark16+1, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep+20);

			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark17+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark17+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+38);

			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark18+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark18+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+38);

			e.Graphics.DrawString("Ż",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark19+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark19+1, (int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+38);
			e.Graphics.DrawString("��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark19+1, (int)enmRecordRectangleInfo.TopY+2*m_intHeaderRowStep+20);


			e.Graphics.DrawString("ǩ��",m_fotHeaderFont,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+
				(int)enmRecordRectangleInfo.ColumnsMark20+1,(int)enmRecordRectangleInfo.TopY+m_intHeaderRowStep+5);
		
		
		}
		#endregion

		#region ��ӡ���еĴ�ֱ��
		/// <summary>
		/// ��ӡ���еĴ�ֱ��
		/// </summary>
		/// <param name="e"></param>
		/// <param name="p_intPageBottomY"></param>
		private void m_mthPrintVLines(PrintPageEventArgs e,int p_intPageBottomY)
		{			
			#region ����������
			int intContentTopY=(int)enmRecordRectangleInfo.TopY+ 150;
			//���������
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX,p_intPageBottomY);
			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark1,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark1,p_intPageBottomY);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark2,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark2,p_intPageBottomY);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark3,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark3,p_intPageBottomY);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark4,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark4,p_intPageBottomY);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark5,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark5,p_intPageBottomY);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark6,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark6,p_intPageBottomY);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7,p_intPageBottomY);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8,p_intPageBottomY);
			//ͫ�״�С���ҷֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark9,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark9,p_intPageBottomY);
			//ͫ�״�С�뷴��ֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark10,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark10,p_intPageBottomY);
			//ͫ�׷������ҷֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark11,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark11,p_intPageBottomY);			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark12,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark12,p_intPageBottomY);
			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark13,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark13,p_intPageBottomY);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark14,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark14,p_intPageBottomY);
			
			//�����м�ֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15,p_intPageBottomY);			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark16,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark16,p_intPageBottomY);
			//�ų��м�ֽ���
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark17,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark17,p_intPageBottomY);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark18,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark18,p_intPageBottomY);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark19,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark19,p_intPageBottomY);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark20,intContentTopY,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark20,p_intPageBottomY);
			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.RightX ,intContentTopY,
				(int)enmRecordRectangleInfo.RightX,p_intPageBottomY);
			#endregion		
		}
		#endregion

		#region ��ӡһ��ˮƽ��
		/// <summary>
		/// ��ӡһ��ˮƽ��
		/// </summary>
		/// <param name="e"></param>
		/// <param name="p_intBottomY"></param>
		private void m_mthPrintOneHorizontalLine(System.Drawing.Printing.PrintPageEventArgs e,int p_intBottomY)
		{			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX ,
				p_intBottomY,
				(int)enmRecordRectangleInfo.RightX,
				p_intBottomY);			
		}
		#endregion

		#region ֻ��ӡһ��
		/// <summary>
		/// ֻ��ӡһ��
		/// </summary>
		/// <param name="e"></param>
		/// <param name="p_intBottomY"></param>
		/// <returns></returns>
		private bool m_blnPrintOneValue(System.Drawing.Printing.PrintPageEventArgs e,int p_intBottomY)
		{			
			p_intBottomY +=(int)enmRecordRectangleInfo.VOffSet;
			#region ������¼�¼����ӡ����
			if(m_blnBeginPrintNewRecord==true) 
			{
				m_intNowRowInOneRecord=0;

				//��������
				string strCreateDate;
				string strCreateTime;
				string strCreateDateTime;
				
				if(m_objPrintDataArr[m_intCurrentRecord].m_intFlag != (int)enmRecordsType.WatchItem && m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr == null)
				{
					strCreateDate = "";
					strCreateTime = "";
					strCreateDateTime = "";
				}
				else
				{
					strCreateDateTime=m_objPrintDataArr[m_intCurrentRecord].m_objRecordContent.m_dtmCreateDate.ToString("yyyy-MM-dd HH:mm:ss");
					try
					{
						strCreateDate=DateTime.Parse(strCreateDateTime).ToString("yyyy-M-d");
						strCreateTime=DateTime.Parse(strCreateDateTime).ToString("HH:mm");
					}
					catch
					{strCreateDate="����";strCreateTime="����";}	
				}
				//��ʼ��ӡһ���¼�¼/////////////////////////////////////////////////////////////////////
				e.Graphics.DrawString(strCreateDate,m_fotSmallFont ,m_slbBrush,
					(int)enmRecordRectangleInfo.LeftX, 
					p_intBottomY);	
				e.Graphics.DrawString(strCreateTime,m_fotSmallFont ,m_slbBrush,
					(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark1+1, 
					p_intBottomY );	
			}
			#endregion			
			
			#region ���޸�˳���ӡ��ǰ��¼��ĳһ��	
			bool blnIsRecordFinish=m_blnPrintOneRowValue(m_strValueArr,m_intNowRowInOneRecord,e,p_intBottomY);
			
			#region ǩ���������޸ĵ���ǩ����
			string strSign = "";
			if(m_objPrintDataArr[m_intCurrentRecord].m_intFlag != (int)enmRecordsType.WatchItem && m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr == null)
				strSign = "";
//			else if((m_intNowRowInOneRecord+1 <m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr.Length))
//			{
//				if((m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[m_intNowRowInOneRecord].m_strModifyUserName) != (m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[m_intNowRowInOneRecord+1].m_strModifyUserName))
//					strSign = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[m_intNowRowInOneRecord].m_strModifyUserName;
//				
//			}
			else
				strSign = m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[m_intNowRowInOneRecord].m_strModifyUserName;
				//			clsEmployee objclsEmployee=new clsEmployee(m_objclsWatchItemRecordContent_AllArr[m_intCurrentRecord].m_objclsWatchItemRecordContentArr[m_intNowRowInOneRecord].m_strModifyUserID);
			//			if(objclsEmployee!=null)
			//				strSign=objclsEmployee.m_StrFirstName;			
			e.Graphics.DrawString(strSign ,m_fotSmallFont ,m_slbBrush,
				(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark20+1, 
				p_intBottomY);
			#endregion

			m_blnBeginPrintNewRecord=blnIsRecordFinish;//��ǰ��¼�Ƿ����					
			m_intNowRowInOneRecord++;
			#endregion

			m_intPosY += intTempDeltaY;
			return blnIsRecordFinish;			
		}

		#endregion
	
		#region Liyi
		/// <summary>
		/// ��ӡһ��ʱ���¼��һ����ֵ�������Ѫѹб�ߵĴ�ӡ��
		/// </summary>
		/// <param name="p_strValueArr">��ֵ(�ӡ����¡�����Ż�������19��)</param>
		/// <param name="p_intIndex">�ڼ��εĽ��</param>
		/// <param name="e">��ӡ����</param>
		/// <param name="p_intPosY">Y����</param>
		private bool m_blnPrintOneRowValue(string [][] p_strValueArr,int p_intIndex,System.Drawing.Printing.PrintPageEventArgs e,int p_intPosY)
		{			
			string [] strValueArr = p_strValueArr[p_intIndex];

			if(p_strValueArr[0][12] == "�ܼ�:")
			{
				return m_blnPrintOneRowValueOfSummary(p_strValueArr,p_intIndex,e,p_intPosY);
			}

			CharacterRange []rgnDSTArr = new CharacterRange[1];
			rgnDSTArr[0] = new CharacterRange(0,0);			
				
			RectangleF rtfText = new RectangleF(0,0,10000,100);

			StringFormat stfMeasure = new StringFormat(StringFormatFlags.LineLimit);			

			RectangleF rtfBounds;

			Region [] rgnDST;

			int intTempColumn=0;//��ǰ����������ԣ�
			int intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark2;//��ǰ��X����
			//����
			#region ��ӡһ�񣬣�������ȫ��ͬ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��

			intTempColumn=1;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark3;//��ǰ��X����
			//����
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��

			intTempColumn=2;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark4;//��ǰ��X����
			//����
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��

			intTempColumn=3;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark5;//��ǰ��X����
			//����
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��

			intTempColumn=4;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark6;//��ǰ��X����
			//����
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��

			
			bool blnIsLastModify=false;
			if( p_intIndex == p_strValueArr.Length-1 || (strValueArr[5] == p_strValueArr[p_intIndex+1][5] && strValueArr[6] == p_strValueArr[p_intIndex+1][6] && strValueArr[5] == p_strValueArr[p_strValueArr.Length-1][5] && strValueArr[6] == p_strValueArr[p_strValueArr.Length-1][6] ))
			{// ��������һ�У����ҵ�ǰԪ�� != ��һ�д�Ԫ��				
				blnIsLastModify=true;					
			}
			//Ѫѹ(����ѹ)
			if(strValueArr[5].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[5],m_fotSmallFont,Brushes.Black,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7,p_intPosY-15);
				if( ! blnIsLastModify)
				{					
					rtfText.X = (int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7;
					rtfText.Y = p_intPosY-15;

					rgnDSTArr[0].First = 0;
					rgnDSTArr[0].Length = strValueArr[5].Length;

					stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

					rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[5],m_fotSmallFont,rtfText,stfMeasure);

					rtfBounds = rgnDST[0].GetBounds(e.Graphics);

					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					
				}
			}	
			
			//Ѫѹ(����ѹ)
			if(strValueArr[6].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[6],m_fotSmallFont,Brushes.Black,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7+30,p_intPosY);
				if( ! blnIsLastModify)
				{					
					rtfText.X = (int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark7+30;
					rtfText.Y = p_intPosY;

					rgnDSTArr[0].First = 0;
					rgnDSTArr[0].Length = strValueArr[6].Length;

					stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

					rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[6],m_fotSmallFont,rtfText,stfMeasure);

					rtfBounds = rgnDST[0].GetBounds(e.Graphics);

					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
					e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					
				}
			}

			intTempColumn=7;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8;//��ǰ��X����
			//ͫ�״�С����
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark9;//��ǰ��X����
			//ͫ�״�С���ң�
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark10;//��ǰ��X����
			//ͫ�׷��䣨��
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��		

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark11;//��ǰ��X����
			//ͫ�׷��䣨�ң�
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��		
			
			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark12;//��ǰ��X����
			//Ѫ�����Ͷ�
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��		
			
			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark13;//��ǰ��X����
			//����Ѫ��
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��		

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark14;//��ǰ��X����
			//����Һ��
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��					

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15;//��ǰ��X����
			//��ʳ��
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��								

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark16;//��ǰ��X����
			//������
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��								

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark17;//��ǰ��X����
			//����
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��								

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark18;//��ǰ��X����
			//���
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��								

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark19;//��ǰ��X����
			//Ż����
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				if(p_intIndex+1 < p_strValueArr.Length)
				{
					if(strValueArr[intTempColumn] != p_strValueArr[p_intIndex+1][intTempColumn])
					{
						rtfText.X = intPosX;
						rtfText.Y = p_intPosY;

						rgnDSTArr[0].First = 0;
						rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

						stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

						rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

						rtfBounds = rgnDST[0].GetBounds(e.Graphics);

						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2);
						e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+3,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+3);
					}
				}
			}
			#endregion	��ӡһ��								

			return p_intIndex==p_strValueArr.Length-1;
		}
		#endregion


		#region Alex
		/// <summary>
		/// ��ӡһ��ʱ���¼��һ����ֵ�������Ѫѹб�ߵĴ�ӡ��
		/// </summary>
		/// <param name="p_strValueArr">��ֵ(�ӡ����¡�����Ż�������19��)</param>
		/// <param name="p_intIndex">�ڼ��εĽ��</param>
		/// <param name="e">��ӡ����</param>
		/// <param name="p_intPosY">Y����</param>
		private bool m_blnPrintOneRowValueOfSummary(string [][] p_strValueArr,int p_intIndex,System.Drawing.Printing.PrintPageEventArgs e,int p_intPosY)
		{			
			string [] strValueArr = p_strValueArr[p_intIndex];

			CharacterRange []rgnDSTArr = new CharacterRange[1];
			rgnDSTArr[0] = new CharacterRange(0,0);			
				
			RectangleF rtfText = new RectangleF(0,0,10000,100);

			StringFormat stfMeasure = new StringFormat(StringFormatFlags.LineLimit);			

			RectangleF rtfBounds;

			Region [] rgnDST;

			int intTempColumn=0;//��ǰ����������ԣ�
			int intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark2;//��ǰ��X����
			//����

			intTempColumn=1;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark3;//��ǰ��X����
			//����

			intTempColumn=2;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark4;//��ǰ��X����
			//����

			intTempColumn=3;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark5;//��ǰ��X����
			//����
			intTempColumn=4;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark6;//��ǰ��X����
			//����

			intTempColumn=7;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark8;//��ǰ��X����
			//ͫ�״�С����

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark9;//��ǰ��X����
			//ͫ�״�С���ң�

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark10;//��ǰ��X����
			//ͫ�׷��䣨��

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark11;//��ǰ��X����
			//ͫ�׷��䣨�ң�
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
			}
			#endregion	��ӡһ��		
			
			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark12;//��ǰ��X����
			//Ѫ�����Ͷ�
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
			}
			#endregion	��ӡһ��		
			
			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark13;//��ǰ��X����
			//����Ѫ��
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
			}
			#endregion	��ӡһ��		

			intTempColumn++;
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark14;//��ǰ��X����
			//����Һ��
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);
				rtfText.X = intPosX;
				rtfText.Y = p_intPosY;

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
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark15;//��ǰ��X����
			//��ʳ��
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);

				rtfText.X = intPosX;
				rtfText.Y = p_intPosY;

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
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark16;//��ǰ��X����
			//������
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);

				rtfText.X = intPosX;
				rtfText.Y = p_intPosY;

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
			//����
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);

				rtfText.X = intPosX;
				rtfText.Y = p_intPosY;

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
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark18;//��ǰ��X����
			//���
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);

				rtfText.X = intPosX;
				rtfText.Y = p_intPosY;

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
			intPosX=(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark19;//��ǰ��X����
			//Ż����
			#region ��ӡһ��
			if(strValueArr[intTempColumn].Trim().Length != 0)
			{
				e.Graphics.DrawString(strValueArr[intTempColumn],m_fotSmallFont,Brushes.Black,intPosX,p_intPosY);

				rtfText.X = intPosX;
				rtfText.Y = p_intPosY;

				rgnDSTArr[0].First = 0;
				rgnDSTArr[0].Length = strValueArr[intTempColumn].Length;

				stfMeasure.SetMeasurableCharacterRanges(rgnDSTArr);					

				rgnDST = e.Graphics.MeasureCharacterRanges(strValueArr[intTempColumn],m_fotSmallFont,rtfText,stfMeasure);

				rtfBounds = rgnDST[0].GetBounds(e.Graphics);

				e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+6,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+6);
				e.Graphics.DrawLine(Pens.Red,rtfBounds.X,rtfBounds.Y+rtfBounds.Height/2+9,rtfBounds.X+rtfBounds.Width,rtfBounds.Y+rtfBounds.Height/2+9);

			}
			#endregion	��ӡһ��								

			return p_intIndex==p_strValueArr.Length-1;
		}
		#endregion

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
			//
			//			if(m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr==null || m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr.Length==0)
			//				return 0;

			if(m_objPrintDataArr[m_intCurrentRecord].m_intFlag == (int)enmRecordsType.WatchItem && m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr==null)
				return 0;
			//			int intRowsOfOneRecord=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr.Length;
			//			string strModifyDate=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[intRowsOfOneRecord-1].m_dtmModifyDate.ToString("yyyy-MM-dd HH:mm:ss") ;

			int intRowsOfOneRecord;
			string strModifyDate;
		
			try
			{
				#region ������¼�¼���ж��Ƿ����ۼ�
				int intLenth;
				if(m_blnBeginPrintNewRecord==true) 
				{									
					#region ��ǰ��¼���鸳ֵ
					if(m_objPrintDataArr[m_intCurrentRecord].m_intFlag != (int)enmRecordsType.WatchItem && m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr == null)
					{
						intLenth = 2;
						intRowsOfOneRecord = intLenth;
						strModifyDate = "";
						m_strValueArr=new string[intLenth][];
						m_strValueArr[0]=new string[19];
						m_strValueArr[0][0]="";
						m_strValueArr[0][1]="";
						m_strValueArr[0][2]="";
						m_strValueArr[0][3]="";
						m_strValueArr[0][4]="";
						m_strValueArr[0][5]="";
						m_strValueArr[0][6]="";
						m_strValueArr[0][7]="";
						m_strValueArr[0][8]="";
						m_strValueArr[0][9]="";
						if(m_objPrintDataArr[m_intCurrentRecord].m_objRecordContent.m_dtmCreateDate == DateTime.MaxValue)
							m_strValueArr[0][10]="�Ϲ�";
						else
							m_strValueArr[0][10]="����";
						m_strValueArr[0][11]="����";
						m_strValueArr[0][12]="�ܼ�:";
						m_strValueArr[0][13]=(m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intInI_Total == 0?"":m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intInI_Total.ToString());
						m_strValueArr[0][14]=(m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intInD_Total == 0?"":m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intInD_Total.ToString());
						m_strValueArr[0][15]=(m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intOutE_Total == 0?"":m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intOutE_Total.ToString());
						m_strValueArr[0][16]=(m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intOutU_Total == 0?"":m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intOutU_Total.ToString());
						m_strValueArr[0][17]=(m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intOutS_Total == 0?"":m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intOutS_Total.ToString());
						m_strValueArr[0][18]=(m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intOutV_Total == 0?"":m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intOutV_Total.ToString());
					
						m_strValueArr[1]=new string[19];
						m_strValueArr[1][0]="";
						m_strValueArr[1][1]="";
						m_strValueArr[1][2]="";
						m_strValueArr[1][3]="";
						m_strValueArr[1][4]="";
						m_strValueArr[1][5]="";
						m_strValueArr[1][6]="";
						m_strValueArr[1][7]="";
						m_strValueArr[1][8]="";
						m_strValueArr[1][9]="";
						if(m_objPrintDataArr[m_intCurrentRecord].m_objRecordContent.m_dtmCreateDate == DateTime.MaxValue)
							m_strValueArr[1][10]="�Ϲ�";
						else
							m_strValueArr[1][10]="����";
						m_strValueArr[1][11]="����";
						m_strValueArr[1][12]="�ܼ�:";
						m_strValueArr[1][13]=(m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intTotal_In == 0?"":m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intTotal_In.ToString());
						m_strValueArr[1][14]="";
						m_strValueArr[1][15]=(m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intTotal_Out == 0?"":m_objPrintDataArr[m_intCurrentRecord].m_objItemSummary.m_intTotal_Out.ToString());
						m_strValueArr[1][16]="";
						m_strValueArr[1][17]="";
						m_strValueArr[1][18]="";
						return intLenth;
					}
					else
					{
						intRowsOfOneRecord=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr.Length;
						strModifyDate=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[intRowsOfOneRecord-1].m_dtmModifyDate.ToString("yyyy-MM-dd HH:mm:ss");
						intLenth=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr.Length;
						m_strValueArr=new string[intLenth][];
						for(int k1=0;k1<intLenth;k1++)
						{
							m_strValueArr[k1]=new string[19];
							m_strValueArr[k1][0]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strTemperature;
							m_strValueArr[k1][1]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strHeartRhythm;
							m_strValueArr[k1][2]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strHeartFrequency;
							m_strValueArr[k1][3]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strPulse;
							m_strValueArr[k1][4]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strBreath;
							m_strValueArr[k1][5]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strBloodPressureS;
							m_strValueArr[k1][6]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strBloodPressureA;
							m_strValueArr[k1][7]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strPupilLeft;
							m_strValueArr[k1][8]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strPupilRight;
							m_strValueArr[k1][9]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strEchoLeft;
							m_strValueArr[k1][10]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strEchoRight;
							m_strValueArr[k1][11]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strBloodOxygenSaturation;
							m_strValueArr[k1][12]=m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_strBedsideBloodSugar;
							m_strValueArr[k1][13]=(m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_intInI == 0?"":m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_intInI.ToString());
							m_strValueArr[k1][14]=(m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_intInD == 0?"":m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_intInD.ToString());
							m_strValueArr[k1][15]=(m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_intOutE == 0?"":m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_intOutE.ToString());
							m_strValueArr[k1][16]=(m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_intOutU == 0?"":m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_intOutU.ToString());
							m_strValueArr[k1][17]=(m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_intOutS == 0?"":m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_intOutS.ToString());
							m_strValueArr[k1][18]=(m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_intOutV == 0?"":m_objPrintDataArr[m_intCurrentRecord].m_objTransDataArr[k1].m_intOutV.ToString());
						}
						return intLenth;
					}
					#endregion
				}
				else 
					return 0;
				#endregion
			}
			catch(Exception ex)
			{
				MDIParent.ShowInformationMessageBox(ex.Message);
				return 1;
			}			
		}
		#endregion
		#endregion

//		/// <summary>
//		/// ��ӡ��Ϣ.
//		/// </summary>
//		[Serializable]			
//		private class clsPrintInfo_WatchItem
//		{
//			public string m_strInPatentID;			
//			public string m_strPatientName;
//			public string m_strSex;
//			public string m_strAge;
//			public string m_strBedName;
//			public string m_strDeptName;
//			public string m_strAreaName;	
//			public DateTime m_dtmInPatientDate;
//			public DateTime m_dtmOpenDate;
//
//			public clsTransDataInfo[] m_objTransDataArr;			
//			public DateTime[] m_dtmFirstPrintDateArr;//Length��m_dtmFirstPrintDateArr.Length��ͬ.
//			public bool[] m_blnIsFirstPrintArr;//Length��m_dtmFirstPrintDateArr.Length��ͬ.
//			
//			public clsWatchItemDataInfo[] m_objPrintDataArr;
//		}

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
//		clsWatchItemTrackPrintTool objPrintTool;
//		private void m_mthDemoPrint_FromDataSource()
//		{	
//			objPrintTool=new clsWatchItemTrackPrintTool();
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
//			objPrintTool=new clsWatchItemTrackPrintTool();
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


