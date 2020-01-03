using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;
using System.Windows.Forms;

namespace iCare
{
	/// <summary>
	/// ר�Ʋ�����ӡ����
	/// </summary>
	public class clsInpatMedRecPrintBase: infPrintRecord
	{
		#region Define
		private static string m_strTypeID;
		public static Hashtable m_hasItems;
        public static string m_strChildTitleName = "סԺ����";//����������,Ĭ��ΪסԺ����
        public static int m_intChildTitleNameOffSetX = 310;//������X����ƫ����
        /// <summary>
        /// �Ƿ��ӡ�޸ĺۼ�
        /// </summary>
        public static bool m_blnIsPrintMark = true;
		#endregion

		public clsInpatMedRecPrintBase()
		{
		}

		public clsInpatMedRecPrintBase(string p_strTypeID)
		{
			m_strTypeID = p_strTypeID;
		}
	
		public static string m_StrTypeID
		{
			get{return m_strTypeID;}
		}
       

		private bool m_blnIsFromDataSource=true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
		private bool m_blnWantInit=true;

        
		private clsInpatMedRecDomain m_objInpatMedRecDomain;
		protected clsPrintInfo_InpatMedRec m_objPrintInfo;
		
		/// <summary>
		/// ���ô�ӡ��Ϣ(�������ݿ��ȡʱҪ���ȵ���.)
		/// </summary>
		/// <param name="p_objPatient">����</param>
		/// <param name="p_dtmInPatientDate">��Ժ����</param>
		/// <param name="p_dtmOpenDate">OpenDate�������һ�δ�ӡ��μ�¼�������ͣ��粡����¼��������OpenDate</param>
		public void m_mthSetPrintInfo(clsPatient p_objPatient,DateTime p_dtmInPatientDate,DateTime p_dtmOpenDate)
		{		
			m_blnIsFromDataSource = true;//�����Ǵ����ݿ��ȡ
			clsPatient m_objPatient = p_objPatient;
			m_objPrintInfo = new clsPrintInfo_InpatMedRec();

			m_objPrintInfo.m_strInPatientID = m_objPatient!=null? m_objPatient.m_StrInPatientID:"";					
			m_objPrintInfo.m_strPatientName = m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrFirstName :"";
			m_objPrintInfo.m_strSex = m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrSex:"";
			m_objPrintInfo.m_strAge = m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrAge : "";
			m_objPrintInfo.m_strBedName = m_objPatient!=null? m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName :"";
			m_objPrintInfo.m_strDeptName = m_objPatient!=null? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjDept.m_StrDeptName :"";
            m_objPrintInfo.m_strAreaName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName : "";			
			m_objPrintInfo.m_dtmInPatientDate = p_dtmInPatientDate;
			m_objPrintInfo.m_dtmOpenDate = p_dtmOpenDate;

            m_objPrintInfo.m_dtmOutHospital = p_objPatient == null ? DateTime.MinValue : p_objPatient.m_DtmLastOutDate;
		
			m_objPrintInfo.m_strHomeplace= m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrBirthPlace:"";//������
			m_objPrintInfo.m_strNativePlace= m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrNativePlace:"";//����
			m_objPrintInfo.m_strOccupation=  m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrOccupation:"";//ְҵ
			m_objPrintInfo.m_strMarried= m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrMarried:"";//���
			m_objPrintInfo.m_strLinkManFirstName= m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrLinkManFirstName:"";//��ϵ��
			m_objPrintInfo.m_strNationality= m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrNation:"";//����
			m_objPrintInfo.m_strHomePhone=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrLinkManPhone:"";//�绰
			m_objPrintInfo.m_StrHomePC =m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrHomePC:"";//Postcode
			m_objPrintInfo.m_strHomeAddress=m_objPatient!=null? (m_objPatient.m_ObjPeopleInfo.m_StrHomeAddress) :"";//��ַ
			m_objPrintInfo.m_strOfficeName = m_objPatient!=null ? m_objPatient.m_ObjPeopleInfo.m_StrOffice_name:"";

            m_objPrintInfo.m_dtmHISInDate = m_objPatient != null ? m_objPatient.m_DtmSelectedHISInDate : DateTime.MinValue;
            m_objPrintInfo.m_strHISInPatientID = m_objPatient != null ? m_objPatient.m_StrHISInPatientID : "";
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
			m_blnWantInit = false;
			m_hasItems = null;
			if(m_objPrintInfo == null)
			{
				MDIParent.ShowInformationMessageBox("����m_mthInitPrintContent֮ǰ�����ȵ���m_mthSetPrintInfo����");
				return;
			}
		
			if(m_objPrintInfo.m_strInPatientID != "")
			{
				m_objInpatMedRecDomain = new clsInpatMedRecDomain();
				long lngRes = m_objInpatMedRecDomain.m_lngGetPrintInfo(m_strTypeID, ref m_objPrintInfo);
				if(lngRes <= 0)
				{
					m_mthSetPrintContent(null,DateTime.MinValue);
					return ;
				}
			}
			else
			{
				MDIParent.ShowInformationMessageBox("��ѡ��һ�����ˣ�");
				return;
			}
			m_hasItems = m_mthSetHashItem(m_objPrintInfo.m_objContent.m_objItemContents);
			//���ñ����ݵ���ӡ��,��ʹ�Ǵ�ӡ�հ׵�,����Ҳ����ִ��.(��:�ڱ������ڲ�,����֮�ϲ�׼��return���,���ǳ�������.)
			m_mthSetPrintContent((clsInpatMedRecContent )m_objPrintInfo.m_objContent,m_objPrintInfo.m_dtmFirstPrintDate);		
				
		}

