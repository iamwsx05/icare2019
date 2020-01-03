using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.Utility.Controls;
using weCare.Core.Entity;
using System.Drawing;

namespace iCare
{
	/// <summary>
	///  �����������������ƣ�ǰǩ��ͬ�����ӡ������
	/// </summary>
	public class clsOperationAgreedRecordPrintTool : infPrintRecord
	{
		private bool m_blnIsFromDataSource=true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
		private bool m_blnWantInit=true;
		//�м��
        //private clsOperationAgreedRecordServ m_objRecordsDomain;
		//��ӡ�Ļ�����Ϣ �����������Ա�
		private string m_strPrintPatientName;
		private string m_strPrintPatientID;
		private DateTime m_dtRecordCreateDate;
		//����Ĵ�ӡ��Ϣ
		private clsOpraAnaSignAgree m_objRecordContent=null;
        
		
		/// <summary>
		/// ���ô�ӡ��Ϣ(�������ݿ��ȡʱҪ���ȵ���.)
		/// </summary>
		/// <param name="p_objPatient">����</param>
		/// <param name="p_dtmInPatientDate">��Ժ����</param>
		/// <param name="p_dtmOpenDate">OpenDate�������һ�δ�ӡ��μ�¼�������ͣ��粡����¼��������OpenDate</param>
		public void m_mthSetPrintInfo(clsPatient p_objPatient,DateTime p_dtmInPatientDate,DateTime p_dtmOpenDate)
		{	
			m_blnIsFromDataSource=true;//�����Ǵ����ݿ��ȡ
			m_strPrintPatientID=p_objPatient.m_StrInPatientID;
			m_strPrintPatientName=p_objPatient.m_StrName;
			m_dtRecordCreateDate=p_dtmOpenDate;
			//�Ӵ���Ĳ��˶����ȡ���˵Ļ�����ӡ��Ϣ������m_objprintinfo����
			//			clsPatient m_objPatient=p_objPatient;
			//			m_objPrintInfo=new clsPrintInfo_OutHospital();
			//			m_objPrintInfo.m_strInPatentID=m_objPatient!=null? m_objPatient.m_StrInPatientID:"";					
			//			m_objPrintInfo.m_strPatientName=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrFirstName :"";
			//			m_objPrintInfo. m_strSex=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrSex:"";
			//			m_objPrintInfo. m_strAge=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrAge : "";
			//			m_objPrintInfo. m_strBedName=m_objPatient!=null? m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName :"";
			//			m_objPrintInfo. m_strDeptName=m_objPatient!=null? m_objPatient.m_ObjInBedInfo.m_ObjLastDeptInfo.m_ObjDept.m_StrDeptName :"";
			//			m_objPrintInfo. m_strAreaName=m_objPatient!=null? m_objPatient.m_ObjInBedInfo.m_ObjLastAreaInfo.m_ObjArea.m_StrAreaName:"";			
			//			m_objPrintInfo.m_dtmInPatientDate=p_dtmInPatientDate;
			//			m_objPrintInfo.m_dtmOpenDate=p_dtmOpenDate;
		}

