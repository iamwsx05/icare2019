using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;

namespace iCare
{
	/// <summary>
	/// סԺ�����Ĵ�ӡ������,Jacky-2003-6-10
	/// </summary>
	public class clsInPatientCaseHistoryPrintTool: infPrintRecord
	{	
		private bool m_blnIsFromDataSource=true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
		private bool m_blnWantInit=true;
        /// <summary>
        /// �Ƿ��ӡ�޸ĺۼ�
        /// </summary>
        public static bool m_blnIsPrintMark = true;
		private clsBaseCaseHistoryDomain m_objRecordsDomain;
		private clsPrintInfo_InPatientCaseHistory m_objPrintInfo;
		
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
			m_objPrintInfo=new clsPrintInfo_InPatientCaseHistory();
			m_objPrintInfo.m_strInPatentID=m_objPatient!=null? m_objPatient.m_StrInPatientID:"";					
			m_objPrintInfo.m_strPatientName=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrFirstName :"";
			m_objPrintInfo. m_strSex=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrSex:"";
			m_objPrintInfo. m_strAge=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrAge : "";
			m_objPrintInfo. m_strBedName=m_objPatient!=null? m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName :"";
			m_objPrintInfo. m_strDeptName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjDept.m_StrDeptName :"";
			m_objPrintInfo. m_strAreaName=m_objPatient!=null? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName:"";			
			m_objPrintInfo.m_dtmInPatientDate=p_dtmInPatientDate;
			m_objPrintInfo.m_dtmOpenDate=p_dtmOpenDate;	
		
			m_objPrintInfo.m_strBirthplace= m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrBirthPlace:"";//������
			m_objPrintInfo.m_strNativePlace= m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrNativePlace:"";//����
			m_objPrintInfo.m_strOccupation=  m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrOccupation:"";//ְҵ
			m_objPrintInfo.m_strMarried= m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrMarried:"";//���
			m_objPrintInfo.m_StrLinkManFirstName= m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrLinkManFirstName:"";//��ϵ��
			m_objPrintInfo.m_strNationality= m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrNation:"";//����
			m_objPrintInfo.m_strHomePhone=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrLinkManPhone:"";//�绰
            m_objPrintInfo.m_strHomeAddress = m_objPatient != null ? (m_objPatient.m_ObjPeopleInfo.m_StrHomeAddress) : "";//��ַ
            m_objPrintInfo.m_strHISInPatientID = p_objPatient!=null? p_objPatient.m_StrHISInPatientID : "";
            m_objPrintInfo.m_dtmHISInPatientDate = p_objPatient != null ? p_objPatient.m_DtmSelectedHISInDate : DateTime.MinValue;

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
			m_blnWantInit=false;

			if(m_objPrintInfo==null)
			{
				MDIParent.ShowInformationMessageBox("����m_mthInitPrintContent֮ǰ�����ȵ���m_mthSetPrintInfo����");
				return;
			}
		
			if(m_objPrintInfo.m_strInPatentID !=""/* && m_objPrintInfo.m_dtmOpenDate != DateTime.MinValue*/)
			{
                m_objRecordsDomain = new clsBaseCaseHistoryDomain(enmBaseCaseHistoryTypeInfo.InPatientCaseHistory);
				long lngRes = m_objRecordsDomain.m_lngGetPrintInfo(m_objPrintInfo.m_strInPatentID,m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss")  ,/*m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"),*/DateTime.MinValue,out m_objPrintInfo.m_objContent,out m_objPrintInfo.m_objPicValueArr,out m_objPrintInfo.m_dtmFirstPrintDate,out m_objPrintInfo.m_blnIsFirstPrint);
//				if(lngRes <= 0)
//					return ;
			}
			//���ñ����ݵ���ӡ��,��ʹ�Ǵ�ӡ�հ׵�,����Ҳ����ִ��.(��:�ڱ������ڲ�,����֮�ϲ�׼��return���,���ǳ�������.)
			m_mthSetPrintContent((clsInPatientCaseHistoryContent )m_objPrintInfo.m_objContent,m_objPrintInfo.m_dtmFirstPrintDate);		
				
		}

		/// <summary>
		/// ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
		/// </summary>
		/// <param name="p_objPrintContent">��ӡ����</param>
		public void m_mthSetPrintContent(object p_objPrintContent)
		{
			m_blnWantInit=false;
			if(p_objPrintContent.GetType().Name !="clsPrintInfo_InPatientCaseHistory")
			{
				MDIParent.ShowInformationMessageBox("��������");
			}
			m_blnIsFromDataSource=false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ
			m_objPrintInfo=(clsPrintInfo_InPatientCaseHistory)p_objPrintContent;

			m_mthSetPrintContent((clsInPatientCaseHistoryContent )m_objPrintInfo.m_objContent,m_objPrintInfo.m_dtmFirstPrintDate);
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
		
			//û�м�¼����ʱ�����ؿ�
			if(m_objPrintInfo.m_objContent == null)
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
				
			m_fotTitleFont = new Font("SimSun", 16,FontStyle.Bold);
			m_fotHeaderFont = new Font("SimSun", 18,FontStyle.Bold);
			m_fotItemHead = new Font("",13,FontStyle.Bold);
			m_fotSmallFont = new Font("SimSun",12);
			m_GridPen = new Pen(Color.Black,2);
			m_slbBrush = new SolidBrush(Color.Black);
			m_objPageSetting = new clsPrintPageSettingForRecord();

		
		#endregion �йش�ӡ��ʼ��
		}

		/// <summary>
		/// �ͷŴ�ӡ����
		/// </summary>
		public void m_mthDisposePrintTools(object p_objArg)
		{
			m_fotTitleFont.Dispose() ;
			m_fotHeaderFont.Dispose() ;
			m_fotSmallFont.Dispose() ;
			m_GridPen.Dispose() ;
			m_slbBrush.Dispose() ;
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
			if(m_blnIsFromDataSource==false || m_objPrintInfo.m_strInPatentID=="") return; 
			//�����ӡ�ɹ�������������Ҫ���µ�ʱ�䣬����У�����ʱ�䡣 
			if(!((PrintEventArgs)p_objPrintArg).Cancel && m_objPrintInfo.m_blnIsFirstPrint)
			{
				m_objRecordsDomain.m_lngUpdateFirstPrintDate(m_objPrintInfo.m_strInPatentID,m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"),m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss")  ,m_objPrintInfo.m_dtmFirstPrintDate);
			}
		}	


	#region ��ӡ

		// ���ô�ӡ���ݡ�
		private  void m_mthSetPrintContent(clsInPatientCaseHistoryContent p_objContent,DateTime p_dtmFirstPrintDate)
		{
			m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
				new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
										  new clsPrintPatientFixInfo(),
//										  new clsPrintInPatientCasePergAndBorn(),
										  new clsPrintInPatientCaseMain(),
										  new clsPrintInPatientCaseCurrent(),
//										  new clsPrintInPatientLCQK(),
										  new clsPrintInPatientBeforetimeStatus(),
//										  new clsPrintInPatientYJS(),
										  new clsPrintInPatientOwenStatus(),
										  new clsPrintInPatientMarriageStatus(),
//										  new clsPrintInPatientXBS(),
//										  new clsPrintInPatientOldMaternitySuffer(),
//										  new clsPrintInPatientShYS(),
//										  new clsPrintInPatientCQJC(),
										  new clsPrintInPatientCaseCatameniaHistory(),
										  new clsPrintInPatientFamilyStatus(),
										  new clsPrintInPatientBodyChekcFixStatus(),
										  new clsPrintInPatientProfessionalStatus(),
										  new clsPrintInPatientLabStatus(),
										  new clsPrintInPatientSummeryStatus(),
										  new clsPrintPatientDiagnoseTitleInfo(),
										  new clsPrintPatientPrimaryDiagnoseInfo(),
				//										  new clsPrintPatientPrimaryDiagnoseNameDateInfo(),
								
			});
			m_objPrintLineContext.m_ObjPrintSign =  new com.digitalwave.Utility.Controls.clsPrintRecordSign();

			object [] objData = new Object[2];
			objData[0] = m_objChangePrintTextColor(p_objContent);
			objData[1] = m_objPrintInfo;
		
			//���ô�ӡ��Ϣ������Set Value��ȥ
			m_objPrintLineContext.m_ObjPrintLineInfo = objData;
			//�����ݿ��ó�����FirstPrintDate����ÿ����ӡ�������m_DtmFirstPrintTime���ڸ���������
			m_objPrintLineContext.m_DtmFirstPrintTime = p_dtmFirstPrintDate;
		}

		private clsInPatientCaseHistoryContent m_objChangePrintTextColor(clsInPatientCaseHistoryContent p_objclsInPatientCase)
		{
			if(p_objclsInPatientCase==null)
				return null;
			//�Ѱ�ɫ��Ϊ��ɫ
			clsXML_DataGrid objclsXML_DataGrid=new clsXML_DataGrid();
			p_objclsInPatientCase.m_strBeforetimeStatusXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack(p_objclsInPatientCase.m_strBeforetimeStatusXML);
			p_objclsInPatientCase.m_strBloodPressureUnitXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack(p_objclsInPatientCase.m_strBloodPressureUnitXML);

			p_objclsInPatientCase.m_strBreathXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientCase.m_strBreathXML);
			p_objclsInPatientCase.m_strConfirmReasonXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientCase.m_strConfirmReasonXML);
			p_objclsInPatientCase.m_strConfirmReasonXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack(p_objclsInPatientCase.m_strConfirmReasonXML);

			p_objclsInPatientCase.m_strCurrentStatusXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack(p_objclsInPatientCase.m_strCurrentStatusXML);
			p_objclsInPatientCase.m_strDiaXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack(p_objclsInPatientCase.m_strDiaXML);
			p_objclsInPatientCase.m_strFamilyHistoryXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack(p_objclsInPatientCase.m_strFamilyHistoryXML);

			p_objclsInPatientCase.m_strFinallyDiagnoseXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack(p_objclsInPatientCase.m_strFinallyDiagnoseXML);
			p_objclsInPatientCase.m_strLabCheckXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack(p_objclsInPatientCase.m_strLabCheckXML);
			p_objclsInPatientCase.m_strMainDescriptionXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack(p_objclsInPatientCase.m_strMainDescriptionXML);

			p_objclsInPatientCase.m_strMarriageHistoryXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack(p_objclsInPatientCase.m_strMarriageHistoryXML);
			p_objclsInPatientCase.m_strMedicalXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack(p_objclsInPatientCase.m_strMedicalXML);
			p_objclsInPatientCase.m_strOwnHistoryXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientCase.m_strOwnHistoryXML);

			p_objclsInPatientCase.m_strPrimaryDiagnoseXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientCase.m_strPrimaryDiagnoseXML);
			p_objclsInPatientCase.m_strProfessionalCheckXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientCase.m_strProfessionalCheckXML);
			p_objclsInPatientCase.m_strPulseXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientCase.m_strPulseXML);

			p_objclsInPatientCase.m_strSummaryXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientCase.m_strSummaryXML);
			p_objclsInPatientCase.m_strSysXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientCase.m_strSysXML);
			p_objclsInPatientCase.m_strTemperatureXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientCase.m_strTemperatureXML);
		
			p_objclsInPatientCase.m_strCatameniaHistoryXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientCase.m_strCatameniaHistoryXML);
		
			return p_objclsInPatientCase;
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
		private int m_intYPos=155 ;//= (int)enmRectangleInfo.TopY+5;
	
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
			SmallRowStep=25,
			/// <summary>
			/// ���ӵ�����
			/// </summary>
			RowLinesNum = 32,

			ColumnsMark1=35,

			/// <summary>
			/// CheckBoxƫ���ұ��ı��ľ���
			/// </summary>
			CheckShift=15,

			/// <summary>
			/// �׻���ƫ���ı�����ľ���
			/// </summary>
			BottomLineShift=15,

			BottomY=1025

		}