		/// <summary>
		/// ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
		/// </summary>
		/// <param name="p_objPrintContent">��ӡ����</param>
		public void m_mthSetPrintContent(object p_objPrintContent)
		{
			m_blnWantInit=false;
			if(p_objPrintContent.GetType().Name !="clsPrintInfo_InpatMedRec")
			{
				MDIParent.ShowInformationMessageBox("��������");
			}
			m_blnIsFromDataSource=false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ
			m_objPrintInfo=(clsPrintInfo_InpatMedRec)p_objPrintContent;

            m_hasItems = m_mthSetHashItem(((clsPrintInfo_InpatMedRec)p_objPrintContent).m_objContent.m_objItemContents);
			m_mthSetPrintContent((clsInpatMedRecContent)m_objPrintInfo.m_objContent,m_objPrintInfo.m_dtmFirstPrintDate);
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
			m_fotHeaderFont = new Font("����", 18,FontStyle.Bold);
			m_fotItemHead = new Font("",13,FontStyle.Bold);
			m_fotSmallFont = new Font("SimSun",12);
            m_fotHospitalFont=new Font("����",15,FontStyle.Bold);
            m_fotChildFont = new Font("����", 18);
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
            if (m_blnIsFromDataSource == false || m_objPrintInfo==null || m_objPrintInfo.m_strInPatientID == "") return;
			//�����ӡ�ɹ�������������Ҫ���µ�ʱ�䣬����У�����ʱ�䡣 
			if(!((PrintEventArgs)p_objPrintArg).Cancel && m_objPrintInfo.m_blnIsFirstPrint)
			{
				m_objInpatMedRecDomain.m_lngUpdateFirstPrintDate(m_strTypeID, m_objPrintInfo);
			}                          
			m_mthEndPrintSub((PrintEventArgs)p_objPrintArg);
		}


		#region ��ӡ

		// ���ô�ӡ���ݡ�
		private  void m_mthSetPrintContent(clsInpatMedRecContent p_objContent,DateTime p_dtmFirstPrintDate)
		{
			m_mthSetPrintLineArr();
			m_objPrintLineContext.m_ObjPrintSign =  new com.digitalwave.Utility.Controls.clsPrintRecordSign();

			object [] objData = new Object[2];
			objData[0] = m_objChangePrintTextColor(p_objContent);
			objData[1] = m_objPrintInfo;
		
			//���ô�ӡ��Ϣ������Set Value��ȥ
			m_objPrintLineContext.m_ObjPrintLineInfo = objData;
			//�����ݿ��ó�����FirstPrintDate����ÿ����ӡ�������m_DtmFirstPrintTime���ڸ���������
			m_objPrintLineContext.m_DtmFirstPrintTime = p_dtmFirstPrintDate;
			m_mthSetSubPrintInfo();
		}

		/// <summary>
		/// ���ô�ӡ��
		/// </summary>
		protected virtual void m_mthSetPrintLineArr()
		{
		}

		/// <summary>
		/// ���ô�ӡ��Ϣ
		/// </summary>
		protected virtual void m_mthSetSubPrintInfo()
		{}

		private clsInpatMedRecContent m_objChangePrintTextColor(clsInpatMedRecContent p_objclsInPatientCase)
		{
			if(p_objclsInPatientCase==null || p_objclsInPatientCase.m_objItemContents == null || p_objclsInPatientCase.m_objItemContents.Length < 1)
				return null;
			//�Ѱ�ɫ��Ϊ��ɫ
			clsXML_DataGrid objclsXML_DataGrid=new clsXML_DataGrid();
		
			for (int i =0; i < p_objclsInPatientCase.m_objItemContents.Length; i++)
			{
				if(p_objclsInPatientCase.m_objItemContents[i] != null)
					p_objclsInPatientCase.m_objItemContents[i].m_strItemContentXml = objclsXML_DataGrid.m_strReplaceWhiteToBlack(p_objclsInPatientCase.m_objItemContents[i].m_strItemContentXml);
			}
			return p_objclsInPatientCase;
		}


#region �йش�ӡ������

		/// <summary>
		/// ��ӡ����������
		/// </summary>
		protected com.digitalwave.Utility.Controls.clsPrintContext m_objPrintLineContext;	

		/// <summary>
		/// ��ӡ�߿����߾�
		/// </summary>
		protected const int m_intRecBaseX = clsPrintPosition.c_intLeftX;
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
        /// ҽԺ����(���壬�Ӵ֣�С��)
        /// </summary>
        private Font m_fotHospitalFont;
        /// <summary>
        /// ������(���壬С��)
        /// </summary>
        private Font m_fotChildFont;
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
		private int m_intYPos=215 ;//= (int)enmRectangleInfo.TopY+5;155
	
