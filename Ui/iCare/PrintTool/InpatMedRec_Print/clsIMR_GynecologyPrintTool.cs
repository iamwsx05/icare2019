using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;

namespace iCare
{
	/// <summary>
	/// ���ƴ�ӡ����
	/// </summary>
	public class clsIMR_GynecologyPrintTool: clsInpatMedRecPrintBase
	{
		public clsIMR_GynecologyPrintTool(string p_strTypeID) :base(p_strTypeID)
		{
		}

		private clsPrintInPatMedRecItem[] m_objPrintOneItemArr;
		private clsPrintInPatMedRecItem[] m_objPrintMultiItemArr;
		private clsPrintInPatMedRecSign[] m_objPrintSignArr;

		private clsPrintInPatMedRecCurePlan[] m_objPrintCurePlanArr;

		protected override void m_mthSetPrintLineArr()
		{
			m_mthInitPrintLineArr();
			m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
				new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
										  new clsPrintPatientFixInfo("����סԺ����",310),
										  m_objPrintOneItemArr[0],m_objPrintOneItemArr[1],
										  m_objPrintMultiItemArr[0],m_objPrintMultiItemArr[1],m_objPrintMultiItemArr[2],m_objPrintMultiItemArr[3],m_objPrintMultiItemArr[4],
										  m_objPrintOneItemArr[2],m_objPrintOneItemArr[3],
										  m_objPrintMultiItemArr[5],m_objPrintMultiItemArr[6],
										 new clsPrintInPatMedRecPic(),
										  m_objPrintOneItemArr[4],m_objPrintOneItemArr[5],m_objPrintSignArr[3],m_objPrintOneItemArr[6],m_objPrintSignArr[4],m_objPrintSignArr[0],
										m_objPrintCurePlanArr[0],m_objPrintCurePlanArr[1],m_objPrintCurePlanArr[2],m_objPrintCurePlanArr[3],m_objPrintCurePlanArr[4],m_objPrintCurePlanArr[5],
										m_objPrintSignArr[1],m_objPrintSignArr[2]
			});
		}

		private void m_mthInitPrintLineArr()
		{
			m_objPrintOneItemArr = new clsPrintInPatMedRecItem[7];
			for(int i1=0;i1<m_objPrintOneItemArr.Length;i1++)
				m_objPrintOneItemArr[i1] = new clsPrintInPatMedRecItem();

			m_objPrintMultiItemArr = new clsPrintInPatMedRecItem[7];
			for(int j2=0;j2<m_objPrintMultiItemArr.Length;j2++)
				m_objPrintMultiItemArr[j2] = new clsPrintInPatMedRecItem();

			m_objPrintSignArr = new clsPrintInPatMedRecSign[5];
			for(int k3=0;k3<m_objPrintSignArr.Length;k3++)
				m_objPrintSignArr[k3] = new clsPrintInPatMedRecSign();

			m_objPrintCurePlanArr = new clsPrintInPatMedRecCurePlan[6];
			for(int m4=0;m4<m_objPrintCurePlanArr.Length;m4++)
				m_objPrintCurePlanArr[m4] = new clsPrintInPatMedRecCurePlan();
		}

		protected override void m_mthSetSubPrintInfo()
		{
			#region ������Ŀ
			m_objPrintOneItemArr[0].m_mthSetPrintValue("����","���ߣ�");
			m_objPrintOneItemArr[1].m_mthSetPrintValue("�ֲ�ʷ","�ֲ�ʷ��");
			m_objPrintOneItemArr[2].m_mthSetPrintValue("�������Ƽ���","�������Ƽ�����");
			m_objPrintOneItemArr[3].m_mthSetPrintValue("����ʷ","����ʷ��");
            m_objPrintOneItemArr[4].m_mthSetPrintValue("�������", "������ϣ�");
            m_objPrintOneItemArr[5].m_mthSetPrintValue("�������", "������ϣ�");
            m_objPrintOneItemArr[6].m_mthSetPrintValue("�������", "������ϣ�");
			#endregion

			#region �¾�ʷ
			m_objPrintMultiItemArr[0].m_mthSetSpecialTitleValue("","�¾�ʷ��");
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"","�¾�ʷ>>����>>����","�¾�ʷ>>����>>����","�¾�ʷ>>����>>����","�¾�ʷ>>����>>����","�¾�ʷ>>����>>Ѫ��"}
				,new string[]{"������","","���ڣ�","������","������","Ѫ�飺"});
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"","�¾�ʷ>>ʹ��>>��������","","�¾�ʷ>>ʹ��>>��ǰ","�¾�ʷ>>ʹ��>>����","�¾�ʷ>>ʹ��>>����","�¾�ʷ>>ʹ��>>��ʹ","�¾�ʷ>>ʹ��>>��ʹ","�¾�ʷ>>ʹ��>>Ż��","�¾�ʷ>>ʹ��>>����","�¾�ʷ>>ʹ��>>�̶�"}
				,new string[]{"\nʹ����","�������䣺","����ʱ�ڣ�","��ǰ��","���ڣ�","����","��ʹ��","��ʹ��","Ż�£�","������","�̶ȣ�"});
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"","�¾�ʷ>>ĩ���¾�>>ĩ���¾�","�¾�ʷ>>ĩ���¾�>>����","�¾�ʷ>>ĩ���¾�>>��ɫ","�¾�ʷ>>ĩ���¾�>>����"}
				,new string[]{"\nĩ���¾���","","������","��ɫ��","������"});
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"","�¾�ʷ>>ĩ��ǰ�¾�>>ĩ��ǰ�¾�","�¾�ʷ>>ĩ��ǰ�¾�>>����","�¾�ʷ>>ĩ��ǰ�¾�>>��ɫ","�¾�ʷ>>ĩ��ǰ�¾�>>����"}
				,new string[]{"\nĩ��ǰ�¾���","","������","��ɫ��","������"});
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"","�¾�ʷ>>�״�>>��","�¾�ʷ>>�״�>>ɫ","�¾�ʷ>>�״�>>��"}
				,new string[]{"\n�״���","����","ɫ��","�᣺"});
			#endregion

			#region ����ʷ�����ʷ,����ʷ,�������,����ʷ
			m_objPrintMultiItemArr[1].m_mthSetSpecialTitleValue("","����ʷ�����ʷ��");
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"���˼����ʷ>>���","���˼����ʷ>>�������","���˼����ʷ>>��������","���˼����ʷ>>�������","���˼����ʷ>>�Բ�ʷ"}
				,new string[]{"��Σ�","������䣺","�������꣺","���������","�Բ�ʷ��"});

			m_objPrintMultiItemArr[2].m_mthSetSpecialTitleValue("","����ʷ��");
			m_objPrintMultiItemArr[2].m_mthSetPrintValue(new string[]{"����ʷ>>���²�","����ʷ>>���","����ʷ>>��Ȼ����","����ʷ>>�˹�����","����ʷ>>ĩ�β�","����ʷ>>�������","","����ʷ>>��","����ʷ>>Ů","����ʷ>>����"}
				,new string[]{"���²���","�����","��Ȼ������","�˹�������","ĩ�β���","���������","  �ִ棺","�ӣ�","Ů��","\n����(�����������)��"});
			
			m_objPrintMultiItemArr[3].m_mthSetSpecialTitleValue("","���������");
			m_objPrintMultiItemArr[3].m_mthSetPrintValue(new string[]{"�������>>�������","�������>>���з���","�������>>�ƻ�������ʩ"}
				,new string[]{"���������","���з�����","�ƻ�������ʩ��"});

			m_objPrintMultiItemArr[4].m_mthSetSpecialTitleValue("","����ʷ��");
			m_objPrintMultiItemArr[4].m_mthSetPrintValue(new string[]{"����ʷ>>���ಡ","����ʷ>>���","����ʷ>>����","����ʷ>>����","����ʷ>>�Բ�","����ʷ>>����"}
				,new string[]{"���ಡ��","��ˣ�","���ף�","���ף�","�Բ���","������"});
			#endregion

			#region �����
			m_objPrintMultiItemArr[5].m_mthSetSpecialTitleValue("�����","");
			m_objPrintMultiItemArr[5].m_mthSetPrintValue(new string[]{"�����>>����","�����>>����","�����>>����","�����>>Ѫѹ","�����>>����"}
				,new string[]{"���£�","������","������","Ѫѹ��","���أ�"});
			m_objPrintMultiItemArr[5].m_mthSetPrintValue(new string[]{"","�����>>һ�����>>����","�����>>һ�����>>Ӫ��","�����>>һ�����>>����","�����>>һ�����>>����","�����>>һ�����>>����"}
				,new string[]{"\nһ�������","������","Ӫ����","���飺","���ǣ�","������"});
			m_objPrintMultiItemArr[5].m_mthSetPrintValue(new string[] {"","�����>>Ƥ��>>����","�����>>Ƥ��>>����","�����>>Ƥ��>>ʧˮ","�����>>Ƥ��>>����"}
				,new string[]{"\nƤ����","���㣺","���","ʧˮ��","������"});
			m_objPrintMultiItemArr[5].m_mthSetPrintValue(new string[] {"","�����>>�ܰ���>>������","�����>>�ܰ���>>Ҹ��","�����>>�ܰ���>>���ɹ�","�����>>�ܰ���>>����","�����>>ͷ��","�����>>����"}
				,new string[]{"\n�ܰ��٣�","�����ѣ�","Ҹ�£�","���ɹ���","������","\nͷ����","������"});
			m_objPrintMultiItemArr[5].m_mthSetPrintValue(new string[] {"","�����>>�ز�>>����","�����>>�ز�>>����","�����>>�ز�>>��","�����>>�ز�>>�鷿","�����>>�ز�>>����"}
				,new string[]{"\n�ز���","������","���ࣺ","�Σ�","�鷿��","����"});
			m_objPrintMultiItemArr[5].m_mthSetPrintValue(new string[]{"","�����>>����>>����","�����>>����>>ѹʹ","�����>>����>>��","�����>>����>>Ƣ","�����>>����>>��","�����>>����>>������","�����>>����>>���䶯","�����>>����>>����"}
				,new string[]{"\n������","���ڣ�","ѹʹ��","�Σ�","Ƣ��","����","�����У�","���䶯��","���"});
			m_objPrintMultiItemArr[5].m_mthSetPrintValue(new string[]{"","�����>>����>>����","�����>>����>>���","�����>>����>>ߵ��ʹ","�����>>����>>����"}
				,new string[]{"\n������","���Σ�","��ȣ�","ߵ��ʹ��","������"});
			m_objPrintMultiItemArr[5].m_mthSetPrintValue(new string[]{"","�����>>��֫>>����","�����>>��֫>>���","�����>>��֫>>ϥ����"}
				,new string[]{"\n��֫��","���Σ�","��ȣ�","ϥ���䣺"});
			#endregion

			#region ���Ƽ��
			m_objPrintMultiItemArr[6].m_mthSetSpecialTitleValue("���Ƽ��","");
			m_objPrintMultiItemArr[6].m_mthSetPrintValue(new string[]{"���Ƽ��>>����","���Ƽ��>>����","���Ƽ��>>�¡","���Ƽ��>>�ӹ���","���Ƽ��>>�ӹ���","���Ƽ��>>����","���Ƽ��>>�ӹ�����֯"}
				,new string[]{"������","\n������","\n�¡��","\n�ӹ�����","\n�ӹ��壺","\n������","\n�ӹ�����֯��"});
			#endregion

			#region ǩ��������
			m_objPrintSignArr[0].m_mthSetPrintSignValue(new string[]{"����ҽʦ","����ҽʦ","סԺҽʦ","ʵϰҽʦ"},new string[]{"����ҽʦ��","����ҽʦ��","סԺҽʦ��","ʵϰҽʦ��"},true);
			m_objPrintSignArr[1].m_mthSetPrintSignValue(new string[]{"���Ƽƻ�>>����ҽ��","���Ƽƻ�>>סԺҽ��"},new string[]{"����ҽ����","סԺҽ����"},true);
            m_objPrintSignArr[2].m_mthSetPrintSignValue(new string[] { "���Ƽƻ�>>����" }, new string[] { "" }, false);
            #region ����/��������Լ�ǩ��
            m_objPrintSignArr[3].m_mthSetPrintSignValue(new string[] { "�������ҽʦǩ��", "�������ҽʦǩ������" }, new string[] { "ҽʦǩ����", "ǩ�����ڣ�" },true);
            m_objPrintSignArr[4].m_mthSetPrintSignValue(new string[] { "�������ҽʦǩ��", "�������ҽʦǩ������" }, new string[] { "ҽʦǩ����", "ǩ�����ڣ�" },true);
            #endregion

			#endregion

			#region ���Ƽƻ�
			m_objPrintCurePlanArr[0].m_BlnPrintInNextPage = true;
			m_objPrintCurePlanArr[0].m_IntCurrentHeight = 100;
			m_objPrintCurePlanArr[0].m_mthSetPrintValue("���Ƽƻ�>>��ϸ���","��ϸ���");
			m_objPrintCurePlanArr[1].m_IntCurrentHeight = 100;
			m_objPrintCurePlanArr[1].m_mthSetPrintValue(new string[]{"���Ƽƻ�>>ʵ���Ҽ��>>Ѫɫ��","���Ƽƻ�>>ʵ���Ҽ��>>��Ѫ��","���Ƽƻ�>>ʵ���Ҽ��>>��Ѫ��","���Ƽƻ�>>ʵ���Ҽ��>>Ѫ��","���Ƽƻ�>>ʵ���Ҽ��>>С����"
																		,"���Ƽƻ�>>ʵ���Ҽ��>>��Һ","���Ƽƻ�>>ʵ���Ҽ��>>ϸ��ͿƬ","���Ƽƻ�>>ʵ���Ҽ��>>��θ����","���Ƽƻ�>>ʵ���Ҽ��>>�ز����߼��","���Ƽƻ�>>ʵ���Ҽ��>>�������"}
				,new string[]{"Ѫɫ�أ�","��Ѫ��","��Ѫ��","Ѫ�ͣ�","\nС���飺","\n��Һ��","ϸ��ͿƬ��","\n��θ���ܣ�","\n�ز����߼�飺","������ϣ�"},"ʵ���Ҽ��");
			m_objPrintCurePlanArr[2].m_IntCurrentHeight = 60;
			m_objPrintCurePlanArr[2].m_mthSetPrintValue("���Ƽƻ�>>��Ժ���","��Ժ���");
			m_objPrintCurePlanArr[3].m_IntCurrentHeight = 100;
			m_objPrintCurePlanArr[3].m_mthSetPrintValue("���Ƽƻ�>>��Ժ�����м����Ŀ","��Ժ�����м����Ŀ");
			m_objPrintCurePlanArr[4].m_IntCurrentHeight = 100;
			m_objPrintCurePlanArr[4].m_mthSetPrintValue("���Ƽƻ�>>���Ƽƻ�","���Ƽƻ�");
			m_objPrintCurePlanArr[5].m_IntCurrentHeight = 100;
			m_objPrintCurePlanArr[5].m_mthSetPrintValue("���Ƽƻ�>>�������","�������");
			#endregion
		}

		#region print class

		/// <summary>
		/// ��Ŀ��ӡ
		/// </summary>
		private class clsPrintInPatMedRecItem : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			/// <summary>
			/// ���д�ӡ�Ĵ������
			/// </summary>
			private string m_strSpecialTitle = "";
			/// <summary>
			/// �����ӡ��С����
			/// </summary>
			private string m_strLeftTitle = "";
			/// <summary>
			/// �����ӡ��С����
			/// </summary>
			private string m_strTitle = "";
			private string m_strText = "";
			private string m_strTextXml = "";
			private bool m_blnNoContent = false;
			private bool m_blnNoPrint = true;
			private clsInpatMedRec_Item m_objItemContent = null;

			public clsPrintInPatMedRecItem()
			{}
			
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_blnNoContent == true && m_blnNoPrint == true)
				{
					m_blnHaveMoreLine = false;

					return;
				}

				if(m_blnIsFirstPrint)
				{
					if(m_strTitle != "")
					{
						p_objGrp.DrawString(m_strTitle,p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
						p_intPosY += 20;
						m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objItemContent==null ? "" : m_objItemContent.m_strItemContent)  ,(m_objItemContent==null ? "<root />" : m_objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,m_objItemContent != null);
						m_mthAddSign2(m_strTitle,m_objPrintContext.m_ObjModifyUserArr);
					}
					else
					{
						if(m_strSpecialTitle != "")
						{
							p_intPosY += 20;
							p_objGrp.DrawString(m_strSpecialTitle,clsIMR_HerbalismPrintTool.m_fotItemHead,Brushes.Black,m_intRecBaseX+300,p_intPosY);
							p_intPosY += 40;
						}
						if(m_strLeftTitle != "")
						{
							p_objGrp.DrawString(m_strLeftTitle,p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
							p_intPosY += 20;
						}
						m_objPrintContext.m_mthSetContextWithCorrectBefore(m_strText ,m_strTextXml,m_dtmFirstPrintTime,m_blnNoPrint == false);
						m_mthAddSign2((m_strSpecialTitle == "" ? m_strLeftTitle:m_strSpecialTitle),m_objPrintContext.m_ObjModifyUserArr);
					}

					m_blnIsFirstPrint = false;					
				}
			
				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					if(m_strTitle != "" || m_strLeftTitle != "")
						m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+30,p_intPosY,p_objGrp);
					else
						m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2,m_intRecBaseX+10,p_intPosY,p_objGrp);
					p_intPosY += 20;

					intLine++;
				}			

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
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
			/// <summary>
			/// ���ö����ӡ����
			/// </summary>
			/// <param name="p_strKeyArr">��ӡ���ݵĹ�ϣ������</param>
			/// <param name="p_strTitleArr">С��������(����Ӧ�ڴ���Lable�����洢�����ݿ�����ӡ������)</param>
			public void m_mthSetPrintValue(string[] p_strKeyArr,string[] p_strTitleArr)
			{
				if(p_strKeyArr == null || p_strTitleArr == null || p_strKeyArr.Length != p_strTitleArr.Length)
				{
					m_blnNoContent = true;
					return;
				}
				m_blnNoPrint = false;
				if(m_blnHavePrintInfo(p_strKeyArr) == true)
					m_mthMakeText(p_strTitleArr,p_strKeyArr,ref m_strText,ref m_strTextXml);
			}
			/// <summary>
			/// ���õ����ӡ����
			/// </summary>
			/// <param name="p_strKey">��ϣ��</param>
			/// <param name="p_strTitle">С����</param>
			public void m_mthSetPrintValue(string p_strKey,string p_strTitle)
			{
				if(m_hasItems != null && p_strKey != null)
					if(m_hasItems.Contains(p_strKey))
						m_objItemContent = m_hasItems[p_strKey] as clsInpatMedRec_Item;
				m_strTitle = p_strTitle;
			}
			/// <summary>
			/// ���ñ��⡰����顱�����¾�ʷ����
			/// </summary>
			/// <param name="p_strBigTitle">���д�ӡ�ı��⣬û�е��ÿ�""��������null</param>
			/// <param name="p_strSTitle">�����ӡ��С���⣬û�е��ÿ�""��������null</param>
			public void m_mthSetSpecialTitleValue(string p_strBigTitle,string p_strSTitle)
			{
				m_strSpecialTitle = p_strBigTitle;
				m_strLeftTitle = p_strSTitle;
			}

		}

		/// <summary>
		/// ǩ��������
		/// </summary>
		private class clsPrintInPatMedRecSign : clsIMR_PrintLineBase
		{
			private clsInpatMedRec_Item[] objSignContent = null;
			private string[] m_strTitleArr = null;
			private bool m_blnPrintInOneLine = false;
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(objSignContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				
				p_intPosY += 30;
				if(m_blnPrintInOneLine)
					m_mthPrintInOneLine(ref p_intPosY,p_objGrp,p_fntNormalText);
				else
					m_mthPrintInMultiLine(ref p_intPosY, p_objGrp, p_fntNormalText);
				p_intPosY += 40;
				
				m_blnHaveMoreLine = false;
			}
//			public void clsPrintInPatMedRecSign()
//			{}
			private void m_mthPrintInOneLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				int intLeft = m_intRecBaseX+20;
				int intStep = 250;
                if (objSignContent.Length >= 3)
                {
                    intLeft += 200;
                }
				if(objSignContent.Length <= 2)
				{
					intLeft += 200;
				}
                int intTemp = 0;
                for (int i = 0; i < objSignContent.Length; i++)
                {
                    intTemp = i;
                    if (intTemp == 2)//3����4��ǩ�������д�ӡ
                    {
                        p_intPosY += 20;
                    }
                    if(intTemp >=2)
                        intTemp -= 2;//�ڶ�������ʼ��ӡ
                    if (m_strTitleArr[i].IndexOf("����") < 0)
                    {
                        p_objGrp.DrawString(m_strTitleArr[i] + (objSignContent[i] == null ? "" : objSignContent[i].m_strItemContent), p_fntNormalText, Brushes.Black, intLeft + intTemp * intStep, p_intPosY);
                    }
                    else
                    {
                        p_objGrp.DrawString(m_strTitleArr[i] + (objSignContent[i] == null ? "" : DateTime.Parse(objSignContent[i].m_strItemContent).ToString("yyyy��MM��dd��")), p_fntNormalText, Brushes.Black, intLeft + intTemp * intStep, p_intPosY);
                    }
                }
			}

			private void m_mthPrintInMultiLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				for(int i=0; i<objSignContent.Length; i++)
				{
					if(m_strTitleArr[i].IndexOf("����") < 0)
					{
						p_objGrp.DrawString(m_strTitleArr[i]+(objSignContent[i]==null ? "" : objSignContent[i].m_strItemContent) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+500,p_intPosY);
						p_intPosY += 20;
					}
					else
					{
						p_objGrp.DrawString(m_strTitleArr[i]+ (objSignContent[i] == null ? "" :DateTime.Parse( objSignContent[i].m_strItemContent).ToString("yyyy��MM��dd��")),p_fntNormalText,Brushes.Black,m_intRecBaseX+500,p_intPosY);
						p_intPosY += 20;
					}
				}
			}

			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
			}
			/// <summary>
			/// ����ǩ��������ֵ
			/// </summary>
			/// <param name="p_strkeyArr">ֵ</param>
			/// <param name="p_strTitleArr">����</param>
			public void m_mthSetPrintSignValue(string[] p_strkeyArr,string[] p_strTitleArr,bool p_blnPrintInOneLine)
			{
				if(p_strkeyArr == null || p_strTitleArr == null || p_strkeyArr.Length != p_strTitleArr.Length)
					return;
				objSignContent = m_objGetContentFromItemArr(p_strkeyArr);
				m_strTitleArr = p_strTitleArr;
				m_blnPrintInOneLine = p_blnPrintInOneLine;
			}

		}

		/// <summary>
		/// ���Ƽƻ�
		/// </summary>
		private class clsPrintInPatMedRecCurePlan : clsIMR_PrintLineBase
		{
			#region Define

			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private string m_strTitle = "";
			private clsInpatMedRec_Item m_objItemContent = null;
			private bool m_blnIsFirstPrint = true;
			private bool m_blnHasPrintTitle = false;
			/// <summary>
			/// ���Ӹ߶�
			/// </summary>
			private const int c_intHeight = 60;
			/// <summary>
			/// �м�����X��
			/// </summary>
			private const int c_intShortLeft = (int)enmRectangleInfo.LeftX + 110;
			/// <summary>
			/// ��ӡ���ݸ��ӿ��
			/// </summary>
			private const int c_intWidth = (int)enmRectangleInfo.RightX - c_intShortLeft;
			/// <summary>
			/// ��ӡС������
			/// </summary>
			private const int c_intTitleWidth = 120;
			private int m_intLongLineTop = 150;
			/// <summary>
			/// ��ӡ���ߵ�X����
			/// </summary>
			private int m_intLeftX = (int)enmRectangleInfo.LeftX -10;

			private int m_intCurrentHeight = 100;
			public int m_IntCurrentHeight
			{
				set{m_intCurrentHeight = value;}
			}
			int m_intPosY;

			private string m_strText = "";
			private string m_strTextXml = "";
			
			private bool m_blnPrintInNextPage = false;
			/// <summary>
			/// �Ƿ�ҳ��ӡ
			/// </summary>
			public bool m_BlnPrintInNextPage
			{
				set{m_blnPrintInNextPage = value;}
			}

			#endregion
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(p_intPosY == m_intLongLineTop +5 && m_blnHasPrintTitle == false)
				{
					m_mthPrintTitle(ref p_intPosY,p_objGrp,p_fntNormalText);
					m_blnHasPrintTitle = true;
				}
				Rectangle rtgTitle = new Rectangle(m_intLeftX,p_intPosY + 5,c_intTitleWidth,c_intHeight);
				Rectangle rtgTitle2 = new Rectangle(c_intShortLeft,p_intPosY + 5,c_intWidth,10);

				StringFormat stfTitle = new StringFormat(StringFormatFlags.FitBlackBox);				
				Font fntTitle = new Font("SimSun",12);	

				int intRealHeight = 0;
				m_intPosY = p_intPosY;
				if(m_blnPrintInNextPage == true)
				{
					p_intPosY += 1169;
					m_blnHaveMoreLine = true;
					m_blnPrintInNextPage = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					if(p_intPosY+40 > ((int)enmRectangleInfo.BottomY -50))
					{
						m_blnHaveMoreLine = true;
						p_intPosY += 500;
						return;
					}
					if(m_strTitle != "")
						p_objGrp.DrawString(m_strTitle,fntTitle ,Brushes.Black,rtgTitle,stfTitle);
					if(m_objItemContent != null)
						m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objItemContent == null ? "" :(m_objItemContent.m_strItemContent == null ? "" : m_objItemContent.m_strItemContent))
							,(m_objItemContent == null ? "<root />" :(m_objItemContent.m_strItemContentXml == null ? "<root />" : m_objItemContent.m_strItemContentXml)),m_dtmFirstPrintTime,m_objItemContent == null);
					else
						m_objPrintContext.m_mthSetContextWithCorrectBefore(m_strText ,m_strTextXml,m_dtmFirstPrintTime,m_strText == "");
					m_mthAddSign2(m_strTitle,m_objPrintContext.m_ObjModifyUserArr);

					m_objPrintContext.m_blnPrintAllBySimSun(11,rtgTitle2,p_objGrp,out intRealHeight,false);
					int intYTemp = p_intPosY - 20;
					if(intRealHeight > m_intCurrentHeight)
						p_intPosY += intRealHeight+5;
					else
						p_intPosY += m_intCurrentHeight+5;
                    //p_objGrp.DrawLine(Pens.Black,m_intLeftX,p_intPosY,(int)enmRectangleInfo.RightX,p_intPosY);
                    //p_objGrp.DrawLine(Pens.Black,m_intLeftX,intYTemp,m_intLeftX,p_intPosY);
                    //p_objGrp.DrawLine(Pens.Black,c_intShortLeft,intYTemp,c_intShortLeft,p_intPosY);
                    //p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.RightX,intYTemp,(int)enmRectangleInfo.RightX,p_intPosY);
					if(m_strTitle == "�������")
					{
                        //p_objGrp.DrawLine(Pens.Black,m_intLeftX,p_intPosY+50,(int)enmRectangleInfo.RightX,p_intPosY+50);
                        //p_objGrp.DrawLine(Pens.Black,m_intLeftX,p_intPosY,m_intLeftX,p_intPosY+50);
                        //p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.RightX,p_intPosY,(int)enmRectangleInfo.RightX,p_intPosY+50);
					}
					m_blnIsFirstPrint = false;
					m_blnHaveMoreLine = false;
				}
			}
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_objPrintContext.m_mthRestartPrint();	
				m_blnIsFirstPrint = true;
			}

			/// <summary>
			/// ��ӡ����ֱ�ߺͱ���
			/// </summary>
			/// <param name="p_intPosY"></param>
			/// <param name="p_objGrp"></param>
			/// <param name="p_fntNormalText"></param>
			private void m_mthPrintTitle(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				Brush slbBtush= new SolidBrush(Color.Black);
				p_objGrp.Clear(Color.White);
                p_objGrp.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle.Trim() + "�������Ƽƻ�", new Font("", 16, FontStyle.Bold), slbBtush, 200, 40);
				
				p_objGrp.DrawString("סԺ�ţ�",p_fntNormalText,slbBtush,660,80);
				p_objGrp.DrawString(m_objPrintInfo.m_strInPatientID ,p_fntNormalText,slbBtush,720,80);	
				
				p_objGrp.DrawLine(Pens.Black,m_intLeftX,110,(int)enmRectangleInfo.RightX,110);

				p_objGrp.DrawString("������",p_fntNormalText,slbBtush,50,115);
				p_objGrp.DrawString(m_objPrintInfo.m_strPatientName  ,p_fntNormalText,slbBtush,100,115);
	
				p_objGrp.DrawString("���䣺",p_fntNormalText,slbBtush,300,115);
				p_objGrp.DrawString(m_objPrintInfo.m_strAge ,p_fntNormalText,slbBtush,350,115);
				
				p_objGrp.DrawString("��Ժ���ڣ�",p_fntNormalText,slbBtush,500,115);
				p_objGrp.DrawString(m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy��MM��dd��"),p_fntNormalText,slbBtush,600,115);
				
				p_objGrp.DrawLine(Pens.Black,m_intLeftX,140,(int)enmRectangleInfo.RightX,140);
				p_objGrp.DrawLine(Pens.Black,m_intLeftX,110,m_intLeftX,155);
				p_objGrp.DrawLine(Pens.Black,c_intShortLeft,140,c_intShortLeft,155);
				p_objGrp.DrawLine(Pens.Black,(int)enmRectangleInfo.RightX,110,(int)enmRectangleInfo.RightX,155);
			}

			/// <summary>
			/// ���ö����ӡ����
			/// </summary>
			/// <param name="p_strKeyArr">��ӡ���ݵĹ�ϣ������</param>
			/// <param name="p_strTitleArr">С��������(����Ӧ�ڴ���Lable�����洢�����ݿ�����ӡ������)</param>
			public void m_mthSetPrintValue(string[] p_strKeyArr,string[] p_strTitleArr,string p_strTitle)
			{
				m_strTitle = p_strTitle;
				if(p_strKeyArr == null || p_strTitleArr == null || p_strKeyArr.Length != p_strTitleArr.Length)
					return;
				if(m_blnHavePrintInfo(p_strKeyArr) == true)
					m_mthMakeText(p_strTitleArr,p_strKeyArr,ref m_strText,ref m_strTextXml);
			}
			/// <summary>
			/// ���õ����ӡ����
			/// </summary>
			/// <param name="p_strKey">��ϣ��</param>
			/// <param name="p_strTitle">С����</param>
			public void m_mthSetPrintValue(string p_strKey,string p_strTitle)
			{
				if(m_hasItems != null && p_strKey != null)
					if(m_hasItems.Contains(p_strKey))
						m_objItemContent = m_hasItems[p_strKey] as clsInpatMedRec_Item;
				m_strTitle = p_strTitle;
			}
		}

		#endregion
	}
}
