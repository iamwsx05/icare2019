using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using System.Drawing;

using com.digitalwave.Utility.Controls;

namespace iCare
{
	/// <summary>
	/// Summary description for clsLabAnalysisReportPrintTool.
	/// </summary>
	public class clsLabAnalysisReportPrintTool //: infPrintRecord
	{
		
		private clsPrintInfo_LabAnalysisReport m_objPrintInfo;

		public clsLabAnalysisReportPrintTool()
		{
			m_objPrintInfo = new clsPrintInfo_LabAnalysisReport();
		}

		#region Interface Function Impletement
		public void m_mthBeginPrint(object p_objPrintArg)
		{

		}

		public void m_mthDisposePrintTools(object p_objArg)
		{
		
		}

		public void m_mthEndPrint(object p_objPrintArg)
		{
		
		}

		public void m_mthInitPrintContent()
		{
			
			
			if(m_objPrintInfo.m_strBarCode == "")
				m_objPrintInfo.m_objJY_BRZL = null;				
			else
			{
				clsLabAnalysisOrderDomain objLabAnalysisReportDomain = new clsLabAnalysisOrderDomain();
				
				
				long lngRes = objLabAnalysisReportDomain.m_lngGetReportInfomation(m_objPrintInfo.m_strBarCode
																				,out m_objPrintInfo.m_objJY_BRZL
																				,out m_objPrintInfo.m_objJY_JGArr
																				,out m_objPrintInfo.m_objJY_QXArr
																				,out m_objPrintInfo.m_objJY_DYArr);
				if(lngRes <= 0)
					return ;   
			}
		}

		public void m_mthInitPrintTool(object p_objArg)
		{
		
		}

		public void m_mthPrintPage(object p_objPrintArg)
		{
			PrintPageEventArgs e = (PrintPageEventArgs)p_objPrintArg;

			e.HasMorePages =false;

			m_mthPrintHeader(e);

			m_mthPrintDetail(e);

			m_mthPrintFooter(e);
		}

		public void m_mthSetPrintContent(object p_objPrintContent)
		{
			m_objPrintInfo = (clsPrintInfo_LabAnalysisReport)p_objPrintContent;
			
		}

		public void m_mthSetPrintInfo(string p_strBarCode)
		{
			if(p_strBarCode != null && p_strBarCode != "")
				m_objPrintInfo.m_strBarCode = p_strBarCode;



		}

		public object m_objGetPrintInfo()
		{
			return null;
		}
		#endregion


		#region Private Function
		/// <summary>
		/// ��ӡ��ͷ
		/// </summary>
		/// <param name="p_objPrintArg"></param>
		private void m_mthPrintHeader(PrintPageEventArgs e)
		{
			Font fntTitle = new Font("SimSun", 18,FontStyle.Bold );
			Font fntPrintDate = new Font("SimSun", 10);
			Font fntText = new Font("SimSun", 12);

			Font fntBarCode = new System.Drawing.Font("3 of 9 Barcode", 18f, FontStyle.Regular);//, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));

			//����ʾBarCode
//			string strBarCode = "";
//			if(m_objPrintInfo.m_strBarCode != null && m_objPrintInfo.m_strBarCode != "") 
//			{
//				strBarCode = "*" + m_objPrintInfo.m_strBarCode + "*";
//				e.Graphics.DrawString(strBarCode,fntBarCode ,Brushes.Black,40 , 30);
//			}

			e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle+"���鱨�浥",fntTitle,Brushes.Black,230,30);

			e.Graphics.DrawString(((m_objPrintInfo.m_objJY_BRZL == null) ? "" : ("("+m_objPrintInfo.m_objJY_BRZL.m_strPat_c_name+")")),fntText,Brushes.Black,360,60);
			e.Graphics.DrawString("��ӡ���ڣ�" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),fntPrintDate,Brushes.Black,530,60);

			e.Graphics.DrawString("������" + ((m_objPrintInfo.m_objJY_BRZL == null) ? "" : (m_objPrintInfo.m_objJY_BRZL.m_strPat_name)),fntText,Brushes.Black,(int)enmRectangleInfo.LeftX,(int)enmRectangleInfo.TopY);