#endregion

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
		/// <summary>
		/// ��ȡ�������
		/// </summary>
		private clsPrintPageSettingForRecord m_objPageSetting;

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
						m_fReturnPoint = new PointF(340f-fltOffsetX,40f);
						break;
					case (int)enmItemDefination.Page_Name_Title:
						m_fReturnPoint = new PointF(280f-fltOffsetX,70f);
						break;
					case (int)enmItemDefination.Name_Title :
						m_fReturnPoint = new PointF(50f-fltOffsetX,110f);
						break;
					case (int)enmItemDefination.Name:
						m_fReturnPoint = new PointF(100f-fltOffsetX,110f);
						break;

					case (int)enmItemDefination.Sex_Title :
						m_fReturnPoint = new PointF(190f-fltOffsetX,110f);
						break;
					case (int)enmItemDefination.Sex :
						m_fReturnPoint = new PointF(240f-fltOffsetX,110f);
						break;

					case (int)enmItemDefination.Age_Title :
						m_fReturnPoint = new PointF(270f-fltOffsetX,110f);
						break;
					case (int)enmItemDefination.Age:
						m_fReturnPoint = new PointF(310f-fltOffsetX,110f);
						break;

					case (int)enmItemDefination.Dept_Name_Title:
						m_fReturnPoint = new PointF(395f,110f);
						break;
					case (int)enmItemDefination.Dept_Name :
						m_fReturnPoint = new PointF(445f,110f);
						break;
			
					case (int)enmItemDefination.BedNo_Title :
						m_fReturnPoint = new PointF(600f,110f);
						break;
					case (int)enmItemDefination.BedNo:
						m_fReturnPoint = new PointF(650f,110f);
						break;

					case (int)enmItemDefination.InPatientID_Title:
						m_fReturnPoint = new PointF(660f-fltOffsetX,110f);
						break;
					case (int)enmItemDefination.InPatientID :
						m_fReturnPoint = new PointF(720f-fltOffsetX,110f);
						break;
									
					default:
						m_fReturnPoint = new PointF(400f,400f);
						break;

				}
				return m_fReturnPoint;
			}
		}

