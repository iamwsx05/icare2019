using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;


namespace iCare
{
	/// <summary>
	/// ���סԺ������ӡ������
	/// </summary>
	public class clsIMR_BurnSuergeryPrintTool: clsInpatMedRecPrintBase
	{
		public clsIMR_BurnSuergeryPrintTool(string p_strTypeID) :base(p_strTypeID)
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
										  new clsPrintPatientFixInfo("�������סԺ����",290),
										  new clsPrintSubTime(),
										  m_objPrintOneItemArr[0],m_objPrintOneItemArr[1],m_objPrintOneItemArr[2],m_objPrintOneItemArr[3],
										  m_objPrintOneItemArr[4],m_objPrintOneItemArr[5],m_objPrintOneItemArr[6],
										  m_objPrintMultiItemArr[0],
										  new clsPrintInPatMedRecPic(),
										  new clsPrintSubInf(),m_objPrintOneItemArr[7],m_objPrintSignArr[0],m_objPrintOneItemArr[8],m_objPrintSignArr[1]
									  });
		}

		private void m_mthInitPrintLineArr()
		{
			m_objPrintOneItemArr = new clsPrintInPatMedRecItem[9];
			for(int i1=0;i1<m_objPrintOneItemArr.Length;i1++)
				m_objPrintOneItemArr[i1] = new clsPrintInPatMedRecItem();

			m_objPrintMultiItemArr = new clsPrintInPatMedRecItem[1];
			for(int j2=0;j2<m_objPrintMultiItemArr.Length;j2++)
				m_objPrintMultiItemArr[j2] = new clsPrintInPatMedRecItem();

            m_objPrintSignArr = new clsPrintInPatMedRecSign[2];
            for (int k3 = 0; k3 < m_objPrintSignArr.Length; k3++)
                m_objPrintSignArr[k3] = new clsPrintInPatMedRecSign();
		}




		protected override void m_mthSetSubPrintInfo()
		{
			#region ������Ŀ
			m_objPrintOneItemArr[0].m_mthSetPrintValue("����ԭ���뾭��","����ԭ���뾭��(������¶ȡ����˿ռ䡢���š���𷽷�)��");
			m_objPrintOneItemArr[1].m_mthSetPrintValue("��Ժǰ���鼰����","��Ժǰ���鼰����(����ʼʱ�䣬���飬���ȿ��ݿˡ����洦���)��");
			m_objPrintOneItemArr[2].m_mthSetPrintValue("����(���ߡ�ʱ�䡢;�д���)","����(���ߡ�ʱ�䡢;�д���)��");
			m_objPrintOneItemArr[3].m_mthSetPrintValue("�ϲ��ˡ��ж�","�ϲ��ˡ��ж���");
			m_objPrintOneItemArr[4].m_mthSetPrintValue("��ȥʷ","��ȥʷ��");
			m_objPrintOneItemArr[5].m_mthSetPrintValue("����ʷ","����ʷ(���¾�������ʷ)��");
			m_objPrintOneItemArr[6].m_mthSetPrintValue("����ʷ","����ʷ��");
            m_objPrintOneItemArr[7].m_mthSetPrintValue("�������", "������ϣ�");
            m_objPrintOneItemArr[8].m_mthSetPrintValue("�������", "������ϣ�");
			#endregion	
            
            #region ����/��������Լ�ǩ��
            m_objPrintSignArr[0].m_mthSetPrintSignValue(new string[] { "�������ҽʦǩ��", "�������ҽʦǩ������" }, new string[] { "ҽʦǩ����", "ǩ�����ڣ�" });
            m_objPrintSignArr[1].m_mthSetPrintSignValue(new string[] { "�������ҽʦǩ��", "�������ҽʦǩ������" }, new string[] { "ҽʦǩ����", "ǩ�����ڣ�" });
            #endregion

			#region ���
			m_objPrintMultiItemArr[0].m_mthSetSpecialTitleValue("�� ��");
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"���>>����","���>>����", "���>>����","���>>����", "���>>Ѫѹ"},
				new string[]{"���أ�","���£�","������","������","Ѫѹ��"});

			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"���>>����","���>>Ӫ��", "���>>��ɫ","���>>���","���>>��ʶ","���>>����", "���>>�ڿ�"},
				new string[]{"\n������","Ӫ����","��ɫ��","��礣�","\n��ʶ��","���飺","�ڿʣ�"});
			
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"���>>ĩ��΢Ѫ�ܳ�ӯ","���>>�㶯������","���>>�㱳��������"},
				new string[]{"\nĩ��΢Ѫ�ܳ�ӯ��","�㶯��������","�㱳����������"});

			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"���>>ͷ�澱","���>>�ز�","���>>����", "���>>������֫"},
				new string[]{"\nͷ�澱��","\n�ز���","\n������","\n������֫��"});

			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"","���>>���ⲿλú��>>��Ĥ","���>>���ⲿλú��>>����","���>>���ⲿλú��>>��ë�ս�","���>>���ⲿλú��>>��˻","���>>���ⲿλú��>>����������"},
				new string[]{"\n���ⲿλú�ˣ�","��Ĥ��","������","��ë�ս���","\n��˻��","���������裺"});

			#endregion

			#region �������
