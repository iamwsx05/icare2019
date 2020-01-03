using System;
using weCare.Core.Entity;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;
using System.Xml;


namespace iCare
{
	/// <summary>
	/// Summary description for clsSubDiseaseTrackPrintTool. ���̼�¼
	/// </summary>
	public class clsSubDiseaseTrackPrintTool: infPrintRecord
	{

		#region Members
		private bool m_blnIsFromDataSource=true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
        private bool m_blnWantInit = true;
        /// <summary>
        /// �Ƿ��ӡ�޸ĺۼ�
        /// </summary>
        public static bool m_blnIsPrintMark = true;		
		private clsRecordsDomain m_objRecordsDomain;
		private clsPrintInfo_SubDiseaseTrack m_objPrintInfo;
		private string m_strServerTime;
		/// <summary>
		/// ���м�¼��
		/// </summary>
		private System.Data.DataTable m_dtbBlankRecord;
        private static List<string> m_lstNewPagePrintTitle = new List<string>();
		#endregion

        #region ʵ�ֽӿڵĺ���
        static clsSubDiseaseTrackPrintTool()
        {
            if (m_lstNewPagePrintTitle.Count == 0)
            {
                m_lstNewPagePrintTitle = com.digitalwave.Emr.StaticObject.clsConfigTools.s_lstGetEmrConfigValue("/EMR/DiseaseTrack/NewPage/*[name()='Title']");
                if (m_lstNewPagePrintTitle.Count == 0)
                    m_lstNewPagePrintTitle.Add("");
            }
        }
		public clsSubDiseaseTrackPrintTool()
		{
		}
        clsPatient m_objCurrentPatient = null;
		/// <summary>
		/// ���ô�ӡ��Ϣ
		/// </summary>
		/// <param name="p_objPatient">����</param>
		/// <param name="p_dtmInPatientDate">��Ժ����</param>
		/// <param name="p_dtmOpenDate">OpenDate�������һ�δ�ӡ��μ�¼�������ͣ��粡����¼��������OpenDate</param>
		public void m_mthSetPrintInfo(clsPatient p_objPatient,DateTime p_dtmInPatientDate,DateTime p_dtmOpenDate)
		{
			m_blnIsFromDataSource=true;//�����Ǵ����ݿ��ȡ
			clsPatient m_objPatient=p_objPatient;
            m_objCurrentPatient = p_objPatient;
			m_objPrintInfo=new clsPrintInfo_SubDiseaseTrack();
			m_objPrintInfo.m_strInPatentID=m_objPatient!=null? m_objPatient.m_StrInPatientID:"";					
			m_objPrintInfo.m_strPatientName=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrFirstName :"";
			m_objPrintInfo. m_strSex=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrSex:"";
			m_objPrintInfo. m_strAge=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrAge : "";
			m_objPrintInfo. m_strBedName=m_objPatient!=null? m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName :"";
			m_objPrintInfo. m_strDeptName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjDept.m_StrDeptName :"";
			m_objPrintInfo. m_strAreaName=m_objPatient!=null? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName:"";			
			m_objPrintInfo.m_dtmInPatientDate=p_dtmInPatientDate;
			m_objPrintInfo.m_dtmOpenDate=p_dtmOpenDate;
            m_objPrintInfo.m_strHISInPatientID = m_objPatient!=null?m_objPatient.m_StrHISInPatientID:"";
            m_objPrintInfo.m_dtmHISInDate = m_objPatient != null ? m_objPatient.m_DtmSelectedHISInDate : DateTime.MinValue;
			m_intGetBlankRecord();
			m_blnWantInit=true;

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
		/// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���
		/// </summary>
		public void m_mthInitPrintContent()
		{
			if(m_objPrintInfo.m_strInPatentID=="")
				return;
			m_strServerTime = new clsPublicDomain().m_strGetServerTime();

            m_objRecordsDomain = new clsRecordsDomain(enmRecordsType.DiseaseTrack);
				
			long lngRes = m_objRecordsDomain.m_lngGetPrintInfo(m_objPrintInfo.m_strInPatentID,m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"),out m_objPrintInfo.m_objTransDataArr,out m_objPrintInfo.m_dtmFirstPrintDateArr,out m_objPrintInfo.m_blnIsFirstPrintArr);
			if(lngRes <= 0)	return ;   
			//����¼ʱ��(CreateDate)���� 
			m_mthSortTransData(ref m_objPrintInfo.m_objTransDataArr,ref m_objPrintInfo.m_dtmFirstPrintDateArr,ref m_objPrintInfo.m_blnIsFirstPrintArr);
			//���ñ����ݵ���ӡ��
			m_mthSetPrintContent(m_objPrintInfo.m_objTransDataArr,m_objPrintInfo.m_dtmFirstPrintDateArr);
			m_objPrintInfo.m_objPrintDataArr=m_objPrintDataArr;			

			m_blnWantInit=false;
		}
		

		/// <summary>
		/// ����¼˳��(CreateDate)�������p_objTansDataInfoArr����
		/// </summary>
		/// <param name="p_objTansDataInfoArr"></param>
		protected void m_mthSortTransData(ref clsTransDataInfo[] p_objTansDataInfoArr,ref DateTime []p_dtmFirstPrintDateArr,ref bool [] p_blnIsFirstPrintArr)
		{
			clsTransDataSort [] objDataSort = new clsTransDataSort[p_objTansDataInfoArr.Length];

			for(int i=0;i<objDataSort.Length;i++)
			{
				objDataSort[i] = new clsTransDataSort();
				objDataSort[i].m_intIndex = i;
				objDataSort[i].m_Data = p_objTansDataInfoArr[i];
			}

			ArrayList m_arlSort = new ArrayList(objDataSort);
			m_arlSort.Sort();
			objDataSort = (clsTransDataSort[])m_arlSort.ToArray(typeof(clsTransDataSort));

			p_objTansDataInfoArr = new clsTransDataInfo[objDataSort.Length];
			DateTime [] dtmNewFirstPrintDateArr = new DateTime[objDataSort.Length];
			bool [] blnNewIsFirstPrintArr = new bool[objDataSort.Length];

			for(int i=0;i<objDataSort.Length;i++)
			{
				p_objTansDataInfoArr[i] = objDataSort[i].m_Data;
				dtmNewFirstPrintDateArr[i] = p_dtmFirstPrintDateArr[objDataSort[i].m_intIndex];
				blnNewIsFirstPrintArr[i] = p_blnIsFirstPrintArr[objDataSort[i].m_intIndex];
			}

			p_dtmFirstPrintDateArr = dtmNewFirstPrintDateArr;
			p_blnIsFirstPrintArr = blnNewIsFirstPrintArr;			
		}

		private class clsTransDataSort : IComparable
		{
			public int m_intIndex;

			public clsTransDataInfo m_Data;

			public int CompareTo(object p_objValue)
			{
				clsTransDataSort objDiff = (clsTransDataSort)p_objValue;
				
				return this.m_Data.CompareTo(objDiff.m_Data);
			}
		}

		/// <summary>
		/// ���ô�ӡ���ݡ��������Ѿ�����ʱʹ�á�
		/// </summary>
		/// <param name="p_objPrintContent">��ӡ����</param>
		public void m_mthSetPrintContent(object p_objPrintContent)
		{
			if(p_objPrintContent.GetType().Name !="clsPrintInfo_SubDiseaseTrack")
			{
				MDIParent.ShowInformationMessageBox("��������");
			}
			m_blnIsFromDataSource=false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ
			m_objPrintInfo=(clsPrintInfo_SubDiseaseTrack)p_objPrintContent;
			m_objPrintDataArr= m_objPrintInfo. m_objPrintDataArr ;		
			m_strServerTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			m_blnWantInit=false;
		}

		/// <summary>
		/// ��ȡ��ӡ����
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

			//û�м�¼����ʱ�����ؿ�
			if(m_objPrintInfo.m_objPrintDataArr == null || m_objPrintInfo.m_objPrintDataArr.Length == 0)
				return null;
			else
				return m_objPrintInfo;
		}
		
		/// <summary>
		/// ��ʼ����ӡ����
		/// </summary>
		/// <param name="p_objArg">�ⲿ��Ҫ��ʼ���ı��������ݲ�ͬ��ʵ��ʹ��</param>
		public void m_mthInitPrintTool(object p_objArg)
		{
//			m_fotTitleFont = new Font("SimSun", 20,FontStyle.Bold );
			m_fotTitleFont = new Font("SimSun", 15.75F,FontStyle.Bold );//�������żӴ�
//			m_fotHeaderFont = new Font("SimSun", 18);
			m_fotHeaderFont = new Font("SimSun", 15.5F);//�������
			m_fotSmallFont = new Font("SimSun",12);
			
			m_GridPen = new Pen(Color.Black,1);
			m_slbBrush = new SolidBrush(Color.Black);
		
			m_objPageSetting = new clsPrintPageSettingForRecord();

			m_objPrintContext = new clsPrintRichTextContext(Color.Black,m_fotSmallFont);
			
			intCurrentRecord=0;
			intNowPage=0;
			blnBeginPrintNewRecord=true;	
		}

		/// <summary>
		/// �ͷŴ�ӡ����
		/// </summary>
		/// <param name="p_objArg">�ⲿʹ�õ��ı��������ݲ�ͬ��ʵ��ʹ��</param>
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
		/// <param name="p_objPrintArg"></param>
		public void m_mthBeginPrint(object p_objPrintArg)
		{
			m_mthBeginPrintSub((PrintEventArgs)p_objPrintArg);
		}

		/// <summary>
		/// ��ӡ��
		/// </summary>
		/// <param name="p_objPrintArg"></param>
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

		/// <summary>
		/// ��ӡ������һ��ʹ�������������ݿ���Ϣ��
		/// </summary>
		/// <param name="p_objPrintArg"></param>
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

		#endregion


		#region ��ӡ
		// ���ô�ӡ���ݡ�
		private void m_mthSetPrintContent(clsTransDataInfo[] p_objTransDataArr,
			DateTime[] p_dtmFirstPrintDate)
		{
			if(p_objTransDataArr==null || p_dtmFirstPrintDate==null ||p_objTransDataArr.Length !=p_dtmFirstPrintDate.Length)
			{
				MDIParent.ShowInformationMessageBox("��ӡ��������!");
				return;
			}
            int intBlankCount = 0;
            System.Data.DataTable dtbBlankRecord = null;
            new clsDiseaseTrackAddBlankDomain().m_lngGetBlankRecordContent(m_objPrintInfo.m_strInPatentID, m_objPrintInfo.m_dtmInPatientDate, out dtbBlankRecord);
			//���ݲ�ͬ�ı����ͣ���ȡ��Ӧ��clsDiseaseTrackInfo
			clsDiseaseTrackInfo objTrackInfo=null;
			m_objPrintDataArr = new clsPrintData_SubDiseaseTrack[p_objTransDataArr.Length];
			
			for(int i=0;i<p_objTransDataArr.Length;i++)
            {
                #region
                switch ((enmDiseaseTrackType)p_objTransDataArr[i].m_intFlag)
				{
					case enmDiseaseTrackType.GeneralDisease:
						objTrackInfo = new clsGeneralDiseaseInfo();
						break;
					case enmDiseaseTrackType.HandOver:
                        objTrackInfo = new clsHandOverInfo(m_objCurrentPatient);
						break;
					case enmDiseaseTrackType.TakeOver:
                        objTrackInfo = new clsTakeOverInfo(m_objCurrentPatient);
						break;
					case enmDiseaseTrackType.Consultation:
						objTrackInfo = new clsConsultationInfo();
						break;
					case enmDiseaseTrackType.Convey:
                        objTrackInfo = new clsConveyInfo(m_objCurrentPatient);
						break;
					case enmDiseaseTrackType.TurnIn:
                        objTrackInfo = new clsTurnInInfo(m_objCurrentPatient);
						break;
					case enmDiseaseTrackType.DiseaseSummary:
						objTrackInfo = new clsDiseaseSummaryInfo();
						break;
					case enmDiseaseTrackType.CheckRoom:
						objTrackInfo = new clsCheckRoomInfo();
						break;
					case enmDiseaseTrackType.CaseDiscuss:
						objTrackInfo = new clsCaseDiscussInfo();
						break;
					case enmDiseaseTrackType.BeforeOperationDiscuss:
						objTrackInfo = new clsBeforeOperationDiscussInfo();
						break;
					case enmDiseaseTrackType.DeadCaseDiscuss:
						objTrackInfo = new clsDeadCaseDiscussInfo();
						break;
					case enmDiseaseTrackType.DeathCaseDiscuss:
						objTrackInfo = new clsDeathCaseDiscussInfo();
						break;
					case enmDiseaseTrackType.AfterOperation:
						objTrackInfo = new clsAfterOperationInfo();
						break;
					case enmDiseaseTrackType.Dead:
                        objTrackInfo = new clsDeadRecordInfo(m_objCurrentPatient);
						break;
					case enmDiseaseTrackType.Death:
						objTrackInfo = new clsDeathRecordInfo();
						break;
					case enmDiseaseTrackType.OutHospital:
						objTrackInfo = new clsOutHospitalInfo();
						break;
					case enmDiseaseTrackType.Save:
						objTrackInfo = new clsSaveRecordInfo();
						break;
					case enmDiseaseTrackType.FirstIllnessNote:
                        if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO == "450101001")//���
                        {
                            objTrackInfo = new clsFirstIllnessNoteInfo();
                        }
                        else
                        { 
                            objTrackInfo = new clsFirstIllnessNoteInfo_F2();
                            p_objTransDataArr[i].m_intFlag = (int)enmDiseaseTrackType.FirstIllnessNote_F2;
                        }
						break;
					case enmDiseaseTrackType.FirstIllnessNote_ZY:
						objTrackInfo=new clsFirstIllnessNote_ZYInfo();
                        break;
                    case enmDiseaseTrackType.EMR_SummaryBeforeOP:
                        objTrackInfo = new clsEMR_SummaryBeforeOPInfo();
                        break;
                #endregion 
            }
		
				//����clsDiseaseTrackInfo������
				objTrackInfo.m_ObjRecordContent = p_objTransDataArr[i].m_objRecordContent;
		
				m_objPrintDataArr[i]=new clsPrintData_SubDiseaseTrack();

				int intCharPerLine = (int) ((float)enmRecordRectangleInfo.RecordLineLength/17.5)+1; /*ÿ����ʾ�ĺ��ֵ���Ŀ*/				
				
				//���� clsDiseaseTrackInfo ��õ��ı���Xml
				string strText = ""; 
				string strXML = "";

				if((enmDiseaseTrackType)p_objTransDataArr[i].m_intFlag == enmDiseaseTrackType.CaseDiscuss)
				{
					DateTime dtmFlagTime;
					if(p_objTransDataArr[i].m_objRecordContent.m_dtmFirstPrintDate==DateTime.MinValue)
						dtmFlagTime=DateTime.Parse(m_strServerTime);					
					else 
						dtmFlagTime=p_objTransDataArr[i].m_objRecordContent.m_dtmFirstPrintDate;

					((clsCaseDiscussInfo)objTrackInfo).m_mthGetFormatTrackInfo(30,true,dtmFlagTime,out strText,out strXML);
				}
                else if ((enmDiseaseTrackType)p_objTransDataArr[i].m_intFlag == enmDiseaseTrackType.DeadCaseDiscuss)
                {
                    DateTime dtmFlagTime;
                    if (p_objTransDataArr[i].m_objRecordContent.m_dtmFirstPrintDate == DateTime.MinValue)
                        dtmFlagTime = DateTime.Parse(m_strServerTime);
                    else
                        dtmFlagTime = p_objTransDataArr[i].m_objRecordContent.m_dtmFirstPrintDate;

                    ((clsDeadCaseDiscussInfo)objTrackInfo).m_mthGetFormatTrackInfo(30, true, dtmFlagTime, out strText, out strXML);
                }
				else
				{
					strText = objTrackInfo.m_strGetTrackText(); 
					strXML = objTrackInfo.m_strGetTrackXml();
				}

				m_objPrintDataArr[i].m_strContent = strText; 
				m_objPrintDataArr[i].m_strContentXml = strXML;
				
				string strSignText=objTrackInfo.m_strGetSignText();
				string strBlanks="";
				for(int j2=0;j2<intCharPerLine-strSignText.Length-1;j2++)
				{
					strBlanks +="��";//ע�⣺�˴����Ŀո���ȫ��ռһ�����ֵĿո�				
				}

				#region   Add blank to print
				int intBlankLine = 0;
				
				if (m_dtbBlankRecord != null && m_dtbBlankRecord.Rows.Count > 0)
				{
					foreach(System.Data.DataRow drtAdd in m_dtbBlankRecord.Rows)
					{
						if (DateTime.Parse(drtAdd[2].ToString()).ToString("yyyy-MM-dd HH:mm:ss") == objTrackInfo.m_ObjRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"))
						{
							intBlankLine = Int32.Parse( drtAdd[3].ToString());
							break;
						}
					}
				}
				#endregion
				
				m_objPrintDataArr[i].m_strSign = strBlanks + strSignText;	
				for(int k1 =0; k1 < intBlankLine; k1++)
					m_objPrintDataArr[i].m_strContent = "\n" + m_objPrintDataArr[i].m_strContent;
				m_objPrintDataArr[i].m_strSignXml = objTrackInfo.m_strGetSignXml();
				m_objPrintDataArr[i].m_dtmFirstPrintDate=p_dtmFirstPrintDate[i];


                //���÷�ҳ��־
                if(objTrackInfo.m_ObjRecordContent.m_StrPagination != null)
                    m_objPrintDataArr[i].m_strPagiNation = objTrackInfo.m_ObjRecordContent.m_StrPagination.ToString();
                if (dtbBlankRecord != null && dtbBlankRecord.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow drtAdd in dtbBlankRecord.Rows)
                    {
                        if (DateTime.Parse(drtAdd[2].ToString()).ToString("yyyy-MM-dd HH:mm:ss") == objTrackInfo.m_ObjRecordContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"))
                        {
                            int intBlankLine2 = Int32.Parse(drtAdd[3].ToString());
                            intBlankCount = intBlankLine;
                            m_objPrintDataArr[i].m_intBlankCount = intBlankLine;
                            for (int j2 = 0; j2 < intBlankLine2; j2++)
                            {
                                m_objPrintDataArr[i].m_strContent = "\n" + m_objPrintDataArr[i].m_strContent;
                            }
                            break;
                        }
                    }
                }
			}
		}

		/*
		// �ͷŴ�ӡ����
		protected override void m_mthDisposePrintTools()
		{
			m_fotTitleFont.Dispose();
			m_fotHeaderFont.Dispose();
			m_fotSmallFont.Dispose();
			m_GridPen.Dispose();
			m_slbBrush.Dispose();
		}*/

		/// <summary>
		///  ��ʼ��ӡ��
		/// </summary>
		/*	private void m_mthStartPrint()
			{
				base.m_mthStartPrint();
			}*/

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
				m_mthAddDataToGrid(p_objPrintPageArg);
				m_mthPrintRectangleInfo(p_objPrintPageArg);
			}
			catch(Exception err)
			{
				MDIParent.ShowInformationMessageBox(err.Message + "\r\n" + err.StackTrace);

			}
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
		}
		#endregion