#endregion		

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
			RightX = 180+17,
			/// <summary>
			/// ����ÿ�еĲ���
			/// </summary>
			RowStep = 7,
			SmallRowStep=20,
			/// <summary>
			/// ���ӵ�����
			/// </summary>
			RowLinesNum = 32,

			ColumnsMark1=35,

			/// <summary>
			/// CheckBoxƫ���ұ��ı��ľ���
			/// </summary>
			CheckShift=15,

			/// <summary>
			/// �׻���ƫ���ı�����ľ���
			/// </summary>
			BottomLineShift=15,

			BottomY=1024,

			PrintWidth = 670,
			PrintWidth2 = 710,

		}


		// ��ӡ��ʼ���ڴ�ӡҳ֮ǰ�Ĳ���
		private void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
		{

		}

		// ��ӡҳ
		private void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
		{
			m_mthPrintTitleInfo(p_objPrintPageArg);
			m_mthPrintHeader(p_objPrintPageArg); 

			Font fntNormal = new Font("",10);

			while(m_objPrintLineContext.m_BlnHaveMoreLine)
			{
				//�������ݴ�ӡ
				m_objPrintLineContext.m_mthPrintNextLine(ref m_intYPos,p_objPrintPageArg.Graphics,fntNormal);

				if(m_intYPos > p_objPrintPageArg.PageBounds.Height-270
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
			Font fntSign = new Font("",6);
			while(m_objPrintLineContext.m_BlnHaveMoreSign)
			{
				m_objPrintLineContext.m_mthPrintNextSign(30+10,m_intYPos,p_objPrintPageArg.Graphics,fntSign);

				m_intYPos += (int)enmRectangleInfo.RowStep-10;				
			}

			//ȫ������			

			m_mthPrintFoot(p_objPrintPageArg);

			//			m_objPrintLineContext.m_mthReset();
			//
			//			m_intYPos = 145;
			//
			//			m_intCurrentPage = 1;
		}

		
#region PrintClasses
		private abstract class clsPrintInPatientCaseInfo : com.digitalwave.Utility.Controls.clsPrintLineBase
		{
			protected clsInPatientCaseHistoryContent  m_objContent;
			/// <summary>
			/// ���־�����ߵı߾�
			/// </summary>
			protected int m_intRecBaseX = clsPrintPosition.c_intLeftX;
			protected int m_intPatientInfoX = 70;
			protected clsPrintInfo_InPatientCaseHistory m_objPrintInfo;
	
			public override object  m_ObjPrintLineInfo
			{
				get
				{
					return base.m_blnHaveMoreLine;
				}
				set
				{
					if(value == null)return;
					object [] objData = (object[])value;
					m_objContent = (clsInPatientCaseHistoryContent )objData[0];					
					m_objPrintInfo=(clsPrintInfo_InPatientCaseHistory )objData[1];
				}				
			}			
		}

		/// <summary>
		/// ��ӡ��һҳ�Ĺ̶�����
		/// </summary>
		private class clsPrintPatientFixInfo : clsPrintInPatientCaseInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				//				p_intPosY += 30;
				p_objGrp.DrawString("ס Ժ �� ��",clsInPatientCaseHistoryPrintTool.m_fotItemHead,Brushes.Black,m_intRecBaseX+290,p_intPosY - 10);
		
				p_intPosY += 20;
				p_objGrp.DrawString("������"+ m_objPrintInfo.m_strPatientName,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
				p_objGrp.DrawString("��¼���ڣ�"+(m_objContent==null ? "": m_objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmInPatientCaseHistory"))),p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
		
				p_intPosY += 20;
				p_objGrp.DrawString("���䣺"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strAge),p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
				p_objGrp.DrawString("��ʷ�����˺Ϳɿ��̶ȣ�"+(m_objContent==null ? "": m_objContent.m_strRepresentor+"," +m_objContent.m_strCredibility),p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
		
				p_intPosY += 20;
				p_objGrp.DrawString("�Ա�"+ m_objPrintInfo.m_strSex ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
				p_objGrp.DrawString("��ʷ��¼�ߣ�"+(m_objContent==null ? "" : /*m_objContent.m_strCreateName*/new clsEmployee(m_objContent.m_strModifyUserID).m_StrFirstName) ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);

				p_intPosY += 20;
				p_objGrp.DrawString("���᣺"+ m_objPrintInfo.m_strNativePlace ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
				p_objGrp.DrawString("���ţ�"+m_objPrintInfo.m_strAreaName+m_objPrintInfo.m_strBedName ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
		
				p_intPosY += 20;
				p_objGrp.DrawString("ְҵ��"+ m_objPrintInfo.m_strOccupation ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
				p_objGrp.DrawString("סԺ�ţ�"+ m_objPrintInfo.m_strHISInPatientID ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
		
				p_intPosY += 20;
				p_objGrp.DrawString("���"+ m_objPrintInfo.m_strMarried,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
				p_objGrp.DrawString("��ϵ�ˣ�"+m_objPrintInfo.m_StrLinkManFirstName ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);

				p_intPosY += 20;
				p_objGrp.DrawString("���壺"+ m_objPrintInfo.m_strNationality ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
				p_objGrp.DrawString("�绰��"+ m_objPrintInfo.m_strHomePhone ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
		
				p_intPosY += 20;
				if(m_objPrintInfo.m_dtmInPatientDate != DateTime.MinValue)
				{
					p_objGrp.DrawString("��Ժ���ڣ�"+ m_objPrintInfo.m_dtmHISInPatientDate.ToString("yyyy��MM��dd�� HH:mm"),p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);			
				}
				else
				{
					p_objGrp.DrawString("��Ժ���ڣ�",p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
				}

				//				p_objGrp.DrawString("��ַ��"+m_objPrintInfo.m_strHomeAddress,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);

				m_objPrintContext.m_mthSetContextWithAllCorrect("��ַ��"+ m_objPrintInfo.m_strHomeAddress ,"<root />");

				//				if(m_objPrintContext.m_BlnHaveNextLine())
				//				{
				//					if(m_objPrintContext.m_BlnHaveNextLine())
				//					{
				//						m_objPrintContext.m_mthPrintLine(380,m_intPatientInfoX+350,p_intPosY,p_objGrp);
				//
				//						p_intPosY += 30;
				//					}
				//				}
				//				else
				//				{
				//					p_intPosY += 30;
				//				}

				//				if(m_objPrintContext.m_BlnHaveNextLine())
				//					m_blnHaveMoreLine = true;
				//				else 
				//				{
				//					m_blnHaveMoreLine = false;
				//					p_intPosY += 30;
				//				}

				int intRealHeight;
				Rectangle rtgBlock = new Rectangle(m_intPatientInfoX+350,p_intPosY,(int)enmRectangleInfo.RightX-(m_intPatientInfoX+350),30);
				m_objPrintContext.m_blnPrintAllBySimSun(11,rtgBlock,p_objGrp,out intRealHeight,false);

				p_intPosY += 30;
				m_blnHaveMoreLine = false;
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();

				m_blnHaveMoreLine = true;
			}
		}


		/// <summary>
		/// �дκͲ���(��ͣ��)
		/// </summary>
//		private class clsPrintInPatientCasePergAndBorn : clsPrintInPatientCaseInfo
//		{
//			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
//			private bool m_blnIsFirstPrint = true;
//
//			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
//			{
//				if(m_objContent == null || MDIParent.m_EnmCaseType != frmInPatientCaseHistory.enmCaseType.����)
//				{
//					m_blnHaveMoreLine = false;
//					return;
//				}
//
//				if(m_blnIsFirstPrint)
//				{
//					if(m_objContent!=null)
//					{
//						p_intPosY -= 10;
//						p_objGrp.DrawString("�� ��"+ (m_objContent==null ? "0": m_objContent.m_strPregTimes) + "  ��",p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
//						p_objGrp.DrawString("�� ��"+(m_objContent==null ? "0": m_objContent.m_strBornTimes) + "  ��",p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
//						p_intPosY += 20;
//						m_blnIsFirstPrint = false;	
//					}
//					m_blnHaveMoreLine = false;
//					return;
//				}
//			
//				if(m_objPrintContext.m_BlnHaveNextLine())
//				{
//					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2,m_intRecBaseX+10,p_intPosY,p_objGrp);
//
//					p_intPosY += 20;
//				}
//
//				if(m_objPrintContext.m_BlnHaveNextLine())
//					m_blnHaveMoreLine = true;
//				else
//					m_blnHaveMoreLine = false;
//			}
//
//			public override void m_mthReset()
//			{
//				m_objPrintContext.m_mthRestartPrint();
//				m_blnIsFirstPrint = true;
//				m_blnHaveMoreLine = true;
//			}
//		}

		/// <summary>
		/// ����
		/// </summary>
		private class clsPrintInPatientCaseMain : clsPrintInPatientCaseInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null || m_objContent.m_strMainDescription == "")
				{
					m_blnHaveMoreLine = false;

					return;
				}

				if(m_blnIsFirstPrint)
				{
					p_intPosY += 20;

					p_objGrp.DrawString("���ߣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					if(m_objContent!=null)
					{
						if(m_objContent.m_strMainDescriptionAll.Length == 0)
						{
							//							p_intPosY += 30;

							m_blnHaveMoreLine = false;

							return;
						}
					}

					/*���״δ�ӡʱ��FirstPrintDate����m_dtmFirstPrintTime,�������Ƿ��״δ�ӡ��
						* ���FirstPrintDateΪnull,�Զ���ϵͳʱ�丳��m_dtmFirstPrintTime
						*/
					//					if(m_objContent.m_dtmFirstPrintDate==DateTime.MinValue)
					//						m_dtmFirstPrintTime=DateTime.Parse(new clsPublicDomain().m_strGetServerTime());					
					//					else 
					//					m_dtmFirstPrintTime=m_objContent.m_dtmFirstPrintDate;

					//���౾���Ѿ���m_dtmFirstPrintTime��ֵ

                    if (clsInPatientCaseHistoryPrintTool.m_blnIsPrintMark)
                    {
                        m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objContent == null ? "" : m_objContent.m_strMainDescriptionAll), (m_objContent == null ? "<root />" : m_objContent.m_strMainDescriptionXML), m_dtmFirstPrintTime, m_objContent != null);
                        m_mthAddSign2("���ߣ�", m_objPrintContext.m_ObjModifyUserArr);
                    }
                    else
                    {
                        m_objPrintContext.m_mthSetContextWithAllCorrect((m_objContent == null ? "" : m_objContent.m_strMainDescriptionAll), (m_objContent == null ? "<root />" : m_objContent.m_strMainDescriptionXML));
                    }

					m_blnIsFirstPrint = false;					
				}
			
				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+60,p_intPosY,p_objGrp);

					p_intPosY += 20;

					intLine++;
				}			

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
				else
				{
					//					if(intLine == 1)
					//						p_intPosY += 30;
					//					if(intLine == 0)
					//						p_intPosY += 30;				
					m_blnHaveMoreLine = false;
				}
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();

				m_blnHaveMoreLine = true;

				m_blnIsFirstPrint = true;
			}
		}

		/// <summary>
		/// �ֲ�ʷ
		/// </summary>
		private class clsPrintInPatientCaseCurrent : clsPrintInPatientCaseInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null || m_objContent.m_strCurrentStatus == "")
				{
					m_blnHaveMoreLine = false;

					return;
				}

				if(m_blnIsFirstPrint)
				{
				
					p_objGrp.DrawString("�ֲ�ʷ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);

					p_intPosY += 20;

					if(m_objContent!=null)
					{
						if(m_objContent.m_strCurrentStatusXAll.Length == 0)
						{
							//							p_intPosY += 30;

							m_blnHaveMoreLine = false;

							return;
						}
					}

					//					if(m_objContent.m_dtmFirstPrintDate==DateTime.MinValue)
					//						m_dtmFirstPrintTime=DateTime.Parse(new clsPublicDomain().m_strGetServerTime());					
					//					else 
					//						m_dtmFirstPrintTime=m_objContent.m_dtmFirstPrintDate;
                    if (clsInPatientCaseHistoryPrintTool.m_blnIsPrintMark)
                    {
                        m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objContent==null ? "" : m_objContent.m_strCurrentStatusXAll)  ,(m_objContent==null ? "<root />" : m_objContent.m_strCurrentStatusXML),m_dtmFirstPrintTime,m_objContent!=null);
					    m_mthAddSign2("�ֲ�ʷ��",m_objPrintContext.m_ObjModifyUserArr);
                    }
                    else
                    {
                        m_objPrintContext.m_mthSetContextWithAllCorrect((m_objContent == null ? "" : m_objContent.m_strCurrentStatusXAll), (m_objContent == null ? "<root />" : m_objContent.m_strCurrentStatusXML));
                    }
					

					m_blnIsFirstPrint = false;					
				}
			
				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+50,p_intPosY,p_objGrp);

					p_intPosY += 20;

					intLine++;
				}			

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
				else
				{
					//					if(intLine == 1)
					//						p_intPosY += 30;
					//					if(intLine == 0)
					//						p_intPosY += 60;

					m_blnHaveMoreLine = false;
				}
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();

				m_blnHaveMoreLine = true;

				m_blnIsFirstPrint = true;
			}
		}

		/// <summary>
		/// ����ʷ
		/// </summary>
		private class clsPrintInPatientBeforetimeStatus : clsPrintInPatientCaseInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;

			private int m_intTimes = 0;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null || m_objContent.m_strBeforetimeStatus == "")
				{
					m_blnHaveMoreLine = false;

					return;
				}

				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("����ʷ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);

					p_intPosY += 20;

					if(m_objContent!=null)
					{
						if(m_objContent.m_strBeforetimeStatusAll.Length == 0)
						{
							//							p_intPosY += 30;

							m_blnHaveMoreLine = false;

							return;
						}
					}
					else
					{
						//						p_intPosY += 30;

						m_blnHaveMoreLine = false;

						return;
					}
					//					if(m_objContent.m_dtmFirstPrintDate==DateTime.MinValue)
					//						m_dtmFirstPrintTime=DateTime.Parse(new clsPublicDomain().m_strGetServerTime());					
					//					else 
					//						m_dtmFirstPrintTime=m_objContent.m_dtmFirstPrintDate;
                    if (clsInPatientCaseHistoryPrintTool.m_blnIsPrintMark)
                    {
                        m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objContent==null ? "" : m_objContent.m_strBeforetimeStatusAll)  ,(m_objContent==null ? "<root />" : m_objContent.m_strBeforetimeStatusXML),m_dtmFirstPrintTime,m_objContent!=null);
                        m_mthAddSign2("����ʷ��", m_objPrintContext.m_ObjModifyUserArr);
                    }
                    else
                    {
                        m_objPrintContext.m_mthSetContextWithAllCorrect((m_objContent == null ? "" : m_objContent.m_strBeforetimeStatusAll), (m_objContent == null ? "<root />" : m_objContent.m_strBeforetimeStatusXML));
                    }

					m_blnIsFirstPrint = false;					
			
				}
			

				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+50,p_intPosY,p_objGrp);

					p_intPosY += 20;

					m_intTimes++;
				}

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
				else
				{
					//					if(m_intTimes < 3)
					//					{
					//						p_intPosY += (3-m_intTimes)*30;
					//
					//						if(m_intTimes == 0)
					//							p_intPosY += 30;
					//					}
				
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

		/// <summary>
		/// ����ʷ
		/// </summary>
		private class clsPrintInPatientOwenStatus : clsPrintInPatientCaseInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;

			private int m_intTimes = 0;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null || m_objContent.m_strOwnHistory == "" || MDIParent.m_EnmCaseType == frmInPatientCaseHistory.enmCaseType.����)
				{
					m_blnHaveMoreLine = false;

					return;
				}

				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("����ʷ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);

					p_intPosY += 20;

					if(m_objContent!=null)
					{
						if(m_objContent.m_strOwnHistoryAll.Length == 0)
						{
							//							p_intPosY += 60;

							m_blnHaveMoreLine = false;

							return;
						}
					}
					else
					{
						//						p_intPosY += 60;

						m_blnHaveMoreLine = false;

						return;
					}
				
					//					if(m_objContent.m_dtmFirstPrintDate==DateTime.MinValue)
					//						m_dtmFirstPrintTime=DateTime.Parse(new clsPublicDomain().m_strGetServerTime());					
					//					else 
					//						m_dtmFirstPrintTime=m_objContent.m_dtmFirstPrintDate;
                    if (clsInPatientCaseHistoryPrintTool.m_blnIsPrintMark)
                    {
                        m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objContent==null ? "" : m_objContent.m_strOwnHistoryAll)  ,(m_objContent==null ? "<root />" : m_objContent.m_strOwnHistoryXML),m_dtmFirstPrintTime,m_objContent!=null);
                        m_mthAddSign2("����ʷ��", m_objPrintContext.m_ObjModifyUserArr);
                    }
                    else
                    {
                        m_objPrintContext.m_mthSetContextWithAllCorrect((m_objContent == null ? "" : m_objContent.m_strOwnHistoryAll), (m_objContent == null ? "<root />" : m_objContent.m_strOwnHistoryXML));
                    }
					

					m_blnIsFirstPrint = false;					
			
				}
			
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+50,p_intPosY,p_objGrp);

					p_intPosY += 20;

					m_intTimes++;
				}

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
				else
				{
					//					if(m_intTimes < 3)
					//					{
					//						p_intPosY += (3-m_intTimes)*30;
					//
					//						if(m_intTimes == 0)
					//							p_intPosY += 30;
					//					}
				
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
		/// <summary>
		/// ����ʷ
		/// </summary>
		private class clsPrintInPatientMarriageStatus : clsPrintInPatientCaseInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;

			private int m_intTimes = 0;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
                if (m_objContent == null || m_objContent.m_strMarriageHistory == "" || m_objPrintInfo.m_strMarried.Trim() == "δ��")
				{
					m_blnHaveMoreLine = false;

					return;
				}

				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("����ʷ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);

					p_intPosY += 20;

					if(m_objContent!=null)
					{
						if(m_objContent.m_strMarriageHistoryAll.Length == 0)
						{
							//							p_intPosY += 60;

							m_blnHaveMoreLine = false;

							return;
						}
					}
					else
					{
						//						p_intPosY += 60;

						m_blnHaveMoreLine = false;

						return;
					}

					//					if(m_objContent.m_dtmFirstPrintDate==DateTime.MinValue)
					//						m_dtmFirstPrintTime=DateTime.Parse(new clsPublicDomain().m_strGetServerTime());					
					//					else 
					//						m_dtmFirstPrintTime=m_objContent.m_dtmFirstPrintDate;
                    if (clsInPatientCaseHistoryPrintTool.m_blnIsPrintMark)
                    {
                        m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objContent==null ? "" : m_objContent.m_strMarriageHistoryAll)  ,(m_objContent==null ? "<root />" : m_objContent.m_strMarriageHistoryXML),m_dtmFirstPrintTime,m_objContent!=null);
                        m_mthAddSign2("����ʷ��", m_objPrintContext.m_ObjModifyUserArr);
                    }
                    else
                    {
                        m_objPrintContext.m_mthSetContextWithAllCorrect((m_objContent == null ? "" : m_objContent.m_strMarriageHistoryAll), (m_objContent == null ? "<root />" : m_objContent.m_strMarriageHistoryXML));
                    }
					

					m_blnIsFirstPrint = false;					
			
				}
			
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine(650,m_intRecBaseX+50,p_intPosY,p_objGrp);

					p_intPosY += 20;

					m_intTimes++;
				}

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
				else
				{
					//					if(m_intTimes < 3)
					//					{
					//						p_intPosY += (3-m_intTimes)*30;
					//
					//						if(m_intTimes == 0)
					//							p_intPosY += 30;
					//					}
				
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

		/// <summary>
		/// �������¾�ʷ(��ͣ��)
		/// </summary>
//		private class clsPrintInPatientYJS : clsPrintInPatientCaseInfo
//		{
//			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
//
//			/// <summary>
//			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
//			/// </summary>
//			private bool m_blnIsFirstPrint = true;
//
//			private int m_intTimes = 0;
//
//			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
//			{
//				if(m_objContent == null || m_objContent.m_strYJS == "")
//				{
//					m_blnHaveMoreLine = false;
//
//					return;
//				}
//
//				if(m_blnIsFirstPrint)
//				{
//					p_objGrp.DrawString("�¾�ʷ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
//
//					p_intPosY += 20;
//
//					if(m_objContent!=null)
//					{
//						if(m_objContent.m_strYJS.Length == 0)
//						{
//							//							p_intPosY += 60;
//
//							m_blnHaveMoreLine = false;
//
//							return;
//						}
//					}
//					else
//					{
//						//						p_intPosY += 60;
//
//						m_blnHaveMoreLine = false;
//
//						return;
//					}
//
//					//					if(m_objContent.m_dtmFirstPrintDate==DateTime.MinValue)
//					//						m_dtmFirstPrintTime=DateTime.Parse(new clsPublicDomain().m_strGetServerTime());					
//					//					else 
//					//						m_dtmFirstPrintTime=m_objContent.m_dtmFirstPrintDate;
//					m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objContent==null ? "" : m_objContent.m_strYJSAll)  ,(m_objContent==null ? "<root />" : m_objContent.m_strYJSXML),m_dtmFirstPrintTime,m_objContent!=null);
//					m_mthAddSign2("�¾�ʷ��",m_objPrintContext.m_ObjModifyUserArr);
//
//					m_blnIsFirstPrint = false;					
//			
//				}
//			
//				if(m_objPrintContext.m_BlnHaveNextLine())
//				{
//					m_objPrintContext.m_mthPrintLine(650,m_intRecBaseX+50,p_intPosY,p_objGrp);
//
//					p_intPosY += 20;
//
//					m_intTimes++;
//				}
//
//				if(m_objPrintContext.m_BlnHaveNextLine())
//					m_blnHaveMoreLine = true;
//				else
//				{
//					//					if(m_intTimes < 3)
//					//					{
//					//						p_intPosY += (3-m_intTimes)*30;
//					//
//					//						if(m_intTimes == 0)
//					//							p_intPosY += 30;
//					//					}
//				
//					m_blnHaveMoreLine = false;
//				}
//			}
//
//			public override void m_mthReset()
//			{
//				m_objPrintContext.m_mthRestartPrint();
//
//				m_blnIsFirstPrint = true;
//
//				m_blnHaveMoreLine = true;
//
//				m_intTimes = 0;
//			}
//		}

		/// <summary>
		/// ���Ʊ������(��ͣ��)
		/// </summary>
//		private class clsPrintInPatientXBS : clsPrintInPatientCaseInfo
//		{
//			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
//
//			/// <summary>
//			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
//			/// </summary>
//			private bool m_blnIsFirstPrint = true;
//
//			private int m_intTimes = 0;
//
//			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
//			{
//				if(m_objContent == null || m_objContent.m_strContraHistory == "" || MDIParent.m_EnmCaseType == frmInPatientCaseHistory.enmCaseType.����)
//				{
//					m_blnHaveMoreLine = false;
//
//					return;
//				}
//
//				if(m_blnIsFirstPrint)
//				{
//					p_objGrp.DrawString("���������",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
//
//					p_intPosY += 20;
//
//					if(m_objContent!=null)
//					{
//						if(m_objContent.m_strContraHistory.Length == 0)
//						{
//							//							p_intPosY += 60;
//
//							m_blnHaveMoreLine = false;
//
//							return;
//						}
//					}
//					else
//					{
//						//						p_intPosY += 60;
//
//						m_blnHaveMoreLine = false;
//
//						return;
//					}
//
//					//					if(m_objContent.m_dtmFirstPrintDate==DateTime.MinValue)
//					//						m_dtmFirstPrintTime=DateTime.Parse(new clsPublicDomain().m_strGetServerTime());					
//					//					else 
//					//						m_dtmFirstPrintTime=m_objContent.m_dtmFirstPrintDate;
//					m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objContent==null ? "" : m_objContent.m_strContraHistoryAll)  ,(m_objContent==null ? "<root />" : m_objContent.m_strContraHistoryXML),m_dtmFirstPrintTime,m_objContent!=null);
//					m_mthAddSign2("���������",m_objPrintContext.m_ObjModifyUserArr);
//
//					m_blnIsFirstPrint = false;					
//			
//				}
//			
//				if(m_objPrintContext.m_BlnHaveNextLine())
//				{
//					m_objPrintContext.m_mthPrintLine(650,m_intRecBaseX+50,p_intPosY,p_objGrp);
//
//					p_intPosY += 20;
//
//					m_intTimes++;
//				}
//
//				if(m_objPrintContext.m_BlnHaveNextLine())
//					m_blnHaveMoreLine = true;
//				else
//				{
//					//					if(m_intTimes < 3)
//					//					{
//					//						p_intPosY += (3-m_intTimes)*30;
//					//
//					//						if(m_intTimes == 0)
//					//							p_intPosY += 30;
//					//					}
//				
//					m_blnHaveMoreLine = false;
//				}
//			}
//
//			public override void m_mthReset()
//			{
//				m_objPrintContext.m_mthRestartPrint();
//
//				m_blnIsFirstPrint = true;
//
//				m_blnHaveMoreLine = true;
//
//				m_intTimes = 0;
//			}
//		}

		/// <summary>
		/// �����������Ƽ���(��ͣ��)
		/// </summary>
//		private class clsPrintInPatientOldMaternitySuffer : clsPrintInPatientCaseInfo
//		{
//			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
//
//			/// <summary>
//			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
//			/// </summary>
//			private bool m_blnIsFirstPrint = true;
//
//			private int m_intTimes = 0;
//
//			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
//			{
//				if(m_objContent == null || m_objContent.m_strOldMaternitySuffer == "" || MDIParent.m_EnmCaseType == frmInPatientCaseHistory.enmCaseType.����)
//				{
//					m_blnHaveMoreLine = false;
//					return;
//				}
//
//				if(m_blnIsFirstPrint)
//				{
//					p_objGrp.DrawString("�������Ƽ�����",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
//
//					p_intPosY += 20;
//
//					if(m_objContent!=null)
//					{
//						if(m_objContent.m_strOldMaternitySuffer.Length == 0)
//						{
//							m_blnHaveMoreLine = false;
//							return;
//						}
//					}
//					else
//					{
//						m_blnHaveMoreLine = false;
//						return;
//					}
//
//					m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objContent==null ? "" : m_objContent.m_strOldMaternitySufferAll)  ,(m_objContent==null ? "<root />" : m_objContent.m_strOldMaternitySufferXML),m_dtmFirstPrintTime,m_objContent!=null);
//					m_mthAddSign2("�������Ƽ�����",m_objPrintContext.m_ObjModifyUserArr);
//
//					m_blnIsFirstPrint = false;					
//			
//				}
//			
//				if(m_objPrintContext.m_BlnHaveNextLine())
//				{
//					m_objPrintContext.m_mthPrintLine(650,m_intRecBaseX+50,p_intPosY,p_objGrp);
//
//					p_intPosY += 20;
//
//					m_intTimes++;
//				}
//
//				if(m_objPrintContext.m_BlnHaveNextLine())
//					m_blnHaveMoreLine = true;
//				else
//				{
//					m_blnHaveMoreLine = false;
//				}
//			}
//
//			public override void m_mthReset()
//			{
//				m_objPrintContext.m_mthRestartPrint();
//
//				m_blnIsFirstPrint = true;
//
//				m_blnHaveMoreLine = true;
//
//				m_intTimes = 0;
//			}
//		}

		/// <summary>
		/// ��������ʷ(��ͣ��)
		/// </summary>
//		private class clsPrintInPatientShYS : clsPrintInPatientCaseInfo
//		{
//			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
//
//			/// <summary>
//			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
//			/// </summary>
//			private bool m_blnIsFirstPrint = true;
//
//			private int m_intTimes = 0;
//
//			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
//			{
//				if(m_objContent == null || m_objContent.m_strShYS == "")
//				{
//					m_blnHaveMoreLine = false;
//
//					return;
//				}
//
//				if(m_blnIsFirstPrint)
//				{
//					p_objGrp.DrawString("����ʷ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
//
//					p_intPosY += 20;
//
//					if(m_objContent!=null)
//					{
//						if(m_objContent.m_strShYS.Length == 0)
//						{
//							//							p_intPosY += 60;
//
//							m_blnHaveMoreLine = false;
//
//							return;
//						}
//					}
//					else
//					{
//						//						p_intPosY += 60;
//
//						m_blnHaveMoreLine = false;
//
//						return;
//					}
//
//					//					if(m_objContent.m_dtmFirstPrintDate==DateTime.MinValue)
//					//						m_dtmFirstPrintTime=DateTime.Parse(new clsPublicDomain().m_strGetServerTime());					
//					//					else 
//					//						m_dtmFirstPrintTime=m_objContent.m_dtmFirstPrintDate;
//					m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objContent==null ? "" : m_objContent.m_strShYSAll)  ,(m_objContent==null ? "<root />" : m_objContent.m_strShYSXML),m_dtmFirstPrintTime,m_objContent!=null);
//					m_mthAddSign2("����ʷ��",m_objPrintContext.m_ObjModifyUserArr);
//
//					m_blnIsFirstPrint = false;					
//			
//				}
//			
//				if(m_objPrintContext.m_BlnHaveNextLine())
//				{
//					m_objPrintContext.m_mthPrintLine(650,m_intRecBaseX+50,p_intPosY,p_objGrp);
//
//					p_intPosY += 20;
//
//					m_intTimes++;
//				}
//
//				if(m_objPrintContext.m_BlnHaveNextLine())
//					m_blnHaveMoreLine = true;
//				else
//				{
//					//					if(m_intTimes < 3)
//					//					{
//					//						p_intPosY += (3-m_intTimes)*30;
//					//
//					//						if(m_intTimes == 0)
//					//							p_intPosY += 30;
//					//					}
//				
//					m_blnHaveMoreLine = false;
//				}
//			}
//
//			public override void m_mthReset()
//			{
//				m_objPrintContext.m_mthRestartPrint();
//
//				m_blnIsFirstPrint = true;
//
//				m_blnHaveMoreLine = true;
//
//				m_intTimes = 0;
//			}
//		}

		/// <summary>
		/// �ٲ����(��ͣ��)
		/// </summary>
//		private class clsPrintInPatientLCQK : clsPrintInPatientCaseInfo
//		{
//			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
//
//			/// <summary>
//			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
//			/// </summary>
//			private bool m_blnIsFirstPrint = true;
//
//			private int m_intTimes = 0;
//
//			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
//			{
//				if(m_objContent == null || m_objContent.m_strLCQK == "" || MDIParent.m_EnmCaseType != frmInPatientCaseHistory.enmCaseType.����)
//				{
//					m_blnHaveMoreLine = false;
//
//					return;
//				}
//
//				if(m_blnIsFirstPrint)
//				{
//					p_objGrp.DrawString("�ٲ������",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
//
//					p_intPosY += 20;
//
//					if(m_objContent!=null)
//					{
//						if(m_objContent.m_strLCQK.Length == 0)
//						{
//							//							p_intPosY += 60;
//
//							m_blnHaveMoreLine = false;
//
//							return;
//						}
//					}
//					else
//					{
//						//						p_intPosY += 60;
//
//						m_blnHaveMoreLine = false;
//
//						return;
//					}
//
//					//					if(m_objContent.m_dtmFirstPrintDate==DateTime.MinValue)
//					//						m_dtmFirstPrintTime=DateTime.Parse(new clsPublicDomain().m_strGetServerTime());					
//					//					else 
//					//						m_dtmFirstPrintTime=m_objContent.m_dtmFirstPrintDate;
//					m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objContent==null ? "" : m_objContent.m_strLCQKAll)  ,(m_objContent==null ? "<root />" : m_objContent.m_strLCQKXML),m_dtmFirstPrintTime,m_objContent!=null);
//					m_mthAddSign2("�ٲ������",m_objPrintContext.m_ObjModifyUserArr);
//
//					m_blnIsFirstPrint = false;					
//			
//				}
//			
//				if(m_objPrintContext.m_BlnHaveNextLine())
//				{
//					m_objPrintContext.m_mthPrintLine(650,m_intRecBaseX+50,p_intPosY,p_objGrp);
//
//					p_intPosY += 20;
//
//					m_intTimes++;
//				}
//
//				if(m_objPrintContext.m_BlnHaveNextLine())
//					m_blnHaveMoreLine = true;
//				else
//				{
//					//					if(m_intTimes < 3)
//					//					{
//					//						p_intPosY += (3-m_intTimes)*30;
//					//
//					//						if(m_intTimes == 0)
//					//							p_intPosY += 30;
//					//					}
//				
//					m_blnHaveMoreLine = false;
//				}
//			}
//
//			public override void m_mthReset()
//			{
//				m_objPrintContext.m_mthRestartPrint();
//
//				m_blnIsFirstPrint = true;
//
//				m_blnHaveMoreLine = true;
//
//				m_intTimes = 0;
//			}
//		}

		/// <summary>
		/// ��ǰ���(��ͣ��)
		/// </summary>
//		private class clsPrintInPatientCQJC : clsPrintInPatientCaseInfo
//		{
//			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
//
//			/// <summary>
//			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
//			/// </summary>
//			private bool m_blnIsFirstPrint = true;
//
//			private int m_intTimes = 0;
//
//			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
//			{
//				if(m_objContent == null || m_objContent.m_strCQJC == "" || MDIParent.m_EnmCaseType != frmInPatientCaseHistory.enmCaseType.����)
//				{
//					m_blnHaveMoreLine = false;
//
//					return;
//				}
//
//				if(m_blnIsFirstPrint)
//				{
//					p_objGrp.DrawString("��ǰ��飺",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
//
//					p_intPosY += 20;
//
//					if(m_objContent!=null)
//					{
//						if(m_objContent.m_strCQJC.Length == 0)
//						{
//							//							p_intPosY += 60;
//
//							m_blnHaveMoreLine = false;
//
//							return;
//						}
//					}
//					else
//					{
//						//						p_intPosY += 60;
//
//						m_blnHaveMoreLine = false;
//
//						return;
//					}
//
//					//					if(m_objContent.m_dtmFirstPrintDate==DateTime.MinValue)
//					//						m_dtmFirstPrintTime=DateTime.Parse(new clsPublicDomain().m_strGetServerTime());					
//					//					else 
//					//						m_dtmFirstPrintTime=m_objContent.m_dtmFirstPrintDate;
//					m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objContent==null ? "" : m_objContent.m_strCQJCAll)  ,(m_objContent==null ? "<root />" : m_objContent.m_strCQJCXML),m_dtmFirstPrintTime,m_objContent!=null);
//					m_mthAddSign2("��ǰ��飺",m_objPrintContext.m_ObjModifyUserArr);
//
//					m_blnIsFirstPrint = false;					
//			
//				}
//			
//				if(m_objPrintContext.m_BlnHaveNextLine())
//				{
//					m_objPrintContext.m_mthPrintLine(650,m_intRecBaseX+50,p_intPosY,p_objGrp);
//
//					p_intPosY += 20;
//
//					m_intTimes++;
//				}
//
//				if(m_objPrintContext.m_BlnHaveNextLine())
//					m_blnHaveMoreLine = true;
//				else
//				{
//					//					if(m_intTimes < 3)
//					//					{
//					//						p_intPosY += (3-m_intTimes)*30;
//					//
//					//						if(m_intTimes == 0)
//					//							p_intPosY += 30;
//					//					}
//				
//					m_blnHaveMoreLine = false;
//				}
//			}
//
//			public override void m_mthReset()
//			{
//				m_objPrintContext.m_mthRestartPrint();
//
//				m_blnIsFirstPrint = true;
//
//				m_blnHaveMoreLine = true;
//
//				m_intTimes = 0;
//			}
//		}

		/// <summary>
		/// �¾�ʷ
		/// </summary>
		private class clsPrintInPatientCaseCatameniaHistory : clsPrintInPatientCaseInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			//			private bool m_blnIsFirstPrint = true;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null || m_objContent.m_intSelectedMC == 0 || m_objPrintInfo.m_strSex.Trim() == "��")
				{
					m_blnHaveMoreLine = false;
					return;
				}

				p_objGrp.DrawString("�¾�����ʷ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
				p_intPosY += 30;
			
				string strLastTime = "";
				if(m_objContent.m_strCatameniaCase!="�Ѿ���")
					strLastTime = m_objContent.m_dtmLastCatameniaTime.ToString("yyyy��M��d��")+"��";

				p_objGrp.DrawString(m_objContent.m_strFirstCatamenia+"                  "+strLastTime+m_objContent.m_strCatameniaCase+"��"+m_objContent.m_strCatameniaHistory,p_fntNormalText,Brushes.Black,m_intRecBaseX+50,p_intPosY);

				p_objGrp.DrawLine(new Pen(Brushes.Black),m_intRecBaseX+90,p_intPosY+10,m_intRecBaseX+150,p_intPosY+10);

				p_objGrp.DrawString(m_objContent.m_strCatameniaLastTime,new Font("",8),Brushes.Black,m_intRecBaseX+100,p_intPosY - 5);
				p_objGrp.DrawString(m_objContent.m_strCatameniaCycle,new Font("",8),Brushes.Black,m_intRecBaseX+100,p_intPosY + 13);

				m_blnHaveMoreLine = false;

				p_intPosY += 40;

			#region old
				//				if(m_blnIsFirstPrint)
				//				{
				//					
				//					p_objGrp.DrawString("�¾�����ʷ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
				//
				//					p_intPosY += 20;
				//
				//					if(m_objContent!=null)
				//					{
				//						if(m_objContent.m_strCatameniaHistoryAll.Length == 0)
				//						{
				////							p_intPosY += 60;
				//
				//							m_blnHaveMoreLine = false;
				//
				//							return;
				//						}
				//					}
				//					else
				//					{
				////						p_intPosY += 60;
				//
				//						m_blnHaveMoreLine = false;
				//
				//						return;
				//					}
				//
				//					//					if(m_objContent.m_dtmFirstPrintDate==DateTime.MinValue)
				//					//						m_dtmFirstPrintTime=DateTime.Parse(new clsPublicDomain().m_strGetServerTime());					
				//					//					else 
				//					//						m_dtmFirstPrintTime=m_objContent.m_dtmFirstPrintDate;
				//					m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objContent==null ? "" : m_objContent.m_strCatameniaHistoryAll ) ,(m_objContent==null ? "<root />" : m_objContent.m_strCatameniaHistoryXML),m_dtmFirstPrintTime,m_objContent!=null);
				//					m_mthAddSign("�¾�����ʷ��",m_objPrintContext.m_ObjModifyUserArr);
				//
				//					m_blnIsFirstPrint = false;
				//				}
				//				
				//				int intLine = 0;
				//				if(m_objPrintContext.m_BlnHaveNextLine())
				//				{
				//					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+50,p_intPosY,p_objGrp);
				//
				//					p_intPosY += 20;
				//
				//					intLine++;
				//				}	
				//		
				//				if(m_objPrintContext.m_BlnHaveNextLine())
				//					m_blnHaveMoreLine = true;
				//				else
				//				{	
				////					if(intLine == 1)
				////						p_intPosY += 30;
				//
				//					m_blnHaveMoreLine = false;
				//				}
			#endregion
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();

				m_blnHaveMoreLine = true;

				//				m_blnIsFirstPrint = true;
			}
		}
	
		/// <summary>
		///����ʷ
		/// </summary>
		private class clsPrintInPatientFamilyStatus : clsPrintInPatientCaseInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private bool m_blnIsFirstPrint = true;

			private int m_intTimes = 0;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null || m_objContent.m_strFamilyHistory == "")
				{
					m_blnHaveMoreLine = false;

					return;
				}

				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("����ʷ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);

					p_intPosY += 20;

					if(m_objContent!=null)
					{
						if(m_objContent.m_strFamilyHistoryAll.Length == 0)
						{
							//							p_intPosY += 60;

							m_blnHaveMoreLine = false;

							return;
						}
					}
					else
					{
						//						p_intPosY += 60;

						m_blnHaveMoreLine = false;

						return;
					}

					//					if(m_objContent.m_dtmFirstPrintDate==DateTime.MinValue)
					//						m_dtmFirstPrintTime=DateTime.Parse(new clsPublicDomain().m_strGetServerTime());					
					//					else 
					//						m_dtmFirstPrintTime=m_objContent.m_dtmFirstPrintDate;
                    if (clsInPatientCaseHistoryPrintTool.m_blnIsPrintMark)
                    {
                        m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objContent==null ? "" : m_objContent.m_strFamilyHistoryAll)  ,(m_objContent==null ? "<root />" : m_objContent.m_strFamilyHistoryXML),m_dtmFirstPrintTime,m_objContent!=null);
                        m_mthAddSign2("����ʷ��", m_objPrintContext.m_ObjModifyUserArr);
                    }
                    else
                    {
                        m_objPrintContext.m_mthSetContextWithAllCorrect((m_objContent == null ? "" : m_objContent.m_strFamilyHistoryAll), (m_objContent == null ? "<root />" : m_objContent.m_strFamilyHistoryXML));
                    }

					m_blnIsFirstPrint = false;					
			
				}
			
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+50,p_intPosY,p_objGrp);

					p_intPosY += 20;

					m_intTimes++;
				}

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
				else
				{
					//					if(m_intTimes < 3)
					//					{
					//						p_intPosY += (3-m_intTimes)*30;
					//
					//						if(m_intTimes == 0)
					//							p_intPosY += 30;
					//					}
				
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
		/// <summary>
		/// ����ժҪ
		/// </summary>
		private class clsPrintInPatientSummeryStatus : clsPrintInPatientCaseInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private bool m_blnIsFirstPrint = true;

			private int m_intTimes = 0;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				//����Ҫ��ӡ����ժҪ
			
				m_blnHaveMoreLine = false;
				return;


//				if( m_objContent == null || m_objContent.m_strSummary == "")
//				{
//					m_blnHaveMoreLine = false;
//
//					return;
//				}

			
				//				if(m_blnIsFirstPrint)
				//				{
				//					p_objGrp.DrawString("����ժҪ",clsInPatientCaseHistoryPrintTool.m_fotItemHead,Brushes.Black,m_intRecBaseX+330,p_intPosY);
				//	
				//					p_intPosY += 30;
				//					if(m_objContent!=null)
				//					{
				//						if(m_objContent.m_strSummaryAll.Length == 0)
				//						{
				//							p_intPosY += 60;
				//
				//							m_blnHaveMoreLine = false;
				//
				//							return;
				//						}
				//					}
				//					else
				//					{
				//						p_intPosY += 60;
				//
				//						m_blnHaveMoreLine = false;
				//
				//						return;
				//					}
				//
				//					//					if(m_objContent.m_dtmFirstPrintDate==DateTime.MinValue)
				//					//						m_dtmFirstPrintTime=DateTime.Parse(new clsPublicDomain().m_strGetServerTime());					
				//					//					else 
				//					//						m_dtmFirstPrintTime=m_objContent.m_dtmFirstPrintDate;
				//					m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objContent==null ? "" : m_objContent.m_strSummaryAll ) ,(m_objContent==null ? "<root />" : m_objContent.m_strSummaryXML),m_dtmFirstPrintTime,m_objContent!=null);
				//					m_mthAddSign("����ժҪ��",m_objPrintContext.m_ObjModifyUserArr);
				//
				//					m_blnIsFirstPrint = false;					
				//				
				//				}
				//				
				//				if(m_objPrintContext.m_BlnHaveNextLine())
				//				{
				//					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+50,p_intPosY,p_objGrp);
				//
				//					p_intPosY += 30;
				//
				//					m_intTimes++;
				//				}
				//
				//				if(m_objPrintContext.m_BlnHaveNextLine())
				//					m_blnHaveMoreLine = true;
				//				else
				//				{
				//					if(m_intTimes < 3)
				//					{
				//						p_intPosY += (3-m_intTimes)*30;
				//
				//						if(m_intTimes == 0)
				//							p_intPosY += 30;
				//					}
				//					
				//					m_blnHaveMoreLine = false;
				//				}
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();

				m_blnIsFirstPrint = true;

				m_blnHaveMoreLine = true;

				m_intTimes = 0;
			}
		}
		/// <summary>
		/// ������� 
		/// </summary>
		private class clsPrintInPatientLabStatus : clsPrintInPatientCaseInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private bool m_blnIsFirstPrint = true;

			private int m_intTimes = 0;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null || m_objContent.m_strLabCheck == "")
				{
					m_blnHaveMoreLine = false;

					return;
				}

				if(m_blnIsFirstPrint)
				{
					p_intPosY += 30;
					p_objGrp.DrawString("������飺",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
				
					p_intPosY += 30;
					if(m_objContent!=null)
					{
						if(m_objContent.m_strLabCheckAll.Length == 0)
						{
							p_intPosY += 30;

							m_blnHaveMoreLine = false;

							return;
						}
					}
					else
					{
						p_intPosY += 30;

						m_blnHaveMoreLine = false;

						return;
					}

					//					if(m_objContent.m_dtmFirstPrintDate==DateTime.MinValue)
					//						m_dtmFirstPrintTime=DateTime.Parse(new clsPublicDomain().m_strGetServerTime());					
					//					else 
					//						m_dtmFirstPrintTime=m_objContent.m_dtmFirstPrintDate;
                    if (clsInPatientCaseHistoryPrintTool.m_blnIsPrintMark)
                    {
                       m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objContent==null ? "" : m_objContent.m_strLabCheckAll)  ,(m_objContent==null ? "<root />" : m_objContent.m_strLabCheckXML),m_dtmFirstPrintTime,m_objContent!=null);
                       m_mthAddSign2("������飺", m_objPrintContext.m_ObjModifyUserArr); 
                    }
                    else
                    {
                        m_objPrintContext.m_mthSetContextWithAllCorrect((m_objContent == null ? "" : m_objContent.m_strLabCheckAll), (m_objContent == null ? "<root />" : m_objContent.m_strLabCheckXML));
                    }

					m_blnIsFirstPrint = false;					
			
				}
			
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+50,p_intPosY,p_objGrp);

					p_intPosY += 30;

					m_intTimes++;
				}

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
				else
				{
					//					if(m_intTimes < 3)
					//					{
					//						p_intPosY += (3-m_intTimes)*30;
					//
					//						if(m_intTimes == 0)
					//							p_intPosY += 30;
					//					}
				
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
		/// <summary>
		///���������
		/// </summary>
		private class clsPrintInPatientBodyChekcFixStatus : clsPrintInPatientCaseInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private bool m_blnIsFirstPrint = true;

			private int m_intTimes = 0;

			private bool m_blnNeedNewPage = false;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				//				if(m_blnNeedNewPage)
				//				{
				//					m_blnNeedNewPage = false;
				//
				//					if(p_intPosY > 145)
				//					{
				//						m_blnHaveMoreLine = true;
				//						p_intPosY = 970;
				//						return;
				//					}
				//				}

				if(m_objContent == null || m_objContent.m_strMedical == "")
				{
					m_blnHaveMoreLine = false;

					return;
				}

				if(m_blnIsFirstPrint)
				{
					p_intPosY += 30;
					p_objGrp.DrawString("�� �� �� ��",clsInPatientCaseHistoryPrintTool.m_fotItemHead,Brushes.Black,m_intRecBaseX+330,p_intPosY);
			
					//					p_intPosY += 30;
					//					p_objGrp.DrawString("���£�"+(m_objContent==null ? " " : m_objContent.m_strTemperature)+"��,"+
					//										"������"+(m_objContent==null ? " " : m_objContent.m_strPulse)+"��/��,"+
					//										"������"+(m_objContent==null ? " " : m_objContent.m_strBreath)+"��/��,"+
					//										"Ѫѹ��"+(m_objContent==null ? " " : m_objContent.m_strSys)+"/"+(m_objContent==null ? 
					//										" " : m_objContent.m_strDia),p_fntNormalText,Brushes.Black,m_intRecBaseX+50,p_intPosY);

				
					if(m_objContent!=null)
					{
						p_intPosY += 30;
						string strAllText = "        ���£�"+(m_objContent==null ? " " : m_objContent.m_strTemperature)+"�桢"+
							"������"+(m_objContent==null ? " " : m_objContent.m_strPulse)+"��/�֡�"+
							"������"+(m_objContent==null ? " " : m_objContent.m_strBreath)+"��/�֡�"+
							"Ѫѹ��"+(m_objContent==null ? " " : m_objContent.m_strSys)+"/"+(m_objContent==null ? 
							" " : m_objContent.m_strDia)+"mmHg��"+(m_objContent==null ? "" : m_objContent.m_strMedicalAll);
						string strNormalXml = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("        ���£�"+(m_objContent==null ? " " : m_objContent.m_strTemperature)+"�桢"+
							"������"+(m_objContent==null ? " " : m_objContent.m_strPulse)+"��/�֡�"+
							"������"+(m_objContent==null ? " " : m_objContent.m_strBreath)+"��/�֡�"+
							"Ѫѹ��"+(m_objContent==null ? " " : m_objContent.m_strSys)+"/"+(m_objContent==null ? 
							" " : m_objContent.m_strDia)+"mmHg��",m_objContent.m_strCreateUserID,m_objContent.m_strCreateName,Color.Black);
						string strXml = ctlRichTextBox.s_strCombineXml(new string[]{strNormalXml,(m_objContent==null ? "<root />" : m_objContent.m_strMedicalXML)});
						//						if(m_objContent.m_strMedicalAll.Length == 0)
						//						{
						//							p_intPosY += 60;
						//
						//							m_blnHaveMoreLine = false;
						//
						//							return;
						//						}
                        if (clsInPatientCaseHistoryPrintTool.m_blnIsPrintMark)
                        {
                            m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText  ,strXml,m_dtmFirstPrintTime,m_objContent!=null);
                            m_mthAddSign2("����飺", m_objPrintContext.m_ObjModifyUserArr);
                        }
                        else
                        {
                            m_objPrintContext.m_mthSetContextWithAllCorrect(strAllText, strXml);
                        }

						m_blnIsFirstPrint = false;	
					}
					else
					{
						p_intPosY += 50;

						m_blnHaveMoreLine = false;

						return;
					}

					//					if(m_objContent.m_dtmFirstPrintDate==DateTime.MinValue)
					//						m_dtmFirstPrintTime=DateTime.Parse(new clsPublicDomain().m_strGetServerTime());					
					//					else 
					//						m_dtmFirstPrintTime=m_objContent.m_dtmFirstPrintDate;
				
				}
			
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2,m_intRecBaseX+10,p_intPosY,p_objGrp);

					p_intPosY += 25;

					m_intTimes++;
				}

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
				else
				{
					if(m_intTimes < 3)
					{
						p_intPosY += (3-m_intTimes)*25;

						if(m_intTimes == 0)
							p_intPosY += 25;
					}
				
					m_blnHaveMoreLine = false;
				}
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();

				m_blnIsFirstPrint = true;

				m_blnNeedNewPage = true;

				m_blnHaveMoreLine = true;

				m_intTimes = 0;
			}
		}
	
		/// <summary>
		///ר�Ƽ�� 
		/// </summary>
		private class clsPrintInPatientProfessionalStatus : clsPrintInPatientCaseInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private bool m_blnIsFirstPrint = true;

			private int m_intTimes = 0;

			private int m_intCurrentPic = 0;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null || m_objContent.m_strProfessionalCheck == "")
				{
					m_blnHaveMoreLine = false;

					return;
				}

				if(m_blnIsFirstPrint)
				{
					if(p_intPosY != 145) p_intPosY += 30;

					p_objGrp.DrawString("ר �� �� ��",clsInPatientCaseHistoryPrintTool.m_fotItemHead,Brushes.Black,m_intRecBaseX+330,p_intPosY);
			
					p_intPosY += 30;

					if(m_objContent!=null)
					{
						if(m_objContent.m_strProfessionalCheckAll.Length == 0 && m_objPrintInfo.m_objPicValueArr==null)
						{
							p_intPosY += 60;

							m_blnHaveMoreLine = false;

							return;
						}
					}
					else
					{
						//						p_intPosY += 60;

						m_blnHaveMoreLine = false;

						return;
					}

					//					if(m_objContent.m_dtmFirstPrintDate==DateTime.MinValue)
					//						m_dtmFirstPrintTime=DateTime.Parse(new clsPublicDomain().m_strGetServerTime());					
					//					else 
					//						m_dtmFirstPrintTime=m_objContent.m_dtmFirstPrintDate;
                    if (clsInPatientCaseHistoryPrintTool.m_blnIsPrintMark)
                    {
                        m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objContent==null ? "" : m_objContent.m_strProfessionalCheckAll)  ,(m_objContent==null ? "<root />" : m_objContent.m_strProfessionalCheckXML),m_dtmFirstPrintTime,m_objContent!=null);
                        m_mthAddSign2("ר�Ƽ�飺", m_objPrintContext.m_ObjModifyUserArr);
                    }
                    else
                    {
                        m_objPrintContext.m_mthSetContextWithAllCorrect((m_objContent == null ? "" : m_objContent.m_strProfessionalCheckAll), (m_objContent == null ? "<root />" : m_objContent.m_strProfessionalCheckXML));
                    }				

					m_blnIsFirstPrint = false;
			
				}

				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2,m_intRecBaseX+10,p_intPosY,p_objGrp);

					p_intPosY += 30;

					m_intTimes++;
				}

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
				else
				{
					if(m_intTimes < 3)
					{
						p_intPosY += (3-m_intTimes)*30;

						if(m_intTimes == 0)
							p_intPosY -= 90;
					}
					m_blnHaveMoreLine = false;
				}

				if(m_blnHaveMoreLine==false)
				{
				#region ��ӡ��ͼ
					if(m_objPrintInfo.m_objPicValueArr!=null && m_objPrintInfo.m_objPicValueArr.Length>0)
					{
						int intPicHeight = m_objPrintInfo.m_objPicValueArr[0].intHeight;
						int intPicWidth = m_objPrintInfo.m_objPicValueArr[0].intWidth;

						if(p_intPosY+intPicHeight>844)
						{
							p_intPosY += intPicHeight;
							m_blnHaveMoreLine = true;
							return;
						}
						else
						{
							p_intPosY += 30;
							int intLeft = m_intRecBaseX+10;
							for(int i=m_intCurrentPic;i<m_objPrintInfo.m_objPicValueArr.Length;i++)
							{					
								System.IO.MemoryStream objStream = new System.IO.MemoryStream((byte[])m_objPrintInfo.m_objPicValueArr[i].m_bytImage);
								Image imgPrint = new Bitmap(objStream);

								p_objGrp.DrawImage(imgPrint,intLeft,p_intPosY);
								intLeft += m_objPrintInfo.m_objPicValueArr[i].intWidth+10;
						
								//����ͼƬҪ��
								if(i+1<m_objPrintInfo.m_objPicValueArr.Length)
								{
									//ͼƬ����һ��
									if((int)enmRectangleInfo.RightX - intLeft < intPicWidth)
									{
										m_blnHaveMoreLine = true;
										p_intPosY += intPicHeight;
										intLeft = m_intRecBaseX+10;
										m_intCurrentPic = i + 1;
										return;										
									}
								}
							}
						}
					
						p_intPosY += intPicHeight;
					
					}
				#endregion 
				}				
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();

				m_blnIsFirstPrint = true;

				m_blnHaveMoreLine = true;

				m_intTimes = 0;

				//��ӡԤ�����ߴ�ӡ�󶼵�����
				m_intCurrentPic = 0;
			}
		}
	
		/// <summary>
		///��Ժ�����������     �����ϲ���Ҫ��ӡ
		///2004-06-30�������Ƽƻ�
		/// </summary>
		private class clsPrintPatientPrimaryDiagnoseInfo : clsPrintInPatientCaseInfo
		{
			private clsPrintRichTextContext m_objPrintContext1 = new clsPrintRichTextContext(Color.Black,new Font("",10));
//			private clsPrintRichTextContext m_objPrintContext2 = new clsPrintRichTextContext(Color.Black,new Font("",10));
//			private clsPrintRichTextContext m_objPrintContext3 = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private bool m_blnIsFirstPrint = true;

			private int m_intTimes = 0;
		
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
			
				if(m_objContent == null || m_objContent.m_strPrimaryDiagnoseAll == ""/* && m_objContent.m_strCarePlan == "")*/)
				{
					p_intPosY += 30;
					m_mthPrintDocSign(ref p_intPosY,p_objGrp,p_fntNormalText);
					m_blnHaveMoreLine = false;

					return;
				}
				if(m_blnIsFirstPrint)
				{					
					//					if(m_objContent!=null)
					//					{
					//						if(m_objContent.m_strPrimaryDiagnoseAll == null || m_objContent.m_strPrimaryDiagnoseAll.Length == 0)
					//						{
					//							p_intPosY += 60;
					//
					//							m_blnHaveMoreLine = false;
					//
					//							return;
					//						}
					//					}
					//					else
					//					{
					//						p_intPosY += 60;
					//
					//						m_blnHaveMoreLine = false;
					//
					//						return;
					//					}

					//					if(m_objContent.m_dtmFirstPrintDate==DateTime.MinValue)
					//						m_dtmFirstPrintTime=DateTime.Parse(new clsPublicDomain().m_strGetServerTime());					
					//					else 
					//					m_dtmFirstPrintTime=m_objContent.m_dtmFirstPrintDate;
					if(m_objContent.m_strPrimaryDiagnoseAll != null && m_objContent.m_strPrimaryDiagnoseAll.Trim() != "")
					{
                        if (clsInPatientCaseHistoryPrintTool.m_blnIsPrintMark)
                        {
                            m_objPrintContext1.m_mthSetContextWithCorrectBefore((m_objContent==null ? "" : m_objContent.m_strPrimaryDiagnoseAll)  ,(m_objContent==null ? "<root />" : m_objContent.m_strPrimaryDiagnoseXML),m_dtmFirstPrintTime,m_objContent!=null);
                            m_mthAddSign2("��Ժ��ϣ�", m_objPrintContext1.m_ObjModifyUserArr);
                        }
                        else
                        {
                            m_objPrintContext1.m_mthSetContextWithAllCorrect((m_objContent == null ? "" : m_objContent.m_strPrimaryDiagnoseAll), (m_objContent == null ? "<root />" : m_objContent.m_strPrimaryDiagnoseXML));
                        }
						
					}
//					m_objPrintContext2.m_mthSetContextWithCorrectBefore((m_objContent==null ? "" : m_objContent.m_strFinallyDiagnoseAll)  ,(m_objContent==null ? "<root />" : m_objContent.m_strFinallyDiagnoseXML),m_dtmFirstPrintTime,m_objContent!=null);
//					m_mthAddSign("�����ϣ�",m_objPrintContext2.m_ObjModifyUserArr);m_objContent.m_strCarePlanAll == null || m_objContent.m_strCarePlanAll.Trim() == ""

//					if(m_objContent.m_strFinallyDiagnoseAll != null && m_objContent.m_strFinallyDiagnoseAll.Trim() != "")
//					{
//						m_objPrintContext2.m_mthSetContextWithCorrectBefore((m_objContent==null ? "" : m_objContent.m_strFinallyDiagnoseAll)  ,(m_objContent==null ? "<root />" : m_objContent.m_strFinallyDiagnoseXML),m_dtmFirstPrintTime,m_objContent!=null);
//						m_mthAddSign("������ϣ�",m_objPrintContext2.m_ObjModifyUserArr);
//					}

//					if ((m_objContent.m_strCarePlanAll.Trim() != "") && (m_objContent.m_strCarePlanAll != null) && MDIParent.m_EnmCaseType == frmInPatientCaseHistory.enmCaseType.����)
//					{
//						m_objPrintContext3.m_mthSetContextWithCorrectBefore((m_objContent==null ? "" : m_objContent.m_strCarePlanAll)  ,(m_objContent==null ? "<root />" : m_objContent.m_strCarePlanXML),m_dtmFirstPrintTime,m_objContent!=null);
//						m_mthAddSign("���Ƽƻ���",m_objPrintContext3.m_ObjModifyUserArr);
//					}
					m_blnIsFirstPrint = false;					
			
				}
			
				//				if(m_objPrintContext1.m_BlnHaveNextLine() || m_objPrintContext2.m_BlnHaveNextLine())
				if(m_objPrintContext1.m_BlnHaveNextLine()/* || m_objPrintContext2.m_BlnHaveNextLine() */)
				{
					//				m_objPrintContext1.m_mthPrintLine(360,m_intRecBaseX+420,p_intPosY,p_objGrp);
					//				m_objPrintContext2.m_mthPrintLine(300,m_intRecBaseX+380,p_intPosY,p_objGrp);
//					if (m_objContent.m_strPrimaryDiagnoseAll != null && m_objContent.m_strPrimaryDiagnoseAll.Trim() != "")
//					{
//						if (m_objContent.m_strCarePlanAll.Trim() != "" && m_objContent.m_strCarePlanAll != null && MDIParent.m_EnmCaseType == frmInPatientCaseHistory.enmCaseType.����)
//						{
							m_objPrintContext1.m_mthPrintLine(330,m_intRecBaseX+380,p_intPosY,p_objGrp);
//							m_objPrintContext2.m_mthPrintLine(300,m_intRecBaseX+380,p_intPosY,p_objGrp);
//							m_objPrintContext3.m_mthPrintLine(330,m_intRecBaseX+405,p_intPosY,p_objGrp);
//						}
//						else
//						{
//							m_objPrintContext1.m_mthPrintLine(310,m_intRecBaseX+40,p_intPosY,p_objGrp);
//							m_objPrintContext2.m_mthPrintLine(330,m_intRecBaseX+405,p_intPosY,p_objGrp);
//						}
//					}
//					else if (MDIParent.m_EnmCaseType == frmInPatientCaseHistory.enmCaseType.����)
//						m_objPrintContext3.m_mthPrintLine(330,m_intRecBaseX+405,p_intPosY,p_objGrp);
					p_intPosY += 20;

					m_intTimes++;
				}
			
				//				if(m_objPrintContext1.m_BlnHaveNextLine() || m_objPrintContext2.m_BlnHaveNextLine())
				if(m_objPrintContext1.m_BlnHaveNextLine()/* || m_objPrintContext2.m_BlnHaveNextLine() */)
					m_blnHaveMoreLine = true;
				else
				{
					p_intPosY += 30;
					m_mthPrintDocSign(ref p_intPosY,p_objGrp,p_fntNormalText);

					//					if(m_intTimes < 3)
					//					{
					//						p_intPosY += (3-m_intTimes)*30;
					//
					//						if(m_intTimes == 0)
					//							
					//					}

					p_intPosY += 60;

					m_blnHaveMoreLine = false;
				}
			}
			
			private void m_mthPrintDocSign(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				//���������ͣ�ã����迼��
//				if (m_objPrintInfo.m_strAreaName != "����" && m_objPrintInfo.m_strAreaName != "����")
//				{
//					p_objGrp.DrawString("��¼��ǩ����"+(m_objContent==null ? "" : m_objContent.m_strCreateName) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+380,p_intPosY);
//
//				}
//				else
//				{
//					if(m_objPrintInfo.m_strAreaName == "����" && MDIParent.m_EnmCaseType != frmInPatientCaseHistory.enmCaseType.����)
//					{
//						p_objGrp.DrawString("����ҽʦ��"+(m_objContent==null ? "" : m_objContent.m_strChargeDoctor) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
//						p_objGrp.DrawString("סԺҽʦ��"+(m_objContent==null ? "" : m_objContent.m_strCreateName) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+250,p_intPosY);
//						p_objGrp.DrawString("����ʿ��"+(m_objContent==null ? "" : m_objContent.m_strMidWife) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+500,p_intPosY);
//					}
//					else
//					{
                        //p_objGrp.DrawString("����ҽʦ��"+(m_objContent==null ? "" : m_objContent.m_strDiretDoctor) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
                        //p_objGrp.DrawString("����ҽʦ��"+(m_objContent==null ? "" : m_objContent.m_strChargeDoctor) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+250,p_intPosY);
						p_objGrp.DrawString("��¼��ǩ����"+(m_objContent==null ? "" : m_objContent.m_strCreateName) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+500,p_intPosY);

//					}
//				}
			}

			public override void m_mthReset()
			{
				m_objPrintContext1.m_mthRestartPrint();
//				m_objPrintContext2.m_mthRestartPrint();

				m_blnIsFirstPrint = true;

				m_blnHaveMoreLine = true;

				m_intTimes = 0;
			}
		}
	
		/// <summary>
		/// ��Ժ�����������ǩ����ʱ��
		/// </summary>
		private class clsPrintPatientPrimaryDiagnoseNameDateInfo : clsPrintInPatientCaseInfo
		{
			private clsPrintRichTextContext m_objPrintContext1 = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsPrintRichTextContext m_objPrintContext2 = new clsPrintRichTextContext(Color.Black,new Font("",10));

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				//����Ҫ��ӡ��Ժ�����������ǩ����ʱ��
				//				string strPrimaryDoctorName = "";
				//				string strFinallyDoctorName = "";
				//				if(m_objContent!=null)
				//				{
				//					clsEmployee objEmplyee1 = new clsEmployee(m_objContent.m_strPrimaryDiagnoseDocID);
				//					clsEmployee objEmplyee2 = new clsEmployee(m_objContent.m_strFinallyDiagnoseDocID);
				//					strPrimaryDoctorName = objEmplyee1.m_StrFirstName;
				//					strFinallyDoctorName = objEmplyee2.m_StrFirstName;
				//				}
				//				p_objGrp.DrawString("��Ժ���ǩ����"+strPrimaryDoctorName ,p_fntNormalText,Brushes.Black,m_intRecBaseX+30,p_intPosY);
				//				p_objGrp.DrawString("������ǩ����"+strFinallyDoctorName ,p_fntNormalText,Brushes.Black,m_intRecBaseX+380,p_intPosY);
				//				
				//				p_intPosY += 30;
				//				p_objGrp.DrawString("��Ժ������ڣ�"+(m_objContent==null ? "" : m_objContent.m_strPrimaryDiagnoseDate) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+30,p_intPosY);
				//				p_objGrp.DrawString("���������ڣ�"+(m_objContent==null ? "" : m_objContent.m_strFinallyDiagnoseDate) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+380,p_intPosY);

				m_blnHaveMoreLine = false;
			}

			public override void m_mthReset()
			{
				m_objPrintContext1.m_mthRestartPrint();
				m_objPrintContext2.m_mthRestartPrint();

				m_blnHaveMoreLine = true;
			}
		}
	
	
		/// <summary>
		/// ���Title  //2004-06-30�������Ƽƻ�Title
		/// </summary>
		private class clsPrintPatientDiagnoseTitleInfo : clsPrintInPatientCaseInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY += 30;
				if (MDIParent.m_EnmCaseType == frmInPatientCaseHistory.enmCaseType.����)
				{}
