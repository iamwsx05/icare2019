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
	/// 小儿危重病例评分打印工具
	/// </summary>
	public class clsBabyInjury_ValuationPrintTool: clsValuationPrintBase
	{
		public clsBabyInjury_ValuationPrintTool()
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
			p_objGrp.DrawString("小儿危重病例评分",m_fotItemHead,m_slbBrush,300,60);
		}

		#region Print Class
		/// <summary>
		/// 项目打印
		/// </summary>
		private class clsPrintValuation: clsPrintValuationInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private BabyInjuryCaseEvaluationDB m_objPrintSubInfo;
			private string[] m_strSpaceArr = {"　","　　","　　　　　　"};
			/// <summary>
			/// 左边距
			/// </summary>
			private int m_intXPos = (int)enmRectangleInfo.LeftX+10;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				m_objPrintSubInfo = m_objPrintInfo as BabyInjuryCaseEvaluationDB;
				if(m_objPrintSubInfo == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}

				p_intPosY += 40;
				m_mthPrintBabyInjury(ref p_intPosY,p_objGrp,p_fntNormalText);
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

			private void m_mthPrintBabyInjury(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY += 15;
				p_objGrp.DrawString("小儿年龄组别：  "+(m_objPrintSubInfo.strAgeGroup == "0"?"小于 1 岁":"大于 1 岁"),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 30;

				p_objGrp.DrawString("心率： " + m_objPrintSubInfo.strHeartRate + "   次/分",p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_objGrp.DrawString("K  ： " + m_objPrintSubInfo.strKPlus + "    mmol/L",p_fntNormalText,Brushes.Black,m_intXPos+350,p_intPosY);
				p_objGrp.DrawString("+",new Font("SimSun",6.75f),Brushes.Black,m_intXPos+360,p_intPosY);
				p_intPosY += 30;
				
				p_objGrp.DrawString("呼吸： " + (m_objPrintSubInfo.strIsrhythmWrong == "1" ?"明显节律不齐" :m_objPrintSubInfo.strBreath + "    次/分"),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_objGrp.DrawString("Na   ： " + m_objPrintSubInfo.strNaPlus + "    mmol/L",p_fntNormalText,Brushes.Black,m_intXPos+350,p_intPosY);
				p_objGrp.DrawString("+",new Font("SimSun",6.75f),Brushes.Black,m_intXPos+370,p_intPosY);
				p_intPosY += 30;

				p_objGrp.DrawString("pH： " + m_objPrintSubInfo.strpH,p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_objGrp.DrawString("Hb： " + m_objPrintSubInfo.strHb + ((m_objPrintSubInfo.strHbSel == "0"?"   g/L":"   g/dl")),p_fntNormalText,Brushes.Black,m_intXPos+350,p_intPosY);
				p_intPosY += 30;

				p_objGrp.DrawString("PaO  ： "+ m_objPrintSubInfo.strPao2 + (m_objPrintSubInfo.strPao0kPaOrmmHgSel == "0"?"   kPa":"   mmHg"),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_objGrp.DrawString("血压（收缩压）： " + m_objPrintSubInfo.strBloodOrShrinkPressure+ (m_objPrintSubInfo.strBldOrShKSel == "0"?"   kPa":"   mmHg"),p_fntNormalText,Brushes.Black,m_intXPos+350,p_intPosY);
				p_objGrp.DrawString("2",new Font("SimSun",6.75f),Brushes.Black,m_intXPos+50,p_intPosY+10);
				p_intPosY += 30;

				p_objGrp.DrawString("胃肠系统： " + m_objPrintSubInfo.strStomachAndIntestines,p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_objGrp.DrawString((m_objPrintSubInfo.strCrOrBUNSel == "0" || m_objPrintSubInfo.strCrOrBUNSel == "1"?"Cr：":"BUN：") + m_objPrintSubInfo.strCrOrBUN + (m_objPrintSubInfo.strCrOrBUNSel == "1" || m_objPrintSubInfo.strCrOrBUNSel == "3"?"   mg/dl":(m_objPrintSubInfo.strCrOrBUNSel == "0"?"   μmol/L":"mmol/L")),p_fntNormalText,Brushes.Black,m_intXPos+350,p_intPosY);
				p_intPosY += 30;
			}
			private void m_mthPrintResult(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				#region 
				p_intPosY += 20;
				p_objGrp.DrawString("小儿危重病例评分结果：",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY);
				p_intPosY += 30;
				m_intXPos += 40;
				int i=0;
				for(;i<=11;i++)
					p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY+30*i,m_intXPos+500,p_intPosY+30*i);
				i--;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY,m_intXPos,p_intPosY+30*i);
				p_objGrp.DrawLine(Pens.Black,m_intXPos+250,p_intPosY,m_intXPos+250,p_intPosY+30*i);
				p_objGrp.DrawLine(Pens.Black,m_intXPos+500,p_intPosY,m_intXPos+500,p_intPosY+30*i);

				p_objGrp.DrawString("   总分",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strTotalEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("心率",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strHeartRateEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("血压（收缩压）",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strBloodPressureOrShrinkPressureEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("呼吸",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strBreathEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("PaO",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString("2",new Font("SimSun",6.75f),Brushes.Black,m_intXPos+30,p_intPosY+14);
				p_objGrp.DrawString(m_objPrintSubInfo.strPao2Eval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("pH",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strpHEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("Na",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString("+",new Font("SimSun",6.75f),Brushes.Black,m_intXPos+20,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strNaPlusEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("K",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString("+",new Font("SimSun",6.75f),Brushes.Black,m_intXPos+10,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strKPlusEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("Cr 或 BUN",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strCrOrBUNEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("Hb",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strRedCellCompOrHbEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("肠胃表现",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strStomachAndintestinesBehaveEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				
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

