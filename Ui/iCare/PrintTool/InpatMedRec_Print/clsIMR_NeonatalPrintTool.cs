using System;
using System.IO;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;
using System.Windows.Forms;
using System.Resources ;
namespace iCare
{
	/// <summary>
	/// ��������סԺ������ӡ������.
	/// </summary>
	public class clsIMR_NeonatalPrintTool: clsInpatMedRecPrintBase
	{
		public clsIMR_NeonatalPrintTool(string p_strTypeID) :base(p_strTypeID)
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private clsPrintInPatMedRecItem[] m_objPrintOneItemArr;
		private clsPrintInPatMedRecItem[] m_objPrintMultiItemArr;
		private clsPrintInPatMedRecSign[] m_objPrintSignArr;

		protected override void m_mthSetPrintLineArr()
		{
			m_mthInitPrintLineArr();
			m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
				new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
										  new clsPrintPatientFixInfo("������סԺ����",295),
										  m_objPrintMultiItemArr[0],m_objPrintOneItemArr[0],m_objPrintOneItemArr[1],
										  m_objPrintMultiItemArr[1],m_objPrintMultiItemArr[2],m_objPrintMultiItemArr[3],
										  m_objPrintOneItemArr[2],m_objPrintMultiItemArr[4],m_objPrintMultiItemArr[5],
										  new clsPrintSubInf(),m_objPrintMultiItemArr[6],m_objPrintOneItemArr[4],m_objPrintSignArr[1],
                                          m_objPrintOneItemArr[3], m_objPrintSignArr[0]
									  });
		}

		private void m_mthInitPrintLineArr()
		{
			m_objPrintOneItemArr = new clsPrintInPatMedRecItem[5];
			for(int i1=0;i1<m_objPrintOneItemArr.Length;i1++)
				m_objPrintOneItemArr[i1] = new clsPrintInPatMedRecItem();

			m_objPrintMultiItemArr = new clsPrintInPatMedRecItem[7];
			for(int j2=0;j2<m_objPrintMultiItemArr.Length;j2++)
				m_objPrintMultiItemArr[j2] = new clsPrintInPatMedRecItem();
 
			m_objPrintSignArr = new clsPrintInPatMedRecSign[2];
			for(int k3=0;k3<m_objPrintSignArr.Length;k3++)
				m_objPrintSignArr[k3] = new clsPrintInPatMedRecSign();
		}




		protected override void m_mthSetSubPrintInfo()
		{
			#region ��ͷ������Ϣ
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"����","����","��Ժ����","����ʱ��",
																	  "������","��������λ","��ְҵ","���绰",
																	  "ĸ����","ĸ������λ","ĸְҵ","ĸ�绰"},
														 new string[]{"���䣺","#��","��Ժ���ڣ�","����ʱ�䣺",
																	  "\n��������","������λ��","ְҵ��","��ϵ�绰��",
																	  "\nĸ������","������λ��","ְҵ��","��ϵ�绰��"});


			#endregion

			#region ������Ŀ
            m_objPrintOneItemArr[0].m_mthSetPrintValue("����", "���ߣ�");
            m_objPrintOneItemArr[1].m_mthSetPrintValue("�ֲ�ʷ", "\n�ֲ�ʷ��");
			m_objPrintOneItemArr[2].m_mthSetPrintValue("��ȥ����","��ȥ������");
			m_objPrintOneItemArr[3].m_mthSetPrintValue("����������ҽʦ���","����������ҽʦ��ϣ�");
			m_objPrintOneItemArr[4].m_mthSetPrintValue("��Ժ���","��Ժ��ϣ�");