		/// <summary>
		/// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���(�������ݿ��ȡʱҪ����.)
		/// </summary>
		public void m_mthInitPrintContent()
		{		
			m_blnWantInit=false;
			//���û�в��˻��߲��˲���������Ӧ�Ĵ�ӡ��ϸ��ϢҲΪ��
			if(m_strPrintPatientID==null)
			{
				clsPublicFunction.ShowInformationMessageBox("����m_mthInitPrintContent֮ǰ�����ȵ���m_mthSetPrintInfo����");
				return;
			}
			if(m_strPrintPatientID=="")
				m_objRecordContent=null;				
			else
			{
				//��֮���ڲ�����ȥ���ݿ��л�ȡ��ϸ����Ϣ
                //clsOperationAgreedRecordServ m_objRecordsDomain =
                //    (clsOperationAgreedRecordServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsOperationAgreedRecordServ));

				clsOpraAnaSignAgree objContent=new clsOpraAnaSignAgree();
				long lngRes= (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetItemRecord(m_strPrintPatientID,m_dtRecordCreateDate,out objContent );
				//û�м�¼�򷵻�null
				if(lngRes <= 0)
					return ;  
				//�����ת���Ƿ��ȡ���㹻��Ϣ������
				m_objRecordContent=objContent;
			}
			//���ñ����ݵ���ӡ��			
//			m_objPrintInfo.m_objRecordContent=m_objRecordContent;
			m_mthSetPrintValue();//�����з��ӡ����,��ʹ�ڴ�ӡ�հױ�ʱ,����Ҳ����ִ��.			
		}

		/// <summary>
		/// ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
		/// </summary>
		/// <param name="p_objPrintContent">��ӡ����</param>
		public void m_mthSetPrintContent(object p_objPrintContent)
		{
			 		 

			m_blnWantInit=false;
			if(p_objPrintContent.GetType().Name !="clsPrintInfo_OutHospital")
			{
				clsPublicFunction.ShowInformationMessageBox("��������");
				return;
			}
//			m_blnIsFromDataSource=false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ
//			m_objPrintInfo=(clsPrintInfo_OutHospital)p_objPrintContent;
//			m_objRecordContent= m_objPrintInfo. m_objRecordContent ;		
//			m_mthSetPrintValue();			
		}

		/// <summary>
		/// ��ȡ��ӡ����,(�������ݿ��ȡʱ,���ñ�����֮ǰ�����ȵ���m_mthSetPrintInfo����)
		/// </summary>
		/// <returns>��ӡ����</returns>
		public object m_objGetPrintInfo()
		{	
			if(m_blnIsFromDataSource )
			{
				if(m_strPrintPatientID==null)
				{
					clsPublicFunction.ShowInformationMessageBox("�������ݿ��ȡʱ,����m_objGetPrintInfo֮ǰ�����ȵ���m_mthSetPrintInfo����");
					return null;
				}

				if(m_blnWantInit)
					m_mthInitPrintContent();				
			}			
			
			return m_objRecordContent;
		}		

		/// <summary>
		/// ��ʼ����ӡ����,��������ն��󼴿�.
		/// </summary>
		public void m_mthInitPrintTool(object p_objArg)
		{				
			#region �йش�ӡ��ʼ��
			m_fotTitleFont = new Font("SimSun", 18,FontStyle.Bold);
			m_fotHeaderFont = new Font("SimSun", 18,FontStyle.Bold);
			m_fotSmallFont = new Font("SimSun",12);
			m_GridPen = new Pen(Color.Black,1);
			m_slbBrush = new SolidBrush(Color.Black);
			m_objPageSetting = new clsPrintPageSettingForRecord();

			#endregion
		}

		/// <summary>
		/// �ͷŴ�ӡ����
		/// </summary>
		public void m_mthDisposePrintTools(object p_objArg)
		{
			m_fotTitleFont.Dispose();
			m_fotHeaderFont.Dispose();
			m_fotSmallFont .Dispose();
			m_GridPen .Dispose();
			m_slbBrush .Dispose();
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
			if(m_blnIsFromDataSource==false || m_strPrintPatientID=="" ) return;
			//�����ӡ�ɹ�������������Ҫ���µ�ʱ�䣬����У�����ʱ�䡣 
//			if(!((PrintEventArgs)p_objPrintArg).Cancel && m_objPrintInfo.m_objRecordContent.m_dtmFirstPrintDate == DateTime.MinValue )
//			{				
//				m_objRecordsDomain.m_lngUpdateFirstPrintDate(m_objPrintInfo.m_strInPatentID,m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"),m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"),dtmFirstPrintTime);//�����Ҹ�m_objPrintInfo.m_objRecordContent.m_dtmFirstPrintDate);	
//			}                          
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
				m_intYPos += (int)enmRectangleInfo.RowStep+5;						
			}
			
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.RightX,(int)enmRectangleInfo.TopY);
			
