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
	/// APACHEII智能评分打印工具
	/// </summary>
	public class clsAPACHEII_ValuationPrintTool: clsValuationPrintBase
	{
		public clsAPACHEII_ValuationPrintTool()
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
			p_objGrp.DrawString("APACHEII 评分",m_fotItemHead,m_slbBrush,320,60);
		}

		#region Print Class
		/// <summary>
		/// 项目打印
		/// </summary>
		private class clsPrintValuation: clsPrintValuationInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private APACHEIIValuationDB m_objPrintSubInfo;
//			private bool m_blnIsFirstPrint = true;
			private string[] m_strSpaceArr = {"　","　　","　　　　　　"};

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				m_objPrintSubInfo = m_objPrintInfo as APACHEIIValuationDB;
				if(m_objPrintSubInfo == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}

				p_intPosY += 15;
				p_objGrp.DrawString("APS值和年龄：",p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+10,p_intPosY);
				p_intPosY += 20;
				m_mthPrintAPS(ref p_intPosY, p_objGrp, p_fntNormalText);
				m_mthPrintAPS_DO2(ref p_intPosY, p_objGrp, p_fntNormalText);
				m_mthPrintAPS_GCS(ref p_intPosY,p_objGrp,p_fntNormalText);
				m_mthPrintAge(ref p_intPosY,p_objGrp,p_fntNormalText);
				m_mthPrint_ICU(ref p_intPosY,p_objGrp,p_fntNormalText);
				p_intPosY += 10;
				p_objGrp.DrawString("评分日期：" + DateTime.Parse(m_objContent.m_strActivityTime).ToString("yyyy年MM月dd日") + m_strSpaceArr[2] + "评估者：" + new clsEmployee(m_objPrintSubInfo.strEvalDoctorID).m_StrFirstName,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+50,p_intPosY);
				p_intPosY += 30;
				m_mthPrintResult(ref p_intPosY,p_objGrp,p_fntNormalText);
			
				m_blnHaveMoreLine = false;
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();

				m_blnHaveMoreLine = true;