			e.Graphics.DrawString("�Ա�" + ((m_objPrintInfo.m_objJY_BRZL == null) ? "" : (m_objPrintInfo.m_objJY_BRZL.m_strPat_sex)),fntText,Brushes.Black,(int)enmRectangleInfo.LeftX + 200,(int)enmRectangleInfo.TopY);

			e.Graphics.DrawString("�ꡡ�䣺" + ((m_objPrintInfo.m_objJY_BRZL == null) ? "" : (m_objPrintInfo.m_objJY_BRZL.m_strPat_age)),fntText,Brushes.Black,(int)enmRectangleInfo.LeftX + 400,(int)enmRectangleInfo.TopY);

			e.Graphics.DrawString("�����ţ�" + ((m_objPrintInfo.m_objJY_BRZL == null) ? "" : (m_objPrintInfo.m_objJY_BRZL.m_strPat_sid)),fntText,Brushes.Black,(int)enmRectangleInfo.LeftX + 600,(int)enmRectangleInfo.TopY);

			e.Graphics.DrawString("�Ʊ�" + ((m_objPrintInfo.m_objJY_BRZL == null) ? "" : (m_objPrintInfo.m_objJY_BRZL.m_strPat_d_name)),fntText,Brushes.Black,(int)enmRectangleInfo.LeftX ,(int)enmRectangleInfo.TopY + 30);

			e.Graphics.DrawString("���ţ�" + ((m_objPrintInfo.m_objJY_BRZL == null) ? "" : (m_objPrintInfo.m_objJY_BRZL.m_strPat_bed_no)),fntText,Brushes.Black,(int)enmRectangleInfo.LeftX + 200,(int)enmRectangleInfo.TopY + 30);

			e.Graphics.DrawString("�����ţ�"+ ((m_objPrintInfo.m_objJY_BRZL == null) ? "" : (m_objPrintInfo.m_objJY_BRZL.m_strPat_in_no)) ,fntText,Brushes.Black,(int)enmRectangleInfo.LeftX + 400,(int)enmRectangleInfo.TopY + 30);

			e.Graphics.DrawString("�ͼ��ߣ�" + ((m_objPrintInfo.m_objJY_BRZL == null) ? "" : (m_objPrintInfo.m_objJY_BRZL.m_strPat_doct)) ,fntText,Brushes.Black,(int)enmRectangleInfo.LeftX + 600,(int)enmRectangleInfo.TopY + 30);

			e.Graphics.DrawString("�����ߣ�" + ((m_objPrintInfo.m_objJY_BRZL == null) ? "" : (m_objPrintInfo.m_objJY_BRZL.m_strPat_I_name)),fntText,Brushes.Black,(int)enmRectangleInfo.LeftX ,(int)enmRectangleInfo.TopY + 60);

			e.Graphics.DrawString("��ϣ�" + ((m_objPrintInfo.m_objJY_BRZL == null) ? "" : (m_objPrintInfo.m_objJY_BRZL.m_strPat_diag)),fntText,Brushes.Black,(int)enmRectangleInfo.LeftX + 200,(int)enmRectangleInfo.TopY + 60);

			e.Graphics.DrawString("����ߣ�" + ((m_objPrintInfo.m_objJY_BRZL == null) ? "" : (m_objPrintInfo.m_objJY_BRZL.m_strPat_chk)),fntText,Brushes.Black,(int)enmRectangleInfo.LeftX + 600,(int)enmRectangleInfo.TopY + 60);


			m_mthPrintOneHorizontalLine(e, (int)enmRectangleInfo.TopY + 80);