			while(m_objPrintContext.m_BlnHaveMoreLine)
			{
				m_objPrintContext.m_mthPrintNextLine(ref m_intYPos,e.Graphics,fntNormal);

				if(m_intYPos >=(int)enmRectangleInfo.BottomY 
					&& m_objPrintContext.m_BlnHaveMoreLine)
				{
					

					#region ��ҳ����
					e.HasMorePages = true;					
				
					e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.LeftX,m_intYPos);
					e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.RightX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.RightX,m_intYPos);
					e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX,m_intYPos,(int)enmRectangleInfo.RightX,m_intYPos);
			
					m_intPages++;	
					m_intYPos = (int)enmRectangleInfo.TopY;
					return;

					#endregion ��ҳ���� 
				}				
				
			}

			#region ���һҳ����
			m_intYPos+=550;				
			e.Graphics.DrawString("����ǩ��:",new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX+30,m_intYPos);
            e.Graphics.DrawString(m_objRecordContent == null ? "":m_objRecordContent.m_strRelationSign, new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 110, m_intYPos);
			e.Graphics.DrawString("ʱ��:",new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX+220,m_intYPos);
            if (m_objRecordContent != null && m_objRecordContent.m_strRelationSign.Length != 0)
				e.Graphics.DrawString(m_objRecordContent.m_dtRelationSignDate.ToString("yyyy-MM-dd"),new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX+265,m_intYPos);
			e.Graphics.DrawString("��λ�쵼ǩ��:",new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX+370,m_intYPos);
            e.Graphics.DrawString(m_objRecordContent == null ? "" : m_objRecordContent.m_strLeadsign, new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 490, m_intYPos);
			e.Graphics.DrawString("ʱ��:",new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX+560,m_intYPos);
            if (m_objRecordContent != null && m_objRecordContent.m_strLeadsign.Length != 0)
				e.Graphics.DrawString(m_objRecordContent.m_dtLeadSignDate.ToString("yyyy-MM-dd"),new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX+605,m_intYPos);
			m_intYPos+=30;				
			e.Graphics.DrawString("ҽʦǩ��:",new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX+30,m_intYPos);
            e.Graphics.DrawString(m_objRecordContent == null ? "" : m_objRecordContent.m_strDoctorName, new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 110, m_intYPos);
			e.Graphics.DrawString("ʱ��:",new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX+220,m_intYPos);
            if (m_objRecordContent != null && m_objRecordContent.m_strDoctorSign.Length != 0)
				e.Graphics.DrawString(m_objRecordContent.m_strDoctorSignDate.ToString("yyyy-MM-dd"),new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX+265,m_intYPos);
			e.Graphics.DrawString("������ǩ��:",new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX+370,m_intYPos);
            e.Graphics.DrawString(m_objRecordContent == null ? "" : m_objRecordContent.m_strDirectorName, new Font("SimSun", 12), Brushes.Black, (int)enmRectangleInfo.LeftX + 470, m_intYPos);
			e.Graphics.DrawString("ʱ��:",new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX+560,m_intYPos);
            if (m_objRecordContent != null && m_objRecordContent.m_strDirectorSign.Length != 0)
				e.Graphics.DrawString(m_objRecordContent.m_dtDirectorSignDate.ToString("yyyy-MM-dd"),new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX+605,m_intYPos);