//			m_objPrintOneItemArr[5].m_mthSetPrintValue("����ʷ","����ʷ��");
//			m_objPrintOneItemArr[6].m_mthSetPrintValue("����ʷ","����ʷ��");
//			m_objPrintOneItemArr[7].m_mthSetPrintValue("��ϵ�绰","��ϵ�绰��");
			//			m_objPrintOneItemArr[8].m_mthSetPrintValue("��Ժ���","��Ժ��ϣ�");
			#endregion	

			#region ����ʷ
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","","����ʷ>>ĸ�������>>���ա��ڸС��Ѷ�֢"},
												 	 	 new string[]{"����ʷ��","ĸ���������","���ա��ڸС��Ѷ�֢��"});

			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","����ʷ>>����ʷ>>̥","����ʷ>>����ʷ>>̥",
																		 "����ʷ>>����ʷ>>��","����ʷ>>����ʷ>>��",
																		 "����ʷ>>����ʷ>>��������","����ʷ>>����ʷ>>����������²������ڲ���",
																		 "����ʷ>>����ʷ>>˫̥","����ʷ>>����ʷ>>���䷽ʽ","����ʷ>>����ʷ>>��ˮ����","����ʷ>>����ʷ>>��ˮ����",
																		 "����ʷ>>����ʷ>>��ˮ����","����ʷ>>����ʷ>>�����ӳ�","����ʷ>>����ʷ>>�����ӳ�","����ʷ>>����ʷ>>̥�����","����ʷ>>����ʷ>>����쳣",},
													     new string[]{"\n����ʷ��","�ڣ�","#̥","�ڣ�","#��","����������","",
																	  "˫̥","���䷽ʽ",
																	  "��ˮ����:","#Сʱ���죩","��ˮ���ʣ�","�����ӳ���","#Сʱ",
																	  "̥�������","����쳣��"});

			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"����ʷ>>����ʷ>>����ǰ������ҩ","����ʷ>>����ʷ>>������Ϣ","����ʷ>>����ʷ>>����ص�","����ʷ>>����ʷ>>���ԭ��"},
														 new string[]{"\n����ǰ������ҩ��","������Ϣ��","\n����ص㣺","���ԭ��"});


			#endregion

			#region ����Ӥ�����
			m_objPrintMultiItemArr[2].m_mthSetPrintValue(new string[]{"","���������>>��������","���������>>��������","���������>>������ʼʱ��","���������>>����",
																	  "���������>>Ƥ����ɫ","���������>>��֫����","���������>>��ˮ����ʷ","",
																	  "���������>>һ����","���������>>�����","���������>>ʮ����","���������>>��Ϣ",
																	  "���������>>��Ϣԭ��","���������>>���ȴ�ʩ","���������>>���","���������>>����","���������>>����",
																	  "���������>>��߷�","���������>>��߷�","���������>>����ʧ","���������>>����ʧ","���������>>�̶�",
																	  "���������>>���","���������>>���","���������>>̥��","���������>>̥��","���������>>����","���������>>����"},
														 new string[]{"������״����","�������أ�","#����","������ʼʱ�䣺","������","Ƥ����ɫ��","\n��֫������","��ˮ����ʷ��",
																	  "\nAPgar���֣�","һ���ӣ�","����ӣ�","ʮ���ӣ�","��Ϣ��","\n��Ϣԭ��","���ȴ�ʩ��","��ܣ�",
																	  "\n����ڣ�","#�����","�ڣ�","#��߷�","�ڣ�","#����ʧ","�̶ȣ�","\n����ڣ�","#������","̥��ڣ�","#����","�ڣ�","#������"});


			#endregion

			#region ι��ʷ�ͽ���ʷ
			m_objPrintMultiItemArr[3].m_mthSetPrintValue(new string[]{"","ι��ʷ�ͽ���ʷ>>����ʱ��","ι��ʷ�ͽ���ʷ>>��ʽ","ι��ʷ�ͽ���ʷ>>���������"},
														 new string[]{"ι��ʷ�ͽ���ʷ��","����ʱ�䣺","��ʽ:","��������֣�"});
		    #endregion
		
			#region ����ʷ
			m_objPrintMultiItemArr[4].m_mthSetPrintValue(new string[]{"","����ʷ>>������","����ʷ>>������","����ʷ>>���׽������","����ʷ>>��Ѫ��"},
				new string[]{"����ʷ��","\n�����䣺","#�꣬","���������","Ѫ�ͣ�"});
			
			m_objPrintMultiItemArr[4].m_mthSetPrintValue(new string[]{"����ʷ>>ĸ����","����ʷ>>ĸ����","����ʷ>>ĸ�׽������","����ʷ>>��Ѫ��"},
				new string[]{"\nĸ���䣺","#�꣬","���������","Ѫ�ͣ�"});

			m_objPrintMultiItemArr[4].m_mthSetPrintValue(new string[]{"����ʷ>>�������","����ʷ>>����","����ʷ>>����","����ʷ>>��̥","����ʷ>>����",
																	  "����ʷ>>����","����ʷ>>ͬ�����估�������"},
				new string[]{"\n���������","������","#��","��̥","����","\n���䣺","\nͬ�����估�������:"});
			
			#endregion

			#region �����1
			m_objPrintMultiItemArr[5].m_mthSetSpecialTitleValue("�� �� �� ��");
			m_objPrintMultiItemArr[5].m_mthSetPrintValue(new string[]{"","�����>>һ�����>>����","�����>>һ�����>>����",
																	  "�����>>һ�����>>����","�����>>һ�����>>����",
																	  "�����>>һ�����>>����","�����>>һ�����>>����",
																	  "�����>>һ�����>>Ѫѹ","�����>>һ�����>>Ѫѹ",
																	  "�����>>һ�����>>����","�����>>һ�����>>����"},
														 new string[]{"һ�������","���£�","#��","������","#��/��","����","#��/��","Ѫѹ","#mmHg","����","Kg"});

			m_objPrintMultiItemArr[5].m_mthSetPrintValue(new string[]{"�����>>һ�����>>���","�����>>һ�����>>���",
																	  "�����>>һ�����>>ͷΧ","�����>>һ�����>>ͷΧ",
																	  "�����>>һ�����>>��Χ","�����>>һ�����>>��Χ",
																	  "�����>>һ�����>>����"},
														 new string[]{"��ߣ�","#Cm","ͷΧ��","#Cm","��Χ:","Cm","����:"});
			
			m_objPrintMultiItemArr[5].m_mthSetPrintValue(new string[]{"","�����>>һ�����>>����","�����>>һ�����>>Ӫ��","�����>>һ�����>>��ɫ","�����>>һ�����>>����"},
														 new string[]{"\nһ�����","������","Ӫ����","��ɫ��","����"});

			m_objPrintMultiItemArr[5].m_mthSetPrintValue(new string[]{"","�����>>Ƥ��>>Ƥ���λ","�����>>Ƥ��>>����",
																	  "�����>>Ƥ��>>��λ","�����>>Ƥ��>>Ӳ��","�����>>Ƥ��>>��λ1","�����>>Ƥ��>>ˮ��",
																	  "�����>>Ƥ��>>��Ƥ","�����>>Ƥ��>>Ƥ��֬��","�����>>Ƥ��>>Ƥ������"},
               new string[]{"\nƤ  ����","Ƥ���λ��","����:","��λ:","Ӳ��:","��λ:","ˮ��:","��Ƥ:","Ƥ��֬��:","Ƥ������:"});

			m_objPrintMultiItemArr[5].m_mthSetPrintValue(new string[]{"�����>>�ܰ�ϵͳ"},
							                             new string[]{"\n�ܰ�ϵͳ��"});

