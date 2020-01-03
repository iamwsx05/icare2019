using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.Utility.Controls;
using System.Drawing;
using com.digitalwave.Utility.Controls;

namespace iCare.ICU.Evaluation
{
	/// <summary>
	/// CT智能评分
	/// </summary>
	public class clsCT_ValuationPrintTool: clsValuationPrintBase
	{
		public clsCT_ValuationPrintTool()
		{}
		protected override void m_mthSetPrintLineArr()
		{
			m_objPrintLineContext = new clsPrintContext(
				new clsPrintLineBase[]{
										  new clsPrintPatientFixInfo(),
										  new clsPrintValuation()
									  });
		}
		protected override void m_mthPrintSubTitle(System.Drawing.Graphics p_objGrp)
		{
			p_objGrp.DrawString("CT智能评分",m_fotItemHead,m_slbBrush,330,60);
		}

		#region Print Class
		/// <summary>
		/// 项目打印
		/// </summary>
		private class clsPrintValuation: clsPrintValuationInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsEvalInfoOfCTEvaluation m_objPrintSubInfo;
			private string[] m_strSpaceArr = {"　","　　","　　　　　　"};
			/// <summary>
			/// 左边距
			/// </summary>
			private int m_intXPos = (int)enmRectangleInfo.LeftX+10;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				m_objPrintSubInfo = m_objPrintInfo as clsEvalInfoOfCTEvaluation;
				if(m_objPrintSubInfo == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}

				p_intPosY += 40;
				m_mthPrintCT(ref p_intPosY,p_objGrp,p_fntNormalText);
				p_intPosY += 30;
				p_objGrp.DrawString("评分日期：" + DateTime.Parse(m_objContent.m_strActivityTime).ToString("yyyy年MM月dd日") + m_strSpaceArr[2] + "评估者：" + new clsEmployee(m_objPrintSubInfo.strEvalDoctorID).m_StrFirstName,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+50,p_intPosY);
				p_intPosY += 30;
				m_mthPrintResult(ref p_intPosY,p_objGrp,p_fntNormalText);
			
				m_blnHaveMoreLine = false;
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();

				m_blnHaveMoreLine = true;
			}

			private	string[] strDHS = {"<2.0","2.0 - 3.0", ">3.0"} ;
			private	string[] strBDB = {">35","30 - 35","<30"};
			private	string[] strFS = {"无","容易控制","较难控制"};
			private	string[] strYYZ = {"良好","较好","较差 - 消耗"} ;
			private	string[] strSJX = {"无","较轻","深度昏迷"};
			private void m_mthPrintCT(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY += 15;
				p_objGrp.DrawString("胆红素： " + m_strGetGCSValue(strDHS,m_objPrintSubInfo.strDHS) + " μmol/L",p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("白蛋白： " + m_strGetGCSValue(strBDB,m_objPrintSubInfo.strBDB) +"  g/L",p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;

				p_objGrp.DrawString("腹水： "+ m_strGetGCSValue(strFS,m_objPrintSubInfo.strFS),p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("神经系统障碍： "+ m_strGetGCSValue(strSJX,m_objPrintSubInfo.strSJX),p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;

				p_objGrp.DrawString("营养状况： " + m_strGetGCSValue(strYYZ,m_objPrintSubInfo.strYYZ),p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				
			}
			private void m_mthPrintResult(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				#region 
				p_intPosY += 20;
				p_objGrp.DrawString("CT评分结果：",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY);
				p_intPosY += 30;
				m_intXPos += 40;
				int i=0;
				for(;i<=6;i++)
					p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY+30*i,m_intXPos+500,p_intPosY+30*i);
				i--;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY,m_intXPos,p_intPosY+30*i);
				p_objGrp.DrawLine(Pens.Black,m_intXPos+250,p_intPosY,m_intXPos+250,p_intPosY+30*i);
				p_objGrp.DrawLine(Pens.Black,m_intXPos+500,p_intPosY,m_intXPos+500,p_intPosY+30*i);

				p_objGrp.DrawString("   总分",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strScore,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("胆红素",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_strGetGCSValue(strDHS,m_objPrintSubInfo.strDHS),p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("白蛋白",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_strGetGCSValue(strBDB,m_objPrintSubInfo.strBDB),p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("腹水",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_strGetGCSValue(strFS,m_objPrintSubInfo.strFS),p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("神经系统障碍",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_strGetGCSValue(strSJX,m_objPrintSubInfo.strSJX),p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("营养状况",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_strGetGCSValue(strYYZ,m_objPrintSubInfo.strYYZ),p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				#endregion
			}
			private string m_strGetGCSValue(string[] p_strValueArr,string p_strIndex)
			{
				string strValue = "";
				try
				{
					strValue = p_strValueArr[int.Parse(p_strIndex)];
				}
				catch
				{
					return "";
				}
				return strValue;
			}
		}
		#endregion

	}
}

