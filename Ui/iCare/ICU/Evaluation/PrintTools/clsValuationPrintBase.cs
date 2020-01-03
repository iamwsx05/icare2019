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
		/// 设置打印信息(当从数据库读取时要首先调用.)
		/// </summary>
		/// <param name="p_objPatient">病人</param>
		/// <param name="p_dtmInPatientDate">入院日期</param>
		/// <param name="p_dtmOpenDate">OpenDate，如果是一次打印多次记录表单的类型（如病案记录），忽略OpenDate</param>
		public void m_mthSetPrintInfo(clsPatient p_objPatient,object p_objContent,DateTime p_dtmCreateDate)
		{	
			if(p_objPatient == null)
			{
				MessageBox.Show("请先选择病人！");
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
		/// 从数据库初始化打印内容。如果没有记录，打印空报表。(当从数据库读取时要调用.)
		/// </summary>
		public void m_mthInitPrintContent()
		{	
			m_blnWantInit = false;

			if(m_objContent == null)
			{
				MessageBox.Show("调用m_mthInitPrintContent之前请首先调用m_mthSetPrintInfo函数");
				return;
			}
		
			//设置表单内容到打印中,即使是打印空白单,此行也必须执行.(即:在本函数内部,此行之上不准有return语句,除非出错跳出.)
			m_mthSetPrintContent(m_objContent);		
				
		}

		/// <summary>
		/// 获取打印内容,(当从数据库读取时,调用本函数之前请首先调用m_mthSetPrintInfo函数)
		/// </summary>
		/// <returns>打印内容</returns>
		public object m_objGetPrintInfo()
		{	
			if(m_objContent==null)
			{
				MessageBox.Show("当从数据库读取时,调用m_objGetPrintInfo之前请首先调用m_mthSetPrintInfo函数");
				return null;
			}

			if(m_blnWantInit)
				m_mthInitPrintContent();	
		
			//没有记录内容时，返回空
			if(m_objContent.m_objContent == null)
				return null;
			else
				return m_objContent;
		}		

		/// <summary>
		/// 初始化打印变量,本例传入空对象即可.
		/// </summary>
		public void m_mthInitPrintTool(object p_objArg)
		{				
		#region 有关打印初始化
				
			m_fotTitleFont = new Font("SimSun", 16,FontStyle.Bold);
			m_fotHeaderFont = new Font("SimSun", 18,FontStyle.Bold);
			m_fotItemHead = new Font("",13,FontStyle.Bold);
			m_fotSmallFont = new Font("SimSun",12);
			m_GridPen = new Pen(Color.Black,2);
			m_slbBrush = new SolidBrush(Color.Black);
			m_objPageSetting = new clsPrintPageSettingForRecord();

		
		#endregion 有关打印初始化
		}

		/// <summary>
		/// 释放打印变量
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
			m_mthEndPrintSub((PrintEventArgs)p_objPrintArg);
		}


		#region 打印

		// 设置打印内容。
		public  void m_mthSetPrintContent(object p_objContent)
		{
			m_mthSetPrintLineArr();
			//设置打印信息，就是Set Value进去
			m_objPrintLineContext.m_ObjPrintLineInfo = p_objContent;
		}

		/// <summary>
		/// 设置打印行
		/// </summary>
		protected virtual void m_mthSetPrintLineArr()
		{
		}

#region 有关打印的声明

		/// <summary>
		/// 打印所有行内容
		/// </summary>
		protected clsPrintContext m_objPrintLineContext;	

		/// <summary>
		/// 打印边框的左边距
		/// </summary>
		private const int m_intRecBaseX = clsPrintPosition.c_intLeftX;
		private int m_intCurrentPage = 1;
		/// <summary>
		/// 标题的字体(20 bold)
		/// </summary>
		private Font m_fotTitleFont;
		/// <summary>
		/// 表头的字体(14 )
		/// </summary>
		private Font m_fotHeaderFont;
		/// <summary>
		/// 大项目的标题，如体格检查
		/// </summary>
		public static Font m_fotItemHead;
		/// <summary>
		/// 表内容的字体(11)
		/// </summary>
		private Font m_fotSmallFont;
		/// <summary>
		/// 边框画笔
		/// </summary>
		private Pen m_GridPen;
		/// <summary>
		/// 刷子
		/// </summary>
		protected SolidBrush m_slbBrush;
		/// <summary>
		/// 当前打印位置（Y）
		/// </summary>
		private int m_intYPos=150 ;//= (int)enmRectangleInfo.TopY+5;
	
		/// <summary>
		/// 格子的信息
		/// </summary>
		public enum enmRectangleInfo
		{

			/// <summary>
			/// 格子的顶端
			/// </summary>
			TopY = 150,
			///<summary>
			/// 格子的左端
			/// </summary>
			LeftX = clsPrintPosition.c_intLeftX,
			/// <summary>
			/// 格子的右端
			/// </summary>
			RightX = clsPrintPosition.c_intRightX,
			/// <summary>
			/// 格子每行的步长
			/// </summary>
			RowStep = 25,
			SmallRowStep=25,
			/// <summary>
			/// 格子的行数
			/// </summary>
			RowLinesNum = 32,

			ColumnsMark1=35,

			/// <summary>
			/// CheckBox偏移右边文本的距离
			/// </summary>
			CheckShift=15,

			/// <summary>
			/// 底划线偏移文本顶点的距离
			/// </summary>
			BottomLineShift=15,

			BottomY=1025

		}

#endregion

		/// <summary>
		/// 打印元素
		/// </summary>
		private enum enmItemDefination
		{
			//基本元素
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
			//填充表格元素
			RecordDate,
			RecordTime,
			RecordContent,
			RecordSign1,
			RecordSign2

		}
		/// <summary>
		/// 获取坐标的类
		/// </summary>
		private clsPrintPageSettingForRecord m_objPageSetting;

#region 定义打印各元素的坐标点
		protected class clsPrintPageSettingForRecord
		{	
			public clsPrintPageSettingForRecord(){}
	
			/// <summary>
			/// 获得坐标点
			/// </summary>
			/// <param name="p_intItemName">项目名称</param>
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
		/// 格子的信息
		/// </summary>
		protected enum enmRectangleInfoInPatientCaseInfo 
		{
			/// <summary>
			/// 格子的顶端
			/// </summary>
			TopY = 140,

			///<summary>
			/// 格子的左端
			/// </summary>
			LeftX = 16,

			/// <summary>
			/// 格子的右端
			/// </summary>
			RightX = 180+17,
			/// <summary>
			/// 格子每行的步长
			/// </summary>
			RowStep = 7,
			SmallRowStep=20,
			/// <summary>
			/// 格子的行数
			/// </summary>
			RowLinesNum = 32,

			ColumnsMark1=35,

			/// <summary>
			/// CheckBox偏移右边文本的距离
			/// </summary>
			CheckShift=15,

			/// <summary>
			/// 底划线偏移文本顶点的距离
			/// </summary>
			BottomLineShift=15,

			BottomY=1024,

			PrintWidth = 670,
			PrintWidth2 = 710,

		}


		// 打印开始后，在打印页之前的操作
		private void m_mthBeginPrintSub(PrintEventArgs p_objPrintArg)
		{

		}

		// 打印页
		private void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
		{
			m_mthPrintTitleInfo(p_objPrintPageArg);
			m_mthPrintHeader(p_objPrintPageArg); 

			while(m_objPrintLineContext.m_BlnHaveMoreLine)
			{
				//还有数据打印
				m_objPrintLineContext.m_mthPrintNextLine(ref m_intYPos,p_objPrintPageArg.Graphics,m_fotSmallFont);

				if(m_intYPos > p_objPrintPageArg.PageBounds.Height-230
					&& m_objPrintLineContext.m_BlnHaveMoreLine)
				{
					//还有数据打印，但需要换页

					m_mthPrintFoot(p_objPrintPageArg);

					p_objPrintPageArg.HasMorePages = true;

					m_intYPos = 150;

					m_intCurrentPage++;

					return;
				}				
			}

			//全部打完			

//			m_mthPrintFoot(p_objPrintPageArg);

		}


		#region PrintClasses

		protected abstract class clsPrintValuationInfo : clsPrintLineBase
		{
			protected clsValuationPrintContent m_objContent;
			/// <summary>
			/// 文字距离左边的边距
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
					p_objGrp.DrawString("√",p_fntNormalText,Brushes.Black,p_intPosX+1,p_intPosY);
			}
			protected void m_mthDrawCheckBox(int p_intPosX,int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText,string p_strText)
			{
				p_objGrp.DrawString("√ " + p_strText,p_fntNormalText,Brushes.Black,p_intPosX+1,p_intPosY);
				p_objGrp.DrawRectangle(new Pen(Color.Black),p_intPosX,p_intPosY,15,15);
			}
		}
		/// <summary>
		/// 打印第一页的固定内容
		/// </summary>
		protected class clsPrintPatientFixInfo : clsPrintValuationInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_objGrp.DrawString("姓名："+ m_objContent.m_strName,p_fntNormalText,Brushes.Black,m_intPatientInfoX+40,p_intPosY);
				p_objGrp.DrawString("性别："+ m_objContent.m_strSex ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+240,p_intPosY);		
				p_objGrp.DrawString("年龄："+m_objContent.m_strAge,p_fntNormalText,Brushes.Black,m_intPatientInfoX+440,p_intPosY);
		
				p_intPosY += 30;
				p_objGrp.DrawString("科别："+m_objContent.m_strDeptName ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+40,p_intPosY);
				p_objGrp.DrawString("病区："+m_objContent.m_strAreaName ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+240,p_intPosY);
				p_objGrp.DrawString("床位："+m_objContent.m_strBedName ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+440,p_intPosY);
				
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

		#region 标题文字部分
		/// <summary>
		/// 打印页脚
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintFoot(System.Drawing.Printing.PrintPageEventArgs e)
		{
			Font fntHeader = new Font("SimSun", 12);

			e.Graphics.DrawString("第      页",fntHeader,Brushes.Black,385,e.PageBounds.Height-105);
			e.Graphics.DrawString(m_intCurrentPage.ToString(),fntHeader,Brushes.Black,425,e.PageBounds.Height-105);			
		}
		
		/// <summary>
		/// 打印边
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
		{			
			Font fntTitle = new Font("SimSun", 18,FontStyle.Bold );
			Font fntHeader = new Font("SimSun", 12);

			e.Graphics.DrawRectangle(Pens.Black,m_intRecBaseX - 10,135,(int)enmRectangleInfo.RightX - (int)enmRectangleInfo.LeftX + 10,e.PageBounds.Height-250);
		}


		/// <summary>
		/// 标题文字部分
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{
			e.Graphics.DrawString("住院号：",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID_Title ));
			e.Graphics.DrawString(m_objContent.m_strPatientID ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.InPatientID ));

			e.Graphics.DrawString("评分日期：",m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.RecordDate ));
			e.Graphics.DrawString(DateTime.Parse( m_objContent.m_strActivityTime).ToString("yyyy年MM月dd日 HH:mm:ss") ,m_fotSmallFont,m_slbBrush,m_objPageSetting.m_getCoordinatePoint((int)enmItemDefination.RecordTime ));
			
			m_mthPrintSubTitle(e.Graphics);
		}
		/// <summary>
		/// 具体评分表单标题
		/// </summary>
		/// <param name="p_objGrp"></param>
		protected virtual void m_mthPrintSubTitle(System.Drawing.Graphics p_objGrp)
		{
		}
	#endregion

		// 打印结束时的操作
		private void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
		{		
			m_mthResetWhenEndPrint();
		}

		/// <summary>
		/// 每次打印结束之后的复位,无论是打印当前页或者打印全部.
		/// </summary>
		private void m_mthResetWhenEndPrint()
		{
			m_objPrintLineContext.m_mthReset();

			m_intYPos = 145;

			m_intCurrentPage = 1;
		}

	#endregion 打印

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
