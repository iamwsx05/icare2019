using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.Utility.Controls;
using System.Drawing;

namespace iCare
{
	/// <summary>
	/// �����Ĵ�ӡ������,Jacky-2003-6-10
	/// </summary>
	public class clsThreeMeasureRecordPrintTool_New: infPrintRecord
	{	
		private bool m_blnIsFromDataSource=true;//�����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
		private bool m_blnWantInit=true;		
		private clsThreeMeasureRecordDomain m_objRecordsDomain;
		private clsPrintInfo_ThreeMeasureRecord m_objPrintInfo;
				
		/// <summary>
		/// ���ô�ӡ��Ϣ(�������ݿ��ȡʱҪ���ȵ���.)
		/// </summary>
		/// <param name="p_objPatient">����</param>
		/// <param name="p_dtmInPatientDate">��Ժ����</param>
		/// <param name="p_dtmOpenDate">OpenDate���������OpenDate</param>
		public void m_mthSetPrintInfo(clsPatient p_objPatient,DateTime p_dtmInPatientDate,DateTime p_dtmOpenDate)
		{		
			m_blnIsFromDataSource=true;//�����Ǵ����ݿ��ȡ
			clsPatient m_objPatient=p_objPatient;
			m_objPrintInfo=new clsPrintInfo_ThreeMeasureRecord();
			m_objPrintInfo.m_strInPatentID=m_objPatient!=null? m_objPatient.m_StrInPatientID:"";					
			m_objPrintInfo.m_strPatientName=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrFirstName :"";
			m_objPrintInfo. m_strSex=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrSex:"";
			m_objPrintInfo. m_strAge=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrAge : "";
			m_objPrintInfo. m_strBedName=m_objPatient!=null? m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName :"";
			//m_objPrintInfo. m_strDeptName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjDept.m_StrDeptName :"";
			m_objPrintInfo. m_strAreaName=m_objPatient!=null? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName:"";			
			m_objPrintInfo.m_dtmInPatientDate=p_dtmInPatientDate;
			m_objPrintInfo.m_dtmOpenDate=p_dtmOpenDate;
            m_objPrintInfo.m_strHISInPatientID = m_objPatient!=null? p_objPatient.m_StrHISInPatientID:"";
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
			if(m_objPrintInfo.m_strInPatentID=="")
				m_objPrintInfo.m_objRecordArr=null;				
			else
			{
				m_objRecordsDomain=new clsThreeMeasureRecordDomain();
				m_objPrintInfo.m_objRecordArr = m_objRecordsDomain.m_objGetThreeMeasureRecordInfoArr(m_objPrintInfo.m_strInPatentID,m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));			
			}
			//���ñ����ݵ���ӡ��			
			m_mthSetPrintValue();//�����з��ӡ����,��ʹ�ڴ�ӡ�հױ�ʱ,����Ҳ����ִ��.			
		}

