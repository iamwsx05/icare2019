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
	/// APACHEIII�������ִ�ӡ����
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
			p_objGrp.DrawString("APACHEIII ����",m_fotItemHead,m_slbBrush,320,60);
		}

		#region Print Class
		/// <summary>
		/// ��Ŀ��ӡ
		/// </summary>
		private class clsPrintValuation: clsPrintValuationInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private APACHEIIIValuationDB m_objPrintSubInfo;
			private string[] m_strSpaceArr = {"��","����","������������"};
			/// <summary>
			/// ��߾�
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

			private void m_mthPrintAPS(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY += 15;
				p_objGrp.DrawString("APACHEIII-ASP�ͼ���ʧ�����ֱ�׼��",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY);
				p_intPosY += 30;
				m_mthDrawCheckBox((int)enmRectangleInfo.LeftX+20,p_intPosY,p_objGrp, p_fntNormalText,m_objPrintSubInfo.strMachineAerateChk == "1");
				p_objGrp.DrawString("�ǻ�еͨ�����ߣ�",p_fntNormalText,Brushes.Black,m_intXPos+40,p_intPosY);
				m_mthDrawCheckBox(m_intXPos+210,p_intPosY,p_objGrp, p_fntNormalText,m_objPrintSubInfo.strKidneyWaneChk == "1");
				p_objGrp.DrawString("�Ǽ�����˥�ߣ�",p_fntNormalText,Brushes.Black,m_intXPos+240,p_intPosY);
				p_intPosY += 20;
				
				p_objGrp.DrawString("APS�����ʧ޿��",p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("����(/min)��" + m_objPrintSubInfo.strHR.Trim() + "��Ѫϸ������(��)��" + m_objPrintSubInfo.strBloodCorpuscle.Trim() + "������(ml/d)��" + m_objPrintSubInfo.strUrineAmount.Trim(),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("ƽ������ѹ(mmHg)��" + m_objPrintSubInfo.strAdvArteryPress.Trim() + "����ϸ������(*10^9/L)��" + m_objPrintSubInfo.strAmountLeucocyte.Trim() + "��Ѫ���ص�(mmol/L)��" + m_objPrintSubInfo.strHematuria.Trim(),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("����(��)��" + m_objPrintSubInfo.strTemperature.Trim() + "��Ѫ����Ũ��(mmol/L)��" + m_objPrintSubInfo.strBloodFlesh.Trim() + "��Ѫ��(mmol/L)��" + m_objPrintSubInfo.strBloodNa.Trim(),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("����Ƶ��(/min)��" + m_objPrintSubInfo.strBreath.Trim()+ "���ܵ�����(��mol/L)��" + m_objPrintSubInfo.strHypercholesterolemia.Trim() + "���׵���(g/L)��" + m_objPrintSubInfo.strProteid.Trim(),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("PH��" + m_objPrintSubInfo.strPH.Trim()+ "��PCO2(������̼��ѹ)��" + m_objPrintSubInfo.strPCO2.Trim() + "��Ѫ��(mmol/L)��" + m_objPrintSubInfo.strBloodGallbladder.Trim(),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 20;
			}

			private void m_mthPrintAPS_DO2(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY += 10;
				p_objGrp.DrawString("(A��a)DO2���ݶ���Ѫ����ѹ��(mmHg)��",p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("���������������� FiO2(������Ũ��)��" + m_objPrintSubInfo.strFiO2.Trim(),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("PaCO2(����Ѫ������̼��ѹ)(mmol/L)��" + m_objPrintSubInfo.strPaCO2.Trim(),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("�������� PaO2(����Ѫ����ѹ)(mmHg)��" + m_objPrintSubInfo.strPao2.Trim(),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("}",new Font("SimSun",44),Brushes.Black,m_intXPos+330,p_intPosY-60);
				p_objGrp.DrawString("=> " + m_objPrintSubInfo.strDo2.Trim(),p_fntNormalText,Brushes.Black,m_intXPos+370,p_intPosY-40);
			}

			private void m_mthPrintAge(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				#region
				p_intPosY += 15;
				p_objGrp.DrawString("APACHEIII-����ͼ����������ֱ�׼��",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY);
				p_intPosY += 30;
				string[] strAge = {"С��44��","45~59��","60~64��","65~69��","70~74��","75~84��","85������"};
				p_objGrp.DrawString("���䣺 " + m_strGetGCSValue(strAge,m_objPrintSubInfo.strAgeGroup),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_objGrp.DrawString("�Ƿ������������ߣ�"+(m_objPrintSubInfo.strOperSel == "0" ? "��":"��"),p_fntNormalText,Brushes.Black,m_intXPos+300,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("��������״����",p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 20;
				int intIndex = 0;
				if(m_objPrintSubInfo.strAIDSChk == "1")
				{
					m_mthDrawCheckBox(m_intXPos+20 +intIndex*200,p_intPosY,p_objGrp, p_fntNormalText,"AIDS ��");
					intIndex++;
				}
				if(m_objPrintSubInfo.strLeukaemiaChk == "1")
				{
					m_mthDrawCheckBox(m_intXPos+20 +intIndex*200,p_intPosY,p_objGrp, p_fntNormalText,@"��Ѫ��/�෢������ ��");
					intIndex++;
				}
				if(m_objPrintSubInfo.strLiverWaneChk == "1")
				{
					m_mthDrawCheckBox(m_intXPos+20 +intIndex*200,p_intPosY,p_objGrp, p_fntNormalText,"�ι���˥�� ��");
					intIndex++;
				}
				if(m_objPrintSubInfo.strImmunityChk== "1")
				{
					if(intIndex > 2)
					{
						intIndex = 0;
						p_intPosY += 20;
					}
					m_mthDrawCheckBox(m_intXPos+20 +intIndex*200,p_intPosY,p_objGrp, p_fntNormalText,@"�������� ��");
					intIndex++;
				}
				if(m_objPrintSubInfo.strLimphomaChk== "1")
				{
					if(intIndex > 2)
					{
						intIndex = 0;
						p_intPosY += 20;
					}
					m_mthDrawCheckBox(m_intXPos+20 +intIndex*200,p_intPosY,p_objGrp, p_fntNormalText,@"�ܰ��� ��");
					intIndex++;
				}
				if(m_objPrintSubInfo.strHepatocirrhosisChk== "1")
				{
					if(intIndex > 2)
					{
						intIndex = 0;
						p_intPosY += 20;
					}
					m_mthDrawCheckBox(m_intXPos+20 +intIndex*200,p_intPosY,p_objGrp, p_fntNormalText,@"��Ӳ�� ��");
					intIndex++;
				}
				if(m_objPrintSubInfo.strMetastaticTumorChk== "1")
				{
					if(intIndex > 2)
					{
						intIndex = 0;
						p_intPosY += 20;
					}
					m_mthDrawCheckBox(m_intXPos+20 +intIndex*200,p_intPosY,p_objGrp, p_fntNormalText,@"ת���� ��");
					intIndex++;
				}
				#endregion
			}
			private void m_mthPrint_Nerve(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				#region
				p_intPosY += 30;
				p_objGrp.DrawString("APACHEIII-�񾭹����쳣���ֱ�׼��",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("����ʹ�������ܷ��Զ����ۣ� " + (m_objPrintSubInfo.strOpenEyeSel == "0" ? "��":"����"),p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("����ʹ�����Դ̼�ʱ�����Լ��˶��仯��",p_fntNormalText,Brushes.Black,m_intXPos+20,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("���ԣ�" +(m_objPrintSubInfo.strLanguageSel == "0"? "�ش���ȷ":(m_objPrintSubInfo.strLanguageSel == "1"? "�ش����":(m_objPrintSubInfo.strLanguageSel == "2"? "����������":"�޷�Ӧ"))),p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("�˶��仯��",p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_intPosY += 20;
				int intXPos = m_intXPos+30;
				if(m_objPrintSubInfo.strAccordingChk == "1")
				{
					m_mthDrawCheckBox(intXPos,p_intPosY,p_objGrp, p_fntNormalText,"�����˶� ��");
					intXPos += 120;
				}
				if(m_objPrintSubInfo.strPositionAcheChk == "1")
				{
					m_mthDrawCheckBox(intXPos,p_intPosY,p_objGrp, p_fntNormalText,@"��ʹ��λ ��");
					intXPos += 120;
				}
				if(m_objPrintSubInfo.strBodyBendAndVerticalChk == "1")
				{
					m_mthDrawCheckBox(intXPos,p_intPosY,p_objGrp, p_fntNormalText,@"֫��������ȥƤ��ǿֱ ��");
					intXPos += 220;
				}
				if(m_objPrintSubInfo.strBrainUnreactionChk == "1")
				{
					m_mthDrawCheckBox(intXPos,p_intPosY,p_objGrp, p_fntNormalText,@"ȥ����ǿֱ���޷�Ӧ ��");
					intXPos += 120;
				}
				#endregion
			}
			private void m_mthPrintResult(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				#region 
				int intXPos = m_intXPos;
				p_intPosY += 20;
				p_objGrp.DrawString("APACHEIII���ַ����ݰ��������",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY);
				p_intPosY += 30;
				m_intXPos += 40;
				int i=0;
				for(;i<=5;i++)
					p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY+30*i,m_intXPos+500,p_intPosY+30*i);
				i--;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY,m_intXPos,p_intPosY+30*i);
				p_objGrp.DrawLine(Pens.Black,m_intXPos+250,p_intPosY,m_intXPos+250,p_intPosY+30*i);
				p_objGrp.DrawLine(Pens.Black,m_intXPos+500,p_intPosY,m_intXPos+500,p_intPosY+30*i);

				p_objGrp.DrawString("�ܷ�",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strTotalVal,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("ASP�÷�",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strAspVal,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("���ʧ��÷�",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strPHAndPCO2Val,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("����ͼ��������÷�",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strAgeAndHealthVal,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("�񾭹����쳣�÷�",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
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