//			m_objPrintMultiItemArr[5].m_mthSetPrintValue(new string[]{"","�����>>ͷ��>>­��","�����>>ͷ��>>�Ա�","�����>>ͷ��>>��","�����>>ͷ��>>������","�����>>ͷ��>>�ʡ���ǻ","�����>>����","�����>>�ز�:����","�����>>����","�����>>����","�����>>����","�����>>��","�����>>Ƣ","�����>>��֫����","�����>>��ϵͳ","�����>>������ֳ��","�����>>�������"},
//				new string[]{"\nͷ����","­�ǣ�","ǰ�ѣ��Աߣ�","\n�ۣ�","\n�����ǣ�","\n�ʣ���ǻ��","\n������","\n�ز���������","\n���ࣺ","\n����","\n������","\n�Σ�","\nƢ��","\n��֫������","\n��ϵ��","\n������ֳ����","\n�������"});

//		    PringSub(
			

		
			#endregion
		
			#region  �����2
			m_objPrintMultiItemArr[6].m_mthSetPrintValue(new string[]{"","�����>>����ϵͳ>>ͷ­����","�����>>����ϵͳ>>�ȷ�ͷ","�����>>����ϵͳ>>Ѫ��",
																		 "�����>>����ϵͳ>>ͷ��","�����>>����ϵͳ>>­����","�����>>����ϵͳ>>ǰ��",
																		 "�����>>����ϵͳ>>����","�����>>����ϵͳ>>��¡","�����>>����ϵͳ>>����",
																		 "�����>>����ϵͳ>>���","�����>>����ϵͳ>>�Ƿ�"},
				new string[]{"\n����ϵͳ��","ͷ­���Σ�","�ȷ�ͷ��","Ѫ�ף�","ͷ����","­������","ǰ�ѣ�","���ף�",
								"��¡��","���ݣ�","��ѣ�","�Ƿ죺"});
			
			m_objPrintMultiItemArr[6].m_mthSetPrintValue(new string[]{"","�����>>����ϵͳ>>����","�����>>����ϵͳ>>������ͣ","�����>>����ϵͳ>>����",
																		 "�����>>����ϵͳ>>��ĭ","�����>>����ϵͳ>>���ܡ������","�����>>����ϵͳ>>����",
																		 "�����>>����ϵͳ>>������","�����>>����ϵͳ>>����","�����>>����ϵͳ>>����"},
				new string[]{"\n����ϵͳ��","���ɣ�","������ͣ��","������","��ĭ��","���ܡ�����礣�","���ȣ�","��������",
								"\n������","���"});

			m_objPrintMultiItemArr[6].m_mthSetPrintValue(new string[]{"","�����>>ѭ��ϵͳ>>����","�����>>ѭ��ϵͳ>>����","�����>>ѭ��ϵͳ>>�Ľ�",
																		 "�����>>ѭ��ϵͳ>>����","�����>>ѭ��ϵͳ>>����","�����>>ѭ��ϵͳ>>����",
																		 "�����>>ѭ��ϵͳ>>����","�����>>ѭ��ϵͳ>>ĩ��ѭ�����"},
				new string[]{"\nѭ��ϵͳ��","���","���","�Ľ磺","���ʣ�","���ɣ�","������","������",
								"\nĩ��ѭ�������"});

			m_objPrintMultiItemArr[6].m_mthSetPrintValue(new string[]{"","�����>>����ϵͳ>>��ǻճĤ","�����>>����ϵͳ>>��","�����>>����ϵͳ>>��̦",
																		 "�����>>����ϵͳ>>����>>�����͡����š�����","�����>>����ϵͳ>>����>>������","�����>>����ϵͳ>>����>>����",
																		 "�����>>����ϵͳ>>������","�����>>����ϵͳ>>������","�����>>����ϵͳ>>����",
																		 "�����>>����ϵͳ>>����","�����>>����ϵͳ>>Ƣ����","�����>>����ϵͳ>>Ƣ����","�����>>����ϵͳ>>����"},
				new string[]{"\n����ϵͳ��","��ǻճĤ��","�ʣ�","��̦��","\n�����͡����š����Σ�","��������","���飺",
								"�����£�","#Cm��","���£�","#Cm��","Ƣ���£�","#cm","���ţ�"});
			
			m_objPrintMultiItemArr[6].m_mthSetPrintValue(new string[]{"","�����>>��ϵͳ>>���ֿ�","�����>>��ϵͳ>>���䡢����","�����>>��ϵͳ>>��˱",
																		 "�����>>��ϵͳ>>ʳ��","�����>>��ϵͳ>>�ճ�","�����>>��ϵͳ>>ӵ��",
																		 "�����>>��ϵͳ>>ϥ��","�����>>��ϵͳ>>������","","�����>>��>>����",
																		 "�����>>��>>���","�����>>��>>ͫ�׶Թⷴ��"},
				new string[]{"\n��ϵͳ��","���ֿ���","���䡢���ʣ�","��˱��","ʳ�٣�","�ճ֣�","ӵ����","ϥ�죺",
								"��������","\n�ۣ�","���ӣ�","�����","ͫ�׶Թⷴ��"});
			
			m_objPrintMultiItemArr[6].m_mthSetPrintValue(new string[]{"�����>>����ϵͳ","�����>>��֫","�����>>����","�����>>���ﻯ��"},
				new string[]{"\n����ϵͳ��","��֫��","\n������","\n���ﻯ�飺"});			
			#endregion

			#region ǩ��������
			m_objPrintSignArr[0].m_mthSetPrintSignValue(new string[]{"����ҽʦǩ��","ǩ������"},new string[]{"����ҽʦǩ����","ǩ�����ڣ�"});
			m_objPrintSignArr[1].m_mthSetPrintSignValue(new string[]{"סԺҽʦǩ��"},new string[]{"סԺҽʦǩ����"});
			//			m_objPrintSignArr[1].m_mthSetPrintSignValue(new string[]{"�������Ƽƻ�>>ҽʦ","�������Ƽƻ�>>ʱ��"},new string[]{"ҽʦ��","���ڣ�"});
			#endregion

		}


		#region Print Class

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
			private string m_strSpecialTitle = "";
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
						m_objPrintContext.m_mthSetContextWithCorrectBefore(m_strText ,m_strTextXml,m_dtmFirstPrintTime,m_blnNoPrint == false);
						m_mthAddSign2(m_strSpecialTitle,m_objPrintContext.m_ObjModifyUserArr);
					}

					m_blnIsFirstPrint = false;					
				}
			
				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					if(m_strTitle != "")
						m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+50,p_intPosY,p_objGrp);
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
			/// ���ô�����硰����顱
			/// </summary>
			/// <param name="p_strTitle"></param>
			public void m_mthSetSpecialTitleValue(string p_strTitle)
			{
				m_strSpecialTitle = p_strTitle;
			}

		}

		/// <summary>
		/// ǩ��������
		/// </summary>
		private class clsPrintInPatMedRecSign : clsIMR_PrintLineBase
		{
			private clsInpatMedRec_Item[] objSignContent = null;
			private string[] m_strTitleArr = null;
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(objSignContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				p_intPosY += 40;
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
				m_blnHaveMoreLine = false;
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
			public void m_mthSetPrintSignValue(string[] p_strkeyArr,string[] p_strTitleArr)
			{
				if(p_strkeyArr == null || p_strTitleArr == null || p_strkeyArr.Length != p_strTitleArr.Length)
					return;
				objSignContent = m_objGetContentFromItemArr(p_strkeyArr);
				m_strTitleArr = p_strTitleArr;
			}

		}

		#endregion

		
		#region ����ӡ
		/// <summary>
		/// ����ӡ
		/// </summary>
		private class clsPrintSubInf : clsIMR_PrintLineBase
		{
			#region Define

			private clsPrintRichTextContext m_objDiagnoseR = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsPrintRichTextContext m_objDiagnoseL = new clsPrintRichTextContext(Color.Black,new Font("",10));
//			private string m_strTitle = "";
//			private string[] m_strTitleArr = null;
     		private string m_strImagePath = Directory.GetParent(Directory.GetParent(Application.StartupPath).ToString()) + "\\picture\\Ophthalmology\\";

			/// <summary>
			/// ���Ӹ߶�
			/// </summary>
			private const int c_intHeight = 40;
			/// <summary>
			/// ������X��
			/// </summary>
			private const int c_intShortLeft = 140;
			/// <summary>
			/// ������X��
			/// </summary>
			private const int c_intShortRight = 463;
			/// <summary>
			/// ��ӡ���ݸ��ӿ��
			/// </summary>
			//private const int c_intWidth = 323;
			private const int c_intWidth = 107;
			/// <summary>
			/// ��ӡС������
			/// </summary>
			private const int c_intTitleWidth = 80;
//			private int m_intLongLineTop = 150;
			/// <summary>
			/// ��ӡ���ߵ�X����
			/// </summary>
//			private int m_intLeftX = (int)enmRectangleInfo.LeftX -10;

//			private int m_intIndex = 0;
//			int m_intPosY;

//			private bool m_IsPrintCol0=false;
			private bool m_IsPrintCol1=false;
			private bool m_IsPrintCol2=false;
			private bool m_IsPrintCol3=false;
			private bool m_IsPrintCol4=false;
			private bool m_IsPrintCol5=false;
			private bool m_IsPrintCol6=false;
			private Pen PrintPenInf =new Pen(Color.Black ,1);

//			private bool m_IsPrintCol7=false;

			#endregion

			public clsPrintSubInf()
			{}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{

				int ColHeight;
				p_objGrp.DrawString ("����ĳ���ȣ���һ��������д��",p_fntNormalText,Brushes.Black,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY);
//				p_intPosY+=20;
				ColHeight=30;
				p_intPosY+=30;

				if (m_mthIsPage(p_intPosY,ColHeight)) {return;}
//				#region ���������
////				p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY-10 ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY-10);
//				#endregion
                System.Drawing.Font p_TsFont=new Font (p_fntNormalText.Name ,p_fntNormalText.Size -3);
				m_mthPrintDetail(ref p_intPosY,p_objGrp,p_TsFont,"     ����                    ����",ColHeight,-1,-1,"");
				p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY-10 ,(float)(enmRectangleInfo.LeftX+80) ,(float)p_intPosY+ColHeight+10);
				m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"   0   ",ColHeight,0,-1,"");
				m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"   1   ",ColHeight,1,-1,"");
				m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"   2   ",ColHeight,2,-1,"");
				m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"   3   ",ColHeight,3,-1,"");
				m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"   4   ",ColHeight,4,-1,"");
				m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"   5   ",ColHeight,5,-1,"");

				ColHeight=80;
				if (m_IsPrintCol1==false)
				{
					//��ҳ��
					if (m_mthIsPage(p_intPosY,ColHeight)) {p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY);return;}
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"Ƥ��",ColHeight,-1,-1,"");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"��ճ͸����ɫ",ColHeight,0,-1,"����ĳ����>>Ƥ��0");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"�⻬�ۺ�ɼ�",ColHeight,1,-1,"����ĳ����>>Ƥ��1");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"�����ѻ���������",ColHeight,2,-1,"����ĳ����>>Ƥ��2");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"��Ƥ�Ѻۣ�Ƥ������ת�ף���������",ColHeight,3,-1,"����ĳ����>>Ƥ��3");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"Ƥ���Ժ񣬳���Ƥֽ�����Ѻ����Ѫ��",ColHeight,4,-1,"����ĳ����>>Ƥ��4");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"Ƥ���񣬳�Ƥ���������Ѻ��ƻ�",ColHeight,5,-1,"����ĳ����>>Ƥ��5");
					m_IsPrintCol1=true;
				}

				ColHeight=20;
				if (m_IsPrintCol2==false)
				{
					if (m_mthIsPage(p_intPosY,ColHeight)) {p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY);return;}
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"̥ë",ColHeight,-1,-1,"");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"��",ColHeight,0,-1,"����ĳ����>>̥ë0");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"��",ColHeight,1,-1,"����ĳ����>>̥ë1");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"��",ColHeight,2,-1,"����ĳ����>>̥ë2");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"����ͺ",ColHeight,3,-1,"����ĳ����>>̥ë3");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"�󲿷�ͺ",ColHeight,4,-1,"����ĳ����>>̥ë4");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"",ColHeight,5,-1,"");
					m_IsPrintCol2=true;
				}

				ColHeight=40;
				if (m_IsPrintCol3==false)
				{
					if (m_mthIsPage(p_intPosY,ColHeight)) {p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY);return;}
                    m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText, "�������", ColHeight, -1, -1, "");
                    m_mthPrintDetail(ref p_intPosY, p_objGrp, p_fntNormalText, "��", ColHeight, 0, -1, "����ĳ����>>�������0");
                    m_mthPrintDetail(ref p_intPosY, p_objGrp, p_fntNormalText, "ϸ���", ColHeight, 1, -1, "����ĳ����>>�������1");
                    m_mthPrintDetail(ref p_intPosY, p_objGrp, p_fntNormalText, "ǰ���к��Ѻ�", ColHeight, 2, -1, "����ĳ����>>�������2");
                    m_mthPrintDetail(ref p_intPosY, p_objGrp, p_fntNormalText, "ǰ2/3�ɼ��Ѻ�", ColHeight, 3, -1, "����ĳ����>>�������3");
                    m_mthPrintDetail(ref p_intPosY, p_objGrp, p_fntNormalText, "ȫ��׼��Ѻ�", ColHeight, 4, -1, "����ĳ����>>�������4");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"",ColHeight,5,-1,"");
					m_IsPrintCol3=true;
				}

				ColHeight=40;
				if (m_IsPrintCol4==false)
				{
					if (m_mthIsPage(p_intPosY,ColHeight)) {p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY);return;}
                    m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText, "�鷿���", ColHeight, -1, -1, "");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"���Դ���",ColHeight,0,-1,"����ĳ����>>�鷿���0");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"ƽ",ColHeight,1,-1,"����ĳ����>>�鷿���1");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"���λ���״<1-2mm",ColHeight,2,-1,"����ĳ����>>�鷿���2");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"����¡��3-4mm",ColHeight,3,-1,"����ĳ����>>�鷿���3");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"������5-10mm",ColHeight,4,-1,"����ĳ����>>�鷿���4");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"",ColHeight,5,-1,"");
					m_IsPrintCol4=true;
				}

				ColHeight=60;
				if (m_IsPrintCol5==false)
				{
					if (m_mthIsPage(p_intPosY,ColHeight)) {p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY);return;}
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"��  ",ColHeight,-1,-1,"");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"����ƽ̹���۵�",ColHeight,0,-1,"����ĳ����>>��0");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"������������۵���ָ�����",ColHeight,1,-1,"����ĳ����>>��1");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"�����������ã��۵����׻ָ�",ColHeight,2,-1,"����ĳ����>>��2");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"�����γɣ������۵��������ָ�",ColHeight,3,-1,"����ĳ����>>��3");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"������Ǳ����Ӳ",ColHeight,4,-1,"����ĳ����>>��4");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"",ColHeight,5,-1,"");
					m_IsPrintCol5=true;
				}

				ColHeight=40;
				if (m_IsPrintCol6==false)
				{
					if (m_mthIsPage(p_intPosY,ColHeight)) {p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY);return;}
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"   ��",ColHeight,-1,-1,"");
					p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15+40),(float)p_intPosY-10,(float)(enmRectangleInfo.LeftX+15+40),(float)p_intPosY+ColHeight+10);
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"����ֳ��",100,-2,-1,"");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"���ҿ���������",ColHeight,0,-1,"����ĳ����>>��0");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"",ColHeight,1,-1,"");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"غ���½�����������",ColHeight,2,-1,"����ĳ����>>��2");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"غ���½�����������",ColHeight,3,-1,"����ĳ����>>��3");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"غ��������������",ColHeight,4,-1,"����ĳ����>>��4");
					m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"",ColHeight,5,-1,"");
					m_IsPrintCol6=true;
				}

				ColHeight=40;
				if (m_mthIsPage(p_intPosY,ColHeight)) {p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY);return;}
				m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"   Ů",ColHeight,-1,(int)(enmRectangleInfo.LeftX+15+40),"");
				p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15+40),(float)p_intPosY-10,(float)(enmRectangleInfo.LeftX+15+40),(float)p_intPosY+ColHeight+10);
				m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"���ټ�С��������¶",ColHeight,0,-1,"����ĳ����>>Ů0");
				m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"",ColHeight,1,-1,"");
				m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"С������¶",ColHeight,2,-1,"����ĳ����>>Ů2");
				m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"С����������¶",ColHeight,3,-1,"����ĳ����>>Ů3");
				m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"��������ȫ�ڸ����ٺ�С����",ColHeight,4,-1,"����ĳ����>>Ů4");
				m_mthPrintDetail(ref p_intPosY,p_objGrp,p_fntNormalText,"",ColHeight,5,-1,"");
				#region ������
				p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY);
				#endregion
				m_blnHaveMoreLine = false;
			}

			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_objDiagnoseR.m_mthRestartPrint();	
				m_objDiagnoseL.m_mthRestartPrint();	
			}

			private void m_mthPrintDetail(ref int p_intPosY, System.Drawing.Graphics p_objGrp, 
										  System.Drawing.Font p_fntNormalText,
										  string PrintStr,int CellHeight,int Col,int p_LineX,string p_Key)
			{
				StringFormat p_StrFormat=new StringFormat ();
				p_StrFormat.FormatFlags =StringFormatFlags.FitBlackBox;
				

				Rectangle rtgCellinf = new Rectangle(0,0,0,0);
//				Pen PrintPenInf =new Pen(Color.Black ,1);
				if (Col==-1) 
				{
					#region ��������˵��л�����
					if (p_LineX==-1)
					{
						p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY);
					}
					else
					{
						p_objGrp.DrawLine (PrintPenInf,p_LineX,(float)p_intPosY ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY);
					}
					#endregion
					p_intPosY+=10;

					#region ������ߵ�����
					p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)(p_intPosY-10) ,(float)(enmRectangleInfo.LeftX+15) ,(float)(p_intPosY+CellHeight+10));
					#endregion
					
				}
				
				switch (Col)
				{
					case 0:
					case 1:
					case 2:
					case 3:
					case 4:
					case 5:
						#region ��ȥ��һ�и���ÿ�л�����
						p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+80+c_intWidth*Col)+20,(float)(p_intPosY-10) ,(float)(enmRectangleInfo.LeftX+80+c_intWidth*Col)+20 ,(float)(p_intPosY+CellHeight+10));
						#endregion
						rtgCellinf=new Rectangle ((int)(enmRectangleInfo.LeftX+80+c_intWidth*Col)+20,p_intPosY,c_intWidth,CellHeight);
						break;
					case -2:
						rtgCellinf=new Rectangle ((int)(enmRectangleInfo.LeftX+20)+5,p_intPosY,20,CellHeight);
						break;
					default:
						rtgCellinf=new Rectangle ((int)(enmRectangleInfo.LeftX+10)+5,p_intPosY,c_intWidth,CellHeight);
						#region �����ұߵ�����
						p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.RightX-20),(float)(p_intPosY-10) ,(float)(enmRectangleInfo.RightX-20) ,(float)(p_intPosY+CellHeight+10));
						#endregion 
						break;
			
				}

				if(m_hasItems.Contains(p_Key))
				{
                    p_objGrp.DrawString("��" + PrintStr, p_fntNormalText, Brushes.Black, rtgCellinf, p_StrFormat);
                   // p_objGrp.DrawString(PrintStr, p_fntNormalText, Brushes.Black, rtgCellinf, p_StrFormat);
				}
				else
				{
					p_objGrp.DrawString (PrintStr,p_fntNormalText,Brushes.Black,rtgCellinf,p_StrFormat);
				}
                if (Col == 5)
				{
					p_intPosY=p_intPosY+CellHeight+10;
//					p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+80+c_intWidth*Col),(float)(p_intPosY-10) ,(float)(enmRectangleInfo.LeftX+80+c_intWidth*Col) ,(float)(p_intPosY+CellHeight+10));
				}
			}

			private bool m_mthIsPage(int p_intPosY,int p_ColHeight)
			{
				if(p_intPosY+40+p_ColHeight > ((int)enmRectangleInfo.BottomY -50))
				{
					m_blnHaveMoreLine = true;
					
					p_intPosY += 500;
					return true;
				}
				else
				{
					return false;
				}

			}

		}

		#endregion
	}
}