		/// <summary>
		/// ���ӵ���Ϣ
		/// </summary>
		public enum enmRectangleInfo
		{

			/// <summary>
			/// ���ӵĶ���
			/// </summary>
			TopY = 210,//150
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

			BottomY=1125//1025

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
				PointF m_fReturnPoint;
				switch(p_intItemName)
				{   
            
					case (int)enmItemDefination.Page_HospitalName:
                        m_fReturnPoint = new PointF(340f, 100f);//(340f,40f)
						break;
					case (int)enmItemDefination.Page_Name_Title:
                        m_fReturnPoint = new PointF(280f, 130f);//(280f,70f)
						break;
					case (int)enmItemDefination.Name_Title :
                        m_fReturnPoint = new PointF(45f, 170f);//(45f,110f)
						break;
					case (int)enmItemDefination.Name:
                        m_fReturnPoint = new PointF(95f, 170f);//(95f,110f)
						break;

					case (int)enmItemDefination.Sex_Title :
                        m_fReturnPoint = new PointF(165f, 170f);//(165f,110f)
						break;
					case (int)enmItemDefination.Sex :
                        m_fReturnPoint = new PointF(210f, 170f);//210f,110f)
						break;

					case (int)enmItemDefination.Age_Title :
                        m_fReturnPoint = new PointF(260f, 170f);//(260f,110f)
						break;
					case (int)enmItemDefination.Age:
                        m_fReturnPoint = new PointF(305f, 170f);//(305f,110f)
						break;

					case (int)enmItemDefination.Dept_Name_Title:
                        m_fReturnPoint = new PointF(390f, 170f);//(390f,110f)
						break;
					case (int)enmItemDefination.Dept_Name :
                        m_fReturnPoint = new PointF(440f, 170f);//(440f,110f)
						break;
			
					case (int)enmItemDefination.BedNo_Title :
                        m_fReturnPoint = new PointF(555f, 170f);//(555f,110f)
						break;
					case (int)enmItemDefination.BedNo:
                        m_fReturnPoint = new PointF(605f, 170f);//(605f,110f)
						break;

					case (int)enmItemDefination.InPatientID_Title:
                        m_fReturnPoint = new PointF(647f, 170f);//(647f,110f)
						break;
					case (int)enmItemDefination.InPatientID :
                        m_fReturnPoint = new PointF(707f, 170f);//(707f,110f)
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
		protected enum enmRectangleInfoInPatientCaseInfo 
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

			BottomY=1124,//1024

			PrintWidth = 690,//670
			PrintWidth2 = 730,//710

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

			if(m_objPrintLineContext == null)
				return;

			while(m_objPrintLineContext.m_BlnHaveMoreLine)
			{
				//�������ݴ�ӡ
				m_objPrintLineContext.m_mthPrintNextLine(ref m_intYPos,p_objPrintPageArg.Graphics,m_fotSmallFont);

                if (m_intYPos > clsPrintPosition.c_intBottomY- 20//45
					&& m_objPrintLineContext.m_BlnHaveMoreLine)
				{
					//�������ݴ�ӡ������Ҫ��ҳ

					m_mthPrintFoot(p_objPrintPageArg);

					p_objPrintPageArg.HasMorePages = true;

					m_intYPos = 215;//155

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

		}


		#region PrintClasses

		internal abstract class clsIMR_PrintLineBase : com.digitalwave.Utility.Controls.clsPrintLineBase
		{
			protected clsInpatMedRecContent  m_objContent;
			/// <summary>
			/// ���־�����ߵı߾�
			/// </summary>
			protected int m_intRecBaseX = clsPrintPosition.c_intLeftX;
			protected int m_intPatientInfoX = 70;
			protected clsPrintInfo_InpatMedRec m_objPrintInfo;
			protected Hashtable m_hasItems;
			protected static string m_strMakedate = "0001-1-1";
			protected Font m_fotHead;

			protected clsIMR_PrintLineBase()
			{
				m_hasItems = clsInpatMedRecPrintBase.m_hasItems;
				m_strMakedate = DateTime.Now.ToString("yyyy-MM-dd HH:00:00");
				m_fotHead = new Font("SimSun", 10.5f,FontStyle.Bold);
			}
	
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
					m_objContent = (clsInpatMedRecContent )objData[0];					
					m_objPrintInfo=(clsPrintInfo_InpatMedRec )objData[1];
				}				
			}
			
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{}
			public override void m_mthReset()
			{}

			/// <summary>
			/// ��ȡ��Ŀ������
			/// </summary>
			/// <param name="p_strKeyArr">��ϣ������</param>
			/// <returns></returns>
			protected clsInpatMedRec_Item[] m_objGetContentFromItemArr(string[] p_strKeyArr)
			{
				if (m_hasItems == null || p_strKeyArr == null || m_hasItems.Count < 1)
					return null;
				clsInpatMedRec_Item[] objConArr = new clsInpatMedRec_Item[p_strKeyArr.Length];
				for (int i =0; i <objConArr.Length; i++)
				{
					if(m_hasItems.Contains(p_strKeyArr[i]))
						objConArr[i] = m_hasItems[p_strKeyArr[i]] as clsInpatMedRec_Item;
					else
						objConArr[i] = null;
				}
				return objConArr;
			}
			/// <summary>
			/// �жϴ�ӡ�����Ƿ�Ϊ��
			/// </summary>
			/// <param name="p_strKeysArr">��ϣ������</param>
			/// <returns></returns>
			protected bool m_blnHavePrintInfo(string[] p_strKeysArr)
			{
				if(m_hasItems == null || m_hasItems.Count < 1)
					return false;
				for(int i=0; i <p_strKeysArr.Length; i++)
				{
					if(m_hasItems.Contains(p_strKeysArr[i]))
						return true;
				}
				return false;
			}

		
			/// <summary>
			/// ���ɴ�ӡ�����ı��飨p_strName���ȱ����p_strKey����һ�£�
			/// </summary>
			/// <param name="p_hasItem">��ϣ��</param>
			/// <param name="p_strName">
			/// ��ӡ�ֶ��������飬���ȱ����p_strKey����һ�¡�
			/// 1�����ӵ��ֶ���#��ͷ�����Լ����жϣ�������ӡ�������ݣ�
			/// 2��ֻ��ӡ���ݲ���ӡ�ֶε��ÿգ���""
			/// </param>
			/// <param name="p_strKey">��ϣ�����顣ֻ��ӡ�ֶβ���ӡ���ݵļ�λ�ÿ�</param>
			/// <param name="p_strTextAll">�����ı�</param>
			/// <param name="p_strTextXML">XML�ı�</param>
			protected void m_mthMakeText(string[] p_strName,string[] p_strKey,ref string p_strTextAll, ref string p_strTextXML)
			{
				if(m_hasItems == null || m_hasItems.Count < 1 || p_strName == null || p_strKey == null || p_strKey.Length != p_strName.Length)
					return;
				string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
				string strSemicolonXML = "<root/>";
				bool blnIsFirst = true;
				string strXML = "";
                //ר�Ʋ����е�ctlRichTextBox�ڸñ������¼һ��ʱ����ӡҲ�������ɫ�޸ı�ǣ�
                //����ͨ����m_strMakedate���¸�ֵ���
                m_strMakedate = m_objContent.m_dtmFirstPrintDate.ToString("yyyy-MM-dd HH:mm:ss");
				for(int i =0; i < p_strName.Length; i++)
				{
					string strSam = "��";
					if(p_strName[i].EndsWith("$$"))
					{
						strSam = "";
						p_strName[i] = p_strName[i].Substring(0,p_strName[i].Length-2);
					}
					strSemicolonXML = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strSam,m_objContent.m_strCreateUserID,strFirstName,Color.Black,m_strMakedate);
					if (p_strKey[i] != "")
					{
						if(m_hasItems.Contains(p_strKey[i]) == true)
						{
							clsInpatMedRec_Item objItem = (clsInpatMedRec_Item)m_hasItems[p_strKey[i]];
							if(p_strName[i].StartsWith("#") == true)
							{
								p_strTextAll += p_strName[i].Substring(1);
								strXML = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(p_strName[i].Substring(1),m_objContent.m_strCreateUserID,strFirstName,Color.Black,m_strMakedate);
								if(p_strTextXML != "")
									p_strTextXML = ctlRichTextBox.s_strCombineXml(new string[]{p_strTextXML,strXML});
								else
									p_strTextXML = strXML;
								blnIsFirst = false;
							}
							else
							{
								if(objItem.m_strItemContentXml == null || objItem.m_strItemContentXml == "")
								{
									string strContent = "";
									if(m_strDateType == "")
										strContent = m_strCheckDateType(objItem.m_strItemContent);
									else
									{
										strContent = m_strCheckDateType(objItem.m_strItemContent,m_strDateType);
										m_strDateType = "";
									}
									p_strTextAll += (blnIsFirst ? "" : strSam) + p_strName[i] +strContent;
									strXML = blnIsFirst ? ctlRichTextBox.clsXmlTool.s_strMakeTextXml((p_strName[i]+strContent),m_objContent.m_strCreateUserID,strFirstName,Color.Black,m_strMakedate) :
										ctlRichTextBox.s_strCombineXml(new string[]{strSemicolonXML,ctlRichTextBox.clsXmlTool.s_strMakeTextXml((p_strName[i]+strContent),m_objContent.m_strCreateUserID,strFirstName,Color.Black,m_strMakedate)});
									if(p_strTextXML != "")
										p_strTextXML = ctlRichTextBox.s_strCombineXml(new string[]{p_strTextXML,strXML});
									else
										p_strTextXML = strXML;
								}
								else
								{
									p_strTextAll += ((blnIsFirst || p_strName[i] == "") ? "" : strSam) + p_strName[i] + objItem.m_strItemContent;
									strXML = (blnIsFirst || p_strName[i] == "") ? ctlRichTextBox.s_strCombineXml(new string[]{ctlRichTextBox.clsXmlTool.s_strMakeTextXml(p_strName[i],m_objContent.m_strCreateUserID,strFirstName,Color.Black,m_strMakedate),objItem.m_strItemContentXml}) :
										ctlRichTextBox.s_strCombineXml(new string[]{strSemicolonXML,ctlRichTextBox.clsXmlTool.s_strMakeTextXml(p_strName[i],m_objContent.m_strCreateUserID,strFirstName,Color.Black,m_strMakedate),objItem.m_strItemContentXml});
									if(p_strTextXML != "")
										p_strTextXML = ctlRichTextBox.s_strCombineXml(new string[]{p_strTextXML,strXML});
									else
										p_strTextXML = strXML;
								}
								blnIsFirst = false;
							}
						
						}
					}
					else
					{
						p_strTextAll += (blnIsFirst ? "" : strSam) + p_strName[i];
						strXML = blnIsFirst ? ctlRichTextBox.clsXmlTool.s_strMakeTextXml(p_strName[i],m_objContent.m_strCreateUserID,strFirstName,Color.Black,m_strMakedate) :
							ctlRichTextBox.s_strCombineXml(new string[]{strSemicolonXML,ctlRichTextBox.clsXmlTool.s_strMakeTextXml(p_strName[i],m_objContent.m_strCreateUserID,strFirstName,Color.Black,m_strMakedate)});
						if(p_strTextXML != "")
							p_strTextXML = ctlRichTextBox.s_strCombineXml(new string[]{p_strTextXML,strXML});
						else
							p_strTextXML = strXML;
						blnIsFirst = true;
					}
				}
			}

