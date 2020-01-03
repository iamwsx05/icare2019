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
	/// APACHEIII智能评分打印工具
	/// </summary>
	public class clsAPACHEIII_ValuationPrintTool: clsValuationPrintBase
	{
		public clsAPACHEIII_ValuationPrintTool()
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
			p_objGrp.DrawString("APACHEIII 评分",m_fotItemHead,m_slbBrush,320,60);
		}

		#region Print Class
		/// <summary>
		/// 项目打印
		/// </summary>
		private class clsPrintValuation: clsPrintValuationInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private APACHEIIIValuationDB m_objPrintSubInfo;
			private string[] m_strSpaceArr = {"　","　　","　　　　　　"};
			/// <summary>
			/// 左边距
			/// </summary>
			private int m_intXPos = (int)enmRectangleInfo.LeftX+10;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				m_objPrintSubInfo = m_objPrintInfo as APACHEIIIValuationDB;
				if(m_objPrintSubInfo == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}

				p_intPosY += 20;
				m_mthPrintAPS(ref p_intPosY, p_objGrp, p_fntNormalText);
				m_mthPrintAPS_DO2(ref p_intPosY, p_objGrp, p_fntNormalText);
				m_mthPrintAge(ref p_intPosY,p_objGrp,p_fntNormalText);
				m_mthPrint_Nerve(ref p_intPosY,p_objGrp,p_fntNormalText);
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

			private void m_mthPrintAPS(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY += 15;
				p_objGrp.DrawString("APACHEIII-ASP和碱酸失衡评分标准：",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY);
				p_intPosY += 30;
				m_mthDrawCheckBox((int)enmRectangleInfo.LeftX+20,p_intPosY,p_objGrp, p_fntNormalText,m_objPrintSubInfo.strMachineAerateChk == "1");
				p_objGrp.DrawString("是机械通气患者：",p_fntNormalText,Brushes.Black,m_intXPos+40,p_intPosY);
				m_mthDrawCheckBox(m_intXPos+210,p_intPosY,p_objGrp, p_fntNormalText,m_objPrintSubInfo.strKidneyWaneChk == "1");
				p_objGrp.DrawString("是急性肾衰竭：",p_fntNormalText,Brushes.Black,m_intXPos+240,p_intPosY);
				p_intPosY += 20;
				
				p_objGrp.DrawString("APS和酸碱失蘅：",p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("心率(/min)：" + m_objPrintSubInfo.strHR.Trim() + "；血细胞比容(％)：" + m_objPrintSubInfo.strBloodCorpuscle.Trim() + "；尿量(ml/d)：" + m_objPrintSubInfo.strUrineAmount.Trim(),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("平均动脉压(mmHg)：" + m_objPrintSubInfo.strAdvArteryPress.Trim() + "；白细胞计数(*10^9/L)：" + m_objPrintSubInfo.strAmountLeucocyte.Trim() + "；血尿素氮(mmol/L)：" + m_objPrintSubInfo.strHematuria.Trim(),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("体温(℃)：" + m_objPrintSubInfo.strTemperature.Trim() + "；血肌酐浓度(mmol/L)：" + m_objPrintSubInfo.strBloodFlesh.Trim() + "；血钠(mmol/L)：" + m_objPrintSubInfo.strBloodNa.Trim(),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("呼吸频率(/min)：" + m_objPrintSubInfo.strBreath.Trim()+ "；总胆红素(μmol/L)：" + m_objPrintSubInfo.strHypercholesterolemia.Trim() + "；白蛋白(g/L)：" + m_objPrintSubInfo.strProteid.Trim(),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("PH：" + m_objPrintSubInfo.strPH.Trim()+ "；PCO2(二氧化碳分压)：" + m_objPrintSubInfo.strPCO2.Trim() + "；血糖(mmol/L)：" + m_objPrintSubInfo.strBloodGallbladder.Trim(),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 20;
			}

			private void m_mthPrintAPS_DO2(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY += 10;
				p_objGrp.DrawString("(A－a)DO2肺泡动脉血氧分压差(mmHg)：",p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("　　　　　　　　 FiO2(吸入氧浓度)：" + m_objPrintSubInfo.strFiO2.Trim(),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("PaCO2(动脉血二氧化碳分压)(mmol/L)：" + m_objPrintSubInfo.strPaCO2.Trim(),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("　　　　 PaO2(动脉血氧分压)(mmHg)：" + m_objPrintSubInfo.strPao2.Trim(),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("}",new Font("SimSun",44),Brushes.Black,m_intXPos+330,p_intPosY-60);
				p_objGrp.DrawString("=> " + m_objPrintSubInfo.strDo2.Trim(),p_fntNormalText,Brushes.Black,m_intXPos+370,p_intPosY-40);
			}

			private void m_mthPrintAge(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				#region
				p_intPosY += 15;
				p_objGrp.DrawString("APACHEIII-年龄和既往健康评分标准：",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY);
				p_intPosY += 30;
				string[] strAge = {"小于44岁","45~59岁","60~64岁","65~69岁","70~74岁","75~84岁","85岁以上"};
				p_objGrp.DrawString("年龄： " + m_strGetGCSValue(strAge,m_objPrintSubInfo.strAgeGroup),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_objGrp.DrawString("是否择期手术患者："+(m_objPrintSubInfo.strOperSel == "0" ? "是":"否"),p_fntNormalText,Brushes.Black,m_intXPos+300,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("既往健康状况：",p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 20;
				int intIndex = 0;
				if(m_objPrintSubInfo.strAIDSChk == "1")
				{
					m_mthDrawCheckBox(m_intXPos+20 +intIndex*200,p_intPosY,p_objGrp, p_fntNormalText,"AIDS ；");
					intIndex++;
				}
				if(m_objPrintSubInfo.strLeukaemiaChk == "1")
				{
					m_mthDrawCheckBox(m_intXPos+20 +intIndex*200,p_intPosY,p_objGrp, p_fntNormalText,@"白血病/多发骨髓瘤 ；");
					intIndex++;
				}
				if(m_objPrintSubInfo.strLiverWaneChk == "1")
				{
					m_mthDrawCheckBox(m_intXPos+20 +intIndex*200,p_intPosY,p_objGrp, p_fntNormalText,"肝功能衰竭 ；");
					intIndex++;
				}
				if(m_objPrintSubInfo.strImmunityChk== "1")
				{
					if(intIndex > 2)
					{
						intIndex = 0;
						p_intPosY += 20;
					}
					m_mthDrawCheckBox(m_intXPos+20 +intIndex*200,p_intPosY,p_objGrp, p_fntNormalText,@"免疫抑制 ；");
					intIndex++;
				}
				if(m_objPrintSubInfo.strLimphomaChk== "1")
				{
					if(intIndex > 2)
					{
						intIndex = 0;
						p_intPosY += 20;
					}
					m_mthDrawCheckBox(m_intXPos+20 +intIndex*200,p_intPosY,p_objGrp, p_fntNormalText,@"淋巴瘤 ；");
					intIndex++;
				}
				if(m_objPrintSubInfo.strHepatocirrhosisChk== "1")
				{
					if(intIndex > 2)
					{
						intIndex = 0;
						p_intPosY += 20;
					}
					m_mthDrawCheckBox(m_intXPos+20 +intIndex*200,p_intPosY,p_objGrp, p_fntNormalText,@"肝硬化 ；");
					intIndex++;
				}
				if(m_objPrintSubInfo.strMetastaticTumorChk== "1")
				{
					if(intIndex > 2)
					{
						intIndex = 0;
						p_intPosY += 20;
					}
					m_mthDrawCheckBox(m_intXPos+20 +intIndex*200,p_intPosY,p_objGrp, p_fntNormalText,@"转移瘤 ；");
					intIndex++;
				}
				#endregion
			}
			private void m_mthPrint_Nerve(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				#region
				p_intPosY += 30;
				p_objGrp.DrawString("APACHEIII-神经功能异常评分标准：",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("对疼痛或语言能否自动睁眼： " + (m_objPrintSubInfo.strOpenEyeSel == "0" ? "能":"不能"),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("对疼痛或语言刺激时的语言及运动变化：",p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("语言：" +(m_objPrintSubInfo.strLanguageSel == "0"? "回答正确":(m_objPrintSubInfo.strLanguageSel == "1"? "回答混乱":(m_objPrintSubInfo.strLanguageSel == "2"? "语句或发音不清":"无反应"))),p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("运动变化：",p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_intPosY += 20;
				int intXPos = m_intXPos+30;
				if(m_objPrintSubInfo.strAccordingChk == "1")
				{
					m_mthDrawCheckBox(intXPos,p_intPosY,p_objGrp, p_fntNormalText,"按嘱运动 ；");
					intXPos += 120;
				}
				if(m_objPrintSubInfo.strPositionAcheChk == "1")
				{
					m_mthDrawCheckBox(intXPos,p_intPosY,p_objGrp, p_fntNormalText,@"疼痛定位 ；");
					intXPos += 120;
				}
				if(m_objPrintSubInfo.strBodyBendAndVerticalChk == "1")
				{
					m_mthDrawCheckBox(intXPos,p_intPosY,p_objGrp, p_fntNormalText,@"肢体屈升或去皮层强直 ；");
					intXPos += 220;
				}
				if(m_objPrintSubInfo.strBrainUnreactionChk == "1")
				{
					m_mthDrawCheckBox(intXPos,p_intPosY,p_objGrp, p_fntNormalText,@"去大脑强直或无反应 ；");
					intXPos += 120;
				}
				#endregion
			}
			private void m_mthPrintResult(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				#region 
				int intXPos = m_intXPos;
				p_intPosY += 20;
				p_objGrp.DrawString("APACHEIII评分法（草案）结果：",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY);
				p_intPosY += 30;
				m_intXPos += 40;
				int i=0;
				for(;i<=5;i++)
					p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY+30*i,m_intXPos+500,p_intPosY+30*i);
				i--;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY,m_intXPos,p_intPosY+30*i);
				p_objGrp.DrawLine(Pens.Black,m_intXPos+250,p_intPosY,m_intXPos+250,p_intPosY+30*i);
				p_objGrp.DrawLine(Pens.Black,m_intXPos+500,p_intPosY,m_intXPos+500,p_intPosY+30*i);

				p_objGrp.DrawString("总分",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strTotalVal,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("ASP得分",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strAspVal,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("酸碱失衡得分",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strPHAndPCO2Val,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("年龄和既往健康得分",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strAgeAndHealthVal,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("神经功能异常得分",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strNeuroneVal,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);

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
