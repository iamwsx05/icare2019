using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.Utility.Controls;
using System.Drawing;
using com.digitalwave.Utility.Controls;
using System.Windows.Forms;

namespace iCare.ICU.Evaluation
{
	/// <summary>
	/// Summary description for clsValuationPrintBase.
	/// </summary>
	public class clsValuationPrintBase: infPrintRecord
	{
		#region Define
		private bool m_blnCanPrint=true;
		private bool m_blnWantInit=true;
		private clsValuationPrintContent m_objContent;
		private clsPatient m_objPatient;
		#endregion

		public clsValuationPrintBase()
		{}

		
		/// <summary>
		/// ���ô�ӡ��Ϣ(�������ݿ��ȡʱҪ���ȵ���.)
		/// </summary>
		/// <param name="p_objPatient">����</param>
		/// <param name="p_dtmInPatientDate">��Ժ����</param>
		/// <param name="p_dtmOpenDate">OpenDate�������һ�δ�ӡ��μ�¼�������ͣ��粡����¼��������OpenDate</param>
		public void m_mthSetPrintInfo(clsPatient p_objPatient,object p_objContent,DateTime p_dtmCreateDate)
		{	
			if(p_objPatient == null)
			{
				MessageBox.Show("����ѡ���ˣ�");
				m_blnCanPrint = false;
				return;
			}
			m_objPatient = p_objPatient;
			m_objContent = new clsValuationPrintContent();
			m_objContent.m_strPatientID = p_objPatient.m_StrInPatientID;
			m_objContent.m_strInPatientDate = p_objPatient.m_DtmLastInDate.ToString("yyyy-MM-dd HH:mm:ss");
			m_objContent.m_strActivityTime = p_dtmCreateDate == DateTime.MinValue ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : p_dtmCreateDate.ToString("yyyy-MM-dd HH:mm:ss");
			m_objContent.m_strAge = p_objPatient.m_ObjPeopleInfo.m_StrAge;
            m_objContent.m_strAreaName = p_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_objPatient.m_DtmSelectedInDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName;
			m_objContent.m_strBedName = p_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName;
            m_objContent.m_strDeptName = p_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_objPatient.m_DtmSelectedInDate).m_ObjLastDept.m_ObjDept.m_StrDeptName;
			m_objContent.m_strName = p_objPatient.m_StrName;
			m_objContent.m_strSex = p_objPatient.m_StrSex;
			if(p_objContent != null)
			{
				m_objContent.m_objContent = p_objContent;
			}
		}

		public void m_mthSetPrintInfo(clsPatient p_objPatient,DateTime p_dtmInPatientDate,DateTime p_dtmOpenDate)
		{
			return;
		}

		/// <summary>
		/// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���(�������ݿ��ȡʱҪ����.)
		/// </summary>
		public void m_mthInitPrintContent()
		{	
			m_blnWantInit = false;

			if(m_objContent == null)
			{
				MessageBox.Show("����m_mthInitPrintContent֮ǰ�����ȵ���m_mthSetPrintInfo����");
				return;
			}
		
			//���ñ����ݵ���ӡ��,��ʹ�Ǵ�ӡ�հ׵�,����Ҳ����ִ��.(��:�ڱ������ڲ�,����֮�ϲ�׼��return���,���ǳ�������.)
			m_mthSetPrintContent(m_objContent);		
				
		}

		/// <summary>
		/// ��ȡ��ӡ����,(�������ݿ��ȡʱ,���ñ�����֮ǰ�����ȵ���m_mthSetPrintInfo����)
		/// </summary>
		/// <returns>��ӡ����</returns>
		public object m_objGetPrintInfo()
		{	
			if(m_objContent==null)
			{
				MessageBox.Show("�������ݿ��ȡʱ,����m_objGetPrintInfo֮ǰ�����ȵ���m_mthSetPrintInfo����");
				return null;
			}

			if(m_blnWantInit)
				m_mthInitPrintContent();	
		
			//û�м�¼����ʱ�����ؿ�
			if(m_objContent.m_objContent == null)
				return null;
			else
				return m_objContent;
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
		}


		#region ��ӡ