		// ��ӡ����ʱ�Ĳ���
		private void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
		{
//			m_mthSavePrintToRecord(m_objPrintDataArr.Length);

			m_mthResetWhenEndPrint();
		}

		/// <summary>
		/// ÿ�δ�ӡ����֮��ĸ�λ,�����Ǵ�ӡ��ǰҳ���ߴ�ӡȫ��.
		/// </summary>
		private void m_mthResetWhenEndPrint()
		{
			intCurrentRecord=0;
			blnBeginPrintNewRecord=true;
			intNowPage=0;
		}

		#region debug
//		public long m_lngSavePrintToRecord(string p_strInPatientID,string p_strInPatientDate,string p_strFormName,
//			int p_intToRecord)
//		{
//			com.digitalwave.HRPService.clsHRPTableService objServ = new com.digitalwave.HRPService.clsHRPTableService();
//			string strSql = @"SELECT *
//FROM ContinuePrintRecord
//WHERE (InPatientID = '"+p_strInPatientID+"') AND (InPatientDate = '"+p_strInPatientDate+"') AND (FormName = '"+p_strFormName+"')";
//			System.Data.DataTable dtResult = new System.Data.DataTable();
//			long lngRes = objServ.DoGetDataTable(strSql,ref dtResult);
//			if(lngRes > 0 && dtResult.Rows.Count > 0)
//			{
//				strSql = @"DELETE FROM ContinuePrintRecord
//WHERE (InPatientID = '"+p_strInPatientID+"') AND (InPatientDate = '"+p_strInPatientDate+"') AND (FormName = '"+p_strFormName+"')";
//			}
//			strSql = @"INSERT INTO ContinuePrintRecord
//      (InPatientID, InPatientDate, FormName, Record)
//VALUES ('"+p_strInPatientID+"', '"+p_strInPatientDate+"', '"+p_strFormName+"', '"+p_intToRecord+"')";			
//			return objServ.DoExcute(strSql);
//		}
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
            //������̼�¼����Ӥ��Ӥ��������Ҫ�󣺵���������С��һ����ʱ���á��¡���ʾ
            if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_StrCurrentHospitalNO == "440605001")
            {
                string strAge = "";
                strAge = m_objPrintInfo.m_strAge;
                if (strAge.IndexOf("��") == -1 && strAge.IndexOf("��") == -1)
                    strAge = "��";
                objEveryRecordPageInfo.strAge = strAge;
            }
            else
                objEveryRecordPageInfo.strAge = m_objPrintInfo.m_strAge;
			objEveryRecordPageInfo.strPatientName=m_objPrintInfo.m_strPatientName;
			objEveryRecordPageInfo.strBedNo =m_objPrintInfo.m_strBedName;
			objEveryRecordPageInfo.strDeptName= m_objPrintInfo.m_strAreaName;
			objEveryRecordPageInfo.strSex=m_objPrintInfo.m_strSex;
			objEveryRecordPageInfo.strInPatientID=m_objPrintInfo.m_strHISInPatientID;
			//objEveryRecordPageInfo.strPrintDate=m_objCurrentPatient!=null? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") :"";		

            StringFormat sf = new StringFormat(StringFormatFlags.FitBlackBox);
            sf.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, m_fotHeaderFont, m_slbBrush, clsPrintPosition.m_rtgHospitalTitlePos,sf);

            e.Graphics.DrawString("��     ��     ��     ¼", m_fotTitleFont, m_slbBrush, clsPrintPosition.m_rtgFormTitlePos,sf);
			

			e.Graphics.DrawString("������",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name_Title  ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strPatientName  ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name ));
		
			e.Graphics.DrawString("�Ա�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strSex ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex  ));

			e.Graphics.DrawString("���䣺",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strAge ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age ));

			e.Graphics.DrawString("������",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name_Title ));
			e.Graphics.DrawString(m_objPrintInfo.m_strAreaName ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name ));
			e.Graphics.DrawString("���ţ�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo_Title ));
			e.Graphics.DrawString(objEveryRecordPageInfo.strBedNo ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo ));	
			
			e.Graphics.DrawString("סԺ�ţ�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID_Title ));
      
            //�ܴ�ҽʦǩ��
            //e.Graphics.DrawString("�ܴ�ҽʦ��____________", m_fotSmallFont, m_slbBrush, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2-80,
            //    (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * ((int)enmRecordRectangleInfo.RowLinesNum + 1) + 40);

            //e.Graphics.DrawLine(m_GridPen, (int)enmRecordRectangleInfo.LeftX + (int)enmRecordRectangleInfo.ColumnsMark2,
            //    (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * ((int)enmRecordRectangleInfo.RowLinesNum + 1) + 80,
            //    (int)enmRecordRectangleInfo.RightX,
            //    (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * ((int)enmRecordRectangleInfo.RowLinesNum + 1) + 80);
            //---------------
            
			e.Graphics.DrawString(objEveryRecordPageInfo.strInPatientID ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID ));
            sf.Dispose();
		}
		#endregion		

		#region ������
		/// <summary>
		///  ������
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintRectangleInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX-10 ,
				(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.RightX,
				(int)enmRecordRectangleInfo.TopY);

			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX-10 ,
				(int)enmRecordRectangleInfo.BottomLine,
				(int)enmRecordRectangleInfo.RightX,
				(int)enmRecordRectangleInfo.BottomLine);

			//����������
			
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.LeftX-10,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.LeftX-10,(int)enmRecordRectangleInfo.BottomLine);
			e.Graphics.DrawLine(m_GridPen,(int)enmRecordRectangleInfo.RightX ,(int)enmRecordRectangleInfo.TopY,
				(int)enmRecordRectangleInfo.RightX,(int)enmRecordRectangleInfo.BottomLine);
            
			
			//ҳ��//////////////////////////////////////////////////////////////
            //(int)enmRecordRectangleInfo.LeftX +((int)enmRecordRectangleInfo.ColumnsMark2 - (int)enmRecordRectangleInfo.ColumnsMark1) / 2,
            e.Graphics.DrawString("����" + intNowPage.ToString() + "ҳ��", m_fotSmallFont, m_slbBrush, ((int)enmRecordRectangleInfo.RightX - (int)enmRecordRectangleInfo.LeftX )/ 2,
				(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep*((int)enmRecordRectangleInfo.RowLinesNum+1)+10 );
		}

						
		#endregion

		#region ������ݵ����		

		/// <summary>
		/// ������ݵ����
		/// </summary>
		/// <param name="e"></param>
		private void m_mthAddDataToGrid(System.Drawing.Printing.PrintPageEventArgs e)
		{
			try
			{
				int intPrintLenth=(int) ((float)enmRecordRectangleInfo.RecordLineLength/17.5)+1; /*ÿ����ʾ�ĺ��ֵ���Ŀ*/				
				string strRecord="";
				string strRecordXML="";				
				DateTime dtmFlagTime;
			
				int intNowRow=1; /*��¼��ҳ��ǰ�Ĵ�ӡ����*/

      
				if(m_objPrintInfo.m_strInPatentID ==""|| m_objPrintDataArr==null)return;
				for(;intCurrentRecord<m_objPrintDataArr.Length;intCurrentRecord++)
				{	
					#region ������¼�¼����ӡ���ڣ����ô�ӡ����ֵ
					if(blnBeginPrintNewRecord)
					{
						blnBeginPrintNewRecord = false;

						strRecord =m_objPrintDataArr[intCurrentRecord].m_strContent;
						strRecordXML=m_objPrintDataArr[intCurrentRecord].m_strContentXml;
						
						//��ӡһ����¼/////////////////////////////////////////////////////////////////////
						/*�޸Ĵ�ӡ���ݷ�ʽ���Ե�һ�δ�ӡʱ��Ϊ�ָ��ʱ���������޸ĵĺۼ���Ҫ���������δ��ӡ������ʾ��ȷ�ļ�¼��*/
                        if (m_objPrintDataArr[intCurrentRecord].m_dtmFirstPrintDate == DateTime.MinValue)
                            dtmFlagTime = DateTime.Parse(m_strServerTime);
                        else 
							dtmFlagTime=m_objPrintDataArr[intCurrentRecord].m_dtmFirstPrintDate;

                        if (clsSubDiseaseTrackPrintTool.m_blnIsPrintMark)
                        {
                            m_objPrintContext.m_mthSetContextWithCorrectBefore(strRecord, strRecordXML, dtmFlagTime, m_objPrintDataArr != null);

                            ctlRichTextBox.clsModifyUserInfo[] m_objModifyUserArr = m_objPrintContext.m_ObjModifyUserArr;

                            for (int i = 0; i < m_objModifyUserArr.Length; i++)
                            {
                                if (m_objModifyUserArr[i].m_clrText.ToArgb() == Color.White.ToArgb())
                                {
                                    m_objModifyUserArr[i].m_clrText = Color.Black;
                                }
                            }
                        }
                        else
                        {
                            m_objPrintContext.m_mthSetContextWithAllCorrect(strRecord, strRecordXML);
                        }					
					

						#region ���Ʒ�ҳ,ͨ��XML���õķ�ʽʵ��
                        if (m_blnIsContainsForNewPage(strRecord) && intNowRow != 1)
                            intNowRow = (int)enmRecordRectangleInfo.RowLinesNum + 1;
						#endregion
					}
					#endregion

					#region ����ǰ��¼��ǩ����ȫ���������;��ҳ����
					while(m_objPrintContext.m_BlnHaveNextLine())//�жϸ�����¼�Ƿ�����һ��
					{
						/* 
						 * ������һ�д�ӡ�ĸպ���һ����¼�ı��⣬
						 * ����µ�һҳ��ʼ��
						 */
						if(intNowRow == (int)enmRecordRectangleInfo.RowLinesNum)
						{
							if(m_objPrintContext.m_IntCurrentIndex == 0)
							{
								e.HasMorePages =true;
								intNowPage ++;
								return;
							}
						}

						if(m_blnCheckPageChange(intNowRow,e)==true) //ÿ��ӡһ��֮ǰ��Ҫ����Ƿ�ҳ
						{
							//�����û��ָ����¼����ա�ע�⣺�ü�¼��û����
//							if(intCurrentRecord < m_intFromRecord)
//							{
//								e.Graphics.Clear(Color.White);
//								m_mthPrintTitleInfo(e);
//								m_mthPrintRectangleInfo(e);
//								m_mthAddDataToGrid(e);
//							}
							return;
						}
						m_objPrintContext.m_mthPrintLine((int)enmRecordRectangleInfo.RecordLineLength,(int)enmRecordRectangleInfo.LeftX, 
							(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep*(intNowRow-1) + 10,e.Graphics);
						
						intNowRow ++;//ÿ��ӡһ�ж�Ҫ���¹���
					}
					#endregion
					
					#region ǩ��
					string [] strTextArr_Sign,strXmlArr_Sign;	
					com.digitalwave.controls.ctlRichTextBox.m_mthSplitXml(m_objPrintDataArr[intCurrentRecord].m_strSign,m_objPrintDataArr[intCurrentRecord].m_strSignXml,intPrintLenth,out strTextArr_Sign,out strXmlArr_Sign);

                    if (m_blnCheckPageChange(intNowRow + strTextArr_Sign.Length -1 ,e)==true) //ÿ��ӡһ��֮ǰ��Ҫ����Ƿ�ҳ
						return;//�˴�ǩ������ǰҳ���꣬��һ�������һҳ

                    string[] signArr = null;
                    List<string> lstName = new List<string>();
                    List<string> lstRank = new List<string>();
					for(int k3=0;k3<strTextArr_Sign.Length;k3++)
					{
                        //if (strTextArr_Sign[k3].Contains("��"))
                        //{
                        //    signArr = strTextArr_Sign[k3].Split('��');
                        //}
                        strTextArr_Sign[k3] = strTextArr_Sign[k3].Replace("��"," ").TrimStart();
                        if (strTextArr_Sign[k3].Contains(" "))
                        {
                            signArr = strTextArr_Sign[k3].TrimStart().Split(' ');
                            int count = strTextArr_Sign[k3].TrimStart().Split(' ').Length;
                            for (int i = 0; i < count; i+=2)
                            {
                                lstRank.Add(strTextArr_Sign[k3].TrimStart().Split(' ')[i]);
                                lstName.Add(strTextArr_Sign[k3].TrimStart().Split(' ')[i+1]);
                            }
                        }

                        int y = (int)enmRecordRectangleInfo.TopY + (int)enmRecordRectangleInfo.RowStep * (intNowRow - 1) + 10;
                        int x = (int)enmRecordRectangleInfo.LeftX + 420;
                        if (lstName.Count >= 1)
                        {
                            x = (int)enmRecordRectangleInfo.LeftX + 570;
                        }

                        if (lstName.Count >= 2)
                        {
                            x = (int)enmRecordRectangleInfo.LeftX + 370;
                        }

                        if (lstName.Count >= 3)
                        {
                            x = (int)enmRecordRectangleInfo.LeftX + 220;
                        }

                        for (int rI = 0; rI < lstName.Count; rI++)
                        {
                            e.Graphics.DrawString(lstRank[rI], m_fotSmallFont, m_slbBrush, x, y);
                            x += lstRank[rI].Length + 85;
                            Image imgEmpSig = ImageSignature.GetEmpSigImage(lstName[rI]);
                            
                            if (imgEmpSig != null)
                            {
                                //if (lstName[rI].Trim() != "����")
                                //    imgEmpSig = ImageSignature.pictureProcess(imgEmpSig, 579, 268);
                                e.Graphics.DrawImage(imgEmpSig, x, y - 2, 70, 30);
                                x += lstRank[rI].Length + 80;
                            }
                            else
                            {
                                e.Graphics.DrawString(lstName[rI], m_fotSmallFont, Brushes.Black, x, y);
                                x += lstRank[rI].Length + 80;
                            }
                        }

                        intNowRow++;//ÿ��ӡһ�ж�Ҫ���¹���
					}

					blnBeginPrintNewRecord=true; //ǩ����ӡ��ϣ���ǰ��¼����				
					intNowRow ++;//��һ��
					#endregion

					#region �����û��ӡ��ָ����¼����֮ǰ�����
//					if(intCurrentRecord < m_intFromRecord)
//					{
//						e.Graphics.Clear(Color.White);//���
//
//						//���������¼�պ�ռһҳ
//						if(intNowRow>(int)enmRecordRectangleInfo.RowLinesNum)
//						{
//							//�����ָ����¼��ǰһ�������ȴ�ӡһ��ҳ����Ϊ�û�������֮ǰ��ӡ������ֽ��
//							if(intCurrentRecord == m_intFromRecord - 1)
//							{
//								intCurrentRecord++;
//								e.HasMorePages = true;
//								return;
//							}
//
//							//������ǣ������һҳ�Ķ��˼����򣬲�����Ҫ��ҳͷ��ҳ��
//							m_mthPrintTitleInfo(e);
//							m_mthPrintRectangleInfo(e);
//							intNowRow = 1;
//							//ҳ��//////////////////////////////////////////////////////////////
//							e.Graphics.DrawString("����"+intNowPage.ToString()+"ҳ��",m_fotSmallFont ,m_slbBrush,(int)enmRecordRectangleInfo.LeftX+(int)enmRecordRectangleInfo.ColumnsMark2 ,
//								(int)enmRecordRectangleInfo.TopY+(int)enmRecordRectangleInfo.RowStep*((int)enmRecordRectangleInfo.RowLinesNum+1)+10 );
//						}						
//					}
					#endregion


                    //�����һҳ�Ƿ����÷�ҳ��־��������÷�ҳ��ҳ��ӡ
                    if (intCurrentRecord < m_objPrintDataArr.Length)
                    {
                        if (intCurrentRecord == m_objPrintDataArr.Length - 1)
                        {
                            if (m_objPrintDataArr[intCurrentRecord].m_strPagiNation == "1")
                            {
                                intNowRow += (int)enmRecordRectangleInfo.RowLinesNum;
                                e.HasMorePages = true;
                                //								intNowPage ++;
                            }
                        }
                        else
                        {
                            if (m_objPrintDataArr[intCurrentRecord + 1].m_strPagiNation == "1")
                            {
                                intNowRow += (int)enmRecordRectangleInfo.RowLinesNum;
                                e.HasMorePages = true;
                                //								intNowPage ++;
                            }
                        }
                    }
				}

				//��ӡ��ɣ�û��ҳ�ˡ���Ϊ��forѭ����e.HasMorePages��ֵ�����ѱ���Ϊtrue
				e.HasMorePages = false;
				intNowPage ++;
			}
			catch(Exception err)
			{
				MDIParent.ShowInformationMessageBox(err.Message + "\r\n" +err.StackTrace);
			}
		}

		/// <summary>
		/// ����Ƿ�ҳ,true:��ҳ��false:����ҳ
		/// </summary>
		/// <param name="p_intNowRow">��ǰ��ӡ�У���p_intNowRow��</param>
		/// <param name="e"></param>
		/// <returns></returns>
		private bool m_blnCheckPageChange(int p_intNowRow,System.Drawing.Printing.PrintPageEventArgs e)
		{
			//����ǰ�г������һ�У��� >ҳ��������ʱ��ҳ
			if(p_intNowRow>(int)enmRecordRectangleInfo.RowLinesNum) 
			{
				e.HasMorePages =true;
				intNowPage ++;

				return true;
			}
			else return false;
		}
		
		#endregion 										
		
	    #region �����ӡ��Ԫ�ص������
		protected class clsPrintPageSettingForRecord
		{	
			public clsPrintPageSettingForRecord(){}
			
			/// <summary>
			/// ��������
			/// </summary>
			/// <param name="p_intItemName">��Ŀ����</param>
			/// <returns></returns>
			public PointF m_getCoordinatePoint(int p_intItemName)
			{
				float fltOffsetX=20;//X��ƫ����
				PointF m_fReturnPoint;
				switch(p_intItemName)
				{   
                    
					case (int)enmItemDefination.Page_HospitalName:
						m_fReturnPoint = new PointF(320f-fltOffsetX,115f);
						break;
					case (int)enmItemDefination.Page_Name_Title:
						m_fReturnPoint = new PointF(255f-fltOffsetX,145f);
						break;
					case (int)enmItemDefination.Name_Title :
                        m_fReturnPoint = new PointF(50f - fltOffsetX, (float)clsPrintPosition.c_intA3TopTitleY);
						break;
					case (int)enmItemDefination.Name:
                        m_fReturnPoint = new PointF(100f - fltOffsetX, (float)clsPrintPosition.c_intA3TopTitleY);
						break;

					case (int)enmItemDefination.Sex_Title :
                        m_fReturnPoint = new PointF(185f - fltOffsetX, (float)clsPrintPosition.c_intA3TopTitleY);
						break;
					case (int)enmItemDefination.Sex :
                        m_fReturnPoint = new PointF(230f - fltOffsetX, (float)clsPrintPosition.c_intA3TopTitleY);
						break;

					case (int)enmItemDefination.Age_Title :
                        m_fReturnPoint = new PointF(260f - fltOffsetX, (float)clsPrintPosition.c_intA3TopTitleY);
						break;
					case (int)enmItemDefination.Age:
                        m_fReturnPoint = new PointF(305f - fltOffsetX, (float)clsPrintPosition.c_intA3TopTitleY);
						break;

					case (int)enmItemDefination.Dept_Name_Title:
                        m_fReturnPoint = new PointF(360f - fltOffsetX, (float)clsPrintPosition.c_intA3TopTitleY);
						break;
					case (int)enmItemDefination.Dept_Name :
                        m_fReturnPoint = new PointF(410f - fltOffsetX, (float)clsPrintPosition.c_intA3TopTitleY);
						break;
					
					case (int)enmItemDefination.BedNo_Title :
                        m_fReturnPoint = new PointF(555 - fltOffsetX, (float)clsPrintPosition.c_intA3TopTitleY);
						break;
					case (int)enmItemDefination.BedNo:
                        m_fReturnPoint = new PointF(605 - fltOffsetX, (float)clsPrintPosition.c_intA3TopTitleY);
						break;

					case (int)enmItemDefination.InPatientID_Title:
                        m_fReturnPoint = new PointF(655f - fltOffsetX, (float)clsPrintPosition.c_intA3TopTitleY);
						break;
					case (int)enmItemDefination.InPatientID :
                        m_fReturnPoint = new PointF(715f - fltOffsetX, (float)clsPrintPosition.c_intA3TopTitleY);
						break;
											
					default:
						m_fReturnPoint = new PointF(400f,400f);
						break;
	
				}
				return m_fReturnPoint;
			}
		}

	    #endregion
		#endregion ��ӡ
		
		#region �йش�ӡ������	
		/// <summary>
		/// ���д�ӡ������
		/// </summary>
		private clsPrintData_SubDiseaseTrack[] m_objPrintDataArr;
		//		[Serializable]
		//		private class clsPrintData
		//		{
		//			public string m_strContent;
		//			public string m_strContentXml;
		//			public string m_strSign;
		//			public string m_strSignXml;
		//			public DateTime m_dtmFirstPrintDate;
		//		}

		/// <summary>
		/// ���̼�¼���ݴ�ӡ������
		/// </summary>
        private clsPrintRichTextContext m_objPrintContext;
		
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
		/// ��¼��ӡ���ڼ�ҳ
		/// </summary>
		private int intNowPage;
		/// <summary>
		/// ��ǰ��ӡ�Ļ����¼
		/// </summary>
		private int intCurrentRecord;
		/// <summary>
		/// ׼����ӡһ���¼�¼(������������¼,��������¼����)
		/// </summary>
		private bool blnBeginPrintNewRecord=true;		
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
			public string strDeptName;
			public string strInPatientID;
			//			public int intCurrentPage;
			//			public int intTotalPages;
			//			public string strPrintDate;
		}

		/// <summary>
		/// ���ӵ���Ϣ
		/// </summary>
		private enum enmRecordRectangleInfo
		{
			/// <summary>
			/// ���ӵĶ���
			/// </summary>
            TopY = clsPrintPosition.c_intA3TopLineY,
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
			RowStep = 23,
			/// <summary>
			/// ���ӵ�����
			/// </summary>
			RowLinesNum = 36,
			/// <summary>
			/// ������¼ÿ�е�pixel����
			/// </summary>
			RecordLineLength=RightX-LeftX,//750,
			/// <summary>
			/// �е���Ŀ
			/// </summary>
			ColumnsNum=3,
			/// <summary>
			/// ��һ�������(X)
			/// </summary>
			ColumnsMark1=160,
			/// <summary>
			/// �ڶ��������(X)
			/// </summary>
			ColumnsMark2=650,

			/// <summary>
			/// ����
			/// </summary>
			BottomLine=(int)TopY+((int)RowLinesNum)*(int)RowStep+15
				
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
			RecordSign2
		
		}
	  
	
		#endregion

		#region ���ⲿ���Ա���ӡ����ʾʵ��.	
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
		//		clsIntensiveTendMainPrintTool objPrintTool;
		//		private void m_mthDemoPrint_FromDataSource()
		//		{	
		//			objPrintTool=new clsIntensiveTendMainPrintTool();
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
		//			objPrintTool=new clsIntensiveTendMainPrintTool();
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

		#region  ���ӿ��д�ӡ����
		private void m_intGetBlankRecord()
		{
			clsDiseaseTrackAddBlankDomain objAddBlankDomain = new clsDiseaseTrackAddBlankDomain();
			objAddBlankDomain.m_lngGetBlankRecordContent(m_objPrintInfo.m_strInPatentID,m_objPrintInfo.m_dtmInPatientDate,out m_dtbBlankRecord);
		}
		#endregion

        private bool m_blnIsContainsForNewPage(string p_strTitle)
        {
            if (m_lstNewPagePrintTitle.Count == 0) return false;
            foreach (string str in m_lstNewPagePrintTitle)
            {
                if (p_strTitle.Contains(str) && str != string.Empty) return true;
            }
            return false;
        }
	}

	
}
