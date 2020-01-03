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
	public class clsInPatientEvaluatePrintTool: infPrintRecord
	{	
		private bool m_blnIsFromDataSource=true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
		private bool m_blnWantInit=true;		
		private clsInPatientEvaluateDomain m_objRecordsDomain;
		private clsPrintInfo_InPatientEvaluate m_objPrintInfo;
		private clsInPatientEvaluate_All m_objInPatientEvaluate_All=null;

		private bool m_blnLimbActive_Not;
		/// <summary>
		/// �Ƿ��ӡ��֫����
		/// </summary>
		public bool m_BlnLimbActive_Not
		{
			set 
			{
				m_blnLimbActive_Not = value;
			}
		}
		
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
			m_objPrintInfo=new clsPrintInfo_InPatientEvaluate();
			m_objPrintInfo.m_strInPatentID=m_objPatient!=null? m_objPatient.m_StrInPatientID:"";					
			m_objPrintInfo.m_strPatientName=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrFirstName :"";
			m_objPrintInfo. m_strSex=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrSex:"";
			m_objPrintInfo. m_strAge=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrAge : "";
			m_objPrintInfo. m_strBedName=m_objPatient!=null? m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName :"";
			m_objPrintInfo. m_strDeptName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjDept.m_StrDeptName :"";
			m_objPrintInfo. m_strAreaName=m_objPatient!=null? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName:"";			
			m_objPrintInfo.m_dtmInPatientDate=p_dtmInPatientDate;
			m_objPrintInfo.m_dtmOpenDate=p_dtmOpenDate;	
	        m_objPrintInfo.m_strHISInPatientID = m_objPatient!=null? m_objPatient.m_StrHISInPatientID : "";
            m_objPrintInfo.m_dtmHISInPatientDate = m_objPatient != null ? m_objPatient.m_DtmSelectedHISInDate : DateTime.MinValue;
			
			m_objPrintInfo. m_strOccupation=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrOccupation : "";//ְҵ
			m_objPrintInfo. m_strNationnality=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrNation : "";//����
			m_objPrintInfo. m_strHometown=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrHomeplace : "";//����
			m_objPrintInfo. m_strMarrage=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrMarried : "";//����
		}

		/// <summary>
		/// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���(�������ݿ��ȡʱҪ����.)
		/// </summary>
		public void m_mthInitPrintContent()
		{		
			m_blnWantInit=false;//
			if(m_objPrintInfo==null)
			{
				clsPublicFunction.ShowInformationMessageBox("����m_mthInitPrintContent֮ǰ�����ȵ���m_mthSetPrintInfo����");
				return;
			}
			if(m_objPrintInfo.m_strInPatentID=="" || m_objPrintInfo.m_dtmInPatientDate==DateTime.MinValue)
				m_objInPatientEvaluate_All=null;				
			else
			{
				m_objRecordsDomain=new clsInPatientEvaluateDomain();	
				long lngRes=m_objRecordsDomain.m_lngGetLatestRecord_All(m_objPrintInfo.m_strInPatentID,m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"),out m_objInPatientEvaluate_All );
				if(lngRes <= 0)
					return ;   

				#region  ��һ�δ�ӡʱ�丳ֵ
				string strFirstPrintDate="";
				m_objRecordsDomain.m_strGetFirstPrintDate(m_objPrintInfo.m_strInPatentID,m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"),out strFirstPrintDate);
			
				DateTime dtmFirstPrintTime;
				if(strFirstPrintDate==null ||strFirstPrintDate.Trim()=="")
					dtmFirstPrintTime=DateTime.Now;
				else 
					dtmFirstPrintTime=DateTime.Parse(strFirstPrintDate);
				#endregion  ��һ�δ�ӡʱ�丳ֵ	
		
				m_objPrintInfo.m_dtmFirstPrintTime=dtmFirstPrintTime;

			}
			//���ñ����ݵ���ӡ��			
			m_objPrintInfo.m_objInPatientEvaluate_All=m_objInPatientEvaluate_All;
			m_mthSetPrintValue();//�����з��ӡ����,��ʹ�ڴ�ӡ�հױ�ʱ,����Ҳ����ִ��.			
		}

		/// <summary>
		/// ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
		/// </summary>
		/// <param name="p_objPrintContent">��ӡ����</param>
		public void m_mthSetPrintContent(object p_objPrintContent)
		{
			m_blnWantInit=false;
			if(p_objPrintContent.GetType().Name !="clsPrintInfo_InPatientEvaluate")
			{
				clsPublicFunction.ShowInformationMessageBox("��������");
			}
			m_blnIsFromDataSource=false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ
			m_objPrintInfo=(clsPrintInfo_InPatientEvaluate)p_objPrintContent;
			m_objInPatientEvaluate_All= m_objPrintInfo. m_objInPatientEvaluate_All ;		
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
					clsPublicFunction.ShowInformationMessageBox("�������ݿ��ȡʱ,����m_objGetPrintInfo֮ǰ�����ȵ���m_mthSetPrintInfo����");
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
			m_fotHeaderFont = new Font("SimSun", 14);
			m_fotSmallFont = new Font("SimSun",11);
			m_GridPen = new Pen(Color.Black,1);
			m_slbBrush = new SolidBrush(Color.Black);
			m_objPageSetting = new clsPrintPageSettingForRecord();
			m_objCPaint=new clsPublicControlPaint();			
		}

		/// <summary>
		/// �ͷŴ�ӡ����
		/// </summary>
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
			if(m_blnIsFromDataSource==false || m_objPrintInfo.m_strInPatentID=="" || m_objPrintInfo.m_objInPatientEvaluate_All==null) return;
			//�����ӡ�ɹ�������������Ҫ���µ�ʱ�䣬����У�����ʱ�䡣 
			if(!((PrintEventArgs)p_objPrintArg).Cancel && m_objPrintInfo.m_dtmFirstPrintTime != DateTime.MinValue)
			{				
				long lngRes=m_objRecordsDomain.m_lngUpdateFirstPrintDate(m_objPrintInfo.m_strInPatentID,m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));	
				if(lngRes<=0)
				{
					switch(lngRes)
					{
						case (long)enmOperationResult.Not_permission:
							clsPublicFunction.s_mthShowNotPermitMessage();
							break;
						case (long)enmOperationResult.DB_Fail://��������״δ�ӡ�򷵻�ֵΪ0����˴˴����ܼӴ��ж�							
							break;
						default : 
							clsPublicFunction.ShowInformationMessageBox("���´�ӡʱ��ʧ��");
							break;

					}	
					return;
				}
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
			
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.RightX,(int)enmRectangleInfo.TopY);
			if(m_intPages!=1)
				m_intYPos=m_intYPos+5;

			while(m_objPrintContext.m_BlnHaveMoreLine)
			{
				m_objPrintContext.m_mthPrintNextLine(ref m_intYPos,e.Graphics,fntNormal);

				#region no use
//				if(m_intYPos >(int)enmRectangleInfo.BottomY 
//					&& m_objPrintContext.m_BlnHaveMoreLine)
//				{
//					#region ��ҳ����
//					e.HasMorePages = true;
//					switch(m_intEndIndex)
//					{
//											
//						case 0:
//							m_mthHandleOneEnd(m_intYPos,e.Graphics,fntNormal);
//							m_intPreY=(int)enmRectangleInfo.TopY;
//							m_intEndIndex--;
//							break;
//						case 1:
//							m_mthHandleTwoEnd(m_intYPos,e.Graphics,fntNormal);
//							m_intPreY=(int)enmRectangleInfo.TopY;
//							m_intEndIndex--;
//							break;
//						case 2:
//							m_mthHandleThreeEnd(m_intYPos,e.Graphics,fntNormal);
//							m_intPreY=(int)enmRectangleInfo.TopY;
//							m_intEndIndex--;
//							break;
//						case 3:
//							m_mthHandleFourEnd(m_intYPos,e.Graphics,fntNormal);
//							m_intPreY=(int)enmRectangleInfo.TopY;
//							m_intEndIndex--;
//							break;
//						case 4:
//							m_mthHandleFiveEnd(m_intYPos,e.Graphics,fntNormal);
//							m_intPreY=(int)enmRectangleInfo.TopY;
//							m_intEndIndex--;
//							break;
//						case 5:
//							m_mthHandleSixEnd(m_intYPos,e.Graphics,fntNormal);
//							m_intPreY=(int)enmRectangleInfo.TopY;
//							m_intEndIndex--;
//							break;
//						case 6:
//							m_mthHandleSevenEnd(m_intYPos,e.Graphics,fntNormal);
//							m_intPreY=(int)enmRectangleInfo.TopY;
//							m_intEndIndex--;
//							break;
//						case 7:
//							m_mthHandleEightEnd(m_intYPos,e.Graphics,fntNormal);
//							m_intPreY=(int)enmRectangleInfo.TopY;
//							m_intEndIndex--;
//							break;
//					}
//				
//					e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.LeftX,m_intYPos-5);
//					e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,m_intYPos-5 );
//					e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.RightX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.RightX,m_intYPos-5);
//									
//					m_intPages++;
//
//					m_intYPos = (int)enmRectangleInfo.TopY+5;
//					return;
//
//					#endregion ��ҳ���� 
//				}
				#endregion				
			}

			#region ���һҳ����
			string strNurse = "";
			string strChargeNurse = "";
			string strRecordTime = "    ��  ��  ��  ʱ  ��";
			string strCheckTime = "    ��  ��  ��";

			if(m_objInPatientEvaluate_All != null && m_objInPatientEvaluate_All.m_objclsInPatientEvaluateContent != null) 
			{
				strNurse = new clsEmployee(m_objInPatientEvaluate_All.m_objclsInPatientEvaluateContent.m_strModifyUserID).m_StrFirstName.Trim();
				if(m_objInPatientEvaluate_All.m_objclsInPatientEvaluateContent.m_strChargeNurseID != null && m_objInPatientEvaluate_All.m_objclsInPatientEvaluateContent.m_strChargeNurseID.Trim() != "")
					strChargeNurse = new clsEmployee(m_objInPatientEvaluate_All.m_objclsInPatientEvaluateContent.m_strChargeNurseID).m_StrFirstName.Trim();
			}			
			
			if(strChargeNurse != "" && (DateTime.Parse(m_objInPatientEvaluate_All.m_objclsInPatientEvaluateContent.m_strCheckTime)).ToString("yyyy-M-d") != "1900-1-1")
				strCheckTime = DateTime.Parse(m_objInPatientEvaluate_All.m_objclsInPatientEvaluateContent.m_strCheckTime).ToString("yyyy��M��d��");

			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX,m_intYPos-5 ,(int)enmRectangleInfo.RightX,m_intYPos-5);
			
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.LeftX,m_intYPos-5);
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,m_intYPos-5 );
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.RightX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.RightX,m_intYPos-5);
		
			m_intYPos = m_intYPos+5;
			e.Graphics.DrawString("��ʿ��"+strNurse,fntNormal ,Brushes.Black,(int)enmRectangleInfo.LeftX+6,m_intYPos);
						
			if(m_objInPatientEvaluate_All !=null && m_objInPatientEvaluate_All.m_objclsInPatientEvaluate !=null && m_objInPatientEvaluate_All.m_objclsInPatientEvaluateContent !=null)
			{
				strRecordTime = DateTime.Parse(m_objInPatientEvaluate_All.m_objclsInPatientEvaluateContent.m_strOpenDate).ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmInPatientEvaluate"));
			}
			e.Graphics.DrawString("��¼ʱ�䣺"+ strRecordTime, fntNormal ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+70,m_intYPos);
			e.Graphics.DrawString("���ܻ�ʿ/������"+strChargeNurse,fntNormal ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+330,m_intYPos);
			e.Graphics.DrawString("����ʱ�䣺"+strCheckTime,fntNormal ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+520,m_intYPos);

			
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
			m_intEndIndex=0;
			m_intYPos = (int)enmRectangleInfo.TopY+5;
			m_intPreY=(int)enmRectangleInfo.TopY;		
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
		private int m_intPreY = (int)enmRectangleInfo.TopY;
		private int m_intEndIndex = 0;
		private int m_intPages=1;		

		/// <summary>
		/// ���ӵ���Ϣ
		/// </summary>
		public enum enmRectangleInfo
		{
			/// <summary>
			/// ���ӵĶ���
			/// </summary>
			TopY = 100,
			///<summary>
			/// ���ӵ����
			/// </summary>
			LeftX = 10,
			/// <summary>
			/// ���ӵ��Ҷ�
			/// </summary>
			RightX = 827-55,
			/// <summary>
			/// ����ÿ�еĲ���
			/// </summary>
			RowStep = 20,
			SmallRowStep=20,
			/// <summary>
			/// ���ӵ�����
			/// </summary>
			RowLinesNum = 32,

			ColumnsMark1=50,

			/// <summary>
			/// CheckBoxƫ���ұ��ı��ľ���
			/// </summary>
			CheckShift=15,

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
				PointF m_fReturnPoint;
				switch(p_intItemName)
				{   
                    
					case (int)enmItemDefination.Page_HospitalName:
						m_fReturnPoint = new PointF(320f,15f);
						break;
					case (int)enmItemDefination.Page_Name_Title:
						m_fReturnPoint = new PointF(260f,40f);
						break;
					case (int)enmItemDefination.Name_Title :
						m_fReturnPoint = new PointF(20f,75f);
						break;
					case (int)enmItemDefination.Name:
						m_fReturnPoint = new PointF(70f,75f);
						break;

					case (int)enmItemDefination.Sex_Title :
						m_fReturnPoint = new PointF(150f,75f);
						break;
					case (int)enmItemDefination.Sex :
						m_fReturnPoint = new PointF(200f,75f);
						break;

					case (int)enmItemDefination.Age_Title :
						m_fReturnPoint = new PointF(250f,75f);
						break;
					case (int)enmItemDefination.Age:
						m_fReturnPoint = new PointF(300f,75f);
						break;

					case (int)enmItemDefination.Dept_Name_Title:
						m_fReturnPoint = new PointF(350f,75f);
						break;
					case (int)enmItemDefination.Dept_Name :
						m_fReturnPoint = new PointF(400f,75f);
						break;
					
					case (int)enmItemDefination.BedNo_Title :
						m_fReturnPoint = new PointF(550f,75f);
						break;
					case (int)enmItemDefination.BedNo:
						m_fReturnPoint = new PointF(600f,75f);
						break;

					case (int)enmItemDefination.InPatientID_Title:
						m_fReturnPoint = new PointF(650f,75f);
						break;
					case (int)enmItemDefination.InPatientID :
						m_fReturnPoint = new PointF(710f,75f);
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
		private clsPrintLine1 m_objLine1;
		private clsPrintLine2 m_objLine2;
		private clsPrintLine3 m_objLine3;
		private clsPrintLine4 m_objLine4;
		private clsPrintLine5 m_objLine5;
		private clsPrintLine6 m_objLine6;
		private clsPrintLine7 m_objLine7;
		private clsPrintLine8 m_objLine8;
		private clsPrintLine9 m_objLine9;
		private clsPrintLine10 m_objLine10;
		private clsPrintLine11 m_objLine11;
		private clsPrintLine12 m_objLine12;
		private clsPrintLine13 m_objLine13;
		private clsPrintLine14 m_objLine14;
		private clsPrintLine15 m_objLine15;
		private clsPrintLine16 m_objLine16;
		private clsPrintLine17 m_objLine17;
		private clsPrintLine18 m_objLine18;
		private clsPrintLine19 m_objLine19;
		private clsPrintLine20 m_objLine20;
		private clsPrintLine21 m_objLine21;
		private clsPrintLine22 m_objLine22;
		private clsPrintLine23 m_objLine23;
		private clsPrintLine24 m_objLine24;
		private clsPrintLine25 m_objLine25;
		private clsPrintLine26 m_objLine26;
		private clsPrintLine27 m_objLine27;
		private clsPrintLine28 m_objLine28;
		private clsPrintLine29 m_objLine29;
		private clsPrintLine30 m_objLine30;
		private clsPrintLine31 m_objLine31;
		#endregion 

		#region ÿһ�εĴ���
		private void m_mthHandleOneEnd(int p_intEndY,Graphics p_objGrp,Font p_fntNormalText)
		{
			p_intEndY=p_intEndY-5;
			int intX1=(int)enmRectangleInfo.LeftX;
			int intX2=(int)enmRectangleInfo.RightX ;
			p_objGrp.DrawLine(Pens.Black,intX1,p_intEndY,intX2,p_intEndY);
			int intIndex=(p_intEndY-m_intPreY)/5;
			if((p_intEndY-m_intPreY)>80)
			{
				p_objGrp.DrawString("��",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex);
				p_objGrp.DrawString("��",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex*2);
				p_objGrp.DrawString("��",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex*3);
				p_objGrp.DrawString("��",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex*4);
			}
		
			m_intPreY = p_intEndY;
			m_intEndIndex++;
		}
		private void m_mthHandleTwoEnd(int p_intEndY,Graphics p_objGrp,Font p_fntNormalText)
		{
			p_intEndY=p_intEndY -5;
			int intX1=(int)enmRectangleInfo.LeftX;
			int intX2=(int)enmRectangleInfo.RightX ;
			p_objGrp.DrawLine(Pens.Black,intX1,p_intEndY,intX2,p_intEndY);
			
			int intIndex=(p_intEndY-m_intPreY)/6;
			if((p_intEndY-m_intPreY)>80) 
			{
				p_objGrp.DrawString("Ӫ",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex);
				p_objGrp.DrawString("��",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex*2);
				p_objGrp.DrawString("��",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex*3);
				p_objGrp.DrawString("л",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex*4);
			}

			m_intPreY = p_intEndY;

			m_intEndIndex++;
		}
		private void m_mthHandleThreeEnd(int p_intEndY,Graphics p_objGrp,Font p_fntNormalText)
		{
			p_intEndY=p_intEndY-5;
			int intX1=(int)enmRectangleInfo.LeftX;
			int intX2=(int)enmRectangleInfo.RightX ;
			p_objGrp.DrawLine(Pens.Black,intX1,p_intEndY,intX2,p_intEndY);
			
			int intIndex=(p_intEndY-m_intPreY)/2;
			if((p_intEndY-m_intPreY)>45)
			{
				p_objGrp.DrawString("��",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+5);
				p_objGrp.DrawString("к",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex*1);
			}
			else if(p_intEndY<(int)enmRectangleInfo.BottomY )
			{
				p_objGrp.DrawString("��й",new Font("SimSun",12),Brushes.Black,intX1+3,m_intPreY+10);
			}
			m_intPreY = p_intEndY;
			m_intEndIndex++;
		}
		private void m_mthHandleFourEnd(int p_intEndY,Graphics p_objGrp,Font p_fntNormalText)
		{
			p_intEndY=p_intEndY-5;
			int intX1=(int)enmRectangleInfo.LeftX;
			int intX2=(int)enmRectangleInfo.RightX ;
			p_objGrp.DrawLine(Pens.Black,intX1,p_intEndY,intX2,p_intEndY);
			
			int intIndex=(p_intEndY-m_intPreY)/2;
			if((p_intEndY-m_intPreY)>45)
			{
				p_objGrp.DrawString("��",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+5);
				p_objGrp.DrawString("��",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex*1);
			}
			else if(p_intEndY<(int)enmRectangleInfo.BottomY )
			{
				p_objGrp.DrawString("�",new Font("SimSun",12),Brushes.Black,intX1+3,m_intPreY+10);
			}
			m_intPreY = p_intEndY;
			m_intEndIndex++;
		}
		private void m_mthHandleFiveEnd(int p_intEndY,Graphics p_objGrp,Font p_fntNormalText)
		{
			p_intEndY=p_intEndY-5;
			int intX1=(int)enmRectangleInfo.LeftX;
			int intX2=(int)enmRectangleInfo.RightX ;
			p_objGrp.DrawLine(Pens.Black,intX1,p_intEndY,intX2,p_intEndY);
			
			int intIndex=(p_intEndY-m_intPreY)/2;
			if((p_intEndY-m_intPreY)>45)
			{
				p_objGrp.DrawString("˯",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+5);
				p_objGrp.DrawString("��",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex*1);
			}
			else if(p_intEndY<(int)enmRectangleInfo.BottomY )
			{
				p_objGrp.DrawString("˯��",new Font("SimSun",12),Brushes.Black,intX1+3,m_intPreY+10);
			}
			m_intPreY = p_intEndY;
			m_intEndIndex++;
		}
		private void m_mthHandleSixEnd(int p_intEndY,Graphics p_objGrp,Font p_fntNormalText)
		{
			p_intEndY=p_intEndY-5;
			int intX1=(int)enmRectangleInfo.LeftX;
			int intX2=(int)enmRectangleInfo.RightX ;
			p_objGrp.DrawLine(Pens.Black,intX1,p_intEndY,intX2,p_intEndY);
			
			int intIndex=(p_intEndY-m_intPreY)/8;
			if((p_intEndY-m_intPreY)>80)
			{
				p_objGrp.DrawString("��",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex);
				p_objGrp.DrawString("֪",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex*3);
				p_objGrp.DrawString("��",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex*5);
				p_objGrp.DrawString("��",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex*7);
			}

			m_intPreY = p_intEndY;

			m_intEndIndex++;
		}
		private void m_mthHandleSevenEnd(int p_intEndY,Graphics p_objGrp,Font p_fntNormalText)
		{
			p_intEndY=p_intEndY-5;
			int intX1=(int)enmRectangleInfo.LeftX;
			int intX2=(int)enmRectangleInfo.RightX ;
			p_objGrp.DrawLine(Pens.Black,intX1,p_intEndY,intX2,p_intEndY);
			
			int intIndex=(p_intEndY-m_intPreY)/3;
			if((p_intEndY-m_intPreY)>45)
			{
				p_objGrp.DrawString("��ɫ",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+10);
				p_objGrp.DrawString("��ϵ",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex*2);
			}
			m_intPreY = p_intEndY;
			m_intEndIndex++;
		}
		private void m_mthHandleEightEnd(int p_intEndY,Graphics p_objGrp,Font p_fntNormalText)
		{
			p_intEndY=p_intEndY-5;
			int intX1=(int)enmRectangleInfo.LeftX;
			int intX2=(int)enmRectangleInfo.RightX ;
			p_objGrp.DrawLine(Pens.Black,intX1,p_intEndY,intX2,p_intEndY);
			
			int intIndex=(p_intEndY-m_intPreY)/3;
			if((p_intEndY-m_intPreY)>45)
			{
				p_objGrp.DrawString("����",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+5);
				p_objGrp.DrawString("����",new Font("SimSun",12),Brushes.Black,intX1+5,m_intPreY+intIndex*2);
			}
			m_intPreY = p_intEndY;
			m_intEndIndex++;
		}

		#endregion 
	

		private clsInPatientEvaluate m_objChangePrintTextColor(clsInPatientEvaluate p_objclsInPatientEvaluate)
		{
			clsInPatientEvaluate objclsInPatientEvaluate=new clsInPatientEvaluate();
			//�Ѱ�ɫ��Ϊ��ɫ
			clsXML_DataGrid objclsXML_DataGrid=new clsXML_DataGrid();
			objclsInPatientEvaluate.m_strAche_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strAche_TableXML);
			objclsInPatientEvaluate.m_strAchePartXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strAchePartXML);
			objclsInPatientEvaluate.m_strAllergicHistory_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strAllergicHistory_TableXML);
			objclsInPatientEvaluate.m_strAllergicSourceXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strAllergicSourceXML);
			objclsInPatientEvaluate.m_strAllergicSymptomXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strAllergicSymptomXML);
			objclsInPatientEvaluate.m_strAlwayslanguageXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strAlwayslanguageXML);
			objclsInPatientEvaluate.m_strAnswer_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strAnswer_TableXML);
			objclsInPatientEvaluate.m_strAppetite_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strAppetite_TableXML);
			objclsInPatientEvaluate.m_strAssistantSleep_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strAssistantSleep_TableXML);
			objclsInPatientEvaluate.m_strAssistantSleepMedicinesXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strAssistantSleepMedicinesXML);
			objclsInPatientEvaluate.m_strAssistantSleepModelXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strAssistantSleepModelXML);
			objclsInPatientEvaluate.m_strBodyXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strBodyXML);
			objclsInPatientEvaluate.m_strBothEyeOtherContentXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strBothEyeOtherContentXML);
			objclsInPatientEvaluate.m_strBothListenOtherContentXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strBothListenOtherContentXML);
			objclsInPatientEvaluate.m_strBreathUrge_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strBreathUrge_TableXML);
			objclsInPatientEvaluate.m_strCanMyself_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strCanMyself_TableXML);
			objclsInPatientEvaluate.m_strChaw_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strChaw_TableXML);
			objclsInPatientEvaluate.m_strChogh_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strChogh_TableXML);
			objclsInPatientEvaluate.m_strConfirmReasonXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strConfirmReasonXML);
			objclsInPatientEvaluate.m_strDeglutition_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strDeglutition_TableXML);
			objclsInPatientEvaluate.m_strDejecta_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strDejecta_TableXML);
			objclsInPatientEvaluate.m_strDejectaCharacterXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strDejectaCharacterXML);
			objclsInPatientEvaluate.m_strDejectaHowManyDaysOnceXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strDejectaHowManyDaysOnceXML);
			objclsInPatientEvaluate.m_strDejectaTimesInOneDayXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strDejectaTimesInOneDayXML);
			objclsInPatientEvaluate.m_strDoctorsAdvice_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strDoctorsAdvice_TableXML);
			objclsInPatientEvaluate.m_strDoctorsAdviceContentXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strDoctorsAdviceContentXML);
			objclsInPatientEvaluate.m_strDrink_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strDrink_TableXML);
			objclsInPatientEvaluate.m_strDrinkQtyOneDayXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strDrinkQtyOneDayXML);
			objclsInPatientEvaluate.m_strDrinkYearsXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strDrinkYearsXML);
			objclsInPatientEvaluate.m_strEconomic_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strEconomic_TableXML);
			objclsInPatientEvaluate.m_strFreakOut_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strFreakOut_TableXML);
			objclsInPatientEvaluate.m_strFreakOutMedicinesXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strFreakOutMedicinesXML);
			objclsInPatientEvaluate.m_strFreakOutQtyOneDayXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strFreakOutQtyOneDayXML);
			objclsInPatientEvaluate.m_strFreakOutYearsXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strFreakOutYearsXML);
			objclsInPatientEvaluate.m_strHowMuchWeightXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strHowMuchWeightXML);
			objclsInPatientEvaluate.m_strIllnessUnderstand_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strIllnessUnderstand_TableXML);
			objclsInPatientEvaluate.m_strInHospitalFeel_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strInHospitalFeel_TableXML);
			objclsInPatientEvaluate.m_strInPatientDiagnoseXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strInPatientDiagnoseXML);
			objclsInPatientEvaluate.m_strInPatientMode_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strInPatientMode_TableXML);
			objclsInPatientEvaluate.m_strLanguage_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strLanguage_TableXML);
			objclsInPatientEvaluate.m_strLeftEyeOtherContentXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strLeftEyeOtherContentXML);
			objclsInPatientEvaluate.m_strLeftListenOtherContentXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strLeftListenOtherContentXML);
			objclsInPatientEvaluate.m_strLimbActive_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strLimbActive_TableXML);
			objclsInPatientEvaluate.m_strListenBalk_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strListenBalk_TableXML);
			objclsInPatientEvaluate.m_strLookIn_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strLookIn_TableXML);
			objclsInPatientEvaluate.m_strMouth_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strMouth_TableXML);
			objclsInPatientEvaluate.m_strParalysisPartXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strParalysisPartXML);
			objclsInPatientEvaluate.m_strPee_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strPee_TableXML);
			objclsInPatientEvaluate.m_strPeeOtherContentXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strPeeOtherContentXML);
			objclsInPatientEvaluate.m_strPhlegm_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strPhlegm_TableXML);
			objclsInPatientEvaluate.m_strPhlegmColorXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strPhlegmColorXML);
			objclsInPatientEvaluate.m_strRightEyeOtherContentXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strRightEyeOtherContentXML);
			objclsInPatientEvaluate.m_strRightListenOtherContentXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strRightListenOtherContentXML);
			objclsInPatientEvaluate.m_strSeeingBalk_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strSeeingBalk_TableXML);
			objclsInPatientEvaluate.m_strShout_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strShout_TableXML);
			objclsInPatientEvaluate.m_strSkin_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strSkin_TableXML);
			objclsInPatientEvaluate.m_strSkinOtherContentXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strSkinOtherContentXML);
			objclsInPatientEvaluate.m_strSleep_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strSleep_TableXML);
			objclsInPatientEvaluate.m_strSleepHoursXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strSleepHoursXML);
			objclsInPatientEvaluate.m_strSleepOtherContentXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strSleepOtherContentXML);
			objclsInPatientEvaluate.m_strSmoking_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strSmoking_TableXML);
			objclsInPatientEvaluate.m_strSmokingQtyOneDayXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strSmokingQtyOneDayXML);
			objclsInPatientEvaluate.m_strSmokingYearsXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strSmokingYearsXML);
			objclsInPatientEvaluate.m_strStomach_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strStomach_TableXML);
			objclsInPatientEvaluate.m_strStomachCharacterXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strStomachCharacterXML);
			objclsInPatientEvaluate.m_strStomachColorXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strStomachColorXML);
			objclsInPatientEvaluate.m_strStomachQtyXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strStomachQtyXML);
			objclsInPatientEvaluate.m_strStomachTimesXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strStomachTimesXML);
			objclsInPatientEvaluate.m_strWeight_TableXML=objclsXML_DataGrid.m_strReplaceWhiteToBlack( p_objclsInPatientEvaluate.m_strWeight_TableXML);
			
			//������д
			objclsInPatientEvaluate. m_strInPatientID=p_objclsInPatientEvaluate.m_strInPatientID;
			objclsInPatientEvaluate. m_strInPatientDate=p_objclsInPatientEvaluate. m_strInPatientDate;
			objclsInPatientEvaluate. m_strOpenDate=p_objclsInPatientEvaluate. m_strOpenDate;
			objclsInPatientEvaluate. m_strCreateDate=p_objclsInPatientEvaluate. m_strCreateDate;
			objclsInPatientEvaluate. m_strCreateUserID=p_objclsInPatientEvaluate. m_strCreateUserID;			
			objclsInPatientEvaluate.m_strEducation=p_objclsInPatientEvaluate.m_strEducation;			
			objclsInPatientEvaluate. m_strReligion=p_objclsInPatientEvaluate. m_strReligion;
			objclsInPatientEvaluate. m_strReligionContent=p_objclsInPatientEvaluate. m_strReligionContent;			
			objclsInPatientEvaluate. m_strDataFrom=p_objclsInPatientEvaluate. m_strDataFrom;
			objclsInPatientEvaluate. m_strDataFromOtherContent=p_objclsInPatientEvaluate. m_strDataFromOtherContent;
			
			objclsInPatientEvaluate. m_strIfConfirm=p_objclsInPatientEvaluate. m_strIfConfirm;
			objclsInPatientEvaluate. m_strConfirmReason=p_objclsInPatientEvaluate. m_strConfirmReason;
			objclsInPatientEvaluate. m_strConfirmReasonXML=p_objclsInPatientEvaluate. m_strConfirmReasonXML;
					
			return 	objclsInPatientEvaluate;
		}

		private object [] m_objInitObject(int p_intLenth,DateTime p_dtmFirstPrintTime)
		{
			object[] objData=new object[p_intLenth];	
			for(int k=0;k<objData.Length-1;k++)			
				objData[k]="";
			objData[objData.Length-1]=p_dtmFirstPrintTime;
			return objData;
		}

		private void m_mthSetPrintValue()
		{
			#region ��ӡ�г�ʼ��
			m_objLine1 = new clsPrintLine1();
			m_objLine2 = new clsPrintLine2();
			m_objLine3 = new clsPrintLine3();
			m_objLine4 = new clsPrintLine4();
			m_objLine5 = new clsPrintLine5();
			m_objLine6 = new clsPrintLine6();
			m_objLine7 = new clsPrintLine7();
			m_objLine8 = new clsPrintLine8();
			m_objLine9 = new clsPrintLine9();
			m_objLine10 = new clsPrintLine10();
			m_objLine11 = new clsPrintLine11();
			m_objLine12 = new clsPrintLine12();
			m_objLine13 = new clsPrintLine13();
			m_objLine14 = new clsPrintLine14();
			m_objLine15 = new clsPrintLine15();
			m_objLine16 = new clsPrintLine16();
			m_objLine17 = new clsPrintLine17();
			m_objLine18 = new clsPrintLine18();
			m_objLine19 = new clsPrintLine19();
			m_objLine20 = new clsPrintLine20();
			m_objLine21 = new clsPrintLine21();
			m_objLine22 = new clsPrintLine22();
			m_objLine23 = new clsPrintLine23();
			m_objLine24 = new clsPrintLine24();
			m_objLine25 = new clsPrintLine25();
			m_objLine26 = new clsPrintLine26();
			m_objLine27 = new clsPrintLine27();
			m_objLine28 = new clsPrintLine28();
			m_objLine29 = new clsPrintLine29();
			m_objLine30 = new clsPrintLine30();
			m_objLine31 = new clsPrintLine31();
			m_objLine5.m_ObjHandlePartEnd = new d_mthPrintPartEnd(m_mthHandleOneEnd);
			m_objLine11.m_ObjHandlePartEnd = new d_mthPrintPartEnd(m_mthHandleTwoEnd);
			m_objLine13.m_ObjHandlePartEnd = new d_mthPrintPartEnd(m_mthHandleThreeEnd);
			m_objLine15.m_ObjHandlePartEnd = new d_mthPrintPartEnd(m_mthHandleFourEnd);
			m_objLine17.m_ObjHandlePartEnd = new d_mthPrintPartEnd(m_mthHandleFiveEnd);
			m_objLine25.m_ObjHandlePartEnd = new d_mthPrintPartEnd(m_mthHandleSixEnd);
			m_objLine28.m_ObjHandlePartEnd = new d_mthPrintPartEnd(m_mthHandleSevenEnd);
			m_objLine31.m_ObjHandlePartEnd = new d_mthPrintPartEnd(m_mthHandleEightEnd);

			m_objPrintContext = new clsPrintContext(
				new clsPrintLineBase[]{
										  m_objLine1,
										  m_objLine2,
										  m_objLine3,
										  m_objLine4,
										  m_objLine5,
										  m_objLine6,
										  m_objLine7,
										  m_objLine8,
										  m_objLine9,
										  m_objLine10,
										  m_objLine11,
										  m_objLine12,
										  m_objLine13,
										  m_objLine14,
										  m_objLine15,
										  m_objLine16,
										  m_objLine17,
										  m_objLine18,
										  m_objLine19,
										  m_objLine20,
										  m_objLine21,
										  m_objLine22,
										  m_objLine23,
										  m_objLine24,
										  m_objLine25,
										  m_objLine26,
										  m_objLine27,
										  m_objLine28,
										  m_objLine29,
										  m_objLine30,
										  m_objLine31
										 
									  });
			m_objPrintContext.m_ObjPrintSign =  new clsPrintRecordSign();
			#endregion 

			if(m_objPrintInfo.m_strInPatentID !="" && m_objInPatientEvaluate_All !=null && m_objInPatientEvaluate_All.m_objclsInPatientEvaluate !=null && m_objInPatientEvaluate_All.m_objclsInPatientEvaluateContent !=null)
			{			
				clsInPatientEvaluate objclsInPatientEvaluate=m_objChangePrintTextColor(m_objInPatientEvaluate_All.m_objclsInPatientEvaluate);
				clsInPatientEvaluateContent objclsInPatientEvaluateContent=m_objInPatientEvaluate_All.m_objclsInPatientEvaluateContent;
						
				DateTime dtmFirstPrintTime=m_objPrintInfo.m_dtmFirstPrintTime;

				Object[] objData1=new object[2];
				objData1[0]=m_objPrintInfo;//��Ϊ���ݴ�ӡ��Ϣ�����滻�������͵Ķ���.
				objData1[1]=dtmFirstPrintTime ;
				m_objLine1.m_ObjPrintLineInfo =objData1;
			
				#region ��ÿһ�е�Ԫ�ظ�ֵ

				///////////////2��/////////////////
				Object[] objData2=new object[3];
				if(m_objPrintInfo.m_strMarrage.IndexOf("��")>=0)
					objData2[0]="3";
				else if(m_objPrintInfo.m_strMarrage.IndexOf("��")>=0)
					objData2[0]="2";
				else if(m_objPrintInfo.m_strMarrage.IndexOf("��")>=0)
					objData2[0]="1";
				else //if(m_objPrintInfo.m_strMarrage.IndexOf("δ")>=0)
					objData2[0]="0";
			
				objData2[1]=objclsInPatientEvaluate.m_strEducation;
				try
				{
					objData2[2]=dtmFirstPrintTime ;	
					m_objLine2.m_ObjPrintLineInfo=objData2;
				}
				catch(Exception ex)
				{
					clsPublicFunction.ShowInformationMessageBox(ex.Message);
				}
				///////////////3��/////////////////
				Object [] objData3=new object[5];
			
				objData3[0]=objclsInPatientEvaluate.m_strReligion;			
				objData3[1]=objclsInPatientEvaluate.m_strReligionContent ;			
				objData3[2]=objclsInPatientEvaluate.m_strDataFrom;			
				objData3[3]=objclsInPatientEvaluate.m_strDataFromOtherContent ;	
			
				objData3[4]=dtmFirstPrintTime ;	
			
				m_objLine3.m_ObjPrintLineInfo=objData3;
			
			
				///////////////4��/////////////////
				Object[] objData4=new object[4];
				objData4[0]=objclsInPatientEvaluateContent.m_strInPatientDiagnose ;
				objData4[1]=objclsInPatientEvaluate.m_strInPatientDiagnoseXML;
				objData4[2]=objclsInPatientEvaluateContent.m_strInPatientMode ;
				objData4[3]=dtmFirstPrintTime ;	
				m_objLine4.m_ObjPrintLineInfo=objData4;

				///////////////5��/////////////////
				Object[] objData5=new object[6];
				objData5[0]=objclsInPatientEvaluateContent.m_strAllergicHistory;
				objData5[1]=objclsInPatientEvaluateContent.m_strAllergicSource;
				objData5[2]=objclsInPatientEvaluate.m_strAllergicSourceXML ;
				objData5[3]=objclsInPatientEvaluateContent.m_strAllergicSymptom ;
				objData5[4]=objclsInPatientEvaluate.m_strAllergicSymptomXML ;
				objData5[5]=dtmFirstPrintTime ;	
				m_objLine5.m_ObjPrintLineInfo=objData5;
				///////////////6��/////////////////
				Object[] objData6=new object[5];
				objData6[0]=objclsInPatientEvaluateContent.m_strAppetite;
				objData6[1]=objclsInPatientEvaluateContent.m_strWeight;
				objData6[2]=objclsInPatientEvaluateContent.m_strHowMuchWeight;
				objData6[3]=objclsInPatientEvaluate.m_strHowMuchWeightXML;
				objData6[4]=dtmFirstPrintTime ;	
				m_objLine6.m_ObjPrintLineInfo=objData6;
				///////////////7��/////////////////
				Object[] objData7=new object[4];
				objData7[0]=objclsInPatientEvaluateContent.m_strMouth;
				objData7[1]=objclsInPatientEvaluateContent.m_strChaw;
				objData7[2]=objclsInPatientEvaluateContent.m_strDeglutition;
				objData7[3]=dtmFirstPrintTime ;	
				m_objLine7.m_ObjPrintLineInfo=objData7;
				///////////////8��/////////////////
				Object[] objData8=new object[14];
				objData8[0]=objclsInPatientEvaluateContent.m_strStomachNothing;
				objData8[1]=objclsInPatientEvaluateContent.m_strStomachRise;
				objData8[2]=objclsInPatientEvaluateContent.m_strStomachAche ;
				objData8[3]=objclsInPatientEvaluateContent.m_strStomachNaupathia;
				objData8[4]=objclsInPatientEvaluateContent.m_strStomachSpew ;
				objData8[5]=objclsInPatientEvaluateContent.m_strStomachColor;
				objData8[6]=objclsInPatientEvaluate.m_strStomachColorXML ;
				objData8[7]=objclsInPatientEvaluateContent.m_strStomachCharacter;
				objData8[8]=objclsInPatientEvaluate.m_strStomachCharacterXML;
				objData8[9]=objclsInPatientEvaluateContent.m_strStomachTimes  ;
				objData8[10]=objclsInPatientEvaluate.m_strStomachTimesXML;
				objData8[11]=objclsInPatientEvaluateContent.m_strStomachQty;
				objData8[12]=objclsInPatientEvaluate.m_strStomachQtyXML ;
				objData8[13]=dtmFirstPrintTime ;	
			
				m_objLine8.m_ObjPrintLineInfo=objData8;
				///////////////9��/////////////////
				Object[] objData9=new object[11];
				objData9[0]=objclsInPatientEvaluateContent.m_strSkinFull;
				objData9[1]=objclsInPatientEvaluateContent.m_strSkinPallor;
				objData9[2]=objclsInPatientEvaluateContent.m_strSkinIcterus ;
				objData9[3]=objclsInPatientEvaluateContent.m_strSkinRed;
				objData9[4]=objclsInPatientEvaluateContent.m_strSkinCyanopathy ;
				objData9[5]=objclsInPatientEvaluateContent.m_strSkinDehydrate;
				objData9[6]=objclsInPatientEvaluateContent.m_strSkinAnthema ;
				objData9[7]=objclsInPatientEvaluateContent.m_strSkinBlood;
				objData9[8]=objclsInPatientEvaluateContent.m_strSkinSore;
				objData9[9]=objclsInPatientEvaluateContent.m_strSkinCut  ;
				objData9[10]=dtmFirstPrintTime ;	
				m_objLine9.m_ObjPrintLineInfo=objData9;
				///////////////10��/////////////////
				Object[] objData10=new object[9];
	
				objData10[0]=objclsInPatientEvaluateContent.m_strSkinDropsy;
				objData10[1]=objclsInPatientEvaluateContent.m_strBody;
				objData10[2]=objclsInPatientEvaluate.m_strBodyXML ;
				objData10[3]=objclsInPatientEvaluateContent.m_strUpperLimbs ;
				objData10[4]=objclsInPatientEvaluateContent.m_strLowerLimbs ;
				objData10[5]=objclsInPatientEvaluateContent.m_strSkinOther ;
				objData10[6]=objclsInPatientEvaluateContent.m_strSkinOtherContent ;
				objData10[7]=objclsInPatientEvaluate.m_strSkinOtherContentXML;
				objData10[8]=dtmFirstPrintTime ;	
				m_objLine10.m_ObjPrintLineInfo=objData10;
				///////////////11��/////////////////
				Object[] objData11=new object[7];
				objData11[0]=objclsInPatientEvaluateContent.m_strChogh;
				objData11[1]=objclsInPatientEvaluateContent.m_strHavePhlegm;
				objData11[2]=objclsInPatientEvaluateContent.m_strPhlegmEasy ;
				objData11[3]=objclsInPatientEvaluateContent.m_strPhlegmChroma ;
				objData11[4]=objclsInPatientEvaluateContent.m_strPhlegmColor ;
				objData11[5]=objclsInPatientEvaluate.m_strPhlegmColorXML;
				objData11[6]=dtmFirstPrintTime ;	
				m_objLine11.m_ObjPrintLineInfo=objData11;
				///////////////12��/////////////////
				Object[] objData12=new object[11];
				objData12[0]=objclsInPatientEvaluateContent.m_strPeeNatural;
				objData12[1]=objclsInPatientEvaluateContent.m_strPeeIrretention;
				objData12[2]=objclsInPatientEvaluateContent.m_strPeeUraemia ;
				objData12[3]=objclsInPatientEvaluateContent.m_strPeeMuch;
				objData12[4]=objclsInPatientEvaluateContent.m_strPeeBlood;
				objData12[5]=objclsInPatientEvaluateContent.m_strPeeCyst;
				objData12[6]=objclsInPatientEvaluateContent.m_strPeePipe ;
				objData12[7]=objclsInPatientEvaluateContent.m_strPeeOther;
				objData12[8]=objclsInPatientEvaluateContent.m_strPeeOtherContent;
				objData12[9]=objclsInPatientEvaluate.m_strPeeOtherContentXML ;
				objData12[10]=dtmFirstPrintTime ;	
				m_objLine12.m_ObjPrintLineInfo=objData12;
				///////////////13��/////////////////
				Object[] objData13=new object[9];
				objData13[0]=objclsInPatientEvaluateContent.m_strDejectaTimesInOneDay;
				objData13[1]=objclsInPatientEvaluate.m_strDejectaTimesInOneDayXML;
				objData13[2]=objclsInPatientEvaluateContent.m_strDejectaHowManyDaysOnce ;
				objData13[3]=objclsInPatientEvaluate.m_strDejectaHowManyDaysOnceXML ;
				objData13[4]=objclsInPatientEvaluateContent.m_strDejectaIrretention;
				objData13[5]=objclsInPatientEvaluateContent.m_strDejectaFistula;
				objData13[6]=objclsInPatientEvaluateContent.m_strDejectaCharacter ;
				objData13[7]=objclsInPatientEvaluate.m_strDejectaCharacterXML;
				objData13[8]=dtmFirstPrintTime ;	
				m_objLine13.m_ObjPrintLineInfo=objData13;
				///////////////14��/////////////////
				Object[] objData14=new object[3];
				objData14[0]=objclsInPatientEvaluateContent.m_strCanMyself;
				objData14[1]=objclsInPatientEvaluateContent.m_strBreathUrge;
				objData14[2]=dtmFirstPrintTime ;	
				m_objLine14.m_ObjPrintLineInfo=objData14;
				///////////////15��/////////////////
				Object[] objData15=new object[6];
				objData15[0]=objclsInPatientEvaluateContent.m_strLimbActive;
				objData15[1]=objclsInPatientEvaluateContent.m_strParalysis;
				objData15[2]=objclsInPatientEvaluateContent.m_strParalysisPart ;
				objData15[3]=objclsInPatientEvaluate.m_strParalysisPartXML;	
				objData15[4]=m_blnLimbActive_Not;
				objData15[5]=dtmFirstPrintTime;
				m_objLine15.m_ObjPrintLineInfo=objData15;
				///////////////16��/////////////////
				Object[] objData16=new object[11];
				objData16[0]=objclsInPatientEvaluateContent.m_strSleepHours;
				objData16[1]=objclsInPatientEvaluate.m_strSleepHoursXML;
				objData16[2]=objclsInPatientEvaluateContent.m_strSleepNothing ;
				objData16[3]=objclsInPatientEvaluateContent.m_strSleepDifficulty;
				objData16[4]=objclsInPatientEvaluateContent.m_strSleepWakeEasy;
				objData16[5]=objclsInPatientEvaluateContent.m_strSleepWakeEarly;
				objData16[6]=objclsInPatientEvaluateContent.m_strSleepDreamMuch ;
				objData16[7]=objclsInPatientEvaluateContent.m_strSleepOther;
				objData16[8]=objclsInPatientEvaluateContent.m_strSleepOtherContent;
				objData16[9]=objclsInPatientEvaluate.m_strSleepOtherContentXML ;
				objData16[10]=dtmFirstPrintTime ;	
				m_objLine16.m_ObjPrintLineInfo=objData16;
				///////////////17��/////////////////
				Object[] objData17=new object[6];
				objData17[0]=objclsInPatientEvaluateContent.m_strAssistantSleep;
				objData17[1]=objclsInPatientEvaluateContent.m_strAssistantSleepMedicines;
				objData17[2]=objclsInPatientEvaluate.m_strAssistantSleepMedicinesXML ;
				objData17[3]=objclsInPatientEvaluateContent.m_strAssistantSleepModel ;
				objData17[4]=objclsInPatientEvaluate.m_strAssistantSleepModelXML ;
				objData17[5]=dtmFirstPrintTime ;	
				m_objLine17.m_ObjPrintLineInfo=objData17;
			
				///////////////18��/////////////////
				Object[] objData18=new object[3];
				objData18[0]=objclsInPatientEvaluateContent.m_strShout;
				objData18[1]=objclsInPatientEvaluateContent.m_strAnswer;
				objData18[2]=dtmFirstPrintTime ;	
				m_objLine18.m_ObjPrintLineInfo=objData18;
				///////////////19��/////////////////
				Object[] objData19=new object[7];
				objData19[0]=objclsInPatientEvaluateContent.m_strLeftEyeDown;
				objData19[1]=objclsInPatientEvaluateContent.m_strLeftEyeBlur;
				objData19[2]=objclsInPatientEvaluateContent.m_strLeftEyeAgain ;
				objData19[3]=objclsInPatientEvaluateContent.m_strLeftEyeOther ;
				objData19[4]=objclsInPatientEvaluateContent.m_strLeftEyeOtherContent ;
				objData19[5]=objclsInPatientEvaluate.m_strLeftEyeOtherContentXML ;
				objData19[6]=dtmFirstPrintTime ;	
				m_objLine19.m_ObjPrintLineInfo=objData19;
				///////////////20��/////////////////
				Object[] objData20=new object[8];
				objData20[6]=objclsInPatientEvaluateContent.m_strSeeingBalk;
				objData20[0]=objclsInPatientEvaluateContent.m_strRightEyeDown;
				objData20[1]=objclsInPatientEvaluateContent.m_strRightEyeBlur;
				objData20[2]=objclsInPatientEvaluateContent.m_strRightEyeAgain ;
				objData20[3]=objclsInPatientEvaluateContent.m_strRightEyeOther ;
				objData20[4]=objclsInPatientEvaluateContent.m_strRightEyeOtherContent ;
				objData20[5]=objclsInPatientEvaluate.m_strRightEyeOtherContentXML ;
				objData20[7]=dtmFirstPrintTime ;	
				m_objLine20.m_ObjPrintLineInfo=objData20;
				///////////////21��/////////////////
				Object[] objData21=new object[7];
				objData21[0]=objclsInPatientEvaluateContent.m_strBothEyeDown;
				objData21[1]=objclsInPatientEvaluateContent.m_strBothEyeBlur;
				objData21[2]=objclsInPatientEvaluateContent.m_strBothEyeAgain ;
				objData21[3]=objclsInPatientEvaluateContent.m_strBothEyeOther ;
				objData21[4]=objclsInPatientEvaluateContent.m_strBothEyeOtherContent ;
				objData21[5]=objclsInPatientEvaluate.m_strBothEyeOtherContentXML ;
				objData21[6]=dtmFirstPrintTime ;	
				m_objLine21.m_ObjPrintLineInfo=objData21;
				///////////////22��/////////////////
				Object[] objData22=new object[7];
				objData22[0]=objclsInPatientEvaluateContent.m_strLeftListenDown;
				objData22[1]=objclsInPatientEvaluateContent.m_strLeftListenTinnitus;
				objData22[2]=objclsInPatientEvaluateContent.m_strLeftListenAgain ;
				objData22[3]=objclsInPatientEvaluateContent.m_strLeftListenOther ;
				objData22[4]=objclsInPatientEvaluateContent.m_strLeftListenOtherContent ;
				objData22[5]=objclsInPatientEvaluate.m_strLeftListenOtherContentXML ;
				objData22[6]=dtmFirstPrintTime ;	
				m_objLine22.m_ObjPrintLineInfo=objData22;
				///////////////23��/////////////////
				Object[] objData23=new object[8];
				objData23[6]=objclsInPatientEvaluateContent.m_strListenBalk;
				objData23[0]=objclsInPatientEvaluateContent.m_strRightListenDown;
				objData23[1]=objclsInPatientEvaluateContent.m_strRightListenTinnitus;
				objData23[2]=objclsInPatientEvaluateContent.m_strRightListenAgain ;
				objData23[3]=objclsInPatientEvaluateContent.m_strRightListenOther ;
				objData23[4]=objclsInPatientEvaluateContent.m_strRightListenOtherContent ;
				objData23[5]=objclsInPatientEvaluate.m_strRightListenOtherContentXML ;
				objData23[7]=dtmFirstPrintTime ;	
				m_objLine23.m_ObjPrintLineInfo=objData23;
				///////////////24��/////////////////
				Object[] objData24=new object[7];
				objData24[0]=objclsInPatientEvaluateContent.m_strBothListenDown;
				objData24[1]=objclsInPatientEvaluateContent.m_strBothListenTinnitus;
				objData24[2]=objclsInPatientEvaluateContent.m_strBothListenAgain ;
				objData24[3]=objclsInPatientEvaluateContent.m_strBothListenOther ;
				objData24[4]=objclsInPatientEvaluateContent.m_strBothListenOtherContent ;
				objData24[5]=objclsInPatientEvaluate.m_strBothListenOtherContentXML ;
				objData24[6]=dtmFirstPrintTime ;	
				m_objLine24.m_ObjPrintLineInfo=objData24;
				///////////////25��/////////////////
				Object[] objData25=new object[5];
				objData25[0]=objclsInPatientEvaluateContent.m_strAche;
				objData25[1]=objclsInPatientEvaluateContent.m_strAchePart;
				objData25[2]=objclsInPatientEvaluate.m_strAchePartXML ;
				objData25[3]=objclsInPatientEvaluateContent.m_strAcheTimes ;
				objData25[4]=dtmFirstPrintTime ;	
				m_objLine25.m_ObjPrintLineInfo=objData25;
				///////////////26��/////////////////
				Object[] objData26=new object[5];
				objData26[0]=objclsInPatientEvaluateContent.m_strAlwayslanguage;
				objData26[1]=objclsInPatientEvaluate.m_strAlwayslanguageXML;
				objData26[2]=objclsInPatientEvaluateContent.m_strMandarin ;
				objData26[3]=objclsInPatientEvaluateContent.m_strCantSay ;
				objData26[4]=dtmFirstPrintTime ;	
				m_objLine26.m_ObjPrintLineInfo=objData26;
				///////////////27��/////////////////
				Object[] objData27=new object[3];
				objData27[0]=objclsInPatientEvaluateContent.m_strInHospitalFeel;
				objData27[1]=objclsInPatientEvaluateContent.m_strLookIn;
				objData27[2]=dtmFirstPrintTime ;	
				m_objLine27.m_ObjPrintLineInfo=objData27;
			
				///////////////28��/////////////////
				Object[] objData28=new object[2];
				objData28[0]=objclsInPatientEvaluateContent.m_strEconomic ;
				objData28[1]=dtmFirstPrintTime ;	
				m_objLine28.m_ObjPrintLineInfo=objData28;
			
				///////////////29��/////////////////
				Object[] objData29=new object[5];
				objData29[0]=objclsInPatientEvaluateContent.m_strIllnessUnderstand;
				objData29[1]=objclsInPatientEvaluateContent.m_strDoctorsAdvice;
				objData29[2]=objclsInPatientEvaluateContent.m_strDoctorsAdviceContent ;
				objData29[3]=objclsInPatientEvaluate.m_strDoctorsAdviceContentXML;
				objData29[4]=dtmFirstPrintTime ;	
				m_objLine29.m_ObjPrintLineInfo=objData29;
				///////////////30��/////////////////
				Object[] objData30=new object[11];
				objData30[0]=objclsInPatientEvaluateContent.m_strIfSmoking;
				objData30[1]=objclsInPatientEvaluateContent.m_strSmokingYears;
				objData30[2]=objclsInPatientEvaluate.m_strSmokingYearsXML ;
				objData30[3]=objclsInPatientEvaluateContent.m_strSmokingQtyOneDay;
				objData30[4]=objclsInPatientEvaluate.m_strSmokingQtyOneDayXML ;
				objData30[5]=objclsInPatientEvaluateContent.m_strIfDrink;
				objData30[6]=objclsInPatientEvaluateContent.m_strDrinkYears ;
				objData30[7]=objclsInPatientEvaluate.m_strDrinkYearsXML;
				objData30[8]=objclsInPatientEvaluateContent.m_strDrinkQtyOneDay;
				objData30[9]=objclsInPatientEvaluate.m_strDrinkQtyOneDayXML  ;
				objData30[10]=dtmFirstPrintTime ;	
				m_objLine30.m_ObjPrintLineInfo=objData30;
			
				///////////////31��/////////////////
				Object[] objData31=new object[8];
				objData31[0]=objclsInPatientEvaluateContent.m_strFreakOut;
				objData31[1]=objclsInPatientEvaluateContent.m_strFreakOutMedicines;
				objData31[2]=objclsInPatientEvaluate.m_strFreakOutMedicinesXML ;
				objData31[3]=objclsInPatientEvaluateContent.m_strFreakOutYears;
				objData31[4]=objclsInPatientEvaluate.m_strFreakOutYearsXML;
				objData31[5]=objclsInPatientEvaluateContent.m_strFreakOutQtyOneDay;
				objData31[6]=objclsInPatientEvaluate.m_strFreakOutQtyOneDayXML ;
				objData31[7]=dtmFirstPrintTime ;	
				m_objLine31.m_ObjPrintLineInfo=objData31;
				#endregion 

			}
			else //��ֵΪ�հױ�
			{
				#region  ��һ�δ�ӡʱ�丳ֵ				
				DateTime dtmFirstPrintTime=DateTime.Now;				
				#endregion  ��һ�δ�ӡʱ�丳ֵ

				Object[] objData1=new object[2];
				objData1[0]=m_objPrintInfo;
				objData1[1]=dtmFirstPrintTime ;
				m_objLine1.m_ObjPrintLineInfo =objData1;
			
				#region ��ÿһ�е�Ԫ�ظ�ֵ

				///////////////2��/////////////////				
				m_objLine2.m_ObjPrintLineInfo=m_objInitObject(3,dtmFirstPrintTime);
				
				///////////////3��/////////////////
				m_objLine3.m_ObjPrintLineInfo=m_objInitObject(5,dtmFirstPrintTime);
			
			
				///////////////4��/////////////////
				m_objLine4.m_ObjPrintLineInfo=m_objInitObject(4,dtmFirstPrintTime);

				///////////////5��/////////////////
				m_objLine5.m_ObjPrintLineInfo=m_objInitObject(6,dtmFirstPrintTime);
				///////////////6��/////////////////
				m_objLine6.m_ObjPrintLineInfo=m_objInitObject(5,dtmFirstPrintTime);
				///////////////7��/////////////////
				m_objLine7.m_ObjPrintLineInfo=m_objInitObject(4,dtmFirstPrintTime);
				///////////////8��/////////////////
				m_objLine8.m_ObjPrintLineInfo=m_objInitObject(14,dtmFirstPrintTime);
				///////////////9��/////////////////
				m_objLine9.m_ObjPrintLineInfo=m_objInitObject(11,dtmFirstPrintTime);
				///////////////10��/////////////////
				m_objLine10.m_ObjPrintLineInfo=m_objInitObject(9,dtmFirstPrintTime);
				///////////////11��/////////////////
				m_objLine11.m_ObjPrintLineInfo=m_objInitObject(7,dtmFirstPrintTime);
				///////////////12��/////////////////
				m_objLine12.m_ObjPrintLineInfo=m_objInitObject(11,dtmFirstPrintTime);
				///////////////13��/////////////////
				m_objLine13.m_ObjPrintLineInfo=m_objInitObject(9,dtmFirstPrintTime);
				///////////////14��/////////////////
				m_objLine14.m_ObjPrintLineInfo=m_objInitObject(3,dtmFirstPrintTime);
				///////////////15��/////////////////
				m_objLine15.m_ObjPrintLineInfo=m_objInitObject(6,dtmFirstPrintTime);
				///////////////16��/////////////////
				m_objLine16.m_ObjPrintLineInfo=m_objInitObject(11,dtmFirstPrintTime);
				///////////////17��/////////////////
				m_objLine17.m_ObjPrintLineInfo=m_objInitObject(6,dtmFirstPrintTime);
			
				///////////////18��/////////////////
				m_objLine18.m_ObjPrintLineInfo=m_objInitObject(3,dtmFirstPrintTime);
				///////////////19��/////////////////
				m_objLine19.m_ObjPrintLineInfo=m_objInitObject(7,dtmFirstPrintTime);
				///////////////20��/////////////////
				m_objLine20.m_ObjPrintLineInfo=m_objInitObject(8,dtmFirstPrintTime);
				///////////////21��/////////////////
				m_objLine21.m_ObjPrintLineInfo=m_objInitObject(7,dtmFirstPrintTime);
				///////////////22��/////////////////
				m_objLine22.m_ObjPrintLineInfo=m_objInitObject(7,dtmFirstPrintTime);
				///////////////23��/////////////////
				m_objLine23.m_ObjPrintLineInfo=m_objInitObject(8,dtmFirstPrintTime);
				///////////////24��/////////////////
				m_objLine24.m_ObjPrintLineInfo=m_objInitObject(7,dtmFirstPrintTime);
				///////////////25��/////////////////
				m_objLine25.m_ObjPrintLineInfo=m_objInitObject(5,dtmFirstPrintTime);
				///////////////26��/////////////////
				m_objLine26.m_ObjPrintLineInfo=m_objInitObject(5,dtmFirstPrintTime);
				///////////////27��/////////////////
				m_objLine27.m_ObjPrintLineInfo=m_objInitObject(3,dtmFirstPrintTime);
			
				///////////////28��/////////////////
				m_objLine28.m_ObjPrintLineInfo=m_objInitObject(2,dtmFirstPrintTime);
			
				///////////////29��/////////////////
				m_objLine29.m_ObjPrintLineInfo=m_objInitObject(5,dtmFirstPrintTime);
				///////////////30��/////////////////
				m_objLine30.m_ObjPrintLineInfo=m_objInitObject(11,dtmFirstPrintTime);
			
				///////////////31��/////////////////
				m_objLine31.m_ObjPrintLineInfo=m_objInitObject(8,dtmFirstPrintTime);
				#endregion 
			}
		}
		private void pdcOperation_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			
		}
		
		
		#region �������ֲ���
		/// <summary>
		/// �������ֲ���
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{            
			e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,m_fotSmallFont ,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_HospitalName  ));
		
			e.Graphics.DrawString("�� Ժ �� �� �� �� ��",m_fotTitleFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_Name_Title ));
			

			e.Graphics.DrawString("������",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name_Title  ));
			e.Graphics.DrawString(m_objPrintInfo.m_strPatientName  ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Name ));
		
			e.Graphics.DrawString("�Ա�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex_Title ));
			e.Graphics.DrawString(m_objPrintInfo.m_strSex ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Sex  ));

			e.Graphics.DrawString("���䣺",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age_Title ));
			e.Graphics.DrawString(m_objPrintInfo.m_strAge ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Age ));

			e.Graphics.DrawString("������",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name_Title ));
			e.Graphics.DrawString(m_objPrintInfo.m_strAreaName ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Dept_Name ));
			
			e.Graphics.DrawString("���ţ�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo_Title ));
			e.Graphics.DrawString(m_objPrintInfo.m_strBedName ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.BedNo ));

			e.Graphics.DrawString("סԺ�ţ�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID_Title ));
			e.Graphics.DrawString(m_objPrintInfo.m_strHISInPatientID ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID ));	
		}
		
	
		#endregion						
		
		#region print class 

		private class clsPrintLine1 : clsPrintLineBase
		{
			private DateTime dtmFirstPrint;
			
			private string strOccupation;
			private string strNation;
			private string strHometown;
			private string strInPatientDate;
		

			private bool m_blnFirstPrint = true;

			public clsPrintLine1()
			{
			}
			
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_blnFirstPrint)
				{
					Font fntText = new Font("SimSun",12);
					Font fntValueText = new Font("SimSun",10);

					p_objGrp.DrawString("ְҵ��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+45,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+140,p_intPosY +(int)enmRectangleInfo.RowStep);
					p_objGrp.DrawString("���壺",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+150,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+195,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+280,p_intPosY +(int)enmRectangleInfo.RowStep);
					p_objGrp.DrawString("���᣺",fntText,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+290,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+335,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+470,p_intPosY +(int)enmRectangleInfo.RowStep);
					p_objGrp.DrawString("��Ժʱ�䣺",fntText,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+480,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+560,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.RightX-10,p_intPosY +(int)enmRectangleInfo.RowStep);


					p_objGrp.DrawString(strOccupation,fntValueText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+45,p_intPosY);
					p_objGrp.DrawString(strNation,fntValueText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+195,p_intPosY);
					p_objGrp.DrawString(strHometown,fntValueText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+335,p_intPosY);
					if(strInPatientDate != null && strInPatientDate.Trim() != "")
						p_objGrp.DrawString(DateTime.Parse(strInPatientDate).ToString("yyyy��MM��dd�� HH:mm"),fntValueText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+560,p_intPosY);

					fntText.Dispose();
					fntValueText.Dispose();

					m_blnFirstPrint = false;
				}

				m_blnHaveMoreLine = false;
				p_intPosY += (int)enmRectangleInfo.RowStep+10;
			}

			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_blnFirstPrint = true;			
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
						dtmFirstPrint=(DateTime)objData[1];
						if(objData[0]==null)
						{
							strOccupation = "";
							strNation = "";
							strHometown = "";
							strInPatientDate ="" ;
						}	
						else
						{
							clsPrintInfo_InPatientEvaluate objPrintInfo = (clsPrintInfo_InPatientEvaluate)objData[0];	//�������Ӳ��˶���ı�Ϊ��ӡ��Ϣ����.
							strOccupation = objPrintInfo.m_strOccupation;
							strNation = objPrintInfo.m_strNationnality;
							strHometown = objPrintInfo.m_strHometown;
							if(objPrintInfo.m_dtmInPatientDate != DateTime.MinValue)
							{
								strInPatientDate =objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss") ;
							}
							else
							{
								strInPatientDate = "";
							}
						}					
					}
				}
			}
		}
	

		private class clsPrintLine2 : clsPrintLineBase
		{
			private string strGroup1; 
			private DateTime dtmFirstPrint;
			private string strGroup2;
			private clsPublicControlPaint m_objCPaint;
			public clsPrintLine2()
			{
				m_objCPaint=new clsPublicControlPaint ();
			
			}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);
				
				p_objGrp.DrawString("����״����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("δ��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+100,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+160-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("�ѻ�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+160,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+220-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+220,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+280-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("�¹�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+280,p_intPosY);
				p_objGrp.DrawString("�����̶ȣ�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+330,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+430-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("��ä",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+430,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+490-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("Сѧ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+490,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+550-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("��ѧ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+550,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+610-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("��ר����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+610,p_intPosY);
				
				switch(strGroup1)
				{
					case "0":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
					case "1":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+160-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
					case "2":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+220-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
					case "3":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+280-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
				}
				switch(strGroup2)
				{

					case "0":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+430-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
					case "1":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+490-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
					case "2":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+550-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
					case "3":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+610-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
				}
			
				m_blnHaveMoreLine = false;
	

				p_intPosY += (int)enmRectangleInfo.RowStep+10;

				fntText.Dispose();
				fntCheck.Dispose();

			}

			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					Object [] objData=(object[])value;

					strGroup1 = (objData[0] != null) ? (objData[0].ToString()) : "";
					strGroup2= (objData[1] != null) ? (objData[1].ToString()) : "";
					dtmFirstPrint=(DateTime)objData[2];
									
				}
			}
		}
	
	
		private class clsPrintLine3 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private clsPrintRichTextContext m_objText2;
			private string strGroup1;
			private DateTime dtmFirstPrint;
			private string strGroup2;
			private bool m_blnFirstPrint=true;

			private clsPublicControlPaint m_objCPaint;
			
			
			public clsPrintLine3()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",12));
				m_objText2 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",12));
				m_objCPaint=new clsPublicControlPaint ();			
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);

				if(m_blnFirstPrint ==true)
				{
					p_objGrp.DrawString("�ڽ�������",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+100,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+140-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��:",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+140,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+170,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+320,p_intPosY +(int)enmRectangleInfo.RowStep);

					p_objGrp.DrawString("������Դ��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+330,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+430-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+430,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+490-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+490,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+550-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+550,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+610-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����:",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+610,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+655,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.RightX-10,p_intPosY +(int)enmRectangleInfo.RowStep);
			
					switch (strGroup1)
					{
						case "0": case "False":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
							break;
						case "1": case "True":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+140-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
							break;
					}
					switch (strGroup2)
					{
						case "0":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+430-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
							break;
						case "1":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+490-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
							break;
						case "2":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+550-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
							break;
						case "3":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+610-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
							break;
					}
					m_blnFirstPrint=false;
				}

				int intRealHeight;

				Rectangle rtgBlock1 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+170,
					p_intPosY-10,
					210,
					30);
				m_objText1.m_blnPrintAllBySimSun(10,rtgBlock1,p_objGrp,out intRealHeight,true);

				Rectangle rtgBlock2 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+660,
					p_intPosY-10,
					(int)enmRectangleInfo.RightX-10-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+650),
					30);
				m_objText2.m_blnPrintAllBySimSun(10,rtgBlock2,p_objGrp,out intRealHeight,true);
				m_blnHaveMoreLine = false;
				p_intPosY += (int)enmRectangleInfo.RowStep+10;

				fntText.Dispose();
				fntCheck.Dispose();
				
			}
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_blnFirstPrint=true;
				m_objText1.m_mthRestartPrint();
				m_objText2.m_mthRestartPrint();
			
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
						Object[] objLine3=(Object[])value ;
						dtmFirstPrint=(DateTime)objLine3[4];
						m_objText1.m_mthSetContextWithCorrectBefore(objLine3[1].ToString() ,"",dtmFirstPrint);
						m_objText2.m_mthSetContextWithCorrectBefore(objLine3[3].ToString() ,"",dtmFirstPrint);
						strGroup1=objLine3[0].ToString();
						strGroup2 =objLine3[2].ToString();
						

                        						
					}
				}
			}
		}
	

		private class clsPrintLine4 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private clsPublicControlPaint m_objCPaint;
			private string strGroup1; 
			private DateTime dtmFirstPrint;
			

			private bool m_blnFirstPrint = true;
			
			public clsPrintLine4()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",12));
				m_objCPaint=new clsPublicControlPaint ();
			
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY += 5;

				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);

				if(m_blnFirstPrint==true)
				{
					p_objGrp.DrawString("��Ժ��ϣ�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+80,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+320,p_intPosY +(int)enmRectangleInfo.RowStep);

					p_objGrp.DrawString("��Ժ��ʽ��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+330,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+430-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+430,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+490-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+490,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+550-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+550,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+610-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+610,p_intPosY);
					switch (strGroup1)
					{
						case "0":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+430-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
							break;
						case "1":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+490-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
							break;
						case "2":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+550-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
							break;
						case "3":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+610-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
							break;
	
					}
					m_blnFirstPrint =false;
				}


				int intRealHeight;

				Rectangle rtgBlock1 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+80,
					p_intPosY-10,
					250,
					30);
				if(m_objText1.m_blnPrintAllBySimSun(10,rtgBlock1,p_objGrp,out intRealHeight,true))
				{
					p_intPosY += (int)enmRectangleInfo.RowStep+20;
				}
				else
				{
					p_intPosY += (int)enmRectangleInfo.RowStep+10;
				}
				m_blnHaveMoreLine = false;
				
				fntText.Dispose();
				fntCheck.Dispose();

			}
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_blnFirstPrint = true;
				m_objText1.m_mthRestartPrint();
				
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
						Object[] objLine4=(Object[])value ;
						dtmFirstPrint=(DateTime)objLine4[3];
						//test
						//						m_objText1.m_mthSetContextWithAllCorrect("asdfasdfasdfasdfsadfasdfasdfasdfasdfasdfasdffdsf","");
						m_objText1.m_mthSetContextWithCorrectBefore(objLine4[0].ToString() ,objLine4[1].ToString(),dtmFirstPrint,true);
						m_mthAddSign("��Ժ��ϣ�",m_objText1.m_ObjModifyUserArr);
						strGroup1=objLine4[2].ToString();
						
                        						
					}
				}
			}
		}
	

		private class clsPrintLine5 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private clsPrintRichTextContext m_objText2;
			private string strGroup1;
			private DateTime dtmFirstPrint;
			private bool m_blnPrintFirst=true;


			private clsPublicControlPaint m_objCPaint;
			
			
			public clsPrintLine5()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",12));
				m_objText2 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",12));
				m_objCPaint=new clsPublicControlPaint ();
			
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY += 5;

				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);

				if(m_blnPrintFirst==true)
				{
					p_objGrp.DrawString("����ʷ��",fntText,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+100,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+140-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("�У�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+140,p_intPosY);
					p_objGrp.DrawString("����ԭ��",fntText,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+170,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+235,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+370,p_intPosY +(int)enmRectangleInfo.RowStep);
					p_objGrp.DrawString("֢״",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+380,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+420,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+600-(int)enmRectangleInfo.CheckShift,p_intPosY +(int)enmRectangleInfo.RowStep);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+610-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);		
					p_objGrp.DrawString("����ȷ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+610,p_intPosY);
			
					switch (strGroup1)
					{
						case "0":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
							break;
						case "1":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+140-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
							break;
						case "2":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+610-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
							break;

					}
					m_blnPrintFirst=false;
				}

				int intRealHeight;
				Rectangle rtgBlock1 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+235,
					p_intPosY-10,
					140,
					30);
				m_objText1.m_blnPrintAllBySimSun(10,rtgBlock1,p_objGrp,out intRealHeight,true);

				Rectangle rtgBlock2 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+418,
					p_intPosY-10,
					610-(int)enmRectangleInfo.CheckShift-418-5,
					30);
				m_objText2.m_blnPrintAllBySimSun(10,rtgBlock2,p_objGrp,out intRealHeight,true);
				
				m_blnHaveMoreLine = false;
				p_intPosY += (int)enmRectangleInfo.RowStep+15;
				m_objHandlePartEnd(p_intPosY,p_objGrp,p_fntNormalText);
			
				fntText.Dispose();		
				fntCheck.Dispose();

			}
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_blnPrintFirst=true;
				m_objText1.m_mthRestartPrint();
				m_objText2.m_mthRestartPrint();
			
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
						Object[] objLine5=(Object[])value ;
						dtmFirstPrint=(DateTime)objLine5[5];
						m_objText1.m_mthSetContextWithCorrectBefore(objLine5[1].ToString() ,objLine5[2].ToString(),dtmFirstPrint,true);
						m_mthAddSign("����ԭ��",m_objText1.m_ObjModifyUserArr);
						m_objText2.m_mthSetContextWithCorrectBefore(objLine5[3].ToString() ,objLine5[4].ToString(),dtmFirstPrint,true);
						m_mthAddSign("����֢״��",m_objText2.m_ObjModifyUserArr);
					
						strGroup1 =objLine5[0].ToString();
						
                        					
					}
				}
			}
	
		}
	
	
		private class clsPrintLine6 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private string strGroup1; private DateTime dtmFirstPrint;
			private string strGroup2;
			private bool m_blnPrintFirst=true;

			private clsPublicControlPaint m_objCPaint;
		
			private int m_intTimes=0;

			
			public clsPrintLine6()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",10));
				m_objCPaint=new clsPublicControlPaint ();
			
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{				
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);
				if(m_blnPrintFirst==true)
				{
					p_objGrp.DrawString("ʳ����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+60-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat );
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+60,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+120-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+120,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+180-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("�½�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+180,p_intPosY);
					p_objGrp.DrawString("�������ر仯��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+240,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+380-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+380,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+420-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����ȷ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+420,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+500-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+500,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+560-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("�½���",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+560,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+610,p_intPosY+(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+660,p_intPosY+(int)enmRectangleInfo.RowStep);
					p_objGrp.DrawString("kg",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+665,p_intPosY);
					switch(strGroup1)
					{
						case "0":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+60-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

							break;
						case "1":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+120-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

							break;
						case "2":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+180-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

							break;

					}

					switch(strGroup2)
					{
						case "0":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+380-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

							break;
						case "1":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+420-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

							break;
						case "2":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+500-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

							break;
						case "3":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+560-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

							break;

					}
					m_blnPrintFirst =false;
				}
				m_objText1.m_mthPrintLine(60,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+610,p_intPosY,p_objGrp);


				if(m_objText1.m_BlnHaveNextLine())
				{
					m_blnHaveMoreLine = true;
					p_intPosY +=(int)enmRectangleInfo.SmallRowStep+5 ;
					m_intTimes++;
				}
				else
				{
					m_blnHaveMoreLine = false;
					p_intPosY += (int)enmRectangleInfo.RowStep+5;
				}

				fntText.Dispose();
				fntCheck.Dispose();
			}

			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_intTimes=0;
				m_blnPrintFirst=true;
				m_objText1.m_mthRestartPrint();
				
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
						Object[] objLine6=(Object[])value ;
						dtmFirstPrint=(DateTime)objLine6[4];
						m_objText1.m_mthSetContextWithCorrectBefore(objLine6[2].ToString() ,objLine6[3].ToString(),dtmFirstPrint,true);
						m_mthAddSign("�������ر仯��",m_objText1.m_ObjModifyUserArr);
						strGroup1=objLine6[0].ToString();
						strGroup2=objLine6[1].ToString();
						
                        						
					}
				}
			}
		}
		
		
		private class clsPrintLine7 : clsPrintLineBase
		{
			private string strGroup1; 
			private DateTime dtmFirstPrint;
			private string strGroup2;
			private string strGroup3;
			private clsPublicControlPaint m_objCPaint;
			public clsPrintLine7()
			{
				m_objCPaint=new clsPublicControlPaint ();
			
			}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);

				p_objGrp.DrawString("��ǻճĤ��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+100,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+160-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+160,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+220-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+220,p_intPosY);
				p_objGrp.DrawString("�׽����ѣ�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+330,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+430-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+430,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+470-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+470,p_intPosY);
				p_objGrp.DrawString("�������ѣ�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+500,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+600-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+600,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+640-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+640,p_intPosY);
				

				switch(strGroup1)
				{
					case "0":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
					case "1":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+160-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
					case "2":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+220-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
				}

				switch(strGroup2)
				{
					case "0": case "False":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+430-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
					case "1": case "True":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+470-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
				}
			
				switch(strGroup3)
				{
					case "0": case "False":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+600-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
					case "1": case "True":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+640-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
				}

				m_blnHaveMoreLine = false;
			

				p_intPosY += (int)enmRectangleInfo.RowStep+10;

                fntText.Dispose();
				fntCheck.Dispose();
			}

			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					Object [] objData=(object[])value;
					dtmFirstPrint=(DateTime)objData[3];
					strGroup1=objData[0].ToString();
					strGroup2=objData[1].ToString ();
					strGroup3=objData[2].ToString ();
					
									
				}
			}
		}
	
		
		private class clsPrintLine8 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private clsPrintRichTextContext m_objText2;
			private clsPrintRichTextContext m_objText3;
			private clsPrintRichTextContext m_objText4;

			private string strGroup1; private DateTime dtmFirstPrint;
			private string strGroup2;
			private string strGroup3;
			private string strGroup4;
			private string strGroup5;

			private bool m_blnPrintFirst=true;
		
			private clsPublicControlPaint m_objCPaint;
			
			
			public clsPrintLine8()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",12));
				m_objText2 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",12));
				m_objText3 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",12));
				m_objText4 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",12));
				m_objCPaint=new clsPublicControlPaint ();
			
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);

				if(m_blnPrintFirst==true)
				{
					p_objGrp.DrawString("θ����֢״��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+115-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+115,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+155-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+155,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+210-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��ʹ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+210,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+265-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+265,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+320-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("Ż�£�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+320,p_intPosY);
					p_objGrp.DrawString("��ɫ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+365,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+405,p_intPosY+(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+460,p_intPosY+(int)enmRectangleInfo.RowStep);
					
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+470,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+510,p_intPosY+(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+560,p_intPosY+(int)enmRectangleInfo.RowStep);
					
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+570,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+610,p_intPosY+(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+635,p_intPosY+(int)enmRectangleInfo.RowStep);
					
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+640,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+660,p_intPosY+(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+690,p_intPosY+(int)enmRectangleInfo.RowStep);
					p_objGrp.DrawString("ml",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+695,p_intPosY);
			
					if(strGroup1=="True" || strGroup1=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+115-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
					if(strGroup2=="True" || strGroup2=="1")//ע��:�˴�Ϊ��ѡ,��������else if.
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+155-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
					if(strGroup3=="True" || strGroup3=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+210-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
					if(strGroup4=="True" || strGroup4=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+265-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
					if(strGroup5=="True" || strGroup5=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+320-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					m_blnPrintFirst=false;
				}

				int intRealHeight;
				Rectangle rtgBlock1 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+405,
					p_intPosY-10,
					75,
					30);
				m_objText1.m_blnPrintAllBySimSun(10,rtgBlock1,p_objGrp,out intRealHeight,true);

				Rectangle rtgBlock2 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+510,
					p_intPosY-10,
					70,
					30);
				m_objText2.m_blnPrintAllBySimSun(10,rtgBlock2,p_objGrp,out intRealHeight,true);

				Rectangle rtgBlock3 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+610,
					p_intPosY-10,
					35,
					30);
				m_objText3.m_blnPrintAllBySimSun(10,rtgBlock3,p_objGrp,out intRealHeight,true);

				Rectangle rtgBlock4 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+660,
					p_intPosY-10,
					35,
					30);
				m_objText4.m_blnPrintAllBySimSun(10,rtgBlock4,p_objGrp,out intRealHeight,true);

				m_blnHaveMoreLine = false;
				p_intPosY += (int)enmRectangleInfo.RowStep+10;
							
				fntText.Dispose();
				fntCheck.Dispose();
			}
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_blnPrintFirst=true;
				m_objText1.m_mthRestartPrint();
				m_objText2.m_mthRestartPrint();
				m_objText3.m_mthRestartPrint();
				m_objText4.m_mthRestartPrint();
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
						Object[] objData=(Object[])value ;
						dtmFirstPrint=(DateTime)objData[13];
						m_objText1.m_mthSetContextWithCorrectBefore(objData[5].ToString(),objData[6].ToString(),dtmFirstPrint,true);
						m_mthAddSign("Ż����ɫ��",m_objText1.m_ObjModifyUserArr);
						m_objText2.m_mthSetContextWithCorrectBefore(objData[7].ToString(),objData[8].ToString(),dtmFirstPrint,true);
						m_mthAddSign("Ż�����ʣ�",m_objText2.m_ObjModifyUserArr);
						m_objText3.m_mthSetContextWithCorrectBefore(objData[9].ToString(),objData[10].ToString(),dtmFirstPrint,true);
						m_mthAddSign("Ż�´�����",m_objText3.m_ObjModifyUserArr);
						m_objText4.m_mthSetContextWithCorrectBefore(objData[11].ToString(),objData[12].ToString(),dtmFirstPrint,true);
						m_mthAddSign("Ż������",m_objText4.m_ObjModifyUserArr);

						strGroup1=objData[0].ToString();
						strGroup2=objData[1].ToString();
						strGroup3=objData[2].ToString();
						strGroup4=objData[3].ToString();
						strGroup5=objData[4].ToString();
                        						
					}
				}
			}
		}
	
				
		private class clsPrintLine9 : clsPrintLineBase
		{
			
			private string strGroup1;
			private DateTime dtmFirstPrint;
			private string strGroup2;
			private string strGroup3;
			private string strGroup4;
			private string strGroup5;
			private string strGroup6;
			private string strGroup7;
			private string strGroup8;
			private string strGroup9;
			private string strGroup10;
			
			private clsPublicControlPaint m_objCPaint;
			
			
			public clsPrintLine9()
			{
				
				m_objCPaint=new clsPublicControlPaint ();
			
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);

				p_objGrp.DrawString("Ƥ����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+60-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+60,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+120-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("�԰�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+120,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+180-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+180,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+240-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+240,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+300-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("���",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+300,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+360-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("��ˮ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+360,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+420-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("Ƥ��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+420,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+480-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("��Ѫ��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+480,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+560-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("�촯",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+560,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+620-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("�˿�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+620,p_intPosY);
			
				if(strGroup1=="True" || strGroup1=="1")
					p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+60-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

				if(strGroup2=="True" || strGroup2=="1")//ע��:�˴�Ϊ��ѡ,��������else if.
		
					p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+120-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

				if(strGroup3=="True" || strGroup3=="1")
					p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+180-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

				if(strGroup4=="True" || strGroup4=="1")
					p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+240-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

				if(strGroup5=="True" || strGroup5=="1")
					p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+300-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

				if(strGroup6=="True" || strGroup6=="1")
					p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+360-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

				if(strGroup7=="True" || strGroup7=="1")
					p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+420-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

				if(strGroup8=="True" || strGroup8=="1")
					p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+480-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

				if(strGroup9=="True" || strGroup9=="1")
					p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+560-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

				if(strGroup10=="True" || strGroup10=="1")
					p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+620-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);


				m_blnHaveMoreLine = false;
				p_intPosY += (int)enmRectangleInfo.RowStep+10;

				fntText.Dispose();
				fntCheck.Dispose();
				
			}

			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
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
						Object[] objLine9=(Object[])value ;
						
						strGroup1 =objLine9[0].ToString();
						strGroup2 =objLine9[1].ToString();
						strGroup3 =objLine9[2].ToString();
						strGroup4 =objLine9[3].ToString();
						strGroup5 =objLine9[4].ToString();
						strGroup6 =objLine9[5].ToString();
						strGroup7 =objLine9[6].ToString();
						strGroup8 =objLine9[7].ToString();
						strGroup9 =objLine9[8].ToString();
						strGroup10 =objLine9[9].ToString();
						dtmFirstPrint=(DateTime)objLine9[10];
                        						
					}
				}
			}
		}
	

		private class clsPrintLine10 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private clsPrintRichTextContext m_objText2;
			private string strGroup1;
			private DateTime dtmFirstPrint;
			private string strGroup2;
			private string strGroup3;
			private string strGroup4;
			private bool m_blnPrintFirst=true;

			private clsPublicControlPaint m_objCPaint;
			
			
			public clsPrintLine10()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",12));
				m_objText2 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",12));
				m_objCPaint=new clsPublicControlPaint ();
			
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);

				if(m_blnPrintFirst==true)
				{
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+60-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("ˮ�ף�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+60,p_intPosY);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+105,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+145,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+190,p_intPosY +(int)enmRectangleInfo.RowStep);
					
					p_objGrp.DrawString("��֫��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+200,p_intPosY);
					p_objGrp.DrawString("��֫��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+240,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+300-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+300,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+340-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+340,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+380-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("˫�ࣻ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+380,p_intPosY);
					p_objGrp.DrawString("��֫��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+420,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+480-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+480,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+520-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+520,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+560-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("˫��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+560,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+620-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("������",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+620,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+670,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.RightX-10,p_intPosY +(int)enmRectangleInfo.RowStep);
					
					if(strGroup1 =="True" || strGroup1=="1")
					{
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+60-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						switch (strGroup2)
						{
							case "0":
								p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+300-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

								break;
							case "1":
								p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+340-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

								break;
							case "2":
								p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+380-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

								break;
						}

						switch (strGroup3)
						{
							case "0":
								p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+480-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

								break;
							case "1":
								p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+520-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

								break;
							case "2":
								p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+560-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

								break;
						}
					}

					if(strGroup4=="True" || strGroup4=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+620-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

 
					m_blnPrintFirst =false ;

				}

				int intRealHeight;
				Rectangle rtgBlock1 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+145,
					p_intPosY-10,
					60,
					30);
				m_objText1.m_blnPrintAllBySimSun(10,rtgBlock1,p_objGrp,out intRealHeight,true);

				Rectangle rtgBlock2 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+670,
					p_intPosY-10,
					60,
					30);
				m_objText2.m_blnPrintAllBySimSun(10,rtgBlock2,p_objGrp,out intRealHeight,true);

				m_blnHaveMoreLine = false;
				p_intPosY += (int)enmRectangleInfo.RowStep+10;

				fntText.Dispose();		
				fntCheck.Dispose();
			}
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_blnPrintFirst=true;
				m_objText1.m_mthRestartPrint();
				m_objText2.m_mthRestartPrint();			
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
						Object[] objLine10=(Object[])value ;
						dtmFirstPrint=(DateTime)objLine10[8];
						m_objText1.m_mthSetContextWithCorrectBefore(objLine10[1].ToString() ,objLine10[2].ToString(),dtmFirstPrint,true);
						m_mthAddSign("Ƥ��ˮ�ף�",m_objText1.m_ObjModifyUserArr);
						m_objText2.m_mthSetContextWithCorrectBefore(objLine10[6].ToString() ,objLine10[7].ToString(),dtmFirstPrint,true);
						m_mthAddSign("Ƥ��������",m_objText2.m_ObjModifyUserArr);
						
						strGroup1=objLine10[0].ToString();
						strGroup2=objLine10[3].ToString();
						strGroup3=objLine10[4].ToString();
						strGroup4=objLine10[5].ToString();
						
                        						
					}
				}
			}
		}
	

		private class clsPrintLine11 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
		
			private string strGroup1; private DateTime dtmFirstPrint;
			private string strGroup2;
			private string strGroup3;
			private string strGroup4;
			
			private bool m_blnPrintFirst=true;

			private clsPublicControlPaint m_objCPaint;
			
			
			public clsPrintLine11()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",12));
				m_objCPaint=new clsPublicControlPaint ();
			
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);

				if(m_blnPrintFirst==true)
				{
					p_objGrp.DrawString("���ԣ�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+60-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+60,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+100,p_intPosY);
					p_objGrp.DrawString("̵��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+120,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+160-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+160,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+200-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("�У�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+200,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+250-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("�׿ȳ�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+250,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+320-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("���׿ȳ�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+320,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+410-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("ϡ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+410,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+450-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+450,p_intPosY);
					p_objGrp.DrawString("��ɫ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+480,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+520,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.RightX-10,p_intPosY +(int)enmRectangleInfo.RowStep);
					
					if(strGroup1=="False" || strGroup1=="0" )
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+60-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					else if(strGroup1=="True" || strGroup1=="1" )//Ϊ�˴�ӡ�հױ���,������elseֱ�Ӵ���,��ͬ
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);


					if(strGroup2=="False" || strGroup2=="0")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+160-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					else if(strGroup2=="True" || strGroup2=="1" )//Ϊ�˴�ӡ�հױ���,������elseֱ�Ӵ���,��ͬ
					{	
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+200-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						if(strGroup3=="False" || strGroup3=="0")
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+250-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						else  if(strGroup3=="True" || strGroup3=="1")//Ϊ�˴�ӡ�հױ���,������elseֱ�Ӵ���,��ͬ
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+320-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						if(strGroup4=="False" || strGroup4=="0")
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+410-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						else  if(strGroup4=="True" || strGroup4=="1")//Ϊ�˴�ӡ�հױ���,������elseֱ�Ӵ���,��ͬ
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+450-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
					}
					m_blnPrintFirst =false;
				}

				int intRealHeight;
				Rectangle rtgBlock1 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+520,
					p_intPosY-10,
					190,
					30);
				m_objText1.m_blnPrintAllBySimSun(10,rtgBlock1,p_objGrp,out intRealHeight,true);

//				m_objText1.m_mthPrintLine( (int)enmRectangleInfo.RightX-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+518), (int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+518,p_intPosY,p_objGrp);
//				if(m_objText1.m_BlnHaveNextLine())
//				{
//					m_blnHaveMoreLine = true;
//					p_intPosY +=(int)enmRectangleInfo.SmallRowStep+5 ;
//					m_intTimes++;
//				}
//				else
//				{
					m_blnHaveMoreLine = false;
					p_intPosY += (int)enmRectangleInfo.RowStep+15;
					m_objHandlePartEnd(p_intPosY,p_objGrp,p_fntNormalText);
//				}
				
				fntText.Dispose();
				fntCheck.Dispose();
			}
//			private int m_intTimes=0;
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
//				m_intTimes=0;
				m_blnPrintFirst=true;
				m_objText1.m_mthRestartPrint();
			
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
						Object[] objLine11=(Object[])value ;
						dtmFirstPrint=(DateTime)objLine11[6];
						//						m_objText1.m_mthSetContextWithAllCorrect("asdfasdfasdfasdfsadfasdfasdfasdfasdfasdfasdffdsf","");


						m_objText1.m_mthSetContextWithCorrectBefore(objLine11[4].ToString() ,objLine11[5].ToString(),dtmFirstPrint,true);
						m_mthAddSign("������ɫ��",m_objText1.m_ObjModifyUserArr);
					
						strGroup1 =objLine11[0].ToString();
						strGroup2 =objLine11[1].ToString();
						strGroup3 =objLine11[2].ToString();
						strGroup4 =objLine11[3].ToString();
						
	                  						
					}
				}
			}
		}
	

		private class clsPrintLine12 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			
			private string strGroup1; private DateTime dtmFirstPrint;
			private string strGroup2;
			private string strGroup3;
			private string strGroup4;
			private string strGroup5;
			private string strGroup6;
			private string strGroup7;
			private string strGroup8;
			private bool m_blnPrintFirst=true;

			private clsPublicControlPaint m_objCPaint;
			
			
			public clsPrintLine12()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",12));
				
				m_objCPaint=new clsPublicControlPaint ();
			
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY += 5;
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);

				if(m_blnPrintFirst==true)
				{
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+60-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+60,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+120-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��ʧ��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+120,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+200-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("������",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+200,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+280-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��Ƶ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+280,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+340-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("Ѫ��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+340,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+400-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��������",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+400,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+500-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("�����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+500,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+580-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("������",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+580,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+630,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.RightX-10,p_intPosY +(int)enmRectangleInfo.RowStep);
					
					if(strGroup1 =="True" || strGroup1=="1" )
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+60-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup2 =="True" || strGroup2=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+120-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup3 =="True" || strGroup3=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+200-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup4 =="True" || strGroup4=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+280-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup5 =="True" || strGroup5=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+340-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup6 =="True" || strGroup6=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+400-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup7 =="True" || strGroup7=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+500-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup8 =="True" || strGroup8=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+580-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					m_blnPrintFirst=false;


				}

				int intRealHeight;
				Rectangle rtgBlock1 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+630,
					p_intPosY-10,
					100,
					30);
				m_objText1.m_blnPrintAllBySimSun(10,rtgBlock1,p_objGrp,out intRealHeight,true);

				m_blnHaveMoreLine = false;
				p_intPosY += (int)enmRectangleInfo.RowStep+10;

				fntText.Dispose();
				fntCheck.Dispose();
			}
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_blnPrintFirst=true;
				m_objText1.m_mthRestartPrint();
			
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
						Object[] objLine12=(Object[])value ;
						dtmFirstPrint=(DateTime)objLine12[10];
						m_objText1.m_mthSetContextWithCorrectBefore(objLine12[8].ToString() ,objLine12[9].ToString(),dtmFirstPrint,true);
						m_mthAddSign("����",m_objText1.m_ObjModifyUserArr);
						
						strGroup1 =objLine12[0].ToString();
						strGroup2 =objLine12[1].ToString();
						strGroup3 =objLine12[2].ToString();
						strGroup4 =objLine12[3].ToString();
						strGroup5 =objLine12[4].ToString();
						strGroup6 =objLine12[5].ToString();
						strGroup7 =objLine12[6].ToString();
						strGroup8 =objLine12[7].ToString();                        						
					}
				}
			}
		}
	

		private class clsPrintLine13 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private clsPrintRichTextContext m_objText2;
			private clsPrintRichTextContext m_objText3;
			private string strGroup1;
			private DateTime dtmFirstPrint;
			private string strGroup2;
			private bool m_blnPrintFirst=true;

			private clsPublicControlPaint m_objCPaint;
			
			
			public clsPrintLine13()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",10));				
				m_objText2 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",10));
				m_objText3 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",10));
				m_objCPaint=new clsPublicControlPaint ();
			
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);

				if(m_blnPrintFirst==true)
				{
					p_objGrp.DrawString("�ű㣺",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					p_objGrp.DrawString("������",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+40,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+85,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+105,p_intPosY +(int)enmRectangleInfo.RowStep);
					p_objGrp.DrawString("��/�գ�1��/",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+110,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+215,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+235,p_intPosY +(int)enmRectangleInfo.RowStep);
					
					p_objGrp.DrawString("�գ���",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+240,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+300-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("ʧ��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+300,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+360-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("�����ڣ���",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+360,p_intPosY);
					p_objGrp.DrawString("�����״��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+420,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+505,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.RightX-10,p_intPosY +(int)enmRectangleInfo.RowStep);
					
					if(strGroup1 =="True" || strGroup1=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+300-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup2=="True" || strGroup2=="1")//��ѡ,������else if.
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+360-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					m_blnPrintFirst =false;
				}

				int intRealHeight;
				Rectangle rtgBlock1 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+85,
					p_intPosY-10,
					30,
					30);
				m_objText1.m_blnPrintAllBySimSun(10,rtgBlock1,p_objGrp,out intRealHeight,true);

				Rectangle rtgBlock2 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+215,
					p_intPosY-10,
					30,
					30);
				m_objText2.m_blnPrintAllBySimSun(10,rtgBlock2,p_objGrp,out intRealHeight,true);

				Rectangle rtgBlock3 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+505,
					p_intPosY-10,
					(int)enmRectangleInfo.RightX-((int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+500),
					30);
				m_objText3.m_blnPrintAllBySimSun(10,rtgBlock3,p_objGrp,out intRealHeight,true);

				m_blnHaveMoreLine = false;
				p_intPosY += (int)enmRectangleInfo.RowStep+15;
				m_objHandlePartEnd(p_intPosY,p_objGrp,p_fntNormalText);
				
				fntText.Dispose();
				fntCheck.Dispose();
			}
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_blnPrintFirst=true;
				m_objText1.m_mthRestartPrint();
				m_objText2.m_mthRestartPrint();
				m_objText3.m_mthRestartPrint();
			
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
						Object[] objLine13=(Object[])value ;
						dtmFirstPrint=(DateTime)objLine13[8];
						//						m_objText1.m_mthSetContextWithAllCorrect("asdfasdfasdfasdfsadfasdfasdfasdfasdfasdfasdffdsf","");


						m_objText1.m_mthSetContextWithCorrectBefore(objLine13[0].ToString() ,objLine13[1].ToString(),dtmFirstPrint,true);
						m_mthAddSign("�ű������",m_objText1.m_ObjModifyUserArr);
						m_objText2.m_mthSetContextWithCorrectBefore(objLine13[2].ToString() ,objLine13[3].ToString(),dtmFirstPrint,true);
						m_mthAddSign("�ű�1��/�գ�",m_objText2.m_ObjModifyUserArr);
						m_objText3.m_mthSetContextWithCorrectBefore(objLine13[6].ToString() ,objLine13[7].ToString(),dtmFirstPrint,true);
						m_mthAddSign("�����״��",m_objText3.m_ObjModifyUserArr);
					
						strGroup1 =objLine13[4].ToString();

						strGroup2= objLine13[5].ToString();
						

                        						
					}
				}
			}
		}
	
		
		private class clsPrintLine14 : clsPrintLineBase
		{
			private string strGroup1; 
			private DateTime dtmFirstPrint;
			private string strGroup2;
			private clsPublicControlPaint m_objCPaint;
			public clsPrintLine14()
			{
				m_objCPaint=new clsPublicControlPaint ();
			
			}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);
				
				p_objGrp.DrawString("����������",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("�����չ�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+100,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+200-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("��Э��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+200,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+280-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("��ȫ����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+280,p_intPosY);
				p_objGrp.DrawString("������٣�",fntText,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+380,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+500-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+500,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+540-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+540,p_intPosY);
				

				switch(strGroup1)
				{
					case "0":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
					case "1":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+200-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
						break;
					case "2":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+280-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
				}

				switch(strGroup2)
				{
					case "0": case "False":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+500-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
					case "1": case "True":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+540-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
				}
			
				m_blnHaveMoreLine = false;
			

				p_intPosY += (int)enmRectangleInfo.RowStep+10;

				fntText.Dispose();
				fntCheck.Dispose();
			}

			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					Object [] objData14=(object[])value;
					strGroup1 =objData14[0].ToString();
					strGroup2=objData14[1].ToString ();
					dtmFirstPrint=(DateTime)objData14[2];
									
				}
			}
		}
	
		
		private class clsPrintLine15 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
		
			private string strGroup1;
			private DateTime dtmFirstPrint;
			private string strGroup2;
			private bool m_blnPrintFirst=true;
			private bool m_blnLimbActive_Not = false;

			private clsPublicControlPaint m_objCPaint;
			
			
			public clsPrintLine15()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",12));
				m_objCPaint=new clsPublicControlPaint ();
			
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);

				if(m_blnPrintFirst==true)
				{
					p_objGrp.DrawString("��֫���",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+100,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+160-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+160,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+220-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("ƫ̱",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+220,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+280-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��̱",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+280,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+340-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("ȫ̱��̱����λ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+340,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+465,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+650,p_intPosY +(int)enmRectangleInfo.RowStep);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+660,p_intPosY);
			
					if(strGroup1=="True" || strGroup1=="1")//ֻҪ��CheckBox,�����ݿ��д�ŵ���һ���������ֶ�,"True"��ʾѡ��
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					else if(strGroup1=="False" || strGroup1=="0")//Ϊ�˴�ӡ�հױ���,������elseֱ�Ӵ���,��ͬ
					{
						if(m_blnLimbActive_Not)
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+160-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						switch (strGroup2)
						{
							case "0":
								p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+220-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

								break;
							case "1":
								p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+280-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

								break;

							case "2":
								p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+340-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

								break;

						}
					}
					m_blnPrintFirst=false ;
				}

				int intRealHeight;
				Rectangle rtgBlock1 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+465,
					p_intPosY-10,
					205,
					30);
				m_objText1.m_blnPrintAllBySimSun(10,rtgBlock1,p_objGrp,out intRealHeight,true);

				m_blnHaveMoreLine = false;
				p_intPosY += (int)enmRectangleInfo.RowStep+15;
				m_objHandlePartEnd(p_intPosY,p_objGrp,p_fntNormalText);
			
				fntText.Dispose();
				fntCheck.Dispose();
			}
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_blnPrintFirst=true;
				m_objText1.m_mthRestartPrint();			
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
						Object[] objLine15=(Object[])value ;
						dtmFirstPrint=(DateTime)objLine15[5];
						//						m_objText1.m_mthSetContextWithAllCorrect("asdfasdfasdfasdfsadfasdfasdfasdfasdfasdfasdffdsf","");


						m_objText1.m_mthSetContextWithCorrectBefore(objLine15[2].ToString() ,objLine15[3].ToString(),dtmFirstPrint,true);
						m_mthAddSign("̱����λ��",m_objText1.m_ObjModifyUserArr);
						strGroup1 =objLine15[0].ToString();
						strGroup2=objLine15 [1].ToString();
						if(objLine15[4] != null && objLine15[4].ToString() != "") 
							m_blnLimbActive_Not = (bool)objLine15[4];
					}
				}
			}
		}
	

		private class clsPrintLine16 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private clsPrintRichTextContext m_objText2;
			private string strGroup1; 
			private DateTime dtmFirstPrint;
			private string strGroup2;
			private string strGroup3;
			private string strGroup4;
			private string strGroup5;
			private string strGroup6;
			private bool m_blnPrintFirst=true;
		

			private clsPublicControlPaint m_objCPaint;
			
			
			public clsPrintLine16()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",12));
				m_objText2 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",12));
				m_objCPaint=new clsPublicControlPaint ();
			
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY += 10;

				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);

				if(m_blnPrintFirst==true)
				{
					p_objGrp.DrawString("˯    �ߣ�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					p_objGrp.DrawString("ÿ��˯��ʱ�乲��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+80,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+210,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+240,p_intPosY +(int)enmRectangleInfo.RowStep);
					
					p_objGrp.DrawString("Сʱ��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+250,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+310-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+310,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+350-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����˯",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+350,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+430-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+430,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+500-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+500,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+560-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+560,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+620-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("������",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+620,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+670,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.RightX-10,p_intPosY +(int)enmRectangleInfo.RowStep);
					
					if(strGroup1=="True" || strGroup1=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+310-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup2=="True" || strGroup2=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+350-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup3=="True" || strGroup3=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+430-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup4=="True" || strGroup4=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+500-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup5=="True" || strGroup5=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+560-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup6 =="True" || strGroup6=="1")

						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+620-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					m_blnPrintFirst =false;


				}
				int intRealHeight;
				Rectangle rtgBlock1 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+210,
					p_intPosY-10,
					30,
					30);
				m_objText1.m_blnPrintAllBySimSun(10,rtgBlock1,p_objGrp,out intRealHeight,true);

				Rectangle rtgBlock2 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+670,
					p_intPosY-10,
					60,
					30);
				m_objText2.m_blnPrintAllBySimSun(10,rtgBlock2,p_objGrp,out intRealHeight,true);

				m_blnHaveMoreLine = false;
				p_intPosY += (int)enmRectangleInfo.RowStep+10;

				fntText.Dispose();
				fntCheck.Dispose();
			}
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_blnPrintFirst=true;
				m_objText1.m_mthRestartPrint();
				m_objText2.m_mthRestartPrint();			
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
						Object[] objLine16=(Object[])value ;
						dtmFirstPrint=(DateTime)objLine16[10];
						m_objText1.m_mthSetContextWithCorrectBefore(objLine16[0].ToString() ,objLine16[1].ToString(),dtmFirstPrint,true);
						m_mthAddSign("˯��ʱ�䣺",m_objText1.m_ObjModifyUserArr);

						strGroup1=objLine16[2].ToString() ;
						strGroup2=objLine16[3].ToString() ;
						strGroup3=objLine16[4].ToString ();
						strGroup4=objLine16[5].ToString() ;
						strGroup5=objLine16[6].ToString() ;
						strGroup6=objLine16[7].ToString() ;
						
						m_objText2.m_mthSetContextWithCorrectBefore(objLine16[8].ToString() ,objLine16[9].ToString(),dtmFirstPrint,true);
						m_mthAddSign("˯��������",m_objText2.m_ObjModifyUserArr);
          						
					}
				}
			}
		}
	

		private class clsPrintLine17 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private clsPrintRichTextContext m_objText2;
			private string strGroup1;
			private DateTime dtmFirstPrint;
			private bool m_blnFirstPrint=true;


			private clsPublicControlPaint m_objCPaint;
			
			
			public clsPrintLine17()
			{
				m_objText1 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",12));
				m_objText2 = new clsPrintRichTextContext(Color.Black,new Font("SimSun",12));
				m_objCPaint=new clsPublicControlPaint ();
			
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);
				if(m_blnFirstPrint ==true)
				{
					p_objGrp.DrawString("����˯�ߣ�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+100,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+140-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("ҩ��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+140,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+180,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+350-(int)enmRectangleInfo.CheckShift-10,p_intPosY +(int)enmRectangleInfo.RowStep);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+350-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("���� ���߷���",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+350,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+470,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.RightX-10,p_intPosY +(int)enmRectangleInfo.RowStep);
					
					switch(strGroup1)
					{
						case "0":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

							break;

						case "1":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+140-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

							break;
						case "2":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+350-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

							break;
					}

					m_blnFirstPrint=false;
				}

				int intRealHeight;
				Rectangle rtgBlock1 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+180,
					p_intPosY-10,
					160,
					30);
				m_objText1.m_blnPrintAllBySimSun(10,rtgBlock1,p_objGrp,out intRealHeight,true);

				Rectangle rtgBlock2 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+470,
					p_intPosY-10,
					275,
					30);
				m_objText2.m_blnPrintAllBySimSun(10,rtgBlock2,p_objGrp,out intRealHeight,true);

				m_blnHaveMoreLine = false;
				p_intPosY += (int)enmRectangleInfo.RowStep+15;
				m_objHandlePartEnd(p_intPosY,p_objGrp,p_fntNormalText);
				
				fntText.Dispose();
				fntCheck.Dispose();
			}
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_blnFirstPrint=true;
				m_objText1.m_mthRestartPrint();
				m_objText2.m_mthRestartPrint();
			
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
						Object [] objData17=(object[])value;
						strGroup1=objData17[0].ToString();
						dtmFirstPrint=(DateTime)objData17[5];
						//						m_objText1.m_mthSetContextWithAllCorrect("asdfasdfasdfasdfsadfasdfasdfasdfasdfasdfasdffdsf","");
						m_objText1.m_mthSetContextWithCorrectBefore(objData17[1].ToString(),objData17[2].ToString(),dtmFirstPrint,true);
						m_mthAddSign("����˯��ҩ�",m_objText1.m_ObjModifyUserArr);

						m_objText2.m_mthSetContextWithCorrectBefore(objData17[3].ToString(),objData17[4].ToString(),dtmFirstPrint,true);
						m_mthAddSign("���߷�����",m_objText2.m_ObjModifyUserArr);

		
                        						
					}
				}
			}
		}
	

		private class clsPrintLine18 : clsPrintLineBase
		{
			private string strGroup1; 
			private DateTime dtmFirstPrint;
			private string strGroup2;
			private clsPublicControlPaint m_objCPaint;
			public clsPrintLine18()
			{
				m_objCPaint=new clsPublicControlPaint ();
			
			}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY += 5;
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);

				p_objGrp.DrawString("��ʶ״̬�� ��֮",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+180-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("��Ӧ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+180,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+240-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("��Ӧ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+240,p_intPosY);
				p_objGrp.DrawString("�Դ�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+300,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+360-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+360,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+420-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("������",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+420,p_intPosY);
			
				switch(strGroup1)
				{
					case "0": case "False":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+180-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
					case "1": case "True":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+240-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
				}

				switch(strGroup2)
				{
					case "0": case "False":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+360-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
					case "1": case "True":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+420-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
				}
				m_blnHaveMoreLine = false;			

				p_intPosY += (int)enmRectangleInfo.RowStep+10;

				fntText.Dispose();
				fntCheck.Dispose();
			}

			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					Object [] objData27=(object[])value;
					strGroup1 =objData27[0].ToString();
					strGroup2=objData27[1].ToString ();
					dtmFirstPrint=(DateTime)objData27[2];
									
				}
			}
		}
	

		private class clsPrintLine19 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private string strGroup1;
			private DateTime dtmFirstPrint;
			private string strGroup2;
			private string strGroup3;
			private string strGroup4;
			private bool m_blnFirstPrint = true;
			private clsPublicControlPaint m_objCPaint;
			public clsPrintLine19()
			{
				m_objCPaint=new clsPublicControlPaint ();
				m_objText1=new clsPrintRichTextContext (Color.Black,new Font("SimSun",12));
			
			}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);

				if(m_blnFirstPrint==true)
				{
				
					p_objGrp.DrawString("��: ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+180,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+280-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("�����½�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+280,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+380-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("ģ��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+380,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+440-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��Ӱ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+440,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+500-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+500,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+540,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.RightX-50,p_intPosY +(int)enmRectangleInfo.RowStep);
					
					if(strGroup1=="True" || strGroup1=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+280-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup2=="True" || strGroup2=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+380-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup3=="True" || strGroup3=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+440-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup4=="True" || strGroup4=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+500-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
					m_blnFirstPrint =false;
		

				}
				int intRealHeight;
				Rectangle rtgBlock1 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+540,
					p_intPosY-5,
					180,
					30);
				m_objText1.m_blnPrintAllBySimSun(10,rtgBlock1,p_objGrp,out intRealHeight,true);

				m_blnHaveMoreLine = false;
				p_intPosY += (int)enmRectangleInfo.RowStep+5;

				fntText.Dispose();
				fntCheck.Dispose();
			}
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_blnFirstPrint = true;
				m_objText1.m_mthRestartPrint();
			
			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					Object[] objData=(Object[])value ;
					dtmFirstPrint=(DateTime)objData[6];
					m_objText1.m_mthSetContextWithCorrectBefore(objData[4].ToString(),objData[5].ToString(),dtmFirstPrint,true);
					m_mthAddSign("�����ϰ� ��",m_objText1.m_ObjModifyUserArr);
					strGroup1=objData[0].ToString();
					strGroup2=objData[1].ToString();
					strGroup3=objData[2].ToString();
					strGroup4=objData[3].ToString();
					
									
				}
			}
		}
	

		private class clsPrintLine20 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private string strGroup0;
			private string strGroup1; 
			private DateTime dtmFirstPrint;
			private string strGroup2;
			private string strGroup3;
			private string strGroup4;
			private bool m_blnFirstPrint = true;
			private clsPublicControlPaint m_objCPaint;
			public clsPrintLine20()
			{
				m_objCPaint=new clsPublicControlPaint ();
				m_objText1=new clsPrintRichTextContext (Color.Black,new Font("SimSun",12));
			
			}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);

				if(m_blnFirstPrint==true)
				{
				
					p_objGrp.DrawString("�����ϰ���",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+100,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+140-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("�ǣ�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+140,p_intPosY);
					p_objGrp.DrawString("��: ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+180,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+280-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("�����½�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+280,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+380-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("ģ��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+380,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+440-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��Ӱ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+440,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+500-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+500,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+540,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.RightX-50,p_intPosY +(int)enmRectangleInfo.RowStep);

					if(strGroup0=="False" || strGroup0=="0")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
					else if(strGroup0=="True" || strGroup0=="1")//Ϊ�˴�ӡ�հױ���,������elseֱ�Ӵ���,��ͬ
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+140-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);


					if(strGroup1=="True" || strGroup1=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+280-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup2=="True" || strGroup2=="1")//��ѡ,������else if.
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+380-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup3=="True" || strGroup3=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+440-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup4=="True" || strGroup4=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+500-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					m_blnFirstPrint =false;
				}
				int intRealHeight;
				Rectangle rtgBlock1 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+540,
					p_intPosY-5,
					180,
					30);
				m_objText1.m_blnPrintAllBySimSun(10,rtgBlock1,p_objGrp,out intRealHeight,true);

				m_blnHaveMoreLine = false;
				p_intPosY += (int)enmRectangleInfo.RowStep+5;

				fntText.Dispose();
				fntCheck.Dispose();
			}
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_blnFirstPrint = true;
				m_objText1.m_mthRestartPrint();
				
			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					Object[] objData=(Object[])value ;
					dtmFirstPrint=(DateTime)objData[7];
					m_objText1.m_mthSetContextWithCorrectBefore(objData[4].ToString(),objData[5].ToString(),dtmFirstPrint,true);
					m_mthAddSign("�����ϰ� �ң�",m_objText1.m_ObjModifyUserArr);
					strGroup1=objData[0].ToString();
					strGroup2=objData[1].ToString();
					strGroup3=objData[2].ToString();
					strGroup4=objData[3].ToString();
					strGroup0=objData[6].ToString();
					
									
				}
			}
		}
	
		
		private class clsPrintLine21 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private string strGroup1;
			private DateTime dtmFirstPrint;
			private string strGroup2;
			private string strGroup3;
			private string strGroup4;
			private bool m_blnFirstPrint = true;
			private clsPublicControlPaint m_objCPaint;
			public clsPrintLine21()
			{
				m_objCPaint=new clsPublicControlPaint ();
				m_objText1=new clsPrintRichTextContext (Color.Black,new Font("SimSun",12));
			
			}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);

				if(m_blnFirstPrint==true)
				{
					p_objGrp.DrawString("˫��: ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+180,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+280-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("�����½�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+280,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+380-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("ģ��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+380,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+440-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��Ӱ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+440,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+500-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+500,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+540,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.RightX-50,p_intPosY +(int)enmRectangleInfo.RowStep);

					if(strGroup1=="True" || strGroup1=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+280-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup2=="True" || strGroup2=="1")//��ѡ,������else if.
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+380-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup3=="True" || strGroup3=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+440-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup4=="True" || strGroup4=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+500-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
					m_blnFirstPrint =false;
		

				}
				int intRealHeight;
				Rectangle rtgBlock1 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+540,
					p_intPosY-5,
					180,
					30);
				m_objText1.m_blnPrintAllBySimSun(10,rtgBlock1,p_objGrp,out intRealHeight,true);

				m_blnHaveMoreLine = false;
				p_intPosY += (int)enmRectangleInfo.RowStep+10;

				fntText.Dispose();
				fntCheck.Dispose();
			}
			
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_blnFirstPrint = true;
				m_objText1.m_mthRestartPrint();
			
			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					Object[] objData=(Object[])value ;
					dtmFirstPrint=(DateTime)objData[6];
					m_objText1.m_mthSetContextWithCorrectBefore(objData[4].ToString(),objData[5].ToString(),dtmFirstPrint,true);
					m_mthAddSign("�����ϰ� ˫�ࣺ",m_objText1.m_ObjModifyUserArr);
					strGroup1=objData[0].ToString();
					strGroup2=objData[1].ToString();
					strGroup3=objData[2].ToString();
					strGroup4=objData[3].ToString();
					
									
				}
			}
		}
	
		
		private class clsPrintLine22 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private string strGroup1; 
			private DateTime dtmFirstPrint;
			private string strGroup2;
			private string strGroup3;
			private string strGroup4;
			private bool m_blnFirstPrint = true;
			private clsPublicControlPaint m_objCPaint;
			public clsPrintLine22()
			{
				m_objCPaint=new clsPublicControlPaint ();
				m_objText1=new clsPrintRichTextContext (Color.Black,new Font("SimSun",12));
			
			}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);

				if(m_blnFirstPrint==true)
				{
				
					p_objGrp.DrawString("��: ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+180,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+280-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("�����½�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+280,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+380-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+380,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+440-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+440,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+500-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+500,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+540,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.RightX-50,p_intPosY +(int)enmRectangleInfo.RowStep);
			
					if(strGroup1=="True" || strGroup1=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+280-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup2=="True" || strGroup2=="1")//��ѡ,������else if.
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+380-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup3=="True" || strGroup3=="1")
						p_objGrp.DrawString("��",fntCheck,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+440-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup4=="True" || strGroup4=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+500-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
				
					m_blnFirstPrint=false;
				}
				int intRealHeight;
				Rectangle rtgBlock1 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+540,
					p_intPosY-5,
					180,
					30);
				m_objText1.m_blnPrintAllBySimSun(10,rtgBlock1,p_objGrp,out intRealHeight,true);

				m_blnHaveMoreLine = false;
				p_intPosY += (int)enmRectangleInfo.RowStep+5;

				fntText.Dispose();
				fntCheck.Dispose();
			}

			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_blnFirstPrint = true;
				m_objText1.m_mthRestartPrint();
				
			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					Object[] objData=(Object[])value ;
					dtmFirstPrint=(DateTime)objData[6];
					m_objText1.m_mthSetContextWithCorrectBefore(objData[4].ToString(),objData[5].ToString(),dtmFirstPrint,true);
					m_mthAddSign("�����ϰ� ��",m_objText1.m_ObjModifyUserArr);
					strGroup1=objData[0].ToString();
					strGroup2=objData[1].ToString();
					strGroup3=objData[2].ToString();
					strGroup4=objData[3].ToString();
					
									
				}
			}
		}
	

		private class clsPrintLine23 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private string strGroup0;
			private string strGroup1;
			private DateTime dtmFirstPrint;
			private string strGroup2;
			private string strGroup3;
			private string strGroup4;
			private bool m_blnFirstPrint = true;

			private clsPublicControlPaint m_objCPaint;
			public clsPrintLine23()
			{
				m_objCPaint=new clsPublicControlPaint ();
				m_objText1=new clsPrintRichTextContext (Color.Black,new Font("SimSun",12));
			
			}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);

				if(m_blnFirstPrint ==true)
				{
				
					p_objGrp.DrawString("�����ϰ���",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+100,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+140-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("�ǣ�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+140,p_intPosY);
					p_objGrp.DrawString("��: ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+180,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+280-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("�����½�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+280,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+380-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+380,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+440-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+440,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+500-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+500,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+540,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.RightX-50,p_intPosY +(int)enmRectangleInfo.RowStep);
					
					if(strGroup0=="False" || strGroup0=="0")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
					else if(strGroup0=="True" || strGroup0=="1")//Ϊ�˴�ӡ�հױ���,������elseֱ�Ӵ���,��ͬ
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+140-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);


					if(strGroup1=="True" || strGroup1=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+280-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup2=="True" || strGroup2=="1")//��ѡ,������else if.
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+380-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup3=="True" || strGroup3=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+440-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup4=="True" || strGroup4=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+500-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					m_blnFirstPrint =false;
				}
				int intRealHeight;
				Rectangle rtgBlock1 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+540,
					p_intPosY-5,
					180,
					30);
				m_objText1.m_blnPrintAllBySimSun(10,rtgBlock1,p_objGrp,out intRealHeight,true);

				m_blnHaveMoreLine = false;
				p_intPosY += (int)enmRectangleInfo.RowStep+5;

				fntText.Dispose();
				fntCheck.Dispose();
			}
			
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_blnFirstPrint = true;
				m_objText1.m_mthRestartPrint();
				
			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					Object[] objData=(Object[])value ;
					dtmFirstPrint=(DateTime)objData[7];
					m_objText1.m_mthSetContextWithCorrectBefore(objData[4].ToString(),objData[5].ToString(),dtmFirstPrint,true);
					m_mthAddSign("�����ϰ� �ң�",m_objText1.m_ObjModifyUserArr);
					strGroup1=objData[0].ToString();
					strGroup2=objData[1].ToString();
					strGroup3=objData[2].ToString();
					strGroup4=objData[3].ToString();
					strGroup0=objData[6].ToString();
					

									
				}
			}
		}
	

		private class clsPrintLine24 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private string strGroup1;
			private DateTime dtmFirstPrint;
			private string strGroup2;
			private string strGroup3;
			private string strGroup4;
			private bool m_blnFirstPrint = true;

			private clsPublicControlPaint m_objCPaint;
			public clsPrintLine24()
			{
				m_objCPaint=new clsPublicControlPaint ();
				m_objText1=new clsPrintRichTextContext (Color.Black,new Font("SimSun",12));
			
			}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);

				if(m_blnFirstPrint==true)
				{				
					p_objGrp.DrawString("˫��: ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+180,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+280-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("�����½�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+280,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+380-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+380,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+440-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+440,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+500-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+500,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+540,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.RightX-50,p_intPosY +(int)enmRectangleInfo.RowStep);
					
					if(strGroup1=="True" || strGroup1=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+280-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup2=="True" || strGroup2=="1")//��ѡ,������else if.
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+380-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup3=="True" || strGroup3=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+440-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					if(strGroup4=="True" || strGroup4=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+500-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
					m_blnFirstPrint =false;
		

				}
				int intRealHeight;
				Rectangle rtgBlock1 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+540,
					p_intPosY-5,
					180,
					30);
				m_objText1.m_blnPrintAllBySimSun(10,rtgBlock1,p_objGrp,out intRealHeight,true);

				m_blnHaveMoreLine = false;
				p_intPosY += (int)enmRectangleInfo.RowStep+10;

				fntText.Dispose();
				fntCheck.Dispose();
			}
			
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_blnFirstPrint = true;
				m_objText1.m_mthRestartPrint();
				
			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					Object[] objData=(Object[])value ;
					dtmFirstPrint=(DateTime)objData[6];
					m_objText1.m_mthSetContextWithCorrectBefore(objData[4].ToString(),objData[5].ToString(),dtmFirstPrint,true);
					m_mthAddSign("�����ϰ� ˫�ࣺ",m_objText1.m_ObjModifyUserArr);
					strGroup1=objData[0].ToString();
					strGroup2=objData[1].ToString();
					strGroup3=objData[2].ToString();
					strGroup4=objData[3].ToString();
					
									
				}
			}
		}
	

		private class clsPrintLine25 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private string strGroup1;
			private DateTime dtmFirstPrint;
			private string strGroup2;
			private bool m_blnFirstPrint = true;
			private clsPublicControlPaint m_objCPaint;
			public clsPrintLine25()
			{
				m_objCPaint=new clsPublicControlPaint ();
				m_objText1=new clsPrintRichTextContext (Color.Black,new Font("SimSun",12));
			
			}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);

				if(m_blnFirstPrint==true)
				{				
					p_objGrp.DrawString("��ʹ��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+60-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+60,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("�У�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+100,p_intPosY);
					p_objGrp.DrawString("��λ:",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+130,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+180,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+400-(int)enmRectangleInfo.CheckShift-10,p_intPosY +(int)enmRectangleInfo.RowStep);

					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+400-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+400,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+460-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��Ъ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+460,p_intPosY);

					if(strGroup1 =="False" || strGroup1=="0")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+60-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
					else if(strGroup1=="True" || strGroup1=="1")//Ϊ�˴�ӡ�հױ���,������elseֱ�Ӵ���,��ͬ
					{
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						if(strGroup2=="False" || strGroup2=="0")
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+400-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
						else if(strGroup2=="True" || strGroup2=="1")//Ϊ�˴�ӡ�հױ���,������elseֱ�Ӵ���,��ͬ
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+460-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
					}
					m_blnFirstPrint =false;

				}				
					
				int intRealHeight;
				Rectangle rtgBlock1 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+180,
					p_intPosY-10,
					200,
					30);
				m_objText1.m_blnPrintAllBySimSun(10,rtgBlock1,p_objGrp,out intRealHeight,true);

				m_blnHaveMoreLine = false;
				p_intPosY += (int)enmRectangleInfo.RowStep+15;
				m_objHandlePartEnd(p_intPosY,p_objGrp,p_fntNormalText);

				fntText.Dispose();
				fntCheck.Dispose();
			}
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_blnFirstPrint = true;
				m_objText1.m_mthRestartPrint();			
			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					
					Object[] objData=(Object[])value ;
					dtmFirstPrint=(DateTime)objData[4];
					m_objText1.m_mthSetContextWithCorrectBefore(objData[1].ToString(),objData[2].ToString(),dtmFirstPrint,true);
					m_mthAddSign("��ʹ��λ��",m_objText1.m_ObjModifyUserArr);
					strGroup1=objData[0].ToString();
					strGroup2=objData[3].ToString();
					


									
				}
			}
		}
	

		private class clsPrintLine26 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private string strGroup1;
			private DateTime dtmFirstPrint;
			private string strGroup2;
			private bool m_blnFirstPrint = true;
			private clsPublicControlPaint m_objCPaint;
			public clsPrintLine26()
			{
				m_objCPaint=new clsPublicControlPaint ();
				m_objText1=new clsPrintRichTextContext (Color.Black,new Font("SimSun",12));
			
			}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);

				if(m_blnFirstPrint==true)
				{				
					p_objGrp.DrawString("���Թ�ͨ���������",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+180,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+330,p_intPosY +(int)enmRectangleInfo.RowStep);
					p_objGrp.DrawString(",��ͨ����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+340,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+430-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+430,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+490-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��������",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+490,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+580-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��ȫ����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+580,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+670-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("ʧ��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+670,p_intPosY);

					switch(strGroup1)
					{
						case "0":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+430-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

							break;
						case "1":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+490-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

							break;
						case "2":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+580-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

							break;

					}
					if(strGroup2=="True" || strGroup2=="1")
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+670-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

					m_blnFirstPrint =false;
				}

				int intRealHeight;
				Rectangle rtgBlock1 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+180,
					p_intPosY-5,
					150,
					30);
				m_objText1.m_blnPrintAllBySimSun(10,rtgBlock1,p_objGrp,out intRealHeight,true);

				m_blnHaveMoreLine = false;
				p_intPosY += (int)enmRectangleInfo.RowStep+5;

				fntText.Dispose();
				fntCheck.Dispose();
			}
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_blnFirstPrint = true;
				m_objText1.m_mthRestartPrint();			
			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					Object[] objData=(Object[])value ;
					dtmFirstPrint=(DateTime)objData[4];
					m_objText1.m_mthSetContextWithCorrectBefore(objData[0].ToString(),objData[1].ToString(),dtmFirstPrint,true);
					m_mthAddSign("������ԣ�",m_objText1.m_ObjModifyUserArr);
					strGroup1=objData[2].ToString();
					strGroup2=objData[3].ToString();
					

									
				}
			}
		}
	

		private class clsPrintLine27 : clsPrintLineBase
		{
			private string strGroup1;
			private DateTime dtmFirstPrint;
			private string strGroup2;
			private clsPublicControlPaint m_objCPaint;
			public clsPrintLine27()
			{
				m_objCPaint=new clsPublicControlPaint ();
			
			}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);
				
				p_objGrp.DrawString("��סԺ�о���",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+120-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+120,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+180-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+180,p_intPosY);
				p_objGrp.DrawString("ϣ�����Ѻͼ�����",new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+220,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+370-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("��̽��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+370,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+450-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("��̽��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+450,p_intPosY);
				switch(strGroup1)
				{
					case "0": case "False":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+120-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
					case "1": case "True":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+180-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
				}

				switch(strGroup2)
				{
					case "0": case "False":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+370-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
					case "1": case "True":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+450-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;
				}

				m_blnHaveMoreLine = false;
			

				p_intPosY += (int)enmRectangleInfo.RowStep+5;

				fntText.Dispose();
				fntCheck.Dispose();

			}

			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					Object [] objData27=(object[])value;
					strGroup1 =objData27[0].ToString();
					strGroup2=objData27[1].ToString ();
					dtmFirstPrint=(DateTime)objData27[2];
						
									
				}
			}
		}
	

		private class clsPrintLine28 : clsPrintLineBase
		{
			private string strGroup1; 
			private DateTime dtmFirstPrint;
			private clsPublicControlPaint m_objCPaint;
			public clsPrintLine28()
			{
				m_objCPaint=new clsPublicControlPaint ();
			
			}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);
				
				p_objGrp.DrawString("��ͥ�ľ�������֧�֣�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+180-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+180,p_intPosY);
				System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+240-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
				p_objGrp.DrawString("������",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+240,p_intPosY);
			    
				switch(strGroup1 )
				{
					case "0": case "False":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+180-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break ;

					case "1": case "True":
						p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+240-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

						break;

				}

				m_blnHaveMoreLine = false;			

				p_intPosY += (int)enmRectangleInfo.RowStep+15;
				m_objHandlePartEnd(p_intPosY,p_objGrp,p_fntNormalText);

				fntText.Dispose();
				fntCheck.Dispose();
			}

			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					Object [] objData=(object[])value;
					strGroup1 =objData[0].ToString();
					dtmFirstPrint=(DateTime)objData[1];
					
				}
			}
		}
	

		private class clsPrintLine29 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private string strGroup1;
			private DateTime dtmFirstPrint;
			private string strGroup2;
			private bool m_blnFirstPrint = true;

			private clsPublicControlPaint m_objCPaint;
			public clsPrintLine29()
			{
				m_objCPaint=new clsPublicControlPaint ();
				m_objText1=new clsPrintRichTextContext (Color.Black,new Font("SimSun",12));
							
			}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY += 10;
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);

				if(m_blnFirstPrint==true)
				{
					p_objGrp.DrawString("�Լ�������ʶ��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+130-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("�����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+130,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+210-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("�������",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+210,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+300-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��ȫ���",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+300,p_intPosY);
					p_objGrp.DrawString("��ѭҽ���򽡿�ָ����",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+390,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+570-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+570,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+610-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+610,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+630,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.RightX-10,p_intPosY +(int)enmRectangleInfo.RowStep);

					switch(strGroup1)
					{
						case "0":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+130-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

							break;
						case "1":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+210-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

							break;
						case "2":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+300-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
							break;


					}
					switch(strGroup2)
					{
						case "0": case "False":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+570-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
							break;
						case "1": case "True":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+610-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
							break;

					}
					m_blnFirstPrint =false;

				}
				int intRealHeight;
				Rectangle rtgBlock1 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+630,
					p_intPosY-12,
					100,
					30);
				m_objText1.m_blnPrintAllBySimSun(10,rtgBlock1,p_objGrp,out intRealHeight,true);

				m_blnHaveMoreLine = false;
				p_intPosY += (int)enmRectangleInfo.RowStep+10;

				fntText.Dispose();
				fntCheck.Dispose();

			}
			
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_blnFirstPrint = true;
				m_objText1.m_mthRestartPrint();				
			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					Object[] objData=(Object[])value ;
					dtmFirstPrint=(DateTime)objData[4];
					m_objText1.m_mthSetContextWithCorrectBefore(objData[2].ToString(),objData[3].ToString(),dtmFirstPrint,true);
					m_mthAddSign("��ѭҽ���򽡿�ָ����",m_objText1.m_ObjModifyUserArr);
					strGroup1=objData[0].ToString();
					strGroup2=objData[1].ToString();
					
									
				}
			}
		}
	

		private class clsPrintLine30 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private clsPrintRichTextContext m_objText2;
			private clsPrintRichTextContext m_objText3;
			private clsPrintRichTextContext m_objText4;
			private string strGroup1; 
			private DateTime dtmFirstPrint;
			private string strGroup2;
			private bool m_blnFirstPrint = true;

			private clsPublicControlPaint m_objCPaint;
			public clsPrintLine30()
			{
				m_objCPaint=new clsPublicControlPaint ();
				m_objText1=new clsPrintRichTextContext (Color.Black,new Font("SimSun",10));
				m_objText2=new clsPrintRichTextContext (Color.Black,new Font("SimSun",10));
				m_objText3=new clsPrintRichTextContext (Color.Black,new Font("SimSun",10));
				m_objText4=new clsPrintRichTextContext (Color.Black,new Font("SimSun",10));
			
			}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);
				if(m_blnFirstPrint==true)
				{
				
					p_objGrp.DrawString("���̣�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+60-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+60,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+100,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+120,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+150,p_intPosY +(int)enmRectangleInfo.RowStep);
					p_objGrp.DrawString("��",fntText,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+160,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+180,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+210,p_intPosY +(int)enmRectangleInfo.RowStep);
					p_objGrp.DrawString("֧/�գ�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+220,p_intPosY);
					p_objGrp.DrawString("�Ⱦƣ�",fntText,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+270,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+330-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+330,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+370-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+370,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+390,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+420,p_intPosY +(int)enmRectangleInfo.RowStep);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+430,p_intPosY);

					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+450,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+480,p_intPosY +(int)enmRectangleInfo.RowStep);
					p_objGrp.DrawString("��/�գ�",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+490,p_intPosY);
					switch(strGroup1)
					{
						case "0": case "False":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+60-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

							break;
						case "1": case"True":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+100-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);

							break;

					}
					switch(strGroup2)
					{
						case "0": case "False":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+330-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
							break;
						case "1": case"True":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+370-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
							break;

					}
					m_blnFirstPrint =false;
				}

				m_objText1.m_mthPrintLine(40-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+120,p_intPosY,p_objGrp);
				m_objText2.m_mthPrintLine(40-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+180,p_intPosY,p_objGrp);
				m_objText3.m_mthPrintLine(40-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+390,p_intPosY,p_objGrp);
				m_objText4.m_mthPrintLine(40-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+450,p_intPosY,p_objGrp);


			
				m_blnHaveMoreLine = false;
				p_intPosY += (int)enmRectangleInfo.RowStep+10;

				fntText.Dispose();
				fntCheck.Dispose();

			}
			
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_blnFirstPrint = true;
				m_objText1.m_mthRestartPrint();
				m_objText2.m_mthRestartPrint();
				m_objText3.m_mthRestartPrint();
				m_objText4.m_mthRestartPrint();
			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					Object[] objData=(Object[])value ;
					dtmFirstPrint=(DateTime)objData[10];
					m_objText1.m_mthSetContextWithCorrectBefore(objData[1].ToString(),objData[2].ToString(),dtmFirstPrint,true);
					m_mthAddSign("���� �꣺",m_objText1.m_ObjModifyUserArr);
					m_objText2.m_mthSetContextWithCorrectBefore(objData[3].ToString(),objData[4].ToString(),dtmFirstPrint,true);
					m_mthAddSign("���� ֧/�գ�",m_objText2.m_ObjModifyUserArr);
					m_objText3.m_mthSetContextWithCorrectBefore(objData[6].ToString(),objData[7].ToString(),dtmFirstPrint,true);
					m_mthAddSign("�Ⱦ� �꣺",m_objText3.m_ObjModifyUserArr);
					m_objText4.m_mthSetContextWithCorrectBefore(objData[8].ToString(),objData[9].ToString(),dtmFirstPrint,true);
					m_mthAddSign("�Ⱦ� ��/�գ�",m_objText4.m_ObjModifyUserArr);

					strGroup1=objData[0].ToString();
					strGroup2=objData[5].ToString();
					
									
				}
			}
		}
	

		private class clsPrintLine31 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objText1;
			private clsPrintRichTextContext m_objText2;
			private clsPrintRichTextContext m_objText3;
			private string strGroup1;
			private DateTime dtmFirstPrint;

			private clsPublicControlPaint m_objCPaint;
			private bool m_blnFirstPrint = true;
			public clsPrintLine31()
			{
				m_objText1=new clsPrintRichTextContext (Color.Black,new Font("SimSun",10));
				m_objText2=new clsPrintRichTextContext (Color.Black,new Font("SimSun",10));
				m_objText3=new clsPrintRichTextContext (Color.Black,new Font("SimSun",10));
				m_objCPaint=new clsPublicControlPaint ();
			
			}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Font fntText = new Font("SimSun",12);
				Font fntCheck = new Font("SimSun",18);

				if(m_blnFirstPrint==true)
				{
								
					p_objGrp.DrawString("ҩ������/������",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+140-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+140,p_intPosY);
					System.Windows.Forms.ControlPaint.DrawCheckBox(p_objGrp,m_objCPaint.m_rtgGetControlPaint((int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+180-(int)enmRectangleInfo.CheckShift,p_intPosY),System.Windows.Forms.ButtonState.Flat);
					p_objGrp.DrawString("�� ������ ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+180,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+260,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+400,p_intPosY +(int)enmRectangleInfo.RowStep);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+420,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+450,p_intPosY +(int)enmRectangleInfo.RowStep);
					p_objGrp.DrawString("��",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+460,p_intPosY);
					p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+480,p_intPosY +(int)enmRectangleInfo.RowStep,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+590,p_intPosY +(int)enmRectangleInfo.RowStep);
					p_objGrp.DrawString("����/�գ� ",fntText ,Brushes.Black,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+600,p_intPosY);
					switch(strGroup1)
					{
						case "0": case"False":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+140-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
							break;
						case "1": case"True":
							p_objGrp.DrawString("��",fntCheck ,Brushes.Black,(int)enmRectangleInfo.LeftX +(int)enmRectangleInfo.ColumnsMark1+180-(int)enmRectangleInfo.CheckShift-10,p_intPosY-10);
							break;
					}
					m_blnFirstPrint =false;

				}

				int intRealHeight;
				Rectangle rtgBlock1 = new Rectangle(
					(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+260,
					p_intPosY-5,
					150,
					30);
				m_objText1.m_blnPrintAllBySimSun(10,rtgBlock1,p_objGrp,out intRealHeight,true);

				m_objText2.m_mthPrintLine(40-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+420,p_intPosY,p_objGrp);
				m_objText3.m_mthPrintLine(110-5,(int)enmRectangleInfo.LeftX+(int)enmRectangleInfo.ColumnsMark1+490,p_intPosY,p_objGrp);

	
				m_blnHaveMoreLine = false;
				p_intPosY += (int)enmRectangleInfo.RowStep+15;
				m_objHandlePartEnd(p_intPosY,p_objGrp,p_fntNormalText);
				
				fntText.Dispose();
				fntCheck.Dispose();

			}
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_blnFirstPrint = true;
				m_objText1.m_mthRestartPrint();
				m_objText2.m_mthRestartPrint();
				m_objText3.m_mthRestartPrint();
				

			}

			public override object m_ObjPrintLineInfo
			{
				get
				{
					return m_objPrintLineInfo;
				}
				set
				{
					Object[] objData=(Object[])value ;
					dtmFirstPrint=(DateTime)objData[7];

					m_objText1.m_mthSetContextWithCorrectBefore(objData[1].ToString(),objData[2].ToString(),dtmFirstPrint,true);
					m_mthAddSign("ҩ������/���� ���ƣ�",m_objText1.m_ObjModifyUserArr);
					m_objText2.m_mthSetContextWithCorrectBefore(objData[3].ToString(),objData[4].ToString(),dtmFirstPrint,true);
					m_mthAddSign("ҩ������/���� �꣺",m_objText2.m_ObjModifyUserArr);
					m_objText3.m_mthSetContextWithCorrectBefore(objData[5].ToString(),objData[6].ToString(),dtmFirstPrint,true);
					m_mthAddSign("ҩ������/���� ������",m_objText3.m_ObjModifyUserArr);

					strGroup1=objData[0].ToString();
					
									
				}
			}
		}
	

	
		#endregion 

		#endregion ��ӡ
//
//		/// <summary>
//		/// Σ�ػ���Ĵ�ӡ��Ϣ.
//		/// </summary>
//		[Serializable]			
//		private class clsPrintInfo_InPatientEvaluate
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
//			public string m_strOccupation;//ְҵ
//			public string m_strNationnality;//����
//			public string m_strHometown;//����
//			public string m_strMarrage;//����
//
//			public clsInPatientEvaluate_All m_objInPatientEvaluate_All;	
//			public DateTime m_dtmFirstPrintTime;
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
		//		clsInPatientEvaluatePrintTool objPrintTool;
		//		private void m_mthDemoPrint_FromDataSource()
		//		{	
		//			objPrintTool=new clsInPatientEvaluatePrintTool();
		//			objPrintTool.m_mthInitPrintTool(null);	
		//			if(m_objBaseCurrentPatient==null || this.m_trvTime.SelectedNode ==null || this.m_trvTime.SelectedNode==m_trvTime.Nodes[0])
		//				objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,DateTime.MinValue,DateTime.MinValue);
		//			else 
		//				objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,m_objBaseCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate,DateTime.MinValue);
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
		//			objPrintTool=new clsInPatientEvaluatePrintTool();
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



