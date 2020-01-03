using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.Utility.Controls;
using System.Drawing;

namespace iCare
{
	/// <summary>
	/// ��ǰС��Ĵ�ӡ������,Jacky-2003-6-6
	/// </summary>
	public class clsBeforeOperationSummaryPrintTool: infPrintRecord
	{	
		private bool m_blnIsFromDataSource=true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
		private bool m_blnWantInit=true;		
		private clsBeforeOperationSummaryDomain m_objRecordsDomain;
		private clsPrintInfo_BeforeOperationSummary m_objPrintInfo;
		private clsBeforeOperationSummary_All m_objclsBeforeOperationSummary_All=null;
		
		/// <summary>
		/// ���ô�ӡ��Ϣ(�������ݿ��ȡʱҪ���ȵ���.)
		/// </summary>
		/// <param name="p_objPatient">����</param>
		/// <param name="p_dtmInPatientDate">��Ժ����</param>
		/// <param name="p_dtmCreateDate">CreateDate�������һ�δ�ӡ��μ�¼�������ͣ��粡����¼��������CreateDate</param>
		public void m_mthSetPrintInfo(clsPatient p_objPatient,DateTime p_dtmInPatientDate,DateTime p_dtmCreateDate)
		{		
			m_blnIsFromDataSource=true;//�����Ǵ����ݿ��ȡ
			clsPatient m_objPatient=p_objPatient;
			m_objPrintInfo=new clsPrintInfo_BeforeOperationSummary();
			m_objPrintInfo.m_strInPatentID=m_objPatient!=null? m_objPatient.m_StrInPatientID:"";					
			m_objPrintInfo.m_strPatientName=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrFirstName :"";
			m_objPrintInfo. m_strSex=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrSex:"";
			m_objPrintInfo. m_strAge=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrAge : "";
			m_objPrintInfo. m_strBedName=m_objPatient!=null? m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName :"";
            m_objPrintInfo.m_strDeptName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjDept.m_StrDeptName: "";
            m_objPrintInfo.m_strAreaName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName : "";			
			m_objPrintInfo.m_dtmInPatientDate=p_dtmInPatientDate;
            m_objPrintInfo.m_dtmOpenDate = p_dtmCreateDate;//�����m_dtmOpenDateʵ���Ͼ���CreateDate,�ṹ����Ҫ�Ķ�	
            m_objPrintInfo.m_dtmHISInPatientDate = m_objPatient != null ? m_objPatient.m_DtmSelectedHISInDate : DateTime.MinValue;
            m_objPrintInfo.m_strHISInPatientID = m_objPatient != null ? m_objPatient.m_StrHISInPatientID : "";		
		}

		/// <summary>
		/// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���(�������ݿ��ȡʱҪ����.)
		/// </summary>
		public void m_mthInitPrintContent()
		{		
			m_blnWantInit=false;//
			if(m_objPrintInfo==null)
			{
				MDIParent.ShowInformationMessageBox("����m_mthInitPrintContent֮ǰ�����ȵ���m_mthSetPrintInfo����");
				return;
			}
			if(m_objPrintInfo.m_strInPatentID=="" || m_objPrintInfo.m_dtmOpenDate==DateTime.MinValue)
				m_objclsBeforeOperationSummary_All=null;				
			else
			{
				m_objRecordsDomain=new clsBeforeOperationSummaryDomain();	
				long lngRes=m_objRecordsDomain.m_lngGetSummary_All(m_objPrintInfo.m_strInPatentID,m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"),m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"),out m_objclsBeforeOperationSummary_All );
				if(lngRes <= 0)
					return ;   
			}
			//���ñ����ݵ���ӡ��			
			m_objPrintInfo.m_objclsBeforeOperationSummary_All=m_objclsBeforeOperationSummary_All;
			m_mthSetPrintValue();//�����з��ӡ����,��ʹ�ڴ�ӡ�հױ�ʱ,����Ҳ����ִ��.			
		}

		/// <summary>
		/// ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
		/// </summary>
		/// <param name="p_objPrintContent">��ӡ����</param>
		public void m_mthSetPrintContent(object p_objPrintContent)
		{
			 		 

			m_blnWantInit=false;
			if(p_objPrintContent.GetType().Name !="clsPrintInfo_BeforeOperationSummary")
			{
				MDIParent.ShowInformationMessageBox("��������");
			}
			m_blnIsFromDataSource=false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ
			m_objPrintInfo=(clsPrintInfo_BeforeOperationSummary)p_objPrintContent;
			m_objclsBeforeOperationSummary_All= m_objPrintInfo. m_objclsBeforeOperationSummary_All ;		
			m_mthSetPrintValue();			
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
			#region �йش�ӡ��ʼ��

			m_fotHospitalNameFont = new Font("SimSun", 16,FontStyle.Bold );
			m_fotTitleFont = new Font("SimSun", 16,FontStyle.Bold );
			m_fotHeaderFont = new Font("SimSun", 18);
			m_fotSmallFont = new Font("SimSun",12);
			m_GridPen = new Pen(Color.Black,1);
			m_slbBrush = new SolidBrush(Color.Black);
			m_objPageSetting = new clsPrintPageSettingForRecord();
			m_objCPaint=new clsPublicControlPaint();			

			#endregion
		}

