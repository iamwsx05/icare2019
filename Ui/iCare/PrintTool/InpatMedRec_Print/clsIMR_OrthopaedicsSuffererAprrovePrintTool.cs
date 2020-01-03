using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;
namespace iCare
{
	/// <summary>
	/// �ǿ�(��������΢�������)����֪��ͬ���� ��ӡ ��ժҪ˵����
	/// </summary>
	public class clsIMR_OrthopaedicsSuffererAprrovePrintTool: clsInpatMedRecPrintBase
	{
		public clsIMR_OrthopaedicsSuffererAprrovePrintTool(string p_strTypeID) : base(p_strTypeID)
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		protected override void m_mthSetPrintLineArr()
		{
			m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
				new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
																		   new clsPrintPatientFixInfo("�ǿ�(��������΢�������)����֪��ͬ����",195),
																		   new clsPrintInPatOrthopaedicsSuffererMain(),																	  
																		   //new  clsPrintInPatMedDoctorAndDate(),
																		  // new  clsPrintInPatMedDoctorAndDate1()
																		   //  new clsPrintInPatMedRecDiagnostic()
																	   });			
		}
        //protected override void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        //{}
        //protected override void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
        //{
        //}
		#region ��ӡ��һҳ�Ĺ̶�����
		/// <summary>
		/// ��ӡ��һҳ�Ĺ̶�����
		/// </summary>
        //internal class clsPrintPatientFixInfo : clsIMR_PrintLineBase
        //{
        //    private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

        //    public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
        //    {

        //        p_objGrp.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,m_fotItemHead ,Brushes.Black,340,40);
		
        //        p_objGrp.DrawString("�ǿ�(��������΢�������)����֪��ͬ����",new Font("SimSun", 16,FontStyle.Bold),Brushes.Black,250,70);
			
				//				p_objGrp.DrawString("���ţ�"+m_objPrintInfo.m_strAreaName+m_objPrintInfo.m_strBedName ,p_fntNormalText,Brushes.Black,350,110);
				//				p_objGrp.DrawString("ĸ��סԺ�ţ�",p_fntNormalText,Brushes.Black,550,110);
				//p_objGrp.DrawString(m_objPrintInfo.m_strInPatientID ,p_fntNormalText,Brushes.Black,680,110);	
        //        p_intPosY =130;
        //        m_blnHaveMoreLine = false;
        //    }

        //    public override void m_mthReset()
        //    {
        //        m_objPrintContext.m_mthRestartPrint();

        //        m_blnHaveMoreLine = true;
        //    }
        //}

		#endregion
		#region ������ǰ���---���������ǩ��
		/// <summary>
		/// ������ǰ���---���������ǩ��
		/// </summary>
		private class clsPrintInPatOrthopaedicsSuffererMain : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			
				private string[] m_strKeysArr1 = {"������ǰ���"};
			    private string[] m_strKeysArr2 = {"���黼�߽���"};
			    private string[] m_strKeysArr3 = {"���ܳ��ֵ�����˵������"};
				private string[] m_strKeysArr4 = {"���߱���ǩ��"};
			    private string[] m_strKeysArr5 = {"���߼���ǩ��"};
				private string[] m_strKeysArr6 = {"ǩ�����뻼�ߵĹ�ϵ"};
				private string[] m_strKeysArr7 = {"ǩ���˵����֤����"};

				private string[] m_strKeysArr8 = {"̸��ҽ��ǩ��"};

						
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				//				if(m_blnHavePrintInfo(m_strKeysArr1) == false )
				//				{
				//					m_blnHaveMoreLine = false;
				//					return;
				//				}
				if(m_blnIsFirstPrint)
				{
					
					string strAllText = "";
					string strXml = "";
					string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
					if(m_objContent!=null)
					{
                        //m_mthMakeText(new string[]{"����������"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strPatientName)+"��","  �Ա�"+ m_objPrintInfo.m_strSex.Trim()+"��" ,"  ���䣺"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strAge)+"��"},
                        //    new string [] {"","",""},ref strAllText,ref strXml);
						
						//m_mthMakeText(new string[]{"   ���䣺"},new string []{"(m_objPrintInfo==null ? : m_objPrintInfo.m_strAge)"},ref strAllText,ref strXml);
                        //m_mthMakeText(new string[]{"  ����-���ţ�"+m_objPrintInfo.m_strAreaName+m_objPrintInfo.m_strBedName+"��"},new string []{""},ref strAllText,ref strXml);
                        //m_mthMakeText(new string[]{"   סԺ�ţ�"+m_objPrintInfo.m_strInPatientID+""},new string []{""},ref strAllText,ref strXml);
						//						m_mthMakeText(new string[]{"������"},new string []{"m_objPrintInfo.m_strPatientName"},ref strAllText,ref strXml);
						//						m_mthMakeText(new string[]{"   ���䣺"},new string []{"(m_objPrintInfo==null ? : m_objPrintInfo.m_strAge)"},ref strAllText,ref strXml);
						//						m_mthMakeText(new string[]{"   ���ţ�"},new string []{"m_objPrintInfo.m_strAreaName+m_objPrintInfo.m_strBedName"},ref strAllText,ref strXml);
						//						m_mthMakeText(new string[]{"   סԺ�ţ�"},new string []{"m_objPrintInfo.m_strInPatientID"},ref strAllText,ref strXml);
						//m_mthMakeText(new string[]{"\n��������:","    ��   $$"},m_strKeysArr1,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr1) != false)
							m_mthMakeText(new string[]{"\n\n������ǰ��ϣ�"},m_strKeysArr1,ref strAllText,ref strXml);
						else
							m_mthMakeText(new string[]{"\n\n������ǰ��ϣ�"},new string[]{""},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr2) != false)
							m_mthMakeText(new string[]{"\n���黼�߽��У�"},new string[]{"���黼�߽���"},ref strAllText,ref strXml);
						else
							m_mthMakeText(new string[]{"\n���黼�߽��У�"},new string[]{""},ref strAllText,ref strXml);

							m_mthMakeText(new string[]{"\n         ��ר��ҽ���������߼����������ľ�����������ؽ�����������������ǰ�������������й������Σ���ԡ����ܳ��ֵ����⡢�ϲ�֢�ͺ���֢������������߼�����˵�����£�"},new string[]{""},ref strAllText,ref strXml);
							m_mthMakeText(new string[]{"\n         �����������������½��С��й�ʩ����������п��ܷ�������������������������ҽ�����н��ͺ�ǩ����"},new string[]{""},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr3) != false)
							m_mthMakeText(new string[]{"\n       "},m_strKeysArr3,ref strAllText,ref strXml);
						else
							m_mthMakeText(new string[]{"\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n"},new string[]{""},ref strAllText,ref strXml);

							

						#region ���������ǩ��
							m_mthMakeText(new string[]{"\n         ���������ǩ����"},new string[]{""},ref strAllText,ref strXml);
							m_mthMakeText(new string[]{"\n         �ң������������濴�����ϸ�֪���ݣ�������V������ҽ����������ϸ���ͣ������ؿ��ǣ��ң�������������"},new string[]{""},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(new string [] {"��������",""}) != false)
							m_mthMakeText(new string[]{"\n         ͬ�����","�������ƣ�����ͬ��ʹ��($$"},new string[]{"��������",""},ref strAllText,ref strXml);
						else
							m_mthMakeText(new string[]{"\n         ͬ�����___________________�������ƣ�����ͬ��ʹ��("},new string[]{""},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(new string[]{"","����"}) != false)
							m_mthMakeCheckText(new string []{"��","����"},ref strAllText,ref strXml);
						else
							m_mthMakeText(new string[]{"����/  "},new string[]{""},ref strAllText,ref strXml);

						if(m_blnHavePrintInfo(new string[]{"","����"}) != false)
							m_mthMakeCheckText(new string []{"��","����"},ref strAllText,ref strXml);
						else
							m_mthMakeText(new string[]{"����/  "},new string[]{""},ref strAllText,ref strXml);
						
						if(m_blnHavePrintInfo(new string[]{"","����"}) != false)
							m_mthMakeCheckText(new string []{"��","����"},ref strAllText,ref strXml);
						else
							m_mthMakeText(new string[]{"����/  "},new string[]{""},ref strAllText,ref strXml);

							m_mthMakeText(new string[]{")ֲ���"},new string[]{""},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(new string[]{"�����>>Ԫ",""}) != false)
							m_mthMakeText(new string[]{"�۸�Լ����ң���д��","Ԫ��$$"},new string[]{"�����>>Ԫ",""},ref strAllText,ref strXml);
						else
							m_mthMakeText(new string[]{"�۸�Լ����ң���д��___________________________________Ԫ��$$"},new string[]{""},ref strAllText,ref strXml);
						#region ǩ��
						if(m_blnHavePrintInfo(m_strKeysArr4) != false)
							m_mthMakeText(new string[]{"\n\n              ���߱���ǩ����"},m_strKeysArr4,ref strAllText,ref strXml);
						else
							m_mthMakeText(new string[]{"\n\n              ���߱���ǩ����____________________"},new string[]{""},ref strAllText,ref strXml);
	
						if(m_blnHavePrintInfo(m_strKeysArr5) != false)
							m_mthMakeText(new string[]{"\n\n              ���߼���ǩ����"},m_strKeysArr5,ref strAllText,ref strXml);
						else
							m_mthMakeText(new string[]{"\n\n              ���߼���ǩ����_____________________"},new string[]{""},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr6) != false)
						{
							if(m_blnHavePrintInfo(m_strKeysArr5) != false)//��ʽ����
								m_mthMakeText(new string[]{"                              ǩ�����뻼�ߵĹ�ϵ��"},m_strKeysArr6,ref strAllText,ref strXml);
							else
								m_mthMakeText(new string[]{"  ǩ�����뻼�ߵĹ�ϵ��____________________"},m_strKeysArr6,ref strAllText,ref strXml);
						}
						else
						{
							if(m_blnHavePrintInfo(m_strKeysArr5) != false)
							m_mthMakeText(new string[]{"                              ǩ�����뻼�ߵĹ�ϵ��____________________"},new string[]{""},ref strAllText,ref strXml);
						    else
								m_mthMakeText(new string[]{"  ǩ�����뻼�ߵĹ�ϵ��____________________"},new string[]{""},ref strAllText,ref strXml);

						}
						if(m_blnHavePrintInfo(new string []{"������ϵ�绰"}) != false)
						{
							m_mthMakeText(new string[]{"\n\n              ������ϵ�绰��"},new string[]{"������ϵ�绰"},ref strAllText,ref strXml);
							if(m_blnHavePrintInfo(m_strKeysArr7) != false)
								m_mthMakeText(new string[]{"          ǩ���˵����֤���룺"},new string[]{"ǩ���˵����֤����"},ref strAllText,ref strXml);
						    else
								m_mthMakeText(new string[]{"          ǩ���˵����֤���룺____________________"},new string[]{"ǩ���˵����֤����"},ref strAllText,ref strXml);

						}
						else
						{
							m_mthMakeText(new string[]{"\n\n              ������ϵ�绰��____________________"},new string[]{""},ref strAllText,ref strXml);
							if(m_blnHavePrintInfo(m_strKeysArr7) != false)
								m_mthMakeText(new string[]{"  ǩ���˵����֤���룺"},new string[]{"ǩ���˵����֤����"},ref strAllText,ref strXml);
							else
								m_mthMakeText(new string[]{"  ǩ���˵����֤���룺____________________"},new string[]{""},ref strAllText,ref strXml);

						}
	                  if(m_blnHavePrintInfo(m_strKeysArr8) != false) 
						   m_mthMakeText(new string[]{"\n\n              ̸��ҽ��ǩ����","                        ǩ�����ڣ�"},new string[]{"̸��ҽ��ǩ��","ǩ������"},ref strAllText,ref strXml);
					  else
						   m_mthMakeText(new string[]{"\n\n              ̸��ҽ��ǩ����____________________","  ǩ�����ڣ�"},new string[]{"","ǩ������"},ref strAllText,ref strXml);
						#endregion ǩ��
						#endregion
					}
					else
					{
						m_blnHaveMoreLine = false;
						return; 
					}
					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent.m_objItemContents!= null);
					//m_mthAddSign2("������Σ�",m_objPrintContext.m_ObjModifyUserArr);
					m_blnIsFirstPrint = false;					
				}

				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2,m_intRecBaseX+10,p_intPosY,p_objGrp);
					p_intPosY += 20;
				}
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_blnHaveMoreLine = true;
				}
				else
				{
					m_blnHaveMoreLine = false;
				}
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();
				m_blnHaveMoreLine = true;
				m_blnIsFirstPrint = true;
			}
		}
		#endregion
	}
}
