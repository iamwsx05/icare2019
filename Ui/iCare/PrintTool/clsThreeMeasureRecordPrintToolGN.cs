using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.Utility.Controls;
using System.Drawing;

namespace iCare.GN
{
	/// <summary>
	/// 三测表的打印工具类,Jacky-2003-6-10
	/// </summary>
	public class clsThreeMeasureRecordPrintTool_New: infPrintRecord
	{	
		private bool m_blnIsFromDataSource=true;//表明是从数据库读取还是从文件直接提取信息
		private bool m_blnWantInit=true;		
		private clsThreeMeasureRecordDomainGN m_objRecordsDomain;
		private clsPrintInfo_ThreeMeasureRecord m_objPrintInfo;
				
		/// <summary>
		/// 设置打印信息(当从数据库读取时要首先调用.)
		/// </summary>
		/// <param name="p_objPatient">病人</param>
		/// <param name="p_dtmInPatientDate">入院日期</param>
		/// <param name="p_dtmOpenDate">OpenDate，本类忽略OpenDate</param>
		public void m_mthSetPrintInfo(clsPatient p_objPatient,DateTime p_dtmInPatientDate,DateTime p_dtmOpenDate)
		{		
			m_blnIsFromDataSource=true;//表明是从数据库读取
			clsPatient m_objPatient=p_objPatient;
			m_objPrintInfo=new clsPrintInfo_ThreeMeasureRecord();
			m_objPrintInfo.m_strInPatentID=m_objPatient!=null? m_objPatient.m_StrInPatientID:"";					
			m_objPrintInfo.m_strPatientName=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrFirstName :"";
				
			m_objPrintInfo. m_strSex=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrSex:"";
			m_objPrintInfo. m_strAge=m_objPatient!=null? m_objPatient.m_ObjPeopleInfo.m_StrAge : "";
			m_objPrintInfo. m_strBedName=m_objPatient!=null? m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName :"";
            //m_objPrintInfo.m_strDeptName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjDept.m_StrDeptName : "";
			m_objPrintInfo. m_strAreaName=m_objPatient!=null? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName:"";			
			m_objPrintInfo.m_dtmInPatientDate=p_dtmInPatientDate;
			m_objPrintInfo.m_dtmOpenDate=p_dtmOpenDate;

            m_objPrintInfo.m_strHISInPatientID = m_objPatient != null ? m_objPatient.m_StrHISInPatientID : "";
            m_objPrintInfo.m_dtmHISInDate = m_objPatient != null ? m_objPatient.m_DtmSelectedHISInDate : DateTime.MinValue;
			if(m_objPatient !=null)	
			{
				int intTemp=p_objPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_intGetDeptCount();
				m_objPrintInfo. m_strDeptNameArr=new string[intTemp];
				for(int i=0;i<intTemp;i++)
				{		
					m_objPrintInfo. m_strDeptNameArr[i]=p_objPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_objGetDeptByIndex(i).m_ObjDept.m_StrDeptName;				
				}
			}
			else m_objPrintInfo.m_strDeptNameArr=null;

		}

		/// <summary>
		/// 从数据库初始化打印内容。如果没有记录，打印空报表。(当从数据库读取时要调用.)
		/// </summary>
		public void m_mthInitPrintContent()
		{		
			m_blnWantInit=false;//
			if(m_objPrintInfo==null)
			{
				clsPublicFunction.ShowInformationMessageBox("调用m_mthInitPrintContent之前请首先调用m_mthSetPrintInfo函数");
				return;
			}
			if(m_objPrintInfo.m_strInPatentID=="")
				m_objPrintInfo.m_objRecordArr=null;				
			else
			{
				m_objRecordsDomain=new clsThreeMeasureRecordDomainGN();
				m_objPrintInfo.m_objRecordArr = m_objRecordsDomain.m_objGetThreeMeasureRecordInfoArr(m_objPrintInfo.m_strInPatentID,m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));			
			}
			//设置表单内容到打印中			
			m_mthSetPrintValue();//无论有否打印数据,即使在打印空白表时,此行也必须执行.			
		}