		/// <summary>
		/// �ͷŴ�ӡ����
		/// </summary>
		public void m_mthDisposePrintTools(object p_objArg)
		{
			
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
			if(m_blnIsFromDataSource==false || m_objPrintInfo.m_strInPatentID=="" || m_objPrintInfo.m_objclsBeforeOperationSummary_All==null) return;
			//�����ӡ�ɹ�������������Ҫ���µ�ʱ�䣬����У�����ʱ�䡣 
			if(!((PrintEventArgs)p_objPrintArg).Cancel && m_objPrintInfo.m_objclsBeforeOperationSummary_All.m_strFirstPrintDate != null && m_objPrintInfo.m_objclsBeforeOperationSummary_All.m_strFirstPrintDate == "")
			{				
				m_objRecordsDomain.m_lngUpdateFirstPrintDate(m_objPrintInfo.m_strInPatentID,m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"),m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"),dtmFirstPrintTime.ToString("yyyy-MM-dd HH:mm:ss"));//m_objPrintInfo.m_objclsBeforeOperationSummary_All.m_strFirstPrintDate);	
			}                          
			m_mthEndPrintSub((PrintEventArgs)p_objPrintArg);
		}

		private  void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
		{
			//ȱʡ�����κζ���
		}
		// ��ӡҳ
		private void m_mthPrintPageSub(PrintPageEventArgs e)
		{
			e.HasMorePages =false;
			m_mthPrintTitleInfo(e);
			Font fntNormal = new Font("SimSun",12);

			if(m_intPages==1)
			{
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
				
                //m_intYPos += (int)enmRectangleInfo.RowStep;
                //m_mthPrintOneHorizontalLine(e,m_intYPos);				
			}
			
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.RightX,(int)enmRectangleInfo.TopY);
			
			while(m_objPrintContext.m_BlnHaveMoreLine)
			{
				m_objPrintContext.m_mthPrintNextLine(ref m_intYPos,e.Graphics,fntNormal);

				if(m_intYPos >=(int)enmRectangleInfo.BottomY - 50
					&& m_objPrintContext.m_BlnHaveMoreLine)
				{
					

					#region ��ҳ����
					e.HasMorePages = true;					
				
					e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.LeftX,m_intYPos);
					e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,m_intYPos);
					e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.RightX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.RightX,m_intYPos);
					if(m_objPrintContext.m_BlnHaveMoreLine==true)
						m_mthPrintOneHorizontalLine(e,m_intYPos);
					m_intPages++;	
					m_intYPos = (int)enmRectangleInfo.TopY;
					return;

					#endregion ��ҳ���� 
				}				
				
			}

			#region ���һҳ����
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.LeftX,m_intYPos);
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,m_intYPos );
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.RightX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.RightX,m_intYPos);
					
			e.Graphics.DrawString("����ҽʦǩ����",new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX+6,m_intYPos+10);
			if(m_objclsBeforeOperationSummary_All!=null)
				e.Graphics.DrawString(m_objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strOperateDoctorName,new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX+6+(int)(7*17.5),m_intYPos+10);
			
			e.Graphics.DrawString("����ҽʦǩ����",new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX+360,m_intYPos+10);
			if(m_objclsBeforeOperationSummary_All!=null)
				e.Graphics.DrawString(m_objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strChargeDoctorName,new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX+360+(int)(7*17.5),m_intYPos+10);
			
			#endregion ���һҳ����

			m_intYPos += (int)enmRectangleInfo.RowStep+15;
			Font fntSign = new Font("",6);
			while(m_objPrintContext.m_BlnHaveMoreSign)
			{
				m_objPrintContext.m_mthPrintNextSign((int)enmRectangleInfo.LeftX,m_intYPos,e.Graphics,fntSign);

				m_intYPos += (int)enmRectangleInfo.RowStep-10;				
			}

			//ȫ������
			m_objPrintContext.m_mthReset();
			m_intPages=1;			
			m_intYPos = (int)enmRectangleInfo.TopY;			
		}

		// ��ӡ����ʱ�Ĳ���
		private  void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
		{
		
		}		
		

		#region ��ӡ
		#region �йش�ӡ������

		private clsPublicControlPaint m_objCPaint;
		private clsPrintContext m_objPrintContext;
		/// <summary>
		/// ҽԺ���Ƶ�����
		/// </summary>
		private Font m_fotHospitalNameFont;
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
		/// ��ȡ�������
		/// </summary>
		private clsPrintPageSettingForRecord m_objPageSetting;
		/// <summary>
		/// ��ӡ�Ĳ��˻�����Ϣ��
		/// </summary>
		/// 
		private int m_intYPos = (int)enmRectangleInfo.TopY+5;
		private int m_intPages=1;		

		/// <summary>
		/// ���ӵ���Ϣ
		/// </summary>
		public enum enmRectangleInfo
		{
			/// <summary>
			/// ���ӵĶ���
			/// </summary>
			TopY = 130,
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
			RowStep = 20,
			SmallRowStep=20,			

			ColumnsMark1=110,			

			/// <summary>
			/// �׻���ƫ���ı�����ľ���
			/// </summary>
			BottomLineShift=15,

			BottomY=1024		
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
				float fltOffsetX=20;//X��ƫ����
				PointF m_fReturnPoint;
				switch(p_intItemName)
				{   
                   case (int)enmItemDefination.Page_HospitalName:
						m_fReturnPoint = new PointF(340f-fltOffsetX,30f);
						break;
					case (int)enmItemDefination.Page_Name_Title:
						m_fReturnPoint = new PointF(280f-fltOffsetX,60f);
						break;
					case (int)enmItemDefination.Name_Title :
						m_fReturnPoint = new PointF(65f-fltOffsetX,100f);
						break;
					case (int)enmItemDefination.Name:
						m_fReturnPoint = new PointF(115f-fltOffsetX,100f);
						break;

					case (int)enmItemDefination.Sex_Title :
						m_fReturnPoint = new PointF(185f,100f);
						break;
					case (int)enmItemDefination.Sex :
						m_fReturnPoint = new PointF(230f,100f);
						break;

					case (int)enmItemDefination.Age_Title :
						m_fReturnPoint = new PointF(260f,100f);
						break;
					case (int)enmItemDefination.Age:
						m_fReturnPoint = new PointF(305f,100f);
						break;

					case (int)enmItemDefination.Dept_Name_Title:
						m_fReturnPoint = new PointF(380f-fltOffsetX,100f);
						break;
					case (int)enmItemDefination.Dept_Name :
						m_fReturnPoint = new PointF(430f-fltOffsetX,100f);
						break;
					
					case (int)enmItemDefination.BedNo_Title :
						m_fReturnPoint = new PointF(575f-fltOffsetX,100f);
						break;
					case (int)enmItemDefination.BedNo:
						m_fReturnPoint = new PointF(625f-fltOffsetX,100f);
						break;

					case (int)enmItemDefination.InPatientID_Title:
						m_fReturnPoint = new PointF(660f-fltOffsetX,100f);
						break;
					case (int)enmItemDefination.InPatientID :
						m_fReturnPoint = new PointF(720f-fltOffsetX,100f);
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
		
		#region ��ӡ�ж���
		private clsPrintLine1[] m_objLine1Arr;		
		#endregion
		
		private DateTime dtmFirstPrintTime;
		/// <summary>
		/// ��ÿһ��ӡ�е�Ԫ�ظ�ֵ
		/// </summary>
		private void m_mthSetPrintValue()
		{		
			#region  ��һ�δ�ӡʱ�丳ֵ			
			dtmFirstPrintTime=DateTime.Now;
			if(m_objclsBeforeOperationSummary_All!=null && m_objclsBeforeOperationSummary_All.m_strFirstPrintDate !=null && m_objclsBeforeOperationSummary_All.m_strFirstPrintDate.Trim()!="")
				dtmFirstPrintTime=DateTime.Parse(m_objclsBeforeOperationSummary_All.m_strFirstPrintDate);
			#endregion  ��һ�δ�ӡʱ�丳ֵ

			#region ��ӡ�г�ʼ��
			m_objLine1Arr = new clsPrintLine1[10];
			for(int i=0;i<m_objLine1Arr.Length;i++)
				m_objLine1Arr[i]=new clsPrintLine1();
			
			m_objPrintContext = new clsPrintContext(
				new clsPrintLineBase[]{
										  m_objLine1Arr[0],m_objLine1Arr[1],m_objLine1Arr[2],m_objLine1Arr[3],m_objLine1Arr[4],
										  m_objLine1Arr[5],m_objLine1Arr[6],m_objLine1Arr[7],m_objLine1Arr[8],m_objLine1Arr[9]
									  });
			m_objPrintContext.m_ObjPrintSign =  new clsPrintRecordSign();
			#endregion
			
			#region ��ÿһ�е�Ԫ�ظ�ֵ
			if(m_objclsBeforeOperationSummary_All!=null && 
				m_objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo!=null &&
				m_objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo!=null)
			{
				///////////////1��/////////////////
				Object[] objData1=new object[4];
				objData1[0]=m_objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strDiagnose;
				objData1[1]=m_objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strDiagnoseXml;

				objData1[2]=dtmFirstPrintTime ;
				objData1[3]=" ��  ��" ;
				m_objLine1Arr[0].m_ObjPrintLineInfo =objData1;

				///////////////2��/////////////////
				objData1[0]=m_objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strDiagnoseGist;
				objData1[1]=m_objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strDiagnoseGistXml;
				objData1[3]=" �������" ;
				m_objLine1Arr[1].m_ObjPrintLineInfo =objData1;

				///////////////3��/////////////////
				objData1[0]=m_objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strBodyInfo;
				objData1[1]=m_objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strBodyInfoXml;
				objData1[3]="������Ӧ֢" ;
				m_objLine1Arr[2].m_ObjPrintLineInfo =objData1;
				///////////////4��/////////////////
				objData1[0]=m_objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strSpecialHandle;
				objData1[1]=m_objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strSpecialHandleXml;
				objData1[3]="����������ʽ����ע��������������Ԥ��������" ;
				m_objLine1Arr[3].m_ObjPrintLineInfo =objData1;
				///////////////5��/////////////////
				objData1[0]=m_objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strPreparation;
				objData1[1]=m_objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strPreparationXml;
				objData1[3]=" ��ǰ׼��" ;
				m_objLine1Arr[4].m_ObjPrintLineInfo =objData1;
				///////////////6��/////////////////
				objData1[0]=m_objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strPatientNotion;
				objData1[1]=m_objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strPatientNotionXml;
				objData1[3]="���߼�������λ���������" ;
				m_objLine1Arr[5].m_ObjPrintLineInfo =objData1;
				///////////////7��/////////////////
				objData1[0]=m_objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strAnaesthesia;
				objData1[1]=m_objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strAnaesthesiaXml;
				objData1[3]=" ��������" ;
				m_objLine1Arr[6].m_ObjPrintLineInfo =objData1;
				///////////////8��/////////////////			
				objData1[0]=DateTime.Parse(m_objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strOperationDate).ToString("yyyy��M��d��");
				objData1[1]="";
				objData1[3]=" ��������" ;
				m_objLine1Arr[7].m_ObjPrintLineInfo =objData1;
				///////////////9��/////////////////
				objData1[0]=m_objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strAfterNotice;
				objData1[1]=m_objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strAfterNoticeXml;
				objData1[3]=" ����ע��" ;
				m_objLine1Arr[8].m_ObjPrintLineInfo =objData1;
				///////////////10��/////////////////
				objData1[0]=m_objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryContentInfo.m_strDiscussNotion;
				objData1[1]=m_objclsBeforeOperationSummary_All.m_objclsBeforeOperationSummaryInfo.m_strDiscussNotionXml;
				objData1[3]="��ǰ�������" ;
				m_objLine1Arr[9].m_ObjPrintLineInfo =objData1;
			}
			else 
			{
				///////////////1��/////////////////
				Object[] objData1=new object[4];
				objData1[0]="";
				objData1[1]="";

				objData1[2]=dtmFirstPrintTime ;
				objData1[3]=" ��  ��" ;
				m_objLine1Arr[0].m_ObjPrintLineInfo =objData1;

				///////////////2��/////////////////
				objData1[0]="";
				objData1[1]="";
				objData1[3]=" �������" ;
				m_objLine1Arr[1].m_ObjPrintLineInfo =objData1;

				///////////////3��/////////////////
				objData1[0]="";
				objData1[1]="";
				objData1[3]="������Ӧ֢" ;
				m_objLine1Arr[2].m_ObjPrintLineInfo =objData1;
				///////////////4��/////////////////
				objData1[0]="";
				objData1[1]="";
				objData1[3]="����������ʽ����ע��������������Ԥ��������" ;
				m_objLine1Arr[3].m_ObjPrintLineInfo =objData1;
				///////////////5��/////////////////
				objData1[0]="";
				objData1[1]="";
				objData1[3]=" ��ǰ׼��" ;
				m_objLine1Arr[4].m_ObjPrintLineInfo =objData1;
				///////////////6��/////////////////
				objData1[0]="";
				objData1[1]="";
				objData1[3]="���߼�������λ���������" ;
				m_objLine1Arr[5].m_ObjPrintLineInfo =objData1;
				///////////////7��/////////////////
				objData1[0]="";
				objData1[1]="";
				objData1[3]=" ��������" ;
				m_objLine1Arr[6].m_ObjPrintLineInfo =objData1;
				///////////////8��/////////////////			
				objData1[0]="";
				objData1[1]="";
				objData1[3]=" ��������" ;
				m_objLine1Arr[7].m_ObjPrintLineInfo =objData1;
				///////////////9��/////////////////
				objData1[0]="";
				objData1[1]="";
				objData1[3]=" ����ע��" ;
				m_objLine1Arr[8].m_ObjPrintLineInfo =objData1;
				///////////////10��/////////////////
				objData1[0]="";
				objData1[1]="";
				objData1[3]="��ǰ�������" ;
				m_objLine1Arr[9].m_ObjPrintLineInfo =objData1;

			}
			
			#endregion 
		}
				
		/// <summary>
		/// ��ӡһ��ˮƽ��
		/// </summary>
		/// <param name="e"></param>
		/// <param name="p_intBottomY"></param>
		private void m_mthPrintOneHorizontalLine(System.Drawing.Printing.PrintPageEventArgs e,int p_intBottomY)
		{			
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX ,
				p_intBottomY,
				(int)enmRectangleInfo.RightX,
				p_intBottomY);			
		}

		#region �������ֲ���
		/// <summary>
		/// �������ֲ���
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{
            e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, m_fotSmallFont, m_slbBrush, m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_HospitalName));
		
			e.Graphics.DrawString("��     ǰ     С     ��",m_fotTitleFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_Name_Title ));

            
		}
		
	
		#endregion		
		
		#region print class 

		private class clsPrintLine1 : clsPrintLineBase
		{
			private com.digitalwave.controls.clsPrintRichTextContext m_objDiagnose;			
			private DateTime dtmFirstPrint;
//			private bool m_blnFirstPrint = true;
			private string m_strTitle;

			/// <summary>
			/// ���
			/// </summary>
			private const int c_intHeight1 = 30;
			/// <summary>
			/// �������
			/// </summary>
			private const int c_intHeight2 = 110;
			/// <summary>
			/// ������Ӧ֢
			/// </summary>
			private const int c_intHeight3 = 100;
			/// <summary>
			/// ����������ʽ����ע��������������Ԥ��������
			/// </summary>
			private const int c_intHeight4 = 110;
			/// <summary>
			/// ��ǰ׼��
			/// </summary>
			private const int c_intHeight5 = 100;
			/// <summary>
			/// ���߼�������λ���������
			/// </summary>
			private const int c_intHeight6 = 60;
			/// <summary>
			/// ��������
			/// </summary>
			private const int c_intHeight7 = 50;
			/// <summary>
			/// ��������
			/// </summary>
			private const int c_intHeight8 = 30;
			/// <summary>
			/// ����ע��
			/// </summary>
			private const int c_intHeight9 = 90;
			/// <summary>
			/// ��ǰ�������
			/// </summary>
			private const int c_intHeight10 = 60;

			/// <summary>
			/// һ�еĸ߶�
			/// </summary>
			private const int c_intOneRowHeight = 25;

			public clsPrintLine1()
			{
				m_objDiagnose = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,new Font("SimSun",12));
			}

