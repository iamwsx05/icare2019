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
	/// Ranson智能评分打印工具
	/// </summary>
	public class clsRanson_ValuationPrintTool: clsValuationPrintBase
	{
		public clsRanson_ValuationPrintTool()
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
			p_objGrp.DrawString("Ranson智能评分",m_fotItemHead,m_slbBrush,310,60);
		}

		#region Print Class
		/// <summary>
		/// 项目打印
		/// </summary>
		private class clsPrintValuation: clsPrintValuationInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsEvalInfoOfRansonEvaluation m_objPrintSubInfo;
			private string[] m_strSpaceArr = {"　","　　","　　　　　　"};
			/// <summary>
			/// 左边距
			/// </summary>
			private int m_intXPos = (int)enmRectangleInfo.LeftX+10;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				m_objPrintSubInfo = m_objPrintInfo as clsEvalInfoOfRansonEvaluation;
				if(m_objPrintSubInfo == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}

				p_intPosY += 40;
				m_mthPrintRanson(ref p_intPosY,p_objGrp,p_fntNormalText);
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

			private void m_mthPrintRanson(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY += 15;
				p_objGrp.DrawString("病症类型： " + (m_objPrintSubInfo.strKind == "0" ?"非胆石性胰腺炎":"胆石性胰腺炎"),p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;

				p_objGrp.DrawString("入院前：",p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("白细胞： " + m_objPrintSubInfo.strBXB + " ×10 /L",p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_objGrp.DrawString("血糖： " + m_objPrintSubInfo.strXT + " mg/dl",p_fntNormalText,Brushes.Black,m_intXPos+350,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("乳酸脱氢酶： " + m_objPrintSubInfo.strRST + " IU/L",p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_objGrp.DrawString("天冬氨酸转氨酶： " + m_objPrintSubInfo.strTDA + " IU/L",p_fntNormalText,Brushes.Black,m_intXPos+350,p_intPosY);
				p_intPosY += 30;

				p_objGrp.DrawString("入院后 ",p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("血细胞比容下降：" + m_objPrintSubInfo.strXXB + " %",p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_objGrp.DrawString("血尿素氮升高： " + m_objPrintSubInfo.strXNS + " mg/dl",p_fntNormalText,Brushes.Black,m_intXPos+350,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("血钙： "+  m_objPrintSubInfo.strXG + " mg/dl",p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_objGrp.DrawString("动脉血氧分压： " + m_objPrintSubInfo.strDMX + " mmHg",p_fntNormalText,Brushes.Black,m_intXPos+350,p_intPosY);
				p_intPosY += 30;

				p_objGrp.DrawString("碱缺失：" + m_objPrintSubInfo.strJQS + " mmol/L",p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_objGrp.DrawString("液体潴留： "+  m_objPrintSubInfo.strYTZ + " L",p_fntNormalText,Brushes.Black,m_intXPos+350,p_intPosY);
				p_intPosY += 30;
			}
			private void m_mthPrintResult(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				#region 
				p_intPosY += 20;
				p_objGrp.DrawString("Ranson评分结果：",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY);
				p_intPosY += 30;
				m_intXPos += 40;
				int i=0;
				for(;i<=11;i++)
					p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY+30*i,m_intXPos+500,p_intPosY+30*i);
				i--;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY,m_intXPos,p_intPosY+30*i);
				p_objGrp.DrawLine(Pens.Black,m_intXPos+250,p_intPosY,m_intXPos+250,p_intPosY+30*i);
				p_objGrp.DrawLine(Pens.Black,m_intXPos+500,p_intPosY,m_intXPos+500,p_intPosY+30*i);

				string[] strText = {"病死率","白细胞","血糖","乳酸脱氢酶","天冬氨酸转氨酶","血细胞比容下降","血尿素氮升高","血钙","动脉血养分压","碱缺失","液体潴留"};
				string[] strContent = new String[]{m_objPrintSubInfo.strMortality,m_objPrintSubInfo.strBXB,m_objPrintSubInfo.strXT,m_objPrintSubInfo.strRST,m_objPrintSubInfo.strTDA
												  ,m_objPrintSubInfo.strXXB,m_objPrintSubInfo.strXNS,m_objPrintSubInfo.strXG,m_objPrintSubInfo.strDMX,m_objPrintSubInfo.strJQS,m_objPrintSubInfo.strYTZ};
				int[] intValue1 = new Int32[]{0,16,220,350,250,10,5,8,60,4,6};
				int[] intValue2 = new Int32[]{0,18,220,400,250,10,2,8,60,5,4};
				string strAge = m_objPrintSubInfo.strKind;
				for(int j2=0;j2<strText.Length;j2++)
				{
					p_objGrp.DrawString(strText[j2],p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
					if(j2 == 0)
						p_objGrp.DrawString(strContent[0],p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
					else if(j2 == 7 || j2 == 8)
						p_objGrp.DrawString((strAge == "0"?( m_intrGetIntValue(strContent[j2]) < intValue1[j2]?"符合":"不符合"):( m_intrGetIntValue(strContent[j2]) < intValue2[j2]?"符合":"不符合")),p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
					else
						p_objGrp.DrawString((strAge == "0"?( m_intrGetIntValue(strContent[j2]) > intValue1[j2]?"符合":"不符合"):( m_intrGetIntValue(strContent[j2]) > intValue2[j2]?"符合":"不符合")),p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
					p_intPosY += 30;
				}			
				#endregion
			}
			private int m_intrGetIntValue(string p_strValue)
			{
				int intValue = 0;
				try
				{
					intValue = int.Parse(p_strValue);
				}
				catch
				{
					return 0;
				}
				return intValue;
			}
		}
		#endregion

	}
}