		// ���ô�ӡ���ݡ�
		public  void m_mthSetPrintContent(object p_objContent)
		{
			m_mthSetPrintLineArr();
			//���ô�ӡ��Ϣ������Set Value��ȥ
			m_objPrintLineContext.m_ObjPrintLineInfo = p_objContent;
		}

		/// <summary>
		/// ���ô�ӡ��
		/// </summary>
		protected virtual void m_mthSetPrintLineArr()
		{
		}

#region �йش�ӡ������

		/// <summary>
		/// ��ӡ����������
		/// </summary>
		protected clsPrintContext m_objPrintLineContext;	

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
		protected SolidBrush m_slbBrush;
		/// <summary>
		/// ��ǰ��ӡλ�ã�Y��
		/// </summary>
		private int m_intYPos=150 ;//= (int)enmRectangleInfo.TopY+5;
	
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
				PointF m_fReturnPoint;
				switch(p_intItemName)
				{   
            
					case (int)enmItemDefination.Page_HospitalName:
						m_fReturnPoint = new PointF(350f,40f);
						break;
					case (int)enmItemDefination.Page_Name_Title:
						m_fReturnPoint = new PointF(280f,70f);
						break;
					case (int)enmItemDefination.Name_Title :
						m_fReturnPoint = new PointF(50f,110f);
						break;
					case (int)enmItemDefination.Name:
						m_fReturnPoint = new PointF(100f,110f);
						break;

					case (int)enmItemDefination.Sex_Title :
						m_fReturnPoint = new PointF(250f,110f);
						break;
					case (int)enmItemDefination.Sex :
						m_fReturnPoint = new PointF(300f,110f);
						break;

					case (int)enmItemDefination.Age_Title :
						m_fReturnPoint = new PointF(450f,110f);
						break;
					case (int)enmItemDefination.Age:
						m_fReturnPoint = new PointF(500f,110f);
						break;

					case (int)enmItemDefination.Dept_Name_Title:
						m_fReturnPoint = new PointF(360f,110f);
						break;
					case (int)enmItemDefination.Dept_Name :
						m_fReturnPoint = new PointF(410f,110f);
						break;
			
					case (int)enmItemDefination.BedNo_Title :
						m_fReturnPoint = new PointF(560f,110f);
						break;
					case (int)enmItemDefination.BedNo:
						m_fReturnPoint = new PointF(610f,110f);
						break;

					case (int)enmItemDefination.InPatientID_Title:
						m_fReturnPoint = new PointF(80f,110f);
						break;
					case (int)enmItemDefination.InPatientID :
						m_fReturnPoint = new PointF(150f,110f);
						break;
					case (int)enmItemDefination.RecordDate :
						m_fReturnPoint = new PointF(450f,110f);
						break;
					case (int)enmItemDefination.RecordTime :
						m_fReturnPoint = new PointF(530f,110f);
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

			while(m_objPrintLineContext.m_BlnHaveMoreLine)
			{
				//�������ݴ�ӡ
				m_objPrintLineContext.m_mthPrintNextLine(ref m_intYPos,p_objPrintPageArg.Graphics,m_fotSmallFont);

				if(m_intYPos > p_objPrintPageArg.PageBounds.Height-230
					&& m_objPrintLineContext.m_BlnHaveMoreLine)
				{
					//�������ݴ�ӡ������Ҫ��ҳ

					m_mthPrintFoot(p_objPrintPageArg);

					p_objPrintPageArg.HasMorePages = true;

					m_intYPos = 150;

					m_intCurrentPage++;

					return;
				}				
			}

			//ȫ������			

//			m_mthPrintFoot(p_objPrintPageArg);

		}


		#region PrintClasses

		protected abstract class clsPrintValuationInfo : clsPrintLineBase
		{
			protected clsValuationPrintContent m_objContent;
			/// <summary>
			/// ���־�����ߵı߾�
			/// </summary>
			protected int m_intRecBaseX = clsPrintPosition.c_intLeftX;
			protected int m_intPatientInfoX = 70;
			protected object m_objPrintInfo;
			