//				p_objGrp.DrawString("������",p_fntNormalText,Brushes.Black,m_intRecBaseX+380,p_intPosY);
				if(m_objContent != null && m_objContent.m_strPrimaryDiagnoseAll != null && m_objContent.m_strPrimaryDiagnoseAll.Trim() != "")
				{
					if (m_objContent.m_strCarePlanAll == null || m_objContent.m_strCarePlanAll.Trim() == "" || MDIParent.m_EnmCaseType != frmInPatientCaseHistory.enmCaseType.����)
					{
						p_objGrp.DrawString("��Ժ��ϣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+380,p_intPosY);
//						p_objGrp.DrawString("�������",p_fntNormalText,Brushes.Black,m_intRecBaseX+380,p_intPosY);
					}
					else
					{
						p_objGrp.DrawString("��Ժ��ϣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+380,p_intPosY);
//						p_objGrp.DrawString("�������",p_fntNormalText,Brushes.Black,m_intRecBaseX+380,p_intPosY);
						p_objGrp.DrawString("���Ƽƻ���",p_fntNormalText,Brushes.Black,m_intRecBaseX+365,p_intPosY);
					}
				}
//				else if (m_objPrintInfo.m_strAreaName == "����" && MDIParent.m_EnmCaseType == frmInPatientCaseHistory.enmCaseType.����)
//				{
//					try
//					{
//						if (!(m_objContent.m_strCarePlanAll == null || m_objContent.m_strCarePlanAll.Trim() == ""))
//							p_objGrp.DrawString("���Ƽƻ���",p_fntNormalText,Brushes.Black,m_intRecBaseX+365,p_intPosY);
//					}
//					catch
//					{}
//				}
				p_intPosY += 20;
				m_blnHaveMoreLine = false;
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();

				m_blnHaveMoreLine = true;
			}
		}
	
	#endregion PrintClasses

	#region �������ֲ���
		/// <summary>
		/// ��ӡҳ��
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintFoot(System.Drawing.Printing.PrintPageEventArgs e)
		{
			float fltOffsetX=20;//X��ƫ����
			Font fntHeader = new Font("SimSun", 12);

			e.Graphics.DrawString("��    ҳ",fntHeader,Brushes.Black,385-fltOffsetX,e.PageBounds.Height-175);
			e.Graphics.DrawString(m_intCurrentPage.ToString(),fntHeader,Brushes.Black,425-fltOffsetX,e.PageBounds.Height-175);			
		}
		//��ӡ�߿�
		private void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
		{			
			Font fntTitle = new Font("SimSun", 18,FontStyle.Bold );
			Font fntHeader = new Font("SimSun", 12);

			e.Graphics.DrawRectangle(Pens.Black,m_intRecBaseX - 10,135,(int)enmRectangleInfo.RightX - (int)enmRectangleInfo.LeftX + 10,e.PageBounds.Height-330);
		}
		/// <summary>
		/// �������ֲ���
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{			            
			e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,m_fotSmallFont ,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_HospitalName  ));
		
			e.Graphics.DrawString("��     ��     ��     ¼",m_fotTitleFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_Name_Title ));
		

			e.Graphics.DrawString("������",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name_Title  ));
			e.Graphics.DrawString(m_objPrintInfo.m_strPatientName  ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name ));
	
			e.Graphics.DrawString("�Ա�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex_Title ));
			e.Graphics.DrawString(m_objPrintInfo.m_strSex,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex  ));

			e.Graphics.DrawString("���䣺",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age_Title ));
			e.Graphics.DrawString(m_objPrintInfo.m_strAge ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age ));

            e.Graphics.DrawString("���ң�", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name_Title));
            e.Graphics.DrawString(m_objPrintInfo.m_strDeptName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name));

            e.Graphics.DrawString("���ţ�", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo_Title));
            e.Graphics.DrawString(m_objPrintInfo.m_strBedName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo));	
		
			e.Graphics.DrawString("סԺ�ţ�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID_Title ));
			e.Graphics.DrawString(m_objPrintInfo.m_strHISInPatientID ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID ));	
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

	#endregion ��ӡ
		//
		//		/// <summary>
		//		/// ��ӡ��Ϣ.
		//		/// </summary>
		//		[Serializable]			
		//		private class clsPrintInfo_InPatientCaseHistory
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
		//			public string m_strNativePlace;//����
		//			public string m_strOccupation;//ְҵ
		//			public string m_strMarried;//���
		//			public string m_StrLinkManFirstName;//��ϵ��
		//			public string m_strNationality;//����
		//			public string m_strHomePhone;//�绰
		//			public string m_strHomeAddress;//��ַ
		//			
		//			public clsBaseCaseHistoryInfo m_objContent;
		//			public DateTime m_dtmFirstPrintDate;
		//			public bool m_blnIsFirstPrint;
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
		//		clsInPatientCaseHistoryPrintTool objPrintTool;
		//		private void m_mthDemoPrint_FromDataSource()
		//		{	
		//			objPrintTool=new clsInPatientCaseHistoryPrintTool();
		//			objPrintTool.m_mthInitPrintTool(null);	
		//			if(m_objBaseCurrentPatient==null)
		//				objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,DateTime.MinValue,DateTime.MinValue);
		//			else if(this.trvTime.SelectedNode ==null || this.trvTime.SelectedNode==trvTime.Nodes[0]|| trvTime.SelectedNode.Tag==null)
		//				objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,m_objBaseCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate,DateTime.MinValue);
		//			else 
		//				objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,m_objBaseCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate,DateTime.Parse(trvTime.SelectedNode.Tag.ToString()));
														
		//			objPrintTool.m_mthInitPrintContent();	
		//					
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
		//			objPrintTool=new clsInPatientCaseHistoryPrintTool();
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
		//		bool bbb=true;
		//		protected override long m_lngSubPrint()//����ԭ�����е�ͬ����ӡ����
		//		{
		//			if(bbb)
		//				m_mthDemoPrint_FromDataSource();
		//			else m_mthDemoPrint_FromFile();
		//			bbb= !bbb;
		//			return 1;
		//		}
		#endregion ���ⲿ���Ա���ӡ����ʾʵ��.
	}	
}