//			private int m_intTimes = 0;

			//���ӳ�������ƫ����
			private static int s_intHeightMargin;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Rectangle rtgTitle = new Rectangle(
					(int)enmRectangleInfo.LeftX,
					p_intPosY+10,
					(int)enmRectangleInfo.ColumnsMark1,
					0);
				Rectangle rtgDianose = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+5,
					p_intPosY+10,
					(int)enmRectangleInfo.RightX- ((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+5)-5,
					0);

				int intPosY = p_intPosY;

				//ԭʼ�̶��ĸ߶�
				int intPegPosY = 0;

				switch(m_strTitle)
				{
					case " ��  ��":
						rtgTitle.Height = c_intHeight1-5;
						intPegPosY = 190;
//						rtgDianose.Height = c_intHeight1-5;
//						p_intPosY += c_intHeight1;
						break;
					case " �������":
						if((rtgTitle.Height =(c_intHeight2 - 5 - s_intHeightMargin)) < c_intOneRowHeight)
						{
							if (rtgTitle.Height < 0)
								s_intHeightMargin = Math.Abs(rtgTitle.Height);
							rtgTitle.Height = c_intOneRowHeight;
						}
						intPegPosY = 300;
//						rtgDianose.Height = c_intHeight1-5;//c_intHeight2-5;
//						p_intPosY += c_intHeight2;
						break;
					case "������Ӧ֢":
						if((rtgTitle.Height = c_intHeight3-5 -s_intHeightMargin) < c_intOneRowHeight)
						{
							if (rtgTitle.Height < 0)
								s_intHeightMargin = Math.Abs(rtgTitle.Height);
							rtgTitle.Height = c_intOneRowHeight;
						}
						intPegPosY = 400;
//						rtgDianose.Height = c_intHeight1-5;//c_intHeight3-5;
//						p_intPosY += c_intHeight3;
						break;
					case "����������ʽ����ע��������������Ԥ��������":
						if ((rtgTitle.Height = c_intHeight4-5 -s_intHeightMargin) < c_intHeight4-15)
						{
							if (rtgTitle.Height < 0)
								s_intHeightMargin = Math.Abs(rtgTitle.Height);
							rtgTitle.Height = c_intHeight4-15;
						}
						intPegPosY = 510;
//						rtgDianose.Height = c_intHeight9-15;//c_intHeight4-5;
//						p_intPosY += c_intHeight4;
						break;
					case " ��ǰ׼��":
						if((rtgTitle.Height = c_intHeight5-5 -s_intHeightMargin) < c_intOneRowHeight)
						{
							if (rtgTitle.Height < 0)
								s_intHeightMargin = Math.Abs(rtgTitle.Height);
							rtgTitle.Height = c_intOneRowHeight;
						}
						intPegPosY = 610;
//						rtgDianose.Height = c_intHeight1-5;//c_intHeight5-5;
//						p_intPosY += c_intHeight5;
						break;
					case "���߼�������λ���������":
						if((rtgTitle.Height = c_intHeight6-5 -s_intHeightMargin) < c_intHeight6-5)
						{
							if (rtgTitle.Height < 0)
								s_intHeightMargin = Math.Abs(rtgTitle.Height);
							rtgTitle.Height = c_intHeight6-5;
						}
						intPegPosY = 670;
//						rtgDianose.Height = c_intHeight6-15;//c_intHeight6-5;
						break;
					case " ��������":
						if ((rtgTitle.Height = c_intHeight7-5 -s_intHeightMargin) < c_intOneRowHeight)
						{
							if (rtgTitle.Height < 0)
								s_intHeightMargin = Math.Abs(rtgTitle.Height);
							rtgTitle.Height = c_intOneRowHeight;
						}
						intPegPosY = 720;
//						rtgDianose.Height = c_intHeight1-5;//c_intHeight7-5;
//						p_intPosY += c_intHeight7;
						break;
					case " ��������":
						if((rtgTitle.Height = c_intHeight8-5 -s_intHeightMargin) < c_intOneRowHeight)
						{
							if (rtgTitle.Height < 0)
								s_intHeightMargin = Math.Abs(rtgTitle.Height);
							rtgTitle.Height = c_intOneRowHeight;
						}
						intPegPosY = 750;
//						rtgDianose.Height = c_intHeight1-5;//c_intHeight8-5;
//						p_intPosY += c_intHeight8;
						break;
					case " ����ע��":
						if ((rtgTitle.Height = c_intHeight9-5 -s_intHeightMargin) < c_intOneRowHeight)
						{
							if (rtgTitle.Height < 0)
								s_intHeightMargin = Math.Abs(rtgTitle.Height);
							rtgTitle.Height = c_intOneRowHeight;
						}
						intPegPosY = 840;
//						rtgDianose.Height = c_intHeight1-5;//c_intHeight9-5;
//						p_intPosY += c_intHeight9;
						break;
					case "��ǰ�������":
						if ((rtgTitle.Height = c_intHeight10 -5 -s_intHeightMargin) < c_intOneRowHeight)
						{
							if (rtgTitle.Height < 0)
								s_intHeightMargin = Math.Abs(rtgTitle.Height);
							rtgTitle.Height = c_intOneRowHeight;
						}
						intPegPosY = 900;
//						rtgDianose.Height = 900 - p_intPosY;//c_intHeight10-5;
//						p_intPosY = 900;//c_intHeight10;
						break;
				}

				StringFormat stfTitle = new StringFormat(StringFormatFlags.FitBlackBox);				
				Font fntTitle = new Font("SimSun",12);				

				p_objGrp.DrawString(m_strTitle,fntTitle ,Brushes.Black,rtgTitle,stfTitle);

				int intRealHeight;
				m_objDiagnose.m_blnPrintAllBySimSun(10,rtgDianose,p_objGrp,out intRealHeight,false);
				
				if(intRealHeight > rtgTitle.Height)
				{
					p_intPosY += intRealHeight + 10;
				}
				else
				{
					p_intPosY += rtgTitle.Height + 5;
				}
				if (p_intPosY > intPegPosY) 
					s_intHeightMargin += p_intPosY - intPegPosY;
				else
					s_intHeightMargin = 0;


				p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX ,
					p_intPosY,
					(int)enmRectangleInfo.RightX,
					p_intPosY);

				fntTitle.Dispose();
				stfTitle.Dispose();

				m_blnHaveMoreLine = false;

				#region old
