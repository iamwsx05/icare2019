using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.Utility.Controls;
using System.Drawing;
namespace iCare
{
	/// <summary>
	/// clsBornSchedulePrintTool ��ժҪ˵����
	/// </summary>
	public class clsBornSchedulePrintTool: infPrintRecord
	{
		private bool m_blnIsFromDataSource=true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
		private bool m_blnWantInit=true;		
		
		private clsPrintInfo_ThreeMeasureRecord m_objPrintInfo;

		public clsBornSchedulePrintTool()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���(�������ݿ��ȡʱҪ����.)
		/// </summary>
		public void m_mthInitPrintContent()
		{		
//			m_blnWantInit=false;//
//			if(m_objPrintInfo==null)
//			{
//				clsPublicFunction.ShowInformationMessageBox("����m_mthInitPrintContent֮ǰ�����ȵ���m_mthSetPrintInfo����");
//				return;
//			}

			//���ñ����ݵ���ӡ��			
			m_mthSetPrintValue();//�����з��ӡ����,��ʹ�ڴ�ӡ�հױ�ʱ,����Ҳ����ִ��.			
		}
		private void m_mthSetPrintValue()
		{
			
		}


		#region ��ӡ

		private com.digitalwave.Utility.Controls.ctlBornScheduleRecordPrintTool m_objPrintRecordTool;
		
		/// <summary>
		/// ���ô�ӡ��Ϣ(�������ݿ��ȡʱҪ���ȵ���.)
		/// </summary>
		/// <param name="p_objPatient">����</param>
		/// <param name="p_dtmInPatientDate">��Ժ����</param>
		/// <param name="p_dtmOpenDate">OpenDate���������OpenDate</param>
		public void m_mthSetPrintInfo(clsPatient p_objPatient,System.DateTime p_dtInPateintDate, System.DateTime p_dtOpenDate)
		{	
			
			m_objPrintRecordTool = new  com.digitalwave.Utility.Controls.ctlBornScheduleRecordPrintTool();	
			m_objPrintRecordTool.m_StrWoman = p_objPatient.m_StrName;
			m_objPrintRecordTool.m_StrBedNo = p_objPatient.m_strBedCode;
			m_objPrintRecordTool.m_StrAge = p_objPatient.m_ObjPeopleInfo.m_StrAge;
            m_objPrintRecordTool.m_StrHospitalName = clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle;
			
		}

		public  void  m_mthSetPrintInfo(clsPatient p_objPatient,clsBornRecordManager p_objBornRecordManager,clsBornScheduleEveryDay p_objBornScheduleEveryDay)
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
		/// ��ȡ��ӡ����,(�������ݿ��ȡʱ,���ñ�����֮ǰ�����ȵ���m_mthSetPrintInfo����)
		/// </summary>
		/// <returns>��ӡ����</returns>
		public object m_objGetPrintInfo()
		{	
//			if(m_blnIsFromDataSource )
//			{
//				if(m_objPrintInfo==null)
//				{
//					clsPublicFunction.ShowInformationMessageBox("�������ݿ��ȡʱ,����m_objGetPrintInfo֮ǰ�����ȵ���m_mthSetPrintInfo����");
//					return null;
//				}
//
//				if(m_blnWantInit)
//					m_mthInitPrintContent();				
//			}			
//
//			//û�м�¼����ʱ�����ؿ�
//			if(m_objPrintInfo.m_objRecordArr == null || m_objPrintInfo.m_objRecordArr.Length == 0)
//				return null;
//			else
//				return m_objPrintInfo;

