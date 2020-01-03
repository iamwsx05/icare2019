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
	/// TISS-28�������ִ�ӡ����
	/// </summary>
	public class clsTISS_ValuationPrintTool: clsValuationPrintBase
	{
		public clsTISS_ValuationPrintTool()
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
			p_objGrp.DrawString("TISS-28����",m_fotItemHead,m_slbBrush,320,60);
		}

		#region Print Class
		/// <summary>
		/// ��Ŀ��ӡ
		/// </summary>
		private class clsPrintValuation: clsPrintValuationInfo
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsTISSValuationInfo m_objPrintSubInfo;
			private string[] m_strSpaceArr = {"��","����","������������"};
			/// <summary>
			/// ��߾�
			/// </summary>
			private int m_intXPos = (int)enmRectangleInfo.LeftX+10;
			private int m_intIndex = 0;
			private bool m_blnPrintNextPage = false;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				m_objPrintSubInfo = m_objPrintInfo as clsTISSValuationInfo;
				if(m_objPrintSubInfo == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				p_intPosY += 10;
				if(m_intIndex == 0)
				{
					p_objGrp.DrawString("���ֽ���� " + m_objPrintSubInfo.strResult.TrimEnd() + " ��",p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+10,p_intPosY);
					p_intPosY += 30;
				}
				m_mthPrintBabyInjury(ref p_intPosY,p_objGrp,p_fntNormalText);
				if(m_blnPrintNextPage)
				{
					m_blnHaveMoreLine = true;
					m_blnPrintNextPage = false;
					return;
				}
				p_intPosY += 30;
				p_objGrp.DrawString("�������ڣ�" + DateTime.Parse(m_objContent.m_strActivityTime).ToString("yyyy��MM��dd��") + m_strSpaceArr[1] + "�����ߣ�" + new clsEmployee(m_objPrintSubInfo.strEvalDoctorID).m_StrFirstName,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.LeftX+50,p_intPosY);
				
				m_blnHaveMoreLine = false;
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();

				m_blnHaveMoreLine = true;
			}

			private void m_mthPrintBabyInjury(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				#region Set string
				string[] strContentArr = new String[]{m_objPrintSubInfo.blnItem1,m_objPrintSubInfo.blnItem2,m_objPrintSubInfo.blnItem3,m_objPrintSubInfo.blnItem4,m_objPrintSubInfo.blnItem5,m_objPrintSubInfo.blnItem6,m_objPrintSubInfo.blnItem7
													 ,m_objPrintSubInfo.blnItem8,m_objPrintSubInfo.blnItem9,m_objPrintSubInfo.blnItem10,m_objPrintSubInfo.blnItem11,m_objPrintSubInfo.blnItem12,m_objPrintSubInfo.blnItem13,m_objPrintSubInfo.blnItem14
													 ,m_objPrintSubInfo.blnItem15,m_objPrintSubInfo.blnItem16,m_objPrintSubInfo.blnItem17,m_objPrintSubInfo.blnItem18,m_objPrintSubInfo.blnItem19,m_objPrintSubInfo.blnItem20,m_objPrintSubInfo.blnItem21
													 ,m_objPrintSubInfo.blnItem22,m_objPrintSubInfo.blnItem23,m_objPrintSubInfo.blnItem24,m_objPrintSubInfo.blnItem25,m_objPrintSubInfo.blnItem26,m_objPrintSubInfo.blnItem27,m_objPrintSubInfo.blnItem28};
				string[] strTitleArr = {"������Ŀ��","","","","","","","ͨ��֧�֣�","","","","��Ѫ��֧�֣�","","","","","","","����֧�֣�","","","��ϵͳ֧�֣�","��л֧�֣�","","","�����Ԥ��ʩ","",""};
				string[] strTextArr = new String[28];
				strTextArr[0] = "1����׼��⣺ÿСʱ����������Һ��ƽ��ĳ����¼�ͼ���";
				strTextArr[1] = "2��ʵ���Ҽ�飺������΢����ѧ���";
				strTextArr[2] = "3����һҩ����������⡢Ƥ��ע�䣬�ͣ��򣩿ڷ������羭θ�ܸ�ҩ��";
				strTextArr[3] = "4������ʹ�ö���ҩ����ξ�ע�������ע1������ҩ��";
				strTextArr[4] = "5������������ϣ��촯�Ļ����Ԥ����ÿ�ո���һ�η���";
				strTextArr[5] = "6��Ƶ���������ϣ�Ƶ���������ϣ�ÿ�λ�������ٸ���1�Σ��ͣ��򣩴�����˿ڻ���";
				strTextArr[6] = "7�������ܵĻ�����θ����������е��ܵĻ���";
				strTextArr[7] = "8����еͨ�����κ���ʽ�Ļ�еͨ��/����ͨ���������Ƿ�ʹ��PEEP����ҩ������PEEP����������";
				strTextArr[8] = "9������ͨ��֧�֣������ܲ��������������Ӧ��PEEP��������Ӧ�õĻ�еͨ��ģʽ�⣬ �ṩ�κ���ʽ������";
				strTextArr[9] = "10���˹������Ļ������ܲ�ܻ������п��Ļ���";
				strTextArr[10] = "11�����Ʒι��ܣ��ز����ƣ��̼��Է����ơ������Ʒ�����������̵";
				strTextArr[11] = "12����һѪ�ܻ���ҩ�ʹ���κ�Ѫ�ܻ���ҩ��";
				strTextArr[12] = "13������Ѫ�ܻ���ҩ�ʹ��1�����ϵ�Ѫ�ܻ���ҩ���������ͼ���";
				strTextArr[13] = "14���������䶪ʧ�Ĵ���Һ�壺��Һ��>3L/(m  * d)������Һ������";
				strTextArr[14] = "15���������ܶ�������";
				strTextArr[15] = "16�����ķ���⣺���÷ζ���Ư�����ܣ������Ƿ�������ų���";
				strTextArr[16] = "17�����ľ����ù�";
				strTextArr[17] = "18���ڹ�ȥ24Сʱ���й�������ͣ���ķθ��գ�������ǰ��ߵ�����⣩";
				strTextArr[18] = "19��ѪҺ���ˡ�ѪҺ͸��";
				strTextArr[19] = "20�������ⶨ���������磬������ܲ�����";
				strTextArr[20] = "21�������������磬߻����>0.5mg/(kg * d)������Һ�����ɣ�";
				strTextArr[21] = "22��­��ѹ����";
				strTextArr[22] = "23�������Դ�л���ж�����ж�������";
				strTextArr[23] = "24������Ӫ��֧��";
				strTextArr[24] = "25��θ����Ӫ������θ�ܻ�����θ����;�������磬�ճ�������";
				strTextArr[25] = "26��ICU�ڵ�һ�����Ԥ��ʩ�����ǻ򾭿����ܲ�ܡ���������������ת�����ھ���顢��ȥ24h�ڼ���������ϴθ���Ի����ٴ����������ֱ��Ӱ��ĳ����Ԥ��ʩ����X�߼�顢������顢�ĵ�ͼ��顢�������ϡ����þ����������ܵȲ���������";
				strTextArr[26] = "27��ICU�ڶ��������Ԥ��ʩ��������Ŀ��1�����ϵĸ�Ԥ��ʩ";
				strTextArr[27] = "28��ICU��������Ԥ��ʩ�����������Բ���";
				#endregion
				p_intPosY += 15;
				RectangleF rtg = new RectangleF(m_intXPos+10,p_intPosY,(int)enmRectangleInfo.RightX-20-m_intXPos,20);
				for(int i=m_intIndex;i<strContentArr.Length;i++)
				{
					if(p_intPosY+20 > ((int)enmRectangleInfo.BottomY))
					{
						m_intIndex = i;
						p_intPosY += 500;
						m_blnPrintNextPage = true;
						return;
					}
					if(strTitleArr[i] != "")
					{
						p_objGrp.DrawString(strTitleArr[i],p_fntNormalText,Brushes.Black,m_intXPos+10,p_intPosY);
						p_intPosY += 20;
					}
					if(strContentArr[i] == "False")
						continue;
					string strText = "����"+strTextArr[i];
					SizeF szfText = p_objGrp.MeasureString(strText,p_fntNormalText,Convert.ToInt32(rtg.Width));
					rtg.Height = szfText.Height+5;
					rtg.Y = p_intPosY;
                    if (strContentArr[i] == "1")
                    {
                        m_mthDrawCheckBox(m_intXPos + 30, p_intPosY, p_objGrp, p_fntNormalText, true);
                    }
                    else
                    {
                        m_mthDrawCheckBox(m_intXPos + 30, p_intPosY, p_objGrp, p_fntNormalText, false);
                    }
					p_objGrp.DrawString(strText,p_fntNormalText,Brushes.Black,rtg);
					p_intPosY += Convert.ToInt32(rtg.Height);
				}
			}
		}
		#endregion

	}
}


