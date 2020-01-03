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
	/// SOFA�������ִ�ӡ����
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
			p_objGrp.DrawString("SOFA��������",m_fotItemHead,m_slbBrush,310,60);
		}

		#region Print Class
		/// <summary>
		/// ��Ŀ��ӡ
		/// </summary>
		private class clsPrintValuation: clsPrintValuationInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsEvalInfoOfSOFAEvaluation m_objPrintSubInfo;
			private string[] m_strSpaceArr = {"��","����","������������"};
			/// <summary>
			/// ��߾�
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

			private void m_mthPrintSOFA(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				string[] strOpenEyes = {"��","ʹ�̼�","��������","�Է�"};
				string[] strSay = {"��","�����","���ʵ�","�ش����","������ȷ"};
				string[] strSport = {"��","ʹ����ֱ","ʹ������","�����ر�","��λ��ʹ","��������"};
				//������
				string[] strDHS = {"����","20--32","33--101","102--204",">204"} ;
				//��Ѫѹ
				string[] strDXY = {"����","ƽ������ѹ < 70 mmHg","��Ͱ� <= 5 ���κμ����Ķ�ͷӶ���","��Ͱ� > 5 �� �������� <= 0.1 �� ȥ���������� <= 0.1","��Ͱ� > 15 �� �������� > 0.1 �� ȥ���������� > 0.1"} ;
				//Ѫ����
				string[] strXJG = {"����","110 < Ѫ���� < 170","171 < Ѫ���� < 299","300 < Ѫ���� < 440 �� ���� < 500","Ѫ���� > 440 �� ���� < 200"} ;
				//ѪС��
				string[] strXXB = {"����","����100��С��150","����50��С��100","����20��С��50","С��20"} ;
				p_intPosY += 15;
				p_objGrp.DrawString("����ϵͳ��",p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;

				p_objGrp.DrawString("PaO �� " + m_objPrintSubInfo.strPa02 + " mmHg",p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_objGrp.DrawString("2",new Font("SimSun",6.75f),Brushes.Black,m_intXPos+60,p_intPosY+10);
				p_objGrp.DrawString("FiO �� " + m_objPrintSubInfo.strFi02 + " %",p_fntNormalText,Brushes.Black,m_intXPos+350,p_intPosY);
				p_objGrp.DrawString("2",new Font("SimSun",6.75f),Brushes.Black,m_intXPos+380,p_intPosY+10);
				p_intPosY += 30;
				
				p_objGrp.DrawString("ѪС�壺 " + m_strGetGCSValue(strXXB,m_objPrintSubInfo.strXXB),p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("�����أ� " + m_strGetGCSValue(strDHS,m_objPrintSubInfo.strDHS) +"  ��mol/L",p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;

				p_objGrp.DrawString("Ѫ����(��mol/L)������(ml/L)�� " + m_strGetGCSValue(strXJG,m_objPrintSubInfo.strXJG),p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("��Ѫѹ�� "+ m_strGetGCSValue(strDXY,m_objPrintSubInfo.strDXY),p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;

				p_objGrp.DrawString("��ϵͳ��",p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
				p_intPosY += 30;
				p_objGrp.DrawString("���۷�Ӧ�� "+m_strGetGCSValue(strOpenEyes,m_objPrintSubInfo.strOpenEyes),p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("���ﷴӦ�� " + m_strGetGCSValue(strSay,m_objPrintSubInfo.strSay),p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_intPosY += 20;
				p_objGrp.DrawString("�˶���Ӧ�� " +m_strGetGCSValue(strSport,m_objPrintSubInfo.strSport),p_fntNormalText,Brushes.Black,m_intXPos+30,p_intPosY);
				p_intPosY += 30;
			}
			private void m_mthPrintResult(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				#region 
				p_intPosY += 20;
				p_objGrp.DrawString("SOFA���ֽ����",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY);
				p_intPosY += 30;
				m_intXPos += 40;
				int i=0;
				for(;i<=7;i++)
					p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY+30*i,m_intXPos+500,p_intPosY+30*i);
				i--;
				p_objGrp.DrawLine(Pens.Black,m_intXPos,p_intPosY,m_intXPos,p_intPosY+30*i);
				p_objGrp.DrawLine(Pens.Black,m_intXPos+250,p_intPosY,m_intXPos+250,p_intPosY+30*i);
				p_objGrp.DrawLine(Pens.Black,m_intXPos+500,p_intPosY,m_intXPos+500,p_intPosY+30*i);

				p_objGrp.DrawString("   �ܷ�",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strTotalEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("����ϵͳ",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strBreathEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("ѪҺϵͳ",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strBloodEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("����",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strDHSEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("��Ѫ��ϵͳ",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strXXGEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("��ϵͳ",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
				p_objGrp.DrawString(m_objPrintSubInfo.strNerveEval,p_fntNormalText,Brushes.Black,m_intXPos+252,p_intPosY+4);
				p_intPosY += 30;

				p_objGrp.DrawString("����",p_fntNormalText,Brushes.Black,m_intXPos,p_intPosY+4);
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
