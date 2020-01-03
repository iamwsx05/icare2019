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
	/// 改良Glasgow昏迷智能评分打印工具
	/// </summary>
	public class clsGlasgow_ValuationPrintTool: clsValuationPrintBase
	{
		public clsGlasgow_ValuationPrintTool()
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
			p_objGrp.DrawString("改良Glasgow昏迷智能评分",m_fotItemHead,m_slbBrush,300,60);
		}

		#region Print Class
		/// <summary>
		/// 项目打印
		/// </summary>
		private class clsPrintValuation: clsPrintValuationInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsImproveGlasgowComaEvaluation m_objPrintSubInfo;
			private string[] m_strSpaceArr = {"　","　　","　　　　　　"};
			/// <summary>
			/// 左边距
			/// </summary>
			private int m_intXPos = (int)enmRectangleInfo.LeftX+10;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				m_objPrintSubInfo = m_objPrintInfo as clsImproveGlasgowComaEvaluation;
				if(m_objPrintSubInfo == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}

				p_intPosY += 40;
				m_mthPrintGlasgow(ref p_intPosY,p_objGrp,p_fntNormalText);
				p_intPosY += 30;
				p_objGrp.DrawString("评分日期：" + DateTime.Parse(m_objContent.m_strActivityTime).ToString("yyyy年MM月dd日") + m_strSpaceArr[2] + "评估者：" + new clsEmployee(m_objPrintSubInfo.m_strEvalDoctorID).m_StrFirstName,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+50,p_intPosY);
				p_intPosY += 30;
				m_mthPrintResult(ref p_intPosY,p_objGrp,p_fntNormalText);
			
				m_blnHaveMoreLine = false;
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();

				m_blnHaveMoreLine = true;
			}

			private void m_mthPrintGlasgow(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY += 15;
				string[] strAge = {"1岁以下","1岁～2岁","2岁～3岁","3岁～4岁","4岁～5岁","5岁以上"};
				p_objGrp.DrawString("年龄组：  " + m_strGetGCSValue(strAge,m_objPrintSubInfo.m_strAgeGroup),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("Ⅰ.睁眼： "+ (m_objPrintSubInfo.m_strAgeGroup == "0" ?m_objPrintSubInfo.m_strOpenEyeU1.Substring(2):m_objPrintSubInfo.m_strOpenEyeO1.Substring(2)),p_fntNormalText,Brushes.Black,m_intXPos+40,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("Ⅱ.运动反应： "+ (m_objPrintSubInfo.m_strAgeGroup == "0" ?m_objPrintSubInfo.m_strSportFeedbackU1.Substring(2):m_objPrintSubInfo.m_strSportFeedbackO1.Substring(2)),p_fntNormalText,Brushes.Black,m_intXPos+40,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("Ⅲ.语言反应： "+ (m_objPrintSubInfo.m_strAgeGroup == "0"|| m_objPrintSubInfo.m_strAgeGroup == "1" ?m_objPrintSubInfo.m_strTalkFeedbackU2.Substring(2):(m_objPrintSubInfo.m_strAgeGroup == "5" ?m_objPrintSubInfo.m_strTalkFeedbackO5.Substring(2):m_objPrintSubInfo.m_strTalkFeedbackU5.Substring(2))),p_fntNormalText,Brushes.Black,m_intXPos+40,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("Ⅳ.瞳孔光反应： " + m_objPrintSubInfo.m_strPupilLightFeedback.Substring(2),p_fntNormalText,Brushes.Black,m_intXPos+40,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("Ⅴ.脑干反射： "+ m_objPrintSubInfo.m_strBrainReflect.Substring(2),p_fntNormalText,Brushes.Black,m_intXPos+40,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("Ⅵ.抽搐： "+ m_objPrintSubInfo.m_strTwitch.Substring(2),p_fntNormalText,Brushes.Black,m_intXPos+40,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("Ⅶ.自发性呼吸： "+ m_objPrintSubInfo.m_strSpontaneityBreath.Substring(2),p_fntNormalText,Brushes.Black,m_intXPos+40,p_intPosY);
				p_intPosY += 30;
			}
			private void m_mthPrintResult(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				#region 
				p_intPosY += 20;
				p_objGrp.DrawString("改良Glasgow昏迷评分结果：",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY);
				p_intPosY += 30;
				m_intXPos += 40;
				int i=0;
				for(;i<=8;i++)
					p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY+30*i,m_intXPos+500,p_intPosY+30*i);
				i--;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY,m_intXPos,p_intPosY+30*i);
				p_objGrp.DrawLine(Pens.Black,m_intXPos+250,p_intPosY,m_intXPos+250,p_intPosY+30*i);
				p_objGrp.DrawLine(Pens.Black,m_intXPos+500,p_intPosY,m_intXPos+500,p_intPosY+30*i);

				p_objGrp.DrawString("总分",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.m_strTotalEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("睁眼",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.m_strOpenEyeEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("运动反应",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.m_strSportFeedbackEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("语言反应",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.m_strTalkFeedbackEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("瞳孔光反应",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.m_strPupilLightFeedbackEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("脑干反射",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.m_strBrainReflectEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("抽搐",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.m_strTwitchEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("自发性呼吸",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.m_strSpontaneityBreathEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);

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