			/// <summary>
			/// �������ڸ�ʽ
			/// </summary>
			/// <param name="p_strValue"></param>
			/// <returns></returns>
			private string m_strCheckDateType(string p_strValue)
			{
				if(p_strValue.Length < 8)
					return p_strValue;
				string str = "";
				try
				{
					str = DateTime.Parse(p_strValue).ToString("yyyy��MM��dd��");
				}
				catch{return p_strValue;}
				return str;
			}

			/// <summary>
			/// ���ڸ�ʽҪ��
			/// </summary>
			protected string m_strDateType = "";
			/// <summary>
			/// ����Ҫ�󷵻��������ڸ�ʽ
			/// </summary>
			/// <param name="p_strValue"></param>
			/// <param name="p_strDateType"></param>
			/// <returns></returns>
			private string m_strCheckDateType(string p_strValue, string p_strDateType)
			{
				if(p_strValue.Length < 8)
					return p_strValue;
				string str = "";
				try
				{
					str = DateTime.Parse(p_strValue).ToString(p_strDateType);
				}
				catch{return p_strValue;}
				return str;
			}

			/// <summary>
			/// ����CheckBox�Ĵ�ӡ�ı�
			/// </summary>
			/// <param name="p_hasItem">��ϣ��</param>
			/// <param name="p_strName">�������飬�����һ��Ϊ��ʶ���ӵڶ��ʼ���Ǽ�</param>
			/// <param name="p_strTextAll">�����ı�</param>
			/// <param name="p_strTextXML">XML�ı�</param>
			protected void m_mthMakeCheckText(string[] p_strName,ref string p_strTextAll, ref string p_strTextXML)
			{
				if(m_hasItems == null || m_hasItems.Count < 1 || p_strName == null)
					return;
				bool blnPrintFirst = false;
				string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
				string strDH_XML = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("��",m_objContent.m_strCreateUserID,strFirstName,Color.Black,m_strMakedate);
				p_strTextAll += p_strName[0];
				string strXML = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(p_strName[0],m_objContent.m_strCreateUserID,strFirstName,Color.Black,m_strMakedate);
				if(p_strTextXML != "")
					p_strTextXML = ctlRichTextBox.s_strCombineXml(new string[]{p_strTextXML,strXML});
				else
					p_strTextXML = strXML;
				for(int i =1; i < p_strName.Length; i++)
				{
					if(m_hasItems.Contains(p_strName[i]) == true)
					{
						int index = p_strName[i].LastIndexOf(">");
						string strText = p_strName[i];
						if (index > 0)
							strText = p_strName[i].Substring(index+1);
						p_strTextAll += (blnPrintFirst == true ? "��" : "") + strText;
						p_strTextXML = ctlRichTextBox.s_strCombineXml(new string[]{p_strTextXML,(blnPrintFirst == true ? strDH_XML : "<root />"),ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strText,m_objContent.m_strCreateUserID,strFirstName,Color.Black,m_strMakedate)});
						blnPrintFirst = true;
					}
				}
				p_strTextAll += "��";
				p_strTextXML = ctlRichTextBox.s_strCombineXml(new string[]{p_strTextXML,ctlRichTextBox.clsXmlTool.s_strMakeTextXml("��",m_objContent.m_strCreateUserID,strFirstName,Color.Black,m_strMakedate)});
			}
            /// <summary>
            /// ����CheckBox�Ĵ�ӡ�ı�(Ĭ�Ͻ�Ϊ����"��")
            /// </summary>
            /// <param name="strHeader">�ı�ͷ</param>
            /// <param name="strFooter">�ı�β</param>
            /// <param name="strControlDescriptions">Check�ؼ�������</param>
            /// <param name="p_strTextAll">���ɵĴ�ӡ�ı�</param>
            /// <param name="p_strTextXML">���ɵĴ�ӡ�ı���XML����</param>
            protected void m_mthMakeCheckText(string strHeader, string strFooter, string[] strControlDescriptions, ref string p_strTextAll, ref string p_strTextXML)
            {
                if (m_hasItems == null || m_hasItems.Count < 1 || strControlDescriptions == null)
                    return;
                bool blnPrintFirst = false;
                string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                //Ԥ�����ɷ���"��" ��XML����
                string strDH_XML = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("��", m_objContent.m_strCreateUserID, strFirstName, Color.Black, m_strMakedate);
                //����ı�ͷ
                string strXML = null;
                if (!string.IsNullOrEmpty(strHeader))
                {
                    p_strTextAll += strHeader;
                    //��������ı�ͷ���ɶ�Ӧ��XML����
                    strXML = ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strHeader, m_objContent.m_strCreateUserID, strFirstName, Color.Black, m_strMakedate);
                }
                //���紫���p_strTextXML ����Я����ʼ��ֵ���ͽ���ֵ�ϲ�
                if (p_strTextXML != "")
                    p_strTextXML = ctlRichTextBox.s_strCombineXml(new string[] { p_strTextXML, strXML });
                else
                    p_strTextXML = strXML;

