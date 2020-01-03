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
	/// MODS智能评分打印工具
	/// </summary>
	public class clsMODS_ValuationPrintTool: clsValuationPrintBase
	{
		public clsMODS_ValuationPrintTool()
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
			p_objGrp.DrawString("MODS智能评分",m_fotItemHead,m_slbBrush,310,60);
		}

		#region Print Class
		/// <summary>
		/// 项目打印
		/// </summary>
		private class clsPrintValuation: clsPrintValuationInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsEvalInfoOfMODSEvaluation m_objPrintSubInfo;
			private string[] m_strSpaceArr = {"　","　　","　　　　　　"};
			/// <summary>
			/// 左边距
			/// </summary>
			private int m_intXPos = (int)enmRectangleInfo.LeftX+10;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				m_objPrintSubInfo = m_objPrintInfo as clsEvalInfoOfMODSEvaluation;
				if(m_objPrintSubInfo == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}

				p_intPosY += 40;
				m_mthPrintMODS(ref p_intPosY,p_objGrp,p_fntNormalText);
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

			private void m_mthPrintMODS(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY += 15;
				p_objGrp.DrawString("呼吸系统：",p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;

				p_objGrp.DrawString("PaO ： " + m_objPrintSubInfo.strPa02 + " mmHg",p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_objGrp.DrawString("2",new Font("SimSun",6.75f),Brushes.Black,m_intXPos+60,p_intPosY+10);
				p_objGrp.DrawString("FiO ： " + m_objPrintSubInfo.strFi02 + " %",p_fntNormalText,Brushes.Black,m_intXPos+350,p_intPosY);
				p_objGrp.DrawString("2",new Font("SimSun",6.75f),Brushes.Black,m_intXPos+380,p_intPosY+10);
				p_intPosY += 30;
				
				p_objGrp.DrawString("肾脏：",p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("血肌酐： " + m_objPrintSubInfo.strXJG + "  umol/l",p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_intPosY += 30;

				p_objGrp.DrawString("肝脏：",p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("胆红素： " + m_objPrintSubInfo.strDHS +"  mg/dl",p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_intPosY += 30;

				p_objGrp.DrawString("心血管系统：",p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("心率： "+ m_objPrintSubInfo.strHR + "  /min",p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_objGrp.DrawString("右房压： " + m_objPrintSubInfo.strYFY+ "  mmHg",p_fntNormalText,Brushes.Black,m_intXPos+350,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("平均动脉压： " + m_objPrintSubInfo.strPJDMY+ "  mmHg",p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_intPosY += 30;

				p_objGrp.DrawString("血液系统：",p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("血小板： " + m_objPrintSubInfo.strXXB,p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_objGrp.DrawString("  *10 /L" + m_objPrintSubInfo.strXXB,p_fntNormalText,Brushes.Black,m_intXPos+150,p_intPosY);
				p_objGrp.DrawString("9",new Font("SimSun",6.75f),Brushes.Black,m_intXPos+192,p_intPosY);
				p_intPosY += 30;

				p_objGrp.DrawString("神经系统：",p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;
				string[] strOpenEyes = {"无","痛刺激","呼唤睁眼","自发"};
				string[] strSay = {"无","不理解","不适当","回答混乱","定向正确"};
				string[] strSport = {"无","痛刺伸直","痛刺屈曲","屈曲回避","定位疼痛","服从命令"};
				p_objGrp.DrawString("睁眼反应： "+m_strGetGCSValue(strOpenEyes,m_objPrintSubInfo.strOpenEyes),p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_objGrp.DrawString("言语反应： " + m_strGetGCSValue(strSay,m_objPrintSubInfo.strSay),p_fntNormalText,Brushes.Black,m_intXPos+350,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("运动反应： " +m_strGetGCSValue(strSport,m_objPrintSubInfo.strSport),p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_intPosY += 30;
			}
			private void m_mthPrintResult(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				#region 
				p_intPosY += 20;
				p_objGrp.DrawString("MODS评分结果：",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY);
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

				p_objGrp.DrawString("肾脏",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strXJGEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("肝脏",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strDHSEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("心血管系统",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strXXGEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("血液系统",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strBloodEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("神经系统",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strNerveEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				
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

	public class cls_MODS_ValuationPrintTool: clsValuationPrintBase
	{
		public cls_MODS_ValuationPrintTool()
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
			p_objGrp.DrawString("MODS器官系统功能临床研究观察登记表",m_fotItemHead,m_slbBrush,220,60);
		}

		#region Print Class
		/// <summary>
		/// 项目打印
		/// </summary>
		private class clsPrintValuation: clsPrintValuationInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private cls_EvalInfoOfMODSEvaluation m_objPrintSubInfo;
			private string[] m_strSpaceArr = {"　","　　","　　　　　　"};
			/// <summary>
			/// 左边距
			/// </summary>
			private int m_intXPos = (int)enmRectangleInfo.LeftX+10;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				m_objPrintSubInfo = m_objPrintInfo as cls_EvalInfoOfMODSEvaluation;
				if(m_objPrintSubInfo == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				//p_objGrp.DrawString("一般资料",p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+50,p_intPosY);
				p_intPosY += 40;
				m_mthPrintMODS(ref p_intPosY,p_objGrp,p_fntNormalText);
				p_intPosY += 30;
				p_objGrp.DrawString("登记日期：" + DateTime.Parse(m_objContent.m_strActivityTime).ToString("yyyy年MM月dd日") + m_strSpaceArr[2] + "评估者：" + new clsEmployee(m_objPrintSubInfo.strEvalDoctorID).m_StrFirstName,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+50,p_intPosY);
				p_intPosY += 30;
				
				
				
				m_mthPrintResult(ref p_intPosY,p_objGrp,p_fntNormalText);
			
				m_blnHaveMoreLine = false;
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();

				m_blnHaveMoreLine = true;
			}

			private void m_mthPrintMODS(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				
				p_objGrp.DrawString("一般资料",p_fntNormalText,Brushes.Black,320,p_intPosY-20);
				p_intPosY += 25;
				p_objGrp.DrawString("住院天数："+m_objPrintSubInfo.strInHospitalDays  + m_strSpaceArr[2] + "入院时间：入院："+m_objPrintSubInfo.strOutHospitalDays ,p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("入院诊断 ： " + m_objPrintSubInfo.strInDiagnose ,p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("出院诊断 ： " + m_objPrintSubInfo.strOutdiagnose ,p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("出院方式 ： " + m_objPrintSubInfo.strOutMode ,p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_intPosY += 25;
				p_objGrp.DrawString("常规检查",p_fntNormalText,Brushes.Black,320,p_intPosY);//(int)enmRectangleInfo.LeftX+50,p_intPosY);
				p_intPosY += 25;
				p_objGrp.DrawString("生命体征：呼吸"+m_objPrintSubInfo.strBreath +"次/分"+m_strSpaceArr[0]+"心率"+m_objPrintSubInfo.strHeartRate+"次/分"+m_strSpaceArr[0]+"体温"+m_objPrintSubInfo.strTemperature+"℃"+m_strSpaceArr[0]+"血压"+m_objPrintSubInfo.strBloodPressure+"kPa/mmHg"+m_strSpaceArr[0]+"体重"+m_objPrintSubInfo.strWeight +"Kg",p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_intPosY += 25;
				p_objGrp.DrawString("心功能：CK(u/L)" + m_objPrintSubInfo.strXGNCK +"CK-MB(u/L)"+ m_objPrintSubInfo.strXGNCK_MB +m_strSpaceArr[0]+"LDH(u/L)"+m_objPrintSubInfo.strXGNLDH +m_strSpaceArr[0]+"CVP/MAP"+m_objPrintSubInfo.strXGNCVP ,p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_intPosY += 25;
				p_objGrp.DrawString("肺功能：PaO2(kPa/mmHg))" + m_objPrintSubInfo.strFGNPAO2 +m_strSpaceArr[0]+"PaCO2(kPa/mmHg)"+ m_objPrintSubInfo.strFGNPACO2 +m_strSpaceArr[0]+"PaO2/FiO2"+m_objPrintSubInfo.strFGNPA02_FI02 ,p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_intPosY += 25;
				p_objGrp.DrawString("呼吸机参数：模式" + m_objPrintSubInfo.strHXJMODEL + m_strSpaceArr[0]+"PIP(cmH2O)"+m_objPrintSubInfo.strHXJPIP  +m_strSpaceArr[0]+"MAP(cmH2O)"+m_objPrintSubInfo.strHXJMAP +m_strSpaceArr[0]+ "PEEP(cmH2O)"+m_objPrintSubInfo.strHXJPEEP ,p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_intPosY += 25;
				p_objGrp.DrawString("Ti(秒)：" + m_objPrintSubInfo.strHXJTI + m_strSpaceArr[0]+"MV(L/min)"+m_objPrintSubInfo.strHXJMV +m_strSpaceArr[0]+"RR(bpm)"+m_objPrintSubInfo.strHXJRR + m_strSpaceArr[0]+"Cd(mL/cmH2O)"+m_objPrintSubInfo.strHXJCD ,p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_intPosY += 25;
				p_objGrp.DrawString("Cs(mL/cmH2O)" + m_objPrintSubInfo.strHXJCS ,p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_objGrp.DrawString("2",new Font("SimSun",6.75f),Brushes.Black,m_intXPos+380,p_intPosY+10);
				p_intPosY += 30;
				
			}
			private void m_mthPrintResult(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				#region 
				//無結果
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