//			if(m_objRecordContent!=null)
//				e.Graphics.DrawString(m_objRecordContent.m_strDoctorSign,new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX+560+(int)(5f*17.5f),m_intYPos);
			//			m_intYPos+=25;
			//			e.Graphics.DrawString("��������:",new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX+560,m_intYPos);
			//			if(m_objRecordContent!=null)
			//				e.Graphics.DrawString(m_objRecordContent.m_strDoctorID,new Font("SimSun",12) ,Brushes.Black,(int)enmRectangleInfo.LeftX+560+(int)(5f*17.5f),m_intYPos);
			
			m_intYPos+=25;
			if(m_intYPos< (int)enmRectangleInfo.BottomY)
				m_intYPos=(int)enmRectangleInfo.BottomY;
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.LeftX,m_intYPos);
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.RightX,(int)enmRectangleInfo.TopY,(int)enmRectangleInfo.RightX,m_intYPos);
			e.Graphics.DrawLine(m_GridPen,(int)enmRectangleInfo.LeftX,m_intYPos,(int)enmRectangleInfo.RightX,m_intYPos);
			
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
			LeftX = 40,
			/// <summary>
			/// ���ӵ��Ҷ�
			/// </summary>
			RightX = 820-40,
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
            
			Page_HospitalName,
			Page_Name_Title,
			
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
						m_fReturnPoint = new PointF(330f,50f);
						break;
					case (int)enmItemDefination.Page_Name_Title:
						m_fReturnPoint = new PointF(200f,80f);
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
//			if(m_objRecordContent!=null && m_objRecordContent.m_dtmFirstPrintDate !=DateTime.MinValue)
//				dtmFirstPrintTime=m_objRecordContent.m_dtmFirstPrintDate;
			#endregion  ��һ�δ�ӡʱ�丳ֵ

			#region ��ӡ�г�ʼ��
			m_objLine1Arr = new clsPrintLine1[1];
			for(int i=0;i<m_objLine1Arr.Length;i++)
				m_objLine1Arr[i]=new clsPrintLine1();
			
			m_objPrintContext = new clsPrintContext(
				new clsPrintLineBase[]{
										  m_objLine1Arr[0]
									  });
			m_objPrintContext.m_ObjPrintSign = new clsPrintRecordSign();
			#endregion
			
			#region ��ÿһ�е�Ԫ�ظ�ֵ
			if(m_objRecordContent!=null )
			{
				///////////////1��/////////////////
				Object[] objData1=new object[4];
				objData1[0]="�� ����"+m_strPrintPatientName+"����Ժҽ��ȫ�������飬���Ϊ"+m_objRecordContent.m_strStateOfIllness+"�����ݲ��飨����������Ҫ�����ڽ��ڣ����ʵʩ"+m_objRecordContent.m_strAction+"�����ڻ�����ǰ����"+m_objRecordContent.m_strBadFactor+"�Ȳ������أ����������˱��������������������ƣ���Σ���ԣ����ǽ�������ø���׼�����������⣬��ʹ���߲����������������أ������������������ƣ��ķ����Բ�����ȫ���⣬���ܳ������⼰����֢����������Լ���λ�쵼�Դ˱�ʾ��Ⲣͬ����б��������������������ƣ�����ǩ�֡� \n�� �����������������ƣ��п��ܳ��ֵ������Լ�����֢�У�"+m_objRecordContent.m_strSyndrome;;
				objData1[1]="";
				objData1[2]=dtmFirstPrintTime;			
				objData1[3]=" ";
				m_objLine1Arr[0].m_ObjPrintLineInfo =objData1;
				
				
				
			}
			else 
			{
				///////////////1��/////////////////
				Object[] objData1=new object[4];
				objData1[0]="";
				objData1[1]="";
				objData1[2]=dtmFirstPrintTime ;	
				objData1[3]=" ";
				m_objLine1Arr[0].m_ObjPrintLineInfo =objData1;


			}
			
			#endregion 
		}
				
		
		#region �������ֲ���
		/// <summary>
		/// �������ֲ���
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{             
			e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,m_fotSmallFont ,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_HospitalName));
		
			e.Graphics.DrawString("�����������������ƣ�ǰǩ��ͬ����",m_fotTitleFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.Page_Name_Title ));
			


		}
		
	
		#endregion		
		
		#region print class 

		private class clsPrintLine1 : clsPrintLineBase
		{
			private clsPrintRichTextContext m_objDiagnose;			
			private DateTime dtmFirstPrint;
			//			private bool m_blnFirstPrint = true;
			private string m_strTitle;

//			/// <summary>
//			/// ����
//			/// </summary>
//			private const int c_intTop1 = 155;
//			/// <summary>
//			/// ���
//			/// </summary>
//			private const int c_intTop2 = 205;
//			/// <summary>
//			/// ����������ҽʦ
//			/// </summary>
//			private const int c_intTop3 = 255;
			/// <summary>
			/// ��Ժ���
			/// </summary>
			private const int c_intTop4 = 305;
			/// <summary>
			/// ���ƾ���
			/// </summary>
			private const int c_intTop5 = 500;
//			/// <summary>
//			/// ��Ժ���
//			/// </summary>
//			private const int c_intTop6 = 660;
//			/// <summary>
//			/// ��Ժҽ��
//			/// </summary>
//			private const int c_intTop7 = 790;
			/// <summary>
			/// һ�еĸ߶�
			/// </summary>
			private const int c_intOneRowHeight = 40;

			public clsPrintLine1()
			{
				m_objDiagnose = new clsPrintRichTextContext(Color.Black,new Font("SimSun",12));
			}

			//			private int m_intTimes = 0;

			/// <summary>
			/// ���ӳ�������ƫ����
			/// </summary>
			private static int s_intHeightMargin;
			/// <summary>
			/// �ݴ��ƫ����
			/// </summary>
			private static int s_intMarginTemp = 0;
			/// <summary>
			/// ��ӡ��һ��
			/// </summary>
			/// <param name="p_intPosY"></param>
			/// <param name="p_objGrp"></param>
			/// <param name="p_fntNormalText"></param>
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				//����
				Rectangle rtgDianose = new Rectangle(
					(int)enmRectangleInfo.LeftX,
					p_intPosY,
					0,
					0);
				bool blnMiddle = true;
				//���������
				int intSwitchNumber = 0;
				// ԭ���̶��ĸ߶�
				int intPegPosY = 0;
				//���л��д���
				rtgDianose.X += (int)enmRectangleInfo.LeftX;
				rtgDianose.Y += 50;
				rtgDianose.Width = (int)enmRectangleInfo.RightX-rtgDianose.X;
				blnMiddle = false;
				if((rtgDianose.Height =((c_intTop5-c_intTop4-40) - s_intHeightMargin)) < c_intOneRowHeight)
				{
					if (rtgDianose.Height < 0)
						s_intHeightMargin = Math.Abs(rtgDianose.Height);
					rtgDianose.Height = c_intOneRowHeight;
				}
				intPegPosY = c_intTop5;

				int intRealHeight;
				m_objDiagnose.m_blnPrintAllBySimSun(12,rtgDianose,p_objGrp,out intRealHeight,blnMiddle);

				if(intRealHeight > rtgDianose.Height)
				{
					p_intPosY += intRealHeight + 5;
				}
				else
				{
					p_intPosY += rtgDianose.Height;
				}
				if (intSwitchNumber == 0) p_intPosY += 40;

				if (p_intPosY > intPegPosY) 
					s_intHeightMargin += p_intPosY - intPegPosY;
				else
					s_intHeightMargin = 0;

				if(intSwitchNumber == 2)
				{
					s_intMarginTemp = s_intHeightMargin;
					s_intHeightMargin = 0;
					if(intRealHeight > rtgDianose.Height)
						p_intPosY -= (intRealHeight + 40);
					else
						p_intPosY -=  rtgDianose.Height;
				}
				if (intSwitchNumber == 3)
				{
					if (s_intHeightMargin < s_intMarginTemp)
					{
						p_intPosY += s_intMarginTemp - s_intHeightMargin;
						s_intHeightMargin = s_intMarginTemp;
					}
				}


				m_blnHaveMoreLine = false;
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
							m_mthAddSign(m_strTitle.Trim(),m_objDiagnose.m_ObjModifyUserArr);
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



	}
}