                //���ɸ�Check�ؼ���ֵ�ַ�����XML���������ֱ����Ǻϲ�
                foreach (string strControlDescription in strControlDescriptions)
                {
                    if (m_hasItems.Contains(strControlDescription))
                    {
                        //������ؼ��ı��ڿؼ������еĿ�ʼλ��
                        int index = strControlDescription.LastIndexOf(">");
                        //Ĭ�Ͽؼ�����Ϊ�ؼ��ı�
                        string strText = strControlDescription;
                        //����ؼ��ı��Ŀ�ʼλ�� > 0 ��Ҫ�������н�ȡ
                        if (index > 0)
                            strText = strControlDescription.Substring(index + 1);//�������н�ȡ�ؼ��ı�

                        p_strTextAll += (blnPrintFirst ? "��" : "") + strText;
                        //�ϲ��ؼ��ı���XML����
                        p_strTextXML = ctlRichTextBox.s_strCombineXml(new string[] { p_strTextXML, (blnPrintFirst ? strDH_XML : "<root />"), ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strText, m_objContent.m_strCreateUserID, strFirstName, Color.Black, m_strMakedate) });
                        blnPrintFirst = true;
                    }
                }
                if (!string.IsNullOrEmpty(strFooter))
                {
                    //����ı�β
                    p_strTextAll += strFooter;
                    //p_strTextAll += "��";
                    //�����ı�β��XML���� ���ϲ�
                    p_strTextXML = ctlRichTextBox.s_strCombineXml(new string[] { p_strTextXML, ctlRichTextBox.clsXmlTool.s_strMakeTextXml(strFooter, m_objContent.m_strCreateUserID, strFirstName, Color.Black, m_strMakedate) });
                }
            }
			private string m_strMakeSuperScriptXml(string p_strText,string p_strUserID,string p_strUserName,Color p_clrText,string p_strMakeTime,bool p_blnType)
			{
				string strXml = "<r><D>";
				string strText = "";
				if(p_blnType)
					strText = "5";
				else
					strText = "-3";
				strXml += "</D><U><UI D=\""+p_strUserID+"\" N=\""+p_strUserName+"\" S=\"1\" M=\""+p_strMakeTime+"\" C=\""+p_clrText.ToArgb()+"\" /><CI S=\"0\" E=\""+(p_strText.Length-1)+"\" Q=\"1\" /></U>"+"<SuperSubScript Index=\"0\" CharOffset=\""+strText+"\" Value=\""+p_strText+"\" /></r>";
				return strXml;
			}
			/// <summary>
			/// ����Ƿ���Ҫ��ҳ
			/// </summary>
			/// <param name="p_intPosY"></param>
			/// <param name="p_objGrp"></param>
			/// <param name="p_fntNormalText"></param>
			/// <param name="p_intHeight"></param>
			/// <returns></returns>
			protected virtual bool m_blnCheckBottom(ref int p_intPosY,int p_intHeight,int p_intPrintYPos)
			{
				if(p_intPrintYPos+p_intHeight+20 > ((int)enmRectangleInfo.BottomY -50))
				{
					p_intPosY += 500;
					return true;
				}
				return false;
			}
			protected virtual void m_mthPrintInBlock(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText,string p_strText)
			{
				RectangleF rtg = new RectangleF((int)enmRectangleInfo.LeftX+20,p_intPosY,(int)enmRectangleInfo.RightX-(int)enmRectangleInfo.LeftX-10,20);
				SizeF szfText = p_objGrp.MeasureString(p_strText,p_fntNormalText,Convert.ToInt32(rtg.Width));
				rtg.Height = szfText.Height+3;
				rtg.Y = p_intPosY;
				p_objGrp.DrawString(p_strText,p_fntNormalText,Brushes.Black,rtg);
			
				p_intPosY += Convert.ToInt32(rtg.Height);
			}
		}
        
		/// <summary>
		/// ��ӡ��һҳ�Ĺ̶�����
		/// </summary>
		internal class clsPrintPatientFixInfo : clsIMR_PrintLineBase
		{
            public clsPrintPatientFixInfo() { }
            public clsPrintPatientFixInfo(string p_strChildTitleName, int p_intChildTitleNameOffSetX)
            {
                m_strChildTitleName = p_strChildTitleName;
                m_intChildTitleNameOffSetX = p_intChildTitleNameOffSetX;
            
            }
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
                string m_strBirthPlace = "";
                if (m_hasItems != null)
                {
                    if (m_hasItems.Contains("������"))
                    {
                        clsInpatMedRec_Item objInpatItem = m_hasItems["������"] as clsInpatMedRec_Item;
                        if (objInpatItem != null)
                        {
                            m_strBirthPlace = objInpatItem.m_strItemContent;
                        }
                    }
                    else m_strBirthPlace = m_objPrintInfo.m_strHomeplace;
                }
                else m_strBirthPlace = m_objPrintInfo.m_strHomeplace;
				p_objGrp.DrawString("������"+ m_objPrintInfo.m_strPatientName,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
                p_objGrp.DrawString("�����أ�" + m_strBirthPlace, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

				p_intPosY += 20;
				p_objGrp.DrawString("�Ա�"+ m_objPrintInfo.m_strSex ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
                p_objGrp.DrawString("���壺" + m_objPrintInfo.m_strNationality, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);
                
                p_intPosY += 20;
                p_objGrp.DrawString("���䣺" + (m_objPrintInfo == null ? "" : m_objPrintInfo.m_strAge), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
				if(m_objPrintInfo.m_dtmInPatientDate != DateTime.MinValue)
				{
					p_objGrp.DrawString("��Ժ���ڣ�"+ m_objPrintInfo.m_dtmHISInDate.ToString("yyyy��MM��dd�� HH:mm"),p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);			
				}
				else
				{
					p_objGrp.DrawString("��Ժ���ڣ�",p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
				}

                p_intPosY += 20;
                p_objGrp.DrawString("���" + m_objPrintInfo.m_strMarried, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                p_objGrp.DrawString("��¼���ڣ�" + (m_objContent == null ? "" : m_objContent.m_dtmRecordDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmInPatientCaseHistory"))), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                p_intPosY += 20;
                p_objGrp.DrawString("ְҵ��" + m_objPrintInfo.m_strOccupation, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                p_objGrp.DrawString("��ʷ�����˺Ϳɿ��̶ȣ�" + (m_objContent == null ? "" : m_objContent.m_strRepresentor + " " + m_objContent.m_strCredibility), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                //p_objGrp.DrawString("���᣺"+ m_objPrintInfo.m_strNativePlace ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
                //p_objGrp.DrawString("���ţ�"+m_objPrintInfo.m_strAreaName+m_objPrintInfo.m_strBedName ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);   
                //p_objGrp.DrawString("סԺ�ţ�"+ m_objPrintInfo.m_strHISInPatientID ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
                //p_objGrp.DrawString("��ϵ�ˣ�"+m_objPrintInfo.m_strLinkManFirstName ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
                //p_objGrp.DrawString("�绰��"+ m_objPrintInfo.m_strHomePhone ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
                //p_objGrp.DrawString("��ʷ��¼�ߣ�"+(m_objContent==null ? "" : new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName) ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
				//m_objPrintContext.m_mthSetContextWithAllCorrect("��ַ��"+ m_objPrintInfo.m_strHomeAddress ,"<root />");

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
		/// ��ӡ��ͼ
		/// </summary>
		internal class clsPrintInPatMedRecPic : clsIMR_PrintLineBase
		{
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private int m_intCurrentPic = 0;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null || m_objPrintInfo.m_objContent.m_objPics == null|| m_objPrintInfo.m_objContent.m_objPics.Length < 1)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				m_blnHaveMoreLine = false;
				if(m_blnIsFirstPrint)
				{
					int intPicHeight = m_objPrintInfo.m_objContent.m_objPics[0].intHeight;
					int intPicWidth = m_objPrintInfo.m_objContent.m_objPics[0].intWidth;

                    if (p_intPosY + intPicHeight > clsPrintPosition.c_intBottomY-45)
					{
						p_intPosY += intPicHeight;
						m_blnHaveMoreLine = true;
						return;
					}
					else
					{
						p_intPosY += 20;
						int intLeft = m_intRecBaseX+10;
						for(int i=m_intCurrentPic;i<m_objPrintInfo.m_objContent.m_objPics.Length;i++)
						{					
							System.IO.MemoryStream objStream = new System.IO.MemoryStream((byte[])m_objPrintInfo.m_objContent.m_objPics[i].m_bytImage);
							Image imgPrint = new Bitmap(objStream);

							p_objGrp.DrawImage(imgPrint,intLeft,p_intPosY,m_objPrintInfo.m_objContent.m_objPics[i].intWidth,m_objPrintInfo.m_objContent.m_objPics[i].intHeight);
							intLeft += m_objPrintInfo.m_objContent.m_objPics[i].intWidth+10;
							intPicHeight = Math.Max(intPicHeight,m_objPrintInfo.m_objContent.m_objPics[i].intHeight);
						
							//����ͼƬҪ��
							if(i+1<m_objPrintInfo.m_objContent.m_objPics.Length)
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
					p_intPosY += intPicHeight+20;
					m_blnIsFirstPrint = false;	
				}
			}
			public override void m_mthReset()
			{
				m_blnIsFirstPrint = true;

				m_blnHaveMoreLine = true;

				//��ӡԤ�����ߴ�ӡ�󶼵�����
				m_intCurrentPic = 0;
			}
		}
		
		
		#endregion

		#region �������ֲ���
		/// <summary>
		/// ��ӡҳ��
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintFoot(System.Drawing.Printing.PrintPageEventArgs e)
		{
			Font fntHeader = new Font("SimSun", 12);

            e.Graphics.DrawString("��      ҳ", fntHeader, Brushes.Black, 385, clsPrintPosition.c_intBottomY+25);
            e.Graphics.DrawString(m_intCurrentPage.ToString(), fntHeader, Brushes.Black, 425, clsPrintPosition.c_intBottomY+25);	
			fntHeader.Dispose();
		}
		
		/// <summary>
		/// ��ӡ��
		/// </summary>
		/// <param name="e"></param>
		protected virtual void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
		{			
//			Font fntTitle = new Font("SimSun", 18,FontStyle.Bold );
//			Font fntHeader = new Font("SimSun", 12);

            e.Graphics.DrawRectangle(Pens.Black, m_intRecBaseX - 10, 195, (int)enmRectangleInfo.RightX - (int)enmRectangleInfo.LeftX + 10, clsPrintPosition.c_intBottomY-180);
		}


		/// <summary>
		/// �������ֲ���
		/// </summary>
		/// <param name="e"></param>
		protected virtual void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {
            StringFormat sfTitle = new StringFormat(StringFormatFlags.FitBlackBox);
            sfTitle.Alignment = StringAlignment.Center;
            sfTitle.LineAlignment = StringAlignment.Center;

            StringFormat sf = new StringFormat(StringFormatFlags.FitBlackBox);
            sf.Alignment = StringAlignment.Center;
            //SizeF m_szfHospitalTitle=e.Graphics.MeasureString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle.Trim(), m_fotHospitalFont);
            //SizeF m_szfChildTitle = e.Graphics.MeasureString(m_strChildTitleName, m_fotChildFont);
            //m_intChildTitleNameOffSetX=(int)(m_szfHospitalTitle.Width / 2 + m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_HospitalName).X - m_szfChildTitle.Width / 2+4);
            e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle.Trim(), m_fotHospitalFont, m_slbBrush, clsPrintPosition.m_rtgHospitalTitlePos, sfTitle);
            
            e.Graphics.DrawString(m_strChildTitleName, m_fotChildFont, Brushes.Black, clsPrintPosition.m_rtgFormTitlePos, sfTitle);
			
            e.Graphics.DrawString("������", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name_Title));

            e.Graphics.DrawString(m_objPrintInfo.m_strPatientName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name));

            e.Graphics.DrawString("�Ա�", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex_Title));

            e.Graphics.DrawString(m_objPrintInfo.m_strSex, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex));

            e.Graphics.DrawString("���䣺", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age_Title));

            e.Graphics.DrawString(m_objPrintInfo.m_strAge, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age));

            e.Graphics.DrawString("������", m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name_Title));

            e.Graphics.DrawString(m_objPrintInfo.m_strAreaName, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name));

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

			m_intYPos = 205;//145

			m_intCurrentPage = 1;

			clsPrintInPatMedRecPic2.m_blnIsPrinted = false;
		}

	#endregion ��ӡ

		/// <summary>
		/// �����������Ϊ������Hastable
		/// </summary>
		/// <param name="p_hasItem"></param>
		/// <param name="p_ctlItem"></param>
		/// <param name="p_objItemArr"></param>
		/// <returns></returns>
		protected virtual Hashtable m_mthSetHashItem(clsInpatMedRec_Item[] p_objItemArr)
		{
			if(p_objItemArr == null)
				return null;
			Hashtable hasItem = new Hashtable(300);
			foreach(clsInpatMedRec_Item objItem in p_objItemArr)
			{
				if(objItem.m_strItemContent == null || objItem.m_strItemContent == "" || objItem.m_strItemContent == "False")
					continue;
				try
				{
					hasItem.Add(objItem.m_strItemName,objItem);
				}
				catch(Exception ex)
				{
					continue;
//					string strEx = ex.ToString();
//					hasItem = null;
				}
			}
			return hasItem;
		}

	}
}
