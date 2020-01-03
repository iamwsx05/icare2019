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
	/// 急性肺损伤智能评分打印工具
	/// </summary>
	public class clsLungInjury_ValuationPrintTool: clsValuationPrintBase
	{
		public clsLungInjury_ValuationPrintTool()
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
			p_objGrp.DrawString("急性肺损伤智能评分",m_fotItemHead,m_slbBrush,300,60);
		}

		#region Print Class
		/// <summary>
		/// 项目打印
		/// </summary>
		private class clsPrintValuation: clsPrintValuationInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsEvalInfoOfclsLungInjuryEvaluation m_objPrintSubInfo;
			private string[] m_strSpaceArr = {"　","　　","　　　　　　"};
			/// <summary>
			/// 左边距
			/// </summary>
			private int m_intXPos = (int)enmRectangleInfo.LeftX+10;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				m_objPrintSubInfo = m_objPrintInfo as clsEvalInfoOfclsLungInjuryEvaluation;
				if(m_objPrintSubInfo == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}

				p_intPosY += 40;
				m_mthPrintLungInjury(ref p_intPosY,p_objGrp,p_fntNormalText);
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

			private void m_mthPrintLungInjury(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY += 15;
				p_objGrp.DrawString("1.胸部 X- 光照片：" + m_objPrintSubInfo.strLungXRay,p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("2.低氧血症：",p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("PaO  值："+m_strSpaceArr[1]+ "FiO  值："+m_strSpaceArr[1]+"PaO  /FiO   值：",p_fntNormalText,Brushes.Black,m_intXPos+40,p_intPosY);
				p_objGrp.DrawString("2　　　　　　　　　　　2　　　　　　　　　　　2 　　　   2",new Font("SimSun",6.75f),Brushes.Black,m_intXPos+70,p_intPosY+10);
				p_intPosY += 30;
				p_objGrp.DrawString(m_objPrintSubInfo.strPao2 + (m_objPrintSubInfo.strPao2AndFio2Sel == "0"?"kPa    ÷   ":"mmHg   ÷  ")+m_objPrintSubInfo.strFio2+(m_objPrintSubInfo.strPao2AndFio2Sel == "0"?"kPa     ＝     ":"mmHg     ＝     ")+m_objPrintSubInfo.strLowOxygenBlood,p_fntNormalText,Brushes.Black,m_intXPos+40,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("3.PEEP（机械通气者）：",p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("PEEP 值： "+ m_objPrintSubInfo.strPEEP,p_fntNormalText,Brushes.Black,m_intXPos+40,p_intPosY);
				p_objGrp.DrawString("cmH  O",p_fntNormalText,Brushes.Black,m_intXPos+180,p_intPosY);
				p_objGrp.DrawString("2",new Font("SimSun",6.75f),Brushes.Black,m_intXPos+210,p_intPosY+10);
				p_intPosY += 30;
				p_objGrp.DrawString("4.肺系统顺应性：",p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("肺系统顺应性 ＝ 潮气量（Vt） ÷ (　气道峰压（PIP） －　呼气末正压（PEEP）)" ,p_fntNormalText,Brushes.Black,m_intXPos+40,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("     "+m_objPrintSubInfo.strLungSysHumor + "   ＝    " + m_objPrintSubInfo.strVt + "         ÷ (       "+ m_objPrintSubInfo.strPIP + "           －    "+ m_objPrintSubInfo.strPEEP+"   )",p_fntNormalText,Brushes.Black,m_intXPos+40,p_intPosY);
				p_intPosY += 30;
			}
			private void m_mthPrintResult(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				#region 
				p_intPosY += 20;
				p_objGrp.DrawString("急性肺损伤评分结果：",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY);
				p_intPosY += 30;
				m_intXPos += 40;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY,m_intXPos+500,p_intPosY);
				p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY+30,m_intXPos+500,p_intPosY+30);
				p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY+60,m_intXPos+500,p_intPosY+60);
				p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY+90,m_intXPos+500,p_intPosY+90);
				p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY+120,m_intXPos+500,p_intPosY+120);
				p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY+150,m_intXPos+500,p_intPosY+150);
				p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY,m_intXPos,p_intPosY+150);
				p_objGrp.DrawLine(Pens.Black,m_intXPos+250,p_intPosY,m_intXPos+250,p_intPosY+150);
				p_objGrp.DrawLine(Pens.Black,m_intXPos+500,p_intPosY,m_intXPos+500,p_intPosY+150);

				p_objGrp.DrawString("   总分",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strTotalEval,p_fntNormalText,Brushes.Black,m_intXPos+250,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("胸部 X- 光照片评分",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strLungXRayEval,p_fntNormalText,Brushes.Black,m_intXPos+250,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("低氧血症评分",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strLowOxygenBloodEval,p_fntNormalText,Brushes.Black,m_intXPos+250,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("PEEP（机械通气者）评分",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strPEEPEval,p_fntNormalText,Brushes.Black,m_intXPos+250,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("肺系统顺应性",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strLungSysHumorEval,p_fntNormalText,Brushes.Black,m_intXPos+250,p_intPosY+4);
				
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