//			m_objPrintMultiItemArr[1].m_IntPrintXPos = 360;
//			m_objPrintMultiItemArr[1].m_IntPrintwidth = 200;
//			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"","","","�������>>���������"
//																		 ,"","�������>>���","","�������>>ǳ���"
//																	 ,"�������>>����","","","�������>>���"
//																	 ,"","","",""},
//				new string[]{"\n������ϣ�","\n�� ����ԭ��","\n�� �����������","#%",
//								"\n�� ��ȣ�","#%","\n�� ǳ��ȣ�","#%","\n�� ���ȣ�","#%","\n�� ��ȣ�","#%",
//								"\n�� ���������ˣ�","\n�� �ϲ��ˡ��ж���","\n�� ������","\n�� ǩ  ����"});
//
//			#endregion
//
//			#region ������
//			m_objPrintMultiItemArr[2].m_IntPrintXPos = 590;
//			m_objPrintMultiItemArr[2].m_IntPrintwidth = 120;
//			m_objPrintMultiItemArr[2].m_mthSetPrintValue(new string[]{"","������>>����ԭ��","������>>���������","������>>���������"
//																		 ,"������>>���","������>>���","������>>ǳ���","������>>ǳ���"
//																		 ,"������>>����","������>>����","������>>���","������>>���"
//																		 ,"������>>����������","������>>�ϲ��ˡ��ж�","������>>����","������>>ǩ��"},
//				new string[]{"\n�����ϣ�","\n","\n","#%","\n","#%","\n","#%","\n","#%","\n","#%","\n","\n","\n","\n"});

			#endregion
			
		}


		#region print Class

		
		/// <summary>
		/// ��Ŀ��ӡ
		/// </summary>
		private class clsPrintInPatMedRecItem : clsIMR_PrintLineBase
		{
			#region Define
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

			private int m_intPrintXPos = 0;
			private int m_intPrintwidth = 0;
			#endregion

			public clsPrintInPatMedRecItem()
			{
				m_intPrintXPos = m_intRecBaseX;
				m_intPrintwidth = (int)enmRectangleInfoInPatientCaseInfo.PrintWidth;
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_blnNoContent == true && m_blnNoPrint == true || m_hasItems == null)
				{
					m_blnHaveMoreLine = false;

					return;
				}
				if(m_blnIsFirstPrint)
				{
					if(m_strTitle != "" && m_objItemContent != null)
					{
						p_objGrp.DrawString(m_strTitle,p_fntNormalText,Brushes.Black,m_intPrintXPos+10,p_intPosY);
						p_intPosY += 20;
						m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objItemContent.m_strItemContent==null ? "" : m_objItemContent.m_strItemContent)  ,(m_objItemContent.m_strItemContentXml==null ? "<root />" : m_objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,m_objItemContent != null);
						m_mthAddSign2(m_strTitle,m_objPrintContext.m_ObjModifyUserArr);
					}
					else
					{
						if(m_strSpecialTitle != "")
						{
							p_intPosY += 20;
							p_objGrp.DrawString(m_strSpecialTitle,clsIMR_BurnSuergeryPrintTool.m_fotItemHead,Brushes.Black,m_intPrintXPos+300,p_intPosY);
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
						m_objPrintContext.m_mthPrintLine(m_intPrintwidth,m_intPrintXPos+50,p_intPosY,p_objGrp);
					else
						m_objPrintContext.m_mthPrintLine(m_intPrintwidth+40,m_intPrintXPos+10,p_intPosY,p_objGrp);
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
		/// ����ʱ������Ժʱ��
		/// </summary>
		private class clsPrintSubTime : clsIMR_PrintLineBase
		{
			private clsInpatMedRec_Item[] m_objContentArr;
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				m_objContentArr = m_objGetContentFromItemArr(new string[]{"����ʱ��","��Ժʱ��","�˺�����","�˺�ʱ��"});
				if(m_objContentArr == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				p_intPosY += 10;
				if(m_objContentArr[0] != null)
					if(m_objContentArr[0].m_strItemContent != null && m_objContentArr[0].m_strItemContent != "")
						p_objGrp.DrawString("����ʱ�䣺" +m_mthSetDateTimeFormat( m_objContentArr[0].m_strItemContent),p_fntNormalText,Brushes.Black,m_intRecBaseX+30,p_intPosY);
				p_intPosY += 25;
				if(m_objContentArr[1] != null)
					if(m_objContentArr[1].m_strItemContent != null && m_objContentArr[1].m_strItemContent != "")
						p_objGrp.DrawString("��Ժʱ�䣺" +m_mthSetDateTimeFormat( m_objContentArr[1].m_strItemContent),p_fntNormalText,Brushes.Black,m_intRecBaseX+30,p_intPosY);
				p_objGrp.DrawString("(�˺�",p_fntNormalText,Brushes.Black,m_intRecBaseX+300,p_intPosY);
				if(m_objContentArr[2] != null)
					if(m_objContentArr[2].m_strItemContent != null && m_objContentArr[2].m_strItemContent != "")
						p_objGrp.DrawString(m_objContentArr[2].m_strItemContent + "��",p_fntNormalText,Brushes.Black,m_intRecBaseX+360,p_intPosY);
				if(m_objContentArr[3] != null)
					if(m_objContentArr[3].m_strItemContent != null && m_objContentArr[3].m_strItemContent != "")
						p_objGrp.DrawString(m_objContentArr[3].m_strItemContent + "Сʱ)",p_fntNormalText,Brushes.Black,m_intRecBaseX+410,p_intPosY);
				p_intPosY += 25;
				m_blnHaveMoreLine = false;
			}
			public override void m_mthReset()
			{
			
			}
			private string m_mthSetDateTimeFormat(string p_strDataTime)
			{
				DateTime dtTime = DateTime.Parse(p_strDataTime);
				return dtTime.ToString("yyyy��MM��dd��") + dtTime.Hour + "ʱ" +dtTime.Minute + "��";
			}
		}

	

		/// <summary>
		/// ���,�������,�����ϴ�ӡ
		/// </summary>
		private class clsPrintSubInf : clsIMR_PrintLineBase
		{
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private clsInpatMedRec_Item[] m_objItemArr = null;
			private clsInpatMedRec_Item[] m_objFirstArr = null;
			private clsInpatMedRec_Item[] m_objLastArr = null;
			private int m_intYPos = 10;
			private int m_intXPos = (int)enmRectangleInfo.LeftX+10;
			private bool[] m_blnPrintCol = new Boolean[]{true,true,true,true,true,true,true};

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_blnIsFirstPrint)
				{
					m_objItemArr = m_objGetContentFromItemArr(new string[]{"��Ա�����������>>ͷ����","��Ա�����������>>����","��Ա�����������>>˫�ϱ�","��Ա�����������>>˫ǰ��","��Ա�����������>>˫��","��Ա�����������>>˫��","��Ա�����������>>�Ȳ�"});
					m_objFirstArr = m_objGetContentFromItemArr(new string[]{"�������>>����ԭ��","�������>>���������","�������>>���","�������>>ǳ���","�������>>����","�������>>���","�������>>����������","�������>>�ϲ��ˡ��ж�","�������>>����","�������>>ǩ��","�������>>�����������"});
					m_objLastArr = m_objGetContentFromItemArr(new string[]{"������>>����ԭ��","������>>���������","������>>���","������>>ǳ���","������>>����","������>>���","������>>����������","������>>�ϲ��ˡ��ж�","������>>����","������>>ǩ��","������>>����������"});
					if(m_objItemArr == null && m_objFirstArr == null && m_objLastArr == null  || m_hasItems == null)
					{
						m_blnHaveMoreLine = false;
						return;
					}
				}
				#region Printting
				if(m_blnPrintCol[0] == true)
				{
					if(m_blnCheckBottom(ref p_intPosY,p_objGrp, p_fntNormalText,130))
					{
						m_intYPos = 155;
						return;
					}
					else if(m_blnIsFirstPrint == true)
					{
						m_mthDrawTitle(p_intPosY, p_objGrp, p_fntNormalText);
						p_objGrp.DrawString("������ϣ�",p_fntNormalText,Brushes.Black,m_intXPos+360,p_intPosY+5);
						p_objGrp.DrawString("�����ϣ�",p_fntNormalText,Brushes.Black,m_intXPos+590,p_intPosY+5);
						p_intPosY += 45;
					}
					p_objGrp.DrawString("ͷ     6",p_fntNormalText,Brushes.Black,m_intXPos,m_intYPos+2);
					p_objGrp.DrawString("��     3",p_fntNormalText,Brushes.Black,m_intXPos,m_intYPos+52);
					p_objGrp.DrawString("}",new Font("SimSun",44),Brushes.Black,m_intXPos+60,m_intYPos+4);
					p_objGrp.DrawString("9��(12������)",p_fntNormalText,Brushes.Black,m_intXPos+85,m_intYPos+27);
					p_objGrp.DrawString((m_objItemArr[0] == null?"":(m_objItemArr[0].m_strItemContent == null?"":m_objItemArr[0].m_strItemContent)),p_fntNormalText,Brushes.Black,m_intXPos+240,m_intYPos+27);
					p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos,m_intXPos,m_intYPos+75);
					p_objGrp.DrawLine(Pens.Black,m_intXPos+200,m_intYPos,m_intXPos+200,m_intYPos+75);
					p_objGrp.DrawLine(Pens.Black,m_intXPos+300,m_intYPos,m_intXPos+300,m_intYPos+75);
					string strTemp = "����ԭ��";
					m_mthPrintDioa(ref p_intPosY, p_objGrp, p_fntNormalText,strTemp,(m_objFirstArr[0] == null?"":m_objFirstArr[0].m_strItemContent),(m_objLastArr[0] == null?"":m_objLastArr[0].m_strItemContent));
					strTemp = "�����������";
					m_mthPrintDioa(ref p_intPosY, p_objGrp, p_fntNormalText,strTemp,(m_objFirstArr[1] == null?"":m_objFirstArr[1].m_strItemContent),(m_objLastArr[1] == null?"":m_objLastArr[1].m_strItemContent));
					m_intYPos += 75;
					m_blnPrintCol[0] = false;
				}
				string[] strTempArr = {"","��ȣ�","ǳ��ȣ�","���ȣ�","��ȣ�","���������ˣ�"};
				string[] strTextArr = {"","��  ��27(������1��)","˫�ϱ� 7","˫ǰ�� 6","˫  �� 5","˫  �� 5"};
				for(int i=1;i<m_blnPrintCol.Length-1;i++)
				{
					if(m_blnPrintCol[i] == true)
					{
						if(m_blnCheckBottom(ref p_intPosY,p_objGrp, p_fntNormalText,40))
						{
							p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos,m_intXPos+300,m_intYPos);
							m_intYPos = 155;
							return;
						}
						else if(m_blnIsFirstPrint == true)
							m_mthDrawTitle(p_intPosY, p_objGrp, p_fntNormalText);
						p_objGrp.DrawString(strTextArr[i],p_fntNormalText,Brushes.Black,m_intXPos,m_intYPos+2);
						p_objGrp.DrawString((m_objItemArr[i] == null?"":(m_objItemArr[i].m_strItemContent == null?"":m_objItemArr[i].m_strItemContent)),p_fntNormalText,Brushes.Black,m_intXPos+240,m_intYPos+2);
						p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos,m_intXPos,m_intYPos+25);
						p_objGrp.DrawLine(Pens.Black,m_intXPos+200,m_intYPos,m_intXPos+200,m_intYPos+25);
						p_objGrp.DrawLine(Pens.Black,m_intXPos+300,m_intYPos,m_intXPos+300,m_intYPos+25);
						m_mthPrintDioa(ref p_intPosY, p_objGrp, p_fntNormalText,strTempArr[i],(m_objFirstArr[i+1] == null?"":m_objFirstArr[i+1].m_strItemContent),(m_objLastArr[i+1] == null?"":m_objLastArr[i+1].m_strItemContent));
						m_intYPos += 25;
						m_blnPrintCol[i] = false;
					}
				}
				if(m_blnPrintCol[6] == true)
				{
					if(m_blnCheckBottom(ref p_intPosY,p_objGrp, p_fntNormalText,75))
					{
						p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos,m_intXPos+300,m_intYPos);
						m_intYPos = 155;
						return;
					}
					else if(m_blnIsFirstPrint == true)
						m_mthDrawTitle(p_intPosY, p_objGrp, p_fntNormalText);
					p_objGrp.DrawString("˫����21",p_fntNormalText,Brushes.Black,m_intXPos,m_intYPos+2);
					p_objGrp.DrawString("˫С��13",p_fntNormalText,Brushes.Black,m_intXPos,m_intYPos+27);
					p_objGrp.DrawString("˫  �� 7",p_fntNormalText,Brushes.Black,m_intXPos,m_intYPos+52);
					p_objGrp.DrawString("}",new Font("SimSun",44),Brushes.Black,m_intXPos+60,m_intYPos+4);
					p_objGrp.DrawString("41-(12-����)",p_fntNormalText,Brushes.Black,m_intXPos+85,m_intYPos+27);
					p_objGrp.DrawString((m_objItemArr[6] == null?"":(m_objItemArr[6].m_strItemContent == null?"":m_objItemArr[6].m_strItemContent)),p_fntNormalText,Brushes.Black,m_intXPos+240,m_intYPos+27);
					p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos,m_intXPos,m_intYPos+75);
					p_objGrp.DrawLine(Pens.Black,m_intXPos+200,m_intYPos,m_intXPos+200,m_intYPos+75);
					p_objGrp.DrawLine(Pens.Black,m_intXPos+300,m_intYPos,m_intXPos+300,m_intYPos+75);
					string strTemp3 = "�ϲ��ˡ��ж���";
					m_mthPrintDioa(ref p_intPosY, p_objGrp, p_fntNormalText,strTemp3,(m_objFirstArr[7] == null?"":m_objFirstArr[7].m_strItemContent),(m_objLastArr[7] == null?"":m_objLastArr[7].m_strItemContent));
					strTemp3 = "������";
					m_mthPrintDioa(ref p_intPosY, p_objGrp, p_fntNormalText,strTemp3,(m_objFirstArr[8] == null?"":m_objFirstArr[8].m_strItemContent),(m_objLastArr[8] == null?"":m_objLastArr[8].m_strItemContent));
					strTemp3 = "ǩ����";
					m_mthPrintDioa(ref p_intPosY, p_objGrp, p_fntNormalText,strTemp3,(m_objFirstArr[9] == null?"":m_objFirstArr[9].m_strItemContent),(m_objLastArr[9] == null?"":m_objLastArr[9].m_strItemContent));
					strTemp3 = "";
					m_mthPrintDioa(ref p_intPosY, p_objGrp, p_fntNormalText,strTemp3,(m_objFirstArr[10] == null?"":m_mthSetDateTimeFormat(m_objFirstArr[10].m_strItemContent,true)),(m_objLastArr[10] == null?"":m_mthSetDateTimeFormat(m_objLastArr[10].m_strItemContent,false)));
					m_intYPos += 75;
					p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos,m_intXPos+300,m_intYPos);
					m_blnPrintCol[6] = false;
				}
				p_intPosY = m_intYPos > p_intPosY ? m_intYPos+20 : p_intPosY+20;
				#endregion
				m_blnHaveMoreLine = false;
			}
			/// <summary>
			/// ������ڴ�ӡ��ʽ
			/// </summary>
			/// <param name="p_strDataTime"></param>
			/// <param name="p_blnText"></param>
			/// <returns></returns>
			private string m_mthSetDateTimeFormat(string p_strDataTime,bool p_blnText)
			{
				if(p_strDataTime == null)
					return "";
				DateTime dtTime = DateTime.Parse(p_strDataTime);
				return dtTime.ToString("yyyy��MM��dd��") +( p_blnText ?dtTime.Hour + "ʱ":"");
			}
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_blnIsFirstPrint = true;
			}
			/// <summary>
			/// ��ӡ����
			/// </summary>
			/// <param name="p_intPosY"></param>
			/// <param name="p_objGrp"></param>
			/// <param name="p_fntNormalText"></param>
			private void m_mthDrawTitle(int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				m_intYPos = p_intPosY+10;
				RectangleF rtgf = new RectangleF(m_intXPos,m_intYPos,100,45);
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos,m_intXPos+300,m_intYPos);
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos,m_intXPos,m_intYPos+45);
				p_objGrp.DrawLine(Pens.Black,m_intXPos+100,m_intYPos,m_intXPos+100,m_intYPos+45);
				p_objGrp.DrawLine(Pens.Black,m_intXPos+200,m_intYPos,m_intXPos+200,m_intYPos+45);
				p_objGrp.DrawLine(Pens.Black,m_intXPos+300,m_intYPos,m_intXPos+300,m_intYPos+45);
				p_objGrp.DrawLine(Pens.Black,m_intXPos,m_intYPos+45,m_intXPos+300,m_intYPos+45);
				p_objGrp.DrawString("���˸�����������",p_fntNormalText,Brushes.Black,rtgf);
				rtgf.X = m_intXPos+100;
				p_objGrp.DrawString("С��������������",p_fntNormalText,Brushes.Black,rtgf);
				rtgf.X = m_intXPos+200;
				p_objGrp.DrawString("��Ա�������������",p_fntNormalText,Brushes.Black,rtgf);
				m_intYPos += 45;
				m_blnIsFirstPrint = false;
			}
			/// <summary>
			/// ����Ƿ���Ҫ��ҳ
			/// </summary>
			/// <param name="p_intPosY"></param>
			/// <param name="p_objGrp"></param>
			/// <param name="p_fntNormalText"></param>
			/// <param name="p_intHeight"></param>
			/// <returns></returns>
			private bool m_blnCheckBottom(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText,int p_intHeight)
     		{
				if(m_intYPos+p_intHeight+20 > ((int)enmRectangleInfo.BottomY -50))
				{
					m_blnHaveMoreLine = true;
					m_blnIsFirstPrint = true;
					p_intPosY += 500;
					return true;
				}
				return false;
			}
			/// <summary>
			/// ��ϴ�ӡ
			/// </summary>
			/// <param name="p_intPosY"></param>
			/// <param name="p_objGrp"></param>
			/// <param name="p_fntNormalText"></param>
			/// <param name="p_strTextArr">����</param>
			/// <param name="p_strFirstCont">�������</param>
			/// <param name="p_strLastCont">������</param>
			private void m_mthPrintDioa(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText,string p_strText,string p_strFirstCont,string p_strLastCont)
			{
				if(p_strFirstCont == null)
					return;
				int intTemp = 0;
				RectangleF rtg = new RectangleF(m_intXPos+380,p_intPosY,200,20);
				string strText = p_strText+p_strFirstCont;
				SizeF szfText = p_objGrp.MeasureString(strText,p_fntNormalText,Convert.ToInt32(rtg.Width));
				rtg.Height = szfText.Height+5;
				rtg.Y = p_intPosY;
				p_objGrp.DrawString(strText,p_fntNormalText,Brushes.Black,rtg);
				intTemp += Convert.ToInt32(rtg.Height);
			
				rtg = new RectangleF(m_intXPos+590,p_intPosY,140,20);
				strText = (p_strLastCont ==null?"":p_strLastCont);
				szfText = p_objGrp.MeasureString(strText,p_fntNormalText,Convert.ToInt32(rtg.Width));
				rtg.Height = szfText.Height+5;
				rtg.Y = p_intPosY;
				p_objGrp.DrawString(strText,p_fntNormalText,Brushes.Black,rtg);
				if(intTemp > Convert.ToInt32(rtg.Height))
					p_intPosY += intTemp;
				else
					p_intPosY += Convert.ToInt32(rtg.Height);
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
                if (objSignContent == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                p_intPosY += 40;
                for (int i = 0; i < objSignContent.Length; i++)
                {
                    if (m_strTitleArr[i].IndexOf("����") < 0)
                    {
                        p_objGrp.DrawString(m_strTitleArr[i] + (objSignContent[i] == null ? "" : objSignContent[i].m_strItemContent), p_fntNormalText, Brushes.Black, m_intRecBaseX + 500, p_intPosY);
                        p_intPosY += 20;
                    }
                    else
                    {
                        p_objGrp.DrawString(m_strTitleArr[i] + (objSignContent[i] == null ? "" : DateTime.Parse(objSignContent[i].m_strItemContent).ToString("yyyy��MM��dd��")), p_fntNormalText, Brushes.Black, m_intRecBaseX + 500, p_intPosY);
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
            public void m_mthSetPrintSignValue(string[] p_strkeyArr, string[] p_strTitleArr)
            {
                if (p_strkeyArr == null || p_strTitleArr == null || p_strkeyArr.Length != p_strTitleArr.Length)
                    return;
                objSignContent = m_objGetContentFromItemArr(p_strkeyArr);
                m_strTitleArr = p_strTitleArr;
            }


        }
		#endregion
	
	}
}
