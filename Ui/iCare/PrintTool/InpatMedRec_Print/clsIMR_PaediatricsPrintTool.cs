using System;
using System.IO;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;
using System.Windows.Forms;

namespace iCare
{
	/// <summary>
	/// ���ƿ�סԺ������ӡ������..
	/// </summary>
	public class clsIMR_PaediatricsPrintTool: clsInpatMedRecPrintBase
	{
		public clsIMR_PaediatricsPrintTool(string p_strTypeID) :base(p_strTypeID)
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
										  new clsPrintPatientFixInfo("����סԺ����",310),
										  m_objPrintOneItemArr[7],
										  m_objPrintOneItemArr[0],m_objPrintOneItemArr[1],m_objPrintOneItemArr[2],
										  m_objPrintOneItemArr[3],m_objPrintOneItemArr[4],m_objPrintMultiItemArr[0],
										  m_objPrintOneItemArr[5],m_objPrintOneItemArr[6],m_objPrintMultiItemArr[1],
										  m_objPrintMultiItemArr[2],m_objPrintMultiItemArr[3],m_objPrintMultiItemArr[4],
										  m_objPrintMultiItemArr[5],m_objPrintSignArr[0]
									  });
		}
	
		private void m_mthInitPrintLineArr()
		{
			m_objPrintOneItemArr = new clsPrintInPatMedRecItem[8];
			for(int i1=0;i1<m_objPrintOneItemArr.Length;i1++)
				m_objPrintOneItemArr[i1] = new clsPrintInPatMedRecItem();

			m_objPrintMultiItemArr = new clsPrintInPatMedRecItem[6];
			for(int j2=0;j2<m_objPrintMultiItemArr.Length;j2++)
				m_objPrintMultiItemArr[j2] = new clsPrintInPatMedRecItem();

			m_objPrintSignArr = new clsPrintInPatMedRecSign[1];
			for(int k3=0;k3<m_objPrintSignArr.Length;k3++)
				m_objPrintSignArr[k3] = new clsPrintInPatMedRecSign();
		}

		protected override void m_mthSetSubPrintInfo()
		{
			#region ������Ŀ
			m_objPrintOneItemArr[0].m_mthSetPrintValue("����","���ߣ�");
			m_objPrintOneItemArr[1].m_mthSetPrintValue("�ֲ�ʷ","�ֲ�ʷ��");
			m_objPrintOneItemArr[2].m_mthSetPrintValue("��ȥʷ","��ȥʷ��");
			m_objPrintOneItemArr[3].m_mthSetPrintValue("��ȥ�������","��ȥ���������");
			m_objPrintOneItemArr[4].m_mthSetPrintValue("��Ⱦ���Ӵ�ʷ","��Ⱦ���Ӵ�ʷ��");
			m_objPrintOneItemArr[5].m_mthSetPrintValue("����ʷ","����ʷ��");
			m_objPrintOneItemArr[6].m_mthSetPrintValue("����ʷ","����ʷ��");
			m_objPrintOneItemArr[7].m_mthSetPrintValue("��ϵ�绰","��ϵ�绰��");
//			m_objPrintOneItemArr[8].m_mthSetPrintValue("����ϵͳ","����ϵͳ��");
//			m_objPrintOneItemArr[9].m_mthSetPrintValue("�������","������飺");
//			m_objPrintOneItemArr[8].m_mthSetPrintValue("��Ժ���","��Ժ��ϣ�");
			#endregion	
		
			#region Ԥ������ʷ
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"","Ԥ������ʷ>>������","Ԥ������ʷ>>ţ��","Ԥ������ʷ>>�ס��١�������","Ԥ������ʷ>>��������","Ԥ������ʷ>>�������������","Ԥ������ʷ>>����ϸ��","Ԥ������ʷ>>��������","Ԥ������ʷ>>����"}
				                                        ,new string[]{"Ԥ������ʷ��","�����磺","ţ����","�ס��١���������","�������磺","������������磺","����ϸ����","�������磺","������"});


			#endregion
			
			#region ����ʷ
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","����ʷ>>̥��","����ʷ>>����","����ʷ>>���","����ʷ>>����","����ʷ>>����","����ʷ>>��������","����ʷ>>ĸ���ڽ���","����ʷ>>���侭��"},
													     new string[]{"����ʷ��","̥�Σ�","���£�","�����","���","#��","�������أ�","ĸ���ڽ�����","���侭����"});
			
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","����ʷ>>���������>>����","����ʷ>>���������>>24Сʱ","����ʷ>>���������>>7��"},
														 new string[]{"\n�����������","������","24Сʱ��","7��"});
			
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","����ʷ>>�񾭾�����>>�¿�ʼ̧ͷ","����ʷ>>�񾭾�����>>�¿�ʼ̧ͷ",
																		 "����ʷ>>�񾭾�����>>�»���","����ʷ>>�񾭾�����>>�»���",
																		 "����ʷ>>�񾭾�����>>�»�վ","����ʷ>>�񾭾�����>>�»�վ",
																		 "����ʷ>>�񾭾�����>>�»���","����ʷ>>�񾭾�����>>�»���",
																		 "����ʷ>>�񾭾�����>>�³���","����ʷ>>�񾭾�����>>�³���",
																		 "����ʷ>>�񾭾�����>>�»�Ц","����ʷ>>�񾭾�����>>�»�Ц",
																		 "����ʷ>>�񾭾�����>>������","����ʷ>>�񾭾�����>>������",
																	     "����ʷ>>�񾭾�����>>��˵��","����ʷ>>�񾭾�����>>��˵��",
																		 "����ʷ>>�񾭾�����>>����"},
														 new string[]{"\n�񾭾�������",
																	  "","#�¿�ʼ̧ͷ",
																	  "","#�»���",
																	  "","#�»�վ",
																	  "","#�»���",
																	  "","#�³���",
																	  "","#�»�Ц",
																	  "","#������",
																	  "","#��˵����",
																	  "����"});
			#endregion
		
			#region Ӫ��ʷ
			m_objPrintMultiItemArr[2].m_mthSetPrintValue(new string[]{"","","Ӫ��ʷ>>Ӥ����ι��>>ĸ��","Ӫ��ʷ>>Ӥ����ι��>>ĸ��ʱ��","Ӫ��ʷ>>Ӥ����ι��>>�˹�","Ӫ��ʷ>>Ӥ����ι��>>�˹�ʱ��","Ӫ��ʷ>>Ӥ����ι��>>���","Ӫ��ʷ>>Ӥ����ι��>>���ʱ��"},
				new string[]{"Ӫ��ʷ��","Ӥ����ι����","ĸ�飺","ʱ�䣺","\n                    �˹���","ʱ�䣺","\n                    ��ϣ�","ʱ�䣺"});
			
			m_objPrintMultiItemArr[2].m_mthSetPrintValue(new string[]{"","Ӫ��ʷ>>��ʳ���>>ά����D","Ӫ��ʷ>>��ʳ���>>ά����C��","Ӫ��ʷ>>��ʳ���>>��","Ӫ��ʷ>>��ʳ���>>����","Ӫ��ʷ>>��ʳ���>>����","Ӫ��ʷ>>��ʳ���>>����"},
				new string[]{"\n��ʳ��ӣ�","ά����D��","ά����C����","��","����","����","����"});
			
			m_objPrintMultiItemArr[2].m_mthSetPrintValue(new string[]{"","Ӫ��ʷ>>���ڿ�ʼι��"},
				new string[]{"\n��ע����ʼ���䣩","\n����ι��",});
			#endregion

			#region �����
			m_objPrintMultiItemArr[3].m_mthSetPrintValue(new string[]{"","�����>>ס����","�����>>���׻���>>����","�����>>���׻���>>ȫ��","�����>>���׻���"},
				new string[]{"�������","ס���У�","���׻��������У�","ȫ�У�","",});
			
			m_objPrintMultiItemArr[3].m_mthSetPrintValue(new string[]{"�����>>��ס����","�����>>�����кεط���"},
				new string[]{"\n��ס������","\n�����кεط�����"});
			
			#endregion

			#region ����ʷ
			m_objPrintMultiItemArr[4].m_mthSetPrintValue(new string[]{"","����ʷ>>������","����ʷ>>������","����ʷ>>���׽������","����ʷ>>����ְҵ����"},
				new string[]{"����ʷ��","\n�������䣺","#�꣬","���������","ְҵ��"});
			
			m_objPrintMultiItemArr[4].m_mthSetPrintValue(new string[]{"����ʷ>>ĸ����","����ʷ>>ĸ����","����ʷ>>ĸ�׽������","����ʷ>>ĸ��ְҵ����"},
				new string[]{"\nĸ�����䣺","#�꣬","���������","ְҵ��"});

			m_objPrintMultiItemArr[4].m_mthSetPrintValue(new string[]{"����ʷ>>ĸ����ʷ","����ʷ>>ͬ�����估�������","����ʷ>>��ͥ�д�Ⱦ�Լ��Ŵ��Լ���"},
				new string[]{"\nĸ����ʷ��","\nͬ�����估���������","\n��ͥ�д�Ⱦ�Լ��Ŵ��Լ���"});
			
			#endregion
		
			#region �����
			m_objPrintMultiItemArr[5].m_mthSetSpecialTitleValue("�� �� �� ��");
			m_objPrintMultiItemArr[5].m_mthSetPrintValue(new string[]{"","�����>>һ�����>>����","�����>>һ�����>>����","�����>>һ�����>>����","�����>>һ�����>>����","�����>>һ�����>>����","�����>>һ�����>>����","�����>>һ�����>>Ѫѹ1","�����>>һ�����>>Ѫѹ2","�����>>һ�����>>Ѫѹ2"},
				new string[]{"һ�������","���£�","#��","������","#��/��","����","#��/��","Ѫѹ","/","#Kpa"});

			m_objPrintMultiItemArr[5].m_mthSetPrintValue(new string[]{"�����>>һ�����>>����","�����>>һ�����>>����","�����>>һ�����>>���","�����>>һ�����>>���","�����>>һ�����>>ͷΧ","�����>>һ�����>>ͷΧ","�����>>һ�����>>��Χ","�����>>һ�����>>��Χ"},
				new string[]{"\n���أ�","#����","��ߣ�","#����","ͷΧ��","#����","��Χ:","����"});
			
			m_objPrintMultiItemArr[5].m_mthSetPrintValue(new string[]{"","�����>>һ�����>>����","�����>>һ�����>>Ӫ��","�����>>һ�����>>����","�����>>һ�����>>����״̬"},
				new string[]{"\nһ�����","������","Ӫ����","���ݣ�","\n����״̬"});

			m_objPrintMultiItemArr[5].m_mthSetPrintValue(new string[]{"�����>>Ƥ����֬��","�����>>�ܰ�ϵͳ"},
				new string[]{"\nƤ����֬����","\n�ܰ�ϵͳ��",});

			m_objPrintMultiItemArr[5].m_mthSetPrintValue(new string[]{"","�����>>ͷ��>>­��","�����>>ͷ��>>�Ա�","�����>>ͷ��>>��","�����>>ͷ��>>������","�����>>ͷ��>>�ʡ���ǻ","�����>>����","�����>>�ز�:����","�����>>����","�����>>����","�����>>����","�����>>��","�����>>Ƣ","�����>>��֫����","�����>>��ϵͳ","�����>>������ֳ��","����ϵͳ","�������","�����>>�������"},
				new string[]{"\nͷ����","­�ǣ�","ǰ�ѣ��Աߣ�","\n�ۣ�","\n�����ǣ�","\n�ʣ���ǻ��","\n������","\n�ز���������","\n      ���ࣺ","\n      ����","\n      ������","\n          �Σ�","\n          Ƣ��","\n��֫������","\n��ϵͳ��","\n������ֳ����","\n����ϵͳ��","\n������飺","\n�������"});

