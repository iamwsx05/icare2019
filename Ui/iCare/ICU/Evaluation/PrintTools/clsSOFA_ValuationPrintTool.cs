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
	/// SOFA智能评分打印工具
	/// </summary>
	public class clsSOFA_ValuationPrintTool: clsValuationPrintBase
	{
		public clsSOFA_ValuationPrintTool()
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
			p_objGrp.DrawString("SOFA智能评分",m_fotItemHead,m_slbBrush,310,60);
		}

		#region Print Class
		/// <summary>
		/// 项目打印
		/// </summary>
		private class clsPrintValuation: clsPrintValuationInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsEvalInfoOfSOFAEvaluation m_objPrintSubInfo;
			private string[] m_strSpaceArr = {"　","　　","　　　　　　"};
			/// <summary>
			/// 左边距
			/// </summary>
			private int m_intXPos = (int)enmRectangleInfo.LeftX+10;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				m_objPrintSubInfo = m_objPrintInfo as clsEvalInfoOfSOFAEvaluation;
				if(m_objPrintSubInfo == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}

				p_intPosY += 40;
				m_mthPrintSOFA(ref p_intPosY,p_objGrp,p_fntNormalText);
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

			private void m_mthPrintSOFA(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				string[] strOpenEyes = {"无","痛刺激","呼唤睁眼","自发"};
				string[] strSay = {"无","不理解","不适当","回答混乱","定向正确"};
				string[] strSport = {"无","痛刺伸直","痛刺屈曲","屈曲回避","定位疼痛","服从命令"};
				//胆红素
				string[] strDHS = {"正常","20--32","33--101","102--204",">204"} ;
				//低血压
				string[] strDXY = {"正常","平均动脉压 < 70 mmHg","多巴胺 <= 5 或任何剂量的多巴酚丁胺","多巴胺 > 5 或 肾上腺素 <= 0.1 或 去甲肾上腺素 <= 0.1","多巴胺 > 15 或 肾上腺素 > 0.1 或 去甲肾上腺素 > 0.1"} ;
				//血肌酐
				string[] strXJG = {"正常","110 < 血肌酐 < 170","171 < 血肌酐 < 299","300 < 血肌酐 < 440 或 尿量 < 500","血肌酐 > 440 或 尿量 < 200"} ;
				//血小板
				string[] strXXB = {"正常","大于100，小于150","大于50，小于100","大于20，小于50","小于20"} ;
				p_intPosY += 15;
				p_objGrp.DrawString("呼吸系统：",p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;

				p_objGrp.DrawString("PaO ： " + m_objPrintSubInfo.strPa02 + " mmHg",p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_objGrp.DrawString("2",new Font("SimSun",6.75f),Brushes.Black,m_intXPos+60,p_intPosY+10);
				p_objGrp.DrawString("FiO ： " + m_objPrintSubInfo.strFi02 + " %",p_fntNormalText,Brushes.Black,m_intXPos+350,p_intPosY);
				p_objGrp.DrawString("2",new Font("SimSun",6.75f),Brushes.Black,m_intXPos+380,p_intPosY+10);
				p_intPosY += 30;
				
				p_objGrp.DrawString("血小板： " + m_strGetGCSValue(strXXB,m_objPrintSubInfo.strXXB),p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("胆红素： " + m_strGetGCSValue(strDHS,m_objPrintSubInfo.strDHS) +"  μmol/L",p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;

				p_objGrp.DrawString("血肌酐(μmol/L)或尿量(ml/L)： " + m_strGetGCSValue(strXJG,m_objPrintSubInfo.strXJG),p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("低血压： "+ m_strGetGCSValue(strDXY,m_objPrintSubInfo.strDXY),p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;

				p_objGrp.DrawString("神经系统：",p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("睁眼反应： "+m_strGetGCSValue(strOpenEyes,m_objPrintSubInfo.strOpenEyes),p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("言语反应： " + m_strGetGCSValue(strSay,m_objPrintSubInfo.strSay),p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("运动反应： " +m_strGetGCSValue(strSport,m_objPrintSubInfo.strSport),p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_intPosY += 30;
			}
			private void m_mthPrintResult(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				#region 
				p_intPosY += 20;
				p_objGrp.DrawString("SOFA评分结果：",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY);
				p_intPosY += 30;
				m_intXPos += 40;
				int i=0;
				for(;i<=7;i++)
					p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY+30*i,m_intXPos+500,p_intPosY+30*i);
				i--;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY,m_intXPos,p_intPosY+30*i);
				p_objGrp.DrawLine(Pens.Black,m_intXPos+250,p_intPosY,m_intXPos+250,p_intPosY+30*i);
				p_objGrp.DrawLine(Pens.Black,m_intXPos+500,p_intPosY,m_intXPos+500,p_intPosY+30*i);

				p_objGrp.DrawString("   总分",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strTotalEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("呼吸系统",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strBreathEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("血液系统",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strBloodEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("肝脏",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strDHSEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("心血管系统",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strXXGEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("神经系统",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strNerveEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("肾脏",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strXJGEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				
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