//				p_intPosY +=5;///////////////////////////////////
//				if(m_blnFirstPrint)
//				{	
//					if(m_strTitle=="����������ʽ����ע��������������Ԥ�������� ")
//					{						
//						p_objGrp.DrawString(m_strTitle.Substring(m_intTimes*6,6),new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX,p_intPosY);
//						if(m_intTimes>=3)
//							m_blnFirstPrint = false;
//					}
//					else if(m_strTitle=="���߼�������λ���������")
//					{
//						p_objGrp.DrawString(m_strTitle.Substring(m_intTimes*6,6),new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX,p_intPosY);
//						if(m_intTimes>=1)
//							m_blnFirstPrint = false;
//					}
//					else
//					{
//						p_objGrp.DrawString(m_strTitle,new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX,p_intPosY+3);
//						m_blnFirstPrint = false;
//					}
//				}
//
//				if(m_objDiagnose.m_BlnHaveNextLine())
//					m_objDiagnose.m_mthPrintLine((int)enmRectangleInfo.RightX- ((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+5)-5, (int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+5,p_intPosY,p_objGrp);
//				
//				if(m_objDiagnose.m_BlnHaveNextLine())
//				{
//					m_blnHaveMoreLine = true;
//					p_intPosY +=(int)enmRectangleInfo.SmallRowStep+5 ;
//					m_intTimes++;
//				}else 
//					if((m_strTitle=="����������ʽ����ע��������������Ԥ�������� " && m_intTimes<3)|| (m_strTitle=="���߼�������λ���������" && m_intTimes<1))
//				{
//					m_blnHaveMoreLine = true;
//					p_intPosY +=(int)enmRectangleInfo.SmallRowStep+5 ;
//					m_intTimes++;
//				}
//				else
//				{
//					m_blnHaveMoreLine = false;
//					p_intPosY += (int)enmRectangleInfo.RowStep+5;
//					
//					if(m_strTitle=="�������")
//					{
//						//dick ���Ĭ�ϵĴ�ӡ���
//						if(m_intTimes < 5 && m_intTimes!=0)
//						{
//							p_intPosY += (5-m_intTimes)*(int)enmRectangleInfo.RowStep;
//
//						}
//					
//						else if(m_intTimes == 0)
//						{
//							p_intPosY +=5*(int)enmRectangleInfo.RowStep;
//						}
//						if(p_intPosY>=(int)enmRectangleInfo.BottomY)
//						{
//							p_intPosY=(int)enmRectangleInfo.BottomY;
//						}
//						else 
//						{
//							p_objGrp.DrawLine(new Pen(Color.Black,1),(int)enmRectangleInfo.LeftX ,
//								p_intPosY,
//								(int)enmRectangleInfo.RightX,
//								p_intPosY);		
//						}
//
//					}
//					else if(m_strTitle=="������Ӧ֢")
//					{
//						if(m_intTimes < 5 && m_intTimes!=0)
//						{
//							p_intPosY += (5-m_intTimes)*(int)enmRectangleInfo.RowStep;
//
//						}
//					
//						else if(m_intTimes == 0)
//						{
//							p_intPosY +=5*(int)enmRectangleInfo.RowStep;
//						}
//						if(p_intPosY>=(int)enmRectangleInfo.BottomY)
//						{
//							p_intPosY=(int)enmRectangleInfo.BottomY;
//						}
//						else 
//						{
//							p_objGrp.DrawLine(new Pen(Color.Black,1),(int)enmRectangleInfo.LeftX ,
//								p_intPosY,
//								(int)enmRectangleInfo.RightX,
//								p_intPosY);		
//						}
//
//					}
//					else if(m_strTitle=="����������ʽ����ע��������������Ԥ��������")
//					{
//						if(m_intTimes < 3 && m_intTimes!=0)
//						{
//							p_intPosY += (3-m_intTimes)*(int)enmRectangleInfo.RowStep;
//
//						}
//					
//						else if(m_intTimes == 0)
//						{
//							p_intPosY +=3*(int)enmRectangleInfo.RowStep;
//						}
//						if(p_intPosY>=(int)enmRectangleInfo.BottomY)
//						{
//							p_intPosY=(int)enmRectangleInfo.BottomY;
//						}
//						else 
//						{
//							p_objGrp.DrawLine(new Pen(Color.Black,1),(int)enmRectangleInfo.LeftX ,
//								p_intPosY,
//								(int)enmRectangleInfo.RightX,
//								p_intPosY);		
//						}
//
//					}
//					else if(m_strTitle=="��ǰ׼��")
//					{
//						if(m_intTimes < 5 && m_intTimes!=0)
//						{
//							p_intPosY += (5-m_intTimes)*(int)enmRectangleInfo.RowStep;
//
//						}
//					
//						else if(m_intTimes == 0)
//						{
//							p_intPosY +=5*(int)enmRectangleInfo.RowStep;
//						}
//						if(p_intPosY>=(int)enmRectangleInfo.BottomY)
//						{
//							p_intPosY=(int)enmRectangleInfo.BottomY;
//						}
//						else 
//						{
//							p_objGrp.DrawLine(new Pen(Color.Black,1),(int)enmRectangleInfo.LeftX ,
//								p_intPosY,
//								(int)enmRectangleInfo.RightX,
//								p_intPosY);		
//						}
//
//					}
//										
//					else if(m_strTitle=="��������")
//					{
//						if(m_intTimes < 1 && m_intTimes!=0)
//						{
//							p_intPosY += (1-m_intTimes)*(int)enmRectangleInfo.RowStep;
//
//						}
//					
//						else if(m_intTimes == 0)
//						{
//							p_intPosY +=1*(int)enmRectangleInfo.RowStep;
//						}
//						if(p_intPosY>=(int)enmRectangleInfo.BottomY)
//						{
//							p_intPosY=(int)enmRectangleInfo.BottomY;
//						}
//						else 
//						{
//							p_objGrp.DrawLine(new Pen(Color.Black,1),(int)enmRectangleInfo.LeftX ,
//								p_intPosY,
//								(int)enmRectangleInfo.RightX,
//								p_intPosY);		
//						}
//						
//					}
//					else 
//					{
//						if(m_intTimes < 2 && m_intTimes!=0)
//						{
//							p_intPosY += (2-m_intTimes)*(int)enmRectangleInfo.RowStep;
//
//						}
//					
//						else if(m_intTimes == 0)
//						{
//							p_intPosY +=2*(int)enmRectangleInfo.RowStep;
//						}
//						if(p_intPosY>=(int)enmRectangleInfo.BottomY)
//						{
//							p_intPosY=(int)enmRectangleInfo.BottomY;
//						}
//						else 
//						{
//							p_objGrp.DrawLine(new Pen(Color.Black,1),(int)enmRectangleInfo.LeftX ,
//								p_intPosY,
//								(int)enmRectangleInfo.RightX,
//								p_intPosY);		
//						}
//						
//					}
					//					m_blnHaveMoreLine = false;
					//					p_intPosY += (int)enmRectangleInfo.RowStep;	
					//	
					//					p_objGrp.DrawLine(new Pen(Color.Black,1),(int)enmRectangleInfo.LeftX ,
					//						p_intPosY,
					//						(int)enmRectangleInfo.RightX,
					//						p_intPosY);					