			public override object  m_ObjPrintLineInfo
			{
				get
				{
					return base.m_blnHaveMoreLine;
				}
				set
				{
					if(value == null)return;
					m_objContent = (clsValuationPrintContent )value;					
					m_objPrintInfo=((clsValuationPrintContent )value).m_objContent;
				}				
			}
			
			protected void m_mthDrawCheckBox(int p_intPosX,int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText,bool p_blnChecked)
			{
				p_objGrp.DrawRectangle(new Pen(Color.Black),p_intPosX,p_intPosY,15,15);
				if(p_blnChecked)
					p_objGrp.DrawString("��",p_fntNormalText,Brushes.Black,p_intPosX+1,p_intPosY);
			}
			protected void m_mthDrawCheckBox(int p_intPosX,int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText,string p_strText)
			{
				p_objGrp.DrawString("�� " + p_strText,p_fntNormalText,Brushes.Black,p_intPosX+1,p_intPosY);
				p_objGrp.DrawRectangle(new Pen(Color.Black),p_intPosX,p_intPosY,15,15);
			}
		}
		/// <summary>
		/// ��ӡ��һҳ�Ĺ̶�����
		/// </summary>
		protected class clsPrintPatientFixInfo : clsPrintValuationInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_objGrp.DrawString("������"+ m_objContent.m_strName,p_fntNormalText,Brushes.Black,m_intPatientInfoX+40,p_intPosY);
				p_objGrp.DrawString("�Ա�"+ m_objContent.m_strSex ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+240,p_intPosY);		
				p_objGrp.DrawString("���䣺"+m_objContent.m_strAge,p_fntNormalText,Brushes.Black,m_intPatientInfoX+440,p_intPosY);
		
				p_intPosY += 30;
				p_objGrp.DrawString("�Ʊ�"+m_objContent.m_strDeptName ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+40,p_intPosY);
				p_objGrp.DrawString("������"+m_objContent.m_strAreaName ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+240,p_intPosY);
				p_objGrp.DrawString("��λ��"+m_objContent.m_strBedName ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+440,p_intPosY);
				
				p_intPosY += 30;
				m_blnHaveMoreLine = false;
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();

				m_blnHaveMoreLine = true;
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

			e.Graphics.DrawString("��      ҳ",fntHeader,Brushes.Black,385,e.PageBounds.Height-105);
			e.Graphics.DrawString(m_intCurrentPage.ToString(),fntHeader,Brushes.Black,425,e.PageBounds.Height-105);			
		}
		
		/// <summary>
		/// ��ӡ��
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
		{			
			Font fntTitle = new Font("SimSun", 18,FontStyle.Bold );
			Font fntHeader = new Font("SimSun", 12);

			e.Graphics.DrawRectangle(Pens.Black,m_intRecBaseX - 10,135,(int)enmRectangleInfo.RightX - (int)enmRectangleInfo.LeftX + 10,e.PageBounds.Height-250);
		}


		/// <summary>
		/// �������ֲ���
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{
			e.Graphics.DrawString("סԺ�ţ�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID_Title ));
			e.Graphics.DrawString(m_objContent.m_strPatientID ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID ));

			e.Graphics.DrawString("�������ڣ�",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.RecordDate ));
			e.Graphics.DrawString(DateTime.Parse( m_objContent.m_strActivityTime).ToString("yyyy��MM��dd�� HH:mm:ss") ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.RecordTime ));
			
			m_mthPrintSubTitle(e.Graphics);
		}
		/// <summary>
		/// �������ֱ�����
		/// </summary>
		/// <param name="p_objGrp"></param>
		protected virtual void m_mthPrintSubTitle(System.Drawing.Graphics p_objGrp)
		{
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

			m_intYPos = 145;

			m_intCurrentPage = 1;
		}

	#endregion ��ӡ

	}

	public class clsValuationPrintContent
	{
		public string m_strPatientID;
		public string m_strInPatientDate;
		public string m_strActivityTime;
		public string m_strModifyDate;
		public string m_strName;
		public string m_strSex;
		public string m_strAge;
		public string m_strDeptName;
		public string m_strAreaName;
		public string m_strBedName;

		public object m_objContent;
	}
}
