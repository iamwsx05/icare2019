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
	/// SIRS�������ִ�ӡ����
	/// </summary>
	public class clsSIRS_EvaluationPrintTool: clsValuationPrintBase
	{
		public clsSIRS_EvaluationPrintTool()
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
			p_objGrp.DrawString("SIRS��������",m_fotItemHead,m_slbBrush,320,60);
		}

		#region Print Class
		/// <summary>
		/// ��Ŀ��ӡ
		/// </summary>
		private class clsPrintValuation: clsPrintValuationInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsEvalInfoOfSIRSEvaluation m_objPrintSubInfo;
			private string[] m_strSpaceArr = {"��","����","������������"};
			/// <summary>
			/// ��߾�
			/// </summary>
			private int m_intXPos = (int)enmRectangleInfo.LeftX+10;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
                //m_objPrintSubInfo = m_objPrintInfo as clsEvalInfoOfSIRSEvaluation;
                try
                {
                    m_objPrintSubInfo = (clsEvalInfoOfSIRSEvaluation)m_objPrintInfo;
                }
                catch
                {

                }
				if(m_objPrintSubInfo == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}

				p_intPosY += 40;
				m_mthPrintSIRS(ref p_intPosY,p_objGrp,p_fntNormalText);
				p_intPosY += 30;
				p_objGrp.DrawString("�������ڣ�" + DateTime.Parse(m_objContent.m_strActivityTime).ToString("yyyy��MM��dd��") + m_strSpaceArr[2] + "�����ߣ�" + new clsEmployee(m_objPrintSubInfo.strEvalDoctorID).m_StrFirstName,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+50,p_intPosY);
				p_intPosY += 30;
				m_mthPrintResult(ref p_intPosY,p_objGrp,p_fntNormalText);
			
				m_blnHaveMoreLine = false;
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();

				m_blnHaveMoreLine = true;
			}

			private void m_mthPrintSIRS(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY += 15;
				string[] strAge = {"> 5��","< 1��","1��12��","1��2��","2��5��","5��12��","12��15��","> 15�����"};
				p_objGrp.DrawString("�����飺  " + m_strGetGCSValue(strAge,m_objPrintSubInfo.strAgeGroup),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("����Ƶ�ʣ� "+ m_objPrintSubInfo.strBreath + " ��/��",p_fntNormalText,Brushes.Black,m_intXPos+40,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("���ʣ� "+ m_objPrintSubInfo.strHeartRate + " ��/��",p_fntNormalText,Brushes.Black,m_intXPos+40,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("���£� "+ m_objPrintSubInfo.strTemperature + " ��",p_fntNormalText,Brushes.Black,m_intXPos+40,p_intPosY);
				p_intPosY += 40;
				p_objGrp.DrawString("��ϸ�������ͷ��ࣺ",p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 30;
				if(m_objPrintSubInfo.strWBCorBacillusSel == "0")
					p_objGrp.DrawString("��ϸ�������� "+ m_objPrintSubInfo.strWBCorBacillus + " (*10^9 /L)",p_fntNormalText,Brushes.Black,m_intXPos+40,p_intPosY);
				else
					p_objGrp.DrawString("��״�ˣ� "+ m_objPrintSubInfo.strWBCorBacillus + " ��",p_fntNormalText,Brushes.Black,m_intXPos+40,p_intPosY);
				p_intPosY += 40;

				RectangleF rtg = new RectangleF(m_intXPos,p_intPosY+300,(int)enmRectangleInfo.RightX-10-m_intXPos,80);
				string strNoteText = "ע��\n" + m_strSpaceArr[1] + "1����ϸ�������ͷ��ࣺ����ݱ�ʵ��������ֵ���е�����\n" + m_strSpaceArr[1]+ @"2��������Ϊ ��> 5���� �͡�< 1�¡������������ʡ����°�����̥���������㣻��ϸ������������������㡣";
				p_objGrp.DrawString(strNoteText,p_fntNormalText,Brushes.Black,rtg);
				
			}
			private void m_mthPrintResult(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				#region 
				int intXPos = m_intXPos;
				p_intPosY += 20;
				p_objGrp.DrawString("SIRS��Ͻ����",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY);
				p_intPosY += 30;
				m_intXPos += 40;
				int i=0;
				for(;i<=5;i++)
					p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY+30*i,m_intXPos+500,p_intPosY+30*i);
				i--;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY,m_intXPos,p_intPosY+30*i);
				p_objGrp.DrawLine(Pens.Black,m_intXPos+250,p_intPosY,m_intXPos+250,p_intPosY+30*i);
				p_objGrp.DrawLine(Pens.Black,m_intXPos+500,p_intPosY,m_intXPos+500,p_intPosY+30*i);

				p_objGrp.DrawString("��Ͻ��",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strTotalEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("����Ƶ��",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strBreathEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("����",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strHeartRateEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("����",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strTemperatureEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("��ϸ�������ͷ���",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strWBCorBacillusEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);

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