//				m_blnIsFirstPrint = true;
			}

			private void m_mthPrintAPS(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY += 10;
				p_objGrp.DrawString(" APS:" + m_strSpaceArr[0]+ "直肠温度(℃)：" + m_objPrintSubInfo.strTemperature.Trim() + "；呼吸频率(/min)：" + m_objPrintSubInfo.strBreath.Trim() + "；动脉血pH：" + m_objPrintSubInfo.strArteryBloodpH.Trim(),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+10,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("平均动脉压(mmHg)：" + m_objPrintSubInfo.strAdvArteryPress.Trim() + "；血肌酐浓度(mmol/L)：" + m_objPrintSubInfo.strBloodFlesh.Trim() + "；血钠浓度(mmol/L)：" + m_objPrintSubInfo.strBloodNa.Trim(),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+70,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("心率(/min)：" + m_objPrintSubInfo.strHeartRate.Trim() + "；白细胞计数(*10^9/L)：" + m_objPrintSubInfo.strAmountLeucocyte.Trim() + "；血钾浓度(mmol/L)：" + m_objPrintSubInfo.strBloodK.Trim(),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+70,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("静脉血HCO－(mmol/L,无动脉血气时)：" + m_objPrintSubInfo.strHCO.Trim(),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+70,p_intPosY);
				m_mthDrawCheckBox((int)enmRectangleInfo.LeftX+400,p_intPosY,p_objGrp, p_fntNormalText,m_objPrintSubInfo.strKidneyProstrate == "1");
				p_objGrp.DrawString("急性肾功能衰竭 ； 血细胞比容(％)：" + m_objPrintSubInfo.strBloodCorpuscle.Trim(),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+420,p_intPosY);
				p_intPosY += 20;
			}

			private void m_mthPrintAPS_DO2(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY += 10;
				p_objGrp.DrawString("(A－a)DO 肺泡动脉血氧分压差(mmHg)：",p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+20,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("　　　　　　　　 FiO2(吸入氧浓度)：" + m_objPrintSubInfo.strFiO2.Trim(),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+30,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("PaCO2(动脉血二氧化碳分压)(mmol/L)：" + m_objPrintSubInfo.strPaCO2.Trim(),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+30,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("　　　　 PaO2(动脉血氧分压)(mmHg)：" + m_objPrintSubInfo.strPao2.Trim(),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+30,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("}",new Font("SimSun",44),Brushes.Black,(int)enmRectangleInfo.LeftX+340,p_intPosY-60);
				p_objGrp.DrawString("=> " + m_objPrintSubInfo.strDo2.Trim(),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+380,p_intPosY-40);
			}

			private void m_mthPrintAPS_GCS(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY -= 90;
				p_objGrp.DrawString("神经系统(GCS)：",p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+490,p_intPosY);
				p_intPosY += 30;
				string[] strGCSArr = m_objPrintSubInfo.strGCS.Split("§".ToCharArray());
				string[] strOpenEyes = {"无","痛刺激","呼唤睁眼","自发"};
				string[] strSay = {"无","不理解","不适当","回答混乱","定向正确"};
				string[] strSport = {"无","痛刺伸直","痛刺屈曲","屈曲回避","定位疼痛","服从命令"};
				p_objGrp.DrawString("睁眼反应：" + m_strGetGCSValue(strOpenEyes,strGCSArr[0]),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+490,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("言语反应：" + m_strGetGCSValue(strSay,strGCSArr[1]),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+490,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("运动反应：" + m_strGetGCSValue(strSport,strGCSArr[2]),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+490,p_intPosY);
				p_intPosY += 20;
			}
			private void m_mthPrintAge(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY += 10;
				string[] strAge = {"小于44岁","45~54岁","55~64岁","65~74岁","75岁以上"};
				p_objGrp.DrawString("年龄：" + m_strGetGCSValue(strAge,m_objPrintSubInfo.strAgeGroup),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+30,p_intPosY);
				p_intPosY += 20;
			}
			private void m_mthPrint_ICU(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				#region 
				p_intPosY += 30;
				p_objGrp.DrawString("APACHE --患者住ICU的主要疾病分值：",p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+10,p_intPosY);
				p_intPosY += 30;
				if(m_objPrintSubInfo.strHealthGroup == "0")
				{
					p_objGrp.DrawString("非手术或急诊手术后患者：",p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+20,p_intPosY);
					p_intPosY += 20;
					if(m_objPrintSubInfo.strPatientUnOp1 == "1")
					{
						m_mthDrawCheckBox((int)enmRectangleInfo.LeftX+30,p_intPosY,p_objGrp,p_fntNormalText,"因下列因素导致呼吸功能衰竭或不全：");
						p_objGrp.DrawString(m_objPrintSubInfo.strPatientUnOp1Eval,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+350,p_intPosY);
						p_intPosY += 20;
					}
					if(m_objPrintSubInfo.strPatientUnOp2 == "1")
					{
						m_mthDrawCheckBox((int)enmRectangleInfo.LeftX+30,p_intPosY,p_objGrp,p_fntNormalText,"因下列因素导致的心血管功能衰竭或不全：");
						p_objGrp.DrawString(m_objPrintSubInfo.strPatientUnOp2Eval,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+400,p_intPosY);
						p_intPosY += 20;
					}
					if(m_objPrintSubInfo.strOthers == "1")
					{
						m_mthDrawCheckBox((int)enmRectangleInfo.LeftX+30,p_intPosY,p_objGrp,p_fntNormalText,"其他：");
						p_objGrp.DrawString(m_objPrintSubInfo.strOthersEval,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+110,p_intPosY);
						p_intPosY += 20;
					}
					if(m_objPrintSubInfo.strNeurotic == "1")
					{
						m_mthDrawCheckBox((int)enmRectangleInfo.LeftX+30,p_intPosY,p_objGrp,p_fntNormalText,"神经系统疾病：");
						p_objGrp.DrawString(m_objPrintSubInfo.strNeuroticEval,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+200,p_intPosY);
						p_intPosY += 20;
					}
					if(m_objPrintSubInfo.strNoInRange == "1")
					{
						m_mthDrawCheckBox((int)enmRectangleInfo.LeftX+30,p_intPosY,p_objGrp,p_fntNormalText,"主要疾病不在上列范围，选择主要器官系统：");
						p_objGrp.DrawString(m_objPrintSubInfo.strNoInRangeEval,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+430,p_intPosY);
						p_intPosY += 20;
					}
					if(m_objPrintSubInfo.strHurts == "1")
					{
						m_mthDrawCheckBox((int)enmRectangleInfo.LeftX+30,p_intPosY,p_objGrp,p_fntNormalText,"创伤：");
						p_objGrp.DrawString(m_objPrintSubInfo.strHurtsEval,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+110,p_intPosY);
						p_intPosY += 20;
					}
				}
				else
				{
					p_objGrp.DrawString("择期手术后患者：",p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+20,p_intPosY);
					p_intPosY += 20;
					p_objGrp.DrawString(m_objPrintSubInfo.strPatientSelOpEval,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+30,p_intPosY);
					m_mthDrawCheckBox((int)enmRectangleInfo.LeftX+180,p_intPosY,p_objGrp,p_fntNormalText,m_objPrintSubInfo.strPatientAfterEmergency == "1");
					p_objGrp.DrawString("是急诊手术后患者 ；",p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+210,p_intPosY);
					p_intPosY += 20;
					if(m_objPrintSubInfo.strMainReasonNoIn == "1")
					{
						m_mthDrawCheckBox((int)enmRectangleInfo.LeftX+30,p_intPosY,p_objGrp,p_fntNormalText,"主要原因不在上列范围，可选择主要器官系统：");
						p_objGrp.DrawString(m_objPrintSubInfo.strMainReasonNoInEval,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+430,p_intPosY);
						p_intPosY += 20;
					}
				}
				#endregion
			}
			private void m_mthPrintResult(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				#region 
				int intXPos = (int)enmRectangleInfo.LeftX+10;
				p_intPosY += 20;
				p_objGrp.DrawString("急性生理和既往健康评价系统：",p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+10,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,(int)enmRectangleInfo.RightX-10,p_intPosY);
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+100);

				p_objGrp.DrawString("危险性",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strREval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 200;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);

				p_objGrp.DrawString("直肠温度",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strTemperatureEval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 80;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);

				p_objGrp.DrawString("平均动脉压",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strAdvArteryPressEval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 100;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);

				p_objGrp.DrawString("心率",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strHeartRateEval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 40;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);

				p_objGrp.DrawString("白细胞计数",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strAmountLeucocyteEval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 100;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);

				p_objGrp.DrawString("呼吸频率",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strBreathEval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 80;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);

				p_objGrp.DrawString("PaO2",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strPao2Eval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 50;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);

				p_objGrp.DrawString("DO2",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strDo2Eval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				
				p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.RightX-10,p_intPosY,(int)enmRectangleInfo.RightX-10,p_intPosY+100);
				p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+10,p_intPosY+25,(int)enmRectangleInfo.RightX-10,p_intPosY+25);
				p_intPosY += 50;
				p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+10,p_intPosY,(int)enmRectangleInfo.RightX-10,p_intPosY);
				intXPos = (int)enmRectangleInfo.LeftX+10;

				p_objGrp.DrawString("动脉血",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strArteryBloodpHEval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 60;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);

				p_objGrp.DrawString("血钠浓度",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strBloodNaEval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 80;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);
				
				p_objGrp.DrawString("血钾浓度",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strBloodKEval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 80;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);

				p_objGrp.DrawString("GCS",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strGCSEval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 40;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);
				
				p_objGrp.DrawString("血肌酐浓度",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strBloodFleshEval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 100;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);
				
				p_objGrp.DrawString("血细胞比容",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strBloodCorpuscleEval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 90;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);
				
				p_objGrp.DrawString("静脉血HCO-",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strHCOEval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 100;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);
				
				p_objGrp.DrawString("总数",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strTotalEval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+10,p_intPosY+25,(int)enmRectangleInfo.RightX-10,p_intPosY+25);
				p_intPosY += 50;
				p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.LeftX+10,p_intPosY,(int)enmRectangleInfo.RightX-10,p_intPosY);
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
