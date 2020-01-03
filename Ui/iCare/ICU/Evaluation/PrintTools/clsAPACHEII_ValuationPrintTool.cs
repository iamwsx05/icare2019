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
	/// APACHEII�������ִ�ӡ����
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
			p_objGrp.DrawString("APACHEII ����",m_fotItemHead,m_slbBrush,320,60);
		}

		#region Print Class
		/// <summary>
		/// ��Ŀ��ӡ
		/// </summary>
		private class clsPrintValuation: clsPrintValuationInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private APACHEIIValuationDB m_objPrintSubInfo;
//			private bool m_blnIsFirstPrint = true;
			private string[] m_strSpaceArr = {"��","����","������������"};

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				m_objPrintSubInfo = m_objPrintInfo as APACHEIIValuationDB;
				if(m_objPrintSubInfo == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}

				p_intPosY += 15;
				p_objGrp.DrawString("APSֵ�����䣺",p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+10,p_intPosY);
				p_intPosY += 20;
				m_mthPrintAPS(ref p_intPosY, p_objGrp, p_fntNormalText);
				m_mthPrintAPS_DO2(ref p_intPosY, p_objGrp, p_fntNormalText);
				m_mthPrintAPS_GCS(ref p_intPosY,p_objGrp,p_fntNormalText);
				m_mthPrintAge(ref p_intPosY,p_objGrp,p_fntNormalText);
				m_mthPrint_ICU(ref p_intPosY,p_objGrp,p_fntNormalText);
				p_intPosY += 10;
				p_objGrp.DrawString("�������ڣ�" + DateTime.Parse(m_objContent.m_strActivityTime).ToString("yyyy��MM��dd��") + m_strSpaceArr[2] + "�����ߣ�" + new clsEmployee(m_objPrintSubInfo.strEvalDoctorID).m_StrFirstName,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+50,p_intPosY);
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
				p_objGrp.DrawString(" APS:" + m_strSpaceArr[0]+ "ֱ���¶�(��)��" + m_objPrintSubInfo.strTemperature.Trim() + "������Ƶ��(/min)��" + m_objPrintSubInfo.strBreath.Trim() + "������ѪpH��" + m_objPrintSubInfo.strArteryBloodpH.Trim(),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+10,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("ƽ������ѹ(mmHg)��" + m_objPrintSubInfo.strAdvArteryPress.Trim() + "��Ѫ����Ũ��(mmol/L)��" + m_objPrintSubInfo.strBloodFlesh.Trim() + "��Ѫ��Ũ��(mmol/L)��" + m_objPrintSubInfo.strBloodNa.Trim(),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+70,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("����(/min)��" + m_objPrintSubInfo.strHeartRate.Trim() + "����ϸ������(*10^9/L)��" + m_objPrintSubInfo.strAmountLeucocyte.Trim() + "��Ѫ��Ũ��(mmol/L)��" + m_objPrintSubInfo.strBloodK.Trim(),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+70,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("����ѪHCO��(mmol/L,�޶���Ѫ��ʱ)��" + m_objPrintSubInfo.strHCO.Trim(),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+70,p_intPosY);
				m_mthDrawCheckBox((int)enmRectangleInfo.LeftX+400,p_intPosY,p_objGrp, p_fntNormalText,m_objPrintSubInfo.strKidneyProstrate == "1");
				p_objGrp.DrawString("����������˥�� �� Ѫϸ������(��)��" + m_objPrintSubInfo.strBloodCorpuscle.Trim(),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+420,p_intPosY);
				p_intPosY += 20;
			}

			private void m_mthPrintAPS_DO2(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY += 10;
				p_objGrp.DrawString("(A��a)DO ���ݶ���Ѫ����ѹ��(mmHg)��",p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+20,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("���������������� FiO2(������Ũ��)��" + m_objPrintSubInfo.strFiO2.Trim(),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+30,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("PaCO2(����Ѫ������̼��ѹ)(mmol/L)��" + m_objPrintSubInfo.strPaCO2.Trim(),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+30,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("�������� PaO2(����Ѫ����ѹ)(mmHg)��" + m_objPrintSubInfo.strPao2.Trim(),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+30,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("}",new Font("SimSun",44),Brushes.Black,(int)enmRectangleInfo.LeftX+340,p_intPosY-60);
				p_objGrp.DrawString("=> " + m_objPrintSubInfo.strDo2.Trim(),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+380,p_intPosY-40);
			}

			private void m_mthPrintAPS_GCS(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY -= 90;
				p_objGrp.DrawString("��ϵͳ(GCS)��",p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+490,p_intPosY);
				p_intPosY += 30;
				string[] strGCSArr = m_objPrintSubInfo.strGCS.Split("��".ToCharArray());
				string[] strOpenEyes = {"��","ʹ�̼�","��������","�Է�"};
				string[] strSay = {"��","�����","���ʵ�","�ش����","������ȷ"};
				string[] strSport = {"��","ʹ����ֱ","ʹ������","�����ر�","��λ��ʹ","��������"};
				p_objGrp.DrawString("���۷�Ӧ��" + m_strGetGCSValue(strOpenEyes,strGCSArr[0]),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+490,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("���ﷴӦ��" + m_strGetGCSValue(strSay,strGCSArr[1]),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+490,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("�˶���Ӧ��" + m_strGetGCSValue(strSport,strGCSArr[2]),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+490,p_intPosY);
				p_intPosY += 20;
			}
			private void m_mthPrintAge(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_intPosY += 10;
				string[] strAge = {"С��44��","45~54��","55~64��","65~74��","75������"};
				p_objGrp.DrawString("���䣺" + m_strGetGCSValue(strAge,m_objPrintSubInfo.strAgeGroup),p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+30,p_intPosY);
				p_intPosY += 20;
			}
			private void m_mthPrint_ICU(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				#region 
				p_intPosY += 30;
				p_objGrp.DrawString("APACHE --����סICU����Ҫ������ֵ��",p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+10,p_intPosY);
				p_intPosY += 30;
				if(m_objPrintSubInfo.strHealthGroup == "0")
				{
					p_objGrp.DrawString("�����������������ߣ�",p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+20,p_intPosY);
					p_intPosY += 20;
					if(m_objPrintSubInfo.strPatientUnOp1 == "1")
					{
						m_mthDrawCheckBox((int)enmRectangleInfo.LeftX+30,p_intPosY,p_objGrp,p_fntNormalText,"���������ص��º�������˥�߻�ȫ��");
						p_objGrp.DrawString(m_objPrintSubInfo.strPatientUnOp1Eval,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+350,p_intPosY);
						p_intPosY += 20;
					}
					if(m_objPrintSubInfo.strPatientUnOp2 == "1")
					{
						m_mthDrawCheckBox((int)enmRectangleInfo.LeftX+30,p_intPosY,p_objGrp,p_fntNormalText,"���������ص��µ���Ѫ�ܹ���˥�߻�ȫ��");
						p_objGrp.DrawString(m_objPrintSubInfo.strPatientUnOp2Eval,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+400,p_intPosY);
						p_intPosY += 20;
					}
					if(m_objPrintSubInfo.strOthers == "1")
					{
						m_mthDrawCheckBox((int)enmRectangleInfo.LeftX+30,p_intPosY,p_objGrp,p_fntNormalText,"������");
						p_objGrp.DrawString(m_objPrintSubInfo.strOthersEval,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+110,p_intPosY);
						p_intPosY += 20;
					}
					if(m_objPrintSubInfo.strNeurotic == "1")
					{
						m_mthDrawCheckBox((int)enmRectangleInfo.LeftX+30,p_intPosY,p_objGrp,p_fntNormalText,"��ϵͳ������");
						p_objGrp.DrawString(m_objPrintSubInfo.strNeuroticEval,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+200,p_intPosY);
						p_intPosY += 20;
					}
					if(m_objPrintSubInfo.strNoInRange == "1")
					{
						m_mthDrawCheckBox((int)enmRectangleInfo.LeftX+30,p_intPosY,p_objGrp,p_fntNormalText,"��Ҫ�����������з�Χ��ѡ����Ҫ����ϵͳ��");
						p_objGrp.DrawString(m_objPrintSubInfo.strNoInRangeEval,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+430,p_intPosY);
						p_intPosY += 20;
					}
					if(m_objPrintSubInfo.strHurts == "1")
					{
						m_mthDrawCheckBox((int)enmRectangleInfo.LeftX+30,p_intPosY,p_objGrp,p_fntNormalText,"���ˣ�");
						p_objGrp.DrawString(m_objPrintSubInfo.strHurtsEval,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+110,p_intPosY);
						p_intPosY += 20;
					}
				}
				else
				{
					p_objGrp.DrawString("�����������ߣ�",p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+20,p_intPosY);
					p_intPosY += 20;
					p_objGrp.DrawString(m_objPrintSubInfo.strPatientSelOpEval,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+30,p_intPosY);
					m_mthDrawCheckBox((int)enmRectangleInfo.LeftX+180,p_intPosY,p_objGrp,p_fntNormalText,m_objPrintSubInfo.strPatientAfterEmergency == "1");
					p_objGrp.DrawString("�Ǽ����������� ��",p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+210,p_intPosY);
					p_intPosY += 20;
					if(m_objPrintSubInfo.strMainReasonNoIn == "1")
					{
						m_mthDrawCheckBox((int)enmRectangleInfo.LeftX+30,p_intPosY,p_objGrp,p_fntNormalText,"��Ҫԭ�������з�Χ����ѡ����Ҫ����ϵͳ��");
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
				p_objGrp.DrawString("��������ͼ�����������ϵͳ��",p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+10,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,(int)enmRectangleInfo.RightX-10,p_intPosY);
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+100);

				p_objGrp.DrawString("Σ����",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strREval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 200;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);

				p_objGrp.DrawString("ֱ���¶�",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strTemperatureEval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 80;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);

				p_objGrp.DrawString("ƽ������ѹ",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strAdvArteryPressEval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 100;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);

				p_objGrp.DrawString("����",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strHeartRateEval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 40;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);

				p_objGrp.DrawString("��ϸ������",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strAmountLeucocyteEval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 100;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);

				p_objGrp.DrawString("����Ƶ��",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
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

				p_objGrp.DrawString("����Ѫ",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strArteryBloodpHEval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 60;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);

				p_objGrp.DrawString("Ѫ��Ũ��",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strBloodNaEval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 80;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);
				
				p_objGrp.DrawString("Ѫ��Ũ��",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strBloodKEval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 80;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);

				p_objGrp.DrawString("GCS",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strGCSEval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 40;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);
				
				p_objGrp.DrawString("Ѫ����Ũ��",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strBloodFleshEval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 100;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);
				
				p_objGrp.DrawString("Ѫϸ������",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strBloodCorpuscleEval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 90;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);
				
				p_objGrp.DrawString("����ѪHCO-",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
				p_objGrp.DrawString(m_objPrintSubInfo.strHCOEval,p_fntNormalText,Brushes.Black,intXPos,p_intPosY+25);
				intXPos += 100;
				p_objGrp.DrawLine(Pens.Black,intXPos,p_intPosY,intXPos,p_intPosY+50);
				
				p_objGrp.DrawString("����",p_fntNormalText,Brushes.Black,intXPos,p_intPosY+3);
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