		/// <summary>
		/// ���ô�ӡ���ݡ�(�������Ѿ�����ʱʹ�á�)
		/// </summary>
		/// <param name="p_objPrintContent">��ӡ����</param>
		public void m_mthSetPrintContent(object p_objPrintContent)
		{
			m_blnWantInit=false;
			if(p_objPrintContent.GetType().Name !="clsPrintInfo_ThreeMeasureRecord")
			{
				clsPublicFunction.ShowInformationMessageBox("��������");
			}
			m_blnIsFromDataSource=false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ
			m_objPrintInfo=(clsPrintInfo_ThreeMeasureRecord)p_objPrintContent;					
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

			//û�м�¼����ʱ�����ؿ�
			if(m_objPrintInfo.m_objRecordArr == null || m_objPrintInfo.m_objRecordArr.Length == 0)
				return null;
			else
				return m_objPrintInfo;
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
			//�����ӡ�ɹ�������������Ҫ���µ�ʱ�䣬����У�����ʱ�䡣 
			if(!((PrintEventArgs)p_objPrintArg).Cancel)
			{				
				m_intStartDate = 0;
				m_intWeek = 0;
				m_intItemIndex = int.MaxValue;
				m_intPageNum = 0;		

				if(m_arlSetFirstInPatientDate.Count > 0)
				{
					if(m_blnIsFromDataSource && m_objPrintInfo.m_strInPatentID!="" ) 
						m_objRecordsDomain.m_lngSetFirstPrintDate((string[])m_arlSetFirstInPatientID.ToArray(typeof(string)),(string[])m_arlSetFirstInPatientDate.ToArray(typeof(string)),(string[])m_arlSetFirstOpenDate.ToArray(typeof(string)));

					m_arlSetFirstInPatientID.Clear();
					m_arlSetFirstInPatientDate.Clear();
					m_arlSetFirstOpenDate.Clear();
				}

				
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
		// ��ӡҳ
		private void m_mthPrintPageSub(PrintPageEventArgs e)
		{
			int intStartY = m_intPrintHeader(e);
			bool blnHasMoreDate;
			int intRecordEndY;

			bool blnIsAppend = m_intItemIndex != int.MaxValue;
			if(!blnIsAppend)
			{
				/*
				 * ��ӡ��ҳ
				 */
				m_objPrintRecordTool.m_mthPrintRecord(100,intStartY+30,980,e,m_intStartDate,7,out blnHasMoreDate,out intRecordEndY,out m_intItemIndex);
//				m_objPrintRecordTool.m_C

				m_intWeek++;
				m_intPageNum++;

				if(m_intItemIndex == int.MaxValue)
				{
					//û�и�ҳ
					m_intStartDate += 7;
					e.HasMorePages = blnHasMoreDate;

					m_mthPrintWeek(intRecordEndY+10,e,m_intWeek);
					//Modify By LiChengZhang 2004-12-2
					//m_mthPrintPageNum(intRecordEndY+20,e,m_intPageNum,blnIsAppend);
					m_mthPrintPageNum(intRecordEndY+10,e,m_intPageNum,blnIsAppend);
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
				m_objPrintRecordTool.m_mthPrintLeftRecord(m_intItemIndex,80,120,e,m_intStartDate,7,out intRecordEndY,out blnHasMoreDate);

				m_intItemIndex = int.MaxValue;

				m_intStartDate += 7;
				e.HasMorePages = blnHasMoreDate;	
			
				m_mthPrintWeek(intRecordEndY+10,e,m_intWeek);

				m_mthPrintPageNum(intRecordEndY+20,e,m_intPageNum,blnIsAppend);
			
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

		
		#region ��ӡ
		private ArrayList m_arlSetFirstInPatientID;
		private ArrayList m_arlSetFirstInPatientDate;
		private ArrayList m_arlSetFirstOpenDate;
		private com.digitalwave.Utility.Controls.clsThreeMeasureRecordPrintTool m_objPrintRecordTool;

		private void m_mthSetPrintValue()
		{
			m_arlSetFirstInPatientID = new ArrayList();
			m_arlSetFirstInPatientDate = new ArrayList();
			m_arlSetFirstOpenDate = new ArrayList();


			if(m_objPrintRecordTool == null)
				m_objPrintRecordTool = new clsThreeMeasureRecordPrintTool();	
		
			m_objPrintRecordTool.m_mthClearAll();

			if(m_objPrintInfo.m_strInPatentID != "")
			{
				if(m_objPrintInfo.m_objRecordArr != null)
				{
					for(int i=0;i<m_objPrintInfo.m_objRecordArr.Length;i++)
					{
						DateTime dtmFirstPrintDate = DateTime.MaxValue;

						if(m_objPrintInfo.m_objRecordArr[i].m_strFirstPrintDate != DBNull.Value.ToString())
						{
							dtmFirstPrintDate = DateTime.Parse(m_objPrintInfo.m_objRecordArr[i].m_strFirstPrintDate);
						}
						else
						{
							m_arlSetFirstInPatientID.Add(m_objPrintInfo.m_objRecordArr[i].m_strInPatientID);
							m_arlSetFirstInPatientDate.Add(m_objPrintInfo.m_objRecordArr[i].m_strInPatientDate);
							m_arlSetFirstOpenDate.Add(m_objPrintInfo.m_objRecordArr[i].m_strOpenDate);
                        }
                        m_objPrintRecordTool.m_DtmInPatientDate = m_objPrintInfo.m_dtmInPatientDate;

						//add modifydate
                        //bool blnOk = m_objPrintRecordTool.m_blnSetXml(m_objPrintInfo.m_objRecordArr[i].m_objXmlValue,dtmFirstPrintDate);

                        // ����ӡ�ۼ���
                        bool blnOk = m_objPrintRecordTool.m_blnSetXml(m_objPrintInfo.m_objRecordArr[i].m_objXmlValue, dtmFirstPrintDate, false);

						if(!blnOk)
						{
							clsPublicFunction.ShowInformationMessageBox("��¼�����������ݿ⡣");
							return ;
						}						
					}		
				}
			}			
			
		}

		private int m_intStartDate;		
		private int m_intWeek;
		private int m_intItemIndex;
		private int m_intPageNum;		

		/// <summary>
		/// ��ӡͷ��
		/// </summary>
		/// <param name="e"></param>
		private int m_intPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
		{			
			Font fntTitle = new Font("SimSun", 20,FontStyle.Bold );
			Font fntHeader = new Font("SimSun", 12);

			e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,fntHeader,Brushes.Black,320,30);
			e.Graphics.DrawString("�� �� ��",fntTitle,Brushes.Black,335,70);

			e.Graphics.DrawString("������",fntHeader,Brushes.Black,20,120);
			e.Graphics.DrawString("�Ա�",fntHeader,Brushes.Black,160,120);
			e.Graphics.DrawString("���䣺",fntHeader,Brushes.Black,240,120);
			e.Graphics.DrawString("������",fntHeader,Brushes.Black,365,120);
			e.Graphics.DrawString("���ţ�",fntHeader,Brushes.Black,570,120);
			e.Graphics.DrawString("סԺ�ţ�",fntHeader,Brushes.Black,650,120);			
			

			int intStartY = 120;

			if(m_objPrintInfo.m_strInPatentID != "")
			{
				e.Graphics.DrawString(m_objPrintInfo.m_strPatientName,fntHeader,Brushes.Blue,70,120);
			
				e.Graphics.DrawString(m_objPrintInfo.m_strSex,fntHeader,Brushes.Blue,210,120);
			
				e.Graphics.DrawString(m_objPrintInfo.m_strAge,fntHeader,Brushes.Blue,280,120);
			
				//��ӡ����
                //if(m_objPrintInfo.m_strDeptNameArr !=null)
                //    for(int i=0;i<m_objPrintInfo.m_strDeptNameArr.Length;i++)
                //    {
                //        if(i == 0)
                //        {
							e.Graphics.DrawString(m_objPrintInfo.m_strAreaName,fntHeader,Brushes.Blue,415,intStartY);
                    //    }
                    //    else
                    //    {
                    //        intStartY += 25;
                    //        e.Graphics.DrawString("��"+m_objPrintInfo.m_strDeptNameArr[i],fntHeader,Brushes.Blue,415,intStartY);
                    //    }
                    //}
				
				e.Graphics.DrawString(m_objPrintInfo.m_strBedName,fntHeader,Brushes.Blue,620,120);
			
				e.Graphics.DrawString(m_objPrintInfo.m_strHISInPatientID,fntHeader,Brushes.Blue,710,120);			
			}

			return intStartY;
		}
		/// <summary>
		/// ��ӡ����
		/// </summary>
		/// <param name="p_intStartY"></param>
		/// <param name="e"></param>
		/// <param name="p_intWeek"></param>
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
		#endregion ��ӡ


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
	}	
}