//				}
				#endregion

			}

			public override void m_mthReset()
			{
//				m_intTimes = 0;
				m_blnHaveMoreLine = true;
//				m_blnFirstPrint = true;
				m_objDiagnose.m_mthRestartPrint();	
			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					if(value != null)
					{
						Object [] objData=(object[])value;
						m_strTitle=objData[3].ToString();
						dtmFirstPrint=(DateTime)objData[2];						

						if(objData[1].ToString() == "")
						{
							m_objDiagnose.m_mthSetContextWithCorrectBefore(objData[0].ToString() ,objData[1].ToString(),dtmFirstPrint);
						}
						else
						{
							m_objDiagnose.m_mthSetContextWithCorrectBefore(objData[0].ToString() ,objData[1].ToString(),dtmFirstPrint,true);
							m_mthAddSign2(m_strTitle+":",m_objDiagnose.m_ObjModifyUserArr);
						}
						if(m_objDiagnose.m_ObjModifyUserArr !=null)
							for(int i=0;i<m_objDiagnose.m_ObjModifyUserArr.Length;i++)
							{
								if(m_objDiagnose.m_ObjModifyUserArr[i].m_clrText.ToArgb()==Color.White.ToArgb())
									m_objDiagnose.m_ObjModifyUserArr[i].m_clrText=Color.Black;
							}					
					}
				}
			}
		}		
		#endregion

		#endregion

//		/// <summary>
//		/// Σ�ػ���Ĵ�ӡ��Ϣ.
//		/// </summary>
//		[Serializable]			
//		private class clsPrintInfo_BeforeOperationSummary
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
//			public clsBeforeOperationSummary_All m_objclsBeforeOperationSummary_All;			
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
		//		clsBeforeOperationSummaryPrintTool objPrintTool;
		//		private void m_mthDemoPrint_FromDataSource()
		//		{	
		//			objPrintTool=new clsBeforeOperationSummaryPrintTool();
		//			objPrintTool.m_mthInitPrintTool(null);	
		//			if(m_objBaseCurrentPatient==null)
		//				objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,DateTime.MinValue,DateTime.MinValue);
		//			else if(this.m_trvInOperationDate.SelectedNode ==null || this.m_trvInOperationDate.SelectedNode==m_trnRoot)
		//				objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,m_objBaseCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate,DateTime.MinValue);
		//			else 
		//				objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,m_objBaseCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate,m_dtpOeprationTime.Value);
		//								
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
		//			objPrintTool=new clsBeforeOperationSummaryPrintTool();
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