			e.Graphics.DrawString(((m_objPrintInfo.m_objJY_BRZL == null) ? "�Ŀ" : (m_objPrintInfo.m_objJY_BRZL.m_strPat_c_name)), fntText, Brushes.Black,(int)enmRectangleInfo.LeftX + 50,(int)enmRectangleInfo.TopY + 90);
			e.Graphics.DrawString("��  ��", fntText, Brushes.Black,(int)enmRectangleInfo.LeftX + 280,(int)enmRectangleInfo.TopY + 90);
			e.Graphics.DrawString("��  λ", fntText, Brushes.Black,(int)enmRectangleInfo.LeftX + 450,(int)enmRectangleInfo.TopY + 90);
			e.Graphics.DrawString("�� �� ֵ", fntText, Brushes.Black,(int)enmRectangleInfo.LeftX + 560,(int)enmRectangleInfo.TopY + 90);

			m_mthPrintOneHorizontalLine(e,(int)enmRectangleInfo.TopY + 110);

			e.Graphics.DrawString("�걾���" + ((m_objPrintInfo.m_objJY_BRZL == null) ? "" : (m_objPrintInfo.m_objJY_BRZL.m_strPat_s_name)), fntText, Brushes.Black,(int)enmRectangleInfo.LeftX ,(int)enmRectangleInfo.TopY + 120);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintDetail(PrintPageEventArgs e)
		{
			Font fntText = new Font("SimSun", 12);

			if(m_objPrintInfo.m_objJY_JGArr == null || m_objPrintInfo.m_objJY_JGArr.Length == 0)
				return ;

			int intYPos = (int)enmRectangleInfo.TopY + 150;

			for(int i = 0; i < m_objPrintInfo.m_objJY_JGArr.Length; i++)
			{
				if(m_objPrintInfo.m_objJY_JGArr[i] == null)
					continue;

				e.Graphics.DrawString(m_objPrintInfo.m_objJY_JGArr[i].m_strRes_name + "  (" + m_objPrintInfo.m_objJY_JGArr[i].m_strRes_it_ecd + ")", fntText, Brushes.Black,(int)enmRectangleInfo.LeftX ,intYPos);
				e.Graphics.DrawString(m_objPrintInfo.m_objJY_JGArr[i].m_strRes_exp + " "+ m_objPrintInfo.m_objJY_JGArr[i].m_strRes_chr+ " "+ m_objPrintInfo.m_objJY_JGArr[i].m_strRes_chr1, fntText, Brushes.Black,(int)enmRectangleInfo.LeftX + 250,intYPos);
				//e.Graphics.DrawString(m_objPrintInfo.m_objJY_JGArr[i].m_strRes_chr, fntText, Brushes.Black,(int)enmRectangleInfo.LeftX + 280,intYPos);
				e.Graphics.DrawString(m_objPrintInfo.m_objJY_JGArr[i].m_strRes_unit, fntText, Brushes.Black,(int)enmRectangleInfo.LeftX + 450,intYPos);
				e.Graphics.DrawString(m_objPrintInfo.m_objJY_JGArr[i].m_strRes_ref1 
					+ (m_objPrintInfo.m_objJY_JGArr[i].m_strRes_ref2.Trim()==""? "": " - " + m_objPrintInfo.m_objJY_JGArr[i].m_strRes_ref2) 
					+ (m_objPrintInfo.m_objJY_JGArr[i].m_strRes_ref3.Trim()==""? "": " - " + m_objPrintInfo.m_objJY_JGArr[i].m_strRes_ref3)
					+ (m_objPrintInfo.m_objJY_JGArr[i].m_strRes_ref4.Trim()==""? "": " - " + m_objPrintInfo.m_objJY_JGArr[i].m_strRes_ref4) , fntText, Brushes.Black,(int)enmRectangleInfo.LeftX + 560,intYPos);

				intYPos += 30;

				//��ҳ
				if(intYPos >= e.PageBounds.Bottom - 60 && i < m_objPrintInfo.m_objJY_JGArr.Length)
				{
					e.HasMorePages = true;
					m_mthPrintHeader(e);

					intYPos = (int)enmRectangleInfo.TopY + 150;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		private void m_mthPrintFooter(PrintPageEventArgs e)
		{
			Font fntText = new Font("SimSun", 12);

			m_mthPrintOneHorizontalLine(e, e.PageBounds.Bottom - 60);//m_objPrintInfo.m_objJY_JGArr[i].m_strRes_ref1

			e.Graphics.DrawString("�ͼ����ڣ�" + ((m_objPrintInfo.m_objJY_BRZL == null) ? "" : (m_objPrintInfo.m_objJY_BRZL.m_dtmPat_sdate).ToString("yyyy-MM-dd")),fntText,Brushes.Black,(int)enmRectangleInfo.LeftX, e.PageBounds.Bottom - 50);
			e.Graphics.DrawString("�ͼ�ʱ�䣺" + ((m_objPrintInfo.m_objJY_BRZL == null) ? "" : (m_objPrintInfo.m_objJY_BRZL.m_dtmPat_sdate).ToString("HH:mm:ss")),fntText,Brushes.Black,(int)enmRectangleInfo.LeftX + 200, e.PageBounds.Bottom - 50);
			e.Graphics.DrawString("�������ڣ�" + ((m_objPrintInfo.m_objJY_JGArr == null || m_objPrintInfo.m_objJY_JGArr.Length==0 || m_objPrintInfo.m_objJY_JGArr[0]==null) ? "" : (m_objPrintInfo.m_objJY_JGArr[0].m_dtmRes_date).ToString("yyyy-MM-dd"))  ,fntText,Brushes.Black,(int)enmRectangleInfo.LeftX + 400, e.PageBounds.Bottom - 50);
			e.Graphics.DrawString("����ʱ�䣺" + ((m_objPrintInfo.m_objJY_JGArr == null || m_objPrintInfo.m_objJY_JGArr.Length==0 || m_objPrintInfo.m_objJY_JGArr[0]==null) ? "" : (m_objPrintInfo.m_objJY_JGArr[0].m_dtmRes_date).ToString("HH:mm:ss")),fntText,Brushes.Black,(int)enmRectangleInfo.LeftX + 600,e.PageBounds.Bottom - 50);
		}


		/// <summary>
		/// ��ӡһ��ˮƽ��
		/// </summary>
		/// <param name="e"></param>
		/// <param name="p_intBottomY"></param>
		private void m_mthPrintOneHorizontalLine(System.Drawing.Printing.PrintPageEventArgs e,int p_intBottomY)
		{	
			Pen LinePen = new Pen(Color.Black);

			e.Graphics.DrawLine(LinePen,(int)enmRectangleInfo.LeftX ,
				p_intBottomY,
				(int)enmRectangleInfo.RightX,
				p_intBottomY);			
		}
		#endregion

//		#region ö�ٺ���
//		/// <summary>
//		/// ���ӵ���Ϣ
//		/// </summary>
//		public enum enmRectangleInfo
//		{
//			/// <summary>
//			/// ���ӵĶ���
//			/// </summary>
//			TopY = 100,
//			///<summary>
//			/// ���ӵ����
//			/// </summary>
//			LeftX = 40,
//			/// <summary>
//			/// ���ӵ��Ҷ�
//			/// </summary>
//			RightX = 820-40,
//			/// <summary>
//			/// ����ÿ�еĲ���
//			/// </summary>
//			RowStep = 20,
//			SmallRowStep=20,			
//
//			ColumnsMark1=110,			
//
//			/// <summary>
//			/// �׻���ƫ���ı�����ľ���
//			/// </summary>
//			BottomLineShift=15,
//
//			BottomY=1024		
//		}
//
//
//		[Serializable]
//		/// <summary>
//		/// 
//		/// </summary>
//		private class clsPrintInfo_LabAnalysisReport
//		{
////			public string m_strInPatentID;			
////			public string m_strPatientName;
////			public string m_strSex;
////			public string m_strAge;
////			public string m_strBedName;
////			public string m_strDeptName;
////			public string m_strAreaName;
//
//			public string m_strBarCode;
//
//			public clsJY_BRZL m_objJY_BRZL;
//			public clsJY_JG[] m_objJY_JGArr;
//			public clsJY_QXJG[] m_objJY_QXArr;
//			public clsJY_DYJG[] m_objJY_DYArr;
//		}
//		#endregion
	}
}