			return null;
		}	
		/// <summary>
		/// ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
		/// </summary>
		/// <param name="p_objPrintContent">��ӡ����</param>
		public void m_mthSetPrintContent(object p_objPrintContent)
		{
			ArrayList arlContent=(ArrayList)p_objPrintContent;
			m_objPrintRecordTool.m_clsBornRecordManager = (clsBornRecordManager)arlContent[0];
			m_objPrintRecordTool.m_clsCurrentDay = (clsBornScheduleEveryDay)arlContent[1];

//			m_blnWantInit=false;
//			if(p_objPrintContent.GetType().Name !="clsPrintInfo_ThreeMeasureRecord")
//			{
//				clsPublicFunction.ShowInformationMessageBox("��������");
//			}
//			m_blnIsFromDataSource=false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ
//			m_objPrintInfo=(clsPrintInfo_ThreeMeasureRecord)p_objPrintContent;					
//			m_mthSetPrintValue();			
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
		/// ��ʼ����ӡ����,��������ն��󼴿�.
		/// </summary>
		public void m_mthInitPrintTool(object p_objArg)
		{				
			
		}

		/// <summary>
		/// �ͷŴ�ӡ����
		/// </summary>
		public void m_mthDisposePrintTools(object p_objArg)
		{
			
		}


		/// <summary>
		/// ��ӡ������һ��ʹ�������������ݿ���Ϣ��
		/// </summary>
		/// <param name="p_objPrintArg">�˴�p_objPrintArgҪ��ΪPrintEventArgs���͵Ķ���</param>
		public void m_mthEndPrint(object p_objPrintArg)
		{			
			//�����ӡ�ɹ�������������Ҫ���µ�ʱ�䣬����У�����ʱ�䡣 
			if(!((PrintEventArgs)p_objPrintArg).Cancel)
			{				
				m_intStartDate = 0;
				m_intWeek = 0;
				m_intItemIndex = int.MaxValue;
				m_intPageNum = 0;		

			}                          
			m_mthEndPrintSub((PrintEventArgs)p_objPrintArg);
		}

		private  void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
		{
			//ȱʡ�����κζ���
			m_intStartDate = 0;
			m_intWeek = 0;
			m_intItemIndex = int.MaxValue;
		}

		private int m_intStartDate;		
		private int m_intWeek;
		private int m_intItemIndex;
		private int m_intPageNum;	
		// ��ӡҳ
		private void m_mthPrintPageSub(PrintPageEventArgs e)
		{
			int intStartY = 0;//m_intPrintHeader(e);
			bool blnHasMoreDate=false;
			int intRecordEndY;

			
			int m_intItemIndex=int.MaxValue;
			bool blnIsAppend = m_intItemIndex != int.MaxValue;
		
			if(!blnIsAppend)
			{
				/*
				 * ��ӡ��ҳ
				 */
				m_objPrintRecordTool.m_mthPrintRecord(100,intStartY+40,980,e,m_intStartDate,7,out blnHasMoreDate,out intRecordEndY,out m_intItemIndex);
				//				m_objPrintRecordTool.m_C

				m_intWeek++;
				m_intPageNum++;

				if(m_intItemIndex == int.MaxValue)
				{
					//û�и�ҳ
					m_intStartDate += 7;
					e.HasMorePages = blnHasMoreDate;

					//m_mthPrintWeek(intRecordEndY+10,e,m_intWeek);
					//Modify By LiChengZhang 2004-12-2
					//m_mthPrintPageNum(intRecordEndY+20,e,m_intPageNum,blnIsAppend);
					//m_mthPrintPageNum(intRecordEndY+10,e,m_intPageNum,blnIsAppend);
					//Modify END
					

					if(!blnHasMoreDate)
					{
						//���������Ѿ���ӡ��
						m_intStartDate = 0;
						m_intWeek = 0;
						m_intItemIndex = int.MaxValue;
						m_intPageNum = 0;
					}
				}
				else
				{
					//�и�ҳ

					m_mthPrintWeek(intRecordEndY+10,e,m_intWeek);

					m_mthPrintPageNum(intRecordEndY+20,e,m_intPageNum,blnIsAppend);

					e.HasMorePages = true;
				}
			}
			else
			{
				//��ӡ��ҳ
				//m_objPrintRecordTool.m_mthPrintLeftRecord(m_intItemIndex,80,120,e,m_intStartDate,7,out intRecordEndY,out blnHasMoreDate);

				m_intItemIndex = int.MaxValue;

				m_intStartDate += 7;
				e.HasMorePages = blnHasMoreDate;	
			
//				m_mthPrintWeek(intRecordEndY+10,e,m_intWeek);
//
//				m_mthPrintPageNum(intRecordEndY+20,e,m_intPageNum,blnIsAppend);
			
				if(!blnHasMoreDate)
				{
					//���������Ѿ���ӡ��
					m_intStartDate = 0;
					m_intWeek = 0;
					m_intItemIndex = int.MaxValue;
					m_intPageNum = 0;
				}
			}
		}

		// ��ӡ����ʱ�Ĳ���
		private  void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
		{			
			
		}	

		private void m_mthPrintWeek(int p_intStartY,System.Drawing.Printing.PrintPageEventArgs e,int p_intWeek)
		{		
			Font fntHeader = new Font("SimSun", 12);

			e.Graphics.DrawString("��   ҳ",fntHeader,Brushes.Black,360,p_intStartY);
			e.Graphics.DrawString(p_intWeek.ToString(),fntHeader,Brushes.Blue,385,p_intStartY);
		}
		/// <summary>
		/// ��ӡҳ��
		/// </summary>
		/// <param name="p_intStartY"></param>
		/// <param name="e"></param>
		/// <param name="p_intPageNum"></param>
		/// <param name="p_blnIsAppend"></param>
		private void m_mthPrintPageNum(int p_intStartY,System.Drawing.Printing.PrintPageEventArgs e,int p_intPageNum,bool p_blnIsAppend)
		{		
			Font fntHeader = new Font("SimSun", 9);

			//			if(!p_blnIsAppend)
			//				e.Graphics.DrawString("��    ҳ",fntHeader,Brushes.Black,650,p_intStartY);
			//			else
			//				e.Graphics.DrawString("��    ҳ��ҳ",fntHeader,Brushes.Black,650,p_intStartY);
			//			e.Graphics.DrawString(p_intPageNum.ToString(),fntHeader,Brushes.Blue,675,p_intStartY);
		}

		#endregion

		#region ���ⲿ���Ա���ӡ����ʾʵ��.	
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
			m_mthPrintPage(e);
			//
			//			if(ppdPrintPreview != null)
			//				while(!ppdPrintPreview.m_blnHandlePrint(e))
			//					objPrintTool.m_mthPrintPage(e);
			//		}
			//			m_mthAddDataToGrid(e);
		}
		private void frmPreview_m_evtPrintFrame(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			//			m_mthPrintTitleInfo(e);	
			//			m_mthPrintRectangleInfo(e);	
			//			m_mthPrintHeaderInfo(e);
		}
		#endregion
	
		#endregion ���ⲿ���Ա���ӡ����ʾʵ��.
	}
}
