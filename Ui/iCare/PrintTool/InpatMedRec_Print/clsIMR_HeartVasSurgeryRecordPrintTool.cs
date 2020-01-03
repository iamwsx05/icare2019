using System;
using System.IO;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;
using System.Windows.Forms;
using System.Resources ;
using RS = iCare.Properties;

namespace iCare
{
	/// <summary>
	/// ���Ǻ��סԺ������ӡ ��ժҪ˵����
	/// </summary>
	public class clsIMR_HeartVasSurgeryRecordPrintTool: clsInpatMedRecPrintBase
	{
		public clsIMR_HeartVasSurgeryRecordPrintTool(string p_strTypeID) :base(p_strTypeID)
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
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
																		   new clsPrintPatientFixInfo("��Ѫ�������Ժ��¼",295),
																		   m_objPrintOneItemArr[0],m_objPrintOneItemArr[1],
																		   m_objPrintMultiItemArr[0],m_objPrintMultiItemArr[1],
                                                                           m_objPrintMultiItemArr[19],
                                                                           m_objPrintMultiItemArr[2],m_objPrintMultiItemArr[18],m_objPrintMultiItemArr[3],m_objPrintMultiItemArr[4],
                                                                           m_objPrintMultiItemArr[5],m_objPrintMultiItemArr[6],m_objPrintMultiItemArr[7],
                                                                           m_objPrintMultiItemArr[8],m_objPrintMultiItemArr[9],m_objPrintMultiItemArr[10],
                                                                           m_objPrintMultiItemArr[11],m_objPrintMultiItemArr[12],m_objPrintMultiItemArr[13],
                                                                           m_objPrintMultiItemArr[14],m_objPrintMultiItemArr[15],m_objPrintMultiItemArr[20],m_objPrintMultiItemArr[16],
                                                                           m_objPrintMultiItemArr[17],m_objPrintOneItemArr[2],m_objPrintOneItemArr[3],
                                                                           m_objPrintMultiItemArr[21],m_objPrintMultiItemArr[22]
																	   });
		}

		private void m_mthInitPrintLineArr()
		{
			  m_objPrintOneItemArr = new clsPrintInPatMedRecItem[4];
			for(int i1=0;i1<m_objPrintOneItemArr.Length;i1++)
				m_objPrintOneItemArr[i1] = new clsPrintInPatMedRecItem();

			m_objPrintMultiItemArr = new clsPrintInPatMedRecItem[23];
			for(int j2=0;j2<m_objPrintMultiItemArr.Length;j2++)
				m_objPrintMultiItemArr[j2] = new clsPrintInPatMedRecItem();
 
			m_objPrintSignArr = new clsPrintInPatMedRecSign[1];
			for(int k3=0;k3<m_objPrintSignArr.Length;k3++)
				m_objPrintSignArr[k3] = new clsPrintInPatMedRecSign();
		}
		
		protected override void m_mthSetSubPrintInfo()
		{

			#region ����-�ֲ�ʷ-���Ǻ�ר�Ƽ��
			m_objPrintOneItemArr[0].m_mthSetPrintValue("����","��  �ߣ�");
			m_objPrintOneItemArr[1].m_mthSetPrintValue("�ֲ�ʷ","�ֲ�ʷ��");
            m_objPrintOneItemArr[2].m_mthSetPrintValue("�������", "������飺");
            m_objPrintOneItemArr[3].m_mthSetPrintValue("�������", "������ϣ�");

			#endregion	
            #region ��ȥʷ
            m_objPrintMultiItemArr[0].m_mthSetPrintValue("","��ȥʷ��");
            m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[] { "��ȥʷ>>��Ⱦ������Ⱦ���Ӵ�ʷ", "��ȥʷ>>���ƻ�Ԥ������", 
                "��ȥʷ>>ҩ�Ｐʳ�����ʷ", "��ȥʷ>>����������ʷ", "��ȥʷ>>��Ѫ��Ѫ��Ʒʷ", "��ȥʷ>>����" },
                new string[] { "��Ⱦ�������Բ�ʷ��$$", "\n���ƻ�Ԥ�����֣�$$", "\nҩ���ʳ�����ʷ��$$", "\n����������ʷ��$$", 
                    "\n��Ѫ��Ѫ��Ʒʷ��$$", "\n������$$" });
            #endregion
            #region ����ʷ
            m_objPrintMultiItemArr[1].m_mthSetPrintValue("","����ʷ��");
            m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[] { "����ʷ>>������", "����ʷ>>�ط������", "����ʷ>>ϰ�߼��Ⱥ�",
                "����ʷ>>��ˮ�Ӵ�ʷ", "����ʷ>>ʳ����ʷ", "����ʷ>>����" },
                new string[] { "�����أ�$$", "�ط���������ס�����", "\n�����Ⱥã�$$", "\n��ˮ�Ӵ�ʷ��$$", "ʳ����ʷ��", "\n������$$" });
            #endregion
            #region ����ʷ
            m_objPrintMultiItemArr[19].m_mthSetPrintValue("", "����ʷ��");
            m_objPrintMultiItemArr[19].m_mthSetPrintValue(new string[] { "����ʷ>>�������", "����ʷ>>��ż���" },
                new string[] { "������䣺$$", "��ż�����"});
            #endregion
            #region ����ʷ
            m_objPrintMultiItemArr[2].m_mthSetPrintValue("","����ʷ��");
            m_objPrintMultiItemArr[2].m_mthSetPrintValue(new string[] { "����ʷ>>��", "����ʷ>>ĸ"},
                new string[] { "����$$", "\nĸ��$$" });
            #endregion
            #region �����
            #region ����
            m_objPrintMultiItemArr[3].m_mthSetSpecialTitleValue("�� �� �� ��");
			m_objPrintMultiItemArr[3].m_mthSetPrintValue(new string[]{"һ�����>>T","һ�����>>T",
																		 "һ�����>>P","һ�����>>P",
																		 "һ�����>>R","һ�����>>R",
																		 "һ�����>>BP","һ�����>>BP",
                                                                         "һ�����>>BP>>mmHg", "һ�����>>BP>>mmHg",
                                                                         },
                new string[] { "     T��", "#��", "              P��$$", "#��/��", "              R��$$", "#��/��", "              BP��$$", "#/","$$","#mmHg"});
            #endregion
            #region һ�����
            m_objPrintMultiItemArr[4].m_mthSetPrintValue("","һ�������");
            m_objPrintMultiItemArr[4].m_mthSetPrintValue(new string[]{"�����>>����","�����>>��λ","�����>>����","�����>>Ӫ��",
																		 "�����>>���ݱ���"},
                new string[] {  "��־��$$", "��λ��", "������", "Ӫ����", "���ݣ�" });
            #endregion
            #region Ƥ��
        m_objPrintMultiItemArr[5].m_mthSetPrintValue("", "Ƥ  ����");
        m_objPrintMultiItemArr[5].m_mthSetPrintValue(new string[]{"�����>>ճĤ","�����>>���","�����>>��Ⱦ","�����>>�԰�",
																	 "�����>>��Ѫ�㼰��λ"},
            new string[] { "ճĤ��$$", "���(ʮһ)��", "��Ⱦ(ʮһ)��", "�԰�(ʮһ)��", "��Ѫ�㼰��λ��" });
            #endregion
            #region �ܰͽ�
        m_objPrintMultiItemArr[6].m_mthSetPrintValue("", "�ܰͽ᣺");
        m_objPrintMultiItemArr[6].m_mthSetPrintValue(new string[]{"�����>>�ܰͽ�"},
            new string[] { "ȫ��ǳ���ܰͽ᣺" });
        #endregion
            #region ͷ��
        m_objPrintMultiItemArr[7].m_mthSetPrintValue("", "ͷ  ����");
        m_objPrintMultiItemArr[7].m_mthSetPrintValue(new string[]{"�����>>ͷ­","�����>>��","�����>>ͫ��","�����>>��","�����>>��",
																	 "�����>>��","�����>>��","�����>>��","�����>>��","�����>>������"},
            new string[] { "ͷ­��$$", "�ۣ�", "ͫ�ף�", "����", "�ǣ�","����","�ʣ�","�ࣺ","�ݣ�","�����壺" });
        #endregion
            #region ����
        m_objPrintMultiItemArr[8].m_mthSetPrintValue("", "��  ����");
        m_objPrintMultiItemArr[8].m_mthSetPrintValue(new string[]{"�����>>����","�����>>���ֿ�","�����>>������ŭ��","�����>>�ξ���",
																	 "�����>>����������","�����>>Ѫ������","�����>>��״��"},
            new string[] { "���ܣ�$$", "���ֿ�(ʮһ)��", "������ŭ��(ʮһ)��", "�ξ���(ʮһ)��", "������������", "Ѫ��������", "��״�٣�" });
            #endregion
            #region �ز�
        m_objPrintMultiItemArr[9].m_mthSetPrintValue("", "��  ����");
        m_objPrintMultiItemArr[9].m_mthSetPrintValue(new string[]{"�����>>�ز�","�����>>����","�����>>����","�ز�>>����"},
            new string[] { "������$$", "\n    �Σ�$$", "\n    �ģ�$$", "\n������$$"});
        #endregion
            #region ����
        m_objPrintMultiItemArr[10].m_mthSetPrintValue("", "��  ����");
        m_objPrintMultiItemArr[10].m_mthSetPrintValue(new string[]{"�����>>����","�����>>�����","�����>>ѹʹ","�����>>����ʹ",
																	 "�����>>��ˮ��","�����>>��","�����>>����","�����>>Ƣ","�����>>��",
                                                   "�����>>�����","�����>>�����׿�","�����>>����Ѫ�ܲ���","�����>>Ѫ������","�����>>����","����>>����" },
            new string[] { "���Σ�$$", "����ȣ�", "ѹʹ��", "����ʹ��", "��ˮ��(ʮһ)��", "�Σ�", "���ң�", "Ƣ��", "����", "����ܣ�",
                            "�����׿飺","����Ѫ�ܲ�����","Ѫ��������","������","������"});
        #endregion
            #region ���ż�����ֳ��
        m_objPrintMultiItemArr[11].m_mthSetPrintValue("", "���ż�����ֳ����");
        m_objPrintMultiItemArr[11].m_mthSetPrintValue(new string[]{"�����>>���ż�����ֳ��"},
            new string[] { "" });
           #endregion
            #region ��������֫
        m_objPrintMultiItemArr[12].m_mthSetPrintValue("", "��������֫��");
        m_objPrintMultiItemArr[12].m_mthSetPrintValue(new string[]{"�����>>��״ָ","�����>>��֫����","�����>>������","�����>>����",
																	 "�����>>����","�����>>�ؽ�","�����>>֫�����"},
            new string[] { "��״ָ/ֺ(ʮһ)��$$", "��֫����(ʮһ)��", "��������", "������", "#��$$","�ؽڣ�","֫����Σ�" });
        #endregion
            #region �񾭷���
        m_objPrintMultiItemArr[13].m_mthSetPrintValue("", "�񾭷��䣺");
        m_objPrintMultiItemArr[13].m_mthSetPrintValue(new string[] { "�����>>�񾭷���" },
            new string[] { "" });
        #endregion
            #region ����
        m_objPrintMultiItemArr[14].m_mthSetPrintValue("", "��  �");
        m_objPrintMultiItemArr[14].m_mthSetPrintValue(new string[]{"�����>>��ǰ��¡��","�����>>�ļⲫ��λ��","�����>>������λ����"},
            new string[] { "��ǰ��¡��(ʮһ)��$$", "�ļⲫ��λ�ã�", "\n������λ������$$" });
        #endregion
            #region ����
        m_objPrintMultiItemArr[15].m_mthSetPrintValue("", "��  �");
        m_objPrintMultiItemArr[15].m_mthSetPrintValue(new string[] { "����>>�ļⲫ��λ��", "�����>>̧���Բ���", "�����>>���λ��", "�����>>ʱ��", "�����>>�İ�Ħ����", "����>>ʱ��" },
            new string[] { "�ļⲫ��λ�ã�$$", "̧���Բ�����", "\n���λ�ã�$$","ʱ�ڣ�","\n�İ�Ħ����(ʮһ)λ�ã�$$","ʱ�ڣ�" });
        #endregion
            #region ����
        m_objPrintMultiItemArr[16].m_mthSetPrintValue("", "��  �");
        m_objPrintMultiItemArr[16].m_mthSetPrintValue(new string[] { "�����>>����", "�����>>����","�����>>����", "�����>>��һ��", //4
            "�����>>��һ��>>���", "�����>>�ڶ���", "�����>>�ڶ���>>���","�����>>�ڶ���>>����",//9
            "�����>>������","�����>>������>>����","�����>>������","�����>>������>>������","�����>>������",//15
            "�����>>������>>��","�����>>������>>��λ","�����>>������>>��","�����>>������>>����","�����>>�������>>������",//20
            "�����>>�������>>����","�����>>�������>>����","�����>>�������>>����","�����>>�������>>������","�����>>�������>>����1","�����>>�������>>����2","�����>>�������>>����2",//27
            "�����>>����������>>������","�����>>����������>>����","�����>>����������>>����","�����>>����������>>����","�����>>����������>>������","�����>>����������>>����1",//33
            "�����>>����������>>����1","�����>>����������>>����1","�����>>�ζ�������","�����>>�������","�����>>�ع���Ե","�����>>�ع���Ե>>ʱ�估����","�����>>�İ�Ħ����" },//40
            new string[] { "���ʣ�$$", "#��/��$$","���ɣ�", "\n��������һ����$$", "������ȣ�$$", "\n            �ڶ�����", "������ȣ�A2($$",")P2�����ѣ�$$",//8
                "\n            ��������$$","�������ʣ�$$","\n            ��������$$","����������(ʮһ)��$$","\n            �����ɣ�$$","����$$","�ڣ���������$$",//17
            "��λ��$$","�ڣ����ʣ�$$","\n��������������������ڣ�$$","�������ʣ�$$","��","#����$$","\n                                 �����ڣ�$$","��������$$","��","#����$$",//27
            "\n        �����������������ڣ�$$","�������ʣ�$$","��","#����$$","\n                                 �����ڣ�$$","�������ʣ�$$","��","#����$$","\n            �ζ�������$$","\n            ���������$$","\n   �ع���Ե��$$",//38
            "�߼䣬ʱ�ڼ����ʣ�$$","\n  �İ�Ħ����(ʮһ)��$$"});
            #endregion
            #region ��ΧѪ����
            m_objPrintMultiItemArr[17].m_mthSetPrintValue("", "��ΧѪ������");
            m_objPrintMultiItemArr[17].m_mthSetPrintValue(new string[] { "�����>>ëϸѪ�ܲ�����", "�����>>ˮ����", "�����>>ǹ����", "�����>>������׾", "�����>>����", "�����>>ëϸѪ����>>����" },
                new string[] { "ëϸѪ�ܲ�������$$", "ˮ������", "ǹ������", "������׾��", "������", "\n������" });
            #endregion
            #region �¾�����ʷ
            m_objPrintMultiItemArr[18].m_mthSetPrintValue("", "�¾�����ʷ��");
            m_objPrintMultiItemArr[18].m_mthSetPrintValue(new string[] { "�����>>�¾�����ʷ" },
                new string[] { "" });
            #endregion
            #region ߵ��
            m_objPrintMultiItemArr[20].m_mthSetPrintValue("", "ߵ  ����������(����ͼ)");
            #endregion
			#endregion
            #region ǩ��������
            string strDoctor = "";
            string strDirectorDoctor = "";
            if (m_hasItems != null)
            {
                if (m_hasItems.Contains("ҽʦ"))
                {
                    strDoctor = "ҽʦ��" + ((clsInpatMedRec_Item)m_hasItems["ҽʦ"]).m_strItemContent + "\n" + ((clsInpatMedRec_Item)m_hasItems["ҽʦǩ������"]).m_strItemContent;
                } if (m_hasItems.Contains("����ҽʦ"))
                {
                    strDirectorDoctor = "���λ�����ҽʦ��" + ((clsInpatMedRec_Item)m_hasItems["����ҽʦ"]).m_strItemContent + "\n" + ((clsInpatMedRec_Item)m_hasItems["����ҽʦǩ������"]).m_strItemContent;
                }
            }
            m_objPrintMultiItemArr[21].m_mthSetPrintValue("", strDoctor);
            m_objPrintMultiItemArr[22].m_mthSetPrintValue("", strDirectorDoctor);
            //m_objPrintSignArr[0].m_mthSetPrintSignValue(new string[] { "ҽʦ", "ҽʦǩ������", "����ҽʦ", "����ҽʦǩ������" }, new string[] { "ҽʦ��", "ǩ�����ڣ�","���λ�����ҽʦ��","ǩ�����ڣ�" });
            
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
            private int intLine = 0;
            private int intOffSetX = 70;
            private int intOffSetWidth = 0;
            private int m_intLineYPos = 0;
            
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
                        if (m_strTitle == "��  �")
                        {
                            m_intLineYPos = p_intPosY;
                            m_mthDrawline(p_objGrp, p_fntNormalText);
                        }
                        if (m_objItemContent != null)
                        {
                            p_objGrp.DrawString(m_strTitle, p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                            m_objPrintContext.m_mthSetContextWithCorrectBefore(m_objItemContent.m_strItemContent, m_objItemContent.m_strItemContentXml, m_dtmFirstPrintTime, true);
                            m_mthAddSign2(m_strTitle, m_objPrintContext.m_ObjModifyUserArr);
                        }
                        else if (m_strText != "")
                        {
                            p_objGrp.DrawString(m_strTitle, p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                            m_objPrintContext.m_mthSetContextWithCorrectBefore(m_strText, m_strTextXml, m_dtmFirstPrintTime, m_blnNoPrint == false);
                            m_mthAddSign2(m_strSpecialTitle, m_objPrintContext.m_ObjModifyUserArr);
                        }
                        else if (m_strTitle.IndexOf("ҽʦ") >= 0)
                        {
                            p_intPosY += 20;
                            p_objGrp.DrawString(m_strTitle, p_fntNormalText, Brushes.Black, m_intRecBaseX + 500, p_intPosY);
                            p_intPosY += 20;
                        }
                        else
                        {
                            p_objGrp.DrawString(m_strTitle, p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                            p_intPosY += 20;
                        }
					}
					else
					{
                        p_intPosY += 20;
						if(m_strSpecialTitle != "")
						{
                            p_objGrp.DrawString(m_strSpecialTitle, clsIMR_HerbalismPrintTool.m_fotItemHead, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
							p_intPosY += 40;
						}
						m_objPrintContext.m_mthSetContextWithCorrectBefore(m_strText ,m_strTextXml,m_dtmFirstPrintTime,m_blnNoPrint == false);
						m_mthAddSign2(m_strSpecialTitle,m_objPrintContext.m_ObjModifyUserArr);
					}

					m_blnIsFirstPrint = false;					
				}
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
                    if (m_strTitle != "")
                    {
                        if (m_strTitle == "һ�������" || m_strTitle == "�񾭷��䣺" || m_strTitle == "������飺" || m_strTitle == "������ϣ�")
                        {
                            intOffSetX = 85;
                            intOffSetWidth = -20;
                        }
                        else if (m_strTitle == "���ż�����ֳ����")
                        {
                            intOffSetX = 140;
                            intOffSetWidth = -60;
                        }
                        else if (m_strTitle == "��������֫��" || m_strTitle == "��ΧѪ������")
                        {
                            intOffSetX = 105;
                            intOffSetWidth = -40;
                        }
                        m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth + intOffSetWidth, m_intRecBaseX + intOffSetX, p_intPosY, p_objGrp);
                    }
                    else
                    {
                        m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intRecBaseX + 10, p_intPosY, p_objGrp);
                    }
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
                intLine = 0;
                intOffSetX = 70;
                intOffSetWidth = 0;
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
            /// <summary>
            /// ��ӡߵ��
            /// </summary>
            /// <param name="p_objGrp"></param>
            /// <param name="p_fntNormalText"></param>
            private void m_mthDrawline(System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                clsInpatMedRec_Item[] objItemContentArr = null;
                clsInpatMedRec_Item objItemContent = null;
                objItemContentArr = m_objGetContentFromItemArr(new string[]{"ߵ��>>��1","ߵ��>>��2","ߵ��>>��3","ߵ��>>��4"
																		   ,"ߵ��>>��5","ߵ��>>��1","ߵ��>>��2","ߵ��>>��3"
																		   ,"ߵ��>>��4","ߵ��>>��5"});
                if (objItemContentArr != null)
                {
                    #region ��ӡ�ĵ�ͼ��
                    p_objGrp.DrawLine(Pens.Black, 625, m_intLineYPos + 20, 775, m_intLineYPos + 20);
                    p_objGrp.DrawString((objItemContentArr[0] == null ? "" : (objItemContentArr[0].m_strItemContent == null ? "" : objItemContentArr[0].m_strItemContent)), p_fntNormalText, Brushes.Black, 625, m_intLineYPos + 22);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, 687, m_intLineYPos + 22);
                    p_objGrp.DrawString((objItemContentArr[1] == null ? "" : (objItemContentArr[1].m_strItemContent == null ? "" : objItemContentArr[1].m_strItemContent)), p_fntNormalText, Brushes.Black, 716, m_intLineYPos + 22);

                    p_objGrp.DrawLine(Pens.Black, 625, m_intLineYPos + 40, 775, m_intLineYPos + 40);
                    p_objGrp.DrawString((objItemContentArr[2] == null ? "" : (objItemContentArr[2].m_strItemContent == null ? "" : objItemContentArr[2].m_strItemContent)), p_fntNormalText, Brushes.Black, 625, m_intLineYPos + 42);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, 687, m_intLineYPos + 42);
                    p_objGrp.DrawString((objItemContentArr[3] == null ? "" : (objItemContentArr[3].m_strItemContent == null ? "" : objItemContentArr[3].m_strItemContent)), p_fntNormalText, Brushes.Black, 716, m_intLineYPos + 42);

                    p_objGrp.DrawLine(Pens.Black, 625, m_intLineYPos + 60, 775, m_intLineYPos + 60);
                    p_objGrp.DrawString((objItemContentArr[4] == null ? "" : (objItemContentArr[4].m_strItemContent == null ? "" : objItemContentArr[4].m_strItemContent)), p_fntNormalText, Brushes.Black, 625, m_intLineYPos + 62);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, 687, m_intLineYPos + 62);
                    p_objGrp.DrawString((objItemContentArr[5] == null ? "" : (objItemContentArr[5].m_strItemContent == null ? "" : objItemContentArr[5].m_strItemContent)), p_fntNormalText, Brushes.Black, 716, m_intLineYPos + 62);

                    p_objGrp.DrawLine(Pens.Black, 625, m_intLineYPos + 80, 775, m_intLineYPos + 80);
                    p_objGrp.DrawString((objItemContentArr[6] == null ? "" : (objItemContentArr[6].m_strItemContent == null ? "" : objItemContentArr[6].m_strItemContent)), p_fntNormalText, Brushes.Black, 625, m_intLineYPos + 82);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, 687, m_intLineYPos + 82);
                    p_objGrp.DrawString((objItemContentArr[7] == null ? "" : (objItemContentArr[7].m_strItemContent == null ? "" : objItemContentArr[7].m_strItemContent)), p_fntNormalText, Brushes.Black, 716, m_intLineYPos + 82);

                    p_objGrp.DrawLine(Pens.Black, 625, m_intLineYPos + 100, 775, m_intLineYPos + 100);
                    p_objGrp.DrawString((objItemContentArr[8] == null ? "" : (objItemContentArr[8].m_strItemContent == null ? "" : objItemContentArr[8].m_strItemContent)), p_fntNormalText, Brushes.Black, 625, m_intLineYPos + 102);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, 687, m_intLineYPos + 102);
                    p_objGrp.DrawString((objItemContentArr[9] == null ? "" : (objItemContentArr[9].m_strItemContent == null ? "" : objItemContentArr[9].m_strItemContent)), p_fntNormalText, Brushes.Black, 716, m_intLineYPos + 102);

                    p_objGrp.DrawLine(Pens.Black, 625, m_intLineYPos + 120, 775, m_intLineYPos + 120);
                    p_objGrp.DrawLine(Pens.Black, 685, m_intLineYPos + 20, 685, m_intLineYPos + 120);
                    p_objGrp.DrawLine(Pens.Black, 715, m_intLineYPos + 20, 715, m_intLineYPos + 120);

                    if (m_hasItems != null)
                        if (m_hasItems.Contains("�����>>�ڶ���>>��������"))
                            objItemContent = m_hasItems["�����>>�ڶ���>>��������"] as clsInpatMedRec_Item;
                    p_objGrp.DrawString(objItemContent == null ? "" : "�������߾�ǰ������" + objItemContent.m_strItemContent + "cm",new Font("Simsun",9.5f), Brushes.Black, 620, m_intLineYPos + 130);
                    #endregion
                }
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
				p_intPosY += 20;
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