		/// <summary>
		/// 设置打印内容。(当数据已经存在时使用。)
		/// </summary>
		/// <param name="p_objPrintContent">打印内容</param>
		public void m_mthSetPrintContent(object p_objPrintContent)
		{
			m_blnWantInit=false;
			if(p_objPrintContent.GetType().Name !="clsPrintInfo_ThreeMeasureRecord")
			{
				clsPublicFunction.ShowInformationMessageBox("参数错误");
			}
			m_blnIsFromDataSource=false;//表明是从文件直接提取信息
			m_objPrintInfo=(clsPrintInfo_ThreeMeasureRecord)p_objPrintContent;					
			m_mthSetPrintValue();			
		}

		/// <summary>
		/// 获取打印内容,(当从数据库读取时,调用本函数之前请首先调用m_mthSetPrintInfo函数)
		/// </summary>
		/// <returns>打印内容</returns>
		public object m_objGetPrintInfo()
		{	
			if(m_blnIsFromDataSource )
			{
				if(m_objPrintInfo==null)
				{
					clsPublicFunction.ShowInformationMessageBox("当从数据库读取时,调用m_objGetPrintInfo之前请首先调用m_mthSetPrintInfo函数");
					return null;
				}

				if(m_blnWantInit)
					m_mthInitPrintContent();				
			}			

			//没有记录内容时，返回空
			if(m_objPrintInfo.m_objRecordArr == null || m_objPrintInfo.m_objRecordArr.Length == 0)
				return null;
			else
				return m_objPrintInfo;
		}		

		/// <summary>
		/// 初始化打印变量,本例传入空对象即可.
		/// </summary>
		public void m_mthInitPrintTool(object p_objArg)
		{				
			
		}

		/// <summary>
		/// 释放打印变量
		/// </summary>
		public void m_mthDisposePrintTools(object p_objArg)
		{
			
		}

		/// <summary>
		/// 打印开始
		/// </summary>
		/// <param name="p_objPrintArg">此处p_objPrintArg要求为PrintEventArgs类型的对象</param>
		public void m_mthBeginPrint(object p_objPrintArg)
		{			
			m_mthBeginPrintSub((PrintEventArgs)p_objPrintArg);
		}

		/// <summary>
		/// 打印中
		/// </summary>
		/// <param name="p_objPrintArg">此处p_objPrintArg要求为PrintPageEventArgs类型的对象</param>
		public void m_mthPrintPage(object p_objPrintArg)
		{
			m_mthPrintPageSub((PrintPageEventArgs)p_objPrintArg);
		}

		/// <summary>
		/// 打印结束。一般使用它来更新数据库信息。
		/// </summary>
		/// <param name="p_objPrintArg">此处p_objPrintArg要求为PrintEventArgs类型的对象</param>
		public void m_mthEndPrint(object p_objPrintArg)
		{			
			//如果打印成功，查找有无需要更新的时间，如果有，更新时间。 
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
			//缺省不做任何动作
			m_intStartDate = 0;
			m_intWeek = 0;
			m_intItemIndex = int.MaxValue;
		}
		// 打印页
		private void m_mthPrintPageSub(PrintPageEventArgs e)
		{
			int intStartY = m_intPrintHeader(e);
			bool blnHasMoreDate;
			int intRecordEndY;

			bool blnIsAppend = m_intItemIndex != int.MaxValue;
			if(!blnIsAppend)
			{
				/*
				 * 打印正页
				 */
				m_objPrintRecordTool.m_mthPrintRecord(100,intStartY+20,980,e,m_intStartDate,7,out blnHasMoreDate,out intRecordEndY,out m_intItemIndex);
//				m_objPrintRecordTool.m_C

				m_intWeek++;
				m_intPageNum++;

				if(m_intItemIndex == int.MaxValue)
				{
					//没有附页
					m_intStartDate += 7;
					e.HasMorePages = blnHasMoreDate;

					m_mthPrintWeek(intRecordEndY+10,e,m_intWeek);
					//Modify By LiChengZhang 2004-12-2
					//m_mthPrintPageNum(intRecordEndY+20,e,m_intPageNum,blnIsAppend);
					m_mthPrintPageNum(intRecordEndY+10,e,m_intPageNum,blnIsAppend);
					//Modify END
					

					if(!blnHasMoreDate)
					{
						//所有日期已经打印完
						m_intStartDate = 0;
						m_intWeek = 0;
						m_intItemIndex = int.MaxValue;
						m_intPageNum = 0;
					}
				}
				else
				{
					//有附页

					m_mthPrintWeek(intRecordEndY+10,e,m_intWeek);

					m_mthPrintPageNum(intRecordEndY+20,e,m_intPageNum,blnIsAppend);

					e.HasMorePages = true;
				}
			}
			else
			{
				//打印附页
				m_objPrintRecordTool.m_mthPrintLeftRecord(m_intItemIndex,80,120,e,m_intStartDate,7,out intRecordEndY,out blnHasMoreDate);

				m_intItemIndex = int.MaxValue;

				m_intStartDate += 7;
				e.HasMorePages = blnHasMoreDate;	
			
				m_mthPrintWeek(intRecordEndY+10,e,m_intWeek);

				m_mthPrintPageNum(intRecordEndY+20,e,m_intPageNum,blnIsAppend);
			
				if(!blnHasMoreDate)
				{
					//所有日期已经打印完
					m_intStartDate = 0;
					m_intWeek = 0;
					m_intItemIndex = int.MaxValue;
					m_intPageNum = 0;
				}
			}
		}