//			m_objPrintMultiItemArr[5].m_mthSetPrintValue(new string[]{"�����>>Ƥ����֬��","�����>>�ܰ�ϵͳ","","�����>>ͷ��>>­��","�����>>ͷ��>>�Ա�","�����>>ͷ��>>��","�����>>ͷ��>>������","�����>>ͷ��>>�ʡ���ǻ","�����>>����","�����>>�ز�:����","�����>>����","�����>>����","�����>>����","�����>>��","�����>>Ƣ","�����>>��֫����","�����>>��ϵͳ","�����>>������ֳ��","�����>>�������"},
//				new string[]{"\nƤ����֬����","\n�ܰ�ϵͳ��","\nͷ����","­�ǣ�","ǰ�ѣ��Աߣ�","\n�ۣ�","\n�����ǣ�","\n�ʣ���ǻ��","\n������","\n�ز���������","\n���ࣺ","\n������","\n�Σ�","\nƢ��","\n��֫������","\n��ϵ��","\n������ֳ����","\n�������"});
			
			#endregion
			
			#region ǩ��������
			m_objPrintSignArr[0].m_mthSetPrintSignValue(new string[]{"ǩ��","�ϼ�ҽʦǩ��","����"},new string[]{"ǩ����","�ϼ�ҽʦǩ����","���ڣ�"});
//			m_objPrintSignArr[0].m_mthSetPrintSignValue(new string[]{"סԺҽʦ","����ҽʦ","����"},new string[]{"סԺҽʦ��","����ҽʦ��","���ڣ�"});
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
	
	}
}