		// 打印结束时的操作
		private  void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
		{			
			
		}	

		
		#region 打印
		private ArrayList m_arlSetFirstInPatientID;
		private ArrayList m_arlSetFirstInPatientDate;
		private ArrayList m_arlSetFirstOpenDate;
		private com.digitalwave.Utility.Controls.GN.clsThreeMeasureRecordPrintTool m_objPrintRecordTool;

		private void m_mthSetPrintValue()
		{
			m_arlSetFirstInPatientID = new ArrayList();
			m_arlSetFirstInPatientDate = new ArrayList();
			m_arlSetFirstOpenDate = new ArrayList();


			if(m_objPrintRecordTool == null)
				m_objPrintRecordTool = new  com.digitalwave.Utility.Controls.GN.clsThreeMeasureRecordPrintTool();	
		
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
						m_objPrintRecordTool.m_DtmInPatientDate=m_objPrintInfo.m_dtmInPatientDate;//mod 2005/11/30

						//add modifydate
						bool blnOk = m_objPrintRecordTool.m_blnSetXml(m_objPrintInfo.m_objRecordArr[i].m_objXmlValue,dtmFirstPrintDate);

						if(!blnOk)
						{
							clsPublicFunction.ShowInformationMessageBox("记录出错，请检查数据库。");
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
		/// 打印头部
		/// </summary>
		/// <param name="e"></param>
		private int m_intPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
		{			
			Font fntTitle = new Font("SimSun", 18,FontStyle.Bold );
			Font fntHeader = new Font("SimSun", 12);

			e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,fntHeader,Brushes.Black,320,30);
			e.Graphics.DrawString("体 温 单",fntTitle,Brushes.Black,345,55);

			e.Graphics.DrawString("姓名：",fntHeader,Brushes.Black,40,90);
			e.Graphics.DrawString("性别：",fntHeader,Brushes.Black,170,90);
			e.Graphics.DrawString("年龄：",fntHeader,Brushes.Black,250,90);
			e.Graphics.DrawString("科室：",fntHeader,Brushes.Black,350,90);
			e.Graphics.DrawString("床号：",fntHeader,Brushes.Black,550,90);
			e.Graphics.DrawString("住院号：",fntHeader,Brushes.Black,650,90);			
			

			int intStartY = 90;

			if(m_objPrintInfo.m_strInPatentID != "")
			{
				e.Graphics.DrawString(m_objPrintInfo.m_strPatientName,fntHeader,Brushes.Black,90,90);
			
				e.Graphics.DrawString(m_objPrintInfo.m_strSex,fntHeader,Brushes.Black,220,90);
			
				e.Graphics.DrawString(m_objPrintInfo.m_strAge,fntHeader,Brushes.Black,300,90);
                if(m_objPrintInfo.m_strDeptNameArr != null)
                    e.Graphics.DrawString(m_objPrintInfo.m_strDeptNameArr[m_objPrintInfo.m_strDeptNameArr.Length-1], fntHeader, Brushes.Black, 400, 90);
				
				e.Graphics.DrawString(m_objPrintInfo.m_strBedName,fntHeader,Brushes.Black,600,90);
			
				e.Graphics.DrawString(m_objPrintInfo.m_strHISInPatientID,fntHeader,Brushes.Black,710,90);			
			
				//打印科室
				if(m_objPrintInfo.m_strDeptNameArr !=null)
					for(int i=0;i<m_objPrintInfo.m_strDeptNameArr.Length;i++)
					{
						if(i == 0)
						{
							e.Graphics.DrawString(m_objPrintInfo.m_strDeptNameArr[i],fntHeader,Brushes.Black,400,intStartY);
						}
						else
						{
							intStartY += 25;
							e.Graphics.DrawString("→"+m_objPrintInfo.m_strDeptNameArr[i],fntHeader,Brushes.Black,400,intStartY);
						}
					}
			}

			return intStartY;
		}
		/// <summary>
		/// 打印周数
		/// </summary>
		/// <param name="p_intStartY"></param>
		/// <param name="e"></param>
		/// <param name="p_intWeek"></param>
		private void m_mthPrintWeek(int p_intStartY,System.Drawing.Printing.PrintPageEventArgs e,int p_intWeek)
		{		
			Font fntHeader = new Font("SimSun", 12);

			e.Graphics.DrawString("第   页",fntHeader,Brushes.Black,360,p_intStartY);
			e.Graphics.DrawString(p_intWeek.ToString(),fntHeader,Brushes.Blue,385,p_intStartY);
		}
		/// <summary>
		/// 打印页数
		/// </summary>
		/// <param name="p_intStartY"></param>
		/// <param name="e"></param>
		/// <param name="p_intPageNum"></param>
		/// <param name="p_blnIsAppend"></param>
		private void m_mthPrintPageNum(int p_intStartY,System.Drawing.Printing.PrintPageEventArgs e,int p_intPageNum,bool p_blnIsAppend)
		{		
			Font fntHeader = new Font("SimSun", 9);

//			if(!p_blnIsAppend)
//				e.Graphics.DrawString("第    页",fntHeader,Brushes.Black,650,p_intStartY);
//			else
//				e.Graphics.DrawString("第    页附页",fntHeader,Brushes.Black,650,p_intStartY);
//			e.Graphics.DrawString(p_intPageNum.ToString(),fntHeader,Brushes.Blue,675,p_intStartY);
		}
		#endregion 打印

//		/// <summary>
//		/// 危重护理的打印信息.
//		/// </summary>
//		[Serializable]			
//		private class clsPrintInfo_ThreeMeasureRecord
//		{
//			public string m_strInPatentID;			
//			public string m_strPatientName;
//			public string m_strSex;
//			public string m_strAge;
//			public string m_strBedName;
//			//public string m_strDeptName;//三测表可能有多个部门(因为要打印转科的所有科室)
//			public string m_strAreaName;	
//			public DateTime m_dtmInPatientDate;
//			public DateTime m_dtmOpenDate;
//
//			public clsThreeMeasureRecordInfo [] m_objRecordArr;
//			public string[] m_strDeptNameArr;
//		}

		#region 在外部测试本打印的演示实例.	
		public void m_mthPrintPage()
		{
			frmPrintPreviewDialogPF frmPreview = new frmPrintPreviewDialogPF();
			frmPreview.m_evtBeginPrint +=new PrintEventHandler(frmPreview_m_evtBeginPrint);
			frmPreview.m_evtEndPrint +=new PrintEventHandler(frmPreview_m_evtEndPrint);
			frmPreview.m_evtPrintContent +=new PrintPageEventHandler(frmPreview_m_evtPrintContent);
			frmPreview.m_evtPrintFrame +=new PrintPageEventHandler(frmPreview_m_evtPrintFrame);
			frmPreview.ShowDialog();
		}
		#region  续打事件
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
		//		clsThreeMeasureRecordPrintTool_New objPrintTool;
		//		private void m_mthDemoPrint_FromDataSource()
		//		{	
		//			objPrintTool=new clsThreeMeasureRecordPrintTool_New();
		//			objPrintTool.m_mthInitPrintTool(null);	
		//			if(m_objBaseCurrentPatient==null)
		//				objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,DateTime.MinValue,DateTime.MinValue);
		//			else 
		//				objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,m_objBaseCurrentPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_DtmInDate,DateTime.MinValue);
		//														
		//			objPrintTool.m_mthInitPrintContent();	
		//							
		//			//保存到文件
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
		//			objPrintTool=new clsThreeMeasureRecordPrintTool_New();
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
		//		protected override long m_lngSubPrint()//代替原窗体中的同名打印函数
		//		{
		//			if(bbb)
		//				m_mthDemoPrint_FromDataSource();
		//			else m_mthDemoPrint_FromFile();
		//			bbb= !bbb;
		//			return 1;
		//		}
		#endregion 在外部测试本打印的演示实例.
	}	
}